using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PieTooltipCustomization
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new BaseView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
