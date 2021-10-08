Imports MySql.Data.MySqlClient
Public Class Daftar_Pasien

    Public Ambil_Data As String
    Public Form_Ambil_Data As String
    Dim scrollVal As Integer

    Dim tglMrs, GtglMrs As Date
    Dim noRanap, noRm, namaPasien, tglDaftar, alamat, asalPasien, ranap, kelas, noKmr, noBed, tglKeluar, noDaftar, kdTarifBed As String
    Dim GnoReg, GnoRm, GnamaPasien, GtglLahir, Gjk, GtglMasuk, Gkelas, GnoKmr, GnoBed, Gdokter, GkdRanap As String

    Function cekStatusBed(ByVal id As String) As String
        Dim status As String = ""
        Call koneksiServer()
        Try
            Dim query As String
            query = "SELECT * FROM t_tarifkelaskamar WHERE kdTarifKelasKmr = '" & id & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                status = UCase(dr.GetString("kdStatusBed"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()

        Return status
    End Function

    Sub tampilAllPasien()
        Call koneksiServer()
        Dim query As String
        'Dim cmd As MySqlCommand
        'Dim dr As MySqlDataReader
        query = "CALL infopxranap()"
        Try
            da = New MySqlDataAdapter(query, conn)
            ds = New DataSet
            da.Fill(ds, scrollVal, Val(TextBox2.Text), 0)
            DataGridView3.DataSource = ds.Tables(0)
            DataGridView3.ReadOnly = True

            cmd = New MySqlCommand("SELECT COUNT(*) FROM t_registrasirawatinap", conn)
            dr = cmd.ExecuteReader
            dr.Read()
            Label5.Text = dr(0)

            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub tampilDataPasien()
        Call koneksiServer()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        query = "CALL daftarpxranap('" & txtRanap.Text & "%')"
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            DataGridView1.Rows.Clear()

            Do While dr.Read
                DataGridView1.Rows.Add(dr.Item("noDaftarRawatInap"), dr.Item("noRekamedis"), dr.Item("nmPasien"),
                                       dr.Item("alamat"), dr.Item("rawatInap"), dr.Item("kelas"),
                                       dr.Item("noKamar"), dr.Item("noBed"), dr.Item("asalPasien"),
                                       dr.Item("tglMasukRawatInap"), dr.Item("asalUnit"))
            Loop
            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub tampilPasienGizi()
        Call koneksiServer()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        query = "CALL daftarpxranap('" & txtRanap.Text & "%')"
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            DataGridView4.Rows.Clear()

            Do While dr.Read
                DataGridView4.Rows.Add(dr.Item("noDaftarRawatInap"), dr.Item("noRekamedis"), dr.Item("nmPasien"),
                                       dr.Item("tglLahir"), dr.Item("jenisKelamin"), dr.Item("tglMasukRawatInap"),
                                       dr.Item("kelas"), dr.Item("noKamar"), dr.Item("noBed"),
                                       dr.Item("namapetugasMedis"), dr.Item("kdRawatInap"), dr.Item("rawatInap"),
                                       dr.Item("asalUnit"))
            Loop
            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        'Call koneksiServer()
        'Dim sql As String = "SELECT noDaftarRawatInap, noRekamedis, nmPasien, DATE_FORMAT(tglLahir,'%d/%m/%Y') AS tglLahir, jenisKelamin, tglMasukRawatInap, kelas, noKamar,
        '                            noBed, namapetugasMedis, kdRawatInap, rawatInap, asalUnit
        '                       FROM vw_pasienrawatinap
        '                      WHERE tglKeluarRawatInap IS NULL AND rawatInap LIKE '" & txtRanap.Text & "%'"
        'Try
        '    da = New MySqlDataAdapter(sql, conn)
        '    ds = New DataSet
        '    da.Fill(ds, "vw_pasienrawatinap")
        '    DataGridView1.DataSource = ds.Tables("vw_pasienrawatinap")
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
        'conn.Close()
    End Sub

    Sub tampilPasienKeluar()
        Call koneksiServer()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        query = "CALL daftarpxkeluarranap('" & txtRanap.Text & "%')"
        Try
            da = New MySqlDataAdapter(query, conn)
            ds = New DataSet
            da.Fill(ds, scrollVal, Val(TextBox2.Text), 0)
            DataGridView2.DataSource = ds.Tables(0)
            DataGridView2.ReadOnly = True

            cmd = New MySqlCommand("SELECT COUNT(*) FROM t_registrasirawatinap WHERE rawatInap LIKE '" & txtRanap.Text & "%'", conn)
            dr = cmd.ExecuteReader
            dr.Read()
            Label5.Text = dr(0)

            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Function cekJmlRuang(noReg As String) As String
        Call koneksiServer()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim jml As String = ""
        query = "SELECT COUNT(tglMasukRawatInap) AS jml
		           FROM vw_daftarruangakomodasi
	              WHERE noDaftar = '" & noReg & "'"
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                jml = dr.Item("jml").ToString
            End If
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()

        Return jml
    End Function

    Sub tampilTextBoxPasien()
        Dim noRM As String = ""

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Daftar Pasien Keluar"
                    If DataGridView2.CurrentRow Is Nothing Then Exit Sub
                    Dim i As Integer = DataGridView2.CurrentRow.Index
                    noRM = DataGridView2.Item(1, i).Value
                Case Else
                    If DataGridView1.CurrentRow Is Nothing Then Exit Sub
                    Dim i As Integer = DataGridView1.CurrentRow.Index
                    noRM = DataGridView1.Item(1, i).Value
            End Select
        End If

        Try
            Call koneksiServer()
            Dim str As String
            str = "CALL datatiappxranap('" & ranap & "','" & noRM & "','" & Format(tglMrs, "yyyy-MM-dd HH:mm:ss") & "')"
            cmd = New MySqlCommand(str, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                Form1.txtJk.Text = dr.Item("jenisKelamin").ToString
                Form1.dateLahir.Text = dr.Item("tglLahir").ToString
                Form1.dateDaftar.Text = dr.Item("tglDaftar").ToString
                Form1.txtProv.Text = dr.Item("provinsi").ToString
                Form1.txtKabKot.Text = dr.Item("kabupaten").ToString
                Form1.txtKec.Text = dr.Item("kecamatan").ToString
                Form1.txtKel.Text = dr.Item("kelurahan").ToString
                Form1.txtNoDaftar.Text = dr.Item("noDaftar").ToString
                Form1.txtDokter.Text = dr.Item("namapetugasMedis").ToString
                Form1.txtKdTarifKlsKmr.Text = dr.Item("kdTarifKelasKmr").ToString
                Form1.txtTarifKmr.Text = dr.Item("tarifKmr").ToString
                Form1.txtCaraBayar.Text = dr.Item("carabayar").ToString
                Form1.txtPenjamin.Text = dr.Item("penjamin").ToString
                Form1.txtTglMasuk.Text = dr.Item("tglMasukRawatInap").ToString
                Form1.txtKdUnitRanap.Text = dr.Item("kdRawatInap").ToString
                Form1.txtKdDokter.Text = dr.Item("kdTenagaMedis").ToString
                Form1.txtJumInap.Text = dr.Item("jumlah").ToString & " Hari"
                Form1.comboDokter.Text = Form1.txtDokter.Text
                txtKdTarifKelas.Text = Form1.txtKdTarifKlsKmr.Text
                txtNoReg.Text = dr.Item("noDaftar").ToString

                If Form1.comboDokter.Text = "-" Or Form1.comboDokter.Text = "" Then
                    Form1.ErrorProvider1.SetError(Form1.comboDokter, "Tolong diinputkan telebih dahulu")
                    'MsgBox("DR KOSONG")
                Else
                    Form1.ErrorProvider1.Clear()
                    'MsgBox("DR ADA")
                End If
            End If
        Catch ex As Exception
        End Try
        conn.Close()
    End Sub
    Sub updateBatalCOReg()
        Call koneksiServer()

        Try
            Dim str As String
            str = "UPDATE t_registrasi 
                      SET tglPulang = NULL,
                          kdStatusKeluar = 8,
                          kdCaraKeluar = 6
                    WHERE noDaftar = '" & txtNoReg.Text & "'"

            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            'MsgBox("Update data Registrasi Pemeriksaan Lab berhasil dilakukan", MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox("Update data Registrasi gagal dilakukan.", MessageBoxIcon.Error, "Error Registrasi Ranap")
        End Try

        conn.Close()
    End Sub

    Sub updateBatalCheckout()
        Call koneksiServer()

        Try
            Dim str As String
            str = "UPDATE t_registrasirawatinap 
                      SET tglKeluarRawatInap = NULL
                    WHERE noDaftarRawatInap = '" & txtNoDaftarRanap.Text & "'"

            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            'MsgBox("Update data Registrasi Pemeriksaan Lab berhasil dilakukan", MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox("Update data Registrasi gagal dilakukan.", MessageBoxIcon.Error, "Error Registrasi Ranap")
        End Try

        conn.Close()
    End Sub

    Sub updateStatusBed()
        Try
            Call koneksiServer()
            Dim str As String
            str = "UPDATE t_tarifkelaskamar SET kdStatusBed = 'st5' 
                                          WHERE kdTarifKelasKmr = '" & txtKdTarifKelas.Text & "'"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Daftar Pasien"
                    Call caridata(TextBox1.Text)
                Case "Daftar Pasien Keluar"
                    'Call caridataBatal(TextBox1.Text)
                Case "Info Pasien"
                    'Call caridataAll()
                    'Call aturDGVAll()
                Case "Pasien Gizi"
                    Call caridata(TextBox1.Text)
                    Call aturDGVGizi()
            End Select
        End If
    End Sub

    Private Sub btnRekap_Click(sender As Object, e As EventArgs) Handles btnRekap.Click
        'Daftar_Bukti_Layanan.Ambil_Data = True
        'Daftar_Bukti_Layanan.Form_Ambil_Data = "Riwayat Layanan"
        'Daftar_Bukti_Layanan.Show()
        Info_Biaya_Perawatan.Ambil_Data = True
        Info_Biaya_Perawatan.Form_Ambil_Data = "Riwayat Layanan"
        Info_Biaya_Perawatan.Show()
        Me.Hide()
    End Sub

    Sub aturDGVAll()
        DataGridView3.Columns(0).Width = 150
        DataGridView3.Columns(1).Width = 100
        DataGridView3.Columns(2).Width = 200
        DataGridView3.Columns(3).Width = 300
        DataGridView3.Columns(4).Width = 200
        DataGridView3.Columns(5).Width = 80
        DataGridView3.Columns(6).Width = 80
        DataGridView3.Columns(7).Width = 80
        DataGridView3.Columns(8).Width = 150
        DataGridView3.Columns(9).Width = 150
        DataGridView3.Columns(10).Width = 150
        DataGridView3.Columns(0).HeaderText = "NO.REGISTRASI"
        DataGridView3.Columns(1).HeaderText = "NO.RM"
        DataGridView3.Columns(2).HeaderText = "NAMA PASIEN"
        DataGridView3.Columns(3).HeaderText = "ALAMAT"
        DataGridView3.Columns(4).HeaderText = "R.RAWAT INAP"
        DataGridView3.Columns(5).HeaderText = "KELAS"
        DataGridView3.Columns(6).HeaderText = "NO.KAMAR"
        DataGridView3.Columns(7).HeaderText = "NO.BED"
        DataGridView3.Columns(8).HeaderText = "ASAL PASIEN"
        DataGridView3.Columns(9).HeaderText = "TGL.MASUK"
        DataGridView3.Columns(10).HeaderText = "TGL.KELUAR"

        DataGridView3.Columns(0).Visible = False
    End Sub

    Sub aturDGV()
        DataGridView1.Columns(0).Width = 150
        DataGridView1.Columns(1).Width = 90
        DataGridView1.Columns(2).Width = 200
        DataGridView1.Columns(3).Width = 300
        DataGridView1.Columns(4).Width = 150
        DataGridView1.Columns(5).Width = 100
        DataGridView1.Columns(6).Width = 100
        DataGridView1.Columns(7).Width = 100
        DataGridView1.Columns(8).Width = 100
        DataGridView1.Columns(9).Width = 150
        DataGridView1.Columns(10).Width = 150
        DataGridView1.Columns(0).HeaderText = "NO.REGISTRASI"
        DataGridView1.Columns(1).HeaderText = "NO.RM"
        DataGridView1.Columns(2).HeaderText = "NAMA PASIEN"
        DataGridView1.Columns(3).HeaderText = "ALAMAT"
        DataGridView1.Columns(4).HeaderText = "R. RAWAT INAP"
        DataGridView1.Columns(5).HeaderText = "KELAS"
        DataGridView1.Columns(6).HeaderText = "NO.KAMAR"
        DataGridView1.Columns(7).HeaderText = "NO.BED"
        DataGridView1.Columns(8).HeaderText = "ASAL PASIEN"
        DataGridView1.Columns(9).HeaderText = "TGL MASUK"
        DataGridView1.Columns(10).HeaderText = "ASAL UNIT"
    End Sub

    Sub aturDGVKeluar()
        DataGridView2.Columns(0).Width = 150
        DataGridView2.Columns(1).Width = 150
        DataGridView2.Columns(2).Width = 200
        DataGridView2.Columns(3).Width = 300
        DataGridView2.Columns(4).Width = 150
        DataGridView2.Columns(5).Width = 100
        DataGridView2.Columns(6).Width = 100
        DataGridView2.Columns(7).Width = 100
        DataGridView2.Columns(8).Width = 150
        DataGridView2.Columns(9).Width = 150
        DataGridView2.Columns(10).Width = 150
        DataGridView2.Columns(11).Width = 100
        DataGridView2.Columns(12).Width = 50
        DataGridView2.Columns(0).HeaderText = "NO.REGISTRASI"
        DataGridView2.Columns(1).HeaderText = "NO.RM"
        DataGridView2.Columns(2).HeaderText = "NAMA PASIEN"
        DataGridView2.Columns(3).HeaderText = "ALAMAT"
        DataGridView2.Columns(4).HeaderText = "R. RAWAT INAP"
        DataGridView2.Columns(5).HeaderText = "KELAS"
        DataGridView2.Columns(6).HeaderText = "NO.KAMAR"
        DataGridView2.Columns(7).HeaderText = "NO.BED"
        DataGridView2.Columns(8).HeaderText = "ASAL PASIEN"
        DataGridView2.Columns(9).HeaderText = "TGL MASUK"
        DataGridView2.Columns(10).HeaderText = "TGL KELUAR"
        DataGridView2.Columns(11).HeaderText = "NO.DAFTAR"
        DataGridView2.Columns(12).HeaderText = "KODE BED"

        DataGridView2.Columns(11).Visible = False
        DataGridView2.Columns(12).Visible = False
    End Sub

    Sub aturDGVGizi()
        Try
            DataGridView1.Columns(0).FillWeight = 145
            DataGridView1.Columns(1).FillWeight = 85
            DataGridView1.Columns(2).FillWeight = 200
            DataGridView1.Columns(3).FillWeight = 100
            DataGridView1.Columns(4).FillWeight = 50
            DataGridView1.Columns(5).FillWeight = 150
            DataGridView1.Columns(6).FillWeight = 80
            DataGridView1.Columns(7).FillWeight = 80
            DataGridView1.Columns(8).FillWeight = 80
            DataGridView1.Columns(9).FillWeight = 200
            DataGridView1.Columns(10).FillWeight = 50
            DataGridView1.Columns(0).HeaderText = "No.Ranap"
            DataGridView1.Columns(1).HeaderText = "No.RM"
            DataGridView1.Columns(2).HeaderText = "Nama Pasien"
            DataGridView1.Columns(3).HeaderText = "Tgl.Lahir"
            DataGridView1.Columns(4).HeaderText = "Jenis Kelamin"
            DataGridView1.Columns(5).HeaderText = "Tgl.Masuk"
            DataGridView1.Columns(6).HeaderText = "Kelas"
            DataGridView1.Columns(7).HeaderText = "No.Kamar"
            DataGridView1.Columns(8).HeaderText = "No.Bed"
            DataGridView1.Columns(9).HeaderText = "Dokter Penanggung Jawab"
            DataGridView1.Columns(10).HeaderText = "KD.RANAP"
            DataGridView1.Columns(11).HeaderText = "Ruang"
            DataGridView1.Columns(12).HeaderText = "Asal Ruang"

            DataGridView1.Columns(10).Visible = False

            DataGridView1.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView1.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView1.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView1.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView1.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView1.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView1.ColumnHeadersDefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
            DataGridView1.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
            DataGridView1.DefaultCellStyle.ForeColor = Color.Black
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        Catch ex As Exception

        End Try
    End Sub

    Sub caridataAll()
        Call koneksiServer()
        Dim query As String
        Dim cmd As MySqlCommand
        'Dim dr As MySqlDataReader
        Dim da As MySqlDataAdapter
        Dim ds As DataSet
        query = "CALL infocaripxranap('" & TextBox1.Text & "')"

        cmd = New MySqlCommand(query, conn)
        da = New MySqlDataAdapter(cmd)
        ds = New DataSet
        da.Fill(ds)
        DataGridView3.DataSource = ds.Tables(0)
    End Sub

    Sub caridataBatal(nama As String)
        Call koneksiServer()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim ds As DataSet
        query = "CALL caripxkeluarranap('" & txtRanap.Text & "%','" & nama & "')"

        cmd = New MySqlCommand(query, conn)
        da = New MySqlDataAdapter(cmd)
        ds = New DataSet
        da.Fill(ds)
        DataGridView2.DataSource = ds.Tables(0)

        'Call koneksiServer()
        'Dim query As String
        'Dim cmd As MySqlCommand
        'Dim dr As MySqlDataReader
        'query = "CALL caripxkeluarranap('" & txtRanap.Text & "%','" & nama & "')"
        'Try
        '    cmd = New MySqlCommand(query, conn)
        '    dr = cmd.ExecuteReader
        '    DataGridView2.Rows.Clear()

        '    Do While dr.Read
        '        DataGridView2.Rows.Add(dr.Item("noDaftarRawatInap"), dr.Item("noRekamedis"), dr.Item("nmPasien"),
        '                               dr.Item("alamat"), dr.Item("rawatInap"), dr.Item("kelas"),
        '                               dr.Item("noKamar"), dr.Item("noBed"), dr.Item("asalPasien"),
        '                               dr.Item("tglMasukRawatInap"), dr.Item("tglKeluarRawatInap"),
        '                               dr.Item("noDaftar"), dr.Item("kdTarifKelasKmr"))
        '    Loop
        '    dr.Close()
        '    conn.Close()
        'Catch ex As Exception
        '    MsgBox(ex.ToString)
        'End Try
    End Sub

    Sub caridata(nama As String)
        Call koneksiServer()
        Dim dt As New DataTable
        Dim dr As MySqlDataReader
        Dim cmd As MySqlCommand
        Dim query As String

        query = "CALL caripxranap('" & txtRanap.Text & "','" & nama & "')"
        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        DataGridView1.Rows.Clear()

        Do While dr.Read
            DataGridView1.Rows.Add(dr.Item("noDaftarRawatInap"), dr.Item("noRekamedis"), dr.Item("nmPasien"),
                                   dr.Item("alamat"), dr.Item("rawatInap"), dr.Item("kelas"),
                                   dr.Item("noKamar"), dr.Item("noBed"), dr.Item("asalPasien"),
                                   dr.Item("tglMasukRawatInap"), dr.Item("asalUnit"))
        Loop
        dr.Close()
        conn.Close()
    End Sub

    Private Sub Daftar_Pasien_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.Manual
        With Screen.PrimaryScreen.WorkingArea
            Me.SetBounds(.Left, .Top, .Width, .Height)
        End With

        TextBox1.Select()
        DataGridView1.Visible = False
        DataGridView2.Visible = False
        DataGridView3.Visible = False
        DataGridView4.Visible = False

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Daftar Pasien"
                    Label1.Text = "DAFTAR PASIEN - RUANG " & Form1.Label1.Text.ToUpper
                    txtRanap.Text = Form1.Label1.Text
                    DataGridView1.Visible = True
                    Call tampilDataPasien()
                    'Call aturDGV()
                Case "Daftar Pasien Keluar"
                    Label1.Text = "DAFTAR PASIEN - RUANG " & Form1.Label1.Text.ToUpper
                    txtRanap.Text = Form1.Label1.Text
                    DataGridView2.Visible = True
                    Call tampilPasienKeluar()
                    Call aturDGVKeluar()
                    btnOK.Visible = False
                    btnBatalOut.Visible = True

                    Button1.Visible = True
                    Button2.Visible = True
                    TextBox2.Visible = True
                    Label4.Visible = True
                    'Label5.Visible = True
                Case "Info Pasien"
                    DataGridView3.Visible = True
                    Call tampilAllPasien()
                    Call aturDGVAll()

                    btnRekap.Visible = True
                    btnOK.Visible = False
                    btnBatalOut.Visible = False

                    Button1.Visible = True
                    Button2.Visible = True
                    TextBox2.Visible = True
                    Label4.Visible = True
                    'Label5.Visible = True
                Case "Pasien Gizi"
                    Me.TopMost = True
                    Label1.Text = "DAFTAR PASIEN - RUANG " & Form1.Label1.Text.ToUpper
                    txtRanap.Text = Gizi.txtRanap.Text
                    DataGridView4.Visible = True
                    Call tampilPasienGizi()
                    'Call aturDGVGizi()
            End Select
        End If


        Me.KeyPreview = True
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox1.Text = "" Then
                If Ambil_Data = True Then
                    Select Case Form_Ambil_Data
                        Case "Daftar Pasien"
                            Call tampilDataPasien()
                            'Call aturDGV()
                        Case "Daftar Pasien Keluar"
                            Call tampilPasienKeluar()
                            Call aturDGVKeluar()
                        Case "Info Pasien"
                            Call tampilAllPasien()
                            Call aturDGVAll()
                        Case "Pasien Gizi"
                            Call tampilDataPasien()
                            Call aturDGVGizi()
                    End Select
                End If
            Else
                If Ambil_Data = True Then
                    Select Case Form_Ambil_Data
                        Case "Daftar Pasien"
                            Call caridata(TextBox1.Text)
                            'Call aturDGV()
                        Case "Daftar Pasien Keluar"
                            Call caridataBatal(TextBox1.Text)
                            Call aturDGVKeluar()
                        Case "Info Pasien"
                            Call caridataAll()
                            Call aturDGVAll()
                        Case "Pasien Gizi"
                            Call caridata(TextBox1.Text)
                            Call aturDGVGizi()
                    End Select
                End If
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick

        If e.RowIndex = -1 Then
            Return
        End If

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Daftar Pasien"
                    noRanap = DataGridView1.Rows(e.RowIndex).Cells(0).Value
                    noRm = DataGridView1.Rows(e.RowIndex).Cells(1).Value
                    namaPasien = DataGridView1.Rows(e.RowIndex).Cells(2).Value
                    alamat = DataGridView1.Rows(e.RowIndex).Cells(3).Value
                    ranap = DataGridView1.Rows(e.RowIndex).Cells(4).Value
                    kelas = DataGridView1.Rows(e.RowIndex).Cells(5).Value
                    noKmr = DataGridView1.Rows(e.RowIndex).Cells(6).Value
                    noBed = DataGridView1.Rows(e.RowIndex).Cells(7).Value
                    asalPasien = DataGridView1.Rows(e.RowIndex).Cells(8).Value
                    tglDaftar = DataGridView1.Rows(e.RowIndex).Cells(9).Value
                    tglMrs = DataGridView1.Rows(e.RowIndex).Cells(9).Value

                    txtNoDaftarRanap.Text = noRanap
                    Form1.txtRegRanap.Text = noRanap
                    Form1.txtRekMed.Text = noRm
                    Form1.txtNamaPasien.Text = namaPasien
                    Form1.dateDaftar.Text = tglDaftar
                    Form1.txtAlamat.Text = alamat
                    Form1.txtAsalPasien.Text = asalPasien
                    Form1.txtUnitRanap.Text = ranap
                    Form1.txtKelas.Text = kelas
                    Form1.txtNoKmr.Text = noKmr
                    Form1.txtNoBed.Text = noBed

                    If Form1.txtAlamat.Text.Count >= 25 Then
                        Form1.txtAlamat.Font = New Font("Microsoft Sans Serif", 10.5, FontStyle.Bold)
                    End If

                    Call tampilTextBoxPasien()
                    Me.Close()
                    Form1.Show()
            End Select
        End If
    End Sub

    Private Sub DataGridView4_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView4.CellMouseDoubleClick

        If e.RowIndex = -1 Then
            Return
        End If

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Pasien Gizi"
                    GnoReg = DataGridView4.Rows(e.RowIndex).Cells(0).Value
                    GnoRm = DataGridView4.Rows(e.RowIndex).Cells(1).Value
                    GnamaPasien = DataGridView4.Rows(e.RowIndex).Cells(2).Value
                    GtglLahir = DataGridView4.Rows(e.RowIndex).Cells(3).Value
                    Gjk = DataGridView4.Rows(e.RowIndex).Cells(4).Value
                    GtglMasuk = DataGridView4.Rows(e.RowIndex).Cells(5).Value
                    GtglMrs = DataGridView4.Rows(e.RowIndex).Cells(5).Value
                    Gkelas = DataGridView4.Rows(e.RowIndex).Cells(6).Value
                    GnoKmr = DataGridView4.Rows(e.RowIndex).Cells(7).Value
                    GnoBed = DataGridView4.Rows(e.RowIndex).Cells(8).Value
                    Gdokter = DataGridView4.Rows(e.RowIndex).Cells(9).Value
                    GkdRanap = DataGridView4.Rows(e.RowIndex).Cells(10).Value

                    Gizi.txtNoReg.Text = GnoReg
                    Gizi.txtNama.Text = GnamaPasien
                    Gizi.txtTglLahir.Text = GtglLahir
                    Gizi.txtJk.Text = Gjk
                    Gizi.txtTglMasuk.Text = GtglMasuk
                    'Gizi.txtKelas.Text = Gkelas
                    'Gizi.txtNoKmr.Text = GnoKmr
                    'Gizi.txtNoBed.Text = GnoBed
                    Gizi.txtDokter.Text = Gdokter
                    Gizi.txtKdRanap.Text = GkdRanap

                    Dim dt As DateTime = Convert.ToDateTime(GtglLahir)
                    Dim cul As IFormatProvider = New System.Globalization.CultureInfo("id-ID", True)
                    Dim dt1 As DateTime = DateTime.Parse(GtglLahir, cul, System.Globalization.DateTimeStyles.AssumeLocal)
                    Gizi.txtUmur.Text = Gizi.hitungUmur(dt1.ToShortDateString)

                    'Call tampilTextBoxPasien()

                    Me.Dispose()
                    Me.Close()
                    Gizi.Show()
            End Select
        End If
    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Daftar Pasien"
                    Me.Close()
                    Form1.Show()
                Case "Daftar Pasien Keluar"
                    Me.Close()
                    Form1.Show()
                Case "Info Pasien"
                    Me.Close()
                    Form1.Show()
                Case "Pasien Gizi"
                    Me.Close()
                    Gizi.Show()
            End Select
        End If
    End Sub

    Private Sub Daftar_Pasien_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F11
                Me.Close()
                Form1.Show()
            Case Keys.F12
                Me.Close()
                Form1.Show()
        End Select
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Daftar Pasien"
                    txtNoDaftarRanap.Text = noRanap
                    Form1.txtRegRanap.Text = noRanap
                    Form1.txtRekMed.Text = noRm
                    Form1.txtNamaPasien.Text = namaPasien
                    Form1.dateDaftar.Text = tglDaftar
                    Form1.txtAlamat.Text = alamat
                    Form1.txtAsalPasien.Text = asalPasien
                    Form1.txtUnitRanap.Text = ranap
                    Form1.txtKelas.Text = kelas
                    Form1.txtNoKmr.Text = noKmr
                    Form1.txtNoBed.Text = noBed

                    If Form1.txtAlamat.Text.Count >= 25 Then
                        Form1.txtAlamat.Font = New Font("Microsoft Sans Serif", 10.5, FontStyle.Bold)
                    End If

                    Call tampilTextBoxPasien()
                    Me.Close()
                    Form1.Show()
                Case "Pasien Gizi"
                    Gizi.txtNoReg.Text = GnoReg
                    Gizi.txtNama.Text = GnamaPasien
                    Gizi.txtTglLahir.Text = GtglLahir
                    Gizi.txtJk.Text = Gjk
                    Gizi.txtTglMasuk.Text = GtglMasuk
                    'Gizi.txtKelas.Text = Gkelas
                    'Gizi.txtNoKmr.Text = GnoKmr
                    'Gizi.txtNoBed.Text = GnoBed
                    Gizi.txtDokter.Text = Gdokter
                    Gizi.txtKdRanap.Text = GkdRanap

                    Dim dt As DateTime = Convert.ToDateTime(GtglLahir)
                    Dim cul As IFormatProvider = New System.Globalization.CultureInfo("id-ID", True)
                    Dim dt1 As DateTime = DateTime.Parse(GtglLahir, cul, System.Globalization.DateTimeStyles.AssumeLocal)
                    Gizi.txtUmur.Text = Gizi.hitungUmur(dt1.ToShortDateString)

                    'Call tampilTextBoxPasien()
                    Me.Dispose()
                    Me.Close()
                    Gizi.Show()
            End Select
        End If
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown

        If DataGridView1.Rows.Count = 0 Then
            Return
        Else
            If e.KeyCode = Keys.Enter And DataGridView1.CurrentCell.RowIndex >= 0 Then
                e.Handled = True
                e.SuppressKeyPress = True

                Dim row As DataGridViewRow
                row = Me.DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex)

                If DataGridView1.CurrentCell.RowIndex = -1 Then
                    Return
                End If

                If Ambil_Data = True Then
                    Select Case Form_Ambil_Data
                        Case "Daftar Pasien"
                            noRanap = row.Cells(0).Value
                            noRm = row.Cells(1).Value
                            namaPasien = row.Cells(2).Value
                            alamat = row.Cells(3).Value
                            ranap = row.Cells(4).Value
                            kelas = row.Cells(5).Value
                            noKmr = row.Cells(6).Value
                            noBed = row.Cells(7).Value
                            asalPasien = row.Cells(8).Value
                            tglDaftar = row.Cells(9).Value
                            tglMrs = row.Cells(9).Value

                            Form1.txtRegRanap.Text = noRanap
                            Form1.txtRekMed.Text = noRm
                            Form1.txtNamaPasien.Text = namaPasien
                            Form1.dateDaftar.Text = tglDaftar
                            Form1.txtAlamat.Text = alamat
                            Form1.txtAsalPasien.Text = asalPasien
                            Form1.txtUnitRanap.Text = ranap
                            Form1.txtKelas.Text = kelas
                            Form1.txtNoKmr.Text = noKmr
                            Form1.txtNoBed.Text = noBed

                            If Form1.txtAlamat.Text.Count >= 25 Then
                                Form1.txtAlamat.Font = New Font("Microsoft Sans Serif", 10.5, FontStyle.Bold)
                            End If

                            Call tampilTextBoxPasien()

                            Me.Close()
                            Form1.Show()
                    End Select
                End If
            End If
        End If
    End Sub

    Private Sub DataGridView2_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView2.KeyDown
        If DataGridView2.Rows.Count = 0 Then
            Return
        Else
            If e.KeyCode = Keys.Enter And DataGridView2.CurrentCell.RowIndex >= 0 Then
                e.Handled = True
                e.SuppressKeyPress = True

                Dim row As DataGridViewRow
                row = Me.DataGridView2.Rows(DataGridView2.CurrentCell.RowIndex)

                If DataGridView2.CurrentCell.RowIndex = -1 Then
                    Return
                End If

                If Ambil_Data = True Then
                    Select Case Form_Ambil_Data
                        Case "Daftar Pasien Keluar"
                            noRanap = row.Cells(0).Value
                            namaPasien = row.Cells(2).Value
                            noDaftar = row.Cells(11).Value
                            kdTarifBed = row.Cells(12).Value
                            kelas = row.Cells(5).Value
                            Call tampilTextBoxPasien()
                            txtNoReg.Text = noDaftar
                            txtNoDaftarRanap.Text = noRanap
                            txtKdTarifKelas.Text = kdTarifBed
                            txtKelas.Text = kelas
                            btnBatalOut.Enabled = True
                            btnBatalOut.Visible = True
                    End Select
                End If
            End If
        End If
    End Sub

    Private Sub DataGridView4_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView4.KeyDown

        If DataGridView4.Rows.Count = 0 Then
            Return
        Else
            If e.KeyCode = Keys.Enter And DataGridView4.CurrentCell.RowIndex >= 0 Then
                e.Handled = True
                e.SuppressKeyPress = True

                Dim row As DataGridViewRow
                row = Me.DataGridView4.Rows(DataGridView4.CurrentCell.RowIndex)

                If DataGridView4.CurrentCell.RowIndex = -1 Then
                    Return
                End If

                If Ambil_Data = True Then
                    Select Case Form_Ambil_Data
                        Case "Pasien Gizi"
                            GnoReg = row.Cells(0).Value
                            GnoRm = row.Cells(1).Value
                            GnamaPasien = row.Cells(2).Value
                            GtglLahir = row.Cells(3).Value
                            Gjk = row.Cells(4).Value
                            GtglMasuk = row.Cells(5).Value
                            Gkelas = row.Cells(6).Value
                            GnoKmr = row.Cells(7).Value
                            GnoBed = row.Cells(8).Value
                            Gdokter = row.Cells(9).Value
                            GkdRanap = row.Cells(10).Value

                            Gizi.txtNoReg.Text = GnoReg
                            Gizi.txtNama.Text = GnamaPasien
                            Gizi.txtTglLahir.Text = GtglLahir
                            Gizi.txtJk.Text = Gjk
                            Gizi.txtTglMasuk.Text = GtglMasuk
                            'Gizi.txtKelas.Text = Gkelas
                            'Gizi.txtNoKmr.Text = GnoKmr
                            'Gizi.txtNoBed.Text = GnoBed
                            Gizi.txtDokter.Text = Gdokter
                            Gizi.txtKdRanap.Text = GkdRanap

                            Dim dt As DateTime = Convert.ToDateTime(GtglLahir)
                            Dim cul As IFormatProvider = New System.Globalization.CultureInfo("id-ID", True)
                            Dim dt1 As DateTime = DateTime.Parse(GtglLahir, cul, System.Globalization.DateTimeStyles.AssumeLocal)
                            Gizi.txtUmur.Text = Gizi.hitungUmur(dt1.ToShortDateString)

                            'Call tampilTextBoxPasien()
                            Me.Dispose()
                            Me.Close()
                            Gizi.Show()
                    End Select
                End If
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex = -1 Then
            Return
        End If

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Daftar Pasien"
                    noRanap = DataGridView1.Rows(e.RowIndex).Cells(0).Value
                    noRm = DataGridView1.Rows(e.RowIndex).Cells(1).Value
                    namaPasien = DataGridView1.Rows(e.RowIndex).Cells(2).Value
                    alamat = DataGridView1.Rows(e.RowIndex).Cells(3).Value
                    ranap = DataGridView1.Rows(e.RowIndex).Cells(4).Value
                    kelas = DataGridView1.Rows(e.RowIndex).Cells(5).Value
                    noKmr = DataGridView1.Rows(e.RowIndex).Cells(6).Value
                    noBed = DataGridView1.Rows(e.RowIndex).Cells(7).Value
                    asalPasien = DataGridView1.Rows(e.RowIndex).Cells(8).Value
                    tglDaftar = DataGridView1.Rows(e.RowIndex).Cells(9).Value
                    tglMrs = DataGridView1.Rows(e.RowIndex).Cells(9).Value
            End Select
        End If
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        If e.RowIndex = -1 Then
            Return
        End If

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Daftar Pasien Keluar"
                    noRanap = DataGridView2.Rows(e.RowIndex).Cells(0).Value
                    namaPasien = DataGridView2.Rows(e.RowIndex).Cells(2).Value
                    ranap = DataGridView2.Rows(e.RowIndex).Cells(4).Value
                    noDaftar = DataGridView2.Rows(e.RowIndex).Cells(11).Value
                    kdTarifBed = DataGridView2.Rows(e.RowIndex).Cells(12).Value
                    kelas = DataGridView2.Rows(e.RowIndex).Cells(5).Value
                    Call tampilTextBoxPasien()
                    txtNoReg.Text = noDaftar
                    txtNoDaftarRanap.Text = noRanap
                    txtRanap.Text = ranap
                    txtKdTarifKelas.Text = kdTarifBed
                    txtKelas.Text = kelas
                    btnBatalOut.Enabled = True
            End Select
        End If

    End Sub

    Private Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick
        If e.RowIndex = -1 Then
            Return
        End If

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Info Pasien"
                    noRanap = DataGridView3.Rows(e.RowIndex).Cells(0).Value
                    noRm = DataGridView3.Rows(e.RowIndex).Cells(1).Value
                    namaPasien = DataGridView3.Rows(e.RowIndex).Cells(2).Value
                    ranap = DataGridView3.Rows(e.RowIndex).Cells(4).Value
                    txtKelas.Text = DataGridView3.Rows(e.RowIndex).Cells(5).Value
                    tglDaftar = DataGridView3.Rows(e.RowIndex).Cells(9).Value
                    tglMrs = DataGridView3.Rows(e.RowIndex).Cells(9).Value
                    txtNoRM.Text = noRm
                    txtNamaPx.Text = namaPasien
                    txtNoDaftarRanap.Text = noRanap
                    txtRanap.Text = ranap
                    txtTglRanap.Text = tglDaftar
            End Select
        End If

    End Sub

    Private Sub DataGridView4_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView4.CellContentClick
        If e.RowIndex = -1 Then
            Return
        End If

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Pasien Gizi"
                    GnoReg = DataGridView4.Rows(e.RowIndex).Cells(0).Value
                    GnoRm = DataGridView4.Rows(e.RowIndex).Cells(1).Value
                    GnamaPasien = DataGridView4.Rows(e.RowIndex).Cells(2).Value
                    GtglLahir = DataGridView4.Rows(e.RowIndex).Cells(3).Value
                    Gjk = DataGridView4.Rows(e.RowIndex).Cells(4).Value
                    GtglMasuk = DataGridView4.Rows(e.RowIndex).Cells(5).Value
                    'Gkelas = DataGridView4.Rows(e.RowIndex).Cells(6).Value
                    'GnoKmr = DataGridView4.Rows(e.RowIndex).Cells(7).Value
                    'GnoBed = DataGridView4.Rows(e.RowIndex).Cells(8).Value
                    Gdokter = DataGridView4.Rows(e.RowIndex).Cells(9).Value
                    GkdRanap = DataGridView4.Rows(e.RowIndex).Cells(10).Value
            End Select
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex = -1 Then
            Return
        End If

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Daftar Pasien"
                    noRanap = DataGridView1.Rows(e.RowIndex).Cells(0).Value
                    noRm = DataGridView1.Rows(e.RowIndex).Cells(1).Value
                    namaPasien = DataGridView1.Rows(e.RowIndex).Cells(2).Value
                    alamat = DataGridView1.Rows(e.RowIndex).Cells(3).Value
                    ranap = DataGridView1.Rows(e.RowIndex).Cells(4).Value
                    kelas = DataGridView1.Rows(e.RowIndex).Cells(5).Value
                    noKmr = DataGridView1.Rows(e.RowIndex).Cells(6).Value
                    noBed = DataGridView1.Rows(e.RowIndex).Cells(7).Value
                    asalPasien = DataGridView1.Rows(e.RowIndex).Cells(8).Value
                    tglDaftar = DataGridView1.Rows(e.RowIndex).Cells(9).Value
                    tglMrs = DataGridView1.Rows(e.RowIndex).Cells(9).Value
            End Select
        End If
    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex = -1 Then
            Return
        End If

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Daftar Pasien Keluar"
                    noRanap = DataGridView2.Rows(e.RowIndex).Cells(0).Value
                    namaPasien = DataGridView2.Rows(e.RowIndex).Cells(2).Value
                    ranap = DataGridView2.Rows(e.RowIndex).Cells(4).Value
                    noDaftar = DataGridView2.Rows(e.RowIndex).Cells(11).Value
                    kdTarifBed = DataGridView2.Rows(e.RowIndex).Cells(12).Value
                    kelas = DataGridView2.Rows(e.RowIndex).Cells(5).Value
                    Call tampilTextBoxPasien()
                    txtNoReg.Text = noDaftar
                    txtNoDaftarRanap.Text = noRanap
                    txtRanap.Text = ranap
                    txtKdTarifKelas.Text = kdTarifBed
                    txtKelas.Text = kelas
                    btnBatalOut.Enabled = True
                    btnBatalOut.Visible = True
            End Select
        End If
    End Sub

    Private Sub DataGridView3_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellClick
        If e.RowIndex = -1 Then
            Return
        End If

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Info Pasien"
                    noRanap = DataGridView3.Rows(e.RowIndex).Cells(0).Value
                    noRm = DataGridView3.Rows(e.RowIndex).Cells(1).Value
                    namaPasien = DataGridView3.Rows(e.RowIndex).Cells(2).Value
                    ranap = DataGridView3.Rows(e.RowIndex).Cells(4).Value
                    txtKelas.Text = DataGridView3.Rows(e.RowIndex).Cells(5).Value
                    tglDaftar = DataGridView3.Rows(e.RowIndex).Cells(9).Value
                    tglMrs = DataGridView3.Rows(e.RowIndex).Cells(9).Value
                    txtNoRM.Text = noRm
                    txtNamaPx.Text = namaPasien
                    txtNoDaftarRanap.Text = noRanap
                    txtRanap.Text = ranap
                    txtTglRanap.Text = tglDaftar
            End Select
        End If
    End Sub

    Private Sub DataGridView4_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView4.CellClick
        If e.RowIndex = -1 Then
            Return
        End If

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Pasien Gizi"
                    GnoReg = DataGridView4.Rows(e.RowIndex).Cells(0).Value
                    GnoRm = DataGridView4.Rows(e.RowIndex).Cells(1).Value
                    GnamaPasien = DataGridView4.Rows(e.RowIndex).Cells(2).Value
                    GtglLahir = DataGridView4.Rows(e.RowIndex).Cells(3).Value
                    Gjk = DataGridView4.Rows(e.RowIndex).Cells(4).Value
                    GtglMasuk = DataGridView4.Rows(e.RowIndex).Cells(5).Value
                    'Gkelas = DataGridView4.Rows(e.RowIndex).Cells(6).Value
                    'GnoKmr = DataGridView4.Rows(e.RowIndex).Cells(7).Value
                    'GnoBed = DataGridView4.Rows(e.RowIndex).Cells(8).Value
                    Gdokter = DataGridView4.Rows(e.RowIndex).Cells(9).Value
                    GkdRanap = DataGridView4.Rows(e.RowIndex).Cells(10).Value
            End Select
        End If
    End Sub

    Private Sub btnBatalOut_Click(sender As Object, e As EventArgs) Handles btnBatalOut.Click
        Dim status As String
        status = cekStatusBed(txtKdTarifKelas.Text)

        If status.Equals("st6", StringComparison.OrdinalIgnoreCase) Then
            'MsgBox("Kamar bisa digunakan", MsgBoxStyle.Information, "Information")
            Dim konfirmasi1 As MsgBoxResult
            konfirmasi1 = MsgBox("Apakah anda ingin membatalkan check out pasien '" & namaPasien & "' ?", vbQuestion + vbYesNo, "Konfirmasi")
            If konfirmasi1 = vbYes Then
                Call updateBatalCOReg()
                Call updateBatalCheckout()
                Call updateStatusBed()
                Me.Close()
                Form1.Show()
            End If
        ElseIf status.Equals("st5", StringComparison.OrdinalIgnoreCase) Then
            MsgBox("Maaf, kamar sudah terlebih dahulu digunakan !!", MsgBoxStyle.Exclamation, "Warning")
            Dim konfirmasi2 As MsgBoxResult
            konfirmasi2 = MsgBox("Silahkan pilih bed kosong lainnya ... ", vbQuestion + vbYesNo, "Konfirmasi")
            If konfirmasi2 = vbYes Then
                Me.Hide()
                Daftar_Kamar.Ambil_Data = True
                Daftar_Kamar.Form_Ambil_Data = "Batal"
                Daftar_Kamar.Show()
            End If
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Daftar Pasien"
                    Call tampilDataPasien()
                    'MsgBox("Refresh")
            End Select
        End If
    End Sub

    Private Sub DataGridView1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
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

    Private Sub DataGridView2_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView2.RowPostPaint
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

    Private Sub DataGridView3_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView3.RowPostPaint
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

    Private Sub DataGridView4_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView4.RowPostPaint
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

    Private Sub DataGridView1_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDown
        If e.RowIndex = -1 Then
            Return
        End If

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Info Pasien"
                    'DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = Not DataGridView1.Rows(e.RowIndex).Cells(10).Selected
            End Select
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        DataGridView1.DefaultCellStyle.ForeColor = Color.Black
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Private Sub DataGridView2_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView2.CellFormatting
        DataGridView2.DefaultCellStyle.ForeColor = Color.Black

        For i As Integer = 0 To DataGridView2.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Info Pasien"
                    For i As Integer = 0 To DataGridView2.Rows.Count - 1
                        If DataGridView2.Rows(i).Cells(10).Value.ToString <> "" Then
                            DataGridView2.Rows(i).Cells(10).Style.BackColor = Color.FromArgb(192, 0, 0)
                            DataGridView2.Rows(i).Cells(10).Style.ForeColor = Color.White
                        ElseIf DataGridView2.Rows(i).Cells(10).Value.ToString = "" Then
                            DataGridView2.Rows(i).Cells(10).Style.BackColor = Color.Green
                            DataGridView2.Rows(i).Cells(10).Style.ForeColor = Color.White
                            'DataGridView1.Rows(i).Cells(10).Value = "-"
                        End If
                    Next

                    If DataGridView2.Rows.Count = 0 Then
                        MsgBox("Tidak ditemukan No.RM / Nama Pasien tsb " & TextBox1.Text, MsgBoxStyle.Information)
                    End If
            End Select
        End If
    End Sub

    Private Sub DataGridView3_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView3.CellFormatting

        DataGridView3.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView3.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView3.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView3.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView3.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView3.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView3.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView3.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView3.DefaultCellStyle.ForeColor = Color.Black


        For i As Integer = 0 To DataGridView3.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Private Sub DataGridView4_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView4.CellFormatting
        DataGridView4.DefaultCellStyle.ForeColor = Color.Black
        For i As Integer = 0 To DataGridView4.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView4.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView4.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
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