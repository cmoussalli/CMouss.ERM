using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERM.Data.DBModels
{
    public class RecordRelation
    {

        public int Id { get; set; }
        public int EntityRelationId { get; set; }

        [ForeignKey(nameof(LeftRecord))]
        public int LeftRecordId { get; set; }

        [ForeignKey(nameof(RightRecord))]
        public int RightRecordId { get; set; }
        
        public string CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }


        public virtual EntityRelation EntityRelation { get; set; } = null!; // Ensure non-nullable navigation property
        public virtual Record LeftRecord { get; set; } = null!; // Ensure non-nullable navigation property
        public virtual Record RightRecord { get; set; } = null!; // Ensure non-nullable navigation property

    }
}
