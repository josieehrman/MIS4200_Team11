using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MIS4200_Team11.DAL
{
    public class Team11Context : DbContext
    {
        public Team11Context() : base("DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<ProfileModels> ProfileModels { get; set; }

        public System.Data.Entity.DbSet<MIS4200_Team11.Models.CoreValues> CoreValues { get; set; }

    }
}