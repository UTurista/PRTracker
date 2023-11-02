using RealityTracker.Protocol.IO;
using System.Numerics;
using System.Text;

namespace RealityTracker.Protocol.Extensions
{
    internal static class StreamReaderExtensions
    {
        public static TType[] ReadSequence<TType>(this CounterBinaryReader reader, int count, Func<CounterBinaryReader, TType> element)
        {
            ArgumentNullException.ThrowIfNull(reader);

            var elements = new List<TType>();
            var initialPosition = reader.BaseStream.Position;
            var endPosition = initialPosition + count;

            var currentPosition = initialPosition;
            while (currentPosition < endPosition)
            {
                var type = element(reader);
                elements.Add(type);

                currentPosition = reader.BaseStream.Position;
            }

            if (currentPosition != endPosition)
            {
                throw new Exception("Unexpected number of bytes read in sequence.");
            }

            return elements.ToArray();
        }
    }
}
