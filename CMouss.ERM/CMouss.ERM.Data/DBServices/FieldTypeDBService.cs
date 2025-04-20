using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CMouss.ERM.Data.DBModels;


namespace CMouss.ERM.Data.DBServices
{

    public class FieldTypeDBService
    {
        private readonly ERMDBContext _context;

        public FieldTypeDBService(ERMDBContext context)
        {
            _context = context;
        }



        public async Task<List<FieldType>> GetAll()
        {
            List<FieldType> response = new();
            var fieldTypes = await _context.FieldTypes.ToListAsync();
            return response;
        }



    }


}
