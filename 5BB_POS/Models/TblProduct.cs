using System;
using System.Collections.Generic;

namespace _5BB_POS.Models;

public partial class TblProduct
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int StockQty { get; set; }

    public int CategoryId { get; set; }

    public bool IsActive { get; set; }

    public virtual TblCategory Category { get; set; } = null!;

    public virtual ICollection<TblSaleItem> TblSaleItems { get; set; } = new List<TblSaleItem>();
}
