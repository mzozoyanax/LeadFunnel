using LeadFunnel.Domain;
using LeadFunnel.Domain.Models;
using LeadFunnel.Domain.ViewModels;
using LeadFunnel.Interface.Repositories;
using LeadFunnel.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadFunnel.Repository
{
    public class TwilioRepository : ITwilioRepository
    {
        private AppDbContext appDbContext = new AppDbContext();
        public async Task<bool> TriggerStudioFlow(RegisterViewModel registerViewModel, string flowSid)
        {
            try
            {
                var cred = appDbContext.TwilioCredentials.FirstOrDefault();

                // Twilio Credentials
                string accountSid = cred.AccountId;
                string apiKey = cred.ApiKey;
                string apiSecret = cred.ApiSecret;

                // Twilio API Base URL
                string baseUrl = $"https://studio.twilio.com/v1/Flows/{flowSid}/Executions";

                // Data to send
                var data = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("To", registerViewModel.Phone),
                    new KeyValuePair<string, string>("From", cred.VirtualPhone)
                });

                // HTTP Request Headers
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{apiKey}:{apiSecret}"));
                var headers = new HttpClient();
                headers.DefaultRequestHeaders.Add("Authorization", $"Basic {credentials}");

                // Send POST request to trigger the Studio Flow
                var client = new HttpClient();
                var response = await client.PostAsync(baseUrl, data);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
