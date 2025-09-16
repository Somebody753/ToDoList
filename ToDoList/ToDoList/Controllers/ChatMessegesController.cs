using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;

namespace ToDoList.Controllers
{
    public class ChatMessegesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ChatMessegesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: ChatMessegesController
        public ActionResult Index()
        {
            var messages = _context.ChatMessages
            .OrderBy(m => m.Timestamp) // latest first
            .Take(50) // limit to last 50 messages
            .ToList();
            return View(messages);
        }

        // GET: ChatMessegesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ChatMessegesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChatMessegesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ChatMessegesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ChatMessegesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ChatMessegesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChatMessegesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
