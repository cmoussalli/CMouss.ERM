using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CMouss.ERM.Data.DBModels;


namespace CMouss.ERM.Data.DBServices
{

    public class TestDBService
    {
        private readonly ERMDBContext _context;

        public TestDBService(ERMDBContext context)
        {
            _context = context;
        }



        public async Task InsertTestData()
        {
            
        }



    }


}
