using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ChatGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChatGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ChatGroups
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ChatGroup.ToListAsync());
        }







        // GET: ChatGroups/Details/5
        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatGroup = await _context.ChatGroup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chatGroup == null)
            {
                return NotFound();
            }

            return View(chatGroup);
        }

        // GET: ChatGroups/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChatGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupName")] ChatGroup chatGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chatGroup);



                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chatGroup);
        }



        // GET: ChatGroups/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatGroup = await _context.ChatGroup.FindAsync(id);
            if (chatGroup == null)
            {
                return NotFound();
            }
            return View(chatGroup);
        }

        // POST: ChatGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,GroupName")] ChatGroup chatGroup)
        {
            if (id != chatGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chatGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChatGroupExists(chatGroup.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(chatGroup);
        }

        // GET: ChatGroups/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatGroup = await _context.ChatGroup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chatGroup == null)
            {
                return NotFound();
            }

            return View(chatGroup);
        }

        // POST: ChatGroups/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var chatGroup = await _context.ChatGroup.FindAsync(id);
            if (chatGroup != null)
            {
                _context.ChatGroup.Remove(chatGroup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChatGroupExists(string id)
        {
            return _context.ChatGroup.Any(e => e.Id == id);
        }






        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JoinGroup(string id)
        {
            System.Diagnostics.Debug.WriteLine("iiiiiiii");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool isMember = await _context.GroupUser
            .AnyAsync(gu => gu.UserId == userId && gu.ChatGroupId == id.ToString());



            if (isMember)
            {
                return RedirectToAction(nameof(Index));
            }

            var groupUser = new GroupUser
            {
                UserId = userId,
                ChatGroupId = id.ToString(),
            };

            _context.GroupUser.Add(groupUser);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



    }
}
