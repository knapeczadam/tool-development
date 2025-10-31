using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W06_HeroContactList.Models;


    internal interface IHeroContactService
    {
        List<HeroContact> LoadContacts(string filePath);
        void SaveContacts(string filePath, List<HeroContact> contacts);

        List<HeroContact> LoadMockContacts()
        {
            return
            [
                new HeroContact
                {
                    Id = 1,
                    FirstName = "Tony",
                    LastName = "Stark",
                    AvatarPath = "TonyStark.jpg",
                    Avatar = Helpers.GetImageSource("Resources/TonyStark.jpg")
                }
            ];
        }
    }
