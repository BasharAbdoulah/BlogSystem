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
    public class LikeController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public LikeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ActionResult<Like>> Post(Like like)
        {
            if (like == null) return BadRequest();

            try
            {
                var likes = await unitOfWork.Like.All();
                var isExist = likes.Any(l => l.PostId == like.PostId && l.UserId == like.UserId);
                var likedPost = await unitOfWork.Post.GetById(like.PostId);

                if (isExist)
                {
                    return BadRequest("Already liked!");
                } else
                {
                likedPost.Likes++;
                    likedPost.CreationDate = DateTime.Now;
                await unitOfWork.Like.Add(like);
                await unitOfWork.CompleteAsync();
                }

            } catch (Exception ex) 
            { BadRequest(ex.Message); }

            return Ok();
        }

        //Get Likes by post id
        [HttpGet("LikesByPost/{id}")]
        public async Task<ActionResult<List<Like>>> Get(int id,int skip, int take)
        {

            try
            {
                var likes = await unitOfWork.Like.All();
                var likesByPost = likes.Where(l => l.PostId == id);
                List<Like> pagenation = likes.ToList();
                List<LikeModel> likesWithUsers = new List<LikeModel>();
                

                if (skip > 0)
                {
                    pagenation.RemoveRange(0, skip);
                }

                var takenLikes = pagenation.Take(take);

                foreach (var item in takenLikes)
                {
                    var targetUser = await unitOfWork.User.GetById(item.UserId);
                    var Like = new LikeModel
                    {
                        Like = item,
                        User = targetUser,
                        LikesCount = likesByPost.Count(),
                        
                    };
                    likesWithUsers.Add(Like);
                }

                return Ok(likesWithUsers);

            } catch (Exception ex) { return  BadRequest(ex.Message); }  
        }

        [HttpDelete]
        public async Task<ActionResult<Like>> Delete(Like likeFRequest)
        {
            try
            {
                var likes = await unitOfWork.Like.All();
                var like = likes.FirstOrDefault(l => l.UserId ==  likeFRequest.UserId && l.PostId == likeFRequest.PostId);
                if (like == null) return BadRequest();

                var likedPost = await unitOfWork.Post.GetById(like.PostId);
                likedPost.Likes--;

                await unitOfWork.Like.Delete(like);
                await unitOfWork.CompleteAsync();
                return Ok();
            } catch (Exception ex) { return  NotFound(ex.Message); }
        }
    }

    internal class LikeModel
    {
        public Like Like { get; set; }
        public User User { get; set; }
        public int LikesCount { get; set; }
    }

    internal class LikeResponse
    {
        Like Like { get; set; }
        User LikeOwner { get; set; }

    }
}
