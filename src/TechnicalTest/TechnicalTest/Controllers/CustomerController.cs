using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechnicalTest.Service.Interface;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

namespace TechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        //GET: api/Customer/
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerService.GetCustomers();

            if (customers == null)
            {
                return NotFound();
            }

            return Ok(customers);
        }

        [HttpPost]
        [Route("Upload")]
        [EnableCors("MyPolicy")]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "ExcelFile");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var importResult = await _customerService.ImportCustomersFromExcel(fullPath, 2);
                    return Ok(new { response = importResult.Item1, message = importResult.Item2 });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
