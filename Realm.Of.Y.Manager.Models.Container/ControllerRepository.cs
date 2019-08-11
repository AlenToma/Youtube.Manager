using FastDeepCloner;
using Rest.API.Translator;
using System;
using System.Linq.Expressions;
using Realm.Of.Y.Manager.Models.Container.Interface.API;

namespace Realm.Of.Y.Manager.Models.Container
{
    public static class ControllerRepository
    {
        private static string _baseUrl = "http://youtubemanager.ddns.net/Youtube.Manager.API";
        public static P Y<P>(Expression<Func<IYController, P>> expression, Action<MethodInformation, P> afterOperation = null)
        {
            using (var y = new APIController<IYController>(_baseUrl))
            {
                var data = y.Execute(expression);
                afterOperation?.Invoke(GetInfo(expression), data);
                return data;
            }
        }

        public static P Db<P>(Expression<Func<IDbController, P>> expression, Action<MethodInformation, P> afterOperation = null)
        {
            using (var y = new APIController<IDbController>(_baseUrl))
            {
                var data = y.Execute(expression);
                afterOperation?.Invoke(GetInfo(expression), data);
                return data;
            }
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
            using (var m = new APIController<T>(_baseUrl))
                return KeyValuePairs.GetOrAdd(expression, m.GetInfo(expression, true));
        }
    }
}
