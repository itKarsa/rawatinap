Imports System.ComponentModel
Imports MySql.Data.MySqlClient
Public Class Info_Biaya_Perawatan

    Public Ambil_Data As String
    Public Form_Ambil_Data As String
    Dim dateForm As Date
    Dim totBiayaRuang As Long
    Dim noRegUnit, noTindakan As String

    Dim scrollVal As Integer

    Sub totalTarif()
        totBiayaRuang = 0

        Dim s As String = txtJumInap.Text
        Dim words As String() = s.Split(New Char() {" "c})
        Dim x As String = words(0)

        'totBiayaRuang = Val(txtTarifKmr2.Text * x) + Val(CInt(Form1.txtTarifDPJP.Text))
        totBiayaRuang = Val(CInt(txtTotalTarifKmr.Text)) + Val(CInt(txtTarifDpjp.Text))
        txtBiayaAkomodasi.Text = totBiayaRuang.ToString("#,##0")
        txtBiayaPelayanan.Text = txtTotalAll.Text
    End Sub

    Sub tampilTarifKamar()
        Call koneksiServer()

        Dim query As String
        Dim cmd As MySqlCommand
        Dim total As Integer
        query = "SELECT * FROM vw_daftarruangakomodasi
                  WHERE noDaftar = '" & txtNoDaftar.Text & "'"
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgvRuang.Rows.Clear()
            Do While dr.Read
                If dr.IsDBNull(6) Then
                    total = 0
                Else
                    total = dr.GetString(6).ToString
                End If
                dgvRuang.Rows.Add(dr.Item("rawatInap"), dr.Item("kelas"), dr.Item("tarifKmr"),
                                  dr.Item("jumlahHariMenginap"), total)
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Sub totalAkomodasi()
        Dim subTotal As Integer

        For i As Integer = 0 To dgvRuang.Rows.Count - 1
            subTotal = subTotal + Val(dgvRuang.Rows(i).Cells(4).Value)
        Next
        txtTotalTarifKmr.Text = subTotal.ToString("#,##0")
    End Sub

    Sub tampilData()
        Call koneksiServer()
        Dim query As String = ""
        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Info Biaya"
                    query = "CALL riwayatlayanan('" & Form1.txtNoDaftar.Text & "','" & Form1.txtRekMed.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "','" & Format(DateAdd(DateInterval.Day, 1, DateTimePicker2.Value), "yyyy-MM-dd") & "')"
                Case "Riwayat Layanan"
                    query = "CALL riwayatlayanan('" & txtNoDaftar.Text & "','" & txtRekMed.Text & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "','" & Format(DateAdd(DateInterval.Day, 1, DateTimePicker2.Value), "yyyy-MM-dd") & "')"
            End Select
        End If

        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dgvInfo.Rows.Clear()

        Do While dr.Read
            dgvInfo.Rows.Add(dr.Item("noRegistrasiRawatJalan"), Convert.ToDateTime(dr.Item("tglTindakan")),
                             dr.Item("unit"), dr.Item("noTindakanPasienRajal"),
                             dr.Item("statusPembayaran"), dr.Item("totalTarifTindakan"))
        Loop
        dr.Close()
        conn.Close()
    End Sub

    Sub tampilDataDetail()
        Dim query As String = ""

        Select Case noRegUnit
            Case "P1"
                query = "CALL riwayatlayananrajaldetail('" & noTindakan & "')"
            Case "PI"
                query = "CALL riwayatlayananrajaldetail('" & noTindakan & "')"
            Case "IG"
                query = "CALL riwayatlayananrajaldetail('" & noTindakan & "')"
            Case "RJ"
                query = "CALL riwayatlayananrajaldetail('" & noTindakan & "')"
            Case "RI"
                query = "CALL riwayatlayananranapdetail('" & noTindakan & "')"
        End Select

        Call koneksiServer()
        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dgvDetailInfo.Rows.Clear()

        Do While dr.Read
            dgvDetailInfo.Rows.Add(dr.Item("noTindakan"), dr.Item("tindakan"), dr.Item("jumlahTindakan"), dr.Item("tarif"),
                                   dr.Item("DPJP"), dr.Item("PPA"), dr.Item("totalTarif"))
        Loop
        dr.Close()
        conn.Close()
    End Sub

    Sub totalBiaya()
        Dim subTotal As Integer

        For i As Integer = 0 To dgvInfo.Rows.Count - 1
            subTotal = subTotal + Val(dgvInfo.Rows(i).Cells(5).Value)
        Next
        txtTotalAll.Text = (Math.Ceiling(subTotal / 100) * 100).ToString("#,##0")
    End Sub

    Sub totalDetailBiaya()
        Dim subTotal As Integer

        For i As Integer = 0 To dgvDetailInfo.Rows.Count - 1
            subTotal = subTotal + Val(dgvDetailInfo.Rows(i).Cells(6).Value)
        Next
        txtTotalDetail.Text = (Math.Ceiling(subTotal / 100) * 100).ToString("#,##0")
    End Sub

    Sub tampilTextBoxPasien()
        Dim noRM As String = ""
        Dim query As String = ""

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Riwayat Layanan"
                    query = "CALL datatiappxranap('" & Daftar_Pasien.txtRanap.Text & "',
                                                '" & Daftar_Pasien.txtNoRM.Text & "',
                                                '" & Format(CDate(Daftar_Pasien.txtTglRanap.Text), "yyyy-MM-dd HH:mm:ss") & "')"
            End Select
        End If
        'MsgBox(query)
        Try
            Call koneksiServer()
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                txtNoReg.Text = dr.Item("noDaftarRawatInap").ToString
                txtNoDaftar.Text = dr.Item("noDaftar").ToString
                txtTglMasuk.Text = dr.Item("tglDaftar").ToString
                dateForm = dr.Item("tglMasukRawatInap").ToString
                txtNamaPasien.Text = dr.Item("nmPasien").ToString
                txtTglLahir.Text = dr.Item("tglLahir").ToString

                Dim lahir As Date = dr.Item("tglLahir").ToString
                txtUmur.Text = Form1.hitungUmur(lahir)

                txtJk.Text = dr.Item("jenisKelamin").ToString
                txtCaraBayar.Text = dr.Item("carabayar").ToString
                txtPenjamin.Text = dr.Item("penjamin").ToString
                txtUnitRanap.Text = dr.Item("rawatInap").ToString
                txtKelas.Text = dr.Item("kelas").ToString
                txtTarifKmr.Text = CInt(dr.Item("tarifKmr")).ToString("#,##0")
                txtTarifKmr2.Text = dr.Item("tarifKmr").ToString
                txtJumInap.Text = dr.Item("jumlah").ToString
                DateTimePicker1.Value = dr.Item("tglDaftar").ToString
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Sub tampilTarifDpjp()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim dt As New DataTable
        query = "CALL tarifdpjpranap('" & txtUnitRanap.Text & "','" & txtKelas.Text & "')"
        Try
            Call koneksiServer()
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                If IsDBNull(dr.Item(0)) Then
                    txtTarifDpjp.Text = 0
                Else
                    txtTarifDpjp.Text = CInt(dr.Item(0)).ToString("#,##0")
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub Info_Biaya_Perawatan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.Manual
        With Screen.PrimaryScreen.WorkingArea
            Me.SetBounds(.Left, .Top, .Width, .Height)
        End With

        Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Info Biaya"
                    Label1.Text = "INFORMASI BIAYA PERAWATAN - RUANG " & Form1.Label1.Text.ToUpper
                    txtRekMed.Text = Form1.txtRekMed.Text
                    txtNoReg.Text = Form1.txtRegRanap.Text
                    txtNoDaftar.Text = Form1.txtNoDaftar.Text
                    txtTglMasuk.Text = Form1.dateDaftar.Text
                    dateForm = Form1.txtTglMasuk.Text
                    txtNamaPasien.Text = Form1.txtNamaPasien.Text
                    txtTglLahir.Text = Form1.dateLahir.Text
                    txtUmur.Text = Form1.txtUmur.Text
                    txtJk.Text = Form1.txtJk.Text
                    txtCaraBayar.Text = Form1.txtCaraBayar.Text
                    txtPenjamin.Text = Form1.txtPenjamin.Text
                    txtUnitRanap.Text = Form1.txtUnitRanap.Text
                    txtKelas.Text = Form1.txtKelas.Text
                    txtTarifKmr.Text = Form1.txtTarifKmr.Text
                    txtTarifKmr2.Text = Form1.txtTarifKmr.Text
                    txtJumInap.Text = Form1.txtJumInap.Text
                    txtTarifDpjp.Text = Form1.txtTarifDPJP.Text
                    DateTimePicker1.Value = Form1.dateDaftar.Value
                Case "Riwayat Layanan"
                    Label1.Text = "INFORMASI BIAYA PERAWATAN - RUANG " & Daftar_Pasien.txtRanap.Text.ToUpper
                    txtRekMed.Text = Daftar_Pasien.txtNoRM.Text
                    Call tampilTextBoxPasien()
                    Call tampilTarifDpjp()
            End Select
        End If

        'txtTarifKmr.Text = Format(Val(txtTarifKmr.Text), "Rp, ###,###,###")

        'Call totalTarif()

        If (screenWidth >= 1360 And screenWidth <= 1440) And (screenHeight >= 768 And screenHeight <= 900) Then
            TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Percent
            TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Percent
            TableLayoutPanel1.RowStyles(1).Height = 65
            TableLayoutPanel1.RowStyles(2).Height = 35
            GroupBox2.Dock = DockStyle.None
            GroupBox2.Anchor = AnchorStyles.Left
            GroupBox2.Anchor = AnchorStyles.Right
            GroupBox2.Anchor = AnchorStyles.Bottom
            Label12.Font = New Font("Tahoma", 12, FontStyle.Bold)
            Label34.Font = New Font("Tahoma", 20, FontStyle.Bold)
            txtTotalBiaya.Font = New Font("Tahoma", 20, FontStyle.Bold)
        Else
            TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Percent
            TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Percent
            TableLayoutPanel1.RowStyles(1).Height = 75
            TableLayoutPanel1.RowStyles(2).Height = 25
            GroupBox2.Dock = DockStyle.Fill
        End If

        tampilTarifKamar()
        totalAkomodasi()

    End Sub

    Private Sub btnTampil_Click(sender As Object, e As EventArgs) Handles btnTampil.Click
        'Call caridataTindakan()
        'Call caridataObat()
        'Call caridataLab()
        'Call caridataRad()

        Call tampilData()
        Call totalBiaya()
        Call totalTarif()
    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub Info_Biaya_Perawatan_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Form1.Show()
    End Sub

    Private Sub btnTampil_MouseLeave(sender As Object, e As EventArgs) Handles btnTampil.MouseLeave
        Me.btnTampil.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnTampil.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnTampil_MouseEnter(sender As Object, e As EventArgs) Handles btnTampil.MouseEnter
        Me.btnTampil.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnTampil.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub dgvInfo_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInfo.CellClick
        If e.RowIndex = -1 Then
            Return
        End If
        noRegUnit = dgvInfo.Rows(e.RowIndex).Cells(0).Value.Substring(0, 2)
        noTindakan = dgvInfo.Rows(e.RowIndex).Cells(3).Value
        Call tampilDataDetail()
        Call totalDetailBiaya()
    End Sub

    Private Sub dgvInfo_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInfo.CellContentClick
        If e.RowIndex = -1 Then
            Return
        End If
        noRegUnit = dgvInfo.Rows(e.RowIndex).Cells(0).Value.Substring(0, 2)
        noTindakan = dgvInfo.Rows(e.RowIndex).Cells(3).Value
        Call tampilDataDetail()
        Call totalDetailBiaya()
    End Sub

    Private Sub dgvInfo_KeyUp(sender As Object, e As KeyEventArgs) Handles dgvInfo.KeyUp
        Dim crRowIndex As Integer = Me.dgvInfo.CurrentCell.RowIndex
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            noRegUnit = dgvInfo.Rows(crRowIndex).Cells(0).Value.Substring(0, 2)
            noTindakan = dgvInfo.Rows(crRowIndex).Cells(3).Value
            Call tampilDataDetail()
            Call totalDetailBiaya()
        End If
    End Sub

    Private Sub dgvInfo_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvInfo.CellFormatting
        Dim subTotalLunas As Integer = 0
        Dim subTotalBelum As Integer = 0
        For i As Integer = 0 To dgvInfo.Rows.Count - 1
            If i Mod 2 = 0 Then
                dgvInfo.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                dgvInfo.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next

        For i As Integer = 0 To dgvInfo.Rows.Count - 1
            If dgvInfo.Rows(i).Cells(4).Value = "BELUM LUNAS" Then
                dgvInfo.Rows(i).Cells(4).Style.BackColor = Color.FromArgb(255, 155, 0)
                dgvInfo.Rows(i).Cells(4).Style.ForeColor = Color.White
                subTotalBelum = subTotalBelum + Val(dgvInfo.Rows(i).Cells(5).Value)
                txtTotBelum.Text = (Math.Ceiling((subTotalBelum + totBiayaRuang) / 100) * 100).ToString("#,##0")
                txtTotalBiaya.Text = txtTotBelum.Text
            ElseIf dgvInfo.Rows(i).Cells(4).Value = "LUNAS" Then
                dgvInfo.Rows(i).Cells(4).Style.BackColor = Color.FromArgb(15, 180, 100)
                dgvInfo.Rows(i).Cells(4).Style.ForeColor = Color.White
                subTotalLunas = subTotalLunas + Val(dgvInfo.Rows(i).Cells(5).Value)
                txtTotSudah.Text = (Math.Ceiling(subTotalLunas / 100) * 100).ToString("#,##0")
            End If
        Next
    End Sub

    Private Sub dgvDetailInfo_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvDetailInfo.CellFormatting
        For i As Integer = 0 To dgvDetailInfo.Rows.Count - 1
            If i Mod 2 = 0 Then
                dgvDetailInfo.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                dgvDetailInfo.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Private Sub dgvInfo_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvInfo.RowPostPaint
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

    Private Sub dgvDetailInfo_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvDetailInfo.RowPostPaint
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

    Private Sub dgvRuang_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvRuang.RowPostPaint
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

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        Me.Button1.BackColor = Color.FromArgb(232, 243, 239)
        Me.Button1.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs) Handles Button1.MouseEnter
        Me.Button1.BackColor = Color.FromArgb(26, 141, 95)
        Me.Button1.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub Button2_MouseLeave(sender As Object, e As EventArgs) Handles Button2.MouseLeave
        Me.Button2.BackColor = Color.FromArgb(232, 243, 239)
        Me.Button2.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub Button2_MouseEnter(sender As Object, e As EventArgs) Handles Button2.MouseEnter
        Me.Button2.BackColor = Color.FromArgb(26, 141, 95)
        Me.Button2.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        scrollVal = scrollVal - Val(TextBox2.Text)
        If scrollVal <= 0 Then
            scrollVal = 0
        End If
        ds.Clear()
        da.Fill(ds, scrollVal, Val(TextBox2.Text), 0)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox2.Text <= 0 Then
            MsgBox("Tentukan Jumlah Data Dahulu")
            TextBox2.Focus()
        End If

        scrollVal = scrollVal + Val(TextBox2.Text)
        ds.Clear()
        da.Fill(ds, scrollVal, Val(TextBox2.Text), 0)
    End Sub
End Class