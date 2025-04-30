using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CMouss.ERM.Serving
{
    public class EntityField
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int EntityTypeId { get; set; }
        public int DataTypeId { get; set; }
        public bool IsRequired { get; set; }
        public string DefaultValue { get; set; } = "";


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public EntityType EntityType { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DataType DataType { get; set; }
      




    }
}
