Imports System.IO

Public Class frmMain
    '****************************************************************
    '   Program:    MiniPhillips
    '   Purpose:    Provide an easy user interface to the spectrometer compared to Spectra Suite.  Display results in table form similar to Half Moon and 
    '               Everfine Application.  Save Data to Excel function.  Display and Bin according to part selection.  Present Pass/Fail Results based on 
    '               OsRam Ellipses and acceptance criteria < 4 macadam 5000k and up, < 3 macadam for other families.
    '   Inputs:     Cal File to get calibration array.  This file is made from Spectra-Suite to perform s/w comparison.  Bin Points for plotting, seperated ellipse 
    '               files for each family. Blk Body file for plotting with center points and calculation factors for each family.
    '   Outputs:    Excel File with raw data, which is the datatable moved into excel and saved to lot number file.  Also capability of writing the scan to file (irrad, spectrum),
    '               displaying stored dark.  Supv mode to update acceptance criteria and add delay with Ctrl d.
    '   Author:     Herman Pahulu
    '   Rev. Hist:  Rev         
    '               1.0.0.0     8/29/14     Original taken from Minisphere Version 1.7.0.0
    '               1.1.0.0     10/30/14    Trim fat....test load
    '               1.2.0.0     10/31/14    Enable working samples
    '               1.3.0.0     11/3/14     Enable Air Support
    '               1.3.1.0     11/3/14     place blue min for max cnts, tolerances for working samples acceptance in system file, fix file runtime error,
    '                                       highlight selected row.
    '               1.4.0.0     2/4/15      Add inline samples for appending offline data to correction file.
    '               1.5.0.0     5/28/15     Add intrinsic offsets and barcode tracking
    '               1.5.1.0     6/10/15     Add CE and CRI intrinsic offsets and have the intrinsic offsets displayed in the notes column
    '               1.5.2.0     8/21/15     Add relay port as an option in system file, fix CE intrinsic offset edit box
    '
    '****************************************************************

    Dim frmLoad As Boolean
    Dim AcqType As Integer
    Dim iSaveBlueInt, iSaveTestInt As Integer
    Dim iPSCom As Integer
    Dim bShowDark As Boolean
    Dim sMsg As String
    Dim pCorrAvg(3) As Double
    Dim gCorrAvg(3) As Double
    Dim dgUpdate As Boolean
    Dim blueData(100, 2) As Double
    Dim whiteData(2000, 8) As Double
    Dim LotData(2000, 1) As Double
    Dim blueIdx(200) As Integer
    Dim idxB As Integer
    Dim iBlue, iWhite, maxBlue, maxWhite, maxB_Int, maxW_Int As Integer
    Dim wPass As Integer
    Dim avgWatts As Double
    Dim binIdx As Integer
    Dim ExportRows As Integer = 0
    Dim PwrSplyType, iOutput As Integer
    Dim usb2k As Integer
    Dim iMaxCntThresh As Integer = 59000
    Dim bDarkUpdate As Boolean
    Dim LISData(1, 3) As Double
    Dim SISData(1, 3) As Double
    Dim InLineOffsets(4) As Double
    Dim InLineData(8, 4) As Double
    Dim OffLineData(8, 4) As Double
    Dim calStr As String
    Dim iCalCk As Integer
    Dim iCalSamples As Integer

    Dim gFixtureID As String
    Dim wsID(2) As String
    Dim CalSampleName(8) As String
    Dim Tblblock(200, 25) As String
    Dim specUpdate As Date

    Dim Bin_Clr As Color
    Dim stage_com As Integer
    Dim calRange As Integer
    Dim LE_ID As String
    Dim FinalID As String
    Dim wkRefName(2) As String
    Dim wkRefData() As Double
    Dim inLineFN As String
    Dim sbarcode As String = ""
    Dim pnlLoc As Integer
    Dim scanBox As Integer

    Public Sub DisplaySpectrum()
        '***********************************************************
        '   Procedure:  DisplaySpectrum
        '   Purpose:    Get Spectrum, plot and display peaks, bw, cwl
        '   Inputs:     None
        '   Outputs:    None
        '   Author:     HP
        '   Rev His:    7/26/11,   Original
        '                     
        '
        '***********************************************************
        Dim iP As Integer

        If frmLoad Then

            chtPlot.Series(0).Points.Clear()
            ChtPlot.ChartAreas(0).AxisY.Maximum = 65000
            ChtPlot.ChartAreas(0).AxisY.Interval = 5000

            Call TakeSample()
            For iP = 1 To iNumPix - 1
                ChtPlot.Series(0).Points.AddXY(wavelengthArray(iP), samplePixels(iP))
            Next

            txtPeak1.Text = FormatNumber(dMaxPixel, 0)
            If dMaxPixel > iMaxCntThresh Then
                txtPeak1.BackColor = Color.Red
            Else
                txtPeak1.BackColor = Color.WhiteSmoke
            End If

        End If

    End Sub

    Public Sub DisplayIrradiance()
        '***********************************************************
        '   Procedure:  DisplayIrradiance
        '   Purpose:    Take Sample, calculate for color, plot and display results
        '   Inputs:     None
        '   Outputs:    None
        '   Author:     HP
        '   Rev His:    7/26/11,   Original
        '                     
        '
        '***********************************************************\
        Dim iP As Integer

        If DateDiff(DateInterval.Minute, darkTime, Now) >= iDarkExpire Then
            AcqSuccess = False
            MsgBox("Update Dark Required!")
            Exit Sub
        Else
            lblSDark.Text = "Dark Expires: " & getExpiration()
        End If

        chtPlot.Series(0).Points.Clear()
        Call TakeSample()
        Call Calculate(AcqType)

        chtPlot.ChartAreas(0).AxisY.Maximum = 1.1 * maxEnergy                   'assign maximum for y axis
        If maxEnergy <> 0 Then                                                  'only change interval if maxenergy is not zero
            chtPlot.ChartAreas(0).AxisY.Interval = CInt((1.1 * maxEnergy) / 6)
        End If

        For iP = 1 To iNumPix - 1
            chtPlot.Series(0).Points.AddXY(wavelengthArray(iP), energyArray(iP))    'plot the irradiance
        Next

        Call UpdateGrid(2)                                      'update the table grid

        txtPeak1.Text = FormatNumber(dMaxPixel, 0)              'display peak wl and counts along saturation notification
        If dMaxPixel > iMaxCntThresh Then
            txtPeak1.BackColor = Color.Red
        Else
            txtPeak1.BackColor = Color.WhiteSmoke
        End If

    End Sub

    Public Sub DisplayBin()
        '***********************************************************
        '   Procedure:  DisplayBin
        '   Purpose:    Take Sample, calculate for color, plot and display results
        '   Inputs:     None
        '   Outputs:    None
        '   Author:     HP
        '   Rev His:    7/26/11,   Original
        '                     
        '
        '***********************************************************\
        Dim bScan As Boolean = False
        Dim iRes As Integer
        Dim bitRes As Integer = 0
        Dim bitTot As Integer = 0

        If DateDiff(DateInterval.Minute, darkTime, Now) >= iDarkExpire Then         'check dark expiration
            AcqSuccess = False
            MsgBox("Update Dark Required!")
            Exit Sub
        Else
            lblSDark.Text = "Dark Expires: " & getExpiration()
        End If
        Call TakeSample()                                               'take spectrum
        Call Calculate(AcqType)                                         'get parameters

        If fCCT = 0 Or dMaxPixel > iMaxCntThresh Then                   'if no CCT or sat at test scan, it's most likely a blue scan
            bScan = True
        End If

        ChtPlot.Series("Data").Points.AddXY(fCIEx, fCIEy)       'plot data cie x, y 
        ChtPlot.Series("Current").Points.Clear()                'clear current series points and add the current selected
        ChtPlot.Series("Current").Points.AddXY(fCIEx, fCIEy)

        If aResultPack(3) < ce_specH And aResultPack(3) > ce_specL Then     'check CE
            ovalCE.FillColor = Color.Green
            bitRes += 1
            bitTot += 1
        Else
            ovalCE.FillColor = Color.Red
            bitTot += 1
        End If

        If fRa >= id_cri Then                                   'check cri
            ovalRa.FillColor = Color.Green
            bitRes += 2
            bitTot += 2
        Else
            ovalRa.FillColor = Color.Red
            bitTot += 2
        End If

        iRes = GetResults(1)           'get std results             'check color

        If iS_Philips Then
            iRes += (GetResults(2) * 2)
            Select Case iRes
                Case 0
                    binID = "F"                        'assign BinID per result
                    Bin_Clr = Color.Red
                Case 1
                    binID = "L"
                    Bin_Clr = Color.Yellow
                Case 2
                    binID = "H"
                    Bin_Clr = Color.Blue
                Case 3
                    binID = "M"
                    Bin_Clr = Color.Green
            End Select
        End If

        If iRes > 0 Then                        'this is for color
            ovalBin.FillColor = Color.Green
            bitRes += 4
            bitTot += 4
        Else
            ovalBin.FillColor = Color.Red
            bitTot += 4
        End If

        aResultPack(10) = bitRes                  'quantify total result 0-7
        If bitRes <> 7 Then
            FinalID = "F"                        'assign BinID per result
            Bin_Clr = Color.Red
        Else
            FinalID = binID
        End If
        Application.DoEvents()

        Call UpdateGrid(bitRes)                                  'update the table grid

        txtPeak1.Text = FormatNumber(dMaxPixel, 0)              'display peak wl and counts along saturation notification
        If dMaxPixel > iMaxCntThresh Then
            txtPeak1.BackColor = Color.Red
        Else
            txtPeak1.BackColor = Color.WhiteSmoke
        End If

    End Sub

    Public Sub UpdateGrid(ByVal num As Integer)
        '***********************************************************
        '   Procedure:  UpdateGrid
        '   Purpose:    Update Data Grid with results and highlight current measurement
        '   Inputs:     None
        '   Outputs:    None
        '   Author:     HP
        '   Rev His:    10/26/11,   Original
        '                     
        '***********************************************************\
        Dim bScan As Boolean = False
        Dim sIntrinsic As String = ""

        If fCCT = 0 Or dMaxPixel > 65500 Then                   'if no CCT or sat at test scan, it's most likely a blue scan
            bScan = True
        End If

        dgUpdate = True
        dgResults.Rows.Add()                                                                'add row
        For i = 1 To 11
            dgResults.Rows.Item(dgResults.RowCount - 1).Cells(i).Value = aResultPack(i)     'place results in columns
        Next
        If SID.Contains(id_wo) Then
            If pnlLoc > 9 Then
                SID = ""
                dgResults.Rows.Item(dgResults.RowCount - 1).Cells(0).Value = dgResults.RowCount
                pnlLoc = 1
            Else
                dgResults.Rows.Item(dgResults.RowCount - 1).Cells(0).Value = pnlID & "-" & pnlLoc
                pnlLoc += 1
            End If
        ElseIf SID = "" Then
            dgResults.Rows.Item(dgResults.RowCount - 1).Cells(0).Value = dgResults.RowCount     'sample no is row count
        Else
            dgResults.Rows.Item(dgResults.RowCount - 1).Cells(0).Value = SID     'sample no is row count
        End If
        If iS_Philips And Not bScan Then
            dgResults.Rows.Item(dgResults.RowCount - 1).Cells(6).Value = binID             'binID for Phillips
            txtBIN.Text = FinalID
            txtBIN.BackColor = Bin_Clr
        Else
            dgResults.Rows.Item(dgResults.RowCount - 1).Cells(6).Value = "Blue"             'binID for Phillips
            txtBIN.Text = ""
            txtBIN.BackColor = Color.White
        End If
        dgResults.Rows.Item(dgResults.RowCount - 1).Cells(12).Value = Now.ToShortDateString & " " & Now.Hour & ":" & Now.Minute & ":" & Now.Second    'timestamp
        dgResults.Rows.Item(dgResults.RowCount - 1).Cells(13).Value = FormatNumber(wrapper.getIntegrationTime(0) * 0.001, 2)   'integration time
        dgResults.Rows.Item(dgResults.RowCount - 1).Cells(14).Value = FormatNumber(dMaxPixel, 0)             'peak intensity

        If ckSaveSpectrum.Checked And iSaveScanNo < 250 Then                                'if save requested
            If iSaveScanNo = -1 Then
                For i = 0 To iNumPix - 1
                    ScanData(i, 0) = wavelengthArray(i)
                Next
                iSaveScanNo += 1
            End If
            iSaveScanNo += 1
            For i = 0 To iNumPix - 1                                                            'save the scan data
                ScanData(i, iSaveScanNo) = energyArray(i)                                       'save in uW
            Next
            dgResults.Rows.Item(dgResults.RowCount - 1).Cells(dgResults.ColumnCount - 1).Value = iSaveScanNo
        Else
            dgResults.Rows.Item(dgResults.RowCount - 1).Cells(dgResults.ColumnCount - 1).Value = -1
        End If

        For i = 0 To 3
            dgResults.Rows.Item(dgResults.RowCount - 1).Cells(15 + i).Value = dFactors(i)
        Next

        dgResults.Rows.Item(dgResults.RowCount - 1).Cells(19).Value = 0 'neoffCE.Value & ":" & neOffx.Value & ":" & neOffy.Value & ":" & neOffCRI.Value

        Application.DoEvents()

        For i = 0 To dgResults.RowCount - 1                                                 'unselect all rows
            dgResults.Rows.Item(i).Selected = False
        Next
        dgResults.Rows.Item(dgResults.RowCount - 1).Selected = True                         'select the row just added
        dgResults.CurrentCell = dgResults(0, dgResults.RowCount - 1)

        dgResults.FirstDisplayedScrollingRowIndex = dgResults.RowCount - 1                  'scroll to last row
        dgUpdate = False

    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        wrapper.closeSpectrometer(0)
        If airConnect Then
            ncd1.ProXR.RelayBanks.TurnOffRelay(rlyport)
        End If

    End Sub

    Private Sub frmMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'F3 shortcut for updating the plot (acquiring data)

        If e.KeyValue = 114 Then            'F3
            If cmdCalculate.Enabled = False Then
                Exit Sub
            End If
            If airConnect Then
                ncd1.ProXR.RelayBanks.TurnOnRelay(rlyport)
                System.Threading.Thread.Sleep(1500)
            End If

            Call DisplayBin()
            If airConnect Then
                ncd1.ProXR.RelayBanks.TurnOffRelay(rlyport)
            End If
        ElseIf e.KeyValue = 112 And airConnect Then         'F1 for up
            ncd1.ProXR.RelayBanks.TurnOnRelay(rlyport)
        ElseIf e.KeyValue = 113 And airConnect Then         'F2 for down
            ncd1.ProXR.RelayBanks.TurnOffRelay(rlyport)
        End If


    End Sub

    Private Sub frmMain_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress

        If e.KeyChar = "W" Then                 'This is barcode scan
            sbarcode = e.KeyChar
        ElseIf sbarcode.Length < 11 And sbarcode <> "" Then
            sbarcode += e.KeyChar
        ElseIf sbarcode.Length = 11 And sbarcode <> "" Then
            sbarcode += e.KeyChar
            id_wo = sbarcode.Substring(1, 3)                'old barcode with 12 characters
            pnlID = id_wo & "-" & sbarcode.Substring(sbarcode.Length - 3)
            SID = pnlID
            pnlLoc = 1
            If txtLot.Text <> "W" & id_wo Then             'at first scan, assign work order label
                txtLot.Text = "W" & id_wo                                                                  'update parameters base on pn
            End If
            sbarcode = ""

        End If

        If Asc(e.KeyChar) = 4 Then          'ctrl d to enable offset editing
            gbIOff.Enabled = True
        ElseIf Asc(e.KeyChar) = 6 Then      'ctrl f to disable offset editing
            gbIOff.Enabled = False
        End If

    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        Call Init()
    End Sub

    Public Sub Init()
        '***********************************************************
        '   Procedure:  Init
        '   Purpose:    Setup routine before showing form
        '   Inputs:     None
        '   Outputs:    None
        '   Author:     HP
        '   Rev His:    10/26/11,   Original
        '                     
        '
        '***********************************************************\

        mnuILSamples.Enabled = False
        specUpdate = Now

        Call LoadSystemInfo()
        If InitSpectrometer() = False Then
            MsgBox("Spectrometer Not Found")
            Exit Sub
        End If

        If airconnect Then
            Call ConnectStage()
        End If

        Me.KeyPreview = True
        opRef.Checked = True                    'blue scan is default
        frmLoad = True

        Call LoadBinChart()                     'this will select 1st CCT which is 2700

        neInt_B.Maximum = 10000
        neInt_W.Maximum = 10000
        neInt_B.Minimum = 4
        neInt_W.Minimum = 4
        neoffCE.Value = intrinsicCE
        neOffx.Value = intrinsicX
        neOffy.Value = intrinsicY
        neOffCRI.Value = intrinsicCRI

        neInt_B.Value = iSaveBlueInt
        neInt_B.BackColor = Color.Yellow
        neInt_W.Value = iSaveTestInt
        lbSystem.Text = IS_Num
        txtCal_B_Dark.Enabled = False
        txtCal_W_Dark.Enabled = False
        txtPeak1.Enabled = False
        tmrScanReady.Enabled = False
        tmrScanReady.Interval = ScanReadyTime
        txtLot.Text = 1000 'default lot No.
        neScanAvg.Value = 3
        txtPeak1.Text = ""
        gbIOff.Enabled = False
        Application.DoEvents()

        wrapper.setBoxcarWidth(0, scanBox)
        ResetCorrs()                            'set corrections to default
        cmdTakeDark.Focus()
        SaveTime = Now

    End Sub

    Public Sub ConnectStage()
        ncd1.PortName = "Com" & Stage_Com
        ncd1.OpenPort()
        ' open the port
        If (Not ncd1.IsOpen) Then
            MessageBox.Show("Fail to Open Port for USB Relay!")
            'Application.Exit()
        End If
        ncd1.ProXR.RelayBanks.SelectBank(1)
        ncd1.ProXR.RelayBanks.TurnOffRelay(rlyport)
    End Sub

    Private Sub cmdCalculate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCalculate.Click
        Dim isec As Integer

        isec = DateDiff(DateInterval.Second, specUpdate, Now)           'check delta from the last spectrometer update
        If isec < 10 Then
            System.Threading.Thread.Sleep((10 - isec) * 1000)
        End If
        If airConnect Then
            ncd1.ProXR.RelayBanks.TurnOnRelay(rlyport)
            System.Threading.Thread.Sleep(1500)
        End If

        Call DisplayBin()

        If airConnect Then
            ncd1.ProXR.RelayBanks.TurnOffRelay(rlyport)
        End If

    End Sub

    Private Sub cmdTakeDark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTakeDark.Click

        Call SetupDark(0)
    End Sub

    Public Sub SetupDark(ByVal num As Integer)
        Dim msg As Integer = vbOK
        Dim bSwitch As Boolean = False

        'Take dark scan and assign saved blu or test integration time based on selection
        If num = 0 Then
            msg = MsgBox("Verify that the supply is off or port is covered with a blank.", MsgBoxStyle.OkCancel, "Update Dark Current")    'verification prompt
        End If

        If msg = vbOK Then

            Application.UseWaitCursor = True
            Application.DoEvents()

            If airConnect And Not bAutoSession Then
                ncd1.ProXR.RelayBanks.TurnOnRelay(rlyport)
                System.Threading.Thread.Sleep(1500)
            End If

            bDarkUpdate = True
            cmdTakeDark.Enabled = False
            cmdCalculate.Enabled = False
            If opTest.Checked Then
                opRef.Checked = True                        'this will change to blue int time and settle
            Else
                bSwitch = True
                System.Threading.Thread.Sleep(int_wait)     'wait before taking blue dark
            End If

            System.Threading.Thread.Sleep(int_wait)
            Call UpdateDark()                               'update blue dark
            opTest.Checked = True                           'this will change to white int time and settle

            System.Threading.Thread.Sleep(int_wait)
            Call UpdateDark()                               'update white dark
            If bSwitch Then
                opRef.Checked = True
                Application.DoEvents()

            End If

            Call AddDarkRow()
            If num = 0 Then
                MsgBox("Dark Updated at Blue and White integration times!")
            End If
            darkTime = Now
            lblSDark.Text = "Dark Expires: " & getExpiration()
            cmdTakeDark.Enabled = True
            If tmrScanReady.Enabled = False Then
                cmdCalculate.Enabled = True
            End If

            If airConnect And Not bAutoSession Then
                ncd1.ProXR.RelayBanks.TurnOffRelay(rlyport)
            End If

            bDarkUpdate = False
            Application.UseWaitCursor = False
            Me.Cursor = Cursors.Arrow
            Application.DoEvents()

        End If
    End Sub

    Public Sub AddDarkRow()
        dgUpdate = True
        dgResults.Rows.Add()                                                                'add row
        dgResults.Rows.Item(dgResults.RowCount - 1).Cells(0).Value = dgResults.RowCount
        dgResults.Rows.Item(dgResults.RowCount - 1).Cells(12).Value = Now.ToShortDateString & " " & Now.Hour & ":" & Now.Minute & ":" & Now.Second    'timestamp
        dgResults.Rows.Item(dgResults.RowCount - 1).Cells(13).Value = FormatNumber(neInt_B.Value, 2)
        dgResults.Rows.Item(dgResults.RowCount - 1).Cells(14).Value = FormatNumber(dBlueDarkAvg, 0)
        dgResults.Rows.Item(dgResults.RowCount - 1).Cells(15).Value = neScanAvg.Value & ", " & FormatNumber(dblueDarkSTD, 1)

        dgResults.Rows.Add()
        dgResults.Rows.Item(dgResults.RowCount - 1).Cells(0).Value = dgResults.RowCount
        dgResults.Rows.Item(dgResults.RowCount - 1).Cells(12).Value = Now.ToShortDateString & " " & Now.Hour & ":" & Now.Minute & ":" & Now.Second    'timestamp
        dgResults.Rows.Item(dgResults.RowCount - 1).Cells(13).Value = FormatNumber(neInt_W.Value, 2)
        dgResults.Rows.Item(dgResults.RowCount - 1).Cells(14).Value = FormatNumber(dWhiteDarkAvg, 0)
        dgResults.Rows.Item(dgResults.RowCount - 1).Cells(15).Value = neScanAvg.Value & ", " & FormatNumber(dWhiteDarkSTD, 1)

        For i = 0 To dgResults.RowCount - 1                                                 'unselect all rows
            dgResults.Rows.Item(i).Selected = False
        Next
        dgResults.Rows.Item(dgResults.RowCount - 1).Selected = True                         'select the row just added
        dgResults.FirstDisplayedScrollingRowIndex = dgResults.RowCount - 1                  'scroll to last row
        dgUpdate = False
    End Sub

    Public Function getExpiration() As String
        Dim snum As Integer

        snum = iDarkExpire - DateDiff(DateInterval.Minute, darkTime, Now)
        If snum <= 0 Then
            Return "NOW"
        Else
            Return snum & " min"
        End If
    End Function

    Public Sub UpdateDark()

        TakeDark(AcqType)
        Application.DoEvents()

        If opRef.Checked Then                           'assign dark save time based on selection (blu or test)
            txtCal_B_Dark.Text = FormatNumber(neInt_B.Value, 2)
        Else
            txtCal_W_Dark.Text = FormatNumber(neInt_W.Value, 2)
        End If

    End Sub

    Public Sub PlotSelect(ByVal inum As Integer)
        '***********************************************************
        '   Procedure:  PlotSelect
        '   Purpose:    Setup plot area parameters
        '   Inputs:     index of plot
        '   Outputs:    None
        '   Author:     HP
        '   Rev His:    7/26/11,   Original
        '                     
        '
        '***********************************************************\

        ChtPlot.BringToFront()
        cmdClearData.Enabled = True
        grpType.Enabled = True
        LoadEllipses(2)
        cbCCT.SelectedIndex = 0

        lblPBIN.BringToFront()
        txtBIN.BringToFront()

    End Sub

    Public Sub LoadBinChart()
        ChtPlot.BringToFront()
        cmdClearData.Enabled = True
        grpType.Enabled = True
        cbCCT.SelectedIndex = 0
        lblPBIN.BringToFront()
        txtBIN.BringToFront()
        lblSysReady.BringToFront()

    End Sub

    Public Sub LoadSystemInfo()
        '*************************************************************************************
        ' Procedure Name:   LoadSystemInfo()
        ' Description:      Open config file and load variables
        ' Inputs:           None
        ' Outputs:          None
        ' Author:           HP
        ' Last Modified:    10/4/13  Add Linear, Column Info and save mode
        '*************************************************************************************
        Dim sReader As StreamReader
        Dim sTemp As String

        sReader = New StreamReader(Application.StartupPath & "\System.cfg")
        sTemp = sReader.ReadLine
        IS_Num = sTemp.Substring(sTemp.IndexOf(",") + 1)            'integration sphere number
        sTemp = sReader.ReadLine
        iSaveBlueInt = sTemp.Substring(sTemp.IndexOf(",") + 1)      'default blue int time
        sTemp = sReader.ReadLine
        iSaveTestInt = sTemp.Substring(sTemp.IndexOf(",") + 1)      'default test int time
        sTemp = sReader.ReadLine
        iBluePeakCnts = sTemp.Substring(sTemp.IndexOf(",") + 1)      'default test int time

        sTemp = sReader.ReadLine
        sTemp = sTemp.Substring(sTemp.IndexOf(",") + 1)      'burn blue desc
        blue_startWL = Val(sTemp.Substring(0, sTemp.IndexOf(",")))
        blue_stopwl = Val(sTemp.Substring(sTemp.IndexOf(",") + 1))

        sTemp = sReader.ReadLine
        sTemp = sTemp.Substring(sTemp.IndexOf(",") + 1)      'burn white desc
        pwr_startWL = Val(sTemp.Substring(0, sTemp.IndexOf(",")))
        pwr_stopwl = Val(sTemp.Substring(sTemp.IndexOf(",") + 1))

        sTemp = sReader.ReadLine
        scanBox = Val(sTemp.Substring(sTemp.IndexOf(",") + 1))
        sTemp = sReader.ReadLine
        ScanReadyTime = sTemp.Substring(sTemp.IndexOf(",") + 1) * 1000
        sTemp = sReader.ReadLine
        baselineThresh = Val(sTemp.Substring(sTemp.IndexOf(",") + 1))
        sTemp = sReader.ReadLine
        int_wait = Val(sTemp.Substring(sTemp.IndexOf(",") + 1)) * 1000
        sTemp = sReader.ReadLine
        iDarkExpire = Val(sTemp.Substring(sTemp.IndexOf(",") + 1))
        sTemp = sReader.ReadLine
        saveType = Val(sTemp.Substring(sTemp.IndexOf(",") + 1))
        sTemp = sReader.ReadLine
        dBoxFactory = sTemp.Substring(sTemp.IndexOf(",") + 1)
        sTemp = sReader.ReadLine
        dBoxInline = sTemp.Substring(sTemp.IndexOf(",") + 1)
        sTemp = sReader.ReadLine
        iS_Philips = sTemp.Substring(sTemp.IndexOf(",") + 1)
        If iS_Philips > 0 Then
            sTemp = sReader.ReadLine
            cbCCT.Items.Clear()
            For i = 1 To iS_Philips - 1
                cbCCT.Items.Add(sTemp.Substring(0, sTemp.IndexOf(",")))
                sTemp = sTemp.Substring(sTemp.IndexOf(",") + 1)
            Next
            cbCCT.Items.Add(sTemp)

            sTemp = sReader.ReadLine            'read air line
            airConnect = sTemp.Substring(sTemp.IndexOf(",") + 1, 1)
            If airConnect Then
                sTemp = sTemp.Substring(sTemp.IndexOf(",") + 1)
                sTemp = sTemp.Substring(sTemp.IndexOf(",") + 1)
                stage_com = sTemp.Substring(0, sTemp.IndexOf(","))
                rlyport = sTemp.Substring(sTemp.IndexOf(",") + 1)
            End If
        End If
        sTemp = sReader.ReadLine
        ceTol = sTemp.Substring(sTemp.IndexOf(",") + 1)      'default test int time
        sTemp = sReader.ReadLine
        clrTol = sTemp.Substring(sTemp.IndexOf(",") + 1)      'default test int time
        sTemp = sReader.ReadLine
        criTol = sTemp.Substring(sTemp.IndexOf(",") + 1)      'default test int time
        sTemp = sReader.ReadLine
        sTemp = sTemp.Substring(sTemp.IndexOf(",") + 1)
        intrinsicCE = sTemp.Substring(0, sTemp.IndexOf(","))
        sTemp = sTemp.Substring(sTemp.IndexOf(",") + 1)
        intrinsicX = sTemp.Substring(0, sTemp.IndexOf(","))
        sTemp = sTemp.Substring(sTemp.IndexOf(",") + 1)
        intrinsicY = sTemp.Substring(0, sTemp.IndexOf(","))
        intrinsicCRI = sTemp.Substring(sTemp.IndexOf(",") + 1)
        sReader.Close()

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Dim imsg As Integer

        imsg = MsgBox("Are you sure you would like to exit?", vbYesNo, "Exit Confirmation!")
        If imsg = vbYes Then
            Me.Close()
        End If
    End Sub

    Private Sub mnuExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExport.Click

        If saveType = 1 Then
            Call savetoText()
        Else
            Call SaveToExcel()              'save data
        End If

    End Sub

    Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Dim imsg As Integer

        imsg = MsgBox("Are you sure you would like to exit?", vbYesNo, "Exit Confirmation!")
        If imsg = vbYes Then
            DisconnectSpectrometer()
            Me.Close()
        End If
    End Sub

    Public Sub SaveToExcel()
        '***********************************************************
        '   Procedure:  SaveToExcel
        '   Purpose:    Open Excel Template and save test data
        '   Inputs:     None
        '   Outputs:    None
        '   Author:     HP
        '   Rev His:    7/26/11,   Original
        '               10/26/11,   Add operator, integrating sphere # to file        
        '
        '***********************************************************\
        Dim i, j As Integer
        Dim cImage As Image
        Dim sCol As String

        ReDim Tblblock(dgResults.RowCount, dgResults.ColumnCount)

        If OpenExcelFile() = 1 Then
            Me.Cursor = Cursors.WaitCursor
            wSheet = wBook.Worksheets.Item("Raw Data")                  'select Raw Data worksheet
            wSheet.Range("B2").Value = txtOperator.Text                 'write operator
            wSheet.Range("B3").Value = txtLot.Text                      'work order
            wSheet.Range("B4").Value = IS_Num                           'IS Num
            wSheet.Range("B5").Value = BinType                          'BinType

            For j = 0 To dgResults.ColumnCount - 1                                  'put together the 2d array for the datagrid
                Tblblock(0, j) = dgResults.Columns.Item(j).HeaderText
            Next

            For i = 1 To dgResults.RowCount
                For j = 0 To dgResults.ColumnCount - 1
                    Tblblock(i, j) = dgResults.Rows.Item(i - 1).Cells(j).Value
                Next
            Next

            sCol = GetCol(dgResults.ColumnCount) & dgResults.RowCount + 29      'get bottom right index of table
            wSheet.Range("A29", sCol).Value = Tblblock                          'write block to sheet

            oRange = wSheet.Range("B30", "O" & dgResults.RowCount + 29)         'format optical data as double to perform operations and chart
            For Each modExcel.oC In oRange
                oC.Value = CDbl(oC.Value)
            Next

            oRange = wSheet.Range("Q30", "R" & dgResults.RowCount + 29)         'format integration and peak to integer
            For Each modExcel.oC In oRange
                oC.Value = CInt(oC.Value)
            Next

            oRange = wSheet.Range("T30", "AB" & dgResults.RowCount + 29)        'format rest of table as double
            For Each modExcel.oC In oRange
                oC.Value = CDbl(oC.Value)
            Next

            Try
                ChtPlot.SaveImage(My.Application.Info.DirectoryPath & "\ExportData\ChtImage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
                cImage = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\ExportData\ChtImage.jpg")
                Clipboard.SetImage(cImage)
                wSheet.Paste(wSheet.Range("C1"))
                Clipboard.Clear()
                cImage.Dispose()
                If iSaveScanNo > -1 Then
                    Call WriteScanSheet()
                End If
                SaveAndCloseExcelFile()
                lblFName.Text = "File Saved as " & saveFN
            Catch
                lblFName.Text = "Save Failed.....What did you do?"
            End Try
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Public Sub WriteScanSheet()
        Dim saveNotes(250, 1) As String
        Dim iSaveNoteIdx As Integer = 0
        Dim IdxLine As String = ""
        Dim SIDLine As String = ""
        Dim tblScanIdx As Integer = 0

        wSheet = wBook.Worksheets.Item("Scans")
        For i = 0 To dgResults.RowCount - 1                                                         'this tells us how many saved spectrums are in the table
            If dgResults.Rows(i).Cells(dgResults.ColumnCount - 1).Value > -1 Then
                saveNotes(iSaveNoteIdx, 0) = dgResults.Rows(i).Cells(dgResults.ColumnCount - 1).Value
                saveNotes(iSaveNoteIdx, 1) = dgResults.Rows(i).Cells(0).Value
                iSaveNoteIdx += 1
            End If
        Next

        For i = 0 To iSaveScanNo
            If Val(saveNotes(tblScanIdx, 0)) = i Then
                wSheet.Range(GetCol(1 + i) & 2).Value = saveNotes(tblScanIdx, 0)
                wSheet.Range(GetCol(1 + i) & 4).Value = saveNotes(tblScanIdx, 1)
                tblScanIdx += 1
            End If
        Next

        wSheet.Range("B1", GetCol(1 + iSaveScanNo) & 1).Merge()
        wSheet.Range("B3", GetCol(1 + iSaveScanNo) & 3).Merge()
        wSheet.Range("B4", GetCol(iSaveScanNo + 1) & "4").Interior.ColorIndex = 36
        wSheet.Range("A5", GetCol(iSaveScanNo + 1) & iNumPix + 4).Value = ScanData
        wSheet = wBook.Worksheets.Item("Raw Data")
    End Sub

    Public Function GetCol(ByVal num As Integer) As String
        Dim iMod, iRem As Integer

        iMod = num \ 26
        iRem = num Mod 26

        Select Case iMod
            Case 0
                Return Chr(Asc("A") + iRem)
            Case Else
                Return Chr(Asc("A") + (iMod - 1)) & Chr(Asc("A") + iRem)
        End Select
    End Function

    Public Sub savetoText()
        Dim swriter As StreamWriter
        Dim sDate As String

        sDate = Format(Now.Month, "00") & Format(Now.Day, "00") & Now.Year.ToString.Substring(2) & "_" & Format(Now.Hour, "00") & Format(Now.Minute, "00") & Format(Now.Second, "00")

        swriter = New StreamWriter(My.Application.Info.DirectoryPath & "\ExportData\" & txtLot.Text & "_" & sDate & ".xls")
        swriter.AutoFlush = True

        swriter.WriteLine("Operator ; " & txtOperator.Text)                     'write operator
        swriter.WriteLine("Job Description ; " & txtLot.Text)                          'work order
        swriter.WriteLine("Sphere ID ; " & IS_Num)                               'IS Num
        swriter.WriteLine("Product ID ; " & cbCCT.SelectedItem.ToString)          'BinType

        For j = 0 To dgResults.ColumnCount - 1                      'write column headers
            swriter.Write(dgResults.Columns(j).HeaderText & ";")
        Next
        swriter.Write(vbCrLf)

        For i = 0 To dgResults.RowCount - 1                         'write test data
            For j = 0 To dgResults.ColumnCount - 1
                swriter.Write(dgResults.Rows(i).Cells(j).Value & ";")
            Next
            swriter.Write(vbCrLf)
        Next

        swriter.Close()

        If iSaveScanNo > -1 Then
            Call WriteScanFile()
        End If

        lblFName.Text = "File Saved as " & txtLot.Text & "_" & sDate & ".xls"
    End Sub

    Public Sub WriteScanFile()
        Dim swriter As StreamWriter
        Dim saveNotes(250, 2) As String
        Dim iSaveNoteIdx As Integer = 0
        Dim scanstring As String
        'Dim IdxLine As String = ""
        Dim SIDLine As String = ""
        Dim tblScanIdx As Integer = 0
        Dim sDate As String

        sDate = Format(Now.Month, "00") & Format(Now.Day, "00") & Now.Year.ToString.Substring(2) & "_" & Format(Now.Hour, "00") & Format(Now.Minute, "00") & Format(Now.Second, "00")


        swriter = New StreamWriter(My.Application.Info.DirectoryPath & "\ExportData\Scans\" & txtLot.Text & "_" & sDate & "_Scans.xls")
        swriter.WriteLine("Operator ; " & txtOperator.Text)                     'write operator
        swriter.WriteLine("Job Description ; " & txtLot.Text)                          'work order
        swriter.WriteLine("Sphere ID ; " & IS_Num)                               'IS Num
        swriter.WriteLine("Product ID ; " & cbCCT.SelectedItem.ToString)          'BinType

        For i = 0 To dgResults.RowCount - 1                                                         'this tells us how many saved spectrums are in the table
            'If dgResults.Rows(i).Cells(dgResults.ColumnCount - 1).Value > -1 Then
            If dgResults.Rows(i).Cells(1).Value > 0 Then
                'saveNotes(iSaveNoteIdx, 0) = dgResults.Rows(i).Cells(dgResults.ColumnCount - 1).Value
                saveNotes(iSaveNoteIdx, 0) = iSaveNoteIdx
                saveNotes(iSaveNoteIdx, 1) = dgResults.Rows(i).Cells(0).Value
                iSaveNoteIdx += 1
            End If
        Next

        For i = 0 To iSaveScanNo
            If Val(saveNotes(tblScanIdx, 0)) = i Then
                'IdxLine += saveNotes(tblScanIdx, 0) & " ; "
                SIDLine += saveNotes(tblScanIdx, 1) & " ; "
                tblScanIdx += 1
            Else
                'IdxLine += " ; "
                'SIDLine += " ; "
                tblScanIdx += 1
            End If
        Next

        'swriter.WriteLine("Scan Index; " & IdxLine)
        swriter.WriteLine("Wavelength ; " & SIDLine)
        For i = 0 To iNumPix - 1
            scanstring = ""
            For j = 0 To iSaveScanNo '- 1
                scanstring += ScanData(i, j) & ";"
            Next
            'swriter.WriteLine(wavelengthArray(i) & ";" & scanstring)
            swriter.WriteLine(scanstring)
        Next
        swriter.Close()
    End Sub

    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        'clear table of all data, prompt for verification
        Dim msg As Integer

        msg = MsgBox("To delete single scan, select scan and press 'delete' key." & vbCrLf & "Are you sure you want to Delete the entire table?", MsgBoxStyle.YesNo, "Scan Delete")
        If msg = vbYes Then
            dgResults.Rows.Clear()
            ChtPlot.Series("Data").Points.Clear()
        End If

        iSaveScanNo = -1

    End Sub

    Private Sub mnuAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAbout.Click
        'display app info
        MsgBox("Phillips Disk Testing Application using OmniDriver, Spam for MFG" & vbCrLf & "Version:  " & My.Application.Info.Version.ToString & vbCrLf & "Author:  Herman Pahulu" & vbCrLf & "Date:  " & My.Application.Info.Copyright, , "Application Information")
    End Sub

    Private Sub cbCCT_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCCT.SelectedIndexChanged
        'load ellipses based on selection

        binIdx = cbCCT.SelectedIndex            'load bin

        BinType = cbCCT.Text        'stemp.Substring(1, 2) & "00K"
        If iS_Philips Then
            bin_spec = 2
        ElseIf Val(BinType.Substring(0, 1)) > 4 Then
            bin_spec = 4
        Else
            bin_spec = 3
        End If

        PlotOsramEllipse(binIdx, ChtPlot)        'plot the ellipse

    End Sub

    Private Sub opRef_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opRef.CheckedChanged
        If opRef.Checked And frmLoad Then
            Me.Cursor = Cursors.WaitCursor
            AcqType = 0                                         'reference acquisition type
            Call UpdateIntTime()
            ChtPlot.ChartAreas(0).BackColor = Color.LightBlue
            neInt_B.BackColor = Color.Yellow
            neInt_W.BackColor = Color.White
            Application.DoEvents()

            System.Threading.Thread.Sleep(int_wait)
            burnPixels = wrapper.getSpectrum(0)
            If Not bDarkUpdate Then
                Me.Cursor = Cursors.Default
            End If
        End If
    End Sub

    Private Sub opTest_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opTest.CheckedChanged
        If opTest.Checked And frmLoad Then
            Me.Cursor = Cursors.WaitCursor
            AcqType = 1                                         'test acquisition type
            Call UpdateIntTime()
            ChtPlot.ChartAreas(0).BackColor = Color.White
            neInt_B.BackColor = Color.White
            neInt_W.BackColor = Color.Yellow
            Application.DoEvents()

            System.Threading.Thread.Sleep(int_wait)
            burnPixels = wrapper.getSpectrum(0)
            If Not bDarkUpdate Then
                Me.Cursor = Cursors.Default
            End If

        End If
    End Sub

    Private Sub cmdClearData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearData.Click
        ChtPlot.Series("Data").Points.Clear()                   'clear data from plot
    End Sub

    Private Sub neIntegrationTime_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles neInt_B.Click, neInt_W.Click, neOffy.Click, neOffx.Click, neOffCRI.Click, neoffCE.Click
        If frmLoad Then
            Call UpdateIntTime()
        End If

    End Sub

    Public Sub UpdateIntTime()

        If opRef.Checked Then
            iIntegrationTime = neInt_B.Value * 1000   'update variable and set to uSec
        Else
            iIntegrationTime = neInt_W.Value * 1000
        End If
        wrapper.setIntegrationTime(0, iIntegrationTime)     'set int time on spectrometer and burn scan
        specUpdate = Now
        System.Threading.Thread.Sleep(100)
        lblIntReadBack.Text = "Int Set to " & wrapper.getIntegrationTime(0) & " uS"

    End Sub

    Private Sub neScanAvg_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles neScanAvg.ValueChanged
        If frmLoad Then
            wrapper.setScansToAverage(0, neScanAvg.Value)       'update based on new scan avg value and burn scan
            System.Threading.Thread.Sleep(100)
            specUpdate = Now

        End If

    End Sub

    Private Sub dgResults_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgResults.SelectionChanged
        If dgUpdate = False Then
            ChtPlot.Series("Current").Points.Clear()
            If dgResults.RowCount <> 0 Then
                If dgResults.CurrentRow.Cells(14).Value < baselineThresh Then
                    Application.DoEvents()
                Else
                    ChtPlot.Series("Current").Points.AddXY(dgResults.CurrentRow.Cells(4).Value, dgResults.CurrentRow.Cells(5).Value)
                End If

            End If
        End If
    End Sub

    Private Sub tmrScanReady_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrScanReady.Tick
        tmrScanReady.Enabled = False

        If cmdTakeDark.Enabled = True Then
            cmdCalculate.Enabled = True
        End If
    End Sub

    Public Sub GetCurrentWS(ByVal wsArray As Array, ByVal numB As Integer)  'numB is for making the samples
        Dim tS As String = ""
        Dim imsg As Integer

        For i = 1 To 3
            promptIdx = i
            If numB = 1 Then        'making sample set
                If i = 1 Then
                    sampledesc = "Enter Working Sample Set Description and" & vbCrLf & "Install Working Sample #" & i & vbCrLf & "Click OK when complete"
                Else
                    sampledesc = "Install Working Sample #" & i & vbCrLf & "Click OK when complete"
                End If
            Else                    'reading for verification
                SID = wsID(i - 1)
                sampledesc = "Install Working Std " & wsID(i - 1) & vbCrLf & "Click OK when complete"
            End If
            wknum = i
            imsg = DialogTakeSample.ShowDialog()
            If numB = 1 Then
                wsID(i - 1) = sampleSetID & "-" & i
            End If
            If AcqSuccess = False Or imsg = vbCancel Then
                Exit Sub
            End If
            wsArray(i - 1, 0) = aResultPack(3)
            wsArray(i - 1, 1) = aResultPack(4)
            wsArray(i - 1, 2) = aResultPack(5)
            wsArray(i - 1, 3) = aResultPack(8)

        Next

    End Sub

    Public Sub GetGoldSamples()
        Dim imsg As Integer

        goldWS = True           'to not prompt for measurements
        For i = 0 To gldSamplesCnt - 1
            promptIdx = i
            sampledesc = "Install Gold Sample " & SampleID(i) & vbCrLf & "Click OK when complete"
            SID = SampleID(i)
            imsg = DialogTakeSample.ShowDialog()
            If AcqSuccess = False Or imsg = vbCancel Then
                Exit Sub
            End If
            SISData(i, 0) = aResultPack(3)
            SISData(i, 1) = aResultPack(4)
            SISData(i, 2) = aResultPack(5)
            SISData(i, 3) = aResultPack(8)
        Next
        SID = ""
        goldWS = False
    End Sub

    Public Sub ConfirmStds()
        Dim ws_currentAvg(3) As Double
        Dim ws_refAvg(3) As Double
        Dim ws_currentTot(3) As Double
        Dim ws_refTot(3) As Double
        Dim iConfirm As Integer = 0
        Dim iFailMsg As String = ""
        Dim stdDeltas(3) As Double

        For i = 0 To 3          'number of parameters
            For j = 0 To 2      'number of samples
                ws_currentTot(i) += WS_Current(j, i)
                ws_refTot(i) += WS_Ref(j, i)
            Next
        Next

        For i = 0 To 3
            ws_currentAvg(i) = ws_currentTot(i) / 3
            ws_refAvg(i) = ws_refTot(i) / 3
        Next

        If ws_currentAvg(0) <= ws_refAvg(0) * (1 + (0.01 * ceTol)) And ws_currentAvg(0) >= ws_refAvg(0) * (1 - (0.01 * ceTol)) Then     'ce tolerance
            iConfirm += 8
        Else
            iFailMsg += "CE "
        End If

        stdDeltas(0) = FormatNumber(1 - (ws_refAvg(0) / ws_currentAvg(0)), 4)

        If ws_currentAvg(1) <= ws_refAvg(1) + clrTol And ws_currentAvg(1) >= ws_refAvg(1) - clrTol Then   'x tolerance
            iConfirm += 4
        Else
            iFailMsg += "CIE x "
        End If

        stdDeltas(1) = FormatNumber(ws_refAvg(1) - ws_currentAvg(1), 4)

        If ws_currentAvg(2) <= (ws_refAvg(2) + clrTol) And ws_currentAvg(2) >= (ws_refAvg(2) - clrTol) Then   'y tolerance
            iConfirm += 2
        Else
            iFailMsg += "CIE y "
        End If
        stdDeltas(2) = FormatNumber(ws_refAvg(2) - ws_currentAvg(2), 4)

        If ws_currentAvg(3) <= (ws_refAvg(3) + criTol) And ws_currentAvg(3) >= (ws_refAvg(3) - criTol) Then       'cri tolerance
            iConfirm += 1
        Else
            iFailMsg += "CRI "
        End If
        stdDeltas(3) = FormatNumber(ws_refAvg(3) - ws_currentAvg(3), 1)

        Select Case iConfirm
            Case 15
                MsgBox("CE delta = " & stdDeltas(0) & ", Spec <= " & (ceTol * 0.01) & vbCrLf & "CIE x delta = " & stdDeltas(1) & ", Spec <= " & clrTol & vbCrLf &
                       "CIE y delta = " & stdDeltas(2) & ", Spec <= " & clrTol & vbCrLf & "CRI delta = " & stdDeltas(3) & ", Spec <= " & criTol & _
                    vbCrLf & "Samples are within Tolderance......System is Ready.")
                lblSysReady.Visible = False
                mnuILSamples.Enabled = True
            Case Else
                MsgBox(iFailMsg & " are out of tolerance, re-measure or consult Engineering.")
        End Select

    End Sub

    Public Sub ReadGoldFile()
        Dim whiteIntTime As Integer
        Dim blueIntTime As Integer
        Dim LIS_TestTime As String
        Dim sampleSetID As String

        AcqSuccess = True
        Dim gfn As String = dBoxFactory & "\CLC-PLF-" & id_fn & "\" & id_fn & "_Philips.ref"

        BaseFN = "CLC-PLF-" & id_fn
        If File.Exists(gfn) = False Then
            AcqSuccess = False
            MsgBox("Gold file could not be located")
            Exit Sub
        Else
            Dim sReader As New StreamReader(gfn)
            Dim sTemp As String

            sTemp = sReader.ReadLine()              'date
            sampleSetID = sTemp.Substring(sTemp.IndexOf(",") + 1)
            gldSamplesCnt = CInt(sampleSetID.Substring(sampleSetID.IndexOf("-") + 1))
            sTemp = sReader.ReadLine
            LIS_TestTime = sTemp.Substring(sTemp.IndexOf(",") + 1)

            For i = 0 To gldSamplesCnt - 1
                sTemp = sReader.ReadLine
                sTemp = sTemp.Substring(sTemp.IndexOf(",") + 1)
                For j = 0 To gldSamplesCnt - 1
                    LISData(i, j) = Val(sTemp.Substring(0, sTemp.IndexOf(",")))
                    sTemp = sTemp.Substring(sTemp.IndexOf(",") + 1)
                Next
                LISData(i, 3) = Val(sTemp)
            Next

            If sReader.EndOfStream Then
                MsgBox("Gold File Doesn't have SIS measurements required for correlation.")
                AcqSuccess = False
                Exit Sub
            End If
            sTemp = sReader.ReadLine            'burn TestTime
            sTemp = sReader.ReadLine                                'Fixture ID
            gFixtureID = sTemp.Substring(sTemp.IndexOf(",") + 1)
            sTemp = sReader.ReadLine
            blueIntTime = sTemp.Substring(sTemp.IndexOf(",") + 1)   'blue integration time
            sTemp = sReader.ReadLine
            whiteIntTime = sTemp.Substring(sTemp.IndexOf(",") + 1)  'white integration time

            For i = 0 To 5
                sTemp = sReader.ReadLine
                sTemp = sTemp.Substring(sTemp.IndexOf(",") + 1)
                For j = 0 To 2
                    SISData(i, j) = Val(sTemp.Substring(0, sTemp.IndexOf(",")))
                    sTemp = sTemp.Substring(sTemp.IndexOf(",") + 1)
                Next
                SISData(i, 3) = Val(sTemp)
            Next

            sReader.Close()

            Call GetCorrelation()

        End If
    End Sub

    Public Sub UpdateGoldFile()
        Dim sTimeStamp As String
        Dim gData(5) As String
        Dim LISDataLine As String = ""
        Dim SISDataLine As String = ""
        Dim swriter As StreamWriter
        Dim gfn As String = Application.StartupPath & "\Corrections\Gold Files\" & BaseFN & ".gsf"

        If File.Exists(gfn) = False Then
            swriter = New StreamWriter(gfn)
        Else
            swriter = New StreamWriter(gfn, 1)
        End If

        sTimeStamp = Format(Now.Month, "00") & Format(Now.Day, "00") & Now.Year.ToString.Substring(2) & "_" & Format(Now.Hour, "00") & Format(Now.Minute, "00") & Format(Now.Second, "00")
        swriter.WriteLine(sTimeStamp)

        For i = 0 To gldSamplesCnt - 1
            swriter.WriteLine(SampleID(i) & "," & LISData(i, 0) & "," & LISData(i, 1) & "," & LISData(i, 2) & "," & LISData(i, 3) & "," & SISData(i, 0) & "," & SISData(i, 1) & "," & SISData(i, 2) & "," & SISData(i, 3))
        Next

        swriter.Close()
    End Sub

    Public Sub ReadREFWS()
        Dim cfn As String = Application.StartupPath & "\Corrections\WS Files\" & BaseFN & ".ws1"
        If File.Exists(cfn) = False Then
            MsgBox("WS file doesn't exist")
            Exit Sub
        Else
            Dim sReader As New StreamReader(cfn)
            Dim sTemp As String

            sTemp = sReader.ReadLine
            wsDate = sTemp.Substring(sTemp.IndexOf(",") + 1)
            sTemp = sReader.ReadLine
            sampleSetID = sTemp.Substring(sTemp.IndexOf(",") + 1)
            For i = 0 To 2
                sTemp = sReader.ReadLine
                wsID(i) = sTemp.Substring(0, sTemp.IndexOf(","))
                sTemp = sTemp.Substring(sTemp.IndexOf(",") + 1)
                For j = 0 To 3
                    WS_Ref(i, j) = sTemp.Substring(0, sTemp.IndexOf(","))
                    sTemp = sTemp.Substring(sTemp.IndexOf(",") + 1)
                Next
            Next
            sReader.Close()
        End If


    End Sub

    Public Sub GetCorrelation()
        Dim LISTotals(3) As Double
        Dim SISTotals(3) As Double
        Dim LISAvg(3) As Double
        Dim SISAvg(3) As Double

        For i = 0 To 3
            For j = 0 To gldSamplesCnt - 1
                LISTotals(i) += LISData(j, i)
                SISTotals(i) += SISData(j, i)
            Next
        Next

        For i = 0 To 3
            LISAvg(i) = LISTotals(i) / gldSamplesCnt
            SISAvg(i) = SISTotals(i) / gldSamplesCnt
        Next

        dFactors(0) = FormatNumber(LISAvg(0) / SISAvg(0), 4)
        dFactors(1) = FormatNumber(LISAvg(1) - SISAvg(1), 4)
        dFactors(2) = FormatNumber(LISAvg(2) - SISAvg(2), 4)
        dFactors(3) = FormatNumber(LISAvg(3) - SISAvg(3), 1)
        neoffCE.Value = intrinsicCE
        neOffx.Value = intrinsicX
        neOffy.Value = intrinsicY
        neOffCRI.Value = intrinsicCRI
        lblCorrection.Text = "Correlation for " & BaseFN & " loaded."
        Application.DoEvents()

    End Sub

    Private Sub mnuStartJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuStartJob.Click
        Dim imsg As Integer

        StartLoad = False
        DialogStart.ShowDialog()    'prompt for operator, fixID WO
        If cbCCT.SelectedText.Contains(id_cct) = False Then         'need to load the correct bin after scan
            For i = 0 To cbCCT.Items.Count - 1
                If cbCCT.Items(i).ToString.Contains(id_cct) Then
                    cbCCT.SelectedIndex = i
                    Exit For
                End If
            Next
        End If
        'select Plot based on CCT of panel

        lblCorrection.Text = "Retrieving from Gold Sample"
        Call OpenGoldSource()       'have user select gold file to read from
        DialogGetDark.ShowDialog()  'get background for both blue and white.  measure blue.
        If AcqSuccess = False Then
            MsgBox("Routine Aborted due to failed measurement, re-check system and try again")
            Exit Sub
        End If
        Call ResetCorrs()               'reset corr factors before measuring gold samples
        Call GetGoldSamples()           'get SIS data for gold samples
        If AcqSuccess = False Then
            MsgBox("Routine Aborted due to failed measurement, re-check system and try again")
            Exit Sub
        End If
        Call UpdateGoldFile()           'add SIS data to gold file
        Call GetCorrelation()           'get correlation 
        Call ReadREFWS()                'read reference working stds
        imsg = DialogWSConfirm.ShowDialog
        If imsg = vbYes Then
            Call GetCurrentWS(WS_Current, 0)                       'measure working stds
            If AcqSuccess = False Then
                MsgBox("Routine Aborted due to failed measurement, re-check system and try again")
                Exit Sub
            End If
            Call ConfirmStds()                                  'check control conditions
            UpdateWS(1, WS_Current)                             'update working standard file
        Else
            MsgBox("Get Correct Samples and Light Engine")
        End If

    End Sub

    Public Sub UpdateWS(ByVal num As Integer, ByVal ws_data As Array)
        Dim wsString As String
        Dim sDate As String
        Dim cfn As String = Application.StartupPath & "\Corrections\WS Files\" & BaseFN & ".ws1"

        sDate = Format(Now.Month, "00") & Format(Now.Day, "00") & Now.Year.ToString.Substring(2) & "_" & Format(Now.Hour, "00") & Format(Now.Minute, "00") & Format(Now.Second, "00")

        Dim swriter As New StreamWriter(cfn, num)

        If num = 0 Then
            swriter.WriteLine("Date of Cal," & Format(Now.Month, "00") & Format(Now.Day, "00") & Now.Year.ToString.Substring(2))
            swriter.WriteLine("SampleSetID," & sampleSetID)
        End If
        For i = 0 To 2
            wsString = wsID(i) & "," & ws_data(i, 0) & "," & ws_data(i, 1) & "," & ws_data(i, 2) & "," & ws_data(i, 3) & "," & dFactors(0) & "," & dFactors(1) & "," & dFactors(2) & "," & dFactors(3) & "," & sDate
            swriter.WriteLine(wsString)
        Next
        swriter.Close()
        If num = 0 Then
            MsgBox("Working Sample Creation Complete, Reminder to Label Working Sample Set Bag: " & sampleSetID & ", Cal Date: " & Format(Now.Month, "00") & Format(Now.Day, "00") & Now.Year.ToString.Substring(2) & ". System is Ready!!")
            lblSysReady.Visible = False
            mnuILSamples.Enabled = True
        End If
        SID = ""
    End Sub

    Public Function OpenGoldSource() As Boolean
        Dim imsg As Integer
        openGSrc.Title = "Please Select a Gold Reference File for this product"
        openGSrc.Filter = "Ref Files|*.ref"
        openGSrc.InitialDirectory = dBoxFactory
        imsg = openGSrc.ShowDialog()
        If imsg = vbCancel Then
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub openGSrc_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles openGSrc.FileOk
        Dim GsrcFN As String

        GsrcFN = openGSrc.FileName
        Call ReadGoldSrc(GsrcFN)

    End Sub

    Public Sub ReadGoldSrc(ByVal fn As String)
        Dim GData(10) As String
        Dim LIS_TestTime As String
        Dim samplesetid As String

        AcqSuccess = True
        Dim sReader As New StreamReader(fn)
        Dim sTemp As String

        sTemp = sReader.ReadLine()              'date
        samplesetid = sTemp.Substring(sTemp.IndexOf(",") + 1)
        gldSamplesCnt = CInt(samplesetid.Substring(samplesetid.IndexOf("-") + 1))
        sTemp = sReader.ReadLine
        LIS_TestTime = sTemp.Substring(sTemp.IndexOf(",") + 1)

        For i = 0 To gldSamplesCnt - 1
            sTemp = sReader.ReadLine
            SampleID(i) = sTemp.Substring(0, sTemp.IndexOf(","))
            sTemp = sTemp.Substring(sTemp.IndexOf(",") + 1)
            For j = 0 To 2
                LISData(i, j) = Val(sTemp.Substring(0, sTemp.IndexOf(",")))
                sTemp = sTemp.Substring(sTemp.IndexOf(",") + 1)
            Next
            LISData(i, 3) = Val(sTemp)
        Next

    End Sub

    Private Sub chtPlot_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ChtPlot.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            ChtPlot.ChartAreas(0).AxisX.Maximum = ChtPlot.ChartAreas(0).AxisX.Maximum + 0.005
            ChtPlot.ChartAreas(0).AxisX.Minimum = ChtPlot.ChartAreas(0).AxisX.Minimum - 0.005
            ChtPlot.ChartAreas(0).AxisY.Maximum = ChtPlot.ChartAreas(0).AxisY.Maximum + 0.005
            ChtPlot.ChartAreas(0).AxisY.Minimum = ChtPlot.ChartAreas(0).AxisY.Minimum - 0.005
        ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
            If (ChtPlot.ChartAreas(0).AxisX.Maximum > ChtPlot.ChartAreas(0).AxisX.Minimum + 0.02) And (ChtPlot.ChartAreas(0).AxisY.Maximum > ChtPlot.ChartAreas(0).AxisY.Minimum + 0.02) Then
                ChtPlot.ChartAreas(0).AxisX.Maximum = ChtPlot.ChartAreas(0).AxisX.Maximum - 0.005
                ChtPlot.ChartAreas(0).AxisX.Minimum = ChtPlot.ChartAreas(0).AxisX.Minimum + 0.005
                ChtPlot.ChartAreas(0).AxisY.Maximum = ChtPlot.ChartAreas(0).AxisY.Maximum - 0.005
                ChtPlot.ChartAreas(0).AxisY.Minimum = ChtPlot.ChartAreas(0).AxisY.Minimum + 0.005
            End If
        End If

        ChtPlot.ChartAreas(0).AxisX.Interval = (ChtPlot.ChartAreas(0).AxisX.Maximum - ChtPlot.ChartAreas(0).AxisX.Minimum) / 5
        ChtPlot.ChartAreas(0).AxisY.Interval = (ChtPlot.ChartAreas(0).AxisY.Maximum - ChtPlot.ChartAreas(0).AxisY.Minimum) / 5
    End Sub

    Private Sub neInt_B_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles neInt_B.ValueChanged, neInt_W.ValueChanged
        If frmLoad Then
            Call UpdateIntTime()
        End If
    End Sub

    Private Sub mnuILSamples_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuILSamples.Click
        If OpenInLineF() Then
            If GetCalSamples() Then
                Call UpdateInLineFile()
            Else
                MsgBox("Update Cal Cancelled")
            End If
        End If
    End Sub

    Public Function OpenInLineF() As Boolean
        Dim imsg As Integer

        openInLine.Title = "Please Select an In-Line Calibration Sample File"
        openInLine.Filter = "Cor Files|*.cor"
        openInLine.InitialDirectory = dBoxInline
        imsg = openInLine.ShowDialog()
        If imsg = vbCancel Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub openInLine_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles openInLine.FileOk

        inLineFN = openInLine.FileName
    End Sub

    Public Function GetCalSamples() As Boolean
        Dim idMsg As Integer

        For i = 1 To 9
            promptIdx = i
            SID = "Inline Disk-" & i
            sampledesc = "Install Disk Sample Pos-" & i & ".  Click OK when complete"
            idMsg = DialogTakeSample.ShowDialog()
            If AcqSuccess = False Then
                Return False
            End If

            If idMsg = vbIgnore Then                    'if user choses to skip disk, assign 0 to offline data.  Sync shall ignore this disk.
                OffLineData(i - 1, 0) = 0
                OffLineData(i - 1, 1) = 0
                OffLineData(i - 1, 2) = 0
                OffLineData(i - 1, 3) = 0
            Else
                OffLineData(i - 1, 0) = aResultPack(3)  'ce
                OffLineData(i - 1, 1) = aResultPack(4)  'x
                OffLineData(i - 1, 2) = aResultPack(5)  'y
                OffLineData(i - 1, 3) = aResultPack(8)  'ra
            End If
        Next i
        SID = ""
        Return True
    End Function

    Public Sub UpdateInLineFile()
        Dim swriter As New StreamWriter(inLineFN, 1)

        swriter.WriteLine("Offline Data taken on " & IS_Num)
        For i = 0 To 8
            swriter.WriteLine("OffLine-" & i + 1 & "," & OffLineData(i, 0) & "," & OffLineData(i, 1) & "," & OffLineData(i, 2) & "," & OffLineData(i, 3))
        Next
        swriter.Close()
        MsgBox("Inline File Updated!")
    End Sub

    Private Sub mnuMakeWkSamples_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMakeWkSamples.Click
        StartLoad = False
        DialogStart.ShowDialog()    'prompt for operator, fixID WO    
        If cbCCT.SelectedText.Contains(id_cct) = False Then         'need to load the correct bin after scan
            For i = 0 To cbCCT.Items.Count - 1
                If cbCCT.Items(i).ToString.Contains(id_cct) Then
                    cbCCT.SelectedIndex = i
                    Exit For
                End If
            Next
        End If
        Call OpenGoldSource()       'have user select gold file to read from              'read gold file to get LIS Data
        lblCorrection.Text = "Retrieving from Gold Sample"
        DialogGetDark.ShowDialog()  'get background for both blue and white.  measure blue and change int to white
        If AcqSuccess = False Then
            MsgBox("Routine Aborted due to failed measurement, re-check system and try again")
            Exit Sub
        End If
        Call ResetCorrs()               'reset corr factors before measuring gold samples
        Call GetGoldSamples()           'get SIS data for gold samples
        If AcqSuccess = False Then
            MsgBox("Routine Aborted due to failed measurement, re-check system and try again")
            Exit Sub
        End If
        Call UpdateGoldFile()           'add SIS data to gold file
        Call GetCorrelation()           'get correlation 
        Call GetCurrentWS(WS_Ref, 1)    'Set working standards 
        Call UpdateWS(0, WS_Ref)        'save reference working standards
    End Sub

    'Private Function mnuRelay() As Object
    '    Throw New NotImplementedException
    'End Function

End Class

