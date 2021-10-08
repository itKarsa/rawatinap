Imports MySql.Data.MySqlClient
Public Class DaftarHasilLab

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Sub dgv1_styleRow()
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Sub tampilData()

        Dim query As String = ""
        query = "SELECT PID,PNAME,SEX,DATE_FORMAT(DOB,'%d %b %Y') AS DOB,
                        AGEYY,AGEMM,AGEDD,LNO,
                        DATE_FORMAT(REQUEST_DT,'%d/%m/%Y %H.%i') AS REQUEST_DT,
                        DATE_FORMAT(VALIDATE_ON,'%d/%m/%Y %H.%i') AS TGL_SELESAI,
                        VALIDATE_ON,TG_NAME,SOURCE_CD,SOURCE_NM,CLINICIAN_NM 
                   FROM labreshd 
                  WHERE SOURCE_NM LIKE '%" & txtSourceNm.Text & "%' 
                    AND STR_TO_DATE(VALIDATE_ON,'%Y%m%d') BETWEEN '" & Format(Now, "yyyy-MM-dd") & "' 
                    AND '" & Format(DateAdd(DateInterval.Day, 1, Now), "yyyy-MM-dd") & "'
               ORDER BY VALIDATE_ON DESC"
        Try
            Call koneksiLis()
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            DataGridView1.Rows.Clear()

            Do While dr.Read
                DataGridView1.Rows.Add(dr.Item("PID"), dr.Item("PNAME"), dr.Item("SEX"), dr.Item("DOB"),
                                       dr.Item("AGEYY"), dr.Item("AGEMM"), dr.Item("AGEDD"), dr.Item("LNO"),
                                       dr.Item("REQUEST_DT"), dr.Item("TGL_SELESAI"), dr.Item("VALIDATE_ON"), dr.Item("TG_NAME"),
                                       dr.Item("SOURCE_CD"), dr.Item("SOURCE_NM"), dr.Item("CLINICIAN_NM"))
            Loop

            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub aturDGV()
        Try
            DataGridView1.Columns(0).Width = 100
            DataGridView1.Columns(1).Width = 200
            DataGridView1.Columns(2).Width = 100
            DataGridView1.Columns(3).Width = 150
            DataGridView1.Columns(4).Width = 50
            DataGridView1.Columns(5).Width = 50
            DataGridView1.Columns(6).Width = 50
            DataGridView1.Columns(7).Width = 100
            DataGridView1.Columns(8).Width = 150
            DataGridView1.Columns(9).Width = 150
            DataGridView1.Columns(10).Width = 150
            DataGridView1.Columns(11).Width = 200
            DataGridView1.Columns(12).Width = 150
            DataGridView1.Columns(13).Width = 250
            DataGridView1.Columns(0).HeaderText = "NO.RM"
            DataGridView1.Columns(1).HeaderText = "NAMA PASIEN"
            DataGridView1.Columns(2).HeaderText = "JENIS KELAMIN"
            DataGridView1.Columns(3).HeaderText = "TGL.LAHIR"
            DataGridView1.Columns(4).HeaderText = "TH"
            DataGridView1.Columns(5).HeaderText = "BLN"
            DataGridView1.Columns(6).HeaderText = "HR"
            DataGridView1.Columns(7).HeaderText = "NO.LAB"
            DataGridView1.Columns(8).HeaderText = "TGL.MASUK LAB"
            DataGridView1.Columns(9).HeaderText = "TGL.SELESAI"
            DataGridView1.Columns(10).HeaderText = "VALIDATE_ON"
            DataGridView1.Columns(11).HeaderText = "JENIS PEMERIKSAAN"
            DataGridView1.Columns(12).HeaderText = "KODE RUANG/POLI"
            DataGridView1.Columns(13).HeaderText = "ASAL RUANG/POLI"
            DataGridView1.Columns(14).HeaderText = "DOKTER PENGIRIM"

            DataGridView1.Columns(2).Visible = False
            DataGridView1.Columns(4).Visible = False
            DataGridView1.Columns(5).Visible = False
            DataGridView1.Columns(6).Visible = False
            DataGridView1.Columns(10).Visible = False
            DataGridView1.Columns(12).Visible = False

            DataGridView1.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
            DataGridView1.DefaultCellStyle.ForeColor = Color.Black
            DataGridView1.DefaultCellStyle.SelectionForeColor = Color.White

            Dim btn As New DataGridViewButtonColumn()
            DataGridView1.Columns.Add(btn)
            btn.HeaderText = "FILE HASIL"
            btn.Text = "Lihat Hasil"
            btn.Name = "btn"
            btn.Width = 150
            btn.FlatStyle = FlatStyle.Flat
            btn.UseColumnTextForButtonValue = True

            For i = 0 To DataGridView1.Rows.Count - 1
                DataGridView1.Rows(i).Cells(15).Style.BackColor = Color.DodgerBlue
                DataGridView1.Rows(i).Cells(15).Style.ForeColor = Color.White
            Next

            For Each column As DataGridViewColumn In DataGridView1.Columns
                column.SortMode = DataGridViewColumnSortMode.NotSortable
            Next

            Call dgv1_styleRow()

        Catch ex As Exception

        End Try
    End Sub
    Sub caridata()
        Call koneksiLis()
        Dim query As String
        query = "SELECT PID,PNAME,SEX,DATE_FORMAT(DOB,'%d %b %Y') AS DOB,
                        AGEYY,AGEMM,AGEDD,LNO,
                        DATE_FORMAT(REQUEST_DT,'%d/%m/%Y %H.%i') AS REQUEST_DT,
                        DATE_FORMAT(VALIDATE_ON,'%d/%m/%Y %H.%i') AS TGL_SELESAI,
                        VALIDATE_ON,TG_NAME,SOURCE_CD,SOURCE_NM,CLINICIAN_NM 
                   FROM labreshd 
                  WHERE SOURCE_NM LIKE '%" & txtSourceNm.Text & "%' 
                    AND (PID LIKE '%" & txtCari.Text & "%' OR PNAME LIKE '%" & txtCari.Text & "%')"
        da = New MySqlDataAdapter(query, conn)

        Try
            Call koneksiLis()
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            DataGridView1.Rows.Clear()

            Do While dr.Read
                DataGridView1.Rows.Add(dr.Item("PID"), dr.Item("PNAME"), dr.Item("SEX"), dr.Item("DOB"),
                                       dr.Item("AGEYY"), dr.Item("AGEMM"), dr.Item("AGEDD"), dr.Item("LNO"),
                                       dr.Item("REQUEST_DT"), dr.Item("TGL_SELESAI"), dr.Item("VALIDATE_ON"), dr.Item("TG_NAME"),
                                       dr.Item("SOURCE_CD"), dr.Item("SOURCE_NM"), dr.Item("CLINICIAN_NM"))
            Loop

            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub filterData()
        Dim query As String = ""
        query = "SELECT PID,PNAME,SEX,DATE_FORMAT(DOB,'%d %b %Y') AS DOB,
                        AGEYY,AGEMM,AGEDD,LNO,
                        DATE_FORMAT(REQUEST_DT,'%d/%m/%Y %H.%i') AS REQUEST_DT,
                        DATE_FORMAT(VALIDATE_ON,'%d/%m/%Y %H.%i') AS TGL_SELESAI,
                        VALIDATE_ON,TG_NAME,SOURCE_CD,SOURCE_NM,CLINICIAN_NM 
                   FROM labreshd
                  WHERE SOURCE_NM LIKE '%" & txtSourceNm.Text & "%'
                    AND STR_TO_DATE(VALIDATE_ON,'%Y%m%d') BETWEEN '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' 
                    AND '" & Format(DateAdd(DateInterval.Day, 1, DateTimePicker2.Value), "yyyy-MM-dd") & "'
               ORDER BY VALIDATE_ON DESC"

        Try
            Call koneksiLis()
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            DataGridView1.Rows.Clear()

            Do While dr.Read
                DataGridView1.Rows.Add(dr.Item("PID"), dr.Item("PNAME"), dr.Item("SEX"), dr.Item("DOB"),
                                       dr.Item("AGEYY"), dr.Item("AGEMM"), dr.Item("AGEDD"), dr.Item("LNO"),
                                       dr.Item("REQUEST_DT"), dr.Item("TGL_SELESAI"), dr.Item("VALIDATE_ON"), dr.Item("TG_NAME"),
                                       dr.Item("SOURCE_CD"), dr.Item("SOURCE_NM"), dr.Item("CLINICIAN_NM"))
            Loop

            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub DaftarHasilLab_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.Manual
        With Screen.PrimaryScreen.WorkingArea
            Me.SetBounds(.Left, .Top, .Width, .Height)
        End With

        Dim ruangA As String
        Dim ruangAr() As String

        txtCari.Select()

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "DaftarHasilLIS"
                    ruangA = Form1.Label1.Text
                    ruangAr = ruangA.Split(" ")
                    txtSourceNm.Text = ruangAr(0)
                    Call tampilData()
                    'Call aturDGV()
            End Select
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim konfirmasi As MsgBoxResult
        Dim nama As String
        Dim PID, LNO, SOURCE_CD As String
        Dim kdruang As String

        nama = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString
        PID = DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString
        LNO = DataGridView1.Rows(e.RowIndex).Cells(7).Value.ToString
        LNO = DataGridView1.Rows(e.RowIndex).Cells(7).Value.ToString
        SOURCE_CD = DataGridView1.Rows(e.RowIndex).Cells(12).Value.ToString

        txtPID.Text = PID
        txtLNO.Text = LNO
        'kdruang = SOURCE_CD.Substring(0, 4)
        'namaRuang(kdruang)

        If e.ColumnIndex = 15 Then
            konfirmasi = MsgBox("Apakah hasil pemeriksaan pasien '" & nama & "' akan dicetak ?", vbQuestion + vbYesNo, "Laboratorium")
            If konfirmasi = vbYes Then
                ViewCetakLIS.Ambil_Data = True
                ViewCetakLIS.Form_Ambil_Data = "CetakLis"
                ViewCetakLIS.Show()
                'MsgBox("Hasil Pemeriksaan pasien " & nama & " berhasil mencetak", MsgBoxStyle.Information)
            End If
        End If

    End Sub

    Sub namaRuang(kode As String)
        Select Case kode
            Case 2001
                txtSourceNm.Text = "Matahari"
            Case 2002
                txtSourceNm.Text = "Teratai"
            Case 2003
                txtSourceNm.Text = "Mawar"
            Case 2004
                txtSourceNm.Text = "Dahlia"
            Case 2005
                txtSourceNm.Text = "Kemuning"
            Case 2006
                txtSourceNm.Text = "Seruni"
            Case 2007
                txtSourceNm.Text = "Unit Stroke"
            Case 2008
                txtSourceNm.Text = "ICU"
            Case 2009
                txtSourceNm.Text = "Perinatologi"
            Case 2010
                txtSourceNm.Text = "Amarilis"
            Case 2011
                txtSourceNm.Text = "Lavender"
            Case 1001
                txtSourceNm.Text = "Dalam"
            Case 1002
                txtSourceNm.Text = "Bedah"
            Case 1003
                txtSourceNm.Text = "Kandungan"
            Case 1004
                txtSourceNm.Text = "Anak"
            Case 1005
                txtSourceNm.Text = "Syaraf"
            Case 1006
                txtSourceNm.Text = "Paru"
            Case 1007
                txtSourceNm.Text = "Mata"
            Case 1008
                txtSourceNm.Text = "THT"
            Case 1009
                txtSourceNm.Text = "Gigi dan Orthodonti"
            Case 1010
                txtSourceNm.Text = "Orthopedi"
            Case 1011
                txtSourceNm.Text = "Jantung"
            Case 1012
                txtSourceNm.Text = "Bedah Digestif"
            Case 1013
                txtSourceNm.Text = "Bedah Plastik"
            Case 1014
                txtSourceNm.Text = "Syaraf"
            Case 1015
                txtSourceNm.Text = "Diabetes Melitus"
            Case 1016
                txtSourceNm.Text = "Kulit dan Kelamin"
            Case 1017
                txtSourceNm.Text = "Komplementer"
            Case 1018
                txtSourceNm.Text = "Anastesi"
            Case 1019
                txtSourceNm.Text = "VCT"
            Case 4001
                txtSourceNm.Text = "IGD"
        End Select
    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub txtCari_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCari.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtCari.Text = "" Then
                Call tampilData()
                'Call aturDGV()
            Else
                Call caridata()
                'Call aturDGV()
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        DataGridView1.DefaultCellStyle.Font = New Font("Tahoma", 10, FontStyle.Bold)
        DataGridView1.DefaultCellStyle.ForeColor = Color.Black
        DataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black

        For i = 0 To DataGridView1.Rows.Count - 1
            DataGridView1.Rows(i).Cells(15).Style.BackColor = Color.FromArgb(232, 243, 239)
            DataGridView1.Rows(i).Cells(15).Style.ForeColor = Color.FromArgb(26, 141, 95)
        Next

        For Each column As DataGridViewColumn In DataGridView1.Columns
            column.SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If i Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next
    End Sub

    Private Sub btnTampil_Click(sender As Object, e As EventArgs) Handles btnTampil.Click
        Call filterData()
    End Sub

    Private Sub btnTampil_MouseLeave(sender As Object, e As EventArgs) Handles btnTampil.MouseLeave
        Me.btnTampil.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnTampil.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnTampil_MouseEnter(sender As Object, e As EventArgs) Handles btnTampil.MouseEnter
        Me.btnTampil.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnTampil.ForeColor = Color.FromArgb(232, 243, 239)
    End Sub
End Class