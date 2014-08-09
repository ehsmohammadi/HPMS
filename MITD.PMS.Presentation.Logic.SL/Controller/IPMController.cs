using System;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public interface IPMController : IApplicationController
    {
        void HandleException(Exception exp);
        void GetRemoteInstance<T>(Action<T, Exception> action) where T : class;
    }
}
