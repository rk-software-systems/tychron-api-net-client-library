namespace RKSoftware.Tychron.APIClient.Error
{
    /// <summary>
    /// Response Codes
    /// <see href="https://docs.tychron.info/sms-api/sending-sms-via-http/#response-codes"/>
    /// </summary>
    public class TychronAPIException : Exception
    {
        private readonly string? _xcdrid;
        private readonly int _statusCode;

        public TychronAPIException(string? xcdrid, int statusCode, string message)
            : base(message)
        {
            _xcdrid = xcdrid;
            _statusCode = statusCode;
        }

        public string? XCDRID => _xcdrid;

        public int StatusCode => _statusCode;
    }
}
