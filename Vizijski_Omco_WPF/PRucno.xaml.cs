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
using HalconDotNet;
using System.Threading;

namespace VizijskiSustavWPF
{
    /// <summary>
    /// Interaction logic for PRucno.xaml
    /// </summary>
    public partial class PRucno : Page
    {
        //private HDevelopExport HDevExp;

        
        public PRucno() // Constructor
        {
            InitializeComponent();
            App.HDevExp = new HDevelopExport(); // New Object
            App.PLC.Update_100_ms += new PLCInterface.UpdateHandler(updatePagePRucno_100ms);
            App.PLC.Update_1_s += new PLCInterface.UpdateHandler(updatePagePRucno_1s);
        }

        private void LiveCam1() // Method
        {
            App.HDevExp.InitHalcon();
            HTuple WindowID = hWindowControlWPF1.HalconID;
            App.HDevExp.RunHalcon11(WindowID);
        }

        private void LiveCam2() // Method
        {
            App.HDevExp.InitHalcon();
            HTuple WindowID = hWindowControlWPF1.HalconID;
            App.HDevExp.RunHalcon9(WindowID);   
        }

        private void LiveCam4() // Method
        {
            App.HDevExp.InitHalcon();
            HTuple WindowID = hWindowControlWPF1.HalconID;
            App.HDevExp.RunHalcon10(WindowID);
        }


        private void updatePagePRucno_100ms(object sender, PLCInterfaceEventArgs e)
        {
          

        
        }

        private void updatePagePRucno_1s(object sender, PLCInterfaceEventArgs e)
        {


        }

        private void b_ukljucikameru1_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Exitloop1 = false;
            App.HDevExp.Exitloop2 = true;
            //App.HDevExp.Exitloop3 = true;
            App.HDevExp.Exitloop4 = true; 
            b_ukljucikameru1.IsEnabled = false;
            b_ukljucikameru2.IsEnabled = true;
            b_ukljucikameru3.IsEnabled = true;
            b_ukljucikameru4.IsEnabled = true;
            // CAM1 call
            Thread LiveCam1Thread = new Thread(new ThreadStart(this.LiveCam1));
            LiveCam1Thread.Start();
        }

        private void b_ukljucikameru2_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Exitloop1 = true;
            App.HDevExp.Exitloop2 = false;
            //App.HDevExp.Exitloop3 = true;
            App.HDevExp.Exitloop4 = true;   
            b_ukljucikameru1.IsEnabled = true;
            b_ukljucikameru2.IsEnabled = false;
            b_ukljucikameru3.IsEnabled = true;
            b_ukljucikameru4.IsEnabled = true;
            // CAM2 call
            Thread LiveCam2Thread = new Thread(new ThreadStart(this.LiveCam2));
            LiveCam2Thread.Start();
        }


        private void b_ukljucikameru3_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Exitloop1 = true;
            App.HDevExp.Exitloop2 = true;
            //App.HDevExp.Exitloop3 = false;
            App.HDevExp.Exitloop4 = true;
            b_ukljucikameru1.IsEnabled = true;
            b_ukljucikameru2.IsEnabled = true;
            b_ukljucikameru3.IsEnabled = false;
            b_ukljucikameru4.IsEnabled = true;
            // CAM3 call
        }

        private void b_ukljucikameru4_Click(object sender, RoutedEventArgs e)
        {
            App.HDevExp.Exitloop1 = true;
            App.HDevExp.Exitloop2 = true;
            //App.HDevExp.Exitloop3 = true;
            App.HDevExp.Exitloop4 = false;
            b_ukljucikameru1.IsEnabled = true;
            b_ukljucikameru2.IsEnabled = true;
            b_ukljucikameru3.IsEnabled = true;
            b_ukljucikameru4.IsEnabled = false;
            // CAM4 call
            Thread LiveCam4Thread = new Thread(new ThreadStart(this.LiveCam4));
            LiveCam4Thread.Start();
        }


    }

}
