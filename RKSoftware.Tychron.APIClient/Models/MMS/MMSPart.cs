using System.Text.Json.Serialization;

namespace RKSoftware.Tychron.APIClient.Model.MMS;

/// <summary>
/// Part <br/>
/// <see href="https://docs.tychron.info/mms-api/sending-mms-via-http/#request-parameters"/>
/// </summary>
public class MmsPart
{
    /// <summary>
    /// An ID used to identify one part (one attachment) of a multipart message.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// The body of the part.If the <b>"transfer_encoding"</b> parameter is used,<br/>
    /// the part body should be encoded.
    /// </summary>
    [JsonPropertyName("body")]
    public string? Body { get; set; }

    /// <summary>
    /// A URL to a specified resource. The content type will be determined by the file's extension.<br/>
    /// There is a 2mb limit on any files (this includes the HTTP overhead). As a rule of thumb,<br/>
    /// try to keep file sizes below 1mb, as most carriers will reject messages containing content<br/>
    /// larger than 1mb.
    /// </summary>
    /// <remarks>
    /// Please note that in some cases, carriers limit files sizes to as low as <i>500kb</i>.<br/>
    /// If you require further details regarding carrier restrictions on file sizes,<br/>
    /// please contact <b>(support @tychron.com)</b>.
    ///</remarks>
    [JsonPropertyName("uri")]
    public string? Uri { get; set; }

    /// <summary>
    /// An <b>encoding</b> that is applied to the message body when transferring via JSON.<br/>
    /// The encoding can be either base64 or identity.
    /// </summary>
    [JsonPropertyName("transfer_encoding")]
    public string? TransferEncoding { get; set; }
}