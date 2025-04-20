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

    public class ERMDBContext:IDFDBContext
    {

        public DbSet<FieldType> FieldTypes { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }
        public DbSet<EntityField> EntityFields { get; set; }
        public DbSet<EntityFieldValue> EntityFieldValues { get; set; }
        public DbSet<EntityRecord> EntityRecords { get; set; }






    }


}
