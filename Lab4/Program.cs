using System.Runtime.InteropServices;
using McMaster.Extensions.CommandLineUtils;
using ClassLibrary_Lab4;


namespace Lab4
{
    [Command(Name = "Lab4", Description = "Application for lab 1-3")]


    [Subcommand(typeof(VersionCommand), typeof(RunCommand), typeof(SetPathCommand))]
    class Program
    {
        public static int Main(string[] args)
        {
            var clApp = new CommandLineApplication<Program>();


            clApp.Conventions.UseDefaultConventions();

            try
            {
                return clApp.Execute(args);
            }
            catch (CommandParsingException)
            {
                return 0;
            }
        }


        private void OnExecute(CommandLineApplication clApp)
        {
            DisplayCommands();
        }

        private static void DisplayCommands()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("  version         -Displays application information");
            Console.WriteLine("  run       -Runs the specified lab exercise");
            Console.WriteLine("                Examples:");
            Console.WriteLine("                Lab4 execute lab1 -i input.txt -o output.txt");
            Console.WriteLine("  set-path   -Sets the folder path for input/output files for all supported OS");
            Console.WriteLine("                Example:");
            Console.WriteLine("                Lab4 config-path -p /path/to/folder");
            Console.WriteLine("  help          -Displays the information");
        }

        [Command("version", Description = "Displays application information and author")]
        class VersionCommand
        {
            private void OnExecute()
            {
                Console.WriteLine("An author: Pushkaruk Nazar");
                Console.WriteLine("A version: 1.0.0");
            }
        }


        [Command("run", Description = "Runs the specified lab exercise")]
        class RunCommand
        {
            [Argument(0, "lab", "Lab runs (lab1, lab2, lab3)")]
            public string Lab_Name { get; set; }

            [Option("-i|--input", "Input file", CommandOptionType.SingleValue)]
            public string Path_Input { get; set; }

            [Option("-o|--output", "Output file", CommandOptionType.SingleValue)]
            public string Path_Output { get; set; }


            private void OnExecute()
            {
                Console.WriteLine(Environment.GetEnvironmentVariable("LAB_PATH"));
                string input_Path = Path_Input ?? Environment.GetEnvironmentVariable("LAB_PATH") ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
                string output_Path = Path_Output ?? Environment.GetEnvironmentVariable("LAB_PATH") ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));

                input_Path = Path.Combine(input_Path, "INPUT.txt");
                output_Path = Path.Combine(output_Path, "OUTPUT.txt");


                if (!File.Exists(input_Path))
                {
                    Console.WriteLine($"File {input_Path} isn't found");
                }

                switch (Lab_Name?.ToLower())
                {
                    case "lab1":
                        Lab1.GoLab1(input_Path, output_Path);
                        break;
                    case "lab2":
                        Lab2.GoLab2(input_Path, output_Path);
                        break;
                    case "lab3":
                        Lab3.GoLab3(input_Path, output_Path);
                        break;
                    default:
                        Console.WriteLine("Error. You need to use lab(1-3).");
                        break;
                }

                Console.WriteLine($"Finished Success. Results is written in {output_Path}");
            }
        }

        [Command("set-path", Description = "Sets the folder path for input/output files for OS")]
        class SetPathCommand
        {
            [Option("-p|--path", "Folder path", CommandOptionType.SingleValue)]
            public string Path_Directory { get; set; }

            private void OnExecute()
            {
                if (string.IsNullOrEmpty(Path_Directory))
                {
                    Console.WriteLine("Path is empty.");
                }

                try
                {
                    ConfigureEnvironmentVariable("LAB_PATH", Path_Directory);
                    Console.WriteLine($"Environment variable LAB_PATH set to: {Path_Directory}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            private void ConfigureEnvironmentVariable(string variableName, string value)
            {
                if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
                {
                    string config_Path = OperatingSystem.IsLinux() ? "/etc/environment" : "/etc/paths";

                    if (File.Exists(config_Path))
                    {
                        using (StreamWriter writer = File.AppendText(config_Path))
                        {
                            writer.WriteLine($"{variableName}={value}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("System file isn't found");
                        throw new InvalidOperationException("Error in setting environment variable.");
                    }
                }
                else if (OperatingSystem.IsWindows())
                {
                    Environment.SetEnvironmentVariable(variableName, value, EnvironmentVariableTarget.Machine);
                }
            }
        }
    }
}