<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Pindah_Kamar
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtJmlPindah = New System.Windows.Forms.TextBox()
        Me.txtPindahKelas = New System.Windows.Forms.ComboBox()
        Me.txtPindahNoBed = New System.Windows.Forms.TextBox()
        Me.txtPindahNoKmr = New System.Windows.Forms.TextBox()
        Me.txtPindahRanap = New System.Windows.Forms.TextBox()
        Me.txtTotalTarif = New System.Windows.Forms.TextBox()
        Me.txtJumHaper = New System.Windows.Forms.TextBox()
        Me.txtNoDaftar = New System.Windows.Forms.TextBox()
        Me.txtPindahNoDaftarRanap = New System.Windows.Forms.TextBox()
        Me.txtPindahKdTarifKmr = New System.Windows.Forms.TextBox()
        Me.comboPindahDok = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtPindahKdDok = New System.Windows.Forms.TextBox()
        Me.txtPindahTarif = New System.Windows.Forms.TextBox()
        Me.btnPindahKmrBed = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.datePindah = New System.Windows.Forms.DateTimePicker()
        Me.txtPindahKdRanap = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtKelas = New System.Windows.Forms.TextBox()
        Me.txtRanap = New System.Windows.Forms.TextBox()
        Me.txtAwalKdTarifKmr = New System.Windows.Forms.TextBox()
        Me.txtNoDaftarRanap = New System.Windows.Forms.TextBox()
        Me.txtDok = New System.Windows.Forms.TextBox()
        Me.txtKdDok = New System.Windows.Forms.TextBox()
        Me.txtTarifKmr = New System.Windows.Forms.TextBox()
        Me.txtNoBed = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtNoKmr = New System.Windows.Forms.TextBox()
        Me.dateMasuk = New System.Windows.Forms.DateTimePicker()
        Me.txtkdRanap = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ErrorProvider2 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Highlight
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(535, 36)
        Me.Panel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Control
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(535, 36)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Pindah Kamar / Ruang Inap"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btnOK)
        Me.Panel3.Controls.Add(Me.Panel2)
        Me.Panel3.Controls.Add(Me.GroupBox2)
        Me.Panel3.Controls.Add(Me.GroupBox1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 36)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(535, 518)
        Me.Panel3.TabIndex = 2
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.SeaGreen
        Me.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnOK.FlatAppearance.BorderSize = 2
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.ForeColor = System.Drawing.Color.White
        Me.btnOK.Location = New System.Drawing.Point(261, 459)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(129, 38)
        Me.btnOK.TabIndex = 8
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 503)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(535, 15)
        Me.Panel2.TabIndex = 4
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtJmlPindah)
        Me.GroupBox2.Controls.Add(Me.txtPindahKelas)
        Me.GroupBox2.Controls.Add(Me.txtPindahNoBed)
        Me.GroupBox2.Controls.Add(Me.txtPindahNoKmr)
        Me.GroupBox2.Controls.Add(Me.txtPindahRanap)
        Me.GroupBox2.Controls.Add(Me.txtTotalTarif)
        Me.GroupBox2.Controls.Add(Me.txtJumHaper)
        Me.GroupBox2.Controls.Add(Me.txtNoDaftar)
        Me.GroupBox2.Controls.Add(Me.txtPindahNoDaftarRanap)
        Me.GroupBox2.Controls.Add(Me.txtPindahKdTarifKmr)
        Me.GroupBox2.Controls.Add(Me.comboPindahDok)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.txtPindahKdDok)
        Me.GroupBox2.Controls.Add(Me.txtPindahTarif)
        Me.GroupBox2.Controls.Add(Me.btnPindahKmrBed)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.datePindah)
        Me.GroupBox2.Controls.Add(Me.txtPindahKdRanap)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Gray
        Me.GroupBox2.Location = New System.Drawing.Point(0, 223)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(535, 231)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Kamar Inap Tujuan"
        '
        'txtJmlPindah
        '
        Me.txtJmlPindah.Enabled = False
        Me.txtJmlPindah.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJmlPindah.Location = New System.Drawing.Point(529, 102)
        Me.txtJmlPindah.Name = "txtJmlPindah"
        Me.txtJmlPindah.Size = New System.Drawing.Size(168, 20)
        Me.txtJmlPindah.TabIndex = 51
        Me.txtJmlPindah.Visible = False
        '
        'txtPindahKelas
        '
        Me.txtPindahKelas.Enabled = False
        Me.txtPindahKelas.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPindahKelas.FormattingEnabled = True
        Me.txtPindahKelas.Items.AddRange(New Object() {"KELAS III", "KELAS II", "KELAS I", "UTAMA", "VIP", "VVIP", "EXECUTIVE"})
        Me.txtPindahKelas.Location = New System.Drawing.Point(261, 85)
        Me.txtPindahKelas.Name = "txtPindahKelas"
        Me.txtPindahKelas.Size = New System.Drawing.Size(214, 28)
        Me.txtPindahKelas.TabIndex = 50
        '
        'txtPindahNoBed
        '
        Me.txtPindahNoBed.Enabled = False
        Me.txtPindahNoBed.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPindahNoBed.Location = New System.Drawing.Point(398, 120)
        Me.txtPindahNoBed.Name = "txtPindahNoBed"
        Me.txtPindahNoBed.Size = New System.Drawing.Size(50, 26)
        Me.txtPindahNoBed.TabIndex = 49
        '
        'txtPindahNoKmr
        '
        Me.txtPindahNoKmr.Enabled = False
        Me.txtPindahNoKmr.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPindahNoKmr.Location = New System.Drawing.Point(261, 120)
        Me.txtPindahNoKmr.Name = "txtPindahNoKmr"
        Me.txtPindahNoKmr.Size = New System.Drawing.Size(50, 26)
        Me.txtPindahNoKmr.TabIndex = 48
        '
        'txtPindahRanap
        '
        Me.txtPindahRanap.Enabled = False
        Me.txtPindahRanap.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPindahRanap.Location = New System.Drawing.Point(261, 53)
        Me.txtPindahRanap.Name = "txtPindahRanap"
        Me.txtPindahRanap.Size = New System.Drawing.Size(214, 26)
        Me.txtPindahRanap.TabIndex = 46
        '
        'txtTotalTarif
        '
        Me.txtTotalTarif.Enabled = False
        Me.txtTotalTarif.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalTarif.Location = New System.Drawing.Point(529, 155)
        Me.txtTotalTarif.Name = "txtTotalTarif"
        Me.txtTotalTarif.Size = New System.Drawing.Size(168, 20)
        Me.txtTotalTarif.TabIndex = 45
        Me.txtTotalTarif.Visible = False
        '
        'txtJumHaper
        '
        Me.txtJumHaper.Enabled = False
        Me.txtJumHaper.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJumHaper.Location = New System.Drawing.Point(529, 132)
        Me.txtJumHaper.Name = "txtJumHaper"
        Me.txtJumHaper.Size = New System.Drawing.Size(168, 20)
        Me.txtJumHaper.TabIndex = 44
        Me.txtJumHaper.Visible = False
        '
        'txtNoDaftar
        '
        Me.txtNoDaftar.Enabled = False
        Me.txtNoDaftar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoDaftar.Location = New System.Drawing.Point(529, 50)
        Me.txtNoDaftar.Name = "txtNoDaftar"
        Me.txtNoDaftar.Size = New System.Drawing.Size(168, 20)
        Me.txtNoDaftar.TabIndex = 42
        Me.txtNoDaftar.Visible = False
        '
        'txtPindahNoDaftarRanap
        '
        Me.txtPindahNoDaftarRanap.Enabled = False
        Me.txtPindahNoDaftarRanap.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPindahNoDaftarRanap.Location = New System.Drawing.Point(529, 21)
        Me.txtPindahNoDaftarRanap.Name = "txtPindahNoDaftarRanap"
        Me.txtPindahNoDaftarRanap.Size = New System.Drawing.Size(168, 20)
        Me.txtPindahNoDaftarRanap.TabIndex = 18
        Me.txtPindahNoDaftarRanap.Visible = False
        '
        'txtPindahKdTarifKmr
        '
        Me.txtPindahKdTarifKmr.Enabled = False
        Me.txtPindahKdTarifKmr.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPindahKdTarifKmr.Location = New System.Drawing.Point(529, 76)
        Me.txtPindahKdTarifKmr.Name = "txtPindahKdTarifKmr"
        Me.txtPindahKdTarifKmr.Size = New System.Drawing.Size(168, 20)
        Me.txtPindahKdTarifKmr.TabIndex = 19
        Me.txtPindahKdTarifKmr.Visible = False
        '
        'comboPindahDok
        '
        Me.comboPindahDok.DropDownWidth = 450
        Me.comboPindahDok.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboPindahDok.FormattingEnabled = True
        Me.comboPindahDok.Location = New System.Drawing.Point(261, 187)
        Me.comboPindahDok.Name = "comboPindahDok"
        Me.comboPindahDok.Size = New System.Drawing.Size(262, 28)
        Me.comboPindahDok.TabIndex = 7
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(24, 88)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 20)
        Me.Label12.TabIndex = 35
        Me.Label12.Text = "Kelas"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(24, 190)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(225, 20)
        Me.Label10.TabIndex = 37
        Me.Label10.Text = "Dokter Penanggung Jawab"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(24, 158)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(114, 20)
        Me.Label11.TabIndex = 36
        Me.Label11.Text = "Tarif per Hari"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(24, 123)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(92, 20)
        Me.Label13.TabIndex = 34
        Me.Label13.Text = "No. Kamar"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(24, 56)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(100, 20)
        Me.Label14.TabIndex = 33
        Me.Label14.Text = "Unit Ranap"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(24, 25)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(133, 20)
        Me.Label15.TabIndex = 32
        Me.Label15.Text = "Tanggal Pindah"
        '
        'txtPindahKdDok
        '
        Me.txtPindahKdDok.Enabled = False
        Me.txtPindahKdDok.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPindahKdDok.Location = New System.Drawing.Point(261, 187)
        Me.txtPindahKdDok.Name = "txtPindahKdDok"
        Me.txtPindahKdDok.Size = New System.Drawing.Size(50, 26)
        Me.txtPindahKdDok.TabIndex = 29
        '
        'txtPindahTarif
        '
        Me.txtPindahTarif.Enabled = False
        Me.txtPindahTarif.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPindahTarif.Location = New System.Drawing.Point(261, 153)
        Me.txtPindahTarif.Name = "txtPindahTarif"
        Me.txtPindahTarif.Size = New System.Drawing.Size(86, 26)
        Me.txtPindahTarif.TabIndex = 6
        '
        'btnPindahKmrBed
        '
        Me.btnPindahKmrBed.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.btnPindahKmrBed.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.btnPindahKmrBed.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPindahKmrBed.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPindahKmrBed.Image = Global.Rawat_Inap.My.Resources.Resources.magnifying_glass_green3
        Me.btnPindahKmrBed.Location = New System.Drawing.Point(489, 50)
        Me.btnPindahKmrBed.Name = "btnPindahKmrBed"
        Me.btnPindahKmrBed.Size = New System.Drawing.Size(33, 32)
        Me.btnPindahKmrBed.TabIndex = 26
        Me.btnPindahKmrBed.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(317, 123)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(73, 20)
        Me.Label9.TabIndex = 24
        Me.Label9.Text = "No. Bed"
        '
        'datePindah
        '
        Me.datePindah.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.datePindah.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.datePindah.Location = New System.Drawing.Point(261, 21)
        Me.datePindah.Name = "datePindah"
        Me.datePindah.Size = New System.Drawing.Size(214, 26)
        Me.datePindah.TabIndex = 1
        '
        'txtPindahKdRanap
        '
        Me.txtPindahKdRanap.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPindahKdRanap.Enabled = False
        Me.txtPindahKdRanap.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPindahKdRanap.Location = New System.Drawing.Point(261, 53)
        Me.txtPindahKdRanap.Name = "txtPindahKdRanap"
        Me.txtPindahKdRanap.Size = New System.Drawing.Size(50, 26)
        Me.txtPindahKdRanap.TabIndex = 19
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtKelas)
        Me.GroupBox1.Controls.Add(Me.txtRanap)
        Me.GroupBox1.Controls.Add(Me.txtAwalKdTarifKmr)
        Me.GroupBox1.Controls.Add(Me.txtNoDaftarRanap)
        Me.GroupBox1.Controls.Add(Me.txtDok)
        Me.GroupBox1.Controls.Add(Me.txtKdDok)
        Me.GroupBox1.Controls.Add(Me.txtTarifKmr)
        Me.GroupBox1.Controls.Add(Me.txtNoBed)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtNoKmr)
        Me.GroupBox1.Controls.Add(Me.dateMasuk)
        Me.GroupBox1.Controls.Add(Me.txtkdRanap)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Gray
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(535, 223)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Kamar Inap Sebelumnya"
        '
        'txtKelas
        '
        Me.txtKelas.Enabled = False
        Me.txtKelas.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKelas.Location = New System.Drawing.Point(261, 87)
        Me.txtKelas.Name = "txtKelas"
        Me.txtKelas.Size = New System.Drawing.Size(187, 26)
        Me.txtKelas.TabIndex = 48
        '
        'txtRanap
        '
        Me.txtRanap.Enabled = False
        Me.txtRanap.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRanap.Location = New System.Drawing.Point(261, 55)
        Me.txtRanap.Name = "txtRanap"
        Me.txtRanap.Size = New System.Drawing.Size(214, 26)
        Me.txtRanap.TabIndex = 7
        '
        'txtAwalKdTarifKmr
        '
        Me.txtAwalKdTarifKmr.Enabled = False
        Me.txtAwalKdTarifKmr.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAwalKdTarifKmr.Location = New System.Drawing.Point(529, 86)
        Me.txtAwalKdTarifKmr.Name = "txtAwalKdTarifKmr"
        Me.txtAwalKdTarifKmr.Size = New System.Drawing.Size(168, 20)
        Me.txtAwalKdTarifKmr.TabIndex = 20
        Me.txtAwalKdTarifKmr.Visible = False
        '
        'txtNoDaftarRanap
        '
        Me.txtNoDaftarRanap.Enabled = False
        Me.txtNoDaftarRanap.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoDaftarRanap.Location = New System.Drawing.Point(529, 22)
        Me.txtNoDaftarRanap.Name = "txtNoDaftarRanap"
        Me.txtNoDaftarRanap.Size = New System.Drawing.Size(168, 20)
        Me.txtNoDaftarRanap.TabIndex = 19
        Me.txtNoDaftarRanap.Visible = False
        '
        'txtDok
        '
        Me.txtDok.Enabled = False
        Me.txtDok.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDok.Location = New System.Drawing.Point(261, 185)
        Me.txtDok.Name = "txtDok"
        Me.txtDok.Size = New System.Drawing.Size(261, 26)
        Me.txtDok.TabIndex = 17
        '
        'txtKdDok
        '
        Me.txtKdDok.Enabled = False
        Me.txtKdDok.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKdDok.Location = New System.Drawing.Point(261, 185)
        Me.txtKdDok.Name = "txtKdDok"
        Me.txtKdDok.Size = New System.Drawing.Size(50, 26)
        Me.txtKdDok.TabIndex = 16
        '
        'txtTarifKmr
        '
        Me.txtTarifKmr.Enabled = False
        Me.txtTarifKmr.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTarifKmr.Location = New System.Drawing.Point(261, 153)
        Me.txtTarifKmr.Name = "txtTarifKmr"
        Me.txtTarifKmr.Size = New System.Drawing.Size(86, 26)
        Me.txtTarifKmr.TabIndex = 15
        '
        'txtNoBed
        '
        Me.txtNoBed.Enabled = False
        Me.txtNoBed.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoBed.Location = New System.Drawing.Point(398, 121)
        Me.txtNoBed.Name = "txtNoBed"
        Me.txtNoBed.Size = New System.Drawing.Size(50, 26)
        Me.txtNoBed.TabIndex = 12
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(319, 124)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 20)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "No. Bed"
        '
        'txtNoKmr
        '
        Me.txtNoKmr.Enabled = False
        Me.txtNoKmr.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoKmr.Location = New System.Drawing.Point(261, 121)
        Me.txtNoKmr.Name = "txtNoKmr"
        Me.txtNoKmr.Size = New System.Drawing.Size(50, 26)
        Me.txtNoKmr.TabIndex = 10
        '
        'dateMasuk
        '
        Me.dateMasuk.Enabled = False
        Me.dateMasuk.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dateMasuk.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dateMasuk.Location = New System.Drawing.Point(261, 23)
        Me.dateMasuk.Name = "dateMasuk"
        Me.dateMasuk.Size = New System.Drawing.Size(214, 26)
        Me.dateMasuk.TabIndex = 8
        '
        'txtkdRanap
        '
        Me.txtkdRanap.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtkdRanap.Enabled = False
        Me.txtkdRanap.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtkdRanap.Location = New System.Drawing.Point(261, 55)
        Me.txtkdRanap.Name = "txtkdRanap"
        Me.txtkdRanap.Size = New System.Drawing.Size(50, 26)
        Me.txtkdRanap.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(25, 188)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(225, 20)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Dokter Penanggung Jawab"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(25, 156)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(114, 20)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Tarif per Hari"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(25, 90)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 20)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Kelas"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(25, 124)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 20)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "No. Kamar"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(25, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 20)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Unit Ranap"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(25, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(130, 20)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Tanggal Masuk"
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'ErrorProvider2
        '
        Me.ErrorProvider2.ContainerControl = Me
        '
        'Pindah_Kamar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.GhostWhite
        Me.ClientSize = New System.Drawing.Size(535, 554)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Pindah_Kamar"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pindah Kamar"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents btnOK As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents txtPindahKdDok As TextBox
    Friend WithEvents txtPindahTarif As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents datePindah As DateTimePicker
    Friend WithEvents txtPindahKdRanap As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtDok As TextBox
    Friend WithEvents txtKdDok As TextBox
    Friend WithEvents txtTarifKmr As TextBox
    Friend WithEvents txtNoBed As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtNoKmr As TextBox
    Friend WithEvents dateMasuk As DateTimePicker
    Friend WithEvents txtRanap As TextBox
    Friend WithEvents txtkdRanap As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents comboPindahDok As ComboBox
    Friend WithEvents btnPindahKmrBed As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents txtPindahNoDaftarRanap As TextBox
    Friend WithEvents txtPindahKdTarifKmr As TextBox
    Friend WithEvents txtNoDaftar As TextBox
    Friend WithEvents txtTotalTarif As TextBox
    Friend WithEvents txtJumHaper As TextBox
    Friend WithEvents txtNoDaftarRanap As TextBox
    Friend WithEvents txtAwalKdTarifKmr As TextBox
    Friend WithEvents txtPindahRanap As TextBox
    Friend WithEvents txtPindahNoBed As TextBox
    Friend WithEvents txtPindahNoKmr As TextBox
    Friend WithEvents txtKelas As TextBox
    Friend WithEvents ErrorProvider1 As ErrorProvider
    Friend WithEvents ErrorProvider2 As ErrorProvider
    Friend WithEvents txtPindahKelas As ComboBox
    Friend WithEvents txtJmlPindah As TextBox
End Class
