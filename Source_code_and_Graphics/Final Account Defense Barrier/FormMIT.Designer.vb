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
        Me.PictureMIT = New System.Windows.Forms.PictureBox()
        Me.PictureLICENSE = New System.Windows.Forms.PictureBox()
        Me.DocLeft = New System.Windows.Forms.PictureBox()
        Me.DocRight = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        CType(Me.PictureMIT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureLICENSE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DocLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DocRight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureMIT
        '
        Me.PictureMIT.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.mit_lic
        Me.PictureMIT.Location = New System.Drawing.Point(0, 0)
        Me.PictureMIT.Name = "PictureMIT"
        Me.PictureMIT.Size = New System.Drawing.Size(450, 527)
        Me.PictureMIT.TabIndex = 83
        Me.PictureMIT.TabStop = False
        '
        'PictureLICENSE
        '
        Me.PictureLICENSE.Location = New System.Drawing.Point(0, 0)
        Me.PictureLICENSE.Name = "PictureLICENSE"
        Me.PictureLICENSE.Size = New System.Drawing.Size(1269, 470)
        Me.PictureLICENSE.TabIndex = 84
        Me.PictureLICENSE.TabStop = False
        '
        'DocLeft
        '
        Me.DocLeft.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DocLeft.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.Arr_Left
        Me.DocLeft.Location = New System.Drawing.Point(127, 474)
        Me.DocLeft.Name = "DocLeft"
        Me.DocLeft.Size = New System.Drawing.Size(56, 25)
        Me.DocLeft.TabIndex = 85
        Me.DocLeft.TabStop = False
        Me.DocLeft.Visible = False
        '
        'DocRight
        '
        Me.DocRight.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DocRight.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.Arr_Right
        Me.DocRight.Location = New System.Drawing.Point(272, 474)
        Me.DocRight.Name = "DocRight"
        Me.DocRight.Size = New System.Drawing.Size(56, 25)
        Me.DocRight.TabIndex = 86
        Me.DocRight.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.exit_Pic
        Me.PictureBox1.Location = New System.Drawing.Point(379, 457)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(40, 40)
        Me.PictureBox1.TabIndex = 87
        Me.PictureBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PictureLICENSE)
        Me.Panel1.Location = New System.Drawing.Point(14, 44)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(423, 470)
        Me.Panel1.TabIndex = 88
        '
        'FormMIT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(450, 527)
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
        CType(Me.PictureMIT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureLICENSE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DocLeft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DocRight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureMIT As PictureBox
    Friend WithEvents PictureLICENSE As PictureBox
    Friend WithEvents DocLeft As PictureBox
    Friend WithEvents DocRight As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Panel1 As Panel
End Class
