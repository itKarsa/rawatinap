Imports MySql.Data.MySqlClient
Public Class DaftarHasilPA

    Public Ambil_Data As String
    Public Form_Ambil_Data As String
    Public rm, nama, tglLahir, tglMasuk, tglhasil, diagnosa, pengirim, dpjp, alamat,
           asalRS, tglSediaan, noPA, lokalisasi, bahan, kdhasil, jk As String

    Sub tampilData(ruang As String, date1 As Date, date2 As Date)
        Dim query As String = ""
        query = "SELECT hsl.noRM,
	                    vw.nmPasien,
	                    vw.tglLahir,
	                    hsl.noRegistrasiPA,
	                    vw.tglMasukPARanap,
	                    hsl.tglHasil,
	                    hsl.diagnosa,
	                    hsl.asalpengirim,
	                    vw.namapetugasMedis,
                        hsl.kdHasilPA,
	                    vw.alamat,
	                    hsl.asalrs,
	                    hsl.tglTerimaSediaan,
	                    hsl.noPA,
	                    hsl.lokalisasi,
	                    hsl.bahan,
	                    vw.dokterPemeriksa,
                        vw.jenisKelamin
                   FROM t_hasilpemeriksaanpatologi AS hsl
	                    INNER JOIN vw_pasienpatologiranap AS vw ON hsl.noRegistrasiPA = vw.noRegistrasiPARanap
                  WHERE (vw.tglMasukPARanap BETWEEN '" & Format(date1, "yyyy-MM-dd") & "' AND '" & Format(DateAdd(DateInterval.Day, 1, date2), "yyyy-MM-dd") & "')
	                    AND hsl.asalpengirim LIKE '" & ruang & "%'
                    ORDER BY nmPasien ASC"
        Try
            Call koneksiServer()
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            DataGridView1.Rows.Clear()

            Do While dr.Read
                DataGridView1.Rows.Add(dr.Item("noRM"), dr.Item("nmPasien"), dr.Item("tglLahir"),
                                       dr.Item("noRegistrasiPA"), dr.Item("tglMasukPARanap"), dr.Item("tglHasil"),
                                       dr.Item("diagnosa"), dr.Item("asalpengirim"), dr.Item("namapetugasMedis"),
                                       dr.Item("kdHasilPA"), dr.Item("alamat"), dr.Item("asalrs"),
                                       dr.Item("tglTerimaSediaan"), dr.Item("noPA"), dr.Item("lokalisasi"),
                                       dr.Item("bahan"), dr.Item("dokterPemeriksa"), dr.Item("jenisKelamin"))
            Loop

            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub caridataByName()
        Dim query As String = ""
        query = "SELECT hsl.noRM,vw.nmPasien,vw.tglLahir,hsl.noRegistrasiPA,vw.tglMasukPARanap,
	                    hsl.tglHasil,hsl.diagnosa,hsl.asalpengirim,vw.namapetugasMedis,hsl.kdHasilPA,
	                    vw.alamat,hsl.asalrs, hsl.tglTerimaSediaan,hsl.noPA,hsl.lokalisasi,
	                    hsl.bahan,vw.dokterPemeriksa,vw.jenisKelamin
                   FROM t_hasilpemeriksaanpatologi AS hsl
	         INNER JOIN vw_pasienpatologiranap AS vw ON hsl.noRegistrasiPA = vw.noRegistrasiPARanap
                  WHERE hsl.asalpengirim LIKE '" & Form1.Label1.Text & "%'
                    AND (vw.nmPasien LIKE '%" & txtCari.Text & "%' OR hsl.noRM LIKE '%" & txtCari.Text & "%')
                  UNION ALL
                 SELECT hsl.noRM,vw.nmPasien,vw.tglLahir,hsl.noRegistrasiPA,vw.tglMasukPARajal,
                        hsl.tglHasil,hsl.diagnosa,hsl.asalpengirim,vw.namapetugasMedis,hsl.kdHasilPA,
			            vw.alamat,hsl.asalrs,hsl.tglTerimaSediaan,hsl.noPA,hsl.lokalisasi,
			            hsl.bahan,vw.dokterPemeriksa,vw.jenisKelamin
                   FROM t_hasilpemeriksaanpatologi AS hsl
             INNER JOIN vw_pasienpatologirajal AS vw ON hsl.noRegistrasiPA = vw.noRegistrasiPARajal
                  WHERE hsl.asalpengirim LIKE '" & Form1.Label1.Text & "%'
                    AND (vw.nmPasien LIKE '%" & txtCari.Text & "%' OR hsl.noRM LIKE '%" & txtCari.Text & "%')
               ORDER BY nmPasien ASC"
        Try
            Call koneksiServer()
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            DataGridView1.Rows.Clear()

            Do While dr.Read
                DataGridView1.Rows.Add(dr.Item("noRM"), dr.Item("nmPasien"), dr.Item("tglLahir"),
                                       dr.Item("noRegistrasiPA"), dr.Item("tglMasukPARanap"), dr.Item("tglHasil"),
                                       dr.Item("diagnosa"), dr.Item("asalpengirim"), dr.Item("namapetugasMedis"),
                                       dr.Item("kdHasilPA"), dr.Item("alamat"), dr.Item("asalrs"),
                                       dr.Item("tglTerimaSediaan"), dr.Item("noPA"), dr.Item("lokalisasi"),
                                       dr.Item("bahan"), dr.Item("dokterPemeriksa"), dr.Item("jenisKelamin"))
            Loop

            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub DaftarHasilPA_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.Manual
        With Screen.PrimaryScreen.WorkingArea
            Me.SetBounds(.Left, .Top, .Width, .Height)
        End With

        txtCari.Select()

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "DaftarHasilPA"
                    Call tampilData(Form1.Label1.Text, DateTimePicker1.Value, DateTimePicker2.Value)
            End Select
        End If
    End Sub

    Private Sub btnTampil_Click(sender As Object, e As EventArgs) Handles btnTampil.Click
        Call tampilData(Form1.Label1.Text, DateTimePicker1.Value, DateTimePicker2.Value)
    End Sub

    Private Sub txtCari_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCari.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtCari.Text = "" Then
                Call tampilData(Form1.Label1.Text, Now, Now)
            Else
                Call caridataByName()
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim konfirmasi As MsgBoxResult

        rm = DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString
        nama = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString
        tglLahir = DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString
        noRegPA = DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString
        tglMasuk = DataGridView1.Rows(e.RowIndex).Cells(4).Value.ToString
        tglhasil = DataGridView1.Rows(e.RowIndex).Cells(5).Value.ToString
        diagnosa = DataGridView1.Rows(e.RowIndex).Cells(6).Value.ToString
        pengirim = DataGridView1.Rows(e.RowIndex).Cells(7).Value.ToString
        dpjp = DataGridView1.Rows(e.RowIndex).Cells(8).Value.ToString
        kdhasil = DataGridView1.Rows(e.RowIndex).Cells(9).Value.ToString
        alamat = DataGridView1.Rows(e.RowIndex).Cells(10).Value.ToString
        asalRS = DataGridView1.Rows(e.RowIndex).Cells(11).Value.ToString
        tglSediaan = DataGridView1.Rows(e.RowIndex).Cells(12).Value.ToString
        noPA = DataGridView1.Rows(e.RowIndex).Cells(13).Value.ToString
        lokalisasi = DataGridView1.Rows(e.RowIndex).Cells(14).Value.ToString
        bahan = DataGridView1.Rows(e.RowIndex).Cells(15).Value.ToString
        kdhasil = DataGridView1.Rows(e.RowIndex).Cells(16).Value.ToString
        jk = DataGridView1.Rows(e.RowIndex).Cells(17).Value.ToString

        If e.ColumnIndex = 18 Then
            konfirmasi = MsgBox("Apakah hasil pemeriksaan pasien '" & nama & "' akan dicetak ?", vbQuestion + vbYesNo, "Laboratorium")
            If konfirmasi = vbYes Then
                ViewHasilPA.Show()
                'MsgBox("Hasil Pemeriksaan pasien " & nama & " berhasil mencetak", MsgBoxStyle.Information)
            End If
        End If

    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub btnTampil_MouseLeave(sender As Object, e As EventArgs) Handles btnTampil.MouseLeave
        Me.btnTampil.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnTampil.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnTampil_MouseEnter(sender As Object, e As EventArgs) Handles btnTampil.MouseEnter
        Me.btnTampil.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnTampil.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        DataGridView1.DefaultCellStyle.Font = New Font("Tahoma", 8, FontStyle.Bold, FontStyle.Italic)
        DataGridView1.DefaultCellStyle.ForeColor = Color.Black
        DataGridView1.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise

        For i = 0 To DataGridView1.Rows.Count - 1
            DataGridView1.Rows(i).Cells(18).Style.BackColor = Color.FromArgb(232, 243, 239)
            DataGridView1.Rows(i).Cells(18).Style.ForeColor = Color.FromArgb(26, 141, 95)
            DataGridView1.Rows(i).Cells(18).Style.SelectionBackColor = Color.FromArgb(26, 141, 95)
            DataGridView1.Rows(i).Cells(18).Style.SelectionForeColor = Color.FromArgb(232, 243, 239)
        Next

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
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