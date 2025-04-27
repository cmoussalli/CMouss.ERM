using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERM.Data.DBModels
{
    public class EntityListView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPublished { get; set; }

        [ForeignKey(nameof(EntityTypeId))]
        public virtual EntityType EntityType { get; set; } = null!;

        public virtual List<EntityListViewField> EntityListViewFields { get; set; } = new();
    }
}
