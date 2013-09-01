using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TeamAssessnment.WebAPI.Models
{
    [DataContract]
    public class ResultsModel
    {
        [DataMember(Name = "categoryName")]
        public string CategoryName { get; set; }
        [DataMember(Name = "assignmentName")]
        public string AssignmentName { get; set; }
        [DataMember(Name = "value")]
        public double Value { get; set; }
    }
}