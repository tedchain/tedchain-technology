// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tedchain.Infrastructure;

namespace Tedchain.Server.Controllers
{
    [Route("")]
    public class TedchainController : Controller
    {
        private readonly IStorageEngine store;
        private readonly TransactionValidator validator;
        private readonly ILogger logger;

        public TedchainController(IStorageEngine store, ILogger logger, TransactionValidator validator = null)
        {
            this.store = store;
            this.validator = validator;
            this.logger = logger;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await this.store.Initialize();
            await base.OnActionExecutionAsync(context, next);
        }

        /// <summary>
        /// Submits a transaction for validation.
        /// </summary>
        /// <param name="body">
        /// The JSON object in the request body.
        /// Expected format:
        /// {
        ///   "mutation": "&lt;string>",
        ///   "signatures": [
        ///     {
        ///       "pub_key": "&lt;string>",
        ///       "signature": "&lt;string>"
        ///     },
        ///     ...
        ///   ]
        /// }
        /// </param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        [HttpPost("submit")]
        public async Task<ActionResult> Post()
        {
            if (validator == null)
                return CreateErrorResponse("ValidationDisabled");

            JObject body;
            try
            {
                string bodyContent;
                using (StreamReader streamReader = new StreamReader(Request.Body))
                    bodyContent = await streamReader.ReadToEndAsync();

                body = JObject.Parse(bodyContent);
            }
            catch (JsonReaderException)
            {
                return BadRequest();
            }

            ByteString parsedMutation;
            List<SignatureEvidence> authentication = new List<SignatureEvidence>();

            if (!(body["mutation"] is JValue && body["signatures"] is JArray))
                return BadRequest();

            try
            {
                parsedMutation = ByteString.Parse((string)body["mutation"]);

                foreach (JToken signatureItem in body["signatures"])
                {
                    JObject evidence = signatureItem as JObject;
                    if (!(evidence != null && evidence["pub_key"] is JValue && evidence["signature"] is JValue))
                        return BadRequest();

                    authentication.Add(new SignatureEvidence(
                        ByteString.Parse((string)evidence["pub_key"]),
                        ByteString.Parse((string)evidence["signature"])));
                }
            }
            catch (FormatException)
            {
                return BadRequest();
            }

            ByteString transactionId;
            try
            {
                transactionId = await validator.PostTransaction(parsedMutation, authentication);
            }
            catch (TransactionInvalidException exception)
            {
                logger.LogInformation("Rejected transaction: {0}", exception.Message);

                return CreateErrorResponse(exception.Reason);
            }

            logger.LogInformation("Validated transaction {0}", transactionId.ToString());

            return Json(new
            {
                transaction_hash = transactionId.ToString(),
                mutation_hash = new ByteString(MessageSerializer.ComputeHash(parsedMutation.ToByteArray())).ToString()
            });
        }

        private ActionResult CreateErrorResponse(string reason)
        {
            JsonResult result = Json(new
            {
                error_code = reason
            });

            result.StatusCode = (int)HttpStatusCode.BadRequest;

            return result;
        }

        /// <summary>
        /// Gets the key, value and version of a given record.
        /// </summary>
        /// <param name="key">The hex-encoded key of the record being queried.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        [HttpGet("record")]
        public async Task<ActionResult> GetRecord(
            [FromQuery(Name = "key")]
            string key)
        {
            ByteString parsedKey;

            try
            {
                parsedKey = ByteString.Parse(key ?? "");
            }
            catch (FormatException)
            {
                return BadRequest();
            }

            Record result = (await this.store.GetRecords(new[] { parsedKey })).First();

            return Json(new
            {
                key = parsedKey.ToString(),
                value = result.Value?.ToString(),
                version = result.Version.ToString()
            });
        }

        /// <summary>
        /// Gets information about the Tedchain instance.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        [HttpGet("info")]
        public ActionResult GetChainInfo()
        {
            if (validator == null)
                return Json(new { });
            else
                return Json(new
                {
                    @namespace = validator.Namespace.ToString()
                });
        }
    }
}
