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
            DependencyProperty.Register(
                "QIsValid", 
                typeof(bool), 
                typeof(QControlBase), 
                new FrameworkPropertyMetadata(
                    true,
                    (FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Inherits),
                    new PropertyChangedCallback(OnQIsValidChanged)
                    ));

        public static void OnQIsValidChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var qc = (QControlBase)d;
            if ((bool)args.NewValue == false)
            {
                qc.QOutput = null;
            }

            else
            {
                qc.QOutput = qc.QState;
            }
        }

        public string QDisplay
        {
            get { 
                return (string)GetValue(QDisplayProperty); }
            set { SetValue(QDisplayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for QDisplay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QDisplayProperty =
            DependencyProperty.Register(
                "QDisplay", 
                typeof(string), 
                typeof(QControlBase), 
                new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true, Inherits=true });



        public string QState
        {
            get { return (string)GetValue(QStateProperty); }
            set { 
                SetValue(QStateProperty, value);
                
            }
        }
       
        // Using a DependencyProperty as the backing store for QState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QStateProperty =
            DependencyProperty.Register(
                "QState", 
                typeof(string), 
                typeof(QControlBase), 
                new FrameworkPropertyMetadata(
                    null,
                    (FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Inherits),
                    new PropertyChangedCallback(OnQStateChanged)
                    //new CoerceValueCallback(CoerceQState)
                ));

        //public static object CoerceQState(DependencyObject d, object value)
        //{
        //    QControlBase qb = (QControlBase)d;
        //    string state = (string)value;
        //    if (qb.QState != qb.QOutput && qb.QOutput != null)
        //    {
        //        return qb.QOutput;
        //    }
        //    else
        //    {
        //        return state;
        //    }
        //}

        public static void OnQStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var qc = (QControlBase)d;
            if (qc.QState != qc.QOutput && qc.QIsValid)
            {
                qc.QOutput = (string)args.NewValue;
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
                return (string)GetValue(QOutputProperty);
            }
            set
            {
                SetValue(QOutputProperty, value);
            }
        }

        //Using a DependencyProperty as the backing store for QOutput.This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QOutputProperty =
            DependencyProperty.Register(
                "QOutput", typeof(string), 
                typeof(QControlBase), 
                new FrameworkPropertyMetadata(
                    null,
                    (FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Inherits),
                    new PropertyChangedCallback(OnQOutputChanged)
                    //new CoerceValueCallback(CoerceQOutput)
                    )
                );
        //public static object CoerceQOutput(DependencyObject d, object value)
        //{
        //    //QControlBase qb = (QControlBase)d;
        //    //string state = (string)value;
        //    //if (qb.QIsValid)
        //    //{
        //    //    return qb.QState;
        //    //}

        //    //else
        //    //{
        //    //    return null;
        //    //}
        //    return value;
        //}

        public static void OnQOutputChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var qc = (QControlBase)d;
            if (args.NewValue != null && qc.QOutput != qc.QState)
            {
                //qc.CoerceValue(QStateProperty);
                qc.QState = (string)args.NewValue;
            }
        }
    }
}
