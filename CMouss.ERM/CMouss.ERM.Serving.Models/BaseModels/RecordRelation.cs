using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CMouss.ERM.Serving
{
    public class RecordRelation
    {

        public int Id { get; set; }
        public int EntityRelationId { get; set; }

        public int LeftRecordId { get; set; }

        public int RightRecordId { get; set; }

        public string CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }



        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public EntityRelation EntityRelation { get; set; } = null!; // Ensure non-nullable navigation property


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Record LeftRecord { get; set; } = null!; // Ensure non-nullable navigation property


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Record RightRecord { get; set; } = null!; // Ensure non-nullable navigation property

    }
}
