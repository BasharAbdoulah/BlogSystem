﻿
using BlogSystem.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly BlogDataContext dBContext;

        public CommentController(BlogDataContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Comment>>> Get()
        {
            var comments = await dBContext.Comments.ToListAsync();
            return Ok(comments);
        }

        [HttpGet("commentsByPostId/{id}"), Authorize]
        public async Task<ActionResult<List<Comment>>> GetByPostId(int id)
        {
            var posts = dBContext.Comments.Where(p =>  p.PostId == id).ToList();
            if (posts == null)
            {
                return BadRequest();
            }

            return Ok(posts);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<Comment>> Post(Comment comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }

            dBContext.Comments.Add(comment);
            await dBContext.SaveChangesAsync();
            return Ok(comment);
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<Comment>> Edit(Comment comment)
        {
            var oldComment = await dBContext.Comments.FirstOrDefaultAsync(c => c.Id == comment.Id);
            if (oldComment == null)
            {
                return BadRequest();
            }

            oldComment.Text = comment.Text;
            oldComment.CommentDate = comment.CommentDate;

            await dBContext.SaveChangesAsync();
            return Ok(oldComment);
        }

        [HttpDelete, Authorize]
        public async Task<ActionResult<Comment>> Delete(int id)
        {
            var comment = await dBContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null) return BadRequest();
            try
            {
                dBContext.Comments.Remove(comment);
                await dBContext.SaveChangesAsync();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Comment has been deleted!");
        }
    }
}