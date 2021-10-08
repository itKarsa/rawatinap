Imports MySql.Data.MySqlClient
Public Class Resep_Dokter

    Public Ambil_Data As String
    Public Form_Ambil_Data As String
    Dim hargaObat, totalObat As Double

    Dim theDataTable As New DataTable

    Sub dgv1_styleRow()
        For i As Integer = 0 To dgvResep.Rows.Count - 1
            If i Mod 2 = 0 Then
                dgvResep.Rows(i).DefaultCellStyle.BackColor = Color.AliceBlue
            Else
                dgvResep.Rows(i).DefaultCellStyle.BackColor = Color.White
            End If
        Next
    End Sub

    Sub addResep()
        Dim dt As String
        dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

        Call koneksiServer()

        Try
            Dim query As String
            query = "insert into t_resepranap
                                    (noResep,noDaftarRawatInap,noRekamedis,
                                     kdDepoObat,kdTenagaMedis,tglResep,statusResep) 
                            values ('" & txtNoResep.Text & "','" & txtNoReg.Text & "',
                                    '" & txtnoRM.Text & "','" & txtKdDepo.Text & "',
                                    '" & txtKdDokter.Text & "','" & dt & "','0')"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Resep Obat berhasil disimpan", MsgBoxStyle.Information, "Informasi")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "ERROR T_RESEP")
            cmd.Dispose()
        End Try

    End Sub

    Sub addDetailResep()
        Call koneksiServer()
        Dim val1, val2, val3, val4, val5, val6, val7, val8, val11, val12 As String
        Dim val9, val10 As Double

        Dim query As String
        query = "insert into t_detailresepranap
                                    (noDetailResepRanap,noResep,kdObat,
                                     nmObat,jumlahPakai,aturanPakai,
                                     jumlahHari,satuanJual,hargaJual,
                                     totalHargaJual,keterangan,statusRacikan) 
                             values (@noDetailResepRanap,@noResep,@kdObat,
                                     @nmObat,@jumlahPakai,@aturanPakai,
                                     @jumlahHari,@satuanJual,@hargaJual,
                                     @totalHargaJual,@keterangan,@statusRacikan)"
        cmd = New MySqlCommand(query, conn)

        Try
            For i As Integer = 0 To dgvResep.Rows.Count - 1
                val2 = dgvResep.Rows(i).Cells(2).Value
                val1 = dgvResep.Rows(i).Cells(1).Value
                val3 = dgvResep.Rows(i).Cells(3).Value
                val4 = dgvResep.Rows(i).Cells(4).Value
                val5 = dgvResep.Rows(i).Cells(5).Value
                val6 = dgvResep.Rows(i).Cells(6).Value
                val7 = dgvResep.Rows(i).Cells(7).Value
                val8 = dgvResep.Rows(i).Cells(8).Value
                val9 = dgvResep.Rows(i).Cells(9).Value.ToString
                val10 = dgvResep.Rows(i).Cells(10).Value.ToString
                val11 = dgvResep.Rows(i).Cells(11).Value
                val12 = dgvResep.Rows(i).Cells(12).Value

                cmd.Parameters.AddWithValue("@noDetailResepRanap", val2)
                cmd.Parameters.AddWithValue("@noResep", val1)
                cmd.Parameters.AddWithValue("@kdObat", val3)
                cmd.Parameters.AddWithValue("@nmObat", val4)
                cmd.Parameters.AddWithValue("@jumlahPakai", val5)
                cmd.Parameters.AddWithValue("@aturanPakai", val6)
                cmd.Parameters.AddWithValue("@jumlahHari", val7)
                cmd.Parameters.AddWithValue("@satuanJual", val8)
                cmd.Parameters.AddWithValue("@hargaJual", val9)
                cmd.Parameters.AddWithValue("@totalHargaJual", val10)
                cmd.Parameters.AddWithValue("@keterangan", val11)
                cmd.Parameters.AddWithValue("@statusRacikan", val12)
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
            Next
            'MsgBox("Resep Obat berhasil disimpan", MsgBoxStyle.Information, "Informasi")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "ERROR T_DETAIL_RESEP")
            cmd.Dispose()
        End Try

    End Sub

    Sub addDetailRacikan()
        Call koneksiServer()
        Dim val1, val2, val3, val4, val5, val8 As String
        Dim val6, val7 As Double

        Dim query As String
        query = "insert into t_detailracikan
                                    (noDetailResepRanap,kdObat,
                                     nmObat,jmlPemberian,
                                     satuan,harga,
                                     totalHarga,keterangan) 
                             values (@noDetailResepRanap,@kdObat,
                                     @nmObat,@jmlPemberian,
                                     @satuan,@harga,
                                     @totalHarga,@keterangan)"
        cmd = New MySqlCommand(query, conn)

        Try
            For i As Integer = 0 To DataGridView2.Rows.Count - 1
                'val0 = dgvResep.Rows(i).Cells(0).Value
                val1 = DataGridView2.Rows(i).Cells(1).Value
                val2 = DataGridView2.Rows(i).Cells(2).Value
                val3 = DataGridView2.Rows(i).Cells(3).Value
                val4 = DataGridView2.Rows(i).Cells(4).Value
                val5 = DataGridView2.Rows(i).Cells(5).Value
                val6 = DataGridView2.Rows(i).Cells(6).Value.ToString
                val7 = DataGridView2.Rows(i).Cells(7).Value.ToString
                val8 = DataGridView2.Rows(i).Cells(8).Value

                'cmd.Parameters.AddWithValue("@noDetailRacikan", val0)
                cmd.Parameters.AddWithValue("@noDetailResepRanap", val1)
                cmd.Parameters.AddWithValue("@kdObat", val2)
                cmd.Parameters.AddWithValue("@nmObat", val3)
                cmd.Parameters.AddWithValue("@jmlPemberian", val4)
                cmd.Parameters.AddWithValue("@satuan", val5)
                cmd.Parameters.AddWithValue("@harga", val6)
                cmd.Parameters.AddWithValue("@totalHarga", val7)
                cmd.Parameters.AddWithValue("@keterangan", val8)
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
            Next
            'MsgBox("Obat Racikan berhasil ditambahkan", MsgBoxStyle.Information, "Informasi")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "ERROR T_DETAIL_RACIKAN")
            cmd.Dispose()
        End Try

    End Sub

    Sub autoNoResep()
        Dim noPermintaan As String

        Try
            Call koneksiServer()
            Dim query As String
            query = "Select SUBSTR(noResep,17) FROM t_resepranap ORDER BY CAST(SUBSTR(noResep,17) AS UNSIGNED) DESC LIMIT 1"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                dr.Read()
                noPermintaan = "RSP" + Format(Now, "ddMMyyHHmmss") + "-" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                txtNoResep.Text = noPermintaan
            Else
                noPermintaan = "RSP" + Format(Now, "ddMMyyHHmmss") + "-1"
                txtNoResep.Text = noPermintaan
            End If
            conn.Close()
        Catch ex As Exception

        End Try
    End Sub

    Sub autoNoDetailFromDb()
        Dim noDetail As String

        Try
            Call koneksiServer()
            Dim query As String
            query = "SELECT SUBSTR(noDetailResepRanap,17) FROM t_detailresepranap ORDER BY CAST(SUBSTR(noDetailResepRanap,17) AS UNSIGNED) DESC LIMIT 1"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                dr.Read()
                noDetail = "DTR" + Format(Now, "ddMMyyHHmmss") + "-" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                txtDetail.Text = noDetail
            Else
                noDetail = "DTR" + Format(Now, "ddMMyyHHmmss") + "-1"
                txtDetail.Text = noDetail
            End If
            conn.Close()
        Catch ex As Exception

        End Try
    End Sub

    Sub autoNoDetailFromDgv()
        Dim noDetail As String
        noDetail = txtDetail.Text

        Dim nums As String() = noDetail.Split(New Char() {"-"})

        Try
            If dgvResep.Rows.Count <> 0 Then
                txtDetail.Text = "DTR" + Format(Now, "ddMMyyHHmmss") + "-" + (Val(nums(1)) + 1).ToString
            Else
                Call autoNoDetailFromDb()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Resep_Dokter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.Manual
        With Screen.PrimaryScreen.WorkingArea
            Me.SetBounds(.Left, .Top, .Width, .Height)
        End With

        Dim dt As Date = Format(Now)
        txtTglPermintaan.Text = dt.ToString("dddd, dd-MM-yyyy", New System.Globalization.CultureInfo("id-ID"))

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Resep"
                    txtnoRM.Text = Form1.txtRekMed.Text
                    txtNoReg.Text = Form1.txtRegRanap.Text
                    txtNama.Text = Form1.txtNamaPasien.Text
                    txtTglLahir.Text = Form1.dateLahir.Text
                    txtJk.Text = Form1.txtJk.Text
                    txtUmur.Text = Form1.txtUmur.Text
                    txtAlamat.Text = Form1.txtAlamat.Text
                    txtRanap.Text = Form1.txtUnitRanap.Text
                    txtTglMasuk.Text = Form1.txtTglMasuk.Text
                    txtCaraBayar.Text = Form1.txtCaraBayar.Text
                    txtPenjamin.Text = Form1.txtPenjamin.Text
                    txtDokter.Text = Form1.txtDokter.Text
                    txtKdDokter.Text = Form1.txtKdDokter.Text
            End Select
        End If

        Call autoNoResep()
        Call autoNoDetailFromDb()
        Call autoNoDetailFromDgv()
    End Sub

    Private Sub btnCariObat_Click(sender As Object, e As EventArgs) Handles btnCariObat.Click
        txtTotalHarga.Text = ""
        Daftar_ObatAlkes.Ambil_Data = True
        Daftar_ObatAlkes.Form_Ambil_Data = "Daftar Obat"
        Daftar_ObatAlkes.Show()
    End Sub

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        Call autoNoDetailFromDgv()
        Dim count As Integer
        count = dgvResep.Rows.Count

        dgvResep.Rows.Add(1)
        dgvResep.Rows(dgvResep.RowCount - 1).Cells(0).Value = count + 1
        dgvResep.Rows(dgvResep.RowCount - 1).Cells(1).Value = txtNoResep.Text
        dgvResep.Rows(dgvResep.RowCount - 1).Cells(2).Value = txtDetail.Text
        dgvResep.Rows(dgvResep.RowCount - 1).Cells(3).Value = txtKdObat.Text
        dgvResep.Rows(dgvResep.RowCount - 1).Cells(4).Value = txtObat.Text
        dgvResep.Rows(dgvResep.RowCount - 1).Cells(5).Value = txtDibutuhkan.Text
        dgvResep.Rows(dgvResep.RowCount - 1).Cells(6).Value = txtAturan.Text
        dgvResep.Rows(dgvResep.RowCount - 1).Cells(7).Value = txtJmlHari.Text
        dgvResep.Rows(dgvResep.RowCount - 1).Cells(8).Value = txtSatuan.Text
        dgvResep.Rows(dgvResep.RowCount - 1).Cells(9).Value = txtHarga.Text
        dgvResep.Rows(dgvResep.RowCount - 1).Cells(10).Value = txtTotalHarga.Text
        dgvResep.Rows(dgvResep.RowCount - 1).Cells(11).Value = txtKet.Text
        dgvResep.Rows(dgvResep.RowCount - 1).Cells(12).Value = 0
        dgvResep.Update()

        Call dgv1_styleRow()
    End Sub

    Private Sub dgvResep_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvResep.CellMouseClick
        If e.RowIndex = -1 Then
            Return
        End If
        'MsgBox(dgvResep.Rows(e.RowIndex).Cells(8).Value.ToString)
    End Sub

    Private Sub txtKronis_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtDibutuhkan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDibutuhkan.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtJmlHari_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJmlHari.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtDibutuhkan_KeyDown(sender As Object, e As KeyEventArgs) Handles txtDibutuhkan.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtDibutuhkan.Text = "" Then
                txtDibutuhkan.Text = 0
            End If
        End If
    End Sub

    Private Sub txtAturan_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtDosis_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtJmlHari_KeyDown(sender As Object, e As KeyEventArgs) Handles txtJmlHari.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtKet_KeyDown(sender As Object, e As KeyEventArgs) Handles txtKet.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtKet.Text = "" Then
                txtKet.Text = "-"
            End If
        End If
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If dgvResep.Rows.Count = 0 Then
            MsgBox("Please insert Resep Obat")
        Else
            addResep()
            addDetailResep()
            addDetailRacikan()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Obat_Racikan.Ambil_Data = True
        Obat_Racikan.Form_Ambil_Data = "Racikan"
        Obat_Racikan.Show()
    End Sub

    Private Sub txtDibutuhkan_TextChanged(sender As Object, e As EventArgs) Handles txtDibutuhkan.TextChanged
        txtTotalHarga.Text = ""
        Dim total As Double

        total = Format(Val(txtDibutuhkan.Text) * Val(CDbl(txtHarga.Text)), "0.00")
        txtTotalHarga.Text = total
    End Sub
End Class