using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Demo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public MainPage()
        {
            this.InitializeComponent();

            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            ToggleText = "22";
        }

        private bool? _isChecked = true;
        public bool? IsChecked
        {
            get { return _isChecked; }
            set
            {
                if(value != _isChecked)
                {
                    _isChecked = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
                }
            }
        }

        private void XPButton_OnToggleChanged(object sender, XP.ToggleEventArgs args)
        {
            Debug.WriteLine("toggle: " + args.IsChecked.ToString());
            //args.IsCancel = true;
        }

        private string _toggleText = "11";
        public string ToggleText
        {
            get { return _toggleText; }
            set
            {
                if(value != _toggleText)
                {
                    _toggleText = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ToggleText"));
                }
            }
        }

        private void XPButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("binding: " + IsChecked.ToString());
            IsChecked = !IsChecked;
        }
    }
}
