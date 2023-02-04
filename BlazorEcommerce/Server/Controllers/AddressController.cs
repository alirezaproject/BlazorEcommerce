using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAddress()
        {
            return Ok(await _addressService.GetAddress());
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateAddress(Address address)
        {
            return Ok(await _addressService.AddOrUpdateAddress(address));
        }
    }
}
