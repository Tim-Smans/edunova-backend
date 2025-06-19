using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduNova.Core.Auth
{
    public class MySettings
    {
        public string? Name { get; set; }
        public char[]? Secret { get; set; }
        public string? ValidIssuer { get; set; }
        public string? ValidAudience { get; set; }
        public List<String>? ValidAudiences { get; set; }
    }
}
