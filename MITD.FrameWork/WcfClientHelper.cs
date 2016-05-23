using System;
using System.ServiceModel;
using MITD.Domain.Model;

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

        public static void CallMethod<TC, T1>(Action<TC, T1> func, TC client, T1 param1
            ,IFaultExceptionAdapter errorAdapter) where TC : ICommunicationObject
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
        
        public static void CallMethod<TC, T1, T2>(Action<TC, T1, T2> func, TC client, T1 param1, T2 param2
            , IFaultExceptionAdapter errorAdapter) where TC : ICommunicationObject
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

        public static void CallMethod<TC, T1, T2, T3>(Action<TC, T1, T2, T3> func, TC client, T1 param1, T2 param2,
            T3 param3, IFaultExceptionAdapter errorAdapter) where TC : ICommunicationObject
        {
            try
            {
                func(client, param1, param2, param3);
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
