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
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _dbContext;
        public AttendancesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto attendanceDto)
        {
            var userId = User.Identity.GetUserId();
            if (_dbContext.Attendances.Any(a => a.AttendeeId == userId && a.CourseId == attendanceDto.CourseId))
                return BadRequest("Người này đã tồn tại!");
            var attendance = new Attendance
            {
                CourseId = attendanceDto.CourseId,
                AttendeeId = userId
            };
            _dbContext.Attendances.Add(attendance);
            _dbContext.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult Delete(AttendanceDto attendanceDto)
        {
            var userId = User.Identity.GetUserId();
            if (!(_dbContext.Attendances.Any(a => a.AttendeeId == userId && a.CourseId == attendanceDto.CourseId)))
                return BadRequest("Exists");
            var attendance = _dbContext.Attendances.Where(a => a.AttendeeId == userId && a.CourseId == attendanceDto.CourseId).First();
            _dbContext.Attendances.Remove(attendance);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
