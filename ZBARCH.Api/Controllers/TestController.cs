using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZBARCH.Services.Abstract;

namespace ZBARCH.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TestController> _logger;

        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }
        /// <summary>
        /// Deletes the test in table
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete, Route("DeleteTest/{testId}")]
        public async Task<IActionResult> DeleteTest([FromRoute] Guid testId)
        {
            var entry = await _testService.ListStringsAsync();
            if (entry == null)
            {
                _logger.LogError(String.Concat("Test Not Found", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
                return BadRequest("Test Not Found");
            }

            try
            {
                await _testService.Delete(testId);
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Concat(ex.Message, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
                return BadRequest(ex.Message);
            }

            return Ok(true);
        }
    }
}
