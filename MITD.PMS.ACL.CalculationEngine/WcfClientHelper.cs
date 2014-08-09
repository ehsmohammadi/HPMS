using MITD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MITD.Services
{
    public static class WcfClientHelper
    {
        public static T CallMethod<C, T1, T>(Func<C, T1, T> func, C client, T1 param1
            ,IFaultExceptionAdapter errorAdapter) where C : ICommunicationObject
        {
            try
            {
                return func(client, param1);
            }
            catch (FaultException<ErrorDetail> exp)
            {
                throw errorAdapter.ConvertToException(exp);
            }
            catch (CommunicationException)
            {
                client.Abort();
                throw;
            }
            catch (TimeoutException)
            {
                client.Abort();
                throw;
            }
            catch
            {
                if (client.State == CommunicationState.Faulted)
                {
                    client.Abort();
                }

                throw;
            }
            finally
            {
                try
                {
                    if (client.State != CommunicationState.Faulted)
                    {
                        client.Close();
                    }
                }
                catch
                {
                    client.Abort();
                }
            }
        }

        public static void CallMethod<C, T1>(Action<C, T1> func, C client, T1 param1
            ,IFaultExceptionAdapter errorAdapter) where C : ICommunicationObject
        {
            try
            {
                func(client, param1);
            }
            catch (FaultException<ErrorDetail> exp)
            {
                throw errorAdapter.ConvertToException(exp);
            }
            catch (CommunicationException)
            {
                client.Abort();
                throw;
            }
            catch (TimeoutException)
            {
                client.Abort();
                throw;
            }
            catch
            {
                if (client.State == CommunicationState.Faulted)
                {
                    client.Abort();
                }

                throw;
            }
            finally
            {
                try
                {
                    if (client.State != CommunicationState.Faulted)
                    {
                        client.Close();
                    }
                }
                catch
                {
                    client.Abort();
                }

            }
        }
        
        public static void CallMethod<C, T1, T2>(Action<C, T1, T2> func, C client, T1 param1, T2 param2
            , IFaultExceptionAdapter errorAdapter) where C : ICommunicationObject
        {
            try
            {
                func(client, param1, param2);
            }
            catch (FaultException<ErrorDetail> exp)
            {
                throw errorAdapter.ConvertToException(exp);
            }
            catch (CommunicationException)
            {
                client.Abort();
                throw;
            }
            catch (TimeoutException)
            {
                client.Abort();
                throw;
            }
            catch
            {
                if (client.State == CommunicationState.Faulted)
                {
                    client.Abort();
                }

                throw;
            }
            finally
            {
                try
                {
                    if (client.State != CommunicationState.Faulted)
                    {
                        client.Close();
                    }
                }
                catch
                {
                    client.Abort();
                }

            }
        }
    }
}
