namespace bike_mind_quest.Models.BikeModels
{
    public class DonkeyRepublicStationInformationModel
    {
        public int Ttl { get; set; }
        public long LastUpdated { get; set; }
        public DonkeyRepublicDataModel? Data { get; set; }
        public string? Version { get; set; }
    }

    public class DonkeyRepublicDataModel
    {
        public List<DonkeyRepublicStationModel>? Stations { get; set; }
    }

    public class DonkeyRepublicStationModel
    {
        public string? StationId { get; set; }
        public string? Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string? RegionId { get; set; }
        public bool IsVirtualStation { get; set; }
        public int Capacity { get; set; }
        public Dictionary<string, int>? VehicleCapacity { get; set; }
        public DonkeyRepublicRentalUrisModel? RentalUris { get; set; }
    }

    public class DonkeyRepublicRentalUrisModel
    {
        public string? Android { get; set; }
        public string? IOS { get; set; }
    }
}
