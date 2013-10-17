using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeamAssessment.Models;
using TeamAssessnment.Data;
using TeamAssessnment.WebAPI.Models;

namespace TeamAssessnment.WebAPI.Controllers
{
    public class TeamsController : BaseApiController
    {
        public HttpResponseMessage PostTeams(CreateTeamsModel model)
        {
            var responseMessage = this.PerformOperationAndHandleExceptions(() =>
            {
                var sessionKey = this.GetHeaderValue(Request.Headers, "sessionKey");
                var dbContext = new TeamAssessnmentContext();
                // dbContext.Configuration.ProxyCreationEnabled = false;

                using (dbContext)
                {
                    var user = dbContext.Users.FirstOrDefault(usr => usr.SessionKey == sessionKey);
                    if (user == null)
                    {
                        throw new ArgumentException("Users must be logged when create a new post!");
                    }

                    if (!(model.Teamnames == null))
                    {
                        string[] teamList = model.Teamnames.Split(new char[] { '\r', '\n', '\t'}, StringSplitOptions.RemoveEmptyEntries);
                        string[] membList1 = model.Members1.Split(new char[] { '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] membList2 = model.Members2.Split(new char[] { '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i < teamList.Length; i++)
			            {
                            List<Member> newMember = new List<Member>();

                            var memberEntity = new Member()
                            {
                                Name = membList1[i].Trim()
                            };

                            newMember.Add(memberEntity);

                            memberEntity = new Member()
                            {
                                Name = membList2[i].Trim()
                            };

                            newMember.Add(memberEntity);

                            var teamEntity = new Team()
                            {
                                Teamname = teamList[i].Trim(),
                                User = user,
                                Members = newMember
                            };

                            dbContext.Teams.Add(teamEntity);
                            dbContext.SaveChanges();
			            }
                     }


                    var createdTeamsModel = this.GetAll();

                  //  var ret = Request.CreateResponse(HttpStatusCode.OK);

                    var ret = Request.CreateResponse(HttpStatusCode.Created,createdTeamsModel);
                    return ret;
                }
            });
            return responseMessage;
        }

        [HttpGet]
        public IQueryable<TeamDetailsModel> GetAll()
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(() =>
            {
                var sessionKey = this.GetHeaderValue(Request.Headers, "sessionKey");
                var context = new TeamAssessnmentContext();

                var user = context.Users.FirstOrDefault(usr => usr.SessionKey == sessionKey);
                if (user == null)
                {
                    throw new InvalidOperationException("Invalid username or password");
                }

                var teamEntities = context.Teams;
                var models =
                    (from teamEntity in teamEntities
                     where teamEntity.User.Id==user.Id
                     select new TeamDetailsModel()
                     {
                         Id = teamEntity.Id,
                         Teamname = teamEntity.Teamname,
                         User = teamEntity.User.Nickname,
                         ImageUrl = teamEntity.ImageURL,
                         Members = (from membEntity in teamEntity.Members
                                    select membEntity.Name),

                         Results = (from resultEntity in teamEntity.Results
                                    select new ResultsModel()
                                    {
                                        CategoryName = resultEntity.Assignment.Category.Name,
                                        AssignmentName = resultEntity.Assignment.Name,
                                        Value = resultEntity.Value
                                    })

                     });
                return models;
            });

            return responseMsg;
        }

        [HttpGet]
        public IQueryable<TeamDetailsModel> GetById(int teamId)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(() =>
            {
                var sessionKey = this.GetHeaderValue(Request.Headers, "sessionKey");
                var context = new TeamAssessnmentContext();

                var user = context.Users.FirstOrDefault(usr => usr.SessionKey == sessionKey);
                if (user == null)
                {
                    throw new InvalidOperationException("Invalid username or password");
                }

                var models = this.GetAll().Where(tm => tm.Id == teamId);
                    
                return models;
            });

            return responseMsg;
        }

        [ActionName("result")]
        public HttpResponseMessage PostResultsOnTeam(int teamId, CreateResultModel model)
        {
            var responseMessage = this.PerformOperationAndHandleExceptions(() =>
            {
                var sessionKey = this.GetHeaderValue(Request.Headers, "sessionKey");
                var dbContext = new TeamAssessnmentContext();
                // dbContext.Configuration.ProxyCreationEnabled = false;

                using (dbContext)
                {
                    var user = dbContext.Users.FirstOrDefault(usr => usr.SessionKey == sessionKey);
                    if (user == null)
                    {
                        throw new ArgumentException("Users must be logged when create a new comment!");
                    }

                    var teamEntity = dbContext.Teams.FirstOrDefault(team => team.Id == teamId);

                    foreach (var assignmentItem in model.AssignmentsResult)
                    {
                        var assignmentEntity = dbContext.Assignments.FirstOrDefault(ass => ass.Name == assignmentItem.Name);
                        var newResult = new Result()
                        {
                            Team = teamEntity,
                            Value = assignmentItem.Value,
                            User = user,
                            Assignment = assignmentEntity,
                        };

                        dbContext.Results.Add(newResult);
                        dbContext.SaveChanges();

                    }



                    var response =
                          this.Request.CreateResponse(HttpStatusCode.OK);
                    return response;
                }
            });
            return responseMessage;

        }


        [ActionName("result")]
        public HttpResponseMessage PostResultOnAssignment(int teamId,int assignmentId, CreateOneResultModel model)
        {
            var responseMessage = this.PerformOperationAndHandleExceptions(() =>
            {
                var sessionKey = this.GetHeaderValue(Request.Headers, "sessionKey");
                var dbContext = new TeamAssessnmentContext();
                // dbContext.Configuration.ProxyCreationEnabled = false;

                using (dbContext)
                {
                    var user = dbContext.Users.FirstOrDefault(usr => usr.SessionKey == sessionKey);
                    if (user == null)
                    {
                        throw new ArgumentException("Users must be logged when create a new comment!");
                    }

                    var teamEntity = dbContext.Teams.FirstOrDefault(team => team.Id == teamId);

                    
                    var assignmentEntity = dbContext.Assignments.FirstOrDefault(ass => ass.Id == assignmentId );
                    var newResult = new Result()
                    {
                        Team = teamEntity,
                        Value = model.AssignmentResult,
                        User = user,                         
                        Assignment = assignmentEntity,
                    };

                    dbContext.Results.Add(newResult);
                    dbContext.SaveChanges();




                    var response =
                          this.Request.CreateResponse(HttpStatusCode.OK);
                    return response;
                }
            });
            return responseMessage;

        }
    }
}
