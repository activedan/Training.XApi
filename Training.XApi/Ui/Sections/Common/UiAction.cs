namespace Training.XApi.Ui.Sections.Common
{
    public interface IUiAction
    {
        string ActionType { get; }
    }

    public class UiAction : IUiAction
    {
        public string ActionType { get; }
        public string Title { get; private set; }
        
        public UiAction(string actionType)
        {
            ActionType = actionType;
        }

        public UiAction SetTitle(string title)
        {
            Title = title;
            return this;
        }
    }
}
