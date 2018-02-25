﻿using heitech.ObjectExpander.Configuration;
using heitech.ObjectExpander.ExtensionMap;
using System;
using System.Threading.Tasks;
using static heitech.ObjectExpander.Extender.ObjectExtender;

namespace heitech.ObjectExpander.Extender
{
    public static class ObjectExtensionCaller
    {
        public static void Call<TKey>(this object obj, TKey key)
        {
            var args = new object[] { };
            InvokeOnMap(() => AttributeMap().Invoke(key, args), obj, key, null, args);
        }

        public static void Call<TKey, TParam>(this object obj, TKey key, TParam param)
        {
            var args = new object[] { param};
            InvokeOnMap(() => AttributeMap().Invoke(key, args), obj, key, null, args);
        }

        public static void Call<TKey, TParam, TParam2>(this object obj, TKey key, TParam param, TParam2 param2)
        {
            var args = new object[] { param, param2 };
            InvokeOnMap(() => AttributeMap().Invoke(key, args), obj, key, null, args);
        }

        static void Throw(object obj, object key) => throw new AttributeNotFoundException(obj.GetType(), key);

        public static Task CallAsync<TKey>(this object obj, TKey key) 
            => throw new NotImplementedException();
        public static Task CallAsync<TKey, TParam>(this object obj, TKey key, TParam param)
            => throw new NotImplementedException();
        public static Task CallAsync<TKey, TParam, TParam2>(this object obj, TKey key, TParam param, TParam2 param2)
            => throw new NotImplementedException();


        public static TResult Invoke<TKey, TResult>(this object obj, TKey key)
        {
            TResult result = default(TResult);
            InvokeOnMap(() => result = (TResult)AttributeMap().Invoke(key),
                obj, key, typeof(TResult));

            return result;
        }

        public static TResult Invoke<TKey, TResult, TParam>(this object obj, TKey key, TParam param)
        {
            TResult result = default(TResult);
                InvokeOnMap(() => result = (TResult)AttributeMap().Invoke(key, param),
                obj, key, typeof(TResult), param);
            return result;
        }

        static void InvokeOnMap(Action _do, object obj, object key, Type returnType, params object[] parameters)
        {
            if (AttributeMap().CanInvoke(key, returnType, parameters))
                _do();
            else
                if (!ObjectExtenderConfig.IgnoreException)
                    Throw(obj, key);
        }

        public static TResult Invoke<TKey, TResult, TParam, TParam2>(this object obj, TKey key, TParam param, TParam2 param2)
        {
            TResult result = default(TResult);
            InvokeOnMap(() => result = (TResult)AttributeMap().Invoke(key, param, param2),
                 obj, key, typeof(TResult), param, param2);
            return result;
        }

        public static Task<TResult> InvokeAsync<TKey, TResult>(this object obj, TKey key) 
            => throw new NotImplementedException();
        public static Task<TResult> InvokeAsync<TKey, TResult, TParam>(this object obj, TKey key, TParam param)
            => throw new NotImplementedException();
        public static Task<TResult> InvokeAsync<TKey, TResult, TParam, TParam2>(this object obj, TKey key, TParam param, TParam2 param2)
            => throw new NotImplementedException();
    }
}