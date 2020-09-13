using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreePractice.Web.API.Data
{
    public class DBFuncStore
    {
        private readonly string connectionString;

        public DBFuncStore(string dbConnectionString)
        {
            this.connectionString = dbConnectionString;

            var dataContext = GetWasteDbContext();

            var students = dataContext.Students;

        }

        private EposDbContext GetWasteDbContext()
        {
            var options = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<EposDbContext>();
            options.UseSqlServer(
                this.connectionString,
                options =>
                {
                    options.CommandTimeout(20); // secs
                });

            return new EposDbContext(options.Options);
        }

    }
}
