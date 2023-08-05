using Entities.Database;
using Logic.Manager.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

namespace Logic.Manager
{
    public class DataApiManager : IDataApiManager
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<DataApiManager> log;
        private readonly DbContext context;
        public string host { get; set; }
        public string key { get; set; }
        public DataApiManager(DbContext context, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
            this.log = loggerFactory.CreateLogger<DataApiManager>();

            host = "http://54.209.174.241:5200/api/";
            key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InVzZXIiLCJqdGkiOiI3YjQ1OTIxYi05NTA2LTQxNWItYThiZC0yZjI4ZTEzZTBiMzEiLCJleHAiOjE2OTExOTE1MzR9.LpMnPbePSPNfxGAgAvKaBtKkgrShV1aClZKZ20n_sj4";
        }
        public async Task<List<RecaudoVehiculos>> GetRecaudoVehiculosApi(string date)
        {           

            List<RecaudoVehiculos> result = new List<RecaudoVehiculos>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(host);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);

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

            List<ConteoVehiculos> result = new List<ConteoVehiculos>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(host);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);

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