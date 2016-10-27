﻿//
// Copyright (c) 2008-2011, Kenneth Bell
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
//

namespace DiscUtils.SquashFs
{
    using System;

    internal class DirectoryRecord : IByteArraySerializable
    {
        public ushort Offset;
        public short InodeNumber;
        public InodeType Type;
        public string Name;

        public int Size
        {
            get { return 8 + Name.Length; }
        }

        public static DirectoryRecord ReadFrom(MetablockReader reader)
        {
            DirectoryRecord result = new DirectoryRecord();
            result.Offset = reader.ReadUShort();
            result.InodeNumber = reader.ReadShort();
            result.Type = (InodeType) reader.ReadUShort();
            ushort size = reader.ReadUShort();
            result.Name = reader.ReadString(size + 1);

            return result;
        }

        public int ReadFrom(byte[] buffer, int offset)
        {
            throw new NotImplementedException();
        }

        public void WriteTo(byte[] buffer, int offset)
        {
            Utilities.WriteBytesLittleEndian(Offset, buffer, offset + 0);
            Utilities.WriteBytesLittleEndian(InodeNumber, buffer, offset + 2);
            Utilities.WriteBytesLittleEndian((ushort) Type, buffer, offset + 4);
            Utilities.WriteBytesLittleEndian((ushort) (Name.Length - 1), buffer, offset + 6);
            Utilities.StringToBytes(Name, buffer, offset + 8, Name.Length);
        }
    }
}