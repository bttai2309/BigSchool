using BigSchool.DTOs;
using BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigSchool.Controllers.Api
{
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;
        // GET: Followings
        public FollowingsController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Follow(FollowingDto followingDto)
        {
            var userId = User.Identity.GetUserId();
            var fol = _dbContext.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == followingDto.FolloweeId);
            if (fol)
                return BadRequest("Following already exists!");
            var folowing = new Following
            {
                FollowerId = userId,
                FolloweeId = followingDto.FolloweeId
            };
            _dbContext.Followings.Add(folowing);
            _dbContext.SaveChanges();

            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult Delete(FollowingDto followingDto)
        {
            var userId = User.Identity.GetUserId();
            var checkFolloweeId = _dbContext.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == followingDto.FolloweeId);
            if (!checkFolloweeId)
            {
                return BadRequest("Exist");
            }
            var following = _dbContext.Followings.Where(a => a.FollowerId == userId && a.FolloweeId == followingDto.FolloweeId).First();
            _dbContext.Followings.Remove(following);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}