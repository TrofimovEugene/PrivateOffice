using Microsoft.EntityFrameworkCore;
using PrivateOfficeWebApp.Controllers;
using PrivateOfficeWebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace UnitTest.Tests
{
    
    public class CourseConroller
    {
        private PrivateOfficeWebAppContext _privateOfficeConext ;

        [Fact]
        public void GetTeasher()
        {
            var options = new DbContextOptionsBuilder<PrivateOfficeWebAppContext>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            using (_privateOfficeConext = new PrivateOfficeWebAppContext(options.Options))
            {
                var controller = new TeachersController(_privateOfficeConext);
                var result = controller.GetTeacher(1);
                Assert.Equal(0, _privateOfficeConext.Teacher.Count(p => p.Role == "admin"));
            }
        }
    }
}
