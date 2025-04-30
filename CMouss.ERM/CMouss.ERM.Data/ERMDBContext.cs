using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Data.DBModels;
using CMouss.IdentityFramework;
using Microsoft.EntityFrameworkCore;

namespace CMouss.ERM.Data
{

    public partial class ERMDBContext:IDFDBContext
    {

        public DbSet<DataType> DataTypes { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }
        public DbSet<EntityField> EntityFields { get; set; }

        public DbSet<EntityListView> EntityListViews { get; set; }
        public DbSet<EntityListViewField> EntityListViewFields { get; set; }
        public DbSet<EntityRelation> EntityRelations { get; set; }




        public DbSet<Record> Records { get; set; }
        public DbSet<RecordFieldValue> RecordFieldValues { get; set; }
        public DbSet<RecordRelation> RecordRelations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EntityRelation>()
                .HasOne(er => er.EntityType_Left)
                .WithMany(et => et.EntityRelations_Left)
                .HasForeignKey(er => er.EntityTypeId_Left)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EntityRelation>()
                .HasOne(er => er.EntityType_Right)
                .WithMany(et => et.EntityRelation_Right)
                .HasForeignKey(er => er.EntityTypeId_Right)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EntityField>()
                .HasOne(ef => ef.EntityType)
                .WithMany(et => et.EntityFields)
                .HasForeignKey(ef => ef.EntityTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EntityListViewField>()
                .HasOne(elvf => elvf.EntityListView)
                .WithMany(elv => elv.EntityListViewFields)
                .HasForeignKey(elvf => elvf.EntityListViewId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure relationship between EntityListView and EntityType
            modelBuilder.Entity<EntityListView>()
                .HasOne(elv => elv.EntityType) // Navigation property
                .WithMany(et => et.EntityListViews) // Reverse navigation property
                .HasForeignKey(elv => elv.EntityTypeId) // Foreign key
                .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascading deletes

            modelBuilder.Entity<RecordRelation>()
                .HasOne(rr => rr.LeftRecord)
                .WithMany(r => r.RecordRelations)
                .HasForeignKey(rr => rr.LeftRecordId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RecordRelation>()
                .HasOne(rr => rr.RightRecord)
                .WithMany(r => r.RecordInverseRelations)
                .HasForeignKey(rr => rr.RightRecordId)
                .OnDelete(DeleteBehavior.Restrict);

        }

    }


}
