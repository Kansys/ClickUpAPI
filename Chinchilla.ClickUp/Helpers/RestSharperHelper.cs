using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace Chinchilla.ClickUp.Helpers
{
	public class RestSharperHelper
	{
		public static ResponseGeneric<TResponseSuccess, TResponseError> ExecuteRequest<TResponseSuccess, TResponseError>(RestClient client, RestRequest request, params JsonConverter[] converters)
			where TResponseSuccess : IResponse
			where TResponseError : IResponse
		{
            RestResponse response;
            var retries = 5;
            do
            {
                response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.TooManyRequests)
                    break;
                Thread.Sleep(20000);
            } while (retries-- > 0);
            return ExtractResult<TResponseSuccess, TResponseError>(response);
        }

        public static async Task<ResponseGeneric<TResponseSuccess, TResponseError>> ExecuteRequestAsync<TResponseSuccess, TResponseError>(RestClient client, RestRequest request)
			where TResponseSuccess : IResponse
			where TResponseError : IResponse
		{
			var response = await client.ExecuteAsync(request, CancellationToken.None);

            return ExtractResult<TResponseSuccess, TResponseError>(response);
		}

        private static ResponseGeneric<TResponseSuccess, TResponseError> ExtractResult<TResponseSuccess, TResponseError>(RestResponse response)
            where TResponseSuccess : IResponse where TResponseError : IResponse
        {
            var result = new ResponseGeneric<TResponseSuccess, TResponseError>
            {
                RequestStatus = response.StatusCode
            };
            if (response.Content != null)
                switch (result.RequestStatus)
                {
                    case HttpStatusCode.OK:
                        result.ResponseSuccess = JsonConvert.DeserializeObject<TResponseSuccess>(response.Content);
                        break;
                    default:
                        result.ResponseError = JsonConvert.DeserializeObject<TResponseError>(response.Content);
                        break;
                }

            return result;
        }
    }
}