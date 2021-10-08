<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Tes
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
        Me.components = New System.ComponentModel.Container()
        Me.tmrCountdown = New System.Windows.Forms.Timer(Me.components)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ShapedPanel1 = New Rawat_Inap.ShapedPanel()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.MyPanel1 = New Rawat_Inap.myPanel(Me.components)
        Me.RichTextBox2 = New System.Windows.Forms.RichTextBox()
        Me.bntBold = New System.Windows.Forms.Button()
        Me.btnBullet = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.ShapedPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tmrCountdown
        '
        Me.tmrCountdown.Interval = 1000
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(140, 96)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(15, 15)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(200, 20)
        Me.DateTimePicker1.TabIndex = 1
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Location = New System.Drawing.Point(15, 57)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(200, 20)
        Me.DateTimePicker2.TabIndex = 2
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(15, 133)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(200, 20)
        Me.TextBox1.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(238, 136)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Label1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.DateTimePicker1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.DateTimePicker2)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(285, 183)
        Me.Panel1.TabIndex = 5
        '
        'ShapedPanel1
        '
        Me.ShapedPanel1.BackColor = System.Drawing.Color.Brown
        Me.ShapedPanel1.BorderColor = System.Drawing.Color.White
        Me.ShapedPanel1.Controls.Add(Me.RichTextBox1)
        Me.ShapedPanel1.Controls.Add(Me.Label3)
        Me.ShapedPanel1.Controls.Add(Me.Label2)
        Me.ShapedPanel1.Controls.Add(Me.TextBox3)
        Me.ShapedPanel1.Controls.Add(Me.TextBox2)
        Me.ShapedPanel1.Edge = 50
        Me.ShapedPanel1.Location = New System.Drawing.Point(355, 27)
        Me.ShapedPanel1.Name = "ShapedPanel1"
        Me.ShapedPanel1.Size = New System.Drawing.Size(565, 278)
        Me.ShapedPanel1.TabIndex = 6
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(312, 42)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(232, 210)
        Me.RichTextBox1.TabIndex = 7
        Me.RichTextBox1.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(20, 82)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "HEIGHT"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(20, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "WIDTH"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(23, 101)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(200, 20)
        Me.TextBox3.TabIndex = 8
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(23, 42)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(200, 20)
        Me.TextBox2.TabIndex = 7
        '
        'RichTextBox2
        '
        Me.RichTextBox2.Location = New System.Drawing.Point(12, 201)
        Me.RichTextBox2.Name = "RichTextBox2"
        Me.RichTextBox2.Size = New System.Drawing.Size(285, 136)
        Me.RichTextBox2.TabIndex = 11
        Me.RichTextBox2.Text = ""
        '
        'bntBold
        '
        Me.bntBold.Location = New System.Drawing.Point(303, 230)
        Me.bntBold.Name = "bntBold"
        Me.bntBold.Size = New System.Drawing.Size(46, 23)
        Me.bntBold.TabIndex = 12
        Me.bntBold.Text = "bold"
        Me.bntBold.UseVisualStyleBackColor = True
        '
        'btnBullet
        '
        Me.btnBullet.Location = New System.Drawing.Point(303, 201)
        Me.btnBullet.Name = "btnBullet"
        Me.btnBullet.Size = New System.Drawing.Size(46, 23)
        Me.btnBullet.TabIndex = 13
        Me.btnBullet.Text = "bullet"
        Me.btnBullet.UseVisualStyleBackColor = True
        '
        'Tes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(983, 349)
        Me.Controls.Add(Me.btnBullet)
        Me.Controls.Add(Me.bntBold)
        Me.Controls.Add(Me.RichTextBox2)
        Me.Controls.Add(Me.ShapedPanel1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Tes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tes"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ShapedPanel1.ResumeLayout(False)
        Me.ShapedPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tmrCountdown As Timer
    Friend WithEvents Button1 As Button
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ShapedPanel1 As ShapedPanel
    Friend WithEvents MyPanel1 As myPanel
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents RichTextBox2 As RichTextBox
    Friend WithEvents bntBold As Button
    Friend WithEvents btnBullet As Button
End Class
