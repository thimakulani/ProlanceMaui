using Firebase.Storage;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProlanceMaui.DBConnect
{
    public  class FirebaseConnect
    {
        public FirestoreDb GetFirestoreDBconnection()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"service_account.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            return FirestoreDb.Create("prolance-3046f");
        }
        public FirebaseStorage GetFirebaseStorage(string token)
        {
            FirebaseStorage firebaseStorage = new FirebaseStorage(
                "prolance-3046f.appspot.com",
                new FirebaseStorageOptions()
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(token),
                });

            return firebaseStorage;
        }
    }
}
