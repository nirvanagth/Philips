Imports System.Windows.Forms

Public Class DialogGetDark

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.Cursor = Cursors.WaitCursor
        bAutoSession = True
        If airConnect Then
            frmMain.ncd1.ProXR.RelayBanks.TurnOnRelay(rlyport)
            System.Threading.Thread.Sleep(1500)
            MsgBox("Please verify that the Light Engine is inserted into the port, if not tap the stage in the case it is stuck.  Click OK, when inserted.", vbOKOnly, "Port Insertion Confirmation")
        End If

        cmdOK.Enabled = False
        lbl1.Text = "Taking Dark For Blue and White Int. Times ......."
        lbl1.Visible = True
        Call frmMain.SetupDark(1)           'get dark
        lbl1.Text += "Done!"

        MsgBox("Turn on Power Supply and press OK when done.", vbOKOnly, "Blue Measurement")
        lbl2.Text = "Taking Blue Measurement 1 ......."
        lbl2.Visible = True
        AcqSuccess = True
        Application.DoEvents()

        frmMain.opRef.Checked = True                'go to blu integration time
        System.Threading.Thread.Sleep(int_wait)

        Call frmMain.DisplayBin()           'measure blue
        If (dMaxPixel < iBluePeakCnts) Or (dMaxPixel > 59000) Then
            MsgBox("Blue Peak Counts are out of Tolerance, Check Engine and re-try or contact Engineering")
            AcqSuccess = False
        End If

        If AcqSuccess = False Then
            Me.Close()
            Exit Sub
        End If
        lbl2.Text += "Done!"
        lbl3.Text = "Taking Blue Measurement 2 ......."
        lbl3.Visible = True
        Application.DoEvents()

        System.Threading.Thread.Sleep(int_wait)
        Call frmMain.DisplayBin()           'measure blue
        If AcqSuccess = False Then
            Me.Close()
            Exit Sub
        End If

        lbl3.Text += "Done!"
        lbl4.Text = "Changing integration time for white ......."
        lbl4.Visible = True
        Application.DoEvents()

        frmMain.opTest.Checked = True
        lbl4.Text += "Done!"

        If airConnect Then
            frmMain.ncd1.ProXR.RelayBanks.TurnOffRelay(rlyport)
        End If
        bAutoSession = False
        Me.Close()
    End Sub

    Private Sub DialogGetDark_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Cursor = Cursors.Default
        cmdOK.Enabled = True
    End Sub

    Private Sub DialogGetDark_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lbl1.Visible = False
        lbl2.Visible = False
        lbl3.Visible = False
        lbl4.Visible = False
    End Sub

End Class
