using System.Threading;
using System.Threading.Tasks;
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

            
            if (counter==3)
            {
                SensorMsgChange(Constants.AutenticationFailedMessage);
                Thread.Sleep(1000);
                autDialog.Dismiss();
            }

            else
            {
                SensorMsgChange(Constants.AutenticationRetryMessage);
                
            }
        }
        public override void OnAuthenticationSucceeded(FingerprintManager.AuthenticationResult result)
        {
            autDialog.Dismiss();
            autDialog.AutenticationSucess();
        }

        private void SensorMsgChange(string msg)
        {
            autDialog.txtAutenticationMsg.Text = msg;
            autDialog.txtAutenticationMsg.SetTextColor(Android.Graphics.Color.Red);
           
           
        }
    }
}