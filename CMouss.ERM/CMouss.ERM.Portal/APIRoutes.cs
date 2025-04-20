namespace CMouss.ERM.Portal
{
    public class APIRoutes
    {
        #region Base
        public const string Base = "api/ERM";

        #endregion


        #region Test
        public static class Test
        {
            public const string TestMain = Base + "/Test";
            const String entity = "Test";

            public const string Echo = Base + "/" + entity + "/Echo";
        }
        #endregion


        #region FieldType
        public static class FieldType
        {
            const String entity = "FieldType";
            public const string GetList = Base + "/" + entity + "/GetList";


        }
        #endregion




    }
}
