using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace W05_DataBinding.PropertyToElement;


    /// <summary>
    /// Interaction logic for ObservableCollectionBinding.xaml
    /// </summary>
    public partial class ObservableCollectionBinding : UserControl
    {
        public static readonly DependencyProperty SizeListProperty = 
            DependencyProperty.Register("SizeList", typeof(ObservableCollection<double>), typeof(ObservableCollectionBinding), new PropertyMetadata(default(ObservableCollection<double>)));

        public ObservableCollection<double> SizeList
        {
            get => (ObservableCollection<double>)GetValue(SizeListProperty);
            set => SetValue(SizeListProperty, value);
        }
        public ObservableCollectionBinding()
        {
            InitializeComponent();

            SizeList = new ObservableCollection<double>();
            SizeList.Add(10);
            SizeList.Add(20);
            SizeList.Add(30);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!SizeList.Contains(40))
            {
                SizeList.Add(40);
            }
            comboBox.SelectedIndex = comboBox.Items.Count - 1;
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (SizeList.Contains(40))
            {
                SizeList.Remove(40);
            }
            comboBox.SelectedIndex = comboBox.Items.Count - 1;

        }
    }
