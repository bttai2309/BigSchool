using BigSchool.Models;
using System;
using Microsoft.Build.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace BigSchool.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Trường")]
        public string Place { get; set; }
        [Required]
        [FutureDate]
        [DisplayName("Ngày khóa học")]
        public string Date { get; set; }
        [Required]
        [ValidTime]
        [DisplayName("Giờ khóa học")]
        public string Time { get; set; }
        [Required]
        public string Lecturer { get; set; }
        [Required]
        [DisplayName("Chuyên ngành")]
        public byte Category { get; set; }
       
        public IEnumerable<Category> Categories { get; set; }
        public string Heading { get; set; }
        public string Action
        {
            get { return (Id != 0) ? "Update" : "Create"; }
        }
        public DateTime GetDateTime()
        {
            String dateTimeStr = Date + " " + Time;

            DateTime dateTime = DateTime.ParseExact(dateTimeStr, "dd/MM/yyyy hh:mm", null);

            return dateTime;
            //return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }

        public IEnumerable<Course> UpcommingCourses { get; set; }
        public bool ShowAction { get; set; }

        
    }
}