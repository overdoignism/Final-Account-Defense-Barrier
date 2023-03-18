<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MSGBOXXX
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
        Me.TextBoxDELETE = New System.Windows.Forms.TextBox()
        Me.MSGHEAD = New System.Windows.Forms.Label()
        Me.ButtonOK = New System.Windows.Forms.PictureBox()
        Me.ButtonCancel = New System.Windows.Forms.PictureBox()
        Me.ButtonYes = New System.Windows.Forms.PictureBox()
        Me.LabelMSG = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ButtonNo = New System.Windows.Forms.PictureBox()
        CType(Me.ButtonOK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonYes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxDELETE
        '
        Me.TextBoxDELETE.BackColor = System.Drawing.Color.Black
        Me.TextBoxDELETE.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxDELETE.ForeColor = System.Drawing.Color.Yellow
        Me.TextBoxDELETE.Location = New System.Drawing.Point(198, 220)
        Me.TextBoxDELETE.Name = "TextBoxDELETE"
        Me.TextBoxDELETE.Size = New System.Drawing.Size(269, 26)
        Me.TextBoxDELETE.TabIndex = 33
        Me.TextBoxDELETE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TextBoxDELETE.Visible = False
        '
        'MSGHEAD
        '
        Me.MSGHEAD.BackColor = System.Drawing.Color.Black
        Me.MSGHEAD.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.MSGHEAD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.MSGHEAD.Location = New System.Drawing.Point(255, 50)
        Me.MSGHEAD.Name = "MSGHEAD"
        Me.MSGHEAD.Size = New System.Drawing.Size(197, 23)
        Me.MSGHEAD.TabIndex = 92
        Me.MSGHEAD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ButtonOK
        '
        Me.ButtonOK.BackColor = System.Drawing.Color.Black
        Me.ButtonOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonOK.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_confirm
        Me.ButtonOK.Location = New System.Drawing.Point(273, 334)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(112, 61)
        Me.ButtonOK.TabIndex = 88
        Me.ButtonOK.TabStop = False
        Me.ButtonOK.Visible = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.BackColor = System.Drawing.Color.Black
        Me.ButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonCancel.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_Cancel
        Me.ButtonCancel.Location = New System.Drawing.Point(403, 334)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(112, 61)
        Me.ButtonCancel.TabIndex = 87
        Me.ButtonCancel.TabStop = False
        Me.ButtonCancel.Visible = False
        '
        'ButtonYes
        '
        Me.ButtonYes.BackColor = System.Drawing.Color.Black
        Me.ButtonYes.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonYes.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_yes
        Me.ButtonYes.Location = New System.Drawing.Point(142, 334)
        Me.ButtonYes.Name = "ButtonYes"
        Me.ButtonYes.Size = New System.Drawing.Size(112, 61)
        Me.ButtonYes.TabIndex = 86
        Me.ButtonYes.TabStop = False
        Me.ButtonYes.Visible = False
        '
        'LabelMSG
        '
        Me.LabelMSG.BackColor = System.Drawing.Color.Black
        Me.LabelMSG.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelMSG.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LabelMSG.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.Message_OK_TXTB
        Me.LabelMSG.Location = New System.Drawing.Point(11, 90)
        Me.LabelMSG.Name = "LabelMSG"
        Me.LabelMSG.Size = New System.Drawing.Size(637, 233)
        Me.LabelMSG.TabIndex = 29
        Me.LabelMSG.Text = "Message Here"
        Me.LabelMSG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Black
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Default
        Me.PictureBox1.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.Message_OK
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(659, 409)
        Me.PictureBox1.TabIndex = 89
        Me.PictureBox1.TabStop = False
        '
        'ButtonNo
        '
        Me.ButtonNo.BackColor = System.Drawing.Color.Black
        Me.ButtonNo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonNo.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_no
        Me.ButtonNo.Location = New System.Drawing.Point(403, 334)
        Me.ButtonNo.Name = "ButtonNo"
        Me.ButtonNo.Size = New System.Drawing.Size(112, 61)
        Me.ButtonNo.TabIndex = 93
        Me.ButtonNo.TabStop = False
        Me.ButtonNo.Visible = False
        '
        'MSGBOXXX
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(659, 409)
        Me.ControlBox = False
        Me.Controls.Add(Me.ButtonNo)
        Me.Controls.Add(Me.MSGHEAD)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonYes)
        Me.Controls.Add(Me.TextBoxDELETE)
        Me.Controls.Add(Me.LabelMSG)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MSGBOXXX"
        Me.Opacity = 0.93R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Form1"
        Me.TransparencyKey = System.Drawing.Color.Lime
        CType(Me.ButtonOK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonYes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelMSG As Label
    Friend WithEvents TextBoxDELETE As TextBox
    Friend WithEvents ButtonCancel As PictureBox
    Friend WithEvents ButtonYes As PictureBox
    Friend WithEvents ButtonOK As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents MSGHEAD As Label
    Friend WithEvents ButtonNo As PictureBox
End Class
