Imports SPAM
Imports OmniDriver
Imports System.IO

Module modGlobal

    Public wrapper As OmniDriver.CCoWrapper                     'spectrometer object
    Public dMaxWL As Double                                     'wavelength of max pixel
    Public dMaxPixel As Double                                  'max pixel of spectrometer
    Public iEDarkCorrection As Integer                          'electronic dark correction
    Public iIntegrationTime As Integer                          'integration time
    Public dRefIntTime As Double                                'reference int time
    Public dBlueIntTime As Double                               'blue int time
    Public iNumPix As Integer                                   'number of pixels

    Public WhiteDarkPixels() As Double                          'dark pixels from sample
    Public BlueDarkPixels() As Double                            'dark reference pixels for calibration array
    Public samplePixels() As Double                             'sample pixels
    Public burnPixels() As Double
    Public wavelengthArray() As Double                          'wavelength array
    Public energyArray() As Double                              'energy array (irradiance)
    Public calibrationArray() As Double                         'calibration array for irradiance
    Public energyArrayW() As Double                             'energy array in watts for power and flux calculations
    Public maxEnergy As Double                                  'max energy for scaling purposes
    Public aResultPack(14) As Double                            'result pack used for easy looping for datagrid
    Public ScanData(3000, 250) As Double

    Public fRa, fR9, fCIEx, fCIEy, fDWL, fCCT, fu_prm, fv_prm, flux, lumens As Double             'color and CRI variables from Spectrometer
    Public blu_x, blu_y As Double
    Public IS_Num As String
    Public iSysType As Integer
    Public dFactors(3) As Double

    Public BinType As String
    Public ctrl_ellipse(128, 1) As Double
    Public spec_ellipse(128, 1) As Double
    Public philipsC_Ellipse(128, 1) As Double
    Public philipsA_ellipse(128, 1) As Double

    Public abtheta_PC(2) As Double
    Public abtheta_PA(2) As Double
    Public abtheta_std(2) As Double

    Public ce_specL, ce_specH As Double

    Public numSamples As Integer
    Public nBins As Integer

    Public aModV() As Double
    Public sCorrFileFullName As String      'full name of correlation file including path
    Public sCorrFN As String                'name of correlation file
    Public DarkAvg As Double
    Public photons As Double

    Public blue_watt, blue_photons As Double
    Public blue_startWL, blue_stopwl As Double
    Public pwr_startWL, pwr_stopwl As Double

    Public Const hc As Double = 1.98782E-25
    Public iDarkExpire As Integer

    Public ScanReadyTime As Integer
    Public dBlueDarkAvg, dWhiteDarkAvg, dblueDarkSTD, dWhiteDarkSTD As Double
    Public baselineThresh As Integer
    Public saveType As Integer
    Public SaveTime As Date
    Public AutoSaveInterval As Integer

    Public AcqSuccess As Boolean
    Public sampledesc As String
    Public promptIdx As Integer

    Public WS_Ref(2, 3) As Double
    Public WS_Date As String
    Public WS_Current(2, 3) As Double
    Public calFN As String
    Public SID As String = ""           'sample ID for Mechanical Measurements
    Public goldWS As Boolean = False
    Public int_wait As Integer
    Public StartLoad As Boolean = False
    Public wsDate As String
    Public wsEngine As String
    Public darkTime As Date
    Public autoWhite As Integer
    Public autoBlue As Integer
    Public iSaveScanNo As Integer = -1
    Public dArea As Double '= 1 '0.00125664
    Public bIS As Boolean
    Public iS_Philips As Integer
    Public binIdx As Integer

    Public pwrSplyModel As Integer
    Public numParts As Integer     'different parts families, read from system file
    Public targ_yPC, targ_xPC, targ_yPA, targ_xPA As Double
    Public bin_maxx, bin_minx, bin_maxy, bin_miny As Double
    Public targ_x_std, targ_y_std As Double
    Public bin_spec As Integer
    Public binID As String
    Public airConnect As Integer
    Public id_cri, id_cct, id_ptype, id_fn As String
    Public gldSamplesCnt As Integer
    Public SampleID(9) As String
    Public BaseFN As String
    Public sampleSetID As String
    Public wknum As Integer
    Public bAutoSession As Boolean = False
    Public iBluePeakCnts As Integer
    Public ceTol, criTol, clrTol As Double
    Public TestINT_Time As Double
    Public dBoxFactory, dBoxInline As String
    Public intrinsicX, intrinsicY As Double
    Public intrinsicCE, intrinsicCRI As Double
    Public pnlID As String = "X"
    Public id_wo As String = ""
    Public rlyport As Integer

    Public Sub PlotOsramEllipse(ByVal num As Integer, ByVal cht As DataVisualization.Charting.Chart)
        Dim i As Integer

        Call LoadEllipses(num)          'load ellipses from file
        cht.Series.Clear()
        With cht.ChartAreas(0)
            .AxisX.Title = "x"
            .AxisY.Title = "y"
        End With
        Call GetZoomRange()
        Zoom(cht)

        If iS_Philips = 0 Then
            cht.Series.Add("Ctrl")          'add 1 macadam series and set parameters
            With cht.Series("Ctrl")
                .Color = Color.Green
                .ChartType = DataVisualization.Charting.SeriesChartType.FastLine
                For i = 0 To 128
                    .Points.AddXY(ctrl_ellipse(i, 0), ctrl_ellipse(i, 1))     'draw points
                Next
            End With

            cht.Series.Add("Spec")          'add 1 macadam series and set parameters
            With cht.Series("Spec")
                .Color = Color.Red
                .ChartType = DataVisualization.Charting.SeriesChartType.FastLine
                For i = 0 To 128
                    .Points.AddXY(spec_ellipse(i, 0), spec_ellipse(i, 1))     'draw points
                Next
            End With

        Else

            cht.Series.Add("Low")
            With cht.Series("Low")
                .Color = Color.Green
                .ChartType = DataVisualization.Charting.SeriesChartType.FastLine
                For i = 0 To 128
                    .Points.AddXY(philipsA_ellipse(i, 0), philipsA_ellipse(i, 1))     'draw points
                Next
            End With

            cht.Series.Add("High")
            With cht.Series("High")
                .Color = Color.Blue
                .ChartType = DataVisualization.Charting.SeriesChartType.FastLine
                For i = 0 To 128
                    .Points.AddXY(philipsC_Ellipse(i, 0), philipsC_Ellipse(i, 1))     'draw points
                Next
            End With
        End If

        cht.Series.Add("Data")              'add data points and plot
        With cht.Series("Data")
            .Color = Color.DarkGray
            .ChartType = DataVisualization.Charting.SeriesChartType.Point
            For i = 0 To frmMain.dgResults.RowCount - 1
                If frmMain.dgResults.Rows(i).Cells(14).Value < baselineThresh Then
                    Application.DoEvents()
                Else
                    .Points.AddXY(frmMain.dgResults.Rows(i).Cells(7).Value, frmMain.dgResults.Rows(i).Cells(8).Value)
                End If
            Next

        End With

        cht.Series.Add("Blu_Ref")           'add blue ref line series
        With cht.Series("Blu_Ref")
            .Color = Color.DarkBlue
            .ChartType = DataVisualization.Charting.SeriesChartType.FastLine
        End With

        cht.Series.Add("Current")
        With cht.Series("Current")
            .Color = Color.DarkRed
            .ChartType = DataVisualization.Charting.SeriesChartType.Point
            If frmMain.dgResults.RowCount > 0 Then
                If frmMain.dgResults.Rows(frmMain.dgResults.RowCount - 1).Cells(14).Value < baselineThresh Then         'dark row
                    Application.DoEvents()
                Else
                    .Points.AddXY(frmMain.dgResults.Rows(frmMain.dgResults.RowCount - 1).Cells(7).Value, frmMain.dgResults.Rows(frmMain.dgResults.RowCount - 1).Cells(8).Value)
                End If
            End If
        End With

    End Sub

    Public Sub LoadEllipses(ByVal num As Integer)
        'open ellipse file, assign macadam arrays
        Dim sReader As StreamReader
        Dim stemp As String
        Dim FName As String


        FName = Application.StartupPath & "\" & BinType & "_PhillipsAC.cfg"

        sReader = New StreamReader(FName)
        stemp = sReader.ReadLine            'burn header

        stemp = sReader.ReadLine
        TestINT_Time = stemp.Substring(stemp.IndexOf(",") + 1)     'intTime         read integration time from ellipse file and set
        frmMain.neInt_W.Value = TestINT_Time
        stemp = sReader.ReadLine
        stemp = stemp.Substring(stemp.IndexOf(",") + 1)     'discard header

        ce_specL = stemp.Substring(0, stemp.IndexOf(","))
        ce_specH = stemp.Substring(stemp.IndexOf(",") + 1)

        stemp = sReader.ReadLine            'read center point
        stemp = stemp.Substring(stemp.IndexOf(",") + 1)     'discard header
        targ_xPA = stemp.Substring(0, stemp.IndexOf(","))
        targ_yPA = stemp.Substring(stemp.IndexOf(",") + 1)
        stemp = sReader.ReadLine            'read spec criteria
        stemp = stemp.Substring(stemp.IndexOf(",") + 1)     'discard header
        abtheta_PA(0) = stemp.Substring(0, stemp.IndexOf(","))
        stemp = stemp.Substring(stemp.IndexOf(",") + 1)
        abtheta_PA(1) = stemp.Substring(0, stemp.IndexOf(","))
        abtheta_PA(2) = stemp.Substring(stemp.IndexOf(",") + 1)

        stemp = sReader.ReadLine            'read center point
        stemp = stemp.Substring(stemp.IndexOf(",") + 1)     'discard header
        targ_xPC = stemp.Substring(0, stemp.IndexOf(","))
        targ_yPC = stemp.Substring(stemp.IndexOf(",") + 1)
        stemp = sReader.ReadLine            'read spec criteria
        stemp = stemp.Substring(stemp.IndexOf(",") + 1)     'discard header
        abtheta_PC(0) = stemp.Substring(0, stemp.IndexOf(","))
        stemp = stemp.Substring(stemp.IndexOf(",") + 1)
        abtheta_PC(1) = stemp.Substring(0, stemp.IndexOf(","))
        abtheta_PC(2) = stemp.Substring(stemp.IndexOf(",") + 1)

        For i = 0 To 128
            stemp = sReader.ReadLine
            philipsA_ellipse(i, 0) = Val(stemp.Substring(0, stemp.IndexOf(",")))
            stemp = stemp.Substring(stemp.IndexOf(",") + 1)
            philipsA_ellipse(i, 1) = Val(stemp.Substring(0, stemp.IndexOf(",")))
            stemp = stemp.Substring(stemp.IndexOf(",") + 1)
            philipsC_Ellipse(i, 0) = Val(stemp.Substring(0, stemp.IndexOf(",")))
            philipsC_Ellipse(i, 1) = Val(stemp.Substring(stemp.IndexOf(",") + 1))
        Next

        sReader.Close()

    End Sub

    Public Sub GetZoomRange()

        bin_maxX = 0 : bin_minX = 1 : bin_minY = 1 : bin_maxY = 0

        If iS_Philips = 0 Then
            For i = 0 To 128
                If bin_maxx < spec_ellipse(i, 0) Then 'customBin(nBins - 1, i, 0) Then
                    bin_maxx = spec_ellipse(i, 0)      'customBin(nBins - 1, i, 0)
                End If

                If bin_minx > spec_ellipse(i, 0) Then
                    bin_minx = spec_ellipse(i, 0)
                End If

                If bin_maxy < spec_ellipse(i, 1) Then
                    bin_maxy = spec_ellipse(i, 1)
                End If

                If bin_miny > spec_ellipse(i, 1) Then
                    bin_miny = spec_ellipse(i, 1)
                End If
            Next
        Else
            For i = 0 To 128
                If bin_maxx < philipsC_Ellipse(i, 0) Then               'customBin(nBins - 1, i, 0) Then
                    bin_maxx = philipsC_Ellipse(i, 0)                   'customBin(nBins - 1, i, 0)
                End If

                If bin_minx > philipsA_ellipse(i, 0) Then
                    bin_minx = philipsA_ellipse(i, 0)
                End If

                If bin_maxy < philipsC_Ellipse(i, 1) Then
                    bin_maxy = philipsC_Ellipse(i, 1)
                End If

                If bin_miny > philipsA_ellipse(i, 1) Then
                    bin_miny = philipsA_ellipse(i, 1)
                End If
            Next

        End If
    End Sub

    Public Sub Zoom(ByVal cht As DataVisualization.Charting.Chart)

        With cht.ChartAreas(0)
            .AxisX.Maximum = bin_maxx + 0.005   '.48
            .AxisX.Minimum = bin_minx - 0.005   '.43
            .AxisY.Maximum = bin_maxy + 0.005   '.44
            .AxisY.Minimum = bin_miny - 0.005   '.38
            .AxisX.Interval = (.AxisX.Maximum - .AxisX.Minimum) / 5
            .AxisY.Interval = (.AxisY.Maximum - .AxisY.Minimum) / 5
        End With

    End Sub

    Public Function GetResults(ByVal num As Integer) As Integer
        '********************************************************
        '   Procedure:  Get Results
        '   Purpose:    Check whether the cie is inside the bin
        '   Inputs:     None
        '   Outputs:    None
        '   Author:     HP
        '   Rev Hist:   10/26/11, Original
        '
        '********************************************************
        Dim iLoc As Integer = 0
        Dim iRes As Integer = 0
        Dim dTempx, dTempy, dTempR, tanTheta, tspec, rspec As Double
        Dim rMin, rMax, tanT As Double
        Dim sdcm As Double


        If num = 1 And iS_Philips > 0 Then          'check low bin
            rMax = abtheta_PA(0)
            rMin = abtheta_PA(1)
            tanT = abtheta_PA(2)
            dTempx = fCIEx - targ_xPA
            dTempy = fCIEy - targ_yPA
        ElseIf num = 2 And iS_Philips > 0 Then      'check high bin
            rMax = abtheta_PC(0)
            rMin = abtheta_PC(1)
            tanT = abtheta_PC(2)
            dTempx = fCIEx - targ_xPC
            dTempy = fCIEy - targ_yPC
        Else                                        'check std bin
            rMax = abtheta_std(0)
            rMin = abtheta_std(1)
            tanT = abtheta_std(2)
            dTempx = fCIEx - targ_x_std
            dTempx = fCIEy - targ_y_std
        End If

        dTempR = Math.Sqrt((dTempx ^ 2) + (dTempy ^ 2)) 'radial distance from center
        If dTempx = 0 Then                              'avoid div by zero
            tanTheta = dTempy / 0.000001
        Else
            tanTheta = dTempy / dTempx
        End If
        tspec = (rMax / rMin) * (tanTheta - tanT) / (1 + tanT * tanTheta)       'taken from BC template
        rspec = Math.Sqrt((rMax ^ 2 + (rMin * tspec) ^ 2) / (1 + tspec ^ 2))    'radial spec

        sdcm = Math.Abs((dTempR / rspec))
        If sdcm < bin_spec Then
            Return 1
        Else
            Return 0
        End If

    End Function

    Public Function InitSpectrometer() As Boolean
        '********************************************************
        '   Procedure:  InitSpectrometer
        '   Purpose:    To set up communication with the spectrometer
        '   Inputs:     None
        '   Outputs:    None
        '   Author:     HP
        '   Rev Hist:   4/30/08, Original
        '  
        '********************************************************
        Dim bstatus As Boolean
        Dim specidx As Integer

        wrapper = New OmniDriver.CCoWrapper       'create spectrometer object
        wrapper.CreateWrapper()                  'create wrapper
        specidx = wrapper.openAllSpectrometers - 1  'ping spectrometers and assign index
        If specidx < 0 Then                         'if none found, prompt and disable frame
            MsgBox("Spectrometer Communication Failure, check connection")
            bstatus = False
        Else
            bstatus = True
            frmMain.lbSpecVersion.Text = "Wrapper API version: " & wrapper.getApiVersion() & "  build number: " & wrapper.getBuildNumber()
            wavelengthArray = wrapper.getWavelengths(0)
            iNumPix = wrapper.getNumberOfPixels(0)

            Call LoadCalFile()
        End If
        Return bstatus

    End Function

    Public Sub DisconnectSpectrometer()
        '**********************************************************
        '   Procedure:  DisconnectSpectrometer
        '   Purpose:    close spectrometers using the wrapper
        '   Inputs:     None
        '   Outputs:    None
        '   Author:     HP
        '   Rev Hist:   10/13/09, Original
        '
        '**********************************************************
        Try
            wrapper.closeAllSpectrometers()
        Catch
        End Try
    End Sub

    Public Sub TakeDark(ByVal num As Integer)
        'take dark scan and save to array based on input (ref or test dark array)
        Dim dBaselineTotal As Double = 0
        Dim dBaseSqr As Double = 0

        If num = 0 Then
            BlueDarkPixels = wrapper.getSpectrum(0)
            For i = 1 To iNumPix - 1                        'go through pixel array to get average
                dBaselineTotal += BlueDarkPixels(i)
            Next
            dBlueDarkAvg = dBaselineTotal / (iNumPix - 1)
            For i = 1 To iNumPix - 1
                dBaseSqr += (BlueDarkPixels(i) - dBlueDarkAvg) ^ 2
            Next
            dblueDarkSTD = Math.Sqrt(dBaseSqr / (iNumPix - 2))
            dBlueIntTime = frmMain.neInt_B.Value * 0.001
        Else
            WhiteDarkPixels = wrapper.getSpectrum(0)
            For i = 1 To iNumPix - 1                        'go through pixel array to get average
                dBaselineTotal += WhiteDarkPixels(i)
            Next
            dWhiteDarkAvg = dBaselineTotal / (iNumPix - 1)
            For i = 1 To iNumPix - 1
                dBaseSqr += (WhiteDarkPixels(i) - dWhiteDarkAvg) ^ 2
            Next
            dWhiteDarkSTD = Math.Sqrt(dBaseSqr / (iNumPix - 2))
            dRefIntTime = frmMain.neInt_W.Value * 0.001
        End If

        Application.DoEvents()

    End Sub

    Public Sub TakeSample()
        '**********************************************************
        '   Procedure:  Take Sample
        '   Purpose:    receive the spectrum, in pulse mode we need to create the pulse and measure when spectrum is taken
        '   Inputs:     num, which dark scan to use (blu or test)
        '   Outputs:    resultpack to include cie x,y lumens watts dom wl CE Ra R9 
        '   Author:     HP
        '   Rev Hist:   10/26/11, Original
        '               5/3/13, Updated for pulse mode
        '
        '**********************************************************

        Dim iTime As Double
        Dim ReadV(1) As Double
        Dim ReadI(1) As Double

        iTime = (iIntegrationTime / 1000) * frmMain.neScanAvg.Value                     'put integration time into ms
        samplePixels = wrapper.getSpectrum(0)                       'spectrum is taken and filled into samplepixels
        Application.DoEvents()                          'make sure the GUI is updated before moving on.

        dMaxPixel = 0
        For i = 1 To iNumPix - 1                        'go through both arrays to find max pixel (intensity) and at 
            If samplePixels(i) > dMaxPixel And wavelengthArray(i) < 1000 Then     'what wL that occurred at
                dMaxPixel = samplePixels(i)
                dMaxWL = wavelengthArray(i)
            End If
        Next

    End Sub

    Public Sub Calculate(ByVal num As Integer)
        '**********************************************************
        '   Procedure:  Calculate
        '   Purpose:    Open and use SPAM objects to get parameters of the scan
        '   Inputs:     num, which dark scan to use (blu or test)
        '   Outputs:    resultpack to include cie x,y lumens watts dom wl CE Ra R9 
        '   Author:     HP
        '   Rev Hist:   10/26/11, Original
        '
        '**********************************************************
        Dim advancedColor As New SPAM.CCoAdvancedColor
        Dim cieColor As New SPAM.CCoCIEColor
        Dim cieConstants As New SPAM.CCoCIEConstants
        Dim cieObserver As SPAM.CCoCIEObserver
        Dim coreEmission As New SPAM.CCoAdvancedAbsoluteIrradiance
        Dim illuminant As SPAM.CCoIlluminant
        Dim xyzColor As New SPAM.CCoxyz_Color
        Dim CRI As New SPAM.CCoColorRenderingIndex
        Dim DomPure As New SPAM.CCoDominantWavelengthPurity
        Dim numMethod As New SPAM.CCoNumericalMethods
        Dim photoFlux As New SPAM.CCoAdvancedPhotometrics
        Dim bIS As Boolean = False
        Dim deltaWL As Double
        Dim photonPerNm As Double
        Dim wlBegin, wlEnd As Double

        photons = 0
        cieConstants.CreateCIEConstants()               'create instances of the objects to be used
        advancedColor.CreateAdvancedColor()
        photoFlux.CreateAdvancedPhotometrics()
        numMethod.CreateNumericalMethods()
        coreEmission.CreateAdvancedAbsoluteIrradiance()

        If samplePixels Is Nothing Then                 'exit and prompt if no scan has been taken
            MsgBox("Please take a sample before attempting to calculate colorspace values")
            Return
        End If

        maxEnergy = 0
        If num = 0 Then                                 'irradiance array using the selected dark scan
            energyArray = coreEmission.processSpectrum(BlueDarkPixels, samplePixels, wavelengthArray, calibrationArray, frmMain.neInt_B.Value * 0.001, dArea, bIS)
            wlBegin = blue_startWL
            wlEnd = blue_stopwl
        ElseIf num = 1 Then
            energyArray = coreEmission.processSpectrum(WhiteDarkPixels, samplePixels, wavelengthArray, calibrationArray, frmMain.neInt_W.Value * 0.001, dArea, bIS)
            wlBegin = pwr_startWL
            wlEnd = pwr_stopwl
        End If

        Dim negcnt As Integer = 0

        For i = 0 To iNumPix - 1                    'ZERO out negative
            If energyArray(i) < 0 Then
                energyArray(i) = 0
                negcnt += 1
            End If
        Next

        For i = 0 To iNumPix - 1                        'get energy array in watts 
            energyArrayW(i) = energyArray(i) * 0.000001 * dArea
            If energyArray(i) > maxEnergy Then
                maxEnergy = energyArray(i)
            End If
            If wavelengthArray(i) > (wlBegin - 1) And wavelengthArray(i) < (wlEnd + 1) Then
                deltaWL = (wavelengthArray(i) - wavelengthArray(i - 2)) / 2
                photonPerNm = energyArrayW(i) * wavelengthArray(i) * (10 ^ -9) / hc
                photons += deltaWL * photonPerNm
            End If
        Next

        'Call LoadSpec()
        'Call LoadWavelength()

        cieObserver = cieConstants.getCIEObserverByIndex(0)
        illuminant = cieConstants.getIlluminantByIndex(7)
        cieColor = advancedColor.computeEmissiveChromaticity(wavelengthArray, energyArray, cieObserver, illuminant)     'compute color object

        xyzColor.Createxyz_Color(cieColor)         'create xyz from color object
        fCIEx = FormatNumber(xyzColor.get_x + dFactors(1) + frmMain.neOffx.Value, 4)     'get x and y
        fCIEy = FormatNumber(xyzColor.get_y + dFactors(2) + frmMain.neOffy.Value, 4)

        CRI.CreateColorRenderingIndex(cieColor)    'create CRI from color object
        fRa = FormatNumber(CRI.getGeneralCRI + dFactors(3) + frmMain.neOffCRI.Value, 1)    'get Ra,
        fR9 = FormatNumber(CRI.getSpecialCRI(9), 1)
        fCCT = FormatNumber(CRI.getReferenceColorTemperature, 0)

        'Dim swriter As StreamWriter
        'swriter = New StreamWriter(My.Application.Info.DirectoryPath & "\ExportData\" & fRa & "_" & "CRI" & ".xls")
        'swriter.AutoFlush = True

        'swriter.WriteLine("CRI;" & fRa)
        'swriter.WriteLine("R9;" & fR9)
        'swriter.WriteLine("CCT;" & fCCT)
        'swriter.Close()
        'default bw 380-780
        DarkAvg = FormatNumber(numMethod.integrate(wavelengthArray, samplePixels, pwr_startWL, pwr_stopwl, 1), 0)
        flux = FormatNumber(numMethod.integrate(wavelengthArray, energyArrayW, wlBegin, wlEnd, 1), 4)     'compute watts by integrating energy array from wl 380, 780
        If (wlBegin = 380 And wlEnd = 780) Or (wlBegin < 380 And wlEnd > 780) Then  'use default if at range or over range
            lumens = photoFlux.computeLuminousFluxLumen(wavelengthArray, energyArrayW, cieObserver.getWavelengths, cieObserver.getV, cieObserver.getKm)    'compute lumens
        Else
            aModV = cieObserver.getV
            Call ModifyV(cieObserver.getWavelengths, cieObserver.getV, wlBegin, wlEnd)
            lumens = photoFlux.computeLuminousFluxLumen(wavelengthArray, energyArrayW, cieObserver.getWavelengths, aModV, cieObserver.getKm)    'compute lumens
        End If

        DomPure.CreateDominantWavelengthPurity(cieColor)       'create dompure from color object
        fDWL = FormatNumber(DomPure.getDominantWavelength, 1)   'get dom wl

        FindBlueData()                                  'get blue watt for CE

        coreEmission.Dispose()
        cieConstants.Dispose()
        advancedColor.Dispose()
        xyzColor.Dispose()
        photoFlux.Dispose()
        numMethod.Dispose()
        CRI.Dispose()
        DomPure.Dispose()
        cieColor.Dispose()

        aResultPack(1) = FormatNumber(lumens, 1)                                 'fill in aresultpack array
        aResultPack(2) = flux
        If blue_watt <> 0 And fCCT <> 0 Then                            'if blue watt is found and current scan is white, calculate CE
            aResultPack(3) = FormatNumber(((lumens / blue_watt) * dFactors(0)) + frmMain.neoffCE.Value, 2)
        Else
            aResultPack(3) = 0
        End If
        aResultPack(4) = fCIEx
        aResultPack(5) = fCIEy
        aResultPack(7) = fCCT
        aResultPack(8) = fRa
        aResultPack(9) = fR9                        '10 is the result 0-7
        aResultPack(11) = fDWL

    End Sub

    Public Sub ModifyV(ByVal wls() As Double, ByVal vs() As Double, ByVal wl_First As Integer, ByVal wl_last As Integer)
        Dim tFind1, tFind2 As Integer

        For i = 0 To 80
            If wl_First <= wls(i) Then
                tFind1 = i - 1
                Exit For
            End If
        Next

        For i = 80 To 0 Step -1
            If wl_last >= wls(i) Then
                tFind2 = i + 1
                Exit For
            End If
        Next

        For i = 0 To tFind1
            aModV(i) = 0
        Next

        For i = tFind2 To 80
            aModV(i) = 0
        Next

    End Sub

    Public Sub FindBlueData()
        'Get the last blue scan, notified by zero CCT and return wattage.  If not found, return zero
        blue_watt = 0
        blue_photons = 0
        For i = frmMain.dgResults.RowCount - 1 To 0 Step -1     'go through table
            If frmMain.dgResults.Rows.Item(i).Cells(14).Value < baselineThresh Then      'skip the dark rows
                Application.DoEvents()
            ElseIf frmMain.dgResults.Rows.Item(i).Cells(4).Value < 0.17 Then    'indication of being blue
                blu_x = frmMain.dgResults.Rows.Item(i).Cells(4).Value
                blu_y = frmMain.dgResults.Rows.Item(i).Cells(5).Value
                blue_watt = frmMain.dgResults.Rows.Item(i).Cells(2).Value
                Exit Sub
            End If
        Next

    End Sub

    Public Sub LoadCalFile()
        'Read the Calibration file created by SpectraSuite.  calxxxx_ooiirad file using std lamp with recommended int time.
        Dim sReader As StreamReader
        Dim stemp As String
        Dim i As Integer
        Dim sIntTime As String
        Dim FiberDia As Double

        ReDim WhiteDarkPixels(iNumPix - 1)                               'redim the arrays so that they can be read into
        ReDim BlueDarkPixels(iNumPix - 1)
        ReDim energyArray(iNumPix - 1)
        ReDim calibrationArray(iNumPix - 1)
        ReDim energyArrayW(iNumPix - 1)
        ReDim ScanData(iNumPix - 1, 250)
        For i = 0 To iNumPix - 1                        'place wavelength data in zero spot
            ScanData(i, 0) = wavelengthArray(i)
        Next

        sReader = New StreamReader(Application.StartupPath & "\MiniIS_Cal.cal")     'rename cal file to MiniIS_Cal.cal
        stemp = sReader.ReadLine        'burn date line
        stemp = sReader.ReadLine        'burn spec id line
        stemp = sReader.ReadLine        'burn lamp desc
        stemp = sReader.ReadLine        'integration line
        sIntTime = Val(stemp.Substring(stemp.IndexOf(")") + 1))

        stemp = sReader.ReadLine        'avg line
        stemp = sReader.ReadLine        'boxcar line
        stemp = sReader.ReadLine        'fiber line
        FiberDia = Val(stemp.Substring(stemp.IndexOf(")") + 1))
        If FiberDia = 11284 Then
            bIS = True
        Else
            bIS = False
        End If
        dArea = Math.PI * (((FiberDia / 2) * 0.0001) ^ 2)
        stemp = sReader.ReadLine        'blank line
        stemp = sReader.ReadLine        'uJ/cnt line

        For i = 0 To iNumPix - 1
            calibrationArray(i) = sReader.ReadLine              'read into calibration array and set darks to zero at start up
            BlueDarkPixels(i) = 0
            WhiteDarkPixels(i) = 0
        Next
        sReader.Close()
    End Sub
    ' Public Sub LoadSpec()
    ' Dim sReader As StreamReader
    'Dim i As Integer

    'ReDim energyArray(iNumPix - 1)
    '   sReader = New StreamReader(Application.StartupPath & "\testSpectrum.cal")

    'For i = 0 To iNumPix - 1
    '       energyArray(i) = sReader.ReadLine
    'Next
    '   sReader.Close()
    'End Sub
    'Public Sub LoadWavelength()
    'Dim sReader As StreamReader
    'Dim i As Integer

    'ReDim wavelengthArray(iNumPix - 1)
    '   sReader = New StreamReader(Application.StartupPath & "\wavelength.cal")

    'For i = 0 To iNumPix - 1
    '       wavelengthArray(i) = sReader.ReadLine
    'Next
    '   sReader.Close()
    'End Sub

    Public Sub ResetCorrs()
        If iS_Philips Then
            dFactors(0) = 1
            dFactors(1) = 0
            dFactors(2) = 0
            dFactors(3) = 0
            frmMain.neOffCRI.Value = 0
            frmMain.neOffx.Value = 0
            frmMain.neOffy.Value = 0
            frmMain.neoffCE.Value = 0
            Application.DoEvents()

        End If

    End Sub

End Module

