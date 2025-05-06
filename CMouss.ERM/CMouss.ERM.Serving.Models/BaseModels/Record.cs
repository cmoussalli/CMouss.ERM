using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CMouss.ERM.Serving
{
    public class Record
    {
        public int Id { get; set; }
        public int EntityTypeId { get; set; }
        public string Name { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string LastUpdateUserId { get; set; }
        public DateTime LastUpdate { get; set; }
        public string OwnerUserId { get; set; }
        //public Dictionary<string, object> FieldValues { get; set; } = new Dictionary<string, object>();


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public EntityType EntityType { get; set; } 


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<RecordFieldValue> RecordFieldValues { get; set; } 


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Relation> Relations { get; set; }






    }
   
    
    
    
    public class Relation
    {
        public int RecordRelationId { get; set; }

        public int EntityRelationId { get; set; }
        public string RelationName { get; set; }
        public string RelationTitle { get; set; }
        public bool IsList { get; set; }
        public bool IsRequired { get; set; }

        public List<Record> Records { get; set; }


        public EntityType RelationEntityType { get; set; }


    }


}
