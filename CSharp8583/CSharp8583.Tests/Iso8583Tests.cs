using CSharp8583.Common;
using CSharp8583.Tests.Messages;
using System;
using Xunit;

namespace CSharp8583.Tests
{
    public class Iso8583Tests
    {
        private readonly Iso8583 _iso8583;
        public Iso8583Tests() => _iso8583 = new Iso8583();

        [Fact]
        public void Duplicate_Message_ISO_Fields_Should_Throw_ArgException()
        {
            try
            {
                var duplicateMessage = new DuplicateFieldsMessage();
            }
            catch (ArgumentException)
            {
                Assert.True(true);
                return;
            }
            Assert.True(false);
        }

        [Fact]
        public void ASCII_Message_All_ISO_Fields_Parsed()
        {
            ASCIIMessage asciiMessage = _iso8583.Parse<ASCIIMessage>(ConstantValues.ASCIIBytes);
            Assert.NotNull(asciiMessage);

            Assert.Equal("1038040008C00000", asciiMessage.BitMap);
            Assert.Equal("0100", asciiMessage.MTI);
            Assert.Equal("000000004444", asciiMessage.Field4);
            Assert.Equal("000021", asciiMessage.Field11);
            Assert.Equal("104212", asciiMessage.Field12);
            Assert.Equal("0529", asciiMessage.Field13);
            Assert.Equal("021", asciiMessage.Field22);
            Assert.Equal("000000001015", asciiMessage.Field37);
            Assert.Equal("JI091003", asciiMessage.Field41);
            Assert.Equal("000000000111111", asciiMessage.Field42);
        }

        [Fact]
        public void ASCII_Message_ISO_Build()
        {
            ASCIIMessage asciiMessage = GetASCIIMessageObj();
            var asciiMessageBytes = _iso8583.Build(asciiMessage, "0100", IsoFields.F39, IsoFields.F73);
            Assert.Equal(asciiMessageBytes, ConstantValues.ASCIIBytes);
        }

        [Fact]
        public void ASCII_Message_Missing_Field42_Should_Throw_ParseException()
        {
            ASCIIMessage asciiMessage = _iso8583.Parse<ASCIIMessage>(ConstantValues.ASCIIBytesMissingF41);
        }

        [Fact]
        public void ASCII_Message_Build_Missing_Field42Value_Should_Throw_ArgNullException()
        {
            ASCIIMessage asciiMessage = GetASCIIMessageObj();
            asciiMessage.Field42 = null;

            try
            {
                var asciiMessageBytes = _iso8583.Build(asciiMessage, "0100", IsoFields.F39, IsoFields.F73);
            }
            catch (ArgumentNullException)
            {
                Assert.True(true);
                return;
            }

            Assert.True(false);
        }

        [Fact]
        public void Parse_ASCII_Message_With_Secondary_BitMap()
        {
            ASCIIMessage asciiMessage = _iso8583.Parse<ASCIIMessage>(ConstantValues.ASCIIBytesWithSecondaryBitmap);

            Assert.NotNull(asciiMessage);
            Assert.Equal(32, asciiMessage.BitMap.Length);
            Assert.Equal("9038040008C000000080000000000000", asciiMessage.BitMap);
            Assert.Equal("0100", asciiMessage.MTI);
            Assert.Equal("000000004444", asciiMessage.Field4);
            Assert.Equal("000021", asciiMessage.Field11);
            Assert.Equal("104212", asciiMessage.Field12);
            Assert.Equal("0529", asciiMessage.Field13);
            Assert.Equal("021", asciiMessage.Field22);
            Assert.Equal("000000001015", asciiMessage.Field37);
            Assert.Equal("JI091003", asciiMessage.Field41);
            Assert.Equal("000000000111111", asciiMessage.Field42);
            Assert.Equal("121212", asciiMessage.Field73);
        }

        [Fact]
        public void Build_ASCII_Message_Secondary_BitMap()
        {
            ASCIIMessage asciiMessage = GetASCIIMessageObj();
            asciiMessage.Field73 = "121212";

            var asciiMessageBytes = _iso8583.Build(asciiMessage, "0100", IsoFields.F39);
            Assert.Equal(asciiMessageBytes, ConstantValues.ASCIIBytesWithSecondaryBitmap);
        }


        private ASCIIMessage GetASCIIMessageObj() => new ASCIIMessage
        {
            Field4 = "000000004444",
            Field11 = "000021",
            Field12 = "104212",
            Field13 = "0529",
            Field22 = "021",
            Field37 = "000000001015",
            Field41 = "JI091003",
            Field42 = "000000000111111"
        };
    }
}
