using FastDeepCloner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Youtube.Manager.Models.Container
{
    public class MethodInformation
    {
        public SafeValueType<string, object> Arguments { get; set; } = new SafeValueType<string, object>();

        public string FullUrl { get; set; }

        public HttpMethod HttpMethod { get; set; }

        public bool IsVoid { get; set; }

        public Type CleanReturnType { get; set; }


        public override string ToString()
        {
            return FullUrl + HttpMethod;
        }

        public string ToQuary(SafeValueType<string, object> args)
        {
            var arguments = args ?? Arguments;

            var url = FullUrl;
            foreach (var arg in arguments)
            {
                if (!url.EndsWith("?"))
                    url += $"?{arg.Key}={arg.Value}";
                else url += $"&{arg.Key}={arg.Value}";
            }

            return url;
        }
    }
}
