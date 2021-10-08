Public Class myGroupBox
    Inherits GroupBox
    Private borderColor As Color

    Public Sub New()
        MyBase.New
        Me.borderColor = Color.Gray
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim tSize As Size = TextRenderer.MeasureText(Text, Font)
        Dim borderReact As Rectangle = e.ClipRectangle
        borderReact.Y = (borderReact.Y + tSize.Height / 2)
        borderReact.Height = (borderReact.Height - tSize.Height / 2)
        ControlPaint.DrawBorder(e.Graphics, borderReact, Me.borderColor, ButtonBorderStyle.Solid)
        Dim textRect As Rectangle = e.ClipRectangle
        textRect.X = (textRect.X + 6)
        textRect.Width = tSize.Width
        textRect.Height = tSize.Height
        e.Graphics.FillRectangle(New SolidBrush(Me.BackColor), textRect)
        e.Graphics.DrawString(Me.Text, Me.Font, New SolidBrush(Me.ForeColor), textRect)
    End Sub
End Class
