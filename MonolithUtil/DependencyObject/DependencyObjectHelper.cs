using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MonolithUtil.DependencyObject
{
    public static class DependencyObjectHelper
    {
        /// <summary>
        /// Dependencyオブジェクトの親ウィンドウを取得します。
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public static Window GetWindow(this System.Windows.DependencyObject myObject)
        {
            return Window.GetWindow(myObject);
        }
    }
}
