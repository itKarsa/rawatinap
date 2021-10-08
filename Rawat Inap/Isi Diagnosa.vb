Imports System.ComponentModel
Imports MySql.Data.MySqlClient
Public Class Isi_Diagnosa

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Sub clearText()
        txtNoDiagnosa.Text = ""
        txtRM.Text = ""
        txtKdMed.Text = ""
        txtIcd10.Text = ""
        txtJDiagnosa.Text = ""
        txtJKasus.Text = ""
        ComboBox1.SelectedValue = -1
        ComboBox2.SelectedValue = -1
        DataGridView1.Rows.Clear()
    End Sub

    Sub autoComboMedis()
        Call koneksiServer()
        cmd = New MySqlCommand("SELECT namapetugasMedis FROM t_tenagamedis2 WHERE kdKelompokTenagaMedis IN ('ktm1')  ORDER BY namapetugasMedis ASC", conn)
        da = New MySqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        comboMedis.DataSource = dt
        comboMedis.DisplayMember = "namapetugasMedis"
        comboMedis.ValueMember = "namapetugasMedis"
        comboMedis.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        comboMedis.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Sub autoComboJenisDiag()
        Call koneksiServer()
        cmd = New MySqlCommand("SELECT UPPER(jenisDiagnosa) AS JENIS FROM t_jenisdiagnosa ORDER BY kdJenisDiagnosa ASC", conn)
        da = New MySqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        ComboBox1.DataSource = dt
        ComboBox1.DisplayMember = "JENIS"
        ComboBox1.ValueMember = "JENIS"
        ComboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        ComboBox1.AutoCompleteSource = AutoCompleteSource.ListItems
        ComboBox1.SelectedValue = -1
    End Sub

    Sub autoComboJenisKasus()
        Call koneksiServer()
        cmd = New MySqlCommand("SELECT UPPER(kasus) AS KASUS FROM t_kasusdiagnosa ORDER BY kdKasusDiagnosa ASC", conn)
        da = New MySqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        ComboBox2.DataSource = dt
        ComboBox2.DisplayMember = "KASUS"
        ComboBox2.ValueMember = "KASUS"
        ComboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        ComboBox2.AutoCompleteSource = AutoCompleteSource.ListItems
        ComboBox2.SelectedValue = -1
    End Sub

    Sub bersih()
        txtNoDiagnosa.Text = ""
        txtkdIcd.Text = ""
        txtIcd10.Text = ""
        txtKet.Text = ""
        txtJDiagnosa.Text = ""
        ComboBox1.Text = ""
        txtJKasus.Text = ""
        ComboBox2.Text = ""
    End Sub

    Sub autoNoDiagnosa()
        Dim str, tgl, bln, thn, jam, mnt, dtk As String
        str = "DG"
        tgl = DateTime.Now.ToString("dd")
        bln = DateTime.Now.ToString("MM")
        thn = DateTime.Now.ToString("yy")
        jam = DateTime.Now.ToString("HH")
        mnt = DateTime.Now.ToString("mm")
        dtk = DateTime.Now.ToString("ss")

        txtNoDiagnosa.Text = str + tgl + bln + thn + jam + mnt + dtk

    End Sub

    Sub autoNoDaftar()

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Diagnosa"
                    comboMedis.Text = Form1.txtDokter.Text
                    txtNoReg.Text = Form1.txtNoDaftar.Text
            End Select
        End If
    End Sub

    Sub addDiagnosa()
        Call koneksiServer()
        Dim val10, val1, val2, val3, val4, val5, val6, val7 As String

        Dim query As String
        query = "INSERT INTO t_diagnosaicd10 (noDaftar,noRekamedis,kdIcd10,icd10,
                                              kdJenisDiagnosa,kdJenisKasus,kdTenagaMedis,tglDiagnosa) 
                                      VALUES (@noDaftar,@noRekamedis,@kdIcd10,@icd10,
                                              @kdJenisDiagnosa,@kdJenisKasus,@kdTenagaMedis,@tglDiagnosa)"
        cmd = New MySqlCommand(query, conn)

        Try
            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                'val0 = DataGridView1.Rows(i).Cells(0).Value
                val1 = DataGridView1.Rows(i).Cells(1).Value
                val2 = DataGridView1.Rows(i).Cells(2).Value
                val3 = DataGridView1.Rows(i).Cells(3).Value
                val4 = DataGridView1.Rows(i).Cells(4).Value
                val5 = DataGridView1.Rows(i).Cells(5).Value
                val6 = DataGridView1.Rows(i).Cells(6).Value
                val7 = DataGridView1.Rows(i).Cells(7).Value
                val10 = DataGridView1.Rows(i).Cells(10).Value

                'cmd.Parameters.AddWithValue("@noDiagnosaIcd10", val0)
                cmd.Parameters.AddWithValue("@noDaftar", val1)
                cmd.Parameters.AddWithValue("@noRekamedis", val2)
                cmd.Parameters.AddWithValue("@kdIcd10", val3)
                cmd.Parameters.AddWithValue("@icd10", val4)
                cmd.Parameters.AddWithValue("@kdJenisDiagnosa", val5)
                cmd.Parameters.AddWithValue("@kdJenisKasus", val6)
                cmd.Parameters.AddWithValue("@kdTenagaMedis", val7)
                cmd.Parameters.AddWithValue("@tglDiagnosa", val10)
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
            Next
            MsgBox("Insert data Diagnosa berhasil dilakukan", MsgBoxStyle.Information, "Informasi")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "Informasi")
            cmd.Dispose()
        End Try

    End Sub

    Private Sub Isi_Diagnosa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call clearText()
        'Call autoNoDiagnosa()
        Call autoComboMedis()
        Call autoComboJenisDiag()
        Call autoComboJenisKasus()

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Diagnosa"
                    comboMedis.Text = Form1.comboDokter.Text
                    txtRM.Text = Form1.txtRekMed.Text
                    txtNoReg.Text = Form1.txtNoDaftar.Text
                    txtTglMrs.Value = Form1.txtTglMasuk.Text
            End Select
        End If

    End Sub

    Private Sub btnCariMed_Click(sender As Object, e As EventArgs)
        Daftar_Tenaga_Medis.Ambil_Data = True
        Daftar_Tenaga_Medis.Form_Ambil_Data = "Diagnosa"
        Daftar_Tenaga_Medis.Show()
    End Sub

    Private Sub btnCariIcd10_Click(sender As Object, e As EventArgs)
        Dim daftarIcd10 = New Daftar_ICD10()
        daftarIcd10.Show()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Call koneksiServer()
        Try
            Dim query As String
            query = "SELECT * FROM t_jenisdiagnosa WHERE jenisDiagnosa = '" & ComboBox1.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                txtJDiagnosa.Text = UCase(dr.GetString("kdJenisDiagnosa"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Call koneksiServer()
        Try
            Dim query As String
            query = "SELECT * FROM t_kasusdiagnosa WHERE kasus = '" & ComboBox2.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                txtJKasus.Text = UCase(dr.GetString("kdKasusDiagnosa"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click

        If txtIcd10.Text = "" Then
            Me.ErrorIcd.SetError(Me.txtIcd10, "Pilih ICD-10 terlebih dahulu")
            MsgBox("Mohon lengkapi data yang masih kosong", MsgBoxStyle.Exclamation, "Warning")
        ElseIf ComboBox1.Text = "" Then
            Me.ErrorDiag.SetError(Me.ComboBox1, "Pilih Jenis Diagnosa terlebih dahulu")
            MsgBox("Mohon lengkapi data yang masih kosong", MsgBoxStyle.Exclamation, "Warning")
        ElseIf ComboBox2.Text = "" Then
            Me.ErrorKasus.SetError(Me.ComboBox2, "Pilih Jenis Kasus terlebih dahulu")
            MsgBox("Mohon lengkapi data yang masih kosong", MsgBoxStyle.Exclamation, "Warning")
        Else
            ErrorIcd.Clear()
            ErrorDiag.Clear()
            ErrorKasus.Clear()

            DataGridView1.Rows.Add(1)
            DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(0).Value = txtNoDiagnosa.Text
            DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(1).Value = txtNoReg.Text
            DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(2).Value = txtRM.Text
            DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(3).Value = txtkdIcd.Text
            DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(4).Value = txtIcd10.Text
            DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(5).Value = txtJDiagnosa.Text
            DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(6).Value = txtJKasus.Text
            DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(7).Value = txtKdMed.Text
            DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(8).Value = ComboBox1.Text
            DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(9).Value = comboMedis.Text
            DataGridView1.Rows(DataGridView1.RowCount - 1).Cells(10).Value = Format(txtTglMrs.Value, "yyyy-MM-dd")
            DataGridView1.Update()

            Call bersih()
            'Call autoNoDiagnosa()
            Call autoNoDaftar()
        End If

    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If DataGridView1.Rows.Count = 0 Then
            MsgBox("Masukkan diagnosa terlebih dahulu !!", MsgBoxStyle.Exclamation)
        Else
            Call addDiagnosa()
            Call Form1.tampilDataDiagnosa()
            Me.Close()
        End If
    End Sub

    Private Sub comboMedis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboMedis.SelectedIndexChanged
        Call koneksiServer()
        Try
            Dim query As String
            query = "SELECT * FROM t_tenagamedis2 WHERE namapetugasMedis = '" & comboMedis.Text & "'"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader

            While dr.Read
                txtKdMed.Text = UCase(dr.GetString("kdPetugasMedis"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnIcd10_Click(sender As Object, e As EventArgs) Handles btnIcd10.Click
        Daftar_ICD10.Ambil_Data = True
        Daftar_ICD10.Form_Ambil_Data = "IsiDiagnosa"
        Daftar_ICD10.Show()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        If DataGridView1.Columns(e.ColumnIndex).Name.ToString = "Column10" Then
            For Each row As DataGridViewRow In DataGridView1.SelectedRows
                DataGridView1.Rows.Remove(row)
            Next
        End If
    End Sub

    Private Sub comboMedis_KeyDown(sender As Object, e As KeyEventArgs) Handles comboMedis.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If comboMedis.Text = "" Then
                comboMedis.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub comboMedis_TextChanged(sender As Object, e As EventArgs) Handles comboMedis.TextChanged
        If comboMedis.Text <> "" Then
            comboMedis.BackColor = Color.White
        End If
    End Sub

    Private Sub comboMedis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles comboMedis.KeyPress
        If System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "[^a-zA-Z()\b., ]") Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnIcd10_KeyDown(sender As Object, e As KeyEventArgs) Handles btnIcd10.KeyDown
        If e.KeyCode = Keys.Enter Then

            Daftar_ICD10.Ambil_Data = True
            Daftar_ICD10.Form_Ambil_Data = "IsiDiagnosa"
            Daftar_ICD10.Show()

            SendKeys.Send("{TAB}")
            If ComboBox1.Text = "" Then
                ComboBox1.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub ComboBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If ComboBox1.Text = "" Then
                ComboBox1.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub


    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "[^a-zA-Z()\b., ]") Then
            e.Handled = True
        End If
    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        If ComboBox1.Text <> "" Then
            ComboBox1.BackColor = Color.White
        End If
    End Sub

    Private Sub ComboBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            If ComboBox2.Text = "" Then
                ComboBox2.BackColor = Color.FromArgb(255, 112, 112)
            End If
        End If
    End Sub

    Private Sub ComboBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox2.KeyPress
        If System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "[^a-zA-Z()\b., ]") Then
            e.Handled = True
        End If
    End Sub

    Private Sub ComboBox2_TextChanged(sender As Object, e As EventArgs) Handles ComboBox2.TextChanged
        If ComboBox2.Text <> "" Then
            ComboBox2.BackColor = Color.White
        End If
    End Sub

    Private Sub txtIcd10_TextChanged(sender As Object, e As EventArgs) Handles txtIcd10.TextChanged
        SendKeys.Send("{TAB}")
    End Sub

    Private Sub btnSimpan_DoubleClick(sender As Object, e As EventArgs) Handles btnSimpan.DoubleClick
        Return
    End Sub

    Private Sub DataGridView1_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DataGridView1.RowsAdded
        If DataGridView1.Rows.Count <> 0 Then
            Me.btnSimpan.Enabled = True
        End If
    End Sub

    Private Sub DataGridView1_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles DataGridView1.RowsRemoved
        If DataGridView1.Rows.Count = 0 Then
            Me.btnSimpan.Enabled = False
        End If
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

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Private Sub btnTambah_MouseLeave(sender As Object, e As EventArgs) Handles btnTambah.MouseLeave
        Me.btnTambah.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnTambah.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnTambah_MouseEnter(sender As Object, e As EventArgs) Handles btnTambah.MouseEnter
        Me.btnTambah.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnTambah.ForeColor = Color.White
    End Sub
End Class