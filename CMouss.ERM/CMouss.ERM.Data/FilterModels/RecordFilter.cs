using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CMouss.ERM.Data
{
    public class RecordFilter
    {

        public string SearchFor { get; set; }
        public List<RecordFilterItem> RecordFilterItems { get; set; } = new();


    }



    public class RecordFilterItem
    {

        int _entityFieldId;
        string _fieldValue;
        string _operatorValue; // "=", "<", ">", "<=", ">=", "!="


        public int EntityFieldId { get { return _entityFieldId; } }
        public string FieldValue { get { return _fieldValue; } }
        public string OperatorValue { get { return _operatorValue; } }




        public RecordFilterItem(int entityFieldId, RecordFilterOperator recordFilterOperator, string fieldValue)
        {
            _entityFieldId = entityFieldId;
            _fieldValue = fieldValue;
            _operatorValue = recordFilterOperator.ToString();
        }

    }

    public enum RecordFilterOperator
    {
        Equal = 11,
        NotEqual = 12,
        Contains = 13,

        GreaterThan = 21,
        LessThan = 22,

        GreaterThanOrEqual = 31,
        LessThanOrEqual = 32

    }

}