using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ImagesProcessing.Responses
{
    public class ContentExistsResult : IHttpActionResult
    {
        string _errorMessage;
        HttpRequestMessage _request;

        public ContentExistsResult(string errorMessage, HttpRequestMessage request)
        {
            _errorMessage = errorMessage;
            _request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(_errorMessage),
                RequestMessage = _request
            };
            return Task.FromResult(response);
        }
    }
}