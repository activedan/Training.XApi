using System.ComponentModel;

namespace Training.XApi.Engine.Enums
{
    public enum ProductType
    {
        Unknown,

        Discount,

        #region Packages

        /// <summary>StandardAdvert</summary>
        [Description("Standard Advert")]
        StandardAdvert,

        /// <summary>PremiumAdvert</summary>
        [Description("Premium Advert")]
        PremiumAdvert,

        /// <summary>PremiumPlusAdvert</summary>
        [Description("PremiumPlus Advert")]
        PremiumPlusAdvert,

        #endregion

        #region Products

        /// <summary>PrivacyProtect</summary>
        [Description("Privacy Protect")]
        PrivacyProtect,

        #endregion

        #region Upsells

        /// <summary>PackageUpgrade item will change the advert package and charge an amount that is calculated on the fly</summary>
        [Description("Premium Upgrade")]
        PackageUpgrade,

        /// <summary>SearchPriority item will change the advert search priority</summary>
        SearchPriority,

        /// <summary>MaxPrice item will increase the maximum allowed advert price</summary>
        MaxPrice,

        /// <summary>NumPhotos item will increase the maximum allowed number of photos</summary>
        ExtraPhotos,

        /// <summary>NumComments item will increase the maximum allowed number of comments</summary>
        ExtraComments,

        /// <summary>RedBookCertificate</summary>
        [Description("RedBook Certificate")]
        RedBookCertificate,

        /// <summary>PriceAssist</summary>
        [Description("PriceAssist")]
        PriceAssis = 0x800,

        /// <summary>Sticker</summary>
        [Description("Sticker")]
        Sticker,

        /// <summary>CarFacts</summary>
        [Description("CarFacts")]
        CarFacts,

        /// <summary>
        /// BikeFacts PPSR Report
        /// </summary>
        [Description("BikeFacts PPSR Report")]
        BikeFacts,

        /// <summary>CarFacts regenerate</summary>
        [Description("CarFacts History Report")]
        CarFactsRegeneration,

        /// <summary>
        /// BoatFacts PPSR Report
        /// </summary>
        [Description("BoatFacts PPSR Report")]
        BoatFacts,

        [Description("Showcase")]
        Showcase,

        [Description("Showcase Repurchase")]
        ShowcaseRepurchase,

        [Description("Extended Showcase Repurchase")]
        ShowcaseRepurchaseExtended,

        [Description("Instant Offer Booking")]
        InstantOfferBooking,

        [Description("Standard Upgrade")]
        StandardPackageUpgrade,

        /// <summary>StandardAdvert</summary>
        [Description("Free Advert")]
        FreeAdvert,

        [Description("Standard Package Photo Upsell")]
        StandardPackagePhotoUpsell,

        [Description("Premium Package Photo Upsell")]
        PremiumPackagePhotoUpsell,

        [Description("Redbook Inspect Upsell")]
        RedbookInspect,

        [Description("Seller Kit")]
        SellerKit,

        [Description("Advert")]
        Advert,

        [Description("CallConnect")]
        CallConnect,

        [Description("Newspaper")]
        Newspaper

        #endregion
    }
}
