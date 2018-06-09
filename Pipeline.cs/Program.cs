using System;
using System.Net;
using System.Threading;

namespace Pipeline
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<string> downloadSite = url => {
                var content = new WebClient().DownloadString(url);
                Console.WriteLine($"The size of the web site {url} is {content.Length}");
            };  

            var threadA = new Thread(() => downloadSite("http://www.nasdaq.com"));
            var threadB = new Thread(() => downloadSite("http://www.bbc.com"));

            threadA.Start();
            threadB.Start(); 
            threadA.Join();
            threadB.Join();

            ThreadPool.QueueUserWorkItem(o => downloadSite("http://www.nasdaq.com"));
            ThreadPool.QueueUserWorkItem(o => downloadSite("http://www.bbc.com")); //#C

            Console.ReadLine();
        }
    }
}
