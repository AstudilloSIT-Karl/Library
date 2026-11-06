
namespace library.Models;

public class LoanView
{
    public long LoanId { get; set; }

    public long MemberId { get; set; }

    public string MemberName { get; set; } = string.Empty;

    public string BookTitle { get; set; } = string.Empty;

    public DateOnly LoanedOn { get; set; }

    public DateOnly? ReturnedOn { get; set; }

    public int DaysAllowed { get; set; }

    public string Status { get; set; } = string.Empty;
}
