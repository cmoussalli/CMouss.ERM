using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERM.Data.DBModels
{
    public class EntityField
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int EntityTypeId { get; set; }
        public int FieldTypeId { get; set; }
        public bool IsRequired { get; set; }
        public string DefaultValue { get; set; } = "";

        public virtual EntityType EntityType { get; set; } = null!; // Ensure non-nullable navigation property
        public virtual FieldType FieldType { get; set; } = null!; // Ensure non-nullable navigation property
    }
}
