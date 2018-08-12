
using Android.App;
using Android.Graphics.Drawables;

using Android.Graphics;
using Android;

namespace SecureWallet
{
    public class AppBarManager
    {
      
        private static Drawable navIconBack;
        
        private static Android.Support.V7.App.ActionBar mActionBar = null;

        /// <summary>
        /// Initialise the app bar 
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="mActionbar"></param>
        public static void InitAppBar(Activity activity, Android.Support.V7.App.ActionBar mActionbarToBeSet)
        {
            mActionBar = mActionbarToBeSet;

            //Initialise the Drawer Icons
           
            navIconBack = activity.Resources.GetDrawable(Resource.Drawable.ic_back);
            navIconBack.SetColorFilter(activity.Resources.GetColor(Resource.Color.black), PorterDuff.Mode.SrcAtop);

            //Set the drawer to show hamburger icon
            mActionBar.SetDisplayHomeAsUpEnabled(true);
            mActionBar.SetDisplayShowTitleEnabled(true);
            mActionBar.SetHomeButtonEnabled(true);
            mActionBar.SetDisplayShowHomeEnabled(true);

        }

        
        public static void HideIcons()
        {
            mActionBar.SetDisplayHomeAsUpEnabled(false);
        }
        /// <summary>
        /// Show the back button in the App bar
        /// </summary>
        /// <param name="mActionbar">Action bar which contains the icon</param>
        public static void ShowBackButtonInAppBar()
        {
            mActionBar.SetDisplayHomeAsUpEnabled(true);
            mActionBar.SetHomeAsUpIndicator(navIconBack);
            
        }

      

        /// <summary>
        /// Sets the title to the action bar
        /// </summary>
        /// <param name="mActionbar">Action Bar</param>
        /// <param name="title">Title to be set</param>
        public static void SetTitle(string title)
        {
            mActionBar.Title = title;
        }

        public static void SetTitle(int titleId)
        {
            mActionBar.SetTitle(titleId);
        }

        public static void RefreshTitle()
        {
            mActionBar.SetDisplayHomeAsUpEnabled(false);
            mActionBar.SetDisplayHomeAsUpEnabled(true);
        }
    }
}