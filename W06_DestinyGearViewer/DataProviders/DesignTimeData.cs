using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W06_DestinyGearViewer.Misc;
using W06_DestinyGearViewer.Models;

namespace W06_DestinyGearViewer.DataProviders
{
    static class DesignTimeData
    {
        public static DestinyPlayer MockPlayer => Helpers.DataProvider.GetMockData();
        public static DestinyCharacter MockCharacter => MockPlayer.Characters[0];
        public static DestinyItem MockItem => MockCharacter.Items[0];
    }
}
