using System.Collections.Generic;
using Training.XApi.Engine.Extensions;
using Training.XApi.Engine.Models.Adverts;
using Training.XApi.Engine.Settings;
using Training.XApi.Ui.Sections.AdvertListings;
using Training.XApi.Ui.Sections.Common;

namespace Training.XApi.UiFactories.Desktop
{
    public class AdvertListUiFactory
    {
        public List<IUiSection> GetAdvertListUi(IEnumerable<Advert> adverts, ISettings apiSettings)
        {
            var advertListings = new List<UiAdvertListing>();

            foreach (var advert in adverts)
            {
                advertListings.Add(new UiAdvertListing(advert, apiSettings));
            }

            var sections = new List<IUiSection>();

            sections.Push(new UiAdvertListDetails($"You currently have {advertListings.Count} cars on carsales"));
            sections.Push(new UiAdvertListings($"Your cars", advertListings));

            return sections;
        }
    }
}
