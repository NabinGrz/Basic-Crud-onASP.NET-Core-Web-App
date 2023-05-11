using BlogMVCWebApp.Web.Data;
using BlogMVCWebApp.Web.Models.DomainModel;
using BlogMVCWebApp.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogMVCWebApp.Web.Controllers
{
    // This controller handles admin-related functionality for tags, such as adding, viewing, editing, and deleting tags
    public class AdminTagController : Controller
    {
        private readonly BlogAppDBContext _blogAppDBContext;

        // Constructor to inject the BlogAppDBContext into the controller
        public AdminTagController(BlogAppDBContext blogAppDBContext)
        {
            this._blogAppDBContext = blogAppDBContext;
        }

        //************************************
        // Returns a view to add a new tag
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // Returns a view to view all tags in the database
        [HttpGet]
        public async Task<IActionResult> ViewAllTags()
        {
            var allTags = await _blogAppDBContext.Tags.ToListAsync();
            return View(allTags);
        }

        // Returns a view to view or edit a specific tag, identified by its ID
        [HttpGet]
        public async Task<IActionResult> ViewOrEditTag(Guid id)
        {
            var tagToView = await _blogAppDBContext.Tags.FindAsync(id);
            if (tagToView == null)
            {
                // If the tag is not found, return a 404 error
                return NotFound();
            }
            return View(tagToView);
        }

        //************************************
        // Adds a new tag to the database based on the information provided in the AddTagRequest object
        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            var tag = new Tag()
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            // Add the tag to the context and save changes to the database
            _blogAppDBContext.Tags.Add(tag);
            await _blogAppDBContext.SaveChangesAsync();

            // Redirect to the ViewAllTags action
            return RedirectToAction("ViewAllTags");
        }

        // Deletes a tag from the database based on its ID
        [HttpPost]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            var tagToDelete = await _blogAppDBContext.Tags.FindAsync(id);
            if (tagToDelete == null)
            {
                // If the tag is not found, return a 404 error
                return NotFound();
            }
            _blogAppDBContext.Tags.Remove(tagToDelete);
            await _blogAppDBContext.SaveChangesAsync();

            // Redirect to the ViewAllTags action
            return RedirectToAction("ViewAllTags");
        }

        // Updates a tag in the database based on the information provided in the UpdateTagRequest object
        [HttpPost]
        public async Task<IActionResult> ViewOrEditTag(UpdateTagRequest tag)
        {
            var tagToUpdate = await _blogAppDBContext.Tags.FindAsync(tag.ID);
            if (tag == null)
            {
                // If the tag is not found, return a 404 error
                return NotFound();
            }

            // Update the tag with the new information and save changes to the database
            tagToUpdate.Name = tag.Name;
            tagToUpdate.DisplayName = tag.DisplayName;
            await _blogAppDBContext.SaveChangesAsync();

            // Redirect to the ViewAllTags action
            return RedirectToAction("ViewAllTags");
        }
    }
}
