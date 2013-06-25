using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelChangeTracking
{
    public interface ITrackableObject
    {
        bool Track { get; set; }
    }
}
