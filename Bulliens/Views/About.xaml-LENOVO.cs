﻿using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;

namespace SteamDome.Views
{
    /// <summary>
    /// About.xaml 的交互逻辑
    /// </summary>
    public partial class About : Window
    {
        private ObservableCollection<IconInfo> iconInfoList;
        public About()
        {
            InitializeComponent();

            WarningInfo.Text += " This computer program is protected by copyright law and international treaties. Unauthoized"
            + "\n" + "reproduction or distribution of this program, or any portion of it, may result in severe civil and criminal"
            + "\n" + "penalties, and will be prosecuted to the maximum extent possible under the law.";

            //利用反射的方式获取assembly的version信息
            pbc_version.Content = "Version:     " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            //利用反射的方式获取assembly的version信息
            //pbc_version.Content = "Version:     " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); 

            iconInfoList = new ObservableCollection<IconInfo>();

            iconInfoList.Add(new IconInfo("/Images/user.png", "https://www.flaticon.com/authors/chanut-is-industries", "Chanut is Industries"));
            iconInfoList.Add(new IconInfo("/Images/password.png", "https://www.flaticon.com/authors/freepik", "Freepik"));
            iconInfoList.Add(new IconInfo("/Images/user.png", "https://www.flaticon.com/authors/chanut-is-industries", "Chanut is Industries"));
            iconInfoList.Add(new IconInfo("/Images/password.png", "https://www.flaticon.com/authors/freepik", "Freepik"));
            iconInfoList.Add(new IconInfo("/Images/user.png", "https://www.flaticon.com/authors/chanut-is-industries", "Chanut is Industries"));
            iconInfoList.Add(new IconInfo("/Images/password.png", "https://www.flaticon.com/authors/freepik", "Freepik"));
            iconInfoList.Add(new IconInfo("/Images/user.png", "https://www.flaticon.com/authors/chanut-is-industries", "Chanut is Industries"));
            iconInfoList.Add(new IconInfo("/Images/password.png", "https://www.flaticon.com/authors/freepik", "Freepik"));

            this.lvIconInfo.ItemsSource = iconInfoList;
        }

        private void linkHelp_Click_1(object sender, RoutedEventArgs e)
        {
            //Hyperlink link = sender as Hyperlink;
            //Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
            if (sender.GetType() != typeof(Hyperlink))
                return;
            string link = ((Hyperlink)sender).NavigateUri.ToString();
            Process.Start(link);
        }
    }

    public class IconInfo
    {
        private string icon;
        private string url;
        private string author;

        public IconInfo(string icon, string url, string author)
        {
            this.icon = icon;
            this.url = url;
            this.author = author;
        }

        public string Icon
        {
            get
            {
                return this.icon;
            }
            set
            {
                this.icon = value;
            }
        }

        public string Url
        {
            get
            {
                return this.url;
            }
            set
            {
                this.url = value;
            }
        }

        public string Author
        {
            get
            {
                return this.author;
            }
            set
            {
                this.author = value;
            }
        }
    }
}
