using System.Collections.Generic;

namespace UMTransporte.App_LocalResources.Chilexpress.Comuna
{
    public class CoverageArea
    {
        public string countyCode { get; set; }
        public string countyName { get; set; }
        public string regionCode { get; set; }
        public int ineCountyCode { get; set; }
        public int queryMode { get; set; }
        public string coverageName { get; set; }
    }

    public class Comuna
    {
        public List<CoverageArea> coverageAreas { get; set; }
        public int statusCode { get; set; }
        public string statusDescription { get; set; }
        public object errors { get; set; }
    }


}
