using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TeamAssessnment.WebAPI.Models
{
    [DataContract]
    public class AssignmentsResults
    {
        [DataMember(Name = "value")]
        public double Value { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}