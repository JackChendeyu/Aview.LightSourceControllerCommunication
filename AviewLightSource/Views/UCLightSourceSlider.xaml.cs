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

namespace AviewLightSource.Views
{
    /// <summary>
    /// UCLightSourceSlider.xaml 的交互逻辑
    /// </summary>
    public partial class UCLightSourceSlider : UserControl
    {
        public int Index { get; set; }
        //public object Source { get; set; }
        public UCLightSourceSlider()
        {
            InitializeComponent();


        }

        private void UserControl_Loaded(object sender, EventArgs e)
        {

            this.Resources.Add($"name{Index}", $"CH{Index}");
            Binding binding = new Binding(".");
            binding.Source = this.Resources[$"name{Index}"];
            BindingOperations.SetBinding(textBlockName, TextBlock.TextProperty, binding);
            Binding bindingValue = new Binding($"Channel{Index}Intensity");
            bindingValue.Mode = BindingMode.TwoWay;
            BindingOperations.SetBinding(slider1, Slider.ValueProperty, bindingValue);
            BindingOperations.SetBinding(textBoxValue, TextBox.TextProperty, bindingValue);
            Binding bindingEnable = new Binding($"Channel{Index}Enable");
            bindingEnable.Mode = BindingMode.TwoWay;
            BindingOperations.SetBinding(slider1, Slider.IsEnabledProperty, bindingEnable);
            BindingOperations.SetBinding(textBoxValue, TextBox.IsEnabledProperty, bindingEnable);
            BindingOperations.SetBinding(checkBoxOnOff, CheckBox.IsEnabledProperty, bindingEnable);
            Binding bindingOnOff = new Binding($"Channel{Index}OnOff");
            bindingOnOff.Mode = BindingMode.TwoWay;
            BindingOperations.SetBinding(checkBoxOnOff, CheckBox.IsCheckedProperty, bindingOnOff);



        }
    }
}
