namespace CMouss.ERM.APIModels
{
    public class EntityField
    {
        public int Id { get; set; }
        public int EntityTypeId { get; set; }
        public string FieldName { get; set; }
        public int FieldTypeId { get; set; }
        public bool IsRequired { get; set; }
        public string DefaultValue { get; set; }
    }
}
