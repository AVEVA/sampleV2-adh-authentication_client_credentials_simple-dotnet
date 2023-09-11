using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace AuthCC
{
    public static class Program
    {
        public static async Task Main()
        {
            // Step 1: get needed variables 
            IConfigurationBuilder builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .AddJsonFile("appsettings.test.json", optional: true);
            IConfigurationRoot _configuration = builder.Build();

            // ==== Client constants ====
            string tenantId = _configuration["TenantId"];
            string resource = _configuration["Resource"];
            string clientId = _configuration["ClientId"];
            string clientSecret = _configuration["ClientSecret"];
            string apiVersion = _configuration["ApiVersion"];

            (_configuration as ConfigurationRoot).Dispose();

            using HttpClient httpClient = new ();
            // Step 2: get the authentication endpoint from the discovery URL
            JsonElement wellknownInformation = await httpClient.GetFromJsonAsync<JsonElement>($"{resource}/identity/.well-known/openid-configuration").ConfigureAwait(false);
            string tokenUrl = wellknownInformation.GetProperty("token_endpoint").GetString();

            // Step 3: use the client ID and Secret to get the needed bearer token
            FormUrlEncodedContent data = new (new[]
            {
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

            HttpResponseMessage tokenInformation = await httpClient.PostAsync(new Uri(tokenUrl), data).ConfigureAwait(false);
            data.Dispose();
            JsonElement tokenObject = await tokenInformation.Content.ReadFromJsonAsync<JsonElement>().ConfigureAwait(false);
            string token = tokenObject.GetProperty("access_token").GetString();

            // Step 4: test token by calling the base tenant endpoint 
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            HttpResponseMessage test = await httpClient.GetAsync(new Uri($"{resource}/api/{apiVersion}/Tenants/{tenantId}")).ConfigureAwait(false);
            if (!test.IsSuccessStatusCode) throw new Exception("Check Failed");
        }
    }
}
