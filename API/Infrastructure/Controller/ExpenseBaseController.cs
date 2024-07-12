using API.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Infrastructure.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExpenseBaseController : ControllerBase
{
    public readonly ExpenseContext _context;

    public ExpenseBaseController(ExpenseContext context) { _context = context; }
}
