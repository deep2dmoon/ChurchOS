using System.Security.Claims;
using htmos.branch;
using htmos.contract;
using htmos.dtos;
using htmos.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace htmos.controller;

[ApiController]
[Route("api/branch")]
public class BranchController(IBranchRepository branchRepository, IUserRepository userRepository) : ControllerBase
{
    [HttpGet("{id}/members")]
    [Authorize(Policy = "CanWrite")]
    public async Task<IActionResult> GetBranch(int id)
    {
        Branch branch = await branchRepository.GetBranchAsync(id);
        return Ok(branch);
    }

    [HttpGet("department/{departmentID}/workers")]
    [Authorize(Policy = "CanWrite")]
    public async Task<IActionResult> GetDepartmentsWorkers(int departmentID, int page, int pageSize)
    {

        int skip = (page - 1) * pageSize;
        int ID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        IEnumerable<object> workers = await branchRepository.GetBranchDepartmentWorkersAsync(ID, departmentID);

        return Ok(new
        {
            data = workers.Skip(skip).Take(pageSize).ToList(),
            page,
            pageSize,
            totalPages = (int)Math.Ceiling(workers.Count() / (double)pageSize)
        });
    }
    [HttpGet("departments")]
    [Authorize(Policy = "CanWrite")]
    public async Task<IActionResult> GetBranchDepartments(int id)
    {
        int ID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        Branch branch = await branchRepository.GetBranchAsyncDeparment(ID);
        return Ok(branch);
    }

    [HttpPost("new/member")]
    [Authorize(Policy = "CanWrite")]
    public async Task<IActionResult> AddMembers(MemberDTO member)
    {
        int ID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        bool IsAdded = await branchRepository.AddMember(ID, member);
        return IsAdded ? Ok(new { response = "New member successfully added" }) : StatusCode(500, "Internal server err");
    }

    [HttpGet("all")]
    [Authorize(Policy = "CanWrite")]
    public async Task<IActionResult> GetAllBranchesAysnc()
    {
        List<BranchAdmin> branches = await branchRepository.GetBranchesAsync();
        return Ok(branches.Select(b => new { b.Branch.ID, b.Branch.Name, Admin = userRepository.GetUserIDAsync(b.UserID).Result }));
    }

    [HttpPost("department/create")]
    [Authorize(Policy = "CanWrite")]
    public async Task<IActionResult> CreateDepartmentAsync(DepartmentDTO department)
    {
        int ID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        BoolResult result = await branchRepository.AddDepartmentAsync(department, ID);
        return Ok(result);
    }

    [HttpPost("department/onboard/worker")]
    [Authorize(Policy = "CanWrite")]
    public async Task<IActionResult> AddMembersAsync(WorkerDTO worker)
    {
        int ID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        BoolResult result = await branchRepository.OnboardWorker(ID, worker);
        return Ok(result);
    }
}