
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace usingtcp
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener listner = new TcpListener(1404);
            listner.Start();
            while(true)
            {
                Console.WriteLine("Waiting for connection");
                TcpClient clint = listner.AcceptTcpClient();
                StreamReader sr = new StreamReader(clint.GetStream());
                StreamWriter sw = new StreamWriter(clint.GetStream());
                try
                {
                    string request = sr.ReadLine();
                    Console.WriteLine(request);
                    string[] tokens = request.Split(' ');
                    string page = tokens[1];
                    if(page=="/")
                    {
                        page = "default.html";
                    }
                    StreamReader file = new StreamReader("../" + page);
                    sw.WriteLine("HTTP/1.0 200 OK");
                    string data = file.ReadLine();
                    while(data!=null)
                    {
                        sw.WriteLine(data);
                        sw.Flush();
                        data = file.ReadLine();
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                clint.Close();
            }

            
        }
    }
}
