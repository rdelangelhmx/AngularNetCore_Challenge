using Microsoft.EntityFrameworkCore;
using Server.Classes;
using Server.Entities;

namespace Server.Persistence;

public partial class CustomersContext : DbContext
{
    private readonly ConfigApp appConfig;

    public CustomersContext(ConfigApp _appConfig)
    { 
        appConfig = _appConfig;
    }

    public CustomersContext(DbContextOptions<CustomersContext> options, ConfigApp _appConfig) : base(options)
    {
        appConfig = _appConfig;
    }

    public virtual DbSet<TblCustomers> TblCustomers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: appConfig.Application.DataBase);
    }
}
