using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Robno.Models
{
    public class RobnoContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public RobnoContext() : base("name=RobnoContext")
        {
        }

        public DbSet<Artikal> Artikals { get; set; }
        public DbSet<JedinicaMjere> JedinicaMjeres { get; set; }
        public DbSet<ArtikalKlasa> ArtikalKlasas { get; set; }
        public DbSet<Tarifa> Tarifas { get; set; }
        public DbSet<StavkaRacuna> StavkaRacunas { get; set; }
        public DbSet<Racun> Racuns { get; set; }
        public DbSet<NacinPlacanja> NacinPlacanjas { get; set; }
        public DbSet<PoslovniPartner> PoslovniPartners { get; set; }
        public DbSet<Primka> Primkas { get; set; }
        public DbSet<Valuta> Valutas { get; set; }
        public DbSet<StavkaPrimke> StavkaPrimkes { get; set; }
    }
}
