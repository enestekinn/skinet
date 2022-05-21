using API.Erros;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{


    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi =true)]

    public class EnrrorController : BaseApiController
    {

        public IActionResult Error(int code){

            return new ObjectResult(new ApiResponse(code));
        }
        
    }
}
/* 
How do we get a request that's come into our API and get it passed to this particular controller?
We can do this in startup class

bu olmadan istek atarsak controllerdaki methodumuzun get mi post mu oldugunu anlamiyor
    [ApiExplorerSettings(IgnoreApi =true)]

 */