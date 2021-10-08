<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ViewCetakLIS
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
        Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Me.vw_lisBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CetakHasilLab = New Rawat_Inap.CetakHasilLab()
        Me.vw_lisTableAdapter = New Rawat_Inap.CetakHasilLabTableAdapters.vw_lisTableAdapter()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        CType(Me.vw_lisBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CetakHasilLab, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'vw_lisBindingSource
        '
        Me.vw_lisBindingSource.DataMember = "vw_lis"
        Me.vw_lisBindingSource.DataSource = Me.CetakHasilLab
        '
        'CetakHasilLab
        '
        Me.CetakHasilLab.DataSetName = "CetakHasilLab"
        Me.CetakHasilLab.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'vw_lisTableAdapter
        '
        Me.vw_lisTableAdapter.ClearBeforeFill = True
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource1.Name = "HasilLIS"
        ReportDataSource1.Value = Me.vw_lisBindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "Rawat_Inap.cetakhasilis.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual
        Me.ReportViewer1.ServerReport.BearerToken = Nothing
        Me.ReportViewer1.ShowBackButton = False
        Me.ReportViewer1.ShowContextMenu = False
        Me.ReportViewer1.ShowCredentialPrompts = False
        Me.ReportViewer1.ShowDocumentMapButton = False
        Me.ReportViewer1.ShowFindControls = False
        Me.ReportViewer1.ShowParameterPrompts = False
        Me.ReportViewer1.ShowPromptAreaButton = False
        Me.ReportViewer1.ShowRefreshButton = False
        Me.ReportViewer1.ShowStopButton = False
        Me.ReportViewer1.Size = New System.Drawing.Size(831, 680)
        Me.ReportViewer1.TabIndex = 0
        '
        'ViewCetakLIS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(831, 680)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Name = "ViewCetakLIS"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CetakLIS"
        CType(Me.vw_lisBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CetakHasilLab, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents vw_lisBindingSource As BindingSource
    Friend WithEvents CetakHasilLab As CetakHasilLab
    Friend WithEvents vw_lisTableAdapter As CetakHasilLabTableAdapters.vw_lisTableAdapter
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
End Class
