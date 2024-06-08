using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace wcTool
{
    public class wcTool
    {
        public static void Main(string[] args)
        {
            var app = new CommandApp();

            app.Configure(config =>
            {
                config.AddCommand<CountCommand>("wc");
            });

            app.Run(args);
        }

        public class CountCommand : Command<CountCommand.Settings>
        {
            public class Settings : CommandSettings
            {
                [CommandOption("-l|--lines")]
                [DefaultValue(false)]
                [Description("Give no of lines")]
                public bool? IsLine { get; init; }

                [CommandOption("-w|--words")]
                [DefaultValue(false)]
                [Description("Give no of words")]
                public bool? IsWord { get; init; }

                [CommandOption("-c|--chars")]
                [DefaultValue(false)]
                [Description("Give no of chars")]
                public bool? IsChar { get; init; }

                [Description("Enter the file path")]
                [CommandArgument(0,"[searchPath]")]
                public string? SearchPath { get; init; }

            }
            public override int Execute(CommandContext context, Settings settings)
            {
                AnsiConsole.Write(
                    new FigletText("WC tool replica").LeftJustified().Color(Color.Teal));

                var isLine=settings.IsLine;
                var isWord=settings.IsWord;
                var isChar=settings.IsChar;
                var searchPath = settings.SearchPath;
                int charCount=0, lineCount=0, wordCount = 0;

                if(searchPath==null)
                {
                    AnsiConsole.WriteLine("Please enter the path");
                    return 0;
                }

                    Console.WriteLine("Entered the actual execution");

                    if(!File.Exists(searchPath))
                    {
                        Console.WriteLine("File not found");
                        return 0;
                    }

                try
                {
                    string text = File.ReadAllText(searchPath);

                    if (isChar == true)
                    {
                        charCount = text.Length;
                        Console.WriteLine("Character count " + charCount);
                    }
                    if (isLine == true)
                    {
                        lineCount = File.ReadAllLines(searchPath).Length;
                        Console.WriteLine("Letter count " + lineCount);
                    }
                    if ( isWord == true)
                    {
                        wordCount = text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
                        Console.WriteLine("Word count " + wordCount);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occured: " + e.Message);
                }



                return 0;

                //throw new NotImplementedException();
            }
        }
    }
}