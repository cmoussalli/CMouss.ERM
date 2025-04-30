using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CMouss.ERM.Serving;

namespace CMouss.ERM.Serving
{
    public class EntityType
    {

      

        public int Id { get; set; }
        public string Name { get; set; }
        public string PluralName { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? DefaultEntityListViewID { get; set; }

        public bool IsDeleted { get; set; }




        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<EntityField> EntityFields { get; set; } = new();


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<EntityListView> EntityListViews { get; set; } = new();


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Record> Records { get; set; } = new();


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public EntityListView? DefaultEntityListView { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<EntityRelation> EntityRelations_Left { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<EntityRelation> EntityRelation_Right { get; set; }
    }
}
