using System.Collections;
using htmos.contract;
using htmos.data;
using Microsoft.EntityFrameworkCore;

namespace htmos.services.repository;

public class MemberRepository(DatabaseContext _context) : IMemberRepository
{

    public async Task<IEnumerable<string>> GetMembersPhone()
    {

        return [.. _context.Members.AsNoTracking().Select(m => m.Phone)];

    }
}