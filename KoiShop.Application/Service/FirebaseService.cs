using Firebase.Storage;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using KoiShop.Application.Dtos;
using Google.Cloud.Firestore;

namespace KoiShop.Application.Service
{
    public class FirebaseService
    {
        private readonly ILogger<FirebaseService> _logger;
        private readonly FirestoreDb _firestoreDb;
        private readonly string _firebaseStorageBucket;

        public FirebaseService(ILogger<FirebaseService> logger, IConfiguration config)
        {
            var serviceAccountPath = config["Firebase:ServiceAccountPath"];
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", serviceAccountPath);

            //if (FirebaseApp.DefaultInstance == null)
            //{
            //    FirebaseApp.Create(new AppOptions
            //    {
            //        Credential = GoogleCredential.FromFile(serviceAccountPath),
            //        //ProjectId = configuration["Firebase:ProjectId"]
            //    });
            //}

            _logger = logger;
            _firebaseStorageBucket = config["Firebase:StorageBucket"];
            _firestoreDb = FirestoreDb.Create(config["Firebase:ProjectId"]);
        }

        public async Task<string> UploadFileToFirebaseStorage(IFormFile file, string directory)
        {
            directory = directory.Trim('/');

            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = $"{directory}/{fileName}";

            try
            {
                using var stream = file.OpenReadStream();
                var firebaseStorage = new FirebaseStorage(_firebaseStorageBucket);
                await firebaseStorage.Child(filePath).PutAsync(stream);
                return await firebaseStorage.Child(filePath).GetDownloadUrlAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error uploading file to Firebase Storage: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteFileInFirebaseStorage(string filePath)
        {
            filePath = filePath.Trim('/');

            try
            {
                var firebaseStorage = new FirebaseStorage(_firebaseStorageBucket);
                await firebaseStorage.Child(filePath).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting file from Firebase Storage: {ex.Message}");
                return false;
            }
        }

        public string GetRelativeFilePath(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                var startIndex = filePath.IndexOf("/o/") + 3;
                var endIndex = filePath.IndexOf("?");

                if (startIndex < 3 || endIndex <= startIndex) return null;

                return Uri.UnescapeDataString(filePath.Substring(startIndex, endIndex - startIndex));
            }
            return null;
        }


        public async Task<string> SaveDocument<T>(T document, string collectionName)
        {
            try
            {
                var docRef = _firestoreDb.Collection(collectionName).Document();
                await docRef.SetAsync(document);
                return docRef.Id;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<bool> UpdateDocument<T>(string documentId, T document, string collectionName)
        {
            try
            {
                var docRef = _firestoreDb.Collection(collectionName).Document(documentId);

                var updates = document.GetType()
                    .GetProperties()
                    .Where(prop => prop.GetValue(document) != null)
                    .ToDictionary(
                        prop => prop.Name,
                        prop => prop.GetValue(document)
                    );

                await docRef.UpdateAsync(updates);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }


        public async Task<List<T>> GetDocuments<T>(string collectionName) where T : class, new()
        {
            try
            {
                CollectionReference collectionRef = _firestoreDb.Collection(collectionName);

                QuerySnapshot snapshot = await collectionRef.GetSnapshotAsync();

                List<T> documents = new List<T>();
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        T obj = document.ConvertTo<T>();
                        var propertyInfo = typeof(T).GetProperty("id");
                        if (propertyInfo != null && propertyInfo.PropertyType == typeof(string))
                        {
                            propertyInfo.SetValue(obj, document.Id);
                        }
                        documents.Add(obj);
                    }
                }
                return documents;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }


        public async Task<T> GetDocumentById<T>(string documentId, string collectionName) where T : class
        {
            try
            {
                DocumentReference docRef = _firestoreDb.Collection(collectionName).Document(documentId);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    T document = snapshot.ConvertTo<T>();

                    var propertyInfo = typeof(T).GetProperty("id");
                    if (propertyInfo != null && propertyInfo.PropertyType == typeof(string))
                    {
                        propertyInfo.SetValue(document, snapshot.Id);
                    }

                    return document;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
