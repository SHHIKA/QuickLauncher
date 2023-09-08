using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuickLauncher.Lib.QUWindow
{
    public partial class Translation : Window
    {
        public Translation()
        {
            InitializeComponent();
            
            ToTranslate.Focus();
        }

        private async void ToTranslate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            string textToTranslate = ToTranslate.Text;
            string apiUrl = "https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl=ja&dt=t&q=" + Uri.EscapeDataString(textToTranslate);

            using HttpClient client = new();

            HttpResponseMessage response = await client.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            string translatedText = ParseGoogleTranslateResponse(responseBody);

            FromTranslate.Text = translatedText;
        }

        private static string ParseGoogleTranslateResponse(string responseBody)
        {
            string[] responseParts = responseBody.Split('"');

            if (responseParts.Length >= 2) return responseParts[1];
            else return "翻訳できませんでした";
        }
    }
}
