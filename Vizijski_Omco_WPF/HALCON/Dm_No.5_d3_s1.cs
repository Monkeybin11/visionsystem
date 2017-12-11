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
  private void diameter3No5S1()
  {


    // Local iconic variables
    HObject ho_Image=null, ho_Rectangle=null, ho_ImageReduced=null;
    HObject ho_Region=null, ho_RegionFillUp1=null, ho_Connection=null;
    HObject ho_SelectedRegions1=null, ho_Contours=null, ho_SmoothedContours=null;
    HObject ho_RegionOpening = null;
    // Local control variables 
    HTuple hv_AcqHandle = new HTuple(), hv_Width = new HTuple();
    HTuple hv_Height = new HTuple(), hv_SelectNumber = new HTuple();
    HTuple hv_Row = new HTuple(), hv_Col = new HTuple(), hv_TupleMax = new HTuple();
    HTuple hv_IndexMax = new HTuple(), hv_ColumMax = new HTuple();
    HTuple hv_rowToMax0 = new HTuple(), hv_colToMax0 = new HTuple();
    HTuple hv_HalfH = new HTuple(), hv_HalfW = new HTuple();
    // HTuple hv_output = new HTuple();
    // HTuple hv_outputmm = new HTuple();
    HTuple hv_Exception = null, hv_MessageError = new HTuple();

      //************************************************************
      //KOMAD NO. 5 D3 S1
      //************************************************************

      
        //Camera communication - Open
        HOperatorSet.OpenFramegrabber("GigEVision", 0, 0, 0, 0, 0, 0, "default", 
            -1, "default", -1, "false", "default", "GC3851M_CAM_4", 0, -1, out hv_AcqHandle);
        HOperatorSet.SetFramegrabberParam(hv_AcqHandle, "ExposureTime", 2500.0);
        HOperatorSet.GrabImageStart(hv_AcqHandle, -1);
        //ho_Image.Dispose();
        HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
        //Camera communication - Close
        HOperatorSet.CloseFramegrabber(hv_AcqHandle);

        //try
        //{
        //Find the edge conture
        HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);

        //ROI and Threshold
        //ho_Rectangle.Dispose();
        HOperatorSet.GenRectangle1(out ho_Rectangle, (hv_Height/2)-100, 200, (hv_Height/2)+100, 
            3200);
        //ho_ImageReduced.Dispose();
        HOperatorSet.ReduceDomain(ho_Image, ho_Rectangle, out ho_ImageReduced);
        //ho_Region.Dispose();
        HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, 0, 40);
        HOperatorSet.OpeningCircle(ho_Region, out ho_RegionOpening, 3.5);
        //ho_RegionFillUp1.Dispose();
        HOperatorSet.FillUp(ho_RegionOpening, out ho_RegionFillUp1);
        //ho_Connection.Dispose();
        HOperatorSet.Connection(ho_RegionFillUp1, out ho_Connection);

        //Select Region
        //ho_SelectedRegions1.Dispose();
        HOperatorSet.SelectShape(ho_Connection, out ho_SelectedRegions1, (new HTuple("area")).TupleConcat(
            "row"), "and", (new HTuple(100000)).TupleConcat(1380), (new HTuple(300000)).TupleConcat(
            1480));
        HOperatorSet.CountObj(ho_SelectedRegions1, out hv_SelectNumber);
        //ho_Contours.Dispose();
        HOperatorSet.GenContourRegionXld(ho_SelectedRegions1, out ho_Contours, "border");

        //Smoth edge conture
        //ho_SmoothedContours.Dispose();
        HOperatorSet.SmoothContoursXld(ho_Contours, out ho_SmoothedContours, 29);
        HOperatorSet.GetContourXld(ho_SmoothedContours, out hv_Row, out hv_Col);

        //* Define max value from tuple
        HOperatorSet.TupleMax(hv_Row, out hv_TupleMax);
        HOperatorSet.TupleFindFirst(hv_Row, hv_TupleMax, out hv_IndexMax);
        hv_ColumMax = hv_Col.TupleSelect(hv_IndexMax);
        hv_rowToMax0 = hv_Row.TupleSelect(hv_IndexMax);
        hv_colToMax0 = hv_Col.TupleSelect(hv_IndexMax);

        //Define constants:
        hv_HalfH = hv_Height/2;
        hv_HalfW = hv_Width/2;

        //Result in px
        hv_output = (-hv_HalfH)+(hv_Row.TupleSelect(hv_IndexMax));

        //Result in mm
        hv_outputmm = hv_output*0.001675;

      //}
      //catch (HalconException HDevExpDefaultException1)
      //{
      //  HDevExpDefaultException1.ToHTuple(out hv_Exception);
      //  //Error handling routine
      //  hv_MessageError = new HTuple(" ERROR: Not able to analize photo, move horizontal axis");
      //}

  }

  public void RunHalcon5()
  {
        diameter3No5S1();
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

