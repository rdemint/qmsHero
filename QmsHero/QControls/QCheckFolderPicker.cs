﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QControls
{
    class QCheckFolderPicker: QFolderPicker
    {
        static QCheckFolderPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(QCheckFolderPicker), new FrameworkPropertyMetadata(typeof(QCheckFolderPicker)));
        }
    }
}
