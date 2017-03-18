using System;
using System.ServiceModel;

namespace SampleWcfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(MyService), new Uri("net.tcp://localhost:8523/HelloWorldService")))
            {
                host.Open();
                Console.WriteLine($"Press <Enter> to stop the service");
                Console.ReadLine();

                host.Close();
            }
        }
    }
}
