using Firebase.Storage;
using FirebaseAdmin.Auth;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KoiFarmShop.Application.Services
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
                // Kiểm tra nếu instance mặc định đã tồn tại thì không tạo lại
                var firebaseApp = FirebaseApp.GetInstance("[DEFAULT]");
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException)
                {
                    // Nếu không tồn tại instance mặc định, tạo mới
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



        public async Task<FirebaseToken> VerifyTokenAsync(string token)
        {
            try
            {
                return await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
            }
            catch (FirebaseAuthException ex)
            {
                _logger.LogError(ex, "Token không hợp lệ.");
                throw;
            }
        }

        public async Task<string> UploadFileToFirebaseStorageAsync(IFormFile file, string directory)
        {
            // Đảm bảo directory không chứa dấu '/' ở đầu hoặc cuối
            directory = directory.Trim('/');

            // Tạo tên file độc nhất bằng cách thêm GUID
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = $"{directory}/{fileName}"; // Sử dụng tham số directory để xác định vị trí lưu trữ

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
                    _logger.LogError(ex, "Đã xảy ra lỗi trong quá trình upload file.");
                    throw; // Ném lại lỗi để xử lý ở nơi gọi
                }
            }
        }

        public async Task<string> UpdateFileInFirebaseStorageAsync(string oldFilePath, IFormFile newFile, string directory)
        {
            // Đảm bảo directory không chứa dấu '/' ở đầu hoặc cuối
            directory = directory.Trim('/');

            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(newFile.FileName)}";
            var newFilePath = $"{directory}/{fileName}"; // Sử dụng tham số directory

            var firebaseStorage = new FirebaseStorage(_firebaseStorageBucket);

            // Xóa tệp cũ
            await firebaseStorage.Child(oldFilePath).DeleteAsync();

            using (var stream = newFile.OpenReadStream())
            {
                // Tải tệp mới lên
                await firebaseStorage.Child(newFilePath).PutAsync(stream);
                return await firebaseStorage.Child(newFilePath).GetDownloadUrlAsync();
            }
        }

        public async Task<bool> DeleteFileInFirebaseStorageAsync(string filePath)
        {
            // chỉ đưa vô file path, ko phải toàn bộ url
            try
            {
                var firebaseStorage = new FirebaseStorage(_firebaseStorageBucket);
                filePath = filePath.TrimEnd('/');
                // Xóa tệp
                await firebaseStorage.Child(filePath).DeleteAsync();
                return true; // Trả về true nếu xóa thành công
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi trong quá trình xóa tệp.");
                return false; // Trả về false nếu có lỗi xảy ra
            }
        }



    }
}
