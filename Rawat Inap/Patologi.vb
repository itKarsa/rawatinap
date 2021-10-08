Option Explicit On
Imports System.IO
Imports System.Net.Sockets
Imports MySql.Data.MySqlClient

Public Class Patologi

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Dim stKawin, gender, str, tmline, kdHasil As String
    Dim Client As TcpClient
    'Dim Listener As New TcpListener(8000)

    Sub clearCheckList()
        CheckedListBox1.ClearSelected()
        CheckedListBox2.ClearSelected()

        For Each i As Integer In CheckedListBox1.CheckedIndices
            CheckedListBox1.SetItemCheckState(i, CheckState.Unchecked)
        Next

        For Each i As Integer In CheckedListBox2.CheckedIndices
            CheckedListBox2.SetItemCheckState(i, CheckState.Unchecked)
        Next
    End Sub

    Sub riwayatPA()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        query = "SELECT * 
                   FROM vw_riwayatpasienpa
                  WHERE noRekamedis = '" & txtNoRM.Text & "'
               ORDER BY tglMasukPARanap DESC"
        'MsgBox(query)
        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dgv.Rows.Clear()

        Do While dr.Read
            dgv.Rows.Add(Convert.ToDateTime(dr.Item("tglMasukPARanap")), dr.Item("tindakan"), dr.Item("namapetugasMedis"),
                         dr.Item("diagnosaKlinis"), dr.Item("statusPA"), dr.Item("kdHasilPA"),
                         dr.Item("noRegistrasiPARanap"), dr.Item("kdHasilPA"), dr.Item("DokPA"))
        Loop
        conn.Close()
    End Sub

    Sub autoNoPermintaan()
        Dim noPermintaanPA As String

        Try
            Call koneksiServer()
            Dim query As String
            query = "Select SUBSTR(noRegistrasiPARanap,18,4) FROM t_registrasipatologiranap ORDER BY CAST(SUBSTR(noRegistrasiPARanap,18,4) AS UNSIGNED) DESC LIMIT 1"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                dr.Read()
                noPermintaanPA = "RIPA" + Format(Now, "ddMMyyHHmmss") + "-" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                txtNoPermintaan.Text = noPermintaanPA
            Else
                noPermintaanPA = "RIPA" + Format(Now, "ddMMyyHHmmss") + "-1"
                txtNoPermintaan.Text = noPermintaanPA
            End If
            conn.Close()
        Catch ex As Exception

        End Try

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

    Sub autoNoTindakan()
        Dim noTindakanPA As String

        Try
            Call koneksiServer()
            Dim query As String
            query = "Select SUBSTR(noTindakanPARanap,17,4) FROM t_tindakanpatologiranap ORDER BY CAST(SUBSTR(noTindakanPARanap,17,4) AS UNSIGNED) DESC LIMIT 1"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                dr.Read()
                noTindakanPA = "TPA" + Format(Now, "ddMMyyHHmmss") + "-" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                txtNoTindakan.Text = noTindakanPA
            Else
                noTindakanPA = "TPA" + Format(Now, "ddMMyyHHmmss") + "-1"
                txtNoTindakan.Text = noTindakanPA
            End If
            conn.Close()
        Catch ex As Exception

        End Try

    End Sub

    Sub addRegistrasiPa()
        Call koneksiServer()
        Try
            'conn.Open()
            Dim str As String
            str = "INSERT INTO t_registrasipatologiranap(noRegistrasiPARanap,noDaftar,kdUnitAsal,unitAsal,kdUnit,
                                                         unit,kelas,statusPerkawinan,
                                                         tglMasukPARanap,kdDokterPengirim,lokalisasi,diagnosaKlinis,stadiumT,
                                                         stadiumN,stadiumM,bahan,fiksasi,riwayatKlinisDulu,
                                                         riwayatKlinisSkrg,diagnosaKanker,ketHaidTerakhir,statusPA,
                                                         userModify,dateModify) 
                   VALUES ('" & txtNoPermintaan.Text & "','" & txtReg.Text & "','" & txtKdRuang.Text & "','" & txtRuang.Text & "','3005',
                           'Patologi Anatomi','" & txtKelas.Text & "','" & stKawin & "',
                           '" & Format(datePermintaan.Value, "yyyy-MM-dd HH:mm:ss") & "','" & txtKdDokter.Text & "','" & txtLokalisasi.Text & "','" & txtDiagnos.Text & "','" & txtStadiumT.Text & "',
                           '" & txtStadiumN.Text & "','" & txtStadiumM.Text & "',@bahan,@fiksasi,'" & txtDulu.Text & "',
                           '" & txtSekarang.Text & "',@dgKanker,'" & txtKetHaid.Text & "','PERMINTAAN',
                           ';" & LoginForm.txtUsername.Text & "',';" & Format(DateTime.Now, "yyyy-MM-dd HH:mm:ss") & "')"
            'cmd = New MySqlCommand(str, conn)
            Using cmd As New MySqlCommand(str, conn)
                cmd.Parameters.AddWithValue("@bahan", pilihBahan)
                cmd.Parameters.AddWithValue("@fiksasi", pilihFiksasi)
                cmd.Parameters.AddWithValue("@dgKanker", pilihRiwayat)
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
            End Using

            MsgBox("Permintaan PA berhasil dilakukan", MsgBoxStyle.Information, "Information")
        Catch ex As Exception
            MsgBox("Insert data Permintaan PA gagal dilakukan." & ex.Message, MsgBoxStyle.Critical, "Error Permintaan")
            cmd.Dispose()
        End Try

        'conn.Close()
    End Sub

    Sub addTindakan()
        Call koneksiServer()
        Try
            Dim str As String
            str = "INSERT INTO t_tindakanpatologiranap(noTindakanPARanap,noRegistrasiPARanap,totalTindakanPA,statusPembayaran) 
                   VALUES ('" & txtNoTindakan.Text & "','" & txtNoPermintaan.Text & "','0','BELUM LUNAS')"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            'MsgBox("Insert Tindakan Lab berhasil dilakukan", MsgBoxStyle.Information, "Information")
        Catch ex As Exception
            MsgBox("Insert Tindakan PA gagal dilakukan." & ex.Message, MsgBoxStyle.Critical, "Error Tindakan")
        End Try

        conn.Close()
    End Sub

    Sub statusHasilLab()

        Try
            Call koneksiServer()
            Dim str As String
            str = "SELECT noRegistrasiPARanap, nmPasien,
                   IF(noHasilPemeriksaanRanap IS NULL,'YES','NO') AS Status
                   FROM vw_cetakhasillabranap
                   WHERE noRegistrasiPARanap = '" & txtNoPermintaan.Text & "'"
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

    Private Sub PopulateCheckListBoxesBahan()
        Call koneksiServer()
        cmd = New MySqlCommand("SELECT bahanPA FROM t_bahanpa", conn)
        da = New MySqlDataAdapter(cmd)

        Using sda As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            sda.Fill(dt)
            CheckedListBox1.DisplayMember = "bahanPA"
            CheckedListBox1.ValueMember = "bahanPA"
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    CheckedListBox1.Items.Add(CStr(dt.Rows(i).Item(0)), False)
                Next
            End If
            'AddHandler chk.CheckedChanged, AddressOf CheckBox_Checked
        End Using
    End Sub

    Private Sub PopulateCheckListBoxesFiksasi()
        Call koneksiServer()
        cmd = New MySqlCommand("SELECT fiksasi FROM t_fiksasipa", conn)
        da = New MySqlDataAdapter(cmd)

        Using sda As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            sda.Fill(dt)
            CheckedListBox2.DisplayMember = "fiksasi"
            CheckedListBox2.ValueMember = "fiksasi"
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    CheckedListBox2.Items.Add(CStr(dt.Rows(i).Item(0)), False)
                Next
            End If
            'AddHandler chk.CheckedChanged, AddressOf CheckBox_Checked
        End Using
    End Sub

    Private Sub PopulateCheckListBoxesKanker()
        Dim str As String()
        str = {"Klinik", "RO", "Parth.Klinik", "Operasi", "Nekropsi"}

        For Each row As String In str
            CheckedListBox3.DisplayMember = "str"
            CheckedListBox3.ValueMember = "str"
            CheckedListBox3.Items.Add(row)
        Next
    End Sub

    'Private Sub PopulateCheckBoxesBahan()
    '    Call koneksiServer()
    '    cmd = New MySqlCommand("SELECT UPPER(bahanPA) AS bahanPA FROM t_bahanpa", conn)
    '    da = New MySqlDataAdapter(cmd)

    '    Using sda As MySqlDataAdapter = New MySqlDataAdapter(cmd)
    '        Dim dt As DataTable = New DataTable()
    '        sda.Fill(dt)
    '        For Each row As DataRow In dt.Rows
    '            Dim chk As CheckBox = New CheckBox()
    '            chk.Width = 165
    '            chk.Text = row("bahanPA").ToString()
    '            chk.ForeColor = Color.Black
    '            AddHandler chk.CheckedChanged, AddressOf CheckBox_Checked
    '            FlowLayoutPanel1.Controls.Add(chk)
    '        Next
    '    End Using
    'End Sub

    'Private Sub PopulateCheckBoxesFiksasi()
    '    Call koneksiServer()
    '    cmd = New MySqlCommand("SELECT fiksasi FROM t_fiksasipa", conn)
    '    da = New MySqlDataAdapter(cmd)

    '    Using sda As MySqlDataAdapter = New MySqlDataAdapter(cmd)
    '        Dim dt As DataTable = New DataTable()
    '        sda.Fill(dt)
    '        For Each row As DataRow In dt.Rows
    '            Dim chk As CheckBox = New CheckBox()
    '            chk.Width = 165
    '            chk.Text = row("fiksasi").ToString()
    '            chk.ForeColor = Color.Black
    '            AddHandler chk.CheckedChanged, AddressOf CheckBox_Checked
    '            FlowLayoutPanel2.Controls.Add(chk)
    '        Next
    '    End Using
    'End Sub

    'Private Sub PopulateCheckBoxesKanker()
    '    Dim str As String()
    '    str = {"Klinik", "RO", "Parth.Klinik", "Operasi", "Nekropsi"}

    '    For Each row As String In str
    '        Dim chk As CheckBox = New CheckBox()
    '        chk.Width = 110
    '        chk.Text = row.ToString()
    '        chk.ForeColor = Color.Black
    '        AddHandler chk.CheckedChanged, AddressOf CheckBox_Checked
    '        FlowLayoutPanel3.Controls.Add(chk)
    '    Next
    'End Sub

    Private Sub CheckBox_Checked(ByVal sender As Object, ByVal e As EventArgs)
        Dim chk As CheckBox = (TryCast(sender, CheckBox))

        If chk.Checked Then
            MessageBox.Show("You selected: " & chk.Text)
            'str += String.Join(",", chk.Text.ToArray())
        End If
    End Sub

    Private Sub PopulateRadioButtonStatus()
        Dim str As String() = {}

        If Form1.txtJk.Text = "L" Then
            str = {"Kawin", "Belum", "Duda"}
        ElseIf Form1.txtJk.Text = "P" Then
            str = {"Kawin", "Belum", "Janda"}
        End If

        For Each row As String In str
            Dim rd As RadioButton = New RadioButton()
            rd.Width = 80
            rd.Text = row.ToString()
            rd.ForeColor = Color.Black
            AddHandler rd.CheckedChanged, AddressOf RadioButton_Checked
            FlowLayoutPanel4.Controls.Add(rd)
        Next
    End Sub

    Private Sub RadioButton_Checked(ByVal sender As Object, ByVal e As EventArgs)
        Dim rd As RadioButton = (TryCast(sender, RadioButton))
        If rd.Checked Then
            stKawin = rd.Text
            'MessageBox.Show("You selected: " & rd.Text)
        End If
    End Sub

    Function tampilProses(noReg As String)
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim value1 As String = ""

        query = "SELECT statusPA
                   FROM vw_riwayatpasienpa 
                  WHERE noDaftar = '" & noReg & "'
                  ORDER BY tglMasukPARanap DESC LIMIT 1"
        'MsgBox(query)
        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            value1 = dr.Item("statusPA").ToString
        End If
        conn.Close()

        Return value1
    End Function

    Function pilihBahan() As String
        Dim bahan As New List(Of String)
        Dim noBahan As String

        For Each item As String In ListBox4.Items
            bahan.Add(item)
        Next

        noBahan = String.Join(", ", bahan.ToArray)

        Return noBahan
    End Function

    Function pilihFiksasi() As String
        Dim fiksasi As New List(Of String)
        Dim noFiksasi As String

        For Each item As String In ListBox5.Items
            fiksasi.Add(item)
        Next

        noFiksasi = String.Join(",", fiksasi.ToArray)

        Return noFiksasi
    End Function

    Function pilihRiwayat() As String
        Dim riwayat As New List(Of String)
        Dim noRiwayat As String

        For Each item As String In ListBox3.Items
            riwayat.Add(item)
        Next

        noRiwayat = String.Join(", ", riwayat.ToArray)

        Return noRiwayat
    End Function

    Private Sub Patologi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Form1.txtJk.Text = "L" Then
            gender = "Pria"
        ElseIf Form1.txtJk.Text = "P" Then
            gender = "Wanita"
        End If

        Call autoNoPermintaan()
        Call autoNoTindakan()
        Call autoDokter()

        txtReg.Text = Form1.txtNoDaftar.Text
        txtNoRM.Text = Form1.txtRekMed.Text
        txtPasien.Text = Form1.txtNamaPasien.Text
        txtUmur.Text = Form1.txtUmur.Text
        txtJk.Text = gender
        txtKdRuang.Text = Form1.txtKdUnitRanap.Text
        txtRuang.Text = Form1.txtUnitRanap.Text
        txtDokter.Text = Form1.comboDokter.Text
        txtKelas.Text = Form1.txtKelas.Text
        txtIpAddress.Text = Form1.txtIpAddress.Text

        PopulateRadioButtonStatus()
        PopulateCheckListBoxesBahan()
        PopulateCheckListBoxesFiksasi()
        PopulateCheckListBoxesKanker()

        tmline = tampilProses(Form1.txtNoDaftar.Text)
        Call riwayatPA()
        'MsgBox(tmline)

        Select Case tmline
            Case ""
                TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Absolute
                TableLayoutPanel1.RowStyles(1).Height = 0
                'Me.Height = 690
                TableLayoutPanel1.RowStyles(4).SizeType = SizeType.Absolute
                TableLayoutPanel1.RowStyles(4).Height = 0
                btnBatal.Enabled = False
                Me.Height = 535
            Case "PERMINTAAN"
                TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Absolute
                TableLayoutPanel1.RowStyles(1).Height = 75
                Me.Height = 765
                PictureBox1.Image = My.Resources.PAOrder
                txtOrder.ForeColor = Color.FromArgb(26, 141, 95)
                btnBatal.Enabled = True
            Case "DALAM TINDAKAN"
                TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Absolute
                TableLayoutPanel1.RowStyles(1).Height = 75
                Me.Height = 765
                PictureBox1.Image = My.Resources.PAProcessing
                txtOrder.ForeColor = Color.FromArgb(26, 141, 95)
                txtProses.ForeColor = Color.FromArgb(26, 141, 95)
                btnBatal.Enabled = False
            Case "SELESAI"
                TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Absolute
                TableLayoutPanel1.RowStyles(1).Height = 75
                Me.Height = 765
                PictureBox1.Image = My.Resources.PAComplete
                txtOrder.ForeColor = Color.FromArgb(26, 141, 95)
                txtProses.ForeColor = Color.FromArgb(26, 141, 95)
                txtSelesai.ForeColor = Color.FromArgb(26, 141, 95)
                btnBatal.Enabled = False
        End Select
    End Sub

    Private Sub txtLokalisasi_TextChanged(sender As Object, e As EventArgs) Handles txtLokalisasi.TextChanged
        If txtLokalisasi.Text <> "" Then
            txtLokalisasi.BackColor = Color.White
        End If
    End Sub

    Private Sub txtLokalisasi_LostFocus(sender As Object, e As EventArgs) Handles txtLokalisasi.LostFocus
        If txtLokalisasi.Text = "" Then
            txtLokalisasi.BackColor = Color.FromArgb(255, 112, 112)
        End If
    End Sub

    Private Sub txtLokalisasi_KeyDown(sender As Object, e As KeyEventArgs) Handles txtLokalisasi.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtLokalisasi.Text = "" Then
                txtLokalisasi.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub txtDiagnos_TextChanged(sender As Object, e As EventArgs) Handles txtDiagnos.TextChanged
        If txtDiagnos.Text <> "" Then
            txtDiagnos.BackColor = Color.White
        End If
    End Sub

    Private Sub txtDiagnos_LostFocus(sender As Object, e As EventArgs) Handles txtDiagnos.LostFocus
        If txtDiagnos.Text = "" Then
            txtDiagnos.BackColor = Color.FromArgb(255, 112, 112)
        End If
    End Sub

    Private Sub txtDiagnos_KeyDown(sender As Object, e As KeyEventArgs) Handles txtDiagnos.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtDiagnos.Text = "" Then
                txtDiagnos.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub txtStadiumT_TextChanged(sender As Object, e As EventArgs) Handles txtStadiumT.TextChanged
        If txtStadiumT.Text <> "" Then
            txtStadiumT.BackColor = Color.White
        End If
    End Sub

    Private Sub txtStadiumT_LostFocus(sender As Object, e As EventArgs) Handles txtStadiumT.LostFocus
        If txtStadiumT.Text = "" Then
            txtStadiumT.BackColor = Color.FromArgb(255, 112, 112)
        End If
    End Sub

    Private Sub txtStadiumT_KeyDown(sender As Object, e As KeyEventArgs) Handles txtStadiumT.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtStadiumT.Text = "" Then
                txtStadiumT.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub txtStadiumN_TextChanged(sender As Object, e As EventArgs) Handles txtStadiumN.TextChanged
        If txtStadiumN.Text <> "" Then
            txtStadiumN.BackColor = Color.White
        End If
    End Sub

    Private Sub txtStadiumN_LostFocus(sender As Object, e As EventArgs) Handles txtStadiumN.LostFocus
        If txtStadiumN.Text = "" Then
            txtStadiumN.BackColor = Color.FromArgb(255, 112, 112)
        End If
    End Sub

    Private Sub txtStadiumN_KeyDown(sender As Object, e As KeyEventArgs) Handles txtStadiumN.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtStadiumN.Text = "" Then
                txtStadiumN.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub txtStadiumM_TextChanged(sender As Object, e As EventArgs) Handles txtStadiumM.TextChanged
        If txtStadiumM.Text <> "" Then
            txtStadiumM.BackColor = Color.White
        End If
    End Sub

    Private Sub txtStadiumM_LostFocus(sender As Object, e As EventArgs) Handles txtStadiumM.LostFocus
        If txtStadiumM.Text = "" Then
            txtStadiumM.BackColor = Color.FromArgb(255, 112, 112)
        End If
    End Sub

    Private Sub txtStadiumM_KeyDown(sender As Object, e As KeyEventArgs) Handles txtStadiumM.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtStadiumM.Text = "" Then
                txtStadiumM.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub txtDulu_TextChanged(sender As Object, e As EventArgs) Handles txtDulu.TextChanged
        If txtDulu.Text <> "" Then
            txtDulu.BackColor = Color.White
        End If
    End Sub

    Private Sub txtDulu_LostFocus(sender As Object, e As EventArgs) Handles txtDulu.LostFocus
        If txtDulu.Text = "" Then
            txtDulu.BackColor = Color.FromArgb(255, 112, 112)
        End If
    End Sub

    Private Sub txtDulu_KeyDown(sender As Object, e As KeyEventArgs) Handles txtDulu.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtDulu.Text = "" Then
                txtDulu.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub txtSekarang_TextChanged(sender As Object, e As EventArgs) Handles txtSekarang.TextChanged
        If txtSekarang.Text <> "" Then
            txtSekarang.BackColor = Color.White
        End If
    End Sub

    Private Sub txtSekarang_LostFocus(sender As Object, e As EventArgs) Handles txtSekarang.LostFocus
        If txtSekarang.Text = "" Then
            txtSekarang.BackColor = Color.FromArgb(255, 112, 112)
        End If
    End Sub

    Private Sub txtSekarang_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSekarang.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtSekarang.Text = "" Then
                txtSekarang.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub txtKetHaid_TextChanged(sender As Object, e As EventArgs) Handles txtKetHaid.TextChanged
        If txtKetHaid.Text <> "" Then
            txtKetHaid.BackColor = Color.White
        End If
    End Sub

    Private Sub txtKetHaid_LostFocus(sender As Object, e As EventArgs) Handles txtKetHaid.LostFocus
        If txtKetHaid.Text = "" Then
            txtKetHaid.BackColor = Color.FromArgb(255, 112, 112)
        End If
    End Sub

    Private Sub txtKetHaid_KeyDown(sender As Object, e As KeyEventArgs) Handles txtKetHaid.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If txtKetHaid.Text = "" Then
                txtKetHaid.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub rdYa_CheckedChanged(sender As Object, e As EventArgs) Handles rdYa.CheckedChanged
        CheckedListBox3.Enabled = True
    End Sub

    Private Sub rdTidak_CheckedChanged(sender As Object, e As EventArgs) Handles rdTidak.CheckedChanged
        CheckedListBox3.Enabled = False
    End Sub

    Private Sub txtDokter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtDokter.SelectedIndexChanged
        Call koneksiServer()
        Try
            Dim query As String
            query = "select * from t_tenagamedis2 where namapetugasMedis = '" & txtDokter.Text & "'"
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

    Private Sub CheckedListBox1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        Dim item As String = CheckedListBox1.SelectedItem
        Dim kdItem As String = ""

        Call koneksiServer()
        Try
            Dim query As String
            query = "select * from t_bahanpa where bahanPA = '" & item & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                kdItem = UCase(dr.GetString("kdBahanPA"))
            End While

            If e.NewValue = CheckState.Checked Then
                ListBox1.Items.Add(kdItem)
                ListBox4.Items.Add(item)
            Else
                ListBox1.Items.Remove(kdItem)
                ListBox4.Items.Remove(item)
            End If

            If item = "Kerokan" Then
                Label33.Visible = True
                txtKetHaid.Visible = True
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
        conn.Close()
    End Sub

    Private Sub CheckedListBox2_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox2.ItemCheck
        Dim item As String = CheckedListBox2.SelectedItem
        Dim kdItem As String = ""

        Call koneksiServer()
        Try
            Dim query As String
            query = "select * from t_fiksasipa where fiksasi = '" & item & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                kdItem = UCase(dr.GetString("kdFiksasi"))
            End While

            If e.NewValue = CheckState.Checked Then
                ListBox2.Items.Add(kdItem)
                ListBox5.Items.Add(item)
            Else
                ListBox2.Items.Remove(kdItem)
                ListBox5.Items.Remove(item)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
        conn.Close()
    End Sub

    Private Sub CheckedListBox3_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox3.ItemCheck
        Dim item As String = CheckedListBox3.SelectedItem

        If e.NewValue = CheckState.Checked Then
            ListBox3.Items.Add(item)
        Else
            ListBox3.Items.Remove(item)
        End If
    End Sub


    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If stKawin = "" Then
            Me.ErStat.SetError(Me.FlowLayoutPanel4, "Mohon dipilih terlebih dahulu")
            MsgBox("Pilih status terlebih dahulu", MsgBoxStyle.Exclamation, "Warning")
        ElseIf txtLokalisasi.Text = "" Then
            Me.ErLokaliasasi.SetError(Me.txtLokalisasi, "Mohon diisi terlebih dahulu")
            MsgBox("Tuliskan Lokaslisasi terlebih dahulu", MsgBoxStyle.Exclamation, "Warning")
        ElseIf txtDiagnos.Text = "" Then
            Me.ErKet.SetError(Me.txtDiagnos, "Mohon diisi terlebih dahulu")
            MsgBox("Pilih Diagnosa Klinis terlebih dahulu", MsgBoxStyle.Exclamation, "Warning")
        ElseIf txtStadiumT.Text = "" Then
            Me.ErStdT.SetError(Me.txtStadiumT, "Mohon diisi terlebih dahulu")
            MsgBox("Tuliskan Stadium T terlebih dahulu", MsgBoxStyle.Exclamation, "Warning")
        ElseIf txtStadiumN.Text = "" Then
            Me.ErStdN.SetError(Me.txtStadiumN, "Mohon diisi terlebih dahulu")
            MsgBox("Tuliskan Stadium N terlebih dahulu", MsgBoxStyle.Exclamation, "Warning")
        ElseIf txtStadiumM.Text = "" Then
            Me.ErStdM.SetError(Me.txtStadiumM, "Mohon diisi terlebih dahulu")
            MsgBox("Tuliskan Stadium N terlebih dahulu", MsgBoxStyle.Exclamation, "Warning")
        ElseIf ListBox4.Items.Count = 0 Then
            Me.ErBahan.SetError(Me.CheckedListBox1, "Mohon dipilih terlebih dahulu")
            MsgBox("Pilih status terlebih dahulu", MsgBoxStyle.Exclamation, "Warning")
        ElseIf ListBox5.Items.Count = 0 Then
            Me.ErFiksasi.SetError(Me.CheckedListBox2, "Mohon dipilih terlebih dahulu")
            MsgBox("Pilih status terlebih dahulu", MsgBoxStyle.Exclamation, "Warning")
        Else
            ErStat.Clear()
            ErLokaliasasi.Clear()
            ErKet.Clear()
            ErStdT.Clear()
            ErStdN.Clear()
            ErStdM.Clear()
            ErBahan.Clear()
            ErFiksasi.Clear()

            Dim konfirmasi As MsgBoxResult
            konfirmasi = MsgBox("Apakah anda yakin permintaan sudah benar ?", vbQuestion + vbYesNo, "Konfirmasi")
            If konfirmasi = vbYes Then
                Call addRegistrasiPa()
                Call addTindakan()

                txtLokalisasi.Text = ""
                txtDiagnos.Text = ""
                txtStadiumM.Text = ""
                txtStadiumN.Text = ""
                txtStadiumT.Text = ""
                txtDokter.Text = "-"
                txtKetHaid.Text = ""
                Call clearCheckList()
            End If
        End If

        'Try
        '    Client = New TcpClient("192.168.200.93", 8080)        'IP tujuan
        '    Dim writer As New StreamWriter(Client.GetStream())
        '    writer.Write(txtIpAddress.Text)                       'IP pengirim
        '    writer.Flush()
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        MsgBox("Dimohon untuk konfirmasi pembatalan melalui petugas Lab. PA", MsgBoxStyle.Information, "Informasi")
    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick
        If e.RowIndex = -1 Then
            Return
        End If

        noRegPA = dgv.Rows(e.RowIndex).Cells(6).Value.ToString
        kdHasil = dgv.Rows(e.RowIndex).Cells(7).Value.ToString
        DokPA = dgv.Rows(e.RowIndex).Cells(8).Value.ToString

    End Sub

    Private Sub dgv_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex = -1 Then
            Return
        End If

        noRegPA = dgv.Rows(e.RowIndex).Cells(6).Value.ToString
        kdHasil = dgv.Rows(e.RowIndex).Cells(7).Value.ToString
        DokPA = dgv.Rows(e.RowIndex).Cells(8).Value.ToString

        'If kdHasil = "" Then
        '    MsgBox("Maaf, hasil pemeriksaan belum selesai ..", MsgBoxStyle.Information, "Informasi")
        'Else
        If e.ColumnIndex = 5 Then
            ViewHasilPA.Show()
        End If
        'End If
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
            If dgv.Rows(i).Cells(4).Value = "PERMINTAAN" Then
                dgv.Rows(i).Cells(4).Style.BackColor = Color.FromArgb(255, 155, 0)
                dgv.Rows(i).Cells(4).Style.ForeColor = Color.White
            ElseIf dgv.Rows(i).Cells(4).Value = "DALAM TINDAKAN" Then
                dgv.Rows(i).Cells(4).Style.BackColor = Color.FromArgb(0, 60, 155)
                dgv.Rows(i).Cells(4).Style.ForeColor = Color.White
            ElseIf dgv.Rows(i).Cells(4).Value = "SELESAI" Then
                dgv.Rows(i).Cells(4).Style.BackColor = Color.FromArgb(15, 180, 100)
                dgv.Rows(i).Cells(4).Style.ForeColor = Color.White
            End If
        Next

        For i = 0 To dgv.Rows.Count - 1
            dgv.Rows(i).Cells(5).Style.BackColor = Color.FromArgb(232, 243, 239)
            dgv.Rows(i).Cells(5).Style.ForeColor = Color.FromArgb(26, 141, 95)
        Next
    End Sub

    Private Sub btnHasilLab_Click(sender As Object, e As EventArgs) Handles btnHasilLab.Click

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

    Private Sub txtDokter_TextChanged(sender As Object, e As EventArgs) Handles txtDokter.TextChanged
        Call koneksiServer()
        Try
            Dim query As String
            query = "select * from t_tenagamedis2 where namapetugasMedis = '" & txtDokter.Text & "'"
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

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If TableLayoutPanel1.RowStyles(4).Height = 0 Then           'Buka Riwayat
            If TableLayoutPanel1.RowStyles(1).Height = 75 Then                  'Timeline ada dan buka riwayat
                Me.Height = 765
                TableLayoutPanel1.RowStyles(4).SizeType = SizeType.Absolute
                TableLayoutPanel1.RowStyles(4).Height = 155
            ElseIf TableLayoutPanel1.RowStyles(1).Height = 0 Then               'Timeline tidak ada dan buka riwayat
                Me.Height = 690
                TableLayoutPanel1.RowStyles(4).SizeType = SizeType.Absolute
                TableLayoutPanel1.RowStyles(4).Height = 155
            End If
        Else                                                        'Tutup Riwayat
            If TableLayoutPanel1.RowStyles(1).Height = 75 Then                  'Timeline ada dan tutup riwayat
                Me.Height = 610
                TableLayoutPanel1.RowStyles(4).SizeType = SizeType.Absolute
                TableLayoutPanel1.RowStyles(4).Height = 0
            ElseIf TableLayoutPanel1.RowStyles(1).Height = 0 Then               'Timeline tidak ada dan tutup riwayat
                Me.Height = 535
                TableLayoutPanel1.RowStyles(4).SizeType = SizeType.Absolute
                TableLayoutPanel1.RowStyles(4).Height = 0
            End If
        End If
    End Sub
End Class