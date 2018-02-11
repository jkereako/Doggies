using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Doggies
{
    sealed public class ApiClient
    {
        HttpClient _client;

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

        public async Task<IEnumerable<string>> GetMasterBreedsAsync()
        {
            var path = new Uri("breeds/list", UriKind.Relative);
            var response = await GetDogAsync(path);
            return response.Message;
        }

        public async Task<IEnumerable<string>> GetSubtypeBreedImageAsync(string breed)
        {
            var path = new Uri($"breed/{breed}/images/random", UriKind.Relative);
            var response = await GetDogAsync(path);
            return response.Message;
        }

        public async Task<IEnumerable<string>> GetSubtypeBreedsAsync(string breed)
        {
            var path = new Uri($"breed/{breed}/list", UriKind.Relative);
            var response = await GetDogAsync(path);
            return response.Message;
        }

        async Task<Response> GetDogAsync(Uri path)
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

        Response Deserialize(Stream contentStream)
        {
            var serializer = new DataContractJsonSerializer(typeof(Response));

            return (Response)serializer.ReadObject(contentStream);
        }
    }
}
