using MITD.Presentation;
using MITD.PMS.Presentation.Contracts;
using System.Collections.ObjectModel;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace MITD.PMS.Presentation.Logic
{
    public interface IJobServiceWrapper : IServiceWrapper
    { 
        void GetJob(Action<JobDTO, Exception> action, long id);
        void AddJob(Action<JobDTO, Exception> action, JobDTO job);
        void UpdateJob(Action<JobDTO, Exception> action, JobDTO job);
        void GetAllJobs(Action<PageResultDTO<JobDTOWithActions>, Exception> action, int pageSize, int pageIndex, Dictionary<string, string> sortBy);
        void GetAllJobs(Action<IList<JobDTO>, Exception> action);      
        void DeleteJob(Action<string, Exception> action, long id);
        void GetJobCustomFields(Action<List<AbstractCustomFieldDescriptionDTO>, Exception> action, long id);


    }
}
