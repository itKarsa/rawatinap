Public Class Hasil_Rad

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Dim noRm, noPerm, noHasil, idDetail As String

    Private Sub Hasil_Rad_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Hasil"
                    noRm = Radiologi.txtNoRM.Text
                    noPerm = Radiologi.txtNoPerm.Text
                    noHasil = Radiologi.txtNoHasil.Text
                    idDetail = Radiologi.txtIdDetail.Text
            End Select
        End If
    End Sub
End Class