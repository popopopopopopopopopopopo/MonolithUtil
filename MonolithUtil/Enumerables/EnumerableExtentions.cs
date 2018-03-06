using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithUtil.Enumerables
{
    /// <summary>
    /// Enumerable型に、シンプルな拡張メソッドを実装するためのクラスです。
    /// </summary>
    /// <remarks>意外と存在していないForeachとかを実装しています。</remarks>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// コレクションの各要素に対して指定されたアクションを実行します。
        /// </summary>
        /// <typeparam name="T">コレクション要素の型</typeparam>
        /// <param name="array">自コレクション</param>
        /// <param name="action">各要素に対し実行する処理</param>
        public static void ForEach<T>(this IEnumerable<T> array, Action<T> action)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (action == null) throw new ArgumentNullException(nameof(action));

            foreach (var item in array)
                action(item);
        }

        #region IsEmpty
        /// <summary>
        /// 指定されたコレクションが空かどうかを確認します。
        /// </summary>
        /// <typeparam name="T">コレクション要素の型</typeparam>
        /// <param name="collection">自コレクション</param>
        /// <returns>空の場合trueが返ります</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> collection) => !collection.Any();
        /// <summary>
        /// 指定されたコレクションが空かどうかを確認します。
        /// </summary>
        /// <typeparam name="T">コレクション要素の型</typeparam>
        /// <param name="collection">自コレクション</param>
        /// <returns>空の場合trueが返ります</returns>
        public static bool IsNullOrNotAny<T>(this IEnumerable<T> collection) => collection==null || !collection.Any();

        public static bool IsNotNullAndAny<T>(this IEnumerable<T> collection) => collection != null && collection.Any();

        #endregion
    }
}