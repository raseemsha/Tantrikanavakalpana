using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Common.Apis;
using Android.Gms.Drive;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SecureWallet
{
    public class DriverIntegrationHelper
    {
        GoogleApiClient googleApiClient;

        IDriveApiDriveContentsResult contentResults;
        public DriverIntegrationHelper(GoogleApiClient googleApiClient, IDriveApiDriveContentsResult contentResults)
        {
            this.googleApiClient = googleApiClient;
            this.contentResults = contentResults;
        }
        public  void CreateFileInAppFolder()
        {
          
            Task.Run(() =>
            {
                
                MetadataChangeSet changeSet = new MetadataChangeSet.Builder()
                       .SetTitle("SecureWallet")
                       .SetMimeType("db")
                       .Build();
            DriveClass.DriveApi
                      .GetAppFolder(googleApiClient)
                          .CreateFile(googleApiClient, changeSet, contentResults.DriveContents);
            });
        }
    }
}