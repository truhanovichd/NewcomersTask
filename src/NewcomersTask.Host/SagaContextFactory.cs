// <copyright file="SagaContextFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NewcomersTask.DB;

namespace NewcomersTask.Host
{
    public class SagaContextFactory : IDesignTimeDbContextFactory<SagaContext>
    {
        public SagaContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            var config = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<SagaContext>();
            optionsBuilder.UseNpgsql(config.GetSection("ConnectionStrings:ServerConnection").Value);

            return new SagaContext(optionsBuilder.Options);
        }
    }
}