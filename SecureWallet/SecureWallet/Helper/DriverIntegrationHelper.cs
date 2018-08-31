
using Android.Gms.Common.Apis;
using Android.Gms.Drive;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Java.IO;
using System.Collections.Generic;

namespace SecureWallet
{
    public class DriverIntegrationHelper
    {
        GoogleApiClient googleApiClient;

        IDriveApiDriveContentsResult contentResults;
        DriveService driveService;

        public DriverIntegrationHelper(GoogleApiClient googleApiClient, IDriveApiDriveContentsResult contentResults)
        {
            this.googleApiClient = googleApiClient;
            this.contentResults = contentResults;
            driveService = new DriveService();

        }
        public void CreateFileInAppFolder()
        {
            // var fileMetaData = new File
            // {
            //     Name = "PasswordTracker.db",
            //     Parents = new List<string>()
            //{
            //    "appDataFolder"
            //}
            // };
            var writer = new OutputStreamWriter(contentResults.DriveContents.OutputStream);
            

            MetadataChangeSet changeSet = new MetadataChangeSet.Builder()
                                                      .SetTitle("PasswordTracker")
                                                      .SetMimeType("multipart/form-data")
                                                      .SetStarred(true)
                                                      .Build();

            //FilesResource.CreateMediaUpload request;
            using (var stream = new System.IO.FileStream(FileOperationManager.folder+ "/PasswordTracker.db", System.IO.FileMode.Open))
            {
                //request = driveService.Files.Create(fileMetaData,stream, "multipart/form-data");
                //request.Fields = "id";
                //request.Upload();
                //var file = request.ResponseBody;

                DriveClass.DriveApi.GetAppFolder(googleApiClient).CreateFile(googleApiClient, changeSet, contentResults.DriveContents);

            }
            

        }

        public void ListFiles()
        {
            var request = driveService.Files.List();
            request.Spaces = "appDataFolder";
            request.Fields = "nextPageToken, files(id, name)";
            request.PageSize = 10;
            var result = request.Execute();
            foreach (var file in result.Files)
            {

            }
        }

    }
}