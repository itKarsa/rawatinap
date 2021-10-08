Imports System.ComponentModel
Imports MySql.Data.MySqlClient
Public Class Checkout

    Public Ambil_Data As String
    Public Form_Ambil_Data As String
    Dim jmlRuang As String
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
                txtKdRanap.Text = dr.Item("kdRawatInap")
            End If
            dr.Close()
        Catch ex As Exception
        End Try
        conn.Close()
    End Sub

    Sub updateRegRanap()
        Call koneksiServer()
        Try
            Dim str As String
            Dim cmd As MySqlCommand
            str = "UPDATE t_registrasirawatinap SET tglKeluarRawatInap = '" & Format(datePulang.Value, "yyyy-MM-dd HH:mm:ss") & "', 
                                                    jumlahHariMenginap = '" & txtJumHaper.Text & "', 
                                                    totalMenginap = '" & txtTotalTarif.Text & "' 
                                              WHERE noDaftarRawatInap = '" & txtNoRegRanap.Text & "'"
            'MsgBox(str)
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message & " -Reg.Ranap-")
        End Try
        conn.Close()
    End Sub

    Sub updateStatusBed()
        Call koneksiServer()
        Try
            Dim str As String
            Dim cmd As MySqlCommand
            str = "UPDATE t_tarifkelaskamar SET kdStatusBed = 'st6' 
                                          WHERE kdTarifKelasKmr = '" & txtKdTarifKelasKmr.Text & "'"
            'MsgBox(str)
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Status Bed berhasil update.", MsgBoxStyle.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message & " -Status Bed-")
        End Try
        conn.Close()
    End Sub

    Sub updateCheckout()
        Call koneksiServer()
        Dim str As String = ""
        Dim cmd As MySqlCommand
        If txtTglMeninggal.Text = "" Then
            str = "UPDATE t_registrasi SET tglPulang = '" & Format(datePulang.Value, "yyyy-MM-dd HH:mm:ss") & "',
                                           kdStatusKeluar = '" & txtKdStatusKeluar.Text & "', 
                                           kdCaraKeluar = '" & txtKdCaraKeluar.Text & "',
                                           dirujukke = '" & txtRujuk.Text & "',
                                           alasan = '" & txtAlasan.Text & "'
                                     WHERE noDaftar = '" & txtNoReg.Text & "'"
        Else
            str = "UPDATE t_registrasi SET tglPulang = '" & Format(datePulang.Value, "yyyy-MM-dd HH:mm:ss") & "',
                                           kdStatusKeluar = '" & txtKdStatusKeluar.Text & "', 
                                           kdCaraKeluar = '" & txtKdCaraKeluar.Text & "',
                                           dirujukke = '" & txtRujuk.Text & "',
                                           alasan = '" & txtAlasan.Text & "',
                                           dateMeninggal = '" & txtTglMeninggal.Text & "'
                                     WHERE noDaftar = '" & txtNoReg.Text & "'"
        End If
        'MsgBox(str)

        Try
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Pasien berhasil checkout.", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message & " -Registrasi-")
        End Try
        conn.Close()
    End Sub

    Sub updateCheckoutGizi(noRegRanap As String)
        Call koneksiGizi()
        Try
            Dim str As String = ""
            str = "UPDATE t_permintaan
                      SET statusUpdate = '2', 
                          dateUpdate = '" & Format(datePulang.Value, "yyyy-MM-dd HH:mm:ss") & "',
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

    Function cekPindahRuang()
        Dim result As Integer
        Try
            Call koneksiServer()
            Dim str As String
            Dim cmd As MySqlCommand
            Dim dr As MySqlDataReader
            str = "SELECT COUNT(noDaftarRawatInap) AS jml FROM t_registrasirawatinap WHERE noDaftar = '" & txtNoReg.Text & "'"
            cmd = New MySqlCommand(str, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                result = dr.Item("jml")
            End If
            dr.Close()
        Catch ex As Exception
        End Try
        conn.Close()

        Return result
    End Function

    Function hitungHaper(ByVal masuk As Date, ByVal pulang As Date) As String
        Dim hari As Integer
        hari = DateDiff(DateInterval.Day, masuk, pulang)
        Return hari + 1
    End Function

    Sub jumHaper()
        Dim masuk As Date = dateMasuk.Value.ToString("dd/MM/yyyy")
        Dim pulang As Date = datePulang.Value.ToString("dd/MM/yyyy")

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

    Sub autoComboStatus()
        Call koneksiServer()
        cmd = New MySqlCommand("SELECT statusKeluar FROM t_statuskeluar WHERE kdInstalasi = 'ki2' order by kdStatusKeluar asc", conn)
        da = New MySqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        comboStatusKeluar.DataSource = dt
        comboStatusKeluar.DisplayMember = "statusKeluar"
        comboStatusKeluar.ValueMember = "statusKeluar"
        comboStatusKeluar.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        comboStatusKeluar.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Sub autoComboCara()
        Call koneksiServer()
        cmd = New MySqlCommand("SELECT caraKeluar FROM t_carakeluar WHERE kdCaraKeluar NOT IN(6,7,8,10,11)", conn)
        da = New MySqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        comboCaraKeluar.DataSource = dt
        comboCaraKeluar.DisplayMember = "caraKeluar"
        comboCaraKeluar.ValueMember = "caraKeluar"
        comboCaraKeluar.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        comboCaraKeluar.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Sub autoComboDokter()
        Call koneksiServer()
        cmd = New MySqlCommand("SELECT namapetugasMedis FROM t_tenagamedis2 WHERE kdKelompokTenagaMedis = 'ktm1' AND namapetugasMedis LIKE '%" & comboDokter.Text & "%' ORDER BY kdPetugasMedis ASC", conn)
        da = New MySqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        comboDokter.DataSource = dt
        comboDokter.DisplayMember = "namapetugasMedis"
        comboDokter.ValueMember = "namapetugasMedis"
        comboDokter.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        comboDokter.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Private Sub Checkout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dateMasuk.Format = DateTimePickerFormat.Custom
        dateKematian.Format = DateTimePickerFormat.Custom
        datePulang.Format = DateTimePickerFormat.Custom
        dateMasuk.CustomFormat = "dd-MM-yyyy HH:mm"
        dateKematian.CustomFormat = "dd-MM-yyyy HH:mm"
        datePulang.CustomFormat = "dd-MM-yyyy HH:mm"
        GroupBox3.Visible = False
        Call autoComboStatus()
        Call autoComboCara()
        Call autoComboDokter()
        Call tampilTextBoxKdRanap()

        clearTextbox()

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Cekout"
                    txtNoReg.Text = Form1.txtNoDaftar.Text
                    txtNoRegRanap.Text = Form1.txtRegRanap.Text
                    txtRM.Text = Form1.txtRekMed.Text
                    dateMasuk.Text = Form1.txtTglMasuk.Text
                    txtRanap.Text = Form1.txtUnitRanap.Text
                    txtNoKmr.Text = Form1.txtNoKmr.Text
                    txtNoBed.Text = Form1.txtNoBed.Text
                    txtKelas.Text = Form1.txtKelas.Text
                    txtTarif.Text = Form1.txtTarifKmr.Text
                    comboDokter.Text = Form1.comboDokter.Text
                    txtKdTarifKelasKmr.Text = Form1.txtKdTarifKlsKmr.Text
                    txtNamaPasien.Text = Form1.txtNamaPasien.Text
            End Select
        End If

        'btnOk.Enabled = True
        'datePulang.Enabled = False

        'comboStatusKeluar.SelectedValue = -1
        'comboCaraKeluar.SelectedValue = -1
        'comboDokter.SelectedValue = -1

        comboStatusKeluar.BackColor = Color.FromArgb(255, 112, 112)
        comboCaraKeluar.BackColor = Color.FromArgb(255, 112, 112)
        txtPrimer.BackColor = Color.FromArgb(255, 112, 112)
        txtIcdPrimer.BackColor = Color.FromArgb(255, 112, 112)
        txtJenis0.BackColor = Color.FromArgb(255, 112, 112)
        txtKasus0.BackColor = Color.FromArgb(255, 112, 112)
        'comboDokter.BackColor = Color.FromArgb(255, 112, 112)

        'Dim inap As Date = datePulang.Value
        'txtJumHaper.Text = hitungHaper(inap)
        'txtTotalTarif.Text = Val(txtTarif.Text * txtJumHaper.Text)
        jmlRuang = cekJmlRuang(txtNoReg.Text)
        Call jumHaper()
        If jmlRuang > 1 Then
            txtJumHaper.Text = Val(txtJumHaper.Text - 1)
        End If
        txtTotalTarif.Text = Val(txtTarif.Text * txtJumHaper.Text)
    End Sub

    Private Sub comboStatusKeluar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboStatusKeluar.SelectedIndexChanged
        If comboStatusKeluar.Text = "Meninggal < 48 jam" Then
            GroupBox3.Visible = True
            txtTglMeninggal.Text = Format(dateKematian.Value, "yyyy-MM-dd HH:mm:ss")
            Me.Height = 780
        ElseIf comboStatusKeluar.Text = "Meninggal >= 48 jam" Then
            GroupBox3.Visible = True
            txtTglMeninggal.Text = Format(dateKematian.Value, "yyyy-MM-dd HH:mm:ss")
            Me.Height = 780
        Else
            GroupBox3.Visible = False
            txtTglMeninggal.Text = ""
            Me.Height = 700
        End If

        Call koneksiServer()
        Try
            Dim query As String
            Dim cmd As MySqlCommand
            Dim dr As MySqlDataReader
            query = "select * from t_statuskeluar where statusKeluar = '" & comboStatusKeluar.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                txtKdStatusKeluar.Text = UCase(dr.GetString("kdStatusKeluar"))
            End While
            dr.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub comboCaraKeluar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboCaraKeluar.SelectedIndexChanged
        If comboCaraKeluar.Text = "Dirujuk" Then
            GroupBox8.Visible = True
            Me.Height = 830
        Else
            GroupBox8.Visible = False
            Me.Height = 780
        End If

        Call koneksiServer()
        Try
            Dim query As String
            Dim cmd As MySqlCommand
            Dim dr As MySqlDataReader
            query = "select * from t_carakeluar where caraKeluar = '" & comboCaraKeluar.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                txtKdCaraKeluar.Text = UCase(dr.GetString("kdCaraKeluar"))
            End While
            dr.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub comboDokter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboDokter.SelectedIndexChanged
        Call koneksiServer()
        Try
            Dim query As String
            Dim cmd As MySqlCommand
            Dim dr As MySqlDataReader
            query = "select * from t_tenagamedis2 where namapetugasMedis = '" & comboDokter.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                txtKdDok.Text = UCase(dr.GetString("kdPetugasMedis"))
            End While
            dr.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Sub insertDiagnosa()
        Call koneksiServer()
        Try
            Dim query As String
            Dim cmd As MySqlCommand
            query = "INSERT INTO t_diagnosaakhir (noDaftar,noRekamMedis,kdIcd10,icd10,
                                                  kdJenisDiagnosa,kdJenisKasus,kdTenagaMedis) 
                                          VALUES (@noDaftar,@noRekamMedis,@kdIcd10,@icd10,
                                                  @kdJenisDiagnosa,@kdJenisKasus,@kdTenagaMedis)"
            cmd = New MySqlCommand(query, conn)

            cmd.Parameters.AddWithValue("@noDaftar", txtNoReg.Text)
            cmd.Parameters.AddWithValue("@noRekamMedis", txtRM.Text)
            cmd.Parameters.AddWithValue("@kdIcd10", txtPrimer.Text)
            cmd.Parameters.AddWithValue("@icd10", txtIcdPrimer.Text)
            cmd.Parameters.AddWithValue("@kdJenisDiagnosa", kdJenis0.Text)
            cmd.Parameters.AddWithValue("@kdJenisKasus", kdKasus0.Text)
            cmd.Parameters.AddWithValue("@kdTenagaMedis", txtKdDok.Text)
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()

            If Not txt1.Text = "" And txtSek1.Text = "" And kdJenis1.Text = "" And kdJenis1.Text = "" Then
                cmd.Parameters.AddWithValue("@noDaftar", txtNoReg.Text)
                cmd.Parameters.AddWithValue("@noRekamMedis", txtRM.Text)
                cmd.Parameters.AddWithValue("@kdIcd10", txt1.Text)
                cmd.Parameters.AddWithValue("@icd10", txtSek1.Text)
                cmd.Parameters.AddWithValue("@kdJenisDiagnosa", kdJenis1.Text)
                cmd.Parameters.AddWithValue("@kdJenisKasus", kdKasus1.Text)
                cmd.Parameters.AddWithValue("@kdTenagaMedis", txtKdDok.Text)
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
            ElseIf Not txt2.Text = "" And txtSek2.Text = "" And kdJenis2.Text = "" And kdJenis2.Text = "" Then
                cmd.Parameters.AddWithValue("@noDaftar", txtNoReg.Text)
                cmd.Parameters.AddWithValue("@noRekamMedis", txtRM.Text)
                cmd.Parameters.AddWithValue("@kdIcd10", txt2.Text)
                cmd.Parameters.AddWithValue("@icd10", txtSek2.Text)
                cmd.Parameters.AddWithValue("@kdJenisDiagnosa", kdJenis2.Text)
                cmd.Parameters.AddWithValue("@kdJenisKasus", kdKasus2.Text)
                cmd.Parameters.AddWithValue("@kdTenagaMedis", txtKdDok.Text)
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
            ElseIf Not txt3.Text = "" And txtSek3.Text = "" And kdJenis3.Text = "" And kdJenis3.Text = "" Then
                cmd.Parameters.AddWithValue("@noDaftar", txtNoReg.Text)
                cmd.Parameters.AddWithValue("@noRekamMedis", txtRM.Text)
                cmd.Parameters.AddWithValue("@kdIcd10", txt3.Text)
                cmd.Parameters.AddWithValue("@icd10", txtSek3.Text)
                cmd.Parameters.AddWithValue("@kdJenisDiagnosa", kdJenis3.Text)
                cmd.Parameters.AddWithValue("@kdJenisKasus", kdKasus3.Text)
                cmd.Parameters.AddWithValue("@kdTenagaMedis", txtKdDok.Text)
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
            ElseIf Not txt4.Text = "" And txtSek4.Text = "" And kdJenis4.Text = "" And kdJenis4.Text = "" Then
                cmd.Parameters.AddWithValue("@noDaftar", txtNoReg.Text)
                cmd.Parameters.AddWithValue("@noRekamMedis", txtRM.Text)
                cmd.Parameters.AddWithValue("@kdIcd10", txt4.Text)
                cmd.Parameters.AddWithValue("@icd10", txtSek4.Text)
                cmd.Parameters.AddWithValue("@kdJenisDiagnosa", kdJenis4.Text)
                cmd.Parameters.AddWithValue("@kdJenisKasus", kdKasus4.Text)
                cmd.Parameters.AddWithValue("@kdTenagaMedis", txtKdDok.Text)
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
            End If
            'MsgBox("Insert data Diagnosa berhasil dilakukan", MsgBoxStyle.Information, "Informasi")
            cmd.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message & " -Diagnosa-", MsgBoxStyle.Information, "Informasi")
            cmd.Dispose()
        End Try
        conn.Close()
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        'txtJumHaper.Text = hitungHaper(dateMasuk.Value, datePulang.Value)
        txtTotalTarif.Text = Val(txtTarif.Text * txtJumHaper.Text)

        Dim konfirmasi As MsgBoxResult

        If comboStatusKeluar.Text = "" Then
            Me.ErrorStatus.SetError(Me.comboStatusKeluar, "Tolong diinputkan terlebih dahulu")
            MsgBox("Pilih Status keluar terlebih dahulu", MsgBoxStyle.Exclamation, "Warning")
        ElseIf comboCaraKeluar.Text = "" Then
            Me.ErrorCara.SetError(Me.comboCaraKeluar, "Tolong diinputkan terlebih dahulu")
            MsgBox("Pilih Cara keluar terlebih dahulu", MsgBoxStyle.Exclamation, "Warning")
        ElseIf comboDokter.Text = "-" Or comboDokter.Text = "" Then
            Me.ErrorDPJP.SetError(Me.comboDokter, "Tolong diinputkan terlebih dahulu")
            MsgBox("Pilih DPJP keluar terlebih dahulu", MsgBoxStyle.Exclamation, "Warning")
        ElseIf txtIcdPrimer.Text = "" Then
            Me.ErrorIcdPrimer.SetError(Me.txtIcdPrimer, "Tolong diinputkan telebih dahulu")
        ElseIf txtJenis0.Text = "" Then
            Me.ErrorJenis0.SetError(Me.txtJenis0, "Tolong diinputkan telebih dahulu")
        ElseIf txtKasus0.Text = "" Then
            Me.ErrorKasus0.SetError(Me.txtKasus0, "Tolong diinputkan telebih dahulu")
        ElseIf datePulang.Value < dateMasuk.Value Then
            MsgBox("'Tanggal KRS' harus melebihi dari 'Tanggal MRS'")
        ElseIf dateKematian.Value < dateMasuk.Value Then
            MsgBox("'Tanggal KRS' harus melebihi dari 'Tanggal MRS'")
        ElseIf comboCaraKeluar.Text = "Dirujuk" Then
            If txtRujuk.Text = "" Then
                MsgBox("Mohon isi tujuan rukujukan terlebih dahulu")
                Me.ErrorDirujuk.SetError(Me.txtRujuk, "Mohon isi tujuan rukujukan terlebih dahulu")
            ElseIf txtAlasan.Text = "" Then
                MsgBox("Mohon isi alasan dirujuknya pasien")
                Me.ErrorAlasan.SetError(Me.txtAlasan, "Mohon isi alasan dirujuknya pasien")
            Else
                ErrorDirujuk.Clear()
                ErrorAlasan.Clear()
                ErrorStatus.Clear()
                ErrorCara.Clear()
                ErrorDPJP.Clear()
                ErrorIcdPrimer.Clear()
                ErrorJenis0.Clear()
                ErrorKasus0.Clear()

                konfirmasi = MsgBox("Apakah anda yakin pasien '" & txtNamaPasien.Text & "' dicheckoutkan tgl. '" & datePulang.Value & "' ?", vbQuestion + vbYesNo, "Konfirmasi")
                If konfirmasi = vbYes Then

                    Call updateCheckout()
                    Call updateStatusBed()
                    Call updateRegRanap()
                    Call insertDiagnosa()
                    Call updateCheckoutGizi(txtNoRegRanap.Text)
                    'MsgBox("Kode Reg : " & txtNoReg.Text & vbNewLine &
                    '       "Kode Reg.Ranap : " & txtNoRegRanap.Text & vbNewLine &
                    '       "Kode Tarif Kelas : " & txtKdTarifKelasKmr.Text)
                    Form1.txtJumInap.Text = txtJumHaper.Text & " HARI"
                    Form1.txtTotal.Text = txtTotalTarif.Text
                    Me.Close()
                End If
            End If
        Else
            ErrorStatus.Clear()
            ErrorCara.Clear()
            ErrorDPJP.Clear()
            ErrorIcdPrimer.Clear()
            ErrorJenis0.Clear()
            ErrorKasus0.Clear()

            konfirmasi = MsgBox("Apakah anda yakin pasien '" & txtNamaPasien.Text & "' dicheckoutkan tgl. '" & datePulang.Value & "' ?", vbQuestion + vbYesNo, "Konfirmasi")
            If konfirmasi = vbYes Then

                Call updateCheckout()
                Call updateStatusBed()
                Call updateRegRanap()
                Call insertDiagnosa()
                Call updateCheckoutGizi(txtNoRegRanap.Text)
                'MsgBox("Kode Reg : " & txtNoReg.Text & vbNewLine &
                '       "Kode Reg.Ranap : " & txtNoRegRanap.Text & vbNewLine &
                '       "Kode Tarif Kelas : " & txtKdTarifKelasKmr.Text)
                Form1.txtJumInap.Text = txtJumHaper.Text & " HARI"
                Form1.txtTotal.Text = txtTotalTarif.Text
                Me.Close()
            End If
        End If

    End Sub

    Private Sub datePulang_ValueChanged(sender As Object, e As EventArgs) Handles datePulang.ValueChanged
        'Dim inap As Date = datePulang.Value
        'txtJumHaper.Text = hitungHaper(inap)
        'txtTotalTarif.Text = Val(txtTarif.Text * txtJumHaper.Text)
        Call jumHaper()
        If jmlRuang > 1 Then
            txtJumHaper.Text = Val(txtJumHaper.Text - 1)
        End If
        txtTotalTarif.Text = Val(txtTarif.Text * txtJumHaper.Text)
    End Sub

    Private Sub txtJenis2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtJenis1.SelectedIndexChanged
        Call koneksiServer()
        Try
            Dim query As String
            query = "select * from t_jenisdiagnosa where jenisDiagnosa = '" & txtJenis1.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                kdJenis1.Text = UCase(dr.GetString("kdJenisDiagnosa"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtJenis3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtJenis2.SelectedIndexChanged
        Call koneksiServer()
        Try
            Dim query As String
            query = "select * from t_jenisdiagnosa where jenisDiagnosa = '" & txtJenis2.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                kdJenis2.Text = UCase(dr.GetString("kdJenisDiagnosa"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtJenis4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtJenis3.SelectedIndexChanged
        Call koneksiServer()
        Try
            Dim query As String
            query = "select * from t_jenisdiagnosa where jenisDiagnosa = '" & txtJenis3.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                kdJenis3.Text = UCase(dr.GetString("kdJenisDiagnosa"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtJenis5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtJenis4.SelectedIndexChanged
        Call koneksiServer()
        Try
            Dim query As String
            query = "select * from t_jenisdiagnosa where jenisDiagnosa = '" & txtJenis4.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                kdJenis4.Text = UCase(dr.GetString("kdJenisDiagnosa"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtKasus1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtKasus0.SelectedIndexChanged
        Call koneksiServer()
        Try
            Dim query As String
            query = "select * from t_kasusdiagnosa where kasus = '" & txtKasus0.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                kdKasus0.Text = UCase(dr.GetString("kdKasusDiagnosa"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtKasus2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtKasus1.SelectedIndexChanged
        Call koneksiServer()
        Try
            Dim query As String
            query = "select * from t_kasusdiagnosa where kasus = '" & txtKasus1.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                kdKasus1.Text = UCase(dr.GetString("kdKasusDiagnosa"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtKasus3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtKasus2.SelectedIndexChanged
        Call koneksiServer()
        Try
            Dim query As String
            query = "select * from t_kasusdiagnosa where kasus = '" & txtKasus2.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                kdKasus2.Text = UCase(dr.GetString("kdKasusDiagnosa"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtKasus4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtKasus3.SelectedIndexChanged
        Call koneksiServer()
        Try
            Dim query As String
            query = "select * from t_kasusdiagnosa where kasus = '" & txtKasus3.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                kdKasus3.Text = UCase(dr.GetString("kdKasusDiagnosa"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtKasus5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtKasus4.SelectedIndexChanged
        Call koneksiServer()
        Try
            Dim query As String
            query = "select * from t_kasusdiagnosa where kasus = '" & txtKasus4.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                kdKasus4.Text = UCase(dr.GetString("kdKasusDiagnosa"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnPrimer_Click(sender As Object, e As EventArgs) Handles btnPrimer.Click
        Dim ficd10 As Daftar_ICD10 = New Daftar_ICD10
        ficd10.fco = Me
        ficd10.Ambil_Data = True
        ficd10.Form_Ambil_Data = "ICD10Primer"
        ficd10.Show()
    End Sub

    Private Sub btn1_Click(sender As Object, e As EventArgs) Handles btn1.Click
        Dim ficd10 As Daftar_ICD10 = New Daftar_ICD10
        ficd10.fco = Me
        ficd10.Ambil_Data = True
        ficd10.Form_Ambil_Data = "ICD10Sek1"
        ficd10.Show()
    End Sub

    Private Sub btn2_Click(sender As Object, e As EventArgs) Handles btn2.Click
        Dim ficd10 As Daftar_ICD10 = New Daftar_ICD10
        ficd10.fco = Me
        ficd10.Ambil_Data = True
        ficd10.Form_Ambil_Data = "ICD10Sek2"
        ficd10.Show()
    End Sub

    Private Sub btn3_Click(sender As Object, e As EventArgs) Handles btn3.Click
        Dim ficd10 As Daftar_ICD10 = New Daftar_ICD10
        ficd10.fco = Me
        ficd10.Ambil_Data = True
        ficd10.Form_Ambil_Data = "ICD10Sek3"
        ficd10.Show()
    End Sub

    Private Sub btn4_Click(sender As Object, e As EventArgs) Handles btn4.Click
        Dim ficd10 As Daftar_ICD10 = New Daftar_ICD10
        ficd10.fco = Me
        ficd10.Ambil_Data = True
        ficd10.Form_Ambil_Data = "ICD10Sek4"
        ficd10.Show()
    End Sub

    Private Sub comboStatusKeluar_KeyDown(sender As Object, e As KeyEventArgs) Handles comboStatusKeluar.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If comboStatusKeluar.Text = "" Then
                comboStatusKeluar.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub comboStatusKeluar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles comboStatusKeluar.KeyPress
        If System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "[^a-zA-Z()\b]") Then
            e.Handled = True
        End If
    End Sub

    Private Sub comboStatusKeluar_TextChanged(sender As Object, e As EventArgs) Handles comboStatusKeluar.TextChanged
        If comboStatusKeluar.Text <> "" Then
            comboStatusKeluar.BackColor = Color.White
            Me.ErrorStatus.Clear()
        End If
    End Sub

    Private Sub comboCaraKeluar_KeyDown(sender As Object, e As KeyEventArgs) Handles comboCaraKeluar.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If comboCaraKeluar.Text = "" Then
                comboCaraKeluar.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub comboCaraKeluar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles comboCaraKeluar.KeyPress
        If System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "[^a-zA-Z()\b]") Then
            e.Handled = True
        End If
    End Sub

    Private Sub comboCaraKeluar_TextChanged(sender As Object, e As EventArgs) Handles comboCaraKeluar.TextChanged
        If comboCaraKeluar.Text <> "" Then
            comboCaraKeluar.BackColor = Color.White
            Me.ErrorCara.Clear()
        End If
    End Sub

    Private Sub comboDokter_KeyDown(sender As Object, e As KeyEventArgs) Handles comboDokter.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If comboDokter.Text = "" Then
                comboDokter.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub comboDokter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles comboDokter.KeyPress
        If System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "[^a-zA-Z()\b. ]") Then
            e.Handled = True
        End If
    End Sub

    Private Sub comboDokter_TextChanged(sender As Object, e As EventArgs) Handles comboDokter.TextChanged
        If comboDokter.Text <> "" Then
            comboDokter.BackColor = Color.White
            Me.ErrorDPJP.Clear()
        End If
    End Sub

    Private Sub btnPrimer_KeyDown(sender As Object, e As KeyEventArgs) Handles btnPrimer.KeyDown
        Dim ficd10 As Daftar_ICD10 = New Daftar_ICD10
        ficd10.fco = Me
        If e.KeyCode = Keys.Enter Then
            ficd10.Ambil_Data = True
            ficd10.Form_Ambil_Data = "ICD10Primer"
            ficd10.Show()
        End If
    End Sub

    Private Sub btn1_KeyDown(sender As Object, e As KeyEventArgs) Handles btn1.KeyDown
        Dim ficd10 As Daftar_ICD10 = New Daftar_ICD10
        ficd10.fco = Me
        If e.KeyCode = Keys.Enter Then
            ficd10.Ambil_Data = True
            ficd10.Form_Ambil_Data = "ICD10Sek1"
            ficd10.Show()
        End If
    End Sub

    Private Sub btn2_KeyDown(sender As Object, e As KeyEventArgs) Handles btn2.KeyDown
        Dim ficd10 As Daftar_ICD10 = New Daftar_ICD10
        ficd10.fco = Me
        If e.KeyCode = Keys.Enter Then
            ficd10.Ambil_Data = True
            ficd10.Form_Ambil_Data = "ICD10Sek2"
            ficd10.Show()
        End If
    End Sub

    Private Sub btn3_KeyDown(sender As Object, e As KeyEventArgs) Handles btn3.KeyDown
        Dim ficd10 As Daftar_ICD10 = New Daftar_ICD10
        ficd10.fco = Me
        If e.KeyCode = Keys.Enter Then
            ficd10.Ambil_Data = True
            ficd10.Form_Ambil_Data = "ICD10Sek3"
            ficd10.Show()
        End If
    End Sub

    Private Sub btn4_KeyDown(sender As Object, e As KeyEventArgs) Handles btn4.KeyDown
        Dim ficd10 As Daftar_ICD10 = New Daftar_ICD10
        ficd10.fco = Me
        If e.KeyCode = Keys.Enter Then
            ficd10.Ambil_Data = True
            ficd10.Form_Ambil_Data = "ICD10Sek4"
            ficd10.Show()
        End If
    End Sub

    Private Sub txtIcdPrimer_TextChanged(sender As Object, e As EventArgs) Handles txtIcdPrimer.TextChanged
        If txtIcdPrimer.Text <> "" Then
            txtIcdPrimer.BackColor = Color.White
            Me.ErrorIcdPrimer.Clear()
            datePulang.Enabled = True
        End If
    End Sub

    Private Sub txtPrimer_TextChanged(sender As Object, e As EventArgs) Handles txtPrimer.TextChanged
        If txtPrimer.Text <> "" Then
            txtPrimer.BackColor = Color.White
        End If
    End Sub

    Private Sub txtJenis0_TextChanged(sender As Object, e As EventArgs) Handles txtJenis0.TextChanged
        If txtJenis0.Text <> "" Then
            txtJenis0.BackColor = Color.White
            Me.ErrorJenis0.Clear()
        End If
    End Sub

    Private Sub txtKasus0_TextChanged(sender As Object, e As EventArgs) Handles txtKasus0.TextChanged
        If txtKasus0.Text <> "" Then
            txtKasus0.BackColor = Color.White
            Me.ErrorKasus0.Clear()
        End If
    End Sub
    Private Sub btnOk_MouseLeave(sender As Object, e As EventArgs) Handles btnOk.MouseLeave
        Me.btnOk.BackColor = Color.SeaGreen
    End Sub

    Private Sub btnOk_MouseEnter(sender As Object, e As EventArgs) Handles btnOk.MouseEnter
        Me.btnOk.BackColor = Color.MediumSeaGreen
    End Sub

    Sub clearTextbox()
        txtRanap.Text = ""
        txtKelas.Text = ""
        txtNoKmr.Text = ""
        txtNoBed.Text = ""
        txtTarif.Text = ""
        txtNoReg.Text = ""
        txtNamaPasien.Text = ""
        txtNoRegRanap.Text = ""
        txtJumHaper.Text = ""
        txtTotalTarif.Text = ""
        txtRM.Text = ""
        txtKdTarifKelasKmr.Text = ""
        comboStatusKeluar.SelectedValue = -1
        comboCaraKeluar.SelectedValue = -1
        comboDokter.Text = ""
        txtPrimer.Text = ""
        txt1.Text = ""
        txt2.Text = ""
        txt3.Text = ""
        txt4.Text = ""
        txtIcdPrimer.Text = ""
        txtSek1.Text = ""
        txtSek2.Text = ""
        txtSek3.Text = ""
        txtSek4.Text = ""
        txtJenis0.Text = ""
        txtJenis1.SelectedValue = -1
        txtJenis2.SelectedValue = -1
        txtJenis3.SelectedValue = -1
        txtJenis4.SelectedValue = -1
        txtKasus0.SelectedValue = -1
        txtKasus1.SelectedValue = -1
        txtKasus2.SelectedValue = -1
        txtKasus3.SelectedValue = -1
        txtKasus4.SelectedValue = -1
        kdJenis0.Text = ""
        kdJenis1.Text = ""
        kdJenis2.Text = ""
        kdJenis3.Text = ""
        kdJenis4.Text = ""
        kdKasus0.Text = ""
        kdKasus1.Text = ""
        kdKasus2.Text = ""
        kdKasus3.Text = ""
        kdKasus4.Text = ""
    End Sub

    Private Sub Checkout_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Call clearTextbox()
    End Sub

    Private Sub btnOk_DoubleClick(sender As Object, e As EventArgs) Handles btnOk.DoubleClick
        Return
    End Sub

    Private Sub Checkout_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        Call clearTextbox()
    End Sub

    Private Sub dateKematian_ValueChanged(sender As Object, e As EventArgs) Handles dateKematian.ValueChanged
        txtTglMeninggal.Text = Format(dateKematian.Value, "yyyy-MM-dd HH:mm:ss")
    End Sub
End Class