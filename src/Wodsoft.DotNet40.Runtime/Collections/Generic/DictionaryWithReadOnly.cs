using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace System.Collections.Generic
{
    public class DictionaryWithReadOnly<TKey, TValue> : Dictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>
    {
        public DictionaryWithReadOnly() { }
        public DictionaryWithReadOnly(int capacity) : base(capacity) { }
        public DictionaryWithReadOnly(IEqualityComparer<TKey> comparer) : base(comparer) { }
        public DictionaryWithReadOnly(IDictionary<TKey, TValue> dictionary) : base(dictionary) { }
        public DictionaryWithReadOnly(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer) { }
        public DictionaryWithReadOnly(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer) { }
        protected DictionaryWithReadOnly(SerializationInfo info, StreamingContext context) : base(info, context) { }

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => Keys;

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => Values;
    }
}
