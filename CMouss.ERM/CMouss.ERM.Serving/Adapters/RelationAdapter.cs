using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERM.Serving.Adapters
{
    public class RelationAdapter
    {
        public Relation Convert(Data.DBModels.Relation model, bool loadParents, bool loadRecords)
        {
            Relation result = new()
            {
                EntityRelationId = model.EntityRelationId,
                RecordRelationId = model.RecordRelationId,
                RelationName = model.RelationName,
                RelationTitle = model.RelationTitle,
                IsList = model.IsList,
                IsRequired = model.IsRequired,
            };

            if (loadParents)
            {
                if (model.RelationEntityType is not null)
                {
                    result.RelationEntityType = new EntityTypeAdapter().Convert(model.RelationEntityType);
                }
            }

            if (loadRecords)
            {
                if (model.Records is not null)
                {
                    result.Records = new RecordAdapter().Convert(model.Records);
                }
            }

            return result;
        }



        public List<Relation> Convert(List<Data.DBModels.Relation> models, bool loadParents, bool loadRecords)
        {
            List<Relation> result = new();
            if (models is not null)
            {
                if (models.Count > 0)
                {
                    foreach (var model in models)
                    {
                        result.Add(Convert(model, loadParents, loadRecords));
                    }
                }
            }
            return result;
        }



    }


}
