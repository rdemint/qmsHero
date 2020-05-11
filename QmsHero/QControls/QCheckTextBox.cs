using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QControls
{
    public class QCheckTextBox : QControlBase, INotifyDataErrorInfo
    {
        static QCheckTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(QCheckTextBox), new FrameworkPropertyMetadata(typeof(QCheckTextBox)));
        }

    }
}
