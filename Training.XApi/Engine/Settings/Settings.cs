using System;

namespace Training.XApi.Engine.Settings
{
    public interface ISettings
    {
        string AdvertApiUrl { get; }
        string ProductApiUrl { get; }
        string MemberApiUrl { get; }
    }
    public class Settings : ISettings
    {
        public string AdvertApiUrl => "";
        public string ProductApiUrl => "";

        public string MemberApiUrl => "";
    }
}
