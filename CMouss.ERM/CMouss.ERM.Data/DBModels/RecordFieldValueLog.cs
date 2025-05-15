using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERM.Data.DBModels
{
    public class RecordFieldValueLog
    {

        public int Id { get; set; }
        public int HisId { get; set; }
        public string Action { get; set; }

        public int RecordId { get; set; }
        public int EntityFieldId { get; set; }
        public string FieldValue { get; set; }


        public virtual Record Record { get; set; }
        public virtual EntityField EntityField { get; set; }

    }
}