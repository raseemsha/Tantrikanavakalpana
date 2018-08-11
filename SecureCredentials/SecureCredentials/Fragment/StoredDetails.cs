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
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Hardware.Fingerprints;
using Java.Security;
using Javax.Crypto;
using Android.Support.V7.App;
using Android;
using Android.Support.V4.App;
using Android.Security.Keystore;

namespace SecureCredentials
{
    public class StoredDetails : Android.Support.V4.App.Fragment, ExpandableListView.IOnGroupClickListener
    {
        private View view;
        private FloatingActionButton fabMain;
        private LinearLayout llfabUpdateData;
        private LinearLayout llfabAddData;
        private List<AddInfoModel> lstStoredData;
        bool isPlus = true;
        StoredInfoAdapter storedInfoAdapter;
        ExpandableListView explstTrainingTopics;
        private KeyStore keyStore;
        private Cipher cipher;
        private string KEY_NAME = "Ahsan";
        FingerprintManager.CryptoObject cryptoObject;
        KeyguardManager keyguardManager;
        FingerprintManager fingerprintManager;
        public StoredDetails()
        {
            lstStoredData = new List<AddInfoModel>();
           
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            keyguardManager= (KeyguardManager)Activity.GetSystemService(Context.KeyguardService);
            fingerprintManager= (FingerprintManager)Activity.GetSystemService(Context.FingerprintService);
            CheckFingerPrintStatus();
        }

        private void CheckFingerPrintStatus()
        {
            if (ContextCompat.CheckSelfPermission(Activity, Manifest.Permission.UseFingerprint)!= (int)Android.Content.PM.Permission.Granted)
            {
                return;
            }

            if (!fingerprintManager.IsHardwareDetected)
            {
                AlertBox.CreateOkAlertBox("Error", "FingerPrint authentication permission not enable",Activity,null);
            }

            else
            {
                if (!fingerprintManager.HasEnrolledFingerprints)
                {
                    AlertBox.CreateOkAlertBox("Error", "Register at least one fingerprint in Settings", Activity, null);
                }

                else
                {
                    if(!keyguardManager.IsKeyguardSecure)
                    {
                        AlertBox.CreateOkAlertBox("Error", "Lock screen security not enable in Settings", Activity, null);
                    }
                    else
                    {
                        GenKey();
                    }

                    if(CipherInit())
                    {
                        cryptoObject = new FingerprintManager.CryptoObject(cipher);
                        
                    }
                        
                }
                    
            }
                

        }

        private void GenKey()
        {
            keyStore = KeyStore.GetInstance("AndroidKeyStore");
            KeyGenerator keyGenerator = null;
            keyGenerator = KeyGenerator.GetInstance(KeyProperties.KeyAlgorithmAes, "AndroidKeyStore");
            keyStore.Load(null);
            keyGenerator.Init(new KeyGenParameterSpec.Builder(KEY_NAME, KeyStorePurpose.Encrypt | KeyStorePurpose.Decrypt)
                .SetBlockModes(KeyProperties.BlockModeCbc)
                .SetUserAuthenticationRequired(true)
                .SetEncryptionPaddings(KeyProperties
                .EncryptionPaddingPkcs7).Build());
            keyGenerator.GenerateKey();
        }


        private bool CipherInit()
        {
            try
            {
                cipher = Cipher.GetInstance(KeyProperties.KeyAlgorithmAes
                    + "/"
                    + KeyProperties.BlockModeCbc
                    + "/"
                    + KeyProperties.EncryptionPaddingPkcs7);
                keyStore.Load(null);
                IKey key = (IKey)keyStore.GetKey(KEY_NAME, null);
                cipher.Init(CipherMode.EncryptMode, key);
                return true;
            }
            catch 
            {
                return false;
            }
        }


        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            GetAndShowData();
            
        }

        private void GetAndShowData()
        {
            try
            {
                lstStoredData = FileOperations.ReadFromDevice<AddInfoModel>();
                if (lstStoredData != null && lstStoredData.Count > 0)
                {
                    BindData();
                }
            }

            catch
            {

            }
            
        }

        private void BindData()
        {
            
                storedInfoAdapter = new StoredInfoAdapter(Activity, lstStoredData);
                explstTrainingTopics.SetAdapter(storedInfoAdapter);
           

            

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            try
            {
                
                view = inflater.Inflate(Resource.Layout.stored_details, container, false);
                InititateControls();
            }

            catch(Exception ex)
            {

            }
             

            return view;
        }

        private void InititateControls()
        {
            fabMain = view.FindViewById<FloatingActionButton>(Resource.Id.fabMain);
            llfabUpdateData = view.FindViewById<LinearLayout>(Resource.Id.llfabUpdateData);
            llfabAddData = view.FindViewById<LinearLayout>(Resource.Id.llfabAddData);
            explstTrainingTopics= view.FindViewById<ExpandableListView>(Resource.Id.expLstItems);
            fabMain.Click += FabMain_Click;
            llfabUpdateData.Click += LlfabUpdateData_Click;
            llfabAddData.Click += LlfabAddData_Click;
            explstTrainingTopics.SetOnGroupClickListener(this);


        }

     

        private void LlfabAddData_Click(object sender, EventArgs e)
        {
            Android.Support.V4.App.FragmentTransaction transaction = Activity.SupportFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.container, new AddInfo(this), "AddInfo");
            transaction.AddToBackStack("AddInfo");
            transaction.Commit();
        }

        private void LlfabUpdateData_Click(object sender, EventArgs e)
        {
            
        }

        private void FabMain_Click(object sender, EventArgs e)
        {
            if(isPlus)
            {
                isPlus = false;
                fabMain.SetImageResource(Resource.Drawable.ic_cross);
                llfabUpdateData.Visibility = ViewStates.Visible;
                llfabAddData.Visibility = ViewStates.Visible;
            }
            else
            {
                isPlus = true;
                
                llfabUpdateData.Visibility = ViewStates.Gone;
                llfabAddData.Visibility = ViewStates.Gone;
                fabMain.SetImageResource(Resource.Drawable.ic_add);
            }
           
        }

        public bool OnGroupClick(ExpandableListView parent, View clickedView, int groupPosition, long id)
        {
            if(cryptoObject!=null)
            {
                AlertBox.AuthenticatePopUp(Activity);
                FingerPrintHandler handler = new FingerPrintHandler(Activity);
                handler.StartAuthentication(fingerprintManager, cryptoObject);
                return ! handler.isAuthenticationSucess;
            }
            
            return true;
        }
    }
}