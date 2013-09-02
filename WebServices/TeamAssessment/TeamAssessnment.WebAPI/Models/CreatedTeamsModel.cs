using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using TeamAssessment.Models;

namespace TeamAssessnment.WebAPI.Models
{
    public class CreatedTeamsModel
    {
        public IEnumerable<Team> Teams { get; set; }
    }
}