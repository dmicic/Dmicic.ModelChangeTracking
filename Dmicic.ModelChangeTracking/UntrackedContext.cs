using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelChangeTracking
{
    public class UntrackedContext<T> : IDisposable where T : ITrackableObject
    {
        public T Subject { get; private set; }

        private UntrackedContext(T subject)
        {
            this.Subject = subject;
        }

        public static UntrackedContext<T> Untrack(T subject)
        {
            var handler = new UntrackedContext<T>(subject);
            handler.Untrack();
            return handler;
        }

        private void Untrack()
        {
            this.Subject.Track = false;
        }

        private void Track()
        {
            this.Subject.Track = true;
        }

        public void Dispose()
        {
            this.Track();
        }
    }
}
