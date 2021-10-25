Imports MySql.Data.MySqlClient
Public Class RiwayatAhliGizi

    Sub tampilRuang()
        Call koneksiServer()
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        cmd = New MySqlCommand("SELECT DISTINCT loket_unit FROM t_aksesmenu WHERE instalasi IN('rawat inap')", conn)
        da = New MySqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        txtRuang.DataSource = dt
        txtRuang.DisplayMember = "loket_unit"
        txtRuang.ValueMember = "loket_unit"
        txtRuang.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txtRuang.AutoCompleteSource = AutoCompleteSource.ListItems
        conn.Close()
    End Sub

    Sub tampilRiwayat()
        Dim sql As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        sql = "SELECT nmPasien,rawatInap,tglPermintaan,
	                  WAKTU,kdDiet,statusUpdate,
                      usermodify,datemodify
                 FROM vw_daftarpermintaangizi
                WHERE kdAhliGizi = '" & txtKdAhliGizi.Text & "'
                  AND (SUBSTR(tglPermintaan,1,10) BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' AND
                       '" & Format(DateAdd(DateInterval.Day, 1, DateTimePicker2.Value), "yyyy-MM-dd") & "')
                  AND rawatInap LIKE '" & txtRuang.Text & "%'
                ORDER BY nmPasien ASC, tglPermintaan ASC"
        'MsgBox(sql)
        Try
            Call koneksiGizi()
            cmd = New MySqlCommand(sql, conn)
            dr = cmd.ExecuteReader
            DataGridView1.Rows.Clear()

            Do While dr.Read
                DataGridView1.Rows.Add(dr.Item("nmPasien"), dr.Item("rawatInap"), dr.Item("tglPermintaan"),
                                       dr.Item("WAKTU"), dr.Item("kdDiet"), "",
                                       dr.Item("statusUpdate"), dr.Item("usermodify"), dr.Item("datemodify"))
            Loop

            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Function tampilNamaDiet(kdDiet As String) As String
        Call koneksiGizi()
        Dim cmdDiet As MySqlCommand
        Dim drDiet As MySqlDataReader
        Dim nama As String = ""
        Dim sqlDiet As String
        Try
            sqlDiet = "CALL namaDiet('" & kdDiet & "')"
            cmdDiet = New MySqlCommand(sqlDiet, conn)
            drDiet = cmdDiet.ExecuteReader
            drDiet.Read()
            If drDiet.HasRows Then
                If IsDBNull(drDiet.Item("MENU")) Then
                    nama = "-"
                Else
                    nama = drDiet.Item("MENU")
                End If
            End If
            drDiet.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString, MessageBoxIcon.Error, "Error Nama Diet")
        End Try

        Return nama
    End Function

    Private Sub RiwayatAhliGizi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtAhliGizi.Text = Gizi.txtAhliGizi.Text
        txtKdAhliGizi.Text = Gizi.txtKdAhliGizi.Text
        Call tampilRuang()
    End Sub

    Private Sub btnTampil_Click(sender As Object, e As EventArgs) Handles btnTampil.Click
        Call tampilRiwayat()
        For Each row As DataGridViewRow In DataGridView1.Rows
            row.Cells(5).Value = tampilNamaDiet(row.Cells(4).Value)
        Next
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next

        If e.RowIndex > 0 And e.ColumnIndex = 0 Then
            If DataGridView1.Item(0, e.RowIndex - 1).Value = e.Value Then
                e.Value = ""
            End If
        End If

        For i As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(i).Cells(6).Value.ToString = "0" Then
                DataGridView1.Rows(i).Cells(6).Value = "KONDISI SEKARANG"
                DataGridView1.Rows(i).Cells(6).Style.BackColor = Color.Green
                DataGridView1.Rows(i).Cells(6).Style.ForeColor = Color.White
            ElseIf DataGridView1.Rows(i).Cells(6).Value.ToString = "1" Then
                DataGridView1.Rows(i).Cells(6).Value = "KONDISI LAMA"
                DataGridView1.Rows(i).Cells(6).Style.BackColor = Color.LightCoral
                DataGridView1.Rows(i).Cells(6).Style.ForeColor = Color.White
            ElseIf DataGridView1.Rows(i).Cells(6).Value.ToString = "2" Then
                DataGridView1.Rows(i).Cells(6).Value = "PASIEN CHECKOUT"
                DataGridView1.Rows(i).Cells(6).Style.BackColor = Color.Red
                DataGridView1.Rows(i).Cells(6).Style.ForeColor = Color.White
            End If
        Next

        DataGridView1.ColumnHeadersDefaultCellStyle.Font = New Font("Tahoma", 10, FontStyle.Bold)
        DataGridView1.DefaultCellStyle.Font = New Font("Tahoma", 9, FontStyle.Bold)
        DataGridView1.DefaultCellStyle.ForeColor = Color.Black
        DataGridView1.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise
        DataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black

    End Sub

    Private Sub btnTampil_MouseLeave(sender As Object, e As EventArgs) Handles btnTampil.MouseLeave
        Me.btnTampil.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnTampil.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnTampil_MouseEnter(sender As Object, e As EventArgs) Handles btnTampil.MouseEnter
        Me.btnTampil.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnTampil.ForeColor = Color.FromArgb(232, 243, 239)
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