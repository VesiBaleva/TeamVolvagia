﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;


namespace TeamAssessnment.WebAPI.Controllers
{
    public class BaseApiController : ApiController
    {
        protected T PerformOperationAndHandleExceptions<T>(Func<T> operation)
        {
            try
            {
                return operation();
            }
            catch (Exception ex)
            {
                var errResponse = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                throw new HttpResponseException(errResponse);
            }
        }

        protected string GetHeaderValue(HttpRequestHeaders headers, string key)
        {
            IEnumerable<string> values;

            if (headers.TryGetValues(key, out values))
            {
                return values.FirstOrDefault();
            }

            return null;
        }
    }
}
