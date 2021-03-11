using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace UMTransporte
{
    public partial class ClienteConectividadWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("prueba");
            RegisterAsyncTask(new PageAsyncTask(MostrarRegionesAsync));
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