<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SmallPWDShow
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
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

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請勿使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.PictureBoxPwd = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBoxPwd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBoxPwd
        '
        Me.PictureBoxPwd.Location = New System.Drawing.Point(10, 9)
        Me.PictureBoxPwd.Name = "PictureBoxPwd"
        Me.PictureBoxPwd.Size = New System.Drawing.Size(505, 22)
        Me.PictureBoxPwd.TabIndex = 0
        Me.PictureBoxPwd.TabStop = False
        Me.PictureBoxPwd.Visible = False
        '
        'SmallPWDShow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(238, 48)
        Me.ControlBox = False
        Me.Controls.Add(Me.PictureBoxPwd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SmallPWDShow"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "FormPWDShow"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        CType(Me.PictureBoxPwd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PictureBoxPwd As PictureBox
End Class
