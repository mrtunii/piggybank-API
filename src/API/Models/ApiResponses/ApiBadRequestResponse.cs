using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace API.Models.ApiResponses
{
    public class ApiBadRequestResponse : ApiResponse
    {
        [JsonProperty("errors")]
        public IEnumerable<string> Errors { get; }

        public ApiBadRequestResponse(ModelStateDictionary modelState)
            : base((int) HttpStatusCode.BadRequest)
        {
            if (modelState.IsValid)
            {
                throw new ArgumentException("ModelState must be invalid", nameof(modelState));
            }

            Errors = modelState.SelectMany(x => x.Value.Errors)
                .Select(x => x.ErrorMessage).ToList();
        }

        public ApiBadRequestResponse(string error)
            : base((int) HttpStatusCode.BadRequest)
        {
            var errors = new List<string> {error};

            Errors = errors;

        }
    }
}