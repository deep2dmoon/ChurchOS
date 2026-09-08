using htmos.branch;
using htmos.model;

namespace htmos.services.workers;

public class FollowUp(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceScopeFactory.CreateScope();
            IBranchRepository branchRepository = scope.ServiceProvider.GetRequiredService<IBranchRepository>();

            await foreach (Member member in branchRepository.GetFirstTimersAsync())
            {
                if (member != null)
                {
                    int RETRY_COUNT = 3;
                    for (int i = 0; i < RETRY_COUNT; i++)
                    {
                        try
                        {
                            Console.WriteLine($"Processing sms for this {member.Phone} UTC: {DateTime.UtcNow}", stoppingToken);
                            break;
                        }
                        catch (System.Exception)
                        {
                            if (i == RETRY_COUNT - 1)
                            {
                                Console.WriteLine("Failed attempt", stoppingToken); // stop retrying at this point
                            }
                            else
                            {
                                Console.WriteLine("retrying");
                                await Task.Delay(TimeSpan.FromMinutes(RETRY_COUNT - 1), stoppingToken); // retry with an exponential back off
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"No new first timer yet :> UTC now {DateTime.UtcNow}", stoppingToken);
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
            }
        }
    }
}