using Newtonsoft.Json;

namespace Chinchilla.ClickUp.Responses.Model;

/// <summary>
/// Model object of Profile information response
/// </summary>
public class ResponseModelProfileInfo
{
    [JsonProperty("display_profile")]
    public string DisplayProfile { get; set; }

    [JsonProperty("verified_ambassador")]
    public string VerifiedAmbassador { get; set; }

    [JsonProperty("verified_consultant")]
    public string VerifiedConsultant { get; set; }

    [JsonProperty("top_tier_user")]
    public string TopTierUser { get; set; }

    [JsonProperty("viewed_verified_ambassador")]
    public string ViewedVerifiedAmbassador { get; set; }

    [JsonProperty("viewed_verified_consultant")]
    public object ViewedVerifiedConsultant { get; set; }

    [JsonProperty("viewed_top_tier_user")]
    public string ViewedTopTierUser { get; set; }
}