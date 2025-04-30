using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CMouss.ERM.Data.DBModels;


namespace CMouss.ERM.Data.DBServices
{

    public class DataTypeDBService
    {
        private readonly ERMDBContext _context;

        public DataTypeDBService(ERMDBContext context)
        {
            _context = context;
        }



        public async Task<List<DataType>> GetAllAsync()
        {
            List<DataType> response = new();
            var fieldTypes = await _context.DataTypes.ToListAsync();
            return response;
        }



    }


}
