using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Serialization;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;
using System.Text;

using NLog;
using Newtonsoft.Json;

namespace Generator
{
    /// <summary>
    /// RPC style service wrapper for generator.
    /// </summary>
    public class GeneratorService
    {
        /// <summary>
        /// Logger for this class.
        /// </summary>
        private Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Access lock.
        /// </summary>
        private readonly Object accessLock = new object();

        /// <summary>
        /// HTTP client.
        /// </summary>
        private HttpClient httpClient;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="serviceBaseUri">Base URI of the service.</param>
        public GeneratorService(String serviceBaseUri)
        {
            //validate inputs
            if (serviceBaseUri == null) throw new ArgumentException("Argument 'serviceBaseUri' is null.");

            //store inputs
            lock (accessLock)
            {
                httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(serviceBaseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        /// <summary>
        /// Check if given HTTP response is '200 OK', throw ApplicationException if not.
        /// </summary>
        /// <param name="resourceUri">URI of the resource requested.</param>
        /// <param name="response">Response to check.</param>
        private void ValidateHttpResponse(String resourceUri, HttpResponseMessage response)
        {
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var msg =
                    $"Non '200 OK' response received when requesting '{httpClient.BaseAddress}/{resourceUri}'." +
                    $"Respone code is '{response.StatusCode}'. Response body is '{response.Content.ReadAsStringAsync().Result}'";
                throw new ApplicationException(msg);
            }
        }

        /// <summary>
        /// Changes the number of electricity production
        /// </summary>
        /// <param name="production">how much electricity this generator produce</param>
        /// <param name="ID">generator ID</param>
        public void changeElectricityProduction(int production, int ID)
        {
            lock (accessLock)
            {
                //compose resource URI part with relevan query parameters
                var resourceUri =
                    $"changeElectricityProduction?" +
                    $"production={HttpUtility.UrlEncode("" + production)}&" +
                    $"ID={HttpUtility.UrlEncode("" + ID)}";

                //send HTTP request to service server, get response back
                var httpResp = httpClient.GetAsync(resourceUri).Result;

                //check if HTTP response is '200 OK', indicate error otherwise
                ValidateHttpResponse(resourceUri, httpResp);
            }
        }

        /// <summary>
        /// Gets unique user id
        /// </summary>
        /// <returns>User id</returns>
        public int getUserID()
        {
            lock (accessLock)
            {
                //compose resource URI part with relevan query parameters
                var resourceUri =
                    $"getUserID";

                //send HTTP request to service server, get response back
                var httpResp = httpClient.GetAsync(resourceUri).Result;

                //check if HTTP response is '200 OK', indicate error otherwise
                ValidateHttpResponse(resourceUri, httpResp);

                //extract operation result from string returned
                var result = Int32.Parse(httpResp.Content.ReadAsStringAsync().Result);

                return result;
            }
        }

        /// <summary>
        /// Gets bool is there a shortage in the electrical grid
        /// </summary>
        /// <returns>User id</returns>
        public bool getShortage()
        {
            lock (accessLock)
            {
                //compose resource URI part with relevan query parameters
                var resourceUri =
                    $"getShortage";

                //send HTTP request to service server, get response back
                var httpResp = httpClient.GetAsync(resourceUri).Result;

                //check if HTTP response is '200 OK', indicate error otherwise
                ValidateHttpResponse(resourceUri, httpResp);

                //extract operation result from string returned
                var result = Boolean.Parse(httpResp.Content.ReadAsStringAsync().Result);

                return result;
            }
        }

        /// <summary>
        /// Sends electricity in case of the shortage from a battery.
        /// </summary>
        /// <param name="energy">how much electricity this user sent</param>
        /// <param name="ID">generator ID</param>
        public void sendElectricity(int energy, int ID)
        {
            lock (accessLock)
            {
                //compose resource URI part with relevan query parameters
                var resourceUri =
                    $"sendElectricity?" +
                    $"energy={HttpUtility.UrlEncode("" + energy)}&" +
                    $"ID={HttpUtility.UrlEncode("" + ID)}";

                //send HTTP request to service server, get response back
                var httpResp = httpClient.GetAsync(resourceUri).Result;

                //check if HTTP response is '200 OK', indicate error otherwise
                ValidateHttpResponse(resourceUri, httpResp);
            }
        }

    }
}
