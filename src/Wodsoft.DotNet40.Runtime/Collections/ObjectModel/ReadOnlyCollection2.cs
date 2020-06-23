using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace System.Collections.ObjectModel
{
    public class ReadOnlyCollection2<T> : ReadOnlyCollection<T>, IReadOnlyCollection<T>
    {
        public ReadOnlyCollection2(IList<T> list) : base(list) { }
    }
}
