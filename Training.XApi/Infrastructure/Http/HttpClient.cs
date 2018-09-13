using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Training.XApi.Engine.Models.Adverts;
using Training.XApi.Engine.Models.Members;

namespace Training.XApi.Infrastructure.Http
{
    public class HttpClient : IHttpClient
    {
        public HttpClient()
        {
            
        }
        
        public async Task<HttpClientResponse<T>> ExecuteAsync<T>(HttpClientRequest request)
        {
            HttpClientResponse<T> response = new HttpClientResponse<T>(WebExceptionStatus.Success);
            response.StatusCode = HttpStatusCode.OK;

            Type requestType = typeof(T);

            if (requestType == typeof(List<Advert>))
            {
                response.Result = (T)Convert.ChangeType(MockAdverts(), typeof(T));
            }
            else if (requestType == typeof(Advert))
            {
                response.Result = (T)Convert.ChangeType(MockAdvert(), typeof(T));
            }
            else if (requestType == typeof(MemberProfile))
            {
                response.Result = (T)Convert.ChangeType(MockProfile(), typeof(T));
            }

            return response;
        }

        private List<Advert> MockAdverts()
        {
            var adverts = new List<Advert>();

            adverts.Add(MockAdvert());

            return adverts;
        }

        private Advert MockAdvert()
        {
            return new Advert()
            {
                AdvertId = Guid.Parse("26be265c-aa6a-44b9-b210-e8b23cb3b427"),
                AdvertReference = "SSE-AD-3519933",
                Status = Engine.Enums.AdvertStatus.Approved,
                DateFirstApproved = DateTime.Now.AddDays(-10),
                DateCancelled = null,
                DateCreated = DateTime.Now.AddDays(-11),
                DateLastApproved = DateTime.Now.AddDays(-5),
                MemberId = Guid.Parse("430d7a19-efd4-441c-88c3-00e30b260293"),
                Vertical = Engine.Enums.Vertical.Car,
                Title = "2009 Holden Commodore",
                Subtitle = "VE SS V Sportwagon 5dr Man 6sp 6.0i [MY09.5]",
                EditingData = new AdvertData()
                {
                    SpecId = "232169"
                },
                LiveData = new AdvertData()
                {
                    SpecId = "232169"
                }
            };
        }

        private MemberProfile MockProfile()
        {
            return new MemberProfile()
            {
                City = "Cremorne",
                Email = "daniel.ward@carsales.com.au",
                FirstName = "Daniel",
                LastName = "Ward",
                Phone = "+61411222333",
                PostCode = "3121",
                ProfilePictureUrl = null,
                State = "VIC"
            };
        }
    }
}
