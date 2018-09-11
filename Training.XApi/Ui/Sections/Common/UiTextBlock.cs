namespace Training.XApi.Ui.Sections.Common
{
    public class UiTextBlock : IUiSection
    {
        public string SectionType => "ui-text-block";
        public bool IsFormSection => false;

        public string Text { get; }
        public string Style { get; }

        private UiTextBlock(string text, string style)
        {
            if (text != null)
            {
                Text = text;
                Style = style;
            }
        }

        static public UiTextBlock Plain(string text) => new UiTextBlock(text, "plain");
        static public UiTextBlock PageTitle(string text) => new UiTextBlock(text, "page-title");
        static public UiTextBlock PageSubtitle(string text) => new UiTextBlock(text, "page-subtitle");
        static public UiTextBlock SectionTitle(string text) => new UiTextBlock(text, "section-title");
        static public UiTextBlock SectionSubtitle(string text) => new UiTextBlock(text, "section-subtitle");
        static public UiTextBlock BasicHeading(string text) => new UiTextBlock(text, "heading");
        static public UiTextBlock Emphasis(string text) => new UiTextBlock(text, "emphasis");
        static public UiTextBlock Paragraph(string text) => new UiTextBlock(text, "paragraph");
        static public UiTextBlock Note(string text) => new UiTextBlock(text, "note");
        static public UiTextBlock CardTitle(string text) => new UiTextBlock(text, "card-title");
    }
}
