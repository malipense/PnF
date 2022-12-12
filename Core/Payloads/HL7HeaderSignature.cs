namespace P_n_F.Core.Payloads
{
    public class HL7HeaderSignature : Signature
    {
        public HL7HeaderSignature(string version)
        {
            Version = version;
        }
        public string Version { get; set; }
    }
}
