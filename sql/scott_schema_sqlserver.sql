/* =====================================================================
   SCOTT / TIGER schema  --  SQL Server (T-SQL) edition
   The classic Oracle EMP/DEPT tables, faithfully reproduced.
   The ASP.NET app creates and seeds these automatically via EF Core;
   this script is here for reference and for running straight against
   SQL Server Management Studio (SSMS) if you prefer.
   ===================================================================== */

IF OBJECT_ID('dbo.EMP',  'U') IS NOT NULL DROP TABLE dbo.EMP;
IF OBJECT_ID('dbo.DEPT', 'U') IS NOT NULL DROP TABLE dbo.DEPT;
GO

CREATE TABLE dbo.DEPT (
    DEPTNO  INT           NOT NULL PRIMARY KEY,
    DNAME   VARCHAR(14)   NULL,
    LOC     VARCHAR(13)   NULL
);
GO

CREATE TABLE dbo.EMP (
    EMPNO    INT           NOT NULL PRIMARY KEY,
    ENAME    VARCHAR(10)   NULL,
    JOB      VARCHAR(9)    NULL,
    MGR      INT           NULL,
    HIREDATE DATE          NULL,
    SAL      DECIMAL(7,2)  NULL,
    COMM     DECIMAL(7,2)  NULL,
    DEPTNO   INT           NULL,
    CONSTRAINT FK_EMP_DEPT FOREIGN KEY (DEPTNO) REFERENCES dbo.DEPT(DEPTNO)
);
GO

INSERT INTO dbo.DEPT (DEPTNO, DNAME, LOC) VALUES
 (10, 'ACCOUNTING', 'NEW YORK'),
 (20, 'RESEARCH',   'DALLAS'),
 (30, 'SALES',      'CHICAGO'),
 (40, 'OPERATIONS', 'BOSTON');
GO

INSERT INTO dbo.EMP (EMPNO, ENAME, JOB, MGR, HIREDATE, SAL, COMM, DEPTNO) VALUES
 (7369, 'SMITH',  'CLERK',     7902, '1980-12-17',  800,  NULL, 20),
 (7499, 'ALLEN',  'SALESMAN',  7698, '1981-02-20', 1600,   300, 30),
 (7521, 'WARD',   'SALESMAN',  7698, '1981-02-22', 1250,   500, 30),
 (7566, 'JONES',  'MANAGER',   7839, '1981-04-02', 2975,  NULL, 20),
 (7654, 'MARTIN', 'SALESMAN',  7698, '1981-09-28', 1250,  1400, 30),
 (7698, 'BLAKE',  'MANAGER',   7839, '1981-05-01', 2850,  NULL, 30),
 (7782, 'CLARK',  'MANAGER',   7839, '1981-06-09', 2450,  NULL, 10),
 (7788, 'SCOTT',  'ANALYST',   7566, '1987-04-19', 3000,  NULL, 20),
 (7839, 'KING',   'PRESIDENT', NULL, '1981-11-17', 5000,  NULL, 10),
 (7844, 'TURNER', 'SALESMAN',  7698, '1981-09-08', 1500,     0, 30),
 (7876, 'ADAMS',  'CLERK',     7788, '1987-05-23', 1100,  NULL, 20),
 (7900, 'JAMES',  'CLERK',     7698, '1981-12-03',  950,  NULL, 30),
 (7902, 'FORD',   'ANALYST',   7566, '1981-12-03', 3000,  NULL, 20),
 (7934, 'MILLER', 'CLERK',     7782, '1982-01-23', 1300,  NULL, 10);
GO

/* The classic sanity check every Oracle dev ran a thousand times: */
-- SELECT * FROM dbo.EMP ORDER BY EMPNO;
-- SELECT d.DNAME, COUNT(*) AS HEADCOUNT, SUM(e.SAL) AS PAYROLL
--   FROM dbo.EMP e JOIN dbo.DEPT d ON e.DEPTNO = d.DEPTNO
--  GROUP BY d.DNAME ORDER BY PAYROLL DESC;
