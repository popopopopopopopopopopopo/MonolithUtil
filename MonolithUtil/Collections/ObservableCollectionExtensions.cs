using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithUtil.Collections
{
    public static class ObservableCollectionExtensions
    {
        /// <summary>
        /// 指定された各要素をObservableCollectionに追加します
        /// </summary>
        /// <typeparam name="T">コレクション要素の型</typeparam>
        /// <param name="collection">自コレクション</param>
        /// <param name="addItems">コレクションに追加する処理</param>
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> addItems)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (addItems == null) throw new ArgumentNullException(nameof(addItems));

            foreach (var item in addItems)
                collection.Add(item);
        }
    }
}
