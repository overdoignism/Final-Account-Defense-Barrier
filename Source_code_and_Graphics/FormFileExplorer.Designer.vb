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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormFileExplorer))
        Me.ListBoxDrivers = New System.Windows.Forms.ListBox()
        Me.ListBoxFiles = New System.Windows.Forms.ListBox()
        Me.ButtonCancel = New System.Windows.Forms.PictureBox()
        Me.ButtonOK = New System.Windows.Forms.PictureBox()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.PictureGray = New System.Windows.Forms.PictureBox()
        Me.LabelPath = New System.Windows.Forms.Label()
        CType(Me.ButtonCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonOK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureGray, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ListBoxDrivers
        '
        Me.ListBoxDrivers.BackColor = System.Drawing.Color.Black
        Me.ListBoxDrivers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBoxDrivers.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBoxDrivers.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ListBoxDrivers.FormattingEnabled = True
        Me.ListBoxDrivers.ItemHeight = 20
        Me.ListBoxDrivers.Location = New System.Drawing.Point(24, 165)
        Me.ListBoxDrivers.Name = "ListBoxDrivers"
        Me.ListBoxDrivers.Size = New System.Drawing.Size(221, 160)
        Me.ListBoxDrivers.TabIndex = 0
        '
        'ListBoxFiles
        '
        Me.ListBoxFiles.BackColor = System.Drawing.Color.Black
        Me.ListBoxFiles.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBoxFiles.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBoxFiles.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ListBoxFiles.FormattingEnabled = True
        Me.ListBoxFiles.ItemHeight = 20
        Me.ListBoxFiles.Location = New System.Drawing.Point(272, 165)
        Me.ListBoxFiles.Name = "ListBoxFiles"
        Me.ListBoxFiles.Size = New System.Drawing.Size(604, 340)
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
        'ButtonOK
        '
        Me.ButtonOK.BackColor = System.Drawing.Color.Black
        Me.ButtonOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonOK.Image = CType(resources.GetObject("ButtonOK.Image"), System.Drawing.Image)
        Me.ButtonOK.Location = New System.Drawing.Point(22, 466)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(112, 61)
        Me.ButtonOK.TabIndex = 84
        Me.ButtonOK.TabStop = False
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
        Me.LabelPath.BackColor = System.Drawing.Color.Black
        Me.LabelPath.Font = New System.Drawing.Font("Arial Narrow", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelPath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LabelPath.Location = New System.Drawing.Point(193, 64)
        Me.LabelPath.Name = "LabelPath"
        Me.LabelPath.Size = New System.Drawing.Size(683, 19)
        Me.LabelPath.TabIndex = 119
        Me.LabelPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FormFileExplorer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(901, 548)
        Me.ControlBox = False
        Me.Controls.Add(Me.LabelPath)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.ListBoxFiles)
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
        CType(Me.ButtonOK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureGray, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ListBoxDrivers As ListBox
    Friend WithEvents ListBoxFiles As ListBox
    Friend WithEvents PictureBox8 As PictureBox
    Friend WithEvents ButtonCancel As PictureBox
    Friend WithEvents ButtonOK As PictureBox
    Friend WithEvents PictureGray As PictureBox
    Friend WithEvents LabelPath As Label
End Class
