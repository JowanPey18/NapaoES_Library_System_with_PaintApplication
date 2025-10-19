using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapaoES.Database
{
    public class Firesharp
    {

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "1YcuY5qGHh1THWcLFor7nmdeAklzANVSMhrPtY0X",
            BasePath = "https://napaoeslibrarysystem-default-rtdb.firebaseio.com/"
        };

        public FirebaseResponse Response;
        public FirebaseClient Client;
        public void Connect()
        {
            Client = new FirebaseClient(config);
        }
    }
}
