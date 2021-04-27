namespace AsciiArt.Cli
{
    public static class Const
    {
        public static readonly string HtmlTemplate =
            "<link rel=\"stylesheet\" type=\"text/css\" href=\"//fonts.googleapis.com/css?family=Nova+Square\" />\n" +

            "        <style>\n" +
            "        body {{ \n" +
            "            line-height: 8px;\n" +
            "        }}\n" +
            "        </style>\n" +
            "        <div class=\"a\">\n" +
            "        <pre>\n" +
            "{0}\n" +
            "        </pre>\n" +
            "        </div>\n";
    }
}