using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Support.V7.App;
using System;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Gms.Common.Apis;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Auth.Api;
using Java.Lang;
using Android.Gms.Drive;
using Android.Gms.Common;
using Android.Content;
using Android.Runtime;

namespace SecureWallet
{
    
    [Activity(Label = "Secure Wallet", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.AdjustPan | SoftInput.StateAlwaysHidden)]
    public class MainActivity : AppCompatActivity, GoogleApiClient.IOnConnectionFailedListener, IResultCallback, IDriveApiDriveContentsResult, GoogleApiClient.IConnectionCallbacks
    {
        private View view = null;
        private static SupportToolbar mToolbar;
        public static MainActivity ActivityMain;
        GoogleApiClient googleApiClient;
        static DriverIntegrationHelper driverIntegrationHelper;
        IDriveApiDriveContentsResult contentResults;
        const int REQUEST_CODE_RESOLUTION = 3;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            ActivityMain = this;
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            view = this.FindViewById(Resource.Id.container);
            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(mToolbar);
            AppBarManager.InitAppBar(this, SupportActionBar);

            BuildGoogleAPIClient();
            
            if (FileOperations.CreateTable<AddInfoModel>())
            {
                
                ReplaceFragment();
            }
            
        }

        private void BuildGoogleAPIClient()
        {

            GoogleSignInOptions googleSignInOption =new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn).RequestEmail().Build();

            googleApiClient = new GoogleApiClient.Builder(this).EnableAutoManage(this,this)
                  .AddApi(DriveClass.API, googleSignInOption)
                  .AddScope(DriveClass.ScopeAppfolder).AddConnectionCallbacks(this).AddOnConnectionFailedListener(OnConnectionFailed)
                  .Build();

        }

        protected override void OnStart()
        {
            base.OnStart();
            try
            {
                if (!googleApiClient.IsConnected)
                {
                    SignIn();
                }
            }

            catch (System.Exception ex)
            {

            }
        }


       

        void SignIn()
        {
            googleApiClient.Connect();

           

        }

     

        protected override void OnStop()
        {
            base.OnStop();
            googleApiClient.Disconnect();
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

        public void Exit()
        {
            FinishAffinity();
           



        }

        public void FingerPrintNotEnabledMessage(string message)
        {
            AlertBox.CreateOkAlertBox("Error", message, this, Exit);
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            if (!result.HasResolution)
            {

                GoogleApiAvailability.Instance.GetErrorDialog(this, result.ErrorCode, 0).Show();
                return;
            }
            try
            {
                result.StartResolutionForResult(this, REQUEST_CODE_RESOLUTION);
            }
            catch
            {

            }
            
        }

        public void OnConnected(Bundle connectionHint)
        {
            DriveClass.DriveApi.NewDriveContents(googleApiClient).SetResultCallback(this);
        }

        void IResultCallback.OnResult(Java.Lang.Object result)
        {
           contentResults =(result).JavaCast<IDriveApiDriveContentsResult>();
            if(contentResults.Status.IsSuccess)
            {
                driverIntegrationHelper = new DriverIntegrationHelper(googleApiClient, contentResults);
                driverIntegrationHelper.CreateFileInAppFolder();
            }
           
           // driverIntegrationHelper.ListFiles();
        }

        

        public void OnConnectionSuspended(int cause)
        {
            throw new NotImplementedException();
        }



        public IDriveContents DriveContents => throw new NotImplementedException();

        public Statuses Status => throw new NotImplementedException();
    }
}

