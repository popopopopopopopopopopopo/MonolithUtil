using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using This = MonolithUtil.Mappers.TableMappingInfo;
using System.Data;

namespace MonolithUtil.Mappers
{
    /// <summary>
    /// モデルクラスをテーブルと見立てて、
    /// 各フィールドとのマッピング情報の格納機能を提供します。
    /// </summary>
    public sealed class TableMappingInfo
    {
        #region フィールド
        /// <summary>
        /// 型とのマッピング情報のキャッシュを保持します。
        /// </summary>
        private static readonly ConcurrentDictionary<System.Type, This> Cache = new ConcurrentDictionary<System.Type, This>();

        /// <summary>
        /// 既定の型マッピング情報を保持します。
        /// </summary>
        private readonly static IReadOnlyDictionary<System.Type, DbType> TypeMap = Mappers.TypeMap.Default();

        #endregion

        #region プロパティ

        /// <summary>
        /// マッピングする型を取得し格納します。
        /// </summary>
        public Type Type { get; private set; }


        /// <summary>
        /// スキーマ名を取得し格納します。
        /// </summary>
        public string Schema { get; private set; }


        /// <summary>
        /// テーブル名を取得し格納します。
        /// </summary>
        public string Name { get; private set; }


        /// <summary>
        /// 列マッピング情報のコレクションを取得し格納します。
        /// </summary>
        public IReadOnlyList<FieldMappingInfo> Columns { get; private set; }


        /// <summary>
        /// スキーマ名とテーブル名を結合したフルネームを取得し格納します。
        /// </summary>
        public string FullName => string.IsNullOrWhiteSpace(this.Schema)
                                ? this.Name
                                : $"{this.Schema}.{this.Name}";
        #endregion

        #region コンストラクタ
        /// <summary>
        /// インスタンスを生成します。privateで宣言しstaticでしか使わせない隠蔽目的
        /// </summary>
        private TableMappingInfo()
        { }

        #endregion

        #region 生成
        /// <summary>
        /// 指定された型に対するマッピング情報を生成します。
        /// </summary>
        /// <typeparam name="T">対象となる型情報</typeparam>
        /// <returns>テーブルマッピング情報</returns>
        public static This Create<T>() => This.Create(typeof(T));


        /// <summary>
        /// 指定された型に対するマッピング情報を生成します。
        /// </summary>
        /// <param name="type">対象となる型情報</param>
        /// <returns>テーブルマッピング情報</returns>
        public static This Create(System.Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            This result = null;
            lock (This.Cache)
            {
                //--- キャッシュから取得
                if (!This.Cache.TryGetValue(type, out result))
                {
                    //--- テーブル情報
                    var table = type.GetCustomAttribute<TableAttribute>(false);
                    result = new This()
                    {
                        Schema = table?.Schema ?? null,
                        Name = table?.Name ?? type.Name,
                        Type = type
                    };

                    //--- 列情報（モデル内に存在するコレクションは除外する
                    var flags = BindingFlags.Instance | BindingFlags.Public;
                    var notMapped = typeof(NotMappedAttribute);

                    //typeMapに存在するカラムだけを対象にする
                    var targetProperties = type.GetProperties(flags)
                        .Where(x => TypeMap.ContainsKey(x.PropertyType));

                    result.Columns = targetProperties
                                    .Where(x => x.CustomAttributes.All(y => y.AttributeType != notMapped))
                                    .Select(FieldMappingInfo.From)
                                    .ToArray();

                    //--- キャッシュ
                    This.Cache.TryAdd(type, result);
                }
            }
            return result;
        }

        #endregion
    }
}
