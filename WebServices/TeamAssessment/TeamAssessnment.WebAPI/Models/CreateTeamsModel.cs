using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace TeamAssessnment.WebAPI.Models
{
    [DataContract]
    public class CreateTeamsModel
    {
        [DataMember(Name = "teamnames")]
        public string Teamnames { get; set; }
        [DataMember(Name = "members1")]
        public string Members1 { get; set; }
        [DataMember(Name = "members2")]
        public string Members2 { get; set; }
    }
}