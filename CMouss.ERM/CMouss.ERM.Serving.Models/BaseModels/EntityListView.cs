using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CMouss.ERM.Serving
{
    public class EntityListView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPublished { get; set; }



        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public EntityType EntityType { get; set; } = null!;


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<EntityListViewField> EntityListViewFields { get; set; } = new();
    }


}
