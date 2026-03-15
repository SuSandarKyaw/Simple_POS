using System;
using System.Collections.Generic;

namespace _5BB_POS.Models;

public partial class TblCategory
{
	public int CategoryId { get; set; }

	public string Name { get; set; } = null!;

	public bool IsActive { get; set; } = true;

	public string CreatedBy { get; set; } = "systemUser";

	public DateTime CreatedAt { get; set; } = DateTime.Now;

	public string? UpdatedBy { get; set; }

	public DateTime? UpdatedAt { get; set; }

	public virtual ICollection<TblProduct> TblProducts { get; set; } = new List<TblProduct>();
}
