<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormConfig
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
        Me.components = New System.ComponentModel.Container()
        Me.TextBoxCatalog = New System.Windows.Forms.TextBox()
        Me.CB_SIM1 = New System.Windows.Forms.ComboBox()
        Me.CB_SIM2 = New System.Windows.Forms.ComboBox()
        Me.CB_Timer = New System.Windows.Forms.ComboBox()
        Me.TB_AC_KEY = New System.Windows.Forms.ComboBox()
        Me.TB_PW_KEY = New System.Windows.Forms.ComboBox()
        Me.PIC_READONLY = New System.Windows.Forms.PictureBox()
        Me.ButtonCSVEx = New System.Windows.Forms.PictureBox()
        Me.ButtonCSVIM = New System.Windows.Forms.PictureBox()
        Me.ButtonCancel = New System.Windows.Forms.PictureBox()
        Me.ButtonOK = New System.Windows.Forms.PictureBox()
        Me.ButtonTransFullCat = New System.Windows.Forms.PictureBox()
        Me.ButtonDelCat = New System.Windows.Forms.PictureBox()
        Me.PictureBoxConfig = New System.Windows.Forms.PictureBox()
        Me.PictureGray = New System.Windows.Forms.PictureBox()
        Me.ReDrawTimer = New System.Windows.Forms.Timer(Me.components)
        CType(Me.PIC_READONLY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonCSVEx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonCSVIM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonOK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonTransFullCat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonDelCat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxConfig, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureGray, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxCatalog
        '
        Me.TextBoxCatalog.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.TextBoxCatalog.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxCatalog.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxCatalog.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.TextBoxCatalog.Location = New System.Drawing.Point(152, 78)
        Me.TextBoxCatalog.Name = "TextBoxCatalog"
        Me.TextBoxCatalog.Size = New System.Drawing.Size(256, 19)
        Me.TextBoxCatalog.TabIndex = 86
        '
        'CB_SIM1
        '
        Me.CB_SIM1.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.CB_SIM1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CB_SIM1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_SIM1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CB_SIM1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_SIM1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.CB_SIM1.FormattingEnabled = True
        Me.CB_SIM1.Location = New System.Drawing.Point(236, 179)
        Me.CB_SIM1.Name = "CB_SIM1"
        Me.CB_SIM1.Size = New System.Drawing.Size(172, 27)
        Me.CB_SIM1.TabIndex = 88
        '
        'CB_SIM2
        '
        Me.CB_SIM2.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.CB_SIM2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CB_SIM2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_SIM2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CB_SIM2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_SIM2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.CB_SIM2.FormattingEnabled = True
        Me.CB_SIM2.Location = New System.Drawing.Point(236, 281)
        Me.CB_SIM2.Name = "CB_SIM2"
        Me.CB_SIM2.Size = New System.Drawing.Size(172, 27)
        Me.CB_SIM2.TabIndex = 92
        '
        'CB_Timer
        '
        Me.CB_Timer.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.CB_Timer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CB_Timer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_Timer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CB_Timer.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_Timer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.CB_Timer.FormattingEnabled = True
        Me.CB_Timer.Location = New System.Drawing.Point(236, 341)
        Me.CB_Timer.Name = "CB_Timer"
        Me.CB_Timer.Size = New System.Drawing.Size(172, 27)
        Me.CB_Timer.TabIndex = 85
        '
        'TB_AC_KEY
        '
        Me.TB_AC_KEY.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.TB_AC_KEY.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.TB_AC_KEY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TB_AC_KEY.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TB_AC_KEY.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TB_AC_KEY.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.TB_AC_KEY.FormattingEnabled = True
        Me.TB_AC_KEY.Location = New System.Drawing.Point(136, 179)
        Me.TB_AC_KEY.Name = "TB_AC_KEY"
        Me.TB_AC_KEY.Size = New System.Drawing.Size(50, 27)
        Me.TB_AC_KEY.TabIndex = 84
        '
        'TB_PW_KEY
        '
        Me.TB_PW_KEY.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.TB_PW_KEY.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.TB_PW_KEY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TB_PW_KEY.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TB_PW_KEY.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TB_PW_KEY.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.TB_PW_KEY.FormattingEnabled = True
        Me.TB_PW_KEY.Location = New System.Drawing.Point(136, 281)
        Me.TB_PW_KEY.Name = "TB_PW_KEY"
        Me.TB_PW_KEY.Size = New System.Drawing.Size(50, 27)
        Me.TB_PW_KEY.TabIndex = 85
        '
        'PIC_READONLY
        '
        Me.PIC_READONLY.BackColor = System.Drawing.Color.Maroon
        Me.PIC_READONLY.Cursor = System.Windows.Forms.Cursors.Default
        Me.PIC_READONLY.Location = New System.Drawing.Point(129, 12)
        Me.PIC_READONLY.Name = "PIC_READONLY"
        Me.PIC_READONLY.Size = New System.Drawing.Size(36, 17)
        Me.PIC_READONLY.TabIndex = 108
        Me.PIC_READONLY.TabStop = False
        Me.PIC_READONLY.Visible = False
        '
        'ButtonCSVEx
        '
        Me.ButtonCSVEx.BackColor = System.Drawing.Color.Black
        Me.ButtonCSVEx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonCSVEx.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_CAT_CSVEX
        Me.ButtonCSVEx.Location = New System.Drawing.Point(568, 177)
        Me.ButtonCSVEx.Name = "ButtonCSVEx"
        Me.ButtonCSVEx.Size = New System.Drawing.Size(112, 61)
        Me.ButtonCSVEx.TabIndex = 107
        Me.ButtonCSVEx.TabStop = False
        '
        'ButtonCSVIM
        '
        Me.ButtonCSVIM.BackColor = System.Drawing.Color.Black
        Me.ButtonCSVIM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonCSVIM.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_CAT_CSVIM
        Me.ButtonCSVIM.Location = New System.Drawing.Point(451, 177)
        Me.ButtonCSVIM.Name = "ButtonCSVIM"
        Me.ButtonCSVIM.Size = New System.Drawing.Size(112, 61)
        Me.ButtonCSVIM.TabIndex = 106
        Me.ButtonCSVIM.TabStop = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.BackColor = System.Drawing.Color.Black
        Me.ButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonCancel.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_Cancel
        Me.ButtonCancel.Location = New System.Drawing.Point(568, 325)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(112, 61)
        Me.ButtonCancel.TabIndex = 105
        Me.ButtonCancel.TabStop = False
        '
        'ButtonOK
        '
        Me.ButtonOK.BackColor = System.Drawing.Color.Black
        Me.ButtonOK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonOK.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_confirm
        Me.ButtonOK.Location = New System.Drawing.Point(451, 325)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(112, 61)
        Me.ButtonOK.TabIndex = 104
        Me.ButtonOK.TabStop = False
        '
        'ButtonTransFullCat
        '
        Me.ButtonTransFullCat.BackColor = System.Drawing.Color.Black
        Me.ButtonTransFullCat.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonTransFullCat.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_CAT_TRANS
        Me.ButtonTransFullCat.Location = New System.Drawing.Point(451, 110)
        Me.ButtonTransFullCat.Name = "ButtonTransFullCat"
        Me.ButtonTransFullCat.Size = New System.Drawing.Size(112, 61)
        Me.ButtonTransFullCat.TabIndex = 103
        Me.ButtonTransFullCat.TabStop = False
        '
        'ButtonDelCat
        '
        Me.ButtonDelCat.BackColor = System.Drawing.Color.Black
        Me.ButtonDelCat.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonDelCat.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_CAT_DEL
        Me.ButtonDelCat.Location = New System.Drawing.Point(568, 110)
        Me.ButtonDelCat.Name = "ButtonDelCat"
        Me.ButtonDelCat.Size = New System.Drawing.Size(112, 61)
        Me.ButtonDelCat.TabIndex = 102
        Me.ButtonDelCat.TabStop = False
        '
        'PictureBoxConfig
        '
        Me.PictureBoxConfig.BackColor = System.Drawing.Color.Lime
        Me.PictureBoxConfig.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.Form_Config
        Me.PictureBoxConfig.Location = New System.Drawing.Point(0, 0)
        Me.PictureBoxConfig.Name = "PictureBoxConfig"
        Me.PictureBoxConfig.Size = New System.Drawing.Size(717, 418)
        Me.PictureBoxConfig.TabIndex = 87
        Me.PictureBoxConfig.TabStop = False
        '
        'PictureGray
        '
        Me.PictureGray.Location = New System.Drawing.Point(0, 0)
        Me.PictureGray.Name = "PictureGray"
        Me.PictureGray.Size = New System.Drawing.Size(717, 418)
        Me.PictureGray.TabIndex = 98
        Me.PictureGray.TabStop = False
        '
        'ReDrawTimer
        '
        Me.ReDrawTimer.Interval = 50
        '
        'FormConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(714, 415)
        Me.ControlBox = False
        Me.Controls.Add(Me.PIC_READONLY)
        Me.Controls.Add(Me.ButtonCSVEx)
        Me.Controls.Add(Me.ButtonCSVIM)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.ButtonTransFullCat)
        Me.Controls.Add(Me.ButtonDelCat)
        Me.Controls.Add(Me.CB_Timer)
        Me.Controls.Add(Me.TB_PW_KEY)
        Me.Controls.Add(Me.CB_SIM2)
        Me.Controls.Add(Me.TB_AC_KEY)
        Me.Controls.Add(Me.CB_SIM1)
        Me.Controls.Add(Me.TextBoxCatalog)
        Me.Controls.Add(Me.PictureBoxConfig)
        Me.Controls.Add(Me.PictureGray)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormConfig"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Settings"
        Me.TransparencyKey = System.Drawing.Color.Lime
        CType(Me.PIC_READONLY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonCSVEx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonCSVIM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonOK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonTransFullCat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonDelCat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxConfig, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureGray, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBoxConfig As PictureBox
    Friend WithEvents TextBoxCatalog As TextBox
    Friend WithEvents CB_SIM1 As ComboBox
    Friend WithEvents CB_SIM2 As ComboBox
    Friend WithEvents CB_Timer As ComboBox
    Friend WithEvents PictureGray As PictureBox
    Friend WithEvents ButtonTransFullCat As PictureBox
    Friend WithEvents ButtonDelCat As PictureBox
    Friend WithEvents ButtonCancel As PictureBox
    Friend WithEvents ButtonOK As PictureBox
    Friend WithEvents TB_AC_KEY As ComboBox
    Friend WithEvents TB_PW_KEY As ComboBox
    Friend WithEvents ButtonCSVIM As PictureBox
    Friend WithEvents ButtonCSVEx As PictureBox
    Friend WithEvents PIC_READONLY As PictureBox
    Friend WithEvents ReDrawTimer As Timer
End Class
