using API.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Infrastructure.Controller;

[ApiController]
[Route("api/[controller]")]
public class ExpenseBaseController : ControllerBase
{
    public readonly ExpenseContext _context;

    public ExpenseBaseController(ExpenseContext context) { _context = context; }
}
