namespace MITD.PMS.Integration.Data.EF
{
    
    public class DataEFConfig
    {

        //public static int CompanyId = 22;
        //
        // 22 = حمل فله


        public static int RootUnitId = 1821;
        // 1821 = شركت كشتيراني جمهوري اسلامي
        // 4334 = مدیریت کشتی
        // 4332 = حمل فله  
        // 4333 = شرکت تامین نیرو                          



        private enum indexType : int { General = 1, Exclusive = 2, UnitIndex = 3 };

        private enum nodeType : int
        {
            Post = 1,
            Section = 2,
            Idle = 6,
            Company = 7
        };


        public static int IndexType_General = (int)indexType.General;
        public static int IndexType_Exclusive = (int)indexType.Exclusive;
        public static int IndexType_UnitIndex = (int)indexType.UnitIndex;

        public static int NodeType_Post = (int)nodeType.Post;
        public static int NodeType_Section = (int)nodeType.Section;
        public static int NodeType_Idle = (int)nodeType.Idle;
        public static int NodeType_Company = (int)nodeType.Company;

        
    }
}
