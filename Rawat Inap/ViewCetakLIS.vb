Imports MySql.Data.MySqlClient
Imports Microsoft.Reporting.WinForms
Imports System.IO
Public Class ViewCetakLIS

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Private Sub ViewCetakLIS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksiLis()

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "CetakLis"
                    Dim PID, LNO As String
                    PID = DaftarHasilLab.txtPID.Text
                    LNO = DaftarHasilLab.txtLNO.Text

                    'MsgBox(PID & " - " & LNO, MsgBoxStyle.Information, "VIEWHASILLIS")

                    Dim dt As New DataTable
                    da = New MySqlDataAdapter("SELECT * FROM vw_lis WHERE PID = '" & PID & "' AND LNO = '" & LNO & "'", conn)
                    ds = New DataSet
                    da.Fill(dt)
                    ReportViewer1.LocalReport.DataSources.Clear()
                    Dim rpt As New ReportDataSource("HasilLIS", dt)
                    ReportViewer1.LocalReport.DataSources.Add(rpt)
            End Select
        End If

        Me.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
    End Sub
End Class