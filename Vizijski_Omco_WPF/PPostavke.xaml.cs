﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Drawing;
using System.IO;
using Microsoft.Win32;

namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for PPostavke.xaml
    /// </summary>
    /// 
    
    public partial class PPostavke : Page
    {
        
        public PPostavke()
        {
            InitializeComponent();
        }

        private void b_zatvori_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void b_reset_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
