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
    public class AlertBox
    {
       

        public static void CreateYesNoAlertBox(string titleOfBox, string message, Context context, Action yesFuntion, Action cancelFunction, int btn1Text, int btn2Text)
        {
            try
            {
                if (context != null)
                {
                    Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(context);
                    alert.SetTitle(titleOfBox);
                    alert.SetMessage(message);
                    if (yesFuntion == null)
                    {
                        alert.SetPositiveButton(btn1Text, (EventHandler<DialogClickEventArgs>)null);
                    }
                    else
                    {
                        alert.SetPositiveButton(btn1Text, delegate { yesFuntion(); });
                    }

                    if (cancelFunction == null)
                    {
                        alert.SetNegativeButton(btn2Text, (EventHandler<DialogClickEventArgs>)null);
                    }
                    else
                    {
                        alert.SetNegativeButton(btn2Text, delegate { cancelFunction(); });
                    }
                    alert.SetCancelable(true);
                    
                    if (!((Android.App.Activity)context).IsFinishing)
                    {
                        alert.Show();
                    }
                }
            }
            catch 
            {
                
            }
        }
        public static void CreateOkAlertBox(string titleOfBox, string message, Context context, Action okFunction)
        {
            try
            {
                if (context != null)
                {
                    Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(context);
                    alert.SetTitle(titleOfBox);
                    alert.SetMessage(message);
                    if (okFunction == null)
                    {
                        alert.SetPositiveButton("Ok", (EventHandler<DialogClickEventArgs>)null);
                    }
                    else
                    {
                        alert.SetPositiveButton("Ok", delegate { okFunction(); });
                    }
                    alert.SetCancelable(true);
                    
                    if (!((Android.App.Activity)context).IsFinishing)
                    {
                        alert.Show();
                    }
                }
            }
            catch
            {
            }
        }

        public static void AuthenticatePopUp(Context context)
        {
            try
            {
                if (context != null)
                {
                    
                    AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(context);
                    LayoutInflater factory = LayoutInflater.From(context);
                    View view = factory.Inflate(Resource.Layout.authenticate, null);
                    
                    alert.SetView(view);

                    alert.SetPositiveButton("CANCEL", (EventHandler<DialogClickEventArgs>)null);
                    
                   
                    alert.SetCancelable(false);
                    if (!((Android.App.Activity)context).IsFinishing)
                    {
                        alert.Show();
                        
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}