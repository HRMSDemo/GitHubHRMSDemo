// ---- Tier 1 (Presentation): talks only to the ASP.NET API over fetch ----
const API = "/api";

let employees = [];
let departments = [];
let editingEmpNo = null;   // null = creating, number = editing

// ---- DOM refs ----
const $ = (id) => document.getElementById(id);
const body = $("empBody");
const rowCount = $("rowCount");
const modal = $("modal");

// ---- load ----
async function loadDepartments() {
  const res = await fetch(`${API}/departments`);
  departments = await res.json();
  const sel = $("fDeptNo");
  sel.innerHTML = departments
    .map(d => `<option value="${d.deptNo}">${d.deptNo} &mdash; ${d.dName}</option>`)
    .join("");
}

async function loadEmployees() {
  const res = await fetch(`${API}/employees`);
  employees = await res.json();
  render();
}

function deptName(no) {
  const d = departments.find(x => x.deptNo === no);
  return d ? d.dName : "";
}

function fmtDate(iso) {
  if (!iso) return "";
  const d = new Date(iso);
  const mon = ["JAN","FEB","MAR","APR","MAY","JUN","JUL","AUG","SEP","OCT","NOV","DEC"][d.getMonth()];
  return `${String(d.getDate()).padStart(2,"0")}-${mon}-${d.getFullYear()}`;
}

function fmtNum(n) {
  if (n === null || n === undefined) return "";
  return Number(n).toLocaleString("en-US", { minimumFractionDigits: 0 });
}

function render() {
  body.innerHTML = employees.map(e => `
    <tr>
      <td class="empno">${e.empNo}</td>
      <td>${e.eName ?? ""}</td>
      <td>${e.job ?? ""}</td>
      <td>${e.mgr ?? ""}</td>
      <td>${fmtDate(e.hireDate)}</td>
      <td class="num">${fmtNum(e.sal)}</td>
      <td class="num">${fmtNum(e.comm)}</td>
      <td>${e.deptNo ? `<span class="dept-tag">${e.deptNo} ${deptName(e.deptNo)}</span>` : ""}</td>
      <td class="actions">
        <button class="row-btn edit" data-edit="${e.empNo}">Edit</button>
        <button class="row-btn del"  data-del="${e.empNo}">Delete</button>
      </td>
    </tr>
  `).join("");
  rowCount.textContent = `${employees.length} row${employees.length === 1 ? "" : "s"}`;
}

// ---- modal handling ----
function openModal(emp) {
  $("formError").classList.add("hidden");
  if (emp) {
    editingEmpNo = emp.empNo;
    $("modalTitle").textContent = `Edit Employee ${emp.empNo}`;
    $("fEmpNo").value = emp.empNo;
    $("fEmpNo").disabled = true;             // PK is immutable on edit
    $("fEName").value = emp.eName ?? "";
    $("fJob").value = emp.job ?? "";
    $("fMgr").value = emp.mgr ?? "";
    $("fHireDate").value = emp.hireDate ? emp.hireDate.substring(0,10) : "";
    $("fSal").value = emp.sal ?? "";
    $("fComm").value = emp.comm ?? "";
    $("fDeptNo").value = emp.deptNo ?? "";
  } else {
    editingEmpNo = null;
    $("modalTitle").textContent = "New Employee";
    ["fEmpNo","fEName","fJob","fMgr","fHireDate","fSal","fComm"].forEach(id => $(id).value = "");
    $("fEmpNo").disabled = false;
    $("fDeptNo").selectedIndex = 0;
  }
  modal.classList.remove("hidden");
}

function closeModal() { modal.classList.add("hidden"); }

function readForm() {
  const val = (id) => $(id).value.trim();
  const numOrNull = (id) => val(id) === "" ? null : Number(val(id));
  return {
    empNo: Number(val("fEmpNo")),
    eName: val("fEName") || null,
    job: val("fJob") || null,
    mgr: numOrNull("fMgr"),
    hireDate: val("fHireDate") ? new Date(val("fHireDate")).toISOString() : null,
    sal: numOrNull("fSal"),
    comm: numOrNull("fComm"),
    deptNo: numOrNull("fDeptNo"),
  };
}

async function save() {
  const emp = readForm();
  const err = $("formError");
  if (!emp.empNo) { err.textContent = "EMPNO is required."; err.classList.remove("hidden"); return; }
  if (!emp.eName) { err.textContent = "ENAME is required."; err.classList.remove("hidden"); return; }

  try {
    let res;
    if (editingEmpNo === null) {
      res = await fetch(`${API}/employees`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(emp),
      });
    } else {
      res = await fetch(`${API}/employees/${editingEmpNo}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(emp),
      });
    }
    if (!res.ok) {
      err.textContent = `Save failed (HTTP ${res.status}). EMPNO may already exist.`;
      err.classList.remove("hidden");
      return;
    }
    closeModal();
    await loadEmployees();
  } catch (ex) {
    err.textContent = "Network error: " + ex.message;
    err.classList.remove("hidden");
  }
}

async function del(empNo) {
  const e = employees.find(x => x.empNo === empNo);
  if (!confirm(`Delete ${e?.eName} (EMPNO ${empNo})?`)) return;
  const res = await fetch(`${API}/employees/${empNo}`, { method: "DELETE" });
  if (res.ok) await loadEmployees();
  else alert(`Delete failed (HTTP ${res.status}).`);
}

// ---- events ----
$("btnNew").addEventListener("click", () => openModal(null));
$("btnClose").addEventListener("click", closeModal);
$("btnCancel").addEventListener("click", closeModal);
$("btnSave").addEventListener("click", save);
modal.addEventListener("click", (e) => { if (e.target === modal) closeModal(); });

body.addEventListener("click", (e) => {
  const editNo = e.target.getAttribute("data-edit");
  const delNo = e.target.getAttribute("data-del");
  if (editNo) openModal(employees.find(x => x.empNo === Number(editNo)));
  if (delNo) del(Number(delNo));
});

// ---- boot ----
(async function init() {
  await loadDepartments();
  await loadEmployees();
})();
