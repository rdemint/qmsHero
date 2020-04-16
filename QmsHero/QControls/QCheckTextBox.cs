using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LadderControls
{
    public class QCheckTextBox : QControlBase
    {
        static QCheckTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(QCheckTextBox), new FrameworkPropertyMetadata(typeof(QCheckTextBox)));
        }
    }
}
