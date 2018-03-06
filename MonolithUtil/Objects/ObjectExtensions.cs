using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MonolithUtil.Objects
{
    public static class ObjectExtension
    {
        public static IEnumerable<object> ToYield(this object entity)
        {
            yield return entity;
        }
    }
}
