﻿using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.APIClient.Model.SMS
{
    public class SmsPart
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
    }
}