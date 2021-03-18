using System;
using System.IO;
using System.Net;
using UMTransporte.ServicioExternoRegionYComunas;
using UMTransporte.App_LocalResources.Chilexpress.Region;
using System.Web.UI.WebControls;

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
        public Root chilexpressRegions = null;
        public ComunaTO[] communes = null;
        public String regionSelected = null;

        public string userCorreos = "PRUEBA WS 1";
        public string pwdCorreos = "b9d591ae8ef9d36bb7d4e18438d6114e";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                System.Diagnostics.Debug.WriteLine("prueba");

                this.ListarRegionesChilexpress();
                this.SetCorreosRegions();
                //RegisterAsyncTask(new PageAsyncTask(MostrarRegionesAsync));
            }
        }

        protected void RegionDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {

            System.Diagnostics.Debug.WriteLine("Obtener comunas: " + this.regionSelected);

            if (this.EmpresaTransporteCorreosRadioButton.Checked == true)
            {
                DropDownList elementChanged = sender as DropDownList;
                System.Diagnostics.Debug.WriteLine("ID Dropdown = " + elementChanged.ID);

                this.communes = ListarComunasSegunRegion(userCorreos, pwdCorreos, elementChanged.SelectedValue);

                System.Diagnostics.Debug.WriteLine("Comunas = "+this.communes.Length);
                if (elementChanged.ID == "xRegionOrigen")
                {
                    this.xComunaOrigen.DataSource = this.communes;
                    this.xComunaOrigen.DataValueField = "NombreComuna";
                    this.xComunaOrigen.DataTextField = "NombreComuna";
                    this.xComunaOrigen.DataBind();
                }
                else
                {
                    this.xComunaDestino.DataSource = this.communes;
                    this.xComunaDestino.DataValueField = "NombreComuna";
                    this.xComunaDestino.DataTextField = "NombreComuna";
                    this.xComunaDestino.DataBind();
                }
            }

        }

        protected void EmpresaTransporteRadioButton_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.EmpresaTransporteChilexpressRadioButton.Checked == true)
            {
                SetChilexpressRegions();
            }
            else
            {
                SetCorreosRegions();
            }
        }

        public void SetCorreosRegions()
        {
            this.regions = ListarRegionesCorreos(userCorreos, pwdCorreos);

            this.xComunaDestinoContainer.Visible = true;
            this.xComunaOrigenContainer.Visible = true;

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

            this.regionSelected = "correos";

        }

        public void SetChilexpressRegions()
        {
            this.chilexpressRegions = ListarRegionesChilexpress();

            this.xComunaDestinoContainer.Visible = false;
            this.xComunaOrigenContainer.Visible = false;

            //Data Source
            this.xRegionOrigen.DataSource = this.chilexpressRegions.Regions;
            this.xRegionOrigen.DataValueField = "RegionId";
            this.xRegionOrigen.DataTextField = "RegionName";

            //Data Bind
            this.xRegionOrigen.DataBind();

            //Data Source
            this.xRegionDestino.DataSource = this.chilexpressRegions.Regions;
            this.xRegionDestino.DataValueField = "RegionId";
            this.xRegionDestino.DataTextField = "RegionName";

            //Data Bind
            this.xRegionDestino.DataBind();

            this.regionSelected = "chilexpress";

            System.Diagnostics.Debug.WriteLine("regiones chilexpress: " + this.chilexpressRegions.Regions.Count);
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

        public RegionTO[] ListarRegionesCorreos(string usuario, string contrasena)
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

        public Root ListarRegionesChilexpress()
        {
            Root regiones = null;

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
                        if (strReader == null) return regiones;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody

                            regiones = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(responseBody);

                            System.Diagnostics.Debug.WriteLine(regiones.Regions);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("Error", ex);
            }

            return regiones;
        }

        public ComunaTO[] ListarComunasSegunRegion(string usuario, string contrasena, string codigoRegion)
        {
            ComunaTO[] comunas;
            try
            {
                var client = new ServicioExternoRegionYComunasSoapClient();
                comunas = client.listarComunasSegunRegion(usuario, contrasena, codigoRegion);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error intentando obtener las comunas según región.", ex);
            }
            return comunas;
        }


    }
}