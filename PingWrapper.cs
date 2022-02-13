using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Pingeratorv3
{
    class PingWrapper
    {
        //Given a remote host, attempt to ping it and return the given result
        public static PingReply PingRemoteHost(string ipORDomain)
        {
            PingReply reply = null;
            //try to ping the host
            try 
            {
                var pinger = new Ping();
                reply = pinger.Send(ipORDomain);
                var  response = reply;
                Console.WriteLine(response);

            }
            catch(PingException)
            {

            }
            return reply;
        }
    }
}
