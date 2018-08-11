using Android;
using Android.Content;
using Android.Hardware.Fingerprints;
using Android.OS;
using Android.Support.V4.Content;

namespace PasswordTracker
{
    public class FingerPrintHandler : FingerprintManager.AuthenticationCallback
    {
        private Context context;
        internal bool isAuthenticationSucess;
        public FingerPrintHandler(Context context)
        {
            this.context = context;
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
            isAuthenticationSucess = false;
        }
        public override void OnAuthenticationSucceeded(FingerprintManager.AuthenticationResult result)
        {
            isAuthenticationSucess = true;
        }
    }
}