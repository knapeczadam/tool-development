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

namespace W10_ColorSwatches.View
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class SwatchControl : UserControl
    {
        public SwatchControl()
        {
            InitializeComponent();
        }

		private void Slider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
		{
            if(sender is Slider s)
            {
                s.GetBindingExpression(Slider.ValueProperty)?.UpdateSource();
            }
        }
    }
}
