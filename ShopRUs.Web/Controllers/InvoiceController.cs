using Microsoft.AspNetCore.Mvc;
using ShopRUs.Web.Models;
using ShopRUs.Web.Services;
using System.Threading.Tasks;

namespace ShopRUs.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        // GET: api/Invoice/1
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDTO>> GetById(int id)
        {
            var invoice = await _invoiceService.GetInvoiceById(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        // POST: api/Invoice
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] InvoiceDTO bill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _invoiceService.CreateInvoice(bill);
            if (!result.Successful)
                return BadRequest(result.Message);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result.Message);
        }
    }
}