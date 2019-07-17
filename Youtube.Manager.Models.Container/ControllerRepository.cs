using FastDeepCloner;
using System;
using System.Linq.Expressions;
using Youtube.Manager.Models.Container.Interface.API;

namespace Youtube.Manager.Models.Container
{
    public static class ControllerRepository
    {
        public static P Youtube<P>(Expression<Func<IYoutubeController, P>> expression, Action<MethodInformation, P> afterOperation = null)
        {
            return Actions.Await(async () =>
            {
                var data = await HttpHelper.ExecuteAsync(expression);
                afterOperation?.Invoke(GetInfo(expression), data);
                return data;
            });
        }

        public static P Db<P>(Expression<Func<IDbController, P>> expression, Action<MethodInformation, P> afterOperation = null)
        {
            return Actions.Await(async () =>
            {
                var data = await HttpHelper.ExecuteAsync(expression);
                afterOperation?.Invoke(GetInfo(expression), data);
                return data;
            });
        }


        private static SafeValueType<Expression, MethodInformation> KeyValuePairs = new SafeValueType<Expression, MethodInformation>();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="P"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MethodInformation GetInfo<T, P>(Expression<Func<T, P>> expression)
        {
            if (KeyValuePairs.ContainsKey(expression))
                return KeyValuePairs.Get(expression);
            return KeyValuePairs.GetOrAdd(expression, expression.GetInfo(true));
        }
    }
}
