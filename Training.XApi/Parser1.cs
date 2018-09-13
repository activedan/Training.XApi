using System;
using System.Collections.Generic;

namespace CodeExamples.O1
{
    public class ParserFirst
    {
        private readonly AllParsers parsers;

        public ParserFirst(AllParsers parsers)
        {
            this.parsers = parsers;
        }

        public SomeRepr Parse(string input)
        {
            return parsers.Parse(input);
        }
    }

    public abstract class SomeRepr
    {

    }

    public class Title : SomeRepr
    {
        public string TitleName { get; set; }
    }

    public interface IParser<T>
    {
        bool Parse(string text, out T value);
    }

    public class TitleParser : IParser<SomeRepr>
    {
        public bool Parse(string text, out SomeRepr value)
        {
            value = null;

            if (!text.StartsWith("Title{"))
                return false;

            var intermediateVal = text.Substring("Title{".Length);

            var takeTil = text.IndexOf("}", StringComparison.Ordinal);

            value = new Title { TitleName = intermediateVal.Substring(0, takeTil) };

            return true;
        }
    }

    public class AllParsers : List<IParser<SomeRepr>>
    {
        public SomeRepr Parse(string text)
        {
            SomeRepr value = null;

            foreach (var parser in this)
            {
                if (parser.Parse(text, out value))
                    return value;
            }

            return value;
        }
    }
}