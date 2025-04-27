using System;
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
        public string CreateUserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string LastUpdateUserId { get; set; }
        public DateTime LastUpdate { get; set; }
        public string OwnerUserId { get; set; }
        //public Dictionary<string, object> FieldValues { get; set; } = new Dictionary<string, object>();

        public virtual EntityType EntityType { get; set; } = null!; // Ensure non-nullable navigation property
        public virtual List<RecordFieldValue> RecordFieldValues { get; set; } = new(); // Initialize collection


        public virtual List<RecordRelation> RecordRelations { get; set; } = new(); // Initialize collection
        public virtual List<RecordRelation> RecordInverseRelations { get; set; } = new(); // Initialize collection

        [NotMapped]
        public List<RelatedRecord> RelatedRecords
        {
            get
            {
                var relatedRecords = new List<RelatedRecord>();
                if (RecordRelations.Count > 0)
                {
                    foreach (var recordRelation in RecordRelations)
                    {
                        var relatedRecord = new RelatedRecord
                        {
                            EntityRelationId = recordRelation.EntityRelationId,
                            RecordRelationId = recordRelation.Id,
                            RelationTitle = recordRelation.EntityRelation.Title_Right,
                            IsList = recordRelation.EntityRelation.IsList_Right,
                            IsRequired = recordRelation.EntityRelation.IsRequired_Left,
                            Record = recordRelation.RightRecord
                        };
                        relatedRecords.Add(relatedRecord);
                    }
                }
                   
                if (RecordInverseRelations.Count > 0)
                {
                    foreach (var recordRelation in RecordInverseRelations)
                    {
                        var relatedRecord = new RelatedRecord
                        {
                            EntityRelationId = recordRelation.EntityRelationId,
                            RecordRelationId = recordRelation.Id,
                            RelationTitle = recordRelation.EntityRelation.Title_Left,
                            IsList = recordRelation.EntityRelation.IsList_Left,
                            IsRequired = recordRelation.EntityRelation.IsRequired_Right,
                            Record = recordRelation.LeftRecord
                        };
                        relatedRecords.Add(relatedRecord);
                    }
                }

                return relatedRecords;
            }
        }

    }




    public class RelatedRecord
    {
        public int EntityRelationId { get; set; }
        public int RecordRelationId { get; set; }
        public string RelationTitle { get; set; }
        public bool IsList { get; set; }
        public bool IsRequired { get; set; }
        public Record Record { get; set; }



    }


}