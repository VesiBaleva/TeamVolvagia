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

                        var categoryModel = new CategoryModel()
                        {
                            Id = categoryEntity.Id,
                            Name = categoryEntity.Name
                        };
                        
                        var response = this.Request.CreateResponse(HttpStatusCode.Created,categoryModel);
                        return response;
                }
            });
            return responseMessage;
        }

        [HttpGet]
        public IQueryable<CategoryModel> GetAll()
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

                var categoryEntities = context.Categories;
                var models =
                    (from categoryEntity in categoryEntities
                     where categoryEntity.User.Id == user.Id
                     select new CategoryModel()
                     {
                         Id = categoryEntity.Id,
                         Name = categoryEntity.Name,
                         Assignments = (from assEntity in categoryEntity.Assignments
                                    select new AssignmentsModel()
                                    {
                                        Name = assEntity.Name,
                                        MaxValue = assEntity.MaxValue
                                    })

                     });
                return models;
            });

            return responseMsg;
        }
    }
}
