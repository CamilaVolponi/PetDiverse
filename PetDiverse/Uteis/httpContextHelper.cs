using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace PetDiverse.Uteis
{
    public static class httpContextHelper
    {
        public static bool IsAjaxRequest (this HttpContext httpContext){
            return httpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            
        }
    }
}
