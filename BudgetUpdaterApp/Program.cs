using Microsoft.Extensions.DependencyInjection;
using Sankirtana.Web.Common;
using TinkoffBusiness.Api.Client;

public class Program
{
    private static EnvironmentConfig _environmentConfig;
    
    static async Task Main(string[] args)
    {
        ConfigureServices();
        
        Console.WriteLine("API URL: " + _environmentConfig.TinkoffApiSettings.ApiUrl);

        var tinkoffApiClient = new TinkoffApiClient(_environmentConfig.TinkoffApiSettings);
        var accounts = await tinkoffApiClient.GetAccountsAsync();
        Console.WriteLine("Accounts count: " + accounts.Count);
        if (accounts.Any())
        {
            var glkAccount = accounts.FirstOrDefault(a => a.Name.ToLower().Contains("голока"));
            Console.WriteLine($"Собрано: {glkAccount.Balance.Otb}");
        }

        Console.ReadLine();
    }

    private static void ConfigureServices()
    {
        IServiceCollection serviceCollection = new ServiceCollection();

        var envConfig = new EnvironmentConfig();
        serviceCollection.AddSingleton(envConfig);

        _environmentConfig = envConfig;
    }
}