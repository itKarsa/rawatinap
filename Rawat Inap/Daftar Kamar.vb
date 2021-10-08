Imports MySql.Data.MySqlClient
Public Class Daftar_Kamar

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Sub tampilData()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        query = "SELECT * 
                   FROM vw_caritarifstatuskamar 
                  WHERE status = 'aktif'
                  ORDER BY rawatInap ASC, kelas ASC, noBed ASC"

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            DataGridView1.Rows.Clear()
            Do While dr.Read
                DataGridView1.Rows.Add(dr.Item("kdTarifKelasKmr"), dr.Item("kdRawatInap"), dr.Item("rawatInap"),
                                       dr.Item("kdKelas"), dr.Item("kelas"), dr.Item("noKamar"),
                                       dr.Item("noBed"), dr.Item("jenisGenderBed"), dr.Item("tarifKmr"),
                                       dr.Item("statusBed"))
            Loop
            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Sub aturDGV()
        Try
            DataGridView1.Columns(0).Width = 70
            DataGridView1.Columns(1).Width = 70
            DataGridView1.Columns(2).Width = 150
            DataGridView1.Columns(3).Width = 70
            DataGridView1.Columns(4).Width = 100
            DataGridView1.Columns(5).Width = 70
            DataGridView1.Columns(6).Width = 70
            DataGridView1.Columns(7).Width = 150
            DataGridView1.Columns(8).Width = 150
            DataGridView1.Columns(9).Width = 150
            DataGridView1.Columns(0).HeaderText = "KODE TARIF"
            DataGridView1.Columns(1).HeaderText = "KODE RANAP"
            DataGridView1.Columns(2).HeaderText = "RUANG"
            DataGridView1.Columns(3).HeaderText = "KODE KELAS"
            DataGridView1.Columns(4).HeaderText = "KELAS"
            DataGridView1.Columns(5).HeaderText = "No.KAMAR"
            DataGridView1.Columns(6).HeaderText = "No.BED"
            DataGridView1.Columns(7).HeaderText = "JENIS BED"
            DataGridView1.Columns(8).HeaderText = "TARIF"
            DataGridView1.Columns(9).HeaderText = "STATUS"

            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(3).Visible = False

        Catch ex As Exception

        End Try
    End Sub

    Sub caridata()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader

        query = "SELECT * 
                   FROM vw_caritarifstatuskamar 
                  WHERE rawatInap like '%" & txtCari.Text & "%' 
                    AND status = 'aktif'
                  ORDER BY rawatInap ASC, kelas ASC, noBed ASC"

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            DataGridView1.Rows.Clear()
            Do While dr.Read
                DataGridView1.Rows.Add(dr.Item("kdTarifKelasKmr"), dr.Item("kdRawatInap"), dr.Item("rawatInap"),
                                       dr.Item("kdKelas"), dr.Item("kelas"), dr.Item("noKamar"),
                                       dr.Item("noBed"), dr.Item("jenisGenderBed"), dr.Item("tarifKmr"),
                                       dr.Item("statusBed"))
            Loop
            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub cariPerRuang()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        query = "SELECT * 
                   FROM vw_caritarifstatuskamar 
                  WHERE rawatInap LIKE '%" & Daftar_Pasien.txtRanap.Text & "%'
                    AND kelas = '" & Daftar_Pasien.txtKelas.Text & "'
                    AND status = 'aktif'
                  ORDER BY rawatInap ASC, kelas ASC, noBed ASC"
        MsgBox(query)

        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            DataGridView1.Rows.Clear()
            Do While dr.Read
                DataGridView1.Rows.Add(dr.Item("kdTarifKelasKmr"), dr.Item("kdRawatInap"), dr.Item("rawatInap"),
                                       dr.Item("kdKelas"), dr.Item("kelas"), dr.Item("noKamar"),
                                       dr.Item("noBed"), dr.Item("jenisGenderBed"), dr.Item("tarifKmr"),
                                       dr.Item("statusBed"))
            Loop
            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
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
                      SET kdTarifKelasKmr = '" & txtKdTarifKls.Text & "',
                          rawatInap = '" & txtRanap.Text & "',
                          noBed = '" & txtNoBed.Text & "',
                          tglKeluarRawatInap = NULL
                    WHERE noDaftarRawatInap = '" & txtNoDaftarRanap.Text & "'"

            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            'MsgBox("Update data Registrasi Pemeriksaan Lab berhasil dilakukan", MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox("Update data Registrasi gagal dilakukan.", MessageBoxIcon.Error, "Error Registrasi Ranap")
        End Try

        conn.Close()
    End Sub

    Sub updateBatalStatusBed()
        Try
            Call koneksiServer()
            Dim str As String
            str = "UPDATE t_tarifkelaskamar SET kdStatusBed = 'st5' 
                                          WHERE kdTarifKelasKmr = '" & txtKdTarifKls.Text & "'"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub Daftar_Kamar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.Manual
        With Screen.PrimaryScreen.WorkingArea
            Me.SetBounds(.Left, .Top, .Width, .Height)
        End With

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Batal"
                    GroupBox1.Visible = False
                    txtNoReg.Text = Daftar_Pasien.txtNoReg.Text
                    txtNoDaftarRanap.Text = Daftar_Pasien.txtNoDaftarRanap.Text
                    Call cariPerRuang()
                    'Call aturDGV()
                Case Else
                    txtCari.Select()
                    Call tampilData()
                    'Call aturDGV()
            End Select
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        'If e.RowIndex > 0 And e.ColumnIndex = 2 Then
        '    If DataGridView1.Item(2, e.RowIndex - 1).Value = e.Value Then
        '        e.Value = ""
        '    ElseIf e.RowIndex < DataGridView1.Rows.Count - 1 Then
        '        DataGridView1.Rows(0).DefaultCellStyle.BackColor = Color.AliceBlue
        '        DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.AliceBlue
        '    End If
        'End If

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).Cells(9).Value = "DIPESAN PASIEN" Then
                DataGridView1.Rows(i).Cells(9).Style.BackColor = Color.Orange
                DataGridView1.Rows(i).Cells(9).Style.ForeColor = Color.White
            ElseIf DataGridView1.Rows(i).Cells(9).Value = "RUSAK" Then
                DataGridView1.Rows(i).Cells(9).Style.BackColor = Color.Gray
                DataGridView1.Rows(i).Cells(9).Style.ForeColor = Color.White
            ElseIf DataGridView1.Rows(i).Cells(9).Value = "DIPERBAIKI" Then
                DataGridView1.Rows(i).Cells(9).Style.BackColor = Color.Gray
                DataGridView1.Rows(i).Cells(9).Style.ForeColor = Color.White
            ElseIf DataGridView1.Rows(i).Cells(9).Value = "DIBERSIHKAN" Then
                DataGridView1.Rows(i).Cells(9).Style.BackColor = Color.Orange
                DataGridView1.Rows(i).Cells(9).Style.ForeColor = Color.White
            ElseIf DataGridView1.Rows(i).Cells(9).Value = "DITEMPATI PASIEN" Then
                DataGridView1.Rows(i).Cells(9).Style.BackColor = Color.FromArgb(192, 0, 0)
                DataGridView1.Rows(i).Cells(9).Style.ForeColor = Color.White
            ElseIf DataGridView1.Rows(i).Cells(9).Value = "KOSONG" Then
                DataGridView1.Rows(i).Cells(9).Style.BackColor = Color.Green
                DataGridView1.Rows(i).Cells(9).Style.ForeColor = Color.White
            End If
        Next
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Batal"
                    Dim konfirmasi As MsgBoxResult
                    konfirmasi = MsgBox("Apa yakin pasien dipindahkan ke Bed '" & txtNoBed.Text & "' ", vbQuestion + vbYesNo, "Konfirmasi")
                    If konfirmasi = vbYes Then
                        Call updateBatalCOReg()
                        Call updateBatalCheckout()
                        Call updateBatalStatusBed()
                        Daftar_Pasien.Close()
                        Me.Close()
                        Form1.Show()
                    End If
                Case Else
                    Me.Close()
            End Select
        End If
    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        Me.Close()
    End Sub

    Private Sub Daftar_Kamar_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F11
                Me.Close()
            Case Keys.F12
                Me.Close()
        End Select
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Dim kdTarif, kdRanap, ranap, kelas, noKamar, noBed, tarifKmr As String

        If e.RowIndex = -1 Then
            Return
        End If

        kdTarif = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        kdRanap = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        ranap = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        noKamar = DataGridView1.Rows(e.RowIndex).Cells(5).Value
        noBed = DataGridView1.Rows(e.RowIndex).Cells(6).Value
        tarifKmr = DataGridView1.Rows(e.RowIndex).Cells(8).Value
        kelas = DataGridView1.Rows(e.RowIndex).Cells(4).Value

        If DataGridView1.Rows(e.RowIndex).Cells(9).Value = "DIPESAN PASIEN" Then
            MsgBox("Maaf, Bed telah dipesan pasien", MsgBoxStyle.Information)
        ElseIf DataGridView1.Rows(e.RowIndex).Cells(9).Value = "RUSAK" Then
            MsgBox("Maaf, Bed tidak dapat digunakan", MsgBoxStyle.Information)
        ElseIf DataGridView1.Rows(e.RowIndex).Cells(9).Value = "DIPERBAIKI" Then
            MsgBox("Maaf, Bed masih dalam perbaikan", MsgBoxStyle.Information)
        ElseIf DataGridView1.Rows(e.RowIndex).Cells(9).Value = "DIBERSIHKAN" Then
            MsgBox("Bed masih dibersihkan", MsgBoxStyle.Information)
        ElseIf DataGridView1.Rows(e.RowIndex).Cells(9).Value = "DITEMPATI PASIEN" Then
            MsgBox("Maaf, Bed telah ditempati pasien", MsgBoxStyle.Information)
        ElseIf DataGridView1.Rows(e.RowIndex).Cells(9).Value = "KOSONG" Then
            If Ambil_Data = True Then
                Select Case Form_Ambil_Data
                    Case "Daftar Kamar"
                        Pindah_Kamar.txtPindahKdTarifKmr.Text = kdTarif
                        Pindah_Kamar.txtPindahKdRanap.Text = kdRanap
                        Pindah_Kamar.txtPindahRanap.Text = ranap
                        Pindah_Kamar.txtPindahKelas.Text = kelas
                        Pindah_Kamar.txtPindahNoKmr.Text = noKamar
                        Pindah_Kamar.txtPindahNoBed.Text = noBed
                        Pindah_Kamar.txtPindahTarif.Text = tarifKmr
                    Case "Batal"
                        txtKdTarifKls.Text = kdTarif
                        txtRanap.Text = ranap
                        txtKelas.Text = kelas
                        txtNoKamar.Text = noKamar
                        txtNoBed.Text = noBed
                        txtTarif.Text = tarifKmr
                End Select
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        Dim kdTarif, kdRanap, ranap, kelas, noKamar, noBed, tarifKmr As String

        If e.RowIndex = -1 Then
            Return
        End If

        kdTarif = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        kdRanap = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        ranap = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        noKamar = DataGridView1.Rows(e.RowIndex).Cells(5).Value
        noBed = DataGridView1.Rows(e.RowIndex).Cells(6).Value
        tarifKmr = DataGridView1.Rows(e.RowIndex).Cells(8).Value
        kelas = DataGridView1.Rows(e.RowIndex).Cells(4).Value

        If DataGridView1.Rows(e.RowIndex).Cells(9).Value = "DIPESAN PASIEN" Then
            MsgBox("Maaf, Bed telah dipesan pasien", MsgBoxStyle.Information)
        ElseIf DataGridView1.Rows(e.RowIndex).Cells(9).Value = "RUSAK" Then
            MsgBox("Maaf, Bed tidak dapat digunakan", MsgBoxStyle.Information)
        ElseIf DataGridView1.Rows(e.RowIndex).Cells(9).Value = "DIPERBAIKI" Then
            MsgBox("Maaf, Bed masih dalam perbaikan", MsgBoxStyle.Information)
        ElseIf DataGridView1.Rows(e.RowIndex).Cells(9).Value = "DIBERSIHKAN" Then
            MsgBox("Bed masih dibersihkan", MsgBoxStyle.Information)
        ElseIf DataGridView1.Rows(e.RowIndex).Cells(9).Value = "DITEMPATI PASIEN" Then
            MsgBox("Maaf, Bed telah ditempati pasien", MsgBoxStyle.Information)
        ElseIf DataGridView1.Rows(e.RowIndex).Cells(9).Value = "KOSONG" Then
            If Ambil_Data = True Then
                Select Case Form_Ambil_Data
                    Case "Daftar Kamar"
                        Pindah_Kamar.txtPindahKdTarifKmr.Text = kdTarif
                        Pindah_Kamar.txtPindahKdRanap.Text = kdRanap
                        Pindah_Kamar.txtPindahRanap.Text = ranap
                        Pindah_Kamar.txtPindahKelas.Text = kelas
                        Pindah_Kamar.txtPindahNoKmr.Text = noKamar
                        Pindah_Kamar.txtPindahNoBed.Text = noBed
                        Pindah_Kamar.txtPindahTarif.Text = FormatNumber(CDbl(tarifKmr), 0)
                        Me.Close()
                    Case "Batal"
                        txtKdTarifKls.Text = kdTarif
                        txtRanap.Text = ranap
                        txtKelas.Text = kelas
                        txtNoKamar.Text = noKamar
                        txtNoBed.Text = noBed
                        txtTarif.Text = tarifKmr

                        Dim konfirmasi As MsgBoxResult
                        konfirmasi = MsgBox("Apa yakin pasien dipindahkan ke Bed '" & txtNoBed.Text & "' ", vbQuestion + vbYesNo, "Konfirmasi")
                        If konfirmasi = vbYes Then
                            Call updateBatalCOReg()
                            Call updateBatalCheckout()
                            Call updateBatalStatusBed()
                            Daftar_Pasien.Close()
                            Me.Close()
                            Form1.Show()
                        End If
                End Select
            End If
        End If
    End Sub

    Private Sub txtCari_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCari.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtCari.Text = "" Then
                Call tampilData()
                'Call aturDGV()
            Else
                Call caridata()
                'Call aturDGV()
            End If
        End If
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        Dim kdTarif, kdRanap, ranap, kelas, noKamar, noBed, tarifKmr As String

        If e.KeyCode = Keys.Enter And DataGridView1.CurrentCell.RowIndex >= 0 Then
            e.Handled = True
            e.SuppressKeyPress = True

            Dim row As DataGridViewRow
            row = Me.DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex)

            If DataGridView1.CurrentCell.RowIndex = -1 Then
                Return
            End If

            kdTarif = row.Cells(0).Value
            kdRanap = row.Cells(1).Value
            ranap = row.Cells(2).Value
            noKamar = row.Cells(5).Value
            noBed = row.Cells(6).Value
            tarifKmr = row.Cells(8).Value
            kelas = row.Cells(4).Value

            If row.Cells(9).Value = "DIPESAN PASIEN" Then
                MsgBox("Maaf, Bed telah dipesan pasien", MsgBoxStyle.Information)
            ElseIf row.Cells(9).Value = "RUSAK" Then
                MsgBox("Maaf, Bed tidak dapat digunakan", MsgBoxStyle.Information)
            ElseIf row.Cells(9).Value = "DIPERBAIKI" Then
                MsgBox("Maaf, Bed masih dalam perbaikan", MsgBoxStyle.Information)
            ElseIf row.Cells(9).Value = "DIBERSIHKAN" Then
                MsgBox("Bed masih dibersihkan", MsgBoxStyle.Information)
            ElseIf row.Cells(9).Value = "DITEMPATI PASIEN" Then
                MsgBox("Maaf, Bed telah ditempati pasien", MsgBoxStyle.Information)
            ElseIf row.Cells(9).Value = "KOSONG" Then
                If Ambil_Data = True Then
                    Select Case Form_Ambil_Data
                        Case "Daftar Kamar"
                            Pindah_Kamar.txtPindahKdTarifKmr.Text = kdTarif
                            Pindah_Kamar.txtPindahKdRanap.Text = kdRanap
                            Pindah_Kamar.txtPindahRanap.Text = ranap
                            Pindah_Kamar.txtPindahKelas.Text = kelas
                            Pindah_Kamar.txtPindahNoKmr.Text = noKamar
                            Pindah_Kamar.txtPindahNoBed.Text = noBed
                            Pindah_Kamar.txtPindahTarif.Text = tarifKmr
                            Me.Close()
                        Case "Batal"
                            txtKdTarifKls.Text = kdTarif
                            txtRanap.Text = ranap
                            txtKelas.Text = kelas
                            txtNoKamar.Text = noKamar
                            txtNoBed.Text = noBed
                            txtTarif.Text = tarifKmr

                            Dim konfirmasi As MsgBoxResult
                            konfirmasi = MsgBox("Apa yakin pasien dipindahkan ke Bed '" & txtNoBed.Text & "' ", vbQuestion + vbYesNo, "Konfirmasi")
                            If konfirmasi = vbYes Then
                                Call updateBatalCOReg()
                                Call updateBatalCheckout()
                                Call updateBatalStatusBed()
                                Daftar_Pasien.Close()
                                Me.Close()
                                Form1.Show()
                            End If
                    End Select
                End If
            End If
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
End Class