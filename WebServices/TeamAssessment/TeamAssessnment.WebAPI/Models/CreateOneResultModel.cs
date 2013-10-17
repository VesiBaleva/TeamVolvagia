using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TeamAssessnment.WebAPI.Models
{
    [DataContract]
    public class CreateOneResultModel
    {
        [DataMember(Name = "assignmentResult")]
        public int AssignmentResult { get; set; }
    }
}