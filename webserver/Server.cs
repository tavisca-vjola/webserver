using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace webserver
{
    class Server
    {
        public static void Main(string[] args)
        {
            try
            {
                while (true)
                {

                    HttpListener web = new HttpListener();

                    web.Prefixes.Add("http://localhost:8080/");
                    Console.WriteLine("Listening..");
                    web.Start();

                    var context = web.GetContext();
                    // Console.WriteLine(context);

                    var response = context.Response;

                    string filename = context.Request.RawUrl;
                    filename = filename.Remove(0, 1);


                    byte[] bytearray = StreamFile(filename);
                    var buffer = bytearray;
                    response.ContentLength64 = buffer.Length;
                    var output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    //Console.WriteLine(output);
                    //output.Close();

                    web.Stop();
                    //Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
        }
        public  static byte[] StreamFile(string filename)
        {
           
            byte[] ImageData = System.IO.File.ReadAllBytes(filename);
           
            return ImageData;
        }

    }
}

  