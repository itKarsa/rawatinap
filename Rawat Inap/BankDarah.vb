Imports MySql.Data.MySqlClient
Public Class BankDarah

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Sub dgv_styleRow()
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.AliceBlue
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.White
            End If
        Next

        For i As Integer = 0 To DataGridView2.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.AliceBlue
            Else
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.White
            End If
        Next
    End Sub

    Sub tampilData()
        Call koneksiServer()
        da = New MySqlDataAdapter("SELECT kdTarif, kelompokTindakan, tindakan, tarif FROM vw_tindakanbdrs WHERE kelas = '" & txtKelas.Text & "'", conn)
        ds = New DataSet
        da.Fill(ds, "vw_tindakanbdrs")
        DataGridView1.DataSource = ds.Tables("vw_tindakanbdrs")

        da = New MySqlDataAdapter("SELECT noRegistrasiBDRSRanap, noTindakanBDRSRanap, 
                                          kdTarif, tindakan, tarif, tglMasukBDRSRanap, 
                                          kdDokterPengirim, namapetugasMedis, 
                                          totalTindakanBDRSRanap, noRekamedis, noDaftar, 
                                          statusBDRS, idTindakanBDRSRanap
                                     FROM vw_databdrspasienranap 
                                    WHERE noRekamedis= '" & txtNoRM.Text & "' 
                                      AND noDaftar = '" & txtReg.Text & "'", conn)
        ds = New DataSet
        da.Fill(ds, "vw_databdrspasienranap")
        DataGridView2.DataSource = ds.Tables("vw_databdrspasienranap")

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

            DataGridView1.DefaultCellStyle.ForeColor = Color.Black
            DataGridView1.Columns(3).DefaultCellStyle.Format = "###,###,###"
            DataGridView1.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            DataGridView2.Columns(0).Width = 180
            DataGridView2.Columns(1).Width = 180
            DataGridView2.Columns(2).Width = 150
            DataGridView2.Columns(3).Width = 350
            DataGridView2.Columns(4).Width = 150
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

            DataGridView2.Columns(0).Visible = False
            DataGridView2.Columns(1).Visible = False
            DataGridView2.Columns(2).Visible = False
            DataGridView2.Columns(6).Visible = False
            DataGridView2.Columns(8).Visible = False
            DataGridView2.Columns(9).Visible = False
            DataGridView2.Columns(10).Visible = False
            DataGridView2.Columns(12).Visible = False

            DataGridView2.DefaultCellStyle.ForeColor = Color.Black
            DataGridView2.Columns(4).DefaultCellStyle.Format = "###,###,###"
            DataGridView2.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Call dgv_styleRow()
        Catch ex As Exception

        End Try
    End Sub

    Sub caridata()
        Call koneksiServer()

        Dim query As String
        query = "SELECT kdTarif, kelompokTindakan, tindakan, tarif FROM vw_tindakanbdrs WHERE tindakan Like '%" & txtCari.Text & "%'"
        da = New MySqlDataAdapter(query, conn)

        Dim str As New DataTable
        str.Clear()
        da.Fill(str)
        DataGridView1.DataSource = str

        conn.Close()
    End Sub

    Sub autoNoPermintaan()
        Dim noPermintaanLab As String

        Try
            Call koneksiServer()
            Dim query As String
            query = "SELECT SUBSTR(noRegistrasiBDRSRanap,19,3) FROM t_registrasibdrsranap ORDER BY CAST(SUBSTR(noRegistrasiBDRSRanap,19,3) AS UNSIGNED) DESC LIMIT 1"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                dr.Read()
                noPermintaanLab = "RIBD" + Format(Now, "ddMMyyHHmmss") + "-" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                txtNoPermintaan.Text = noPermintaanLab
            Else
                noPermintaanLab = "RIBD" + Format(Now, "ddMMyyHHmmss") + "-1"
                txtNoPermintaan.Text = noPermintaanLab
            End If
            conn.Close()
        Catch ex As Exception

        End Try

    End Sub

    Sub autoNoTindakan()
        Dim noTindakanLab As String

        Try
            Call koneksiServer()
            Dim query As String
            query = "SELECT SUBSTR(noTindakanBDRSRanap,17,3) FROM t_tindakanbdrsranap ORDER BY CAST(SUBSTR(noTindakanBDRSRanap,18,3) AS UNSIGNED) DESC LIMIT 1"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                dr.Read()
                noTindakanLab = "TBD" + Format(Now, "ddMMyyHHmmss") + "-" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                txtNoTindakan.Text = noTindakanLab
            Else
                noTindakanLab = "TBD" + Format(Now, "ddMMyyHHmmss") + "-1"
                txtNoTindakan.Text = noTindakanLab
            End If
            conn.Close()
        Catch ex As Exception

        End Try

    End Sub

    Sub transferSelected()
        Call koneksiServer()
        Dim dr As New System.Windows.Forms.DataGridViewRow
        Dim newRow As DataRow

        For Each dr In Me.DataGridView1.SelectedRows
            newRow = ds.Tables(0).NewRow

            newRow.Item(0) = txtNoPermintaan.Text
            newRow.Item(1) = txtNoTindakan.Text
            newRow.Item(2) = dr.Cells(0).Value
            newRow.Item(3) = dr.Cells(2).Value
            newRow.Item(4) = dr.Cells(3).Value
            newRow.Item(5) = datePermintaan.Text
            newRow.Item(6) = txtKdDokter.Text
            newRow.Item(7) = txtDokter.Text
            newRow.Item(11) = "PERMINTAAN"

            ds.Tables(0).Rows.Add(newRow)
        Next

        conn.Close()
    End Sub

    Sub totalTarif()

        Call koneksiServer()
        Dim query As String
        Dim jum As Integer = 0
        Dim totTarif As Long = 0

        query = "SELECT COUNT(tindakan) as JUMLAH FROM vw_databdrspasienranap WHERE noRekamedis = '" & txtNoRM.Text & "'"
        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        While dr.Read
            jum = dr.GetString("JUMLAH")
        End While
        conn.Close()

        If jum = 0 Then
            For i As Integer = 0 To DataGridView2.Rows.Count - 1
                totTarif = totTarif + Val(DataGridView2.Rows(i).Cells(4).Value)
            Next
            txtTotalTarif.Text = totTarif
        Else
            For i As Integer = jum To DataGridView2.Rows.Count - 1
                totTarif = totTarif + Val(DataGridView2.Rows(i).Cells(4).Value)
            Next
            txtTotalTarif.Text = totTarif
        End If
    End Sub

    Sub addRegistrasiBDRS()
        Dim dt As String
        dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

        Call koneksiServer()
        Try
            Dim str As String
            str = "insert into t_registrasibdrsranap(noRegistrasiBDRSRanap,noDaftar,
                                                          kdUnitAsal,unitAsal,kdUnit,
                                                          unit,tglMasukBDRSRanap,statusBDRS,
                                                          kdDokterPengirim,ketKlinis) 
                   values ('" & txtNoPermintaan.Text & "','" & txtReg.Text & "','" & txtKdRuang.Text & "',
                           '" & txtRuang.Text & "','3004','Bank Darah','" & dt & "','PERMINTAAN',
                           '" & txtKdDokter.Text & "','" & txtKet.Text & "')"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Permintaan BDRS berhasil dilakukan", MsgBoxStyle.Information, "Information")
        Catch ex As Exception
            MsgBox("Insert data Permintaan BDRS gagal dilakukan.", MsgBoxStyle.Critical, "Error")
        End Try

        conn.Close()
    End Sub

    Sub addTindakan()
        Call koneksiServer()
        Try
            Dim str As String
            str = "insert into t_tindakanbdrsranap(noTindakanBDRSRanap,noRegistrasiBDRSRanap,
                                                        totalTindakanBDRSRanap) 
                   values ('" & txtNoTindakan.Text & "','" & txtNoPermintaan.Text & "','" & txtTotalTarif.Text & "')"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            'MsgBox("Insert Tindakan Lab berhasil dilakukan", MsgBoxStyle.Information, "Information")
        Catch ex As Exception
            MsgBox("Insert Tindakan BDRS gagal dilakukan.", MsgBoxStyle.Critical, "Error")
        End Try

        conn.Close()
    End Sub

    Sub addDetail()
        Call koneksiServer()
        Dim query As String
        Dim jum As Integer = 0

        query = "SELECT COUNT(tindakan) as JUMLAH FROM vw_databdrspasienranap WHERE noRekamedis = '" & txtNoRM.Text & "'"
        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        While dr.Read
            jum = dr.GetString("JUMLAH")
        End While
        conn.Close()

        Call koneksiServer()
        Dim val1, val2, val3, val4, val8 As String
        Dim str As String
        str = "insert into t_detailtindakanbdrsranap
                                    (noTindakanBDRSRanap,kdTarif,tindakan,tarif,
                                    jumlahTindakan,totalTarif) 
                   values (@noTindakanBDRSRanap,@kdTarif,@tindakan,@tarif,
                           '1',@totalTarif)"
        cmd = New MySqlCommand(str, conn)

        If jum = 0 Then
            Try
                For i As Integer = 0 To DataGridView2.Rows.Count - 1
                    val1 = DataGridView2.Rows(i).Cells(1).Value
                    val2 = DataGridView2.Rows(i).Cells(2).Value
                    val3 = DataGridView2.Rows(i).Cells(3).Value
                    val4 = DataGridView2.Rows(i).Cells(4).Value
                    val8 = DataGridView2.Rows(i).Cells(4).Value

                    cmd.Parameters.AddWithValue("@noTindakanBDRSRanap", val1)
                    cmd.Parameters.AddWithValue("@kdTarif", val2)
                    cmd.Parameters.AddWithValue("@tindakan", val3)
                    cmd.Parameters.AddWithValue("@tarif", val4)
                    cmd.Parameters.AddWithValue("@totalTarif", val8)
                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                Next
                'MsgBox("Insert data Detail Tindakan Lab berhasil dilakukan", MessageBoxIcon.Information, "Information")
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information, "Information")
                cmd.Dispose()
            End Try
        Else
            Try
                For i As Integer = jum To DataGridView2.Rows.Count - 1
                    val1 = DataGridView2.Rows(i).Cells(1).Value
                    val2 = DataGridView2.Rows(i).Cells(2).Value
                    val3 = DataGridView2.Rows(i).Cells(3).Value
                    val4 = DataGridView2.Rows(i).Cells(4).Value
                    val8 = DataGridView2.Rows(i).Cells(4).Value

                    cmd.Parameters.AddWithValue("@noTindakanBDRSRanap", val1)
                    cmd.Parameters.AddWithValue("@kdTarif", val2)
                    cmd.Parameters.AddWithValue("@tindakan", val3)
                    cmd.Parameters.AddWithValue("@tarif", val4)
                    cmd.Parameters.AddWithValue("@totalTarif", val8)
                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                Next
                'MsgBox("Insert data Detail Tindakan Lab berhasil dilakukan", MsgBoxStyle.Information, "Information")
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Information")
                cmd.Dispose()
            End Try
        End If
        conn.Close()

    End Sub

    Sub statusHasilLab()

        Try
            Call koneksiServer()
            Dim str As String
            str = "SELECT noRegistrasiBDRSRanap, nmPasien,
                   IF(noHasilPemeriksaanRanap IS NULL,'YES','NO') AS Status
                   FROM vw_cetakhasilbdrsranap
                   WHERE noRegistrasiBDRSRanap = '" & txtNoPerm.Text & "'"
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

    Private Sub BankDarah_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                Case "BDRS"
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
        Call totalTarif()

        txtKet.Select()
        txtKet.BackColor = Color.FromArgb(255, 112, 112)

        If DataGridView2.Rows.Count > 0 Then
            Me.btnSimpan.Enabled = True
        Else
            Me.btnSimpan.Enabled = False
        End If
    End Sub

    Private Sub DataGridView1_DataSourceChanged(sender As Object, e As EventArgs) Handles DataGridView1.DataSourceChanged
        Call dgv_styleRow()
    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub DataGridView2_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseClick
        Dim noPerm As String

        If e.RowIndex = -1 Then
            Return
        End If

        noPerm = DataGridView2.Rows(e.RowIndex).Cells(0).Value
        txtNoPerm.Text = noPerm

        Call statusHasilLab()
    End Sub

    Private Sub btnSimpan_MouseLeave(sender As Object, e As EventArgs) Handles btnSimpan.MouseLeave
        Me.btnSimpan.BackColor = Color.Navy
    End Sub

    Private Sub btnSimpan_MouseEnter(sender As Object, e As EventArgs) Handles btnSimpan.MouseEnter
        Me.btnSimpan.BackColor = Color.Blue
    End Sub

    Private Sub btnHasilLab_MouseLeave(sender As Object, e As EventArgs) Handles btnHasilLab.MouseLeave
        Me.btnHasilLab.BackColor = Color.Navy
    End Sub

    Private Sub btnHasilLab_MouseEnter(sender As Object, e As EventArgs) Handles btnHasilLab.MouseEnter
        Me.btnHasilLab.BackColor = Color.Blue
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
                    Call dgv_styleRow()
                    DataGridView2.Columns(4).DefaultCellStyle.Format = "###,###,###"
                    DataGridView2.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                End If
            End If
        End If
    End Sub
End Class