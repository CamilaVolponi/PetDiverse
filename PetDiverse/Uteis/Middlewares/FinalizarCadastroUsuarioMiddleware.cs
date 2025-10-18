using PetDiverse.Controllers;
using PetDiverse.Data;
using System.Security.Claims;

namespace PetDiverse.Uteis.Middlewares
{
    public class FinalizarCadastroUsuarioMiddleware
    {
        private readonly RequestDelegate _next;

        public FinalizarCadastroUsuarioMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            // Verifica se o usuário está autenticado
            if (context.User?.Identity?.IsAuthenticated == true && !context.IsAjaxRequest())
            {
                var path = context.Request.Path.Value?.ToLower();
                var rotaRedirecionamento = "/pessoadoadora/create";
                var dbcontext = context.RequestServices.GetService<ApplicationDbContext>();
                if (!dbcontext.PessoaDoadora.Any(p => p.IdUsuario == context.User.FindFirstValue(ClaimTypes.NameIdentifier)) 
                    && !path.StartsWith(rotaRedirecionamento) && !path.Contains("/logout") && !path.StartsWith("/css") && !path.StartsWith("/js") && !path.StartsWith("/img"))
                {
                    context.Response.Redirect(rotaRedirecionamento);
                    return;
                }
            }
            await _next(context);
        }
    }
}
