using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelChangeTracking
{
    public enum EEditState
    {
        NotChanged = 0,
        Changed = 1,
        New = 2,
        Deleted = 3
    }
}
