using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DBapp.Models
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<student> student { get; set; }
    }
}