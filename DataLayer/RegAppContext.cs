using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class RegAppContext : DbContext
    {
        public RegAppContext(DbContextOptions<RegAppContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
