using Car_Picker_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_Picker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupsController : ControllerBase
    {
        private readonly ILookupInterface _lookupInterface;
        public LookupsController(ILookupInterface lookupInterface)
        {
            _lookupInterface = lookupInterface;
        }


        [HttpGet("lookups-by-type/{typeId}")]
        public async Task<IActionResult> GetLookups([FromRoute] int typeId)
        {
            try
            {
                var response = await _lookupInterface.GetLookupItemsByTypeId(typeId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
