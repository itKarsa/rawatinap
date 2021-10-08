<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Obat_Racikan
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Obat_Racikan))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtKetJenis = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtJmlHari = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtAturan = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtKdJenis = New System.Windows.Forms.Label()
        Me.txtTotalJenis = New System.Windows.Forms.TextBox()
        Me.txtSatuanJenis = New System.Windows.Forms.TextBox()
        Me.txtJmlJenis = New System.Windows.Forms.TextBox()
        Me.txtJenisRacikan = New System.Windows.Forms.ComboBox()
        Me.btnRacikan = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtHargaJenis = New System.Windows.Forms.TextBox()
        Me.txtNoDetail = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtKet = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtHargaObatDec = New System.Windows.Forms.Label()
        Me.txtStokObat = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtKdObat = New System.Windows.Forms.Label()
        Me.txtNoResep = New System.Windows.Forms.Label()
        Me.txtObat = New System.Windows.Forms.TextBox()
        Me.btnCariObat = New System.Windows.Forms.Button()
        Me.btnTambahObat = New System.Windows.Forms.Button()
        Me.txtTotalObat = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtHargaObat = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtSatuanObat = New System.Windows.Forms.TextBox()
        Me.txtJmlPakaiObat = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnTambahRacikan = New System.Windows.Forms.Button()
        Me.btnSelesai = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(733, 35)
        Me.Panel1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Control
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(733, 35)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Obat Racikan"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtKetJenis)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.txtJmlHari)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.txtAturan)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtKdJenis)
        Me.GroupBox1.Controls.Add(Me.txtTotalJenis)
        Me.GroupBox1.Controls.Add(Me.txtSatuanJenis)
        Me.GroupBox1.Controls.Add(Me.txtJmlJenis)
        Me.GroupBox1.Controls.Add(Me.txtJenisRacikan)
        Me.GroupBox1.Controls.Add(Me.btnRacikan)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtHargaJenis)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(360, 292)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " Jenis Racikan"
        '
        'txtKetJenis
        '
        Me.txtKetJenis.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtKetJenis.Location = New System.Drawing.Point(121, 147)
        Me.txtKetJenis.Multiline = True
        Me.txtKetJenis.Name = "txtKetJenis"
        Me.txtKetJenis.Size = New System.Drawing.Size(227, 68)
        Me.txtKetJenis.TabIndex = 42
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(6, 149)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(81, 15)
        Me.Label16.TabIndex = 41
        Me.Label16.Text = "Keterangan"
        '
        'txtJmlHari
        '
        Me.txtJmlHari.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtJmlHari.Location = New System.Drawing.Point(121, 123)
        Me.txtJmlHari.Name = "txtJmlHari"
        Me.txtJmlHari.Size = New System.Drawing.Size(102, 22)
        Me.txtJmlHari.TabIndex = 39
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(5, 125)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(85, 15)
        Me.Label24.TabIndex = 40
        Me.Label24.Text = "Jumlah Hari"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtAturan
        '
        Me.txtAturan.FormattingEnabled = True
        Me.txtAturan.Items.AddRange(New Object() {"1 x 1", "2 x 1", "3 x 1", "3 x 2", "4 x 1"})
        Me.txtAturan.Location = New System.Drawing.Point(121, 97)
        Me.txtAturan.Name = "txtAturan"
        Me.txtAturan.Size = New System.Drawing.Size(102, 24)
        Me.txtAturan.TabIndex = 38
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(4, 99)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 15)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "Aturan Pakai"
        '
        'txtKdJenis
        '
        Me.txtKdJenis.AutoSize = True
        Me.txtKdJenis.Location = New System.Drawing.Point(266, 53)
        Me.txtKdJenis.Name = "txtKdJenis"
        Me.txtKdJenis.Size = New System.Drawing.Size(63, 16)
        Me.txtKdJenis.TabIndex = 15
        Me.txtKdJenis.Text = "Label14"
        Me.txtKdJenis.Visible = False
        '
        'txtTotalJenis
        '
        Me.txtTotalJenis.Enabled = False
        Me.txtTotalJenis.Location = New System.Drawing.Point(121, 147)
        Me.txtTotalJenis.Name = "txtTotalJenis"
        Me.txtTotalJenis.Size = New System.Drawing.Size(155, 22)
        Me.txtTotalJenis.TabIndex = 10
        Me.txtTotalJenis.Visible = False
        '
        'txtSatuanJenis
        '
        Me.txtSatuanJenis.Enabled = False
        Me.txtSatuanJenis.Location = New System.Drawing.Point(121, 49)
        Me.txtSatuanJenis.Name = "txtSatuanJenis"
        Me.txtSatuanJenis.Size = New System.Drawing.Size(102, 22)
        Me.txtSatuanJenis.TabIndex = 9
        '
        'txtJmlJenis
        '
        Me.txtJmlJenis.Enabled = False
        Me.txtJmlJenis.Location = New System.Drawing.Point(121, 73)
        Me.txtJmlJenis.Name = "txtJmlJenis"
        Me.txtJmlJenis.Size = New System.Drawing.Size(102, 22)
        Me.txtJmlJenis.TabIndex = 8
        '
        'txtJenisRacikan
        '
        Me.txtJenisRacikan.DropDownWidth = 170
        Me.txtJenisRacikan.FormattingEnabled = True
        Me.txtJenisRacikan.Location = New System.Drawing.Point(121, 23)
        Me.txtJenisRacikan.Name = "txtJenisRacikan"
        Me.txtJenisRacikan.Size = New System.Drawing.Size(214, 24)
        Me.txtJenisRacikan.TabIndex = 6
        '
        'btnRacikan
        '
        Me.btnRacikan.BackColor = System.Drawing.Color.Navy
        Me.btnRacikan.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnRacikan.FlatAppearance.BorderSize = 2
        Me.btnRacikan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRacikan.ForeColor = System.Drawing.Color.White
        Me.btnRacikan.Location = New System.Drawing.Point(121, 258)
        Me.btnRacikan.Name = "btnRacikan"
        Me.btnRacikan.Size = New System.Drawing.Size(155, 33)
        Me.btnRacikan.TabIndex = 5
        Me.btnRacikan.Text = "Isi Racikan"
        Me.btnRacikan.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(6, 75)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(112, 15)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Jml. Permintaan"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(4, 149)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 15)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Total Harga"
        Me.Label5.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(6, 52)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 15)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Satuan"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(226, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 15)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Harga"
        Me.Label3.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(6, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 15)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Jenis Racikan"
        '
        'txtHargaJenis
        '
        Me.txtHargaJenis.Enabled = False
        Me.txtHargaJenis.Location = New System.Drawing.Point(229, 22)
        Me.txtHargaJenis.Name = "txtHargaJenis"
        Me.txtHargaJenis.Size = New System.Drawing.Size(119, 22)
        Me.txtHargaJenis.TabIndex = 7
        Me.txtHargaJenis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtHargaJenis.Visible = False
        '
        'txtNoDetail
        '
        Me.txtNoDetail.Enabled = False
        Me.txtNoDetail.Location = New System.Drawing.Point(13, 200)
        Me.txtNoDetail.Name = "txtNoDetail"
        Me.txtNoDetail.Size = New System.Drawing.Size(164, 22)
        Me.txtNoDetail.TabIndex = 14
        Me.txtNoDetail.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Enabled = False
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(10, 186)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(71, 15)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "No. Detail"
        Me.Label12.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtKet)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.txtHargaObatDec)
        Me.GroupBox2.Controls.Add(Me.txtStokObat)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.txtKdObat)
        Me.GroupBox2.Controls.Add(Me.txtNoResep)
        Me.GroupBox2.Controls.Add(Me.txtObat)
        Me.GroupBox2.Controls.Add(Me.btnCariObat)
        Me.GroupBox2.Controls.Add(Me.txtNoDetail)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.btnTambahObat)
        Me.GroupBox2.Controls.Add(Me.txtTotalObat)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.txtHargaObat)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.txtSatuanObat)
        Me.GroupBox2.Controls.Add(Me.txtJmlPakaiObat)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Enabled = False
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(369, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(361, 292)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Isi Racikan"
        '
        'txtKet
        '
        Me.txtKet.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtKet.Location = New System.Drawing.Point(109, 120)
        Me.txtKet.Multiline = True
        Me.txtKet.Name = "txtKet"
        Me.txtKet.Size = New System.Drawing.Size(184, 69)
        Me.txtKet.TabIndex = 35
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(10, 123)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(81, 15)
        Me.Label15.TabIndex = 34
        Me.Label15.Text = "Keterangan"
        '
        'txtHargaObatDec
        '
        Me.txtHargaObatDec.AutoSize = True
        Me.txtHargaObatDec.ForeColor = System.Drawing.Color.Black
        Me.txtHargaObatDec.Location = New System.Drawing.Point(225, 147)
        Me.txtHargaObatDec.Name = "txtHargaObatDec"
        Me.txtHargaObatDec.Size = New System.Drawing.Size(63, 16)
        Me.txtHargaObatDec.TabIndex = 33
        Me.txtHargaObatDec.Text = "Label14"
        Me.txtHargaObatDec.Visible = False
        '
        'txtStokObat
        '
        Me.txtStokObat.Enabled = False
        Me.txtStokObat.Location = New System.Drawing.Point(109, 48)
        Me.txtStokObat.Name = "txtStokObat"
        Me.txtStokObat.Size = New System.Drawing.Size(110, 22)
        Me.txtStokObat.TabIndex = 32
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(10, 51)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(35, 15)
        Me.Label14.TabIndex = 31
        Me.Label14.Text = "Stok"
        '
        'txtKdObat
        '
        Me.txtKdObat.AutoSize = True
        Me.txtKdObat.ForeColor = System.Drawing.Color.Black
        Me.txtKdObat.Location = New System.Drawing.Point(17, 230)
        Me.txtKdObat.Name = "txtKdObat"
        Me.txtKdObat.Size = New System.Drawing.Size(63, 16)
        Me.txtKdObat.TabIndex = 30
        Me.txtKdObat.Text = "Label14"
        Me.txtKdObat.Visible = False
        '
        'txtNoResep
        '
        Me.txtNoResep.AutoSize = True
        Me.txtNoResep.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold)
        Me.txtNoResep.ForeColor = System.Drawing.Color.Black
        Me.txtNoResep.Location = New System.Drawing.Point(183, 205)
        Me.txtNoResep.Name = "txtNoResep"
        Me.txtNoResep.Size = New System.Drawing.Size(59, 13)
        Me.txtNoResep.TabIndex = 16
        Me.txtNoResep.Text = "NoResep"
        Me.txtNoResep.Visible = False
        '
        'txtObat
        '
        Me.txtObat.Enabled = False
        Me.txtObat.Location = New System.Drawing.Point(109, 23)
        Me.txtObat.Name = "txtObat"
        Me.txtObat.Size = New System.Drawing.Size(184, 22)
        Me.txtObat.TabIndex = 29
        '
        'btnCariObat
        '
        Me.btnCariObat.BackColor = System.Drawing.Color.Navy
        Me.btnCariObat.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnCariObat.FlatAppearance.BorderSize = 2
        Me.btnCariObat.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCariObat.ForeColor = System.Drawing.Color.White
        Me.btnCariObat.Image = CType(resources.GetObject("btnCariObat.Image"), System.Drawing.Image)
        Me.btnCariObat.Location = New System.Drawing.Point(299, 21)
        Me.btnCariObat.Name = "btnCariObat"
        Me.btnCariObat.Size = New System.Drawing.Size(65, 27)
        Me.btnCariObat.TabIndex = 28
        Me.btnCariObat.Text = "Cari"
        Me.btnCariObat.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCariObat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCariObat.UseVisualStyleBackColor = False
        '
        'btnTambahObat
        '
        Me.btnTambahObat.BackColor = System.Drawing.Color.Navy
        Me.btnTambahObat.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnTambahObat.FlatAppearance.BorderSize = 2
        Me.btnTambahObat.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTambahObat.ForeColor = System.Drawing.Color.White
        Me.btnTambahObat.Location = New System.Drawing.Point(109, 258)
        Me.btnTambahObat.Name = "btnTambahObat"
        Me.btnTambahObat.Size = New System.Drawing.Size(110, 33)
        Me.btnTambahObat.TabIndex = 16
        Me.btnTambahObat.Text = "Tambah"
        Me.btnTambahObat.UseVisualStyleBackColor = False
        '
        'txtTotalObat
        '
        Me.txtTotalObat.Enabled = False
        Me.txtTotalObat.Location = New System.Drawing.Point(109, 144)
        Me.txtTotalObat.Name = "txtTotalObat"
        Me.txtTotalObat.Size = New System.Drawing.Size(110, 22)
        Me.txtTotalObat.TabIndex = 15
        Me.txtTotalObat.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(10, 147)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(82, 15)
        Me.Label13.TabIndex = 14
        Me.Label13.Text = "Total Harga"
        Me.Label13.Visible = False
        '
        'txtHargaObat
        '
        Me.txtHargaObat.Enabled = False
        Me.txtHargaObat.Location = New System.Drawing.Point(109, 120)
        Me.txtHargaObat.Name = "txtHargaObat"
        Me.txtHargaObat.Size = New System.Drawing.Size(110, 22)
        Me.txtHargaObat.TabIndex = 13
        Me.txtHargaObat.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(10, 123)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(46, 15)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "Harga"
        Me.Label10.Visible = False
        '
        'txtSatuanObat
        '
        Me.txtSatuanObat.Enabled = False
        Me.txtSatuanObat.Location = New System.Drawing.Point(109, 72)
        Me.txtSatuanObat.Name = "txtSatuanObat"
        Me.txtSatuanObat.Size = New System.Drawing.Size(110, 22)
        Me.txtSatuanObat.TabIndex = 11
        '
        'txtJmlPakaiObat
        '
        Me.txtJmlPakaiObat.Enabled = False
        Me.txtJmlPakaiObat.Location = New System.Drawing.Point(109, 96)
        Me.txtJmlPakaiObat.Name = "txtJmlPakaiObat"
        Me.txtJmlPakaiObat.Size = New System.Drawing.Size(110, 22)
        Me.txtJmlPakaiObat.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(10, 75)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(52, 15)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "Satuan"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(10, 99)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(94, 15)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Jumlah Pakai"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(10, 26)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 15)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Obat"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DataGridView1, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 35)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 207.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(733, 505)
        Me.TableLayoutPanel1.TabIndex = 4
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.HotTrack
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column8, Me.Column7, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column9})
        Me.TableLayoutPanel1.SetColumnSpan(Me.DataGridView1, 2)
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.EnableHeadersVisualStyles = False
        Me.DataGridView1.Location = New System.Drawing.Point(3, 301)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.Size = New System.Drawing.Size(727, 201)
        Me.DataGridView1.TabIndex = 4
        '
        'Column1
        '
        Me.Column1.FillWeight = 30.0!
        Me.Column1.HeaderText = "No."
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column8
        '
        Me.Column8.HeaderText = "No. Detail"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Visible = False
        '
        'Column7
        '
        Me.Column7.FillWeight = 50.0!
        Me.Column7.HeaderText = "Kode"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Visible = False
        '
        'Column2
        '
        Me.Column2.FillWeight = 200.0!
        Me.Column2.HeaderText = "Nama Obat"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.FillWeight = 45.0!
        Me.Column3.HeaderText = "Jml. Pakai"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.FillWeight = 80.0!
        Me.Column4.HeaderText = "Satuan"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.HeaderText = "Harga"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Visible = False
        '
        'Column6
        '
        Me.Column6.HeaderText = "Sub Total"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Visible = False
        '
        'Column9
        '
        Me.Column9.HeaderText = "Keterangan"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel2.Controls.Add(Me.TableLayoutPanel2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 542)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(733, 42)
        Me.Panel2.TabIndex = 5
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.btnTambahRacikan, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnSelesai, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(419, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(314, 42)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'btnTambahRacikan
        '
        Me.btnTambahRacikan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnTambahRacikan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTambahRacikan.Location = New System.Drawing.Point(3, 3)
        Me.btnTambahRacikan.Name = "btnTambahRacikan"
        Me.btnTambahRacikan.Size = New System.Drawing.Size(151, 36)
        Me.btnTambahRacikan.TabIndex = 0
        Me.btnTambahRacikan.Text = "Tambah Racikan"
        Me.btnTambahRacikan.UseVisualStyleBackColor = True
        Me.btnTambahRacikan.Visible = False
        '
        'btnSelesai
        '
        Me.btnSelesai.BackColor = System.Drawing.Color.Green
        Me.btnSelesai.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSelesai.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelesai.ForeColor = System.Drawing.Color.White
        Me.btnSelesai.Location = New System.Drawing.Point(160, 3)
        Me.btnSelesai.Name = "btnSelesai"
        Me.btnSelesai.Size = New System.Drawing.Size(151, 36)
        Me.btnSelesai.TabIndex = 1
        Me.btnSelesai.Text = "Selesai"
        Me.btnSelesai.UseVisualStyleBackColor = False
        '
        'Obat_Racikan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.ClientSize = New System.Drawing.Size(733, 584)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Obat_Racikan"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Obat Racikan"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnRacikan As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtTotalJenis As TextBox
    Friend WithEvents txtSatuanJenis As TextBox
    Friend WithEvents txtJmlJenis As TextBox
    Friend WithEvents txtHargaJenis As TextBox
    Friend WithEvents txtJenisRacikan As ComboBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtNoDetail As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents btnTambahObat As Button
    Friend WithEvents txtTotalObat As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents txtHargaObat As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txtSatuanObat As TextBox
    Friend WithEvents txtJmlPakaiObat As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Panel2 As Panel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents btnTambahRacikan As Button
    Friend WithEvents btnSelesai As Button
    Friend WithEvents btnCariObat As Button
    Friend WithEvents txtObat As TextBox
    Friend WithEvents txtKdJenis As Label
    Friend WithEvents txtKdObat As Label
    Friend WithEvents txtStokObat As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents txtHargaObatDec As Label
    Friend WithEvents txtKet As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents txtNoResep As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents txtAturan As ComboBox
    Friend WithEvents txtKetJenis As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents txtJmlHari As TextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
End Class
