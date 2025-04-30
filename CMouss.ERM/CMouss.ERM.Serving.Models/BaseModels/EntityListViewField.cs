using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CMouss.ERM.Serving
{
    public class EntityListViewField
    {
        public int Id { get; set; }
        public int EntityListViewId { get; set; }
        public int EntityFieldId { get; set; }
        public int SortId { get; set; }



        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public EntityListView EntityListView { get; set; } = null!; // Ensure non-nullable navigation property


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public EntityField EntityField { get; set; } = null!; // Ensure non-nullable navigation property
    }
}
