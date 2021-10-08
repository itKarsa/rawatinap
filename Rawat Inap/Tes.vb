Imports MySql.Data.MySqlClient

Public Class Tes

    Sub jumHaper()
        Dim masuk As Date = DateTimePicker1.Value.ToString("dd/MM/yyyy")
        Dim pulang As Date = DateTimePicker2.Value.ToString("dd/MM/yyyy")

        Dim hari As Integer
        hari = DateDiff(DateInterval.Day, masuk, pulang)
        'TextBox1.Text = hari + 1
        If Format(hari) = 0 Then
            TextBox1.Text = 1
        ElseIf Format(hari) < 0 Then
            TextBox1.Text = 0
        ElseIf Format(hari) > 0 Then
            TextBox1.Text = hari + 1
        End If
    End Sub

    Function hitungUmur(ByVal masuk As Date, ByVal keluar As Date) As String
        Dim y, m, d As Integer
        y = keluar.Year - masuk.Year
        m = keluar.Month - masuk.Month
        d = keluar.Day - masuk.Day

        If Math.Sign(d) = -1 Then
            d = 30 - Math.Abs(d)
            m -= 1
        End If
        If Math.Sign(m) = -1 Then
            m = 12 - Math.Abs(m)
            y -= 1
        End If

        Return d
    End Function

    Sub autoNoPermintaan()
        Dim noPermintaanLab As String
        Dim queryTgl As String = ""
        Dim cmdTgl As MySqlCommand
        Dim drTgl As MySqlDataReader
        Dim tgl As String = ""
        Dim tglSkrg As String = ""
        Dim queryId As String = ""
        Dim cmdId As MySqlCommand
        Dim drId As MySqlDataReader
        Dim kode As String = ""

        Try
            Call koneksiServer()
            queryTgl = "SELECT SUBSTR(noRegistrasiPenunjangRanap,6,2) AS tgl FROM t_registrasipenunjangranap ORDER BY tglMasukPenunjangRanap DESC LIMIT 1"
            kode = "RILAB"
            cmdTgl = New MySqlCommand(queryTgl, conn)
            drTgl = cmdTgl.ExecuteReader
            drTgl.Read()

            If drTgl.HasRows Then
                tgl = drTgl.Item(0).ToString
            End If

            drTgl.Close()
            tglSkrg = Format(Date.Now, "dd")

            MsgBox(tglSkrg & " | " & tgl)

            If tglSkrg = tgl Then
                queryId = "SELECT SUBSTR(noRegistrasiPenunjangRanap,19,5) FROM t_registrasipenunjangranap ORDER BY tglMasukPenunjangRanap DESC LIMIT 1"
                cmdId = New MySqlCommand(queryId, conn)
                drId = cmdId.ExecuteReader
                drId.Read()
                If drId.HasRows Then
                    noPermintaanLab = kode + Format(Now, "ddMMyyHHmmss") + "-" + (Val(Trim(drId.Item(0).ToString)) + 1).ToString
                    TextBox1.Text = noPermintaanLab
                End If
                drId.Close()
            Else
                noPermintaanLab = kode + Format(Now, "ddMMyyHHmmss") + "-1"
                TextBox1.Text = noPermintaanLab
            End If
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MessageBoxIcon.Exclamation, "NO.PERMINTAAN")
        End Try
    End Sub

    Private Sub Tes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "dd-MM-yyyy HH:mm:00"
        DateTimePicker2.CustomFormat = "dd-MM-yyyy HH:mm:00"

        Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height

        TextBox2.Text = screenWidth
        TextBox3.Text = screenHeight
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        autoNoPermintaan()
        'Dim hasil As TimeSpan = DateTimePicker2.Value - DateAndTime.Now
        'TextBox1.Text = (String.Format("{0} {1}:{2}:{3}",
        '                               hasil.Days, hasil.Hours, hasil.Minutes, hasil.Seconds))
        'Label1.Text = hasil.Hours
        'If Label1.Text > 3 Then
        '    MsgBox("pasien sudah lebih dari 3 jam")
        'Else
        '    MsgBox("pasien belum lebih dari 3 jam, pasien tidak bisa dipindah")
        'End If

        'Dim startTime As New TimeSpan(0, 0, 1)
        'Dim endTime As New TimeSpan(12, 59, 59)
        'Dim midTime As New TimeSpan(13, 0, 1)

        'If DateTimePicker1.Value.TimeOfDay >= startTime And DateTimePicker1.Value.TimeOfDay <= endTime Then
        '    MsgBox("tarif kamar baru")
        'ElseIf DateTimePicker1.Value.TimeOfDay >= midTime Then
        '    MsgBox("tarif kamar baru tambah 1 hari")
        'End If

        'Call jumHaper()
        'TextBox1.Text = hitungUmur(DateTimePicker1.Value, DateTimePicker2.Value)
    End Sub

    Public Shared Function FromRtf(ByVal rtf As RichTextBox) As String
        Dim b, i, u As Boolean
        b = False : i = False : u = False
        Dim fontfamily As String = "Arial"
        Dim fontsize As Integer = 12
        Dim htmlstr As String = String.Format("<html>{0}<body>{0}<div style=""text-align: left;""><span style=""font-family: Arial; font-size: 12pt;"">", vbCrLf)
        Dim x As Integer = 0
        While x < rtf.Text.Length
            rtf.Select(x, 1)
            If rtf.SelectionFont.Bold AndAlso (Not b) Then
                htmlstr &= "<b>"
                b = True
            ElseIf (Not rtf.SelectionFont.Bold) AndAlso b Then
                htmlstr &= "</b>"
                b = False
            End If
            If rtf.SelectionFont.Italic AndAlso (Not i) Then
                htmlstr &= "<i>"
                i = True
            ElseIf (Not rtf.SelectionFont.Italic) AndAlso i Then
                htmlstr &= "</i>"
                i = False
            End If
            If rtf.SelectionFont.Underline AndAlso (Not u) Then
                htmlstr &= "<u>"
                u = True
            ElseIf (Not rtf.SelectionFont.Underline) AndAlso u Then
                htmlstr &= "</u>"
                u = False
            End If
            If fontfamily <> rtf.SelectionFont.FontFamily.Name Then
                htmlstr &= String.Format("</span><span style=""font-family: {0}; font-size: {0}pt;"">", rtf.SelectionFont.FontFamily.Name, fontsize)
                fontfamily = rtf.SelectionFont.FontFamily.Name
            End If
            If fontsize <> rtf.SelectionFont.SizeInPoints Then
                htmlstr &= String.Format("</span><span style=""font-family: {0}; font-size: {0}pt;"">", fontfamily, rtf.SelectionFont.SizeInPoints)
                fontsize = rtf.SelectionFont.SizeInPoints
            End If
            Dim curchar As String = rtf.SelectedText
            Select Case curchar
                Case vbCr, vbLf : curchar = "<br />"
                Case "&" : curchar = "&amp;" : x += "&amp;".Length - 1
                Case "<" : curchar = "&lt;" : x += "&lt;".Length - 1
                Case ">" : curchar = "&gt;" : x += "&gt;".Length - 1
                Case " " : curchar = "&nbsp;" : x += "&nbsp;".Length - 1
            End Select
            rtf.SelectedText = curchar
            x += 1
        End While
        Return htmlstr & String.Format("</span>{0}</body>{0}</html>", vbCrLf)
    End Function

    Private Sub btnBullet_Click(sender As Object, e As EventArgs) Handles btnBullet.Click
        RichTextBox2.SelectionBullet = True
    End Sub

    Private Sub bntBold_Click(sender As Object, e As EventArgs) Handles bntBold.Click
        With Me.RichTextBox2
            If .SelectionFont IsNot Nothing Then
                Dim currentFont As System.Drawing.Font = .SelectionFont
                Dim newFontStyle As System.Drawing.FontStyle

                If .SelectionFont.Bold = True Then
                    newFontStyle = currentFont.Style - Drawing.FontStyle.Bold
                Else
                    newFontStyle = currentFont.Style + Drawing.FontStyle.Bold
                End If

                .SelectionFont = New Drawing.Font(currentFont.FontFamily, currentFont.Size, newFontStyle)
            End If
        End With
    End Sub
End Class