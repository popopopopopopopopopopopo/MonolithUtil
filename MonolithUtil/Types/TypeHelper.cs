using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using This = MonolithUtil.Types.TypeHelper;

namespace MonolithUtil.Types
{
    /// <summary>
    /// 型情報の補助機能を提供します。
    /// </summary>
    /// <remarks>渡されたオブジェクトが特定の型に対応するものかを判定したりします。</remarks>
    public static class TypeHelper
    {
        /// <summary>
        /// 指定されたコレクションを表す型情報から要素の型情報を取得します。
        /// </summary>
        /// <typeparam name="TCollection">コレクションの型情報</typeparam>
        /// <returns></returns>
        public static Type GetElementType<TCollection>() => This.GetElementType(typeof(TCollection));

        /// <summary>
        /// 指定されたコレクションを表す型情報から要素の型情報を取得します。
        /// </summary>
        /// <param name="collectionType">コレクションの型情報</param>
        /// <returns>要素の型情報</returns>
        /// <remarks>要素の型が取得できない場合はnullを返します。</remarks>
        public static Type GetElementType(System.Type collectionType)
        {
            if (collectionType == null)
                throw new ArgumentNullException(nameof(collectionType));

            var elementType = new[] { collectionType }
                .Where(x => x.IsInterface)
                .Concat(collectionType.GetInterfaces())
                .Where(x => x.IsGenericType)
                .Where(x => x.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                .Select(x => x.GetGenericArguments()[0])
                .FirstOrDefault();

            return elementType;
        }

        /// <summary>
        /// 指定された型がコレクション型かどうかを判定します。
        /// </summary>
        /// <typeparam name="T">型情報</typeparam>
        /// <returns>コレクション型かどうか</returns>
        public static bool IsCollection<T>() => typeof(T).IsCollection();

        /// <summary>
        /// 指定された型がコレクション型かどうかを判定します。
        /// </summary>
        /// <param name="type">型情報</param>
        /// <returns>コレクション型かどうか</returns>
        public static bool IsCollection(this System.Type type) => This.GetElementType(type) != null;

        public static System.Type[] GetGenericTypes<T>(Func<System.Type, bool> predicate)
        {
            var assembly = Assembly.GetAssembly(typeof(T));
            return assembly.GetTypes().Where(predicate).ToArray();
        }

        public static System.Type[] GetTypes(Assembly searchAssembly,Func<System.Type, bool> predicate)
        {
            return searchAssembly.GetTypes().Where(predicate).ToArray();
        }

        public static object GetPropertyValue(this Type type,string propertyName,object target = null)
        {
            return type.InvokeMember(propertyName, BindingFlags.GetProperty, null, null, null);
        }

        /// <summary>
        /// プロパティ名を指定したジェネリクスから取得します
        /// </summary>
        /// <typeparam name="T">列挙対象のジェネリクス</typeparam>
        /// <returns>列挙されたプロパティ名</returns>
        public static string[] GetPropertyNames<T>() where T : class
        {
            var props = typeof(T).GetProperties();
            if (props.Any())
            {
                return props.Select(p => p.Name).ToArray();
            }
            return null;
        }
    }
}
