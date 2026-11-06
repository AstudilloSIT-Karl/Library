

using library.Data;
using library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace library.Controllers;

public class LoansController : Controller
{
    private readonly LoanRepository _loans;
    private readonly MemberRepository _members;
    private readonly BookRepository _books;

    public LoansController(
        LoanRepository loans,
        MemberRepository members,
        BookRepository books)
    {
        _loans = loans;
        _members = members;
        _books = books;
    }

    public async Task<IActionResult> Index()
    {
        var loans = await _loans.GetAllAsync();
        return View(loans);
    }

    [HttpGet]
    public async Task<IActionResult> Borrow()
    {
        await LoadDropdowns();
        return View(new BorrowForm());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Borrow(BorrowForm form)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdowns();
            return View(form);
        }

        var success = await _loans.BorrowAsync(
            form.MemberId,
            form.BookId,
            form.DaysAllowed);

        if (!success)
        {
            ModelState.AddModelError(
                "",
                "This member already has 3 open loans.");

            await LoadDropdowns();
            return View(form);
        }

        TempData["Success"] = "Book borrowed successfully!";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Return(long id)
    {
        await _loans.ReturnAsync(id);

        TempData["Success"] = "Book returned successfully!";

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadDropdowns()
    {
        ViewBag.Members = new SelectList(
            await _members.GetAllAsync(),
            "MemberId",
            "FullName");

        ViewBag.Books = new SelectList(
            await _books.GetAllAsync(),
            "BookId",
            "Title");
    }
}

