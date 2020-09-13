using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TRMDataManager.Models
{
    public class TRMDbEntities : DbContext
    {
        public TRMDbEntities()
            : base("name=TRMDbEntities")
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<DataFile> DataFiles { get; set; }
    }
}