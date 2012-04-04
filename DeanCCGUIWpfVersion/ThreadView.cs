using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using DeanCCCore.Core;

namespace DeanCCGUIWpfVersion
{
    public sealed class ThreadView : DataGrid
    {
        public ThreadView()
        {
            ItemsSource = Common.EnableThreads;
        }
    }
}
