using htmos.contract;
using htmos.dtos;
namespace htmos.services;

public class AnnouncementService(IMemberRepository memberRepository)
{

    public async Task<BoolResult> BroadcastAllAsync(string announcement)
    {
        try
        {
            int BATCH_SIZE = 3;
            IEnumerable<string> Members = await memberRepository.GetMembersPhone();
            for (int i = 0; i < Members.Count(); i += BATCH_SIZE)
            {
                IEnumerable<string> members = [.. Members.Skip(i).Take(BATCH_SIZE)];
                // var result = await smsService.SendBulkSMS(members, announcement);
                Console.Write(new { announcement, members = members.Count() });
            }
            return new BoolResult(true, $"Announcement broadcasted to {Members.Count()} members at {DateTime.UtcNow}");
        }
        catch (System.Exception)
        {

            throw;
        }
    }
}