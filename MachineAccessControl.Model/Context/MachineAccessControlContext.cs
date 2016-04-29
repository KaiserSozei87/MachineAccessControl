using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using MachineAccessControl.Model.Models;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using MachineAccessControl.Common;

namespace MachineAccessControl.Model.Context
{
    public interface IContext
    {
        IDbSet<AccessControl> AccessControls { get; set; }
        IDbSet<AccessControlError> AccessControlErrors { get; set; }
        IDbSet<AccessControlTransaction> AccessControlTransactions { get; set; }
        IDbSet<Machine> Machines { get; set; }
        IDbSet<MachineLocation> MachineLocations { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();

    }

    public partial class MachineAccessControlContext : DbContext, IContext
    {
        public MachineAccessControlContext()
            : base("name=MachineAccessControl")
        {
        }

        public  IDbSet<AccessControl> AccessControls { get; set; }
        public  IDbSet<AccessControlError> AccessControlErrors { get; set; }
        public  IDbSet<AccessControlTransaction> AccessControlTransactions { get; set; }
        public  IDbSet<Machine> Machines { get; set; }
        public  IDbSet<MachineLocation> MachineLocations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessControl>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<AccessControl>()
                .Property(e => e.LastUpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<AccessControl>()
                .HasMany(e => e.AccessControlErrors)
                .WithRequired(e => e.AccessControl)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AccessControl>()
                .HasMany(e => e.AccessControlTransactions)
                .WithRequired(e => e.AccessControl)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AccessControlError>()
                .Property(e => e.ErrorName)
                .IsUnicode(false);

            modelBuilder.Entity<AccessControlError>()
                .Property(e => e.ErrorDesc)
                .IsUnicode(false);

            modelBuilder.Entity<AccessControlError>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<AccessControlTransaction>()
                .Property(e => e.TranType)
                .IsUnicode(false);

            modelBuilder.Entity<AccessControlTransaction>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<AccessControlTransaction>()
                .Property(e => e.MachineName)
                .IsUnicode(false);

            modelBuilder.Entity<Machine>()
                .Property(e => e.MachineName)
                .IsUnicode(false);

            modelBuilder.Entity<Machine>()
                .HasMany(e => e.AccessControls)
                .WithRequired(e => e.Machine)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MachineLocation>()
                .Property(e => e.LocationName)
                .IsUnicode(false);

            modelBuilder.Entity<MachineLocation>()
                .HasMany(e => e.Machines)
                .WithRequired(e => e.MachineLocations)
                .HasForeignKey(e => e.MachineLocation)
                .WillCascadeOnDelete(false);
        }


        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity
                    && (x.State == System.Data.Entity.EntityState.Added || x.State == System.Data.Entity.EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                IAuditableEntity entity = entry.Entity as IAuditableEntity;
                if (entity != null)
                {
                    string identityName = Thread.CurrentPrincipal.Identity.Name;
                    DateTime now = DateTime.UtcNow;

                    if (entry.State == System.Data.Entity.EntityState.Added)
                    {
                        entity.CreatedBy = identityName;
                        entity.CreatedDate = now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                    }

                    entity.UpdatedBy = identityName;
                    entity.UpdatedDate = now;
                }
            }
            try
            {


                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }

    }
}
