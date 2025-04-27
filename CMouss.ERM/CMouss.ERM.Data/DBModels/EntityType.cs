using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERM.Data.DBModels
{
    public class EntityType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PluralName { get; set; }

        public int? DefaultEntityListViewID { get; set; }

        public bool IsDeleted { get; set; }

        public virtual List<EntityField> EntityFields { get; set; } = new();
        public virtual List<EntityListView> EntityListViews { get; set; } = new();
        public virtual List<Record> Records { get; set; } = new();

        [ForeignKey(nameof(DefaultEntityListViewID))]
        public virtual EntityListView? DefaultEntityListView { get; set; }


        public virtual List<EntityRelation> EntityRelations_Left { get; set; }
        public virtual List<EntityRelation> EntityRelation_Right { get; set; }


        //[NotMapped]
        //public virtual List<EntityRelation> EntityRelations
        //{
        //    get
        //    {

        //    }
        //}



    }
}