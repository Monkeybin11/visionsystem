//
// File generated by HDevelop for HALCON/.NET (C#) Version 13.0.1.1
//
//  This file is intended to be used with the HDevelopTemplate or
//  HDevelopTemplateWPF projects located under %HALCONEXAMPLES%\c#

using System;
using HalconDotNet;

public partial class HDevelopExport
{

  // Main procedure 
  private void diameter4No5S2()
  {

    // Local iconic variables
    HObject ho_Image=null, ho_Rectangle=null, ho_ImageReduced=null;
    HObject ho_EdgeAmplitude=null, ho_EdgeDirection=null, ho_ImageConverted=null;
    HObject ho_ImageMedian=null, ho_Regions=null, ho_RegionFillUp1=null;
    HObject ho_Connection=null, ho_SelectedRegions1=null, ho_Contours=null;
    HObject ho_SmoothedContours=null;

    // Local control variables 
    HTuple hv_AcqHandle = new HTuple(), hv_Width = new HTuple();
    HTuple hv_Height = new HTuple(), hv_SelectNumber = new HTuple();
    HTuple hv_Row = new HTuple(), hv_Col = new HTuple(), hv_TupleMin = new HTuple();
    HTuple hv_IndexMin = new HTuple(), hv_ColumMin = new HTuple();
    HTuple hv_rowToMin0 = new HTuple(), hv_colToMin0 = new HTuple();
    HTuple hv_HalfH = new HTuple(), hv_HalfW = new HTuple();
    //HTuple hv_output = new HTuple(); 
    //HTuple hv_outputmm = new HTuple();
    HTuple hv_Exception = null, hv_MessageError = new HTuple();
    
      //************************************************************
      //KOMAD NO. 5 D4 S2
      //************************************************************

      //try
      //{
        //Camera communication - Open
        HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", 
            -1, "default", -1, "false", "default", "GC3851M_CAM_4", 0, -1, out hv_AcqHandle);
        HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 1500.0);
        HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
        //ho_Image.Dispose();
        HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
        //Camera communication - Close
        HOperatorSet.CloseFramegrabber(hv_AcqHandle);


        //Find the edge conture
        HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
        //ho_Rectangle.Dispose();
        HOperatorSet.GenRectangle1(out ho_Rectangle, hv_Height - 2600, (hv_Width / 2) - 120,
            hv_Height - 200, (hv_Width / 2) + 120);
        //ho_ImageReduced.Dispose();
        HOperatorSet.ReduceDomain(ho_Image, ho_Rectangle, out ho_ImageReduced);
        //Image enhancement/processing
        //ho_EdgeAmplitude.Dispose(); ho_EdgeDirection.Dispose();
        HOperatorSet.SobelDir(ho_ImageReduced, out ho_EdgeAmplitude, out ho_EdgeDirection,
            "sum_sqrt", 13);
        //ho_ImageConverted.Dispose();
        HOperatorSet.ConvertImageType(ho_EdgeDirection, out ho_ImageConverted, "byte");
        //ho_ImageMedian.Dispose();
        HOperatorSet.MedianImage(ho_ImageConverted, out ho_ImageMedian, "square",
            7, 0);
        //ho_Regions.Dispose();
        HOperatorSet.Threshold(ho_ImageMedian, out ho_Regions, 200, 255);
        //ho_RegionFillUp1.Dispose();
        HOperatorSet.FillUp(ho_Regions, out ho_RegionFillUp1);
        //ho_Connection.Dispose();
        HOperatorSet.Connection(ho_Regions, out ho_Connection);
        //ho_SelectedRegions1.Dispose();
        HOperatorSet.SelectShape(ho_Connection, out ho_SelectedRegions1, "area",
            "and", 50000, 1500000);
        HOperatorSet.CountObj(ho_SelectedRegions1, out hv_SelectNumber);
        //ho_Contours.Dispose();
        HOperatorSet.GenContourRegionXld(ho_SelectedRegions1, out ho_Contours, "border");
        //ho_SmoothedContours.Dispose();
        HOperatorSet.SmoothContoursXld(ho_Contours, out ho_SmoothedContours, 29);
        HOperatorSet.GetContourXld(ho_SmoothedContours, out hv_Row, out hv_Col);
        //* Define min value from tuple
        HOperatorSet.TupleMax(hv_Col, out hv_TupleMin);
        HOperatorSet.TupleFindFirst(hv_Col, hv_TupleMin, out hv_IndexMin);

        //Define constants:
        hv_HalfH = hv_Height / 2;
        hv_HalfW = hv_Width / 2;

        hv_colToMin0 = (hv_Col.TupleSelect(hv_IndexMin)) + 13;
        //Result in px
        hv_output = (-hv_HalfW) + hv_colToMin0;
        //Result in mm
        hv_outputmm = hv_output * 0.001675;

        //}
        //// catch (Exception) 
        //catch (HalconException HDevExpDefaultException1)
        //{
        //  HDevExpDefaultException1.ToHTuple(out hv_Exception);
        //  //Error handling routine
        //  hv_MessageError = new HTuple(" ERROR: Not able to analize photo, move horizontal axis");
        //}

    }

  public void RunHalcon8()
  {
        diameter4No5S2();
        argumenti.PXvalue = (float)hv_output.D;
        
        // Chech for infinity Double to float conversion
        if (float.IsPositiveInfinity(argumenti.PXvalue))
        {
            argumenti.PXvalue = float.MaxValue;
        }
        else if (float.IsNegativeInfinity(argumenti.PXvalue))
        {
            argumenti.PXvalue = float.MinValue;
        }

        if (UpdateResult != null)
            UpdateResult(this, argumenti);
  }

}

