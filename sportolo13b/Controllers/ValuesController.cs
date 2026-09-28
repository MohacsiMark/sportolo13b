using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace sportolo13b.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public string ConnectionString = "server=localhost;uid=root;password=;database=sportolo13b;";
    }
}
