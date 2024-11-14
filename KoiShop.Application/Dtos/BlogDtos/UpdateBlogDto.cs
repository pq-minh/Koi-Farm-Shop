using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Dtos.BlogDtos
{
    [FirestoreData]
    public class UpdateBlogDto
    {
        [FirestoreProperty]
        public string id { get; set; }


        [FirestoreProperty]
        public string? title { get; set; }


        [FirestoreProperty]
        public string? content { get; set; }


        [FirestoreProperty]
        public DateTime? createDate { get; set; }


        //[FirestoreProperty]
        //public DateTime updateDate { get; set; } = DateTime.UtcNow;


        [FirestoreProperty]
        public string? status { get; set; }


        [FirestoreProperty]
        public string? blogType { get; set; }


        [FirestoreProperty]
        public string? userId { get; set; }

    }
}
