using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using UMTransporte.ServicioExternoRegionYComunas;

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
            System.Diagnostics.Debug.WriteLine("prueba");
            this.regions = ListarTodasLasRegiones("PRUEBA WS 1", "b9d591ae8ef9d36bb7d4e18438d6114e");
            this.SetCorreosRegions();
            //RegisterAsyncTask(new PageAsyncTask(MostrarRegionesAsync));
        }

        public void SetCorreosRegions()
        {
            //Data Source
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
        }

        /* Add Service Reference namespace RegionesYComunas
   using RegionesYComunas; */
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

        public async Task<HttpResponseMessage> MostrarRegionesAsync()
        {
            try
            {
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Clear();

                Uri uri = new Uri("https://testservices.wschilexpress.com/georeference/api/v1.0/regions");

                HttpResponseMessage response = await client.GetAsync(uri);

                return response;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write("Error Regiones" + e);

                return new HttpResponseMessage();
            }
        }


    }
}