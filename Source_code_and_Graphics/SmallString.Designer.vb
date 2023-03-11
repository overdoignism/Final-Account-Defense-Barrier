<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SmallString
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
        Me.IamaString = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'IamaString
        '
        Me.IamaString.Location = New System.Drawing.Point(12, 12)
        Me.IamaString.Name = "IamaString"
        Me.IamaString.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9733)
        Me.IamaString.Size = New System.Drawing.Size(47, 22)
        Me.IamaString.TabIndex = 0
        Me.IamaString.Visible = False
        '
        'SmallString
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(73, 44)
        Me.ControlBox = False
        Me.Controls.Add(Me.IamaString)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SmallString"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "SmallString"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents IamaString As TextBox
End Class
