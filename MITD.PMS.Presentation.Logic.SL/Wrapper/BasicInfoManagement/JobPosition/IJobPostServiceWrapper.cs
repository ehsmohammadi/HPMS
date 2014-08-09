using System.Collections.Generic;
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

namespace MITD.PMS.Presentation.Logic
{
    public interface IJobPositionServiceWrapper : IServiceWrapper
    { 
        void GetJobPosition(Action<JobPositionDTO, Exception> action, long id);
        void AddJobPosition(Action<JobPositionDTO, Exception> action, JobPositionDTO jobPosition);
        void UpdateJobPosition(Action<JobPositionDTO, Exception> action, JobPositionDTO jobPosition);
        void GetAllJobPositions(Action<PageResultDTO<JobPositionDTOWithActions>, Exception> action, int pageSize, int pagePost);
        void DeleteJobPosition(Action<string, Exception> action, long id);
        void GetAllJobPositions(Action<List<JobPositionDTO>, Exception> action);
    }
}
