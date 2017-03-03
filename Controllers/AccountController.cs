using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using accounts.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace accounts.Controllers
{
    public class AccountController : Controller
    {
        private Context _context;
        public AccountController(Context context){
            _context = context;
        }
        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetInt32("userId") == null){
                return RedirectToAction("Index", "Login");
            }
            int userId = (int)HttpContext.Session.GetInt32("userId");
            if(HttpContext.Session.GetString("err") != null){
                ViewBag.err = HttpContext.Session.GetString("err");
            }
            User userFull = _context.Users
                .Include(user => user.accounts)
                .ThenInclude(account => account.transactions)
                .SingleOrDefault(user => user.id == userId);

            ViewBag.user = userFull;
            return View();
        }
        [HttpPost]
        [Route("createTransaction")]
        public IActionResult CreateTransaction(Transaction newTransaction){
            HttpContext.Session.SetString("err", ""); 
            //retrieve account
            Account account = _context.Accounts.Single(findAccount => findAccount.id == newTransaction.accountId);
            if(newTransaction.amount == 0){
                HttpContext.Session.SetString("err", "Please provide a Transaction amount.");                
                return RedirectToAction("Dashboard");
            }
            if(newTransaction.amount < 0){
                //withdrawal: validate amount doesn't create neg balance
                if((account.balance + newTransaction.amount) < 0){
                    HttpContext.Session.SetString("err", "Transaction would create negative balance.");
                    return RedirectToAction("Dashboard");
                }
            }
            account.balance += newTransaction.amount;
            //create and save transaction
            _context.Transactions.Add(newTransaction);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}
// ToDo: Format transaction amount input to appear as currency
