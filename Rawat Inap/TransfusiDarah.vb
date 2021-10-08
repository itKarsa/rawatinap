Public Class TransfusiDarah

    Dim jenisReq As String

    Private Sub PopulateRadioButton()
        Dim str As String()
        str = {"Elektif", "Rutin", "Darurat"}

        For Each row As String In str
            Dim rd As RadioButton = New RadioButton()
            rd.Width = 80
            rd.Text = row.ToString()
            rd.ForeColor = Color.Black
            AddHandler rd.CheckedChanged, AddressOf RadioButton_Checked
            FlowLayoutPanel1.Controls.Add(rd)
        Next
    End Sub

    Private Sub RadioButton_Checked(ByVal sender As Object, ByVal e As EventArgs)
        Dim rd As RadioButton = (TryCast(sender, RadioButton))
        If rd.Checked Then
            jenisReq = rd.Text
            'MessageBox.Show("You selected: " & rd.Text)
        End If
    End Sub

    Private Sub TransfusiDarah_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class