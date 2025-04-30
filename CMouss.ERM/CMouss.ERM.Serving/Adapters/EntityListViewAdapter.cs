using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERM.Serving.Adapters
{
    public class EntityListViewAdapter
    {

        public EntityListView Convert(Data.DBModels.EntityListView model)
        {
            return new EntityListView
            {
                Id = model.Id,
                Name = model.Name,
                EntityTypeId = model.EntityTypeId,
                IsDeleted = model.IsDeleted,
                IsPublished = model.IsPublished,
                EntityListViewFields = model.EntityListViewFields.Select(x => new EntityListViewField
                {
                    Id = x.Id,
                    EntityFieldId = x.EntityFieldId,
                    EntityListViewId = x.EntityListViewId,
                    SortId = x.SortId
                }).ToList()
            };

        }


        public List<EntityListView> Convert(List<Data.DBModels.EntityListView> models)
        {
            return models.Select(Convert).ToList();
        }



    }
}
