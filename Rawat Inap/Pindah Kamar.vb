Imports System.ComponentModel
Imports MySql.Data.MySqlClient
Public Class Pindah_Kamar

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Sub autoComboRanap()
        'Call koneksiServer()
        'cmd = New MySqlCommand("select rawatInap from vw_caritarifkamar GROUP BY rawatInap", conn)
        'da = New MySqlDataAdapter(cmd)
        'Dim dt As New DataTable
        'da.Fill(dt)

        'comboPindahRanap.DataSource = dt
        'comboPindahRanap.DisplayMember = "rawatInap"
        'comboPindahRanap.ValueMember = "rawatInap"
        'comboPindahRanap.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        'comboPindahRanap.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Sub autoComboKelas()
        'Call koneksiServer()
        'cmd = New MySqlCommand("select kelas from vw_caritarifkamar where rawatInap = '" & comboPindahRanap.Text & "' GROUP BY kelas ", conn)
        'da = New MySqlDataAdapter(cmd)
        'Dim dt As New DataTable
        'da.Fill(dt)

        'comboPindahKelas.DataSource = dt
        'comboPindahKelas.DisplayMember = "kelas"
        'comboPindahKelas.ValueMember = "kelas"
        'comboPindahKelas.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        'comboPindahKelas.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Sub autoComboNoKmr()
        'Call koneksiServer()
        'cmd = New MySqlCommand("SELECT noKamar FROM vw_caritarifkamar WHERE rawatInap = '" & comboPindahRanap.Text & "' AND kelas = '" & comboPindahKelas.Text & "' GROUP BY noKamar", conn)
        'da = New MySqlDataAdapter(cmd)
        'Dim dt As New DataTable
        'da.Fill(dt)

        'comboPindahNoKmr.DataSource = dt
        'comboPindahNoKmr.DisplayMember = "noKamar"
        'comboPindahNoKmr.ValueMember = "noKamar"
        'comboPindahNoKmr.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        'comboPindahNoKmr.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Sub autoComboNoBed()
        'Call koneksiServer()
        'cmd = New MySqlCommand("SELECT noBed FROM vw_caritarifkamar WHERE rawatInap = '" & comboPindahRanap.Text & "' AND kelas = '" & comboPindahKelas.Text & "' AND noKamar = '" & comboPindahNoKmr.Text & "'", conn)
        'da = New MySqlDataAdapter(cmd)
        'Dim dt As New DataTable
        'da.Fill(dt)

        'comboPindahNoBed.DataSource = dt
        'comboPindahNoBed.DisplayMember = "noBed"
        'comboPindahNoBed.ValueMember = "noBed"
        'comboPindahNoBed.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        'comboPindahNoBed.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Sub autoComboTarifKamar()
        'Call koneksiServer()
        'Try
        '    Dim query As String
        '    query = "SELECT tarifKmr 
        '             FROM 
        '                 vw_caritarifkamar 
        '             WHERE 
        '                 rawatInap = '" & comboPindahRanap.Text & "' 
        '             AND kelas = '" & comboPindahKelas.Text & "' 
        '             AND noKamar = '" & comboPindahNoKmr.Text & "' 
        '             AND noBed = '" & comboPindahNoBed.Text & "'"
        '    cmd = New MySqlCommand(query, conn)
        '    dr = cmd.ExecuteReader

        '    While dr.Read
        '        txtPindahTarif.Text = UCase(dr.GetString("tarifKmr"))
        '    End While
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub

    Sub autoComboKdTarifKamar()
        'Call koneksiServer()
        'Try
        '    Dim query As String
        '    query = "SELECT
        '             kdTarifKelasKmr 
        '            FROM
        '             vw_caritarifkamar 
        '            WHERE
        '             rawatInap = '" & comboPindahRanap.Text & "' 
        '             AND kelas = '" & comboPindahKelas.Text & "' 
        '             AND noKamar = '" & comboPindahNoKmr.Text & "' 
        '             AND noBed = '" & comboPindahNoBed.Text & "' 
        '             AND tarifKmr = '" & txtPindahTarif.Text & "'"
        '    cmd = New MySqlCommand(query, conn)
        '    dr = cmd.ExecuteReader

        '    While dr.Read
        '        txtPindahKdTarifKmr.Text = UCase(dr.GetString("kdTarifKelasKmr"))
        '    End While
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub

    Sub autoComboDok()
        Call koneksiServer()
        cmd = New MySqlCommand("SELECT namapetugasMedis FROM t_tenagamedis2 WHERE kdKelompokTenagaMedis = 'ktm1' ORDER BY namapetugasMedis ASC", conn)
        da = New MySqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        comboPindahDok.DataSource = dt
        comboPindahDok.DisplayMember = "namapetugasMedis"
        comboPindahDok.ValueMember = "namapetugasMedis"
        comboPindahDok.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        comboPindahDok.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Sub autoDaftarRanap()
        Dim formatId, str, tgl, bln, thn, jam, mnt, dtk As String
        str = "RI"
        tgl = DateTime.Now.ToString("dd")
        bln = DateTime.Now.ToString("MM")
        thn = DateTime.Now.ToString("yy")
        jam = DateTime.Now.ToString("HH")
        mnt = DateTime.Now.ToString("mm")
        dtk = DateTime.Now.ToString("ss")
        formatId = str + tgl + bln + thn + jam + mnt + dtk

        Dim noDaftarRanap As String

        Try
            Call koneksiServer()
            Dim query As String
            Dim cmd As MySqlCommand
            Dim dr As MySqlDataReader
            query = "SELECT SUBSTR(noDaftarRawatInap,16,6) FROM t_registrasirawatinap ORDER BY CAST(SUBSTR(noDaftarRawatInap,16,6) AS UNSIGNED) DESC LIMIT 1"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                dr.Read()
                noDaftarRanap = formatId + "-" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                txtPindahNoDaftarRanap.Text = noDaftarRanap
            Else
                noDaftarRanap = formatId + "-1"
                txtPindahNoDaftarRanap.Text = noDaftarRanap
            End If
            dr.Close()
            conn.Close()
        Catch ex As Exception

        End Try
    End Sub

    Sub tampilTextBoxKdRanap()
        Dim kdRanap As String
        kdRanap = Form1.txtUnitRanap.Text

        Try
            Call koneksiServer()
            Dim str As String
            Dim cmd As MySqlCommand
            Dim dr As MySqlDataReader
            str = "SELECT kdRawatInap FROM t_rawatinap WHERE rawatinap = '" & kdRanap & "'"
            cmd = New MySqlCommand(str, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                txtkdRanap.Text = dr.Item("kdRawatInap")
            End If
            dr.Close()
            conn.Close()
        Catch ex As Exception
        End Try
    End Sub

    Sub tampilTextBoxKdDok()
        Dim kdDok As String
        kdDok = Form1.txtDokter.Text

        Try
            Call koneksiServer()
            Dim str As String
            Dim cmd As MySqlCommand
            Dim dr As MySqlDataReader
            str = "SELECT kdPetugasMedis FROM t_tenagamedis2 WHERE namapetugasMedis = '" & kdDok & "'"
            cmd = New MySqlCommand(str, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                txtKdDok.Text = dr.Item("kdPetugasMedis")
            End If
            dr.Close()
            conn.Close()
        Catch ex As Exception
        End Try
    End Sub

    Sub addPindahKamar()
        Call koneksiServer()
        Try
            Dim str As String = ""
            Dim cmd As MySqlCommand

            Dim startTime As New TimeSpan(0, 0, 1)  '00:00:01 WIB
            Dim endTime As New TimeSpan(12, 59, 59) '12:59:59 WIB
            Dim midTime As New TimeSpan(13, 0, 0)   '13:00:00 WIB

            If datePindah.Value.TimeOfDay >= startTime And datePindah.Value.TimeOfDay <= endTime Then
                'MsgBox("tarif kamar baru")
                str = "INSERT INTO t_registrasirawatinap (noDaftarRawatInap,noDaftar,kdTarifKelasKmr,tglMasukRawatInap,
                                                      asalUnit,kdRawatInap,rawatInap,noKamar,noBed,tarifKmr,kelas) 
                                             VALUES ('" & txtPindahNoDaftarRanap.Text & "','" & txtNoDaftar.Text & "',
                                                     '" & txtPindahKdTarifKmr.Text & "','" & Format(datePindah.Value, "yyyy-MM-dd HH:mm:ss") & "',
                                                     '" & txtRanap.Text & "','" & txtPindahKdRanap.Text & "',
                                                     '" & txtPindahRanap.Text & "','" & txtPindahNoKmr.Text & "',
                                                     '" & txtPindahNoBed.Text & "','" & CDbl(txtPindahTarif.Text) & "',
                                                     '" & txtPindahKelas.Text & "')"
            ElseIf datePindah.Value.TimeOfDay >= midTime Then
                'MsgBox("tarif kamar baru tambah 1 hari")
                str = "INSERT INTO t_registrasirawatinap (noDaftarRawatInap,noDaftar,kdTarifKelasKmr,tglMasukRawatInap,
                                                      asalUnit,kdRawatInap,rawatInap,noKamar,noBed,tarifKmr,kelas) 
                                             VALUES ('" & txtPindahNoDaftarRanap.Text & "','" & txtNoDaftar.Text & "',
                                                     '" & txtPindahKdTarifKmr.Text & "','" & Format(DateAdd(DateInterval.Day, 1, datePindah.Value), "yyyy-MM-dd") & "',
                                                     '" & txtRanap.Text & "','" & txtPindahKdRanap.Text & "',
                                                     '" & txtPindahRanap.Text & "','" & txtPindahNoKmr.Text & "',
                                                     '" & txtPindahNoBed.Text & "','" & CDbl(txtPindahTarif.Text) & "',
                                                     '" & txtPindahKelas.Text & "')"
            End If


            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Pasien pindah kamar berhasil dilakukan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Sub updateRegRanap()
        Call koneksiServer()
        Try
            Dim str As String
            Dim cmd As MySqlCommand
            str = "UPDATE t_registrasirawatinap SET tglKeluarRawatInap = '" & Format(datePindah.Value, "yyyy-MM-dd HH:mm:ss") & "',
                                                    jumlahHariMenginap = '" & txtJumHaper.Text & "', 
                                                    totalMenginap = '" & txtTotalTarif.Text & "' 
                                              WHERE noDaftarRawatInap = '" & txtNoDaftarRanap.Text & "'"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            'MessageBox.Show("Update data Reg Ranap berhasil dilakukan.")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Sub updateStatusKamarSebelum()
        Call koneksiServer()
        Try
            Dim str As String
            Dim cmd As MySqlCommand
            str = "UPDATE t_tarifkelaskamar
                      SET kdStatusBed = 'st6'
                    WHERE kdTarifKelasKmr = '" & txtAwalKdTarifKmr.Text & "'"

            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            'MessageBox.Show("Update data status asal kamar berhasil dilakukan.")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Sub updateStatusKamarSesudah()
        Call koneksiServer()
        Try
            Dim str As String
            Dim cmd As MySqlCommand
            str = "UPDATE t_tarifkelaskamar
                      SET kdStatusBed = 'st5'
                    WHERE kdTarifKelasKmr = '" & txtPindahKdTarifKmr.Text & "'"

            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            'MessageBox.Show("Update data status pindah kamar berhasil dilakukan.")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Sub updateCheckoutGizi(noRegRanap As String)
        Call koneksiGizi()
        Try
            Dim str As String = ""
            str = "UPDATE t_permintaan
                      SET statusUpdate = '2', 
                          dateUpdate = '" & Format(datePindah.Value, "yyyy-MM-dd HH:mm:ss") & "',
                          userModify = CONCAT(userModify,';" & LoginForm.txtUsername.Text & "'),  
                          dateModify = CONCAT(dateModify,';" & Format(DateTime.Now, "yyyy-MM-dd HH:mm:ss") & "')
                    WHERE noDaftarRawatInap = '" & noRegRanap & "'"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            'MsgBox("Pasien Checkout berhasil dilakukan", MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox("Pasien Checkout gagal dilakukan.", MessageBoxIcon.Error, "Error Update Kondisi")
        End Try
        conn.Close()
    End Sub

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

    Function hitungHaper(ByVal tanggal As Date) As String
        Dim y, m, d As Integer
        y = tanggal.Year - dateMasuk.Value.Year
        m = tanggal.Month - dateMasuk.Value.Month
        d = tanggal.Day - dateMasuk.Value.Day

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

    Sub jumHaper()
        Dim masuk As Date = dateMasuk.Value.ToString("dd/MM/yyyy")
        Dim pulang As Date = datePindah.Value.ToString("dd/MM/yyyy")

        Dim hari As Integer
        hari = DateDiff(DateInterval.Day, masuk, pulang)
        If Format(hari) = 0 Then
            txtJumHaper.Text = 1
        ElseIf Format(hari) < 0 Then
            txtJumHaper.Text = 0
        ElseIf Format(hari) > 0 Then
            txtJumHaper.Text = hari + 1
        End If
    End Sub

    Private Sub Pindah_Kamar_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        dateMasuk.Format = DateTimePickerFormat.Custom
        datePindah.Format = DateTimePickerFormat.Custom
        dateMasuk.CustomFormat = "dd-MM-yyyy HH:mm"
        datePindah.CustomFormat = "dd-MM-yyyy HH:mm"

        Call autoDaftarRanap()
        Call autoComboRanap()
        Call autoComboDok()

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Pindah"
                    txtNoDaftarRanap.Text = Form1.txtRegRanap.Text
                    txtNoDaftar.Text = Form1.txtNoDaftar.Text
                    dateMasuk.Text = Form1.txtTglMasuk.Text
                    txtDok.Text = Form1.txtDokter.Text
                    txtRanap.Text = Form1.txtUnitRanap.Text
                    txtAwalKdTarifKmr.Text = Form1.txtKdTarifKlsKmr.Text
                    txtKelas.Text = Form1.txtKelas.Text
                    txtNoKmr.Text = Form1.txtNoKmr.Text
                    txtNoBed.Text = Form1.txtNoBed.Text
                    txtTarifKmr.Text = Form1.txtTarifKmr.Text
            End Select
        End If
        Call tampilTextBoxKdDok()
        Call tampilTextBoxKdRanap()

        btnPindahKmrBed.Select()
        comboPindahDok.SelectedIndex = -1
        comboPindahDok.BackColor = Color.FromArgb(255, 112, 112)
        txtPindahKelas.Text = txtKelas.Text

        txtJmlPindah.Text = cekJmlRuang(txtNoDaftar.Text)
        Call jumHaper()
        If txtJmlPindah.Text > 1 Then
            txtJumHaper.Text = Val(txtJumHaper.Text - 1)
        End If
        txtTotalTarif.Text = Val(txtTarifKmr.Text * txtJumHaper.Text)

        txtTarifKmr.Text = FormatNumber(txtTarifKmr.Text, 0)
        If txtPindahTarif.Text = Nothing Then
            Return
        Else
            txtPindahTarif.Text = FormatNumber(CDbl(txtPindahTarif.Text), 0)
        End If
    End Sub

    Private Sub comboPindahDok_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboPindahDok.SelectedIndexChanged
        Call koneksiServer()
        Try
            Dim query As String
            query = "SELECT * FROM t_tenagamedis2 WHERE namapetugasMedis = '" & comboPindahDok.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                txtPindahKdDok.Text = UCase(dr.GetString("kdPetugasMedis"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub comboPindahKelas_SelectedIndexChanged(sender As Object, e As EventArgs)
        Call autoComboNoKmr()
    End Sub

    Private Sub comboPindahNoKmr_SelectedIndexChanged(sender As Object, e As EventArgs)
        Call autoComboNoBed()
    End Sub

    Private Sub comboPindahNoBed_SelectedIndexChanged(sender As Object, e As EventArgs)
        Call autoComboTarifKamar()
        Call autoComboKdTarifKamar()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Dim status As String
        status = cekStatusBed(txtPindahKdTarifKmr.Text)
        'MsgBox(status)

        If txtPindahNoBed.Text = "" Then
            MsgBox("Pilih kamar terlebih dahulu", MsgBoxStyle.Exclamation, "Warning")
            ErrorProvider1.SetError(txtPindahNoBed, "Pilih kamar terlebih dahulu")
        End If

        If status.Equals("st6", StringComparison.OrdinalIgnoreCase) Then
            'MsgBox("Kamar bisa digunakan", MsgBoxStyle.Information, "Information")
            Dim konfirmasi As MsgBoxResult
            konfirmasi = MsgBox("Apakah anda yakin pasien akan dipindahkan ?", vbQuestion + vbYesNo, "Konfirmasi")
            If konfirmasi = vbYes Then
                If comboPindahDok.Text = "-" Or comboPindahDok.Text = "" Then
                    MsgBox("Pilih DPJP terlebih dahulu", MsgBoxStyle.Exclamation, "Warning")
                    ErrorProvider2.SetError(comboPindahDok, "Pilih DPJP terlebih dahulu")
                ElseIf datePindah.Value < dateMasuk.Value Then
                    MsgBox("'Tanggal Pindah' harus lebih besar dari 'Tanggal Masuk'")
                Else
                    'MsgBox("DPJP ADA")
                    Call addPindahKamar()
                    Call updateRegRanap()
                    Call updateStatusKamarSebelum()
                    Call updateStatusKamarSesudah()
                    Call updateCheckoutGizi(txtNoDaftarRanap.Text)

                    Form1.txtRegRanap.Text = txtPindahNoDaftarRanap.Text
                    Form1.txtAsalPasien.Text = txtPindahRanap.Text
                    Form1.txtTglMasuk.Text = datePindah.Text
                    Form1.txtDokter.Text = txtPindahKdDok.Text
                    Form1.txtUnitRanap.Text = txtPindahRanap.Text
                    Form1.txtKelas.Text = txtPindahKelas.Text
                    Form1.txtNoKmr.Text = txtPindahNoKmr.Text
                    Form1.txtNoBed.Text = txtPindahNoBed.Text
                    Form1.txtKdTarifKlsKmr.Text = txtPindahKdTarifKmr.Text
                    Form1.txtTarifKmr.Text = txtPindahTarif.Text

                    Me.Close()
                End If
            End If
        ElseIf status.Equals("st5", StringComparison.OrdinalIgnoreCase) Then
            MsgBox("Maaf, kamar sudah terlebih dahulu digunakan !!", MsgBoxStyle.Exclamation, "Warning")
            ErrorProvider1.SetError(txtPindahNoBed, "Maaf, kamar sudah terlebih dahulu digunakan !!")
        End If
    End Sub

    Private Sub datePindah_ValueChanged(sender As Object, e As EventArgs) Handles datePindah.ValueChanged
        If datePindah.Value < dateMasuk.Value Then
            MsgBox("'Tanggal Pindah' harus lebih besar dari 'Tanggal Masuk'")
        Else
            'Dim inap As Date = datePindah.Value
            'txtJumHaper.Text = hitungHaper(inap)
            'txtTotalTarif.Text = Val(txtTarifKmr.Text * txtJumHaper.Text)
            Call jumHaper()
            If txtJmlPindah.Text > 1 Then
                txtJumHaper.Text = Val(txtJumHaper.Text - 1)
            End If
            txtTotalTarif.Text = Val(txtTarifKmr.Text * txtJumHaper.Text)
        End If
    End Sub

    Private Sub txtPindahTarif_TextChanged(sender As Object, e As EventArgs) Handles txtPindahTarif.TextChanged
        Me.btnOK.Enabled = True
    End Sub

    Private Sub txtPindahTarif_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPindahTarif.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub btnPindahKmrBed_Click(sender As Object, e As EventArgs) Handles btnPindahKmrBed.Click
        Daftar_Kamar.Ambil_Data = True
        Daftar_Kamar.Form_Ambil_Data = "Daftar Kamar"
        Daftar_Kamar.Show()
    End Sub

    Private Sub btnPindahKmrBed_MouseLeave(sender As Object, e As EventArgs) Handles btnPindahKmrBed.MouseLeave
        Me.btnPindahKmrBed.BackColor = Color.FromArgb(232, 243, 239)
        btnPindahKmrBed.Image = My.Resources.magnifying_glass_green3
    End Sub

    Private Sub btnPindahKmrBed_MouseEnter(sender As Object, e As EventArgs) Handles btnPindahKmrBed.MouseEnter
        Me.btnPindahKmrBed.BackColor = Color.SeaGreen
        btnPindahKmrBed.Image = My.Resources.magnifying_glass
    End Sub

    Private Sub btnPindahKmrBed_KeyDown(sender As Object, e As KeyEventArgs) Handles btnPindahKmrBed.KeyDown
        If e.KeyCode = Keys.Enter Then
            Daftar_Kamar.Ambil_Data = True
            Daftar_Kamar.Form_Ambil_Data = "Daftar Kamar"
            Daftar_Kamar.Show()
        End If
    End Sub

    Private Sub comboPindahDok_TextChanged(sender As Object, e As EventArgs) Handles comboPindahDok.TextChanged
        If comboPindahDok.Text <> "" Then
            comboPindahDok.BackColor = Color.White
            btnOK.Enabled = True
        End If
    End Sub

    Private Sub comboPindahDok_KeyDown(sender As Object, e As KeyEventArgs) Handles comboPindahDok.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnOK.Select()
            If comboPindahDok.Text = "" Then
                comboPindahDok.BackColor = Color.FromArgb(255, 112, 112)
                'btnOK.Enabled = False
            End If
        End If
    End Sub

    Private Sub Pindah_Kamar_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        txtRanap.Text = ""
        txtKelas.Text = ""
        txtNoKmr.Text = ""
        txtNoBed.Text = ""
        txtDok.Text = ""
        txtPindahRanap.Text = ""
        txtPindahKelas.Text = ""
        txtPindahNoKmr.Text = ""
        txtPindahNoBed.Text = ""
        txtPindahTarif.Text = ""
        comboPindahDok.Text = ""
        txtNoDaftarRanap.Text = ""
        txtAwalKdTarifKmr.Text = ""
        txtPindahNoDaftarRanap.Text = ""
        txtNoDaftar.Text = ""
        txtPindahKdTarifKmr.Text = ""
        txtJumHaper.Text = ""
        txtTotalTarif.Text = ""
    End Sub

    Private Sub btnOK_DoubleClick(sender As Object, e As EventArgs) Handles btnOK.DoubleClick
        Return
    End Sub

    Private Sub txtPindahRanap_TextChanged(sender As Object, e As EventArgs) Handles txtPindahRanap.TextChanged
        If txtPindahRanap.Text.Contains("LAVENDER") Then
            txtPindahKelas.Enabled = True
        ElseIf txtPindahRanap.Text.Contains("AMARILIS") Then
            txtPindahKelas.Enabled = True
        Else
            txtPindahKelas.Enabled = False
        End If
    End Sub
End Class