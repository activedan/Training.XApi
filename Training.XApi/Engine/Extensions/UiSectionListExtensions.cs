using System.Collections.Generic;
using Training.XApi.Ui.Sections.Common;

namespace Training.XApi.Engine.Extensions
{
    public static class UiSectionListExtensions
    {
        public static List<IUiSection> Push(this List<IUiSection> uiSections, IUiSection section)
        {
            uiSections.Add(section);
            return uiSections;
        }
    }
}
