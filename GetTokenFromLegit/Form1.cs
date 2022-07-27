using System.Net;
using System.Text;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text.Json.Nodes;
using System.Text.Json;
using Newtonsoft.Json;
using System.Net.Http;

namespace GetTokenFromLegit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                String url = "https://api.apexclearing.local/legit/api/v1/ad/token";
                Uri uri = new Uri(url);
                HttpClient client = new 
                    HttpClient();
                string userN = Environment.UserName;
                //var credentials = new NetworkCredential.Current();
                ICredentials credent = CredentialCache.DefaultCredentials;
                NetworkCredential credentials = 
                    credent.GetCredential(uri, "Basic");
                

                var data = new Dictionary<string, string>
                {

                    {"username", "" + userN + ""},
                    {"password", "" + tbPassword.Text + ""}
                };

                var result = await client.PostAsync(url, data.AsJson());
                //result.Headers.Add("content-type", "application/json");
                var content = await result.Content.ReadAsStringAsync();
                textBox1.Text = content;          

                Debug.WriteLine(content);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public static class Extensions
    {
        public static StringContent AsJson(this object o)
            => new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");
    }
}