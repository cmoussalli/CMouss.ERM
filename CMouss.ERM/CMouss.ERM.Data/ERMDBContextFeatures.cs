using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMouss.ERM.Data.DBModels;

namespace CMouss.ERM.Data
{
    public partial class ERMDBContext
    {

        public void InsertTestData()
        {


            #region DataTypes

            //DataTypes
            DataType fieldType11 = new()
            {
                Id = 11,
                Name = "SingleLine Text",
            };
            DataTypes.Add(fieldType11);
            DataType fieldType12 = new()
            {
                Id = 12,
                Name = "Multiline Text",
            };
            DataTypes.Add(fieldType12);


            DataType fieldType31 = new()
            {
                Id = 31,
                Name = "Number",
            };
            DataTypes.Add(fieldType31);
            DataType fieldType32 = new()
            {
                Id = 32,
                Name = "NumberWithDecimals",
            };
            DataTypes.Add(fieldType32);

            DataType fieldType41 = new()
            {
                Id = 41,
                Name = "DateTime",
            };
            DataTypes.Add(fieldType41);
            DataType fieldType42 = new()
            {
                Id = 42,
                Name = "Date",
            };
            DataTypes.Add(fieldType42);
            DataType fieldType43 = new()
            {
                Id = 43,
                Name = "Time",
            };
            DataTypes.Add(fieldType43);


            DataType fieldType51 = new()
            {
                Id = 51,
                Name = "DropDown",
            };
            DataTypes.Add(fieldType51);




            DataType fieldType61 = new()
            {
                Id = 61,
                Name = "Boolean",
            };
            DataTypes.Add(fieldType61);


            DataType fieldType101 = new()
            {
                Id = 101,
                Name = "Email",
            };
            DataTypes.Add(fieldType101);

            DataType fieldType102 = new()
            {
                Id = 102,
                Name = "URL",
            };
            DataTypes.Add(fieldType102);

            DataType fieldType103 = new()
            {
                Id = 103,
                Name = "GeoLocation",
            };
            DataTypes.Add(fieldType103);

            #endregion


            #region EntityTypes + Fields

            #region Contact

            //EntityTypes and fields
            EntityType entityType1_Contact = new()
            {
                Id = 1,
                Name = "Contact",
                PluralName = "Contacts",
                PostUpdateScript = "r.Email = r.LastName + \", \" + r.FirstName;",
                IsDeleted = false,
            };
            EntityTypes.Add(entityType1_Contact);

            #region Contact Fields
            EntityField entityField101_Contact_FirstName = new()
            {
                Id = 1,
                EntityTypeId = 1,
                Name = "FirstName",
                DataTypeId = 11,
                IsRequired = true
            };
            EntityFields.Add(entityField101_Contact_FirstName);
            EntityField entityField102_Contact_LastName = new()
            {
                Id = 2,
                EntityTypeId = 1,
                Name = "LastName",
                DataTypeId = 11,
                IsRequired = true,
            };
            EntityFields.Add(entityField102_Contact_LastName);
            EntityField entityField103_Contact_Email = new()
            {
                Id = 3,
                EntityTypeId = 1,
                Name = "Email",
                DataTypeId = 101,
                IsRequired = false,
            };
            EntityFields.Add(entityField103_Contact_Email);
            #endregion

            #region EntityListViews
            EntityListView entityView1 = new()
            {
                Id = 1,
                EntityTypeId = 1,
                Name = "All Contacts",
                IsDeleted = false,
                IsPublished = true,
            };
            EntityListViews.Add(entityView1);
            EntityListViewField entityViewField1 = new() {Id = 1,EntityListViewId = 1,EntityFieldId = 1,SortId = 1};
            EntityListViewField entityViewField2 = new() {Id = 2,EntityListViewId = 1,EntityFieldId = 2,SortId = 2};


            #endregion

            #endregion

            #region Company
            EntityType entityType2 = new()
            {
                Id = 2,
                Name = "Company",
                PluralName = "Companies",
                IsDeleted = false
            };
            EntityTypes.Add(entityType2);
            EntityField entityField201 = new()
            {
                Id = 4,
                EntityTypeId = 2,
                Name = "Company Name",
                DataTypeId = 11,
                IsRequired = true,
            };
            EntityFields.Add(entityField201);

            #endregion

            #endregion

            #region Entity Relations

            #region Company-Contact
            EntityRelation entityRelation1 = new()
            {
                Id = 1,
                Name= "Company-Contact",
                
                Title_Left = "Employer",
                EntityTypeId_Left = 2,
                IsRequired_Left = false,
                IsList_Left = true,

                Title_Right = "Employee",
                EntityTypeId_Right = 1,
                IsRequired_Right = false,
                IsList_Right = true,
                
                IsDeleted = false

            };
            EntityRelations.Add(entityRelation1);

            SaveChanges();
            #endregion

            #endregion

            #region EntityListViews

            // Use existing EntityListView for Contacts
            var entityListView1_AllContacts = EntityListViews.First(elv => elv.Id == 1);

            EntityListViewField entityListViewField1_AllContacts_FirstName = new()
            {
                Id = 1,
                EntityListViewId = entityListView1_AllContacts.Id,
                EntityFieldId = entityField101_Contact_FirstName.Id,
                SortId = 1
            }; EntityListViewFields.Add(entityListViewField1_AllContacts_FirstName);

            EntityListViewField entityListViewField1_AllContacts_LastName = new()
            {
                Id = 2,
                EntityListViewId = entityListView1_AllContacts.Id,
                EntityFieldId = entityField102_Contact_LastName.Id,
                SortId = 2
            }; EntityListViewFields.Add(entityListViewField1_AllContacts_LastName);

            //Make it default view for EntityType Contact
            entityType1_Contact.DefaultEntityListViewID = entityListView1_AllContacts.Id;


            SaveChanges();
            #endregion




            #region Records
            Record record1 = new()
            {
                Id = 1,
                EntityTypeId = 1,
                Name = "John Doe",
                CreateDateTime = DateTime.Now,
                LastUpdate = DateTime.Now,
                CreateUserId = "",
                OwnerUserId = "",
                LastUpdateUserId = ""
            };
            Records.Add(record1);
            RecordFieldValue recordFieldValue1 = new()
            {
                Id = 1,
                RecordId = 1,
                EntityFieldId = 1,
                FieldValue = "John",
            };
            RecordFieldValues.Add(recordFieldValue1);
            RecordFieldValue recordFieldValue2 = new()
            {
                Id = 2,
                RecordId = 1,
                EntityFieldId = 2,
                FieldValue = "Doe",
            };
            RecordFieldValues.Add(recordFieldValue2);
            RecordFieldValue recordFieldValue3 = new()
            {
                Id = 3,
                RecordId = 1,
                EntityFieldId = 3,
                FieldValue = "johndoe@mail.com"
            };
            RecordFieldValues.Add(recordFieldValue3);

            Record record2 = new()
            {
                Id = 2,
                Name = "Jane Smith",
                EntityTypeId = 1,
                CreateDateTime = DateTime.Now,
                LastUpdate = DateTime.Now,
                CreateUserId = "",
                OwnerUserId = "",
                LastUpdateUserId = ""
            };
            Records.Add(record2);
            RecordFieldValue recordFieldValue4 = new()
            {
                Id = 4,
                RecordId = 2,
                EntityFieldId = 1,
                FieldValue = "Jane",
            };
            RecordFieldValues.Add(recordFieldValue4);
            RecordFieldValue recordFieldValue5 = new()
            {
                Id = 5,
                RecordId = 2,
                EntityFieldId = 2,
                FieldValue = "Smith",
            };
            RecordFieldValues.Add(recordFieldValue5);
            RecordFieldValue recordFieldValue6 = new()
            {
                Id = 6,
                RecordId = 2,
                EntityFieldId = 3,
                FieldValue = "janesmith@mail.com"
            };
            RecordFieldValues.Add(recordFieldValue6);

            Record record3 = new()
            {
                    Id = 3,
                Name = "ACME Corp",
                EntityTypeId = 2,
                CreateDateTime = DateTime.Now,
                LastUpdate = DateTime.Now,
                CreateUserId = "",
                OwnerUserId = "",
                LastUpdateUserId = ""
            };
            Records.Add(record3);

            RecordFieldValue recordFieldValue7 = new()
            {
                Id = 7,
                RecordId = 3,
                EntityFieldId = 4,
                FieldValue = "ACME Corp",
            };
            RecordFieldValues.Add(recordFieldValue7);

            Record record4 = new()
            {
                Id = 4,
                Name = "ACME2 Corp",
                EntityTypeId = 2,
                CreateDateTime = DateTime.Now,
                LastUpdate = DateTime.Now,
                CreateUserId = "",
                OwnerUserId = "",
                LastUpdateUserId = ""
            };
            Records.Add(record4);

            RecordFieldValue recordFieldValue8 = new()
            {
                Id = 8,
                RecordId = 4,
                EntityFieldId = 4,
                FieldValue = "ACME2 Corp",
            };
            RecordFieldValues.Add(recordFieldValue8);



            #endregion





            #region Relations

            #region Company-Contact

            RecordRelation recordRelation1 = new()
            {
                Id = 1,
                LeftRecordId = 3,
                RightRecordId = 1,
                EntityRelationId = 1,
                CreateDateTime = DateTime.Now,
                CreateUserId = "",
            };
            RecordRelations.Add(recordRelation1);

            RecordRelation recordRelation2 = new()
            {
                Id = 2,
                LeftRecordId = 3,
                RightRecordId = 2,
                EntityRelationId = 1,
                CreateDateTime = DateTime.Now,
                CreateUserId = "",
            };

            #endregion  

            #endregion



            this.SaveChanges();
        }



    }
}
