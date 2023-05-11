using Microsoft.AspNetCore.Mvc;
using Refit;
using ZBARHC.Dto;

namespace ZBARCH.Client
{
    public interface ITestClient
    {
        public interface IUserClient
        {
            [Post("/User/Login")]
            Task<IActionResult> Login([Body] TestDto testDto);

            [Post("/User/Register")]
            Task<IActionResult> Register([Body] TestDto testDto);
            [Get("/User/GetToken")]
            Task<string> GetToken();
        }
    }
}
