using System;
using System.Collections.Generic;
using System.Text;

namespace HashSharpCore.Models
{
    public class SiteSettings
    {
        public JwtSettings JwtSettings { get; set; }
        public string BaseUrl { get; set; }
    }

    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireAddDay { get; set; }
    }
}
