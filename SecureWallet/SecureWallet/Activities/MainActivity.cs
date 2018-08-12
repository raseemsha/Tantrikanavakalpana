using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Support.V7.App;
using System;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;

namespace SecureWallet
{
    
    [Activity(Label = "Secure Wallet", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.AdjustPan | SoftInput.StateAlwaysHidden)]
    public class MainActivity : AppCompatActivity
    {
        private View view = null;
        private static SupportToolbar mToolbar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            view = this.FindViewById(Resource.Id.container);
            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(mToolbar);
            AppBarManager.InitAppBar(this, SupportActionBar);
            
            
            if (FileOperations.CreateTable<AddInfoModel>())
            {
                ReplaceFragment();
            }
            
        }

        public void ReplaceFragment()
        {
            try
            {
                
                Android.Support.V4.App.FragmentTransaction transaction = SupportFragmentManager.BeginTransaction();
                transaction.Replace(Resource.Id.container, new StoredDetails(), Constants.StoredDetailsFragment);
                transaction.AddToBackStack(Constants.StoredDetailsFragment);
                transaction.Commit();
            }
            catch 
            {
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        { 
             OnBackPressed();
               
            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            if (this.SupportFragmentManager.BackStackEntryCount > 0)
            {
                if (Utility.IsFragmentActive(this, Constants.StoredDetailsFragment))
                {
                    ApplicationExistConfirmation();
                }
                else
                {
                    this.SupportFragmentManager.PopBackStackImmediate();
                }
            }

            else
            {
                base.OnBackPressed();
            }
            }

        private void ApplicationExistConfirmation()
        {
            AlertBox.CreateYesNoAlertBox("Warning", Constants.AppExit, this, Exit, null,Resource.String.yes, Resource.String.no);
        }

        private void Exit()
        {
            Finish();
        }
    }
}

