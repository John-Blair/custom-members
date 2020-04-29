using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace JB.Helpers
{
    public  class SemaphoreUtils
    {
        private Semaphore _semaphore2;
        public void Create (int initialCount, int maximumCount, string name, out bool createdNew)
        {
            _semaphore2 = new System.Threading.Semaphore(initialCount, maximumCount, name, out createdNew);
        }

    }
}
