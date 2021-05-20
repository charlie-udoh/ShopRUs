using Microsoft.AspNetCore.Mvc;
using ShopRUs.Web.Interfaces;
using ShopRUs.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopRUs.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> Get()
        {
            return Ok(await _customerService.GetAllCustomers());
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetById(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // GET: api/Customer/John
        [HttpGet("name/{name}")]
        public async Task<ActionResult<CustomerDTO>> GetByName(string name)
        {
            var customer = await _customerService.GetCustomerByName(name);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CustomerDTO customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var record = await _customerService.CreateCustomer(customer);
            return CreatedAtAction(nameof(GetById), new { id = record.Id }, record);
        }
    }
}
