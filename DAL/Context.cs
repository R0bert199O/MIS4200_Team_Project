using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MIS4200_Team_Project.Models;
using System.Data.Entity;

namespace MIS4200_Team_Project.DAL
{
    public class Context : DbContext
    {
        public Context() : base ("name=DefaultConnection")
        {
            // this method is a 'constructor' and is called when a new context is created
            // the base attribute says which connection string to use
        }
        // Include each object here. The value inside <> is the name of the class,
        // the value outside should generally be the plural of the class name
        // and is the name used to reference the entity in code
        public DbSet<UserDetails> userDetails { get; set; }
    }
}