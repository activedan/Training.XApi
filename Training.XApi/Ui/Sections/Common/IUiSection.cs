namespace Training.XApi.Ui.Sections.Common
{
    public interface IUiSection
    {
        string SectionType { get; }
        bool IsFormSection { get; }
    }
}
