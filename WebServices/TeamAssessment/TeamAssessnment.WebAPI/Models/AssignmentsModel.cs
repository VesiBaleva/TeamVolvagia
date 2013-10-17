using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TeamAssessnment.WebAPI.Models
{
    [DataContract]
    public class AssignmentsModel
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "maxValue")]
        public double MaxValue { get; set; }
        [DataMember(Name = "id")]
        public int Id { get; set; }
    }
}