using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QmsHero.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:QmsHero.Controls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:QmsHero.Controls;assembly=QmsHero.Controls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:QControlBase/>
    ///
    /// </summary>
    public class QControlBase : Control
    {
        static QControlBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(QControlBase), new FrameworkPropertyMetadata(typeof(QControlBase)));
        }

        public bool QIsValid
        {
            get { return (bool)GetValue(QIsValidProperty); }
            set { SetValue(QIsValidProperty, value); }
        }

        // Using a DependencyProperty as the backing store for QIsValid.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QIsValidProperty =
            DependencyProperty.Register("QIsValid", typeof(bool), typeof(QControlBase), new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true });



        public string QDisplay
        {
            get { return (string)GetValue(QDisplayProperty); }
            set { SetValue(QDisplayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for QDisplay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QDisplayProperty =
            DependencyProperty.Register("QDisplay", typeof(string), typeof(QControlBase), new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true });



        public string QState
        {
            get { return (string)GetValue(QStateProperty); }
            set { 
                SetValue(QStateProperty, value);
                
            }
        }

        // Using a DependencyProperty as the backing store for QState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QStateProperty =
            DependencyProperty.Register("QState", typeof(string), typeof(QControlBase), 
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(OnQStateChanged), new CoerceValueCallback())

        public void OnQStateChanged(string value)
        {
            this.QOutput = value;
            if (this.QState != value)
            {
                this.QState = value;
            }
        }


        public string QOutput
        {
            get
            {
                //if (QIsValid == true)
                //{
                //    return (string)GetValue(QOutputProperty);
                //}
                //else
                //{
                //    return null;
                //}
                return (string)GetValue(QStateProperty);
            }
            set
            {
                //SetValue(QOutputProperty, value);
                SetValue(QStateProperty, value);
            }
        }

        //Using a DependencyProperty as the backing store for QOutput.This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QOutputProperty =
            DependencyProperty.Register("QOutput", typeof(string), typeof(QControlBase), new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true }, );
    }
}
