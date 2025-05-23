﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMouss.ERM.Data.DBModels
{
    public class Record
    {
        public int Id { get; set; }
        public int EntityTypeId { get; set; }
        public string Name { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string LastUpdateUserId { get; set; }
        public DateTime LastUpdate { get; set; }

        public int IterationId { get; set; }

        public bool IsDeleted { get; set; } = false;

        public string OwnerUserId { get; set; }
        //public Dictionary<string, object> FieldValues { get; set; } = new Dictionary<string, object>();

        public virtual EntityType EntityType { get; set; } = null!; // Ensure non-nullable navigation property
        public virtual List<RecordFieldValue> RecordFieldValues { get; set; } = new(); // Initialize collection


        public virtual List<RecordRelation> RecordRelations { get; set; } = new(); // Initialize collection
        public virtual List<RecordRelation> RecordInverseRelations { get; set; } = new(); // Initialize collection

        [NotMapped]
        public List<Relation> Relations
        {
            get
            {
                List<Relation> relas = new();
                if (RecordRelations.Count > 0)
                {
                    var relas1 = from r in RecordRelations
                                 group r by r.EntityRelationId into g
                                 select new Relation
                                 {
                                     EntityRelationId = g.Key,
                                     RecordRelationId = g.FirstOrDefault().Id,
                                     RelationName = g.FirstOrDefault().EntityRelation.Name,
                                     RelationTitle = g.FirstOrDefault().EntityRelation.Title_Right,
                                     IsList = g.FirstOrDefault().EntityRelation.IsList_Right,
                                     IsRequired = g.FirstOrDefault().EntityRelation.IsRequired_Right,
                                     Records = g.Select(x => x.RightRecord).ToList(),
                                     RelationEntityType = g.FirstOrDefault().EntityRelation.EntityType_Right
                                 };
                    relas.AddRange(relas1);
                }

                if (RecordInverseRelations.Count > 0)
                {
                    var relas2 = from r in RecordInverseRelations
                                 group r by r.EntityRelationId into g
                                 select new Relation
                                 {
                                     EntityRelationId = g.Key,
                                     RecordRelationId = g.FirstOrDefault().Id,
                                     RelationName = g.FirstOrDefault().EntityRelation.Name,
                                     RelationTitle = g.FirstOrDefault().EntityRelation.Title_Left,
                                     IsList = g.FirstOrDefault().EntityRelation.IsList_Left,
                                     IsRequired = g.FirstOrDefault().EntityRelation.IsRequired_Left,
                                     Records = g.Select(x => x.LeftRecord).ToList(),
                                     RelationEntityType = g.FirstOrDefault().EntityRelation.EntityType_Left
                                 };
                    relas.AddRange(relas2);
                }

                return relas.ToList();
            }

        }

    }




    public class Relation
    {
        public int EntityRelationId { get; set; }
        public int RecordRelationId { get; set; }
        public string RelationName { get; set; }
        public string RelationTitle { get; set; }
        public bool IsList { get; set; }
        public bool IsRequired { get; set; }

        public List<Record> Records { get; set; }
        public EntityType RelationEntityType { get; set; }


    }


    public class RecordValue_Save
    {
        public int EntityFieldId { get; set; }
        public string Value { get; set; }

        public RecordValue_Save(int entityFieldId, string value)
        {
            EntityFieldId = entityFieldId;
            Value = value;
        }
    }


    public class RecordRelation_Save
    {
        public int EntityRelationId { get; set; }
        public List<int> RecordIds { get; set; } = new();

        public RecordRelation_Save(int entityRelationId, List<int> recordIds)
        {
            EntityRelationId = entityRelationId;
            RecordIds = recordIds;
        }
    }
    

}