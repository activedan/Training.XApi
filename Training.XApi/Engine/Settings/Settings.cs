using System;

namespace Training.XApi.Engine.Settings
{
    public interface ISettings
    {
        string ProductApiUrl { get; }
        string MemberApiUrl { get; }
    }
    public class Settings : ISettings
    {
        public string ProductApiUrl => throw new NotImplementedException();

        public string MemberApiUrl => throw new NotImplementedException();
    }
}
