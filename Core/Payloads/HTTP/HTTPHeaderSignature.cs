namespace P_n_F.Core.Payloads.HTTP
{
    public class HTTPHeaderSignature : Signature
    {
        public HTTPHeaderSignature(string method, string version)
        {
            Method = method;
            Version = version;
        }
        public string Method { get; set; }
        public string Version { get; set; }
    }
}
