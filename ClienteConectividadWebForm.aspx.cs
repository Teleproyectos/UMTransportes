using System;
using System.IO;
using System.Net;
using UMTransporte.ServicioExternoRegionYComunas;
using UMTransporte.App_LocalResources.Chilexpress.Region;
using UMTransporte.App_LocalResources.Chilexpress.Cotizacion;
using UMTransporte.App_LocalResources.Chilexpress.Comuna;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using Newtonsoft.Json;

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
        public Envio chilexpressCotizacion = null;
        public Comuna communesChilexpress = null;
        public ComunaTO[] communes = null;
        public String regionSelected = null;
        public IList opcionesEnvios = null;

        public string userCorreos = "PRUEBA WS 1";
        public string pwdCorreos = "b9d591ae8ef9d36bb7d4e18438d6114e";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                this.ChilexpressTipoPrioritarioRadioButton.Visible = false;
                this.ChilexpressTipoExpressRadioButton.Visible = false;
                this.ChilexpressTipoExtremoRadioButton.Visible = false;
                this.ChilexpressTipoExtendidoRadioButton.Visible = false;
                this.ListarRegionesChilexpress();
                this.SetCorreosRegions();
            }
        }

        protected void RegionDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            else if(this.EmpresaTransporteChilexpressRadioButton.Checked == true)
            {
                DropDownList elementChanged = sender as DropDownList;

                this.communesChilexpress = ListarComunasChilexpressSegunRegion(elementChanged.SelectedValue);

                if (elementChanged.ID == "xRegionOrigen")
                {
                    this.xComunaOrigen.DataSource = this.communesChilexpress.coverageAreas;
                    this.xComunaOrigen.DataValueField = "countyCode";
                    this.xComunaOrigen.DataTextField = "countyName";
                    this.xComunaOrigen.DataBind();
                }
                else
                {
                    this.xComunaDestino.DataSource = this.communesChilexpress.coverageAreas;
                    this.xComunaDestino.DataValueField = "countyCode";
                    this.xComunaDestino.DataTextField = "countyName";
                    this.xComunaDestino.DataBind();
                }
            }

        }

        protected void ComunaDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.EmpresaTransporteChilexpressRadioButton.Checked == true)
            {
                if(this.xComunaOrigen.SelectedValue != "" || this.xComunaDestino.SelectedValue != "")
                {
                    CalcularEnvioChx();
                }
            }
            else if (this.EmpresaTransporteCorreosRadioButton.Checked == true)
            {
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
            var request = (HttpWebRequest) WebRequest.Create(url);
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

                            regiones = JsonConvert.DeserializeObject<Root>(responseBody);
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

        public Comuna ListarComunasChilexpressSegunRegion(string comuna)
        {
            Comuna comunas = null;
            var url = "https://testservices.wschilexpress.com/georeference/api/v1.0/coverage-areas?RegionCode="+comuna+"&type=0";

            System.Diagnostics.Debug.WriteLine(url);
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "GET";
            request.Headers["Cache-Control"] = "no-cache";
            request.Headers["Ocp-Apim-Subscription-Key"] = "e29328f7c7b4485e811a6c95eb0689f4";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return comunas;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody

                            comunas = JsonConvert.DeserializeObject<Comuna>(responseBody);
                            System.Diagnostics.Debug.WriteLine(comunas);
                        }
                    }
                }
                System.Diagnostics.Debug.WriteLine("aca");
            }
            catch (WebException ex)
            {
                Console.WriteLine("Error", ex);
            }
            //this.CalcularEnvioChx();

            return comunas;
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

        protected void TipoEnvioChilexpressRadioButton_SelectedIndexChanged(object sender, EventArgs e)
        {

            CalcularEnvioChx();

            foreach(CourierServiceOption opcionEnvio in opcionesEnvios)
            {
                if (this.ChilexpressTipoPrioritarioRadioButton.Checked == true && opcionEnvio.serviceTypeCode == 2)
                {
                    this.xValor.Text = opcionEnvio.serviceValue;
                }
                else if (this.ChilexpressTipoExpressRadioButton.Checked == true && opcionEnvio.serviceTypeCode == 3)
                {
                    this.xValor.Text = opcionEnvio.serviceValue;
                }
                else if (this.ChilexpressTipoExtendidoRadioButton.Checked == true && opcionEnvio.serviceTypeCode == 4)
                {
                    this.xValor.Text = opcionEnvio.serviceValue;
                }
                else if (this.ChilexpressTipoExtremoRadioButton.Checked == true && opcionEnvio.serviceTypeCode == 5)
                {
                    this.xValor.Text = opcionEnvio.serviceValue;
                }

            }
        }

        public Envio CalcularEnvioChx()
        {
            this.ChilexpressTipoPrioritarioRadioButton.Visible = false;
            this.ChilexpressTipoExpressRadioButton.Visible = false;
            this.ChilexpressTipoExtremoRadioButton.Visible = false;
            this.ChilexpressTipoExtendidoRadioButton.Visible = false;

            System.Diagnostics.Debug.WriteLine(this.xComunaDestino.SelectedValue);

            Envio chilexpressCotizacion = new Envio
            {
                OriginCountyCode = this.xComunaOrigen.SelectedValue,
                DestinationCountyCode = this.xComunaDestino.SelectedValue,
                Package = new Package
                {
                    Weight = this.xPeso.Text,
                    Height = this.xAlto.Text,
                    Length = this.xLargo.Text,
                    Width = this.xAncho.Text
                },
                ProductType = 3,
                ContentType = 1,
                DeclaredWorth = "2333",
                DeliveryTime = 0
            };

            var jsonChilexpress = JsonConvert.SerializeObject(chilexpressCotizacion);

            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] data = encoder.GetBytes(jsonChilexpress);

            var url = "https://testservices.wschilexpress.com/rating/api/v1.0/rates/courier?";
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers["Cache-Control"] = "no-cache";
            request.ContentLength = data.Length;
            request.Headers["Ocp-Apim-Subscription-Key"] = "a8ae1056dec0483c98d352b1a45c55fc";

            request.GetRequestStream().Write(data, 0, data.Length);
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return chilexpressCotizacion;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            
                            var resultChx = JsonConvert.DeserializeObject<Cotizacion>(responseBody);

                            IList list = resultChx.data.courierServiceOptions;
                            this.opcionesEnvios = list;
                            for (int i = 0; i < list.Count; i++)
                            {
                                var opcionEnvio = (CourierServiceOption)list[i];
                                if(opcionEnvio.serviceTypeCode == 2)
                                {
                                    this.ChilexpressTipoPrioritarioRadioButton.Visible = true;
                                }
                                else if(opcionEnvio.serviceTypeCode == 3)
                                {
                                    this.ChilexpressTipoExpressRadioButton.Visible = true;
                                }
                                else if (opcionEnvio.serviceTypeCode == 4)
                                {
                                    this.ChilexpressTipoExtendidoRadioButton.Visible = true;
                                }
                                else if (opcionEnvio.serviceTypeCode == 5)
                                {
                                    this.ChilexpressTipoExtremoRadioButton.Visible = true;
                                }
                            }

                        }
                    }
                }
                this.ReporteOperacionTextBox.Text = "CORRECTO";
            }
            catch (WebException ex)
            {
                Console.WriteLine("Error", ex);
                this.ReporteOperacionTextBox.Text = "FALLA";
            }

            return chilexpressCotizacion;
        }


    }
}