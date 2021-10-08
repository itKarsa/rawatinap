Imports MySql.Data.MySqlClient
Imports Microsoft.Reporting.WinForms
Public Class ViewHasilPA

    Dim rtbMa As RichTextBox = New RichTextBox
    Dim rtbMi As RichTextBox = New RichTextBox
    Dim rtbCo As RichTextBox = New RichTextBox

    Sub tampilHasil()
        Try
            Call koneksiServer()
            Dim str As String
            Dim cmd As MySqlCommand
            Dim dr As MySqlDataReader
            str = "SELECT makroskopik, mikroskopik, kesimpulan FROM t_hasilpemeriksaanpatologi WHERE noRegistrasiPA = '" & noRegPA & "'"
            cmd = New MySqlCommand(str, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then

                If dr.IsDBNull(0) Then
                    Return
                Else
                    rtbMa.Rtf = System.Text.Encoding.Unicode.GetChars(dr.Item("makroskopik"))
                End If

                If dr.IsDBNull(1) Then
                    Return
                Else
                    rtbMi.Rtf = System.Text.Encoding.Unicode.GetChars(dr.Item("mikroskopik"))
                End If

                If dr.IsDBNull(2) Then
                    Return
                Else
                    rtbCo.Rtf = System.Text.Encoding.Unicode.GetChars(dr.Item("kesimpulan"))
                End If

            End If
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Private Sub ViewHasilPA_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tampilHasil()
        Dim htmlMakro, htmlMikro, htmlConclu As String
        Dim r As New SautinSoft.RtfToHtml
        r.OutputFormat = SautinSoft.RtfToHtml.eOutputFormat.HTML_5
        r.Encoding = SautinSoft.RtfToHtml.eEncoding.UTF_8
        r.TextStyle.InlineCSS = True
        r.TextStyle.Font = True

        htmlMakro = r.ConvertString(rtbMa.Rtf)
        htmlMikro = r.ConvertString(rtbMi.Rtf)
        htmlConclu = r.ConvertString(rtbCo.Rtf)

        Dim drPA As New ReportParameter("dokPA", DokPA)
        Dim makros As New ReportParameter("makros", htmlMakro)
        Dim mikros As New ReportParameter("mikros", htmlMikro)
        Dim kesimpulan As New ReportParameter("kesimpulan", htmlConclu)

        ReportViewer1.LocalReport.SetParameters(drPA)
        ReportViewer1.LocalReport.SetParameters(makros)
        ReportViewer1.LocalReport.SetParameters(mikros)
        ReportViewer1.LocalReport.SetParameters(kesimpulan)

        koneksiServer()
        Dim dt As New DataTable
        da = New MySqlDataAdapter("SELECT * FROM vw_cetakhasilpatologi
                                            WHERE noRegistrasiPARajal = '" & noRegPA & "'", conn)

        ds = New DataSet
        da.Fill(dt)
        ReportViewer1.LocalReport.DataSources.Clear()
        Dim rpt As New ReportDataSource("HasilPA", dt)
        ReportViewer1.LocalReport.DataSources.Add(rpt)


        Me.ReportViewer1.SetDisplayMode(DisplayMode.PrintLayout)
    End Sub
End Class