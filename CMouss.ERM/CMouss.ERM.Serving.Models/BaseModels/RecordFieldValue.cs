using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CMouss.ERM.Serving
{
    public class RecordFieldValue
    {
        public int Id { get; set; }
        public int RecordId { get; set; }
        public int EntityFieldId { get; set; }
        public string FieldValue { get; set; }



        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Record Record { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public EntityField EntityField { get; set; }

    }
}
