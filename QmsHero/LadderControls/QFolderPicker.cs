using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LadderControls
{
    class QFolderPicker: QControlBase
    {
        static QFolderPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(QFolderPicker), new FrameworkPropertyMetadata(typeof(QFolderPicker)));
        }
    }
}
