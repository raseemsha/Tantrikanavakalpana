using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Hardware.Fingerprints;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace SecureWallet
{
    public class AutenticationDialog : Android.Support.V4.App.DialogFragment
    {
        private View view;
        internal TextView txtAutenticationMsg;
        private TextView txtCancel;
        FingerPrintHandler handler;
        public event EventHandler<DialogEventArgs> EventTrigger;

        public AutenticationDialog(FingerprintManager fingerprintManager,FingerprintManager.CryptoObject cryptoObject,Context context)
        {
            
            handler = new FingerPrintHandler(context,this);
            handler.StartAuthentication(fingerprintManager, cryptoObject);

        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Cancelable = false;

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
           view= inflater.Inflate(Resource.Layout.authenticate, container, false);
            InitiateControls();
            return view;
        }

        private void InitiateControls()
        {
            txtAutenticationMsg = view.FindViewById<TextView>(Resource.Id.txtAutenticationMsg);
            txtCancel = view.FindViewById<TextView>(Resource.Id.txtCancel);
            txtCancel.Click += TxtCancel_Click;
        }

        private void TxtCancel_Click(object sender, EventArgs e)
        {
            Dismiss();
        }

        /// <summary>
        /// 
        /// </summary>
        public void AutenticationSucess()
        {
            EventTrigger?.Invoke(this, new DialogEventArgs());
        }

       
    }

    public class DialogEventArgs
    {
        private string m_returnValue;
        public string ReturnValue
        {
            get
            {
                return m_returnValue;
            }

            set
            {
                m_returnValue = value;
            }
        }
    }
}