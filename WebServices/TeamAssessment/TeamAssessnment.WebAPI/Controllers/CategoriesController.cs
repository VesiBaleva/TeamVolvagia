﻿using System;
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
    public class CategoriesController : BaseApiController
    {
        public HttpResponseMessage PostCategories(CreateAssignmentsWithCategories model)
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

                    List<Assignment> assignmentsList = new List<Assignment>();

                    if (!(model.Assignment == null))
                    {
                        foreach (var assignmentItem in model.Assignment)
                        {
                            var newAssignment = new Assignment()
                            {
                                User = user,
                                Name = assignmentItem.Name,
                                MaxValue = assignmentItem.MaxValue
                            };

                            assignmentsList.Add(newAssignment);
                        }

                        var categoryEntity = new Category()
                        {
                            Assignments = assignmentsList,
                            User = user,
                            Name = model.CategoryName
                        };

                        dbContext.Categories.Add(categoryEntity);
                        dbContext.SaveChanges();
                        
                    }

                    var ret = Request.CreateResponse(HttpStatusCode.OK);

                    // var ret = Request.CreateResponse(HttpStatusCode.Created);
                    return ret;
                }
            });
            return responseMessage;
        }
    }
}