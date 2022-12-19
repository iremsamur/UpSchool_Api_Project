using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UpSchool_WebApi.DAL;
using UpSchool_WebApi.DAL.Entities;

namespace UpSchool_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        //Bu ControllerBase'den miras aldığı için api controller oluyor
        //Controller'dan alsa view döndürebilirdi.


        Context context = new Context();
        [HttpGet] //veriyi çektiğim veri geldiği için HttpGet attribute'unu alır.

        public IActionResult CategoryList()
        {
            var values = context.Categories.ToList();
            return Ok(values);//işlem başarılı ise ok içinde veriyi döner. Ok durum kodu
        }
        //ekleme işlemi için post attribute'u kullanılır
        [HttpPost]
        public IActionResult CategoryAdd(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
            return Ok();
        }

        //HttpGet'in parametreli hali parametreye göre veri getirme
        //bu defa attribute parametreyi alır. HttpGet içindeki parametre adı ile
        //api fonksiyonun parametre adı aynı olmalı
        [HttpGet("{id}")]//Bu id ile parametredeki aynı olmalı
        public IActionResult CategoryGet(int id)
        {
            var values = context.Categories.Find(id);
            return Ok(values);
        }

        //silme işlemi
        [HttpDelete("{id}")]
        public IActionResult CategoryDelete(int id)
        {
            var values = context.Categories.Find(id);
            context.Categories.Remove(values);
            context.SaveChanges();
            return Ok();
        }
        [HttpPut] //güncelleme işlemi
        public IActionResult CategoryUpdate(Category category)
        {
            var values = context.Categories.Find(category.CategoryID);
            values.CategoryName = category.CategoryName;
            values.Description = category.Description;
            values.Status = category.Status;
            context.SaveChanges();
            return Ok();
        }
        
    }

}
