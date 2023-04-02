<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCSVWorking
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
        Me.ProgLab = New System.Windows.Forms.Label()
        Me.PictureMode = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureMode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ProgLab
        '
        Me.ProgLab.AutoEllipsis = True
        Me.ProgLab.BackColor = System.Drawing.Color.Black
        Me.ProgLab.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ProgLab.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ProgLab.Location = New System.Drawing.Point(189, 79)
        Me.ProgLab.Name = "ProgLab"
        Me.ProgLab.Size = New System.Drawing.Size(110, 19)
        Me.ProgLab.TabIndex = 30
        Me.ProgLab.Text = "0/0"
        Me.ProgLab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureMode
        '
        Me.PictureMode.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.CSV_IMP
        Me.PictureMode.Location = New System.Drawing.Point(306, 52)
        Me.PictureMode.Name = "PictureMode"
        Me.PictureMode.Size = New System.Drawing.Size(135, 41)
        Me.PictureMode.TabIndex = 31
        Me.PictureMode.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.CSV_Waiting
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(452, 142)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'FormCSVWorking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(452, 142)
        Me.ControlBox = False
        Me.Controls.Add(Me.PictureMode)
        Me.Controls.Add(Me.ProgLab)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormCSVWorking"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "CSV_Working"
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.Color.Lime
        CType(Me.PictureMode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ProgLab As Label
    Friend WithEvents PictureMode As PictureBox
End Class
