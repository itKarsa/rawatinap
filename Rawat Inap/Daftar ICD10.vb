Imports MySql.Data.MySqlClient
Public Class Daftar_ICD10

    Public fco As Checkout
    Public fop As Operasi
    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Dim kdIcd10, icd10, ket As String

    Sub caridata()
        Dim query As String
        query = "SELECT * 
                   FROM vw_icd10 WHERE icd10 LIKE '%" & txtCari.Text & "%' 
                     OR kdIcd10 LIKE '%" & txtCari.Text & "%' 
                     OR keterangan LIKE '%" & txtCari.Text & "%'"
        da = New MySqlDataAdapter(query, conn)

        Dim str As New DataTable
        str.Clear()
        da.Fill(str)
        DataGridView1.DataSource = str
    End Sub

    Sub tampilData()
        Call koneksiServer()
        da = New MySqlDataAdapter("SELECT * FROM vw_icd10", conn)
        ds = New DataSet
        da.Fill(ds, "t_icd10")
        DataGridView1.DataSource = ds.Tables("t_icd10")
    End Sub

    Sub aturDGV()
        Try
            DataGridView1.Columns(0).Width = 70
            DataGridView1.Columns(1).Width = 350
            DataGridView1.Columns(2).Width = 1000
            DataGridView1.Columns(0).HeaderText = "KODE ICD-10"
            DataGridView1.Columns(1).HeaderText = "ICD-10"
            DataGridView1.Columns(2).HeaderText = "KETERANGAN"

            DataGridView1.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise
            DataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Daftar_ICD10_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtCari.Select()
        Call tampilData()
        Call aturDGV()
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

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick

        If e.RowIndex = -1 Then
            Return
        End If

        kdIcd10 = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        icd10 = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        ket = DataGridView1.Rows(e.RowIndex).Cells(2).Value

    End Sub

    Private Sub DataGridView1_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick

        If e.RowIndex = -1 Then
            Return
        End If

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "IsiDiagnosa"
                    Isi_Diagnosa.txtkdIcd.Text = kdIcd10
                    Isi_Diagnosa.txtIcd10.Text = icd10
                    Isi_Diagnosa.txtKet.Text = ket
                Case "ICD10Primer"
                    fco.txtPrimer.Text = kdIcd10
                    fco.txtIcdPrimer.Text = ket
                    fco.txtJenis0.Text = "UTAMA"
                Case "ICD10Sek1"
                    fco.txt1.Text = kdIcd10
                    fco.txtSek1.Text = ket
                Case "ICD10Sek2"
                    fco.txt2.Text = kdIcd10
                    fco.txtSek2.Text = ket
                Case "ICD10Sek3"
                    fco.txt3.Text = kdIcd10
                    fco.txtSek3.Text = ket
                Case "ICD10Sek4"
                    fco.txt4.Text = kdIcd10
                    fco.txtSek4.Text = ket
                    'Case "ICD10PrimerOP"
                    '    fop.txtPrimer.Text = kdIcd10
                    '    fop.txtIcdPrimer.Text = ket
                    'Case "ICD10Sek1OP"
                    '    fop.txt1.Text = kdIcd10
                    '    fop.txtSek1.Text = ket
                    'Case "ICD10Sek2OP"
                    '    fop.txt2.Text = kdIcd10
                    '    fop.txtSek2.Text = ket
                    'Case "ICD10Sek3OP"
                    '    fop.txt3.Text = kdIcd10
                    '    fop.txtSek3.Text = ket
                    'Case "ICD10Sek4OP"
                    '    fop.txt4.Text = kdIcd10
                    '    fop.txtSek4.Text = ket
            End Select
        End If
        Me.Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "IsiDiagnosa"
                    Isi_Diagnosa.txtkdIcd.Text = kdIcd10
                    Isi_Diagnosa.txtIcd10.Text = icd10
                    Isi_Diagnosa.txtKet.Text = ket
                Case "ICD10Primer"
                    fco.txtPrimer.Text = kdIcd10
                    fco.txtIcdPrimer.Text = ket
                    fco.txtJenis0.Text = "UTAMA"
                Case "ICD10Sek1"
                    fco.txt1.Text = kdIcd10
                    fco.txtSek1.Text = ket
                Case "ICD10Sek2"
                    fco.txt2.Text = kdIcd10
                    fco.txtSek2.Text = ket
                Case "ICD10Sek3"
                    fco.txt3.Text = kdIcd10
                    fco.txtSek3.Text = ket
                Case "ICD10Sek4"
                    fco.txt4.Text = kdIcd10
                    fco.txtSek4.Text = ket
                    'Case "ICD10PrimerOP"
                    '    fop.txtPrimer.Text = kdIcd10
                    '    fop.txtIcdPrimer.Text = ket
                    'Case "ICD10Sek1OP"
                    '    fop.txt1.Text = kdIcd10
                    '    fop.txtSek1.Text = ket
                    'Case "ICD10Sek2OP"
                    '    fop.txt2.Text = kdIcd10
                    '    fop.txtSek2.Text = ket
                    'Case "ICD10Sek3OP"
                    '    fop.txt3.Text = kdIcd10
                    '    fop.txtSek3.Text = ket
                    'Case "ICD10Sek4OP"
                    '    fop.txt4.Text = kdIcd10
                    '    fop.txtSek4.Text = ket
            End Select
            Me.Close()
        End If
    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.RowIndex > 0 And e.ColumnIndex = 1 Then
            If DataGridView1.Item(1, e.RowIndex - 1).Value = e.Value Then
                e.Value = ""
            ElseIf e.RowIndex < DataGridView1.Rows.Count - 1 Then
                DataGridView1.Rows(0).DefaultCellStyle.BackColor = Color.WhiteSmoke
                DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.WhiteSmoke
            Else
                DataGridView1.Rows(0).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        End If
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        Dim colkdIcd10, colicd10, colket As String
        If e.KeyCode = Keys.Enter And DataGridView1.CurrentCell.RowIndex >= 0 Then
            e.Handled = True
            e.SuppressKeyPress = True

            Dim row As DataGridViewRow
            row = Me.DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex)

            If DataGridView1.CurrentCell.RowIndex = -1 Then
                Return
            End If

            colkdIcd10 = row.Cells(0).Value
            colicd10 = row.Cells(1).Value
            colket = row.Cells(2).Value

            If Ambil_Data = True Then
                Select Case Form_Ambil_Data
                    Case "IsiDiagnosa"
                        Isi_Diagnosa.txtkdIcd.Text = colkdIcd10
                        Isi_Diagnosa.txtIcd10.Text = colicd10
                        Isi_Diagnosa.txtKet.Text = colket
                    Case "ICD10Primer"
                        fco.txtPrimer.Text = colkdIcd10
                        fco.txtIcdPrimer.Text = colket
                        fco.txtJenis0.Text = "UTAMA"
                    Case "ICD10Sek1"
                        fco.txt1.Text = colkdIcd10
                        fco.txtSek1.Text = colket
                    Case "ICD10Sek2"
                        fco.txt2.Text = colkdIcd10
                        fco.txtSek2.Text = colket
                    Case "ICD10Sek3"
                        fco.txt3.Text = colkdIcd10
                        fco.txtSek3.Text = colket
                    Case "ICD10Sek4"
                        fco.txt4.Text = colkdIcd10
                        fco.txtSek4.Text = colket
                        'Case "ICD10PrimerOP"
                        '    fop.txtPrimer.Text = colkdIcd10
                        '    fop.txtIcdPrimer.Text = colket
                        'Case "ICD10Sek1OP"
                        '    fop.txt1.Text = colkdIcd10
                        '    fop.txtSek1.Text = colket
                        'Case "ICD10Sek2OP"
                        '    fop.txt2.Text = colkdIcd10
                        '    fop.txtSek2.Text = colket
                        'Case "ICD10Sek3OP"
                        '    fop.txt3.Text = colkdIcd10
                        '    fop.txtSek3.Text = colket
                        'Case "ICD10Sek4OP"
                        '    fop.txt4.Text = colkdIcd10
                        '    fop.txtSek4.Text = colket
                End Select
            End If
            Me.Close()
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