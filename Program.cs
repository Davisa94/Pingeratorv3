using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pingeratorv3
{
    class Program
    {
        static void Main(string[] args)
        {
            string googleDNS = "8.8.8.8";
            string cloudFlareDNS = "1.1.1.1";
            string openDNS = "208.67.222.222";
            //Sanity Checks
            /*Console.WriteLine("Hello World!");
            DictionaryTest.Test();
            Console.WriteLine(FileActions.ConcatFilenameToPath("beans.mp3"));
            FileActions.GenerateSettingsFile();
            FileActions.ParseSettingsFromFile();*/
            Console.WriteLine("Starting Server ....");
            //Get settings from file
            // Dictionary<string, string> settings = new Dictionary<string, string>();
            // settings = FileActions.ParseSettingsFromFile();
            //Turn port and url into prefix
            // string[] prefixes = { $"{settings["serverAddress"]}:{settings["serverPort"]}/" };
            // foreach (string prefix in prefixes)
            // {
            //     Console.WriteLine(prefix);
            // }
            // string soundsPath = settings["soundsFolder"];
            // HttpSoundServer.SimpleListenerExample(prefixes, soundsPath);
            // List<Task> tasks = new List<Task>();
            
            // init each instance of the ping class
            PingTest googlePing = new PingTest(googleDNS);
            PingTest cloudflarePing = new PingTest(cloudFlareDNS);
            PingTest openDNSPing = new PingTest(openDNS);
            // List<PingTest> pingers = new List<PingTest>();
            // pingers.Add(googlePing);
            // pingers.Add(cloudflarePing);
            // pingers.Add(openDNSPing);

            // init speed test object
            SpeedTest speedyTester = new SpeedTest();
            // init database manager
            DatabaseManager dbManager = new DatabaseManager();
            //dbManager.buildDatabase();
            //dbManager.testInsert();
            while (true)
            {
                // ping google
                long googlePingResults = googlePing.runPing();
                // ping cloudflare
                long cloudFlarePingResults = cloudflarePing.runPing();
                // ping openDNS
                long openDNSPingResults = openDNSPing.runPing();
                // Test Speed
                double uploadSpeed = speedyTester.testUpload();
                double downloadSpeed = speedyTester.testDownload();
                // Write each to database
                dbManager.InsertPing(googlePingResults, googleDNS);
                dbManager.InsertPing(cloudFlarePingResults, cloudFlareDNS);
                dbManager.InsertPing(openDNSPingResults, openDNS);
                dbManager.InsertSpeed(uploadSpeed, downloadSpeed);
                // repeat
            }
            // Console.WriteLine(PingWrapper.PingRemoteHost("8.8.8.8").RoundtripTime);
            // Console.WriteLine(PingWrapper.PingRemoteHost("8.8.8.8").Address);
            // Console.WriteLine("Hello World!");
        }
    }
}
