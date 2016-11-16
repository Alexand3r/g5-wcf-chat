using ChatLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChatLibrary
{


    public class ChatServer : IChatServer
    {
        private Engine engine = Engine.Instance;

        public void DoWork()
        {
        }
        public bool login(User u)
        {
            return engine.ConnectUser(u);
        }
        public bool createuser(User u)
        {
            return engine.CreateUser(u);
        }
        public void sendmessage(Message m)
        {
            engine.AddMessage(m.username, m.message);

        }
        public List<Message> getmessages()
        {
            return engine.GetAllMessages();
        }

        public List<Message> getNewMessages(int id)
        {
            return engine.GetNewMessages(id);
        }
        public bool logout(User u)
        {
            return engine.logout(u);
        }
        public void usersList()
        { }

    }
}
