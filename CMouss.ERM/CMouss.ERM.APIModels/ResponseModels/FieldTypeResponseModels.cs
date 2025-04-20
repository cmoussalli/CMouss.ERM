using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERM.APIModels.ResponseModels
{
    public class FieldTypeResponseModels
    {

        public class GetAll: GenericResponseModel
        {
            public List<FieldType> FieldTypes { get; set; }

        }


    }


}
