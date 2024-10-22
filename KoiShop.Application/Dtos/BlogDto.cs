using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace KoiShop.Application.Dtos
{
    [FirestoreData]
    public class BlogDto
    {
        [FirestoreProperty]
        public string PostId { get; set; }

        [FirestoreProperty]
        public string Tittle { get; set; }

        [FirestoreProperty]
        public string Content { get; set; }

        [FirestoreProperty]
        public DateTime CreateDate { get; set; }

        [FirestoreProperty]
        public DateTime UpdateDate  { get; set; }

        [FirestoreProperty]
        public string Status { get; set; }

        [FirestoreProperty]
        public string typePost {  get; set; }

        [FirestoreProperty]
        public string userId {  get; set; }
    }
}
