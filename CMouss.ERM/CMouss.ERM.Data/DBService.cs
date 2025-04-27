using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Data.DBServices;

namespace CMouss.ERM.Data
{
    public class DBService
    {

        ERMDBContext context;

        EntityFieldDBService entityFieldDBService;
        public EntityFieldDBService EntityFieldDBService { get { return entityFieldDBService; } }


        EntityRelationDBService entityRelationDBService;
        public EntityRelationDBService EntityRelationDBService { get { return entityRelationDBService; } }



        TestDBService testDBService;
        public TestDBService TestDBService { get { return testDBService; } }

        EntityTypeDBService entityTypeDBService;
        public EntityTypeDBService EntityTypeDBService { get { return entityTypeDBService; } }


        EntityListViewDBService entityViewDBService;
        public EntityListViewDBService EntityListViewDBService { get { return entityViewDBService; } }


        EntityListViewFieldDBService entityViewFieldDBService;
        public EntityListViewFieldDBService EntityListViewFieldDBService { get { return entityViewFieldDBService; } }

        FieldTypeDBService fieldTypeDBService;
        public FieldTypeDBService FieldTypeDBService { get { return fieldTypeDBService; } }

        RecordDBService recordDBService;
        public RecordDBService RecordDBService { get { return recordDBService; } }

        RecordFieldValueDBService recordFieldValueDBService;
        public RecordFieldValueDBService RecordFieldValueDBService { get { return recordFieldValueDBService; } }

        RecordRelationDBService recordRelationDBService;
        public RecordRelationDBService RecordRelationDBService { get { return recordRelationDBService; } }







        public DBService()
        {
            context = new ERMDBContext();

            entityFieldDBService = new EntityFieldDBService(context);
            entityRelationDBService = new EntityRelationDBService(context);
            entityTypeDBService = new EntityTypeDBService(context);
            entityViewDBService = new EntityListViewDBService(context);
            entityViewFieldDBService = new EntityListViewFieldDBService(context);
            fieldTypeDBService = new FieldTypeDBService(context);
            recordDBService = new RecordDBService(context);
            recordFieldValueDBService = new RecordFieldValueDBService(context);
            recordRelationDBService = new RecordRelationDBService(context);
            testDBService = new TestDBService(context);

        }




    }
}
