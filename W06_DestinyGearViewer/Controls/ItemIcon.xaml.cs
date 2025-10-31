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
using W06_DestinyGearViewer.Models;

namespace W06_DestinyGearViewer.Controls
{
    /// <summary>
    /// Interaction logic for ItemIcon.xaml
    /// </summary>
    public partial class ItemIcon : UserControl
    {
        public static readonly DependencyProperty ItemSourceProperty = DependencyProperty.Register(
            nameof(ItemSource), typeof(DestinyItem), typeof(ItemIcon), new PropertyMetadata(default(DestinyItem)));

        public DestinyItem ItemSource
        {
            get { return (DestinyItem) GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }


        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command), typeof(ICommand), typeof(ItemIcon), new PropertyMetadata(default(ICommand)));

        public ICommand Command
        {
            get { return (ICommand) GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }   

        public ItemIcon()
        {
            InitializeComponent();

            //if (Helpers.IsDesignMode)
            //{
            //    ItemSource = DesignTimeData.MockItem;
            //}
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Command?.Execute(ItemSource);
        }
    }
}
