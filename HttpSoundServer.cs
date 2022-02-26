using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pingeratorv3
{
    class HttpSoundServer
    {
        public static bool HandleSongRequest(string request, string SoundsPath)
        {
            //parse request string
            string songName = SongFromRequestString(request);
            //Concat sound to the path
            string songPath = FileActions.ConcatFilenameToPath(songName, SoundsPath);
            //play the sound
            Console.WriteLine($"Playing Song: {songPath}");
            //bool played = PlayLocalSound.PlayAll(songPath);
            return true;
        }
        public static bool HandleDataRequest(string request)
        {
            return true;
        }
        public static string HandleSoundNamesRequest(string request, string SoundsPath)
        {
            // //Get the sound names
            // //string[] soundNamesList = FileActions.GetAllSoundNames(SoundsPath);
            // //Strip the soundspath from each:
            // //split on \
            // //set the index of soundNamesList to the last index of the split string
            // for (int i = 0; i < soundNamesList.Length; i++)
            // {
            //     var tempSplit = soundNamesList[i].Split("\\");
            //     soundNamesList[i] = tempSplit[^1];
            //     Console.WriteLine($"\t\t\t{soundNamesList[i]}");
            // }
            // /*            foreach (string soundName in soundNamesList)
            //             {
            //                 soundName.Replace(SoundsPath, "");
            //                 Console.WriteLine($"\t\t\t{soundName}");
            //             }
            //             foreach (string soundName in soundNamesList)
            //             {
            //                 Console.WriteLine($"\t\t\t{soundName}");
            //             }*/

            // Console.WriteLine(SoundsPath);
            // string soundNames = FileActions.SoundsToReturnString(soundNamesList);
            // //return the sound names 
            // return soundNames;
            return "no u";
            
        }
        public static string SongFromRequestString(string request)
        {

            //strip any whitespace
            request = FileActions.ReplaceWhitespace(request, "");
            //there are two forms of request:
            //try to split the request on the :
            string[] temp = request.Split(":");
            //if the result is an array of size 1 try the next method:
            if (temp.Length >= 1)
            {
                temp = request.Split("=");
            }
            //return the second in the array
            return temp[1];

        }
        public static void StartServer(int port, string address)
        {
            
        }
        public static void Test(int port, string address)
        {

        }
        public static HttpListenerContext AwaitRequest(HttpListener listener)
        {
            Console.WriteLine("Awaiting Request");
            HttpListenerContext context = listener.GetContext();
            Console.WriteLine("Got Request");
            return context;
        }
        public static void HandleRequest(HttpListenerContext context, string SoundsPath)
        {
            //variable declarations:
            byte[] buffer;
            HttpListenerResponse response = context.Response;
            Console.WriteLine("Handling Request");
            HttpListenerRequest request = context.Request;
            // Obtain a response object.
            string text;
            using (var reader = new StreamReader(request.InputStream,
                                                 request.ContentEncoding))
            {
                text = reader.ReadToEnd();
            }
            //Check which request type it is:
            if(text.Contains("All"))
            {
                Console.WriteLine(text);
                Console.WriteLine("Fetching sound names....");
                //We want to get and return all sounds to the requester
                string soundNames = HandleSoundNamesRequest(text, SoundsPath);
                buffer = System.Text.Encoding.UTF8.GetBytes(soundNames);
            }
            else
            {
                Console.WriteLine(text);
                Console.WriteLine("Prepping Sound");
                bool played = HandleSongRequest(text, SoundsPath);
                // Construct a response.
                string responseString = "";
                if (played)
                {
                    responseString = $"<HTML><BODY> Played {text}</BODY></HTML>";

                }
                else
                {
                    responseString = $"{text} COULD NOT BE PLAYED";
                }
                buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            }
           


            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        }
        public static void CheckSupport()
        {
            //If we arent supported Give info on why not
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
        }
        public static void VerifyPrefixes(string[] prefixes)
        {
            // for example "http://contoso.com:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");
        }
        
        public static HttpListener AddPrefixes(HttpListener listener, string[] prefixes)
        {
            foreach(string prefix in prefixes)
            {
                listener.Prefixes.Add(prefix);
            }
            return listener;
        }
        public static void SimpleListenerExample(string[] prefixes, string SoundsPath)
        {
            //Check for support
            CheckSupport();
            // Verify URI prefixes
            VerifyPrefixes(prefixes);

            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes.
            listener = AddPrefixes(listener, prefixes);
            //start the listener
            listener.Start();
            Console.WriteLine("Listening...");
            //main loop
            try
            {
                while (true)
                {
                    // Note: The GetContext method blocks while waiting for a request.
                    HttpListenerContext context = AwaitRequest(listener);
                    HandleRequest(context, SoundsPath);
                }
            }
            catch
            {
                listener.Stop();
            }
            
        }

    }
}
