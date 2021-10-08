Imports MySql.Data.MySqlClient
Public Class Daftar_Tenaga_Medis

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Sub caridata()
        Dim query As String
        query = "select * from t_tenagamedis where namapetugasMedis like '%" & txtCari.Text & "%'"
        da = New MySqlDataAdapter(query, conn)

        Dim str As New DataTable
        str.Clear()
        da.Fill(str)
        DataGridView1.DataSource = str
    End Sub

    Sub tampilData()
        Call koneksidb()
        da = New MySqlDataAdapter("SELECT * FROM vw_datatenagamedis", conn)
        ds = New DataSet
        da.Fill(ds, "vw_datatenagamedis")
        DataGridView1.DataSource = ds.Tables("vw_datatenagamedis")
    End Sub

    Sub aturDGV()
        Try
            DataGridView1.Columns(0).Width = 70
            DataGridView1.Columns(1).Width = 250
            DataGridView1.Columns(2).Width = 70
            DataGridView1.Columns(3).Width = 100
            DataGridView1.Columns(0).HeaderText = "KODE PETUGAS MEDIS"
            DataGridView1.Columns(1).HeaderText = "NAMA PETUGAS MEDIS"
            DataGridView1.Columns(2).HeaderText = "KODE KELOMPOK TENAGA MEDIS"
            DataGridView1.Columns(3).HeaderText = "KELOMPOK TENAGA MEDIS"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Daftar_Tenaga_Medis_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tampilData()
        Call aturDGV()
    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub txtCari_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCari.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call caridata()
            Call aturDGV()
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim kdTenagaMed, NamaMed As String

        If DataGridView1.CurrentRow Is Nothing Then Exit Sub
        Dim i As Integer = DataGridView1.CurrentRow.Index
        kdTenagaMed = DataGridView1.Item(0, i).Value
        NamaMed = DataGridView1.Item(1, i).Value

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Diagnosa"
                    Isi_Diagnosa.txtKdMed.Text = kdTenagaMed
                    Isi_Diagnosa.comboMedis.Text = NamaMed
            End Select
        End If
        Me.Close()
    End Sub

    Private Sub DataGridView1_Click(sender As Object, e As EventArgs) Handles DataGridView1.Click
        Dim kdTenagaMed, NamaMed As String

        If DataGridView1.CurrentRow Is Nothing Then Exit Sub
        Dim i As Integer = DataGridView1.CurrentRow.Index
        kdTenagaMed = DataGridView1.Item(0, i).Value
        NamaMed = DataGridView1.Item(1, i).Value

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Diagnosa"
                    Isi_Diagnosa.txtKdMed.Text = kdTenagaMed
                    Isi_Diagnosa.comboMedis.Text = NamaMed
            End Select
        End If
    End Sub
End Class