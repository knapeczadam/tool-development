using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace W10_ColorSwatches.ViewModel
{
    partial class SwatchViewModel: ObservableObject
    {
		[ObservableProperty, NotifyPropertyChangedFor(nameof(Color))]
		private int _red;

		[ObservableProperty, NotifyPropertyChangedFor(nameof(Color))]
		private int _green;

		[ObservableProperty, NotifyPropertyChangedFor(nameof(Color))]
		private int _blue;

		[ObservableProperty, NotifyPropertyChangedFor(nameof(Color))]
		private int _alpha = 255;

		public Color Color => Color.FromArgb((byte)Alpha, (byte)Red, (byte)Green, (byte)Blue);

		[ObservableProperty]
        private string _name = "";
	}
}
