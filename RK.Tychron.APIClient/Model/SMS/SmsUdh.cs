﻿using System.Text.Json.Serialization;

namespace RK.Tychron.APIClient.Model.SMS
{
    public class SmsUdh
    {
        [JsonPropertyName("ref_num")]
        public int RefNum { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
