using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CMouss.ERM.Data.DBModels
{
    public class EntityType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PluralName { get; set; }

        public int? DefaultEntityListViewID { get; set; }

        public bool IsDeleted { get; set; }



        public List<EntityField> EntityFields { get; set; } = new();

        public List<EntityListView> EntityListViews { get; set; } = new();

        public List<Record> Records { get; set; } = new();

        public EntityListView? DefaultEntityListView { get; set; }

        public List<EntityRelation> EntityRelations_Left { get; set; }

        public List<EntityRelation> EntityRelation_Right { get; set; }


    }
}