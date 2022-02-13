using System;

namespace Pingeratorv3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(PingWrapper.PingRemoteHost("8.8.8.8").RoundtripTime);
            Console.WriteLine(PingWrapper.PingRemoteHost("8.8.8.8").Address);
            Console.WriteLine("Hello World!");
        }
    }
}
