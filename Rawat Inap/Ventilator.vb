Imports System.ComponentModel
Imports MySql.Data.MySqlClient
Public Class Ventilator

    Dim timespan As TimeSpan
    Dim id, spek, alat, tglPakai, tglLepas, durasi As String

    Sub autoVenti()
        Call koneksiServer()

        Using cmd As New MySqlCommand("SELECT jenisVenti FROM t_ventilator WHERE spesifikasi = '" & txtSpek.Text & "'", conn)
            da = New MySqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)

            txtAlat.DataSource = dt
            txtAlat.DisplayMember = "jenisVenti"
            txtAlat.ValueMember = "jenisVenti"
            txtAlat.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            txtAlat.AutoCompleteSource = AutoCompleteSource.ListItems
        End Using
        conn.Close()
    End Sub

    Sub tampilDataRiwayat()
        Call koneksiServer()
        Dim query As String
        'Dim cmd As MySqlCommand
        'Dim dr As MySqlDataReader
        query = "SELECT id, spesifikasiKamar, alat, tglPakai, 
                        tglLepas, durasi, userModify, dateModify
                   FROM t_detailpenggunaanventilator
                  WHERE noDaftarRawatInap = '" & Form1.txtRegRanap.Text & "' 
                    AND delData = '0'
               ORDER BY tglPakai DESC"
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dgv.Rows.Clear()

            Do While dr.Read
                dgv.Rows.Add(dr.Item("id"), dr.Item("spesifikasiKamar"), dr.Item("alat"), dr.Item("tglPakai"),
                             dr.Item("tglLepas"), dr.Item("durasi"), dr.Item("userModify"), dr.Item("dateModify"))
            Loop
            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub addVenti()
        Call koneksiServer()
        Try
            Dim str As String
            Dim cmd As MySqlCommand
            str = "INSERT INTO t_detailpenggunaanventilator (noDaftarRawatInap,spesifikasiKamar,alat,
                                                             tglPakai,tglLepas,durasi,
                                                             userModify,dateModify) 
                                                     VALUES ('" & Form1.txtRegRanap.Text & "','" & txtSpek.Text & "','" & txtAlat.Text & "',
                                                             '" & Format(datePakai.Value, "yyyy-MM-dd HH:mm:ss") & "','" & Format(dateLepas.Value, "yyyy-MM-dd HH:mm:ss") & "','" & txtDurasi.Text & "',
                                                             '" & LoginForm.txtUsername.Text.ToUpper & "','" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "')"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Input data berhasil dilakukan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Sub updateVenti(id As String)
        Call koneksiServer()
        Try
            Dim str As String
            Dim cmd As MySqlCommand
            str = "UPDATE t_detailpenggunaanventilator 
                      SET spesifikasiKamar = '" & txtSpek.Text & "', 
                          alat = '" & txtAlat.Text & "', 
                          tglPakai = '" & Format(datePakai.Value, "yyyy-MM-dd HH:mm:ss") & "', 
                          tglLepas = '" & Format(dateLepas.Value, "yyyy-MM-dd HH:mm:ss") & "', 
                          durasi = '" & txtDurasi.Text & "', 
                          userModify = CONCAT(userModify,';" & LoginForm.txtUsername.Text.ToUpper & "'),
                          dateModify = CONCAT(dateModify,';" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "') 
                    WHERE id = '" & id & "'"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Update data berhasil dilakukan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Sub deleteVenti(id As String)
        Call koneksiServer()
        Try
            Dim str As String
            Dim cmd As MySqlCommand
            str = "UPDATE t_detailpenggunaanventilator 
                      SET delData = '1', 
                          userModify = CONCAT(userModify,';" & LoginForm.txtUsername.Text.ToUpper & "'),
                          dateModify = CONCAT(dateModify,';" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "') 
                    WHERE id = '" & id & "'"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Delete riwayat berhasil dilakukan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub Ventilator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        datePakai.Format = DateTimePickerFormat.Custom
        dateLepas.Format = DateTimePickerFormat.Custom
        datePakai.CustomFormat = "dddd, ddMMMMyyyy HH:mm:ss"
        dateLepas.CustomFormat = "dddd, ddMMMMyyyy HH:mm:ss"

        txtNama.Text = ""
        txtUnitRanap.Text = ""
        txtSpek.Text = ""
        txtAlat.Text = ""
        txtDurasi.Text = ""

        txtNama.Text = Form1.txtNamaPasien.Text
        txtUnitRanap.Text = Form1.txtUnitRanap.Text
        txtSpek.Text = Form1.txtSpek.Text
        txtAlat.Text = Form1.txtAlat.Text

        Call tampilDataRiwayat()
    End Sub

    Private Sub txtUnitRanap_TextChanged(sender As Object, e As EventArgs) Handles txtUnitRanap.TextChanged
        Call koneksiServer()
        Try
            Dim query As String
            Dim dr As MySqlDataReader
            Dim cmd As MySqlCommand
            query = "CALL cekVenti('" & txtUnitRanap.Text & "')"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()

            If dr.HasRows Then
                txtSpek.Text = dr.Item("venti").ToString
            End If

            dr.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub txtSpek_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtSpek.SelectedIndexChanged
        Call autoVenti()
    End Sub

    Private Sub datePakai_ValueChanged(sender As Object, e As EventArgs) Handles datePakai.ValueChanged
        timespan = (datePakai.Value - dateLepas.Value).Duration
        txtDurasi.Text = CInt(timespan.TotalHours)
    End Sub

    Private Sub dateLepas_ValueChanged(sender As Object, e As EventArgs) Handles dateLepas.ValueChanged
        timespan = (datePakai.Value - dateLepas.Value).Duration
        txtDurasi.Text = CInt(timespan.TotalHours)
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If TableLayoutPanel1.Width = 450 Then
            Me.Width = 930
            TableLayoutPanel1.Width = 915

            'Call riwayatHd()
        ElseIf TableLayoutPanel1.Width = 915 Then
            Me.Width = 460
            TableLayoutPanel1.Width = 450
        End If
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Select Case btnOk.Text
            Case "OK"
                Call addVenti()
                'MsgBox("Simpan")
            Case "Update"
                Call updateVenti(txtID.Text)
                'MsgBox("Update")
        End Select

        Call tampilDataRiwayat()
    End Sub

    Private Sub HapusRiwayatItem_Click(sender As Object, e As EventArgs) Handles HapusRiwayatItem.Click
        Dim konfirmasi As MsgBoxResult
        konfirmasi = MsgBox("Apakah anda yakin ingin menghapus riwayat ?", vbQuestion + vbYesNo, "Konfirmasi")
        If konfirmasi = vbYes Then
            Call deleteVenti(id)
        End If

        Call tampilDataRiwayat()
    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick
        If e.RowIndex = -1 Then
            Return
        End If

        id = dgv.Rows(e.RowIndex).Cells(0).Value.ToString
        spek = dgv.Rows(e.RowIndex).Cells(1).Value.ToString
        alat = dgv.Rows(e.RowIndex).Cells(2).Value.ToString
        tglPakai = dgv.Rows(e.RowIndex).Cells(3).Value.ToString
        tglLepas = dgv.Rows(e.RowIndex).Cells(4).Value.ToString
        durasi = dgv.Rows(e.RowIndex).Cells(5).Value.ToString

        btnOk.Text = "Update"

        txtID.Text = id
        txtSpek.Text = spek
        txtAlat.Text = alat
        datePakai.Text = tglPakai
        dateLepas.Text = tglLepas
    End Sub

    Private Sub dgv_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex = -1 Then
            Return
        End If

        id = dgv.Rows(e.RowIndex).Cells(0).Value.ToString
        spek = dgv.Rows(e.RowIndex).Cells(1).Value.ToString
        alat = dgv.Rows(e.RowIndex).Cells(2).Value.ToString
        tglPakai = dgv.Rows(e.RowIndex).Cells(3).Value.ToString
        tglLepas = dgv.Rows(e.RowIndex).Cells(4).Value.ToString
        durasi = dgv.Rows(e.RowIndex).Cells(5).Value.ToString

        btnOk.Text = "Update"

        txtID.Text = id
        txtSpek.Text = spek
        txtAlat.Text = alat
        datePakai.Text = tglPakai
        dateLepas.Text = tglLepas
    End Sub

    Private Sub dgv_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv.CellMouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            dgv.Rows(e.RowIndex).Selected = True
            ContextMenuStrip1.Show(dgv, e.Location)
            ContextMenuStrip1.Show(Cursor.Position)
        End If
    End Sub

    Private Sub dgv_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgv.CellFormatting
        For i As Integer = 0 To dgv.Rows.Count - 1
            If i Mod 2 = 0 Then
                dgv.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                dgv.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
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
    Private Sub Ventilator_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Call Form1.tampilVenti()
    End Sub

    Private Sub Ventilator_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Call Form1.tampilVenti()
    End Sub
End Class