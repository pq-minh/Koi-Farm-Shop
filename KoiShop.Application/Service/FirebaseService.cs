using Firebase.Storage;
using FirebaseAdmin.Auth;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KoiShop.Application.Service
{
    public class FirebaseService
    {
        private readonly string _firebaseStorageBucket;
        private readonly ILogger<FirebaseService> _logger;

        public FirebaseService(ILogger<FirebaseService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _firebaseStorageBucket = configuration["Firebase:StorageBucket"];

            try
            {
                // ktra nếu instance mặc định đã tồn tại thì không tạo lại
                var firebaseApp = FirebaseApp.GetInstance("[DEFAULT]");
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException)
                {
                    var serviceAccountPath = configuration["Firebase:ServiceAccountPath"];
                    FirebaseApp.Create(new AppOptions()
                    {
                        Credential = GoogleCredential.FromFile(serviceAccountPath)
                    });
                }
                else
                {
                    throw;
                }
            }
        }
        public async Task<string> UploadFileToFirebaseStorageAsync(IFormFile file, string directory)
        {
            directory = directory.Trim('/');

            // tạo tên file độc nhất = GUID
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = $"{directory}/{fileName}";               // directory = vị trí lưu trữ file

            using (var stream = file.OpenReadStream())
            {
                try
                {
                    var firebaseStorage = new FirebaseStorage(_firebaseStorageBucket);
                    await firebaseStorage.Child(filePath).PutAsync(stream);
                    return await firebaseStorage.Child(filePath).GetDownloadUrlAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while uploading the file.");
                    throw; 
                }
            }
        }
        public async Task<bool> DeleteFileInFirebaseStorageAsync(string filePath)
        {
            // chỉ đưa vô file path, ko phải toàn bộ url
            try
            {
                var firebaseStorage = new FirebaseStorage(_firebaseStorageBucket);
                filePath = filePath.TrimEnd('/');
                
                await firebaseStorage.Child(filePath).DeleteAsync();
                return true; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the file.");
                return false; 
            }
        }



    }
}
