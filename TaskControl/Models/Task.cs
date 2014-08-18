using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace TaskControl.Models
{


    public class Task
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Task description is required")]
        public string TaskDescription { get; set; }

        [Required(ErrorMessage = "Who is required")]
        public string Who { get; set; }

        public bool Do { get; set; }
    }

    public class TaskDBContext : DbContext
    {
       

        public DbSet<Task> Tasks { get; set; }
    }

    public class User
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "User name field is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password field is required")]
        public string Password { get; set; }
    }

    public class UserDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}