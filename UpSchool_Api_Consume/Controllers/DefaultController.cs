using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace UpSchool_Api_Consume.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //apiye istekte bulunuyoruz
        //api consume
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();//talep oluşturur.
            var responseMessage = await client.GetAsync("https://localhost:44362/api/Category");//buraya bir tane
            //request url veriyoruz. Bu swagger'daki url'dir oradan alabiliriz
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //eğer apiden dönen kod respone status code Ok ise

                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                return View();
            }
            return View();
        }
    }
}
