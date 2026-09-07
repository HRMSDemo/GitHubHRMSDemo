using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScottTiger.Models;

/// <summary>
/// The classic DEPT table from the Oracle SCOTT schema.
/// DEPTNO NUMBER(2) PK, DNAME VARCHAR2(14), LOC VARCHAR2(13).
/// </summary>
[Table("DEPT")]
public class Dept
{
    [Key]
    [Column("DEPTNO")]
    public int DeptNo { get; set; }

    [Column("DNAME")]
    [MaxLength(14)]
    public string? DName { get; set; }

    [Column("LOC")]
    [MaxLength(13)]
    public string? Loc { get; set; }

    // Navigation: one department has many employees.
    public ICollection<Emp> Employees { get; set; } = new List<Emp>();
}
