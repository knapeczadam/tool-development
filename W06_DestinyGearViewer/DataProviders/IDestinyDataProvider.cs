using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W06_DestinyGearViewer.Models;

namespace W06_DestinyGearViewer.DataProviders
{
    internal interface IDestinyDataProvider
    {
        DestinyPlayer GetPlayerData(string playerName);
        DestinyPlayer GetMockData();

        byte[] GetResource(string resourceName);
    }
}
