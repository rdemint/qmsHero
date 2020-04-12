using LadderControls.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace LadderControls
{
    public class QFolderPicker: QControlBase
    {
        ShowFolderDialogCommand showDialogCommand;
        static QFolderPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(QFolderPicker), new FrameworkPropertyMetadata(typeof(QFolderPicker)));
        }

        //// Using a DependencyProperty as the backing store for command.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ShowDialogCommandProperty =
        //    DependencyProperty.Register(
        //        "ShowDialogCommand", 
        //        typeof(ShowFolderDialogCommand), 
        //        typeof(QFolderPicker), 
        //        new PropertyMetadata(0));

        //public ShowFolderDialogCommand ShowDialogCommand
        //{
        //    get { return (ShowFolderDialogCommand)GetValue(ShowDialogCommandProperty); }
        //    set { SetValue(ShowDialogCommandProperty, value); }
        //}

        public ShowFolderDialogCommand ShowDialogCommand
        {
            get {
                if (this.showDialogCommand == null) 
                {
                    this.showDialogCommand = new ShowFolderDialogCommand();
                }
                return this.showDialogCommand; }
            set { this.showDialogCommand = value; }
        }
    }


}
