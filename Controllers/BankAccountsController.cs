using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BankingMVCApp.Models;
using BankingMVCApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingMVCApp.Controllers;

public class BankAccountsController(ILogger<BankAccountsController> logger, AppDbContext appContext) : Controller
{
    private readonly ILogger<BankAccountsController> _logger = logger;
    private readonly AppDbContext _appContext = appContext;

    public async Task<IActionResult> Index()
    {
        if (!await _appContext.BankAccounts.AnyAsync())
        {
            for (int i = 0; i<10;i++)
            {
                await _appContext.BankAccounts.AddAsync(new BankAccount(i.ToString(), $"{i}@gtbank.com"));
                await _appContext.SaveChangesAsync();
            }
        }

        var bankAccounts = await _appContext.BankAccounts.ToListAsync();

        return View(bankAccounts);
    }

    public async Task<IActionResult> Create(BankAccount model)
    {
        if (ModelState.IsValid)
        {
            // Validate model and process data
            _appContext.BankAccounts.Add(model);
            await _appContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateEmail(int id, string newEmail)
    {
        var bankAccount = await _appContext.BankAccounts.FindAsync(id);

        if (bankAccount == null)
        {
            return NotFound();
        }
        bankAccount.UpdateEmail(newEmail);
        
        _appContext.BankAccounts.Update(bankAccount);
        await _appContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }


    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var bankAccount = await _appContext.BankAccounts.FindAsync(id);

        if (bankAccount == null)
        {
            return NotFound();
        }

        return View(bankAccount); // Show confirmation view
    }

    [HttpPost, ActionName("ConfirmDelete")]
    public async Task<IActionResult> ConfirmDelete(int id)
    {
        var bankAccount = await _appContext.BankAccounts.FindAsync(id);

        if (bankAccount == null)
        {
            return NotFound();
        }

        bankAccount.MoveBalanceToMobile();
        
        _appContext.BankAccounts.Remove(bankAccount);
        await _appContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
