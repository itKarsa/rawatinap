Imports MySql.Data.MySqlClient

Public Class Daftar_Tindakan_Perawatan

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Sub tampilData()
        Call koneksiServer()
        Dim query As String

        If txtRuang.Text.Contains("Anggrek (Unit Stroke)") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND kdKelompokTindakan IN (01,02,05,34,35,36,37,45,06,09,12,63) 
                        OR tindakan LIKE '%PLEBOTO%'"
        ElseIf txtRuang.Text.Contains("Dahlia") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND kdKelompokTindakan IN (01,02,05,34,35,36,37,45,03,08,63)
                        OR tindakan LIKE '%PLEBOTO%'"
        ElseIf txtRuang.Text.Contains("Matahari") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND kdKelompokTindakan IN (01,02,05,34,35,36,37,45,03,07,63)
                        OR tindakan LIKE '%PLEBOTO%'"
        ElseIf txtRuang.Text.Contains("Perinatologi") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I' 
                        AND kdKelompokTindakan IN (10,13,49,63)
                        OR tindakan LIKE '%PLEBOTO%'"
        ElseIf txtRuang.Text.Contains("Seruni") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND kdKelompokTindakan IN (01,02,05,34,35,36,37,45,10,49,63)
                        OR tindakan LIKE '%PLEBOTO%'"
        ElseIf txtRuang.Text.Contains("Teratai") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND kdKelompokTindakan IN (01,02,05,34,35,36,37,45,03,04,06,07,08,09,12,40,41,42,63)
                        OR tindakan LIKE '%PLEBOTO%'"
        ElseIf txtRuang.Text.Contains("ICU") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I' 
                        AND (kdKelompokTindakan IN (49,63) OR kdTarif IN (496310)) 
                        AND kdTarif NOT IN (490110)"
        ElseIf txtRuang.Text.Contains("HCU") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I' 
                        AND kdKelompokTindakan IN (48,63)
                        OR tindakan LIKE '%PLEBOTO%'"
        ElseIf txtRuang.Text.Contains("IGD") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND kdKelompokTindakan IN (01,02,05,34,35,36,37,45,66,63)
                        OR tindakan LIKE '%PLEBOTO%'"
        Else
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND kdKelompokTindakan IN (01,02,05,34,35,36,37,45,63)
                        OR tindakan LIKE '%PLEBOTO%'"
        End If

        da = New MySqlDataAdapter(query, conn)
        ds = New DataSet
        da.Fill(ds, "vw_caritindakan")
        DataGridView1.DataSource = ds.Tables("vw_caritindakan")
    End Sub

    Sub caridata()
        Dim query As String
        If txtRuang.Text.Contains("Anggrek (Unit Stroke)") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "'
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND kdKelompokTindakan in (01,02,05,34,35,36,37,45,06,09,12,63) 
                        OR tindakan LIKE '%PLEBOTO%'"
        ElseIf txtRuang.Text.Contains("Dahlia") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND kdKelompokTindakan in (01,02,05,34,35,36,37,45,03,08,63)
                        OR tindakan LIKE '%PLEBOTO%'"
        ElseIf txtRuang.Text.Contains("Matahari") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND kdKelompokTindakan in (01,02,05,34,35,36,37,45,03,07,63)
                        OR tindakan LIKE '%PLEBOTO%'"
        ElseIf txtRuang.Text.Contains("Perinatologi") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND kdKelompokTindakan in (10,13,49,63)
                        OR tindakan LIKE '%PLEBOTO%'"
        ElseIf txtRuang.Text.Contains("Seruni") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND kdKelompokTindakan in (01,02,05,34,35,36,37,45,10,49,63)
                        OR tindakan LIKE '%PLEBOTO%'"
        ElseIf txtRuang.Text.Contains("Teratai") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND kdKelompokTindakan in (01,02,05,34,35,36,37,45,03,04,06,07,08,09,12,40,41,42,63)
                        OR tindakan LIKE '%PLEBOTO%'"
        ElseIf txtRuang.Text.Contains("ICU") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan IN (49,63) OR kdTarif IN (496310)) 
                        AND kdTarif NOT IN (490110)"
        ElseIf txtRuang.Text.Contains("HCU") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I'  
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND kdKelompokTindakan in (48,63)
                        OR tindakan LIKE '%PLEBOTO%'"
        ElseIf txtRuang.Text.Contains("IGD") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND kdKelompokTindakan in (01,02,05,34,35,36,37,45,66,63)
                        OR tindakan LIKE '%PLEBOTO%'"
        Else
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND kdKelompokTindakan in (01,02,05,34,35,36,37,45,63)
                        OR tindakan LIKE '%PLEBOTO%'"
        End If

        da = New MySqlDataAdapter(query, conn)

        Dim str As New DataTable
        str.Clear()
        da.Fill(str)
        DataGridView1.DataSource = str
    End Sub

    Sub transferSelected()
        Call koneksiServer()
        Dim dt As New DataTable
        Dim dr As New System.Windows.Forms.DataGridViewRow

        Dim R As DataRow = dt.NewRow

        Dim count As Integer
        count = FormTindakan.dgvDetail.Rows.Count

        For Each dr In Me.DataGridView1.SelectedRows
            FormTindakan.dgvDetail.Rows.Add(1)
            FormTindakan.dgvDetail.Rows(FormTindakan.dgvDetail.RowCount - 1).Cells(0).Value = count + 1
            FormTindakan.dgvDetail.Rows(FormTindakan.dgvDetail.RowCount - 1).Cells(1).Value = FormTindakan.txtNoTindak.Text
            FormTindakan.dgvDetail.Rows(FormTindakan.dgvDetail.RowCount - 1).Cells(2).Value = dr.Cells(0).Value.ToString
            FormTindakan.dgvDetail.Rows(FormTindakan.dgvDetail.RowCount - 1).Cells(3).Value = dr.Cells(3).Value.ToString
            FormTindakan.dgvDetail.Rows(FormTindakan.dgvDetail.RowCount - 1).Cells(4).Value = dr.Cells(4).Value.ToString
            FormTindakan.dgvDetail.Rows(FormTindakan.dgvDetail.RowCount - 1).Cells(5).Value = 1
            FormTindakan.dgvDetail.Rows(FormTindakan.dgvDetail.RowCount - 1).Cells(6).Value = ""
            FormTindakan.dgvDetail.Rows(FormTindakan.dgvDetail.RowCount - 1).Cells(7).Value = ""
            FormTindakan.dgvDetail.Rows(FormTindakan.dgvDetail.RowCount - 1).Cells(8).Value = ""
            FormTindakan.dgvDetail.Rows(FormTindakan.dgvDetail.RowCount - 1).Cells(9).Value = "-"
            FormTindakan.dgvDetail.Rows(FormTindakan.dgvDetail.RowCount - 1).Cells(10).Value = FormTindakan.dateTindakan.Text
            FormTindakan.dgvDetail.Rows(FormTindakan.dgvDetail.RowCount - 1).Cells(11).Value = Val(dr.Cells(4).Value * 1).ToString
            FormTindakan.dgvDetail.Update()
        Next

        conn.Close()
    End Sub

    Sub aturDGV()
        Try
            DataGridView1.Columns(0).Width = 70
            DataGridView1.Columns(1).Width = 70
            DataGridView1.Columns(2).Width = 200
            DataGridView1.Columns(3).Width = 550
            DataGridView1.Columns(4).Width = 100
            DataGridView1.Columns(5).Width = 50
            DataGridView1.Columns(0).HeaderText = "KODE"
            DataGridView1.Columns(1).HeaderText = "KELAS"
            DataGridView1.Columns(2).HeaderText = "KELOMPOK TINDAKAN"
            DataGridView1.Columns(3).HeaderText = "TINDAKAN"
            DataGridView1.Columns(4).HeaderText = "TARIF"
            DataGridView1.Columns(5).HeaderText = "KODE"

            DataGridView1.Columns(2).Visible = False
            DataGridView1.Columns(5).Visible = False

            DataGridView1.Columns(4).DefaultCellStyle.Format = "###,###,###"
            DataGridView1.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Daftar_Tindakan_Perawatan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtCari.Select()

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Daftar Tindakan"
                    txtKelas.Text = FormTindakan.txtKelas.Text
                    txtRuang.Text = FormTindakan.txtRuang.Text
                    txtDokter.Text = FormTindakan.txtDokter.Text
            End Select
        End If

        Call tampilData()
        Call aturDGV()

        txtJmlTindakan.Text = DataGridView1.Rows.Count
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Daftar Tindakan"
                    Call transferSelected()
            End Select
        End If
        Me.Close()
    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter And DataGridView1.CurrentCell.RowIndex >= 0 Then
            e.Handled = True
            e.SuppressKeyPress = True

            Dim row As DataGridViewRow
            row = Me.DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex)

            If DataGridView1.CurrentCell.RowIndex = -1 Then
                Return
            End If

            If Ambil_Data = True Then
                Select Case Form_Ambil_Data
                    Case "Daftar Tindakan"
                        Call transferSelected()
                End Select
            End If

            Me.Close()

        End If

    End Sub

    Private Sub txtCari_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCari.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtCari.Text = "" Then
                Call tampilData()
                Call aturDGV()
            Else
                Call caridata()
                Call aturDGV()
            End If
        End If
    End Sub
End Class