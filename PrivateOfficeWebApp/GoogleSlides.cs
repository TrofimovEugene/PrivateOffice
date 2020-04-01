using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Slides.v1;
using Google.Apis.Slides.v1.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PrivateOfficeWebApp.Pages.Teacher.Classes
{
    public class GoogleSlides
    {

        static string[] Scopes = { SlidesService.Scope.Presentations };
        static string ApplicationName = "PrivateOfficeWebApp";

        public void Slides()
        {
            UserCredential credential;

            using (var stream =
            new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "admin",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;
            }

            // Create Google Slides API service. 
            var service = new SlidesService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var presentationId = "1EAYk18WDjIG-zp_0vLm3CsfQh_i8eXc67Jo2O9C6Vuc";
            Presentation presentation1 = new Presentation();

            presentation1.Title = "planClasses";

            var request = service.Presentations.Create(presentation1);
            request.Fields = presentationId;
            request.Execute();
        }
    }
}