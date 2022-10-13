using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SiPintar.Views
{
    public static class UserControlBase
    {
        public static UserControl Obj;

        public static void SetButton(UserControl _obj)
        {
            Obj = _obj;
            if (Obj != null)
            {
                Obj.DataContextChanged += UserControlBase_DataContextChanged;
            }
        }

        private static void UserControlBase_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Obj.DataContext != null)
            {
                var Vis = Obj.Content as Visual;
                if (Vis != null)
                {
                    BindingButtonByTag(Vis);
                }
            }
        }

        private static void BindingButtonByTag(Visual myVisual)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(myVisual); i++)
            {
                var childVisual = (Visual)VisualTreeHelper.GetChild(myVisual, i);
                if (childVisual is Button)
                {
                    var btn = childVisual as Button;
                    if (btn.Tag != null && !string.IsNullOrWhiteSpace(btn.Tag.ToString()))
                    {
                        btn.Click += Btn_Click;
                    }
                }
                BindingButtonByTag(childVisual);
            }
        }

        private static void Btn_Click(object sender, RoutedEventArgs e)
        {
            var Btn = sender as Button;
            if (Btn != null)
            {
                string BtnName = Btn.Tag.ToString();
                object Vm = Obj.DataContext;
                if (Vm != null)
                {
                    var BtnCommand = Vm.GetType().GetProperty(BtnName).GetValue(Vm) as ICommand;
                    if (BtnCommand != null)
                    {
                        BtnCommand.Execute(null);
                    }
                }
            }
        }
    }
}
