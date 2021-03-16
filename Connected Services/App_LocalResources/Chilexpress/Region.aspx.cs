namespace UMTransporte.App_LocalResources.Chilexpress.Region
{
    public class Region
    {
        public string RegionId { get; set; }
        public string RegionName { get; set; }
        public int IneRegionCode { get; set; }
    }

    public class Root
    {
        public System.Collections.Generic.List<Region> Regions { get; set; }
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public object Errors { get; set; }
    }
}
