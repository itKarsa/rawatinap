Imports MySql.Data.MySqlClient
Public Class Gizi

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Dim imt, bb, tb, tb2 As Double
    Dim lila, lilaPer, sLila As Double

    Dim menuDiet As String()

    Sub aturDGV()
        Try
            DataGridView2.Columns(0).Width = 145
            DataGridView2.Columns(1).Width = 100
            DataGridView2.Columns(2).Width = 150
            DataGridView2.Columns(3).Width = 150
            DataGridView2.Columns(4).Width = 120
            DataGridView2.Columns(5).Width = 80
            DataGridView2.Columns(6).Width = 120
            DataGridView2.Columns(7).Width = 120
            DataGridView2.Columns(8).Width = 120
            DataGridView2.Columns(9).Width = 70
            DataGridView2.Columns(10).Width = 70
            DataGridView2.Columns(11).Width = 70
            DataGridView2.Columns(12).Width = 70
            DataGridView2.Columns(13).Width = 70
            DataGridView2.Columns(14).Width = 70
            DataGridView2.Columns(15).Width = 150
            DataGridView2.Columns(16).Width = 150
            DataGridView2.Columns(0).HeaderText = "Kode"
            DataGridView2.Columns(1).HeaderText = "No.Ranap"
            DataGridView2.Columns(2).HeaderText = "Nama Pasien"
            DataGridView2.Columns(3).HeaderText = "Tgl.Permintaan"
            DataGridView2.Columns(4).HeaderText = "Waktu"
            DataGridView2.Columns(5).HeaderText = "Diagnosa Gizi"
            DataGridView2.Columns(6).HeaderText = "Alergi"
            DataGridView2.Columns(7).HeaderText = "Extra Diet"
            DataGridView2.Columns(8).HeaderText = "Ket.Diet"
            DataGridView2.Columns(9).HeaderText = "BB"
            DataGridView2.Columns(10).HeaderText = "TB"
            DataGridView2.Columns(11).HeaderText = "IMT"
            DataGridView2.Columns(12).HeaderText = "LK"
            DataGridView2.Columns(13).HeaderText = "LLA"
            DataGridView2.Columns(14).HeaderText = "%LLA"
            DataGridView2.Columns(15).HeaderText = "Ahli Gizi"
            DataGridView2.Columns(16).HeaderText = "StatusUpdate"

            DataGridView2.Columns(0).Visible = False
            DataGridView2.Columns(1).Visible = False
            DataGridView2.Columns(15).Visible = False

            'DataGridView2.Columns(9).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            'DataGridView2.Columns(9).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            'DataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

            DataGridView2.Columns(4).DefaultCellStyle.Format = "dd/MM/yyyy"
            DataGridView2.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView2.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView2.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView2.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView2.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView2.Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView2.Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        Catch ex As Exception

        End Try
    End Sub

    Function hitungUmur(ByVal tanggal As Date) As String
        Dim y, m, d As Integer
        y = Now.Year - tanggal.Year
        m = Now.Month - tanggal.Month
        d = Now.Day - tanggal.Day

        If Math.Sign(d) = -1 Then
            d = 30 - Math.Abs(d)
            m -= 1
        End If
        If Math.Sign(m) = -1 Then
            m = 12 - Math.Abs(m)
            y -= 1
        End If

        Return y & " Th, " & m & " Bln, " & d & " Hr"
    End Function

    Sub tampilDaftarPermintaan()
        Dim sql As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        sql = "SELECT kdPermintaan,noDaftarRawatInap,nmPasien,rawatInap,tglPermintaan,
                      WAKTU,alergi,diagnosaGizi,extraDiet,
                      keteranganDiet,nmAhligizi,statusUpdate,dateUpdate,
                      kdDiet,kdBentukMakanan,kelas
                 FROM vw_daftarpermintaangizi
                WHERE rawatInap LIKE '" & txtRanap.Text & "%'
                  AND statusUpdate = '0'
                  AND tglKeluarRawatInap IS NULL
                ORDER BY nmPasien ASC, tglPermintaan ASC"

        Try
            Call koneksiGizi()
            cmd = New MySqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            DataGridView2.Rows.Clear()

            Do While dr.Read
                DataGridView2.Rows.Add(dr.Item("kdPermintaan"), dr.Item("noDaftarRawatInap"), dr.Item("nmPasien"), dr.Item("rawatInap"),
                                       dr.Item("tglPermintaan"), dr.Item("WAKTU"), dr.Item("diagnosaGizi"), dr.Item("alergi"),
                                       dr.Item("extraDiet"), dr.Item("keteranganDiet"), dr.Item("nmAhligizi"), dr.Item("statusUpdate"),
                                       dr.Item("dateUpdate"), dr.Item("kdDiet"), dr.Item("kdBentukMakanan"), dr.Item("kelas"))
            Loop

            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub filterPermintaan()
        Dim sql As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        If txtFilterPasien.Text = "ALL" Then
            sql = "SELECT kdPermintaan,noDaftarRawatInap,nmPasien,rawatInap,tglPermintaan,
                          WAKTU,alergi,diagnosaGizi,extraDiet,
                          keteranganDiet,nmAhligizi,statusUpdate,dateUpdate,
                          kdDiet,kdBentukMakanan,kelas
                     FROM vw_daftarpermintaangizi
                    WHERE rawatInap LIKE '" & txtRanap.Text & "%'
                      AND statusUpdate = '0'
                      AND tglKeluarRawatInap IS NULL
                    ORDER BY nmPasien ASC, tglPermintaan ASC"
        ElseIf txtFilterPasien.Text <> "ALL" Then
            sql = "SELECT kdPermintaan,noDaftarRawatInap,nmPasien,rawatInap,tglPermintaan,
                          WAKTU,alergi,diagnosaGizi,extraDiet,
                          keteranganDiet,nmAhligizi,statusUpdate,dateUpdate,
                          kdDiet,kdBentukMakanan,kelas
                     FROM vw_daftarpermintaangizi
                    WHERE (SUBSTR(tglPermintaan,1,10) BETWEEN '" & Format(dateFilter.Value, "yyyy-MM-dd") & "' AND
                          '" & Format(DateAdd(DateInterval.Day, 1, dateFilter2.Value), "yyyy-MM-dd") & "')
                      AND nmPasien = '" & txtFilterPasien.Text & "'
                    ORDER BY nmPasien ASC, tglPermintaan ASC"
        End If

        Try
            Call koneksiGizi()
            cmd = New MySqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            DataGridView2.Rows.Clear()

            Do While dr.Read
                DataGridView2.Rows.Add(dr.Item("kdPermintaan"), dr.Item("noDaftarRawatInap"), dr.Item("nmPasien"), dr.Item("rawatInap"),
                                       dr.Item("tglPermintaan"), dr.Item("WAKTU"), dr.Item("diagnosaGizi"), dr.Item("alergi"),
                                       dr.Item("extraDiet"), dr.Item("keteranganDiet"), dr.Item("nmAhligizi"), dr.Item("statusUpdate"),
                                       dr.Item("dateUpdate"), dr.Item("kdDiet"), dr.Item("kdBentukMakanan"), dr.Item("kelas"))
            Loop

            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub tampilDetailWaktuMenu(noRegRanap As String)
        Dim sql As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        sql = "CALL tampildetailwaktumenu('" & noRegRanap & "')"

        Try
            Call koneksiGizi()
            cmd = New MySqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            DataGridView3.Rows.Clear()

            Do While dr.Read
                DataGridView3.Rows.Add(dr.Item("nmPasien"), dr.Item("tglDistribusi"), dr.Item("PAGI"), dr.Item("SIANG"), dr.Item("SORE"))
            Loop

            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub tampilDistribusi(noRegRanap As String)

        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        Dim sql As String = "SELECT nmPasien,WAKTU,kdBentukMakanan,kdDiet,
                                    tglDistribusi
                               FROM vw_detailpermintaangizi 
                              WHERE noDaftarRawatInap = '" & noRegRanap & "'
                           ORDER BY WAKTU, tglDistribusi ASC"
        Try
            Call koneksiGizi()
            cmd = New MySqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            DataGridView4.Rows.Clear()
            Do While dr.Read
                DataGridView4.Rows.Add(dr.Item("nmPasien"), dr.Item("WAKTU"), dr.Item("kdBentukMakanan"),
                                       dr.Item("kdDiet"), "", dr.Item("tglDistribusi"))
            Loop
            conn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Sub tampilDataPasien(cek As String, noRegRanap As String)

        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim sql As String = ""

        If cek = "RiwayatPasien" Then
            For Each row As DataGridViewRow In DataGridView2.SelectedRows
                If row.Cells(11).Value.ToString = "KONDISI SEKARANG" Then
                    sql = "SELECT px.nmPasien,px.jenisKelamin,px.tglLahir,
	                                dok.namaPetugasMedis,rri.tglMasukRawatInap,UPPER( ri.kdRawatInap ) AS kode 
                               FROM t_pasien px,t_registrasi reg,t_registrasirawatinap rri,
	                                t_tenagamedis2 dok,t_rawatinap ri 
                              WHERE px.noRekamedis = reg.noRekamedis AND
	                                reg.noDaftar = rri.noDaftar AND
	                                reg.kdTenagaMedis = dok.kdPetugasMedis AND
	                                rri.kdRawatInap = ri.kdRawatInap AND
	                                rri.noDaftarRawatInap = '" & noRegRanap & "'"
                ElseIf row.Cells(11).Value.ToString = "KONDISI LAMA" Then
                    sql = "SELECT px.nmPasien,px.jenisKelamin,px.tglLahir,
	                                dok.namaPetugasMedis 
                               FROM t_pasien px,t_registrasi reg,t_registrasirawatinap rri,
	                                t_tenagamedis2 dok
                              WHERE px.noRekamedis = reg.noRekamedis AND
	                                reg.noDaftar = rri.noDaftar AND
	                                reg.kdTenagaMedis = dok.kdPetugasMedis AND
	                                rri.noDaftarRawatInap = '" & noRegRanap & "'"
                    'MsgBox("Tes2")
                ElseIf row.Cells(11).Value.ToString = "PASIEN CHECKOUT" Then
                    sql = "SELECT px.nmPasien,px.jenisKelamin,px.tglLahir,
	                                dok.namaPetugasMedis 
                               FROM t_pasien px,t_registrasi reg,t_registrasirawatinap rri,
	                                t_tenagamedis2 dok
                              WHERE px.noRekamedis = reg.noRekamedis AND
	                                reg.noDaftar = rri.noDaftar AND
	                                reg.kdTenagaMedis = dok.kdPetugasMedis AND
	                                rri.noDaftarRawatInap = '" & noRegRanap & "'"
                    'MsgBox("Tes3")
                End If
            Next
        ElseIf cek = "DaftarPasien" Then
            sql = "SELECT px.nmPasien,px.jenisKelamin,px.tglLahir,
	                      dok.namaPetugasMedis,rri.tglMasukRawatInap,UPPER( ri.kdRawatInap ) AS kode 
                     FROM t_pasien px,t_registrasi reg,t_registrasirawatinap rri,
	                      t_tenagamedis2 dok,t_rawatinap ri 
                    WHERE px.noRekamedis = reg.noRekamedis AND
	                      reg.noDaftar = rri.noDaftar AND
	                      reg.kdTenagaMedis = dok.kdPetugasMedis AND
	                      rri.kdRawatInap = ri.kdRawatInap AND
	                      rri.noDaftarRawatInap = '" & noRegRanap & "'"
        End If



        Try
            Call koneksiServer()
            cmd = New MySqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                If cek = "RiwayatPasien" Then
                    For Each row As DataGridViewRow In DataGridView2.SelectedRows
                        If row.Cells(11).Value.ToString = "KONDISI SEKARANG" Then
                            txtNama.Text = dr.Item("nmPasien").ToString
                            txtJk.Text = dr.Item("jenisKelamin").ToString
                            txtTglLahir.Text = dr.Item("tglLahir").ToString
                            txtDokter.Text = dr.Item("namapetugasMedis").ToString
                            txtTglMasuk.Text = dr.Item("tglMasukRawatInap").ToString
                            txtKdRanap.Text = dr.Item("kode").ToString
                        ElseIf row.Cells(11).Value.ToString = "KONDISI LAMA" Then
                            txtNama.Text = dr.Item("nmPasien").ToString
                            txtJk.Text = dr.Item("jenisKelamin").ToString
                            txtTglLahir.Text = dr.Item("tglLahir").ToString
                            txtDokter.Text = dr.Item("namapetugasMedis").ToString
                        ElseIf row.Cells(11).Value.ToString = "PASIEN CHECKOUT" Then
                            txtNama.Text = dr.Item("nmPasien").ToString
                            txtJk.Text = dr.Item("jenisKelamin").ToString
                            txtTglLahir.Text = dr.Item("tglLahir").ToString
                            txtDokter.Text = dr.Item("namapetugasMedis").ToString
                        End If
                    Next
                ElseIf cek = "DaftarPasien" Then
                    txtNama.Text = dr.Item("nmPasien").ToString
                    txtJk.Text = dr.Item("jenisKelamin").ToString
                    txtTglLahir.Text = dr.Item("tglLahir").ToString
                    txtDokter.Text = dr.Item("namapetugasMedis").ToString
                    txtTglMasuk.Text = dr.Item("tglMasukRawatInap").ToString
                    txtKdRanap.Text = dr.Item("kode").ToString
                End If
            End If
            dr.Close()
            conn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Sub tampilMenuDiet(kdDiet As String)
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        Dim sql As String = "SELECT KDDIET,NMDIET 
                               FROM t_diet 
                              WHERE KDDIET = '" & kdDiet & "'"
        Try
            Call koneksiGizi()
            cmd = New MySqlCommand(sql, conn)
            dr = cmd.ExecuteReader

            Do While dr.Read
                DataGridView1.Rows.Add(dr.Item("KDDIET"), dr.Item("NMDIET"), "")
            Loop

            dr.Close()
            conn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Function tampilNamaDiet(kdDiet As String) As String
        Call koneksiGizi()
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim nama As String = ""
        Dim sql As String = "CALL namaDiet('" & kdDiet & "')"
        Try
            cmd = New MySqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                If IsDBNull(dr.Item("MENU")) Then
                    nama = "-"
                Else
                    nama = dr.Item("MENU")
                End If
            End If
            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, MessageBoxIcon.Error, "Error Nama Diet")
        End Try

        Return nama
    End Function

    Sub updateKondisi(noGiziLama As String)
        Call koneksiGizi()
        Try
            Dim str As String = ""
            str = "UPDATE t_permintaan
                      SET statusUpdate = '1', 
                          dateUpdate = '" & Format(txtTgl.Value, "yyyy-MM-dd HH:mm:ss") & "'
                    WHERE kdPermintaan = '" & noGiziLama & "'"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            'MsgBox("Update dokter berhasil dilakukan", MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox("Update data gagal dilakukan.", MessageBoxIcon.Error, "Error Update Kondisi")
        End Try
        conn.Close()
    End Sub

    Sub updateCheckout(noGiziLama As String)
        Call koneksiGizi()
        Try
            Dim str As String = ""
            str = "UPDATE t_permintaan
                      SET statusUpdate = '2', 
                          dateUpdate = '" & Format(txtTgl.Value, "yyyy-MM-dd HH:mm:ss") & "',
                          userModify = CONCAT(userModify,';" & LoginForm.txtUsername.Text & "'),  
                          dateModify = CONCAT(dateModify,';" & Format(DateTime.Now, "yyyy-MM-dd HH:mm:ss") & "')
                    WHERE kdPermintaan = '" & noGiziLama & "'"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Pasien Checkout berhasil dilakukan", MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox("Pasien Checkout gagal dilakukan.", MessageBoxIcon.Error, "Error Update Kondisi")
        End Try
        conn.Close()
    End Sub

    Function cekDistribusi() As String
        Call koneksiGizi()
        Dim str As String = ""
        Dim cek As String = ""
        str = "SELECT COUNT(kdPermintaan) AS ADA 
                 FROM t_distribusi 
                WHERE tglDistribusi = '" & Format(txtTgl.Value, "yyyy-MM-dd") & "' 
                  AND kdWaktu = '" & txtKdWaktu.Text & "' 
                  AND kdPermintaan = '" & txtKdPermintaanLama.Text & "'"
        Try
            cmd = New MySqlCommand(str, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                If dr.Item("ADA") = 0 Then
                    cek = "BELUM"
                Else
                    cek = "SUDAH"
                End If
            End If
            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox("Error.", MessageBoxIcon.Error, "Error Update Kondisi")
        End Try
        conn.Close()

        Return cek
    End Function

    Function pilihMenu() As String
        Dim val0 As New List(Of String)
        Dim noMenu As String

        For Each row As DataGridViewRow In DataGridView1.Rows
            If Not row.IsNewRow Then
                val0.Add(row.Cells(0).Value)
            End If
        Next

        noMenu = String.Join(",", val0.ToArray)

        Return noMenu
    End Function

    Sub autoComboKelas()
        conn.Close()
        Call koneksiGizi()
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter

        cmd = New MySqlCommand("SELECT kelas FROM t_kelas ORDER BY KDKELAS ASC", conn)
        da = New MySqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        txtKelas.DataSource = dt
        txtKelas.DisplayMember = "kelas"
        txtKelas.ValueMember = "kelas"
        txtKelas.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txtKelas.AutoCompleteSource = AutoCompleteSource.ListItems
        conn.Close()
    End Sub

    Sub autoComboAhliGizi()
        conn.Close()
        Call koneksiGizi()
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter

        cmd = New MySqlCommand("SELECT nmAhliGizi FROM t_ahligizi ORDER BY KdAhligizi ASC", conn)
        da = New MySqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        txtAhliGizi.DataSource = dt
        txtAhliGizi.DisplayMember = "nmAhliGizi"
        txtAhliGizi.ValueMember = "nmAhliGizi"
        txtAhliGizi.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txtAhliGizi.AutoCompleteSource = AutoCompleteSource.ListItems
        conn.Close()
    End Sub

    Sub autoBentukMakanan()
        conn.Close()
        Call koneksiGizi()
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter

        cmd = New MySqlCommand("SELECT kdBentukMakanan FROM t_bentukmakanan ORDER BY kdBentukMakanan ASC", conn)
        da = New MySqlDataAdapter(cmd)
        Dim dt1 As New DataTable
        da.Fill(dt1)

        txtBtkMkn.DataSource = dt1
        txtBtkMkn.DisplayMember = "kdBentukMakanan"
        txtBtkMkn.ValueMember = "kdBentukMakanan"
        txtBtkMkn.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txtBtkMkn.AutoCompleteSource = AutoCompleteSource.ListItems
        conn.Close()
    End Sub

    Sub autoMenuDiet()
        conn.Close()
        Call koneksiGizi()
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter

        cmd = New MySqlCommand("SELECT NMDIET FROM t_diet WHERE NOT lHEADER = 1 ORDER BY NMDIET ASC", conn)
        da = New MySqlDataAdapter(cmd)
        Dim dt1 As New DataTable
        da.Fill(dt1)

        txtMenuDiet.DataSource = dt1
        txtMenuDiet.DisplayMember = "NMDIET"
        txtMenuDiet.ValueMember = "NMDIET"
        txtMenuDiet.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txtMenuDiet.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Sub autoComboPasien()
        conn.Close()
        Call koneksiServer()
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter

        cmd = New MySqlCommand("SELECT 'ALL' AS nmPasien
                                UNION ALL
                                SELECT px.nmPasien
                                  FROM t_pasien px, t_registrasi reg, t_registrasirawatinap rri
                                 WHERE px.noRekamedis = reg.noRekamedis AND
	                                   reg.noDaftar = rri.noDaftar AND
                                       rri.tglKeluarRawatInap IS NULL AND 
                                       rri.rawatInap LIKE '" & txtRanap.Text & "%'", conn)
        da = New MySqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        txtFilterPasien.DataSource = dt
        txtFilterPasien.DisplayMember = "nmPasien"
        txtFilterPasien.ValueMember = "nmPasien"
        txtFilterPasien.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txtFilterPasien.AutoCompleteSource = AutoCompleteSource.ListItems
        conn.Close()
    End Sub

    Sub clearText()
        txtKdPermintaan.Text = ""
        txtKdPermintaanLama.Text = ""
        txtNoReg.Text = ""
        txtTglMasuk.Text = ""
        txtTglLahir.Text = ""
        txtNama.Text = ""
        txtJk.Text = ""
        txtUmur.Text = ""
        txtKelas.Text = ""
        txtKdKelas.Text = ""
        txtNoKmr.Text = ""
        txtNoBed.Text = ""
        txtDokter.Text = ""
        txtAlergi.Text = ""
        txtDiagGizi.Text = ""
        txtBB.Text = ""
        txtTB.Text = ""
        txtLK.Text = ""
        txtLLA.Text = ""
        txtIMT.Text = ""
        'txtAhliGizi.Text = ""
        'txtKdAhliGizi.Text = ""
        txtWaktu.Text = ""
        txtKdWaktu.Text = ""
        txtBtkMkn.Text = ""
        txtKdBtkMkn.Text = ""
        txtMenuDiet.Text = ""
        txtKdMenu.Text = ""
        txtExtraDiet.Text = ""
        txtKet.Text = ""
        txtTgl.Value = Now
        DataGridView1.Rows.Clear()
        For Each i As Integer In CheckedListBox1.CheckedIndices
            CheckedListBox1.SetItemCheckState(i, CheckState.Unchecked)
        Next
    End Sub

    Sub autoNoPermintaan()
        Dim noPermintaanGizi As String

        Try
            Call koneksiGizi()
            Dim query As String
            Dim cmd As MySqlCommand
            Dim dr As MySqlDataReader
            query = "SELECT SUBSTR(KDPERMINTAAN,17,4) FROM t_permintaan ORDER BY CAST(SUBSTR(KDPERMINTAAN,17,4) AS UNSIGNED) DESC LIMIT 1"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                dr.Read()
                noPermintaanGizi = "RGZ" + Format(Now, "ddMMyyHHmmss") + "-" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                txtKdPermintaan.Text = noPermintaanGizi
            Else
                noPermintaanGizi = "RGZ" + Format(Now, "ddMMyyHHmmss") + "-1"
                txtKdPermintaan.Text = noPermintaanGizi
            End If
            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex, MsgBoxStyle.Critical, "No.Permintaan")
        End Try

    End Sub

    Sub detailMenu()
        Call koneksiGizi()
        Dim val0, val1 As String

        Dim query As String
        Dim cmd As MySqlCommand
        query = "INSERT INTO t_permintaanmenu (kdPermintaan,kdBentukMakanan,bentukMakanan,kdDiet,keteranganDiet) 
                                       VALUES ('" & txtKdPermintaan.Text & "','" & txtKdBtkMkn.Text & "','" & txtBtkMkn.Text & "',
                                               @kdDiet,@keteranganDiet)"
        cmd = New MySqlCommand(query, conn)

        Try
            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                val0 = DataGridView1.Rows(i).Cells(0).Value
                val1 = DataGridView1.Rows(i).Cells(1).Value

                cmd.Parameters.AddWithValue("@kdDiet", val0)
                cmd.Parameters.AddWithValue("@keteranganDiet", val1)
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
            Next
            'MsgBox("Detail menu diet berhasil disimpan", MsgBoxStyle.Information, "Informasi")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "Informasi")
            cmd.Dispose()
        End Try

    End Sub

    Private Sub Gizi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.Manual
        With Screen.PrimaryScreen.WorkingArea
            Me.SetBounds(.Left, .Top, .Width, .Height)
        End With

        'Dim dt As Date = Format(Now)
        'txtTgl.Text = dt.ToString("dddd, dd-MM-yyyy", New System.Globalization.CultureInfo("id-ID"))
        'txtTgl.MinDate = DateTime.Today

        Call PopulateCheckListBoxesDiagnosa()
        Call tampilDaftarPermintaan()
        'Call aturDGV()

        Call autoComboKelas()
        Call autoNoPermintaan()
        Call autoComboAhliGizi()
        Call autoBentukMakanan()
        Call autoMenuDiet()
        Call autoComboPasien()

        txtAhliGizi.SelectedValue = Form1.txtUser.Text
        txtBtkMkn.SelectedValue = -1
        txtMenuDiet.SelectedValue = -1
        DataGridView2.ClearSelection()
        SplitContainer1.Panel2Collapsed = True
        SplitContainer2.Panel2Collapsed = True
    End Sub

    Private Sub txtAlergi_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAlergi.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtAlergi.Text = "" Then
                txtAlergi.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub txtDiagGizi_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtDiagGizi.Text = "" Then
                txtDiagGizi.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub txtAhliGizi_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAhliGizi.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtAhliGizi.Text = "" Then
                txtAhliGizi.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub txtBB_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBB.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtBB.Text = "" Then
                txtBB.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub txtTB_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTB.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtTB.Text = "" Then
                txtTB.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub txtLK_KeyDown(sender As Object, e As KeyEventArgs) Handles txtLK.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtLK.Text = "" Then
                txtLK.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub txtLLA_KeyDown(sender As Object, e As KeyEventArgs) Handles txtLLA.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtLLA.Text = "" Then
                txtLLA.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub txtLLAPer_KeyDown(sender As Object, e As KeyEventArgs) Handles txtLLAPer.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtLLAPer.Text = "" Then
                txtLLAPer.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub txtBtkMknPagi_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBtkMkn.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtBtkMkn.Text = "" Then
                txtBtkMkn.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub txtMenuDiet_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMenuDiet.KeyDown
        Dim count As Integer
        count = DataGridView1.Rows.Count

        If e.KeyCode = Keys.Enter Then
            'SendKeys.Send("{TAB}")

            If txtMenuDiet.Text = "" Then
                MsgBox("Pilih menu diet terlebih dahulu !!", MsgBoxStyle.Exclamation, "Information")
            Else
                DataGridView1.Rows.Add(1)
                DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(0).Value = txtKdMenu.Text
                DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(1).Value = txtMenuDiet.Text
                DataGridView1.Update()
            End If
        End If
    End Sub

    Private Sub txtExtraDiet_KeyDown(sender As Object, e As KeyEventArgs) Handles txtExtraDiet.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtExtraDiet.Text = "" Then
                txtExtraDiet.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub txtKetPagi_KeyDown(sender As Object, e As KeyEventArgs) Handles txtKet.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtKet.Text = "" Then
                txtKet.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub txtWaktu_KeyDown(sender As Object, e As KeyEventArgs) Handles txtWaktu.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtWaktu.Text = "" Then
                txtWaktu.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub txtAhliGizi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtAhliGizi.SelectedIndexChanged
        conn.Close()
        Call koneksiGizi()
        Try
            Dim query As String
            query = "SELECT * FROM t_ahligizi WHERE nmAhligizi = '" & txtAhliGizi.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                txtKdAhliGizi.Text = UCase(dr.GetString("KdAhligizi"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub txtBtkMknPagi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtBtkMkn.SelectedIndexChanged
        conn.Close()
        Call koneksiGizi()
        Try
            Dim query As String
            query = "SELECT * FROM t_bentukMakanan WHERE bentukMakanan = '" & txtBtkMkn.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                txtKdBtkMkn.Text = UCase(dr.GetString("kdBentukMakanan"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub txtMenuDietPagi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtMenuDiet.SelectedIndexChanged
        conn.Close()
        Call koneksiGizi()
        Try
            Dim query As String
            query = "SELECT * FROM t_diet WHERE NMDIET = '" & txtMenuDiet.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                txtKdMenu.Text = UCase(dr.GetString("kdDiet"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()

    End Sub

    Private Sub btnKirim_Click(sender As Object, e As EventArgs) Handles btnKirim.Click

        Dim dtMasuk, dtLahir As String

        If txtTglMasuk.Text.Equals("") Then
            MsgBox("Pilih pasien terlebih dahulu", MsgBoxStyle.Exclamation)
        ElseIf txtTglLahir.Text.Equals("") Then
            MsgBox("Pilih pasien terlebih dahulu", MsgBoxStyle.Exclamation)
        Else
            'dtPesan = DateTime.Now.ToString("yyyy-MM-dd")
            dtMasuk = Convert.ToDateTime(txtTglMasuk.Text).ToString("yyyy-MM-dd HH:mm:ss")
            dtLahir = Convert.ToDateTime(txtTglLahir.Text).ToString("yyyy-MM-dd")
        End If

        Dim reNama As String
        reNama = Replace(txtNama.Text, "'", "''")

        If txtKdPermintaan.Text.Equals("") Then
            MsgBox("Mohon lengkapi field yang masing kosong(KODE PERMINTAAN) !!", MsgBoxStyle.Exclamation)
        ElseIf txtTgl.Text.Equals("") Then
            MsgBox("Mohon lengkapi field yang masing kosong(TGL. PERMINTAAN) !!", MsgBoxStyle.Exclamation)
        ElseIf txtNoReg.Text.Equals("") Then
            MsgBox("Mohon lengkapi field yang masing kosong(NO. REGISTRASI) !!", MsgBoxStyle.Exclamation)
        ElseIf txtKdRanap.Text.Equals("") Then
            MsgBox("Mohon lengkapi field yang masing kosong(KODE RUANG) !!", MsgBoxStyle.Exclamation)
        ElseIf txtKdKelas.Text.Equals("") Then
            MsgBox("Mohon lengkapi field yang masing kosong(KODE KELAS) !!", MsgBoxStyle.Exclamation)
        ElseIf txtKdKelas.Text.Equals("-") Then
            Me.ErKelas.SetError(Me.txtAlergi, "Tolong dipilih terlebih dahulu")
            MsgBox("Mohon kelas dipilih terlebih dahulu !!", MsgBoxStyle.Exclamation)
        ElseIf txtAlergi.Text.Equals("") Then
            Me.ErAlergi.SetError(Me.txtAlergi, "Tolong diinputkan terlebih dahulu")
            MsgBox("Mohon lengkapi field yang masing kosong !!", MsgBoxStyle.Exclamation)
        ElseIf txtDiagGizi.Text.Equals("") Then
            Me.ErDiagGizi.SetError(Me.txtDiagGizi, "Tolong diinputkan terlebih dahulu")
            MsgBox("Mohon lengkapi field yang masing kosong !!", MsgBoxStyle.Exclamation)
        ElseIf txtAhliGizi.Text.Equals("") Then
            Me.ErAhli.SetError(Me.txtAhliGizi, "Tolong diinputkan terlebih dahulu")
            MsgBox("Mohon lengkapi field yang masing kosong !!", MsgBoxStyle.Exclamation)
        ElseIf txtKdAhliGizi.Text.Equals("") Then
            MsgBox("Mohon lengkapi field yang masing kosong(Kode Ahli Gizi) !!", MsgBoxStyle.Exclamation)
        ElseIf txtWaktu.Text.Equals("") Then
            Me.ErWaktu.SetError(Me.txtWaktu, "Tolong diinputkan terlebih dahulu")
            MsgBox("Mohon lengkapi field yang masing kosong !!", MsgBoxStyle.Exclamation)
        ElseIf txtKdWaktu.Text.Equals("") Then
            MsgBox("Mohon lengkapi field yang masing kosong !!", MsgBoxStyle.Exclamation)
        ElseIf txtBtkMkn.Text.Equals("") Then
            Me.ErBentuk.SetError(Me.txtBtkMkn, "Tolong diinputkan terlebih dahulu")
            MsgBox("Mohon lengkapi field yang masing kosong !!", MsgBoxStyle.Exclamation)
        ElseIf txtExtraDiet.Text.Equals("") Then
            Me.ErExtra.SetError(Me.txtExtraDiet, "Tolong diinputkan terlebih dahulu")
            MsgBox("Mohon lengkapi field yang masing kosong !!", MsgBoxStyle.Exclamation)
        ElseIf txtKet.Text.Equals("") Then
            Me.ErDiet.SetError(Me.txtKet, "Tolong diinputkan terlebih dahulu")
            MsgBox("Mohon lengkapi field yang masing kosong !!", MsgBoxStyle.Exclamation)
        ElseIf DataGridView1.Rows.Count = 0 Then
            MsgBox("Mohon isi menu diet yang diminta terlebih dahulu !!", MsgBoxStyle.Exclamation)
        Else
            ErAlergi.Clear()
            ErDiagGizi.Clear()
            ErAhli.Clear()
            ErWaktu.Clear()
            ErBentuk.Clear()
            ErExtra.Clear()
            ErDiet.Clear()
            Try
                conn.Close()
                Call koneksiGizi()
                Dim strGz As String
                Dim cmdGz As MySqlCommand
                strGz = "INSERT INTO t_permintaan (kdPermintaan,tglPermintaan,noDaftarRawatInap,kdRawatInap,
                                                 kdKelas,alergi,diagnosaGizi,KdAhligizi,
                                                 kdWaktu,kdDiet,kdBentukMakanan,
                                                 extraDiet,keteranganDiet,statusUpdate,
                                                 userModify,dateModify) 
                                         VALUES ('" & txtKdPermintaan.Text & "','" & Format(txtTgl.Value, "yyyy-MM-dd HH:mm:ss") & "','" & txtNoReg.Text & "',
                                                 '" & txtKdRanap.Text & "','" & txtKdKelas.Text & "','" & txtAlergi.Text & "',
                                                 '" & txtDiagGizi.Text & "','" & txtKdAhliGizi.Text & "','" & txtKdWaktu.Text & "',
                                                 '" & pilihMenu() & "','" & txtBtkMkn.Text & "','" & txtExtraDiet.Text & "',
                                                 '" & txtKet.Text & "','0',
                                                 '" & LoginForm.txtUsername.Text.ToUpper & "','" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "')"

                'Using cmdGz As New MySqlCommand(strGz, conn)
                '    cmdGz.Parameters.AddWithValue("@kdDiet", pilihMenu)
                '    cmdGz.ExecuteNonQuery()
                '    cmdGz.Parameters.Clear()
                'End Using
                cmdGz = New MySqlCommand(strGz, conn)
                cmdGz.ExecuteNonQuery()

                DataGridView2.ClearSelection()
                DataGridView1.Rows.Clear()
                MsgBox("Order Makanan " & txtWaktu.Text & " berhasil ditambahkan", MsgBoxStyle.Information)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        conn.Close()
        Call autoNoPermintaan()
        Call tampilDaftarPermintaan()

    End Sub

    Private Sub txtWaktu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtWaktu.SelectedIndexChanged
        conn.Close()
        Call koneksiGizi()
        Try
            Dim query As String
            Dim cmd As MySqlCommand
            Dim dr As MySqlDataReader

            query = "SELECT * FROM t_waktu WHERE waktu = '" & txtWaktu.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                txtKdWaktu.Text = UCase(dr.GetString("kdWaktu"))
            End While
            dr.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub txtKelas_TextChanged(sender As Object, e As EventArgs)
        conn.Close()
        Call koneksiServer()
        Try
            Dim query As String
            Dim cmd As MySqlCommand
            Dim dr As MySqlDataReader

            query = "SELECT * FROM t_kelas WHERE kelas = '" & txtKelas.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                txtKdKelas.Text = UCase(dr.GetString("kdKelas"))
            End While
            dr.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub DataGridView2_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseClick

        Dim kdPermintaan, tglPermintaan, noDaftarRawatInap, nmPasien,
            rawatInap, kdAlergi, diagnosaGizi, ahliGizi, waktu, extraDiet,
            status, keteranganDiet, bentuk, kelas, kdDiet, diet As String

        If e.RowIndex = -1 Then
            Return
        End If

        DataGridView1.Rows.Clear()
        kdPermintaan = DataGridView2.Rows(e.RowIndex).Cells(0).Value
        noDaftarRawatInap = DataGridView2.Rows(e.RowIndex).Cells(1).Value
        nmPasien = DataGridView2.Rows(e.RowIndex).Cells(2).Value
        rawatInap = DataGridView2.Rows(e.RowIndex).Cells(3).Value
        tglPermintaan = DataGridView2.Rows(e.RowIndex).Cells(4).Value
        waktu = DataGridView2.Rows(e.RowIndex).Cells(5).Value
        diagnosaGizi = DataGridView2.Rows(e.RowIndex).Cells(6).Value
        kdAlergi = DataGridView2.Rows(e.RowIndex).Cells(7).Value.ToString
        extraDiet = DataGridView2.Rows(e.RowIndex).Cells(8).Value
        keteranganDiet = DataGridView2.Rows(e.RowIndex).Cells(9).Value
        'dgvBB = DataGridView2.Rows(e.RowIndex).Cells(9).Value
        'dgvTB = DataGridView2.Rows(e.RowIndex).Cells(10).Value
        'dgvIMT = DataGridView2.Rows(e.RowIndex).Cells(11).Value.ToString
        'dgvLK = DataGridView2.Rows(e.RowIndex).Cells(12).Value
        'dgvLLA = DataGridView2.Rows(e.RowIndex).Cells(13).Value.ToString
        'dgvLLAper = DataGridView2.Rows(e.RowIndex).Cells(14).Value.ToString
        ahliGizi = DataGridView2.Rows(e.RowIndex).Cells(10).Value
        status = DataGridView2.Rows(e.RowIndex).Cells(11).Value.ToString
        kdDiet = DataGridView2.Rows(e.RowIndex).Cells(13).Value.ToString
        bentuk = DataGridView2.Rows(e.RowIndex).Cells(14).Value.ToString
        kelas = DataGridView2.Rows(e.RowIndex).Cells(15).Value.ToString

        txtKdPermintaanLama.Text = kdPermintaan
        txtNoReg.Text = noDaftarRawatInap
        txtNama.Text = nmPasien
        txtAlergi.Text = kdAlergi
        txtDiagGizi.Text = diagnosaGizi
        'txtBB.Text = dgvBB
        'txtTB.Text = dgvTB
        'txtIMT.Text = dgvIMT
        'txtLK.Text = dgvLK
        'txtLLA.Text = dgvLLA
        'txtLLAPer.Text = dgvLLAper
        txtAhliGizi.Text = ahliGizi
        txtWaktu.Text = waktu
        txtExtraDiet.Text = extraDiet
        txtKet.Text = keteranganDiet
        txtBtkMkn.Text = bentuk
        txtKelas.Text = kelas
        menuDiet = kdDiet.Split(New Char() {","c})

        For Each diet In menuDiet
            Call tampilMenuDiet(diet)
        Next

        Call tampilDataPasien("RiwayatPasien", noDaftarRawatInap)

        If e.ColumnIndex = 16 Then
            If SplitContainer1.Panel2Collapsed = True Then
                SplitContainer1.Panel2Collapsed = False
                Call tampilDetailWaktuMenu(noDaftarRawatInap)
                For Each row As DataGridViewRow In DataGridView3.Rows
                    row.Cells(5).Value = tampilNamaDiet(row.Cells(2).Value)
                    row.Cells(6).Value = tampilNamaDiet(row.Cells(3).Value)
                    row.Cells(7).Value = tampilNamaDiet(row.Cells(4).Value)
                Next
            ElseIf SplitContainer1.Panel2Collapsed = False Then
                SplitContainer1.Panel2Collapsed = True
            End If
        ElseIf e.ColumnIndex = 17 Then
            If SplitContainer2.Panel2Collapsed = True Then
                SplitContainer2.Panel2Collapsed = False
                Call tampilDistribusi(noDaftarRawatInap)
                For Each row As DataGridViewRow In DataGridView4.Rows
                    row.Cells(4).Value = tampilNamaDiet(row.Cells(3).Value)
                Next
            ElseIf SplitContainer2.Panel2Collapsed = False Then
                SplitContainer2.Panel2Collapsed = True
            End If
        End If
    End Sub

    Private Sub DataGridView2_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView2.CellFormatting

        DataGridView2.Columns(5).DefaultCellStyle.Format = "dd/MM/yyyy"
        'DataGridView2.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'DataGridView2.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'DataGridView2.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'DataGridView2.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'DataGridView2.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'DataGridView2.Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'DataGridView2.Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        For i As Integer = 0 To DataGridView2.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next

        If e.RowIndex > 0 And e.ColumnIndex = 2 Then
            If DataGridView2.Item(2, e.RowIndex - 1).Value = e.Value Then
                e.Value = ""
            End If
        End If

        'If e.RowIndex > 0 And e.ColumnIndex = 3 Then
        '    If DataGridView2.Item(3, e.RowIndex - 1).Value.ToString = e.Value Then
        '        e.Value = ""
        '    End If
        'End If

        For i As Integer = 0 To DataGridView2.RowCount - 1
            If DataGridView2.Rows(i).Cells(11).Value.ToString = "0" Then
                DataGridView2.Rows(i).Cells(11).Value = "KONDISI SEKARANG"
                DataGridView2.Rows(i).Cells(11).Style.BackColor = Color.Green
                DataGridView2.Rows(i).Cells(11).Style.ForeColor = Color.White
            ElseIf DataGridView2.Rows(i).Cells(11).Value.ToString = "1" Then
                DataGridView2.Rows(i).Cells(11).Value = "KONDISI LAMA"
                DataGridView2.Rows(i).Cells(11).Style.BackColor = Color.LightCoral
                DataGridView2.Rows(i).Cells(11).Style.ForeColor = Color.White
            ElseIf DataGridView2.Rows(i).Cells(11).Value.ToString = "2" Then
                DataGridView2.Rows(i).Cells(11).Value = "PASIEN CHECKOUT"
                DataGridView2.Rows(i).Cells(11).Style.BackColor = Color.Red
                DataGridView2.Rows(i).Cells(11).Style.ForeColor = Color.White
            End If
        Next

        For i = 0 To DataGridView2.RowCount - 1
            DataGridView2.Rows(i).Cells(16).Style.BackColor = Color.DodgerBlue
            DataGridView2.Rows(i).Cells(16).Style.ForeColor = Color.White
            DataGridView2.Rows(i).Cells(17).Style.BackColor = Color.DodgerBlue
            DataGridView2.Rows(i).Cells(17).Style.ForeColor = Color.White
        Next

        DataGridView2.ColumnHeadersDefaultCellStyle.Font = New Font("Tahoma", 10, FontStyle.Bold)
        DataGridView2.DefaultCellStyle.Font = New Font("Tahoma", 9, FontStyle.Bold)
        DataGridView2.DefaultCellStyle.ForeColor = Color.Black
        DataGridView2.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise
        DataGridView2.DefaultCellStyle.SelectionForeColor = Color.Black

    End Sub

    Private Sub DataGridView3_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView3.CellFormatting

        For i As Integer = 0 To DataGridView3.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next

        If e.RowIndex > 0 And e.ColumnIndex = 0 Then
            If DataGridView3.Item(0, e.RowIndex - 1).Value = e.Value Then
                e.Value = ""
            End If
        End If

        DataGridView3.ColumnHeadersDefaultCellStyle.Font = New Font("Tahoma", 10, FontStyle.Bold)
        DataGridView3.DefaultCellStyle.Font = New Font("Tahoma", 9, FontStyle.Bold)
        DataGridView3.DefaultCellStyle.ForeColor = Color.Black
        DataGridView3.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise
        DataGridView3.DefaultCellStyle.SelectionForeColor = Color.Black

    End Sub

    Private Sub DataGridView4_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView4.CellFormatting

        DataGridView4.ColumnHeadersDefaultCellStyle.Font = New Font("Tahoma", 10, FontStyle.Bold)
        DataGridView4.DefaultCellStyle.Font = New Font("Tahoma", 9, FontStyle.Bold)
        DataGridView4.DefaultCellStyle.ForeColor = Color.Black
        DataGridView4.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise
        DataGridView4.DefaultCellStyle.SelectionForeColor = Color.Black

        For i As Integer = 0 To DataGridView4.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView4.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView4.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next

        For i As Integer = 0 To DataGridView4.RowCount - 1
            If DataGridView4.Rows(i).Cells(5).Value.ToString IsNot "" Then
                DataGridView4.Rows(i).Cells(6).Value = "SUDAH DIPROSES"
                DataGridView4.Rows(i).Cells(6).Style.BackColor = Color.Green
                DataGridView4.Rows(i).Cells(6).Style.ForeColor = Color.White
            ElseIf DataGridView4.Rows(i).Cells(5).Value.ToString = "" Then
                DataGridView4.Rows(i).Cells(6).Value = "PERMINTAAN"
                DataGridView4.Rows(i).Cells(6).Style.BackColor = Color.Orange
                DataGridView4.Rows(i).Cells(6).Style.ForeColor = Color.White
            End If
        Next

        'For i As Integer = 0 To DataGridView4.RowCount - 1
        '    If IsDBNull(DataGridView4.Rows(i).Cells(5).Value) Then
        '        DataGridView4.Rows(i).Cells(5).Value = "-"
        '    End If
        'Next

    End Sub

    Private Sub txtBB_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBB.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtTB_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTB.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtIMT_TextChanged(sender As Object, e As EventArgs) Handles txtIMT.TextChanged
        If txtIMT.Text = "NaN" Or txtIMT.Text = "NAN" Then
            txtIMT.Text = 0
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        'MsgBox("Masih dalam tahap develop", MsgBoxStyle.Information)
        txtWaktu.Text = ""
        txtBtkMkn.Text = ""
        txtMenuDiet.Text = ""
        txtExtraDiet.Text = ""
        txtKet.Text = ""
        txtTgl.Value = Now
        DataGridView1.Rows.Clear()

        'Dim konfirmasi As MsgBoxResult
        'Dim startTimeSiang As New TimeSpan(7, 30, 0)  '07:30:00 WIB
        'Dim startTimeSore As New TimeSpan(10, 0, 0)  '10:00:00 WIB
        'Dim startTimePagiSore As New TimeSpan(10, 0, 0)  '10:00:00 WIB
        'Dim startTimeSoreCito As New TimeSpan(10, 0, 0)  '10:00:00 WIB
        'Dim endTimeSiang As New TimeSpan(10, 0, 0) '10:00:00 WIB
        'Dim endTimeSore As New TimeSpan(11, 0, 0) '11:00:00 WIB
        'Dim endTimePagiSore As New TimeSpan(11, 0, 0) '11:00:00 WIB
        'Dim endTimeSoreCito As New TimeSpan(12, 30, 0) '12:30:00 WIB


        'If txtTgl.Value.TimeOfDay >= startTimeSiang And txtTgl.Value.TimeOfDay <= endTimeSiang Then
        '    konfirmasi = MsgBox("Anda telah melewati batas waktu perubahan bentuk makanan / menu diet !!!" & vbCrLf &
        '                        "Diharapkan untuk konfirmasi melalui telepon kepada PM" & vbCrLf &
        '                        "Apakah anda sudah konfirmasi PM ?", MsgBoxStyle.Exclamation + vbYesNo, "Konfirmasi")
        '    If konfirmasi = vbYes Then

        '    End If
        'ElseIf txtWaktu.Text = "SORE" And txtTgl.Value.TimeOfDay >= startTimeSore And txtTgl.Value.TimeOfDay <= endTimeSore Then
        '    konfirmasi = MsgBox("Anda telah melewati batas waktu perubahan bentuk makanan / menu diet !!!" & vbCrLf &
        '                            "Diharapkan untuk konfirmasi melalui telepon kepada PM" & vbCrLf &
        '                            "Apakah anda sudah konfirmasi PM ?", MsgBoxStyle.Exclamation + vbYesNo, "Konfirmasi")
        '    If konfirmasi = vbYes Then

        '    End If
        'ElseIf txtWaktu.Text = "SORE" And txtTgl.Value.TimeOfDay >= startTimePagiSore And txtTgl.Value.TimeOfDay <= endTimePagiSore Then
        '    konfirmasi = MsgBox("Anda telah melewati batas waktu perubahan bentuk makanan / menu diet !!!" & vbCrLf &
        '                            "Diharapkan untuk konfirmasi melalui telepon kepada PM" & vbCrLf &
        '                            "Apakah anda sudah konfirmasi PM ?", MsgBoxStyle.Exclamation + vbYesNo, "Konfirmasi")
        '    If konfirmasi = vbYes Then

        '    End If
        'End If

    End Sub

    Private Sub btnClear2_Click(sender As Object, e As EventArgs) Handles btnClear2.Click
        txtAlergi.Text = ""
        txtDiagGizi.Text = ""
        txtBB.Text = "0"
        txtTB.Text = "0"
        txtIMT.Text = "0"
        txtLK.Text = "0"
        txtLLA.Text = "0"
        txtLLAPer.Text = "0"
        For Each i As Integer In CheckedListBox1.CheckedIndices
            CheckedListBox1.SetItemCheckState(i, CheckState.Unchecked)
        Next
    End Sub

    Private Sub txtCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Daftar_Pasien.Ambil_Data = True
        Daftar_Pasien.Form_Ambil_Data = "Pasien Gizi"
        Daftar_Pasien.Show()
        Call clearText()
        Call autoNoPermintaan()
    End Sub

    Private Sub txtLK_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLK.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtLLA_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLLA.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtBB_TextChanged(sender As Object, e As EventArgs) Handles txtBB.TextChanged
        If txtBB.Text = "" Then
            Return
        Else
            bb = txtBB.Text
        End If

        If txtBB.Text <> "" Then
            txtBB.BackColor = Color.White
        End If

    End Sub

    Private Sub txtTB_TextChanged(sender As Object, e As EventArgs) Handles txtTB.TextChanged
        tb = Val(txtTB.Text) / 100
        tb2 = tb * tb
        imt = bb / tb2
        txtIMT.Text = FormatNumber(imt, 1)

        If txtTB.Text <> "" Then
            txtTB.BackColor = Color.White
        End If
    End Sub

    Private Sub txtAlergi_TextChanged(sender As Object, e As EventArgs) Handles txtAlergi.TextChanged
        If txtAlergi.Text <> "" Then
            txtAlergi.BackColor = Color.White
        End If
    End Sub

    Private Sub txtDiagGizi_TextChanged(sender As Object, e As EventArgs)
        If txtDiagGizi.Text <> "" Then
            txtDiagGizi.BackColor = Color.White
        End If
    End Sub

    Private Sub txtExtraDiet_TextChanged(sender As Object, e As EventArgs) Handles txtExtraDiet.TextChanged
        If txtExtraDiet.Text <> "" Then
            txtExtraDiet.BackColor = Color.White
        End If
    End Sub

    Private Sub txtKet_TextChanged(sender As Object, e As EventArgs) Handles txtKet.TextChanged
        If txtKet.Text <> "" Then
            txtKet.BackColor = Color.White
        End If
    End Sub

    Private Sub txtAhliGizi_TextChanged(sender As Object, e As EventArgs) Handles txtAhliGizi.TextChanged
        If txtAhliGizi.Text <> "" Then
            txtAhliGizi.BackColor = Color.White
        End If
    End Sub

    Private Sub txtLK_TextChanged(sender As Object, e As EventArgs) Handles txtLK.TextChanged
        If txtLK.Text <> "" Then
            txtLK.BackColor = Color.White
        End If
    End Sub

    Private Sub txtLLA_TextChanged(sender As Object, e As EventArgs) Handles txtLLA.TextChanged
        If txtLLA.Text <> "" Then
            txtLLA.BackColor = Color.White
        End If
    End Sub

    Private Sub txtNoReg_TextChanged(sender As Object, e As EventArgs) Handles txtNoReg.TextChanged
        Call tampilDataPasien("DaftarPasien", txtNoReg.Text)
    End Sub

    Private Sub txtTglLahir_TextChanged(sender As Object, e As EventArgs) Handles txtTglLahir.TextChanged
        Dim dt As DateTime
        Dim cul As IFormatProvider
        Dim dt1 As DateTime

        If txtTglLahir.Text <> "" Then
            dt = Convert.ToDateTime(txtTglLahir.Text)
            cul = New System.Globalization.CultureInfo("id-ID", True)
            dt1 = DateTime.Parse(txtTglLahir.Text, cul, System.Globalization.DateTimeStyles.AssumeLocal)
            txtUmur.Text = hitungUmur(dt1.ToShortDateString)
        End If
    End Sub

    Private Sub txtLLAPer_TextChanged(sender As Object, e As EventArgs) Handles txtLLAPer.TextChanged
        If txtLLAPer.Text <> "" Then
            txtLLAPer.BackColor = Color.White
        End If
    End Sub

    Private Sub txtBtkMknPagi_TextChanged(sender As Object, e As EventArgs) Handles txtBtkMkn.TextChanged
        If txtBtkMkn.Text <> "" Then
            txtBtkMkn.BackColor = Color.White
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Call tampilDaftarPermintaan()
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        'DataGridView1.Rows(e.RowIndex).Cells(0).Value = CStr(e.RowIndex + 1)

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next

        DataGridView1.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise
        DataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black
        DataGridView1.DefaultCellStyle.ForeColor = Color.Black
    End Sub

    Private Sub dateFilter_ValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtKelas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtKelas.SelectedIndexChanged
        conn.Close()
        Call koneksiGizi()
        Try
            Dim query As String
            query = "SELECT * FROM t_kelas WHERE kelas = '" & txtKelas.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                txtKdKelas.Text = UCase(dr.GetString("KDKELAS"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub btnTampil_Click(sender As Object, e As EventArgs) Handles btnTampil.Click
        Call filterPermintaan()
    End Sub

    Private Sub txtFilterPasien_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtFilterPasien.SelectedIndexChanged
        Call filterPermintaan()
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

    Private Sub DataGridView4_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView4.RowPostPaint
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

    Private Sub PopulateCheckListBoxesDiagnosa()
        Call koneksiGizi()
        cmd = New MySqlCommand("SELECT diagnosaGizi FROM t_diagnosagizi", conn)
        da = New MySqlDataAdapter(cmd)

        Using sda As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            sda.Fill(dt)
            CheckedListBox1.DisplayMember = "diagnosaGizi"
            CheckedListBox1.ValueMember = "diagnosaGizi"

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    CheckedListBox1.Items.Add(CStr(dt.Rows(i).Item(0)), False)
                Next
            End If
            'AddHandler chk.CheckedChanged, AddressOf CheckBox_Checked
        End Using
    End Sub

    Private Sub CheckedListBox1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        Dim item As String = CheckedListBox1.SelectedItem
        Dim kdItem As String = ""

        Dim startTxt As Integer = 0
        Dim endTxt As Integer
        endTxt = txtDiagGizi.Text.LastIndexOf(item) + 1
        If e.NewValue = CheckState.Checked Then
            txtDiagGizi.AppendText("~" + item + Environment.NewLine)
            txtDiagGizi.SelectAll()
            txtDiagGizi.SelectionBackColor = Color.White
        Else
            While startTxt < endTxt
                txtDiagGizi.Find(item, startTxt, txtDiagGizi.TextLength, RichTextBoxFinds.MatchCase)
                txtDiagGizi.SelectionBackColor = Color.PaleTurquoise
                startTxt = txtDiagGizi.Text.IndexOf(item, startTxt) + 1
                txtDiagGizi.Text.Replace(vbCrLf, "")
                txtDiagGizi.SelectedText = ""
            End While
        End If
    End Sub

    Private Sub btnPulang_Click(sender As Object, e As EventArgs) Handles btnPulang.Click
        Call updateCheckout(txtKdPermintaanLama.Text)
        Call tampilDaftarPermintaan()
    End Sub

    Private Sub txtKelas_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKelas.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If
    End Sub

    Private Sub DataGridView2_CellMouseMove(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseMove
        'DataGridView2.Rows(e.RowIndex).Cells(20).Style.BackColor = Color.DeepSkyBlue
    End Sub

    Private Sub picKeluar_Click(sender As Object, e As EventArgs) Handles picKeluar.Click
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub DataGridView2_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellMouseLeave
        'DataGridView2.Rows(e.RowIndex).Cells(20).Style.BackColor = Color.DodgerBlue
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If cekDistribusi() = "SUDAH" Then
            MsgBox("Maaf anda tidak dapat melakukan edit, dikarenakan order makanan dalam proses distribusi. Silahkan hubungi ke dapur dahulu", MsgBoxStyle.Exclamation)
        ElseIf txtKdKelas.Text.Equals("-") Then
            Me.ErKelas.SetError(Me.txtAlergi, "Kelas dipilih terlebih dahulu")
            MsgBox("Mohon kelas dipilih terlebih dahulu !!", MsgBoxStyle.Exclamation)
        Else
            Try
                conn.Close()
                Call koneksiGizi()
                Dim str As String
                str = "UPDATE t_permintaan SET tglPermintaan = '" & Format(txtTgl.Value, "yyyy-MM-dd HH:mm:ss") & "', kdKelas = '" & txtKdKelas.Text & "', 
                                               alergi = '" & txtAlergi.Text & "', diagnosaGizi = '" & txtDiagGizi.Text & "', 
                                               KdAhligizi = '" & txtKdAhliGizi.Text & "', kdWaktu = '" & txtKdWaktu.Text & "', 
                                               kdDiet = @kdDiet, kdBentukMakanan = '" & txtBtkMkn.Text & "', 
                                               extraDiet = '" & txtExtraDiet.Text & "', keteranganDiet = '" & txtKet.Text & "',
                                               statusUpdate = '0',
                                               userModify = CONCAT(userModify,';" & LoginForm.txtUsername.Text & "'),  
                                               dateModify = CONCAT(dateModify,';" & Format(DateTime.Now, "yyyy-MM-dd HH:mm:ss") & "')
                                         WHERE kdPermintaan = '" & txtKdPermintaanLama.Text & "'"
                Using cmd As New MySqlCommand(str, conn)
                    cmd.Parameters.AddWithValue("@kdDiet", pilihMenu)
                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                End Using

                DataGridView2.ClearSelection()
                DataGridView1.Rows.Clear()

                MsgBox("Order Makanan " & txtWaktu.Text & " berhasil di Edit", MsgBoxStyle.Information)
                conn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Call tampilDaftarPermintaan()
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        'Dim konfirmasi As MsgBoxResult
        'Dim startTime As New TimeSpan(7, 30, 0)  '00:00:01 WIB
        'Dim endTimePagi As New TimeSpan(10, 0, 0) '10:00:00 WIB

        'If txtTgl.Value.TimeOfDay >= startTime And txtTgl.Value.TimeOfDay <= endTimePagi Then
        '    konfirmasi = MsgBox("Anda telah melewati batas waktu perubahan bentuk makanan / menu diet !!!" & vbCrLf &
        '                        "Diharapkan untuk konfirmasi melalui telepon kepada PM" & vbCrLf &
        '                        "Apakah anda sudah konfirmasi PM ?", vbQuestion + vbYesNo, "Konfirmasi")
        '    If konfirmasi = vbYes Then

        '    End If
        'End If

        If txtKdKelas.Text.Equals("-") Then
            Me.ErKelas.SetError(Me.txtAlergi, "Kelas dipilih terlebih dahulu")
            MsgBox("Mohon kelas dipilih terlebih dahulu !!", MsgBoxStyle.Exclamation)
        Else
            Try
                conn.Close()
                Call koneksiGizi()
                Dim str As String
                str = "INSERT INTO t_permintaan (kdPermintaan,tglPermintaan,noDaftarRawatInap,kdRawatInap,
                                                 kdKelas,alergi,diagnosaGizi,
                                                 KdAhligizi,kdWaktu,kdDiet,kdBentukMakanan,
                                                 extraDiet,keteranganDiet,statusUpdate,userModify,dateModify) 
                                         VALUES ('" & txtKdPermintaan.Text & "','" & Format(txtTgl.Value, "yyyy-MM-dd HH:mm:ss") & "','" & txtNoReg.Text & "',
                                                 '" & txtKdRanap.Text & "','" & txtKdKelas.Text & "','" & txtAlergi.Text & "',
                                                 '" & txtDiagGizi.Text & "','" & txtKdAhliGizi.Text & "','" & txtKdWaktu.Text & "',
                                                 @kdDiet,'" & txtBtkMkn.Text & "','" & txtExtraDiet.Text & "',
                                                 '" & txtKet.Text & "','0',
                                                 '" & LoginForm.txtUsername.Text.ToUpper & "','" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "')"
                Using cmd As New MySqlCommand(str, conn)
                    cmd.Parameters.AddWithValue("@kdDiet", pilihMenu)
                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                End Using

                DataGridView2.ClearSelection()
                DataGridView1.Rows.Clear()

                MsgBox("Order Makanan " & txtWaktu.Text & " berhasil ditambahkan", MsgBoxStyle.Information)
                conn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Call updateKondisi(txtKdPermintaanLama.Text)
        Call autoNoPermintaan()
        Call tampilDaftarPermintaan()
    End Sub

    Private Sub DataGridView4_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView4.CellMouseClick
        If e.RowIndex = -1 Then
            Return
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        RiwayatAhliGizi.Show()
    End Sub

    Private Sub txtKelas_LostFocus(sender As Object, e As EventArgs) Handles txtKelas.LostFocus
        If txtKelas.Text = "-" Then
            MsgBox("Pilih kelas terlebih dahulu  !!!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub btnTampil_MouseLeave(sender As Object, e As EventArgs) Handles btnTampil.MouseLeave
        Me.btnTampil.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnTampil.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnTampil_MouseEnter(sender As Object, e As EventArgs) Handles btnTampil.MouseEnter
        Me.btnTampil.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnTampil.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnCari_MouseLeave(sender As Object, e As EventArgs) Handles btnCari.MouseLeave
        Me.btnCari.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnCari.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnCari_MouseEnter(sender As Object, e As EventArgs) Handles btnCari.MouseEnter
        Me.btnCari.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnCari.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub
End Class