using System;
using System.Collections.Generic;

namespace _5BB_POS.Models;

public partial class TblSale
{
    public int SaleId { get; set; }

    public string InvoiceNo { get; set; } = null!;

    public DateTime SaleDate { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual ICollection<TblSaleItem> TblSaleItems { get; set; } = new List<TblSaleItem>();
}
