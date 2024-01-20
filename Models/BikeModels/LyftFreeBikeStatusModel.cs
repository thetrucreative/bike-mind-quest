namespace bike_mind_quest.Models.BikeModels
{
    public class LyftFreeBikeStatusModel
    {
        public LyftDataModel? Data { get; set; }
        public long LastUpdated { get; set; }
        public int Ttl { get; set; }
        public string? Version { get; set; }
    }

    public class LyftDataModel
    {
        public List<LyftBikeModel>? Bikes { get; set; }
    }

    public class LyftBikeModel
    {
        public int IsReserved { get; set; }
        public double Lon { get; set; }
        public int IsDisabled { get; set; }
        public double CurrentRangeMeters { get; set; }
        public string? VehicleTypeId { get; set; }
        public double Lat { get; set; }
        public string? BikeId { get; set; }
        public LyftRentalUrisModel? RentalUris { get; set; }
    }

    public class LyftRentalUrisModel
    {
        public string? Android { get; set; }
        public string? IOS { get; set; }
    }
}
