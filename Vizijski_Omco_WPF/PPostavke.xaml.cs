﻿using System.Threading;
using System.Windows;

namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for PPostavke.xaml
    /// </summary>
    /// 
    
    public partial class PPostavke
    {

        
        public PPostavke()
        {
            InitializeComponent();
            //App.PLC.Update_1_s += new PLCInterface.UpdateHandler(updatePage);
            //App.PLC.Update_100_ms += new PLCInterface.UpdateHandler(updatePage_100);
        }

        private void BiskljuciAplikaciju_Click(object sender, RoutedEventArgs e)
        {
            // Gasimo LIVE kamere
            App.HDevExp.Exitloop1 = true;
            App.HDevExp.Exitloop2 = true;
            App.HDevExp.Exitloop3 = true;
            App.HDevExp.Exitloop4 = true;
            Thread.Sleep(1000);
            App.Current.Shutdown();

        }

        private void BizbrisiPodatke_Click(object sender, RoutedEventArgs e)
        {
            App.ResetData();
        }

        //private void updatePage(object sender, PLCInterfaceEventArgs e)
        //{

        //}

        //private void updatePage_100(object sender, PLCInterfaceEventArgs e)
        //{

        //}

    }
}
