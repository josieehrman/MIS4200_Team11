using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MIS4200_Team11.DAL
{
    public class Team11Context : DbContext
    {
        public Team11Context() : base("DefaultConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();  // note: this is all one line!
        }


        public System.Data.Entity.DbSet<ProfileModels> ProfileModels { get; set; }

        public System.Data.Entity.DbSet<MIS4200_Team11.Models.CoreValues> CoreValues { get; set; }

    }
}