Imports MySql.Data.MySqlClient
Imports Rawat_Inap.Koneksi
Public Class Daftar_Unit_Pelayanan

    Sub tampilData()
        Call koneksidb()
        da = New MySqlDataAdapter("SELECT * FROM vw_unitpelayanan", conn)
        ds = New DataSet
        da.Fill(ds, "vw_unitpelayanan")
        DataGridView1.DataSource = ds.Tables("vw_unitpelayanan")
    End Sub

    Sub aturDGV()
        Try
            DataGridView1.Columns(0).Width = 70
            DataGridView1.Columns(1).Width = 150
            DataGridView1.Columns(0).HeaderText = "KODE"
            DataGridView1.Columns(1).HeaderText = "UNIT PELAYANAN"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Daftar_Unit_Pelayanan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tampilData()
        Call aturDGV()
    End Sub
End Class