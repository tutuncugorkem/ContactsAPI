using Contacts.Core.DTOs;
using ContactsAPI.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ContactsAPI.Controllers
{
    
    public class CustomBaseController : ControllerBase
    {
        //controller içerisinde endpointlerde status kod ve "ok" "bad request" gibi durumları burada kontrol edeceğiz. o taraf kirlenmesin diye
        [NonAction]  //endpoint zannetmesin
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == 204)
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode,
                };


            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };

        }

       
    }
}
