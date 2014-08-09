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
    public interface IUnitServiceWrapper : IServiceWrapper
    { 
        void GetUnit(Action<UnitDTO, Exception> action, long id);
        void AddUnit(Action<UnitDTO, Exception> action, UnitDTO unit);
        void UpdateUnit(Action<UnitDTO, Exception> action, UnitDTO unit);
        void GetAllUnits(Action<PageResultDTO<UnitDTOWithActions>, Exception> action, int pageSize, int pagePost);
        void DeleteUnit(Action<string, Exception> action, long id);
        void GetAllUnits(Action<List<UnitDTO>, Exception> action);
    }
}
