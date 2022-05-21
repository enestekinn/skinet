using System.Collections.Generic;
using API.Erros;

namespace API.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {

public ApiValidationErrorResponse() : base(400){

}
public IEnumerable<string> Errors {get; set;}
    }
}

/*
 Hatayi olusturan esas sinif Api Controller sinifi orada gelen parametreyi kontrol ediyor ve model state e gonderiyor.
Swagger  client side  belli bir url e istek attigi o url in ne ise yaradigi hakkinda bilgi veriyor. Swagger .Net5 ile varsayilan olarak geliyor
Swager icin asagidakileri ekledik.
  <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="5.0.3" />
   <PackageReference Include="Microsoft.AspNetCore.Authentication.OpenIdConnect" Version="5.0.3" />


*/