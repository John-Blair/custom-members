using System.Text;

namespace JB.Helpers
{
    public static class StringBuilderExtensions
    {

        public static void AppendHtmlLine(this StringBuilder str, string value)
        {
            const string newline = "<br/>";

            str.AppendLine(value + newline);
        }

    }
}
