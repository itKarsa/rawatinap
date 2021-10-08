Imports MySql.Data.MySqlClient
Imports System.Data.Odbc
Module Koneksi

    Public conn As MySqlConnection
    Public cmd As MySqlCommand
    Public da As MySqlDataAdapter
    Public dr As MySqlDataReader
    Public ds As DataSet
    Public dt As New DataTable
    Public str As String
    Public maxrow As Integer

    Public connOdbc As New OdbcConnection
    Public cmdOdbc As New OdbcCommand
    Public daOdbc As New OdbcDataAdapter
    Public drOdbc As OdbcDataReader

    Public noRegPA, DokPA, makros, mikros, conclu As String

    Public Sub koneksidb()
        Try
            Dim str As String = "Server=localhost;user id=root;password=;database=simrs"
            conn = New MySqlConnection(str)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub koneksiServer()
        Try
            Dim str As String = "Server=192.168.200.2;user id=lis;password=lis1234;database=simrs1coba;default command timeout=120;Convert Zero Datetime=True"
            conn = New MySqlConnection(str)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            MsgBox("Terputus dari server, Silahkan Login kembali/Hubungi Tim IT", MsgBoxStyle.Exclamation, "Rawat Inap : Information")
            LoginForm.Close()
        End Try
    End Sub

    Public Sub koneksiOdbc()
        Try
            Dim str As String = "DSN=simrs;Server=localhost;user id=root;password=;database=simrs"
            connOdbc = New OdbcConnection(str)
            If connOdbc.State = ConnectionState.Closed Then
                connOdbc.Open()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub koneksiGizi()
        Try
            Dim str As String = "Server=192.168.200.2;user id=lis;password=lis1234;database=gizicoba;default command timeout=120;Convert Zero Datetime=True"
            conn = New MySqlConnection(str)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            MsgBox("Terputus dari server 'Gizi', Silahkan Login kembali/Hubungi Tim IT", MsgBoxStyle.Exclamation, "Gizi : Information")
            Gizi.Close()
        End Try
    End Sub

    Public Sub koneksiFarmasi()
        Try
            Dim str As String = "Server=192.168.200.2;user id=lis;password=lis1234;database=farmasi2;default command timeout=120;Convert Zero Datetime=True"
            conn = New MySqlConnection(str)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            MsgBox("Terputus dari server 'Farmasi', Silahkan Login kembali/Hubungi Tim IT", MsgBoxStyle.Exclamation, "Rawat Inap : Information")
            Resep_Dokter.Close()
        End Try
    End Sub

    Public Sub koneksiLocal()
        Try
            Dim str As String = "Server=localhost;Database=simrsgizi;Uid=root;Pwd=''"
            conn = New MySqlConnection(str)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub koneksiLis()
        Try
            Dim str As String = "Server=192.168.200.2;user id=lis;password=lis1234;database=simrslab;Convert Zero Datetime=True"
            conn = New MySqlConnection(str)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub DrawFormGradient(frm As Form, ByVal FirstColor As Color, ByVal SecondColor As Color)
        Dim objBrush As New Drawing2D.LinearGradientBrush(frm.DisplayRectangle, FirstColor, SecondColor, Drawing2D.LinearGradientMode.Vertical)
        Dim objGraphic As Graphics = frm.CreateGraphics
        objGraphic.FillRectangle(objBrush, frm.DisplayRectangle)
        objBrush.Dispose()
        objGraphic.Dispose()
    End Sub

    Public Function cekJmlRuang(noReg As String) As String
        Call koneksiServer()
        Dim query As String
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        Dim jml As String = ""
        query = "SELECT COUNT(tglMasukRawatInap) AS jml
		           FROM vw_daftarruangakomodasi
	              WHERE noDaftar = '" & noReg & "'"
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                jml = dr.Item("jml").ToString
            End If
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        conn.Close()

        Return jml
    End Function
End Module
