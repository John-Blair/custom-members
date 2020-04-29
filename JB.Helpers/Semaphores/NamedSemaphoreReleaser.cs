using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JB.Helpers
{
    public class NamedSemaphoreReleaser : IDisposable
    {
        private readonly Semaphore _semaphore;
        private GCHandle _handle;

        public NamedSemaphoreReleaser()
        {
            _semaphore = new Semaphore(1,1);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // NOTE: Leave out the finalizer altogether if this class doesn't 
        // own unmanaged resources itself, but leave the other methods
        // exactly as they are. 
        ~NamedSemaphoreReleaser()
        {
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _semaphore.Release();
                _semaphore.Dispose();
            }
            // free native resources here if there are any
            // critical
            _handle.Free();
        }
    }
}
