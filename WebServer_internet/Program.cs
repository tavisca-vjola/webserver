using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebServer_internet
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
        }
   
        private static System.Threading.AutoResetEvent listenForNextRequest = new System.Threading.AutoResetEvent(false);

        protected Program()
        {
            httpListener = new HttpListener();
        }

        private HttpListener httpListener;

        public string Prefix { get; set; }
        public void Start()
        {
            if (String.IsNullOrEmpty(Prefix))
                throw new InvalidOperationException("Specify prefix");
            httpListener.Prefixes.Clear();
            httpListener.Prefixes.Add(Prefix);
            httpListener.Start();
            System.Threading.ThreadPool.QueueUserWorkItem(Listen);
        }

        internal void Stop()
        {
            httpListener.Stop();
            IsRunning = false;
        }

        public bool IsRunning { get; private set; }

        // Loop here to begin processing of new requests.
        private void Listen(object state)
        {
            while (httpListener.IsListening)
            {
                AsyncCallback ListenerCallback = null;
                httpListener.BeginGetContext(new AsyncCallback(ListenerCallback), httpListener);
                listenForNextRequest.WaitOne();
            }
        }
    }
}
