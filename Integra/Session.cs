using Integra.Core;
using Integra.Models;
using System;
using System.Data.Entity;
using System.Reflection;

namespace Integra
{
    public class Session
    {
        public int SessionID { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StudioSet StudioSet { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    //public class IntegraContext : DbContext
    //{
    //    public IntegraContext() : base(nameof(Integra))
    //    {

    //    }

    //    public DbSet<Session> Sessions { get; set; }

    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
            

    //        modelBuilder.Entity<StudioSet>()
    //            .HasKey(pk => pk.SessionID);

    //        modelBuilder.Entity<StudioSet>()
    //            .Property(p => p.Name)
    //            .IsRequired()
    //            .HasMaxLength(16);

    //        // IGNORE
    //        modelBuilder.Entity<StudioSet>().Ignore(p => p.SelectedPart);
    //        modelBuilder.Entity<StudioSet>().Ignore(p => p.Tone);
    //        modelBuilder.Entity<StudioSet>().Ignore(p => p.Part);
    //        modelBuilder.Entity<StudioSet>().Ignore(p => p.MFXType);
    //        modelBuilder.Entity<StudioSet>().Ignore(p => p.IsInitialized);

    //        PropertyInfo[] properties = typeof(StudioSet).GetProperties(BindingFlags.Instance | BindingFlags.Public);

    //        foreach (PropertyInfo property in properties)
    //        {
    //            Offset attribute = property.GetCustomAttribute<Offset>(false);

    //            if (attribute == null)
    //            {
    //                modelBuilder.Entity<StudioSet>().Ignore(p => );
    //            }
    //        }

    //    }



    //        base.OnModelCreating(modelBuilder);
    //    }
    //}
}
