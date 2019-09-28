using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TipsCustomControlLib
{
    public static class SharedResources
    {
        static ComponentResourceKey _brushKey = new ComponentResourceKey(typeof(MyCustomControl), "CommonBrushes");

        public static ComponentResourceKey CommonBrushKey
        {
            get { return _brushKey; }
        }
    }
}
