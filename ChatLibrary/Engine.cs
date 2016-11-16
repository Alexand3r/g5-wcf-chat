using ChatLibrary.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLibrary
{
    public class Engine
    {

      

        private List<User> connectedUsers = new List<User>();
        public List<User> ConnectedUsers
        {
            get
            { return connectedUsers; }
        }
        private List<Message> messages = new List<Message>();
        public List<Message> Messages
        {
            get
            { return messages; }
        }


        #region Logger
        private static readonly ILog log = LogManager.GetLogger
      (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region SingleTon
        private static Engine instance;
        public static Engine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Engine();
                }
                return instance;
            }
        }
        #endregion

        #region Create New User

        public bool CreateUser(User usr)
        {
            bool success = false;
            bool available = checkAvailableUsername(usr.username);
            if (available)
            {
                using (userDBEntities usDB = new userDBEntities())
                {
                    User u = new User();
                    u.username = usr.username;
                    u.password = PasswordHash.CreateHash(usr.password);
                    try
                    {
                        usDB.Users.Add(u);
                        usDB.SaveChanges();
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }

                }

            }
            else
            { log.Error("Username already exists"); }


            return success;
        }
        private bool checkAvailableUsername(string username)
        {
            using (userDBEntities usDB = new userDBEntities())
            {
                try
                {
                    //check for availability
                    var usr = usDB.Users.Where(u => u.username == username).First();
                }
                catch
                {
                    log.Debug("Username available");
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Login User
        private User Login(string username, string password)
        {
            User user = null;

            using (userDBEntities usDB = new userDBEntities())
            {   //Find user & validate password
                try
                {
                    var usr = usDB.Users.Where(u => u.username == username).First();
                    bool pass = PasswordHash.ValidatePassword(password, usr.password);
                    if (pass)
                    {
                        user = (User)usr;
                    }
                }
                catch
                {
                    log.Debug("DB is down, Username/Password wrong or something else went terribly wrong");

                }

            }
            return user;
        }
        public bool ConnectUser(User usrr)
        {
            bool success = false;
            User usr = Login(usrr.username, usrr.password);
            if (usr != null)
            {
                logout(usrr);//TODO: This needs to be re done to accomodate user key
                int index = connectedUsers.FindIndex(u => u.username == usrr.username);
                if (index < 0)

                {
                    ConnectedUsers.Add(usr);
                    try
                    {
                        messages.Add(new Message(idGenMsg(), usr.username, "User connected!"));
                        success = true;
                    }
                    catch (Exception ex)
                    {

                        log.Debug(ex);
                    }
                }
            }

            return success;
        }
        
        public void AddMessage(string username,string msj)
        {
            
                int index = connectedUsers.FindIndex(u => u.username == username);
             
                if (index>=0)
                    messages.Add(new Message(idGenMsg(), username, msj));
            

        }
        public List<Message> GetAllMessages()
        {
            return messages;
        }
        //Use ID as reference to order sent messages
        public List<Message> GetNewMessages(int id)
        {
            List<Message> newMessages = new List<Message>();
            foreach (Message m in messages)
            {if (m.id > id)
                {
                    newMessages.Add(m);
                }
            }
            return newMessages;
        }
        #endregion

        public bool logout(User usrr) {

            try
            {
                User usr = connectedUsers.Find(u => u.username == usrr.username);
                if (usr != null)
                {
                    ConnectedUsers.Remove(usr);
                    messages.Add(new Message(idGenMsg(), usrr.username, "User disconnected"));
                    return true;
                }
               else
                { return false; }
            }
            catch {
                return false;
            }
            
        }
        private int idGenMsg()
        {
            return messages.Count;
        }
    }
}
