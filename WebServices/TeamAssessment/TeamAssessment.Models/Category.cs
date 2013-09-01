using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamAssessment.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual User User { get; set; }

        [Required]
        public virtual ICollection<Assignment> Assignments { get; set; }

        public Category()
        {
            this.Assignments = new HashSet<Assignment>();
        }
    }
}
