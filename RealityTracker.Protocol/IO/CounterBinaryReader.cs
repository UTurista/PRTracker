using System.Numerics;
using System.Text;

namespace RealityTracker.Protocol.IO
{
    /// <summary>
    /// A wrapper around <see cref="BinaryReader"/> that provides a counter to see how many bytes were read.<br>
    /// </summary>
    public class CounterBinaryReader : BinaryReader
    {
        public CounterBinaryReader(Stream input) : base(input)
        {
        }

        public int BytesRead { get; private set; }

        internal void ResetCount()
        {
            BytesRead = 0;
        }
        public override byte ReadByte()
        {
            BytesRead += 1;
            return base.ReadByte();
        }

        public override bool ReadBoolean()
        {
            BytesRead += 1;
            return base.ReadBoolean();
        }

        public override byte[] ReadBytes(int count)
        {
            BytesRead += count;
            return base.ReadBytes(count);
        }

        public override decimal ReadDecimal()
        {
            BytesRead += 16;
            return base.ReadDecimal();
        }

        public override float ReadSingle()
        {
            BytesRead += 4;
            return base.ReadSingle();
        }

        public override short ReadInt16()
        {
            BytesRead += 2;
            return base.ReadInt16();
        }

        public override sbyte ReadSByte()
        {
            BytesRead += 1;
            return base.ReadSByte();
        }

        public override ushort ReadUInt16()
        {
            BytesRead += 2;
            return base.ReadUInt16();
        }

        public override int ReadInt32()
        {
            BytesRead += 4;
            return base.ReadInt32();
        }

        public string ReadCString()
        {
            StringBuilder sb = new();
            byte character;
            BytesRead += 1; // At least 1 for the 0x00

            while ((character = base.ReadByte()) != 0x00)
            {
                BytesRead++;
                sb.Append(character);
            }
            return sb.ToString();
        }

        public Vector3 ReadVector3()
        {
            var x = base.ReadInt16();
            var y = base.ReadInt16();
            var z = base.ReadInt16();

            BytesRead += 6;
            return new Vector3(x, y, z);
        }

        internal float[] ReadSingleArray(int length)
        {
            if(length == 0)
            {
                return Array.Empty<float>();    
            }

            var array = new float[length];
            for (var i = 0; i < length; i++)
            {
                array[i] = base.ReadSingle();
                BytesRead += 4;
            }
            return array;
        }
    }
}
