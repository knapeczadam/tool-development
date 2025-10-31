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
using W10_ApexIcons.Models;
using W10_ApexIcons.ViewModels;

namespace W10_ApexIcons.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class IconView : UserControl
    {
        public IconData IconSource
        {
            get { return (IconData)GetValue(IconSourceProperty); }
            set { 
                SetValue(IconSourceProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for IconSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register(nameof(IconSource), typeof(IconData), typeof(IconView), new FrameworkPropertyMetadata(OnIconSourceChanged));

		private static void OnIconSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
		    if(d is IconView view && view.DataContext is IconEditorViewModel vm)
            {
                vm.IconData = e.NewValue as IconData;
            }
		}

		public IconView()
        {
            InitializeComponent();
        }
    }
}
