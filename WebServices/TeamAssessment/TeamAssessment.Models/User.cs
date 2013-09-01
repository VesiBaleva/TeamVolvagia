using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamAssessment.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Nickname { get; set; }

        [Required]
        public string AuthCode { get; set; }

        public string SessionKey { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Result> Results { get; set; }

        public User()
        {
            this.Teams = new HashSet<Team>();
            this.Categories = new HashSet<Category>();
            this.Assignments = new HashSet<Assignment>();
            this.Results = new HashSet<Result>();
        }
    }
}
