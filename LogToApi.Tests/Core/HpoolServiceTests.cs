using System;
using System.Collections;
using System.Collections.Generic;
using LogToApi.Common.Models;
using LogToApi.Core.Services;
using Shouldly;
using Xunit;

namespace LogToApi.Tests.Core
{
    public class HpoolServiceTests
    {
        public class TestData
        {
            public string RawData { get; set; }
            public LogRecord Expected { get; set; }
        }

        public static IEnumerable<object[]> TestDataList => new List<object[]>
        {
            new object[]
            {
                new TestData()
                {
                    RawData = null,
                    Expected = null
                }
            },
            new object[]
            {
                new TestData()
                {
                    RawData = "time=\"2021-08-08T08:00:45+01:00\" level=info msg=\"new mining info\" capacity=\"22.08 TB\" file=loggers.go func=logging.CPrint height=685404 jobId=417323396 line=168 scan consume=1048 scan time=\"2021-08-08 08:00:45\" tid=954",
                    Expected = new LogRecord()
                    {
                        DateTime = new DateTime(2021,08,08,10,0,45,DateTimeKind.Local),
                        Level = "info",
                        Message = "new mining info",
                        Capacity = "22.08 TB"
                    }
                }
            },
            new object[]
            {
                new TestData()
                {
                    RawData = "time=\"2021-08-08T07:42:55+01:00\" level=info msg=\"upload status\" file=loggers.go func=logging.CPrint interval=180 line=168 pre upload time=\"2021-08-08 07:39:55\" tid=278",
                    Expected = new LogRecord()
                    {
                        DateTime = new DateTime(2021,08,08,9,42,55,DateTimeKind.Local),
                        Level = "info",
                        Message = "upload status",
                        Capacity = null
                    }
                }
            },
            new object[]
            {
                new TestData()
                {
                    RawData = "time=\"2021-08-08T07:43:56+01:00\" level=error msg=\"add plot file\" error=\"badbit or failbit after reading size 104 at position 0\" f7=\"{loggers.go,logging.CPrint,156}\" f8=\"{plotterSpace.go,chia.(*PlotterSpace).Load,240}\" f9=\"{asm_arm.s,runtime.goexit,841}\" file=/media/usb2/Chia/plot-k32-2021-06-07-23-58-adbe90ac13a733d390900ec383f536f51dbfccc542d14dc000da48faaba8e3e4.plot tid=47",
                    Expected = new LogRecord()
                    {
                        DateTime = new DateTime(2021,08,08,9,43,56,DateTimeKind.Local),
                        Level = "error",
                        Message = "add plot file",
                        ErrorText = "badbit or failbit after reading size 104 at position 0 File: \"/media/usb2/Chia/plot-k32-2021-06-07-23-58-adbe90ac13a733d390900ec383f536f51dbfccc542d14dc000da48faaba8e3e4.plot\"",
                        Capacity = null
                    }
                }
            },
            new object[]
            {
                new TestData()
                {
                    RawData = "time=\"2021-08-08T07:58:24+01:00\" level=error msg=\"read message fail\" error=\"read tcp 192.168.1.100:37872->203.107.53.48:443: use of closed network connection\" f7=\"{loggers.go,logging.CPrint,156}\" f8=\"{server.go,websocket.(*Server).connect.func1,111}\" f9=\"{asm_arm.s,runtime.goexit,841}\" tid=114",
                    Expected = new LogRecord()
                    {
                        DateTime = new DateTime(2021,08,08,9,58,24,DateTimeKind.Local),
                        Level = "error",
                        Message = "read message fail",
                        ErrorText = "read tcp 192.168.1.100:37872->203.107.53.48:443: use of closed network connection",
                        Capacity = null
                    }
                }
            },
            new object[]
            {
                new TestData()
                {
                    RawData = "time=\"2021-08-08T07:58:25+01:00\" level=info msg=\"connect success\" file=loggers.go func=logging.CPrint line=168 tid=8 url=\"ws://203.107.53.48:3501/x-proxynode/ws\"time=\"2021-08-08T07:58:25+01:00\" level=info msg=\"connect success\" file=loggers.go func=logging.CPrint line=168 tid=8 url=\"ws://203.107.53.48:3501/x-proxynode/ws\"",
                    Expected = new LogRecord()
                    {
                        DateTime = new DateTime(2021,08,08,9,58,25,DateTimeKind.Local),
                        Level = "info",
                        Message = "connect success",
                        ErrorText = null,
                        Capacity = null
                    }
                }
            },
        };
        
        [Theory]
        [MemberData(nameof(TestDataList))]
        public void GetRecordTests(TestData data)
        {
            var result = HpoolService.GetRecord(data.RawData);
            result.ShouldBeEquivalentTo(data.Expected);
        }
    }
}