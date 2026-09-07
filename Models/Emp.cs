using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScottTiger.Models;

/// <summary>
/// The classic EMP table from the Oracle SCOTT schema.
/// EMPNO NUMBER(4) PK, ENAME VARCHAR2(10), JOB VARCHAR2(9), MGR NUMBER(4),
/// HIREDATE DATE, SAL NUMBER(7,2), COMM NUMBER(7,2), DEPTNO NUMBER(2) FK.
/// </summary>
[Table("EMP")]
public class Emp
{
    [Key]
    [Column("EMPNO")]
    public int EmpNo { get; set; }

    [Column("ENAME")]
    [MaxLength(10)]
    public string? EName { get; set; }

    [Column("JOB")]
    [MaxLength(9)]
    public string? Job { get; set; }

    [Column("MGR")]
    public int? Mgr { get; set; }          // nullable: KING (the PRESIDENT) has no manager

    [Column("HIREDATE")]
    public DateTime? HireDate { get; set; }

    [Column("SAL", TypeName = "decimal(7,2)")]
    public decimal? Sal { get; set; }

    [Column("COMM", TypeName = "decimal(7,2)")]
    public decimal? Comm { get; set; }     // nullable: only SALESMEN carry commission

    [Column("DEPTNO")]
    public int? DeptNo { get; set; }

    [ForeignKey(nameof(DeptNo))]
    public Dept? Dept { get; set; }
}
