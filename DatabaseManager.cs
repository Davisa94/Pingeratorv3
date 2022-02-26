using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pingeratorv3
{
    class DatabaseManager
    {
        public static SQLiteConnection InitializeDatabase()
        {
            SQLiteConnection localConnection = new SQLiteConnection("Data Source=LocalDatabase.db;Version=3");
            return localConnection;
        }
        
        public string PingToString(long pingTime)
        {
            string pingstring = pingTime.ToString();
            return pingstring;
        }

        public int getIpId(string host)
        {
            //TODO: do this
            return 0;
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
        public void InsertPing(long pingTime, string host, SQLiteConnection conn)
        {
            string ping = this.PingToString(pingTime); 
            string unixTimeStamp = Convert.ToString((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            int ipId = getIpId(host);
            string sql = $"INSERT INTO ping VALUES ('{unixTimeStamp}',{ping} ,{ipId} )";
            SQLiteCommand insertCommand = new SQLiteCommand(sql, conn);
            insertCommand.ExecuteNonQuery();
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
        public static void InsertSpeed(double upload, double download, SQLiteConnection conn)
        {
            string unixTimeStamp = Convert.ToString((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            string sql = $"INSERT INTO speed VALUES ('{unixTimeStamp}', {upload}, {download})";
            SQLiteCommand insertCommand = new SQLiteCommand(sql, conn);
            insertCommand.ExecuteNonQuery();
        }

    }
}