Imports MySql.Data.MySqlClient
Public Class FormTindakan

    Public Ambil_Data As String
    Public Form_Ambil_Data As String

    Dim table As New DataTable

    Sub tampilDataTindakan()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        If txtRuang.Text.Contains("UNIT STROKE") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND (kdKelompokTindakan IN (02,05,34,35,36,37,38,45,48,06,09,12,62,63) OR kdTarif IN (491110,491410,491510))
                        AND kdTarif NOT IN (020910,020920,020930,020940,020950,020960,020970)
                        OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                     ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("KRISAN") Then
            query = "SELECT * 
                           FROM vw_caritindakan 
                          WHERE kelas = '" & txtKelas.Text & "' 
                            AND (kdKelompokTindakan IN (01,02,05,34,35,36,37,38,45,03,08,62,63) OR kdTarif IN (491110,491410,491510,061510,061520,061530,061540))
                            AND kdTarif NOT IN (020910,020920,020930,020940,020950,020960,020970)
                            OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                         ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Contains("MATAHARI") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND (kdKelompokTindakan IN (02,05,10,34,35,36,37,38,45,03,07,62,63,75) 
                            OR kdTarif IN (491110,491410,491510,061510,061520,061530,061540) 
                            OR kdTindakan IN (3000009,3000011,3000015,3000018,3000019))
                        AND kdTarif NOT IN (020910,020920,020930,020940,020950,020960,020970)
                         OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("PERINATOLOGI - NICU", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I' 
                        AND (kdKelompokTindakan IN (05,10,13,49,62,63,81) 
                            OR kdTarif IN (496310) 
                            OR kdTindakan IN (3000009,3000011,3000015,3000018,3000019))
                        AND kdTarif NOT IN (490110)
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Contains("PERINATOLOGI") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I' 
                        AND (kdKelompokTindakan IN (05,10,13,49,62,63,82) OR kdTindakan IN (3000009,3000011,3000015,3000018,3000019))
                        AND kdTarif NOT IN (0200002,020910,020920,020930,020940,020950,020960,020970)
                         OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("SERUNI A - HCU", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND (kdKelompokTindakan IN (05,34,35,36,37,38,10,45,48,62,63) 
                            OR kdTarif IN (491110,491410,491510) 
                            OR kdTindakan IN (3000009,3000011,3000015,3000018,3000019)) 
                        AND kdTarif NOT IN (020910,020920,020930,020940,020950,020960,020970)
                         OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("SERUNI A - PICU", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND (kdKelompokTindakan IN (05,34,35,36,37,38,10,45,49,62,63) 
                            OR kdTarif IN (491110,491410,491510) 
                            OR kdTindakan IN (3000009,3000011,3000015,3000018,3000019)) 
                        AND kdTarif NOT IN (020910,020920,020930,020940,020950,020960,020970)
                         OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Contains("SERUNI") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND (kdKelompokTindakan IN (02,05,34,35,36,37,38,10,45,62,63) 
                            OR kdTarif IN (491110,491410,491510,061510,061520,061530,061540) 
                            OR kdTindakan IN (3000009,3000011,3000015,3000018,3000019)) 
                        AND kdTarif NOT IN (020910,020920,020930,020940,020950,020960,020970)
                         OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Contains("TERATAI") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND (kdKelompokTindakan IN (02,05,34,35,36,37,38,45,03,04,06,07,08,09,12,40,41,42,62,63) OR kdTarif IN (491110,491410,491510))
                        AND kdTarif NOT IN (020910,020920,020930,020940,020950,020960,020970)
                         OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("ICU", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I' 
                        AND (kdKelompokTindakan IN (38,49,62,63) 
                            OR kdTarif IN (496310,061510,061520,061530,061540) 
                            OR kdTindakan IN (3000009,3000011,3000015,3000018,3000019))
                        AND kdTarif NOT IN (490110)
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("ICU - HCU", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I' 
                        AND (kdKelompokTindakan IN (38,48,62,63) 
                            OR kdTarif IN (486210,061510,061520,061530,061540) 
                            OR kdTindakan IN (3000009,3000011,3000015,3000018,3000019))
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("CVCU", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I' 
                        AND (kdKelompokTindakan IN (02,05,10,34,35,36,37,38,45,48,49,62,63,77) 
                            OR kdTarif IN (486210,061510,061520,061530,061540,020510,020520,020530) 
                            OR kdTindakan IN (3000009,3000011,3000015,3000018,3000019))
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Contains("IGD") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND kdKelompokTindakan IN (01,02,05,34,35,36,37,38,45,66,62,63)
                        AND kdTarif NOT IN (020910,020920,020930,020940,020950,020960,020970)
                         OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text = "AMARILIS" Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND (kdKelompokTindakan IN (02,05,34,35,36,37,38,45,62,63) 
                            OR kdTarif IN (491110,491410,491510,061510,061520,061530,061540)
                            OR kdTindakan IN (3000009,3000011,3000015,3000018,3000019))
                        AND kdTarif NOT IN (020910,020920,020930,020940,020950,020960,020970)
                         OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("LAVENDER - ISOLASI TEKANAN NEGATIF", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I' 
                        AND (kdKelompokTindakan IN (38,48,56,62,63,73,82) OR kdTarif IN (486210,663030,061510,061520,061530,061540))
                        AND kdTarif NOT IN (490110)
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("LAVENDER - ICU ISOLASI TEKANAN NEGATIF", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I' 
                        AND (kdKelompokTindakan IN (38,49,56,62,63,73,81) OR kdTarif IN (496310,663030,061510,061520,061530,061540))
                        AND kdTarif NOT IN (490110)
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("KEMUNING - ISOLASI TEKANAN NEGATIF", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I' 
                        AND (kdKelompokTindakan IN (02,03,05,07,10,38,48,56,62,63,73,82) OR kdTarif IN (486210,663030,061510,061520,061530,061540))
                        AND kdTarif NOT IN (490110)
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("KEMUNING - ISOLASI NATURAL FLOW", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I' 
                        AND (kdKelompokTindakan IN (02,03,05,07,10,38,48,56,62,63,73,82) OR kdTarif IN (486210,663030,061510,061520,061530,061540))
                        AND kdTarif NOT IN (490110)
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("KRISAN - ISOLASI NATURAL FLOW", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I' 
                        AND (kdKelompokTindakan IN (38,48,56,62,63,73,82) OR kdTarif IN (486210,663030,061510,061520,061530,061540))
                        AND kdTarif NOT IN (490110)
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("DAHLIA - ISOLASI NATURAL FLOW", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I' 
                        AND (kdKelompokTindakan IN (38,48,49,56,62,63,73,82) OR kdTarif IN (486210,663030,061510,061520,061530,061540))
                        AND kdTarif NOT IN (490110)
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        Else
            query = "SELECT * 
                               FROM vw_caritindakan 
                              WHERE kelas = '" & txtKelas.Text & "' 
                                AND (kdKelompokTindakan IN (02,05,34,35,36,37,38,48,49,45,62,63) OR kdTarif IN (491110,491410,491510,061510,061520,061530,061540))
                                AND kdTarif NOT IN (020910,020920,020930,020940,020950,020960,020970)
                                 OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                              ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        End If
        'MsgBox(query)
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            DataGridView1.Rows.Clear()
            Do While dr.Read
                DataGridView1.Rows.Add(dr.Item("kdTarif"), dr.Item("kelas"), dr.Item("kelompokTindakan"),
                                       dr.Item("tindakan"), dr.Item("tarif"), dr.Item("kdKelompokTindakan"))
            Loop
            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub caridataTindakan()
        Call koneksiServer()
        Dim query As String = ""
        Dim cmd As MySqlCommand
        Dim dr As MySqlDataReader
        If txtRuang.Text.Contains("UNIT STROKE") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "'
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan in (02,05,34,35,36,37,38,45,48,06,09,12,62,63) OR kdTarif IN (491110,491410,491510))
                        AND kdTarif NOT IN (020910,020920,020930,020940,020950,020960,020970)
                         OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("KRISAN") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan in (02,05,34,35,36,37,38,45,03,08,62,63) OR kdTarif IN (491110,491410,491510,061510,061520,061530,061540))
                        AND kdTarif NOT IN (020910,020920,020930,020940,020950,020960,020970)
                         OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Contains("MATAHARI") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan in (02,05,10,34,35,36,37,38,45,03,07,62,63,75) 
                            OR kdTarif IN (491110,491410,491510,061510,061520,061530,061540) 
                            OR kdTindakan IN (3000009,3000011,3000015,3000018,3000019))
                        AND kdTarif NOT IN (020910,020920,020930,020940,020950,020960,020970)
                         OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("PERINATOLOGI - NICU", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan IN (05,10,13,49,62,63) 
                            OR kdTarif IN (496310) 
                            OR kdTindakan IN (3000009,3000011,3000015,3000018,3000019))
                        AND kdTarif NOT IN (490110)
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Contains("PERINATOLOGI") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan IN (05,10,13,62,63) OR kdTindakan IN (3000009,3000011,3000015,3000018,3000019))
                        AND kdTarif NOT IN (0200002,020910,020920,020930,020940,020950,020960,020970)
                         OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("SERUNI A - HCU", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan in (05,34,35,36,37,38,10,45,48,62,63) 
                            OR kdTarif IN (491110,491410,491510) 
                            OR kdTindakan IN (3000009,3000011,3000015,3000018,3000019))
                        AND kdTarif NOT IN (020910,020920,020930,020940,020950,020960,020970)
                         OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("SERUNI A - PICU", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan in (05,34,35,36,37,38,10,45,49,62,63) 
                            OR kdTarif IN (491110,491410,491510) 
                            OR kdTindakan IN (3000009,3000011,3000015,3000018,3000019))
                        AND kdTarif NOT IN (020910,020920,020930,020940,020950,020960,020970)
                         OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Contains("SERUNI") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan in (02,05,34,35,36,37,38,10,45,62,63) 
                            OR kdTarif IN (491110,491410,491510,061510,061520,061530,061540) 
                            OR kdTindakan IN (3000009,3000011,3000015,3000018,3000019))
                        AND kdTarif NOT IN (020910,020920,020930,020940,020950,020960,020970)
                         OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Contains("TERATAI") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan in (02,05,34,35,36,37,38,45,03,04,06,07,08,09,12,40,41,42,62,63) OR kdTarif IN (491110,491410,491510))
                        AND kdTarif NOT IN (020910,020920,020930,020940,020950,020960,020970)
                         OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("ICU", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan IN (38,49,62,63) 
                            OR kdTarif IN (496310,061510,061520,061530,061540) 
                            OR kdTindakan IN (3000009,3000011,3000015,3000018,3000019)) 
                        AND kdTarif NOT IN (490110)
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("ICU - HCU", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I'  
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan IN (38,48,62,63) 
                            OR kdTarif IN (486210,061510,061520,061530,061540) 
                            OR kdTindakan IN (3000009,3000011,3000015,3000018,3000019))
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("CVCU", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I'  
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan IN (02,05,10,34,35,36,37,38,45,48,49,62,63,77)
                            OR kdTarif IN (486210,061510,061520,061530,061540,020510,020520,020530) 
                            OR kdTindakan IN (3000009,3000011,3000015,3000018,3000019))
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Contains("IGD") Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND kdKelompokTindakan in (01,02,05,34,35,36,37,38,45,66,62,63)
                        AND kdTarif NOT IN (020910,020920,020930,020940,020950,020960,020970)
                         OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                   ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text = "AMARILIS" Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan IN (02,05,34,35,36,37,38,45,62,63) 
                            OR kdTarif IN (491110,491410,491510,061510,061520,061530,061540) 
                            OR kdTindakan IN (3000009,3000011,3000015,3000018,3000019))                       
                        AND kdTarif NOT IN (020910,020920,020930,020940,020950,020960,020970)
                         OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("LAVENDER - ISOLASI TEKANAN NEGATIF", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan IN (38,48,56,62,63,73,82) OR kdTarif IN (486210,663030,061510,061520,061530,061540))
                        AND kdTarif NOT IN (490110)
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("LAVENDER - ICU ISOLASI TEKANAN NEGATIF", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan IN (38,49,56,62,63,73,81) OR kdTarif IN (496310,663030,061510,061520,061530,061540))
                        AND kdTarif NOT IN (490110)
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("LAVENDER - ICU ISOLASI TEKANAN NEGATIF", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan IN (38,49,56,62,63,73,81) OR kdTarif IN (496310,663030,061510,061520,061530,061540))
                        AND kdTarif NOT IN (490110)
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("KEMUNING - ISOLASI TEKANAN NEGATIF", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I'
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan IN (02,03,05,07,10,38,48,56,62,63,73,82) OR kdTarif IN (486210,663030,061510,061520,061530,061540))
                        AND kdTarif NOT IN (490110)
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("KEMUNING - ISOLASI NATURAL FLOW", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I'
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan IN (02,03,05,07,10,38,48,56,62,63,73,82) OR kdTarif IN (486210,663030,061510,061520,061530,061540))
                        AND kdTarif NOT IN (490110)
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("KRISAN - ISOLASI NATURAL FLOW", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I'
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan IN (38,48,56,62,63,73,82) OR kdTarif IN (486210,663030,061510,061520,061530,061540))
                        AND kdTarif NOT IN (490110)
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        ElseIf txtRuang.Text.Equals("DAHLIA - ISOLASI NATURAL FLOW", StringComparison.OrdinalIgnoreCase) Then
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = 'KELAS I'
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan IN (38,48,49,56,62,63,73,82) OR kdTarif IN (486210,663030,061510,061520,061530,061540))
                        AND kdTarif NOT IN (490110)
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        Else
            query = "SELECT * 
                       FROM vw_caritindakan 
                      WHERE kelas = '" & txtKelas.Text & "' 
                        AND tindakan like '%" & txtCari.Text & "%'
                        AND (kdKelompokTindakan in (02,05,34,35,36,37,38,48,49,45,62,63) OR kdTarif IN (491110,491410,491510,061510,061520,061530,061540))
                        AND kdTarif NOT IN (020910,020920,020930,020940,020950,020960,020970)
                         OR (tindakan LIKE '%PLEBOTO%' AND kelas = '" & txtKelas.Text & "')
                      ORDER BY kdKelompokTindakan ASC, tindakan ASC"
        End If
        'MsgBox(query)
        Try
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            DataGridView1.Rows.Clear()
            Do While dr.Read
                DataGridView1.Rows.Add(dr.Item("kdTarif"), dr.Item("kelas"), dr.Item("kelompokTindakan"),
                                       dr.Item("tindakan"), dr.Item("tarif"), dr.Item("kdKelompokTindakan"))
            Loop
            dr.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub transferSelected()
        Dim row As New System.Windows.Forms.DataGridViewRow
        'Dim count As Integer
        'count = dgvDetail.Rows.Count

        For Each row In Me.DataGridView1.SelectedRows
            dgvDetail.Rows.Add(1)
            'dgvDetail.Rows(dgvDetail.RowCount - 1).Cells(0).Value = count + 1
            dgvDetail.Rows(dgvDetail.RowCount - 1).Cells(1).Value = txtNoTindak.Text
            dgvDetail.Rows(dgvDetail.RowCount - 1).Cells(2).Value = row.Cells(0).Value.ToString
            dgvDetail.Rows(dgvDetail.RowCount - 1).Cells(3).Value = row.Cells(3).Value.ToString
            dgvDetail.Rows(dgvDetail.RowCount - 1).Cells(4).Value = row.Cells(4).Value.ToString
            dgvDetail.Rows(dgvDetail.RowCount - 1).Cells(5).Value = 1
            'dgvDetail.Rows(dgvDetail.RowCount - 1).Cells(6).Value = ""
            'dgvDetail.Rows(dgvDetail.RowCount - 1).Cells(7).Value = ""
            dgvDetail.Rows(dgvDetail.RowCount - 1).Cells(8).Value = ""
            dgvDetail.Rows(dgvDetail.RowCount - 1).Cells(9).Value = "-"
            dgvDetail.Rows(dgvDetail.RowCount - 1).Cells(10).Value = dateTindakan.Text
            dgvDetail.Rows(dgvDetail.RowCount - 1).Cells(11).Value = Val(row.Cells(4).Value * 1).ToString
            dgvDetail.Update()
        Next
    End Sub

    Sub aturDGV()
        Try
            DataGridView1.Columns(0).Width = 70
            DataGridView1.Columns(1).Width = 70
            DataGridView1.Columns(2).Width = 200
            DataGridView1.Columns(3).Width = 280
            DataGridView1.Columns(4).Width = 100
            DataGridView1.Columns(5).Width = 50
            DataGridView1.Columns(0).HeaderText = "KODE"
            DataGridView1.Columns(1).HeaderText = "KELAS"
            DataGridView1.Columns(2).HeaderText = "KELOMPOK TINDAKAN"
            DataGridView1.Columns(3).HeaderText = "TINDAKAN"
            DataGridView1.Columns(4).HeaderText = "TARIF"
            DataGridView1.Columns(5).HeaderText = "KODE"
        Catch ex As Exception

        End Try
    End Sub

    Sub totalTarif()
        Dim totTarif As Long
        totTarif = 0
        For i As Integer = 0 To dgvDetail.Rows.Count - 1
            totTarif = totTarif + Val(dgvDetail.Rows(i).Cells(11).Value)
        Next
        txtTotTarif.Text = "Rp. " & totTarif.ToString("#,##0")
        txtTotal.Text = totTarif
    End Sub

    Sub aturDGVTindakan()
        Try
            dgvDetail.Columns(4).DefaultCellStyle.Format = "###,###,###"
            dgvDetail.Columns(11).DefaultCellStyle.Format = "###,###,###"
            dgvDetail.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvDetail.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            dgvDetail.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Catch ex As Exception

        End Try
    End Sub

    Sub autoNoTindak()
        Dim noTindakanRanap As String

        Try
            conn.Close()
            Call koneksiServer()
            Dim query As String
            dr.Close()
            query = "SELECT SUBSTR(noTindakanPasienRanap,17) FROM t_tindakanpasienranap ORDER BY CAST(SUBSTR(noTindakanPasienRanap,17) AS UNSIGNED) DESC LIMIT 1"
            cmd = New MySqlCommand(query, conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                dr.Read()
                noTindakanRanap = "TRI" + Format(Now, "ddMMyyHHmmss") + "-" + (Val(Trim(dr.Item(0).ToString)) + 1).ToString
                txtNoTindak.Text = noTindakanRanap
            Else
                noTindakanRanap = "TRI" + Format(Now, "ddMMyyHHmmss") + "-1"
                txtNoTindak.Text = noTindakanRanap
            End If
            conn.Close()
        Catch ex As Exception

        End Try

    End Sub

    Sub addDetail()

        conn.Close()
        Call koneksiServer()
        Dim val1, val2, val3, val4, val5, val11, val8 As String

        Dim str As String
        Dim cmd As MySqlCommand
        str = "insert into t_detailtindakanpasienranap
                                    (noTindakanPasienRanap,kdTarif,tindakan,tarif,
                                     jumlahTindakan,totalTarif,kdTenagaMedis) 
                             values (@noTindakanPasienRanap,@kdTarif,@tindakan,@tarif,
                                     @jumlahTindakan,@totalTarif,@kdTenagaMedis)"
        cmd = New MySqlCommand(str, conn)

        Try
            For i As Integer = 0 To dgvDetail.Rows.Count - 1
                val1 = dgvDetail.Rows(i).Cells(1).Value
                val2 = dgvDetail.Rows(i).Cells(2).Value
                val3 = dgvDetail.Rows(i).Cells(3).Value
                val4 = dgvDetail.Rows(i).Cells(4).Value
                val5 = dgvDetail.Rows(i).Cells(5).Value
                val11 = dgvDetail.Rows(i).Cells(11).Value
                val8 = dgvDetail.Rows(i).Cells(8).Value

                cmd.Parameters.AddWithValue("@noTindakanPasienRanap", val1)
                cmd.Parameters.AddWithValue("@kdTarif", val2)
                cmd.Parameters.AddWithValue("@tindakan", val3)
                cmd.Parameters.AddWithValue("@tarif", val4)
                cmd.Parameters.AddWithValue("@jumlahTindakan", val5)
                cmd.Parameters.AddWithValue("@totalTarif", val11)
                cmd.Parameters.AddWithValue("@kdTenagaMedis", val8)
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
            Next
            'MsgBox("Insert data Detail-1 Tindakan berhasil dilakukan", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "ERROR DETAIL-1")
            cmd.Dispose()
        End Try
        conn.Close()
    End Sub

    Sub addTindakan()
        Dim dt As String
        dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

        conn.Close()
        Call koneksiServer()
        Try
            Dim str As String
            Dim cmd As MySqlCommand
            str = "insert into t_tindakanpasienranap(
                                    noTindakanPasienRanap,noDaftarRawatInap,
                                    tglTindakan,totalTarifTindakan,statusPembayaran,
                                    userModify,dateModify) 
                   values ('" & txtNoTindak.Text & "','" & txtNoDaftarRanap.Text & "',
                           '" & Format(dateTindakan.Value, "yyyy-MM-dd HH:mm:ss") & "','" & txtTotal.Text & "','BELUM LUNAS',
                           '" & LoginForm.txtUsername.Text.ToUpper & "','" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "')"
            cmd = New MySqlCommand(str, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Insert data Tindakan berhasil dilakukan", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox("Insert data Tindakan gagal dilakukan.", MsgBoxStyle.Critical)
        End Try
        conn.Close()
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If dgvDetail.Rows.Count = 0 Then
            MsgBox("Masukkan tindakan terlebih dahulu !!", MsgBoxStyle.Exclamation)
        Else
            Call addTindakan()
            Call addDetail()

            Me.Close()
            Form1.Show()
            Call Form1.tampilDataTindakan()
        End If
    End Sub

    Private Sub FormTindakan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.Manual
        With Screen.PrimaryScreen.WorkingArea
            Me.SetBounds(.Left, .Top, .Width, .Height)
        End With

        'TODO: This line of code loads data into the 'SimrsDev.vw_dokter' table. You can move, or remove it, as needed.
        dateTindakan.Format = DateTimePickerFormat.Custom
        dateTindakan.CustomFormat = "yyyy-MM-dd HH:mm:ss"

        If Ambil_Data = True Then
            Select Case Form_Ambil_Data
                Case "Tindakan"
                    txtNoDaftarRanap.Text = Form1.txtRegRanap.Text
                    txtKelas.Text = Form1.txtKelas.Text
                    txtRuang.Text = Form1.txtUnitRanap.Text
                    txtDokter.Text = Form1.comboDokter.Text
                    txtNamaPasien.Text = Form1.txtNamaPasien.Text
                    Label1.Text = "Form Tindakan Pasien Rawat Inap a.n " & Form1.txtNamaPasien.Text
                    SplitContainer2.Panel2Collapsed = True
                    Call tampilDataTindakan()
                Case "Edit Tindakan"
                    txtNoDaftarRanap.Text = Form1.txtRegRanap.Text
                    txtKelas.Text = Form1.txtKelas.Text
                    txtRuang.Text = Form1.txtUnitRanap.Text
                    txtDokter.Text = Form1.comboDokter.Text
                    txtNamaPasien.Text = Form1.txtNamaPasien.Text
                    Label1.Text = "Form Tindakan Pasien Rawat Inap a.n " & Form1.txtNamaPasien.Text
                    SplitContainer2.Panel2Collapsed = False
                    Call tampilDataTindakan()
            End Select
        End If

        txtCari.Select()
        btnSimpan.Enabled = False

        'Call tampilDataTindakan()
        'Call aturDGV()

        'Call tampilDataTindakan()
        'Call aturDGVTindakan()
        Call autoNoTindak()
        Call totalTarif()

        'If Form1.Label1.Text.Contains("Lavender") Then
        '    Label6.Visible = True
        '    txtKelas.Visible = True
        'ElseIf Form1.Label1.Text.Contains("Amarilis") Then
        '    Label6.Visible = True
        '    txtKelas.Visible = True
        'Else
        '    Label6.Visible = False
        '    txtKelas.Visible = False
        'End If

    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        If dgvDetail.Rows.Count = 0 Then
            Me.Close()
            Form1.Show()
        Else
            Dim konfirmasi As MsgBoxResult
            konfirmasi = MsgBox("Apakah anda yakin tindakan sudah disimpan ?", vbQuestion + vbYesNo, "Konfirmasi")
            If konfirmasi = vbYes Then
                Me.Close()
                Form1.Show()
            End If
        End If
    End Sub

    Private Sub btnCariTindakan_Click(sender As Object, e As EventArgs) Handles btnCariTindakan.Click
        'Daftar_Tindakan_Perawatan.Ambil_Data = True
        'Daftar_Tindakan_Perawatan.Form_Ambil_Data = "Daftar Tindakan"
        'Daftar_Tindakan_Perawatan.Show()

        If SplitContainer1.Panel1Collapsed = False Then
            SplitContainer1.Panel1Collapsed = True
        Else
            SplitContainer1.Panel1Collapsed = False
        End If
    End Sub

    Private Sub btnCariTindakan_KeyDown(sender As Object, e As KeyEventArgs) Handles btnCariTindakan.KeyDown
        If e.KeyCode = Keys.Enter Then
            Daftar_Tindakan_Perawatan.Ambil_Data = True
            Daftar_Tindakan_Perawatan.Form_Ambil_Data = "Daftar Tindakan"
            Daftar_Tindakan_Perawatan.Show()
        End If
    End Sub

    Private Sub dgvDetail_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles dgvDetail.RowsAdded
        Call totalTarif()
    End Sub

    Private Sub dgvDetail_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles dgvDetail.RowsRemoved
        Call totalTarif()
    End Sub

    Private Sub TextBox_keyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub dgvDetail_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgvDetail.EditingControlShowing
        Select Case dgvDetail.CurrentCell.ColumnIndex
            'Case Column5.Index
            '    'AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
            '    Dim tb As TextBox = TryCast(e.Control, TextBox)
            '    If tb IsNot Nothing Then
            '        AddHandler tb.KeyPress, AddressOf TextBox_keyPress
            '    End If
            'Case Column7.Index
            '    'MsgBox("DPJP")
            '    Dim autoText1 As TextBox = TryCast(e.Control, TextBox)
            '    autoText1.AutoCompleteMode = AutoCompleteMode.Suggest
            '    autoText1.AutoCompleteSource = AutoCompleteSource.CustomSource
            '    Dim dataDokter As New AutoCompleteStringCollection()
            '    If Column7 IsNot Nothing Then
            '        'MsgBox("Autocomplete DPJP")
            '        addItemsDokter(dataDokter)
            '        autoText1.AutoCompleteCustomSource = dataDokter
            '    End If
            Case Column9.Index
                'MsgBox("PPA")
                Dim autoText2 As TextBox = TryCast(e.Control, TextBox)
                autoText2.AutoCompleteMode = AutoCompleteMode.Suggest
                autoText2.AutoCompleteSource = AutoCompleteSource.CustomSource
                Dim dataPerawat As New AutoCompleteStringCollection()
                If Column9 IsNot Nothing Then
                    'MsgBox("Autocomplete PPA")
                    addItemsPerawat(dataPerawat)
                    autoText2.AutoCompleteCustomSource = dataPerawat
                End If
        End Select
    End Sub

    Private Sub addItemsDokter(colDokter As AutoCompleteStringCollection)
        Call koneksiServer()
        Try
            Dim query1 As String
            Dim dr1 As MySqlDataReader
            Dim cmd1 As MySqlCommand
            query1 = "SELECT  namapetugasMedis FROM t_tenagamedis2 WHERE kdKelompokTenagaMedis = 'ktm1' ORDER BY namapetugasMedis ASC"
            cmd1 = New MySqlCommand(query1, conn)
            dr1 = cmd1.ExecuteReader

            While dr1.Read
                colDokter.Add(dr1.GetString("namapetugasMedis"))
            End While
            dr1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub addItemsPerawat(colPerawat As AutoCompleteStringCollection)
        Call koneksiServer()
        Try
            Dim query2 As String
            Dim dr2 As MySqlDataReader
            Dim cmd2 As MySqlCommand
            query2 = "SELECT namapetugasMedis FROM t_tenagamedis2 WHERE kdKelompokTenagaMedis in ('ktm1','ktm4','ktm11','ktm15') ORDER BY namapetugasMedis ASC"
            cmd2 = New MySqlCommand(query2, conn)
            dr2 = cmd2.ExecuteReader

            While dr2.Read
                colPerawat.Add(dr2.GetString("namapetugasMedis"))
            End While
            dr2.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub dgvDetail_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDetail.CellValueChanged
        Dim drKd As MySqlDataReader
        Dim cmdKd As MySqlCommand
        For i As Integer = 0 To dgvDetail.RowCount - 1
            If dgvDetail.Rows(i).Cells(5).Value IsNot Nothing Then
                Dim subTotal As Integer
                subTotal = Integer.Parse(dgvDetail.Rows(i).Cells(5).Value) * Integer.Parse(dgvDetail.Rows(i).Cells(4).Value)
                dgvDetail.Rows(i).Cells(11).Value = subTotal
                Call totalTarif()
            End If
            If dgvDetail.Rows(i).Cells(7).Value = "" Then
                dgvDetail.Rows(i).Cells(7).Value = txtDokter.Text
            End If
            If dgvDetail.Rows(i).Cells(9).Value <> "" Then
                Dim perawat As String = dgvDetail.Rows(i).Cells(9).Value.ToString
                Dim queryPpa As String
                Try
                    'dr.Close()
                    'conn.Close()
                    Call koneksiServer()
                    queryPpa = "SELECT * FROM t_tenagamedis2 WHERE namapetugasMedis = '" & perawat & "'"
                    cmdKd = New MySqlCommand(queryPpa, conn)
                    drKd = cmdKd.ExecuteReader
                    While drKd.Read
                        dgvDetail.Rows(i).Cells(8).Value = drKd.GetString("kdPetugasMedis")
                    End While
                    drKd.Close()
                    conn.Close()
                Catch ex As Exception
                    'MessageBox.Show(ex.Message)
                End Try
            Else
                dgvDetail.Rows(i).Cells(9).Value = "-"
            End If
        Next
    End Sub

    Private Sub dgvDetail_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvDetail.RowPostPaint
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

    Private Sub dgvDetail_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvDetail.CellFormatting
        dgvDetail.Columns(4).DefaultCellStyle.Format = "###,###,###"
        dgvDetail.Columns(11).DefaultCellStyle.Format = "###,###,###"
        dgvDetail.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvDetail.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvDetail.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        For i As Integer = 0 To dgvDetail.Rows.Count - 1
            If i Mod 2 = 0 Then
                dgvDetail.Rows(i).DefaultCellStyle.BackColor = Color.White
            Else
                dgvDetail.Rows(i).DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        Next

        For i As Integer = 0 To dgvDetail.Rows.Count - 1
            If dgvDetail.Rows(i).Cells(7).Value = "-" Then
                dgvDetail.Rows(i).Cells(7).Style.BackColor = Color.FromArgb(255, 112, 112)
                dgvDetail.Rows(i).Cells(7).Style.ForeColor = Color.White
            Else
                If i Mod 2 = 0 Then
                    dgvDetail.Rows(i).Cells(7).Style.BackColor = Color.White
                    dgvDetail.Rows(i).Cells(7).Style.ForeColor = Color.Black
                Else
                    dgvDetail.Rows(i).Cells(7).Style.BackColor = Color.WhiteSmoke
                    dgvDetail.Rows(i).Cells(7).Style.ForeColor = Color.Black
                End If
            End If
        Next

        For i As Integer = 0 To dgvDetail.Rows.Count - 1
            If dgvDetail.Rows(i).Cells(9).Value = "-" Then
                dgvDetail.Rows(i).Cells(9).Style.BackColor = Color.FromArgb(255, 112, 112)
                dgvDetail.Rows(i).Cells(9).Style.ForeColor = Color.White
            Else
                If i Mod 2 = 0 Then
                    dgvDetail.Rows(i).Cells(9).Style.BackColor = Color.White
                    dgvDetail.Rows(i).Cells(9).Style.ForeColor = Color.Black
                Else
                    dgvDetail.Rows(i).Cells(9).Style.BackColor = Color.WhiteSmoke
                    dgvDetail.Rows(i).Cells(9).Style.ForeColor = Color.Black
                End If
            End If
        Next
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.RowIndex > 0 And e.ColumnIndex = 2 Then
            If DataGridView1.Item(2, e.RowIndex - 1).Value = e.Value Then
                e.Value = ""
            End If
        End If

        'If txtRuang.Text.Contains("Amarilis") Then
        '    DataGridView1.Columns(1).Visible = False
        'ElseIf txtRuang.Text.Contains("Lavender") Then
        '    DataGridView1.Columns(1).Visible = False
        'Else
        '    DataGridView1.Columns(1).Visible = True
        'End If

        'DataGridView1.Columns(2).Visible = False
        DataGridView1.Columns(5).Visible = False

        DataGridView1.Columns(4).DefaultCellStyle.Format = "###,###,###"
        DataGridView1.Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridView1.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

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

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter And DataGridView1.CurrentCell.RowIndex >= 0 Then
            e.Handled = True
            e.SuppressKeyPress = True

            Dim row As DataGridViewRow
            row = Me.DataGridView1.Rows(DataGridView1.CurrentCell.RowIndex)

            If DataGridView1.CurrentCell.RowIndex = -1 Then
                Return
            End If

            Call transferSelected()
            btnSimpan.Enabled = True
        End If
    End Sub

    Private Sub txtCari_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCari.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtCari.Text = "" Then
                Call tampilDataTindakan()
                'Call aturDGV()
            Else
                Call caridataTindakan()
                'Call aturDGV()
            End If
        End If
    End Sub

    Private Sub btnPilihOk_Click(sender As Object, e As EventArgs) Handles btnPilihOk.Click
        Call transferSelected()
        Me.btnSimpan.Enabled = True
    End Sub

    Private Sub txtPilihBatal_Click(sender As Object, e As EventArgs) Handles btnPilihBatal.Click
        Dim drDgv As New DataGridViewRow
        For Each drDgv In Me.dgvDetail.SelectedRows
            dgvDetail.Rows.Remove(drDgv)
        Next

        If dgvDetail.Rows.Count = 0 Then
            Me.btnSimpan.Enabled = False
        End If
    End Sub

    Private Sub btnPilihOk_MouseLeave(sender As Object, e As EventArgs) Handles btnPilihOk.MouseLeave
        Me.btnPilihOk.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnPilihOk.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnPilihOk_MouseEnter(sender As Object, e As EventArgs) Handles btnPilihOk.MouseEnter
        Me.btnPilihOk.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnPilihOk.ForeColor = Color.White
    End Sub

    Private Sub btnPilihBatal_MouseLeave(sender As Object, e As EventArgs) Handles btnPilihBatal.MouseLeave
        Me.btnPilihBatal.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnPilihBatal.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnPilihBatal_MouseEnter(sender As Object, e As EventArgs) Handles btnPilihBatal.MouseEnter
        Me.btnPilihBatal.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnPilihBatal.ForeColor = Color.White
    End Sub

    Private Sub btnCariTindakan_MouseLeave(sender As Object, e As EventArgs) Handles btnCariTindakan.MouseLeave
        Me.btnCariTindakan.BackColor = Color.FromArgb(232, 243, 239)
        Me.btnCariTindakan.ForeColor = Color.FromArgb(26, 141, 95)
    End Sub

    Private Sub btnCariTindakan_MouseEnter(sender As Object, e As EventArgs) Handles btnCariTindakan.MouseEnter
        Me.btnCariTindakan.BackColor = Color.FromArgb(26, 141, 95)
        Me.btnCariTindakan.ForeColor = Color.White
    End Sub

    Private Sub dgvDetail_DataSourceChanged(sender As Object, e As EventArgs) Handles dgvDetail.DataSourceChanged
        If dgvDetail.Rows.Count = 0 Then
            Me.btnSimpan.Enabled = False
        Else
            Me.btnSimpan.Enabled = True
        End If
    End Sub

    Private Sub btnSimpan_DoubleClick(sender As Object, e As EventArgs) Handles btnSimpan.DoubleClick
        Return
    End Sub

    Private Sub txtKelas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtKelas.SelectedIndexChanged
        Call tampilDataTindakan()
    End Sub
End Class