using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QQRobot
{
   
    internal class ByteBuffer
    {
        private byte[] _buffer = new byte[0x10];
        public Stream BaseStream = new MemoryStream();

        public byte Get()
        {
            return (byte)this.BaseStream.ReadByte();
        }

        private bool Peek()
        {
            return (this.BaseStream.Position < this.BaseStream.Length);
        }

        public void Put(bool value)
        {
            this._buffer[0] = value ? ((byte)1) : ((byte)0);
            this.BaseStream.Write(this._buffer, 0, 1);
        }

        public void Put(byte value)
        {
            this.BaseStream.WriteByte(value);
        }

        public void Put(byte[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            this.BaseStream.Write(value, 0, value.Length);
        }

        public void PutInt(int value)
        {
            this.PutInt((uint)value);
        }

        public void PutInt(uint value)
        {
            this._buffer[0] = (byte)(value >> 0x18);
            this._buffer[1] = (byte)(value >> 0x10);
            this._buffer[2] = (byte)(value >> 8);
            this._buffer[3] = (byte)value;
            this.BaseStream.Write(this._buffer, 0, 4);
        }

        public void PutInt(int index, uint value)
        {
            int position = (int)this.BaseStream.Position;
            this.Seek(index, SeekOrigin.Begin);
            this.PutInt(value);
            this.Seek(position, SeekOrigin.Begin);
        }

        private long Seek(int offset, SeekOrigin origin)
        {
            return this.BaseStream.Seek((long)offset, origin);
        }

        public byte[] ToByteArray()
        {
            long position = this.BaseStream.Position;
            this.BaseStream.Position = 0L;
            byte[] buffer = new byte[this.BaseStream.Length];
            this.BaseStream.Read(buffer, 0, buffer.Length);
            this.BaseStream.Position = position;
            return buffer;
        }
    }
}
