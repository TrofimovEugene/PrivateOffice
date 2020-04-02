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

        public void createSlides(string theme, string roll, string block)
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
            for (int i = 0; i <= 1; i++)
            {
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
            }
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

            var title = "";
            var subText = "";

            for (int indexSlide = 0; indexSlide < slides.Count; indexSlide++)
            { 
                var slide = slides[indexSlide];
                var objectIdSlidesTitle =  slide.PageElements[0].ObjectId;
                var objectIdSlidessubText = slide.PageElements[1].ObjectId;

                switch (indexSlide)
                {
                    case 0:
                        title = "Тема";
                        subText = theme;
                        break;
                    case 1:
                        title = "Опрос";
                        subText = roll;
                        break;
                    case 2:
                        title = "Блок";
                        subText = block;
                        break;
                }

                requestAddText.Add(new Request()
                {
                    InsertText = new InsertTextRequest()
                    {
                        ObjectId = objectIdSlidesTitle,
                        Text = title
                    }
                });

                requestAddText.Add(new Request()
                {
                    InsertText = new InsertTextRequest()
                    {
                        ObjectId = objectIdSlidessubText,
                        Text = subText
                    }
                });
                    
            }

            bodySlides.Requests = requestAddText;
            response = service
               .Presentations
               .BatchUpdate(bodySlides, presentation.PresentationId)
               .Execute();
        }
    }
}