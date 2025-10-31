using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W05_DataBinding.ManualBinding
{
    public class ManualUser
    {
        public event Action<string> OnFirstNameChanged;
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnFirstNameChanged?.Invoke(value);
                }
            }
        }

        public event Action<string> OnLastNameChanged;
        private string _lastName;

        public string LastName
        {

            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnLastNameChanged?.Invoke(value);
                }
            }
        }
    }
}
