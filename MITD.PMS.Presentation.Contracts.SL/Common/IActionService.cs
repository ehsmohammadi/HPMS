using System;

namespace MITD.PMS.Presentation.Contracts
{
    public  interface IActionService<T>:IActionService
    {
        void DoAction(T dto);

    }

    public interface IActionService
    {
        

    }



}
