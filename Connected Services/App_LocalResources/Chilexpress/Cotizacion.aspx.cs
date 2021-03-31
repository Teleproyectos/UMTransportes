using System.Collections.Generic;

namespace UMTransporte.App_LocalResources.Chilexpress.Cotizacion
{
    public class Package
    {
        public string Weight { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
    }

    public class Envio
    {
        public string OriginCountyCode { get; set; }
        public string DestinationCountyCode { get; set; }
        public Package Package { get; set; }
        public int ProductType { get; set; }
        public int ContentType { get; set; }
        public string DeclaredWorth { get; set; }
        public int DeliveryTime { get; set; }
    }

    public class CourierServiceOption
    {
        public int serviceTypeCode { get; set; }
        public string serviceDescription { get; set; }
        public bool didUseVolumetricWeight { get; set; }
        public string finalWeight { get; set; }
        public string serviceValue { get; set; }
        public string conditions { get; set; }
        public int deliveryType { get; set; }
        public List<object> additionalServices { get; set; }
    }

    public class Data
    {
        public List<CourierServiceOption> courierServiceOptions { get; set; }
    }

    public class Cotizacion
    {
        public Data data { get; set; }
        public int statusCode { get; set; }
        public string statusDescription { get; set; }
        public object errors { get; set; }
    }


}
