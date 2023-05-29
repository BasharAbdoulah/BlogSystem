
using BlogSystem.DBModels;
using BlogSystem.UOW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CommentController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<Comment>>> Get()
        {
            try
            {
                var comments = await unitOfWork.Comment.All();
                if (comments == null) return BadRequest();
                return Ok(comments);

            } catch (Exception ex) { return BadRequest(ex.Message); }
        }

        //[HttpGet("GetCommentsByPost")]
        //public async Task<ActionResult<List<Comment>>> GetBYPost(int id) { 
        
        //    var commentsByPost = await dBContext.Comments.Where(c => c.PostId == id).ToListAsync();
        //    if (!commentsByPost.Any()) return BadRequest();

        //    return Ok(commentsByPost);
        //}

        [HttpGet("GetCommentsByUser")]
        public async Task<ActionResult<List<Comment>>> GetBYUser(int id)
        {

            try
            {
                var comments = await unitOfWork.Comment.All();
                var commentsByUser = comments.Where(x => x.UserId == id);
                if (!commentsByUser.Any()) return BadRequest();

                return Ok(commentsByUser);
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("commentsByPostId/{id}")]
        public async Task<ActionResult<List<Comment>>> GetByPostId(int id)
        {
            try
            {
                var comments = await unitOfWork.Comment.All();
                var commentsByPostId = comments.Where(p => p.PostId == id);

                if (comments == null)
                {
                    return BadRequest();
                }

                return Ok(commentsByPostId);
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> Post(Comment comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }

               try
            {
                comment.CommentDate = DateTime.Now;
                await unitOfWork.Comment.Add(comment); 
                await unitOfWork.CompleteAsync();
            } catch (Exception ex) { return BadRequest(ex); }
            return Ok(comment);
        }

        [HttpPut]
        public async Task<ActionResult<Comment>> Edit(Comment comment)
        {
            var oldComment = await GetById(comment.Id);
            if (oldComment == null)
            {
                return BadRequest();
            }

            try
            {
                await unitOfWork.Comment.Add(comment);
                await unitOfWork.CompleteAsync();
            } catch (Exception ex) { return BadRequest(ex); }

            return Ok(oldComment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> Delete(int id)
        {
            var comment = await GetById(id);
            if (comment == null) return BadRequest();
            try
            {
                await unitOfWork.Comment.Delete(comment);
                await unitOfWork.CompleteAsync();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Comment has been deleted!");
        }

        public async Task<Comment> GetById(int id)
        {
            var comment = await unitOfWork.Comment.GetById(id);
            return comment;
        }
    }
}
