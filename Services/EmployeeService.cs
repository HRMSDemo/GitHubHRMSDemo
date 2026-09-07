using Microsoft.EntityFrameworkCore;
using ScottTiger.Data;
using ScottTiger.Models;

namespace ScottTiger.Services;

/// <summary>
/// Business-tier implementation. All employee rules and data access live
/// here; the controller stays thin and the front end never sees EF Core.
/// </summary>
public class EmployeeService : IEmployeeService
{
    private readonly ScottDbContext _db;

    public EmployeeService(ScottDbContext db) => _db = db;

    public async Task<IEnumerable<Emp>> GetAllAsync() =>
        await _db.Employees
                 .AsNoTracking()
                 .OrderBy(e => e.EmpNo)
                 .ToListAsync();

    public async Task<Emp?> GetAsync(int empNo) =>
        await _db.Employees.FindAsync(empNo);

    public async Task<Emp> CreateAsync(Emp emp)
    {
        _db.Employees.Add(emp);
        await _db.SaveChangesAsync();
        return emp;
    }

    public async Task<bool> UpdateAsync(int empNo, Emp emp)
    {
        if (empNo != emp.EmpNo) return false;
        if (!await _db.Employees.AnyAsync(e => e.EmpNo == empNo)) return false;

        _db.Entry(emp).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int empNo)
    {
        var emp = await _db.Employees.FindAsync(empNo);
        if (emp is null) return false;

        _db.Employees.Remove(emp);
        await _db.SaveChangesAsync();
        return true;
    }
}
