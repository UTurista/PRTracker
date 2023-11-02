namespace RealityTracker.Protocol
{
    public class SlowNetworkStream : Stream
    {
        private readonly Stream _sourceStream;
        private readonly int _delayMilliseconds;
        private readonly int _chunkSize;

        public SlowNetworkStream(Stream sourceStream, int delayMilliseconds, int chunkSize)
        {
            _sourceStream = sourceStream ?? throw new ArgumentNullException(nameof(sourceStream));
            _delayMilliseconds = delayMilliseconds;
            _chunkSize = chunkSize;
        }

        public override bool CanRead => _sourceStream.CanRead;
        public override bool CanWrite => _sourceStream.CanWrite;
        public override bool CanSeek => _sourceStream.CanSeek;
        public override long Length => _sourceStream.Length;

        public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            Console.WriteLine($"Read {_sourceStream.Position}/{count}...");
            count = Math.Min(count, _chunkSize);

            int totalBytesRead = _sourceStream.Read(buffer, offset, count);
            Thread.Sleep(_delayMilliseconds);

            return totalBytesRead;
        }

        // Implement other required Stream properties and methods...

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            Console.WriteLine($"ReadAsync {_sourceStream.Position}/{count}..." );
            count = Math.Min(count, _chunkSize);

            int totalBytesRead = await _sourceStream.ReadAsync(buffer, offset, count, cancellationToken: cancellationToken);
            await Task.Delay(_delayMilliseconds, cancellationToken);
            
            return totalBytesRead;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            int totalBytesWritten = 0;

            while (totalBytesWritten < count)
            {
                int bytesRemaining = count - totalBytesWritten;
                int bytesToWrite = Math.Min(bytesRemaining, _chunkSize);

                if (bytesToWrite <= 0)
                    break;

                await _sourceStream.WriteAsync(buffer, offset + totalBytesWritten, bytesToWrite, cancellationToken);

                totalBytesWritten += bytesToWrite;
                await Task.Delay(_delayMilliseconds);
            }
        }

        // Implement other required Stream methods...
    }
}