using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreePractice.Web.API.Data
{
    public class EposDbContext :  DbContext
    {
        public EposDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Models.Student> Students { get; set; }
    }
}
