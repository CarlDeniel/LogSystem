using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSystem
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string EmployeeId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool IsSuperUser { get; set; }
    }

    public class TimeLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public TimeSpan? RenderedTime { get; set; }
        public virtual User User { get; set; }
    }


}

