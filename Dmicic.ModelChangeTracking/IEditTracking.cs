using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ModelChangeTracking
{
    public interface IEditTracking : IChangeTracking, ITrackableObject
    {
        EEditState State { get; set; }
    }
}
