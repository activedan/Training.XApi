using System;

namespace CodeExamples.O2
{
    public class ParserSecond
    {
        public SomeRepr Parse(string input)
        {
            if (input.StartsWith("Title{"))
            {
                var intermediateVal = input.Substring("Title{".Length);

                var takeTil = intermediateVal.IndexOf("}", StringComparison.Ordinal);

                return new Title { TitleName = intermediateVal.Substring(0, takeTil) };
            }

            // Pretend this is handled
            throw new NotImplementedException();
        }
    }

    public abstract class SomeRepr
    {

    }

    public class Title : SomeRepr
    {
        public string TitleName { get; set; }
    }
}