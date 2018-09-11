namespace Training.XApi.Ui.Sections.Common
{
    public class UiControlButton : IUiControl
    {
        public string Type => "form-control-button";

        public string Id { get; }
        public string Label { get; }
        public string Style { get; }
        public IUiAction Action { get; }
        public string ButtonType { get; }
        public bool? AlwaysEnabled { get; private set; }

        private UiControlButton(string id, string label, IUiAction action, string style, string buttonType = null)
        {
            Id = id;
            Label = label;
            Style = style;
            Action = action;
            ButtonType = buttonType;
        }

        public UiControlButton IsAlwaysEnabled()
        {
            AlwaysEnabled = true;
            return this;
        }

        public static UiControlButton Primary(string id, string label, IUiAction action) => new UiControlButton(id, label, action, "primary");

        public static UiControlButton Secondary(string id, string label, IUiAction action) => new UiControlButton(id, label, action, "secondary");

        public static UiControlButton Breadcrumb(string id, string label, IUiAction action) => new UiControlButton(id, label, action, "breadcrumb");

        public static UiControlButton FormValidate(string id, string label, IUiAction action) => new UiControlButton(id, label, action, "primary", "form-control-form-button");

    }
}
