using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Service.Job
{
    public class JobService : IJobService
    {
        private readonly IJobService jobService;

        public JobService(IJobService _jobService)
        {
            jobService = _jobService;
        }

        public void sendWelcomeEmail()
        {

        }
    }
}
