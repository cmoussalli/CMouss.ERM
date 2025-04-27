using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERM.Data.DBModels
{
    public class EntityListViewField
    {
        public int Id { get; set; }
        public int EntityListViewId { get; set; }
        public int EntityFieldId { get; set; }
        public int SortId { get; set; }

        public virtual EntityListView EntityListView { get; set; } = null!; // Ensure non-nullable navigation property
        public virtual EntityField EntityField { get; set; } = null!; // Ensure non-nullable navigation property
    }


}
