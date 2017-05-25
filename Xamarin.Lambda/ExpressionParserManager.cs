using System;
using System.Collections.Generic;

namespace Xamarin.Lambda
{
    public static class ExpressionParserManager
    {
        private static Dictionary<CacheKey, Delegate> _dicCache = new Dictionary<CacheKey, Delegate>();

        /// <summary>
        /// Key for caching
        /// </summary> 
        private class CacheKey
        {
            public Type TDelegateType { get; private set; }
            public string Code { get; set; }
            public CacheKey(Type type, string code)
            {
                this.TDelegateType = type;
                this.Code = code;
            }
            public override int GetHashCode()
            {
                return this.Code.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }

                CacheKey cacheKey2 = (CacheKey)obj;

                if (this.TDelegateType != cacheKey2.TDelegateType)
                {
                    return false;
                }

                if (this.Code != cacheKey2.Code)
                {
                    return false;
                }

                return true;
            }
        }
    }
}