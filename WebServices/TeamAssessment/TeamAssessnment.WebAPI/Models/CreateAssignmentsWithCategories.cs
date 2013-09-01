using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TeamAssessnment.WebAPI.Models
{
    [DataContract]
    public class CreateAssignmentsWithCategories
    {
        [DataMember(Name = "categoryName")]
        public string CategoryName { get; set; }
        [DataMember(Name = "assignments")]
        public IEnumerable<AssignmentsModel> Assignment { get; set; }

    }
}