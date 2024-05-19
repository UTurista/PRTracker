using System.Numerics;
using System.Text;

namespace RealityTracker.Protocol.IO;

/// <summary>
/// A wrapper around <see cref="BinaryReader"/> that provides a counter to see how many bytes were read.<br>
/// </summary>
public sealed class CounterBinaryReader
{
    private readonly BinaryReader _reader;

    public int BytesRead { get; private set; }

    public CounterBinaryReader(Stream input)
    {
        _reader = new BinaryReader(input);
    }

    /// <summary>
    /// Resets the counter of bytes read.
    /// </summary>
    public void ResetCount()
    {
        BytesRead = 0;
    }

    /// <summary>
    /// Reads a single byte.
    /// </summary>
    public byte ReadByte()
    {
        BytesRead += 1;
        return _reader.ReadByte();
    }

    /// <summary>
    /// Reads a single boolean.
    /// </summary>
    public bool ReadBoolean()
    {
        BytesRead += 1;
        return _reader.ReadBoolean();
    }

    /// <summary>
    /// Reads an array of bytes.
    /// </summary>
    /// <param name="count">The number of bytes to read</param>
    public byte[] ReadBytes(int count)
    {
        BytesRead += count;
        return _reader.ReadBytes(count);
    }

    /// <summary>
    /// Reads a single 16-byte decimal value
    /// </summary>
    public decimal ReadDecimal()
    {
        BytesRead += 16;
        return _reader.ReadDecimal();
    }

    /// <summary>
    /// Reads a single 4-byte floating point value
    /// </summary>
    public float ReadSingle()
    {
        BytesRead += 4;
        return _reader.ReadSingle();
    }

    /// <summary>
    /// Reads a single 2-byte integer
    /// </summary>
    public short ReadInt16()
    {
        BytesRead += 2;
        return _reader.ReadInt16();
    }

    /// <summary>
    /// Reads a single signed byte
    /// </summary>
    public sbyte ReadSByte()
    {
        BytesRead += 1;
        return _reader.ReadSByte();
    }

    /// <summary>
    /// Reads a single 2-byte unsigned integer.
    /// </summary>
    public ushort ReadUInt16()
    {
        BytesRead += 2;
        return _reader.ReadUInt16();
    }

    /// <summary>
    /// Reads a single 4-byte integer.
    /// </summary>
    public int ReadInt32()
    {
        BytesRead += 4;
        return _reader.ReadInt32();
    }

    /// <summary>
    /// Reads a null (0x00) terminated string.
    /// </summary>
    public string ReadCString()
    {
        StringBuilder sb = new();
        byte character;
        BytesRead += 1; // At least 1 for the 0x00

        while ((character = _reader.ReadByte()) != 0x00)
        {
            BytesRead++;
            sb.Append(character);
        }
        return sb.ToString();
    }

    /// <summary>
    /// Reads a (x, y, z) int16 vector.
    /// </summary>
    public Vector3 ReadVector3()
    {
        var x = _reader.ReadInt16();
        var y = _reader.ReadInt16();
        var z = _reader.ReadInt16();

        BytesRead += 6;
        return new Vector3(x, y, z);
    }

    /// <summary>
    /// Reads an array of floats.
    /// </summary>
    /// <param name="length">The number of floats to read.</param>
    internal float[] ReadSingleArray(int length)
    {
        if (length <= 0)
        {
            return Array.Empty<float>();
        }

        var array = new float[length];
        for (var i = 0; i < length; i++)
        {
            array[i] = _reader.ReadSingle();
            BytesRead += 4;
        }
        return array;
    }
}
