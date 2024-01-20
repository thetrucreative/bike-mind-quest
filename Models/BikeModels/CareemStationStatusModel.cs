namespace bike_mind_quest.Models.BikeModels
{
    public class CareemStationStatusModel
    {
        public long LastUpdated { get; set; }
        public int Ttl { get; set; }
        public CareemDataModel? Data { get; set; }
        public string? Version { get; set; }
    }

    public class CareemDataModel
    {
        public List<CareemStationModel>? Stations { get; set; }
    }

    public class CareemStationModel
    {
        public string? StationId { get; set; }
        public int NumBikesAvailable { get; set; }
        public int NumBikesDisabled { get; set; }
        public int NumDocksAvailable { get; set; }
        public int NumDocksDisabled { get; set; }
        public long LastReported { get; set; }
        public bool IsChargingStation { get; set; }
        public string? Status { get; set; }
        public bool IsInstalled { get; set; }
        public bool IsRenting { get; set; }
        public bool IsReturning { get; set; }
        public object? Traffic { get; set; }
        public List<VehicleDockModel>? VehicleDocksAvailable { get; set; }
        public List<VehicleTypeModel>? VehicleTypesAvailable { get; set; }
    }

    public class VehicleDockModel
    {
        public List<string>? VehicleTypeIds { get; set; }
        public int Count { get; set; }
    }

    public class VehicleTypeModel
    {
        public string? VehicleTypeId { get; set; }
        public int Count { get; set; }
    }
}
