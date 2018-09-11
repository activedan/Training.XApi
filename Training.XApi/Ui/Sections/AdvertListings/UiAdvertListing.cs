using System;
using System.Collections.Generic;
using Training.XApi.Engine.Extensions;
using Training.XApi.Engine.Models.Adverts;
using Training.XApi.Engine.Settings;
using Training.XApi.Ui.Sections.Common;

namespace Training.XApi.Ui.Sections.AdvertListings
{
    public class UiAdvertListings : IUiSection
    {
        public string PageType => "ui-advert-listings";
        public string SectionType => null;
        public bool IsFormSection => false;

        public UiTextBlock Title { get; set; }

        public List<UiAdvertListing> AdvertListings { get; set; }

        public UiAdvertListings(string title, List<UiAdvertListing> advertListings)
        {
            Title = UiTextBlock.SectionTitle(title);
            AdvertListings = advertListings;
        }
    }

    public class UiAdvertListing
    {
        public UiTextBlock AdStatus { get; set; }
        public string Status { get; set; }
        public UiTextBlock AdTitle { get; set; }
        public UiTextBlock AdId { get; set; }
        public UiTextBlock AdType { get; set; }
        public UiTextBlock AdPrice { get; set; }
        public UiTextBlock AdDateTimeCreated { get; set; }

        public UiAdvertListing(Advert advert, ISettings apiSettings)
        {
            AdStatus = UiTextBlock.Paragraph(advert.Status.ToString());

            AdTitle = UiTextBlock.Emphasis(advert.Title);
            AdId = UiTextBlock.Paragraph(advert.AdvertReference);
            AdType = UiTextBlock.Paragraph("Standard");

            AdPrice = UiTextBlock.Paragraph(String.Format("{0:C}", advert.EditingData.Price));
            AdDateTimeCreated = UiTextBlock.Paragraph(advert.DateCreated.GetHumanReadableDate());
        }
    }
}
