using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithUtil.Collections
{
    public static class ConcurrentBagExtentions
    {
        /// <summary>
        /// 指定された各要素をConcurrentBagに追加します
        /// </summary>
        /// <typeparam name="T">コレクション要素の型</typeparam>
        /// <param name="collection">自コレクション</param>
        /// <param name="addItems">コレクションに追加する処理</param>
        public static void AddRange<T>(this ConcurrentBag<T> collection, IEnumerable<T> addItems)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (addItems == null) throw new ArgumentNullException(nameof(addItems));

            foreach (var item in addItems)
                collection.Add(item);
        }
    }
}
