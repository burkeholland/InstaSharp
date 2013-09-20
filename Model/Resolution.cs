namespace InstaSharp.Model
{
	public class Resolution
	{
		[JsonMapping("url", JsonMapping.MappingType.Primitive)]
		public string Url { get; set; }
		[JsonMapping("width", JsonMapping.MappingType.Primitive)]
		public int Width { get; set; }
		[JsonMapping("height", JsonMapping.MappingType.Primitive)]
		public int Height { get; set; }
	}
}
