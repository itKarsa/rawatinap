Imports System.ComponentModel
Imports MySql.Data.MySqlClient
Public Class Ruangan

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Dim no As String
    Dim ruang As String
    Dim kode As String

    Sub dgv_styleRow()
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Sub populate(unit As String, kode As String)

        'Dim row As String() = New String() {(ListView1.Items.Count + 1).ToString(), unit, kode}
        'Dim item As New ListViewItem(row)
        'ListView1.Items.Add(item)
    End Sub

    Sub tampilRuang1()
        Call koneksiServer()
        Dim sql As String = "SELECT UPPER(rawatInap) AS rawatInap, 
                                    UPPER(kdRawatInap) AS kdRawatInap 
                               FROM t_rawatinap ORDER BY  CAST(SUBSTR(kdRawatInap,3,2) AS UNSIGNED)"
        cmd = New MySqlCommand(sql, conn)

        Try
            da = New MySqlDataAdapter(cmd)
            da.Fill(dt)

            For Each row In dt.Rows
                populate(row(0), row(1))
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        conn.Close()
    End Sub

    Sub tampilRuang2()
        Call koneksiServer()
        da = New MySqlDataAdapter("SELECT DISTINCT loket_unit FROM t_aksesmenu WHERE instalasi IN('rawat inap')", conn)

        ds = New DataSet
        da.Fill(ds, "t_aksesmenu")
        DataGridView1.DataSource = ds.Tables("t_aksesmenu")

        DataGridView1.Columns(0).Width = 100
        DataGridView1.Columns(0).HeaderText = "Ruang"

        dgv_styleRow()

        conn.Close()
    End Sub

    Private Sub Ruangan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tampilRuang2()
    End Sub

    Private Sub btnOk_MouseLeave(sender As Object, e As EventArgs) Handles btnOk.MouseLeave
        Me.btnOk.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnOk.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnOk_MouseEnter(sender As Object, e As EventArgs) Handles btnOk.MouseEnter
        Me.btnOk.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnOk.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Select Case Form_Ambil_Data
            Case "Ruang"
                Form1.Label1.Text = ruang
            Case "GiziRuang"
                Gizi.txtRanap.Text = ruang
                'Gizi.txtKdRanap.Text = kode
                Gizi.Show()
                Form1.Hide()
        End Select
        Me.Close()
        Form1.btnCariReg.Enabled = True
        Form1.btnHasilLis.Enabled = True
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        If e.RowIndex = -1 Then
            Return
        End If

        ruang = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        'kode = DataGridView1.Rows(e.RowIndex).Cells(1).Value

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Ruang"
                    Form1.Label1.Text = ruang
                Case "GiziRuang"
                    Gizi.txtRanap.Text = ruang
                    'Gizi.txtKdRanap.Text = kode
            End Select
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

        If e.RowIndex = -1 Then
            Return
        End If

        ruang = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        'kode = DataGridView1.Rows(e.RowIndex).Cells(1).Value

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Ruang"
                    Form1.Label1.Text = ruang
                Case "GiziRuang"
                    Gizi.txtRanap.Text = ruang
                    'Gizi.txtKdRanap.Text = kode
            End Select
        End If
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick

        If e.RowIndex = -1 Then
            Return
        End If

        ruang = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        'kode = DataGridView1.Rows(e.RowIndex).Cells(1).Value

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Ruang"
                    Form1.Label1.Text = ruang
                Case "GiziRuang"
                    Gizi.txtRanap.Text = ruang
                    'Gizi.txtKdRanap.Text = kode
                    Gizi.Show()
                    Form1.Hide()
            End Select
        End If
        Me.Close()
        Form1.btnCariReg.Enabled = True
        Form1.btnHasilLis.Enabled = True
    End Sub

    Private Sub DataGridView1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
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