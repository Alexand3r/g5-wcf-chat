using ChatLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ChatLibrary
{
   
    [ServiceContract]
    public interface IChatServer
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/login",
             ResponseFormat = WebMessageFormat.Json)]
        bool login(User u);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/createuser",
             ResponseFormat = WebMessageFormat.Json)]
        bool createuser(User u);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/sendmessage",
             ResponseFormat = WebMessageFormat.Json)]
        void sendmessage(Message m);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/getmessages",
             BodyStyle = WebMessageBodyStyle.Bare,
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        List<Message> getmessages();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/getnewmessages?id={id}",
             BodyStyle = WebMessageBodyStyle.Bare,
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        List<Message> getNewMessages(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/logout",
             ResponseFormat = WebMessageFormat.Json)]
        bool logout(User u);

    }
}
