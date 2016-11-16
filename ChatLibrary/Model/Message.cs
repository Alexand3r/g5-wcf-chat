using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChatLibrary
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string  username { get; set; }
        [DataMember]
        public string message { get; set; }


        public Message(int id, string usr,string msg)
        {
            this.id = id;
            this.username = usr;
            this.message = msg;
        }
    }
}
