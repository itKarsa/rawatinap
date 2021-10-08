Option Explicit On
Imports System.IO
Imports System.Net.Sockets
Imports MySql.Data.MySqlClient
Public Class Laboratorium

    Dim delete_action As String
    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Dim Client As TcpClient
    'Dim Listener As New TcpListener(8000)

    Sub tampilData()
        Call koneksiServer()
        da = New MySqlDataAdapter("SELECT kdTarif,SUBSTR(kelompokTindakan,23,35) AS kelompokTindakan, tindakan, tarif FROM vw_tindakanlab WHERE kelas = '" & txtKelas.Text & "'", conn)
        ds = New DataSet
        da.Fill(ds, "vw_tindakanlab")
        DataGridView1.DataSource = ds.Tables("vw_tindakanlab")
        conn.Close()
    End Sub

    Sub tampilDataRiwayat()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        query = "CALL riwayatlabranap('" & txtNoRM.Text & "')"
        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        DataGridView3.Rows.Clear()

        Do While dr.Read
            DataGridView3.Rows.Add(dr.Item("noRekamedis"), dr.Item("idTindakanPenunjangRanap"),
                                   dr.Item("noTindakanPenunjangRanap"), dr.Item("tglMasukPenunjangRanap"),
                                   dr.Item("kdTarif"), dr.Item("tindakan"), dr.Item("tarif"), dr.Item("jumlahTindakan"),
                                   dr.Item("DPJP"), dr.Item("PPA"), dr.Item("statusPenunjang"), dr.Item("totalTarif"),
                                   dr.Item("totalTindakanPenunjangRanap"), dr.Item("statusPembayaran"))
        Loop
        dr.Close()
        conn.Close()
    End Sub

    Sub aturDGVTindakan()
        Try
            DataGridView1.Columns(0).Width = 70
            DataGridView1.Columns(1).Width = 150
            DataGridView1.Columns(2).Width = 300
            DataGridView1.Columns(3).Width = 130
            DataGridView1.Columns(0).HeaderText = "KODE"
            DataGridView1.Columns(1).HeaderText = "KELOMPOK"
            DataGridView1.Columns(2).HeaderText = "TINDAKAN"
            DataGridView1.Columns(3).HeaderText = "TARIF"

            DataGridView1.Columns(0).Visible = False

            DataGridView2.Columns(0).Width = 180
            DataGridView2.Columns(1).Width = 180
            DataGridView2.Columns(2).Width = 150
            DataGridView2.Columns(3).Width = 350
            DataGridView2.Columns(4).Width = 120
            DataGridView2.Columns(5).Width = 150
            DataGridView2.Columns(6).Width = 100
            DataGridView2.Columns(7).Width = 250
            DataGridView2.Columns(8).Width = 100
            DataGridView2.Columns(9).Width = 100
            DataGridView2.Columns(10).Width = 100
            DataGridView2.Columns(11).Width = 100
            DataGridView2.Columns(12).Width = 100
            DataGridView2.Columns(0).HeaderText = "NO.PERMINTAAN"
            DataGridView2.Columns(1).HeaderText = "NO.TINDAKAN"
            DataGridView2.Columns(2).HeaderText = "KD.TARIF"
            DataGridView2.Columns(3).HeaderText = "TINDAKAN"
            DataGridView2.Columns(4).HeaderText = "TARIF"
            DataGridView2.Columns(5).HeaderText = "TGL.PERMINTAAN"
            DataGridView2.Columns(6).HeaderText = "KD.MEDIS"
            DataGridView2.Columns(7).HeaderText = "DOKTER PENGIRIM"
            DataGridView2.Columns(8).HeaderText = "TOTAL TARIF"
            DataGridView2.Columns(9).HeaderText = "NO.RM"
            DataGridView2.Columns(10).HeaderText = "NO.DAFTAR"
            DataGridView2.Columns(11).HeaderText = "STATUS"
            DataGridView2.Columns(12).HeaderText = "ID.DETAIL"
        Catch ex As Exception

        End Try
    End Sub

    Sub caridata()
        Call koneksiServer()

        Dim query As String
        query = "SELECT kdTarif, SUBSTR(kelompokTindakan,23,35) AS kelompokTindakan, tindakan, tarif 
                   FROM vw_tindakanlab 
                  WHERE kelas = '" & txtKelas.Text & "' 
                    AND tindakan Like '%" & txtCari.Text & "%'"
        da = New MySqlDataAdapter(query, conn)

        Dim str As New DataTable
        str.Clear()
        da.Fill(str)
        DataGridView1.DataSource = str

        conn.Close()
    End Sub

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

            'MsgBox(tglSkrg & " | " & tgl)

            If tglSkrg = tgl Then
                queryId = "SELECT SUBSTR(noRegistrasiPenunjangRanap,19,5) FROM t_registrasipenunjangranap ORDER BY tglMasukPenunjangRanap DESC LIMIT 1"
                cmdId = New MySqlCommand(queryId, conn)
                drId = cmdId.ExecuteReader
                drId.Read()
                If drId.HasRows Then
                    noPermintaanLab = kode + Format(Now, "ddMMyyHHmmss") + "-" + (Val(Trim(drId.Item(0).ToString)) + 1).ToString
                    txtNoPermintaan.Text = noPermintaanLab
                End If
                drId.Close()
            Else
                noPermintaanLab = kode + Format(Now, "ddMMyyHHmmss") + "-1"
                txtNoPermintaan.Text = noPermintaanLab
            End If
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MessageBoxIcon.Exclamation, "NO.PERMINTAAN")
        End Try
    End Sub

    Sub autoNoTindakan()
        Dim noTindakanLab As String

        Try
            Call koneksiServer()
            Dim query As String
            Dim cmdNoTin As MySqlCommand
            Dim drNoTin As MySqlDataReader
            query = "SELECT SUBSTR(noTindakanPenunjangRanap,18,4) FROM t_tindakanpenunjangranap ORDER BY CAST(SUBSTR(noTindakanPenunjangRanap,18,4) AS UNSIGNED) DESC LIMIT 1"
            cmdNoTin = New MySqlCommand(query, conn)
            drNoTin = cmdNoTin.ExecuteReader
            drNoTin.Read()
            If drNoTin.HasRows Then
                drNoTin.Read()
                noTindakanLab = "TLAB" + Format(Now, "ddMMyyHHmmss") + "-" + (Val(Trim(drNoTin.Item(0).ToString)) + 1).ToString
                txtNoTindakan.Text = noTindakanLab
            Else
                noTindakanLab = "TLAB" + Format(Now, "ddMMyyHHmmss") + "-1"
                txtNoTindakan.Text = noTindakanLab
            End If
            conn.Close()
        Catch ex As Exception

        End Try

    End Sub

    Sub transferSelected()
        Call koneksiServer()
        Dim drow As New System.Windows.Forms.DataGridViewRow

        For Each drow In Me.DataGridView1.SelectedRows
            DataGridView2.Rows.Add(1)
            DataGridView2.Rows(DataGridView2.RowCount - 1).Cells(0).Value = txtNoPermintaan.Text
            DataGridView2.Rows(DataGridView2.RowCount - 1).Cells(1).Value = txtNoTindakan.Text
            DataGridView2.Rows(DataGridView2.RowCount - 1).Cells(2).Value = drow.Cells(0).Value.ToString
            DataGridView2.Rows(DataGridView2.RowCount - 1).Cells(3).Value = drow.Cells(2).Value.ToString
            DataGridView2.Rows(DataGridView2.RowCount - 1).Cells(4).Value = drow.Cells(3).Value
            DataGridView2.Rows(DataGridView2.RowCount - 1).Cells(5).Value = datePermintaan.Text
            DataGridView2.Rows(DataGridView2.RowCount - 1).Cells(6).Value = txtKdDokter.Text
            DataGridView2.Rows(DataGridView2.RowCount - 1).Cells(7).Value = txtDokter.Text
            DataGridView2.Rows(DataGridView2.RowCount - 1).Cells(11).Value = "PERMINTAAN"
            DataGridView2.Update()
        Next

        For i As Integer = 0 To DataGridView2.RowCount - 1
            If DataGridView2.Rows(i).Cells(11).Value.ToString = "PERMINTAAN" Then
                DataGridView2.Rows(i).Cells(11).Style.BackColor = Color.Orange
                DataGridView2.Rows(i).Cells(11).Style.ForeColor = Color.White
            End If
        Next

        conn.Close()
    End Sub

    Sub totalTarif()
        Dim jum As Integer = 0
        Dim totTarif As Long = 0

        For i As Integer = jum To DataGridView2.Rows.Count - 1
            totTarif = totTarif + Val(DataGridView2.Rows(i).Cells(4).Value)
        Next
        txtTotalTarif.Text = totTarif
    End Sub

    Sub addRegistrasiLab()
        'Dim dt As String
        'dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Call koneksiServer()
        Try
            Dim str As String
            Dim cmdReg As MySqlCommand
            str = "INSERT INTO t_registrasipenunjangranap(noRegistrasiPenunjangRanap,noDaftar,
                                                          kdUnitAsal,unitAsal,kdUnit,
                                                          unit,tglMasukPenunjangRanap,statusPenunjang,
                                                          kdTenagaMedisPengirim,ketKlinis) 
                   VALUES ('" & txtNoPermintaan.Text & "','" & txtReg.Text & "','" & txtKdRuang.Text & "',
                           '" & txtRuang.Text & "','3002','Laboratorium','" & Format(datePermintaan.Value, "yyyy-MM-dd HH:mm:ss") & "','PERMINTAAN',
                           '" & txtKdDokter.Text & "','" & txtKet.Text & "')"
            cmdReg = New MySqlCommand(str, conn)
            cmdReg.ExecuteNonQuery()
            MsgBox("Permintaan Laboratorium berhasil dilakukan", MsgBoxStyle.Information, "Information")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Registrasi")
        End Try

        conn.Close()
    End Sub

    Sub addTindakan()
        Call koneksiServer()
        Try
            Dim str As String
            Dim cmdTindak As MySqlCommand
            str = "INSERT INTO t_tindakanpenunjangranap(noTindakanPenunjangRanap,noRegistrasiPenunjangRanap,
                                                        totalTindakanPenunjangRanap,statusPembayaran) 
                   VALUES ('" & txtNoTindakan.Text & "','" & txtNoPermintaan.Text & "','" & txtTotalTarif.Text & "','BELUM LUNAS')"
            cmdTindak = New MySqlCommand(str, conn)
            cmdTindak.ExecuteNonQuery()
            'MsgBox("Insert Tindakan Lab berhasil dilakukan", MsgBoxStyle.Information, "Information")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Tindakan")
        End Try

        conn.Close()
    End Sub

    Sub addDetail()
        Call koneksiServer()
        Dim val1, val2, val3, val4, val8 As String
        Dim str As String
        Dim cmdDet As MySqlCommand
        str = "INSERT INTO t_detailtindakanpenunjangranap
                                    (noTindakanPenunjangRanap,kdTarif,tindakan,tarif,
                                    jumlahTindakan,totalTarif,statusTindakan) 
                   VALUES (@noTindakanPenunjangRanap,@kdTarif,@tindakan,@tarif,
                           '1',@totalTarif,'PERMINTAAN')"
        cmdDet = New MySqlCommand(str, conn)

        Try
            For i As Integer = 0 To DataGridView2.Rows.Count - 1
                val1 = DataGridView2.Rows(i).Cells(1).Value
                val2 = DataGridView2.Rows(i).Cells(2).Value
                val3 = DataGridView2.Rows(i).Cells(3).Value
                val4 = DataGridView2.Rows(i).Cells(4).Value
                val8 = DataGridView2.Rows(i).Cells(4).Value

                cmdDet.Parameters.AddWithValue("@noTindakanPenunjangRanap", val1)
                cmdDet.Parameters.AddWithValue("@kdTarif", val2)
                cmdDet.Parameters.AddWithValue("@tindakan", val3)
                cmdDet.Parameters.AddWithValue("@tarif", val4)
                cmdDet.Parameters.AddWithValue("@totalTarif", val8)
                cmdDet.ExecuteNonQuery()
                cmdDet.Parameters.Clear()
            Next
            'MsgBox("Insert data Detail Tindakan Lab berhasil dilakukan", MessageBoxIcon.Information, "Information")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "Information")
            cmdDet.Dispose()
        End Try
        conn.Close()

    End Sub

    Sub statusHasilLab()

        Try
            Call koneksiServer()
            Dim str As String
            str = "SELECT noRegistrasiPenunjangRanap, nmPasien,
                   IF(noHasilPemeriksaanRanap IS NULL,'YES','NO') AS Status
                   FROM vw_cetakhasillabranap
                   WHERE noRegistrasiPenunjangRanap = '" & txtNoPerm.Text & "'"
            cmd = New MySqlCommand(str, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                txtStatus.Text = dr.Item("Status")
            End If

            If txtStatus.Text = "YES" Then
                btnHasilLab.Enabled = False
            ElseIf txtStatus.Text = "NO" Then
                btnHasilLab.Enabled = True
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Laboratorium_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SplitContainer1.Panel2Collapsed = True

        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.Manual
        With Screen.PrimaryScreen.WorkingArea
            Me.SetBounds(.Left, .Top, .Width, .Height)
        End With

        datePermintaan.Format = DateTimePickerFormat.Custom
        datePermintaan.CustomFormat = "yyyy-MM-dd HH:mm:ss"

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Laboratorium"
                    txtReg.Text = Form1.txtNoDaftar.Text
                    txtNoRM.Text = Form1.txtRekMed.Text
                    txtPasien.Text = Form1.txtNamaPasien.Text
                    txtUmur.Text = Form1.txtUmur.Text
                    txtJk.Text = Form1.txtJk.Text
                    txtKdRuang.Text = Form1.txtKdUnitRanap.Text
                    txtRuang.Text = Form1.txtUnitRanap.Text
                    txtDokter.Text = Form1.txtDokter.Text
                    txtIpAddress.Text = Form1.txtIpAddress.Text
                    txtKelas.Text = Form1.txtKelas.Text
            End Select
        End If

        Call autoNoPermintaan()
        Call autoNoTindakan()
        Call tampilData()
        Call aturDGVTindakan()
        Call tampilDataRiwayat()

        txtKet.Select()
        txtKet.BackColor = Color.FromArgb(255, 112, 112)

        If DataGridView2.Rows.Count > 0 Then
            Me.btnSimpan.Enabled = True
        Else
            Me.btnSimpan.Enabled = False
        End If

    End Sub

    Private Sub txtCari_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCari.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call caridata()
            Call aturDGVTindakan()
        End If
    End Sub

    Private Sub btnPilihOk_Click(sender As Object, e As EventArgs) Handles btnPilihOk.Click
        Call transferSelected()
        Call totalTarif()
    End Sub

    Private Sub txtDokter_TextChanged(sender As Object, e As EventArgs) Handles txtDokter.TextChanged
        Call koneksiServer()
        Try
            Dim query As String
            query = "SELECT * FROM t_tenagamedis2 WHERE namapetugasMedis = '" & txtDokter.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                txtKdDokter.Text = UCase(dr.GetString("kdPetugasMedis"))
            End While
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
        conn.Close()
    End Sub

    Private Sub btnPilihBatal_Click(sender As Object, e As EventArgs) Handles btnPilihBatal.Click
        Dim drDgv As New DataGridViewRow
        For Each drDgv In Me.DataGridView2.SelectedRows
            DataGridView2.Rows.Remove(drDgv)
        Next

        If DataGridView2.Rows.Count = 0 Then
            Me.btnSimpan.Enabled = False
        End If

        Call totalTarif()
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If txtKet.Text = "" Then
            MsgBox("Diagnosa klinis harus diisi terlebih dahulu.", MsgBoxStyle.Exclamation)
        Else
            Call addRegistrasiLab()
            Call addTindakan()
            Call addDetail()
            Call tampilDataRiwayat()
            Call autoNoPermintaan()
            Call autoNoTindakan()
            DataGridView2.Rows.Clear()
            txtKet.BackColor = Color.FromArgb(255, 112, 112)
            txtKet.Text = ""
            txtTotalTarif.Text = ""
            btnSimpan.Enabled = False

            'Try
            '    Client = New TcpClient("192.168.200.93", 8080)        'IP tujuan
            '    Dim writer As New StreamWriter(Client.GetStream())
            '    writer.Write(txtIpAddress.Text)                        'IP pengirim
            '    writer.Flush()
            'Catch ex As Exception
            '    MsgBox(ex.Message)
            'End Try
        End If
    End Sub

    Private Sub btnSimpan_DoubleClick(sender As Object, e As EventArgs) Handles btnSimpan.DoubleClick
        Return
    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        If DataGridView2.Rows.Count = 0 Then
            Me.Close()
            Form1.Show()
        Else
            Dim konfirmasi As MsgBoxResult
            konfirmasi = MsgBox("Apakah yakin tindakan sudah disimpan ?", vbQuestion + vbYesNo, "Konfirmasi")
            If konfirmasi = vbYes Then
                Me.Close()
                Form1.Show()
            End If
        End If
    End Sub

    Private Sub btnHasilLab_Click(sender As Object, e As EventArgs) Handles btnHasilLab.Click
        Hasil_Lab.Ambil_Data = True
        Hasil_Lab.Form_Ambil_Data = "Hasil"
        Hasil_Lab.Show()
    End Sub

    Private Sub DataGridView2_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseClick
        'Dim noPerm As String

        If e.RowIndex = -1 Then
            Return
        End If

        'noPerm = DataGridView2.Rows(e.RowIndex).Cells(0).Value
        'txtNoPerm.Text = noPerm

        'Call statusHasilLab()
    End Sub

    Private Sub txtKet_TextChanged(sender As Object, e As EventArgs) Handles txtKet.TextChanged
        If txtKet.Text <> "" Then
            txtKet.BackColor = Color.White
        End If
    End Sub

    Private Sub txtKet_LostFocus(sender As Object, e As EventArgs) Handles txtKet.LostFocus
        If txtKet.Text = "" Then
            txtKet.BackColor = Color.FromArgb(255, 112, 112)
        End If
    End Sub

    Private Sub txtKet_KeyDown(sender As Object, e As KeyEventArgs) Handles txtKet.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtKet.Text = "" Then
                txtKet.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter And DataGridView1.CurrentCell.RowIndex >= 0 Then
            e.Handled = True
            e.SuppressKeyPress = True

            Dim row As DataGridViewRow
            row = Me.DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex)

            If row.Cells(2).Value = "LAIN-LAIN" Then
                MsgBox("Maaf masih dalam tahap develop", MsgBoxStyle.Information)
            Else
                Call transferSelected()
                Call totalTarif()

                If DataGridView2.Rows.Count > 0 Then
                    DataGridView2.Columns(4).DefaultCellStyle.Format = "###,###,###"
                    DataGridView2.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                End If
            End If
        End If
    End Sub

    Private Sub DataGridView2_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridView2.RowsAdded
        If DataGridView2.Rows.Count <> 0 Then
            Me.btnSimpan.Enabled = True
        End If
        Call totalTarif()
    End Sub

    Private Sub DataGridView2_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles DataGridView2.RowsRemoved
        If DataGridView2.Rows.Count = 0 Then
            Me.btnSimpan.Enabled = False
        End If
        Call totalTarif()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        If e.RowIndex = -1 Then
            Return
        End If

        If DataGridView1.Rows(e.RowIndex).Cells(2).Value = "LAIN-LAIN" Then
            GroupBox4.Visible = True
            txtLain2.Visible = True
            txtLain2.BackColor = Color.FromArgb(255, 112, 112)
            txtLain2.Text = "Tambahkan tindakan lain"
            txtLain2.Font = New Font("Arial", 12, FontStyle.Italic)
            MsgBox("Maaf masih dalam tahap pengembangan", MsgBoxStyle.Information)
        Else
            GroupBox4.Visible = False
            txtLain2.Visible = False
        End If
    End Sub

    Private Sub txtLain2_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLain2.GotFocus
        txtLain2.Text = ""
    End Sub
    Private Sub txtLain2_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLain2.LostFocus
        If txtLain2.Text = "" Then
            txtLain2.Text = "Tambahkan tindakan lain"
            txtLain2.Font = New Font("Arial", 12, FontStyle.Italic)
        End If
    End Sub

    Private Sub btnRiwayat_Click(sender As Object, e As EventArgs) Handles btnRiwayat.Click
        If SplitContainer1.Panel2Collapsed = False Then
            SplitContainer1.Panel2Collapsed = True
        Else
            SplitContainer1.Panel2Collapsed = False
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.RowIndex > 0 And e.ColumnIndex = 1 Then
            If DataGridView1.Item(1, e.RowIndex - 1).Value = e.Value Then
                e.Value = ""
            End If
        End If

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next

        DataGridView1.DefaultCellStyle.ForeColor = Color.Black
        DataGridView1.Columns(3).DefaultCellStyle.Format = "###,###,###"
        DataGridView1.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    End Sub

    Private Sub DataGridView2_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView2.CellFormatting
        For i As Integer = 0 To DataGridView2.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next

        DataGridView2.DefaultCellStyle.ForeColor = Color.Black
        DataGridView2.Columns(4).DefaultCellStyle.Format = "###,###,###"
        DataGridView2.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub

    Private Sub DataGridView3_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView3.CellFormatting
        If e.RowIndex > 0 And e.ColumnIndex = 3 Then
            If DataGridView3.Item(3, e.RowIndex - 1).Value = e.Value Then
                e.Value = "~"
            End If
        End If

        For i As Integer = 0 To DataGridView3.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next

        For i As Integer = 0 To DataGridView3.RowCount - 1
            If DataGridView3.Rows(i).Cells(10).Value.ToString = "PERMINTAAN" Then
                DataGridView3.Rows(i).Cells(10).Style.BackColor = Color.Orange
                DataGridView3.Rows(i).Cells(10).Style.ForeColor = Color.White
            ElseIf DataGridView3.Rows(i).Cells(10).Value.ToString = "DALAM TINDAKAN" Then
                DataGridView3.Rows(i).Cells(10).Style.BackColor = Color.Green
                DataGridView3.Rows(i).Cells(10).Style.ForeColor = Color.White
            ElseIf DataGridView3.Rows(i).Cells(10).Value.ToString = "SELESAI" Then
                DataGridView3.Rows(i).Cells(10).Style.BackColor = Color.Red
                DataGridView3.Rows(i).Cells(10).Style.ForeColor = Color.White
            End If
        Next

        DataGridView3.DefaultCellStyle.ForeColor = Color.Black
        DataGridView3.Columns(6).DefaultCellStyle.Format = "###,###,###"
        DataGridView3.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridView3.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub

    Private Sub DataGridView1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Dim dg As DataGridView = DirectCast(sender, DataGridView)
        ' Current row record
        Dim rowNumber As String = (e.RowIndex + 1).ToString()

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

    Private Sub DataGridView2_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView2.RowPostPaint
        Dim dg As DataGridView = DirectCast(sender, DataGridView)
        ' Current row record
        Dim rowNumber As String = (e.RowIndex + 1).ToString()

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

    Private Sub DataGridView3_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView3.RowPostPaint
        Dim dg As DataGridView = DirectCast(sender, DataGridView)
        ' Current row record
        Dim rowNumber As String = (e.RowIndex + 1).ToString()

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

    Private Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick
        If e.RowIndex = -1 Then
            Return
        End If

        txtID.Text = DataGridView3.Item(1, e.RowIndex).Value.ToString
        txtNoTindakRiwayat.Text = DataGridView3.Item(2, e.RowIndex).Value.ToString
        txtTotalTarifDetail.Text = DataGridView3.Item(11, e.RowIndex).Value.ToString
        txtTotalTarifRiwayat.Text = DataGridView3.Item(12, e.RowIndex).Value.ToString
    End Sub

    Private Sub DataGridView3_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellClick
        If e.RowIndex = -1 Then
            Return
        End If

        txtID.Text = DataGridView3.Item(1, e.RowIndex).Value.ToString
        txtNoTindakRiwayat.Text = DataGridView3.Item(2, e.RowIndex).Value.ToString
        txtTotalTarifDetail.Text = DataGridView3.Item(11, e.RowIndex).Value.ToString
        txtTotalTarifRiwayat.Text = DataGridView3.Item(12, e.RowIndex).Value.ToString
    End Sub

    Sub deleteDetail(idDel As String)
        Try
            Call koneksiServer()
            Dim query As String
            query = "DELETE FROM t_detailtindakanpenunjangranap WHERE idTindakanPenunjangRanap= '" & idDel & "'"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()
            conn.Close()
            'MsgBox("Hapus tindakan berhasil", MsgBoxStyle.Information, "Information")

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Delete")
        End Try
    End Sub

    Sub updateAfterDelete(noTindakanDel As String)
        Dim total As Integer
        total = Integer.Parse(txtTotalTarifRiwayat.Text) - Integer.Parse(txtTotalTarifDetail.Text)
        Try
            Call koneksiServer()
            Dim str As String
            str = "UPDATE t_tindakanpenunjangranap
                          SET totalTindakanPenunjangRanap = '" & total & "'
                        WHERE noTindakanPenunjangRanap = '" & noTindakanDel & "'"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            'MsgBox("Update dokter berhasil dilakukan", MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox("Update data gagal dilakukan.", MessageBoxIcon.Error, "Error Update After Delete")
        End Try

        conn.Close()
    End Sub


    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        Dim konfirmasi As MsgBoxResult
        Dim index As Integer
        index = DataGridView3.CurrentCell.RowIndex

        If DataGridView3.Rows(index).Cells(10).Value.ToString = "DALAM TINDAKAN" Then
            MsgBox("Tidak dapat menghapus permintaan dengan status 'DALAM TINDAKAN'", MessageBoxIcon.Exclamation)
        ElseIf DataGridView3.Rows(index).Cells(10).Value.ToString = "SELESAI" Then
            MsgBox("Tidak dapat menghapus permintaan dengan status 'SELESAI'", MessageBoxIcon.Exclamation)
        Else
            konfirmasi = MsgBox("Apakah yakin permintaan tsb ingin dihapus ?", vbQuestion + vbYesNo, "Konfirmasi")
            If konfirmasi = vbYes Then
                Call deleteDetail(txtID.Text)
                Call updateAfterDelete(txtNoTindakRiwayat.Text)
                DataGridView3.Rows.RemoveAt(index)

                MsgBox("Permintaan tsb berhasil dihapus", MessageBoxIcon.Information)

                Call tampilDataRiwayat()
            End If
        End If
    End Sub

    Private Sub btnPilihOk_MouseLeave(sender As Object, e As EventArgs) Handles btnPilihOk.MouseLeave
        Me.btnPilihOk.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnPilihOk.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnPilihOk_MouseEnter(sender As Object, e As EventArgs) Handles btnPilihOk.MouseEnter
        Me.btnPilihOk.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnPilihOk.ForeColor = Color.White
    End Sub

    Private Sub btnPilihBatal_MouseLeave(sender As Object, e As EventArgs) Handles btnPilihBatal.MouseLeave, btnPilihOk.MouseLeave
        Me.btnPilihBatal.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnPilihBatal.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnPilihBatal_MouseEnter(sender As Object, e As EventArgs) Handles btnPilihBatal.MouseEnter
        Me.btnPilihBatal.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnPilihBatal.ForeColor = Color.White
    End Sub

    Private Sub btnRiwayat_MouseLeave(sender As Object, e As EventArgs) Handles btnRiwayat.MouseLeave, btnPilihOk.MouseLeave
        Me.btnRiwayat.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnRiwayat.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnRiwayat_MouseEnter(sender As Object, e As EventArgs) Handles btnRiwayat.MouseEnter
        Me.btnRiwayat.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnRiwayat.ForeColor = Color.White
    End Sub
End Class