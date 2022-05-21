using Microsoft.AspNetCore.Mvc;

namespace API.Controllers{



// alttaki iki satiri  base class da tanimdaladigimiz icin  
// bu siniftan turetilen classlarda  tanimlamamiza gerek yok
[ApiController]
[Route("api/[controller]")]
    public class BaseApiController: ControllerBase{
        

        
    }
}