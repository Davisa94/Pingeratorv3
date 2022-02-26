using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Pingeratorv3
{
    /*from modules import GlobalValues as GV
from pythonping import ping
import sys


class Pinger:
    def pinger(self, host):
        responses = ping(host, count=1)
        return responses

# TODO: Comment me
class Ping(Pinger):
    def __init__(self):
        self.host=""

    def run(self, db_interactor):
        responses = self.pinger(self.host)
        res = {}
        for response in responses:
            res = response
        print(res)
        sys.stdout.flush()
        db_interactor.insertPing(res, self.host)

class PingGoogle(Ping):
    def __init__(self):
        self.host = GV._GOOGLEHOST

class PingOpenDNS(Ping):
    def __init__(self):
        self.host = GV._OPENDNSHOST

class PingCloudFlare(Ping):
    def __init__(self):
        self.host = GV._CLOUDFLAREHOST*/
    class PingTest
    {
        public System.Net.IPAddress host;
        public PingOptions options;
        public string data;
        public byte[] buffer;
        public int timeout;

        public PingTest(string host, string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", int timeout=200)
        {
            this.host = System.Net.IPAddress.Parse(host);
            this.data = data;
            this.timeout = timeout;
            this.buffer = Encoding.ASCII.GetBytes(data);
            this.options.DontFragment = true;
        }
        public long runPing()
        {
            //init a rtt variable double
            long roundTripTime = -1;
            //Step one try to ping the given host
            try
            {
                Ping pinger = new Ping();
                PingReply reply = pinger.Send(this.host, this.timeout, this.buffer, this.options);
                //check status
                if(reply.Status == IPStatus.Success)
                {
                    roundTripTime = reply.RoundtripTime;
                }
            }
            catch (Exception E)
            {
                Console.WriteLine("An exception has occured" + E.ToString());
            }
            return roundTripTime;

            //set the round trip time var
            //catch any exceptions
            //return the RTT variable
         //https://docs.microsoft.com/en-us/dotnet/api/system.net.networkinformation.ping?view=net-6.0
        }
    }
}
