<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormFileExplorer
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
        Me.ListBoxDrivers = New System.Windows.Forms.ListBox()
        Me.ListBoxFiles = New System.Windows.Forms.ListBox()
        Me.ButtonCancel = New System.Windows.Forms.PictureBox()
        Me.ButtonFileOpen = New System.Windows.Forms.PictureBox()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.PictureGray = New System.Windows.Forms.PictureBox()
        Me.LabelPath = New System.Windows.Forms.Label()
        Me.LSCBBAR = New System.Windows.Forms.PictureBox()
        Me.LSCBD = New System.Windows.Forms.PictureBox()
        Me.LSCBU = New System.Windows.Forms.PictureBox()
        Me.LSCBBACK = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        CType(Me.ButtonCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonFileOpen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureGray, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LSCBBAR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LSCBD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LSCBU, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LSCBBACK, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListBoxDrivers
        '
        Me.ListBoxDrivers.BackColor = System.Drawing.Color.Black
        Me.ListBoxDrivers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBoxDrivers.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBoxDrivers.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ListBoxDrivers.FormattingEnabled = True
        Me.ListBoxDrivers.ItemHeight = 18
        Me.ListBoxDrivers.Location = New System.Drawing.Point(24, 172)
        Me.ListBoxDrivers.Name = "ListBoxDrivers"
        Me.ListBoxDrivers.Size = New System.Drawing.Size(221, 162)
        Me.ListBoxDrivers.TabIndex = 0
        '
        'ListBoxFiles
        '
        Me.ListBoxFiles.BackColor = System.Drawing.Color.Black
        Me.ListBoxFiles.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBoxFiles.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBoxFiles.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ListBoxFiles.FormattingEnabled = True
        Me.ListBoxFiles.ItemHeight = 18
        Me.ListBoxFiles.Location = New System.Drawing.Point(0, 0)
        Me.ListBoxFiles.Name = "ListBoxFiles"
        Me.ListBoxFiles.Size = New System.Drawing.Size(615, 324)
        Me.ListBoxFiles.TabIndex = 3
        '
        'ButtonCancel
        '
        Me.ButtonCancel.BackColor = System.Drawing.Color.Black
        Me.ButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonCancel.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_Cancel
        Me.ButtonCancel.Location = New System.Drawing.Point(139, 466)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(112, 61)
        Me.ButtonCancel.TabIndex = 85
        Me.ButtonCancel.TabStop = False
        '
        'ButtonFileOpen
        '
        Me.ButtonFileOpen.BackColor = System.Drawing.Color.Black
        Me.ButtonFileOpen.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonFileOpen.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_OpenF_DI
        Me.ButtonFileOpen.Location = New System.Drawing.Point(22, 466)
        Me.ButtonFileOpen.Name = "ButtonFileOpen"
        Me.ButtonFileOpen.Size = New System.Drawing.Size(112, 61)
        Me.ButtonFileOpen.TabIndex = 84
        Me.ButtonFileOpen.TabStop = False
        '
        'PictureBox8
        '
        Me.PictureBox8.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.File_expo
        Me.PictureBox8.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(901, 548)
        Me.PictureBox8.TabIndex = 53
        Me.PictureBox8.TabStop = False
        '
        'PictureGray
        '
        Me.PictureGray.Location = New System.Drawing.Point(0, 0)
        Me.PictureGray.Name = "PictureGray"
        Me.PictureGray.Size = New System.Drawing.Size(901, 548)
        Me.PictureGray.TabIndex = 86
        Me.PictureGray.TabStop = False
        Me.PictureGray.Visible = False
        '
        'LabelPath
        '
        Me.LabelPath.AutoEllipsis = True
        Me.LabelPath.BackColor = System.Drawing.Color.Black
        Me.LabelPath.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelPath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LabelPath.Location = New System.Drawing.Point(193, 65)
        Me.LabelPath.Name = "LabelPath"
        Me.LabelPath.Size = New System.Drawing.Size(683, 19)
        Me.LabelPath.TabIndex = 119
        Me.LabelPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LSCBBAR
        '
        Me.LSCBBAR.BackColor = System.Drawing.Color.Black
        Me.LSCBBAR.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.SCB_BAR
        Me.LSCBBAR.Location = New System.Drawing.Point(859, 187)
        Me.LSCBBAR.Name = "LSCBBAR"
        Me.LSCBBAR.Size = New System.Drawing.Size(17, 59)
        Me.LSCBBAR.TabIndex = 130
        Me.LSCBBAR.TabStop = False
        '
        'LSCBD
        '
        Me.LSCBD.BackColor = System.Drawing.Color.Black
        Me.LSCBD.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.SCB_S_DW
        Me.LSCBD.Location = New System.Drawing.Point(859, 482)
        Me.LSCBD.Name = "LSCBD"
        Me.LSCBD.Size = New System.Drawing.Size(17, 17)
        Me.LSCBD.TabIndex = 129
        Me.LSCBD.TabStop = False
        '
        'LSCBU
        '
        Me.LSCBU.BackColor = System.Drawing.Color.Black
        Me.LSCBU.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.SCB_S_UP
        Me.LSCBU.Location = New System.Drawing.Point(859, 170)
        Me.LSCBU.Name = "LSCBU"
        Me.LSCBU.Size = New System.Drawing.Size(17, 17)
        Me.LSCBU.TabIndex = 128
        Me.LSCBU.TabStop = False
        '
        'LSCBBACK
        '
        Me.LSCBBACK.BackColor = System.Drawing.Color.Black
        Me.LSCBBACK.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.SCB_BARV
        Me.LSCBBACK.Location = New System.Drawing.Point(859, 187)
        Me.LSCBBACK.Name = "LSCBBACK"
        Me.LSCBBACK.Size = New System.Drawing.Size(17, 295)
        Me.LSCBBACK.TabIndex = 127
        Me.LSCBBACK.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ListBoxFiles)
        Me.Panel1.Location = New System.Drawing.Point(274, 172)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(582, 324)
        Me.Panel1.TabIndex = 131
        '
        'FormFileExplorer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(901, 548)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.LSCBBAR)
        Me.Controls.Add(Me.LSCBD)
        Me.Controls.Add(Me.LSCBU)
        Me.Controls.Add(Me.LSCBBACK)
        Me.Controls.Add(Me.LabelPath)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonFileOpen)
        Me.Controls.Add(Me.ListBoxDrivers)
        Me.Controls.Add(Me.PictureBox8)
        Me.Controls.Add(Me.PictureGray)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormFileExplorer"
        Me.Opacity = 0.93R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "File Explorer"
        Me.TransparencyKey = System.Drawing.Color.Lime
        CType(Me.ButtonCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonFileOpen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureGray, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LSCBBAR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LSCBD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LSCBU, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LSCBBACK, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ListBoxDrivers As ListBox
    Friend WithEvents ListBoxFiles As ListBox
    Friend WithEvents PictureBox8 As PictureBox
    Friend WithEvents ButtonCancel As PictureBox
    Friend WithEvents ButtonFileOpen As PictureBox
    Friend WithEvents PictureGray As PictureBox
    Friend WithEvents LabelPath As Label
    Friend WithEvents LSCBBAR As PictureBox
    Friend WithEvents LSCBD As PictureBox
    Friend WithEvents LSCBU As PictureBox
    Friend WithEvents LSCBBACK As PictureBox
    Friend WithEvents Panel1 As Panel
End Class
