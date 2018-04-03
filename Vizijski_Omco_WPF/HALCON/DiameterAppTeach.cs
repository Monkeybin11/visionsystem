using HalconDotNet;

namespace VizijskiSustavWPF.HALCON
{
    public partial class HDevelopExport
    {
        // Main procedure 
        private void TeachAction(HTuple hvDia, HTuple hvSide)
        {
        
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_DerivGauss);
            HOperatorSet.GenEmptyObj(out ho_RegionCrossings);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_region_outer);
            HOperatorSet.GenEmptyObj(out ho_contour_outer);
            HOperatorSet.GenEmptyObj(out ho_ContCircle);

            //try
            //{
            // Camera communication - Open
            OpenCamFrame();
            HOperatorSet.GrabImageAsync(out ho_Image, hv_AcqHandle, -1);
            // Camera communication - Close
            CloseCamFrame();

            //try
            //{

            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            //* Define constants and tuples:
            hv_HalfW = hv_Width/2;
            hv_row_len = hv_Height.Clone();
            hv_row_outer = new HTuple();
            hv_col_outer = new HTuple();
            // HOperatorSet.GenRectangle1(out ho_Rectangle, 0, hv_HalfW - 150, hv_Height,hv_HalfW + 150);
            HOperatorSet.GenRectangle1(out ho_Rectangle, 0, hv_HalfW - 250, hv_Height,hv_HalfW + 250);
            HOperatorSet.ReduceDomain(ho_Image, ho_Rectangle, out ho_ReducedImage);
            //* Edge detection
            HOperatorSet.DerivateGauss(ho_ReducedImage, out ho_DerivGauss, 1, "x");

            //* diameter 2 doesn't have a clean background => different values
            if ((int)(new HTuple(hvDia.TupleEqual(2))) != 0)
            {
                HOperatorSet.DualThreshold(ho_DerivGauss, out ho_RegionCrossings, 20, 2, 2);
                HOperatorSet.Union1(ho_RegionCrossings, out ho_Region);
            }

            else
            {
                HOperatorSet.DualThreshold(ho_DerivGauss, out ho_RegionCrossings, 20, 12, 2);
                HOperatorSet.Union1(ho_RegionCrossings, out ho_Region);
            }
            //* Retrieve points from detected edges
            HOperatorSet.GetRegionPoints(ho_Region, out hv_Rows, out hv_Cols);
            //* Side 2 => upper side closer to probe
            if ((int)(new HTuple(hvSide.TupleEqual(2))) != 0)
            {
                //* Extract the points which define the outer or inner edge
                if ((int)((new HTuple(hvDia.TupleEqual(3))).TupleOr(new HTuple(hvDia.TupleEqual(4)))) != 0)
                {
                    HTuple endVal35 = hv_row_len;
                    HTuple stepVal35 = 1;
                    for (hv_i=1; hv_i.Continue(endVal35, stepVal35); hv_i = hv_i.TupleAdd(stepVal35))
                    {
                        HOperatorSet.TupleFind(hv_Rows, hv_i-1, out hv_Indices);
                        //* if none point exists in this row replace it with an incremental interpolation
                        if ((int)(new HTuple(hv_Indices.TupleEqual(-1))) != 0)
                        {
                            if (hv_row_outer == null)
                            {
                                continue;
                            }

                            //if((int)(new HTuple(hv_row_outer.TupleEqual(new HTuple()))) != 0)
                            //{
                            //    continue;
                            //}

                            if (hv_col_outer == null)
                            {
                                continue;
                            }

                            //if((int)(new HTuple(hv_col_outer.TupleEqual(new HTuple()))) != 0)
                            //{
                            //    continue;
                            //}

                            if (hv_i > 1)
                            {
                                hv_row_outer[hv_i-1] = (hv_row_outer.TupleSelect(hv_i-2))+1;
                                hv_col_outer[hv_i-1] = hv_col_outer.TupleSelect(hv_i-2);
                            }
                            continue;
                        }

                        HOperatorSet.TupleLength(hv_Indices, out _);
                        HOperatorSet.TupleMin(hv_Cols.TupleSelect(hv_Indices), out hv_col_min);
                        HOperatorSet.TupleFind(hv_Cols.TupleSelect(hv_Indices), hv_col_min, 
                            out hv_indice_min);
                        if (hv_row_outer == null)
                            hv_row_outer = new HTuple();
                        hv_row_outer[hv_i-1] = hv_Rows.TupleSelect(hv_Indices.TupleSelect(hv_indice_min));
                        if (hv_col_outer == null)
                            hv_col_outer = new HTuple();
                        hv_col_outer[hv_i-1] = hv_Cols.TupleSelect(hv_Indices.TupleSelect(hv_indice_min));
                    }
                }

                else if ((int)((new HTuple(hvDia.TupleEqual(1))).TupleOr(new HTuple(hvDia.TupleEqual(2)))) != 0)
                {
                    HTuple endVal50 = hv_row_len;
                    HTuple stepVal50 = 1;
                    for (hv_i=1; hv_i.Continue(endVal50, stepVal50); hv_i = hv_i.TupleAdd(stepVal50))
                    {
                        HOperatorSet.TupleFind(hv_Rows, hv_i-1, out hv_Indices);
                        //* if none point exists in this row replace it with an incremental interpolation
                        if ((int)(new HTuple(hv_Indices.TupleEqual(-1))) != 0)
                        {
                            if (hv_row_outer == null)
                            {
                                continue;
                            }

                            //if((int)(new HTuple(hv_row_outer.TupleEqual(new HTuple()))) != 0)
                            //{
                            //    continue;
                            //}

                            if (hv_col_outer == null)
                            {
                                continue;
                            }

                            //if((int)(new HTuple(hv_col_outer.TupleEqual(new HTuple()))) != 0)
                            //{
                            //    continue;
                            //}

                            if (hv_i > 1)
                            {
                                hv_row_outer[hv_i-1] = (hv_row_outer.TupleSelect(hv_i-2))+1;
                                hv_col_outer[hv_i-1] = hv_col_outer.TupleSelect(hv_i-2);
                            }
                            continue;
                        }

                        HOperatorSet.TupleLength(hv_Indices, out _);
                        HOperatorSet.TupleMax(hv_Cols.TupleSelect(hv_Indices), out hv_col_max);
                        HOperatorSet.TupleFind(hv_Cols.TupleSelect(hv_Indices), hv_col_max, 
                            out hv_indice_max);
                        if (hv_row_outer == null)
                            hv_row_outer = new HTuple();
                        hv_row_outer[hv_i-1] = hv_Rows.TupleSelect(hv_Indices.TupleSelect(hv_indice_max));
                        if (hv_col_outer == null)
                            hv_col_outer = new HTuple();
                        hv_col_outer[hv_i-1] = hv_Cols.TupleSelect(hv_Indices.TupleSelect(hv_indice_max));
                    }
                }

                //* retrieve the outer or inner edge points + cirlce fitting
                HOperatorSet.GenRegionPoints(out ho_region_outer, hv_row_outer, hv_col_outer);
                HOperatorSet.GenContourPolygonXld(out ho_contour_outer, hv_row_outer, hv_col_outer);
                HOperatorSet.FitCircleContourXld(ho_contour_outer, "geotukey", -1, 0, 0, 
                    3, 2, out hv_Row, out hv_Col, out hv_Radius, out _, out _, 
                    out _);
                HOperatorSet.GenCircleContourXld(out ho_ContCircle, hv_Row, hv_Col, hv_Radius, 
                    0, 6.28318, "positive", 1);
                HOperatorSet.GetContourXld(ho_ContCircle, out hv_Row, out hv_Col);

                //* find the maximum of the estimated circle
                HOperatorSet.TupleMax(hv_Col, out hv_TupleMax);
                HOperatorSet.TupleFindFirst(hv_Col, hv_TupleMax, out hv_IndexMax);

                //* calculate pixel and mm outputs
                hv_colToMax0 = hv_Col.TupleSelect(hv_IndexMax);
                hv_output = (-hv_HalfW)+hv_colToMax0;

                // Display S2
                HOperatorSet.DispObj(ho_Image, hv_TeachWinHandle);
                HOperatorSet.DispObj(ho_ContCircle, hv_TeachWinHandle);
            }

            //* Side 1 => side closer to vertical moving axis
            if ((int)(new HTuple(hvSide.TupleEqual(1))) != 0)
            {
                //* Extract the points which define the outer or inner edge
                if ((int)((new HTuple(hvDia.TupleEqual(3))).TupleOr(new HTuple(hvDia.TupleEqual(4)))) != 0)
                {
                    HTuple endVal87 = hv_row_len;
                    HTuple stepVal87 = 1;
                    for (hv_i=1; hv_i.Continue(endVal87, stepVal87); hv_i = hv_i.TupleAdd(stepVal87))
                    {
                        HOperatorSet.TupleFind(hv_Rows, hv_i-1, out hv_Indices);
                        //* if none point exists in this row replace it with an incremental interpolation
                        if ((int)(new HTuple(hv_Indices.TupleEqual(-1))) != 0)
                        {
                            if (hv_row_outer == null)
                            {
                                continue;
                            }

                            //if((int)(new HTuple(hv_row_outer.TupleEqual(new HTuple()))) != 0)
                            //{
                            //    continue;
                            //}

                            if (hv_col_outer == null)
                            {
                                continue;
                            }

                            //if ((int)(new HTuple(hv_col_outer.TupleEqual(new HTuple()))) != 0)
                            //{
                            //    continue;
                            //}

                            if (hv_i > 1)
                            {
                                hv_row_outer[hv_i-1] = (hv_row_outer.TupleSelect(hv_i-2))+1;
                                hv_col_outer[hv_i-1] = hv_col_outer.TupleSelect(hv_i-2);
                            }
                            continue;
                        }

                        HOperatorSet.TupleLength(hv_Indices, out _);
                        HOperatorSet.TupleMax(hv_Cols.TupleSelect(hv_Indices), out hv_col_max);
                        HOperatorSet.TupleFind(hv_Cols.TupleSelect(hv_Indices), hv_col_max, 
                            out hv_indice_max);
                        if (hv_row_outer == null)
                            hv_row_outer = new HTuple();
                        hv_row_outer[hv_i-1] = hv_Rows.TupleSelect(hv_Indices.TupleSelect(hv_indice_max));
                        if (hv_col_outer == null)
                            hv_col_outer = new HTuple();
                        hv_col_outer[hv_i-1] = hv_Cols.TupleSelect(hv_Indices.TupleSelect(hv_indice_max));
                    }
                }
                else if ((int)((new HTuple(hvDia.TupleEqual(1))).TupleOr(new HTuple(hvDia.TupleEqual(2)))) != 0)
                {
                    HTuple endVal102 = hv_row_len;
                    HTuple stepVal102 = 1;
                    for (hv_i=1; hv_i.Continue(endVal102, stepVal102); hv_i = hv_i.TupleAdd(stepVal102))
                    {
                        HOperatorSet.TupleFind(hv_Rows, hv_i-1, out hv_Indices);
                        //* if none point exists in this row replace it with an incremental interpolation
                        if ((int)(new HTuple(hv_Indices.TupleEqual(-1))) != 0)
                        {
                            if (hv_row_outer == null)
                            {
                                continue;
                            }

                            //if((int)(new HTuple(hv_row_outer.TupleEqual(new HTuple()))) != 0)
                            //{
                            //    continue;
                            //}

                            if (hv_col_outer == null)
                            {
                                continue;
                            }

                            //if((int)(new HTuple(hv_col_outer.TupleEqual(new HTuple()))) != 0)
                            //{
                            //    continue;
                            //}

                            if (hv_i > 1)
                            {
                                hv_row_outer[hv_i-1] = (hv_row_outer.TupleSelect(hv_i-2))+1;
                                hv_col_outer[hv_i-1] = hv_col_outer.TupleSelect(hv_i-2);
                            }
                            continue;
                        }

                        HOperatorSet.TupleLength(hv_Indices, out _);
                        HOperatorSet.TupleMin(hv_Cols.TupleSelect(hv_Indices), out hv_col_min);
                        HOperatorSet.TupleFind(hv_Cols.TupleSelect(hv_Indices), hv_col_min, 
                            out hv_indice_min);
                        if (hv_row_outer == null)
                            hv_row_outer = new HTuple();
                        hv_row_outer[hv_i-1] = hv_Rows.TupleSelect(hv_Indices.TupleSelect(hv_indice_min));
                        if (hv_col_outer == null)
                            hv_col_outer = new HTuple();
                        hv_col_outer[hv_i-1] = hv_Cols.TupleSelect(hv_Indices.TupleSelect(hv_indice_min));
                    }
                }

                //* retrieve the outer or inner edge points + cirlce fitting
                HOperatorSet.GenRegionPoints(out ho_region_outer, hv_row_outer, hv_col_outer);
                HOperatorSet.GenContourPolygonXld(out ho_contour_outer, hv_row_outer, hv_col_outer);
                HOperatorSet.FitCircleContourXld(ho_contour_outer, "geotukey", -1, 0, 0, 
                    3, 2, out hv_Row, out hv_Col, out hv_Radius, out _, out _, 
                    out _);
                HOperatorSet.GenCircleContourXld(out ho_ContCircle, hv_Row, hv_Col, hv_Radius, 
                    0, 6.28318, "positive", 1);
                HOperatorSet.GetContourXld(ho_ContCircle, out hv_Row, out hv_Col);
                    
                //* find the minimum of the estimated circle
                HOperatorSet.TupleMin(hv_Col, out hv_TupleMin);
                HOperatorSet.TupleFindFirst(hv_Col, hv_TupleMin, out hv_IndexMin);

                //* calculate pixel and mm outputs
                hv_colToMin0 = hv_Col.TupleSelect(hv_IndexMin);
                hv_output = hv_HalfW-hv_colToMin0;

                // Display S1
                HOperatorSet.DispObj(ho_Image, hv_TeachWinHandle);
                HOperatorSet.DispObj(ho_ContCircle, hv_TeachWinHandle);
            }

            //}
            //// catch (Exception) 
            //catch (HalconException HDevExpDefaultException1)
            //{
            //HDevExpDefaultException1.ToHTuple(out hv_Exception);
            //hv_MessageError = new HTuple(" ERROR: Not able to analize photo, move horizontal axis");
            //}

            //}
            //catch (HalconException HDevExpDefaultException)
            //{
            //    ho_TestImage.Dispose();
            //    ho_DerivGauss.Dispose();
            //    ho_RegionCrossings.Dispose();
            //    ho_Region.Dispose();
            //    ho_region_outer.Dispose();
            //    ho_contour_outer.Dispose();
            //    ho_ContCircle.Dispose();

            //    throw HDevExpDefaultException;
            //}
            ho_Image.Dispose();
            ho_DerivGauss.Dispose();
            ho_RegionCrossings.Dispose();
            ho_Region.Dispose();
            ho_region_outer.Dispose();
            ho_contour_outer.Dispose();
            ho_ContCircle.Dispose();
        }

        // D1 S1 Call
        public void RunHalcon16(HTuple window)
        {
            hv_TeachWinHandle = window;
            HOperatorSet.ClearWindow(hv_TeachWinHandle);
            TeachAction(1, 1);
        }
        // D1 S2 Call
        public void RunHalcon17(HTuple window)
        {
            hv_TeachWinHandle = window;
            HOperatorSet.ClearWindow(hv_TeachWinHandle);
            TeachAction(1, 2);
        }
        // D2 S1 Call
        public void RunHalcon18(HTuple window)
        {
            hv_TeachWinHandle = window;
            HOperatorSet.ClearWindow(hv_TeachWinHandle);
            TeachAction(2, 1);
        }
        // D2 S2 Call
        public void RunHalcon19(HTuple window)
        {
            hv_TeachWinHandle = window;
            HOperatorSet.ClearWindow(hv_TeachWinHandle);
            TeachAction(2, 2);
        }
        // D3 S1 Call
        public void RunHalcon20(HTuple window)
        {
            hv_TeachWinHandle = window;
            HOperatorSet.ClearWindow(hv_TeachWinHandle);
            TeachAction(3, 1);
        }
        // D3 S2 Call
        public void RunHalcon21(HTuple window)
        {
            hv_TeachWinHandle = window;
            HOperatorSet.ClearWindow(hv_TeachWinHandle);
            TeachAction(3, 2);
        }
        // D4 S1 Call
        public void RunHalcon22(HTuple window)
        {
            hv_TeachWinHandle = window;
            HOperatorSet.ClearWindow(hv_TeachWinHandle);
            TeachAction(4, 1);
        }
        // D4 S2 Call
        public void RunHalcon23(HTuple window)
        {
            hv_TeachWinHandle = window;
            HOperatorSet.ClearWindow(hv_TeachWinHandle);
            TeachAction(4, 2);
        }

    }
}

