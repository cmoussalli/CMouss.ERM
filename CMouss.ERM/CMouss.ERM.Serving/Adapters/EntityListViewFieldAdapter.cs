using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERM.Serving.Adapters
{
    public class EntityListViewFieldAdapter
    {

        public EntityListViewField Convert(Data.DBModels.EntityListViewField model)
        {
            EntityListViewField result =new ()
            {
                Id = model.Id,
                EntityListViewId = model.EntityListViewId,
                EntityFieldId = model.EntityFieldId,
                SortId = model.SortId
            };

            if (model.EntityListView is not null) { new EntityListViewAdapter().Convert(model.EntityListView); }
            if (model.EntityField is not null) { new EntityFieldAdapter().Convert(model.EntityField); }

            return result;
        }

        public List<EntityListViewField> Convert(List<Data.DBModels.EntityListViewField> models)
        {
            List<EntityListViewField> result = new();
            foreach (var model in models)
            {
                result.Add(Convert(model));
            }
            return result;
        }



    }
}
