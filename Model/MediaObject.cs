namespace InstaSharp.Model {
    public class MediaObject {
        [JsonMapping("low_resolution", JsonMapping.MappingType.Class)]
        public Resolution LowResolution { get; set; }
        [JsonMapping("thumbnail", JsonMapping.MappingType.Class)]
        public Resolution Thumbnail { get; set; }
        [JsonMapping("standard_resolution", JsonMapping.MappingType.Class)]
        public Resolution StandardResolution { get; set; }
    }
}
