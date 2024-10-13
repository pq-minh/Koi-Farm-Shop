using System;
using System.Collections.Generic;

namespace KoiFarmShop.Domain.Entities
{

    public partial class Request
    {
        public int RequestId { get; set; }

        public int? PackageId { get; set; }

        public DateOnly? CreatedDate { get; set; }

        public int? RelationalRequestId { get; set; }

        public DateOnly? ConsignmentDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public double? AgreementPrice { get; set; }

        public string? TypeRequest { get; set; }

        public string? Status { get; set; }

        public string? UserId { get; set; }

        public virtual Package? Package { get; set; }

        public virtual ICollection<Quotation> Quotations { get; set; } = new List<Quotation>();

        public virtual AspNetUser? User { get; set; }
    }
}