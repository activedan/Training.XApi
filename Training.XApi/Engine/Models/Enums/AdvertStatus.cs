namespace Training.XApi.Engine.Enums
{
    /// <summary>Advert status enum</summary>
    public enum AdvertStatus
    {
        /// <summary>Default - error.</summary>
        Unknown,

        /// <summary>A new advert. Never been approved.</summary>
        New,

        /// <summary>Approved.</summary>
        Approved,

        /// <summary>Expired - can be reactivated by the seller for a set amount of time. If not reactivated it will be cancelled.</summary>
        Expired,

        /// <summary>Paused - ad taken down "temporarily".</summary>
        Paused,

        /// <summary>Cancelled - no longer for sale and cannot be reactivated by the seller (only by admin/cst).</summary>
        Cancelled,

        /// <summary>Advert is suspected to be fraudulent. When an ad is in this status, it's cancelled.</summary>
        Fraudulent,

        /// <summary>Advert breaks T&C.</summary>
        NonCompliant
    }
}
