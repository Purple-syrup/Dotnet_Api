using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyType> propertyTypes { get; set; }
        public DbSet<FurnishingType> FurnishingTypes { get; set; }
    }
}