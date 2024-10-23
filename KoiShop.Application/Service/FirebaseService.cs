using Firebase.Storage;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using KoiShop.Application.Dtos;
using Google.Cloud.Firestore;
using KoiShop.Application.Dtos.BlogDtos;
using KoiShop.Application.Dtos.KoiShop.Application.Dtos;

namespace KoiShop.Application.Service
{
    public class FirebaseService
    {
        private readonly ILogger<FirebaseService> _logger;
        private readonly FirestoreDb _firestoreDb;
        private readonly string _firebaseStorageBucket;

        public FirebaseService(ILogger<FirebaseService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _firebaseStorageBucket = configuration["Firebase:StorageBucket"];

            var serviceAccountPath = configuration["Firebase:ServiceAccountPath"];
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", serviceAccountPath);

            // Kiểm tra và tạo ứng dụng Firebase nếu chưa tồn tại
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile(serviceAccountPath),
                    ProjectId = configuration["Firebase:ProjectId"]
                });
            }

            // Khởi tạo Firestore
            _firestoreDb = FirestoreDb.Create(configuration["Firebase:ProjectId"]);
        }

        public async Task<string> UploadFileToFirebaseStorage(IFormFile file, string directory)
        {
            directory = directory.Trim('/');

            // Tạo tên file độc nhất bằng GUID
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = $"{directory}/{fileName}"; // Directory là vị trí lưu trữ file

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
                // Tách đường dẫn tuyệt đối thành đường dẫn tương đối vì Firebase chỉ nhận vào đường dẫn tương đối
                var startIndex = filePath.IndexOf("/o/") + 3; // 3 là độ dài của chuỗi "/o/"
                var endIndex = filePath.IndexOf("?");

                if (startIndex < 3 || endIndex <= startIndex) return null;

                return Uri.UnescapeDataString(filePath.Substring(startIndex, endIndex - startIndex));
            }
            return null;
        }

        // sử dụng generic object
        public async Task<string> SaveDocument<T>(T document, string collectionName)
        {
            try
            {
                // tham chiếu tới collection được truyền vào
                var docRef = _firestoreDb.Collection(collectionName).Document();
                await docRef.SetAsync(document);
                return docRef.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving document to {collectionName}: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateDocument<T>(string documentId, T document, string collectionName)
        {
            try
            {
                // tham chiếu tới document trong collection được truyền vào
                var docRef = _firestoreDb.Collection(collectionName).Document(documentId);

                // convert đối tượng T thành Dictionary để cập nhật từng trường
                var updates = document.GetType()
                    .GetProperties()
                    .Where(prop => prop.GetValue(document) != null)
                    .ToDictionary(
                        prop => prop.Name,
                        prop => prop.GetValue(document)
                    );

                await docRef.UpdateAsync(updates);
                _logger.LogInformation($"Document with ID: {documentId} in {collectionName} updated successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating document in {collectionName}: {ex.Message}");
                return false;
            }
        }


        public async Task<List<T>> GetDocuments<T>(string collectionName) where T : class, new()
        {
            try
            {
                // lấy tham chiếu tới collection được truyền vào
                CollectionReference collectionRef = _firestoreDb.Collection(collectionName);

                // lấy tất cả các document trong collection
                QuerySnapshot snapshot = await collectionRef.GetSnapshotAsync();

                // chuyển đổi thành danh sách các đối tượng kiểu T
                List<T> documents = new List<T>();
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        T obj = document.ConvertTo<T>();
                        // nếu đối tượng có thuộc tính "Id", gán document Id cho nó
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
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving documents from {collectionName}: {ex.Message}");
                throw;
            }
        }


        public async Task<T> GetDocumentById<T>(string documentId, string collectionName) where T : class
        {
            try
            {
                // lấy tham chiếu tới document trong Firestore
                DocumentReference docRef = _firestoreDb.Collection(collectionName).Document(documentId);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    // chuyển document sang đối tượng kiểu T
                    T document = snapshot.ConvertTo<T>();

                    // nếu đối tượng có thuộc tính "id", gán document Id cho nó
                    var propertyInfo = typeof(T).GetProperty("id");
                    if (propertyInfo != null && propertyInfo.PropertyType == typeof(string))
                    {
                        propertyInfo.SetValue(document, snapshot.Id);
                    }

                    return document;
                }
                else
                {
                    return null; // null nếu document ko tồn tại
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving document: {ex.Message}");
                throw;
            }
        }
    }
}
