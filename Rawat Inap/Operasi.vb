Imports System.ComponentModel
Imports MySql.Data.MySqlClient
Public Class Operasi

    Dim diagnos As String
    Dim numOrder As Integer = 0

    Dim kdTindakan, tindakan, kdTarif, tarif, subTotal, stats, tmline, notind, noRegLama, valKdTarif As String
    Dim totTarif As Double

    Sub riwayatOpe()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        query = "SELECT * 
                   FROM vw_riwayatpasienop
                  WHERE noRM = '" & txtNoRM.Text & "' AND statusHapus = '0'
               ORDER BY tglPermintaanOP DESC"

        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dgv.Rows.Clear()

        Do While dr.Read
            dgv.Rows.Add(Convert.ToDateTime(dr.Item("tglPermintaanOP")), dr.Item("tindakan"), dr.Item("dokterOP"),
                         dr.Item("diagnosaPra"), dr.Item("diagnosaPost"), dr.Item("keterangan"),
                         dr.Item("sarsCovid"), dr.Item("statusOP"))
        Loop
        conn.Close()
    End Sub

    Sub autoDokter()
        Call koneksiServer()

        Using cmd As New MySqlCommand("SELECT DISTINCT namapetugasMedis FROM t_tenagamedis2 WHERE kdKelompokTenagaMedis in('ktm1') ORDER BY namapetugasMedis ASC", conn)
            da = New MySqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)

            txtDokter.DataSource = dt
            txtDokter.DisplayMember = "namapetugasMedis"
            txtDokter.ValueMember = "namapetugasMedis"
            txtDokter.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            txtDokter.AutoCompleteSource = AutoCompleteSource.ListItems
        End Using
        conn.Close()
    End Sub

    Sub autoDokterPra()
        Call koneksiServer()

        Using cmd As New MySqlCommand("SELECT DISTINCT namapetugasMedis FROM t_tenagamedis2 WHERE kdKelompokTenagaMedis in('ktm1') ORDER BY namapetugasMedis ASC", conn)
            da = New MySqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)

            txtDokterPra.DataSource = dt
            txtDokterPra.DisplayMember = "namapetugasMedis"
            txtDokterPra.ValueMember = "namapetugasMedis"
            txtDokterPra.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            txtDokterPra.AutoCompleteSource = AutoCompleteSource.ListItems
        End Using
        conn.Close()
    End Sub

    Sub autoJenisOP()
        Call koneksiServer()

        Using cmd As New MySqlCommand("SELECT jenisOperasi FROM t_jenisop", conn)
            da = New MySqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)

            txtJenisOP.DataSource = dt
            txtJenisOP.DisplayMember = "jenisOperasi"
            txtJenisOP.ValueMember = "jenisOperasi"
            txtJenisOP.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            txtJenisOP.AutoCompleteSource = AutoCompleteSource.ListItems
        End Using
        conn.Close()
    End Sub

    Sub autoNoRegOP()
        Dim noPermintaanOP As String

        Try
            Call koneksiServer()
            Dim query As String
            Dim cmdNoReg As MySqlCommand
            Dim drNoReg As MySqlDataReader
            query = "SELECT SUBSTR(noRegistrasiOP,16,4) FROM t_registrasiop WHERE DATE(tglRegistrasiOP) = DATE(NOW()) ORDER BY CAST(SUBSTR(noRegistrasiOP,16,4) AS UNSIGNED) DESC LIMIT 1"
            cmdNoReg = New MySqlCommand(query, conn)
            drNoReg = cmdNoReg.ExecuteReader
            drNoReg.Read()
            If drNoReg.HasRows Then
                drNoReg.Read()
                noPermintaanOP = "OP" + Format(Now, "yyMMddHHmmss") + "-" + (Val(Trim(drNoReg.Item(0).ToString)) + 1).ToString
                txtNoRegOp.Text = noPermintaanOP
            Else
                noPermintaanOP = "OP" + Format(Now, "yyMMddHHmmss") + "-1"
                txtNoRegOp.Text = noPermintaanOP
            End If
            conn.Close()
        Catch ex As Exception

        End Try

    End Sub

    Sub autoNoRegAnestesi()
        Dim noPermintaanAne As String

        Try
            Call koneksiServer()
            Dim query As String
            Dim cmdNoReg As MySqlCommand
            Dim drNoReg As MySqlDataReader
            query = "SELECT SUBSTR(noTindakanAnestesi,17,4) 
                       FROM t_tindakananestesi 
                      WHERE DATE(SUBSTR(tglUser,2,10)) = DATE(NOW()) 
                   ORDER BY CAST(SUBSTR(noTindakanAnestesi,17,4) AS UNSIGNED) DESC LIMIT 1"
            cmdNoReg = New MySqlCommand(query, conn)
            drNoReg = cmdNoReg.ExecuteReader
            drNoReg.Read()

            If drNoReg.HasRows Then
                drNoReg.Read()
                noPermintaanAne = "AOP" + Format(Now, "yyMMddHHmmss") + "-" + (Val(Trim(drNoReg.Item(0).ToString)) + 1).ToString
                txtNoRegAne.Text = noPermintaanAne
            Else
                noPermintaanAne = "AOP" + Format(Now, "yyMMddHHmmss") + "-1"
                txtNoRegAne.Text = noPermintaanAne
            End If
            conn.Close()
        Catch ex As Exception

        End Try

    End Sub

    Sub autoNoTindakanOP()
        Dim noTindakanOP As String

        Try
            Call koneksiServer()
            Dim query As String
            Dim cmdNoTin As MySqlCommand
            Dim drNoTin As MySqlDataReader
            query = "SELECT SUBSTR(noTindakanOP,17,4) FROM t_tindakanop WHERE DATE(SUBSTR(tglUser,2,10)) = DATE(NOW()) ORDER BY CAST(SUBSTR(noTindakanOP,17,4) AS UNSIGNED) DESC LIMIT 1"
            cmdNoTin = New MySqlCommand(query, conn)
            drNoTin = cmdNoTin.ExecuteReader
            drNoTin.Read()
            If drNoTin.HasRows Then
                drNoTin.Read()
                noTindakanOP = "TOP" + Format(Now, "yyMMddHHmmss") + "-" + (Val(Trim(drNoTin.Item(0).ToString)) + 1).ToString
                txtNoTindakan.Text = noTindakanOP
            Else
                noTindakanOP = "TOP" + Format(Now, "yyMMddHHmmss") + "-1"
                txtNoTindakan.Text = noTindakanOP
            End If
            conn.Close()
        Catch ex As Exception

        End Try

    End Sub

    Sub addRegistrasiOP()
        Dim dtReg As String
        dtReg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

        Call koneksiServer()
        Try
            Dim str As String
            Dim cmdReg As MySqlCommand
            str = "INSERT INTO t_registrasiop(noRegistrasiOP,tglRegistrasiOP,noRM,
                                              noRegistrasiPasien,noDaftarPasien,instalasi,
                                              dokterPengirim,jenisOperasi,tglPermintaanOP,
                                              keterangan,statusOP,statusBaca,
                                              idUser,tglUser) 
                   VALUES ('" & txtNoRegOp.Text & "','" & dtReg & "','" & txtNoRM.Text & "',
                           '" & Form1.txtRegRanap.Text & "','" & Form1.txtNoDaftar.Text & "','RAWAT INAP',
                           '" & txtDokter.Text & "','" & txtkdJenisOp.Text & "','" & Format(datePermintaan.Value, "yyyy-MM-dd HH:mm") & "',
                           '" & txtKet.Text & "','0','0',
                           ';" & LoginForm.txtUsername.Text.ToLower & "',';" & dtReg & "')"
            cmdReg = New MySqlCommand(str, conn)
            cmdReg.ExecuteNonQuery()
            MsgBox("Permintaan Operasi berhasil dilakukan", MsgBoxStyle.Information, "Information")
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
            str = "INSERT INTO t_tindakanop(noTindakanOP,noRegistrasiOP,totalTarifTindakan,statusPembayaran,
                                            dokterOP,idUser,tglUser) 
                   VALUES ('" & txtNoTindakan.Text & "','" & txtNoRegOp.Text & "','" & totTarif & "','BELUM LUNAS',
                           ';" & txtDokter.Text & "',';" & LoginForm.txtUsername.Text.ToLower & "',';" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "')"
            cmdTindak = New MySqlCommand(str, conn)
            cmdTindak.ExecuteNonQuery()
            'MsgBox("Insert Tindakan Lab berhasil dilakukan", MsgBoxStyle.Information, "Information")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Tindakan")
        End Try

        conn.Close()
    End Sub

    Sub addAnestesi()
        Call koneksiServer()
        Try
            Dim str As String
            Dim cmdTindak As MySqlCommand
            str = "INSERT INTO t_tindakananestesi (noTindakanAnestesi,noRegistrasiOP,
                                                   idUser,tglUser) 
                   VALUES ('" & txtNoRegAne.Text & "','" & txtNoRegOp.Text & "',
                           ';" & LoginForm.txtUsername.Text.ToLower & "',';" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "')"
            cmdTindak = New MySqlCommand(str, conn)
            cmdTindak.ExecuteNonQuery()
            'MsgBox("Insert Tindakan Lab berhasil dilakukan", MsgBoxStyle.Information, "Information")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Anestesi")
        End Try

        conn.Close()
    End Sub

    Sub addDiagnosPra()
        Call koneksiServer()
        Try
            Dim str As String
            Dim cmdTindak As MySqlCommand
            str = "INSERT INTO t_diagnosapasienop (noRegistrasiOP,diagnosaPra,tglDiagniosaPra,
                                                   dokterDiagnosaPra,sarsCovid,idUser,tglUser)
                   VALUES ('" & txtNoRegOp.Text & "',';" & diagnos & "','" & Format(dateDiagnosPra.Value, "yyyy-MM-dd HH:mm") & "',
                           '" & txtDokterPra.Text & "','" & stats & "',';" & LoginForm.txtUsername.Text.ToLower & "',';" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "')"
            cmdTindak = New MySqlCommand(str, conn)
            cmdTindak.ExecuteNonQuery()
            'MsgBox("Insert Tindakan Lab berhasil dilakukan", MsgBoxStyle.Information, "Information")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Diagnosa")
        End Try

        conn.Close()
    End Sub

    Sub updateBatal()
        Call koneksiServer()
        Try
            Dim str As String
            Dim cmd As MySqlCommand
            str = "UPDATE t_registrasiop SET statusOP = '6', 
                                             idUser = CONCAT(idUser,';" & LoginForm.txtUsername.Text.ToUpper & "'),
                                             tglUser = CONCAT(tglUser,';" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "') 
                                       WHERE noRegistrasiOP = '" & noRegLama & "'"
            'MsgBox(str)
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Permintaan berhasil dibatalkan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message & "Pembatalan Permintaan")
        End Try
        conn.Close()
    End Sub

    Sub updateBatalDetail()
        Call koneksiServer()
        Try
            Dim str As String
            Dim cmd As MySqlCommand
            str = "UPDATE t_detailtindakanop SET statusHapus = '1', 
                                                 idUser = CONCAT(idUser,';" & LoginForm.txtUsername.Text.ToUpper & "'),
                                                 tglUser = CONCAT(tglUser,';" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "') 
                                           WHERE noTindakanOP = '" & notind & "'"
            'MsgBox(str)
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            'MessageBox.Show("Permintaan berhasil dibatalkan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message & "Pembatalan Detail Permintaan")
        End Try
        conn.Close()
    End Sub

    Function tampilProses(noReg As String) As (noRegBf As String, stats As String, noTind As String)
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim value1 As String = ""
        Dim value2 As String = ""
        Dim value3 As String = ""

        query = "SELECT reg.noRegistrasiOP,reg.tglRegistrasiOP,
                        reg.statusOP,tnd.noTindakanOP
                   FROM t_registrasiop AS reg
             INNER JOIN t_tindakanop AS tnd ON reg.noRegistrasiOP = tnd.noRegistrasiOP
                  WHERE reg.noRegistrasiPasien = '" & noReg & "'
                  ORDER BY reg.tglRegistrasiOP DESC LIMIT 1"

        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            value1 = dr.Item("noRegistrasiOP").ToString
            value2 = dr.Item("statusOP").ToString
            value3 = dr.Item("noTindakanOP").ToString
        End If
        conn.Close()

        Return (value1, value2, value3)
    End Function

    Private Sub Operasi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TableLayoutPanel1.RowStyles(3).SizeType = SizeType.Percent
        TableLayoutPanel1.RowStyles(3).Height = 0

        datePermintaan.Format = DateTimePickerFormat.Custom
        datePermintaan.CustomFormat = "dddd, ddMMMMyyyy HH:mm"
        dateDiagnosPra.Format = DateTimePickerFormat.Custom
        dateDiagnosPra.CustomFormat = "dddd, ddMMMMyyyy HH:mm"
        Timer1.Start()

        txtNoRM.Text = Form1.txtRekMed.Text
        txtPasien.Text = Form1.txtNamaPasien.Text
        txtUmur.Text = Form1.txtUmur.Text
        txtJk.Text = Form1.txtJk.Text
        txtRuang.Text = Form1.txtUnitRanap.Text
        txtDokter.Text = Form1.txtDokter.Text
        txtKelas.Text = Form1.txtKelas.Text

        Call autoDokter()
        Call autoDokterPra()
        Call autoJenisOP()
        Call autoNoRegOP()
        Call autoNoRegAnestesi()
        Call autoNoTindakanOP()

        noRegLama = tampilProses(Form1.txtRegRanap.Text).noRegBf
        tmline = tampilProses(Form1.txtRegRanap.Text).stats
        notind = tampilProses(Form1.txtRegRanap.Text).noTind

        If tmline = "" Then
            TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Absolute
            TableLayoutPanel1.RowStyles(1).Height = 0
            Me.Height = 500
            txtOrder.ForeColor = Color.DimGray
            txtJadwal.ForeColor = Color.DimGray
            txtAnestesi.ForeColor = Color.DimGray
            txtOperasi.ForeColor = Color.DimGray
            txtRecover.ForeColor = Color.DimGray
            txtSelesai.ForeColor = Color.DimGray
            btnBatal.Enabled = False
        ElseIf tmline = 0 Then
            TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Absolute
            TableLayoutPanel1.RowStyles(1).Height = 90
            Me.Height = 600
            txtOrder.ForeColor = Color.FromArgb(26, 141, 95)
            PictureBox1.Image = My.Resources.opOrder
            btnBatal.Enabled = True
        ElseIf tmline = 1 Then
            TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Absolute
            TableLayoutPanel1.RowStyles(1).Height = 90
            Me.Height = 600
            txtOrder.ForeColor = Color.FromArgb(26, 141, 95)
            txtJadwal.ForeColor = Color.FromArgb(26, 141, 95)
            PictureBox1.Image = My.Resources.opRegister
            btnBatal.Enabled = False
        ElseIf tmline = 2 Then
            TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Absolute
            TableLayoutPanel1.RowStyles(1).Height = 90
            Me.Height = 600
            txtOrder.ForeColor = Color.FromArgb(26, 141, 95)
            txtJadwal.ForeColor = Color.FromArgb(26, 141, 95)
            txtAnestesi.ForeColor = Color.FromArgb(26, 141, 95)
            PictureBox1.Image = My.Resources.opAnestesi
            btnBatal.Enabled = False
        ElseIf tmline = 3 Then
            TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Absolute
            TableLayoutPanel1.RowStyles(1).Height = 90
            Me.Height = 600
            txtOrder.ForeColor = Color.FromArgb(26, 141, 95)
            txtJadwal.ForeColor = Color.FromArgb(26, 141, 95)
            txtAnestesi.ForeColor = Color.FromArgb(26, 141, 95)
            txtOperasi.ForeColor = Color.FromArgb(26, 141, 95)
            PictureBox1.Image = My.Resources.opOperasi
            btnBatal.Enabled = False
        ElseIf tmline = 4 Then
            TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Absolute
            TableLayoutPanel1.RowStyles(1).Height = 90
            Me.Height = 600
            txtOrder.ForeColor = Color.FromArgb(26, 141, 95)
            txtJadwal.ForeColor = Color.FromArgb(26, 141, 95)
            txtAnestesi.ForeColor = Color.FromArgb(26, 141, 95)
            txtOperasi.ForeColor = Color.FromArgb(26, 141, 95)
            txtRecover.ForeColor = Color.FromArgb(26, 141, 95)
            PictureBox1.Image = My.Resources.opRecovery
            btnBatal.Enabled = False
        ElseIf tmline = 5 Then
            TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Absolute
            TableLayoutPanel1.RowStyles(1).Height = 90
            Me.Height = 600
            txtOrder.ForeColor = Color.FromArgb(26, 141, 95)
            txtJadwal.ForeColor = Color.FromArgb(26, 141, 95)
            txtAnestesi.ForeColor = Color.FromArgb(26, 141, 95)
            txtOperasi.ForeColor = Color.FromArgb(26, 141, 95)
            txtRecover.ForeColor = Color.FromArgb(26, 141, 95)
            txtSelesai.ForeColor = Color.FromArgb(26, 141, 95)
            PictureBox1.Image = My.Resources.opSelesai
            btnBatal.Enabled = False
        ElseIf tmline = 6 Then
            TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Absolute
            TableLayoutPanel1.RowStyles(1).Height = 0
            Me.Height = 600
            txtOrder.ForeColor = Color.DimGray
            txtJadwal.ForeColor = Color.DimGray
            txtAnestesi.ForeColor = Color.DimGray
            txtOperasi.ForeColor = Color.DimGray
            txtRecover.ForeColor = Color.DimGray
            txtSelesai.ForeColor = Color.DimGray
            btnBatal.Enabled = False
        End If
    End Sub

    Private Sub txtJenisOP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtJenisOP.SelectedIndexChanged
        Call koneksiServer()

        Try
            Dim query As String
            query = "SELECT kdJenisOperasi FROM t_jenisop WHERE jenisOperasi = '" & txtJenisOP.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                txtkdJenisOp.Text = dr.GetString("kdJenisOperasi")
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If txtJenisOP.Text = "" Then
            Me.ErrorJenis.SetError(Me.txtJenisOP, "Mohon dipilih terlebih dahulu")
            MsgBox("Pilih Jenis Operasi terlebih dahulu", MsgBoxStyle.Exclamation, "Warning")
        ElseIf txtIcdPrimer.Text = "" Then
            Me.ErDiag.SetError(Me.txtIcdPrimer, "Mohon dipilih terlebih dahulu")
            MsgBox("Pilih Dokter terlebih dahulu", MsgBoxStyle.Exclamation, "Warning")
        ElseIf txtDokter.Text = "" Then
            Me.ErrorDokter.SetError(Me.txtDokter, "Mohon dipilih terlebih dahulu")
            MsgBox("Pilih Dokter terlebih dahulu", MsgBoxStyle.Exclamation, "Warning")
        ElseIf txtDokterPra.Text = "" Then
            Me.ErDokPra.SetError(Me.txtDokterPra, "Mohon dipilih terlebih dahulu")
            MsgBox("Pilih Dokter terlebih dahulu", MsgBoxStyle.Exclamation, "Warning")
        ElseIf txtKet.Text = "" Then
            Me.ErrorKet.SetError(Me.txtKet, "Mohon diisi terlebih dahulu")
            MsgBox("Tuliskan keterangan terlebih dahulu", MsgBoxStyle.Exclamation, "Warning")
        Else
            ErrorJenis.Clear()
            ErrorDokter.Clear()
            ErDokPra.Clear()
            ErDiag.Clear()
            ErrorKet.Clear()
            Dim konfirmasi As MsgBoxResult

            If tmline = "" Then
                konfirmasi = MsgBox("Apakah anda yakin permintaan sudah benar ?", vbQuestion + vbYesNo, "Konfirmasi")
                If konfirmasi = vbYes Then
                    Call addRegistrasiOP()
                    Call addAnestesi()
                    Call addDiagnosPra()
                    Call addTindakan()

                    txtIcdPrimer.Text = ""
                    txtSek1.Text = ""
                    txtSek2.Text = ""
                    txtJenisOP.Text = ""
                    txtDokter.Text = ""
                    txtDokterPra.Text = ""
                    txtKet.Text = ""
                End If
            ElseIf tmline = 0 Then
                konfirmasi = MsgBox("Permintaan sudah terkirim," & vbCrLf &
                                    "Apakah anda ingin membatalkan permintaan sebelumnya dan menginputkan permintaan baru ?", vbQuestion + vbYesNo, "Konfirmasi")
                If konfirmasi = vbYes Then
                    Call updateBatal()
                    Call updateBatalDetail()

                    Call addRegistrasiOP()
                    Call addAnestesi()
                    Call addDiagnosPra()
                    Call addTindakan()

                    txtIcdPrimer.Text = ""
                    txtSek1.Text = ""
                    txtSek2.Text = ""
                    txtJenisOP.Text = ""
                    txtDokter.Text = ""
                    txtDokterPra.Text = ""
                    txtKet.Text = ""
                End If
            ElseIf tmline = 1 Then
                MsgBox("Pasien telah mendapat konfirmasi jadwal operasi", MsgBoxStyle.Information)
            ElseIf tmline = 2 Then
                MsgBox("Pasien sedang dalam proses tindakan", MsgBoxStyle.Information)
            ElseIf tmline = 3 Then
                MsgBox("Pasien sedang dalam proses tindakan", MsgBoxStyle.Information)
            ElseIf tmline = 4 Then
                MsgBox("Pasien sedang dalam proses pemulihan post operasi", MsgBoxStyle.Information)
            ElseIf tmline = 5 Then
                konfirmasi = MsgBox("Apakah anda ingin menginputkan permintaan lagi ?", vbQuestion + vbYesNo, "Konfirmasi")
                If konfirmasi = vbYes Then
                    Call addRegistrasiOP()
                    Call addAnestesi()
                    Call addDiagnosPra()
                    Call addTindakan()

                    txtIcdPrimer.Text = ""
                    txtSek1.Text = ""
                    txtSek2.Text = ""
                    txtJenisOP.Text = ""
                    txtDokter.Text = ""
                    txtDokterPra.Text = ""
                    txtKet.Text = ""
                End If
            ElseIf tmline = 6 Then
                Call addRegistrasiOP()
                Call addAnestesi()
                Call addDiagnosPra()
                Call addTindakan()

                txtIcdPrimer.Text = ""
                txtSek1.Text = ""
                txtSek2.Text = ""
                txtJenisOP.Text = ""
                txtDokter.Text = ""
                txtDokterPra.Text = ""
                txtKet.Text = ""
            End If
        End If

        Call autoNoRegOP()
        Call autoNoRegAnestesi()
        Call autoNoTindakanOP()
    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Call updateBatal()
        Call updateBatalDetail()
        TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Absolute
        TableLayoutPanel1.RowStyles(1).Height = 0
        Me.Height = 500
        txtOrder.ForeColor = Color.DimGray
        txtJadwal.ForeColor = Color.DimGray
        txtAnestesi.ForeColor = Color.DimGray
        txtOperasi.ForeColor = Color.DimGray
        txtRecover.ForeColor = Color.DimGray
        txtSelesai.ForeColor = Color.DimGray

        Call autoNoRegOP()
        Call autoNoRegAnestesi()
        Call autoNoTindakanOP()
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

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            stats = RadioButton3.Text
        ElseIf RadioButton3.Checked = False Then
            Return
        End If
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        If RadioButton4.Checked = True Then
            stats = RadioButton4.Text
        ElseIf RadioButton4.Checked = False Then
            Return
        End If
    End Sub

    Private Sub txtIcdPrimer_LostFocus(sender As Object, e As EventArgs) Handles txtIcdPrimer.LostFocus
        If txtIcdPrimer.Text <> "" And txtSek1.Text = "" And txtSek2.Text = "" Then
            diagnos = String.Join(";", txtIcdPrimer.Text)
        ElseIf txtIcdPrimer.Text <> "" And txtSek1.Text <> "" And txtSek2.Text = "" Then
            diagnos = String.Join(";", txtIcdPrimer.Text, txtSek1.Text)
        ElseIf txtIcdPrimer.Text <> "" And txtSek1.Text <> "" And txtSek2.Text <> "" Then
            diagnos = String.Join(";", txtIcdPrimer.Text, txtSek1.Text, txtSek2.Text)
        ElseIf txtIcdPrimer.Text = "" And txtSek1.Text = "" And txtSek2.Text = "" Then
            Return
        End If
    End Sub

    Private Sub txtSek1_LostFocus(sender As Object, e As EventArgs) Handles txtSek1.LostFocus
        If txtIcdPrimer.Text <> "" And txtSek1.Text = "" And txtSek2.Text = "" Then
            diagnos = String.Join(";", txtIcdPrimer.Text)
        ElseIf txtIcdPrimer.Text <> "" And txtSek1.Text <> "" And txtSek2.Text = "" Then
            diagnos = String.Join(";", txtIcdPrimer.Text, txtSek1.Text)
        ElseIf txtIcdPrimer.Text <> "" And txtSek1.Text <> "" And txtSek2.Text <> "" Then
            diagnos = String.Join(";", txtIcdPrimer.Text, txtSek1.Text, txtSek2.Text)
        ElseIf txtIcdPrimer.Text = "" And txtSek1.Text = "" And txtSek2.Text = "" Then
            Return
        End If
    End Sub

    Private Sub txtSek2_LostFocus(sender As Object, e As EventArgs) Handles txtSek2.LostFocus
        If txtIcdPrimer.Text <> "" And txtSek1.Text = "" And txtSek2.Text = "" Then
            diagnos = String.Join(";", txtIcdPrimer.Text)
        ElseIf txtIcdPrimer.Text <> "" And txtSek1.Text <> "" And txtSek2.Text = "" Then
            diagnos = String.Join(";", txtIcdPrimer.Text, txtSek1.Text)
        ElseIf txtIcdPrimer.Text <> "" And txtSek1.Text <> "" And txtSek2.Text <> "" Then
            diagnos = String.Join(";", txtIcdPrimer.Text, txtSek1.Text, txtSek2.Text)
        ElseIf txtIcdPrimer.Text = "" And txtSek1.Text = "" And txtSek2.Text = "" Then
            Return
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        tmline = tampilProses(Form1.txtNoDaftar.Text).stats
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If TableLayoutPanel1.RowStyles(3).Height = 0 Then
            If TableLayoutPanel1.RowStyles(1).Height = 90 Then
                Me.Height = 870
                TableLayoutPanel1.RowStyles(3).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(3).Height = 60
            ElseIf TableLayoutPanel1.RowStyles(1).Height = 0 Then
                Me.Height = 800
                TableLayoutPanel1.RowStyles(3).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(3).Height = 60
            End If
            Call riwayatOpe()
        Else
            If TableLayoutPanel1.RowStyles(1).Height = 90 Then
                Me.Height = 600
                TableLayoutPanel1.RowStyles(3).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(3).Height = 0
            ElseIf TableLayoutPanel1.RowStyles(1).Height = 0 Then
                Me.Height = 500
                TableLayoutPanel1.RowStyles(3).SizeType = SizeType.Percent
                TableLayoutPanel1.RowStyles(3).Height = 0
            End If

        End If
    End Sub

    Private Sub btnSimpan_MouseLeave(sender As Object, e As EventArgs) Handles btnSimpan.MouseLeave
        Me.btnSimpan.BackColor = Color.SeaGreen
    End Sub

    Private Sub btnSimpan_MouseEnter(sender As Object, e As EventArgs) Handles btnSimpan.MouseEnter
        Me.btnSimpan.BackColor = Color.MediumSeaGreen
    End Sub

    Private Sub Operasi_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Timer1.Stop()

        txtNoRM.Text = ""
        txtPasien.Text = ""
        txtUmur.Text = ""
        txtJk.Text = ""
        txtRuang.Text = ""
        txtDokter.Text = ""
        txtKelas.Text = ""
        txtKet.Text = ""
        txtIcdPrimer.Text = ""
        txtSek1.Text = ""
        txtSek2.Text = ""
        dateDiagnosPra.Value = DateTime.Now
        datePermintaan.Value = DateTime.Now
        txtJenisOP.Text = ""
        txtDokterPra.Text = ""
    End Sub

    Private Sub dgv_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgv.CellFormatting

        For i As Integer = 0 To dgv.Rows.Count - 1
            If i Mod 2 = 0 Then
                dgv.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                dgv.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next

        For i As Integer = 0 To dgv.RowCount - 1
            If dgv.Rows(i).Cells(7).Value.ToString = "0" Then
                dgv.Rows(i).Cells(7).Value = "PERMINTAAN"
                dgv.Rows(i).Cells(7).Style.BackColor = Color.FromArgb(255, 155, 0)
                dgv.Rows(i).Cells(7).Style.ForeColor = Color.White
            ElseIf dgv.Rows(i).Cells(7).Value.ToString = "1" Then
                dgv.Rows(i).Cells(7).Value = "PENJADWALAN"
                dgv.Rows(i).Cells(7).Style.BackColor = Color.FromArgb(0, 60, 155)
                dgv.Rows(i).Cells(7).Style.ForeColor = Color.White
            ElseIf dgv.Rows(i).Cells(7).Value.ToString = "2" Then
                dgv.Rows(i).Cells(7).Value = "ANESTESI"
                dgv.Rows(i).Cells(7).Style.BackColor = Color.FromArgb(0, 60, 155)
                dgv.Rows(i).Cells(7).Style.ForeColor = Color.White
            ElseIf dgv.Rows(i).Cells(7).Value.ToString = "3" Then
                dgv.Rows(i).Cells(7).Value = "OPERASI"
                dgv.Rows(i).Cells(7).Style.BackColor = Color.FromArgb(0, 60, 155)
                dgv.Rows(i).Cells(7).Style.ForeColor = Color.White
            ElseIf dgv.Rows(i).Cells(7).Value.ToString = "4" Then
                dgv.Rows(i).Cells(7).Value = "RECOVERY"
                dgv.Rows(i).Cells(7).Style.BackColor = Color.FromArgb(0, 60, 155)
                dgv.Rows(i).Cells(7).Style.ForeColor = Color.White
            ElseIf dgv.Rows(i).Cells(7).Value.ToString = "5" Then
                dgv.Rows(i).Cells(7).Value = "SELESAI"
                dgv.Rows(i).Cells(7).Style.BackColor = Color.FromArgb(15, 180, 100)
                dgv.Rows(i).Cells(7).Style.ForeColor = Color.White
            ElseIf dgv.Rows(i).Cells(7).Value.ToString = "6" Then
                dgv.Rows(i).Cells(7).Value = "BATAL"
                dgv.Rows(i).Cells(7).Style.BackColor = Color.FromArgb(255, 115, 115)
                dgv.Rows(i).Cells(7).Style.ForeColor = Color.White
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