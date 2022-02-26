using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pingeratorv3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sanity Checks
            /*Console.WriteLine("Hello World!");
            DictionaryTest.Test();
            Console.WriteLine(FileActions.ConcatFilenameToPath("beans.mp3"));
            FileActions.GenerateSettingsFile();
            FileActions.ParseSettingsFromFile();*/
            Console.WriteLine("Starting Server ....");
            //Get settings from file
            Dictionary<string, string> settings = new Dictionary<string, string>();
            settings = FileActions.ParseSettingsFromFile();
            //Turn port and url into prefix
            string[] prefixes = { $"{settings["serverAddress"]}:{settings["serverPort"]}/" };
            foreach (string prefix in prefixes)
            {
                Console.WriteLine(prefix);
            }
            string soundsPath = settings["soundsFolder"];
            HttpSoundServer.SimpleListenerExample(prefixes, soundsPath);
            List<Task> tasks = new List<Task>();
            Console.WriteLine(PingWrapper.PingRemoteHost("8.8.8.8").RoundtripTime);
            Console.WriteLine(PingWrapper.PingRemoteHost("8.8.8.8").Address);
            Console.WriteLine("Hello World!");
        }
    }
}
