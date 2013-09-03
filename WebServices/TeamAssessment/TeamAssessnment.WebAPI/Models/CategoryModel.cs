using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TeamAssessnment.WebAPI.Models
{
    [DataContract]
    public class CategoryModel
    {
        [DataMember(Name = "assignments")]
        public IEnumerable<AssignmentsModel> Assignments { get; set; }
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}