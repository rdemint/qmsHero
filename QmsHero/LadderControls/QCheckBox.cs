using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LadderControls
{
    public class QCheckBox: QControlBase
    {

        static QCheckBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(QCheckBox), new FrameworkPropertyMetadata(typeof(QCheckBox)));
        }
    }
}
