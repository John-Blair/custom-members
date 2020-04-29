using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JB.Helpers
{
    static public class TextSection
    {
        static public string TextClass(int textSize)
        {
            var textClass = "";
            switch (textSize)
            {
                case 1:
                    textClass = "h6";
                    break;
                case 2:
                    textClass = "h5";
                    break;
                case 3:
                    textClass = "h4";
                    break;
                case 4:
                    textClass = "h3";
                    break;
                case 5:
                    textClass = "h2";
                    break;
                case 6:
                    textClass = "h1";
                    break;
            }
            return textClass;
        }

        static public string Title(bool isMainTitle, int titleSize, string title)
        {
            string htmlTitle = string.Empty;
            string textClass;

            if (isMainTitle)
            {
                // Use H1 with the correct font size.
                textClass = TextSection.TextClass(titleSize);
                if (titleSize == 6)
                {
                    // Using H1 title size - no need for the h1 class too.
                    textClass = "";
                }
                if (!string.IsNullOrEmpty(textClass))
                {
                    htmlTitle = $"<h1 class='{textClass}'>{title}</h1>";

                }
                else
                {
                    htmlTitle = $"<h1>{title}</h1>";

                }
            }
            else
            {
                // Don't use H1.
                switch (titleSize)
                {
                    case 1:
                        htmlTitle = $"<h6>{title}</h6>";
                        break;
                    case 2:
                        htmlTitle = $"<h5>{title}</h5>";
                        break;
                    case 3:
                        htmlTitle = $"<h4>{title}</h4>";
                        break;
                    case 4:
                        htmlTitle = $"<h3>{title}</h3>";
                        break;
                    case 5:
                        htmlTitle = $"<h2>{title}</h2>";
                        break;
                    case 6:
                        // Only 1 H1 per page - and user must explicitly check it.
                        // Otherwise use the equivalent class to get same format.
                        htmlTitle = $"<div class='h1'>{title}</div>";
                        break;
                }
            }

            return htmlTitle;
        }
    }
}
