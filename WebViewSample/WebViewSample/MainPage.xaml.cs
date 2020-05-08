using Ganss.XSS;
using SmartReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WebViewSample
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var _url = Entry.Text;
            GetCleanView(_url);
        }

        async Task GetCleanView(string _url)
        {

            try
            {
                //throw new Exception();
                var reader = new Reader(_url);
                var article = await reader?.GetArticleAsync();
                var images = await article?.GetImagesAsync();
                if (article.IsReadable)
                {
                    var sanitizer = new HtmlSanitizer();
                    var _Html = sanitizer.Sanitize(article?.Content);
                    var html = new HtmlWebViewSource { Html = _Html };
                    WebView.Source = html;
                }
            }
            catch (Exception ex)
            {
                Label.Text = "Error";
                WebView.Source = _url;
            }
        }
    }
}
