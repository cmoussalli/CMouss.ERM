using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CMouss.ERM.Data.DBModels
{
    public class AreaLink
    {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public int EntityTypeId { get; set; }
        public string SortingId { get; set; }
        public string GroupTitle { get; set; }


        public virtual Area Area { get; set; }
        public virtual EntityType EntityType { get; set; }

    }
}
