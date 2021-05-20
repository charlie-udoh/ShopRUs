using Microsoft.AspNetCore.Mvc;
using ShopRUs.Web.Interfaces;
using ShopRUs.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopRUs.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        // GET: api/Discount
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiscountDTO>>> Get()
        {
            return Ok(await _discountService.GetAllDiscounts());
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DiscountDTO>> GetById(int id)
        {
            var customer = await _discountService.GetDiscountById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // GET: api/Discount/staffmember
        [HttpGet("{type}")]
        public async Task<ActionResult<DiscountDTO>> GetByType(string type)
        {
            var discount = await _discountService.GetDiscountByType(type);
            if (discount == null)
            {
                return NotFound();
            }
            return Ok(discount);
        }

        // POST: api/Discount
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DiscountDTO discount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var record = await _discountService.CreateDiscount(discount);
            return CreatedAtAction(nameof(GetById), new { id = record.Id }, record);
        }
    }
}
