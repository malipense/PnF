using System;
using System.Collections.Generic;
using System.Text.Json;

namespace P_n_F.Core
{
    internal class HL7Converter
    {
        private const string TEMPLATE = @"MSH|^~\&|EPIC|EPICADT|SMS|SMSADT|199912271408|CHARRIS|ADT^A04|1817457|D|2.5|\r\n" + 
                                        "PID|{0}|{1}";
        public string Convert(string json)
        {
            Fields fields = JsonSerializer.Deserialize<Fields>(json);
            string hl7Message = TEMPLATE;

            for (int i = 0; i < fields.list.Count; i++)
            {
                hl7Message = hl7Message.Replace("{" + i + "}", fields.list[i]);
            }
            
            return hl7Message;
        }

    }

    internal class Fields
    {
        public List<string> list { get; set; }
    }
}
