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
    public class AssignmentsController : BaseApiController
    {
        [HttpGet]
        public IQueryable<AssignmentsModel> GetAll()
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

                var assignmentEntities = context.Assignments;
                var models =
                    (from assignmentEntity in assignmentEntities
                     where assignmentEntity.User.Id == user.Id
                     select new AssignmentsModel()
                     {
                          Name = assignmentEntity.Name,
                          MaxValue = assignmentEntity.MaxValue
                     });
                return models;
            });


            return responseMsg;
        }

        [HttpGet]
        public IQueryable<AssignmentsModel> GetById(int categoryId)
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

                var assignmentEntities = context.Assignments;
                var models =
                    (from assignmentEntity in assignmentEntities
                     where assignmentEntity.User.Id == user.Id && assignmentEntity.Category.Id == categoryId
                     select new AssignmentsModel()
                     {
                         Name = assignmentEntity.Name,
                         MaxValue = assignmentEntity.MaxValue
                     });
                return models;
            });

            return responseMsg;
        }
    }
}
