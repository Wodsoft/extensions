using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace System.Security.Cryptography
{
    /// <summary>
    /// Provides support for computing a hash or HMAC value incrementally across several segments.
    /// </summary>
    public sealed class IncrementalHash : IDisposable
    {
        private HashAlgorithm _hash;
        private bool _disposed;
        private MemoryStream _stream;

        /// <summary>
        /// Create an <see cref="IncrementalHash"/> for the algorithm specified by <paramref name="hashAlgorithm"/>.
        /// </summary>
        /// <param name="hashAlgorithm">The hash algorithm to perform.</param>
        public IncrementalHash(HashAlgorithm hashAlgorithm)
        {
            if (hashAlgorithm == null)
                throw new ArgumentNullException(nameof(hashAlgorithm));
            _hash = hashAlgorithm;
            _stream = new MemoryStream();
        }

        /// <summary>
        /// Get the name of the algorithm being performed.
        /// </summary>
        public HashAlgorithm Algorithm
        {
            get { return _hash; }
        }

        /// <summary>
        /// Append the entire contents of <paramref name="data"/> to the data already processed in the hash or HMAC.
        /// </summary>
        /// <param name="data">The data to process.</param>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <c>null</c>.</exception>
        /// <exception cref="ObjectDisposedException">The object has already been disposed.</exception>
        public void AppendData(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            AppendData(data, 0, data.Length);
        }

        /// <summary>
        /// Append <paramref name="count"/> bytes of <paramref name="data"/>, starting at <paramref name="offset"/>,
        /// to the data already processed in the hash or HMAC.
        /// </summary>
        /// <param name="data">The data to process.</param>
        /// <param name="offset">The offset into the byte array from which to begin using data.</param>
        /// <param name="count">The number of bytes in the array to use as data.</param>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="offset"/> is out of range. This parameter requires a non-negative number.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="count"/> is out of range. This parameter requires a non-negative number less than
        ///     the <see cref="Array.Length"/> value of <paramref name="data"/>.
        ///     </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="count"/> is greater than
        ///     <paramref name="data"/>.<see cref="Array.Length"/> - <paramref name="offset"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The object has already been disposed.</exception>
        public void AppendData(byte[] data, int offset, int count)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "Need non negative number.");
            if (count < 0 || (count > data.Length))
                throw new ArgumentOutOfRangeException(nameof(count));
            if ((data.Length - count) < offset)
                throw new ArgumentException("Invalid offset or length.");
            if (_disposed)
                throw new ObjectDisposedException(typeof(IncrementalHash).Name);

            _stream.Write(data, offset, count);
        }

        /// <summary>
        /// Retrieve the hash or HMAC for the data accumulated from prior calls to
        /// <see cref="AppendData(byte[])"/>, and return to the state the object
        /// was in at construction.
        /// </summary>
        /// <returns>The computed hash or HMAC.</returns>
        /// <exception cref="ObjectDisposedException">The object has already been disposed.</exception>
        public byte[] GetHashAndReset()
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(IncrementalHash).Name);

            _stream.Position = 0;
            byte[] hashValue = _hash.ComputeHash(_stream);
            _stream.Dispose();
            _stream = new MemoryStream();
            return hashValue;
        }

        /// <summary>
        /// Release all resources used by the current instance of the
        /// <see cref="IncrementalHash"/> class.
        /// </summary>
        public void Dispose()
        {
            _disposed = true;

            _hash.Dispose();
            _hash = null;
        }

    }
}
