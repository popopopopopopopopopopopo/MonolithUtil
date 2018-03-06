using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MonolithUtil.Images
{
    /// <summary>
    /// ビットマップイメージ関連の処理の簡素化メソッド群を提供します。
    /// </summary>
    public class BitMapImageHelper
    {
        /// <summary>
        /// uri文字列からBitmapImageを取得します。
        /// </summary>
        /// <param name="uriSource">ソース文字列</param>
        /// <param name="uriSourceKind">Uri種別（デフォルトはAbsoluteです）</param>
        /// <returns>取得したBitmapImage</returns>
        public static BitmapImage GetBitmapImageByUri(string uriSource, UriKind uriSourceKind= UriKind.Absolute)
        {
            return new BitmapImage(new Uri(uriSource, uriSourceKind));
        }

        /// <summary>
        /// uri文字列からBitmapImageを取得します。
        /// 絶対パッケージURI（pack://application:,,,/）を自動的に補完します
        /// </summary>
        /// <param name="uriSource">ソースとなる絶対パッケージURI（pack://application:,,,/）をのぞく、参照アセンブリ名からでよい</param>
        /// <param name="uriSourceKind">Uri種別（デフォルトはAbsoluteです）</param>
        /// <returns>取得したBitmapImage</returns>
        public static BitmapImage GetBitmapImageReferencedAssemblyByUri(string uriSource, UriKind uriSourceKind = UriKind.Absolute)
        {
            return new BitmapImage(new Uri($"pack://application:,,,/{uriSource}", uriSourceKind));
        }

        /// <summary>
        /// uri文字列からBitmapImageを取得します。
        /// 絶対パッケージURI（pack://application:,,,/）を自動的に補完します
        /// </summary>
        /// <param name="assemblyName">アセンブリ名。ソースとなる絶対パッケージURI（pack://application:,,,/）をのぞく、参照アセンブリ名からでよい。また、component記述も無くて良い</param>
        /// <param name="directoryPath">ディレクトリ名。入れ子の場合も対応可能　開始・終了のスラッシュ不要</param>
        /// <param name="fileName">ファイル名</param>
        /// <param name="uriSourceKind">Uri種別（デフォルトはAbsoluteです）</param>
        /// <returns>取得したBitmapImage</returns>
        public static BitmapImage GetBitmapImageReferencedAssemblyByUri(string assemblyName, string directoryPath, string fileName, UriKind uriSourceKind = UriKind.Absolute)
        {
            return new BitmapImage(new Uri($"pack://application:,,,/{assemblyName};component/{directoryPath}/{fileName}", uriSourceKind));
        }
    }
}
