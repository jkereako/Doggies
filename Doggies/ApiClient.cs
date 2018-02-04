using System;
using System.Net.Http;
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
            _client.BaseAddress = new Uri("https://dog.ceo/api/");
        }

        public async Task<IEnumerable<String>> GetMasterBreedsAsync()
        {
            var uri = new Uri("/api/breeds/list");

            var response = await GetAsync(uri);

            return response.Message;
        }

        private async Task<Response> GetAsync(Uri uri)
        {
            // Make the request
            var response = await _client.GetAsync(uri);

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
