using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public partial interface IUnitIndexServiceWrapper
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
