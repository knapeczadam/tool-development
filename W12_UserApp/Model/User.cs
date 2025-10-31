using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace W12_UserApp.Model
{
    public class User : INotifyPropertyChanged
    {
        [JsonProperty("id")]
        [XmlElement("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [XmlIgnore]
        private string? _firstName;

        [JsonProperty("first_name")]
        [XmlElement("first_name")]
        public string? FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FirstName)));
            }

        }

        [XmlIgnore]
        private string? _lastName;

        [JsonProperty("last_name")]
        [XmlElement("last_name")]
        public string? LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LastName)));
            }

        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
