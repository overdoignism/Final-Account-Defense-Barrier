<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMain))
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.TextBoxTitle = New System.Windows.Forms.TextBox()
        Me.TextBoxURL = New System.Windows.Forms.TextBox()
        Me.TextBoxNameAddr = New System.Windows.Forms.TextBox()
        Me.TextBoxRegMailPhone = New System.Windows.Forms.TextBox()
        Me.TextBoxNote2Hid = New System.Windows.Forms.TextBox()
        Me.Label_Act_Msg = New System.Windows.Forms.Label()
        Me.TextBoxNote1 = New System.Windows.Forms.TextBox()
        Me.LabelCatalog = New System.Windows.Forms.Label()
        Me.LABVER = New System.Windows.Forms.Label()
        Me.LabelBy = New System.Windows.Forms.Label()
        Me.PictureWinMin = New System.Windows.Forms.PictureBox()
        Me.PicTimerINACT = New System.Windows.Forms.PictureBox()
        Me.PicTimerACT = New System.Windows.Forms.PictureBox()
        Me.PicDIGI_4 = New System.Windows.Forms.PictureBox()
        Me.PicDIGI_3 = New System.Windows.Forms.PictureBox()
        Me.PicDIGI_2 = New System.Windows.Forms.PictureBox()
        Me.PicDIGI_1 = New System.Windows.Forms.PictureBox()
        Me.ButtonRestart = New System.Windows.Forms.PictureBox()
        Me.ButtonHelp = New System.Windows.Forms.PictureBox()
        Me.ButtonExit = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBoxCATMAN = New System.Windows.Forms.PictureBox()
        Me.ButtonSave = New System.Windows.Forms.PictureBox()
        Me.ButtonFileInfo = New System.Windows.Forms.PictureBox()
        Me.ButtonTransCatalog = New System.Windows.Forms.PictureBox()
        Me.ButtonGoDown = New System.Windows.Forms.PictureBox()
        Me.ButtonGoUP = New System.Windows.Forms.PictureBox()
        Me.ButtonDelete = New System.Windows.Forms.PictureBox()
        Me.PictureBoxPwdVi = New System.Windows.Forms.PictureBox()
        Me.PictureBoxPwdCPY = New System.Windows.Forms.PictureBox()
        Me.ButtonLaunch = New System.Windows.Forms.PictureBox()
        Me.ButtonViewNote = New System.Windows.Forms.PictureBox()
        Me.ButtonCopyReg = New System.Windows.Forms.PictureBox()
        Me.ButtonCopyAccount = New System.Windows.Forms.PictureBox()
        Me.PictureBoxPwd = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureGray = New System.Windows.Forms.PictureBox()
        CType(Me.PictureWinMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicTimerINACT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicTimerACT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicDIGI_4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicDIGI_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicDIGI_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicDIGI_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonRestart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonHelp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonExit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxCATMAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonFileInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonTransCatalog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonGoDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonGoUP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxPwdVi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxPwdCPY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonLaunch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonViewNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonCopyReg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonCopyAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxPwd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureGray, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.Color.Black
        Me.ListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 19
        Me.ListBox1.Items.AddRange(New Object() {""})
        Me.ListBox1.Location = New System.Drawing.Point(26, 199)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(286, 266)
        Me.ListBox1.TabIndex = 2
        '
        'TextBoxTitle
        '
        Me.TextBoxTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.TextBoxTitle.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxTitle.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.TextBoxTitle.Location = New System.Drawing.Point(479, 90)
        Me.TextBoxTitle.Name = "TextBoxTitle"
        Me.TextBoxTitle.Size = New System.Drawing.Size(370, 19)
        Me.TextBoxTitle.TabIndex = 5
        '
        'TextBoxURL
        '
        Me.TextBoxURL.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.TextBoxURL.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxURL.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxURL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.TextBoxURL.Location = New System.Drawing.Point(479, 169)
        Me.TextBoxURL.Name = "TextBoxURL"
        Me.TextBoxURL.Size = New System.Drawing.Size(370, 19)
        Me.TextBoxURL.TabIndex = 6
        '
        'TextBoxNameAddr
        '
        Me.TextBoxNameAddr.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.TextBoxNameAddr.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxNameAddr.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxNameAddr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.TextBoxNameAddr.Location = New System.Drawing.Point(479, 248)
        Me.TextBoxNameAddr.Name = "TextBoxNameAddr"
        Me.TextBoxNameAddr.Size = New System.Drawing.Size(370, 19)
        Me.TextBoxNameAddr.TabIndex = 7
        '
        'TextBoxRegMailPhone
        '
        Me.TextBoxRegMailPhone.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.TextBoxRegMailPhone.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxRegMailPhone.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxRegMailPhone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.TextBoxRegMailPhone.Location = New System.Drawing.Point(479, 406)
        Me.TextBoxRegMailPhone.Name = "TextBoxRegMailPhone"
        Me.TextBoxRegMailPhone.Size = New System.Drawing.Size(370, 19)
        Me.TextBoxRegMailPhone.TabIndex = 10
        '
        'TextBoxNote2Hid
        '
        Me.TextBoxNote2Hid.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.TextBoxNote2Hid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxNote2Hid.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxNote2Hid.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.TextBoxNote2Hid.Location = New System.Drawing.Point(479, 564)
        Me.TextBoxNote2Hid.Name = "TextBoxNote2Hid"
        Me.TextBoxNote2Hid.Size = New System.Drawing.Size(370, 19)
        Me.TextBoxNote2Hid.TabIndex = 12
        Me.TextBoxNote2Hid.UseSystemPasswordChar = True
        '
        'Label_Act_Msg
        '
        Me.Label_Act_Msg.AutoEllipsis = True
        Me.Label_Act_Msg.BackColor = System.Drawing.Color.Black
        Me.Label_Act_Msg.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Act_Msg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label_Act_Msg.Location = New System.Drawing.Point(24, 655)
        Me.Label_Act_Msg.Name = "Label_Act_Msg"
        Me.Label_Act_Msg.Size = New System.Drawing.Size(291, 19)
        Me.Label_Act_Msg.TabIndex = 29
        Me.Label_Act_Msg.Text = "(none)"
        Me.Label_Act_Msg.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'TextBoxNote1
        '
        Me.TextBoxNote1.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.TextBoxNote1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxNote1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxNote1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.TextBoxNote1.Location = New System.Drawing.Point(479, 485)
        Me.TextBoxNote1.Name = "TextBoxNote1"
        Me.TextBoxNote1.Size = New System.Drawing.Size(370, 19)
        Me.TextBoxNote1.TabIndex = 11
        '
        'LabelCatalog
        '
        Me.LabelCatalog.BackColor = System.Drawing.Color.FromArgb(CType(CType(42, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(22, Byte), Integer))
        Me.LabelCatalog.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LabelCatalog.ForeColor = System.Drawing.Color.FromArgb(CType(CType(126, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.LabelCatalog.Location = New System.Drawing.Point(26, 90)
        Me.LabelCatalog.Name = "LabelCatalog"
        Me.LabelCatalog.Size = New System.Drawing.Size(286, 19)
        Me.LabelCatalog.TabIndex = 118
        Me.LabelCatalog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LABVER
        '
        Me.LABVER.BackColor = System.Drawing.Color.Black
        Me.LABVER.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LABVER.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LABVER.Location = New System.Drawing.Point(343, 634)
        Me.LABVER.Name = "LABVER"
        Me.LABVER.Size = New System.Drawing.Size(188, 18)
        Me.LABVER.TabIndex = 119
        Me.LABVER.Text = "?"
        '
        'LabelBy
        '
        Me.LabelBy.BackColor = System.Drawing.Color.Black
        Me.LabelBy.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelBy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LabelBy.Location = New System.Drawing.Point(343, 616)
        Me.LabelBy.Name = "LabelBy"
        Me.LabelBy.Size = New System.Drawing.Size(188, 18)
        Me.LabelBy.TabIndex = 120
        Me.LabelBy.Text = "?"
        '
        'PictureWinMin
        '
        Me.PictureWinMin.BackColor = System.Drawing.Color.Black
        Me.PictureWinMin.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureWinMin.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.WinMin
        Me.PictureWinMin.Location = New System.Drawing.Point(141, 8)
        Me.PictureWinMin.Name = "PictureWinMin"
        Me.PictureWinMin.Size = New System.Drawing.Size(30, 26)
        Me.PictureWinMin.TabIndex = 121
        Me.PictureWinMin.TabStop = False
        '
        'PicTimerINACT
        '
        Me.PicTimerINACT.BackColor = System.Drawing.Color.Black
        Me.PicTimerINACT.Cursor = System.Windows.Forms.Cursors.Default
        Me.PicTimerINACT.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.TimerID_INACT
        Me.PicTimerINACT.Location = New System.Drawing.Point(241, 543)
        Me.PicTimerINACT.Name = "PicTimerINACT"
        Me.PicTimerINACT.Size = New System.Drawing.Size(41, 37)
        Me.PicTimerINACT.TabIndex = 116
        Me.PicTimerINACT.TabStop = False
        '
        'PicTimerACT
        '
        Me.PicTimerACT.BackColor = System.Drawing.Color.Black
        Me.PicTimerACT.Cursor = System.Windows.Forms.Cursors.Default
        Me.PicTimerACT.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.TimerID_ACT
        Me.PicTimerACT.Location = New System.Drawing.Point(241, 510)
        Me.PicTimerACT.Name = "PicTimerACT"
        Me.PicTimerACT.Size = New System.Drawing.Size(41, 37)
        Me.PicTimerACT.TabIndex = 115
        Me.PicTimerACT.TabStop = False
        Me.PicTimerACT.Visible = False
        '
        'PicDIGI_4
        '
        Me.PicDIGI_4.BackColor = System.Drawing.Color.Black
        Me.PicDIGI_4.Cursor = System.Windows.Forms.Cursors.Default
        Me.PicDIGI_4.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.DIGI_Y_0
        Me.PicDIGI_4.Location = New System.Drawing.Point(204, 524)
        Me.PicDIGI_4.Name = "PicDIGI_4"
        Me.PicDIGI_4.Size = New System.Drawing.Size(31, 55)
        Me.PicDIGI_4.TabIndex = 114
        Me.PicDIGI_4.TabStop = False
        '
        'PicDIGI_3
        '
        Me.PicDIGI_3.BackColor = System.Drawing.Color.Black
        Me.PicDIGI_3.Cursor = System.Windows.Forms.Cursors.Default
        Me.PicDIGI_3.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.DIGI_Y_0
        Me.PicDIGI_3.Location = New System.Drawing.Point(167, 524)
        Me.PicDIGI_3.Name = "PicDIGI_3"
        Me.PicDIGI_3.Size = New System.Drawing.Size(31, 55)
        Me.PicDIGI_3.TabIndex = 113
        Me.PicDIGI_3.TabStop = False
        '
        'PicDIGI_2
        '
        Me.PicDIGI_2.BackColor = System.Drawing.Color.Black
        Me.PicDIGI_2.Cursor = System.Windows.Forms.Cursors.Default
        Me.PicDIGI_2.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.DIGI_Y_0
        Me.PicDIGI_2.Location = New System.Drawing.Point(125, 524)
        Me.PicDIGI_2.Name = "PicDIGI_2"
        Me.PicDIGI_2.Size = New System.Drawing.Size(31, 55)
        Me.PicDIGI_2.TabIndex = 112
        Me.PicDIGI_2.TabStop = False
        '
        'PicDIGI_1
        '
        Me.PicDIGI_1.BackColor = System.Drawing.Color.Black
        Me.PicDIGI_1.Cursor = System.Windows.Forms.Cursors.Default
        Me.PicDIGI_1.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.DIGI_Y_0
        Me.PicDIGI_1.Location = New System.Drawing.Point(88, 524)
        Me.PicDIGI_1.Name = "PicDIGI_1"
        Me.PicDIGI_1.Size = New System.Drawing.Size(31, 55)
        Me.PicDIGI_1.TabIndex = 111
        Me.PicDIGI_1.TabStop = False
        '
        'ButtonRestart
        '
        Me.ButtonRestart.BackColor = System.Drawing.Color.Black
        Me.ButtonRestart.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonRestart.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_LOGOUT
        Me.ButtonRestart.Location = New System.Drawing.Point(741, 620)
        Me.ButtonRestart.Name = "ButtonRestart"
        Me.ButtonRestart.Size = New System.Drawing.Size(112, 61)
        Me.ButtonRestart.TabIndex = 110
        Me.ButtonRestart.TabStop = False
        '
        'ButtonHelp
        '
        Me.ButtonHelp.BackColor = System.Drawing.Color.Black
        Me.ButtonHelp.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonHelp.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_HELP
        Me.ButtonHelp.Location = New System.Drawing.Point(611, 620)
        Me.ButtonHelp.Name = "ButtonHelp"
        Me.ButtonHelp.Size = New System.Drawing.Size(112, 61)
        Me.ButtonHelp.TabIndex = 109
        Me.ButtonHelp.TabStop = False
        '
        'ButtonExit
        '
        Me.ButtonExit.BackColor = System.Drawing.Color.Black
        Me.ButtonExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonExit.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_Final
        Me.ButtonExit.Location = New System.Drawing.Point(871, 620)
        Me.ButtonExit.Name = "ButtonExit"
        Me.ButtonExit.Size = New System.Drawing.Size(112, 61)
        Me.ButtonExit.TabIndex = 108
        Me.ButtonExit.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.BackColor = System.Drawing.Color.Black
        Me.PictureBox4.Cursor = System.Windows.Forms.Cursors.No
        Me.PictureBox4.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.EPTSLOT_692
        Me.PictureBox4.Location = New System.Drawing.Point(346, 525)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(112, 61)
        Me.PictureBox4.TabIndex = 107
        Me.PictureBox4.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.Black
        Me.PictureBox3.Cursor = System.Windows.Forms.Cursors.No
        Me.PictureBox3.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.EPTSLOT_313
        Me.PictureBox3.Location = New System.Drawing.Point(871, 446)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(112, 61)
        Me.PictureBox3.TabIndex = 106
        Me.PictureBox3.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Black
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.No
        Me.PictureBox1.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.EPTSLOT_246
        Me.PictureBox1.Location = New System.Drawing.Point(871, 51)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(112, 61)
        Me.PictureBox1.TabIndex = 105
        Me.PictureBox1.TabStop = False
        '
        'PictureBoxCATMAN
        '
        Me.PictureBoxCATMAN.BackColor = System.Drawing.Color.Black
        Me.PictureBoxCATMAN.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBoxCATMAN.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_CATMAN
        Me.PictureBoxCATMAN.Location = New System.Drawing.Point(26, 117)
        Me.PictureBoxCATMAN.Name = "PictureBoxCATMAN"
        Me.PictureBoxCATMAN.Size = New System.Drawing.Size(170, 64)
        Me.PictureBoxCATMAN.TabIndex = 104
        Me.PictureBoxCATMAN.TabStop = False
        '
        'ButtonSave
        '
        Me.ButtonSave.BackColor = System.Drawing.Color.Black
        Me.ButtonSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonSave.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_L_save
        Me.ButtonSave.Location = New System.Drawing.Point(346, 51)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(112, 61)
        Me.ButtonSave.TabIndex = 103
        Me.ButtonSave.TabStop = False
        '
        'ButtonFileInfo
        '
        Me.ButtonFileInfo.BackColor = System.Drawing.Color.Black
        Me.ButtonFileInfo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonFileInfo.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_L_fileInfo
        Me.ButtonFileInfo.Location = New System.Drawing.Point(346, 446)
        Me.ButtonFileInfo.Name = "ButtonFileInfo"
        Me.ButtonFileInfo.Size = New System.Drawing.Size(112, 61)
        Me.ButtonFileInfo.TabIndex = 102
        Me.ButtonFileInfo.TabStop = False
        '
        'ButtonTransCatalog
        '
        Me.ButtonTransCatalog.BackColor = System.Drawing.Color.Black
        Me.ButtonTransCatalog.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonTransCatalog.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_L_transKEY
        Me.ButtonTransCatalog.Location = New System.Drawing.Point(346, 368)
        Me.ButtonTransCatalog.Name = "ButtonTransCatalog"
        Me.ButtonTransCatalog.Size = New System.Drawing.Size(112, 61)
        Me.ButtonTransCatalog.TabIndex = 101
        Me.ButtonTransCatalog.TabStop = False
        '
        'ButtonGoDown
        '
        Me.ButtonGoDown.BackColor = System.Drawing.Color.Black
        Me.ButtonGoDown.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonGoDown.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_L_moveDWN
        Me.ButtonGoDown.Location = New System.Drawing.Point(346, 289)
        Me.ButtonGoDown.Name = "ButtonGoDown"
        Me.ButtonGoDown.Size = New System.Drawing.Size(112, 61)
        Me.ButtonGoDown.TabIndex = 100
        Me.ButtonGoDown.TabStop = False
        '
        'ButtonGoUP
        '
        Me.ButtonGoUP.BackColor = System.Drawing.Color.Black
        Me.ButtonGoUP.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonGoUP.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_L_moveUP
        Me.ButtonGoUP.Location = New System.Drawing.Point(346, 209)
        Me.ButtonGoUP.Name = "ButtonGoUP"
        Me.ButtonGoUP.Size = New System.Drawing.Size(112, 61)
        Me.ButtonGoUP.TabIndex = 99
        Me.ButtonGoUP.TabStop = False
        '
        'ButtonDelete
        '
        Me.ButtonDelete.BackColor = System.Drawing.Color.Black
        Me.ButtonDelete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonDelete.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_L_delete
        Me.ButtonDelete.Location = New System.Drawing.Point(346, 130)
        Me.ButtonDelete.Name = "ButtonDelete"
        Me.ButtonDelete.Size = New System.Drawing.Size(112, 61)
        Me.ButtonDelete.TabIndex = 98
        Me.ButtonDelete.TabStop = False
        '
        'PictureBoxPwdVi
        '
        Me.PictureBoxPwdVi.BackColor = System.Drawing.Color.Black
        Me.PictureBoxPwdVi.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBoxPwdVi.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_view_small
        Me.PictureBoxPwdVi.Location = New System.Drawing.Point(871, 289)
        Me.PictureBoxPwdVi.Name = "PictureBoxPwdVi"
        Me.PictureBoxPwdVi.Size = New System.Drawing.Size(112, 28)
        Me.PictureBoxPwdVi.TabIndex = 97
        Me.PictureBoxPwdVi.TabStop = False
        '
        'PictureBoxPwdCPY
        '
        Me.PictureBoxPwdCPY.BackColor = System.Drawing.Color.Black
        Me.PictureBoxPwdCPY.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBoxPwdCPY.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_copy_small
        Me.PictureBoxPwdCPY.Location = New System.Drawing.Point(871, 321)
        Me.PictureBoxPwdCPY.Name = "PictureBoxPwdCPY"
        Me.PictureBoxPwdCPY.Size = New System.Drawing.Size(112, 28)
        Me.PictureBoxPwdCPY.TabIndex = 96
        Me.PictureBoxPwdCPY.TabStop = False
        '
        'ButtonLaunch
        '
        Me.ButtonLaunch.BackColor = System.Drawing.Color.Black
        Me.ButtonLaunch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonLaunch.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_Launch
        Me.ButtonLaunch.Location = New System.Drawing.Point(871, 130)
        Me.ButtonLaunch.Name = "ButtonLaunch"
        Me.ButtonLaunch.Size = New System.Drawing.Size(112, 61)
        Me.ButtonLaunch.TabIndex = 95
        Me.ButtonLaunch.TabStop = False
        '
        'ButtonViewNote
        '
        Me.ButtonViewNote.BackColor = System.Drawing.Color.Black
        Me.ButtonViewNote.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonViewNote.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_view
        Me.ButtonViewNote.Location = New System.Drawing.Point(871, 525)
        Me.ButtonViewNote.Name = "ButtonViewNote"
        Me.ButtonViewNote.Size = New System.Drawing.Size(112, 61)
        Me.ButtonViewNote.TabIndex = 94
        Me.ButtonViewNote.TabStop = False
        '
        'ButtonCopyReg
        '
        Me.ButtonCopyReg.BackColor = System.Drawing.Color.Black
        Me.ButtonCopyReg.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonCopyReg.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_COPY
        Me.ButtonCopyReg.Location = New System.Drawing.Point(871, 368)
        Me.ButtonCopyReg.Name = "ButtonCopyReg"
        Me.ButtonCopyReg.Size = New System.Drawing.Size(112, 61)
        Me.ButtonCopyReg.TabIndex = 93
        Me.ButtonCopyReg.TabStop = False
        '
        'ButtonCopyAccount
        '
        Me.ButtonCopyAccount.BackColor = System.Drawing.Color.Black
        Me.ButtonCopyAccount.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtonCopyAccount.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.button_COPY
        Me.ButtonCopyAccount.Location = New System.Drawing.Point(871, 209)
        Me.ButtonCopyAccount.Name = "ButtonCopyAccount"
        Me.ButtonCopyAccount.Size = New System.Drawing.Size(112, 61)
        Me.ButtonCopyAccount.TabIndex = 92
        Me.ButtonCopyAccount.TabStop = False
        '
        'PictureBoxPwd
        '
        Me.PictureBoxPwd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBoxPwd.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.TOPSEC
        Me.PictureBoxPwd.Location = New System.Drawing.Point(479, 327)
        Me.PictureBoxPwd.Name = "PictureBoxPwd"
        Me.PictureBoxPwd.Size = New System.Drawing.Size(370, 22)
        Me.PictureBoxPwd.TabIndex = 81
        Me.PictureBoxPwd.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.Final_Account_Defense_Barrier.My.Resources.Resource1.main
        Me.PictureBox2.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(995, 698)
        Me.PictureBox2.TabIndex = 42
        Me.PictureBox2.TabStop = False
        '
        'PictureGray
        '
        Me.PictureGray.Location = New System.Drawing.Point(0, 0)
        Me.PictureGray.Name = "PictureGray"
        Me.PictureGray.Size = New System.Drawing.Size(997, 699)
        Me.PictureGray.TabIndex = 117
        Me.PictureGray.TabStop = False
        Me.PictureGray.Visible = False
        '
        'FormMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(995, 698)
        Me.ControlBox = False
        Me.Controls.Add(Me.PictureWinMin)
        Me.Controls.Add(Me.LabelBy)
        Me.Controls.Add(Me.LABVER)
        Me.Controls.Add(Me.LabelCatalog)
        Me.Controls.Add(Me.PicTimerINACT)
        Me.Controls.Add(Me.PicTimerACT)
        Me.Controls.Add(Me.PicDIGI_4)
        Me.Controls.Add(Me.PicDIGI_3)
        Me.Controls.Add(Me.PicDIGI_2)
        Me.Controls.Add(Me.PicDIGI_1)
        Me.Controls.Add(Me.ButtonRestart)
        Me.Controls.Add(Me.ButtonHelp)
        Me.Controls.Add(Me.ButtonExit)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.PictureBoxCATMAN)
        Me.Controls.Add(Me.ButtonSave)
        Me.Controls.Add(Me.ButtonFileInfo)
        Me.Controls.Add(Me.ButtonTransCatalog)
        Me.Controls.Add(Me.ButtonGoDown)
        Me.Controls.Add(Me.ButtonGoUP)
        Me.Controls.Add(Me.ButtonDelete)
        Me.Controls.Add(Me.PictureBoxPwdVi)
        Me.Controls.Add(Me.PictureBoxPwdCPY)
        Me.Controls.Add(Me.ButtonLaunch)
        Me.Controls.Add(Me.ButtonViewNote)
        Me.Controls.Add(Me.ButtonCopyReg)
        Me.Controls.Add(Me.ButtonCopyAccount)
        Me.Controls.Add(Me.PictureBoxPwd)
        Me.Controls.Add(Me.TextBoxNote1)
        Me.Controls.Add(Me.Label_Act_Msg)
        Me.Controls.Add(Me.TextBoxNote2Hid)
        Me.Controls.Add(Me.TextBoxRegMailPhone)
        Me.Controls.Add(Me.TextBoxNameAddr)
        Me.Controls.Add(Me.TextBoxURL)
        Me.Controls.Add(Me.TextBoxTitle)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureGray)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormMain"
        Me.Text = "FADB"
        Me.TransparencyKey = System.Drawing.Color.Lime
        CType(Me.PictureWinMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicTimerINACT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicTimerACT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicDIGI_4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicDIGI_3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicDIGI_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicDIGI_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonRestart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonHelp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonExit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxCATMAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonFileInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonTransCatalog, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonGoDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonGoUP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxPwdVi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxPwdCPY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonLaunch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonViewNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonCopyReg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonCopyAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxPwd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureGray, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents TextBoxTitle As TextBox
    Friend WithEvents TextBoxURL As TextBox
    Friend WithEvents TextBoxNameAddr As TextBox
    Friend WithEvents TextBoxRegMailPhone As TextBox
    Friend WithEvents TextBoxNote2Hid As TextBox
    Friend WithEvents Label_Act_Msg As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents TextBoxNote1 As TextBox
    Friend WithEvents PictureBoxPwd As PictureBox
    Friend WithEvents ButtonCopyAccount As PictureBox
    Friend WithEvents ButtonCopyReg As PictureBox
    Friend WithEvents ButtonViewNote As PictureBox
    Friend WithEvents ButtonLaunch As PictureBox
    Friend WithEvents PictureBoxPwdCPY As PictureBox
    Friend WithEvents PictureBoxPwdVi As PictureBox
    Friend WithEvents ButtonDelete As PictureBox
    Friend WithEvents ButtonGoUP As PictureBox
    Friend WithEvents ButtonGoDown As PictureBox
    Friend WithEvents ButtonTransCatalog As PictureBox
    Friend WithEvents ButtonFileInfo As PictureBox
    Friend WithEvents ButtonSave As PictureBox
    Friend WithEvents PictureBoxCATMAN As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents ButtonHelp As PictureBox
    Friend WithEvents ButtonExit As PictureBox
    Friend WithEvents ButtonRestart As PictureBox
    Friend WithEvents PicDIGI_1 As PictureBox
    Friend WithEvents PicDIGI_2 As PictureBox
    Friend WithEvents PicDIGI_3 As PictureBox
    Friend WithEvents PicDIGI_4 As PictureBox
    Friend WithEvents PicTimerACT As PictureBox
    Friend WithEvents PicTimerINACT As PictureBox
    Friend WithEvents PictureGray As PictureBox
    Friend WithEvents LabelCatalog As Label
    Friend WithEvents LABVER As Label
    Friend WithEvents LabelBy As Label
    Friend WithEvents PictureWinMin As PictureBox
End Class
