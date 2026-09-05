using htmos.dtos;
using htmos.model;

namespace htmos.branch;

public interface IBranchRepository
{
    Task<List<BranchAdmin>> GetBranchesAsync();
    Task<Branch> GetBranchAsync(int ID);
    Task<bool> AddMember(int ID, MemberDTO member);
    IAsyncEnumerable<Member> GetFirstTimersAsync();
    Task<BoolResult> AddDepartmentAsync(DepartmentDTO department_, int AdminID);
    Task<BoolResult> OnboardWorker(int AdminID, WorkerDTO workerDTO);
    Task<Branch> GetBranchAsyncDeparment(int ID);
    Task<IEnumerable<object>> GetBranchDepartmentWorkersAsync(int ID, int departmentID);

}