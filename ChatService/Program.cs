using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatLibrary;
using System.ServiceModel.Web;
using System.ServiceModel;
using System.ServiceModel.Description;
using log4net;
namespace ChatService
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static Engine engine = Engine.Instance;
        static void Main(string[] args)
        {
            log.Info("Starting Server");
            startServer();
            Console.ReadKey();
        }
        private static void startServer()
        {
            ServiceHost host = null;
            try
            {
                Uri address = new Uri("http://192.168.0.101:9988/Chat");
                host = new WebServiceHost(typeof(ChatServer), address);
                host.AddServiceEndpoint(typeof(IChatServer), new WebHttpBinding(), "");
                ServiceDebugBehavior debug = host.Description.Behaviors.Find<ServiceDebugBehavior>();
                debug.HttpHelpPageEnabled = false;
                host.Open();
                log.Info("Server has started on " + address);

            }
            catch(Exception ex)
            {
                host = null;
                log.Error(ex.Message);
            }


        }
    }
}
