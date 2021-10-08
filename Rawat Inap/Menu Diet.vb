Imports MySql.Data.MySqlClient
Public Class Menu_Diet

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Sub dgv_styleRow()
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.AliceBlue
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.White
            End If
        Next

        For i As Integer = 0 To DataGridView2.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.AliceBlue
            Else
                DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.White
                DataGridView2.Rows(i).DefaultCellStyle.ForeColor = Color.Black
            End If
        Next

    End Sub

    Sub autoMenuDiet()
        Call koneksiGizi()
        cmd = New MySqlCommand("SELECT kddiet, nmdiet, keterangandiet FROM t_diet WHERE NOT lHEADER = 1 AND KDDIET > 'A06' AND KDDIET <'D12'", conn)
        da = New MySqlDataAdapter(cmd)
        ds = New DataSet
        da.Fill(ds, "t_diet")
        DataGridView1.DataSource = ds.Tables("t_diet")

        'da = New MySqlDataAdapter("SELECT kdPermintaanmenu, kdPermintaan, 
        '                                  KDDIET, KETERANGANDIET
        '                             FROM t_permintaanmenu", conn)
        'ds = New DataSet
        'da.Fill(ds, "t_permintaanmenu")
        'DataGridView2.DataSource = ds.Tables("t_permintaanmenu")

        conn.Close()
    End Sub

    Sub aturDGVTindakan()
        Try
            DataGridView1.Columns(0).Width = 70
            DataGridView1.Columns(1).Width = 100
            DataGridView1.Columns(2).Width = 250
            DataGridView1.Columns(0).HeaderText = "Kode"
            DataGridView1.Columns(1).HeaderText = "Nama Diet"
            DataGridView1.Columns(2).HeaderText = "Keterangan Diet"

            DataGridView1.DefaultCellStyle.ForeColor = Color.Black
            DataGridView1.DefaultCellStyle.Padding = New Padding(10, 0, 0, 0)

            'DataGridView2.Columns(0).Width = 50
            'DataGridView2.Columns(1).Width = 150
            'DataGridView2.Columns(2).Width = 70
            'DataGridView2.Columns(3).Width = 150
            'DataGridView2.Columns(0).HeaderText = "No."
            'DataGridView2.Columns(1).HeaderText = "No.Permintaan"
            'DataGridView2.Columns(2).HeaderText = "Kode"
            'DataGridView2.Columns(3).HeaderText = "Keterangan Diet"

            'DataGridView2.Columns(0).Visible = False
            'DataGridView2.DefaultCellStyle.ForeColor = Color.Black

            dgv_styleRow()
        Catch ex As Exception

        End Try
    End Sub

    Sub transferSelected()
        'Call koneksiServer()
        Dim count As Integer
        count = DataGridView2.Rows.Count
        Dim dr As New System.Windows.Forms.DataGridViewRow
        For Each dr In Me.DataGridView1.SelectedRows
            DataGridView2.Rows.Add(1)
            DataGridView2.Rows(DataGridView2.RowCount - 1).Cells(0).Value = count + 1
            DataGridView2.Rows(DataGridView2.RowCount - 1).Cells(1).Value = txtNoPermintaan.Text
            DataGridView2.Rows(DataGridView2.RowCount - 1).Cells(2).Value = dr.Cells(0).Value
            DataGridView2.Rows(DataGridView2.RowCount - 1).Cells(3).Value = dr.Cells(2).Value
            DataGridView2.Update()
        Next

        Dim row As DataGridViewRow
        Dim i As Integer = 0
        For Each row In DataGridView2.Rows
            DataGridView2.Rows(i).HeaderCell.Value = (1 + i).ToString
            i += 1
        Next

        'conn.Close()
    End Sub

    Sub caridata()
        Call koneksiGizi()
        Dim query As String
        query = "SELECT kddiet, nmdiet, keterangandiet 
                   FROM t_diet 
                  WHERE NOT lHEADER = 1 
                    AND KDDIET BETWEEN 'B01' AND 'D12'
                    AND keterangandiet Like '%" & txtCari.Text & "%'"
        da = New MySqlDataAdapter(query, conn)

        Dim str As New DataTable
        str.Clear()
        da.Fill(str)
        DataGridView1.DataSource = str
        conn.Close()
    End Sub

    Private Sub Menu_Diet_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Menu Diet"
                    txtNoPermintaan.Text = Gizi.txtKdPermintaan.Text
            End Select
        End If

        Call autoMenuDiet()
        Call aturDGVTindakan()
    End Sub

    Private Sub txtCari_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCari.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call caridata()
            Call aturDGVTindakan()
        End If
    End Sub

    Private Sub btnPilihOk_Click(sender As Object, e As EventArgs) Handles btnPilihOk.Click
        Call transferSelected()
    End Sub
End Class