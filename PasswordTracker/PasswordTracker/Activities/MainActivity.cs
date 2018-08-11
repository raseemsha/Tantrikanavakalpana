using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Support.V7.App;

namespace PasswordTracker
{
    
    [Activity(Label = "PasswordTracker", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.AdjustPan | SoftInput.StateAlwaysHidden)]
    public class MainActivity : AppCompatActivity
    {
        private View view = null;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            view = this.FindViewById(Resource.Id.container);

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
                transaction.Replace(Resource.Id.container, new StoredDetails(), "Details");
                transaction.AddToBackStack("Details");
                transaction.Commit();
            }
            catch 
            {
            }
        }
    }
}

