using NSpeedTest;
using NSpeedTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pingeratorv3
{
// import sys
// import speedtest
// ################################################################
// # A class that encapsulates the speedtest library and gives
// # Useful methods for simplifying the process of managing 
// # returned data
// # Author: Austin Benitez
// ################################################################

// class SpeedyTester :

//     def run(self, db_interactor):
//         st = speedtest.Speedtest()
//         dl = st.download()
//         dl = dl/1000000
//         ul = st.upload()
//         ul = ul / 1000000
//         print("Download: {} mb/s".format(dl))
//         sys.stdout.flush()
//         print("Upload: {} mb/s".format(ul))
//         sys.stdout.flush()
//         db_interactor.insertSpeed(st)
    class SpeedTest
    {
        public Server testServer;
        public int retryCount;
        public int simultaneousUploads;
        public SpeedTestClient speedClient;

        public SpeedTest(Server server, int retryCount=2,int simultaneousUploads=2)
        {
            this.testServer = server;
            this.retryCount = retryCount;
            this.simultaneousUploads = simultaneousUploads;
            initializeClient();
            initializeClosestServer();
        }
      // <server url="http://speedtest.gonzaga.edu:8080/speedtest/upload.php" lat="47.6589" lon="-117.4250" name="Spokane, WA" country="United States" cc="US" sponsor="Gonzaga University" id="36007" host="speedtest.gonzaga.edu:8080"/>
      public void initializeClosestServer()
        {
            this.testServer.Country = "United States";
            this.testServer.Name = "Spokane, WA";
            this.testServer.Latitude = 47.6589;
            this.testServer.Longitude = -117.4250;
            this.testServer.Host = "speedtest.gonzaga.edu:8080";
            this.testServer.Id = 36007;
            this.testServer.Url = "http://speedtest.gonzaga.edu:8080/speedtest/upload.php";
            this.testServer.Sponsor = "Gonzaga University";
        }

        public void setServer(int serverID)
        {
            //TODO: impliment me
        }

        private SpeedTestClient initializeClient()
        {
            this.speedClient = new SpeedTestClient();
            return speedClient;
        }

        public double testDownload()
        {
            double result = this.speedClient.TestDownloadSpeed(this.testServer, this.simultaneousUploads, this.retryCount);
            return result;
        }

        public double testUpload()
        {
            double result = this.speedClient.TestUploadSpeed(this.testServer, this.simultaneousUploads, this.retryCount);
            return result;
        }

    }
}
