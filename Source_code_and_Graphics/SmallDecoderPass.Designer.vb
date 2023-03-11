<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SmallDecoderPass
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
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

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請勿使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TextBoxPWDStr = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'TextBoxPWDStr
        '
        Me.TextBoxPWDStr.Location = New System.Drawing.Point(12, 12)
        Me.TextBoxPWDStr.Name = "TextBoxPWDStr"
        Me.TextBoxPWDStr.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9733)
        Me.TextBoxPWDStr.Size = New System.Drawing.Size(45, 22)
        Me.TextBoxPWDStr.TabIndex = 0
        Me.TextBoxPWDStr.Visible = False
        '
        'SmallDecoderPass
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(69, 49)
        Me.Controls.Add(Me.TextBoxPWDStr)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SmallDecoderPass"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "SmallDecoder"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxPWDStr As TextBox
End Class
