using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERM.Data.DBModels
{
    public class EntityRecord
    {
        public int Id { get; set; }
        public int EntityTypeId { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string LastUpdateUserId { get; set; }
        public DateTime LastUpdate { get; set; }
        public string OwnerUserId { get; set; }
        //public Dictionary<string, object> FieldValues { get; set; } = new Dictionary<string, object>();
    }
}