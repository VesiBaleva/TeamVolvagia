using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TeamAssessnment.WebAPI.Models
{
    [DataContract]
    public class CreateResultModel
    {
        [DataMember(Name = "assignmentsResult")]
        public IEnumerable<AssignmentsResults> AssignmentsResult { get; set; }
    }
}