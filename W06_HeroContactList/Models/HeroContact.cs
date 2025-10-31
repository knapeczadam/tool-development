using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace W06_HeroContactList.Models;


    internal partial class HeroContact : ObservableObject
    {
        [property: JsonProperty("id")]
        [ObservableProperty]
        private uint _id;

        [property: JsonProperty("first_name")]
        [ObservableProperty]
        private string _firstName;

        [property: JsonProperty("last_name")]
        [ObservableProperty]
        private string _lastName;

        [property: JsonProperty("avatar")]
        [ObservableProperty]
        private string _avatarPath;

        [property: System.Text.Json.Serialization.JsonIgnore]
        [ObservableProperty]
        private ImageSource _avatar;
    }
