using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhotoHub.Models;

namespace PhotoHub.Controllers
{
    public class MediaController : Controller
    {
        private readonly PhotoGit1Context _context;

        public MediaController(PhotoGit1Context context)
        {
            _context = context;
        }

        // GET: Media
        [Authorize]
        public async Task<IActionResult> Index()
        {


          //  return View(await _context.Media.ToListAsync());

            var id = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            var query = await _context.Access                          
               .Join(_context.Media,                                    
                  ca => ca.MediaId,                                   
                  i => i.MediaId,                                    
                  (ca, i) => new { Access = ca, Media = i })     
               .Where(ca => ca.Access.UserId == id)
               .Select(e => e.Media)                                    
               .ToListAsync();        
           
            return View(query);
        }

        // GET: Media/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _context.Media
                .FirstOrDefaultAsync(m => m.MediaId == id);
            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }

        // GET: Media/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Media/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MediaId,Name,Url,DateUpload,MediaDiscription,MediaFile")] Media media)
        {
           // if (ModelState.IsValid)
           // {
                media.MediaId = Guid.NewGuid();
                _context.Add(media);
                await _context.SaveChangesAsync();


                media.BinFile = media.MediaFile.FileName;
                Access access = new Access()
                {
                    MediaAccessId = Guid.NewGuid(),
                    MediaId = media.MediaId,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
            _context.Add(access);
            await _context.SaveChangesAsync();



                return RedirectToAction(nameof(Index));
            //}
           // return View(media);
        }

        // GET: Media/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _context.Media.FindAsync(id);
            if (media == null)
            {
                return NotFound();
            }
            return View(media);
        }

        // POST: Media/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MediaId,Name,Url,DateUpload,MediaDiscription")] Media media)
        {
            if (id != media.MediaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(media);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaExists(media.MediaId))
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
            return View(media);
        }

        // GET: Media/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _context.Media
                .FirstOrDefaultAsync(m => m.MediaId == id);
            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }

        // POST: Media/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var media = await _context.Media.FindAsync(id);
           // var access = await _context.Access.FindAsync(id);//added for try delete
            _context.Media.Remove(media);
          //  _context.Access.Remove(access);//added for try delete
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MediaExists(Guid id)
        {
            return _context.Media.Any(e => e.MediaId == id);
        }

        //for downloading the media
       /* public FileResult Download(Guid id)
            {
                var fileToRetrieve = db.GetFile(id);
                return File(fileToRetrieve.Data, fileToRetrieve.ContentType);
            }*/
    }
}
