using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamAssessment.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Teamname { get; set; }

        public virtual ICollection<Member> Members { get; set; }

        public virtual ICollection<Result> Results { get; set; }

        public string ImageURL { get; set; }

        public virtual User User { get; set; }

        public Team()
        {
            this.Members = new HashSet<Member>();
            this.Results = new HashSet<Result>();
        }
    }
}
