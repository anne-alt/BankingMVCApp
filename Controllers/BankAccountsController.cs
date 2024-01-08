using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BankingMVCApp.Models;
using BankingMVCApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingMVCApp.Controllers;

public class BankAccountsController : Controller
{
    private readonly ILogger<BankAccountsController> _logger;
    private readonly AppDbContext _appContext;

    public BankAccountsController(ILogger<BankAccountsController> logger, AppDbContext appContext)
    {
        _logger = logger;
        _appContext = appContext;
    }

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
}
