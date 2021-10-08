Imports MySql.Data.MySqlClient
Public Class LoginForm

    Sub autoUnit()
        Call koneksiServer()

        Using cmd As New MySqlCommand("SELECT DISTINCT loket_unit FROM t_aksesmenu WHERE instalasi IN('rawat inap','gizi')", conn)
            Using rd As MySqlDataReader = cmd.ExecuteReader
                While rd.Read
                    With txtUnit
                        .AutoCompleteMode = AutoCompleteMode.Suggest
                        .AutoCompleteCustomSource.Add(rd.Item(0))
                        .AutoCompleteSource = AutoCompleteSource.CustomSource
                    End With
                End While
                rd.Close()
            End Using
        End Using
        conn.Close()
    End Sub

    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        autoUnit()

        txtUsername.ForeColor = Color.Gray
        txtUsername.Font = New Font("Tahoma", 12, FontStyle.Italic)
        If String.IsNullOrEmpty(txtUsername.Text) Then
            txtUsername.Text = "Username"
        End If

        txtPass.ForeColor = Color.Gray
        txtPass.Font = New Font("Tahoma", 12, FontStyle.Italic)
        If String.IsNullOrEmpty(txtPass.Text) Then
            txtPass.Text = "Password"
        End If

        txtUnit.ForeColor = Color.Gray
        txtUnit.Font = New Font("Tahoma", 12, FontStyle.Italic)
        If String.IsNullOrEmpty(txtUnit.Text) Then
            txtUnit.Text = "Ruang"
        End If
    End Sub

    Private Sub txtUsername_GotFocus(sender As Object, e As EventArgs) Handles txtUsername.GotFocus
        txtUsername.ForeColor = Color.Black
        txtUsername.Font = New Font("Tahoma", 12, FontStyle.Bold)
        If txtUsername.Text.Equals("Username", StringComparison.OrdinalIgnoreCase) Then
            txtUsername.Text = String.Empty
        End If
    End Sub

    Private Sub txtUsername_LostFocus(sender As Object, e As EventArgs) Handles txtUsername.LostFocus
        txtUsername.ForeColor = Color.Gray
        txtUsername.Font = New Font("Tahoma", 12, FontStyle.Italic)
        If String.IsNullOrEmpty(txtUsername.Text) Then
            txtUsername.Text = "Username"
        End If
    End Sub

    Private Sub txtPass_GotFocus(sender As Object, e As EventArgs) Handles txtPass.GotFocus
        txtPass.ForeColor = Color.Black
        txtPass.Font = New Font("Tahoma", 12, FontStyle.Bold)
        If txtPass.Text.Equals("Password", StringComparison.OrdinalIgnoreCase) Then
            txtPass.Text = String.Empty
        End If
    End Sub

    Private Sub txtPass_LostFocus(sender As Object, e As EventArgs) Handles txtPass.LostFocus
        txtPass.ForeColor = Color.Gray
        txtPass.Font = New Font("Tahoma", 12, FontStyle.Italic)
        If String.IsNullOrEmpty(txtPass.Text) Then
            txtPass.Text = "Password"
        End If
    End Sub

    Private Sub txtUnit_GotFocus(sender As Object, e As EventArgs) Handles txtUnit.GotFocus
        txtUnit.ForeColor = Color.Black
        txtUnit.Font = New Font("Tahoma", 12, FontStyle.Bold)
        If txtUnit.Text.Equals("Ruang", StringComparison.OrdinalIgnoreCase) Then
            txtUnit.Text = String.Empty
        End If
    End Sub

    Private Sub txtUnit_LostFocus(sender As Object, e As EventArgs) Handles txtUnit.LostFocus
        'txtUnit.ForeColor = Color.Gray
        'txtUnit.Font = New Font("Tahoma", 12, FontStyle.Italic)
        'If String.IsNullOrEmpty(txtUnit.Text) Then
        '    txtUnit.Text = "Ruang"
        'End If
    End Sub

    Private Sub btnLogin_MouseEnter(sender As Object, e As EventArgs) Handles btnLogin.MouseEnter
        btnLogin.BackgroundImage = My.Resources.btn_greenv2
    End Sub

    Private Sub btnLogin_MouseLeave(sender As Object, e As EventArgs) Handles btnLogin.MouseLeave
        btnLogin.BackgroundImage = My.Resources.btn_green
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            Call koneksiServer()
            Dim str As String
            str = "SELECT
	                    t_pemakai.username,
	                    t_pemakai.password,
                        t_pemakai.namaUser,
	                    t_aksesmenu.loket_unit 
                    FROM
	                    t_aksesmenu
	                    INNER JOIN t_pemakai ON t_pemakai.username = t_aksesmenu.username 
                    WHERE t_pemakai.username = '" & txtUsername.Text & "' 
                      AND t_pemakai.password = '" & txtPass.Text & "' 
                      AND t_aksesmenu.loket_unit = '" & txtUnit.Text & "'"
            cmd = New MySqlCommand(str, conn)
            dr = cmd.ExecuteReader
            If dr.HasRows Then
                'MessageBox.Show("Login berhasil", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                While dr.Read
                    Form1.Label1.Text = dr.GetString("loket_unit")
                    Form1.txtUser.Text = dr.GetString("namaUser")
                End While

                If Form1.Label1.Text.Equals("Gizi", StringComparison.OrdinalIgnoreCase) Then
                    Form1.Show()
                    Form1.btnCariReg.Enabled = False
                    Form1.btnBatalOut.Enabled = False
                    Form1.btnPilihRuang.Enabled = False
                    Form1.btnHasilLis.Enabled = False
                    Me.Hide()
                Else
                    Form1.Show()
                    Form1.btnCariReg.Enabled = True
                    Form1.btnHasilLis.Enabled = True
                    Form1.btnGizi.Enabled = False
                    Me.Hide()
                End If


            Else
                dr.Close()
                MessageBox.Show("Login gagal, username atau Password salah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtPass.Text = ""
                txtUsername.Text = ""
                txtUsername.Focus()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtUsername_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUsername.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtPass_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPass.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtUnit_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUnit.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub
End Class
