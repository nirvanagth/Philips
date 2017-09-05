<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblCE = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ShapeContainer2 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.ovalCE = New Microsoft.VisualBasic.PowerPacks.OvalShape()
        Me.ovalRa = New Microsoft.VisualBasic.PowerPacks.OvalShape()
        Me.ovalBin = New Microsoft.VisualBasic.PowerPacks.OvalShape()
        Me.txtCal_B_Dark = New System.Windows.Forms.TextBox()
        Me.txtCal_W_Dark = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.neInt_W = New System.Windows.Forms.NumericUpDown()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.neInt_B = New System.Windows.Forms.NumericUpDown()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.neScanAvg = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtOperator = New System.Windows.Forms.TextBox()
        Me.opTest = New System.Windows.Forms.RadioButton()
        Me.lblSDark = New System.Windows.Forms.Label()
        Me.opRef = New System.Windows.Forms.RadioButton()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.cmdClearData = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.openInLine = New System.Windows.Forms.OpenFileDialog()
        Me.lblFName = New System.Windows.Forms.Label()
        Me.openGSrc = New System.Windows.Forms.OpenFileDialog()
        Me.lblPBIN = New System.Windows.Forms.Label()
        Me.txtBIN = New System.Windows.Forms.TextBox()
        Me.lblIntReadBack = New System.Windows.Forms.Label()
        Me.lblCorrection = New System.Windows.Forms.Label()
        Me.lbSystem = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tmrScanReady = New System.Windows.Forms.Timer(Me.components)
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.cbCCT = New System.Windows.Forms.ComboBox()
        Me.cmdTakeDark = New System.Windows.Forms.Button()
        Me.grpType = New System.Windows.Forms.GroupBox()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.dgResults = New System.Windows.Forms.DataGridView()
        Me.colSample = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colLumen = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFlux = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCe = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCiex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCiey = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBin = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCCT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRa = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colR9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colResult = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDomWL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_Time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIntTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPeak = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Mult = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.offX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.offY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.offRa = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNotes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdCalculate = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbSpecVersion = New System.Windows.Forms.Label()
        Me.txtPeak1 = New System.Windows.Forms.TextBox()
        Me.mnuStartJob = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAutomation = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMakeWkSamples = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuILSamples = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuExport = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.txtLot = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ncd1 = New NCD.NCDComponent(Me.components)
        Me.ChtPlot = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.OvalShape2 = New Microsoft.VisualBasic.PowerPacks.OvalShape()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.lblSysReady = New System.Windows.Forms.Label()
        Me.gbIOff = New System.Windows.Forms.GroupBox()
        Me.neOffCRI = New System.Windows.Forms.NumericUpDown()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.neOffy = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.neoffCE = New System.Windows.Forms.NumericUpDown()
        Me.neOffx = New System.Windows.Forms.NumericUpDown()
        Me.ckSaveSpectrum = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.neInt_W, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.neInt_B, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.neScanAvg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.grpType.SuspendLayout()
        CType(Me.dgResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.ChtPlot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbIOff.SuspendLayout()
        CType(Me.neOffCRI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.neOffy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.neoffCE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.neOffx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.lblCE)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.ShapeContainer2)
        Me.GroupBox1.Location = New System.Drawing.Point(916, 292)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(251, 59)
        Me.GroupBox1.TabIndex = 103
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Results"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(92, 26)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(25, 13)
        Me.Label14.TabIndex = 25
        Me.Label14.Text = "CRI"
        '
        'lblCE
        '
        Me.lblCE.AutoSize = True
        Me.lblCE.Location = New System.Drawing.Point(173, 26)
        Me.lblCE.Name = "lblCE"
        Me.lblCE.Size = New System.Drawing.Size(21, 13)
        Me.lblCE.TabIndex = 25
        Me.lblCE.Text = "CE"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 25)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(22, 13)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "Bin"
        '
        'ShapeContainer2
        '
        Me.ShapeContainer2.Location = New System.Drawing.Point(3, 16)
        Me.ShapeContainer2.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer2.Name = "ShapeContainer2"
        Me.ShapeContainer2.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.ovalCE, Me.ovalRa, Me.ovalBin})
        Me.ShapeContainer2.Size = New System.Drawing.Size(245, 40)
        Me.ShapeContainer2.TabIndex = 26
        Me.ShapeContainer2.TabStop = False
        '
        'ovalCE
        '
        Me.ovalCE.FillColor = System.Drawing.SystemColors.Control
        Me.ovalCE.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid
        Me.ovalCE.Location = New System.Drawing.Point(205, 3)
        Me.ovalCE.Name = "ovalCE"
        Me.ovalCE.Size = New System.Drawing.Size(25, 27)
        '
        'ovalRa
        '
        Me.ovalRa.FillColor = System.Drawing.SystemColors.Control
        Me.ovalRa.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid
        Me.ovalRa.Location = New System.Drawing.Point(128, 6)
        Me.ovalRa.Name = "ovalRa"
        Me.ovalRa.Size = New System.Drawing.Size(23, 24)
        '
        'ovalBin
        '
        Me.ovalBin.FillColor = System.Drawing.SystemColors.Control
        Me.ovalBin.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid
        Me.ovalBin.Location = New System.Drawing.Point(45, 4)
        Me.ovalBin.Name = "ovalBin"
        Me.ovalBin.Size = New System.Drawing.Size(23, 24)
        '
        'txtCal_B_Dark
        '
        Me.txtCal_B_Dark.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCal_B_Dark.Location = New System.Drawing.Point(163, 28)
        Me.txtCal_B_Dark.Name = "txtCal_B_Dark"
        Me.txtCal_B_Dark.Size = New System.Drawing.Size(43, 20)
        Me.txtCal_B_Dark.TabIndex = 17
        '
        'txtCal_W_Dark
        '
        Me.txtCal_W_Dark.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCal_W_Dark.Location = New System.Drawing.Point(357, 28)
        Me.txtCal_W_Dark.Name = "txtCal_W_Dark"
        Me.txtCal_W_Dark.Size = New System.Drawing.Size(49, 20)
        Me.txtCal_W_Dark.TabIndex = 17
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtCal_W_Dark)
        Me.GroupBox3.Controls.Add(Me.neInt_W)
        Me.GroupBox3.Controls.Add(Me.txtCal_B_Dark)
        Me.GroupBox3.Controls.Add(Me.Label44)
        Me.GroupBox3.Controls.Add(Me.neInt_B)
        Me.GroupBox3.Controls.Add(Me.Label43)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.neScanAvg)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Location = New System.Drawing.Point(204, 346)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(517, 55)
        Me.GroupBox3.TabIndex = 104
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Spectrum Acquisition"
        '
        'neInt_W
        '
        Me.neInt_W.DecimalPlaces = 2
        Me.neInt_W.Location = New System.Drawing.Point(286, 27)
        Me.neInt_W.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.neInt_W.Minimum = New Decimal(New Integer() {4, 0, 0, 0})
        Me.neInt_W.Name = "neInt_W"
        Me.neInt_W.Size = New System.Drawing.Size(65, 20)
        Me.neInt_W.TabIndex = 53
        Me.neInt_W.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(354, 11)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(80, 13)
        Me.Label44.TabIndex = 21
        Me.Label44.Text = "Wht_Save (ms)"
        '
        'neInt_B
        '
        Me.neInt_B.DecimalPlaces = 2
        Me.neInt_B.Location = New System.Drawing.Point(78, 28)
        Me.neInt_B.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.neInt_B.Minimum = New Decimal(New Integer() {4, 0, 0, 0})
        Me.neInt_B.Name = "neInt_B"
        Me.neInt_B.Size = New System.Drawing.Size(68, 20)
        Me.neInt_B.TabIndex = 53
        Me.neInt_B.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(212, 30)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(68, 13)
        Me.Label43.TabIndex = 21
        Me.Label43.Text = "Wht Int Time"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(160, 8)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(75, 13)
        Me.Label10.TabIndex = 21
        Me.Label10.Text = "Blu_Save (ms)"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(9, 31)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(63, 13)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Blu Int Time"
        '
        'neScanAvg
        '
        Me.neScanAvg.Location = New System.Drawing.Point(455, 27)
        Me.neScanAvg.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.neScanAvg.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.neScanAvg.Name = "neScanAvg"
        Me.neScanAvg.Size = New System.Drawing.Size(41, 20)
        Me.neScanAvg.TabIndex = 53
        Me.neScanAvg.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(412, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Scans"
        '
        'txtOperator
        '
        Me.txtOperator.Location = New System.Drawing.Point(804, 107)
        Me.txtOperator.Name = "txtOperator"
        Me.txtOperator.Size = New System.Drawing.Size(99, 20)
        Me.txtOperator.TabIndex = 70
        '
        'opTest
        '
        Me.opTest.AutoSize = True
        Me.opTest.Location = New System.Drawing.Point(93, 19)
        Me.opTest.Name = "opTest"
        Me.opTest.Size = New System.Drawing.Size(46, 17)
        Me.opTest.TabIndex = 0
        Me.opTest.TabStop = True
        Me.opTest.Text = "Test"
        Me.opTest.UseVisualStyleBackColor = True
        '
        'lblSDark
        '
        Me.lblSDark.AutoSize = True
        Me.lblSDark.ForeColor = System.Drawing.Color.Red
        Me.lblSDark.Location = New System.Drawing.Point(788, 343)
        Me.lblSDark.Name = "lblSDark"
        Me.lblSDark.Size = New System.Drawing.Size(71, 13)
        Me.lblSDark.TabIndex = 78
        Me.lblSDark.Text = "Dark Expired!"
        '
        'opRef
        '
        Me.opRef.AutoSize = True
        Me.opRef.Location = New System.Drawing.Point(6, 19)
        Me.opRef.Name = "opRef"
        Me.opRef.Size = New System.Drawing.Size(72, 17)
        Me.opRef.TabIndex = 0
        Me.opRef.TabStop = True
        Me.opRef.Text = "Blue (Ref)"
        Me.opRef.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(908, 54)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(188, 34)
        Me.PictureBox1.TabIndex = 99
        Me.PictureBox1.TabStop = False
        '
        'cmdClearData
        '
        Me.cmdClearData.Location = New System.Drawing.Point(804, 306)
        Me.cmdClearData.Name = "cmdClearData"
        Me.cmdClearData.Size = New System.Drawing.Size(87, 27)
        Me.cmdClearData.TabIndex = 88
        Me.cmdClearData.Text = "Clear Data"
        Me.cmdClearData.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.opTest)
        Me.GroupBox2.Controls.Add(Me.opRef)
        Me.GroupBox2.Location = New System.Drawing.Point(916, 355)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(153, 46)
        Me.GroupBox2.TabIndex = 100
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Scan Selection"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(801, 91)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 13)
        Me.Label6.TabIndex = 82
        Me.Label6.Text = "Operator"
        '
        'openInLine
        '
        Me.openInLine.InitialDirectory = "C:\MyDocuments"
        '
        'lblFName
        '
        Me.lblFName.AutoSize = True
        Me.lblFName.ForeColor = System.Drawing.Color.Red
        Me.lblFName.Location = New System.Drawing.Point(820, 27)
        Me.lblFName.Name = "lblFName"
        Me.lblFName.Size = New System.Drawing.Size(94, 13)
        Me.lblFName.TabIndex = 79
        Me.lblFName.Text = "File Save Notice..."
        '
        'openGSrc
        '
        Me.openGSrc.InitialDirectory = "C:\MyDocuments"
        '
        'lblPBIN
        '
        Me.lblPBIN.AutoSize = True
        Me.lblPBIN.BackColor = System.Drawing.Color.White
        Me.lblPBIN.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPBIN.ForeColor = System.Drawing.Color.Red
        Me.lblPBIN.Location = New System.Drawing.Point(677, 181)
        Me.lblPBIN.Name = "lblPBIN"
        Me.lblPBIN.Size = New System.Drawing.Size(100, 20)
        Me.lblPBIN.TabIndex = 83
        Me.lblPBIN.Text = "Phillips BIN"
        '
        'txtBIN
        '
        Me.txtBIN.BackColor = System.Drawing.Color.White
        Me.txtBIN.Enabled = False
        Me.txtBIN.Font = New System.Drawing.Font("Microsoft Sans Serif", 75.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBIN.ForeColor = System.Drawing.Color.Black
        Me.txtBIN.Location = New System.Drawing.Point(681, 212)
        Me.txtBIN.Name = "txtBIN"
        Me.txtBIN.ReadOnly = True
        Me.txtBIN.Size = New System.Drawing.Size(91, 122)
        Me.txtBIN.TabIndex = 68
        Me.txtBIN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblIntReadBack
        '
        Me.lblIntReadBack.AutoSize = True
        Me.lblIntReadBack.ForeColor = System.Drawing.Color.Red
        Me.lblIntReadBack.Location = New System.Drawing.Point(500, 782)
        Me.lblIntReadBack.Name = "lblIntReadBack"
        Me.lblIntReadBack.Size = New System.Drawing.Size(88, 13)
        Me.lblIntReadBack.TabIndex = 74
        Me.lblIntReadBack.Text = "Int Read Back = "
        '
        'lblCorrection
        '
        Me.lblCorrection.AutoSize = True
        Me.lblCorrection.ForeColor = System.Drawing.Color.Red
        Me.lblCorrection.Location = New System.Drawing.Point(9, 782)
        Me.lblCorrection.Name = "lblCorrection"
        Me.lblCorrection.Size = New System.Drawing.Size(116, 13)
        Me.lblCorrection.TabIndex = 73
        Me.lblCorrection.Text = "Correlation Not Loaded"
        '
        'lbSystem
        '
        Me.lbSystem.AutoSize = True
        Me.lbSystem.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbSystem.ForeColor = System.Drawing.Color.Red
        Me.lbSystem.Location = New System.Drawing.Point(801, 64)
        Me.lbSystem.Name = "lbSystem"
        Me.lbSystem.Size = New System.Drawing.Size(33, 24)
        Me.lbSystem.TabIndex = 80
        Me.lbSystem.Text = "AL"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(175, 27)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(0, 13)
        Me.Label5.TabIndex = 86
        '
        'tmrScanReady
        '
        '
        'cmdClear
        '
        Me.cmdClear.Location = New System.Drawing.Point(1054, 782)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(83, 37)
        Me.cmdClear.TabIndex = 92
        Me.cmdClear.Text = "Clear Scans"
        Me.cmdClear.UseVisualStyleBackColor = True
        '
        'cbCCT
        '
        Me.cbCCT.FormattingEnabled = True
        Me.cbCCT.Location = New System.Drawing.Point(52, 15)
        Me.cbCCT.Name = "cbCCT"
        Me.cbCCT.Size = New System.Drawing.Size(75, 21)
        Me.cbCCT.TabIndex = 0
        '
        'cmdTakeDark
        '
        Me.cmdTakeDark.Location = New System.Drawing.Point(804, 365)
        Me.cmdTakeDark.Name = "cmdTakeDark"
        Me.cmdTakeDark.Size = New System.Drawing.Size(87, 27)
        Me.cmdTakeDark.TabIndex = 91
        Me.cmdTakeDark.Text = "Update Dark"
        Me.cmdTakeDark.UseVisualStyleBackColor = True
        '
        'grpType
        '
        Me.grpType.Controls.Add(Me.cbCCT)
        Me.grpType.Controls.Add(Me.Label49)
        Me.grpType.Location = New System.Drawing.Point(915, 145)
        Me.grpType.Name = "grpType"
        Me.grpType.Size = New System.Drawing.Size(147, 48)
        Me.grpType.TabIndex = 96
        Me.grpType.TabStop = False
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(18, 20)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(28, 13)
        Me.Label49.TabIndex = 21
        Me.Label49.Text = "CCT"
        '
        'dgResults
        '
        Me.dgResults.AllowUserToAddRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgResults.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgResults.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSample, Me.colLumen, Me.colFlux, Me.colCe, Me.colCiex, Me.colCiey, Me.colBin, Me.colCCT, Me.colRa, Me.colR9, Me.colResult, Me.colDomWL, Me.col_Time, Me.colIntTime, Me.colPeak, Me.Mult, Me.offX, Me.offY, Me.offRa, Me.colNotes})
        Me.dgResults.Cursor = System.Windows.Forms.Cursors.Arrow
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgResults.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgResults.Location = New System.Drawing.Point(12, 407)
        Me.dgResults.Name = "dgResults"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgResults.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgResults.ShowCellToolTips = False
        Me.dgResults.Size = New System.Drawing.Size(1221, 359)
        Me.dgResults.TabIndex = 97
        '
        'colSample
        '
        Me.colSample.HeaderText = "Sample"
        Me.colSample.Name = "colSample"
        Me.colSample.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'colLumen
        '
        Me.colLumen.HeaderText = "Lumens"
        Me.colLumen.Name = "colLumen"
        Me.colLumen.ReadOnly = True
        Me.colLumen.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colLumen.Width = 50
        '
        'colFlux
        '
        Me.colFlux.HeaderText = "Watts"
        Me.colFlux.Name = "colFlux"
        Me.colFlux.ReadOnly = True
        Me.colFlux.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colFlux.Width = 50
        '
        'colCe
        '
        Me.colCe.HeaderText = "CE"
        Me.colCe.Name = "colCe"
        Me.colCe.ReadOnly = True
        Me.colCe.Width = 50
        '
        'colCiex
        '
        Me.colCiex.HeaderText = "Cie x"
        Me.colCiex.Name = "colCiex"
        Me.colCiex.ReadOnly = True
        Me.colCiex.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colCiex.Width = 70
        '
        'colCiey
        '
        Me.colCiey.HeaderText = "Cie y"
        Me.colCiey.Name = "colCiey"
        Me.colCiey.ReadOnly = True
        Me.colCiey.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colCiey.Width = 70
        '
        'colBin
        '
        Me.colBin.HeaderText = "Bin"
        Me.colBin.Name = "colBin"
        Me.colBin.ReadOnly = True
        Me.colBin.Width = 50
        '
        'colCCT
        '
        Me.colCCT.HeaderText = "CCT"
        Me.colCCT.Name = "colCCT"
        Me.colCCT.ReadOnly = True
        Me.colCCT.Width = 50
        '
        'colRa
        '
        Me.colRa.HeaderText = "Ra"
        Me.colRa.Name = "colRa"
        Me.colRa.ReadOnly = True
        Me.colRa.Width = 50
        '
        'colR9
        '
        Me.colR9.HeaderText = "R9"
        Me.colR9.Name = "colR9"
        Me.colR9.ReadOnly = True
        Me.colR9.Width = 50
        '
        'colResult
        '
        Me.colResult.HeaderText = "Res."
        Me.colResult.Name = "colResult"
        Me.colResult.ReadOnly = True
        Me.colResult.Width = 30
        '
        'colDomWL
        '
        Me.colDomWL.HeaderText = "DomWL"
        Me.colDomWL.Name = "colDomWL"
        Me.colDomWL.ReadOnly = True
        Me.colDomWL.Width = 50
        '
        'col_Time
        '
        Me.col_Time.HeaderText = "TimeStamp"
        Me.col_Time.Name = "col_Time"
        Me.col_Time.ReadOnly = True
        Me.col_Time.Width = 125
        '
        'colIntTime
        '
        Me.colIntTime.HeaderText = "Int"
        Me.colIntTime.Name = "colIntTime"
        Me.colIntTime.ReadOnly = True
        Me.colIntTime.Width = 50
        '
        'colPeak
        '
        Me.colPeak.HeaderText = "Peak"
        Me.colPeak.Name = "colPeak"
        Me.colPeak.ReadOnly = True
        Me.colPeak.Width = 50
        '
        'Mult
        '
        Me.Mult.HeaderText = "Mult"
        Me.Mult.Name = "Mult"
        Me.Mult.Width = 70
        '
        'offX
        '
        Me.offX.HeaderText = "offX"
        Me.offX.Name = "offX"
        Me.offX.Width = 70
        '
        'offY
        '
        Me.offY.HeaderText = "offY"
        Me.offY.Name = "offY"
        Me.offY.Width = 70
        '
        'offRa
        '
        Me.offRa.HeaderText = "offRa"
        Me.offRa.Name = "offRa"
        Me.offRa.Width = 70
        '
        'colNotes
        '
        Me.colNotes.HeaderText = "Notes"
        Me.colNotes.Name = "colNotes"
        '
        'cmdCalculate
        '
        Me.cmdCalculate.Location = New System.Drawing.Point(804, 166)
        Me.cmdCalculate.Name = "cmdCalculate"
        Me.cmdCalculate.Size = New System.Drawing.Size(87, 27)
        Me.cmdCalculate.TabIndex = 89
        Me.cmdCalculate.Text = "Acquire"
        Me.cmdCalculate.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 378)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 76
        Me.Label2.Text = "Peak Intensity"
        '
        'lbSpecVersion
        '
        Me.lbSpecVersion.AutoSize = True
        Me.lbSpecVersion.Location = New System.Drawing.Point(438, 34)
        Me.lbSpecVersion.Name = "lbSpecVersion"
        Me.lbSpecVersion.Size = New System.Drawing.Size(37, 13)
        Me.lbSpecVersion.TabIndex = 87
        Me.lbSpecVersion.Text = "Tester"
        '
        'txtPeak1
        '
        Me.txtPeak1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtPeak1.Location = New System.Drawing.Point(106, 375)
        Me.txtPeak1.Name = "txtPeak1"
        Me.txtPeak1.ReadOnly = True
        Me.txtPeak1.Size = New System.Drawing.Size(71, 20)
        Me.txtPeak1.TabIndex = 72
        '
        'mnuStartJob
        '
        Me.mnuStartJob.Name = "mnuStartJob"
        Me.mnuStartJob.Size = New System.Drawing.Size(203, 22)
        Me.mnuStartJob.Text = "Start New Job"
        '
        'mnuExit
        '
        Me.mnuExit.Name = "mnuExit"
        Me.mnuExit.Size = New System.Drawing.Size(140, 22)
        Me.mnuExit.Text = "Exit"
        '
        'mnuAutomation
        '
        Me.mnuAutomation.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuStartJob, Me.mnuMakeWkSamples, Me.mnuILSamples})
        Me.mnuAutomation.Name = "mnuAutomation"
        Me.mnuAutomation.Size = New System.Drawing.Size(83, 20)
        Me.mnuAutomation.Text = "Automation"
        '
        'mnuMakeWkSamples
        '
        Me.mnuMakeWkSamples.Name = "mnuMakeWkSamples"
        Me.mnuMakeWkSamples.Size = New System.Drawing.Size(203, 22)
        Me.mnuMakeWkSamples.Text = "Create Working Samples"
        '
        'mnuILSamples
        '
        Me.mnuILSamples.Name = "mnuILSamples"
        Me.mnuILSamples.Size = New System.Drawing.Size(203, 22)
        Me.mnuILSamples.Text = "In-Line Samples"
        '
        'mnuAbout
        '
        Me.mnuAbout.Name = "mnuAbout"
        Me.mnuAbout.Size = New System.Drawing.Size(52, 20)
        Me.mnuAbout.Text = "About"
        '
        'mnuExport
        '
        Me.mnuExport.Name = "mnuExport"
        Me.mnuExport.Size = New System.Drawing.Size(140, 22)
        Me.mnuExport.Text = "Export Scans"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuExport, Me.mnuExit})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.mnuAutomation, Me.mnuAbout})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1296, 24)
        Me.MenuStrip1.TabIndex = 98
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.Color.Red
        Me.cmdExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.Location = New System.Drawing.Point(1150, 782)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(83, 37)
        Me.cmdExit.TabIndex = 90
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'txtLot
        '
        Me.txtLot.Location = New System.Drawing.Point(916, 107)
        Me.txtLot.Name = "txtLot"
        Me.txtLot.Size = New System.Drawing.Size(175, 20)
        Me.txtLot.TabIndex = 69
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(913, 91)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 13)
        Me.Label9.TabIndex = 81
        Me.Label9.Text = "Job Description"
        '
        'ncd1
        '
        Me.ncd1.BaudRate = 115200
        Me.ncd1.IPAddress = "192.168.0.104"
        Me.ncd1.IsTwoWay = True
        Me.ncd1.Port = 2101
        Me.ncd1.PortName = "COM1"
        Me.ncd1.ReadTimeOut = 100
        Me.ncd1.UsingComPort = True
        '
        'ChtPlot
        '
        ChartArea1.Name = "ChartArea1"
        Me.ChtPlot.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.ChtPlot.Legends.Add(Legend1)
        Me.ChtPlot.Location = New System.Drawing.Point(64, 50)
        Me.ChtPlot.Name = "ChtPlot"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.ChtPlot.Series.Add(Series1)
        Me.ChtPlot.Size = New System.Drawing.Size(731, 286)
        Me.ChtPlot.TabIndex = 106
        '
        'OvalShape2
        '
        Me.OvalShape2.Location = New System.Drawing.Point(611, 359)
        Me.OvalShape2.Name = "OvalShape2"
        Me.OvalShape2.Size = New System.Drawing.Size(23, 24)
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.OvalShape2})
        Me.ShapeContainer1.Size = New System.Drawing.Size(1296, 819)
        Me.ShapeContainer1.TabIndex = 107
        Me.ShapeContainer1.TabStop = False
        '
        'lblSysReady
        '
        Me.lblSysReady.AutoSize = True
        Me.lblSysReady.BackColor = System.Drawing.Color.White
        Me.lblSysReady.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSysReady.ForeColor = System.Drawing.Color.Red
        Me.lblSysReady.Location = New System.Drawing.Point(290, 64)
        Me.lblSysReady.Name = "lblSysReady"
        Me.lblSysReady.Size = New System.Drawing.Size(320, 29)
        Me.lblSysReady.TabIndex = 79
        Me.lblSysReady.Text = "SYSTEM IS NOT READY!!"
        '
        'gbIOff
        '
        Me.gbIOff.Controls.Add(Me.neOffCRI)
        Me.gbIOff.Controls.Add(Me.Label12)
        Me.gbIOff.Controls.Add(Me.neOffy)
        Me.gbIOff.Controls.Add(Me.Label4)
        Me.gbIOff.Controls.Add(Me.Label11)
        Me.gbIOff.Controls.Add(Me.Label3)
        Me.gbIOff.Controls.Add(Me.neoffCE)
        Me.gbIOff.Controls.Add(Me.neOffx)
        Me.gbIOff.Location = New System.Drawing.Point(804, 224)
        Me.gbIOff.Name = "gbIOff"
        Me.gbIOff.Size = New System.Drawing.Size(402, 48)
        Me.gbIOff.TabIndex = 96
        Me.gbIOff.TabStop = False
        Me.gbIOff.Text = "Intrinsic Offsets"
        '
        'neOffCRI
        '
        Me.neOffCRI.DecimalPlaces = 1
        Me.neOffCRI.Increment = New Decimal(New Integer() {1, 0, 0, 262144})
        Me.neOffCRI.Location = New System.Drawing.Point(330, 21)
        Me.neOffCRI.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.neOffCRI.Minimum = New Decimal(New Integer() {1, 0, 0, -2147483648})
        Me.neOffCRI.Name = "neOffCRI"
        Me.neOffCRI.Size = New System.Drawing.Size(46, 20)
        Me.neOffCRI.TabIndex = 53
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(299, 25)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(25, 13)
        Me.Label12.TabIndex = 21
        Me.Label12.Text = "CRI"
        '
        'neOffy
        '
        Me.neOffy.DecimalPlaces = 4
        Me.neOffy.Increment = New Decimal(New Integer() {1, 0, 0, 262144})
        Me.neOffy.Location = New System.Drawing.Point(223, 21)
        Me.neOffy.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.neOffy.Minimum = New Decimal(New Integer() {1, 0, 0, -2147483648})
        Me.neOffy.Name = "neOffy"
        Me.neOffy.Size = New System.Drawing.Size(65, 20)
        Me.neOffy.TabIndex = 53
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(205, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(12, 13)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "y"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(17, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(21, 13)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "CE"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(108, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(12, 13)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "x"
        '
        'neoffCE
        '
        Me.neoffCE.DecimalPlaces = 1
        Me.neoffCE.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.neoffCE.Location = New System.Drawing.Point(43, 21)
        Me.neoffCE.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.neoffCE.Name = "neoffCE"
        Me.neoffCE.Size = New System.Drawing.Size(44, 20)
        Me.neoffCE.TabIndex = 53
        '
        'neOffx
        '
        Me.neOffx.DecimalPlaces = 4
        Me.neOffx.Increment = New Decimal(New Integer() {1, 0, 0, 262144})
        Me.neOffx.Location = New System.Drawing.Point(125, 21)
        Me.neOffx.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.neOffx.Minimum = New Decimal(New Integer() {1, 0, 0, -2147483648})
        Me.neOffx.Name = "neOffx"
        Me.neOffx.Size = New System.Drawing.Size(65, 20)
        Me.neOffx.TabIndex = 53
        '
        'ckSaveSpectrum
        '
        Me.ckSaveSpectrum.AutoSize = True
        Me.ckSaveSpectrum.Location = New System.Drawing.Point(919, 204)
        Me.ckSaveSpectrum.Name = "ckSaveSpectrum"
        Me.ckSaveSpectrum.Size = New System.Drawing.Size(99, 17)
        Me.ckSaveSpectrum.TabIndex = 108
        Me.ckSaveSpectrum.Text = "Save Spectrum"
        Me.ckSaveSpectrum.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1313, 742)
        Me.Controls.Add(Me.ckSaveSpectrum)
        Me.Controls.Add(Me.gbIOff)
        Me.Controls.Add(Me.lblSysReady)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.txtOperator)
        Me.Controls.Add(Me.lblSDark)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.cmdClearData)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lblFName)
        Me.Controls.Add(Me.lblPBIN)
        Me.Controls.Add(Me.txtBIN)
        Me.Controls.Add(Me.lblIntReadBack)
        Me.Controls.Add(Me.lblCorrection)
        Me.Controls.Add(Me.lbSystem)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.cmdTakeDark)
        Me.Controls.Add(Me.grpType)
        Me.Controls.Add(Me.dgResults)
        Me.Controls.Add(Me.cmdCalculate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lbSpecVersion)
        Me.Controls.Add(Me.txtPeak1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.txtLot)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.ChtPlot)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Main"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.neInt_W, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.neInt_B, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.neScanAvg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.grpType.ResumeLayout(False)
        Me.grpType.PerformLayout()
        CType(Me.dgResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.ChtPlot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbIOff.ResumeLayout(False)
        Me.gbIOff.PerformLayout()
        CType(Me.neOffCRI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.neOffy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.neoffCE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.neOffx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblCE As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCal_B_Dark As System.Windows.Forms.TextBox
    Friend WithEvents txtCal_W_Dark As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents neInt_W As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents neInt_B As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents neScanAvg As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtOperator As System.Windows.Forms.TextBox
    Friend WithEvents opTest As System.Windows.Forms.RadioButton
    Friend WithEvents lblSDark As System.Windows.Forms.Label
    Friend WithEvents opRef As System.Windows.Forms.RadioButton
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents cmdClearData As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents openInLine As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblFName As System.Windows.Forms.Label
    Friend WithEvents openGSrc As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblPBIN As System.Windows.Forms.Label
    Friend WithEvents txtBIN As System.Windows.Forms.TextBox
    Friend WithEvents lblIntReadBack As System.Windows.Forms.Label
    Friend WithEvents lblCorrection As System.Windows.Forms.Label
    Friend WithEvents lbSystem As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tmrScanReady As System.Windows.Forms.Timer
    Friend WithEvents cmdClear As System.Windows.Forms.Button
    Friend WithEvents cbCCT As System.Windows.Forms.ComboBox
    Friend WithEvents cmdTakeDark As System.Windows.Forms.Button
    Friend WithEvents grpType As System.Windows.Forms.GroupBox
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents dgResults As System.Windows.Forms.DataGridView
    Friend WithEvents cmdCalculate As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbSpecVersion As System.Windows.Forms.Label
    Friend WithEvents txtPeak1 As System.Windows.Forms.TextBox
    Friend WithEvents mnuStartJob As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAutomation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuExport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents txtLot As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents ncd1 As NCD.NCDComponent
    Friend WithEvents ChtPlot As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents mnuILSamples As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShapeContainer2 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents ovalCE As Microsoft.VisualBasic.PowerPacks.OvalShape
    Friend WithEvents ovalRa As Microsoft.VisualBasic.PowerPacks.OvalShape
    Friend WithEvents ovalBin As Microsoft.VisualBasic.PowerPacks.OvalShape
    Friend WithEvents OvalShape2 As Microsoft.VisualBasic.PowerPacks.OvalShape
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents mnuMakeWkSamples As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblSysReady As System.Windows.Forms.Label
    Friend WithEvents gbIOff As System.Windows.Forms.GroupBox
    Friend WithEvents neOffy As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents neOffx As System.Windows.Forms.NumericUpDown
    Friend WithEvents neOffCRI As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents neoffCE As System.Windows.Forms.NumericUpDown
    Friend WithEvents colSample As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLumen As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFlux As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCe As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCiex As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCiey As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBin As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCCT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRa As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colR9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colResult As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDomWL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIntTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPeak As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Mult As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents offX As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents offY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents offRa As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNotes As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ckSaveSpectrum As System.Windows.Forms.CheckBox

End Class
