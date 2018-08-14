using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SecureWallet
{
    public static class Constants
    {
        public const string AutenticationRetryMessage = "Finger print was not recognised.Please try again.";
        public const string TouchSensor = "Touch sensor.";
        public const string AutenticationFailedMessage = "Too many attempts.Autentication failed.Try after {0} seconds";
        public const string Wallet = "Wallet";
        public const string AddInfo = "Add Information";
        public const string AppExit = "Are you sure you want to exit the application ?";


        public const string StoreDetailsHeader = "Secured Informations :{0}";
        public const string StoredDetailsFragment = "StoredDetailsFragment";
        public const string AddInfoFragment = "AddInfoFragment";
    }
}