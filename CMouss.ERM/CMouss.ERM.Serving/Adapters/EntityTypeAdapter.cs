using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Serving;


namespace CMouss.ERM.Serving.Adapters
{
    public class EntityTypeAdapter
    {
        public EntityType Convert(Data.DBModels.EntityType model)
        {
            EntityType response = new();
            response.Id = model.Id;
            response.Name = model.Name;
            response.PluralName = model.PluralName;
            response.PostUpdateScript = model.PostUpdateScript;
            response.IsDeleted = model.IsDeleted;

            if (model.EntityFields is not null) { new EntityFieldAdapter().Convert(model.EntityFields); }
            if (model.EntityListViews is not null) { new EntityListViewAdapter().Convert(model.EntityListViews); }
            if (model.Records is not null) { new RecordAdapter().Convert(model.Records); }
            if (model.DefaultEntityListView is not null) { response.DefaultEntityListView = new EntityListViewAdapter().Convert(model.DefaultEntityListView); }
            if (model.EntityRelations_Left is not null) { new EntityRelationAdapter().Convert(model.EntityRelations_Left); }
            if (model.EntityRelation_Right is not null) { new EntityRelationAdapter().Convert(model.EntityRelation_Right); }


            return response;
        }

        public List<EntityType> Convert(List<Data.DBModels.EntityType> models)
        {
            List<EntityType> result = new();
            foreach (var model in models)
            {
                result.Add(Convert(model));
            }
            return result;
        }
    }
}
