using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TeamAssessnment.WebAPI.Models
{
    [DataContract]
    public class TeamAssignmentsResult
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "assignmentId")]
        public int AssignmentId { get; set; }
        [DataMember(Name = "value")]
        public double Value { get; set; }
    }
}