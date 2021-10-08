Imports MySql.Data.MySqlClient
Public Class Obat_Racikan

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Dim hargaJenis, hargaObat, totalJenis, totalObat As Double

    Sub addDetailRacikan()
        Call koneksiServer()
        Dim val1, val2, val3, val4, val5, val6 As String
        Dim val7, val8 As Double

        Dim query As String
        query = "insert into t_detailracikan
                                    (noDetailResepRanap,kdObat,
                                     nmObat,jmlPemberian,
                                     satuan,harga,
                                     totalHarga,keterangan) 
                             values (@kdRacikan,@kdObat,
                                     @nmObat,@jmlPemberian,
                                     @satuan,@harga,
                                     @totalHarga,@keterangan)"
        cmd = New MySqlCommand(query, conn)

        Try
            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                'val0 = dgvResep.Rows(i).Cells(0).Value
                val1 = txtNoDetail.Text
                val2 = DataGridView1.Rows(i).Cells(1).Value
                val3 = DataGridView1.Rows(i).Cells(2).Value
                val4 = DataGridView1.Rows(i).Cells(3).Value
                val5 = DataGridView1.Rows(i).Cells(4).Value
                val6 = DataGridView1.Rows(i).Cells(5).Value.ToString
                val7 = DataGridView1.Rows(i).Cells(6).Value.ToString
                val8 = DataGridView1.Rows(i).Cells(7).Value.ToString

                'cmd.Parameters.AddWithValue("@noDetailRacikan", val0)
                cmd.Parameters.AddWithValue("@noDetailResepRanap", val1)
                cmd.Parameters.AddWithValue("@kdObat", val2)
                cmd.Parameters.AddWithValue("@nmObat", val3)
                cmd.Parameters.AddWithValue("@jmlPemberian", val4)
                cmd.Parameters.AddWithValue("@satuan", val5)
                cmd.Parameters.AddWithValue("@harga", val6)
                cmd.Parameters.AddWithValue("@totalHarga", val7)
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
            Next
            MsgBox("Obat Racikan berhasil ditambahkan", MsgBoxStyle.Information, "Informasi")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "Informasi")
            cmd.Dispose()
        End Try

    End Sub

    Sub autoJenisRacikan()
        conn.Close()
        Call koneksiFarmasi()
        cmd = New MySqlCommand("SELECT obat FROM vw_stokobat WHERE plu IN ('1046','1047','1048','1049') AND kddepo = 'DP03'", conn)
        da = New MySqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        txtJenisRacikan.DataSource = dt
        txtJenisRacikan.DisplayMember = "obat"
        txtJenisRacikan.ValueMember = "obat"
        txtJenisRacikan.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txtJenisRacikan.AutoCompleteSource = AutoCompleteSource.ListItems
        conn.Close()
    End Sub

    Private Sub Obat_Racikan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Racikan"
                    Call Resep_Dokter.autoNoDetailFromDgv()
                    txtNoResep.Text = Resep_Dokter.txtNoResep.Text
                    txtNoDetail.Text = Resep_Dokter.txtDetail.Text
            End Select
        End If

        Call autoJenisRacikan()
    End Sub

    Private Sub btnRacikan_Click(sender As Object, e As EventArgs) Handles btnRacikan.Click
        If txtJenisRacikan.Text = "" And
           txtSatuanJenis.Text = "" And
           txtJmlJenis.Text = "" And
           txtTotalJenis.Text = "" Then

            MsgBox("Pilih jenis racikan terlebih dahulu !!", MsgBoxStyle.Exclamation, "Warning")
        Else
            GroupBox2.Enabled = True
            'MsgBox(totalJenis)
        End If
    End Sub

    Private Sub txtJenisRacikan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtJenisRacikan.SelectedIndexChanged
        conn.Close()
        Call koneksiFarmasi()
        Try
            Dim query As String
            query = "SELECT plu,satuan, hargajual FROM vw_stokobat WHERE obat = '" & txtJenisRacikan.Text & "' AND kddepo = 'DP03'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                txtKdJenis.Text = UCase(dr.GetString("plu"))
                txtHargaJenis.Text = UCase(dr.GetString("hargajual"))
                hargaJenis = UCase(dr.GetString("hargajual"))
                txtSatuanJenis.Text = UCase(dr.GetString("satuan"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim total As Double
        txtJmlJenis.Text = 1
        txtJmlJenis.Enabled = True
        total = Format(Val(txtJmlJenis.Text) * Val(hargaJenis), "0.00")
        totalJenis = total
        txtTotalJenis.Text = total

        conn.Close()
    End Sub

    Private Sub txtHargaJenis_TextChanged(sender As Object, e As EventArgs) Handles txtHargaJenis.TextChanged
        txtHargaJenis.Text = FormatCurrency(txtHargaJenis.Text, 2)
    End Sub

    Private Sub txtJmlJenis_TextChanged(sender As Object, e As EventArgs) Handles txtJmlJenis.TextChanged
        Dim total As Double

        total = Format(Val(txtJmlJenis.Text) * Val(hargaJenis), "0.00")
        totalJenis = total
        txtTotalJenis.Text = total

    End Sub

    Private Sub txtTotalJenis_TextChanged(sender As Object, e As EventArgs) Handles txtTotalJenis.TextChanged
        txtTotalJenis.Text = FormatCurrency(txtTotalJenis.Text, 2)
    End Sub

    Private Sub btnCariObat_Click(sender As Object, e As EventArgs) Handles btnCariObat.Click
        txtJmlPakaiObat.Text = ""
        txtTotalObat.Text = ""
        Daftar_ObatAlkes.Ambil_Data = True
        Daftar_ObatAlkes.Form_Ambil_Data = "Racikan"
        Daftar_ObatAlkes.Show()
    End Sub

    Private Sub txtObat_TextChanged(sender As Object, e As EventArgs) Handles txtObat.TextChanged
        Dim total As Double

        total = Val(txtJmlPakaiObat.Text) * Val(txtHargaObatDec.Text)
        totalObat = total
        txtTotalObat.Text = total
    End Sub

    Private Sub txtJmlPakaiObat_TextChanged(sender As Object, e As EventArgs) Handles txtJmlPakaiObat.TextChanged
        Dim total As Double

        total = Val(txtJmlPakaiObat.Text) * Val(txtHargaObatDec.Text)
        totalObat = total
        txtTotalObat.Text = total
    End Sub

    Private Sub txtHargaObat_TextChanged(sender As Object, e As EventArgs) Handles txtHargaObat.TextChanged
        txtHargaObat.Text = FormatCurrency(txtHargaObat.Text, 2)
    End Sub

    Private Sub btnSelesai_Click(sender As Object, e As EventArgs) Handles btnSelesai.Click
        Dim konfirmasi As MsgBoxResult

        konfirmasi = MsgBox("Apakah anda yakin semua obat racikan sudah dimasukkan ?", vbQuestion + vbYesNo, "Konfirmasi")
        If konfirmasi = vbYes Then
            Dim count As Integer
            count = Resep_Dokter.dgvResep.Rows.Count

            Resep_Dokter.dgvResep.Rows.Add(1)
            Resep_Dokter.dgvResep.Rows(Resep_Dokter.dgvResep.RowCount - 1).Cells(0).Value = count + 1
            Resep_Dokter.dgvResep.Rows(Resep_Dokter.dgvResep.RowCount - 1).Cells(1).Value = txtNoResep.Text
            Resep_Dokter.dgvResep.Rows(Resep_Dokter.dgvResep.RowCount - 1).Cells(2).Value = txtNoDetail.Text
            Resep_Dokter.dgvResep.Rows(Resep_Dokter.dgvResep.RowCount - 1).Cells(3).Value = txtKdJenis.Text
            Resep_Dokter.dgvResep.Rows(Resep_Dokter.dgvResep.RowCount - 1).Cells(4).Value = txtJenisRacikan.Text
            Resep_Dokter.dgvResep.Rows(Resep_Dokter.dgvResep.RowCount - 1).Cells(5).Value = txtJmlJenis.Text
            Resep_Dokter.dgvResep.Rows(Resep_Dokter.dgvResep.RowCount - 1).Cells(6).Value = txtAturan.Text
            Resep_Dokter.dgvResep.Rows(Resep_Dokter.dgvResep.RowCount - 1).Cells(7).Value = txtJmlHari.Text
            Resep_Dokter.dgvResep.Rows(Resep_Dokter.dgvResep.RowCount - 1).Cells(8).Value = txtSatuanJenis.Text
            Resep_Dokter.dgvResep.Rows(Resep_Dokter.dgvResep.RowCount - 1).Cells(9).Value = txtHargaJenis.Text
            Resep_Dokter.dgvResep.Rows(Resep_Dokter.dgvResep.RowCount - 1).Cells(10).Value = txtTotalJenis.Text
            Resep_Dokter.dgvResep.Rows(Resep_Dokter.dgvResep.RowCount - 1).Cells(11).Value = txtKetJenis.Text
            Resep_Dokter.dgvResep.Rows(Resep_Dokter.dgvResep.RowCount - 1).Cells(12).Value = 1
            Resep_Dokter.dgvResep.Update()

            Call Resep_Dokter.dgv1_styleRow()

            Dim val0, val1, val2, val3, val4, val5, val6, val7, val8 As String

            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                val0 = DataGridView1.Rows(i).Cells(0).Value
                val1 = DataGridView1.Rows(i).Cells(1).Value
                val2 = DataGridView1.Rows(i).Cells(2).Value
                val3 = DataGridView1.Rows(i).Cells(3).Value
                val4 = DataGridView1.Rows(i).Cells(4).Value
                val5 = DataGridView1.Rows(i).Cells(5).Value
                val6 = DataGridView1.Rows(i).Cells(6).Value.ToString
                val7 = DataGridView1.Rows(i).Cells(7).Value.ToString
                val8 = DataGridView1.Rows(i).Cells(8).Value

                Resep_Dokter.DataGridView2.Rows.Add(1)
                Resep_Dokter.DataGridView2.Rows(Resep_Dokter.DataGridView2.RowCount - 1).Cells(0).Value = val0
                Resep_Dokter.DataGridView2.Rows(Resep_Dokter.DataGridView2.RowCount - 1).Cells(1).Value = val1
                Resep_Dokter.DataGridView2.Rows(Resep_Dokter.DataGridView2.RowCount - 1).Cells(2).Value = val2
                Resep_Dokter.DataGridView2.Rows(Resep_Dokter.DataGridView2.RowCount - 1).Cells(3).Value = val3
                Resep_Dokter.DataGridView2.Rows(Resep_Dokter.DataGridView2.RowCount - 1).Cells(4).Value = val4
                Resep_Dokter.DataGridView2.Rows(Resep_Dokter.DataGridView2.RowCount - 1).Cells(5).Value = val5
                Resep_Dokter.DataGridView2.Rows(Resep_Dokter.DataGridView2.RowCount - 1).Cells(6).Value = val6
                Resep_Dokter.DataGridView2.Rows(Resep_Dokter.DataGridView2.RowCount - 1).Cells(7).Value = val7
                Resep_Dokter.DataGridView2.Rows(Resep_Dokter.DataGridView2.RowCount - 1).Cells(8).Value = val8
                Resep_Dokter.DataGridView2.Update()

            Next
            Me.Close()
        End If

    End Sub

    Private Sub txtTotalObat_TextChanged(sender As Object, e As EventArgs) Handles txtTotalObat.TextChanged
        txtTotalObat.Text = FormatCurrency(txtTotalObat.Text, 2)
    End Sub

    Private Sub btnTambahObat_Click(sender As Object, e As EventArgs) Handles btnTambahObat.Click
        Dim count As Integer
        count = DataGridView1.Rows.Count

        DataGridView1.Rows.Add(1)
        DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(0).Value = count + 1
        DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(1).Value = txtNoDetail.Text
        DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(2).Value = txtKdObat.Text
        DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(3).Value = txtObat.Text
        DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(4).Value = txtJmlPakaiObat.Text
        DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(5).Value = txtSatuanObat.Text
        DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(6).Value = txtHargaObat.Text
        DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(7).Value = txtTotalObat.Text
        DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(8).Value = txtKet.Text
        DataGridView1.Update()

        'MsgBox(totalObat)
    End Sub

    Private Sub btnTambahRacikan_Click(sender As Object, e As EventArgs) Handles btnTambahRacikan.Click
        'Call addDetailRacikan()
    End Sub

    Private Sub txtJmlHari_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJmlHari.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtJmlPakaiObat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJmlPakaiObat.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtJmlJenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJmlJenis.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub
End Class