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
        Me.LabelPath = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LSCBBAR = New System.Windows.Forms.PictureBox()
        Me.LSCBD = New System.Windows.Forms.PictureBox()
        Me.LSCBU = New System.Windows.Forms.PictureBox()
        Me.LSCBBACK = New System.Windows.Forms.PictureBox()
        Me.ButtonCancel = New System.Windows.Forms.PictureBox()
        Me.ButtonFileOpen = New System.Windows.Forms.PictureBox()
        Me.PicFileExp = New System.Windows.Forms.PictureBox()
        Me.PictureGray = New System.Windows.Forms.PictureBox()
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher()
        Me.Panel1.SuspendLayout()
        CType(Me.LSCBBAR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LSCBD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LSCBU, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LSCBBACK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonFileOpen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicFileExp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureGray, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ListBoxDrivers
        '
        Me.ListBoxDrivers.BackColor = System.Drawing.Color.Black
        Me.ListBoxDrivers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBoxDrivers.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBoxDrivers.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ListBoxDrivers.FormattingEnabled = True
        Me.ListBoxDrivers.ItemHeight = 30
        Me.ListBoxDrivers.Location = New System.Drawing.Point(37, 284)
        Me.ListBoxDrivers.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.ListBoxDrivers.Name = "ListBoxDrivers"
        Me.ListBoxDrivers.Size = New System.Drawing.Size(394, 450)
        Me.ListBoxDrivers.TabIndex = 0
        '
        'ListBoxFiles
        '
        Me.ListBoxFiles.BackColor = System.Drawing.Color.Black
        Me.ListBoxFiles.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBoxFiles.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBoxFiles.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ListBoxFiles.FormattingEnabled = True
        Me.ListBoxFiles.IntegralHeight = False
        Me.ListBoxFiles.ItemHeight = 30
        Me.ListBoxFiles.Location = New System.Drawing.Point(0, 0)
        Me.ListBoxFiles.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.ListBoxFiles.Name = "ListBoxFiles"
        Me.ListBoxFiles.Size = New System.Drawing.Size(1080, 598)
        Me.ListBoxFiles.TabIndex = 3
        '
        'LabelPath
        '
        Me.LabelPath.AutoEllipsis = True
        Me.LabelPath.BackColor = System.Drawing.Color.Black
        Me.LabelPath.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelPath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LabelPath.Location = New System.Drawing.Point(326, 110)
        Me.LabelPath.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.LabelPath.Name = "LabelPath"
        Me.LabelPath.Size = New System.Drawing.Size(1211, 33)
        Me.LabelPath.TabIndex = 119
        Me.LabelPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ListBoxFiles)
        Me.Panel1.Location = New System.Drawing.Point(474, 284)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1032, 600)
        Me.Panel1.TabIndex = 131
        '
        'LSCBBAR
        '
        Me.LSCBBAR.BackColor = System.Drawing.Color.Black
        Me.LSCBBAR.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.SCB_BAR
        Me.LSCBBAR.Location = New System.Drawing.Point(1508, 312)
        Me.LSCBBAR.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.LSCBBAR.Name = "LSCBBAR"
        Me.LSCBBAR.Size = New System.Drawing.Size(30, 103)
        Me.LSCBBAR.TabIndex = 130
        Me.LSCBBAR.TabStop = False
        '
        'LSCBD
        '
        Me.LSCBD.BackColor = System.Drawing.Color.Black
        Me.LSCBD.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.SCB_S_DW
        Me.LSCBD.Location = New System.Drawing.Point(1508, 854)
        Me.LSCBD.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.LSCBD.Name = "LSCBD"
        Me.LSCBD.Size = New System.Drawing.Size(30, 30)
        Me.LSCBD.TabIndex = 129
        Me.LSCBD.TabStop = False
        '
        'LSCBU
        '
        Me.LSCBU.BackColor = System.Drawing.Color.Black
        Me.LSCBU.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.SCB_S_UP
        Me.LSCBU.Location = New System.Drawing.Point(1508, 282)
        Me.LSCBU.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.LSCBU.Name = "LSCBU"
        Me.LSCBU.Size = New System.Drawing.Size(30, 30)
        Me.LSCBU.TabIndex = 128
        Me.LSCBU.TabStop = False
        '
        'LSCBBACK
        '
        Me.LSCBBACK.BackColor = System.Drawing.Color.Black
        Me.LSCBBACK.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.SCB_BARV
        Me.LSCBBACK.Location = New System.Drawing.Point(1508, 312)
        Me.LSCBBACK.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.LSCBBACK.Name = "LSCBBACK"
        Me.LSCBBACK.Size = New System.Drawing.Size(30, 542)
        Me.LSCBBACK.TabIndex = 127
        Me.LSCBBACK.TabStop = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.BackColor = System.Drawing.Color.Black
        Me.ButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonCancel.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_Cancel
        Me.ButtonCancel.Location = New System.Drawing.Point(242, 788)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(196, 107)
        Me.ButtonCancel.TabIndex = 85
        Me.ButtonCancel.TabStop = False
        '
        'ButtonFileOpen
        '
        Me.ButtonFileOpen.BackColor = System.Drawing.Color.Black
        Me.ButtonFileOpen.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonFileOpen.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_OpenF
        Me.ButtonFileOpen.Location = New System.Drawing.Point(37, 788)
        Me.ButtonFileOpen.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.ButtonFileOpen.Name = "ButtonFileOpen"
        Me.ButtonFileOpen.Size = New System.Drawing.Size(196, 107)
        Me.ButtonFileOpen.TabIndex = 84
        Me.ButtonFileOpen.TabStop = False
        '
        'PicFileExp
        '
        Me.PicFileExp.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.File_expo_New
        Me.PicFileExp.Location = New System.Drawing.Point(0, 0)
        Me.PicFileExp.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.PicFileExp.Name = "PicFileExp"
        Me.PicFileExp.Size = New System.Drawing.Size(1577, 959)
        Me.PicFileExp.TabIndex = 53
        Me.PicFileExp.TabStop = False
        '
        'PictureGray
        '
        Me.PictureGray.Location = New System.Drawing.Point(0, 0)
        Me.PictureGray.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.PictureGray.Name = "PictureGray"
        Me.PictureGray.Size = New System.Drawing.Size(1577, 959)
        Me.PictureGray.TabIndex = 86
        Me.PictureGray.TabStop = False
        Me.PictureGray.Visible = False
        '
        'FileSystemWatcher1
        '
        Me.FileSystemWatcher1.EnableRaisingEvents = True
        Me.FileSystemWatcher1.NotifyFilter = CType((System.IO.NotifyFilters.FileName Or System.IO.NotifyFilters.DirectoryName), System.IO.NotifyFilters)
        Me.FileSystemWatcher1.SynchronizingObject = Me
        '
        'FormFileExplorer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(168.0!, 168.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.Lime
        Me.ClientSize = New System.Drawing.Size(1572, 917)
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
        Me.Controls.Add(Me.PicFileExp)
        Me.Controls.Add(Me.PictureGray)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormFileExplorer"
        Me.Opacity = 0.93R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "File Explorer"
        Me.TransparencyKey = System.Drawing.Color.Lime
        Me.Panel1.ResumeLayout(False)
        CType(Me.LSCBBAR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LSCBD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LSCBU, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LSCBBACK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonFileOpen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicFileExp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureGray, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ListBoxDrivers As ListBox
    Friend WithEvents ListBoxFiles As ListBox
    Friend WithEvents PicFileExp As PictureBox
    Friend WithEvents ButtonCancel As PictureBox
    Friend WithEvents ButtonFileOpen As PictureBox
    Friend WithEvents PictureGray As PictureBox
    Friend WithEvents LabelPath As Label
    Friend WithEvents LSCBBAR As PictureBox
    Friend WithEvents LSCBD As PictureBox
    Friend WithEvents LSCBU As PictureBox
    Friend WithEvents LSCBBACK As PictureBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents FileSystemWatcher1 As IO.FileSystemWatcher
End Class
