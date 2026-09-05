namespace htmos.contract;

public interface IMemberRepository
{
    Task<IEnumerable<string>> GetMembersPhone();
}