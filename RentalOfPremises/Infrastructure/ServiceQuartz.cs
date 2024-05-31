using Quartz;
using RentalOfPremises.Services.Jobs;

namespace RentalOfPremises.Api.Infrastructure
{
    public static class ServiceQuartz
    {
        public static void AddQuartz(this IServiceCollection services)
        {
            services.AddQuartz(options =>
            {
                options.UseMicrosoftDependencyInjectionJobFactory();
                var jobKey = JobKey.Create(nameof(RoomOccupiedJob));
                options.AddJob<RoomOccupiedJob>(JobKey.Create(nameof(RoomOccupiedJob)))
                .AddTrigger(trigger => trigger
                    .ForJob(jobKey).WithSimpleSchedule(schedule => schedule
                        .WithIntervalInMinutes(10).RepeatForever()));

            });
            services.AddQuartzHostedService();
        }
    }
}
