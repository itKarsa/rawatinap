Imports MySql.Data.MySqlClient
Public Class KomponenDarah

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Dim no As String
    Dim darah As String
    Dim suhu As String

    Sub dgv_styleRow()
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.AliceBlue
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.White
            End If
        Next
    End Sub

    Sub tampilKomDarah()
        Call koneksiServer()
        da = New MySqlDataAdapter("SELECT
	                                UPPER( komponenDarah ) AS komponenDarah,
	                                UPPER( suhuSimpan ) AS suhuSimpan 
                                   FROM
	                                t_komponendarah", conn)

        ds = New DataSet
        da.Fill(ds, "t_komponendarah")
        DataGridView1.DataSource = ds.Tables("t_komponendarah")

        Dim row As DataGridViewRow
        Dim i As Integer = 0
        For Each row In DataGridView1.Rows
            DataGridView1.Rows(i).HeaderCell.Value = (1 + i).ToString
            i += 1
        Next

        DataGridView1.Columns(0).Width = 100
        DataGridView1.Columns(1).Width = 50
        DataGridView1.Columns(0).HeaderText = "Komponen Darah"
        DataGridView1.Columns(1).HeaderText = "Suhu Simpan"

        dgv_styleRow()

        conn.Close()
    End Sub

    Private Sub KomponenDarah_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tampilKomDarah()
    End Sub

    Private Sub btnOk_MouseLeave(sender As Object, e As EventArgs) Handles btnOk.MouseLeave
        Me.btnOk.BackColor = Color.Green
    End Sub

    Private Sub btnOk_MouseEnter(sender As Object, e As EventArgs) Handles btnOk.MouseEnter
        Me.btnOk.BackColor = Color.LimeGreen
    End Sub

    Private Sub btnCancel_MouseLeave(sender As Object, e As EventArgs) Handles btnCancel.MouseLeave
        Me.btnCancel.BackColor = Color.FromArgb(192, 0, 0)
    End Sub

    Private Sub btnCancel_MouseEnter(sender As Object, e As EventArgs) Handles btnCancel.MouseEnter
        Me.btnCancel.BackColor = Color.Red
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        TransfusiDarah.txtKomDarah.Text = darah
        TransfusiDarah.txtSuhuSimpan.Text = suhu
        TransfusiDarah.Show()

        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        darah = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        suhu = DataGridView1.Rows(e.RowIndex).Cells(1).Value

        TransfusiDarah.txtKomDarah.Text = darah
        TransfusiDarah.txtSuhuSimpan.Text = suhu
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        darah = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        suhu = DataGridView1.Rows(e.RowIndex).Cells(1).Value

        TransfusiDarah.txtKomDarah.Text = darah
        TransfusiDarah.txtSuhuSimpan.Text = suhu
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        darah = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        suhu = DataGridView1.Rows(e.RowIndex).Cells(1).Value

        TransfusiDarah.txtKomDarah.Text = darah
        TransfusiDarah.txtSuhuSimpan.Text = suhu
        TransfusiDarah.Show()

        Me.Close()
    End Sub
End Class