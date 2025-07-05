using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FessorApi.Pages
{
    [Route("page/teste")]
    public class TesteController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var htmlContent = @"
<!DOCTYPE html>
<html lang=""pt-BR"">
<head>
    <meta charset=""UTF-8"">
    <title>Página de Teste</title>
</head>
<body>
    <h1>Bem-vindo à Página de Teste</h1>
    <p>Esta página web foi enviada por um controller ASP.NET Core.</p>
</body>
</html>";
            return Content(htmlContent, "text/html");
        }
    }
}