Imports System.Windows.Forms

Public Class DialogStart

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        If txtOperator.Text.Length = 0 Then
            MsgBox("Please Enter operator name.")
            Exit Sub
        ElseIf txtWO.Text.Length = 0 Then
            MsgBox("Please Scan Traveller")
            Exit Sub
        End If

        Call BarDecode(txtWO.Text)
        frmMain.txtOperator.Text = txtOperator.Text
        frmMain.txtLot.Text = id_wo
        BaseFN = "CLC-PLF-" & id_fn
        dBoxFactory += "\" & BaseFN
        Me.Close()
    End Sub

    Public Sub BarDecode(ByVal barscan As String)
       
        id_wo = barscan.Substring(1, 4)
        id_ptype = barscan.Substring(10, 1)
        id_cri = CInt(barscan.Substring(6, 2))
        id_cct = CInt(barscan.Substring(8, 2)) * 100
        BinType = id_cct & "K"
        id_fn = (id_cri / 10) & (id_cct / 100)
    End Sub

End Class

