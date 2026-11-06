
using System.Collections.Generic;

namespace library.Models;

public class MemberDetails
{
    public Member Member { get; set; } = null!;

    public List<LoanView> Loans { get; set; } = new();
}
