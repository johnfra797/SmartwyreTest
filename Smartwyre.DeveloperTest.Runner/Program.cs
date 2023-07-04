using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Smartwyre.Data.Context;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.Data.DTO;
using Smartwyre.Data.Repositories.Implementations;
using Smartwyre.Data.Repositories.Definitions;

static ServiceProvider ConfigureServices()
{
    var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();
    var connectionString = configuration.GetConnectionString("SmartwyreConnection");
    var collection = new ServiceCollection();
    collection.AddDbContext<SmartwyreContext>();
    collection.AddTransient<IRebateService, RebateService>();
    collection.AddTransient<IDataRepository, DataRepository>();
    return collection.BuildServiceProvider();
}


var provider = ConfigureServices();

var rebateService = provider.GetRequiredService<IRebateService>();
rebateService.PopulateDB();
CalculateRebateRequest calculateRebateRequest = new CalculateRebateRequest()
{
    RebateIdentifier= "803dca12-59c6-4636-bca0-93dc30c7752a",
    ProductIdentifier = "b948bc91-0dac-487a-8bb5-0a68eeabf127",
    Volume = 1,
};
CalculateRebateResult calculateRebateResult = rebateService.Calculate(calculateRebateRequest);
Console.WriteLine($"Result {calculateRebateResult.Success}");