using System.Collections.Generic;
using MITD.Presentation;
using MITD.PMS.Presentation.Contracts;
using System;

namespace MITD.PMS.Presentation.Logic
{
    public partial interface IUnitIndexServiceWrapper : IServiceWrapper
    { 
        void GetUnitIndex(Action<UnitIndexDTO, Exception> action, long id);
        void AddUnitIndex(Action<UnitIndexDTO, Exception> action, UnitIndexDTO unitIndex);
        void UpdateUnitIndex(Action<UnitIndexDTO, Exception> action, UnitIndexDTO unitIndex);
        void DeleteUnitIndex(Action<string, Exception> action, long id);
        void GetAllAbstractUnitIndex(Action<List<AbstractUnitIndexDTOWithActions>, Exception> action);

        //void GetUnitIndexCustomFields(Action<List<AbstractCustomFieldDescriptionDTO>, Exception> action, long id);
        //void GetUnitIndexCustomFields(Action<List<AbstractCustomFieldDTO>, Exception> action, List<long> fieldIdList);
        //void GetUnitIndexEntityCustomFields(Action<List<AbstractCustomFieldDescriptionDTO>, Exception> action);
        //void GetCustomFields(Action<List<AbstractCustomFieldDescriptionDTO>, Exception> action, long unitIndexId);


        void GetAllUnitIndex(Action<List<UnitIndexDTO>, Exception> action);
    }
}
