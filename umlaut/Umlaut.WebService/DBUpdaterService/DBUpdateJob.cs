using Quartz;
using System.Threading.Tasks;
using Umlaut.Database;
using Umlaut.WebService.DBUpdaterService.DBUpdaters;

namespace Umlaut.WebService.DBUpdaterService
{
    [DisallowConcurrentExecution]
    public class DBUpdateJob : IJob
    {
        private GraduateDBUpdater _graduateUpdater;
        private StatisticsDBUpdater _statisticsUpdater;
        public DBUpdateJob(GraduateDBUpdater updater, StatisticsDBUpdater statisticsUpdater)
        {
            _graduateUpdater = updater;
            _statisticsUpdater = statisticsUpdater;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var t = new Task(async () =>
            {
                await _graduateUpdater.Update();
                await _statisticsUpdater.Update();
            } );
            t.Start();
            return Task.CompletedTask;
        }
    }
}
