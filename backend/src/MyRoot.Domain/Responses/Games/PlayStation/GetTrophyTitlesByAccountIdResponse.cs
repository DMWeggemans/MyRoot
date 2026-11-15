using System.Text.Json.Serialization;

namespace MyRoot.Domain.Responses.Games.PlayStation
{
    public static partial class PlayStation
    {
        public class GetTrophyTitlesByAccountIdResponse
        {
            [JsonPropertyName("trophyTitles")] public List<Payload> TrophyTitles { get; set; } = [];
            [JsonPropertyName("totalItemCount")] public int TotalItemCount { get; set; }
            [JsonPropertyName("nextOffset")] public int NextOffset { get; set; }
            [JsonPropertyName("previousOffset")] public int PreviousOffset { get; set; }

            public class Payload
            {
                [JsonPropertyName("npServiceName")] public string NPServiceName { get; set; } = string.Empty;
                [JsonPropertyName("npCommunicationId")] public string NPCommunicationId { get; set; } = string.Empty;
                [JsonPropertyName("trophySetVersion")] public string TrophySetVersion { get; set; } = string.Empty;
                [JsonPropertyName("trophyTitleName")] public string TrophyTitleName { get; set; } = string.Empty;
                [JsonPropertyName("trophyTitleDetail")] public string TrophyTitleDetail { get; set; } = string.Empty;
                [JsonPropertyName("trophyTitleIconUrl")] public string TrophyTitleIconUrl { get; set; } = string.Empty;
                [JsonPropertyName("trophyTitlePlatform")] public string TrophyTitlePlatform { get; set; } = string.Empty;
                [JsonPropertyName("hasTrophyGroups")] public bool HasTrophyGroups { get; set; }
                [JsonPropertyName("definedTrophies")] public Trophies DefinedTrophies { get; set; } = new Trophies();
                [JsonPropertyName("progress")] public int Progress { get; set; }
                [JsonPropertyName("earnedTrophies")] public Trophies EarnedTrophies { get; set; } = new Trophies();
                [JsonPropertyName("hiddenFlag")] public bool HiddenFlag { get; set; }
                [JsonPropertyName("lastUpdatedDateTime")] public DateTimeOffset LastUpdatedDateTime { get; set; }
            }
        }

        public class Trophies
        {
            [JsonPropertyName("bronze")] public int Bronze { get; set; }
            [JsonPropertyName("silver")] public int Silver { get; set; }
            [JsonPropertyName("gold")] public int Gold { get; set; }
            [JsonPropertyName("platinum")] public int Platinum { get; set; }
        }
    }
}
