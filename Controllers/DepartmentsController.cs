using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScottTiger.Data;
using ScottTiger.Models;

namespace ScottTiger.Controllers;

/// <summary>
/// Read-only surface for the DEPT table — used to populate the department
/// dropdown in the UI (10 ACCOUNTING / 20 RESEARCH / 30 SALES / 40 OPERATIONS).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly ScottDbContext _db;

    public DepartmentsController(ScottDbContext db) => _db = db;

    // GET /api/departments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Dept>>> GetAll() =>
        Ok(await _db.Departments
                    .AsNoTracking()
                    .OrderBy(d => d.DeptNo)
                    .ToListAsync());
}
