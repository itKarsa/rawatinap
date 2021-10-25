Option Explicit On
Imports System.IO
Imports System.Net.Sockets
Imports MySql.Data.MySqlClient
Imports System.Deployment.Application
Imports System.Net
Imports System.Net.Dns
Imports Tulpep.NotificationWindow
Public Class Form1

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Dim Client As TcpClient
    Dim idx As String

    Dim id, noTindakan, tindakan, tglTindakan, tarif, stotal, totalTindakan, kdPPA, spek As String
    'Dim Listener As New TcpListener(8000)

    Function cekTotalTindakan(ByVal id As String) As String
        Dim total As String = ""
        Call koneksiServer()
        Try
            Dim query As String
            query = "SELECT totalTarifTindakan FROM t_tindakanpasienranap WHERE noTindakanPasienRanap = '" & id & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                total = UCase(dr.GetString("totalTarifTindakan"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()

        Return total
    End Function

    Sub dgv1_styleRow()
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Sub tampilDataDiagnosa()
        Call koneksiServer()
        da = New MySqlDataAdapter("SELECT kdIcd10, icd10, 
                                          UPPER(jenisDiagnosa), UPPER(kasus), 
                                          namapetugasMedis, tglDiagnosa 
                                     FROM vw_diagnosapasienranap WHERE noRekamedis = '" & txtRekMed.Text & "'", conn)
        ds = New DataSet
        da.Fill(ds, "vw_diagnosapasienranap")
        DataGridView1.DataSource = ds.Tables("vw_diagnosapasienranap")
    End Sub

    Sub tampilDataTindakan()
        Call koneksiServer()
        Dim query As String
        'Dim cmd As MySqlCommand
        'Dim dr As MySqlDataReader
        query = "SELECT kdTarif, tindakan, tarif, 
                        jumlahTindakan, DPJP, PPA, 
                        tglTindakan, totalTarif, noTindakanPasienRanap, 
                        idTindakanPasienRanap 
                   FROM vw_tindakanmedisranap WHERE noDaftarRawatInap = '" & txtRegRanap.Text & "'"
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            DataGridView2.Rows.Clear()

            Do While dr.Read
                DataGridView2.Rows.Add(dr.Item("kdTarif"), dr.Item("tindakan"), dr.Item("tarif"),
                                       dr.Item("jumlahTindakan"), dr.Item("DPJP"), dr.Item("PPA"),
                                       dr.Item("tglTindakan"), dr.Item("totalTarif"), dr.Item("noTindakanPasienRanap"),
                                       dr.Item("idTindakanPasienRanap"))
            Loop
            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub autoDokter()
        Call koneksiServer()

        Using cmd As New MySqlCommand("SELECT DISTINCT namapetugasMedis FROM t_tenagamedis2 WHERE kdKelompokTenagaMedis in('ktm1') ORDER BY namapetugasMedis ASC", conn)
            da = New MySqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)

            comboDokter.DataSource = dt
            comboDokter.DisplayMember = "namapetugasMedis"
            comboDokter.ValueMember = "namapetugasMedis"
            comboDokter.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            comboDokter.AutoCompleteSource = AutoCompleteSource.ListItems
        End Using
        conn.Close()
    End Sub

    Sub tampilVenti()
        Call koneksiServer()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        query = "SELECT COALESCE(spesifikasiKamar, '-') AS spek, 
                        COALESCE(alat, '-') AS alat
                   FROM t_detailpenggunaanventilator
                  WHERE noDaftarRawatInap = '" & txtRegRanap.Text & "'
               ORDER BY tglPakai DESC LIMIT 1"
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()

            If dr.HasRows Then
                txtSpek.Text = dr.Item("spek").ToString
                txtAlat.Text = dr.Item("alat").ToString
            Else
                txtSpek.Text = "-"
                txtAlat.Text = "-"
            End If

            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub cekRIK()
        Call koneksiServer()

        Dim query As String
        Dim cmd As MySqlCommand
        query = "CALL cekRIK( '" & txtUnitRanap.Text & "')"
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            Do While dr.Read
                spek = dr.Item("Ruang").ToString
            Loop
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()
    End Sub

    Sub aturDGVDiagnosa()
        Try
            DataGridView1.Columns(0).Width = 100
            DataGridView1.Columns(1).Width = 200
            DataGridView1.Columns(2).Width = 150
            DataGridView1.Columns(3).Width = 100
            DataGridView1.Columns(4).Width = 250
            DataGridView1.Columns(5).Width = 150
            DataGridView1.Columns(0).HeaderText = "KODE"
            DataGridView1.Columns(1).HeaderText = "ICD10"
            DataGridView1.Columns(2).HeaderText = "Jenis Diagnosa"
            DataGridView1.Columns(3).HeaderText = "Kasus"
            DataGridView1.Columns(4).HeaderText = "Dokter Pemeriksa"
            DataGridView1.Columns(5).HeaderText = "Tgl.Diagnosa"

            DataGridView1.Columns(5).DefaultCellStyle.Format = "dd/MM/yyyy"
            DataGridView1.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise
            DataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black
            DataGridView1.DefaultCellStyle.ForeColor = Color.Black
            DataGridView1.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True

            dgv1_styleRow()
        Catch ex As Exception
            MessageBox.Show(ex.Message & " DGV ICD10")
        End Try
    End Sub

    Sub subTotal()
        Dim tarif, jumlah, subTotal As String

        subTotal = 0
        For i As Integer = 0 To DataGridView2.Rows.Count - 1
            tarif = DataGridView2.Rows(i).Cells(2).Value.ToString
            jumlah = DataGridView2.Rows(i).Cells(3).Value.ToString
            subTotal = Val(tarif) * Val(jumlah)
            DataGridView2.Rows(i).Cells(7).Value = subTotal
        Next
    End Sub

    Public Function hitungUmur(ByVal tanggal As Date) As String
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

        Return y & "Th, " & m & "Bln, " & d & "hr"
    End Function

    Private Sub btnPindah_Click(sender As Object, e As EventArgs) Handles btnPindah.Click
        Pindah_Kamar.Ambil_Data = True
        Pindah_Kamar.Form_Ambil_Data = "Pindah"
        Pindah_Kamar.ShowDialog()
    End Sub

    Private Sub btnCekout_Click(sender As Object, e As EventArgs) Handles btnCekout.Click
        Dim fco As Checkout = New Checkout

        fco.Ambil_Data = True
        fco.Form_Ambil_Data = "Cekout"
        fco.ShowDialog()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'Listener.Stop()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Timer1.Start()
        'Listener.Start()
        Call autoDokter()

        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.Manual
        With Screen.PrimaryScreen.WorkingArea
            Me.SetBounds(.Left, .Top, .Width, .Height)
        End With

        If ApplicationDeployment.IsNetworkDeployed Then
            Dim ver As ApplicationDeployment = ApplicationDeployment.CurrentDeployment
            Label32.Text = "Version " & ver.CurrentVersion.ToString()
        End If

        Dim pcname As String
        Dim ipadd As String = ""
        pcname = System.Net.Dns.GetHostName

        Dim objAddressList() As System.Net.IPAddress =
            System.Net.Dns.GetHostEntry("").AddressList
        For i = 0 To objAddressList.GetUpperBound(0)
            If objAddressList(i).AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then
                ipadd = objAddressList(i).ToString
                txtIpAddress.Text = objAddressList(i).ToString
                Exit For
            End If
        Next

        txtHostname.Text = "PC Name : " & pcname & " | IP Address : " & ipadd & " | User : " & txtUser.Text

        ErrorProvider1.Clear()
        btnCariReg.Select()
        comboDokter.Enabled = False
        btnPindah.Enabled = False
        btnCekout.Enabled = False
        btnTindakan.Enabled = False
        btnDiagnosa.Enabled = False
        btnResep.Enabled = False
        btnLab.Enabled = False
        btnRadiologi.Enabled = False
        btnhemo.Enabled = False
        btnOperasi.Enabled = False
        btnPasienCovid.Enabled = False
        btnVenti.Enabled = False

        txtTotal.Text = FormatNumber(txtTotal.Text, 0)

        If Label1.Text.Contains("Gizi") Then
            btnGizi.BackColor = Color.FromArgb(26, 141, 95)
            btnGizi.ForeColor = Color.FromArgb(232, 243, 239)
        End If

        Me.KeyPreview = True
    End Sub

    Private Sub btnTindakan_Click(sender As Object, e As EventArgs) Handles btnTindakan.Click
        FormTindakan.Ambil_Data = True
        FormTindakan.Form_Ambil_Data = "Tindakan"
        FormTindakan.Show()
        Me.Hide()
    End Sub

    Private Sub btnCariReg_Click(sender As Object, e As EventArgs) Handles btnCariReg.Click
        Daftar_Pasien.Ambil_Data = True
        Daftar_Pasien.Form_Ambil_Data = "Daftar Pasien"
        Daftar_Pasien.Show()
        Me.Hide()

        txtKelas.Text = ""
        txtUnitRanap.Text = ""
        txtKdUnitRanap.Text = ""
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles btnDiagnosa.Click
        Isi_Diagnosa.Ambil_Data = True
        Isi_Diagnosa.Form_Ambil_Data = "Diagnosa"
        Isi_Diagnosa.ShowDialog()
    End Sub

    Private Sub dateLahir_ValueChanged(sender As Object, e As EventArgs) Handles dateLahir.ValueChanged
        Dim lahir As Date = dateLahir.Value
        txtUmur.Text = hitungUmur(lahir)
    End Sub

    Private Sub txtRekMed_TextChanged(sender As Object, e As EventArgs) Handles txtRekMed.TextChanged
        Call tampilDataDiagnosa()
        Call aturDGVDiagnosa()
    End Sub

    Private Sub txtReg_TextChanged(sender As Object, e As EventArgs) Handles txtRegRanap.TextChanged
        Call tampilDataTindakan()
        'Call aturDGVTindakan()
        Call subTotal()
        btnPindah.Enabled = True
        btnCekout.Enabled = True
        btnBatalOut.Enabled = True
        btnTindakan.Enabled = True
        btnDiagnosa.Enabled = True
        btnResep.Enabled = False
        btnLab.Enabled = True
        btnRadiologi.Enabled = True
        btnLayanan.Enabled = True
        btnHemo.Enabled = True
        comboDokter.Enabled = True
        btnInfoBiaya.Enabled = True
        btnOperasi.Enabled = True
        btnOKParu.Enabled = True
        btnPasienCovid.Enabled = True
        btnPatologi.Enabled = True
    End Sub

    Private Sub btnLab_Click(sender As Object, e As EventArgs) Handles btnLab.Click
        Laboratorium.Ambil_Data = True
        Laboratorium.Form_Ambil_Data = "Laboratorium"
        Laboratorium.Show()
        Me.Hide()
    End Sub

    Private Sub btnRadiologi_Click(sender As Object, e As EventArgs) Handles btnRadiologi.Click
        Radiologi.Ambil_Data = True
        Radiologi.Form_Ambil_Data = "Radiologi"
        Radiologi.Show()
        Me.Hide()
    End Sub

    Private Sub btnResep_Click(sender As Object, e As EventArgs) Handles btnResep.Click
        Resep_Dokter.Ambil_Data = True
        Resep_Dokter.Form_Ambil_Data = "Resep"
        Resep_Dokter.Show()
        Me.Hide()
    End Sub

    Private Sub btnPindah_MouseLeave(sender As Object, e As EventArgs) Handles btnPindah.MouseLeave
        Me.btnPindah.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnPindah.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnPindah_MouseEnter(sender As Object, e As EventArgs) Handles btnPindah.MouseEnter
        Me.btnPindah.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnPindah.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnCekout_MouseLeave(sender As Object, e As EventArgs) Handles btnCekout.MouseLeave
        Me.btnCekout.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnCekout.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnCekout_MouseEnter(sender As Object, e As EventArgs) Handles btnCekout.MouseEnter
        Me.btnCekout.BackColor = Color.Crimson
        Me.btnCekout.ForeColor = Color.White
    End Sub

    Private Sub btnBatalOut_MouseLeave(sender As Object, e As EventArgs) Handles btnBatalOut.MouseLeave
        Me.btnBatalOut.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnBatalOut.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnBatalOut_MouseEnter(sender As Object, e As EventArgs) Handles btnBatalOut.MouseEnter
        Me.btnBatalOut.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnBatalOut.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnLab_MouseLeave(sender As Object, e As EventArgs) Handles btnLab.MouseLeave
        Me.btnLab.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnLab.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnLab_MouseEnter(sender As Object, e As EventArgs) Handles btnLab.MouseEnter
        Me.btnLab.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnLab.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnRadiologi_MouseLeave(sender As Object, e As EventArgs) Handles btnRadiologi.MouseLeave
        Me.btnRadiologi.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnRadiologi.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnRadiologi_MouseEnter(sender As Object, e As EventArgs) Handles btnRadiologi.MouseEnter
        Me.btnRadiologi.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnRadiologi.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnTindakan_MouseLeave(sender As Object, e As EventArgs) Handles btnTindakan.MouseLeave
        Me.btnTindakan.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnTindakan.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnTindakan_MouseEnter(sender As Object, e As EventArgs) Handles btnTindakan.MouseEnter
        Me.btnTindakan.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnTindakan.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnDiagnosa_MouseLeave(sender As Object, e As EventArgs) Handles btnDiagnosa.MouseLeave
        Me.btnDiagnosa.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnDiagnosa.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnDiagnosa_MouseEnter(sender As Object, e As EventArgs) Handles btnDiagnosa.MouseEnter
        Me.btnDiagnosa.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnDiagnosa.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnGizi_MouseLeave(sender As Object, e As EventArgs) Handles btnGizi.MouseLeave
        Me.btnGizi.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnGizi.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnGizi_MouseEnter(sender As Object, e As EventArgs) Handles btnGizi.MouseEnter
        Me.btnGizi.BackColor = Color.MediumSeaGreen
        Me.btnGizi.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnResep_MouseLeave(sender As Object, e As EventArgs) Handles btnResep.MouseLeave
        Me.btnResep.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnResep.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnResep_MouseEnter(sender As Object, e As EventArgs) Handles btnResep.MouseEnter
        Me.btnResep.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnResep.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnhemo_MouseLeave(sender As Object, e As EventArgs) Handles btnHemo.MouseLeave
        Me.btnHemo.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnHemo.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnhemo_MouseEnter(sender As Object, e As EventArgs) Handles btnHemo.MouseEnter
        Me.btnHemo.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnHemo.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnLayanan_MouseLeave(sender As Object, e As EventArgs) Handles btnLayanan.MouseLeave
        Me.btnLayanan.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnLayanan.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnLayanan_MouseEnter(sender As Object, e As EventArgs) Handles btnLayanan.MouseEnter
        Me.btnLayanan.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnLayanan.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnCariReg_MouseLeave(sender As Object, e As EventArgs) Handles btnCariReg.MouseLeave
        Me.btnCariReg.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnCariReg.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnCariReg_MouseEnter(sender As Object, e As EventArgs) Handles btnCariReg.MouseEnter
        Me.btnCariReg.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnCariReg.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnOperasi_MouseLeave(sender As Object, e As EventArgs) Handles btnOperasi.MouseLeave
        Me.btnOperasi.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnOperasi.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnOperasi_MouseEnter(sender As Object, e As EventArgs) Handles btnOperasi.MouseEnter
        Me.btnOperasi.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnOperasi.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnOKParu_MouseLeave(sender As Object, e As EventArgs) Handles btnOKParu.MouseLeave
        Me.btnOKParu.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnOKParu.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnOKParu_MouseEnter(sender As Object, e As EventArgs) Handles btnOKParu.MouseEnter
        Me.btnOKParu.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnOKParu.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnPatologi_MouseLeave(sender As Object, e As EventArgs) Handles btnPatologi.MouseLeave
        Me.btnPatologi.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnPatologi.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnPatologi_MouseEnter(sender As Object, e As EventArgs) Handles btnPatologi.MouseEnter
        Me.btnPatologi.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnPatologi.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnDarah_MouseLeave(sender As Object, e As EventArgs) Handles btnDarah.MouseLeave
        Me.btnDarah.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnDarah.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnDarah_MouseEnter(sender As Object, e As EventArgs) Handles btnDarah.MouseEnter
        Me.btnDarah.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnDarah.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnHasilLis_MouseLeave(sender As Object, e As EventArgs) Handles btnHasilLis.MouseLeave
        Me.btnHasilLis.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnHasilLis.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnHasilLis_MouseEnter(sender As Object, e As EventArgs) Handles btnHasilLis.MouseEnter
        Me.btnHasilLis.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnHasilLis.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnVenti_MouseLeave(sender As Object, e As EventArgs) Handles btnVenti.MouseLeave
        Me.btnVenti.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnVenti.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnVenti_MouseEnter(sender As Object, e As EventArgs) Handles btnVenti.MouseEnter
        Me.btnVenti.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnVenti.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnHasilPA_MouseLeave(sender As Object, e As EventArgs) Handles btnHasilPA.MouseLeave
        Me.btnHasilPA.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnHasilPA.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnHasilPA_MouseEnter(sender As Object, e As EventArgs) Handles btnHasilPA.MouseEnter
        Me.btnHasilPA.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnHasilPA.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnPilihRuang_MouseLeave(sender As Object, e As EventArgs) Handles btnPilihRuang.MouseLeave
        Me.btnPilihRuang.BackColor = Color.PowderBlue
    End Sub

    Private Sub btnPilihRuang_MouseEnter(sender As Object, e As EventArgs) Handles btnPilihRuang.MouseEnter
        Me.btnPilihRuang.BackColor = Color.Aquamarine
    End Sub

    Private Sub btnInfoBiaya_MouseLeave(sender As Object, e As EventArgs) Handles btnInfoBiaya.MouseLeave
        Me.btnInfoBiaya.BackColor = Color.SeaGreen
    End Sub

    Private Sub btnInfoBiaya_MouseEnter(sender As Object, e As EventArgs) Handles btnInfoBiaya.MouseEnter
        Me.btnInfoBiaya.BackColor = Color.MediumSeaGreen
    End Sub

    Private Sub btnInfoPasien_MouseLeave(sender As Object, e As EventArgs) Handles btnInfoPasien.MouseLeave
        Me.btnInfoPasien.BackColor = Color.SeaGreen
    End Sub

    Private Sub btnInfoPasien_MouseEnter(sender As Object, e As EventArgs) Handles btnInfoPasien.MouseEnter
        Me.btnInfoPasien.BackColor = Color.MediumSeaGreen
    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        Dim konfirmasi As MsgBoxResult

        konfirmasi = MsgBox("Apakah anda yakin ingin keluar..?", vbQuestion + vbYesNo, "Konfirmasi")
        If konfirmasi = vbYes Then
            Me.Close()
            LoginForm.Show()
        End If
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F1
                If txtRegRanap.Text IsNot "" Then
                    Pindah_Kamar.Ambil_Data = True
                    Pindah_Kamar.Form_Ambil_Data = "Pindah"
                    Pindah_Kamar.Show()
                End If
            Case Keys.F2
                If txtRegRanap.Text IsNot "" Then
                    Checkout.Ambil_Data = True
                    Checkout.Form_Ambil_Data = "Cekout"
                    Checkout.Show()
                End If
            Case Keys.F3
                If txtRegRanap.Text IsNot "" Then
                    FormTindakan.Ambil_Data = True
                    FormTindakan.Form_Ambil_Data = "Tindakan"
                    FormTindakan.Show()
                    Me.Hide()
                End If
            Case Keys.F4
                If txtRegRanap.Text IsNot "" Then
                    Isi_Diagnosa.Ambil_Data = True
                    Isi_Diagnosa.Form_Ambil_Data = "Diagnosa"
                    Isi_Diagnosa.Show()
                End If
            Case Keys.F5
                If txtRegRanap.Text IsNot "" Then
                    Laboratorium.Ambil_Data = True
                    Laboratorium.Form_Ambil_Data = "Laboratorium"
                    Laboratorium.Show()
                    Me.Hide()
                End If
            Case Keys.F6
                If txtRegRanap.Text IsNot "" Then
                    Radiologi.Ambil_Data = True
                    Radiologi.Form_Ambil_Data = "Radiologi"
                    Radiologi.Show()
                    Me.Hide()
                End If
            Case Keys.F7
                If btnResep.Enabled = False Then
                    Return
                Else
                    If txtRegRanap.Text IsNot "" Then
                        Resep_Dokter.Ambil_Data = True
                        Resep_Dokter.Form_Ambil_Data = "Resep"
                        Resep_Dokter.Show()
                        Me.Hide()
                    End If
                End If
            Case Keys.F8
                If btnGizi.Enabled = False Then
                    Return
                Else
                    Dim ruang As New Ruangan
                    ruang.Ambil_Data = True
                    ruang.Form_Ambil_Data = "GiziRuang"
                    ruang.ShowDialog()
                End If
            Case Keys.F9
                If txtRegRanap.Text IsNot "" Then
                    Daftar_Bukti_Layanan.Ambil_Data = True
                    Daftar_Bukti_Layanan.Form_Ambil_Data = "Layanan"
                    Daftar_Bukti_Layanan.Show()
                    Me.Hide()
                End If
            Case Keys.Escape
                Dim konfirmasi As MsgBoxResult
                konfirmasi = MsgBox("Anda yakin ingin keluar..?", vbQuestion + vbYesNo, "Konfirmasi")
                If konfirmasi = vbYes Then
                    Me.Close()
                    LoginForm.Show()
                End If
        End Select

        If (e.KeyCode = Keys.F AndAlso e.Modifiers = Keys.Control) Then
            Daftar_Pasien.Ambil_Data = True
            Daftar_Pasien.Form_Ambil_Data = "Daftar Pasien"
            Daftar_Pasien.Show()
            LoginForm.Show()
        End If

    End Sub

    Private Sub btnGizi_Click(sender As Object, e As EventArgs) Handles btnGizi.Click
        Dim ruang As New Ruangan
        ruang.Ambil_Data = True
        ruang.Form_Ambil_Data = "GiziRuang"
        ruang.ShowDialog()
    End Sub

    Private Sub btnLayanan_Click(sender As Object, e As EventArgs) Handles btnLayanan.Click
        Daftar_Bukti_Layanan.Ambil_Data = True
        Daftar_Bukti_Layanan.Form_Ambil_Data = "Layanan"
        Daftar_Bukti_Layanan.Show()
        Me.Hide()
    End Sub

    Private Sub btnCariReg_KeyDown(sender As Object, e As KeyEventArgs) Handles btnCariReg.KeyDown
        If e.KeyCode = Keys.Enter Then
            Daftar_Pasien.Ambil_Data = True
            Daftar_Pasien.Form_Ambil_Data = "Daftar Pasien"
            Daftar_Pasien.Show()
            Me.Hide()
        End If
    End Sub

    Sub notif()
        Dim objPopup As New PopupNotifier
        objPopup.Image = My.Resources.popup_info
        objPopup.ImagePadding = New Padding(7, 13, 5, 10)

        objPopup.HeaderColor = Color.Green
        objPopup.AnimationDuration = 1000
        objPopup.Delay = 10000
        objPopup.TitleFont = New Font("Arial", 18, FontStyle.Bold)
        objPopup.TitleColor = Color.FromArgb(232, 243, 239)
        objPopup.TitleText = "Notification Alert"

        objPopup.ContentPadding = New Padding(5)
        objPopup.ContentFont = New Font("Arial", 12, FontStyle.Italic)
        objPopup.ContentText = "You get a message"
        objPopup.ContentColor = Color.Red
        objPopup.Popup()

        AddHandler objPopup.Click, AddressOf clickHandler
        AddHandler objPopup.Close, AddressOf closekHandler
    End Sub

    Private Sub closekHandler(sender As Object, e As EventArgs)
        MessageBox.Show("Close", "Message")
    End Sub

    Private Sub clickHandler(sender As Object, e As EventArgs)
        MessageBox.Show("Clicked", "Message")
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'Dim Message As String

        'If Listener.Pending = True Then
        '    Message = ""
        '    Client = Listener.AcceptTcpClient()
        '    Dim Reader As New StreamReader(Client.GetStream())

        '    While Reader.Peek > -1
        '        Message &= Convert.ToChar(Reader.Read()).ToString
        '    End While

        '    If Message.Contains("</>") Then
        '        nStart = InStr(Message, "</>") + 4
        '        nLast = InStr(Message, "<\>")
        '        Message = Mid(Message, nStart, nLast - nStart)
        '    End If

        '    ListBox1.Items.Add("Friend:- " + Message)
        '    Console.WriteLine(Message)
        '    notif()
        'End If
    End Sub

    Private Sub btnPatologi_Click(sender As Object, e As EventArgs) Handles btnPatologi.Click
        Dim fpa As Patologi = New Patologi
        fpa.ShowDialog()
    End Sub

    Private Sub btnDarah_Click(sender As Object, e As EventArgs) Handles btnDarah.Click
        BankDarah.Ambil_Data = True
        BankDarah.Form_Ambil_Data = "BDRS"
        BankDarah.Show()
        Me.Hide()
    End Sub

    Private Sub btnPilihRuang_Click(sender As Object, e As EventArgs) Handles btnPilihRuang.Click
        Dim ruang As New Ruangan
        ruang.Ambil_Data = True
        ruang.Form_Ambil_Data = "Ruang"
        ruang.ShowDialog()
    End Sub

    Private Sub btnHasilLis_Click(sender As Object, e As EventArgs) Handles btnHasilLis.Click
        DaftarHasilLab.Ambil_Data = True
        DaftarHasilLab.Form_Ambil_Data = "DaftarHasilLIS"
        DaftarHasilLab.Show()
        Me.Hide()
    End Sub

    Private Sub btnBatalOut_Click(sender As Object, e As EventArgs) Handles btnBatalOut.Click
        Daftar_Pasien.Ambil_Data = True
        Daftar_Pasien.Form_Ambil_Data = "Daftar Pasien Keluar"
        Daftar_Pasien.Show()
        Me.Hide()
    End Sub

    Private Sub btnInfoBiaya_Click(sender As Object, e As EventArgs) Handles btnInfoBiaya.Click
        Info_Biaya_Perawatan.Ambil_Data = True
        Info_Biaya_Perawatan.Form_Ambil_Data = "Info Biaya"
        Info_Biaya_Perawatan.Show()
        Me.Hide()
    End Sub

    Private Sub btnVenti_Click(sender As Object, e As EventArgs) Handles btnVenti.Click
        Dim venti As Ventilator = New Ventilator
        venti.ShowDialog()
    End Sub

    Sub updateDokter()
        Call koneksiServer()
        Try
            Dim str As String
            str = "UPDATE t_registrasi
                      SET kdTenagaMedis = '" & txtKdDokter.Text & "'
                    WHERE noDaftar = '" & txtNoDaftar.Text & "'
                      AND kdInstalasi = 'ki2'"

            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            'MsgBox("Update dokter berhasil dilakukan", MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox("Update data gagal dilakukan.", MessageBoxIcon.Error, "Error Registrasi Ranap")
        End Try

        conn.Close()
    End Sub

    Private Sub comboDokter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboDokter.SelectedIndexChanged
        Call koneksiServer()
        If comboDokter.Text = "-" Or comboDokter.Text = "" Then
            txtKdDokter.Text = "0000"
            ErrorProvider1.SetError(comboDokter, "Tolong diinputkan telebih dahulu")
        Else
            ErrorProvider1.Clear()
            Try
                Dim query As String
                query = "SELECT kdPetugasMedis FROM t_tenagamedis2 WHERE namapetugasMedis = '" & comboDokter.Text & "'"
                cmd = New MySqlCommand(query, conn)
                dr = cmd.ExecuteReader

                While dr.Read
                    txtKdDokter.Text = UCase(dr.GetString("kdPetugasMedis"))
                End While
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        conn.Close()
        'MsgBox(txtNoDaftar.Text)
        Call updateDokter()
    End Sub

    Private Sub DataGridView2_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridView2.RowsAdded
        Call subTotal()
    End Sub

    Private Sub btnInfoPasien_Click(sender As Object, e As EventArgs) Handles btnInfoPasien.Click
        Daftar_Pasien.Ambil_Data = True
        Daftar_Pasien.Form_Ambil_Data = "Info Pasien"
        Daftar_Pasien.Show()
        Me.Hide()
    End Sub

    Private Sub txtTarifKmr_TextChanged(sender As Object, e As EventArgs) Handles txtTarifKmr.TextChanged
        Dim g As Long
        g = Val(CInt(txtTarifKmr.Text))
        txtTarifKmr.Text = g.ToString("#,##0")
    End Sub

    Private Sub txtKelas_TextChanged(sender As Object, e As EventArgs) Handles txtKelas.TextChanged
        If (txtKelas.Text = "KELAS I" And Label1.Text = "Lavender") Then
            txtTarifDPJP.Text = 0
        ElseIf (txtKelas.Text = "KELAS I" And Label1.Text = "Kemuning") Then
            txtTarifDPJP.Text = 0
        ElseIf (txtKelas.Text = "KELAS I" And Label1.Text = "Dahlia" And txtUnitRanap.Text = "DAHLIA - ISOLASI") Then
            txtTarifDPJP.Text = 0
        ElseIf Label1.Text = "ICU" Then
            txtTarifDPJP.Text = 0
        ElseIf (txtKelas.Text = "KELAS I" And Label1.Text = "Perinatologi" And txtUnitRanap.Text = "PERINATOLOGI - NICU") Then
            txtTarifDPJP.Text = 0
        ElseIf (txtKelas.Text = "KELAS I" And txtUnitRanap.Text = "PERINATOLOGI") Then
            txtTarifDPJP.Text = Format(Integer.Parse("150000"), "###,###")
        ElseIf (txtKelas.Text = "KELAS I" And txtUnitRanap.Text = "PERINATOLOGI ISOLASI") Then
            txtTarifDPJP.Text = Format(Integer.Parse("150000"), "###,###")
        ElseIf txtUnitRanap.Text.Equals("Seruni A - PICU/HCU", StringComparison.OrdinalIgnoreCase) Then
            txtTarifDPJP.Text = 0
        ElseIf txtKelas.Text.Equals("EXECUTIVE") Then
            txtTarifDPJP.Text = Format(Integer.Parse("500000"), "###,###")
            txtTarifAskep.Text = Format(Integer.Parse("100000"), "###,###")
        ElseIf txtKelas.Text.Equals("VVIP") Then
            txtTarifDPJP.Text = Format(Integer.Parse("500000"), "###,###")
            txtTarifAskep.Text = Format(Integer.Parse("100000"), "###,###")
        ElseIf txtKelas.Text.Equals("VIP") Then
            txtTarifDPJP.Text = Format(Integer.Parse("250000"), "###,###")
            txtTarifAskep.Text = Format(Integer.Parse("85000"), "###,###")
        ElseIf txtKelas.Text.Equals("UTAMA") Then
            txtTarifDPJP.Text = Format(Integer.Parse("150000"), "###,###")
            txtTarifAskep.Text = Format(Integer.Parse("70000"), "###,###")
        ElseIf txtKelas.Text.Equals("KELAS I") Then
            txtTarifDPJP.Text = Format(Integer.Parse("120000"), "###,###")
            txtTarifAskep.Text = Format(Integer.Parse("60000"), "###,###")
        ElseIf txtKelas.Text.Equals("KELAS II") Then
            txtTarifDPJP.Text = Format(Integer.Parse("100000"), "###,###")
            txtTarifAskep.Text = Format(Integer.Parse("40000"), "###,###")
        ElseIf txtKelas.Text.Equals("KELAS III") Then
            txtTarifDPJP.Text = Format(Integer.Parse("90000"), "###,###")
            txtTarifAskep.Text = Format(Integer.Parse("40000"), "###,###")
        Else
            txtTarifDPJP.Text = 0
        End If

        If txtUnitRanap.Text.Equals("Lavender Ventilator", StringComparison.OrdinalIgnoreCase) Then
            txtTarifAskep.Text = Format(Integer.Parse("300000"), "###,###")
        ElseIf txtUnitRanap.Text.Equals("Lavender Tanpa Ventilator", StringComparison.OrdinalIgnoreCase) Then
            txtTarifAskep.Text = Format(Integer.Parse("120000"), "###,###")
        ElseIf txtUnitRanap.Text.Equals("Perinatologi", StringComparison.OrdinalIgnoreCase) Then
            txtTarifAskep.Text = Format(Integer.Parse("40000"), "###,###")
        ElseIf txtUnitRanap.Text.Equals("Perinatologi - NICU", StringComparison.OrdinalIgnoreCase) Then
            txtTarifAskep.Text = Format(Integer.Parse("250000"), "###,###")
        ElseIf txtUnitRanap.Text.Equals("ICU", StringComparison.OrdinalIgnoreCase) Then
            txtTarifAskep.Text = Format(Integer.Parse("250000"), "###,###")
        ElseIf txtUnitRanap.Text.Contains("HCU") Then
            txtTarifAskep.Text = Format(Integer.Parse("125000"), "###,###")
        ElseIf txtUnitRanap.Text.Contains("PICU") Then
            txtTarifAskep.Text = Format(Integer.Parse("250000"), "###,###")
        End If

    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex = -1 Then
            Return
        End If

        tindakan = DataGridView2.Item(1, e.RowIndex).Value.ToString
        tarif = DataGridView2.Item(2, e.RowIndex).Value.ToString
        'jml = DataGridView2.Item(3, e.RowIndex).Value.ToString
        noTindakan = DataGridView2.Item(8, e.RowIndex).Value.ToString
        stotal = DataGridView2.Item(7, e.RowIndex).Value.ToString
        id = DataGridView2.Item(9, e.RowIndex).Value.ToString
        totalTindakan = cekTotalTindakan(noTindakan)
        'txtTotalTindakan.Text = cekTotalTindakan(id)
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        If e.RowIndex = -1 Then
            Return
        End If

        tindakan = DataGridView2.Item(1, e.RowIndex).Value.ToString
        tarif = DataGridView2.Item(2, e.RowIndex).Value.ToString
        'jml = DataGridView2.Item(3, e.RowIndex).Value.ToString
        noTindakan = DataGridView2.Item(8, e.RowIndex).Value.ToString
        stotal = DataGridView2.Item(7, e.RowIndex).Value.ToString
        id = DataGridView2.Item(9, e.RowIndex).Value.ToString
        totalTindakan = cekTotalTindakan(noTindakan)
        'txtTotalTindakan.Text = cekTotalTindakan(id)
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

    Private Sub DataGridView2_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView2.CellFormatting
        DataGridView2.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise
        DataGridView2.DefaultCellStyle.SelectionForeColor = Color.Black
        DataGridView2.DefaultCellStyle.ForeColor = Color.Black
        DataGridView2.DefaultCellStyle.Font = New Font("Tahoma", 9.5, FontStyle.Regular)

        For i As Integer = 0 To DataGridView2.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next

        If e.ColumnIndex = 7 And e.RowIndex <> Me.DataGridView2.NewRowIndex Then
            Dim d As Double = Double.Parse(e.Value.ToString())
            e.Value = d.ToString("N0")
        End If
    End Sub

    Private Sub DataGridView2_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellValueChanged
        Dim idDgv As String = ""
        Dim noTindakDgv As String = ""
        Dim jmlDgv As String = ""
        Dim tarifDgv As String = ""
        Dim subTotalDgv As String = ""
        Dim dgvrow As New System.Windows.Forms.DataGridViewRow

        For Each dgvrow In Me.DataGridView2.SelectedRows
            'idDgv = dgvrow.Cells(9).Value
            'noTindakDgv = dgvrow.Cells(8).Value
            'jmlDgv = dgvrow.Cells(3).Value
            'tarifDgv = dgvrow.Cells(2).Value
            'MsgBox(jmlDgv)
            'If jmlDgv IsNot Nothing Then
            '    subTotalDgv = Val(jmlDgv) * Val(tarifDgv)
            '    dgvrow.Cells(7).Value = subTotalDgv
            '    'Call totalTarif()
            '    'Call updateJml(id, jml, subTotal)
            '    'Call updateTotal(noTindak, txtTotalDetail2.Text)
            '    'MsgBox(id & "|" & noTindakDgv & "|" & jmlDgv & "|" & subTotalDgv & "|" & "totalTindakan" & "|")
            'ElseIf jmlDgv Is Nothing Then
            '    subTotalDgv = Val(jmlDgv) * Val(tarifDgv)
            '    dgvrow.Cells(7).Value = subTotalDgv
            '    dgvrow.Cells(3).Value = 0
            'End If

            'If dgvrow.Cells(5).Value <> "" Then
            '    Dim perawat As String = dgvrow.Cells(5).Value.ToString
            '    Dim queryPpa As String
            '    Try
            '        dr.Close()
            '        conn.Close()
            '        Call koneksiServer()
            '        queryPpa = "SELECT * FROM t_tenagamedis2 WHERE namapetugasMedis = '" & perawat & "'"
            '        cmd = New MySqlCommand(queryPpa, conn)
            '        dr = cmd.ExecuteReader
            '        While dr.Read
            '            kdPPA = dr.GetString("kdPetugasMedis")
            '            'MsgBox(kdPPA)
            '        End While
            '        'dr.Close()
            '        'conn.Close()
            '    Catch ex As Exception
            '        'MessageBox.Show(ex.Message)
            '    End Try
            'Else
            '    dgvrow.Cells(9).Value = "-"
            'End If

            'Call updatePpa(id, dgvrow.Cells(11).Value.ToString)
        Next
    End Sub

    Private Sub TextBox_keyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub btnOperasi_Click(sender As Object, e As EventArgs) Handles btnOperasi.Click
        Dim fop As Operasi = New Operasi
        fop.ShowDialog()
    End Sub

    Private Sub btnPasienCovid_Click(sender As Object, e As EventArgs) Handles btnPasienCovid.Click
        Dim fpc As PasienCovid = New PasienCovid
        fpc.ShowDialog()
    End Sub

    Private Sub btnHemo_Click(sender As Object, e As EventArgs) Handles btnHemo.Click
        Dim fhd As Hemodialisa = New Hemodialisa
        fhd.ShowDialog()
    End Sub

    Private Sub btnOKParu_Click(sender As Object, e As EventArgs) Handles btnOKParu.Click
        Dim fok As OKParu = New OKParu
        fok.ShowDialog()
    End Sub

    Private Sub btnHasilPA_Click(sender As Object, e As EventArgs) Handles btnHasilPA.Click
        DaftarHasilPA.Ambil_Data = True
        DaftarHasilPA.Form_Ambil_Data = "DaftarHasilPA"
        DaftarHasilPA.Show()
        Me.Hide()
    End Sub

    Private Sub txtUnitRanap_TextChanged(sender As Object, e As EventArgs) Handles txtUnitRanap.TextChanged
        Call tampilVenti()
        Call cekRIK()

        If spek = "RIK" Then
            btnVenti.Enabled = True
        Else
            btnVenti.Enabled = False
        End If
    End Sub

    Private Sub DataGridView2_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DataGridView2.EditingControlShowing
        'If DataGridView2.CurrentCell.ColumnIndex = 7 Then
        '    AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
        'End If

        Select Case DataGridView2.CurrentCell.ColumnIndex
            Case Column4.Index
                AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
            Case Column6.Index
                'MsgBox("PPA")
                'Dim autoText2 As TextBox = TryCast(e.Control, TextBox)
                ''autoText2.AutoCompleteMode = AutoCompleteMode.Suggest
                ''autoText2.AutoCompleteSource = AutoCompleteSource.CustomSource
                'Dim dataPerawat As New AutoCompleteStringCollection()
                'If Column5 IsNot Nothing Then
                '    'MsgBox("Autocomplete PPA")
                '    autoText2.AutoCompleteMode = AutoCompleteMode.Suggest
                '    autoText2.AutoCompleteSource = AutoCompleteSource.CustomSource
                '    addItemsPerawat(dataPerawat)
                '    autoText2.AutoCompleteCustomSource = dataPerawat
                'End If
        End Select
    End Sub

    Private Sub addItemsPerawat(colPerawat As AutoCompleteStringCollection)
        Call koneksiServer()
        Try
            Dim query2 As String
            Dim dr2 As MySqlDataReader
            Dim cmd2 As MySqlCommand
            query2 = "SELECT namapetugasMedis FROM t_tenagamedis2 WHERE kdKelompokTenagaMedis in ('ktm1','ktm4','ktm11','ktm15') ORDER BY namapetugasMedis ASC"
            cmd2 = New MySqlCommand(query2, conn)
            dr2 = cmd2.ExecuteReader

            While dr2.Read
                colPerawat.Add(dr2.GetString("namapetugasMedis"))
            End While
            dr2.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub DataGridView2_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            DataGridView2.Rows(e.RowIndex).Selected = True
            idx = e.RowIndex
            'DataGridView2.CurrentCell = DataGridView2.Rows(e.RowIndex).Cells(1)
            ContextMenuStrip1.Show(DataGridView2, e.Location)
            ContextMenuStrip1.Show(Cursor.Position)
        End If
    End Sub

    Private Sub TambahItem_Click(sender As Object, e As EventArgs) Handles TambahItem.Click
        FormTindakan.Ambil_Data = True
        FormTindakan.Form_Ambil_Data = "Edit Tindakan"
        'Call FormTindakan.tampilDataSudahDitindakAll(txtKdInstalasi.Text, noTindakanPenunjang)
        FormTindakan.Show()
        Me.Hide()
    End Sub

    Private Sub HapusItem_Click(sender As Object, e As EventArgs) Handles HapusItem.Click
        Dim konfirmasi As MsgBoxResult
        konfirmasi = MsgBox("Apakah anda yakin ingin menghapus tindakan '" & tindakan & "' ?", vbQuestion + vbYesNo, "Konfirmasi")
        If konfirmasi = vbYes Then
            Call deleteDetail(id)
            Call updateAfterDelete(noTindakan)

            Dim index As Integer
            index = DataGridView2.CurrentCell.RowIndex
            DataGridView2.Rows.RemoveAt(index)

            Call tampilDataTindakan()
        End If
    End Sub

    Sub deleteDetail(idDel As String)
        Try
            Call koneksiServer()
            Dim query As String
            query = "DELETE FROM t_detailtindakanpasienranap WHERE idTindakanPasienRanap= '" & idDel & "'"
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
        total = Integer.Parse(totalTindakan) - Integer.Parse(stotal)
        Try
            Call koneksiServer()
            Dim str As String
            str = "UPDATE t_tindakanpasienranap
                          SET totalTarifTindakan = '" & total & "'
                        WHERE noTindakanPasienRanap = '" & noTindakanDel & "'"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            'MsgBox("Update dokter berhasil dilakukan", MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox("Update data gagal dilakukan.", MessageBoxIcon.Error, "Error Update After Delete")
        End Try

        conn.Close()
    End Sub
End Class
