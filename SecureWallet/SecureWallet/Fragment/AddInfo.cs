using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Linq;

namespace SecureWallet
{
    public class AddInfo : Android.Support.V4.App.Fragment
    {
        private View view;
        private Button btnAdditionalInfo;
        private EditText edtTitle;
        private EditText edtUserId;
        private EditText edtPassword;
        private EditText edtAdditionalInfo;
        private StoredDetails storedDetails;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
           
            
        }
        public AddInfo(StoredDetails storedDetails)
        {
            this.storedDetails = storedDetails;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
             view=  inflater.Inflate(Resource.Layout.add_info, container, false);
            InititateControls();
            return view;
            
        }

        private void InititateControls()
        {
            edtTitle = view.FindViewById<EditText>(Resource.Id.edtTitle);
            edtUserId = view.FindViewById<EditText>(Resource.Id.edtUserId);
            edtPassword = view.FindViewById<EditText>(Resource.Id.edtPassword);
            edtAdditionalInfo = view.FindViewById<EditText>(Resource.Id.edtAdditionalInfo);
            btnAdditionalInfo = view.FindViewById<Button>(Resource.Id.btnAdditionalInfo);
            btnAdditionalInfo.Click += BtnAdditionalInfo_Click;
        }

        private void BtnAdditionalInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(edtTitle.Text))
                {
                    AlertBox.CreateOkAlertBox("Warning", "Please enter title", Activity, null);
                    return;
                }

                var listOfValues = FileOperations.ReadFromDevice<AddInfoModel>();
                var titleExist = listOfValues?.Where(x => x.Title == edtTitle.Text).Any();

                if (titleExist == true)
                {
                    AlertBox.CreateOkAlertBox("Warning", "Entered title already exist.Please change the field value", Activity, null);
                    return;
                }

                var addInfo = new AddInfoModel { Title = edtTitle.Text, UserId = edtUserId.Text, Password = edtPassword.Text, AdditionalInformation = edtAdditionalInfo.Text };

                FileOperations.InsertOrReplace(addInfo);
                
                Activity.SupportFragmentManager.PopBackStackImmediate();
            }

            catch
            {

            }
            

        }
    }
}