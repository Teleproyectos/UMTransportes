using System;
using System.IO;
using System.Net;
using UMTransporte.ServicioExternoRegionYComunas;
using UMTransporte.App_LocalResources.Chilexpress.Region;


namespace UMTransporte
{
    public partial class ClienteConectividadWebForm : System.Web.UI.Page
    {
        /**
         * Apis
         **/
        // CERT:_http://apicert.correos.cl:8008/ServicioRegionYComunasExterno/cch/ws/distribucionGeografica/externo/implementacion/ServicioExternoRegionYComunas.asmx?WSDL
        // PROD: http://b2b.correos.cl/ServicioRegionYComunasExterno/cch/ws/distribucionGeografica/externo/implementacion/ServicioExternoRegionYComunas.asmx?WSDL

        public RegionTO[] regions = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            System.Diagnostics.Debug.WriteLine("prueba");
            this.regions = ListarTodasLasRegiones("PRUEBA WS 1", "b9d591ae8ef9d36bb7d4e18438d6114e");
            this.SetCorreosRegions();
            ListarRegionesChilexpress();
            this.SetCorreosRegions();
            //RegisterAsyncTask(new PageAsyncTask(MostrarRegionesAsync));
        }

        public void SetCorreosRegions()
        {
            /*//Data Source
            this.xRegionOrigen.DataSource = this.regions;
            this.xRegionOrigen.DataValueField = "Identificador";
            this.xRegionOrigen.DataTextField = "Nombre";

            //Data Bind
            this.xRegionOrigen.DataBind();

            //Data Source
            this.xRegionDestino.DataSource = this.regions;
            this.xRegionDestino.DataValueField = "Identificador";
            this.xRegionDestino.DataTextField = "Nombre";

            //Data Bind
            this.xRegionDestino.DataBind();

            System.Diagnostics.Debug.WriteLine(regions.Length);
            */
        }

        /*public void SetChilexpressRegions(responseBody)
        {
            //Data Source
            this.xRegionOrigen.DataSource = responseBody;
            this.xRegionOrigen.DataValueField = "Identificador";
            this.xRegionOrigen.DataTextField = "Nombre";

            //Data Bind
            this.xRegionOrigen.DataBind();

            //Data Source
            this.xRegionDestino.DataSource = responseBody;
            this.xRegionDestino.DataValueField = "Identificador";
            this.xRegionDestino.DataTextField = "Nombre";

            //Data Bind
            this.xRegionDestino.DataBind();

            System.Diagnostics.Debug.WriteLine(regions.Length);
        }*/

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

        public RegionTO[] ListarTodasLasRegiones(string usuario, string contrasena)
        {
            try
            {
                var client = new ServicioExternoRegionYComunasSoapClient();
                RegionTO[] regiones = client.listarTodasLasRegiones(usuario, contrasena);
                return regiones;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error intentando obtener las regiones.", ex);
            }
        }

        private static void ListarRegionesChilexpress()
        {
            var url = "https://testservices.wschilexpress.com/georeference/api/v1.0/regions";
            System.Diagnostics.Debug.WriteLine(url);
            var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody

                            Root regiones = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(responseBody);

                            System.Diagnostics.Debug.WriteLine(regiones.Regions.Count);

                        }
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("Error", ex);
            }
        }


    }
}