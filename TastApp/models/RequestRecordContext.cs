using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TastApp.models
{
    public class RequestRecordContext: DbContext
    {
        public DbSet<RequestRecord> RequestRecords { get; set; }
        public DbSet<ErrorsLog> ErrorsLog { get; set; }
        public RequestRecordContext(DbContextOptions<RequestRecordContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

