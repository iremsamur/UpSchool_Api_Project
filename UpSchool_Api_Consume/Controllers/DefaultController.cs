using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UpSchool_Api_Consume.Models;

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
                var result = JsonConvert.DeserializeObject<List<CategoryViewModel>>(jsondata);//json'ı model nesnesine oluşturuyor
                return View(result);
            }
            else
            {
                ViewBag.responseMessage = "Bir hata oluştu";
                return View();
            }
       
        }
        //post işlemi çağırma apiden
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryViewModel  p)
        {
            p.Status = true;

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(p);
            //Bu defa jsondan veri çekmiyorum json veri gönderdiğim post olduğu için serializeobject kullanılıyor
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:44362/api/Category",content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.responseMessage = "Bir hata ile karşılaşıldı";
                return View();
            }



       

        }
        //güncelleme işlemi
        //güncellemek için seçilen verinin bilgilerini getirme
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44362/api/Category/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CategoryViewModel>(jsonData);//veri getirildiği için deserialize
                return View(result);

            }
            else
            {
                ViewBag.responseMessage = "Bir hata ile karşılaşıldı.";
                return View();
            }
        }
        //güncelleme
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryViewModel p)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(p);
            var content = new StringContent(jsonData,Encoding.UTF8);
            //yine güncellemede yeni verileri gönderdiğimiz için
            //serialize oluyor
            var responseMessage = await client.PutAsync("https://localhost:44362/api/Category",content);//güncellemede put kullanılır.
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
           
        }
        //silme işlemi
        public async Task<IActionResult> DeleteCategory(int id)
        {

            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:44362/api/Category/{id}");
            return RedirectToAction("Index");
        }




    }
}
