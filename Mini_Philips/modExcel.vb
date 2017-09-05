Imports Microsoft.Office.Interop.Excel

Module modExcel

    'Excel Workbook items
    Public Excel1 As Microsoft.Office.Interop.Excel.Application
    Public wBook As Microsoft.Office.Interop.Excel.Workbook
    Public wSheet As Microsoft.Office.Interop.Excel.Worksheet
    Public oRange As Microsoft.Office.Interop.Excel.Range
    Public oC As Microsoft.Office.Interop.Excel.Range

    'File names
    Public FilePath As String = My.Application.Info.DirectoryPath
    Public PathAndFileName As String = FilePath & "\ExportData\Mini"
    Public DataTemplateFile As String = FilePath & "\Mini_Template.xlsx"
    Public Const FileExtension = ".xlsx"
    Public saveFN As String

    Public Function OpenExcelFile() As Integer
        '*****************************************************
        '   Procedure:  OpenExcelFile
        '   Purpose:    Open excel template file and saves it as another
        '               Will open the targeted file.
        '   Inputs:     None
        '   Outputs:    None
        '   Author:     HP
        '   Rev Hist:   8/28/08, Original
        '
        '
        '******************************************************
        Dim sTemp As String = ""
        Dim sDate As String

        sDate = Format(Now.Month, "00") & Format(Now.Day, "00") & Now.Year.ToString.Substring(2) & "_" & Format(Now.Hour, "00") & Format(Now.Minute, "00") & Format(Now.Second, "00")

        ' Set filename of new excel file
        sTemp = PathAndFileName & " " & frmMain.txtLot.Text & "_" & sDate & FileExtension         'in exports folder
        saveFN = frmMain.txtLot.Text & "_" & sDate & FileExtension

        'Create new object
        Excel1 = New Microsoft.Office.Interop.Excel.Application()
        'Excel1.ErrorCheckingOptions.NumberAsText = False
        wBook = Excel1.Workbooks.Add()
        wSheet = wBook.ActiveSheet()

        wBook = Excel1.Workbooks.Open(DataTemplateFile)     'open template
        wSheet = wBook.Worksheets.Item("Raw Data")
        wBook.SaveAs(sTemp)                                 'save new file
        wBook.Close()
        releaseObject(wBook)
        releaseObject(wSheet)                               'release the template
        GC.Collect()

        wBook = Excel1.Workbooks.Add()
        wSheet = wBook.ActiveSheet()
        wBook = Excel1.Workbooks.Open(sTemp)                'open file for save
        wSheet = wBook.Worksheets.Item("Raw Data")
        Return 1

    End Function

    Public Sub SaveAndCloseExcelFile()
        '*****************************************************
        '   Procedure:  SaveAndCloseExcelFile
        '   Purpose:    Saves open excel file and releases process from windows
        '   Inputs:     None
        '   Outputs:    None
        '   Author:     HP
        '   Rev Hist:   8/28/08, Original
        '
        '
        '******************************************************

        ' saves the current work book
        Excel1.ActiveWorkbook.Save()
        releaseObject(wSheet)           'releases object from windows
        wBook.Close(False)
        releaseObject(wBook)
        Excel1.Quit()
        releaseObject(Excel1)
        GC.Collect()


    End Sub

    Public Sub releaseObject(ByVal obj As Object)
        '*****************************************************
        '   Procedure:  releaseObject
        '   Purpose:    Releases process from windows
        '   Inputs:     obj - process to be removed
        '   Outputs:    None
        '   Author:     HP
        '   Rev Hist:   8/28/08, Original
        '
        '
        '******************************************************
        Try
            While System.Runtime.InteropServices.Marshal.ReleaseComObject(obj) > 0

            End While
            obj = Nothing
        Catch ex As Exception
            obj = Nothing

        End Try
    End Sub

End Module






