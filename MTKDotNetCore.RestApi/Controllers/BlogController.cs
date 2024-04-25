using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using MTKDotNetCore.RestApi.Models;

namespace MTKDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {

        private readonly AppDbContext __context;

        public BlogController()
        {
            __context = new AppDbContext();
        }


        [HttpGet]
        public IActionResult Read()
        {
            var lst = __context.Blogs.ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = __context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null){
                return NotFound("No Data Found!");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            __context.Blogs.Add(blog);
            int result = __context.SaveChanges();
            string message = result > 0 ? "Saving Successful" : "Saving Failed!";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = __context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null){
                return NotFound("No Data Found!");
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            var result = __context.SaveChanges();
            string message = result > 0 ? "Updating Successful" : "Updating Failed!";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = __context.Blogs.FirstOrDefault(x=> x.BlogId == id);
            if(item is null){
                return NotFound("No Data Found!");
            }
            if(!string.IsNullOrEmpty(blog.BlogTitle)){
                item.BlogTitle = blog.BlogTitle;
            }
            if(!string.IsNullOrEmpty(blog.BlogAuthor)){
                item.BlogAuthor = blog.BlogAuthor;
            }
            if(!string.IsNullOrEmpty(blog.BlogContent)){
                item.BlogContent = blog.BlogContent;
            }
            var result = __context.SaveChanges();
            string message = result > 0 ? "Updating Successful" : "Updating Failed!";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = __context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null){
                return NotFound("No Data Found!");
            }
            __context.Blogs.Remove(item);
            var result = __context.SaveChanges();
            string message = result > 0 ? "Deleting Successful" : "Deleting Failed!";
            return Ok(message);
        }

    }

}