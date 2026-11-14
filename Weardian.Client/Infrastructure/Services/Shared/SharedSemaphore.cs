using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weardian.Client.Infrastructure.Services.Shared
{
    public class SharedSemaphore
    {
        public SemaphoreSlim RefreshLock { get; } = new SemaphoreSlim(1, 1);
    }
}
