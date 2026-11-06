
using System.ComponentModel.DataAnnotations;

namespace library.Models;

public class BorrowForm
{
    [Required]
    public long MemberId { get; set; }

    [Required]
    public long BookId { get; set; }

    [Required]
    [Range(1, 30)]
    public int DaysAllowed { get; set; } = 7;
}
