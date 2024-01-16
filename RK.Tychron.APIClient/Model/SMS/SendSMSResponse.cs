﻿namespace RK.Tychron.APIClient.Model.SMS
{
    /// <summary>
    /// This object represents SMS message Tychron API response.
    /// <see href="https://docs.tychron.info/sms-api/sending-sms-via-http/#response-format"/>
    /// </summary>
    public class SendSmsResponse<T>
    {
        /// <summary>
        /// An ID used to identify the CDR (Call Detail Record) generated by the request. Please provide this ID for any billing questions when contacting support.
        /// <see href="https://docs.tychron.info/sms-api/sending-sms-via-http/#response-headers"/>
        /// </summary>
        public string? XCDRID { get; set; }

        /// <summary>
        /// Messages responses
        /// </summary>
        public List<T>? Messages { get; init; } 

        /// <summary>
        /// This flag is set to true when we obtain the following response from Tychron API (HTTP Status Code 207)
        /// <see href="https://docs.tychron.info/sms-api/sending-sms-via-http/#response-codes"/>
        /// </summary>
        public bool PartialFailure { get; init; }
    }
}
