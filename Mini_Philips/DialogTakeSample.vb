Imports System.Windows.Forms

Public Class DialogTakeSample

    Private Sub DialogTakeSample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmdok.Enabled = False
        tmrOK.Interval = 500
        tmrOK.Enabled = True
        txtSID.Text = ""
        If sampledesc.Contains("Enter") Then
            lblSID.Visible = True
            txtSID.Visible = True
        Else
            lblSID.Visible = False
            txtSID.Visible = False
        End If
        cmdNoDisk.Enabled = sampledesc.Contains("Disk")
        lblTitle.Text = sampledesc

    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Dim imsg As Integer

        AcqSuccess = True
        If txtSID.Visible And txtSID.Text = "" Then
            MsgBox("Please Enter Sample Set ID.")
            Exit Sub
        ElseIf txtSID.Visible Then          'this is for measuring the first working sample
            sampleSetID = txtSID.Text
            SID = sampleSetID & "-" & wknum
            If airConnect Then
                frmMain.ncd1.ProXR.RelayBanks.TurnOnRelay(rlyport)
                System.Threading.Thread.Sleep(2000)
                MsgBox("Please verify that the Light Engine is inserted into the port, if not tap the stage in the case it is stuck.  Click OK, when inserted.", vbOKOnly, "Port Insertion Confirmation")
            End If
            Call frmMain.DisplayBin()       'measure the part
            If airConnect Then
                frmMain.ncd1.ProXR.RelayBanks.TurnOffRelay(rlyport)
            End If
            imsg = MsgBox("Please confirm that this is the sample description to be written on the sample bag:  " & txtSID.Text, vbOKOnly, "Sample Description Verification")
        ElseIf DateDiff(DateInterval.Minute, darkTime, Now) >= iDarkExpire Then
            MsgBox("Dark Has Expired, You will now be prompted to re-do Dark and Blue.")
            Call DialogGetDark.ShowDialog()
            MsgBox("Re-install Sample and click OK when complete.")
            If airConnect Then
                frmMain.ncd1.ProXR.RelayBanks.TurnOnRelay(rlyport)
                System.Threading.Thread.Sleep(2000)
                MsgBox("Please verify that the Light Engine is inserted into the port, if not tap the stage in the case it is stuck.  Click OK, when inserted.", vbOKOnly, "Port Insertion Confirmation")
            End If
            Call frmMain.DisplayBin()
            If airConnect Then
                frmMain.ncd1.ProXR.RelayBanks.TurnOffRelay(rlyport)
            End If
        Else
            If Not goldWS And Not (sampledesc.Contains("Pos")) Then     'if sample description has pos then SID is assigned (in-line)
                SID = sampleSetID & "-" & wknum
            End If
            If airConnect Then
                frmMain.ncd1.ProXR.RelayBanks.TurnOnRelay(rlyport)
                System.Threading.Thread.Sleep(2000)
                MsgBox("Please verify that the Light Engine is inserted into the port, if not tap the stage in the case it is stuck.  Click OK, when inserted.", vbOKOnly, "Port Insertion Confirmation")
            End If
            Call frmMain.DisplayBin()
            If airConnect Then
                frmMain.ncd1.ProXR.RelayBanks.TurnOffRelay(rlyport)
            End If
        End If
        Me.DialogResult = vbOK
        Me.Close()

    End Sub

    Private Sub tmrOK_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrOK.Tick
        If frmMain.cmdCalculate.Enabled Then
            tmrOK.Enabled = False
            cmdok.Enabled = True
        End If
    End Sub

    Private Sub cmdNoDisk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNoDisk.Click
        Me.DialogResult = Windows.Forms.DialogResult.Ignore
        Me.Close()
    End Sub
End Class
