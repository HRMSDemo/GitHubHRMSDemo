using Microsoft.AspNetCore.Mvc;
using ScottTiger.Models;
using ScottTiger.Services;

namespace ScottTiger.Controllers;

/// <summary>
/// REST surface for the EMP table. Thin by design — it delegates every
/// operation to the business tier (IEmployeeService).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _svc;

    public EmployeesController(IEmployeeService svc) => _svc = svc;

    // GET /api/employees
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Emp>>> GetAll() =>
        Ok(await _svc.GetAllAsync());

    // GET /api/employees/7788
    [HttpGet("{empNo:int}")]
    public async Task<ActionResult<Emp>> Get(int empNo)
    {
        var emp = await _svc.GetAsync(empNo);
        return emp is null ? NotFound() : Ok(emp);
    }

    // POST /api/employees
    [HttpPost]
    public async Task<ActionResult<Emp>> Create(Emp emp)
    {
        var created = await _svc.CreateAsync(emp);
        return CreatedAtAction(nameof(Get), new { empNo = created.EmpNo }, created);
    }

    // PUT /api/employees/7788
    [HttpPut("{empNo:int}")]
    public async Task<IActionResult> Update(int empNo, Emp emp) =>
        await _svc.UpdateAsync(empNo, emp) ? NoContent() : NotFound();

    // DELETE /api/employees/7788
    [HttpDelete("{empNo:int}")]
    public async Task<IActionResult> Delete(int empNo) =>
        await _svc.DeleteAsync(empNo) ? NoContent() : NotFound();
}
