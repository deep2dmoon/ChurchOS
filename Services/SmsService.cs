using System.Text.Json;

namespace htmos.services;

public class SmsService(HttpClient httpClient, IConfiguration configuration)
{
    public async Task<object> SendSMS()
    {
        var data = new
        {
            api_key = configuration.GetValue<string>("termiiAPI:key"),
            to = "2348020626275",
            from = configuration.GetValue<string>("termiiSender:ID"),
            sms = "You are welcome to Harvest Tabernacle Ministry, You are honoured!!!",
            channel = "generic",
            type = "plain"
        };

        try
        {
            var response = await httpClient.PostAsJsonAsync($"{configuration.GetValue<string>("termiiBase:url")}api/sms/send", data);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            return new { result = JsonSerializer.Deserialize<object>(result) };
        }
        catch (System.Exception)
        {

            throw;
        }

    }

    public async Task<object> SendBulkSMS(IEnumerable<string> recipients, string Annountment)
    {
        try
        {
            var data = new
            {
                api_key = configuration.GetValue<string>("termiiAPI:key"),
                to = recipients,
                from = configuration.GetValue<string>("termiiSender:ID"),
                sms = Annountment,
                channel = "generic",
                type = "plain"
            };
            var response = await httpClient.PostAsJsonAsync($"{configuration.GetValue<string>("termiiBase:url")}api/sms/send/bulk", data);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            return new { result = JsonSerializer.Deserialize<object>(result) };

        }
        catch (System.Exception)
        {

            throw;
        }
    }
}