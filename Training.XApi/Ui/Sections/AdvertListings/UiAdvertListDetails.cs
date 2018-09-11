using Training.XApi.Ui.Sections.Common;

namespace Training.XApi.Ui.Sections.AdvertListings
{
    public class UiAdvertListDetails : IUiSection
    {
        public string SectionType => "ui-advert-list-details";

        public bool IsFormSection => false;

        public UiTextBlock AdvertSummary { get; }
        public UiControlButton CreateAdBtn { get; set; }

        public UiAdvertListDetails(string advertSummary)
        {
            AdvertSummary = UiTextBlock.SectionSubtitle(advertSummary);
            CreateAdBtn = UiControlButton.Primary("manage-ad", "Create ad", new UiActionNavigate("https://www.carsales.com.au/sell-my-car"));
        }
    }
}
