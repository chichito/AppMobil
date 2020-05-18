using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMobil.Services.Helpers
{
    public class Constants
    {
        public const string StoreWebApiURL = "http://10.134.7.106:5000/api/";
        //public const string StoreWebApiURL = "http://192.168.1.38:5000/api/";

        #region SignalR Constants
        // URL
        public const string SignalRURL = "http://10.134.7.106:5000/StoreHub";
        //public const string SignalRURL = "http://192.168.1.38:5000/StoreHub";

        // Groups
        public const string Usuario = "Usuario";
        public const string Employee = "Employee";
        public const string Store = "Store";

        // Server Methods 
        public static readonly string ServerSubscribeMethod = "SubscribeToGroup";
        public static readonly string ServerUnsuscribeMethod = "UnsubscribeFromGroup";
        public static readonly string ServerMethod = "PostNewInfo";

        // Client Methods
        public static readonly string ClientMethod = "NotifyNewInfo";
        public static readonly string ClientSubscribeMethod = "Subscribe";
        public static readonly string ClientUnsuscribeMethod = "Unsuscribe";

        #endregion
    }
}
