using Microsoft.EntityFrameworkCore;
using Server.Classes;
using Server.Entities;
using Server.Persistence;

namespace Server.Helpers;

public class SeedData : IHostedService 
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ConfigApp appConfig;

    public SeedData(IServiceProvider serviceProvider, ConfigApp _appConfig)
    {
        _serviceProvider = serviceProvider;
        appConfig = _appConfig;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        using (var context = new CustomersContext(scope.ServiceProvider.GetRequiredService<DbContextOptions<CustomersContext>>(), appConfig))
        {
            if (context.TblCustomers.Any())
                return;

            for (var i = 0; i < 05; i++)
            {
                var fName = $"{(new string[] { "Fake", "Dummy", "Random" })[new Random().Next(3)]}";
                var lName = $"{(new string[] { "Name", "Nikname", "Alias" })[new Random().Next(3)]}";
                context.TblCustomers.Add(
                new TblCustomers
                {
                    FirstName = fName,
                    LastName = lName,
                    Email = $"{fName.ToLower()}.{lName.ToLower()}@mail.com",
                });
            }
            context.SaveChanges();
        }

    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
