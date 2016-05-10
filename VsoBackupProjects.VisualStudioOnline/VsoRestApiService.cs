using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VsoBackupProjects.VisualStudioOnline
{
    public class VsoRestApiService
    {
        private readonly string _userName;
        private readonly string _password;

        public VsoRestApiService(string username, string password)
        {
            _userName = username;
            _password = password;
        }
        public async Task<T> ExecuteRequest<T>(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var authenticationHeaderValue = $"{_userName}:{_password}";
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationHeaderValue)));

                var json = await GetAsync(client, url);
                var retVal = JsonConvert.DeserializeObject<T>(json);
                return retVal;
            }
        }
        
        public async Task DownloadAsync(string requestUri, string filename)
        {
            if (requestUri == null)
                throw new ArgumentNullException("requestUri");

            await DownloadAsync(new Uri(requestUri), filename);
        }


        private async Task DownloadAsync(Uri requestUri, string filename)
        {
            if (filename == null)
                throw new ArgumentNullException("filename");


            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = new TimeSpan(1, 0, 0, 0);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/zip"));
                var authenticationHeaderValue =
                    $"{_userName}:{_password}";
                httpClient.DefaultRequestHeaders.Authorization =
                  new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationHeaderValue)));
#if (!METHOD1)

                try
                {
                    using (HttpResponseMessage response = await httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead))
                    using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
                    {
                        string fileToWriteTo = filename; // Path.GetTempFileName();
                        using (Stream streamToWriteTo = File.Open(fileToWriteTo, FileMode.Create))
                        {
                            await streamToReadFrom.CopyToAsync(streamToWriteTo);
                        }
                    }



                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    Debug.WriteLine(ex.Message);
                }
#else

                try
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Get, requestUri))
                    {
                        using (
                            Stream contentStream = await (await httpClient.SendAsync(request)).Content.ReadAsStreamAsync(),
                            stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
                        {

                            await contentStream.CopyToAsync(stream);
                        }
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    Debug.WriteLine(ex.Message);
                }
#endif
            }
        }

        private static async Task<String> GetAsync(HttpClient client, String apiUrl)
        {
            string responseBody;

            using (var response = await client.GetAsync(apiUrl))
            {
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }

            return responseBody;
        }
    }

}
