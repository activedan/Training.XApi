using System.Collections.Generic;

namespace Training.XApi.Ui.Sections.Common
{
    public class UiActionNavigate : IUiAction
    {
        public string ActionType => "navigate";
        public string Url { get; }

        public string ContentUrl { get; private set; }
        public List<IUiSection> Content { get; private set; }

        public UiActionNavigate(string url)
        {
            Url = url;
        }

        public UiActionNavigate SetContent(string contentUrl, List<IUiSection> content)
        {
            ContentUrl = contentUrl;
            Content = content;
            return this;
        }
    }
}
