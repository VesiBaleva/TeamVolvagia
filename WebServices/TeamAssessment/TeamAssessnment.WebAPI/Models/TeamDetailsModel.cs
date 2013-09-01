using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TeamAssessnment.WebAPI.Models
{
    [DataContract]
    public class TeamDetailsModel
    {
        [DataMember(Name = "id")]
        public  int Id { get; set; }
        [DataMember(Name = "user")]
        public  string User { get; set; }
        [DataMember(Name = "imageUrl")]
        public  string ImageUrl { get; set; }
        [DataMember(Name = "teamname")]
        public  string Teamname { get; set; }
        [DataMember(Name = "members")]
        public  IEnumerable<string> Members { get; set; }
        [DataMember(Name = "results")]
        public  IEnumerable<ResultsModel> Results { get; set; }
    }
}