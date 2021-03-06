﻿using Database.Common.DataOperation;

namespace Database.Common.Messages
{
    /// <summary>
    /// Sent as a request for the data contained in the specified chunk.
    /// </summary>
    public class ChunkDataRequest : BaseMessageData
    {
        /// <summary>
        /// The end of the chunk.
        /// </summary>
        private readonly ChunkMarker _end;

        /// <summary>
        /// The start of the chunk.
        /// </summary>
        private readonly ChunkMarker _start;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChunkDataRequest"/> class.
        /// </summary>
        /// <param name="start">The start of the chunk.</param>
        /// <param name="end">The end of the chunk.</param>
        public ChunkDataRequest(ChunkMarker start, ChunkMarker end)
        {
            _start = start;
            _end = end;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChunkDataRequest"/> class.
        /// </summary>
        /// <param name="data">The data to read from.</param>
        /// <param name="index">The index at which to start reading from.</param>
        public ChunkDataRequest(byte[] data, int index)
        {
            _start = ChunkMarker.ConvertFromString(ByteArrayHelper.ToString(data, ref index));
            _end = ChunkMarker.ConvertFromString(ByteArrayHelper.ToString(data, ref index));
        }

        /// <summary>
        /// Gets the end of the chunk.
        /// </summary>
        public ChunkMarker End
        {
            get { return _end; }
        }

        /// <summary>
        /// Gets the start of the chunk.
        /// </summary>
        public ChunkMarker Start
        {
            get { return _start; }
        }

        /// <inheritdoc />
        protected override byte[] EncodeInternal()
        {
            return ByteArrayHelper.Combine(
                ByteArrayHelper.ToBytes(_start.ToString()),
                ByteArrayHelper.ToBytes(_end.ToString()));
        }

        /// <inheritdoc />
        protected override int GetMessageTypeId()
        {
            return (int)MessageType.ChunkDataRequest;
        }
    }
}