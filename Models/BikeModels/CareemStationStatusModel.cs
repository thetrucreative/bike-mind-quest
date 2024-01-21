using Newtonsoft.Json;

namespace bike_mind_quest.Models.BikeModels
{
    public class CareemStationStatusModel
    {
        [JsonProperty("last_updated")]
        public long LastUpdated { get; set; }
        [JsonProperty("ttl")]
        public int Ttl { get; set; }
        [JsonProperty("data")]
        public CareemStationStatusData? Data { get; set; }
        [JsonProperty("version")]
        public string? Version { get; set; }
    }

    public class CareemStationStatusData
    {
        [JsonProperty("stations")]
        public CareemStation[]? Stations { get; set; }
    }

    public class CareemStation
    {
        [JsonProperty("station_id")]
        public string? StationId { get; set; }
        [JsonProperty("num_bikes_available")]
        public int NumBikesAvailable { get; set; }
        [JsonProperty("num_bikes_disabled")]
        public int NumBikesDisabled { get; set; }
        [JsonProperty("num_docks_available")]
        public int NumDocksAvailable { get; set; }
        [JsonProperty("num_docks_disabled")]
        public int NumDocksDisabled { get; set; }
        [JsonProperty("last_reported")]
        public long LastReported { get; set; }
        [JsonProperty("is_charging_station")]
        public bool IsChargingStation { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("is_installed")]
        public bool IsInstalled { get; set; }
        [JsonProperty("is_renting")]
        public bool IsRenting { get; set; }
        [JsonProperty("is_returning")]
        public bool IsReturning { get; set; }
        [JsonProperty("traffic")]
        public object? Traffic { get; set; }
        [JsonProperty("vehicle_docks_available")]
        public CareemVehicleDock[]? VehicleDocksAvailable { get; set; }
        [JsonProperty("vehicle_types_available")]
        public CareemVehicleType[]? VehicleTypesAvailable { get; set; }
    }

    public class CareemVehicleDock
    {
        [JsonProperty("vehicle_type_ids")]
        public string[]? VehicleTypeIds { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class CareemVehicleType
    {
        [JsonProperty("vehicle_type_id")]
        public string? VehicleTypeId { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
