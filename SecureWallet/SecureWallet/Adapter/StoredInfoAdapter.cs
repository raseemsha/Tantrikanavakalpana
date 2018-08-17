using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System.Collections.Generic;

namespace SecureWallet
{
    
    public class StoredInfoAdapter : BaseExpandableListAdapter
    {
        private List<AddInfoModel> listValues;
        LayoutInflater layoutInflator;
        Context context;
        ExpandableListView.IOnGroupClickListener listner;
        private StoredDetails storedDetails;
        
        public StoredInfoAdapter(Activity context,List<AddInfoModel> listValues, StoredDetails storedDetails)
        {
            this.storedDetails = storedDetails;
            this.context = context;
            this.listValues = listValues;
            layoutInflator = (LayoutInflater)this.context.GetSystemService(Context.LayoutInflaterService);
           
        }

        public override int GroupCount
        {
            get
            {
                return listValues.Count;
            }
            
        }
        

        public override bool HasStableIds
        {
            get
            {
                return true;
            }
        }
        

        public override Object GetChild(int groupPosition, int childPosition)
        {
            return null;
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return 1;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            convertView = layoutInflator.Inflate(Resource.Layout.show_item_child_adapter, parent, false);
            TextView txtUserId= convertView.FindViewById<TextView>(Resource.Id.txtUserIdValue);
            TextView txtPassword = convertView.FindViewById<TextView>(Resource.Id.txtPasswordValue);
            TextView txtAdditionalInfo = convertView.FindViewById<TextView>(Resource.Id.txtAdditionalInfo);
            ImageView imgDelete= convertView.FindViewById<ImageView>(Resource.Id.imgDelete);
            ImageView imgEdit = convertView.FindViewById<ImageView>(Resource.Id.imgEdit);
            imgDelete.Click += delegate
            {
                FileOperations.DeleteSingleEntryFromTable("AddInfoModel",string.Format("Title ='{0}'", listValues[groupPosition].Title));
                listValues.RemoveAt(groupPosition);
                NotifyDataSetChanged();
                storedDetails.SetHeader();

            };
            imgEdit.Click += delegate
            {
                NavigateToEditFragment(listValues[groupPosition]);
            };

            if (string.IsNullOrEmpty(listValues[groupPosition].UserId))
            {
                txtUserId.Visibility = ViewStates.Gone;
            }

            else
            {
                txtUserId.Visibility = ViewStates.Visible;
                txtUserId.Text = listValues[groupPosition].UserId;
            }
            if(string.IsNullOrEmpty(listValues[groupPosition].Password))
            {
                txtPassword.Visibility = ViewStates.Gone;
            }
                else
            {
                txtPassword.Visibility = ViewStates.Visible;
                txtPassword.Text = listValues[groupPosition].Password;
            }
                
            
            if(string.IsNullOrEmpty(listValues[groupPosition].AdditionalInformation))
            {
                txtAdditionalInfo.Visibility = ViewStates.Gone;
            }
            else
            {
                txtAdditionalInfo.Visibility = ViewStates.Visible;
                txtAdditionalInfo.Text = listValues[groupPosition].AdditionalInformation;
            }
            


            return convertView;
        }


        private void NavigateToEditFragment(AddInfoModel addInfoModel)
        {
            Android.Support.V4.App.FragmentTransaction transaction = storedDetails.Activity.SupportFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.container, new AddInfo(storedDetails, addInfoModel), Constants.AddEditInfoFragment);
            transaction.AddToBackStack(Constants.AddEditInfoFragment);
            transaction.Commit();
        }



        public override Object GetGroup(int groupPosition)
        {
            return groupPosition;
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            var titleName = listValues[groupPosition].Title;
            convertView = layoutInflator.Inflate(Resource.Layout.show_item_parent_adapter, parent, false);
            TextView txtTopicTitle = convertView.FindViewById<TextView>(Resource.Id.txtHeaderTitle);
            ImageView imgView = convertView.FindViewById<ImageView>(Resource.Id.imgExpandCollapseIndicator);
            ImageView imgLockUnLock = convertView.FindViewById<ImageView>(Resource.Id.imgLockUnLock);
            if (isExpanded)
            {
                imgLockUnLock.SetImageResource(Resource.Drawable.ic_unlock);
                imgLockUnLock.SetColorFilter(new Color(ContextCompat.GetColor(context, Resource.Color.red)), PorterDuff.Mode.SrcAtop);
                imgView.SetImageResource(Resource.Drawable.ic_up_arrow);
            }
            else
            {
                imgLockUnLock.SetImageResource(Resource.Drawable.ic_lock);
                imgLockUnLock.SetColorFilter(new Color(ContextCompat.GetColor(context, Resource.Color.green)), PorterDuff.Mode.SrcAtop);
                imgView.SetImageResource(Resource.Drawable.ic_down_arrow);
            }

            txtTopicTitle.Text = titleName[0].ToString().ToUpper() + titleName.Substring(1);
            return convertView;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }

       
    }
}