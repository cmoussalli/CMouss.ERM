using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERM.APIModels.ResponseModels
{

    public class GeenricResponse
    {
        public GenericResponseModel Status { get; set; }
    }

    public class GenericResponseModel
    {
        public string Message { get; set; }
        public string ReferenceId { get; set; }
        public bool IsSuccess { get; set; }


        public GenericResponseModel()
        {
            IsSuccess = false;
            Message = "";
            ReferenceId = "";
        }

        public GenericResponseModel(bool isSuccess)
        {
            IsSuccess = isSuccess;
            Message = "";
            ReferenceId = "";
        }
        public GenericResponseModel(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
            ReferenceId = "";
        }
        public GenericResponseModel(bool isSuccess, string message, string referenceId)
        {
            IsSuccess = isSuccess;
            Message = message;
            ReferenceId = referenceId;

        }
    }



}
