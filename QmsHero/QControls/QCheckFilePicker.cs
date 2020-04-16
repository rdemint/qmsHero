using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LadderControls
{
    public class QCheckFilePicker: QFilePicker
    {
        static QCheckFilePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(QCheckFilePicker), new FrameworkPropertyMetadata(typeof(QCheckFilePicker)));
        }
    }
}
