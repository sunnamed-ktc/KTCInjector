using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace KTCInjector
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "KTCInjector";

            Console.ForegroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;

            WriteWithColor("Injecting started, wait 5 seconds...", ConsoleColor.White);

            bool isProceesHas = true;
            if (!FindNeededProcess())
            {
                isProceesHas = false;

                do
                {
                    StartKingdomTwoCrowns();
                }
                while (!FindNeededProcess());
            }

            if (isProceesHas)
            {
                if (TryInjectGame())
                {
                    WriteWithColor("[Inject Successfully] - #1 All good, enjoy dude =)", ConsoleColor.Green);
                }
            }
            else
            {
                if (TryInjectWithStartDelayGame())
                {
                    WriteWithColor("[Inject Successfully] - #2 All good, enjoy dude =)", ConsoleColor.Green);
                }
                else
                {
                    WriteWithColor("[Inject Failure] - Hmm... try to do this again, something went wrong =( It`s not ur day. Hah", ConsoleColor.Red);
                }
            }

            while (true)
            {
                if (!FindNeededProcess())
                {
                    Environment.Exit(0);
                }
            }
        }

        public static void StartKingdomTwoCrowns()
        {
            Process.Start("steam://rungameid/701160");
        }

        public static bool TryInjectWithStartDelayGame(int delay = 5)
        {
            bool result = false;
            try
            {
                Process kingdomtwocrowns = Process.Start("steam://rungameid/701160");
                Thread.Sleep(delay * 1000);

                if (kingdomtwocrowns != null)
                {
                    TryInjectGame();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                WriteWithColor("#1 Error, with game happend something bad! \n" + $"({ex.Message})", ConsoleColor.Red);
                WriteWithColor("#1 Press any button to close application!", ConsoleColor.Yellow);
                Console.ReadKey();

                Environment.Exit(0);
            }
            return result;
        }

        public static bool TryInjectGame()
        {
            bool result = false;
            try
            {
                if (!FindNeededProcess())
                {
                    return result;
                }

                ProcessStartInfo info = new ProcessStartInfo("smi.exe", "inject -p KingdomTwoCrowns -a KTCHack.dll -n KTCHack -c Injection -m Inject");

                info.WindowStyle = ProcessWindowStyle.Hidden;

                Process process = Process.Start(info);

                if (process != null)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                WriteWithColor("#2 Error, with game happend something bad! \n" + $"({ex.Message})", ConsoleColor.Red);
                WriteWithColor("#2 Press any button to close application!", ConsoleColor.Yellow);
                Console.ReadKey();

                Environment.Exit(0);
            }
            return result;
        }

        public static bool FindNeededProcess()
        {
            return Process.GetProcesses().FirstOrDefault(p => p.ProcessName == "KingdomTwoCrowns") != null;
        }

        public static void WriteWithColor(string text, ConsoleColor color)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;

            Console.ForegroundColor = color;

            Console.WriteLine(text);

            Console.ForegroundColor = defaultColor;
        }
    }
}
