using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERM.Data.DBModels
{
    public class EntityFieldValue
    {
        [Key]
        public int Id { get; set; }
        public int EntityRecordId { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
    }
}