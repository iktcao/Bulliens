using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Boiler.Views;
using System.Windows;

namespace Boiler
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        DonateDialog donateDialog = new DonateDialog();
        AboutDialog aboutDialog = new AboutDialog();

        private async void btnDonate_Click(object sender, RoutedEventArgs e)
        {
            donateDialog.btnClose.Click += btnClose_Donate;
            await this.ShowMetroDialogAsync(donateDialog);
        }

        private async void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            aboutDialog.btnClose.Click += btnClose_About;
            await this.ShowMetroDialogAsync(aboutDialog);
        }

        private void btnClose_About(object sender, RoutedEventArgs e)
        {
            this.HideMetroDialogAsync(aboutDialog);
        }

        private void btnClose_Donate(object sender, RoutedEventArgs e)
        {
            this.HideMetroDialogAsync(donateDialog);
        }

        // 工具栏关于按钮
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    About about = new About();
        //    about.Owner = this;
        //    about.ShowDialog();
        //}
    }
}
