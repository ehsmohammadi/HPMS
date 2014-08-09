using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Presentation.Contracts;
 
namespace MITD.PMS.Presentation.Logic
{
    public static partial class TestData
    {
        private static List<int> jobIndexActionCode = new List<int> { 201, 202 };
        private static List<int> jobIndexCategoryActionCode = new List<int> { 200, 101, 102, 103 };

        public static List<JobIndexDTO> JobIndexList = new List<JobIndexDTO>
            {
                new JobIndexDTO()
                    {
                        Name = "فرهنگ و تعهد سازمانی",
                        DictionaryName ="CultureAndCommitment" ,
                        Id = 10,
                    },
                new JobIndexDTO()
                    {
                        Name = "رشد و تعالی سازمانی",
                        DictionaryName ="Excellency" ,
                        Id =11,
                    },
                new JobIndexDTO()
                    {
                        Name = "نظم و انظباط",
                        DictionaryName ="Discipline" ,
                        Id = 12,
                    },
                new JobIndexDTO()
                    {
                        Name = "پایبندی به ارزشهای سازمانی",
                        DictionaryName ="OrganizationalValues" ,
                        Id = 13,
                    },
                new JobIndexDTO()
                    {
                        Name = "تلاش و سخت کوشی",
                        DictionaryName ="Effort" ,
                        Id = 14, 
                    },
                    new JobIndexDTO()
                    {
                        Name = "تسلط بر زبان انگلیسی",
                        DictionaryName ="EnglishLanguageProficiency" ,
                        Id = 15, 
                    },
                    new JobIndexDTO()
                    {
                        Name = "آشنایی با ICDL",
                        DictionaryName ="ICDL " ,
                        Id = 16, 
                    },
                    new JobIndexDTO()
                    {
                        Name = "سلامت روحی و جسمی",
                        DictionaryName ="PhysicalAndMentalHealth" ,
                        Id = 17, 
                    },
                    new JobIndexDTO()
                    {
                        Name = "كنترل هزینه",
                        DictionaryName ="CostControl" ,
                        Id = 18, 
                    },
                    new JobIndexDTO()
                    {
                        Name = "بودجه بندی",
                        DictionaryName ="Budgeting" ,
                        Id = 19, 
                    },
                    new JobIndexDTO()
                    {
                        Name = "برنامه ریزی و کنترل تعمیرات ادواری ( زمان)",
                        DictionaryName ="xxxx" ,
                        Id = 20, 
                    },
                    new JobIndexDTO()
                    {
                        Name = "برنامه ریزی و کنترل تعمیرات ادواری (كيفيت)",
                        DictionaryName ="xxxxx" ,
                        Id = 21, 
                    },
                    new JobIndexDTO()
                    {
                        Name = "قابلیت برنامه ریزی و کنترل تعمیرات سفری",
                        DictionaryName ="xxxxx" ,
                        Id = 22, 
                    },
                    new JobIndexDTO()
                    {
                        Name = "پروسه انتخاب پیمانکاران",
                        DictionaryName ="xxxxx" ,
                        Id = 23, 
                    },
                    new JobIndexDTO()
                    {
                        Name = "کنترل عملکرد پیمانکاران",
                        DictionaryName ="xxxxx" ,
                        Id = 24, 
                    },
                    new JobIndexDTO()
                    {
                        Name = "مدت زمان Off HIRE فني (ساعت)",
                        DictionaryName ="xxxxxx" ,
                        Id = 25, 
                    },
                    new JobIndexDTO()
                    {
                        Name = "تعداد نواقص اعلام شده توسط FSC و PSC برای شناورها",
                        DictionaryName ="xxxxxx" ,
                        Id = 26, 
                    },
                    new JobIndexDTO()
                    {
                        Name = "آنالير مشكلات فني ، پیگیری نواقص و ارائه راهکار",
                        DictionaryName ="xxxxxx" ,
                        Id = 27, 
                    },
                    new JobIndexDTO()
                    {
                        Name = "به روزنگه داشتن گواهینامه ها و رعایت الزامات قانونی",
                        DictionaryName ="xxxxxx" ,
                        Id = 28, 
                    },
                    new JobIndexDTO()
                    {
                        Name = "گذراندن دوره های تخصصی ",
                        DictionaryName ="xxxxxx" ,
                        Id = 29, 
                    },
                    new JobIndexDTO()
                    {
                        Name = "سن کشتی",
                        DictionaryName ="xxxxxx" ,
                        Id = 30, 
                    },
                    new JobIndexDTO()
                    {
                        Name = "نوع کشتی",
                        DictionaryName ="xxxxxx" ,
                        Id = 31, 
                    },
                    new JobIndexDTO()
                    {
                        Name = "تعداد کشتی",
                        DictionaryName ="xxxxxx" ,
                        Id = 32, 
                    },
                    new JobIndexDTO()
                    {
                        Name = "مکان و محل تعمیرات",
                        DictionaryName ="xxxxxx" ,
                        Id = 33, 
                    },
                    new JobIndexDTO()
                    {
                        Name = "کیفیت پرسنل كشتي",
                        DictionaryName ="xxxxxx" ,
                        Id = 34, 
                    },
            };


        public static List<AbstractJobIndexDTOWithActions> jobIndexAndCategoryList = new List<AbstractJobIndexDTOWithActions>
            {
                new JobIndexCategoryDTOWithActions
                    {
                        Name = "دسته شاخص 1",
                        Id = 1,
                        ParentId = null,
                        ActionCodes = jobIndexCategoryActionCode
                    },
                new JobIndexCategoryDTOWithActions
                    {
                        Name = "دسته شاخص 2",
                        Id = 2,
                        ParentId = null,
                        ActionCodes =jobIndexCategoryActionCode
                    },
                new JobIndexCategoryDTOWithActions
                    {
                        Name = "دسته شاخص 3",
                        Id = 3,
                        ParentId = null,
                        ActionCodes =jobIndexCategoryActionCode
                    },
                new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==10).Name,
                        Id = JobIndexList.Single(p=>p.Id==10).Id,
                        ParentId = 1,
                        ActionCodes = jobIndexActionCode
                    },
                new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==11).Name,
                        Id = JobIndexList.Single(p=>p.Id==11).Id,
                        ParentId = 1,
                        ActionCodes = jobIndexActionCode
                    },
                     new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==12).Name,
                        Id = JobIndexList.Single(p=>p.Id==12).Id,
                        ParentId = 1,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==13).Name,
                        Id = JobIndexList.Single(p=>p.Id==13).Id,
                        ParentId = 1,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==14).Name,
                        Id = JobIndexList.Single(p=>p.Id==14).Id,
                        ParentId = 1,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==15).Name,
                        Id = JobIndexList.Single(p=>p.Id==15).Id,
                        ParentId = 1,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==16).Name,
                        Id = JobIndexList.Single(p=>p.Id==16).Id,
                        ParentId = 1,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==17).Name,
                        Id = JobIndexList.Single(p=>p.Id==17).Id,
                        ParentId = 1,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==18).Name,
                        Id = JobIndexList.Single(p=>p.Id==18).Id,
                        ParentId = 2,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==19).Name,
                        Id = JobIndexList.Single(p=>p.Id==19).Id,
                        ParentId = 2,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==20).Name,
                        Id = JobIndexList.Single(p=>p.Id==20).Id,
                        ParentId = 2,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==21).Name,
                        Id = JobIndexList.Single(p=>p.Id==21).Id,
                        ParentId = 2,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==22).Name,
                        Id = JobIndexList.Single(p=>p.Id==22).Id,
                        ParentId = 2,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==23).Name,
                        Id = JobIndexList.Single(p=>p.Id==23).Id,
                        ParentId = 2,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==24).Name,
                        Id = JobIndexList.Single(p=>p.Id==24).Id,
                        ParentId = 2,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==25).Name,
                        Id = JobIndexList.Single(p=>p.Id==25).Id,
                        ParentId = 2,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==26).Name,
                        Id = JobIndexList.Single(p=>p.Id==26).Id,
                        ParentId = 2,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==27).Name,
                        Id = JobIndexList.Single(p=>p.Id==27).Id,
                        ParentId = 2,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==28).Name,
                        Id = JobIndexList.Single(p=>p.Id==28).Id,
                        ParentId = 2,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==29).Name,
                        Id = JobIndexList.Single(p=>p.Id==29).Id,
                        ParentId = 2,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==30).Name,
                        Id = JobIndexList.Single(p=>p.Id==30).Id,
                        ParentId = 3,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==31).Name,
                        Id = JobIndexList.Single(p=>p.Id==31).Id,
                        ParentId = 3,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==32).Name,
                        Id = JobIndexList.Single(p=>p.Id==32).Id,
                        ParentId = 3,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==33).Name,
                        Id = JobIndexList.Single(p=>p.Id==33).Id,
                        ParentId = 3,
                        ActionCodes = jobIndexActionCode
                    },
                    new JobIndexDTOWithActions
                    {
                        Name = JobIndexList.Single(p=>p.Id==34).Name,
                        Id = JobIndexList.Single(p=>p.Id==34).Id,
                        ParentId = 3,
                        ActionCodes = jobIndexActionCode
                    },
            };

        public static List<AbstractCustomFieldDescriptionDTO> jobIndexCustomFieldDescription =
            new List<AbstractCustomFieldDescriptionDTO>();

        public static List<CustomFieldDTO> jobIndexCustomFields = new List<CustomFieldDTO>();
    }
}
