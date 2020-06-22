using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Collections.Generic
{
    public class ListWithReadOnly<T> : List<T>, IReadOnlyList<T>
    {
        public ListWithReadOnly() { }

        public ListWithReadOnly(int capacity) : base(capacity) { }

        public ListWithReadOnly(IEnumerable<T> collection) : base(collection) { }
    }
}
