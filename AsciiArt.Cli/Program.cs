using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using AsciiArt.Core;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.VisualBasic;

namespace AsciiArt.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication<Program>();

            app.Command("convert",
                (convert) =>
                {
                    var inputFileOption = convert.Option<string>("-i|--input", "Input file to convert"
                            , CommandOptionType.SingleValue)
                        .IsRequired();

                    var invertOption = convert.Option("--invert", "Invert colors"
                        , CommandOptionType.NoValue);

                    var scaleOption = convert.Option<string>("-s|--scale", "Scale image size"
                        , CommandOptionType.SingleValue);

                    var outputOption = convert.Option<string>("-o|--output", "Output File"
                        , CommandOptionType.SingleValue);

                    convert.HelpOption("-? | -h | --help");

                    convert.OnExecute(() =>
                    {
                        Byte[] img;
                        bool invert;
                        double compressRatio = 1;
                        string outputFile;

                        invert = invertOption.HasValue();
                        
                        outputFile = outputOption.Value()
                                     ?? "output.html";
                        
                        
                        if (inputFileOption.Value() == null)
                        {
                            Console.WriteLine("No input file suggested.");
                            return;
                        }

                        

                        try
                        {
                            img = File.ReadAllBytes(inputFileOption.Value());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Something wrong with input file.");
                            return;
                        }


                        
                        
                        if (scaleOption.Value() != null)
                        {
                            try
                            {
                                compressRatio = Double.Parse(scaleOption.Value(), CultureInfo.InvariantCulture);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Wrong format of scale option.");
                                return;
                            }
                        }

                        var result = ImageUtils.GrayscaleToAscii(ImageUtils.ReadAndGreyscale(img, compressRatio, invert));

                        var output = File.Open(outputFile, FileMode.Create, FileAccess.Write);
                        
                        output.Write(new UTF8Encoding(true).GetBytes(String.Format(Const.HtmlTemplate, result)));
                        output.Close();

                        Console.WriteLine("Finished!");

                        // do the conversation!
                    });
                    
                }
            );
            app.Execute(args);
        }
    }
}