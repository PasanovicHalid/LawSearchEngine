using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawSearchEngine.Infrastructure.Configurations
{
    internal class ElasticsearchConfiguration
    {
        public string Url { get; set; } = string.Empty;
        public string CertificateThumbprint { get; set; } = string.Empty;
        public string BasicAuthUsername { get; set; } = string.Empty;
        public string BasicAuthPassword { get; set; } = string.Empty;
    }
}
