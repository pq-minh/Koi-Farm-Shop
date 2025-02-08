using KoiShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Domain.Constant
{
   public class QuotationWithKoi
    {
        public int QuotationId { get; set; }
        public int? RequestId { get; set; }
        public DateTime? CreateDate { get; set; }
        public double? Price { get; set; }
        public string? Status { get; set; }
        public string? UserId { get; set; }
        public string? Note { get; set; }

        // Thông tin từ Koi
        public string? KoiName { get; set; }
        public string? KoiImage { get; set; }
        public int? KoiAge { get; set; }
        public double? KoiWeight { get; set; }
        public double? KoiSize { get; set; }
        //Thông tin về request 
        public string? TypeRequest {  get; set; }
        public DateTime? ConsignmentDate { get; set; }

        public DateTime? EndDate { get; set; }
        //Thông tin người gửi 
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? PhoneNumber {  get; set; }

        // Thông tin loại cá
        public virtual FishCategory? FishType { get; set; }
    }
}
