using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueProcessor
{
    [Table("tblAuditEmployeeData")]
    public class AuditEmployeeData
    {
        public int? ID { get; set; }
        
        public string? Data { get; set; }    
    }

    public class AuditEmployeeDataContext : DbContext
    {
        public DbSet<AuditEmployeeData> data { get; set; }
        public AuditEmployeeDataContext(DbContextOptions<AuditEmployeeDataContext> options) : base(options)
        {

        }

    }
    public class CookBookContextFactory : IDesignTimeDbContextFactory<AuditEmployeeDataContext>
    {
        public AuditEmployeeDataContext CreateDbContext(string[]? args = null)
        {
            /* var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();*/
            var optionsBuilder = new DbContextOptionsBuilder<AuditEmployeeDataContext>();
            optionsBuilder.UseSqlServer("Server=tcp:my-demo-server.database.windows.net,1433;Initial Catalog=EmployeeDB;Persist Security Info=False;User ID=demo-sql-database-server;Password=Rahul1234@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            return new AuditEmployeeDataContext(optionsBuilder.Options);
        }
    }
}
