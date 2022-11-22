using CSharp8583.Attributes;
using CSharp8583.Common;
using CSharp8583.Messages;
using CSharp8583.Models;

namespace CSharp8583.Tests.Messages
{
    //------------------------------------------------------------------------------------------------
    //BIT   NAME                            PRESENCE WHEN REQUIRED SIZE    DERIVATION
    //0	    BIT MAP PRIMARY                 M       16	Receiving Member
    //1	    BIT MAP SECONDARY               M       16	Receiving Member
    //3	    PROCESSING CODE                 M		6	Copied from Request
    //12	DATE SENT                       M		8	Copied from Request
    //26	ACTION CODE                     M		4	Receiving Member
    //31	TRANSACTION REFERENCE NUMBER    M       18	Copied from Request
    //44	SYNTAX ERROR DATA               C       Used to indicate which field in a request from the CI failed validation	3	Receiving Member
    //51	CURRENCY                        M       3	Copied from Request
    //98	SENDING FPS INSTITUTION         M       11	Copied from Request
    //128	MESSAGE AUTHENTICATION CODE     M       16	Receiving Member
    //------------------------------------------------------------------------------------------------
    public class FPSMessage : BaseMessage
    {
        [IsoField(position: IsoFields.F3, maxLen: 6, lengthType: LengthType.FIXED, contentType: ContentType.N)]
        public string Field3 { get; set; }

        [IsoField(position: IsoFields.F12, maxLen: 8, lengthType: LengthType.FIXED, contentType: ContentType.N)]
        public string Field12 { get; set; }

        [IsoField(position: IsoFields.F26, maxLen: 4, lengthType: LengthType.FIXED, contentType: ContentType.N)]
        public string Field26 { get; set; }

        [IsoField(position: IsoFields.F31, maxLen: 18, lengthType: LengthType.LLVAR, contentType: ContentType.ANS)]
        public string Field31 { get; set; }

        [IsoField(position: IsoFields.F44, maxLen: 3, lengthType: LengthType.LLVAR, contentType: ContentType.ANS)]
        public string Field44 { get; set; }

        [IsoField(position: IsoFields.F51, maxLen: 3, lengthType: LengthType.FIXED, contentType: ContentType.N)]
        public string Field51 { get; set; }

        [IsoField(position: IsoFields.F98, maxLen: 11, lengthType: LengthType.FIXED, contentType: ContentType.ANS)]
        public string Field98 { get; set; }

        [IsoField(position: IsoFields.F128, maxLen: 16, lengthType: LengthType.FIXED, contentType: ContentType.ANS)]
        public string Field128 { get; set; }
    }
}
