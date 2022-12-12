using P_n_F.Core.Payloads;
using P_n_F.Core.Payloads.HTTP;
using System;
using System.Collections.Generic;
using System.Text;

namespace P_n_F.Core
{
    public class PayloadAnalyzer
    {
        public PayloadType GetType(string payload)
        {
            payload = "GET / HTTP/1.1\r\n" +
                "Host:test\r\n";

            string payload2 = @"MSH|^~\&|EPIC|EPICADT|SMS|SMSADT|199912271408|CHARRIS|ADT^A04|1817457|D|2.5|";

            Signature signature;

            signature = TryGetHttpHeaderSignature(payload2);
            if(signature != null)
            {
                var http = (HTTPHeaderSignature)signature;
                Console.WriteLine($"Type of request: {http.Version} - {http.Method}");
                return PayloadType.HTTP;
            }

            signature = TryGetHL7HeaderSignature(payload2);
            if(signature != null)
            {
                var hl7 = (HL7HeaderSignature)signature;
                Console.WriteLine($"Type of request: HL7 {hl7.Version}");
                return PayloadType.HL7;
            }

            //var byteStream = Encoding.ASCII.GetBytes(payload);
            //var hexString = BitConverter.ToString(byteStream);
            return PayloadType.Unsupported;
        }
        private HTTPHeaderSignature TryGetHttpHeaderSignature(string payload)
        {
            var signature = payload.Split('\r')[0];
            var subItems = signature.Split(' ');
            var method = subItems[0];
            HTTPHeaderSignature hTTPHeader = null;

            foreach(var met in HTTPMethods.Methods)
            {
                if (method == met)
                {
                    var version = subItems[2];
                    hTTPHeader = new HTTPHeaderSignature(method, version);
                }
            }

            return hTTPHeader;
        }
        private HL7HeaderSignature TryGetHL7HeaderSignature(string payload)
        {
            var signature = payload.Split('\r')[0];
            HL7HeaderSignature hl7Header = null;
            if(signature.StartsWith("MSH|"))
            {
                var subItems = signature.Split('|');
                hl7Header = new HL7HeaderSignature(subItems[11]);
            }
            return hl7Header;
        }

        private bool ValidateKeys(List<HTTPKeyEntry> keyEntries)
        {
            foreach (var keyEntry in keyEntries)
                if(keyEntry.IsSet == false)
                    return false;
            return true;
        }
        private List<HTTPKeyEntry> GetKeys(List<string> lines)
        {
            List<HTTPKeyEntry> httpMessageKeys = new List<HTTPKeyEntry>();

            foreach (var line in lines)
                httpMessageKeys.Add(new HTTPKeyEntry(line.Split(':')[0]));

            foreach (var key in httpMessageKeys)
            {
                foreach (var item in HTTPHeaderDefinition.RequiredHeaders)
                {
                    if (key.Name == item)
                        key.IsSet = true;
                }
            }
            return httpMessageKeys;
        }
        private List<string> SplitHttpMessageLines(string payload)
        {
            byte lineFeedCode = 0x0A;
            byte carriageReturnCode = 0x0D;
            List<string> httpMessageLines = new List<string>();

            int currentIndex = 0;
            for (int i = 0; i < payload.Length; i++)
            {
                if (payload[i] == carriageReturnCode
                && payload[i + 1] == lineFeedCode)
                {
                    httpMessageLines.Add(payload.Substring(currentIndex, Math.Abs(currentIndex - i)));
                    currentIndex = i + 2;
                }
            }
            return httpMessageLines;
        }
    }

    public static class HTTPHeaderDefinition
    {
        public static readonly byte spaceCode = 0x20;
        public static readonly byte colonCode = 0x3A;

        public static readonly List<string> RequiredHeaders = new List<string>()
        {
            "Host",
            "Connection",
            "Cache-Control",
            "User-Agent",
            "Accept"
        };
    }

    public class HTTPKeyEntry
    {
        public HTTPKeyEntry(string name)
        {
            Name = name;
            IsSet = false;
        }
        public string Name { get; set; }
        public bool IsSet { get; set; }
    }
}
