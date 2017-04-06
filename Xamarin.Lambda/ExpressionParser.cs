using System;
using System.Linq;
using System.Linq.Expressions;

namespace Xamarin.Lambda
{
    /// <summary>
    /// Lambda expression parser
    /// </summary>
    static public class ExpressionParser
    {
        #region all Parse()

        /// <summary>
        /// parse the Lambda expression code
        /// </summary>
        /// <param name = "lambdaCode"> lambda expression code. Such as: m => m.ToString () </param>
        /// <param name = "namespaces"> namespace set </param>
        static public LambdaExpression Parse(string lambdaCode, params string[] namespaces)
        {
            return ParseCore<Delegate>(null, lambdaCode, null, false, null, namespaces);
        }

        /// <summary>
        /// parse the Lambda expression code
        /// </summary>
        /// <param name = "lambdaCode"> lambda expression code. Such as: m => m.ToString () </param>
        /// <param name="defaultInstance"></param>
        /// <param name = "namespaces"> namespace set </param>
        static public LambdaExpression Parse(string lambdaCode, Type defaultInstance, params string[] namespaces)
        {
            return ParseCore<Delegate>(null, lambdaCode, defaultInstance, false, null, namespaces);
        }

        /// <summary>
        /// parse the Lambda expression code
        /// </summary>
        /// <param name = "lambdaCode"> lambda expression code. Such as: m => m.ToString () </param>
        /// <param name="paramTypes"></param>
        /// <param name = "namespaces"> namespace set </param>
        /// <param name="defaultInstance"></param>
        static public LambdaExpression Parse(string lambdaCode, Type defaultInstance, Type[] paramTypes, params string[] namespaces)
        {
            return ParseCore<Delegate>(null, lambdaCode, defaultInstance, false, paramTypes, namespaces);
        }

        /// <summary>
        /// parse the Lambda expression code
        /// </summary>
        /// <param name = "lambdaCode"> lambda expression code. Such as: m => m.ToString () </param>
        /// <param name = "delegateType"> delegate type </param>
        /// <param name = "namespaces"> namespace set </param>
        static public LambdaExpression Parse(Type delegateType, string lambdaCode, params string[] namespaces)
        {
            return ParseCore<Delegate>(delegateType, lambdaCode, null, false, null, namespaces);
        }

        /// <summary>
        /// parse the Lambda expression code
        /// </summary>
        /// <param name = "lambdaCode"> lambda expression code. Such as: m => m.ToString () </param>
        /// <param name = "delegateType"> delegate type </param>
        /// <param name = "firstTypeIsDefaultInstance"> whether the first type is the default instance </param>
        /// <param name = "namespaces"> namespace set </param>
        static public LambdaExpression Parse(Type delegateType, string lambdaCode, bool firstTypeIsDefaultInstance, params string[] namespaces)
        {
            return ParseCore<Delegate>(delegateType, lambdaCode, null, firstTypeIsDefaultInstance, null, namespaces);
        }

        /// <summary>
        /// parse the Lambda expression code
        /// </summary>
        /// <param name = "lambdaCode"> lambda expression code. Such as: m => m.ToString () </param>
        /// <param name = "namespaces"> namespace set </param>
        static public Expression<TDelegate> Parse<TDelegate>(string lambdaCode, params string[] namespaces)
        {
            return (Expression<TDelegate>)ParseCore<TDelegate>(null, lambdaCode, null, false, null, namespaces);
        }

        /// <summary>
        /// parse the Lambda expression code
        /// </summary>
        /// <param name = "lambdaCode"> lambda expression code. Such as: m => m.ToString () </param>
        /// <param name = "firstTypeIsDefaultInstance"> whether the first type is the default instance </param>
        /// <param name = "namespaces"> namespace set </param>
        static public Expression<TDelegate> Parse<TDelegate>(string lambdaCode, bool firstTypeIsDefaultInstance, params string[] namespaces)
        {
            return (Expression<TDelegate>)ParseCore<TDelegate>(null, lambdaCode, null, firstTypeIsDefaultInstance, null, namespaces);
        }

        #endregion

        #region all Compile()

        /// <summary>
        /// parse the Lambda expression code
        /// </summary>
        /// <param name = "lambdaCode"> lambda expression code. Such as: m => m.ToString () </param>
        /// <param name = "namespaces"> namespace set </param>
        static public Delegate Compile(string lambdaCode, params string[] namespaces)
        {
            return Parse(lambdaCode, namespaces).Compile();
        }

        /// <summary>
        /// parse the Lambda expression code
        /// </summary>
        /// <param name = "lambdaCode"> lambda expression code. Such as: m => m.ToString () </param>
        /// <param name="defaultInstance">type Of the default instance</param>
        /// <param name = "namespaces"> namespace set </param>
        static public Delegate Compile(string lambdaCode, Type defaultInstance, params string[] namespaces)
        {
            return Parse(lambdaCode, defaultInstance, namespaces).Compile();
        }

        /// <summary>
        /// parse the Lambda expression code
        /// </summary>
        /// <param name="delegateType">delegate type</param>
        /// <param name = "lambdaCode"> lambda expression code. Such as: m => m.ToString () </param>
        /// <param name = "namespaces"> namespace set </param>
        static public Delegate Compile(Type delegateType, string lambdaCode, params string[] namespaces)
        {
            return Parse(delegateType, lambdaCode, namespaces).Compile();
        }

        /// <summary>
        /// parse the Lambda expression code
        /// </summary>
        /// <param name="delegateType">delegate type</param>
        /// <param name = "lambdaCode"> lambda expression code. Such as: m => m.ToString () </param>
        /// <param name = "firstTypeIsDefaultInstance"> whether the first type is the default instance </param>
        /// <param name = "namespaces"> namespace set </param>
        static public Delegate Compile(Type delegateType, string lambdaCode, bool firstTypeIsDefaultInstance, params string[] namespaces)
        {
            return Parse(delegateType, lambdaCode, firstTypeIsDefaultInstance, namespaces).Compile();
        }

        /// <summary>
        /// parse the Lambda expression code
        /// </summary>
        /// <param name = "lambdaCode"> lambda expression code. Such as: m => m.ToString () </param>
        /// <param name = "namespaces"> namespace set </param>
        static public TDelegate Compile<TDelegate>(string lambdaCode, params string[] namespaces)
        {
            return Parse<TDelegate>(lambdaCode, namespaces).Compile();
        }

        /// <summary>
        /// parse the Lambda expression code
        /// </summary>
        /// <param name = "lambdaCode"> lambda expression code. Such as: m => m.ToString () </param>
        /// <param name = "firstTypeIsDefaultInstance"> whether the first type is the default instance </param>
        /// <param name = "namespaces"> namespace set </param>
        static public TDelegate Compile<TDelegate>(string lambdaCode, bool firstTypeIsDefaultInstance, params string[] namespaces)
        {
            return Parse<TDelegate>(lambdaCode, firstTypeIsDefaultInstance, namespaces).Compile();
        }

        #endregion

        #region all Exec()

        /// <summary>
        /// Execute the code with instance as the context $ ($ 0 for instance, (can be omitted $ 0); $ 1 that objects of the first object; $ 2 that objects of the second object ....)
        /// </summary>
        /// <typeparam name = "T"> returned result type </typeparam>
        /// <param name = "instance"> execute the code with this object as the context ($ 0 in code, $ 0 can be omitted) </param>
        /// <param name = "code"> executed code </param>
        /// <param name = "namespaces"> import namespace </param>
        /// <param name = "objects"> parameter object </param>
        /// <returns> </returns>
        static public T Exec<T>(object instance, string code, string[] namespaces, params object[] objects)
        {
            object[] allObjs = new object[objects.Length + 1];
            allObjs[0] = instance;
            Array.Copy(objects, 0, allObjs, 1, objects.Length);

            object[] inputObjs = new object[objects.Length + 2];
            inputObjs[1] = inputObjs[0] = instance;
            Array.Copy(objects, 0, inputObjs, 2, objects.Length);

            string lambdaParams = string.Join(",", allObjs.Select((m, i) => "$" + i).ToArray());
            Type[] paramTypes = inputObjs.Select(m => m.GetType()).ToArray();

            string newCode = string.Format("({0})=>{1}", lambdaParams, code);

            return (T)Parse(newCode, instance.GetType(), paramTypes, namespaces).Compile().DynamicInvoke(inputObjs);
        }

        /// <summary>
        /// Execute the code with instance as the context $ ($ 0 for instance, (can be omitted $ 0); $ 1 that objects of the first object; $ 2 that objects of the second object ....)
        /// </summary>
        /// <param name = "instance"> execute the code with this object as the context ($ 0 in code, $ 0 can be omitted) </ param>
        /// <param name = "code"> executed code </param>
        /// <param name = "namespaces"> import namespace </param>
        /// <param name = "objects"> parameter object </param>
        /// <returns> </returns>
        static public object Exec(object instance, string code, string[] namespaces, params object[] objects)
        {
            return Exec<object>(instance, code, namespaces, objects);
        }

        #endregion

        #region private method.内部方法

        /// <summary>
        /// parse the Lambda expression code
        /// </summary>
        /// <param name="delegateType">Type of delegate</param>
        /// <param name = "lambdaCode"> lambda expression code. Such as: m => m.ToString () </param>
        /// <param name="paramTypes"></param>
        /// <param name = "namespaces"> namespace set </param>
        /// <param name="defaultInstanceType"></param>
        /// <param name="firstTypeIsDefaultInstance"></param>
        static private LambdaExpression ParseCore<TDelegate>(Type delegateType, string lambdaCode, Type defaultInstanceType, bool firstTypeIsDefaultInstance, Type[] paramTypes, params string[] namespaces)
        {
            ExpressionParserCore<TDelegate> parser = new ExpressionParserCore<TDelegate>(delegateType, lambdaCode, defaultInstanceType, paramTypes, firstTypeIsDefaultInstance);
            if (namespaces != null && namespaces.Length > 0)
            {
                parser.Namespaces.AddRange(namespaces);
            }
            return parser.ToLambdaExpression();
        }

        #endregion
    }
}