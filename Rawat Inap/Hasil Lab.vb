Imports MySql.Data.MySqlClient
Imports Microsoft.Reporting.WinForms
Public Class Hasil_Lab

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Dim noRm, noPerm As String

    Private Sub Hasil_Lab_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksiServer()

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Hasil"
                    noRm = Laboratorium.txtNoRM.Text
                    noPerm = Laboratorium.txtNoPerm.Text

                    Dim dt As New DataTable
                    da = New MySqlDataAdapter("SELECT * FROM vw_cetakhasillabranap
                                                       WHERE noRekamedis = '" & noRm & "' 
                                                         AND noRegistrasiPenunjangRanap = '" & noPerm & "'", conn)
                    ds = New DataSet
                    da.Fill(dt)
                    ReportViewer1.LocalReport.DataSources.Clear()
                    Dim rpt As New ReportDataSource("HasilLab", dt)
                    ReportViewer1.LocalReport.DataSources.Add(rpt)
            End Select
        End If

        Me.ReportViewer1.RefreshReport()
    End Sub
End Class