using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Data;
using CMouss.ERM.Serving;
using CMouss.ERM.Serving.ServingServices;

namespace CMouss.ERM.Serving
{

    public enum ServingHubConnectionType
    {
        SQLite,
        SQLServer
    }
    public class ServingHubConfig
    {
        public string ConnectionString { get; set; }
        public ServingHubConnectionType ServingHubConnectionType { get; set; } = ServingHubConnectionType.SQLite;


    }

    public class ServingServiceParams
    {
        public ServingHubConfig ServingHubConfig { get; set; }
        public bool IsConnected { get; set; }
        public bool IsConfigured { get; set; }
    }




    public partial class ERMServingHub
    {
        #region Props
        
        ServingHubConfig conf;
        public string ConnectionString { get => conf.ConnectionString; }


        #endregion

        private ServingServiceParams GetServingServiceParams()
        {
            var serviceParams = new ServingServiceParams();
            serviceParams.IsConnected = false;
            serviceParams.IsConfigured = false;
            if (conf != null)
            {
                serviceParams.IsConfigured = true;
                if (!string.IsNullOrEmpty(conf.ConnectionString))
                {
                    serviceParams.IsConnected = true;
                }
            }
            return serviceParams;
        }



        #region Services
        public EntityTypeService EntityTypeService { get { return new EntityTypeService(GetServingServiceParams()); } }


        #endregion




        public ERMServingHub(ServingHubConfig config)
        {
            conf = config;

        }






    }


}
