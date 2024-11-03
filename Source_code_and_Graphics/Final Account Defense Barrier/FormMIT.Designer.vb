<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMIT
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.DocRight = New System.Windows.Forms.PictureBox()
        Me.DocLeft = New System.Windows.Forms.PictureBox()
        Me.PictureLICENSEBack = New System.Windows.Forms.PictureBox()
        Me.PictureMIT = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DocRight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DocLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureLICENSEBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureMIT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PictureLICENSEBack)
        Me.Panel1.Location = New System.Drawing.Point(12, 41)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(423, 470)
        Me.Panel1.TabIndex = 88
        '
        'PictureBox1
        '
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.exit_Pic
        Me.PictureBox1.Location = New System.Drawing.Point(377, 454)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(40, 40)
        Me.PictureBox1.TabIndex = 87
        Me.PictureBox1.TabStop = False
        '
        'DocRight
        '
        Me.DocRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DocRight.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DocRight.Location = New System.Drawing.Point(270, 471)
        Me.DocRight.Name = "DocRight"
        Me.DocRight.Size = New System.Drawing.Size(56, 25)
        Me.DocRight.TabIndex = 86
        Me.DocRight.TabStop = False
        '
        'DocLeft
        '
        Me.DocLeft.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DocLeft.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.Arr_Left
        Me.DocLeft.Location = New System.Drawing.Point(125, 471)
        Me.DocLeft.Name = "DocLeft"
        Me.DocLeft.Size = New System.Drawing.Size(56, 25)
        Me.DocLeft.TabIndex = 85
        Me.DocLeft.TabStop = False
        Me.DocLeft.Visible = False
        '
        'PictureLICENSEBack
        '
        Me.PictureLICENSEBack.BackColor = System.Drawing.Color.Black
        Me.PictureLICENSEBack.Location = New System.Drawing.Point(0, 0)
        Me.PictureLICENSEBack.Name = "PictureLICENSEBack"
        Me.PictureLICENSEBack.Size = New System.Drawing.Size(423, 470)
        Me.PictureLICENSEBack.TabIndex = 84
        Me.PictureLICENSEBack.TabStop = False
        '
        'PictureMIT
        '
        Me.PictureMIT.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.mit_lic
        Me.PictureMIT.Location = New System.Drawing.Point(0, 0)
        Me.PictureMIT.Name = "PictureMIT"
        Me.PictureMIT.Size = New System.Drawing.Size(448, 527)
        Me.PictureMIT.TabIndex = 83
        Me.PictureMIT.TabStop = False
        '
        'FormMIT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.Lime
        Me.ClientSize = New System.Drawing.Size(448, 524)
        Me.ControlBox = False
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.DocRight)
        Me.Controls.Add(Me.DocLeft)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PictureMIT)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormMIT"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.TransparencyKey = System.Drawing.Color.Lime
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DocRight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DocLeft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureLICENSEBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureMIT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureMIT As PictureBox
    Friend WithEvents PictureLICENSEBack As PictureBox
    Friend WithEvents DocLeft As PictureBox
    Friend WithEvents DocRight As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Panel1 As Panel
End Class
