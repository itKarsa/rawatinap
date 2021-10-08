Imports MySql.Data.MySqlClient
Public Class Daftar_ObatAlkes

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Dim kode, obat, stok, satuan, harga As String

    Sub tampilData()
        Try
            Call koneksiFarmasi()
            da = New MySqlDataAdapter("SELECT plu,obat,jmlstok,
                                              satuan,golongan,kelompok,
                                              formularium, hargajual 
                                         FROM vw_stokobat 
                                        WHERE kddepo = 'DP03' 
                                        ORDER BY CAST(plu AS UNSIGNED)", conn)
            ds = New DataSet
            da.Fill(ds, "vw_stokobat")
            DataGridView1.DataSource = ds.Tables("vw_stokobat")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub caridata()
        Dim query As String
        query = "SELECT plu,obat,jmlstok,
                        satuan,golongan,kelompok,
                        formularium, hargajual 
                   FROM vw_stokobat 
                  WHERE kddepo = 'DP03' 
                    AND obat like '%" & txtCari.Text & "%'"
        da = New MySqlDataAdapter(query, conn)

        Dim str As New DataTable
        str.Clear()
        da.Fill(str)
        DataGridView1.DataSource = str
    End Sub

    Sub aturDGV()
        Try
            DataGridView1.Columns(0).Width = 70
            DataGridView1.Columns(1).Width = 250
            DataGridView1.Columns(2).Width = 100
            DataGridView1.Columns(3).Width = 70
            DataGridView1.Columns(4).Width = 120
            DataGridView1.Columns(5).Width = 100
            DataGridView1.Columns(6).Width = 150
            DataGridView1.Columns(7).Width = 150
            DataGridView1.Columns(0).HeaderText = "Kode"
            DataGridView1.Columns(1).HeaderText = "Nama Obat"
            DataGridView1.Columns(2).HeaderText = "Jumlah Stok"
            DataGridView1.Columns(3).HeaderText = "Satuan"
            DataGridView1.Columns(4).HeaderText = "Golongan"
            DataGridView1.Columns(5).HeaderText = "Kelompok"
            DataGridView1.Columns(6).HeaderText = "Formularium"
            DataGridView1.Columns(7).HeaderText = "Harga"

            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(7).Visible = False

            DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView1.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Daftar_ObatAlkes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tampilData()
        Call aturDGV()
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.AliceBlue
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.White
            End If
        Next

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).Cells(2).Value = "0" Then
                DataGridView1.Rows(i).Cells(2).Style.BackColor = Color.FromArgb(192, 0, 0)
                DataGridView1.Rows(i).Cells(2).Style.ForeColor = Color.White
            End If
        Next
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

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        Me.Close()
    End Sub

    Private Sub Daftar_ObatAlkes_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F11
                Me.Close()
            Case Keys.F12
                Me.Close()
        End Select
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick

        If e.RowIndex = -1 Then
            Return
        End If

        kode = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        obat = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        stok = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        satuan = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        harga = DataGridView1.Rows(e.RowIndex).Cells(7).Value

        If DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString = "0" Then
            MsgBox("Stok obat habis, mohon hubungi depo farmasi rawat inap", MsgBoxStyle.Information)
        Else
            If Ambil_Data = True Then
                Select Case Form_Ambil_Data
                    Case "Daftar Obat"
                        Resep_Dokter.txtKdObat.Text = kode
                        Resep_Dokter.txtObat.Text = obat
                        Resep_Dokter.txtStok.Text = stok
                        Resep_Dokter.txtSatuan.Text = satuan
                        Resep_Dokter.txtHarga.Text = harga
                    Case "Racikan"
                        Obat_Racikan.txtKdObat.Text = kode
                        Obat_Racikan.txtObat.Text = obat
                        Obat_Racikan.txtStokObat.Text = stok
                        Obat_Racikan.txtSatuanObat.Text = satuan
                        Obat_Racikan.txtHargaObat.Text = harga
                        Obat_Racikan.txtHargaObatDec.Text = harga
                        Obat_Racikan.txtJmlPakaiObat.Text = 1
                End Select
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick

        If e.RowIndex = -1 Then
            Return
        End If

        kode = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        obat = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        stok = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        satuan = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        harga = DataGridView1.Rows(e.RowIndex).Cells(7).Value

        If DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString = "0" Then
            MsgBox("Stok obat habis, mohon hubungi depo farmasi rawat inap", MsgBoxStyle.Information)
        Else
            If Ambil_Data = True Then
                Select Case Form_Ambil_Data
                    Case "Daftar Obat"
                        Resep_Dokter.txtKdObat.Text = kode
                        Resep_Dokter.txtObat.Text = obat
                        Resep_Dokter.txtStok.Text = stok
                        Resep_Dokter.txtSatuan.Text = satuan
                        Resep_Dokter.txtHarga.Text = harga
                        Resep_Dokter.txtDibutuhkan.Text = 1
                        Resep_Dokter.txtDibutuhkan.Enabled = True
                    Case "Racikan"
                        Obat_Racikan.txtKdObat.Text = kode
                        Obat_Racikan.txtObat.Text = obat
                        Obat_Racikan.txtStokObat.Text = stok
                        Obat_Racikan.txtSatuanObat.Text = satuan
                        Obat_Racikan.txtHargaObat.Text = harga
                        Obat_Racikan.txtHargaObatDec.Text = harga
                        Obat_Racikan.txtJmlPakaiObat.Text = 1
                        Obat_Racikan.txtJmlPakaiObat.Enabled = True
                End Select
            End If
        End If

        Me.Close()
    End Sub

    Private Sub txtCari_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCari.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtCari.Text = "" Then
                Call tampilData()
                Call aturDGV()
            Else
                Call caridata()
            End If
        End If
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Daftar Obat"
                    Resep_Dokter.txtKdObat.Text = kode
                    Resep_Dokter.txtObat.Text = obat
                    Resep_Dokter.txtStok.Text = stok
                    Resep_Dokter.txtSatuan.Text = satuan
                    Resep_Dokter.txtHarga.Text = harga
                    Resep_Dokter.txtDibutuhkan.Text = 1
                    Resep_Dokter.txtDibutuhkan.Enabled = True
                Case "Racikan"
                    Obat_Racikan.txtKdObat.Text = kode
                    Obat_Racikan.txtObat.Text = obat
                    Obat_Racikan.txtStokObat.Text = stok
                    Obat_Racikan.txtSatuanObat.Text = satuan
                    Obat_Racikan.txtHargaObat.Text = harga
                    Obat_Racikan.txtHargaObatDec.Text = harga
                    Obat_Racikan.txtJmlPakaiObat.Text = 1
                    Obat_Racikan.txtJmlPakaiObat.Enabled = True
            End Select
        End If
        Me.Close()
    End Sub
End Class