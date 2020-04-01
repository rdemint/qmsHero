using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QmsDoc.Controls
{
    public class QBase: Control
    {

        public string QIsValid
        {
            get { return (string)GetValue(QIsValidProperty); }
            set { SetValue(QIsValidProperty, value); }
        }

        // Using a DependencyProperty as the backing store for QIsValid.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QIsValidProperty =
            DependencyProperty.Register("QIsValid", typeof(string), typeof(QBase), new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true });



        public string QDisplay
        {
            get { return (string)GetValue(QDisplayProperty); }
            set { SetValue(QDisplayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for QDisplay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QDisplayProperty =
            DependencyProperty.Register("QDisplay", typeof(string), typeof(QBase), new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true });



        public string QState
        {
            get { return (string)GetValue(QStateProperty); }
            set { SetValue(QStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for QState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QStateProperty =
            DependencyProperty.Register("QState", typeof(string), typeof(QBase), new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true });



        public string QOutput
        {
            get { return (string)GetValue(QOutputProperty); }
            set { SetValue(QOutputProperty, value); }
        }

        // Using a DependencyProperty as the backing store for QOutput.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QOutputProperty =
            DependencyProperty.Register("QOutput", typeof(string), typeof(QBase), new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true });


    }
}
