using htmos.branch;
using htmos.contract;
using htmos.data;
using htmos.dtos;
using htmos.model;
using Microsoft.EntityFrameworkCore;
namespace htmos.services.repository;

public class BranchRepository(DatabaseContext _context, IUserRepository userRepository) : IBranchRepository
{
    public async Task<Branch> GetBranchAsync(int ID)
    {
        BranchAdmin? branchAdmin = await _context.BranchAdmins.Include(ba => ba.Branch).
        ThenInclude(b => b.Members).
        FirstOrDefaultAsync(ba => ba.User.ID == ID);
        return branchAdmin?.Branch!;
    }
    public async Task<Branch> GetBranchAsyncDeparment(int ID)
    {
        BranchAdmin? branchAdmin = await _context.BranchAdmins.AsNoTracking().Include(ba => ba.Branch).
        ThenInclude(b => b.Departments).
        FirstOrDefaultAsync(ba => ba.User.ID == ID);
        return branchAdmin?.Branch!;
    }

    public async Task<IEnumerable<object>> GetBranchDepartmentWorkersAsync(int ID, int departmentID)
    {
        BranchAdmin? branchAdmin = await _context.BranchAdmins.AsNoTracking().Include(ba => ba.Branch).
         ThenInclude(b => b.Departments).ThenInclude(d => d.Workers).
         FirstOrDefaultAsync(ba => ba.User.ID == ID);
        Department? department = branchAdmin?.Branch.Departments.FirstOrDefault(d => d.ID == departmentID);
        return department!.Workers.Select(w => new { Name = userRepository.GetUserIDAsync(w.MemberID).Result })!;
    }

    public async Task<List<BranchAdmin>> GetBranchesAsync()
    {
        return [.. _context.BranchAdmins.AsNoTracking().Include(ba => ba.Branch).ThenInclude(b => b.BranchAdmin)];
    }

    public async Task<bool> AddMember(int AdminID, MemberDTO member)
    {
        try
        {
            Branch branch = await GetBranchAsync(AdminID);
            Member member_ = new() { Name = member.Name, Phone = member.Phone, BranchID = branch.ID, IsFirstTimer = member.IsFirstTimer };
            branch.Members.Add(member_); await _context.SaveChangesAsync();
            return true;
        }
        catch (System.Exception)
        {
            throw;
        }

    }
    public async Task<BoolResult> OnboardWorker(int AdminID, WorkerDTO workerDTO)
    {
        try
        {
            Branch branch = await GetBranchAsync(AdminID);
            Worker worker = new() { MemberID = workerDTO.MemberID, DepartmentID = workerDTO.DepartmentID };
            Worker? IsExistingWorker = branch.Workers.FirstOrDefault(w => w.MemberID == workerDTO.MemberID);
            if (IsExistingWorker != null) return new BoolResult(false, "Worker already onboard");

            branch.Workers.Add(worker);
            await _context.SaveChangesAsync();
            return new BoolResult(true, "Department created successfully");

        }
        catch (System.Exception)
        {
            throw;
        }
    }
    public async IAsyncEnumerable<Member> GetFirstTimersAsync()
    {

        List<Member> members = [.. _context.Members.Where(m => m.IsFirstTimer == true)];
        if (members.Count < 1) yield return null!;

        Queue<Member> QueuedMembers = [];

        foreach (Member member in members)
        {
            QueuedMembers.Enqueue(member);
        }
        while (QueuedMembers.Count > 0)
        {
            yield return QueuedMembers.Dequeue();
            await Task.Delay(TimeSpan.FromMinutes(1));
        }
    }

    public async Task<BoolResult> AddDepartmentAsync(DepartmentDTO department_, int AdminID)
    {
        try
        {
            Branch branch = await GetBranchAsync(AdminID);
            Department? IsExisting = branch.Departments.FirstOrDefault(d => d.Name == department_.Name);
            if (IsExisting != null) return new BoolResult(false, "Department already created");


            Department department = new() { Name = department_.Name };
            branch.Departments.Add(department);
            await _context.SaveChangesAsync();
            return new BoolResult(true, "Department  created succesfully");
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
