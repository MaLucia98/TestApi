using Entities.Database;
using Logic.Manager.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;

namespace Logic.Manager
{
    public class DataApiManager : IDataApiManager
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<DataApiManager> log;
        private readonly DbContext context;
        public string host { get; set; }
        public DataApiManager(DbContext context, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
            this.log = loggerFactory.CreateLogger<DataApiManager>();

            host = "http://54.209.174.241:5200/api/";          
        }
        public async Task<UserResponse> GetKeyDataApi()
        {
            var result = new UserResponse();
            var model = new UserConection()
            {
                userName = "user",
                password = "1234"
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(host);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                settings.Formatting = Formatting.Indented;
                string json = JsonConvert.SerializeObject(model, settings);

                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"Login",httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string text = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<UserResponse>(text);
                }
            }

            return result;
        }
        public async Task<List<RecaudoVehiculos>> GetRecaudoVehiculosApi(string date)
        {
            var userResponse = await GetKeyDataApi();

            List<RecaudoVehiculos> result = new List<RecaudoVehiculos>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(host);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userResponse.token);

                HttpResponseMessage response = await client.GetAsync($"RecaudoVehiculos/{date}");

                if (response.IsSuccessStatusCode)
                {
                    string text = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<RecaudoVehiculos>>(text);
                }               
            }

            return result;
         
        }
        public async Task<List<ConteoVehiculos>> GetConteoVehiculosApi(string date)
        {
            var userResponse = await GetKeyDataApi();

            List<ConteoVehiculos> result = new List<ConteoVehiculos>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(host);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userResponse.token);

                HttpResponseMessage response = await client.GetAsync($"ConteoVehiculos/{date}");

                if (response.IsSuccessStatusCode)
                {
                    string text = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<ConteoVehiculos>>(text);
                }
            }

            return result;

        }
    }
}