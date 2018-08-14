
using System;
using System.Timers;
using Android;
using Android.Content;
using Android.Hardware.Fingerprints;
using Android.OS;
using Android.Support.V4.Content;

namespace SecureWallet
{
    public class FingerPrintHandler : FingerprintManager.AuthenticationCallback
    {
        private Context context;
        private AutenticationDialog autDialog;
        int counter;
        private Timer messageChangeTimer;
        public FingerPrintHandler(Context context, AutenticationDialog autDialog)
        {
            this.context = context;
            this.autDialog = autDialog;
        }

        internal void StartAuthentication(FingerprintManager fingerprintManager, FingerprintManager.CryptoObject cryptoObject)
        {
            CancellationSignal cancellationSignal = new CancellationSignal();
            if (ContextCompat.CheckSelfPermission(context, Manifest.Permission.UseFingerprint)!= (int)Android.Content.PM.Permission.Granted)
            {
                return;
            }
                
            fingerprintManager.Authenticate(cryptoObject, cancellationSignal, 0, this, null);
        }

        public override void OnAuthenticationFailed()
        {
            counter++;
           
            StopTimer();
            

            
            if (counter==5)
            {
                StoredDetails.timeOnUnsucessfulAttempts = DateTime.Now;
                SensorMsgChange(string.Format(Constants.AutenticationFailedMessage,5));

                StartTimer(Constants.TouchSensor,true);
            }

            else
            {
                SensorMsgChange(Constants.AutenticationRetryMessage);
                StartTimer(Constants.TouchSensor);
            }
        }
        public override void OnAuthenticationSucceeded(FingerprintManager.AuthenticationResult result)
        {
           
            StopTimer();
            autDialog.Dismiss();
            autDialog.AutenticationSucess();
        }

        private void SensorMsgChange(string msg)
        {
            autDialog.txtAutenticationMsg.Text = msg;
            autDialog.txtAutenticationMsg.SetTextColor(Android.Graphics.Color.Red);
           
           
        }

        public void StartTimer(string msg,bool dismissRequired=false)
        {

            messageChangeTimer = new Timer();
            messageChangeTimer.Interval = 2* 1000;//Time should be in milliseconds
            if(dismissRequired)
            {
                messageChangeTimer.Elapsed += delegate { autDialog.Dismiss(); }; 
            }
            else
            {
                messageChangeTimer.Elapsed += delegate { SensorMsgChange(msg); autDialog.txtAutenticationMsg.SetTextColor(Android.Graphics.Color.Black); };
            }
           
            messageChangeTimer.Enabled = true;
        }

        public  void StopTimer()
        {

            if (messageChangeTimer != null)
            {
                messageChangeTimer.Stop();
                messageChangeTimer.Enabled = false;
            }
        }
    }
}