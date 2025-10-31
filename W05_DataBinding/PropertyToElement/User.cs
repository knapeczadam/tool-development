using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace W05_DataBinding.PropertyToElement
{
    public class User : DependencyObject
    {
        public static readonly DependencyProperty FirstNameProperty =
            DependencyProperty.Register(nameof(FirstName), typeof(string), typeof(User),
                new PropertyMetadata(default(string)));
        public string FirstName
        {
            get => (string)GetValue(FirstNameProperty);
            set => SetValue(FirstNameProperty, value);
        }

        public static readonly DependencyProperty LastNameProperty =
            DependencyProperty.Register(nameof(LastName), typeof(string), typeof(User), new PropertyMetadata(default(string)));
        public string LastName
        {
            get => (string) GetValue(LastNameProperty);
            set => SetValue(LastNameProperty, value);
        }

    }
}
