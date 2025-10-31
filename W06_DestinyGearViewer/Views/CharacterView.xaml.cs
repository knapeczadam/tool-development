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
using W06_DestinyGearViewer.ViewModels;

namespace W06_DestinyGearViewer.Views
{
    /// <summary>
    /// Interaction logic for CharacterView.xaml
    /// </summary>
    public partial class CharacterView : UserControl
    {
        public static readonly DependencyProperty CharacterSourceProperty = DependencyProperty.Register(
            nameof(CharacterSource), typeof(DestinyCharacter), typeof(CharacterView), new FrameworkPropertyMetadata(OnItemSourceChanged));

        public DestinyCharacter CharacterSource
        {
            get { return (DestinyCharacter) GetValue(CharacterSourceProperty); }
            set { SetValue(CharacterSourceProperty, value); }
        }

        private static void OnItemSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CharacterView view)
            {
                if (view.RootGrid.DataContext is CharacterViewModel viewModel)
                {
                    viewModel.CharacterData = e.NewValue as DestinyCharacter;
                }
            }
        }

        public CharacterView()
        {
            InitializeComponent();

            //if (Helpers.IsDesignMode)
            //{
            //    CharacterSource = Helpers.DataProvider.GetMockData().Characters[0];
            //}
        }
    }
}
