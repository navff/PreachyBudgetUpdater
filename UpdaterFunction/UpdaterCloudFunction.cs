using System;
using System.Linq;
using UpdaterCloudFunction.Common;

namespace UpdaterCloudFunction
{
    public class UpdaterCloudFunction
    {
        public string FunctionHandler(string? s = null) {
            var envConfig = new EnvironmentConfig();
            Console.WriteLine("API URL: " + envConfig.TinkoffApiSettings.ApiUrl);

            var tinkoffApiClient = new TinkoffApiClient(envConfig.TinkoffApiSettings);
            var accounts = tinkoffApiClient.GetAccountsAsync().Result;
        
            Console.WriteLine("Accounts count: " + accounts.Count);
            if (accounts.Any())
            {
                var glkAccount = accounts.First(a => a.Name.ToLower().Contains("голока"));
                Console.WriteLine($"Собрано: {glkAccount.Balance.Otb}");
                var googleService = new SheetOperationService(envConfig.GoogleCredsJson);
                googleService.SetTinkoffBalanceOnSheet(glkAccount.Balance.Otb.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("ru-ru")));
            }

            return "Good!";
        }
    }
}

