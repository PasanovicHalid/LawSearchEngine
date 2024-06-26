﻿namespace LawSearchEngine.Infrastructure.Configurations
{
    internal class ElasticsearchConfiguration
    {
        public string Url { get; set; } = string.Empty;
        public string CertificateThumbprint { get; set; } = string.Empty;
        public string BasicAuthUsername { get; set; } = string.Empty;
        public string BasicAuthPassword { get; set; } = string.Empty;
    }
}
