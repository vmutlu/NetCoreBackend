using Castle.Core.Interceptor;

namespace Apsiyon.Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptorBaseAttribute
    {
        protected virtual void OnBefore(IInvocation ınvocation)
        {

        }

        protected virtual void OnAfter(IInvocation ınvocation)
        {

        }

        protected virtual void OnException(IInvocation ınvocation)
        {

        }

        protected virtual void OnSuccess(IInvocation ınvocation)
        {

        }

        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);

            try
            {
                invocation.Proceed();
            }
            catch (System.Exception e)
            {
                isSuccess = false;
                OnException(invocation);
                throw;
            }

            finally
            {
                if (isSuccess)
                    OnSuccess(invocation);
            }

            OnAfter(invocation);
        }
    }
}
