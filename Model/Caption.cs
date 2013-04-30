using System;

namespace InstaSharp.Model
{
    public class Caption
    {

        [JsonMapping("created_time", JsonMapping.MappingType.Primitive)]
        public DateTime CreatedTime { get; set; }

        [JsonMapping("text", JsonMapping.MappingType.Primitive)]
        public string Text { get; set; }


        [JsonMapping("id", JsonMapping.MappingType.Primitive)]
        public string Id { get; set; }

        [JsonMapping("from", JsonMapping.MappingType.Class)]
        public User From { get; set; }
    }
}