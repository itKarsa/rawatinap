Imports System.ComponentModel
Imports MySql.Data.MySqlClient
Public Class Hemodialisa

    Dim noRegHD, kdUnitAsal, stats, tmline, noRegLama As String

    Sub autoNoReg()
        Try
            conn.Close()
            Call koneksiServer()
            Dim query As String
            dr.Close()
            query = "SELECT SUBSTR(noRegistrasiHD,18) FROM t_registrasihdranap ORDER BY CAST(SUBSTR(noRegistrasiHD,18) AS UNSIGNED) DESC LIMIT 1"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                dr.Read()
                noRegHD = "RIHD" + Format(Now, "ddMMyyHHmmss") + "-" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
            Else
                noRegHD = "RIHD" + Format(Now, "ddMMyyHHmmss") + "-1"
            End If
            conn.Close()
        Catch ex As Exception

        End Try

    End Sub

    Sub riwayatHd()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        query = "SELECT * 
                   FROM vw_riwayatpasienhd 
                  WHERE noRM = '" & txtRekMed.Text & "'
               ORDER BY tglRegistrasiHD DESC"

        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dgv.Rows.Clear()

        Do While dr.Read
            dgv.Rows.Add(Convert.ToDateTime(dr.Item("tglRegistrasiHD")), dr.Item("jumlahHD"), dr.Item("status"), dr.Item("statusPermintaan"))
        Loop
        conn.Close()
    End Sub

    Sub addHemo()
        Call koneksiServer()
        Try
            Dim str As String
            Dim cmd As MySqlCommand
            str = "INSERT INTO t_registrasihdranap (noRegistrasiHD,noRegistrasi,kdUnitAsal,unitAsal,
                                                    kdUnit,unit,tglRegistrasiHD,kdDokterPengirim,
                                                    noRM,keterangan,status,statusPermintaan,
                                                    userPermintaan,tglPermintaan,userModify,dateModify) 
                                            VALUES ('" & noRegHD & "','" & Form1.txtNoDaftar.Text & "','" & kdUnitAsal & "','" & txtUnitRanap.Text & "',
                                                    '3011','Hemodialisa','" & Format(dateReg.Value, "yyyy-MM-dd HH:mm:ss") & "','-',
                                                    '" & txtRekMed.Text & "','" & txtKet.Text & "','" & stats & "','PERMINTAAN',
                                                    '" & LoginForm.txtUsername.Text.ToUpper & "','" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "',
                                                    '" & LoginForm.txtUsername.Text.ToUpper & "','" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "')"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Registrasi Hemodialisa berhasil dilakukan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Function tampilProses(noReg As String, unit As String) As (noRegBf As String, stats As String)
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim value1 As String = ""
        Dim value2 As String = ""

        query = "SELECT noregistrasiHD,tglRegistrasiHD,statusPermintaan 
                   FROM vw_riwayatpasienhd 
                  WHERE noRM = '" & txtRekMed.Text & "'
               ORDER BY tglRegistrasiHD DESC LIMIT 1"

        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            value1 = dr.Item("noregistrasiHD").ToString
            value2 = dr.Item("statusPermintaan").ToString
        End If
        conn.Close()

        Return (value1, value2)
    End Function

    Sub updateBatal()
        Call koneksiServer()
        Try
            Dim str As String
            Dim cmd As MySqlCommand
            str = "UPDATE t_registrasihdranap 
                      SET statusPermintaan = 'BATAL', 
                          userModify = CONCAT(userModify,';" & LoginForm.txtUsername.Text.ToUpper & "'),
                          dateModify = CONCAT(dateModify,';" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "') 
                    WHERE noRegistrasiHD = '" & noRegLama & "'"
            'MsgBox(str)
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Permintaan berhasil dibatalkan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message & "Pembatalan Permintaan")
        End Try
        conn.Close()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Call autoNoReg()
        tmline = tampilProses(Form1.txtNoDaftar.Text, txtUnitRanap.Text).stats
    End Sub

    Private Sub Hemodialisa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Width = 460
        TableLayoutPanel1.Width = 450
        'TableLayoutPanel1.ColumnStyles(1).SizeType = SizeType.Percent
        'TableLayoutPanel1.ColumnStyles(1).Width = 0


        dateReg.Format = DateTimePickerFormat.Custom
        dateReg.CustomFormat = "dddd, ddMMMMyyyy HH:mm:ss"
        Timer1.Start()

        txtRekMed.Text = ""
        txtNama.Text = ""
        txtUnitRanap.Text = ""
        txtKelas.Text = ""
        txtKet.Text = ""
        kdUnitAsal = ""

        txtRekMed.Text = Form1.txtRekMed.Text
        txtNama.Text = Form1.txtNamaPasien.Text
        txtUnitRanap.Text = Form1.txtUnitRanap.Text
        txtKelas.Text = Form1.txtKelas.Text
        kdUnitAsal = Form1.txtKdUnitRanap.Text

        noRegLama = tampilProses(Form1.txtNoDaftar.Text, txtUnitRanap.Text).noRegBf
        tmline = tampilProses(Form1.txtNoDaftar.Text, txtUnitRanap.Text).stats

        If tmline = "" Then
            Me.Height = 404
            Panel4.Visible = False
            Panel4.Height = 10
            Return
        ElseIf tmline = "BATAL" Then
            Me.Height = 404
            Panel4.Visible = False
            Panel4.Height = 10
        Else
            Me.Height = 515
            Panel4.Visible = True
            Panel4.Height = 114

            Select Case tmline
                Case "PERMINTAAN"
                    txtOrder.ForeColor = Color.FromArgb(26, 141, 95)
                    btnBatal.Enabled = True
                Case "PROSES"
                    txtOrder.ForeColor = Color.FromArgb(26, 141, 95)
                    txtProses.ForeColor = Color.FromArgb(26, 141, 95)
                    btnBatal.Enabled = False
                    PictureBox1.Image = My.Resources.pic_Processing
                Case "SELESAI"
                    txtOrder.ForeColor = Color.FromArgb(26, 141, 95)
                    txtProses.ForeColor = Color.FromArgb(26, 141, 95)
                    txtDone.ForeColor = Color.FromArgb(26, 141, 95)
                    btnBatal.Enabled = False
                    PictureBox1.Image = My.Resources.pic_Complete
            End Select
        End If
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Dim konfirmasi As MsgBoxResult
        Select Case tmline
            Case ""
                konfirmasi = MsgBox("Apakah permintaan sudah benar ?", vbQuestion + vbYesNo, "Konfirmasi")
                If konfirmasi = vbYes Then
                    Call addHemo()
                End If
            Case "PERMINTAAN"
                konfirmasi = MsgBox("Permintaan sudah terkirim," & vbCrLf &
                                    "Apakah anda ingin membatalkan permintaan sebelumnya dan menginputkan permintaan baru ?", vbQuestion + vbYesNo, "Konfirmasi")
                If konfirmasi = vbYes Then
                    Call updateBatal()
                    Call addHemo()
                End If
            Case "PROSES"
                MsgBox("Pasien sedang dalam proses tindakan", MsgBoxStyle.Information)
            Case "SELESAI"
                konfirmasi = MsgBox("Apakah anda ingin menginputkan permintaan lagi ?", vbQuestion + vbYesNo, "Konfirmasi")
                If konfirmasi = vbYes Then
                    Call addHemo()
                    txtOrder.ForeColor = Color.Black
                    txtProses.ForeColor = Color.DarkGray
                    txtDone.ForeColor = Color.DarkGray
                    btnBatal.Enabled = True
                    PictureBox1.Image = My.Resources.pic_Order
                End If
        End Select
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If TableLayoutPanel1.Width = 450 Then
            Me.Width = 960
            TableLayoutPanel1.Width = 950

            Call riwayatHd()
        ElseIf TableLayoutPanel1.Width = 950 Then
            Me.Width = 460
            TableLayoutPanel1.Width = 450
        End If
    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Call updateBatal()
        Me.Height = 404
        Panel4.Visible = False
        Panel4.Height = 10
        btnBatal.Enabled = False
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

    Private Sub Hemodialisa_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Timer1.Stop()
    End Sub

    Private Sub dgv_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgv.CellFormatting
        For i As Integer = 0 To dgv.Rows.Count - 1
            If i Mod 2 = 0 Then
                dgv.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                dgv.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next

        For i As Integer = 0 To dgv.Rows.Count - 1
            If dgv.Rows(i).Cells(3).Value = "PERMINTAAN" Then
                dgv.Rows(i).Cells(3).Style.BackColor = Color.FromArgb(255, 155, 0)
                dgv.Rows(i).Cells(3).Style.ForeColor = Color.White
            ElseIf dgv.Rows(i).Cells(3).Value = "PROSES" Then
                dgv.Rows(i).Cells(3).Style.BackColor = Color.FromArgb(0, 60, 155)
                dgv.Rows(i).Cells(3).Style.ForeColor = Color.White
            ElseIf dgv.Rows(i).Cells(3).Value = "SELESAI" Then
                dgv.Rows(i).Cells(3).Style.BackColor = Color.FromArgb(15, 180, 100)
                dgv.Rows(i).Cells(3).Style.ForeColor = Color.White
            ElseIf dgv.Rows(i).Cells(3).Value = "BATAL" Then
                dgv.Rows(i).Cells(3).Style.BackColor = Color.FromArgb(255, 115, 115)
                dgv.Rows(i).Cells(3).Style.ForeColor = Color.White
            End If
        Next
    End Sub

    Private Sub dgv_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgv.RowPostPaint
        Dim dg As DataGridView = DirectCast(sender, DataGridView)
        ' Current row record
        Dim rowNumber As String = (e.RowIndex + 1).ToString()

        ' Format row based on number of records displayed by using leading zeros
        'While rowNumber.Length < dg.RowCount.ToString().Length
        '    rowNumber = "0" & rowNumber
        'End While

        ' Position text
        Dim size As SizeF = e.Graphics.MeasureString(rowNumber, Me.Font)
        If dg.RowHeadersWidth < CInt(size.Width + 20) Then
            dg.RowHeadersWidth = CInt(size.Width + 20)
        End If

        ' Use default system text brush
        Dim b As Brush = SystemBrushes.ControlText

        ' Draw row number
        e.Graphics.DrawString(rowNumber, dg.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2))
    End Sub
End Class