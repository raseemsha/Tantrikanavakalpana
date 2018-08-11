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
        public const string AutenticationFailedMessage = "Too many attempts.Autentication failed.";
    }
}