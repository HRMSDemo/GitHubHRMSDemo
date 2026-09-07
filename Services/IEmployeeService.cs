using ScottTiger.Models;

namespace ScottTiger.Services;

/// <summary>
/// Business (middle) tier contract. The API controllers depend on this
/// abstraction, not on EF Core directly — that separation is what makes
/// this a genuine 3-tier app rather than controllers talking straight to
/// the database.
/// </summary>
public interface IEmployeeService
{
    Task<IEnumerable<Emp>> GetAllAsync();
    Task<Emp?> GetAsync(int empNo);
    Task<Emp> CreateAsync(Emp emp);
    Task<bool> UpdateAsync(int empNo, Emp emp);
    Task<bool> DeleteAsync(int empNo);
}
