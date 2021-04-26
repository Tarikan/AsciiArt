namespace AsciiArt.Cli
{
    public static class Const
    {
        public static readonly string HtmlTemplate =
            "<link rel=\"stylesheet\" type=\"text/css\" href=\"//fonts.googleapis.com/css?family=Nova+Square\" />\n" +

            "        <style>\n" +
            "        div.a {{ \n" +
            "            line-height: 1;\n" +
            "            font-family: Nova Square;\n" +
            "            font-size: 14;\n" +
            "        }}\n" +
            "        </style>\n" +
            "        <div class=\"a\">\n" +
            "        <pre>\n" +
            "{0}\n" +
            "        </pre>\n" +
            "        </div>\n";
    }
}