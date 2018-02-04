using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Doggies
{
    public class ApiClient
    {
        private HttpClient _client;

        public ApiClient()
        {
            _client = new HttpClient();
            // The trailing slash is required. If it isn't there, then `/api`
            // will be discarded.
            _client.BaseAddress = new Uri("https://dog.ceo/api/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public async Task<IEnumerable<String>> GetMasterBreedsAsync()
        {
            var path = new Uri("breeds/list", UriKind.Relative);
            var response = await GetDogAsync(path);

            return response.Message;
        }

        private async Task<Response> GetDogAsync(Uri path)
        {
            // Make the request
            var response = await _client.GetAsync(path);

            // Throw an exception if the status code is not 200
            response.EnsureSuccessStatusCode();

            // Read the response as a stream. DataContractJsonSerializer requires
            // a stream.
            var contentStream = await response.Content.ReadAsStreamAsync();


            return Deserialize(contentStream);
        }

        private Response Deserialize(Stream contentStream)
        {
            var serializer = new DataContractJsonSerializer(typeof(Response));

            return (Response)serializer.ReadObject(contentStream);
        }
    }
}
