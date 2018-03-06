using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MonolithUtil.Images
{
    /// <summary>
    /// Imageクラスへの拡張メソッドを提供します
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// Contains the specified image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public static bool Contains(this Image image, Point point)
        {
            var imageRec = new Rect(0, 0, image.ActualWidth, image.ActualHeight);
            return imageRec.Contains(point);
        }
    }
}
