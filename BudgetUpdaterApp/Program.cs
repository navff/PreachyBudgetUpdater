using BudgetUpdaterApp;
using Microsoft.Extensions.DependencyInjection;
using Sankirtana.Web.Common;
using TinkoffBusiness.Api.Client;

public class Program
{
    static async Task Main(string[] args)
    {
        var envConfig = new EnvironmentConfig();
        Console.WriteLine("API URL: " + envConfig.TinkoffApiSettings.ApiUrl);

        var tinkoffApiClient = new TinkoffApiClient(envConfig.TinkoffApiSettings);
        var accounts = await tinkoffApiClient.GetAccountsAsync();
        Console.WriteLine("Accounts count: " + accounts.Count);
        if (accounts.Any())
        {
            var glkAccount = accounts.FirstOrDefault(a => a.Name.ToLower().Contains("голока"));
            Console.WriteLine($"Собрано: {glkAccount.Balance.Otb}");
            var googleService = new SheetOperationService(envConfig.GoogleCredsJson);
            googleService.SetTinkoffBalanceOnSheet(glkAccount.Balance.Otb.ToString());
        }
    }
}