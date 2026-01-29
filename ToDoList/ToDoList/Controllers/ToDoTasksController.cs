using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ToDoTasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToDoTasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ToDoTasks
        public async Task<IActionResult> Index(string chatgroupID)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool isMember = await _context.GroupUser
                .AnyAsync(gu => gu.ChatGroupId == chatgroupID && gu.UserId == userId);

            if (!isMember)
                return Forbid();



            var tasks = await _context.ToDoTask
                                      .Where(t => t.ChatGroupId == chatgroupID)
                                      .ToListAsync();

            tasks = tasks.OrderBy(t => t.DeadlineDate).ToList();


            return View(tasks);
        }



        // GET: ToDoTasks/Create

        [Authorize]
        public async Task<IActionResult> Create(string groupId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool isMember = await _context.GroupUser
                .AnyAsync(gu => gu.ChatGroupId == groupId && gu.UserId == userId);

            if (!isMember)
                return Forbid();




            var task = new ToDoTask
            {
                ChatGroupId = groupId 
            };
            return View(task);
        }

        // POST: ToDoTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TaskName,TaskDetails,TaskDone,DeadlineDate")] ToDoTask toDoTask, string groupId)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                toDoTask.UserId = userId;
                toDoTask.ChatGroupId = groupId;


                _context.Add(toDoTask);
                await _context.SaveChangesAsync();
                return RedirectToAction("GroupTasks", "ChatGroups", new { id = groupId });
            }

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            return RedirectToAction("GroupTasks", "ChatGroups", new { id = groupId });//redirecting back to group
        }






        // GET: ToDoTasks/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id, string groupId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool isMember = await _context.GroupUser
                .AnyAsync(gu => gu.ChatGroupId == groupId && gu.UserId == userId);

            if (!isMember)
                return Forbid();




            if (id == null)
            {
                return NotFound();
            }

            var toDoTask = await _context.ToDoTask.FindAsync(id);
            if (toDoTask == null)
            {
                return NotFound();
            }

            return View(toDoTask);
        }

        // POST: ToDoTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TaskName,TaskDetails,TaskDone,DeadlineDate")] ToDoTask toDoTask,string groupId)
        {
            if (id != toDoTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    toDoTask.UserId = userId;
                    toDoTask.ChatGroupId = groupId;
                    _context.Update(toDoTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoTaskExists(toDoTask.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("GroupTasks", "ChatGroups", new { id = groupId });//redirecting back to group
            }
            return View(toDoTask);
        }


        // GET: ToDoTasks/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoTask = await _context.ToDoTask
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toDoTask == null)
            {
                return NotFound();
            }

            return View(toDoTask);
        }

        // POST: ToDoTasks/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string groupId)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool isMember = await _context.GroupUser
                .AnyAsync(gu => gu.ChatGroupId == groupId && gu.UserId == userId);

            if (!isMember)
                return Forbid();




            var toDoTask = await _context.ToDoTask.FindAsync(id);
            if (toDoTask != null)
            {
                _context.ToDoTask.Remove(toDoTask);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("GroupTasks", "ChatGroups", new { id = groupId });//redirecting back to group
        }







        //Switching boolean TaskDone, only can be done by member
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleDone(int? id, string groupId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool isMember = await _context.GroupUser
                .AnyAsync(gu => gu.ChatGroupId == groupId && gu.UserId == userId);

            if (!isMember)
                return Forbid();



            var task = await _context.ToDoTask
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            task.TaskDone = !task.TaskDone; 
            await _context.SaveChangesAsync();

            return RedirectToAction("GroupTasks", "ChatGroups", new { id = groupId });//redirecting back to group
        }
















        private bool ToDoTaskExists(int id)
        {
            return _context.ToDoTask.Any(e => e.Id == id);
        }
    }
}
