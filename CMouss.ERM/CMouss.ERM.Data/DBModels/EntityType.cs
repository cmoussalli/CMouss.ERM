using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERM.Data.DBModels
{
    public class EntityType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public ICollection<Field> Fields { get; set; } = new List<Field>();
    }
}