using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CMouss.ERM.Serving
{
    public class EntityRelation
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Ensure non-nullable property is initialized


        //Left Side
        public string Title_Left { get; set; }

        public int EntityTypeId_Left { get; set; }

        public bool IsRequired_Left { get; set; }
        public bool IsList_Left { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public EntityType EntityType_Left { get; set; } = null!; // Ensure non-nullable navigation property



        //Right Side
        public string Title_Right { get; set; }

        public int EntityTypeId_Right { get; set; }

        public bool IsRequired_Right { get; set; }
        public bool IsList_Right { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public EntityType EntityType_Right { get; set; } = null!; // Ensure non-nullable navigation property



        public bool IsDeleted { get; set; }




    }
}
