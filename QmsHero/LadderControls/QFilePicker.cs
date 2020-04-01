using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LadderControls
{
    class QFilePicker: QControlBase
    {
        static QFilePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(QFilePicker), new FrameworkPropertyMetadata(typeof(QFilePicker)));
        }
    }
}
