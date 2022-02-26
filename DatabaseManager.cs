using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pingeratorv3
{
    class DatabaseManager
    {
        public SQLiteConnection myConnection;
        public DatabaseManager()
        {
            InitializeDatabase();
        }
        public SQLiteConnection InitializeDatabase()
        {
            SQLiteConnection localConnection;
            
         if(!File.Exists("LocalDatabase.sqlite"))
         {
            this.initializeNewDatabase();
            localConnection = new SQLiteConnection("Data Source=LocalDatabase.sqlite;Version=3");
            this.myConnection = localConnection;
            this.buildDatabase();
         }
         else{
            localConnection = new SQLiteConnection("Data Source=LocalDatabase.sqlite;Version=3");
            this.myConnection = localConnection;
         }
         return localConnection;
        }
        public void initializeNewDatabase()
        {
            SQLiteConnection.CreateFile("LocalDatabase.sqlite");
        }
        public string PingToString(long pingTime)
        {
            string pingstring = pingTime.ToString();
            return pingstring;
        }

        public int getIpId(string host)
        {
            //TODO: do this
            return 1;
        }
        /* def insertPing(self, ping_response, host):
        # get current time
        curr_date_time = datetime.now()
        if "Request timed out" not in str(ping_response):
            ping = self.responseToRawPing(ping_response)
        else:
            ping = -1
        ip_id = self.getIPID(host)
        sql = "INSERT INTO ping VALUES ('{}', {}, {})".format(curr_date_time, ping, ip_id[0])
        self.DBO.execute(sql)*/
        public void testInsert()
        {
            string sql = "INSERT INTO `ping` (`datetime_tested`, `ping_value`, `ipv4_id`) VALUES (`2021-08-02 21:44:11`,24.42 ,1 )";
            this.myConnection.Open();
            SQLiteCommand testInsertCMD = new SQLiteCommand(sql, this.myConnection);
            testInsertCMD.ExecuteScalar();
        }
        public void buildDatabase()
        {
            myConnection.Open();
            var createIpLookup = "CREATE table ipLookup(id INTEGER PRIMARY KEY AUTOINCREMENT,time_created TIMESTAMP NOT NULL,ipv4 VARCHAR(15), domain_name VARCHAR(80)); ";
            var createSpeed = "CREATE table speed (speed_id INTEGER PRIMARY KEY AUTOINCREMENT,datetime_tested TIMESTAMP NOT NULL,downspeed_value DOUBLE NULL,upspeed_value DOUBLE NULL); ";
            var createPing = "CREATE table ping (ping_id INTEGER PRIMARY KEY AUTOINCREMENT,datetime_tested TIMESTAMP NOT NULL,ping_value FLOAT NULL,ipv4_id INTEGER NOT NULL,CONSTRAINT ipv4_id FOREIGN KEY(ipv4_id) REFERENCES ipLookup(id)); ";
            var insertGoogle = "INSERT INTO iplookup (time_created, ipv4, domain_name) VALUES('2021-25-02 21:44:11', '8.8.8.8', 'google.com');";
            var insertCloudflare = "INSERT INTO iplookup (time_created, ipv4, domain_name) VALUES('2021-25-02 21:44:11', '1.1.1.1', 'cloudflare.com');";
            var insertOpenDNS = "INSERT INTO iplookup (time_created, ipv4, domain_name) VALUES('2021-25-02 21:44:11', '208.67.222.222', 'openDNS.com');";
            List<string> inserts = new List<string>();
            inserts.Add(createIpLookup);
            inserts.Add(createSpeed);
            inserts.Add(createPing);
            inserts.Add(insertCloudflare);
            inserts.Add(insertGoogle);
            inserts.Add(insertOpenDNS);
            
            SQLiteCommand command;
            foreach (var insert in inserts)
            {
                command = new SQLiteCommand(insert, this.myConnection);
            }
            myConnection.Close();
        }
        public void InsertPing(long pingTime, string host)
        {
            var conn = this.myConnection;
            string ping = this.PingToString(pingTime); 
            string unixTimeStamp = Convert.ToString((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            int ipId = getIpId(host);
            string sql = $"INSERT INTO ping (datetime_tested, ping_value, ipv4_id) VALUES ('{unixTimeStamp}',{ping} ,{ipId} )";
            Console.WriteLine(sql);
            conn.Open();
            SQLiteCommand insertCommand = new SQLiteCommand(sql, conn);
            insertCommand.ExecuteNonQuery();
            conn.Close();
      }
      /*def insertSpeed(self, st_obj):
      # get current time
      up_speed = st_obj.upload()
      down_speed = st_obj.download()
      # ping = st_obj.ping()
      curr_date_time = datetime.now()
      sql = "INSERT INTO speed VALUES ('{}',{},{});".format(curr_date_time, up_speed, down_speed)
      self.DBO.execute(sql)*/
      //TODO: change speed object datatype
      //id SERIAL NOT NULL,
    //   datetime_tested TIMESTAMP NOT NULL,
    //   downspeed_value DOUBLE NULL,
    //   upspeed_value DOUBLE NULL
        public void InsertSpeed(double upload, double download)
        {
            var conn = this.myConnection;
            conn.Open();
            string unixTimeStamp = Convert.ToString((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            string sql = $"INSERT INTO speed (datetime_tested, downspeed_value, upspeed_value) VALUES ('{unixTimeStamp}', {upload}, {download})";
            SQLiteCommand insertCommand = new SQLiteCommand(sql, conn);
            insertCommand.ExecuteNonQuery();
            conn.Close();
        }

    }
}