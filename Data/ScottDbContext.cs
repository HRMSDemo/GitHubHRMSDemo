using Microsoft.EntityFrameworkCore;
using ScottTiger.Models;

namespace ScottTiger.Data;

/// <summary>
/// EF Core context for the SCOTT schema. Seeds the four departments and the
/// fourteen employees exactly as they shipped with Oracle in the late '90s.
/// </summary>
public class ScottDbContext : DbContext
{
    public ScottDbContext(DbContextOptions<ScottDbContext> options) : base(options) { }

    public DbSet<Dept> Departments => Set<Dept>();
    public DbSet<Emp> Employees => Set<Emp>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        // ---- DEPT ---------------------------------------------------------
        mb.Entity<Dept>().HasData(
            new Dept { DeptNo = 10, DName = "ACCOUNTING", Loc = "NEW YORK" },
            new Dept { DeptNo = 20, DName = "RESEARCH",   Loc = "DALLAS"   },
            new Dept { DeptNo = 30, DName = "SALES",      Loc = "CHICAGO"  },
            new Dept { DeptNo = 40, DName = "OPERATIONS", Loc = "BOSTON"   }
        );

        // ---- EMP ----------------------------------------------------------
        mb.Entity<Emp>().HasData(
            new Emp { EmpNo=7369, EName="SMITH",  Job="CLERK",     Mgr=7902, HireDate=new DateTime(1980,12,17), Sal=800m,  Comm=null,  DeptNo=20 },
            new Emp { EmpNo=7499, EName="ALLEN",  Job="SALESMAN",  Mgr=7698, HireDate=new DateTime(1981, 2,20), Sal=1600m, Comm=300m,  DeptNo=30 },
            new Emp { EmpNo=7521, EName="WARD",   Job="SALESMAN",  Mgr=7698, HireDate=new DateTime(1981, 2,22), Sal=1250m, Comm=500m,  DeptNo=30 },
            new Emp { EmpNo=7566, EName="JONES",  Job="MANAGER",   Mgr=7839, HireDate=new DateTime(1981, 4, 2), Sal=2975m, Comm=null,  DeptNo=20 },
            new Emp { EmpNo=7654, EName="MARTIN", Job="SALESMAN",  Mgr=7698, HireDate=new DateTime(1981, 9,28), Sal=1250m, Comm=1400m, DeptNo=30 },
            new Emp { EmpNo=7698, EName="BLAKE",  Job="MANAGER",   Mgr=7839, HireDate=new DateTime(1981, 5, 1), Sal=2850m, Comm=null,  DeptNo=30 },
            new Emp { EmpNo=7782, EName="CLARK",  Job="MANAGER",   Mgr=7839, HireDate=new DateTime(1981, 6, 9), Sal=2450m, Comm=null,  DeptNo=10 },
            new Emp { EmpNo=7788, EName="SCOTT",  Job="ANALYST",   Mgr=7566, HireDate=new DateTime(1987, 4,19), Sal=3000m, Comm=null,  DeptNo=20 },
            new Emp { EmpNo=7839, EName="KING",   Job="PRESIDENT", Mgr=null, HireDate=new DateTime(1981,11,17), Sal=5000m, Comm=null,  DeptNo=10 },
            new Emp { EmpNo=7844, EName="TURNER", Job="SALESMAN",  Mgr=7698, HireDate=new DateTime(1981, 9, 8), Sal=1500m, Comm=0m,    DeptNo=30 },
            new Emp { EmpNo=7876, EName="ADAMS",  Job="CLERK",     Mgr=7788, HireDate=new DateTime(1987, 5,23), Sal=1100m, Comm=null,  DeptNo=20 },
            new Emp { EmpNo=7900, EName="JAMES",  Job="CLERK",     Mgr=7698, HireDate=new DateTime(1981,12, 3), Sal=950m,  Comm=null,  DeptNo=30 },
            new Emp { EmpNo=7902, EName="FORD",   Job="ANALYST",   Mgr=7566, HireDate=new DateTime(1981,12, 3), Sal=3000m, Comm=null,  DeptNo=20 },
            new Emp { EmpNo=7934, EName="MILLER", Job="CLERK",     Mgr=7782, HireDate=new DateTime(1982, 1,23), Sal=1300m, Comm=null,  DeptNo=10 }
        );
    }
}
