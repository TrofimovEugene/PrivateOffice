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
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;
            }

            // Create Google Slides API service. 
            var service = new SlidesService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            Presentation presentation = new Presentation();

             presentation.Title = "Командная разработка";

            PresentationsResource.CreateRequest request = service.Presentations.Create(presentation);
            presentation = request.Execute();

            var requestCreateSlides = new List<Request>();
            requestCreateSlides.Add(new Request()
            {
                CreateSlide = new CreateSlideRequest()
                {
                    SlideLayoutReference = new LayoutReference()
                    {
                        PredefinedLayout = "TITLE_AND_BODY"
                    }
                }
            });
            var bodySlides = new BatchUpdatePresentationRequest();

            bodySlides.Requests = requestCreateSlides;
            var response = service
                .Presentations
                .BatchUpdate(bodySlides, presentation.PresentationId)
                .Execute();

            PresentationsResource.GetRequest getPresentation = service.Presentations.Get(presentation.PresentationId);
            var presentationSlides = getPresentation.Execute();
            
            IList<Page> slides = presentationSlides.Slides;
            var requestAddText = new List<Request>();
            for (int i = 0; i< slides.Count; i++)
            { 
                var slide = slides[i];
               var objectIdSlides =  slide.PageElements[0].ObjectId;
                requestAddText.Add(new Request()
                {
                    InsertText = new InsertTextRequest()
                    {
                        ObjectId = objectIdSlides,
                        Text ="Блок"
                    }
                });

                bodySlides.Requests = requestAddText;
                response = service
                   .Presentations
                   .BatchUpdate(bodySlides, presentation.PresentationId)
                   .Execute();
            }
        }
    }
}