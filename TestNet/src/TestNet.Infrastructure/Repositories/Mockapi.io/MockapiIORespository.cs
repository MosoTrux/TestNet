using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text.Json;
using TestNet.Core.Options;
using TestNet.Core.Repositories.Mockapi.io;

namespace TestNet.Infrastructure.Repositories.Mockapi.io
{
    public class MockapiIORespository : IMockapiIORespository
    {
        private AppSettings _appSettings;
        private IConfiguration _configuration;
        public MockapiIORespository(IOptions<AppSettings> options, IConfiguration configuration)
        {
            _appSettings = options.Value;
            _configuration = configuration;
        }
        public GetDiscountResponse GetDiscount(long productId)
        {
            var result = new GetDiscountResponse()
            {
                id = productId.ToString(),
                discount = 0
            };
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync($"{GetURL()}{productId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var json = response.Content.ReadAsStringAsync().Result;
                        result = JsonSerializer.Deserialize<GetDiscountResponse>(json);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }

            return result;
        }

        private string GetURL()
        {
            IConfigurationSection mockapiIO = _configuration.GetSection("AppSettings:MockapiIO");
            IEnumerable<IConfigurationSection> apis = mockapiIO.GetChildren();

            var result = apis.Select(configSection =>
            new MockapiIO
            (
                name: configSection["Name"]!.ToString(),
                url: configSection["Url"]!.ToString())
            ).Where(x=>x.Name.Equals("GetDiscount")).FirstOrDefault();


            return result.Url;


        }



    }
}
