using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamAssessment.Models
{
    public class Result
    {
        [Key]
        public int Id { get; set; }

        public virtual Team Team { get; set; }

        public virtual Assignment Assignment { get; set; }

        public double Value { get; set; }

        public virtual User User { get; set; }

    }
}
