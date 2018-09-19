using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

namespace SecureWallet
{
    public class Utility 
    {
        public static bool IsFragmentActive(Android.Support.V4.App.FragmentActivity activity, string fragmentTag)
        {
            if (activity == null)
            {
                return false;
            }

            if (activity.SupportFragmentManager.FindFragmentByTag(fragmentTag) != null)
            {
                if (activity.SupportFragmentManager.FindFragmentByTag(fragmentTag).IsVisible)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static void HideKeyboard(Context context, View view)
        {
            try
            {
                if (view != null && context != null)
                {
                    // Hides the key board
                    var imm = (InputMethodManager)context.GetSystemService(Android.Content.Context.InputMethodService);
                   
                    var result = imm.HideSoftInputFromWindow(view.WindowToken, 0);
                  
                }
            }
            catch
            {
            }
        }

    }
}