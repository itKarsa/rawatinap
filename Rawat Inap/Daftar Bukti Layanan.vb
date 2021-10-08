Imports MySql.Data.MySqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Daftar_Bukti_Layanan

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Dim noRI, noRM, noRegUnit, nama, unit, noTindakan, tindakan, tglTindakan, ruang As String

    Sub dgv3_styleRow()
        For i As Integer = 0 To DataGridView3.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView3.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Sub tampilData()
        Call koneksiServer()
        Dim query As String = ""
        query = "CALL riwayatlayanan('" & Form1.txtNoDaftar.Text & "','" & noRM & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "','" & Format(DateAdd(DateInterval.Day, 1, DateTimePicker2.Value), "yyyy-MM-dd") & "')"
        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        DataGridView1.Rows.Clear()

        Do While dr.Read
            DataGridView1.Rows.Add(dr.Item("noRegistrasiRawatJalan"), dr.Item("tglTindakan"), 
                                   dr.Item("unit"), dr.Item("noTindakanPasienRajal"))
        Loop
        dr.Close()
        conn.Close()
    End Sub

    Sub tampilDataDetail()
        Dim query As String = ""

        Select Case noRegUnit
            Case "P1"
                query = "CALL riwayatlayananrajaldetail('" & noTindakan & "')"
            Case "PI"
                query = "CALL riwayatlayananrajaldetail('" & noTindakan & "')"
            Case "IG"
                query = "CALL riwayatlayananrajaldetail('" & noTindakan & "')"
            Case "RJ"
                query = "CALL riwayatlayananrajaldetail('" & noTindakan & "')"
            Case "RI"
                query = "CALL riwayatlayananranapdetail('" & noTindakan & "')"
        End Select

        Call koneksiServer()
        cmd = New MySqlCommand(query, conn)
        dr = cmd.ExecuteReader
        DataGridView2.Rows.Clear()

        Do While dr.Read
            DataGridView2.Rows.Add(dr.Item("noRekamedis"), dr.Item("idTindakan"), dr.Item("noTindakan"),
                                   dr.Item("tglTindakan"), dr.Item("kdTarif"), dr.Item("tindakan"), dr.Item("tarif"),
                                   dr.Item("jumlahTindakan"), dr.Item("DPJP"), dr.Item("PPA"), dr.Item("totalTarif"),
                                   dr.Item("kdTenagaMedis"))
        Loop
        dr.Close()
        conn.Close()
    End Sub

    Sub tampilDataRekap()
        Call koneksiServer()
        da = New MySqlDataAdapter("CALL rekaptindakan('" & noRM & "','" & noRI & "')", conn)
        ds = New DataSet
        da.Fill(ds, "rekaptindakan")
        DataGridView3.DataSource = ds.Tables("rekaptindakan")
    End Sub

    Sub aturDGVRekap()
        Try
            DataGridView3.Columns(0).Width = 100
            DataGridView3.Columns(1).Width = 150
            DataGridView3.Columns(2).Width = 100
            DataGridView3.Columns(3).Width = 70
            DataGridView3.Columns(4).Width = 70
            DataGridView3.Columns(5).Width = 410
            DataGridView3.Columns(6).Width = 60
            DataGridView3.Columns(7).Width = 120
            DataGridView3.Columns(8).Width = 120
            DataGridView3.Columns(0).HeaderText = "No.RM"
            DataGridView3.Columns(1).HeaderText = "Nama"
            DataGridView3.Columns(2).HeaderText = "No.RI"
            DataGridView3.Columns(3).HeaderText = "Ruang"
            DataGridView3.Columns(4).HeaderText = "Kelas"
            DataGridView3.Columns(5).HeaderText = "Tindakan"
            DataGridView3.Columns(6).HeaderText = "Jumlah"
            DataGridView3.Columns(7).HeaderText = "Tarif"
            DataGridView3.Columns(8).HeaderText = "Total"

            DataGridView3.Columns(0).Visible = False
            DataGridView3.Columns(1).Visible = False
            DataGridView3.Columns(2).Visible = False
            DataGridView3.Columns(3).Visible = False
            DataGridView3.Columns(4).Visible = False

            'DataGridView3.Columns(5).DefaultCellStyle.WrapMode = DataGridViewTriState.True
            DataGridView3.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView3.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DataGridView3.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DataGridView3.Columns(7).DefaultCellStyle.Format = "N0"
            DataGridView3.Columns(8).DefaultCellStyle.Format = "N0"

            DataGridView3.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise
            DataGridView3.DefaultCellStyle.SelectionForeColor = Color.Black

            Call dgv3_styleRow()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Auto_Save(sql As String)
        Try
            Call koneksiServer()
            cmd = New MySqlCommand
            With cmd
                .Connection = conn
                .CommandText = sql
                .ExecuteNonQuery()
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Function numrows(sql)
        Try
            Call koneksiServer()
            cmd = New MySqlCommand
            da = New MySqlDataAdapter
            dt = New DataTable

            With cmd
                .Connection = conn
                .CommandText = sql
            End With

            da.SelectCommand = cmd
            da.Fill(dt)

            maxrow = dt.Rows.Count
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
            da.Dispose()
        End Try

        Return maxrow
    End Function

    Sub totalTarif()
        Dim totTarif As Long
        totTarif = 0
        For i As Integer = 0 To DataGridView2.Rows.Count - 1
            totTarif = totTarif + Val(DataGridView2.Rows(i).Cells(10).Value)
        Next
        txtTotalDetail.Text = "Rp " & totTarif.ToString("#,##0")
        txtTotalDetail2.Text = totTarif
        'txtTotTarif2.Text = totTarif
    End Sub

    Private Sub Daftar_Bukti_Layanan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.Manual
        With Screen.PrimaryScreen.WorkingArea
            Me.SetBounds(.Left, .Top, .Width, .Height)
        End With

        conn.Close()
        SplitContainer1.Panel1Collapsed = True

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Layanan"
                    noRI = Form1.txtRegRanap.Text
                    noRM = Form1.txtRekMed.Text
                    nama = Form1.txtNamaPasien.Text
                    DateTimePicker1.Value = Form1.dateDaftar.Value
                    ruang = Form1.Label1.Text
                Case "Riwayat Layanan"
                    noRI = Daftar_Pasien.txtNoDaftarRanap.Text
                    noRM = Daftar_Pasien.txtNoRM.Text
                    nama = Daftar_Pasien.txtNamaPx.Text
                    DateTimePicker1.Value = Daftar_Pasien.txtTglRanap.Text
                    ruang = Daftar_Pasien.txtRanap.Text
                    btnHapusTindakan.Visible = False
                    DataGridView2.ReadOnly = True
            End Select
        End If

        Label1.Text = "Riwayat Tindakan Layanan Pasien a.n " & nama
        Label6.Text = "Rekapitulasi Tindakan Perawatan - Ruang " & ruang

        'Call tampilData()
        Call tampilDataRekap()
        'Call aturDGV()
        Call aturDGVRekap()

        Dim jml As Long = 0
        For i As Integer = 0 To DataGridView3.Rows.Count - 1
            jml = jml + Val(DataGridView3.Rows(i).Cells(6).Value)
        Next
        txtJml.Text = jml

    End Sub

    Private Sub btnKeluar_MouseLeave(sender As Object, e As EventArgs) Handles btnKeluar.MouseLeave
        Me.btnKeluar.BackColor = Color.FromArgb(192, 0, 0)
    End Sub

    Private Sub btnKeluar_MouseEnter(sender As Object, e As EventArgs) Handles btnKeluar.MouseEnter
        Me.btnKeluar.BackColor = Color.Red
    End Sub

    Private Sub btnRekap_MouseLeave(sender As Object, e As EventArgs) Handles btnRekap.MouseLeave
        Me.btnRekap.BackColor = Color.SeaGreen
    End Sub

    Private Sub btnRekap_MouseEnter(sender As Object, e As EventArgs) Handles btnRekap.MouseEnter
        Me.btnRekap.BackColor = Color.MediumSeaGreen
    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Riwayat Layanan"
                    Me.Close()
                    Daftar_Pasien.Ambil_Data = True
                    Daftar_Pasien.Form_Ambil_Data = "Info Pasien"
                    Daftar_Pasien.Show()
                Case Else
                    Me.Close()
                    Form1.Show()
            End Select
        End If
    End Sub

    Private Sub btnRekap_Click(sender As Object, e As EventArgs) Handles btnRekap.Click
        If SplitContainer1.Panel1Collapsed = False Then
            SplitContainer1.Panel1Collapsed = True
        Else
            SplitContainer1.Panel1Collapsed = False
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim dt As Date

        If e.RowIndex = -1 Then
            Return
        End If

        noRegUnit = DataGridView1.Rows(e.RowIndex).Cells(0).Value.Substring(0, 2)
        dt = Convert.ToDateTime(DataGridView1.Rows(e.RowIndex).Cells(1).Value)
        tglTindakan = dt.ToString("yyyy-MM-dd HH:mm:ss")
        unit = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        noTindakan = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        Label3.Text = "Daftar Tindakan Pelayanan (DETAIL - " & tglTindakan & " | " & noTindakan & ")"
        txtUnit.Text = unit
        'MsgBox(noRM & " | " & tglTindakan & " | " & noTindakan)
        Call tampilDataDetail()
        Call totalTarif()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim dt As Date

        If e.RowIndex = -1 Then
            Return
        End If

        noRegUnit = DataGridView1.Rows(e.RowIndex).Cells(0).Value.Substring(0, 2)
        dt = Convert.ToDateTime(DataGridView1.Rows(e.RowIndex).Cells(1).Value)
        tglTindakan = dt.ToString("yyyy-MM-dd HH:mm:ss")
        unit = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        noTindakan = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        Label3.Text = "Daftar Tindakan Pelayanan (DETAIL - " & tglTindakan & " | " & noTindakan & ")"
        txtUnit.Text = unit
        'MsgBox(noRM & " | " & tglTindakan & " | " & noTindakan)
        Call tampilDataDetail()
        Call totalTarif()
    End Sub

    Private Sub DataGridView1_KeyUp(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyUp
        Dim crRowIndex As Integer = Me.DataGridView1.CurrentCell.RowIndex
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            Dim dt As Date
            noRegUnit = DataGridView1.Rows(crRowIndex).Cells(0).Value.Substring(0, 2)
            dt = Convert.ToDateTime(DataGridView1.Rows(crRowIndex).Cells(1).Value)
            tglTindakan = dt.ToString("yyyy-MM-dd HH:mm:ss")
            unit = DataGridView1.Rows(crRowIndex).Cells(2).Value
            noTindakan = DataGridView1.Rows(crRowIndex).Cells(3).Value
            Label3.Text = "Daftar Tindakan Pelayanan (DETAIL - " & tglTindakan & " | " & noTindakan & ")"
            txtUnit.Text = unit
            Call tampilDataDetail()
            'Call aturDGVDetail()
            Call totalTarif()
        End If
    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex = -1 Then
            Return
        End If

        txtID.Text = DataGridView2.Item(1, e.RowIndex).Value.ToString
        txtNoTindak.Text = DataGridView2.Item(2, e.RowIndex).Value.ToString
        txtSubtotal.Text = DataGridView2.Item(10, e.RowIndex).Value.ToString
        tindakan = DataGridView2.Item(5, e.RowIndex).Value.ToString
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        If e.RowIndex = -1 Then
            Return
        End If

        txtID.Text = DataGridView2.Item(1, e.RowIndex).Value.ToString
        txtNoTindak.Text = DataGridView2.Item(2, e.RowIndex).Value.ToString
        txtSubtotal.Text = DataGridView2.Item(10, e.RowIndex).Value.ToString
        tindakan = DataGridView2.Item(5, e.RowIndex).Value.ToString
    End Sub

    Sub updateJml(id As String, jml As String, subtotal As String)
        Call koneksiServer()
        Try
            Dim str As String
            str = "UPDATE t_detailtindakanpasienranap
                      SET jumlahTindakan = '" & jml & "',
                          totalTarif = '" & subtotal & "'
                    WHERE idTindakanPasienRanap = '" & id & "'"

            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            'MsgBox("Update jumlah berhasil", MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox("Update jumlah gagal.", MessageBoxIcon.Error, "Error Update Jumlah")
        End Try

        conn.Close()
    End Sub

    Sub updateTotal(noTindakan As String, total As String)
        Call koneksiServer()
        Try
            Dim str As String
            str = "UPDATE t_tindakanpasienranap
                      SET totalTarifTindakan = '" & total & "'
                    WHERE noTindakanPasienRanap = '" & noTindakan & "'"

            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            'MsgBox("Update total berhasil", MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox("Update total gagal.", MessageBoxIcon.Error, "Error Update Tindakan")
        End Try

        conn.Close()
    End Sub

    Sub updatePpa(id As String, kdppa As String)
        Call koneksiServer()
        Try
            Dim str As String
            str = "UPDATE t_detailtindakanpasienranap
                      SET kdTenagaMedis = '" & kdppa & "'
                    WHERE idTindakanPasienRanap = '" & id & "'"

            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            'MsgBox("Update jumlah berhasil", MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox("Update jumlah gagal.", MessageBoxIcon.Error, "Error Update Jumlah")
        End Try

        conn.Close()
    End Sub

    Private Sub DataGridView2_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellValueChanged
        Dim id As String = ""
        Dim noTindak As String = ""
        Dim jml As String = ""
        Dim subTotal As String = ""
        Dim dgvrow As New System.Windows.Forms.DataGridViewRow

        For Each dgvrow In Me.DataGridView2.SelectedRows
            id = dgvrow.Cells(1).Value
            noTindak = dgvrow.Cells(2).Value
            jml = dgvrow.Cells(7).Value

            If jml IsNot Nothing Then
                subTotal = Val(jml) * Val(dgvrow.Cells(6).Value)
                dgvrow.Cells(10).Value = Format(Integer.Parse(subTotal), "###,###")
                Call totalTarif()
                Call updateJml(id, jml, subTotal)
                Call updateTotal(noTindak, txtTotalDetail2.Text)
                'MsgBox(id & "|" & noTindak & "|" & jml & "|" & subTotal & "|" & txtTotalDetail2.Text & "|")
            ElseIf jml Is Nothing Then
                subTotal = Val(jml) * Val(dgvrow.Cells(6).Value)
                dgvrow.Cells(10).Value = Format(Integer.Parse(subTotal), "###,###")
                dgvrow.Cells(7).Value = 0
            End If

            If dgvrow.Cells(9).Value <> "" Then
                Dim perawat As String = dgvrow.Cells(9).Value.ToString
                Dim queryPpa As String
                Try
                    dr.Close()
                    conn.Close()
                    Call koneksiServer()
                    queryPpa = "SELECT * FROM t_tenagamedis2 WHERE namapetugasMedis = '" & perawat & "'"
                    cmd = New MySqlCommand(queryPpa, conn)
                    dr = cmd.ExecuteReader
                    While dr.Read
                        dgvrow.Cells(11).Value = dr.GetString("kdPetugasMedis")
                    End While
                    'dr.Close()
                    'conn.Close()
                Catch ex As Exception
                    'MessageBox.Show(ex.Message)
                End Try
            Else
                dgvrow.Cells(9).Value = "-"
            End If

            Call updatePpa(id, dgvrow.Cells(11).Value.ToString)
        Next
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
        total = Integer.Parse(txtTotalDetail2.Text) - Integer.Parse(txtSubtotal.Text)
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

    Private Sub btnTampil_Click(sender As Object, e As EventArgs) Handles btnTampil.Click
        Call tampilData()
        'Call aturDGV()
    End Sub

    Private Sub btnTampil_MouseEnter(sender As Object, e As EventArgs) Handles btnTampil.MouseEnter
        Me.btnTampil.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnTampil.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnTampil_MouseLeave(sender As Object, e As EventArgs) Handles btnTampil.MouseLeave
        Me.btnTampil.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnTampil.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnHapusTindakan_Click(sender As Object, e As EventArgs) Handles btnHapusTindakan.Click
        If txtUnit.Text = "Laboratorium" Then
            MsgBox("Maaf, fungsi hapus hanya untuk tindakan perawatan", MsgBoxStyle.Exclamation, "Warning")
        ElseIf txtUnit.Text = "Radiologi" Then
            MsgBox("Maaf, fungsi hapus hanya untuk tindakan perawatan", MsgBoxStyle.Exclamation, "Warning")
        ElseIf txtUnit.Text = "IGD" Then
            MsgBox("Maaf, fungsi hapus hanya untuk tindakan perawatan", MsgBoxStyle.Exclamation, "Warning")
        Else
            Dim konfirmasi As MsgBoxResult
            konfirmasi = MsgBox("Apakah anda yakin ingin menghapus tindakan '" & tindakan & "' ?", vbQuestion + vbYesNo, "Konfirmasi")
            If konfirmasi = vbYes Then
                Call deleteDetail(txtID.Text)
                Call updateAfterDelete(txtNoTindak.Text)

                Dim index As Integer
                index = DataGridView2.CurrentCell.RowIndex
                DataGridView2.Rows.RemoveAt(index)

                Call tampilData()
                'Call aturDGV()
                Call tampilDataDetail()
                Call tampilDataRekap()
                Call aturDGVRekap()

                Call totalTarif()
            End If
        End If
    End Sub

    Private Sub TextBox_keyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub DataGridView2_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DataGridView2.EditingControlShowing
        'If DataGridView2.CurrentCell.ColumnIndex = 7 Then
        '    AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
        'End If

        Select Case DataGridView2.CurrentCell.ColumnIndex
            Case Column8.Index
                AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
            Case Column10.Index
                'MsgBox("PPA")
                Dim autoText2 As TextBox = TryCast(e.Control, TextBox)
                'autoText2.AutoCompleteMode = AutoCompleteMode.Suggest
                'autoText2.AutoCompleteSource = AutoCompleteSource.CustomSource
                Dim dataPerawat As New AutoCompleteStringCollection()
                If Column9 IsNot Nothing Then
                    'MsgBox("Autocomplete PPA")
                    autoText2.AutoCompleteMode = AutoCompleteMode.Suggest
                    autoText2.AutoCompleteSource = AutoCompleteSource.CustomSource
                    addItemsPerawat(dataPerawat)
                    autoText2.AutoCompleteCustomSource = dataPerawat
                End If
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

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Private Sub DataGridView2_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView2.CellFormatting
        For i As Integer = 0 To DataGridView2.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            Dim ExcelApp As Excel.Application
            Dim ExcelWorkBook As Excel.Workbook
            Dim ExcelWorkSheet As Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim a As Integer
            Dim b As Integer

            ExcelApp = New Excel.Application
            ExcelWorkBook = ExcelApp.Workbooks.Add(misValue)
            ExcelWorkSheet = ExcelWorkBook.Sheets("sheet1")

            For a = 0 To DataGridView3.RowCount - 2
                For b = 0 To DataGridView3.ColumnCount - 1
                    For c As Integer = 1 To DataGridView3.Columns.Count
                        ExcelWorkSheet.Cells(1, c) = DataGridView3.Columns(c - 1).HeaderText
                        ExcelWorkSheet.Cells(a + 2, b + 1) = DataGridView3(b, a).Value.ToString()
                    Next
                Next
            Next

            ExcelWorkSheet.SaveAs("D:\NEW SIMRS\RekapTindakan\Pasien-" & Form1.txtNamaPasien.Text & ".xlsx")
            ExcelWorkBook.Close()
            ExcelApp.Quit()

            releaseObject(ExcelApp)
            releaseObject(ExcelWorkBook)
            releaseObject(ExcelWorkSheet)

            MsgBox("Hasil export tersimpan di D:\NEW SIMRS\RekapTindakan\, dengan nama Pasien-" & Form1.txtNamaPasien.Text & ".xlsx")
        Catch ex As Exception

        End Try

    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try

    End Sub
End Class