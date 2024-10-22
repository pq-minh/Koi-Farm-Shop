using Firebase.Storage;
using FirebaseAdmin.Auth;
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

        public FirebaseService(ILogger<FirebaseService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _firebaseStorageBucket = configuration["Firebase:StorageBucket"];

            try
            {
                // Thiết lập biến môi trường GOOGLE_APPLICATION_CREDENTIALS
                var serviceAccountPath = configuration["Firebase:ServiceAccountPath"];
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", serviceAccountPath);

                // Kiểm tra nếu instance mặc định đã tồn tại thì không tạo lại
                var firebaseApp = FirebaseApp.GetInstance("[DEFAULT]");
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException)
                {
                    // Nếu chưa có instance, khởi tạo Firebase Admin SDK
                    FirebaseApp.Create(new AppOptions()
                    {
                        // Không cần truyền Credential vì đã sử dụng biến môi trường
                        ProjectId = configuration["Firebase:ProjectId"]
                    });
                }
                else
                {
                    throw;
                }
            }

            // Khởi tạo Firestore (sử dụng instance mặc định đã tạo)
            _firestoreDb = FirestoreDb.Create(configuration["Firebase:ProjectId"]);
            _logger.LogInformation("Firestore database initialized successfully.");
        }

        public async Task<string> UploadFileToFirebaseStorage(IFormFile file, string directory)
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
                    throw; 
                }
            }
        }
        public async Task<bool> DeleteFileInFirebaseStorage(string filePath)
        {
            // chỉ đưa vô file path, ko phải toàn bộ url
            try
            {
                var firebaseStorage = new FirebaseStorage(_firebaseStorageBucket);
                filePath = filePath.Trim('/');
                
                await firebaseStorage.Child(filePath).DeleteAsync();
                return true; 
            }
            catch (Exception ex)
            {
                return false; 
            }
        }

        public async Task<string> GetRelativeFilePath(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                // Tách đường dẫn tuyệt đối thành đường dẫn tương đối vì Firebase chỉ nhận vào đường dẫn tương đối
                // ví dụ url: /o/KoiFishImage%2Fca6c022b-4247-426b-99a0-03bf1d94c534_Screenshot%202024-10-17%20203637.png?
                var startIndex = filePath.IndexOf("/o/") + 3; // 3 là độ dài của chuỗi "/o/"
                var endIndex = filePath.IndexOf("?");

                if (startIndex < 3 || endIndex <= startIndex)
                    return null;

                var relativeFilePath = Uri.UnescapeDataString(filePath.Substring(startIndex, endIndex - startIndex));
                return relativeFilePath;
            }
            return null;
        }



        // lưu blog post vào Firestore
        public async Task<string> SaveBlog(BlogDto blogPost)
        {
            try
            {
                DocumentReference docRef = _firestoreDb.Collection("Blogs").Document();
                await docRef.SetAsync(blogPost);
                return docRef.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<BlogDto>> GetBlog()
        {
            try
            {
                // lấy tham chiếu tới collection tên: "blogPosts"
                CollectionReference blogsRef = _firestoreDb.Collection("Blogs");

                // lấy tất cả document trong collection
                QuerySnapshot snapshot = await blogsRef.GetSnapshotAsync();

                // chuyển thành list các Blog
                List<BlogDto> blogs = new List<BlogDto>();
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        BlogDto blogDto = document.ConvertTo<BlogDto>();
                        blogDto.PostId = document.Id; // gán Id của document mỗi Blog
                        blogs.Add(blogDto);
                    }
                }
                return blogs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateBlog(string blogId)
        {
            return true;
        }

    }
}
