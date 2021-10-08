Imports MySql.Data.MySqlClient
Public Class PasienCovid

    Dim stats, tglStats As String

    Sub selectstatsCovid()
        Call koneksiServer()
        Dim str As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        str = "SELECT statusCovid19,tglStatusCovid19 
                 FROM t_registrasi
                WHERE noDaftar = '" & Form1.txtNoDaftar.Text & "'"
        'MsgBox(str)
        cmd = New MySqlCommand(str, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            stats = dr.Item("statusCovid19").ToString
            tglStats = dr.Item("tglStatusCovid19").ToString
        End If
        conn.Close()
    End Sub

    Sub updatePasienCovid()
        Call koneksiServer()
        Dim str As String = ""
        Dim cmd As MySqlCommand

        If rbYa.Checked Then
            str = "UPDATE t_registrasi SET statusCovid19 = '" & stats & "',
                                           tglStatusCovid19 = '" & tglStats & "'
                                     WHERE noDaftar = '" & Form1.txtNoDaftar.Text & "'"
        ElseIf rbNo.Checked Then
            str = "UPDATE t_registrasi SET statusCovid19 = '" & stats & "'
                                     WHERE noDaftar = '" & Form1.txtNoDaftar.Text & "'"
        End If
        'MsgBox(str)

        Try

            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Status berhasil diupdate.", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message & " -Registrasi-")
        End Try
        conn.Close()
    End Sub

    Private Sub PasienCovid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "dd-MM-yyyy HH:mm"

        txtNoRM.Text = Form1.txtRekMed.Text
        txtNama.Text = Form1.txtNamaPasien.Text
        rbNo.Checked = True

        Call selectstatsCovid()
        If stats = "" And tglStats = "" Then
            'MsgBox("Kosong")
            tglStats = Format(DateTimePicker1.Value, "yyyy-MM-dd HH:mm:ss")
        Else
            'MsgBox(stats & " | " & tglStats)
            DateTimePicker1.Value = Format(CDate(tglStats), "dd-MM-yyyy HH:mm")
            rbYa.Checked = True

            If RadioButton1.Text = stats Then
                RadioButton1.Checked = True
            ElseIf RadioButton2.Text = stats Then
                RadioButton2.Checked = True
            ElseIf RadioButton3.Text = stats Then
                RadioButton3.Checked = True
            ElseIf RadioButton4.Text = stats Then
                RadioButton4.Checked = True
            ElseIf RadioButton5.Text = stats Then
                RadioButton5.Checked = True
            ElseIf RadioButton6.Text = stats Then
                RadioButton6.Checked = True
            ElseIf RadioButton7.Text = stats Then
                RadioButton7.Checked = True
            ElseIf RadioButton8.Text = stats Then
                RadioButton8.Checked = True
            End If
        End If
    End Sub

    Private Sub rbNo_CheckedChanged(sender As Object, e As EventArgs) Handles rbNo.CheckedChanged
        If rbNo.Checked = True Then
            FlowLayoutPanel2.Enabled = False
            DateTimePicker1.Enabled = False
            stats = "Tidak"
            tglStats = ""
        ElseIf rbNo.Checked = False Then
            Return
        End If

    End Sub

    Private Sub rbYa_CheckedChanged(sender As Object, e As EventArgs) Handles rbYa.CheckedChanged
        If rbYa.Checked = True Then
            FlowLayoutPanel2.Enabled = True
            DateTimePicker1.Enabled = True
        ElseIf rbYa.Checked = False Then
            Return
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            stats = RadioButton1.Text
        ElseIf RadioButton1.Checked = False Then
            Return
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            stats = RadioButton2.Text
        ElseIf RadioButton2.Checked = False Then
            Return
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            stats = RadioButton3.Text
        ElseIf RadioButton3.Checked = False Then
            Return
        End If
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        If RadioButton4.Checked = True Then
            stats = RadioButton4.Text
        ElseIf RadioButton4.Checked = False Then
            Return
        End If
    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        If RadioButton5.Checked = True Then
            stats = RadioButton5.Text
        ElseIf RadioButton5.Checked = False Then
            Return
        End If
    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        If RadioButton6.Checked = True Then
            stats = RadioButton6.Text
        ElseIf RadioButton6.Checked = False Then
            Return
        End If
    End Sub

    Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton7.CheckedChanged
        If RadioButton7.Checked = True Then
            stats = RadioButton7.Text
        ElseIf RadioButton7.Checked = False Then
            Return
        End If
    End Sub

    Private Sub RadioButton8_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton8.CheckedChanged
        If RadioButton8.Checked = True Then
            stats = RadioButton8.Text
        ElseIf RadioButton8.Checked = False Then
            Return
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        tglStats = Format(DateTimePicker1.Value, "yyyy-MM-dd HH:mm:ss")
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Call updatePasienCovid()
    End Sub
End Class