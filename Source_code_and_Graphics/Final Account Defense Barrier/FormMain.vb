'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports System.Text
Imports System.Threading

Public Class FormMain

    Dim TheSalt() As Byte

    Dim NowProcFile As String
    Dim NowDataStringCpt As String
    Dim NowTitleStringCpt As String

    Dim FilesList1() As FileLists

    Private VG_Data_Done As Boolean = False
    Private VG_Title_Done As Boolean = False
    Private VG_CCC_Done As Boolean = False
    Private FillTrash() As String

    Private LastActionSec As DateTime

    Dim version As Version = Reflection.Assembly.GetEntryAssembly().GetName().Version
    Dim versionNumber As String = version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision

    Dim DbTester As New Windows.Forms.Timer()
    Dim FADB_TimerService As New Windows.Forms.Timer()

    Dim IsShowingMessage As Boolean
    Dim AccountMoving As Boolean


    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim LoadLangFile As String = ""

        '=========== Load Bitmap Resource - Part 1
        Dim t1 As New Threading.Thread(Sub() Load_Bitmap_For_Threads(0)) 'FormLogin
        Dim t2 As New Threading.Thread(Sub() Load_Bitmap_For_Threads(5)) 'PassKDF
        Dim t3 As New Threading.Thread(Sub() Load_Bitmap_For_Threads(6)) 'FormMain
        t1.Start()
        t2.Start()
        t3.Start()

        '===================== Arguments
        Sys_Chk.Use_Secure_Desktop = True
        Sys_Chk._KDF_Type = 0

        Dim Arguments() As String = Environment.GetCommandLineArgs()

        For Each ArgStr As String In Arguments

            If ArgStr.Length <= 100 Then

                If ArgStr.ToUpper.StartsWith(No_Sec_Desk_str) Then Sys_Chk.Use_Secure_Desktop = False

                If ArgStr.ToUpper.StartsWith(Allow_CAP_SCR_str) Then Sys_Chk.Screen_Capture_Allowed = True

                If ArgStr.ToUpper.StartsWith(OPACITY_str + ":") Then
                    Dim TMPSTR1() As String = ArgStr.Split(":")
                    If TMPSTR1.Length > 1 Then
                        Sys_Chk._OpacitySng = Val(TMPSTR1(1)) / 100
                        Me.Opacity = Sys_Chk._OpacitySng
                    End If
                End If

                If ArgStr.ToUpper.StartsWith(SALT_str + ":") Then
                    Dim TMPSTR1() As String = ArgStr.Split(":")
                    If TMPSTR1.Length > 1 Then
                        Dim SHA256_Worker As New Security.Cryptography.SHA256CryptoServiceProvider
                        TheSalt = SHA256_Worker.ComputeHash(System.Text.Encoding.Unicode.GetBytes(TMPSTR1(1)))
                    End If
                End If

                If ArgStr.ToUpper.StartsWith(LangFile_str + ":") Then
                    Dim TMPSTR1() As String = ArgStr.Split(":")
                    If TMPSTR1.Length > 1 Then
                        LoadLangFile = TMPSTR1(1)
                        LIdx = 4
                    End If
                End If

                If ArgStr.ToUpper.StartsWith(KDFType_str + ":") Then
                    Dim TMPSTR1() As String = ArgStr.Split(":")
                    If TMPSTR1.Length > 1 Then
                        Integer.TryParse(TMPSTR1(1), Sys_Chk._KDF_Type)
                    End If
                End If
                If Sys_Chk._KDF_Type > 3 Or Sys_Chk._KDF_Type < 1 Then Sys_Chk._KDF_Type = 1


            End If

        Next

        '==============Language Load
        Load_Language_From_Resource()
        If LIdx = 0 Then
            MakeZh_cnTxt()
            LIdx = GetSysLangCode()
        Else
            Load_Language_File(LoadLangFile)
        End If

        If Sys_Chk.OS_ver < 62 Then MSGBOXNEW(LangStrs(LIdx, UsingTxt.OT_OSo), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray)

        '===============================================
        'NoNotice = True 'for test
        'SecureDesktop = True 'for test

        'If Not No_RunAs_Chk Then
        '    If Not Sys_Chk.Running_Admin Then
        '        If MSGBOXNEW(TextStrs(109), MsgBoxStyle.YesNo, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, New PictureBox) = DialogResult.OK Then
        '            RestartApp2(True, Me)
        '        End If
        '    End If
        'End If

        '================= Get Monitor Scale
        Me.CenterToScreen()
        GetMonScale(Me)

        '================= Not allow capture screen
        If Not Sys_Chk.Screen_Capture_Allowed Then 'Disable Screen Capture
            SetWindowDisplayAffinity(Me.Handle, WDA_EXCLUDEFROMCAPTURE)
        End If

        '========= BIP39 Load
        Load_BIP39_Word()

        'Me.SetStyle(ControlStyles.UserPaint, True)
        'Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        'Me.SetStyle(ControlStyles.DoubleBuffer, True)
        'Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)

        '================================ Login stage =====
        t1.Join()
        t2.Join()
        Dim TmpDR As DialogResult
        If Sys_Chk.Use_Secure_Desktop Then

            Sys_Chk.Secure_Desktop_Name = Security.Cryptography.ProtectedData.Protect(
                        System.Text.Encoding.Unicode.GetBytes(Random_Strs(7, 9, 0)), Nothing,
                        DataProtectionScope.CurrentUser)

            TmpDR = LogInFormWork(LangStrs(LIdx, UsingTxt.Ca_CLin), DirName, AES_KEY_Protected, DERIVED_IDT_Protected, 0, Nothing, Sys_Chk, PictureGray, TheSalt)
        Else
            TmpDR = LogInFormWork(LangStrs(LIdx, UsingTxt.Ca_CLin), DirName, AES_KEY_Protected, DERIVED_IDT_Protected, 0, Nothing, Sys_Chk, PictureGray, TheSalt, Me)
        End If

        If TmpDR <> DialogResult.OK Then End_Program()

        FullGC()
        '=============================================


        '=========== Load Bitmap Resource - Part 2
        Dim t4 As New Threading.Thread(Sub() Load_Bitmap_For_Threads(1)) 'FormLogin - Normal
        Dim t5 As New Threading.Thread(Sub() Load_Bitmap_For_Threads(2)) 'FormLogin - Password
        Dim t6 As New Threading.Thread(Sub() Load_Bitmap_For_Threads(4)) 'FormSetting
        t4.Start()
        t5.Start()
        t6.Start()


        '============= Set text and tooltip
        Dim ToolTip1 As New System.Windows.Forms.ToolTip()
        ToolTip1.SetToolTip(TextBoxTitle, LangStrs(LIdx, UsingTxt.TT_Tt))
        ToolTip1.SetToolTip(TextBoxURL, LangStrs(LIdx, UsingTxt.TT_Ur))
        ToolTip1.SetToolTip(TextBoxNameAddr, LangStrs(LIdx, UsingTxt.TT_Un))
        ToolTip1.SetToolTip(TextBoxRegMailPhone, LangStrs(LIdx, UsingTxt.TT_Rm))
        ToolTip1.SetToolTip(TextBoxNote1, LangStrs(LIdx, UsingTxt.TT_N1))
        ToolTip1.SetToolTip(TextBoxNote2Hid, LangStrs(LIdx, UsingTxt.TT_N2))
        ToolTip1.SetToolTip(ButtonHotkeyMode, LangStrs(LIdx, UsingTxt.OT_FSK))
        ToolTip1.SetToolTip(SysChk_Textbox, Author_str)
        ToolTip1.InitialDelay = 1

        FormConfig.CB_SIM1.Items.Add(LangStrs(LIdx, UsingTxt.IT_DI))
        FormConfig.CB_SIM2.Items.Add(LangStrs(LIdx, UsingTxt.IT_DI))
        FormConfig.CB_SIM1.Items.Add(LangStrs(LIdx, UsingTxt.IT_co))
        FormConfig.CB_SIM2.Items.Add(LangStrs(LIdx, UsingTxt.IT_co))
        FormConfig.CB_SIM1.Items.Add(LangStrs(LIdx, UsingTxt.IT_cpc))
        FormConfig.CB_SIM2.Items.Add(LangStrs(LIdx, UsingTxt.IT_cpc))
        FormConfig.CB_SIM1.Items.Add(LangStrs(LIdx, UsingTxt.IT_sk))
        FormConfig.CB_SIM2.Items.Add(LangStrs(LIdx, UsingTxt.IT_sk))
        FormConfig.CB_SIM1.Items.Add(LangStrs(LIdx, UsingTxt.IT_Hy))
        FormConfig.CB_SIM2.Items.Add(LangStrs(LIdx, UsingTxt.IT_Hy))


        For Each TmpStr As String In SCutChar
            FormConfig.TB_AC_KEY.Items.Add(TmpStr)
            FormConfig.TB_PW_KEY.Items.Add(TmpStr)
        Next

        FormConfig.CB_Timer.Items.Add(LangStrs(LIdx, UsingTxt.IT_DI))
        FormConfig.CB_Timer.Items.Add(LangStrs(LIdx, UsingTxt.IT_30s))
        FormConfig.CB_Timer.Items.Add("1 " + LangStrs(LIdx, UsingTxt.IT_min))
        FormConfig.CB_Timer.Items.Add("3 " + LangStrs(LIdx, UsingTxt.IT_min))
        FormConfig.CB_Timer.Items.Add("5 " + LangStrs(LIdx, UsingTxt.IT_min))
        FormConfig.CB_Timer.Items.Add("10 " + LangStrs(LIdx, UsingTxt.IT_min))
        FormConfig.CB_Timer.Items.Add("30 " + LangStrs(LIdx, UsingTxt.IT_min))
        '===========================



        '====================== Listbox Scrollbar
        ListBox_SB_Init()

        '===================================== AutoCountDownClose
        AutoCloseTimer.Interval = 1000
        AutoCloseTimer.Enabled = True
        FormConfig.CB_Timer.SelectedIndex = 0
        ACTLimit = 0
        ACTCount = 0

        Dim ACT_MF As New DetectActivityMessageFilter
        Application.AddMessageFilter(ACT_MF)
        '===========================================================

        '=====================Init Catalog setting
        ReDim CAT_setting_Str(6)
        CAT_setting_Str(5) = "1"
        Read_CatDatas()
        '=================================

        '=====================Timer Init
        DbTester.Interval = 1000
        DbTester.Enabled = True
        AddHandler DbTester.Tick, AddressOf Timer2_Tick

        FADB_TimerService.Interval = 500
        FADB_TimerService.Enabled = True
        AddHandler FADB_TimerService.Tick, AddressOf FADB_TimerService_Tick


        '========= Read-only mode
        Select Case CreateLockFile()
            Case 0
            Case Else
                MSGBOXNEW(LangStrs(LIdx, UsingTxt.OT_ROM) + D_vbcrlf + LangStrs(LIdx, UsingTxt.OT_LOK), MsgBoxStyle.Exclamation, LangStrs(LIdx, UsingTxt.Ti_Att), Me, PictureGray)
        End Select

        If Sys_Chk._ReadOnlyMode Then
            ButtonPwd.Image = PwdGrayImage
            ButtonPwd.Enabled = False
            TextBoxTitle.ReadOnly = True
            TextBoxURL.ReadOnly = True
            TextBoxNameAddr.ReadOnly = True
            TextBoxRegMailPhone.ReadOnly = True
            TextBoxNote1.ReadOnly = True
            TextBoxNote2Hid.ReadOnly = True
            PIC_READONLY.Image = New Bitmap(My.Resources.Resource1.Read_Only_Img)
            PIC_READONLY.Visible = True
        End If
        '===========================================

        t3.Join()
        t5.Join()
        LastAct(LangStrs(LIdx, UsingTxt.Ca_Load))
        PictureBoxMain.Image = FormMainBitmap


        '===================== File System Watch
        Dim FullPath As String = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName
        FullPath = System.IO.Path.GetDirectoryName(FullPath) + "\" + DirName
        FullPath = Replace(FullPath, ":\\", ":\")
        FileSystemWatcher1.Path = DirName
        FileSystemWatcher1.NotifyFilter = NotifyFilters.FileName Or NotifyFilters.LastWrite
        FileSystemWatcher1.InternalBufferSize = 64 * 1024
        FileSystemWatcher1.EnableRaisingEvents = True


        'ListBoxAccounts.DrawMode = DrawMode.OwnerDrawFixed

    End Sub

    Dim Last_Risk_Idx As Integer = 0

    Private Sub Timer2_Tick(sender As Object, e As EventArgs)

        If Not Sys_Chk.HasDebugger Then
            Sys_Chk.HasDebugger = IsDebugged()
        End If

        If Sys_Chk.HasDebugger And Not Sys_Chk.HasDebugger_Warned Then
            Sys_Chk.HasDebugger_Warned = True
            MSGBOXNEW(LangStrs(LIdx, UsingTxt.OT_dbg), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Cri), Me, PictureGray)
        End If

        If Sys_Chk.Found_Bad_MSFile And Not Sys_Chk.Found_Bad_MSFile_Warned Then
            Sys_Chk.Found_Bad_MSFile_Warned = True
            MSGBOXNEW(LangStrs(LIdx, UsingTxt.OT_bwf), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Cri), Me, PictureGray)
        End If

        Dim Risk_Index As Integer = Get_Risk_Index()

        If Risk_Index <> Last_Risk_Idx Then

            SysChk_Textbox.ForeColor = Get_Risk_Color(Risk_Index)
            Panel_SysChk.BackColor = SysChk_Textbox.ForeColor

            Sys_Chk._Self_Ver_Str = "v" + versionNumber
            SysChk_Textbox.Text = Sys_Chk._Self_Ver_Str + vbCrLf + Get_Risk_Message(Risk_Index)

            Last_Risk_Idx = Risk_Index

        End If

    End Sub

    Private Sub FADB_TimerService_Tick(sender As Object, e As EventArgs)

        '=====================Last Action Part
        Dim DurTime As Long
        Dim timeSpan As TimeSpan = DateTime.Now - LastActionSec
        DurTime = CType(timeSpan.TotalSeconds, Long)

        If DurTime <= 59 Then
            Label_Act_Last.Text = "+ " + DurTime.ToString + " s"
        ElseIf (DurTime >= 60) And (DurTime <= 3600) Then
            Label_Act_Last.Text = "+ " + (DurTime \ 60).ToString + " min"
        Else
            Label_Act_Last.Text = "Over 1 hour."
        End If

        '============ Move Button Holding
        If B_GoUP_Holding Then
            If Holding_CountDown > 0 Then
                ButtonGoUP.Image = CountDownOnImg(LBTN_moveUP_on, Holding_CountDown.ToString, 17)
                Holding_CountDown -= 1
            Else
                B_GoUP_Holding_InTimer = True
                ButtonGoUP.Image = CountDownOnImg(LBTN_moveUP_on, "Auto", 15)
                MoveFileUpDown(1, False)
            End If

        End If

        If B_GoDN_Holding Then

            If Holding_CountDown > 0 Then
                ButtonGoDown.Image = CountDownOnImg(LBTN_moveDWN_on, Holding_CountDown.ToString, 17)
                Holding_CountDown -= 1
            Else
                B_GoDN_Holding_InTimer = True
                ButtonGoDown.Image = CountDownOnImg(LBTN_moveDWN_on, "Auto", 15)
                MoveFileUpDown(2, False)
            End If

        End If

    End Sub

    '============== File Seq Move ================ 
    Dim B_GoUP_Holding As Boolean
    Dim B_GoUP_Holding_InTimer As Boolean
    Dim B_GoDN_Holding As Boolean
    Dim B_GoDN_Holding_InTimer As Boolean
    Dim Holding_CountDown As Integer

    Private Sub ButtonGo_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonGoUP.MouseDown, ButtonGoDown.MouseDown

        FileSystemWatcher1.EnableRaisingEvents = False

        Select Case sender.Name

            Case "ButtonGoUP"
                B_GoUP_Holding = True
                Holding_CountDown = 3

            Case "ButtonGoDown"
                B_GoDN_Holding = True
                Holding_CountDown = 3

        End Select

    End Sub

    Private Sub ButtonGo_MouseUp(sender As Object, e As MouseEventArgs) Handles _
        ButtonGoUP.MouseUp, ButtonGoDown.MouseUp

        Select Case sender.Name

            Case "ButtonGoUP"
                B_GoUP_Holding = False
                B_GoUP_Holding_InTimer = False
                ButtonGoUP.Image = LBTN_moveUP_on

            Case "ButtonGoDown"
                B_GoDN_Holding = False
                B_GoDN_Holding_InTimer = False
                ButtonGoDown.Image = LBTN_moveDWN_on
        End Select

        FileSystemWatcher1.EnableRaisingEvents = True

    End Sub

    Private Sub ButtonGo_Click(sender As Object, e As EventArgs) Handles ButtonGoUP.Click, ButtonGoDown.Click

        Select Case sender.Name

            Case "ButtonGoUP"
                If Not B_GoUP_Holding_InTimer Then MoveFileUpDown(1, False)

            Case "ButtonGoDown"
                If Not B_GoDN_Holding_InTimer Then MoveFileUpDown(2, False)

        End Select

    End Sub

    Private Sub MoveFileUpDown(UpDown As Integer, GoRefresh As Boolean)

        Dim nextFilename As String
        Dim NowSelect As Integer = ListBoxAccounts.SelectedIndex
        Dim NowSourceFile As String = FilesList1(NowSelect).FileName
        Dim NextSelect As Integer
        Dim NowWorkName As String = ListBoxAccounts.Items(NowSelect)
        Dim ReallyDo As Boolean = False
        Dim LastActStr As String = ""

        Select Case UpDown

            Case 1

                If NowSelect >= 2 Then
                    NextSelect = NowSelect - 1
                    ReallyDo = True
                    LastActStr = Replace(LangStrs(LIdx, UsingTxt.RG_MUp), "$$$", NowWorkName)
                Else
                    Exit Sub
                End If

            Case 2

                If NowSelect < ListBoxAccounts.Items.Count - 1 Then
                    NextSelect = NowSelect + 1
                    ReallyDo = True
                    LastActStr = Replace(LangStrs(LIdx, UsingTxt.RG_MDn), "$$$", NowWorkName)
                Else
                    Exit Sub
                End If

        End Select

        Dim DoLoopCounter As Integer

        If ReallyDo Then

            nextFilename = FilesList1(NextSelect).FileName

            System.IO.File.Move(NowSourceFile, NowSourceFile + ".TMP")
            Do While Not System.IO.File.Exists(NowSourceFile + ".TMP")
                Thread.Sleep(50)
                DoLoopCounter += 1
                If DoLoopCounter = 100 Then Exit Sub 'Error
            Loop

            System.IO.File.Move(nextFilename, NowSourceFile)
            System.IO.File.Move(NowSourceFile + ".TMP", nextFilename)

            Dim tempFileList As FileLists = FilesList1(NowSelect)
            FilesList1(NowSelect).ShowName = FilesList1(NextSelect).ShowName
            FilesList1(NextSelect).ShowName = tempFileList.ShowName
            FilesList1(NowSelect).FileIsBad = FilesList1(NextSelect).FileIsBad
            FilesList1(NextSelect).FileIsBad = tempFileList.FileIsBad
            NowProcFile = FilesList1(NextSelect).FileName

            If Not GoRefresh Then

                AccountMoving = True
                Dim tempObj As Object = ListBoxAccounts.Items(NowSelect)
                ListBoxAccounts.Items(NowSelect) = ListBoxAccounts.Items(NextSelect)
                ListBoxAccounts.Items(NextSelect) = tempObj
                ListBoxAccounts.SelectedIndex = NextSelect
                AccountMoving = False
                LSCBBAR_GoCorrectPos()

            Else
                GetList()
                Go_ListBoxIdx(NextSelect)
            End If

            LastAct(LastActStr)

        End If

    End Sub

    '============== Subs and functions ================

    Private Sub ListBoxAccounts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles _
        ListBoxAccounts.SelectedIndexChanged

        If Not AccountMoving Then
            Read_and_decrypt(ListBoxAccounts.SelectedIndex)
            LSCBBAR_GoCorrectPos()
            FullGC()
        End If

    End Sub

    Private Sub Read_and_decrypt(TheIndex As Long)

        Dim ErrFlag As Boolean = False
        Dim Hresult As Integer = 0

        IsShowingMessage = False
        ReadingWorking = True
        TextBoxTitle.Text = ""
        TextBoxURL.Text = ""
        TextBoxNameAddr.Text = ""
        TextBoxRegMailPhone.Text = ""
        TextBoxNote1.Text = ""
        TextBoxNote2Hid.Text = ""
        TextBox_BHKMHelper.Text = "0"
        ButtonHotkeyMode.Image = b_HKO_off

        If TheIndex >= 0 Then
            ButtonGoUP.Enabled = False
            ButtonGoUP.Image = LBTN_MoveU_Di
            ButtonGoDown.Enabled = False
            ButtonGoDown.Image = LBTN_MoveD_Di
            ButtonTransCatalog.Enabled = False
            ButtonTransCatalog.Image = LBTN_TKey_Di
            ButtonFileInfo.Enabled = False
            ButtonFileInfo.Image = LBTN_FInfo_Di
            ButtonSave.Enabled = False
            ButtonSave.Image = LBTN_Save_Di
            ButtonDelete.Enabled = False
            ButtonDelete.Image = LBTN_Del_Di
            TextBoxNote2Hid.PasswordChar = "●"
            ButtonViewNote.Image = b_view
            VG_CCC_Done = False
        Else
            Exit Sub
        End If

        If TheIndex = 0 Then

            NowProcFile = ""
            VG_Data_Done = True
            VG_Title_Done = True
            ReadingWorking = False
            NowPassStatue = 0

            CurrentAccountPass = Security.Cryptography.ProtectedData.
                 Protect(Encoding.Unicode.GetBytes(""), Nothing, DataProtectionScope.CurrentUser)

            LastAct(LangStrs(LIdx, UsingTxt.IT_nc))
            LSCBBAR_GoCorrectPos()

            Exit Sub

        Else

            Dim AES_IV_USE(15) As Byte

            If Not Sys_Chk._ReadOnlyMode Then
                ButtonDelete.Enabled = True
                ButtonDelete.Image = LBTN_Del_En
            End If

            NowProcFile = FilesList1(TheIndex).FileName
            If New System.IO.FileInfo(NowProcFile).Length > 1048576 Then FilesList1(TheIndex).FileIsBad = True

            If FilesList1(TheIndex).FileIsBad Then
                ErrFlag = True
            Else

                Try
                    '==================== Stage 1 init
                    Dim TheEncLib As New Encode_Libs
                    Dim IOreader1 As New System.IO.StreamReader(NowProcFile)

                    TextBoxTitle.Text = FilesList1(TheIndex).ShowName

                    Dim AllFileStr() As String = Replace(IOreader1.ReadToEnd, vbCrLf, vbCr).Split(vbCr)

                    IOreader1.Close()
                    IOreader1.Dispose()

                    If AllFileStr.Length = 4 Then

                        Dim SHA256_Worker As New Security.Cryptography.SHA256CryptoServiceProvider
                        Dim TmpWork1 As String = AllFileStr(0) + vbCrLf + AllFileStr(1) + vbCrLf + AllFileStr(2)

                        Security.Cryptography.ProtectedMemory.Unprotect(DERIVED_IDT_Protected, MemoryProtectionScope.SameProcess)
                        If AllFileStr(3) <> TheEncLib.ByteIn_StringOut _
                            (SHA256_Worker.ComputeHash(System.Text.Encoding.Unicode.GetBytes(TmpWork1 +
                          TheEncLib.ByteIn_StringOut(DERIVED_IDT_Protected)))) Then
                            ErrFlag = True
                        End If
                        Security.Cryptography.ProtectedMemory.Protect(DERIVED_IDT_Protected, MemoryProtectionScope.SameProcess)

                    Else
                        ErrFlag = True
                    End If

                    '==================== Stage 1.5 Title Data

                    If ErrFlag = False Then
                        NowTitleStringCpt = AllFileStr(0)
                    End If

                    '==================== Stage 2 Account Data

                    Dim AccountDatas() As String = AllFileStr(1).Split(",")

                    If AccountDatas.Length = 2 Then

                        TheEncLib.StringIn_ByteOut(AccountDatas(1), AES_IV_USE)

                        Dim WorkStr1 As String = System.Text.Encoding.UTF8.
                             GetString(TheEncLib.AES_Decrypt_Str_Return_Bytes(AccountDatas(0), AES_KEY_Protected, AES_IV_USE))

                        TheEncLib.Dispose()

                        Dim WorkStr2() As String = Replace(WorkStr1, vbCrLf, vbCr).Split(vbCr)

                        If WorkStr2.Length <> 8 Then
                            TextBoxTitle.Text = LangStrs(LIdx, UsingTxt.OT_Df)
                            ErrFlag = True
                        ElseIf WorkStr2(0) <> INDTstr Then
                            TextBoxTitle.Text = LangStrs(LIdx, UsingTxt.OT_Df)
                            ErrFlag = True
                        Else
                            NowDataStringCpt = AllFileStr(1)
                            TextBoxURL.Text = WorkStr2(1)
                            TextBoxNameAddr.Text = WorkStr2(2)
                            TextBoxNote1.Text = WorkStr2(4) 'Replace(WorkStr2(4), vbLf, vbCrLf)
                            TextBoxRegMailPhone.Text = WorkStr2(5)
                            TextBoxNote2Hid.Text = WorkStr2(6)
                            TextBox_BHKMHelper.Text = WorkStr2(7)
                            If TextBox_BHKMHelper.Text = "1" Then
                                ButtonHotkeyMode.Image = b_HKO_on
                            Else
                                TextBox_BHKMHelper.Text = "0"
                            End If
                        End If

                    Else
                        ErrFlag = True
                    End If

                    '==================== Stage 3 Account Password

                    If ErrFlag = False Then
                        NowPassStatue = 1
                        Read_File_Pass_Str = AllFileStr(2)
                    End If

                Catch ex As Exception

                    Hresult = ex.HResult
                    ErrFlag = True

                End Try

            End If

            ButtonFileInfo.Enabled = True
            ButtonFileInfo.Image = LBTN_FInfo_En

            If Not Sys_Chk._ReadOnlyMode Then
                ButtonGoUP.Enabled = True
                ButtonGoUP.Image = LBTN_MoveU_En
                ButtonGoDown.Enabled = True
                ButtonGoDown.Image = LBTN_MoveD_En
            End If

        End If

        If ErrFlag Then

            NowPassStatue = 0
            FilesList1(TheIndex).FileIsBad = True
            LastAct(LangStrs(LIdx, UsingTxt.OT_SEr))

            If Hresult = 0 Then
                MSGBOXNEW(LangStrs(LIdx, UsingTxt.Err_Fde), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
            Else
                MSGBOXNEW(GetSimpleErrorMessage(Hresult, WorkType.FileR), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, Me.PictureGray)
            End If

        Else

            If Not Sys_Chk._ReadOnlyMode Then
                ButtonTransCatalog.Enabled = True
                ButtonTransCatalog.Image = LBTN_TKey_En
            End If

            ButtonCopyReg.Enabled = True

            VG_Data_Done = False
            VG_Title_Done = False
            LastAct(Replace(LangStrs(LIdx, UsingTxt.RG_Oed), "$$$", TextBoxTitle.Text))
            FullGC()
        End If

        ReadingWorking = False

    End Sub

    Private Function WriteFile(Filename1 As String, ByRef PKey() As Byte, ByRef DICurrent() As Byte) As Integer

        FileSystemWatcher1.EnableRaisingEvents = False

        WriteFile = 0

        Dim AES_IV_Use(15) As Byte
        Dim TheEncLib As New Encode_Libs

        Try
            '=========================== segment 1 account title

            If (VG_Title_Done Or VG_Data_Done) Or (NowPassStatue = 3) Then

                Dim TMPstr0 As String

                If VG_Title_Done Then

                    TMPstr0 = TextBoxTitle.Text + vbCr + Random_Strs(30, 50, 1) + vbCr + INDTstr

                    TMPstr0 = TheEncLib.AES_Encrypt_String_Return_String(TMPstr0, PKey, AES_IV_Use) +
                        "," + TheEncLib.ByteIn_StringOut(AES_IV_Use)

                Else
                    TMPstr0 = NowTitleStringCpt
                End If

                '=========================== segment 2 account data

                Dim TMPstr1 As String

                If VG_Data_Done Then

                    'Dim TmpNote1 As String = Replace(TextBoxNote1.Text, vbCrLf, vbLf)
                    'If TmpNote1 Is Nothing Then TmpNote1 = ""

                    TMPstr1 = INDTstr + vbCr + TextBoxURL.Text + vbCr + TextBoxNameAddr.Text +
                    vbCr + Random_Strs(30, 50, 1) + vbCr + TextBoxNote1.Text + vbCr + TextBoxRegMailPhone.Text +
                    vbCr + TextBoxNote2Hid.Text + vbCr + TextBox_BHKMHelper.Text

                    TMPstr1 = TheEncLib.AES_Encrypt_String_Return_String(TMPstr1, PKey, AES_IV_Use) +
                        "," + TheEncLib.ByteIn_StringOut(AES_IV_Use)

                Else
                    TMPstr1 = NowDataStringCpt
                End If

                '=========================== segment 3 account password

                Dim TMPstr2 As String

                If (NowPassStatue = 3) Or (NowPassStatue = 0) Then

                    Dim SD As New SmallDecoderPass
                    SD.InputByte = CurrentAccountPass
                    SD.ShowDialog()

                    Dim PwdLength As Long = SD.TextBoxPWDStr.Text.Length

                    Dim D01_IDX As Long = Get_RangeRnd_ByRNG(50, 100) + (PwdLength * Get_RangeRnd_ByRNG(2, 5))
                    If D01_IDX > 120 Then D01_IDX = Get_RangeRnd_ByRNG(50, 100)

                    Dim D02_IDX As Long = Get_RangeRnd_ByRNG(50, 100) - (PwdLength * Get_RangeRnd_ByRNG(2, 5))
                    If D02_IDX < 30 Then D02_IDX = Get_RangeRnd_ByRNG(50, 100)

                    TMPstr2 = TheEncLib.AES_Encrypt_String_Return_String(Random_Strs(D01_IDX, D01_IDX, 1) + vbCr +
                        SD.TextBoxPWDStr.Text + vbCr + Random_Strs(D02_IDX, D02_IDX, 1), PKey, AES_IV_Use) +
                        "," + TheEncLib.ByteIn_StringOut(AES_IV_Use)

                    SD.Dispose()
                    FullGC()

                Else

                    TMPstr2 = Read_File_Pass_Str

                End If

                Dim SHA256_Worker As New Security.Cryptography.SHA256CryptoServiceProvider
                Dim TmpStr4 As String = TMPstr0 + vbCrLf + TMPstr1 + vbCrLf + TMPstr2


                Security.Cryptography.ProtectedMemory.Unprotect(DICurrent, MemoryProtectionScope.SameProcess)
                TmpStr4 += vbCrLf + TheEncLib.ByteIn_StringOut(
                SHA256_Worker.ComputeHash(System.Text.Encoding.Unicode.GetBytes(TmpStr4 +
                TheEncLib.ByteIn_StringOut(DICurrent))))
                Security.Cryptography.ProtectedMemory.Protect(DICurrent, MemoryProtectionScope.SameProcess)

                If System.IO.File.Exists(Filename1) Then
                    Dim FileFill(New System.IO.FileInfo(Filename1).Length) As Byte
                    System.IO.File.WriteAllBytes(Filename1, FileFill)
                End If

                System.IO.File.WriteAllText(Filename1, TmpStr4)

            End If

        Catch ex As Exception

            WriteFile = ex.HResult

        End Try

        TheEncLib.Dispose()

        FileSystemWatcher1.EnableRaisingEvents = True

    End Function

    Private Sub GetList()

        ListBoxAccounts.Items.Clear()
        ListBoxAccounts.Items.Add(LangStrs(LIdx, UsingTxt.IT_nc))

        ReDim FilesList1(0)
        Dim IDX01 As Integer = 1
        Dim IOreader1 As IO.StreamReader

        For Each foundFile As String In System.IO.Directory.GetFiles(DirName, "*.ACC", IO.SearchOption.TopDirectoryOnly)

            Dim ErrFlag As Boolean = False

            Try

                ReDim Preserve FilesList1(IDX01)
                FilesList1(IDX01).FileName = foundFile
                If New System.IO.FileInfo(FilesList1(IDX01).FileName).Length > 1048576 Then ErrFlag = True

                If ErrFlag = False Then

                    IOreader1 = New System.IO.StreamReader(foundFile)

                    Dim TitleString As String = IOreader1.ReadLine
                    Dim TitleDatas() As String = TitleString.Split(",")

                    If TitleDatas.Length = 2 Then

                        Dim WorkByte() As Byte
                        Dim AES_IV_USE(0) As Byte
                        Dim TheEncLib As New Encode_Libs
                        Dim TmpWorkStr() As String

                        TheEncLib.StringIn_ByteOut(TitleDatas(1), AES_IV_USE)
                        WorkByte = TheEncLib.AES_Decrypt_Str_Return_Bytes(TitleDatas(0), AES_KEY_Protected, AES_IV_USE)
                        TmpWorkStr = System.Text.Encoding.UTF8.GetString(WorkByte).Split(vbCr)
                        FilesList1(IDX01).ShowName = TmpWorkStr(0)

                        If TmpWorkStr.Length = 3 Then
                            If TmpWorkStr(2) = INDTstr Then
                                VG_Title_Done = False
                            Else
                                ErrFlag = True
                            End If
                        Else
                            ErrFlag = True
                        End If

                        TheEncLib.Dispose()

                    Else
                        ErrFlag = True
                    End If

                    IOreader1.Close()
                End If

            Catch ex As Exception
                ErrFlag = True
            End Try

            If ErrFlag Then
                FilesList1(IDX01).ShowName = LangStrs(LIdx, UsingTxt.OT_Df)
                FilesList1(IDX01).FileIsBad = True
            End If

            ListBoxAccounts.Items.Add(FilesList1(IDX01).ShowName)
            IDX01 += 1

        Next

        LB_Range_Scale = CDbl(ListBoxAccounts.Items.Count) - LB_Ration

    End Sub

    Private Function Delete_ACC_File(What_file As String, Ask_String As String) As Integer

        '0 = OK 1 = Cancel >1 = Error
        FileSystemWatcher1.EnableRaisingEvents = False

        Dim OLDidx As Integer = ListBoxAccounts.SelectedIndex
        Delete_ACC_File = 0

        If MSGBOXNEW(Ask_String.Replace("$$$", TextBoxTitle.Text), MsgBoxStyle.OkCancel, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray) = MsgBoxResult.Ok Then
            Try
                Dim OverWriteByte(New System.IO.FileInfo(What_file).Length - 1) As Byte
                System.IO.File.WriteAllBytes(What_file, OverWriteByte)
                System.IO.File.Delete(What_file)

                If OLDidx >= 1 Then Go_ListBoxIdx(OLDidx - 1)

            Catch ex As Exception
                Delete_ACC_File = ex.HResult
            End Try
        Else
            Delete_ACC_File = 1
        End If

        FileSystemWatcher1.EnableRaisingEvents = True

    End Function

    '===================== Catalog Manager (And CSV) ====================================

    Private Sub TransFullCat()

        Dim hresult As Integer = 0

        Dim DirNameCurrent As String = ""
        Dim AESKeyCurrent(0) As Byte
        Dim DERIVED_IDT_Current(0) As Byte
        Dim NowSelect As Integer = ListBoxAccounts.SelectedIndex

        Dim TmpDR As DialogResult

        If Sys_Chk.Use_Secure_Desktop Then
            TmpDR = LogInFormWork(LangStrs(LIdx, UsingTxt.Ca_PwdIn), DirNameCurrent, AESKeyCurrent, DERIVED_IDT_Current, 1, Nothing, Sys_Chk, PictureGray, TheSalt)
        Else
            TmpDR = LogInFormWork(LangStrs(LIdx, UsingTxt.Ca_PwdIn), DirNameCurrent, AESKeyCurrent, DERIVED_IDT_Current, 1, Nothing, Sys_Chk, PictureGray, TheSalt, Me)
        End If

        If TmpDR = DialogResult.OK Then

            If DirName = DirNameCurrent Then
                MSGBOXNEW(LangStrs(LIdx, UsingTxt.Ca_Tabt), MsgBoxStyle.Exclamation, LangStrs(LIdx, UsingTxt.Ti_Att), Me, PictureGray)
                FullGC()
                Exit Sub
            End If

            If IsCatLocked(DirNameCurrent) Then
                MSGBOXNEW(LangStrs(LIdx, UsingTxt.Ca_Tro), MsgBoxStyle.Exclamation, LangStrs(LIdx, UsingTxt.Ti_Att), Me, PictureGray)
                FullGC()
                Exit Sub
            End If

            Me.Enabled = False

            For IDX01 As Integer = 1 To ListBoxAccounts.Items.Count - 1

                Go_ListBoxIdx(IDX01)

                If FilesList1(IDX01).FileIsBad = False Then

                    Dim Filename2 As String = Get_New_ACC_Filename(DirNameCurrent)

                    GetPass()
                    NowPassStatue = 3
                    VG_Data_Done = True
                    VG_Title_Done = True

                    hresult = WriteFile(Filename2, AESKeyCurrent, DERIVED_IDT_Current)

                    If hresult > 0 Then Exit For

                End If

            Next

            Me.Enabled = True

            If hresult = 0 Then

                LastAct(LangStrs(LIdx, UsingTxt.Ca_TRd))

                If MSGBOXNEW(LangStrs(LIdx, UsingTxt.Ca_TRd) + D_vbcrlf + LangStrs(LIdx, UsingTxt.Ca_DuD).Replace("$$$", LabelCatalog.Text),
                     MsgBoxStyle.OkCancel, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray) <> MsgBoxResult.Cancel Then
                    Full_Catalog_Delete()
                End If

            Else

                LastAct(LangStrs(LIdx, UsingTxt.OT_SEr))
                MSGBOXNEW(GetSimpleErrorMessage(hresult, WorkType.FileW), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, Me.PictureGray)

            End If

        End If

        Exe_Fill_Trash()
        FullGC()

    End Sub

    Private Function Get_New_ACC_Filename(ByRef The_path As String) As String

        Return The_path + "\" + Decimal_to_x36(Now.Ticks - New DateTime(2001, 1, 1).Ticks, True).ToString + ".ACC"

    End Function

    Private Sub Full_Catalog_Delete()

        FileSystemWatcher1.EnableRaisingEvents = False

        Dim ErrFlag As Boolean = False
        Dim hresult As Integer = 0

        If MSGBOXNEW(LangStrs(LIdx, UsingTxt.Ca_IDel), 65535, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray) = MsgBoxResult.Ok Then

            Try
                LockFile.Close()
                IO.File.Delete(LockFilePath)
            Catch ex As Exception

            End Try

            Try

                Dim foundFiles As FileLists

                For IDX01 As Long = 1 To UBound(FilesList1)

                    foundFiles = FilesList1(IDX01)

                    Try

                        Dim FileLen As Long = New System.IO.FileInfo(foundFiles.FileName).Length
                        Dim OverWriteByte(FileLen - 1) As Byte
                        System.IO.File.WriteAllBytes(foundFiles.FileName, OverWriteByte)
                        System.IO.File.Delete(foundFiles.FileName)

                    Catch ex As Exception

                        ErrFlag = True
                        hresult = ex.HResult

                    End Try

                Next

                System.IO.Directory.Delete(DirName, True)

            Catch ex As Exception

                ErrFlag = True
                hresult = ex.HResult

            End Try

            If ErrFlag Then

                FileSystemWatcher1.EnableRaisingEvents = True
                LastAct(LangStrs(LIdx, UsingTxt.OT_SEr))
                MSGBOXNEW(GetSimpleErrorMessage(hresult, WorkType.FileD) + D_vbcrlf +
                          LangStrs(LIdx, UsingTxt.OT_Dsf), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
                Exit Sub

            End If

            FullGC()
            RestartApp2(Sys_Chk.Running_Admin, Me)

        End If

    End Sub

    Private Sub PictureBoxCATMAN_Click(sender As Object, e As EventArgs) Handles PictureBoxCATMAN.Click

        FormConfig.CB_Timer.SelectedIndex = Val(CAT_setting_Str(0))
        FormConfig.TB_AC_KEY.SelectedIndex = Val(CAT_setting_Str(2))
        FormConfig.CB_SIM1.SelectedIndex = Val(CAT_setting_Str(3))
        FormConfig.TB_PW_KEY.SelectedIndex = Val(CAT_setting_Str(5))
        FormConfig.CB_SIM2.SelectedIndex = Val(CAT_setting_Str(6))

        FormConfig.FormL = Me.Left + (Me.Width - FormConfig.Width) / 2
        FormConfig.FormT = Me.Top + (Me.Height - FormConfig.Height) / 2
        FormConfig.Opacity = Me.Opacity
        FormConfig.PictureBoxConfig.Image = FormConfigBitmap
        FormConfig.ReadOnlyMode = Sys_Chk._ReadOnlyMode

        MakeWindowsMono(Me, PictureGray)
        Dim DR As DialogResult = FormConfig.ShowDialog(Me)
        PictureGray.Visible = False
        PictureGray.SendToBack()
        System.Windows.Forms.Application.DoEvents()

        Select Case DR

            Case DialogResult.OK

                ACTLimitSelectIDX = FormConfig.CB_Timer.SelectedIndex
                RegisterKeys(CAT_setting_Str)
                AutoCountDownClose(ACTLimitSelect(CInt(CAT_setting_Str(0))))
                Write_CatDatas()

            Case DialogResult.Ignore ' Use "Ignore" for catalog transfer

                If FormConfig.OtherWorkMode = 1 Then
                    If ListBoxAccounts.Items.Count > 1 Then
                        TransFullCat()
                    End If
                ElseIf FormConfig.OtherWorkMode = 2 Then
                    Full_Catalog_Delete()
                End If

            Case DialogResult.Abort ' ===============Use "Abort" for CSV import

                If ParseCSV_S1(FormConfig.ReturnCSV) = 0 Then
                    GetList()
                    LastAct(LangStrs(LIdx, UsingTxt.CS_CId))
                    MSGBOXNEW(LangStrs(LIdx, UsingTxt.CS_CId) + D_vbcrlf + LangStrs(LIdx, UsingTxt.CS_Pcp), MsgBoxStyle.OkOnly, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray)
                    Go_ListBoxIdx(ListBoxAccounts.Items.Count - 1)
                Else
                    LastAct(LangStrs(LIdx, UsingTxt.CS_CIf))
                    MSGBOXNEW(LangStrs(LIdx, UsingTxt.CS_CIf) + D_vbcrlf + LangStrs(LIdx, UsingTxt.CS_Mnc), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
                End If

                FullGC()
                Exe_Fill_Trash()
                FullGC()

            Case DialogResult.Retry ' ===============Use "Retry" for CSV export

                Dim DirNameCurrent As String = ""
                Dim AESKeyCurrent(0) As Byte
                Dim DERIVED_IDT_Current(0) As Byte
                Dim TmpDR As DialogResult

                If Sys_Chk.Use_Secure_Desktop Then
                    TmpDR = LogInFormWork(LangStrs(LIdx, UsingTxt.Ca_PwdIn), DirNameCurrent, AESKeyCurrent, DERIVED_IDT_Current, 3, Nothing, Sys_Chk, PictureGray, TheSalt)
                Else
                    TmpDR = LogInFormWork(LangStrs(LIdx, UsingTxt.Ca_PwdIn), DirNameCurrent, AESKeyCurrent, DERIVED_IDT_Current, 3, Nothing, Sys_Chk, PictureGray, TheSalt, Me)
                End If

                If TmpDR = DialogResult.OK Then

                    If DirName <> DirNameCurrent Then
                        MSGBOXNEW(LangStrs(LIdx, UsingTxt.CS_Pnm), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
                        Exit Sub
                    End If

                    Try
                        Dim CSV_Head As String = "TITLE,URL,USERNAME,PASSWORD,REGDATA,NOTES,NOTES2,SETS"
                        Dim CSV_Filename As String = CSV_GetOkFilename(LabelCatalog.Text + " - " + Format(Now, "yyyyMMddHHmmss") + ".CSV")
                        Dim TheWriter As New System.IO.StreamWriter(CSV_Filename, False)
                        TheWriter.WriteLine(CSV_Head)
                        Dim Account_Count As String = " /" + Str(ListBoxAccounts.Items.Count - 1)

                        Dim CSVW As New FormCSVWorking
                        CSVW.ProgLab.Text = "0" + Account_Count
                        CSVW.FormW = Me.Width
                        CSVW.FormH = Me.Height
                        CSVW.FormT = Me.Top
                        CSVW.FormL = Me.Left
                        CSVW.Opacity = Me.Opacity
                        CSVW.PictureMode.Image = My.Resources.Resource1.CSV_EXP
                        CSVW.Show()

                        MakeWindowsMono(Me, PictureGray)

                        For idx01 As Integer = 1 To ListBoxAccounts.Items.Count - 1
                            CSVW.ProgLab.Text = Str(idx01) + Account_Count
                            System.Windows.Forms.Application.DoEvents()

                            If Not FilesList1(idx01).FileIsBad Then
                                ListBoxAccounts.SelectedIndex = idx01
                                GetPass()
                                TheWriter.WriteLine(Quo + TextBoxTitle.Text + Quo + "," + Quo +
                                TextBoxURL.Text + Quo + "," + Quo +
                                TextBoxNameAddr.Text + Quo + "," + Quo +
                                System.Text.Encoding.UTF8.GetString(Security.Cryptography.ProtectedData.Unprotect(
                                CurrentAccountPass, Nothing, DataProtectionScope.CurrentUser)) + Quo + "," + Quo +
                                TextBoxRegMailPhone.Text + Quo + "," + Quo +
                                TextBoxNote1.Text + Quo + "," + Quo +
                                TextBoxNote2Hid.Text + Quo + "," + Quo +
                                TextBox_BHKMHelper.Text + Quo)
                            End If

                        Next

                        UnMakeWindowsMono(PictureGray)

                        TheWriter.Close()
                        TheWriter.Dispose()
                        CSVW.Close()
                        CSVW.Dispose()

                        MSGBOXNEW(Replace(LangStrs(LIdx, UsingTxt.CS_Svd), "$$$", CSV_Filename) + D_vbcrlf + LangStrs(LIdx, UsingTxt.CS_Pcp), MsgBoxStyle.OkOnly, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray)

                        If Is_Legal_MS_File(Windows_Exp_Path) Then

                            Dim FullPath As String = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName
                            FullPath = System.IO.Path.GetDirectoryName(FullPath) + "\" + CSV_Filename
                            FullPath = Replace(FullPath, ":\\", ":\")

                            'If Not Launch_Unelevated_Core(Windows_Exp_Path, False, "/select," + FullPath) Then
                            '    MSGBOXNEW(LangStrs(LIdx, UsingTxt.OT_UL), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
                            'Else
                            '    LastAct(LangStrs(LIdx, UsingTxt.OT_Ld))
                            'End If

                            Process.Start(Windows_Exp_Path, "/select," + FullPath)
                        Else
                            Sys_Chk.Found_Bad_MSFile = True
                        End If

                    Catch ex As Exception

                        MSGBOXNEW(GetSimpleErrorMessage(ex.HResult, WorkType.FileW), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, Me.PictureGray)

                    End Try

                End If

        End Select

    End Sub

    Private Function CSV_GetOkFilename(TheFilename As String) As String

        Dim invalidChars As New String(IO.Path.GetInvalidFileNameChars())
        For Each c As Char In invalidChars
            TheFilename = TheFilename.Replace(c, "_"c)
        Next

        Return TheFilename

    End Function


    '============================== CSV import
    Private Function ParseCSV_S1(ByRef InputStr As String) As Integer

        Dim CSVW As New FormCSVWorking
        Dim ErrFlag As Integer = 0
        Dim hresult As Integer

        Try
            Dim TITLE_IND() As String = {"TITLE", "NAME", "ACCOUNT"}
            Dim USER_IND() As String = {"USERNAME", "LOGIN NAME", "LOGIN_USERNAME"}
            Dim URL_IND() As String = {"URL", "LOGIN_URI", "WEB SITE"}
            Dim PWD_IND() As String = {"PASSWORD", "LOGIN_PASSWORD"}
            Dim NOTE_IND() As String = {"NOTES", "COMMENTS"}
            Dim NOTE2_IND() As String = {"NOTES2"}
            Dim REGDATA_IND() As String = {"REGDATA"}
            Dim SETDATA_IND() As String = {"SETS"}

            Dim CSV_head(0) As String
            Dim CSV_head_IND(0) As Integer '0=nothing 1=Title 2=Username 3=URL 4=Password 5=Note
            Dim CSV_head_Count As Integer
            Dim CSV_head_Chk(8) As Integer

            Dim TmpStr1 As String = Replace(InputStr, vbCrLf, vbCr)
            TmpStr1 = Replace(TmpStr1, vbLf, vbCr)
            Dim TmpStr2() As String = TmpStr1.Split(vbCr)

            If TmpStr2(0) IsNot Nothing Then
                ParseCSV_S2(TmpStr2(0), CSV_head)
                CSV_head_Count = CSV_head.Length
                ReDim CSV_head_IND(CSV_head_Count - 1)
            Else
                Throw New Exception("")
            End If

            If CSV_head.Length <= 2 Then Throw New Exception("")

            '============================== CSV_head_IND ================ Start 
            For IDX01 As Integer = 0 To CSV_head.Length - 1
                If CSV_head(IDX01) IsNot Nothing Then
                    Dim TmpUpCase As String = CSV_head(IDX01).ToUpper
                    If CSV_head_Chk(1) = 0 Then
                        For Each TmpStrIND1 As String In TITLE_IND
                            If TmpUpCase = TmpStrIND1 Then
                                CSV_head_IND(IDX01) = 1
                                CSV_head_Chk(1) = 1
                                Exit For
                            End If
                        Next
                    End If
                    If CSV_head_Chk(2) = 0 Then
                        For Each TmpStrIND1 As String In USER_IND
                            If TmpUpCase = TmpStrIND1 Then
                                CSV_head_IND(IDX01) = 2
                                CSV_head_Chk(2) = 1
                                Exit For
                            End If
                        Next
                    End If
                    If CSV_head_Chk(3) = 0 Then
                        For Each TmpStrIND1 As String In URL_IND
                            If TmpUpCase = TmpStrIND1 Then
                                CSV_head_IND(IDX01) = 3
                                CSV_head_Chk(3) = 1
                                Exit For
                            End If
                        Next
                    End If
                    If CSV_head_Chk(4) = 0 Then
                        For Each TmpStrIND1 As String In PWD_IND
                            If TmpUpCase = TmpStrIND1 Then
                                CSV_head_IND(IDX01) = 4
                                CSV_head_Chk(4) = 1
                                Exit For
                            End If
                        Next
                    End If
                    If CSV_head_Chk(5) = 0 Then
                        For Each TmpStrIND1 As String In NOTE_IND
                            If TmpUpCase = TmpStrIND1 Then
                                CSV_head_IND(IDX01) = 5
                                CSV_head_Chk(5) = 1
                                Exit For
                            End If
                        Next
                    End If
                    If CSV_head_Chk(6) = 0 Then
                        For Each TmpStrIND1 As String In REGDATA_IND
                            If TmpUpCase = TmpStrIND1 Then
                                CSV_head_IND(IDX01) = 6
                                CSV_head_Chk(6) = 1
                                Exit For
                            End If
                        Next
                    End If
                    If CSV_head_Chk(7) = 0 Then
                        For Each TmpStrIND1 As String In NOTE2_IND
                            If TmpUpCase = TmpStrIND1 Then
                                CSV_head_IND(IDX01) = 7
                                CSV_head_Chk(7) = 1
                                Exit For
                            End If
                        Next
                    End If
                    If CSV_head_Chk(8) = 0 Then
                        For Each TmpStrIND1 As String In SETDATA_IND
                            If TmpUpCase = TmpStrIND1 Then
                                CSV_head_IND(IDX01) = 8
                                CSV_head_Chk(8) = 1
                                Exit For
                            End If
                        Next
                    End If
                End If
            Next

            If (CSV_head_Chk(1) + CSV_head_Chk(2) + CSV_head_Chk(4)) < 3 Then
                'Title + Username + Password they are must
                Throw New Exception("")
            End If

            '============================== CSV_head_IND ================ End 

            Dim CSV_Total_Str As String = ""
            If TmpStr2.Length >= 1 Then
                For IDX01 As Integer = 1 To TmpStr2.Length - 1
                    If TmpStr2(IDX01) <> "" Then
                        CSV_Total_Str += TmpStr2(IDX01) + ","
                    End If
                Next
            End If

            If CSV_Total_Str.EndsWith(",") Then
                CSV_Total_Str = CSV_Total_Str.Substring(0, CSV_Total_Str.Length - 1)
            End If

            Dim AllCSV(0) As String
            ParseCSV_S2(CSV_Total_Str, AllCSV)
            Dim Acc_Counts As String = " /" + Str(AllCSV.Length / CSV_head.Length)

            If (AllCSV.Length Mod CSV_head.Length <> 0) Or (AllCSV.Length = 0) Then
                'AllCSV must align CSV_head 
                Throw New Exception("")
            End If

            CSVW.ProgLab.Text = "0" + Acc_Counts
            CSVW.FormW = Me.Width
            CSVW.FormH = Me.Height
            CSVW.FormT = Me.Top
            CSVW.FormL = Me.Left
            CSVW.Opacity = Me.Opacity
            CSVW.Show(Me)


            FileSystemWatcher1.EnableRaisingEvents = False
            MakeWindowsMono(Me, PictureGray)
            System.Windows.Forms.Application.DoEvents()

            For IDX01 As Integer = 0 To AllCSV.Length - 1

                If IDX01 Mod CSV_head.Length = 0 Then ' It's new one
                    Read_and_decrypt(-1)
                End If

                Dim CSV_Idx As Integer = IDX01 Mod CSV_head_Count
                If AllCSV(IDX01) Is Nothing Then AllCSV(IDX01) = ""

                Select Case CSV_head_IND(CSV_Idx)
                    Case 1 'Title
                        TextBoxTitle.Text = AllCSV(IDX01)
                    Case 2 'Username
                        TextBoxNameAddr.Text = AllCSV(IDX01)
                    Case 3 'URL
                        TextBoxURL.Text = AllCSV(IDX01)
                    Case 4 'Password
                        CurrentAccountPass = Security.Cryptography.ProtectedData.Protect(
                        System.Text.Encoding.UTF8.GetBytes(AllCSV(IDX01)), Nothing, DataProtectionScope.CurrentUser)
                        NowPassStatue = 3
                    Case 5 'Note
                        TextBoxNote1.Text = AllCSV(IDX01)
                    Case 6 'RegData
                        TextBoxRegMailPhone.Text = AllCSV(IDX01)
                    Case 7 'Note2
                        TextBoxNote2Hid.Text = AllCSV(IDX01)
                    Case 8 'Sets
                        TextBox_BHKMHelper.Text = AllCSV(IDX01)
                End Select

                If IDX01 Mod CSV_head.Length = CSV_head.Length - 1 Then

                    If TextBoxTitle.Text = "" Then
                        TextBoxTitle.Text = "(?)"
                    End If

                    ' need to save
                    VG_Title_Done = True
                    VG_Data_Done = True
                    NowPassStatue = 3

                    hresult = WriteFile(Get_New_ACC_Filename(DirName), AES_KEY_Protected, DERIVED_IDT_Protected)

                    If hresult = 0 Then
                        LastAct(Replace(LangStrs(LIdx, UsingTxt.RG_Add), "$$$", TextBoxTitle.Text))
                        CSVW.ProgLab.Text = Str((IDX01 + 1) / CSV_head.Length) + Acc_Counts
                        System.Windows.Forms.Application.DoEvents()
                    Else
                        LastAct(LangStrs(LIdx, UsingTxt.OT_SEr))
                        ErrFlag = 1
                        MSGBOXNEW(GetSimpleErrorMessage(hresult, WorkType.FileW), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, Me.PictureGray)
                        Exit For
                    End If
                End If

            Next

        Catch ex As Exception

            If CSVW.Visible Then
                CSVW.Visible = False
                CSVW.Close()
                CSVW.Dispose()
                UnMakeWindowsMono(PictureGray)
            End If
            ErrFlag = 1

        End Try

        Thread.Sleep(50)
        CSVW.Visible = False
        CSVW.Close()
        CSVW.Dispose()
        UnMakeWindowsMono(PictureGray)
        FileSystemWatcher1.EnableRaisingEvents = True

        Return ErrFlag

    End Function

    Private Sub ParseCSV_S2(ByRef InputStr As String, ByRef ReturnStrArray() As String)

        Dim Quo_On As Boolean = False
        Dim RSA_IDX As Integer = 0
        Dim TmpEachItem() As String
        Dim TmpSTB As New System.Text.StringBuilder

        ReDim ReturnStrArray(RSA_IDX)

        For Each TmpChar As Char In InputStr

            If TmpChar = """" Then
                Quo_On = Not Quo_On
            End If

            If TmpChar = "," Then
                If Not Quo_On Then
                    TmpSTB.Append(TmpChar)
                Else
                    TmpSTB.Append(Chr(13))
                End If
            Else
                TmpSTB.Append(TmpChar)
            End If
        Next

        TmpEachItem = TmpSTB.ToString().Split(",")

        For Each TmpStr As String In TmpEachItem

            ReDim Preserve ReturnStrArray(RSA_IDX)

            TmpStr = Replace(TmpStr, Chr(13), ",")

            If TmpStr IsNot Nothing Then
                If TmpStr.StartsWith("""") Then TmpStr = TmpStr.Substring(1, TmpStr.Length - 1)
                If TmpStr.EndsWith("""") Then TmpStr = TmpStr.Substring(0, TmpStr.Length - 1)
            Else
                TmpStr = ""
            End If

            TmpStr = Replace(TmpStr, """""", """")

            ReturnStrArray(RSA_IDX) = TmpStr
            RSA_IDX += 1

        Next

    End Sub

    Private Sub Write_CatDatas()

        FileSystemWatcher1.EnableRaisingEvents = False

        Try
            Dim DPNOTE As String = DirName + "\" + Notefile
            Dim IOWer1 As New System.IO.StreamWriter(DPNOTE, False)

            Dim TmpWork As String = FormConfig.TextBoxCatalog.Text + vbCr
            TmpWork += ACTLimitSelectIDX.ToString + ",0," + CAT_setting_Str(2) + "," + CAT_setting_Str(3)
            TmpWork += ",0," + CAT_setting_Str(5) + "," + CAT_setting_Str(6)

            Dim TheEncLib As New Encode_Libs
            Dim AES_IV_Use(15) As Byte

            TmpWork = TheEncLib.AES_Encrypt_String_Return_String(TmpWork, AES_KEY_Protected, AES_IV_Use) +
                "," + TheEncLib.ByteIn_StringOut(AES_IV_Use)

            IOWer1.WriteLine(TmpWork)
            IOWer1.Close()
            LabelCatalog.Text = FormConfig.TextBoxCatalog.Text
            LastAct(LangStrs(LIdx, UsingTxt.Ca_Upd))
        Catch ex As Exception
            LastAct(LangStrs(LIdx, UsingTxt.Err_Unk))
            MSGBOXNEW(GetSimpleErrorMessage(ex.HResult, WorkType.FileW), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, Me.PictureGray)
        End Try

        FileSystemWatcher1.EnableRaisingEvents = True

    End Sub

    Private Sub Read_CatDatas()

        GetList()

        ListBoxAccounts.SelectedIndex = 0

        Dim CAT_note_File As String

        FormConfig.TB_AC_KEY.SelectedIndex = 1
        FormConfig.TB_PW_KEY.SelectedIndex = 2
        FormConfig.CB_SIM1.SelectedIndex = 0
        FormConfig.CB_SIM2.SelectedIndex = 0
        FormConfig.CB_Timer.SelectedIndex = 0
        LabelCatalog.Text = LangStrs(LIdx, UsingTxt.IT_un)
        FormConfig.TextBoxCatalog.Text = LabelCatalog.Text

        Dim IOreader1 As System.IO.StreamReader = Nothing

        Try

            CAT_note_File = DirName + "\" + Notefile

            If System.IO.File.Exists(CAT_note_File) Then

                If New System.IO.FileInfo(CAT_note_File).Length < 102400 Then
                    IOreader1 = New System.IO.StreamReader(CAT_note_File)
                    Dim WorkStr() As String = IOreader1.ReadLine.Split(",")
                    Dim TmpWorkStr As String, TmpWorkStr2() As String

                    If WorkStr.Length = 2 Then

                        Dim AES_IV_USE(15) As Byte
                        Dim TheEncLib As New Encode_Libs

                        TheEncLib.StringIn_ByteOut(WorkStr(1), AES_IV_USE)

                        TmpWorkStr = System.Text.Encoding.UTF8.
                            GetString(TheEncLib.AES_Decrypt_Str_Return_Bytes(WorkStr(0), AES_KEY_Protected, AES_IV_USE))

                        TmpWorkStr2 = TmpWorkStr.Split(vbCr)

                        LabelCatalog.Text = TmpWorkStr2(0)
                        FormConfig.TextBoxCatalog.Text = LabelCatalog.Text

                        CAT_setting_Str = TmpWorkStr2(1).Split(",")

                        If CAT_setting_Str.Length >= 7 Then
                            RegisterKeys(CAT_setting_Str)
                            AutoCountDownClose(ACTLimitSelect(CInt(CAT_setting_Str(0))))
                        End If

                    End If

                End If

            Else
                FormConfig.TextBoxCatalog.Text = LabelCatalog.Text
            End If

        Catch ex As Exception
            MSGBOXNEW(LangStrs(LIdx, UsingTxt.Er2_RCs) + D_vbcrlf + GetSimpleErrorMessage(ex.HResult, WorkType.FileR), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, Me.PictureGray)
        End Try

        If IOreader1 IsNot Nothing Then
            IOreader1.Close()
            IOreader1.Dispose()
        End If

    End Sub

    '============================== Auto CountDown Close ===========

    Public DIGI_NUM() As Bitmap = {My.Resources.Resource1.DIGI_Y_0, My.Resources.Resource1.DIGI_Y_1,
    My.Resources.Resource1.DIGI_Y_2, My.Resources.Resource1.DIGI_Y_3, My.Resources.Resource1.DIGI_Y_4,
    My.Resources.Resource1.DIGI_Y_5, My.Resources.Resource1.DIGI_Y_6, My.Resources.Resource1.DIGI_Y_7,
    My.Resources.Resource1.DIGI_Y_8, My.Resources.Resource1.DIGI_Y_9}

    Public DIGI_NUM_N As New Bitmap(My.Resources.Resource1.DIGI_Y__)
    Public WithEvents AutoCloseTimer As New Windows.Forms.Timer

    Private Sub AutoCountDownClose(CountdownSec As Integer) 'Set Auto CountDown Close Timer

        ACTLimit = CountdownSec
        ACTCount = 0

        If CountdownSec = 0 Then
            PicTimerACT.Visible = False
            PicTimerINACT.Visible = True
        Else
            PicTimerACT.Visible = True
            PicTimerINACT.Visible = False
        End If

    End Sub

    Private Sub AutoCloseTimer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles AutoCloseTimer.Tick

        Dim TimeCountDown() As Char

        If ACTLimit > 0 Then

            Sys_Chk.Use_AutoClose = True
            PicTimerACT.Visible = True
            PicTimerINACT.Visible = False
            ACTCount += 1
            TimeCountDown = TimeSpan.FromSeconds(ACTLimit - ACTCount + 1).ToString("mm\:ss").ToCharArray
            PicDIGI_1.Image = DIGI_NUM(Convert.ToInt32(TimeCountDown(0)) - 48)
            PicDIGI_2.Image = DIGI_NUM(Convert.ToInt32(TimeCountDown(1)) - 48)
            PicDIGI_3.Image = DIGI_NUM(Convert.ToInt32(TimeCountDown(3)) - 48)
            PicDIGI_4.Image = DIGI_NUM(Convert.ToInt32(TimeCountDown(4)) - 48)

            If ACTCount > ACTLimit Then
                RestartApp2(Sys_Chk.Running_Admin, Me)
            End If
        Else
            Sys_Chk.Use_AutoClose = False
            PicTimerACT.Visible = False
            PicTimerINACT.Visible = True
            PicDIGI_1.Image = DIGI_NUM_N
            PicDIGI_2.Image = DIGI_NUM_N
            PicDIGI_3.Image = DIGI_NUM_N
            PicDIGI_4.Image = DIGI_NUM_N
        End If

    End Sub

    '================== Textbox Works =================================

    Private Sub TextBoxNameAddr_TextChanged(sender As Object, e As EventArgs) Handles TextBoxNameAddr.TextChanged

        If Not ReadingWorking Then
            VG_CCC_Done = True
            If InStr(TextBoxNameAddr.Text, "@") > 0 And InStr(TextBoxNameAddr.Text, ".") > 0 Then
                TextBoxRegMailPhone.Text = TextBoxNameAddr.Text
            End If
        End If

    End Sub

    Private Sub TextBoxChanged(sender As Object, e As EventArgs) Handles TextBoxURL.TextChanged,
            TextBoxNameAddr.TextChanged, TextBoxRegMailPhone.TextChanged, TextBoxNote1.TextChanged,
            TextBoxNote2Hid.TextChanged, TextBox_BHKMHelper.TextChanged

        If Not ReadingWorking Then
            VG_Data_Done = True
            If TextBoxTitle.Text <> "" Then
                If Not Sys_Chk._ReadOnlyMode Then
                    ButtonSave.Enabled = True
                    ButtonSave.Image = LBTN_Save_En
                End If
            End If
        End If

    End Sub

    Private Sub TextBoxTitle_TextChanged(sender As Object, e As EventArgs) Handles TextBoxTitle.TextChanged

        If Not ReadingWorking Then
            If TextBoxTitle.Text <> "" Then
                VG_Title_Done = True
                ButtonSave.Enabled = True
                ButtonSave.Image = LBTN_Save_En
            Else
                If Not Sys_Chk._ReadOnlyMode Then
                    ButtonSave.Enabled = True
                    ButtonSave.Image = LBTN_Save_En
                End If
            End If
        End If

    End Sub

    '===================== Hotkey and Clipboard defence works ============================

    Public Const WM_KEYDOWN As Integer = &H100
    Public Const WM_MOUSEMOVE As Integer = &H200
    Public Const WM_LBUTTONDOWN As Integer = &H201
    Public Const WM_RBUTTONDOWN As Integer = &H204
    Public Const WM_MBUTTONDOWN As Integer = &H207

    Public Const WM_DRAWCLIPBOARD As Integer = &H308
    Public Const WM_CHANGECBCHAIN As Integer = &H30D

    Public Const KEY_W As Integer = &H57
    Public Const MOD_ALT As Integer = &H1
    Public Const MOD_SHIFT As Integer = &H4
    Public Const MOD_CTRL As Integer = &H2
    Public Const MOD_WIN As Integer = &H8
    Public Const WM_HOTKEY As Integer = &H312
    'Public Const WM_LBUTTONDOWN As Integer = &H201

    <DllImport("user32.dll")>
    Private Shared Function ChangeClipboardChain(ByVal hWndRemove As IntPtr, ByVal hWndNewNext As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function SetClipboardViewer(ByVal hWndNewViewer As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function SendMessage(ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Integer
    End Function

    <System.Diagnostics.DebuggerStepThrough()>
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)

        MyBase.WndProc(m)

        If IsProtectMode Then Exit Sub

        Select Case m.Msg

            Case WM_HOTKEY 'For Hotkey

                If HotKeyIsWorking Then Exit Select

                Dim id As IntPtr = m.WParam

                Dim HotKey_Work1 As Integer = CAT_setting_Str(3)
                Select Case HotKey_Work1
                    Case 1, 2, 4
                        If TextBox_BHKMHelper.Text = "1" Then HotKey_Work1 = 3
                End Select

                Dim HotKey_Work2 As Integer = CAT_setting_Str(6)
                Select Case HotKey_Work2
                    Case 1, 2, 4
                        If TextBox_BHKMHelper.Text = "1" Then HotKey_Work2 = 3
                End Select

                Select Case id.ToString

                    Case HotKeyID1

                        Select Case HotKey_Work1
                            Case 1
                                CopyOnlyMode(TextBoxNameAddr)
                            Case 2
                                CopyPastClearMode(TextBoxNameAddr)
                            Case 3
                                SendKeyMode(TextBoxNameAddr)
                            Case 4
                                MixedMode(TextBoxNameAddr)
                        End Select

                    Case HotKeyID2

                        GetPass()

                        If NowPassStatue > 1 Then

                            Dim SD As New SmallDecoderPass
                            SD.InputByte = CurrentAccountPass
                            SD.ShowDialog()

                            Select Case HotKey_Work2
                                Case 1
                                    CopyOnlyMode(SD.TextBoxPWDStr)
                                Case 2
                                    CopyPastClearMode(SD.TextBoxPWDStr)
                                Case 3
                                    SendKeyMode(SD.TextBoxPWDStr)
                                Case 4
                                    MixedMode(SD.TextBoxPWDStr)
                            End Select

                            SD.Dispose()
                            FullGC()

                        End If

                End Select

            Case WM_DRAWCLIPBOARD 'For Clipboard Monitor Block(1)

                ' 阻止剪貼簿事件廣播
                'm.Result = IntPtr.Zero
                ' 將事件傳遞給下一個視窗
                'SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam)

            Case WM_CHANGECBCHAIN 'For Clipboard Monitor Block(2)
                ' 如果有下一個剪貼簿事件接收器，則將其更新為下一個

                If (m.WParam = nextClipboardViewer) Then
                    nextClipboardViewer = m.LParam
                Else
                    SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam)
                End If

        End Select
    End Sub

    Public nextClipboardViewer As IntPtr
    Public HotKeyIsWorking As Boolean

    Public Sub SendKeyMode(ByRef TheTextBox As TextBox)

        If TheTextBox.Text = "" Then Exit Sub
        HotKeyIsWorking = True

        Thread.Sleep(1000)
        System.Windows.Forms.SendKeys.Send(FixSendKey(TheTextBox.Text))

        HotKeyIsWorking = False

    End Sub

    Public Sub CopyPastClearMode(ByRef TheTextBox As TextBox)

        If TheTextBox.Text = "" Then Exit Sub

        HotKeyIsWorking = True
        Thread.Sleep(800)

        StartClipboardMonitorBlocker()

        Do
            Try
                Thread.Sleep(200)
                'My.Computer.Clipboard.SetText(TheTextBox.Text)
                Dim CPWorker As New ClipboardHelper2
                CPWorker.SetClipboardText(TheTextBox.Text)
                Exit Do
            Catch ex As Exception
            End Try
        Loop


        System.Windows.Forms.SendKeys.Send("^{v}")
        System.Windows.Forms.Clipboard.Clear()

        StopClipboardMonitorBlocker()
        HotKeyIsWorking = False

    End Sub

    Public Sub MixedMode(ByRef TheTextBox As TextBox)

        If TheTextBox.Text = "" Then Exit Sub
        HotKeyIsWorking = True

        Thread.Sleep(800)

        Dim TxtLength As Long = TheTextBox.Text.Length
        Dim IDX01 As Integer = 0

        Dim SB1 As New StringBuilder
        Dim SB2 As New StringBuilder

        For Each TmpChr As Char In TheTextBox.Text
            Select Case IDX01 Mod 2
                Case 0
                    SB1.Append(FixSendKey(TmpChr) + "{right}")
                Case 1
                    SB2.Append(TmpChr)
            End Select
            IDX01 += 1
        Next

        StartClipboardMonitorBlocker()

        Do
            Try
                Thread.Sleep(200)
                Dim CPWorker As New ClipboardHelper2
                CPWorker.SetClipboardText(SB2.ToString)
                Exit Do
            Catch ex As Exception
            End Try
        Loop

        System.Windows.Forms.SendKeys.Send("^{v}{home}")
        System.Windows.Forms.Clipboard.Clear()

        System.Windows.Forms.SendKeys.Send(SB1.ToString)

        StopClipboardMonitorBlocker()
        HotKeyIsWorking = False
        SB1.Clear()
        SB2.Clear()

        FullGC()

    End Sub

    Public Sub CopyOnlyMode(ByRef TheTextBox As TextBox)

        If TheTextBox.Text.Length > 0 Then
            StartClipboardMonitorBlocker()

            Do
                Try
                    Thread.Sleep(200)
                    'My.Computer.Clipboard.SetText(TheTextBox.Text)
                    Dim CPWorker As New ClipboardHelper2
                    CPWorker.SetClipboardText(TheTextBox.Text)
                    Exit Do
                Catch ex As Exception
                End Try
            Loop

            StopClipboardMonitorBlocker()
        End If

    End Sub

    Public Function FixSendKey(ByRef InString As String) As String

        Dim TmpSB As New System.Text.StringBuilder

        For Each Chr01 As Char In InString
            Select Case Chr01
                Case " "
                    TmpSB.Append(" ")
                Case Else

                    If Control.IsKeyLocked(Keys.CapsLock) Then
                        If Char.IsLower(Chr01) Then
                            TmpSB.Append("{" + Char.ToUpper(Chr01) + "}")
                        ElseIf Char.IsUpper(Chr01) Then
                            TmpSB.Append("{" + Char.ToLower(Chr01) + "}")
                        Else
                            TmpSB.Append("{" + Chr01 + "}")
                        End If
                    Else
                        TmpSB.Append("{" + Chr01 + "}")
                    End If

            End Select
        Next

        FixSendKey = TmpSB.ToString()
        TmpSB.Clear()

    End Function

    Public Sub StopClipboardMonitorBlocker()
        ChangeClipboardChain(Me.Handle, nextClipboardViewer)
    End Sub

    Public Sub StartClipboardMonitorBlocker()
        nextClipboardViewer = SetClipboardViewer(Me.Handle)
    End Sub

    Private Sub RegisterKeys(RegSettings() As String)

        Dim kc As New KeysConverter()
        Dim o As Object
        Dim CheckBool As Boolean

        UnregisterHotKey(Me.Handle, HotKeyID1)
        UnregisterHotKey(Me.Handle, HotKeyID2)

        If Val(RegSettings(3)) > 0 Then
            o = kc.ConvertFromString(SCutChar(Val(RegSettings(2))))

            For idx01 As Integer = 1000 To 1999
                CheckBool = RegisterHotKey(Me.Handle, idx01, MOD_ALT + MOD_CTRL, CType(o, Keys))
                If CheckBool = True Then
                    HotKeyID1 = idx01
                    Exit For
                End If
            Next
            If CheckBool = False Then MSGBOXNEW(Replace(LangStrs(LIdx, UsingTxt.OT_HKC), "$$$", "1"), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
        End If

        If Val(RegSettings(6)) > 0 Then
            o = kc.ConvertFromString(SCutChar(Val(RegSettings(5))))

            For idx01 As Integer = 2000 To 2999
                CheckBool = RegisterHotKey(Me.Handle, idx01, MOD_ALT + MOD_CTRL, CType(o, Keys))
                If CheckBool = True Then
                    HotKeyID2 = idx01
                    Exit For
                End If
            Next
            If CheckBool = False Then MSGBOXNEW(Replace(LangStrs(LIdx, UsingTxt.OT_HKC), "$$$", "2"), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
        End If

    End Sub

    '============================ Normal Button Works===================================


    Private Sub ButtonHotkeyMode_Click(sender As Object, e As EventArgs) Handles ButtonHotkeyMode.Click

        If TextBox_BHKMHelper.Text = "0" Then
            ButtonHotkeyMode.Image = b_HKO_Son_on
            TextBox_BHKMHelper.Text = "1"
        Else
            ButtonHotkeyMode.Image = b_HKO_Soff_on
            TextBox_BHKMHelper.Text = "0"
        End If

    End Sub

    Private Sub ButtonSystemCheck_Click(sender As Object, e As EventArgs) Handles ButtonSecCheck.Click

        Dim ErrFlag As Boolean

        If Not Is_Legal_MS_File(Windows_Notepad_Path) Then
            Sys_Chk.Found_Bad_MSFile = True
            ErrFlag = True
        Else
            ErrFlag = Not SentToNotePad(Make_Security_Report())
        End If

        If ErrFlag Then
            MSGBOXNEW(LangStrs(LIdx, UsingTxt.Err_Unk), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, Me.PictureGray)
        End If


    End Sub

    Private Sub ButtonCopyAccount_Click(sender As Object, e As EventArgs) Handles ButtonCopyAccount.Click

        If TextBoxNameAddr.Text = "" Then Exit Sub

        Dim CPWorker As New ClipboardHelper2

        If CPWorker.SetClipboardText(TextBoxNameAddr.Text) Then
            LastAct(LangStrs(LIdx, UsingTxt.OT_Cd))
        Else
            LastAct(LangStrs(LIdx, UsingTxt.Err_CPf))
            MSGBOXNEW(LangStrs(LIdx, UsingTxt.Err_CPf) + D_vbcrlf + LangStrs(LIdx, UsingTxt.Err_CPfc), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
        End If

    End Sub

    Private Sub ButtonCopyReg_Click(sender As Object, e As EventArgs) Handles ButtonCopyReg.Click

        If TextBoxRegMailPhone.Text = "" Then Exit Sub

        Dim CPWorker As New ClipboardHelper2

        If CPWorker.SetClipboardText(TextBoxRegMailPhone.Text) Then
            LastAct(LangStrs(LIdx, UsingTxt.OT_Cd))
        Else
            LastAct(LangStrs(LIdx, UsingTxt.Err_CPf))
            MSGBOXNEW(LangStrs(LIdx, UsingTxt.Err_CPf) + D_vbcrlf + LangStrs(LIdx, UsingTxt.Err_CPfc), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
        End If


    End Sub

    Private Sub ButtonViewNote_Click(sender As Object, e As EventArgs) Handles ButtonViewNote.Click

        If Not IsShowingMessage Then
            ButtonViewNote.Image = b_view_Son_on
            TextBoxNote2Hid.PasswordChar = ControlChars.NullChar
        Else
            ButtonViewNote.Image = b_view_on
            TextBoxNote2Hid.PasswordChar = "●"
        End If

        IsShowingMessage = Not IsShowingMessage

    End Sub

    Private Sub ButtonLaunch_Click(sender As Object, e As EventArgs) Handles ButtonLaunch.Click

        If TextBoxURL.Text = "" Then Exit Sub

        If Not Launch_URI(TextBoxURL.Text, Sys_Chk.Running_Admin, Sys_Chk.Found_Bad_MSFile) Then
            MSGBOXNEW(LangStrs(LIdx, UsingTxt.OT_UL), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
        Else
            LastAct(LangStrs(LIdx, UsingTxt.OT_Ld))
        End If


    End Sub

    Private Sub ButtonPwd_Click(sender As Object, e As EventArgs) Handles ButtonPwd.Click

        GetPass()

        If Sys_Chk.Use_Secure_Desktop Then
            NowPassStatue = LogInFormWork(LangStrs(LIdx, UsingTxt.PK_Pi), Nothing, CurrentAccountPass, Nothing, 2, NowPassStatue, Sys_Chk, PictureGray, TheSalt)
        Else
            NowPassStatue = LogInFormWork(LangStrs(LIdx, UsingTxt.PK_Pi), Nothing, CurrentAccountPass, Nothing, 2, NowPassStatue, Sys_Chk, PictureGray, TheSalt, Me)
        End If

        FullGC()
        Exe_Fill_Trash(0.5)
        Exe_Fill_Trash(1)
        FullGC()

        If (NowPassStatue >= 3) And (TextBoxTitle.Text <> "") Then
            ButtonSave.Enabled = True
            ButtonSave.Image = LBTN_Save_En
        End If

    End Sub

    Private Sub PictureBoxPwdCPY_Click(sender As Object, e As EventArgs) Handles PictureBoxPwdCPY.Click

        Dim CopySucess As Boolean = False

        GetPass()

        If NowPassStatue > 1 Then
            StartClipboardMonitorBlocker()
            Dim SDobj As New SmallDecoderPass
            SDobj.Workmode = 1
            SDobj.InputByte = CurrentAccountPass
            SDobj.ShowDialog()
            CopySucess = SDobj.CopySucess
            SDobj.Dispose()
            FullGC()
            StopClipboardMonitorBlocker()
        End If

        If CopySucess Then
            LastAct(LangStrs(LIdx, UsingTxt.OT_Cd))
        Else
            LastAct(LangStrs(LIdx, UsingTxt.Err_CPf))
            MSGBOXNEW(LangStrs(LIdx, UsingTxt.Err_CPf) + D_vbcrlf + LangStrs(LIdx, UsingTxt.Err_CPfc), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
        End If

    End Sub

    Private Sub PictureBoxPwdVi_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBoxPwdVi.MouseDown
        GetPass()

        If NowPassStatue > 1 Then
            Dim FPWDS As New SmallDecoderPass
            PictureBoxPwdVi.Image = b_view_small_Son_on
            FPWDS.Workmode = 2
            FPWDS.InputByte = CurrentAccountPass
            FPWDS.Width = 1
            FPWDS.Height = 1
            FPWDS.ShowDialog()
            ButtonPwd.Image = FPWDS.PictureBoxPwd
            FPWDS.Dispose()
            FullGC()
        End If
    End Sub

    Private Sub PictureBoxPwdVi_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBoxPwdVi.MouseUp
        If Not Sys_Chk._ReadOnlyMode Then
            ButtonPwd.Image = My.Resources.Resource1.TOPSEC
        Else
            ButtonPwd.Image = PwdGrayImage
        End If
        PictureBoxPwdVi.Image = b_view_small_on
    End Sub

    Private Sub PictureBoxPwdVi_MouseLeave(sender As Object, e As EventArgs) Handles PictureBoxPwdVi.MouseLeave
        If Not Sys_Chk._ReadOnlyMode Then
            ButtonPwd.Image = My.Resources.Resource1.TOPSEC
        Else
            ButtonPwd.Image = PwdGrayImage
        End If
        PictureBoxPwdVi.Image = b_view_small_on
    End Sub


    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click

        If TextBoxTitle.Text = "" Then Exit Sub

        Dim hresult As Integer

        '========== Crypto Address Detect
        If (TextBoxNameAddr.Text.Length > 20) And VG_CCC_Done Then
            Dim CCC As New CryptoCurrencyWork
            CCC.DetectCurrency(TextBoxNameAddr.Text)
            If CCC.DetectState = 1 Then
                Dim WorkStr As String = (LangStrs(LIdx, UsingTxt.PK_Fad1) + LangStrs(LIdx, UsingTxt.PK_Fad2) + LangStrs(LIdx, UsingTxt.PK_C2s)).Replace("$$$", CoinList(CCC.DetectType))
                If MSGBOXNEW(WorkStr, MsgBoxStyle.OkCancel, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray) = MsgBoxResult.Cancel Then
                    Exit Sub
                End If
            End If
            CCC.Dispose()
        End If

        If NowProcFile = "" Then

            If MSGBOXNEW(LangStrs(LIdx, UsingTxt.RG_DuA).Replace("$$$", TextBoxTitle.Text), MsgBoxStyle.OkCancel,
                         LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray) = MsgBoxResult.Ok Then

                hresult = WriteFile(Get_New_ACC_Filename(DirName), AES_KEY_Protected, DERIVED_IDT_Protected)

                If hresult = 0 Then
                    GetList()
                    LastAct(Replace(LangStrs(LIdx, UsingTxt.RG_Add), "$$$", TextBoxTitle.Text))
                    Exe_Fill_Trash()
                    FullGC()
                    Go_ListBoxIdx(ListBoxAccounts.Items.Count - 1)
                Else
                    LastAct(LangStrs(LIdx, UsingTxt.OT_SEr))
                    MSGBOXNEW(GetSimpleErrorMessage(hresult, WorkType.FileW), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, Me.PictureGray)
                End If

            End If

        Else

            Dim OLDidx As Integer = ListBoxAccounts.SelectedIndex

            If MSGBOXNEW(LangStrs(LIdx, UsingTxt.RG_DuUpd).Replace("$$$", TextBoxTitle.Text), MsgBoxStyle.OkCancel, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray) = MsgBoxResult.Ok Then

                hresult = WriteFile(NowProcFile, AES_KEY_Protected, DERIVED_IDT_Protected)

                If hresult = 0 Then
                    If OLDidx >= 1 Then
                        GetList()
                        Go_ListBoxIdx(OLDidx)
                    End If
                    LastAct(Replace(LangStrs(LIdx, UsingTxt.RG_Updd), "$$$", TextBoxTitle.Text))
                    Exe_Fill_Trash()
                    FullGC()
                Else
                    LastAct(LangStrs(LIdx, UsingTxt.OT_SEr))
                    MSGBOXNEW(GetSimpleErrorMessage(hresult, WorkType.FileW), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, Me.PictureGray)
                End If

            End If

        End If

    End Sub

    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click

        Dim NowDeleteAccName As String = TextBoxTitle.Text
        Dim hresult As Integer = Delete_ACC_File(NowProcFile, LangStrs(LIdx, UsingTxt.RG_DuD))

        Select Case hresult
            Case 0
                GetList()
                LastAct(Replace(LangStrs(LIdx, UsingTxt.RG_Ded), "$$$", NowDeleteAccName))
            Case 1
            Case Else
                LastAct(LangStrs(LIdx, UsingTxt.OT_SEr))
                MSGBOXNEW(GetSimpleErrorMessage(hresult, WorkType.FileD), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, Me.PictureGray)
        End Select

    End Sub

    Private Sub ButtonTransCatalog_Click(sender As Object, e As EventArgs) Handles ButtonTransCatalog.Click

        MSGBOXNEW(LangStrs(LIdx, UsingTxt.Ca_AwT).Replace("$$$", TextBoxTitle.Text) + D_vbcrlf + LangStrs(LIdx, UsingTxt.Ca_NPrq),
                     MsgBoxStyle.Exclamation, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray)

        Dim NowTransAccName As String = TextBoxTitle.Text

        Dim hresult As Integer

        Dim DirNameCurrent As String = ""
        Dim AESKeyCurrent(0) As Byte
        Dim DERIVED_IDT_Current(0) As Byte
        Dim NowSelect As Integer = ListBoxAccounts.SelectedIndex

        Dim TmpDR As DialogResult

        If Sys_Chk.Use_Secure_Desktop Then
            TmpDR = LogInFormWork(LangStrs(LIdx, UsingTxt.Ca_PwdIn), DirNameCurrent, AESKeyCurrent, DERIVED_IDT_Current, 1, Nothing, Sys_Chk, PictureGray, TheSalt)
        Else
            TmpDR = LogInFormWork(LangStrs(LIdx, UsingTxt.Ca_PwdIn), DirNameCurrent, AESKeyCurrent, DERIVED_IDT_Current, 1, Nothing, Sys_Chk, PictureGray, TheSalt, Me)
        End If

        If TmpDR = DialogResult.OK Then

            Dim Filename2 As String = Get_New_ACC_Filename(DirNameCurrent)
            FullGC()

            If DirName = DirNameCurrent Then
                MSGBOXNEW(LangStrs(LIdx, UsingTxt.Ca_Tabt), MsgBoxStyle.Exclamation, LangStrs(LIdx, UsingTxt.Ti_Att), Me, PictureGray)
                Exit Sub
            End If

            If IsCatLocked(DirNameCurrent) Then
                MSGBOXNEW(LangStrs(LIdx, UsingTxt.Ca_Tro), MsgBoxStyle.Exclamation, LangStrs(LIdx, UsingTxt.Ti_Att), Me, PictureGray)
                Exit Sub
            End If

            GetPass()
            NowPassStatue = 3
            VG_Data_Done = True
            VG_Title_Done = True

            hresult = WriteFile(Filename2, AESKeyCurrent, DERIVED_IDT_Current)

            If hresult > 0 Then
                LastAct(LangStrs(LIdx, UsingTxt.OT_SEr))
                MSGBOXNEW(GetSimpleErrorMessage(hresult, WorkType.FileW), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, Me.PictureGray)
                Exit Sub
            End If

            hresult = Delete_ACC_File(NowProcFile, LangStrs(LIdx, UsingTxt.Ca_TD) + D_vbcrlf + LangStrs(LIdx, UsingTxt.RG_DuD))

            If hresult > 1 Then
                LastAct(LangStrs(LIdx, UsingTxt.OT_SEr))
                MSGBOXNEW(GetSimpleErrorMessage(hresult, WorkType.FileD), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, Me.PictureGray)
                Exit Sub
            End If

            LastAct(Replace(LangStrs(LIdx, UsingTxt.RG_Ted), "$$$", NowTransAccName))
            GetList()
            Go_ListBoxIdx(NowSelect)

        End If

        FullGC()

    End Sub

    Private Sub ButtonFileInfo_Click(sender As Object, e As EventArgs) Handles ButtonFileInfo.Click

        If NowProcFile = "" Then Exit Sub

        Try

            Dim FileInfoData As New System.IO.FileInfo(NowProcFile)

            Dim FileNameStr As String = LangStrs(LIdx, UsingTxt.FI_Fn) + vbCrLf + System.IO.Path.GetFileName(NowProcFile)

            Dim DateOfBuild As String = FileInfoData.CreationTime.ToShortDateString +
                " " + FileInfoData.CreationTime.ToShortTimeString

            Dim DateOfLW As String = FileInfoData.LastWriteTime.ToShortDateString +
                " " + FileInfoData.LastWriteTime.ToShortTimeString

            MSGBOXNEW(FileNameStr + D_vbcrlf + LangStrs(LIdx, UsingTxt.FI_bt) + vbCrLf + DateOfBuild + D_vbcrlf + LangStrs(LIdx, UsingTxt.FI_Lwt) + vbCrLf +
                      DateOfLW + D_vbcrlf + LangStrs(LIdx, UsingTxt.FI_Cf) + vbCrLf + DirName, MsgBoxStyle.OkOnly, LangStrs(LIdx, UsingTxt.Ti_Info), Me, PictureGray)

        Catch ex As Exception

        End Try

    End Sub

    '======================================= Base Main Window Operate ======================

    Private lastLocation As Point
    Private isMouseDown As Boolean = False

    Private Sub PictureBox2_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBoxMain.MouseDown
        ' 紀錄滑鼠按下的位置
        isMouseDown = True
        lastLocation = e.Location
    End Sub

    Private Sub PictureBox2_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBoxMain.MouseMove
        ' 當滑鼠左鍵按下時，設定窗體的位置
        If isMouseDown Then
            Me.Location = New Point(Me.Location.X + (e.X - lastLocation.X), Me.Location.Y + (e.Y - lastLocation.Y))
        End If
    End Sub

    Private Sub PictureBox2_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBoxMain.MouseUp
        ' 當滑鼠左鍵釋放時，重設 isMouseDown 變數
        isMouseDown = False
    End Sub

    Private Sub ButtonExit_Click(sender As Object, e As EventArgs) Handles ButtonFin.Click
        End_Program()
    End Sub

    Private Sub ButtonHelp_Click(sender As Object, e As EventArgs) Handles ButtonHelp.Click

        If Not Launch_URI(MainWebURL, Sys_Chk.Running_Admin, Sys_Chk.Found_Bad_MSFile) Then
            MSGBOXNEW(LangStrs(LIdx, UsingTxt.Err_Unk), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
        End If

    End Sub

    Private Sub ButtonRestart_Click_1(sender As Object, e As EventArgs) Handles ButtonRestart.Click
        RestartApp2(Sys_Chk.Running_Admin, Me)
    End Sub

    Private Sub PictureWinMin_Click(sender As Object, e As EventArgs) Handles ButtonWinMin.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub FormMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        End_Program()
    End Sub

    '================================ ListBox Scrollbar work ===============

    Dim WithEvents LSCB_UPDW As New Windows.Forms.Timer
    Dim WithEvents LSCB_MSC As New Windows.Forms.Timer
    Dim NowUPorDW As Integer
    Dim LB_Ration As Double
    Dim LB_Range_Scale As Double
    Dim UpY As Integer
    Dim DwY As Integer
    Dim BarIsHolding As Boolean

    Private Sub ListBox_SB_Init()
        UpY = LSCBU.Top + LSCBU.Height
        DwY = LSCBD.Top - LSCBBAR.Height + 1
        LSCB_UPDW.Interval = 150
        LSCB_UPDW.Enabled = False
        LSCB_MSC.Interval = 100
        LSCB_MSC.Enabled = False
        LB_Ration = ListBoxAccounts.ClientRectangle.Height / ListBoxAccounts.ItemHeight
    End Sub

    Private Sub LSCBU_MouseDown(sender As Object, e As MouseEventArgs) Handles _
        LSCBU.MouseDown, LSCBD.MouseDown, LSCBBACK.MouseDown

        If sender.Name = "LSCBU" Then
            NowUPorDW = 0
            ListBoxAccounts.TopIndex -= 1
            LSCBBAR_GoCorrectPos()
        ElseIf sender.Name = "LSCBD" Then
            NowUPorDW = 1
            ListBoxAccounts.TopIndex += 1
            LSCBBAR_GoCorrectPos()
        Else
            Dim WhereIsY As Integer = e.Y + LSCBBACK.Top
            If WhereIsY < LSCBBAR.Top Then
                NowUPorDW = 2
                If ListBoxAccounts.TopIndex - LB_Ration < 0 Then
                    ListBoxAccounts.TopIndex = 0
                Else
                    ListBoxAccounts.TopIndex -= LB_Ration
                End If
                LSCBBAR_GoCorrectPos()
            Else
                NowUPorDW = 3
                ListBoxAccounts.TopIndex += LB_Ration
                LSCBBAR_GoCorrectPos()
            End If

        End If

        LSCB_UPDW.Enabled = True
    End Sub

    Private Sub LSCBWORK(ByVal sender As Object, ByVal e As EventArgs) Handles LSCB_UPDW.Tick
        Select Case NowUPorDW
            Case 0
                ListBoxAccounts.TopIndex -= 1
            Case 1
                ListBoxAccounts.TopIndex += 1
            Case 2
                If ListBoxAccounts.TopIndex - LB_Ration < 0 Then
                    ListBoxAccounts.TopIndex = 0
                Else
                    ListBoxAccounts.TopIndex -= LB_Ration
                End If
            Case 3
                ListBoxAccounts.TopIndex += LB_Ration
        End Select
        LSCBBAR_GoCorrectPos()
    End Sub

    Private Sub LSCBU_MouseUp(sender As Object, e As MouseEventArgs) Handles LSCBU.MouseUp, LSCBD.MouseUp, LSCBBACK.MouseUp
        LSCB_UPDW.Enabled = False
    End Sub

    Private Sub LSCB_MSC_WORK(ByVal sender As Object, ByVal e As EventArgs) Handles LSCB_MSC.Tick
        LSCBBAR_GoCorrectPos()
        LSCB_MSC.Enabled = False
    End Sub

    Private Sub LSCBBAR_GoCorrectPos()

        If LB_Range_Scale > 0 Then
            Dim TmpDbl As Double = CDbl(ListBoxAccounts.TopIndex) / LB_Range_Scale
            TmpDbl *= CDbl(DwY - UpY)
            LSCBBAR.Top = CInt(TmpDbl + UpY)
        Else
            LSCBBAR.Top = UpY
        End If

    End Sub

    Private Sub LSCBBAR_MouseDown(sender As Object, e As MouseEventArgs) Handles LSCBBAR.MouseDown
        If e.Button = MouseButtons.Left Then
            If LB_Range_Scale > 0 Then
                BarIsHolding = True
                Cursor = Cursors.SizeNS ' 更改游標形狀以指示按住按鈕時可以移動它
            End If
        End If
    End Sub

    Private Sub LSCBBAR_MouseMove(sender As Object, e As MouseEventArgs) Handles LSCBBAR.MouseMove

        If BarIsHolding Then

            Dim TmpY As Integer = sender.Top + e.Y - sender.Height / 2
            'sender.Left += e.X - sender.Width / 2 ' 移動按鈕的位置

            If (TmpY >= UpY) And (TmpY <= DwY) Then
                ListBoxAccounts.TopIndex = CInt((CDbl(sender.Top - UpY) / CDbl(DwY - UpY)) * LB_Range_Scale)
                sender.Top = TmpY
            ElseIf TmpY < UpY Then
                ListBoxAccounts.TopIndex = 0
                sender.Top = UpY
            End If

        End If
    End Sub

    Private Sub LSCBBAR_MouseUp(sender As Object, e As MouseEventArgs) Handles LSCBBAR.MouseUp
        BarIsHolding = False
        Cursor = Cursors.Default ' 將游標形狀恢復為預設值
    End Sub

    Private Sub ListBox1_MouseWheel(sender As Object, e As MouseEventArgs) Handles ListBoxAccounts.MouseWheel
        LSCB_MSC.Enabled = True
    End Sub

    Private Sub LSCB_MouseWheel(sender As Object, e As MouseEventArgs) Handles _
            LSCBBAR.MouseWheel, LSCBBACK.MouseWheel, LSCBD.MouseWheel, LSCBU.MouseWheel
        If e.Delta > 0 Then
            If ListBoxAccounts.TopIndex - 3 < 0 Then
                ListBoxAccounts.TopIndex = 0
            Else
                ListBoxAccounts.TopIndex -= 3
            End If
        Else
            ListBoxAccounts.TopIndex += 3
        End If
        LSCBBAR_GoCorrectPos()
    End Sub

    Private Sub Go_ListBoxIdx(Gowhere As Integer)

        If ListBoxAccounts.SelectedIndex <> Gowhere Then

            If Gowhere >= ListBoxAccounts.Items.Count Then
                Gowhere = ListBoxAccounts.Items.Count - 1
            End If

            ListBoxAccounts.SelectedIndex = Gowhere
        End If

        LSCBBAR_GoCorrectPos()
    End Sub

    '====================== Last action

    Private Sub LastAct(Str As String)

        'Label_Act_Last.Text = DateTime.Now.ToString("HH:mm:ss")
        LastActionSec = DateTime.Now
        Label_Act_Work.Text = Str

        Dim ActWork As New Bitmap(Label_Act_Work.Width, Label_Act_Work.Height)
        Dim NewRec As Rectangle
        NewRec.Width = Label_Act_Work.Width
        NewRec.Height = Label_Act_Work.Height
        Label_Act_Work.DrawToBitmap(ActWork, NewRec)
        Label_Act_Show.Image = ResizeBitmap(ActWork, 0.4166, 0.45)

    End Sub

    '====================== Button Visual works =========================

    Dim b_Logout As New Bitmap(My.Resources.Resource1.button_LOGOUT)
    Dim b_Final As New Bitmap(My.Resources.Resource1.button_Final)
    Dim b_HELP As New Bitmap(My.Resources.Resource1.button_HELP)
    Dim b_Launch As New Bitmap(My.Resources.Resource1.button_Launch)
    Dim b_COPY As New Bitmap(My.Resources.Resource1.button_COPY)
    Dim b_view_small As New Bitmap(My.Resources.Resource1.button_view_small)
    Dim b_copy_small As New Bitmap(My.Resources.Resource1.button_copy_small)
    Dim b_view As New Bitmap(My.Resources.Resource1.button_view)
    Dim b_HKO_on As New Bitmap(My.Resources.Resource1.button_HKO_small_on)
    Dim b_HKO_off As New Bitmap(My.Resources.Resource1.button_HKO_small_off)
    Dim b_PictureBoxCATMAN As New Bitmap(My.Resources.Resource1.button_CATMAN)
    Dim b_security_check As New Bitmap(My.Resources.Resource1.button_security_check)

    Dim b_Logout_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_LOGOUT)
    Dim b_Final_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_Final)
    Dim b_HELP_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_HELP)
    Dim b_Launch_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_Launch)
    Dim b_COPY_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_COPY)
    Dim b_copy_small_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_copy_small)
    Dim b_security_check_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_security_check)

    Dim b_view_small_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_view_small)
    Dim b_view_small_Son As Bitmap = Make_Button_HueChange(My.Resources.Resource1.button_view_small, 315) 'Not actully use
    Dim b_view_small_Son_on As Bitmap = Make_Button_brighter(b_view_small_Son, 1.1)

    Dim b_view_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_view)
    Dim b_view_Son As Bitmap = Make_Button_HueChange(My.Resources.Resource1.button_view, 315)
    Dim b_view_Son_on As Bitmap = Make_Button_brighter(b_view_Son, 1.1)

    Dim b_HKO_Soff_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_HKO_small_off, 1.1)
    Dim b_HKO_Son_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_HKO_small_on, 1.1)

    Dim b_PictureBoxCATMAN_on As Bitmap = Make_Button_brighter(Make_Button_HueChange(b_PictureBoxCATMAN, 15), 1.1)

    Dim BTN_Win_Min As New Bitmap(My.Resources.Resource1.WinMin)
    Dim BTN_Win_Min_on As Bitmap = Make_Button_brighter(BTN_Win_Min)

    Dim BTN_Pwd As New Bitmap(My.Resources.Resource1.TOPSEC)
    Dim BTN_Pwd_on As Bitmap = Make_Button_brighter(BTN_Pwd)

    Private Sub Mouse_Enter(sender As Object, e As EventArgs) Handles _
        ButtonRestart.MouseEnter, ButtonFin.MouseEnter, ButtonHelp.MouseEnter, ButtonLaunch.MouseEnter,
        ButtonCopyAccount.MouseEnter, PictureBoxPwdVi.MouseEnter, PictureBoxPwdCPY.MouseEnter,
        ButtonCopyReg.MouseEnter, ButtonViewNote.MouseEnter, PictureBoxCATMAN.MouseEnter,
        ButtonHotkeyMode.MouseEnter, ButtonSecCheck.MouseEnter, ButtonWinMin.MouseEnter,
        ButtonPwd.MouseEnter

        Select Case sender.Name
            Case "ButtonSystemCheck"
                ButtonSecCheck.Image = b_security_check_on
            Case "ButtonRestart"
                ButtonRestart.Image = b_Logout_on
            Case "ButtonFin"
                ButtonFin.Image = b_Final_on
            Case "ButtonHelp"
                ButtonHelp.Image = b_HELP_on
            Case "ButtonLaunch"
                ButtonLaunch.Image = b_Launch_on
            Case "ButtonCopyAccount"
                ButtonCopyAccount.Image = b_COPY_on
            Case "PictureBoxPwdVi"
                PictureBoxPwdVi.Image = b_view_small_on
            Case "PictureBoxPwdCPY"
                PictureBoxPwdCPY.Image = b_copy_small_on
            Case "ButtonCopyReg"
                ButtonCopyReg.Image = b_COPY_on
            Case "ButtonViewNote"
                If Not IsShowingMessage Then
                    ButtonViewNote.Image = b_view_on
                Else
                    ButtonViewNote.Image = b_view_Son_on
                End If
            Case "PictureBoxCATMAN"
                PictureBoxCATMAN.Image = b_PictureBoxCATMAN_on
            Case "ButtonHotkeyMode"
                If TextBox_BHKMHelper.Text = "0" Then ButtonHotkeyMode.Image = b_HKO_Soff_on
                If TextBox_BHKMHelper.Text = "1" Then ButtonHotkeyMode.Image = b_HKO_Son_on
            Case "ButtonSecCheck"
                ButtonSecCheck.Image = b_security_check_on
            Case "ButtonWinMin"
                ButtonWinMin.Image = BTN_Win_Min_on
            Case "ButtonPwd"
                ButtonPwd.Image = BTN_Pwd_on
        End Select
    End Sub

    Private Sub Mouse_Leave(sender As Object, e As EventArgs) Handles _
        ButtonRestart.MouseLeave, ButtonFin.MouseLeave, ButtonHelp.MouseLeave, ButtonLaunch.MouseLeave,
        ButtonCopyAccount.MouseLeave, PictureBoxPwdVi.MouseLeave, PictureBoxPwdCPY.MouseLeave,
        ButtonCopyReg.MouseLeave, ButtonViewNote.MouseLeave, PictureBoxCATMAN.MouseLeave,
        ButtonHotkeyMode.MouseLeave, ButtonSecCheck.MouseLeave, ButtonWinMin.MouseLeave,
        ButtonPwd.MouseLeave

        Select Case sender.Name
            Case "ButtonSystemCheck"
                ButtonSecCheck.Image = b_security_check
            Case "ButtonRestart"
                ButtonRestart.Image = b_Logout
            Case "ButtonFin"
                ButtonFin.Image = b_Final
            Case "ButtonHelp"
                ButtonHelp.Image = b_HELP
            Case "ButtonLaunch"
                ButtonLaunch.Image = b_Launch
            Case "ButtonCopyAccount"
                ButtonCopyAccount.Image = b_COPY
            Case "PictureBoxPwdVi"
                PictureBoxPwdVi.Image = b_view_small
            Case "PictureBoxPwdCPY"
                PictureBoxPwdCPY.Image = b_copy_small
            Case "ButtonCopyReg"
                ButtonCopyReg.Image = b_COPY
            Case "ButtonViewNote"
                If Not IsShowingMessage Then
                    ButtonViewNote.Image = b_view
                Else
                    ButtonViewNote.Image = b_view_Son
                End If
            Case "PictureBoxCATMAN"
                PictureBoxCATMAN.Image = b_PictureBoxCATMAN
            Case "ButtonHotkeyMode"
                If TextBox_BHKMHelper.Text = "0" Then ButtonHotkeyMode.Image = b_HKO_off
                If TextBox_BHKMHelper.Text = "1" Then ButtonHotkeyMode.Image = b_HKO_on
            Case "ButtonSecCheck"
                ButtonSecCheck.Image = b_security_check
            Case "ButtonWinMin"
                ButtonWinMin.Image = BTN_Win_Min
            Case "ButtonPwd"
                ButtonPwd.Image = BTN_Pwd
        End Select
    End Sub

    Dim LBTN_Save_En As New Bitmap(My.Resources.Resource1.button_L_save)
    Dim LBTN_Del_En As New Bitmap(My.Resources.Resource1.button_L_delete)
    Dim LBTN_MoveU_En As New Bitmap(My.Resources.Resource1.button_L_moveUP)
    Dim LBTN_MoveD_En As New Bitmap(My.Resources.Resource1.button_L_moveDWN)
    Dim LBTN_TKey_En As New Bitmap(My.Resources.Resource1.button_L_transKEY)
    Dim LBTN_FInfo_En As New Bitmap(My.Resources.Resource1.button_L_fileInfo)

    Dim LBTN_Save_Di As Bitmap = Make_Button_Gray(My.Resources.Resource1.button_L_save)
    Dim LBTN_Del_Di As Bitmap = Make_Button_Gray(My.Resources.Resource1.button_L_delete)
    Dim LBTN_MoveU_Di As Bitmap = Make_Button_Gray(My.Resources.Resource1.button_L_moveUP)
    Dim LBTN_MoveD_Di As Bitmap = Make_Button_Gray(My.Resources.Resource1.button_L_moveDWN)
    Dim LBTN_TKey_Di As Bitmap = Make_Button_Gray(My.Resources.Resource1.button_L_transKEY)
    Dim LBTN_FInfo_Di As Bitmap = Make_Button_Gray(My.Resources.Resource1.button_L_fileInfo)

    Dim LBTN_Save_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_L_save, 1.2)
    Dim LBTN_delete_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_L_delete, 1.2)
    Dim LBTN_moveUP_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_L_moveUP, 1.2)
    Dim LBTN_moveDWN_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_L_moveDWN, 1.2)
    Dim LBTN_transKEY_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_L_transKEY, 1.2)
    Dim LBTN_fileInfo_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_L_fileInfo, 1.2)

    Private Sub L_Mouse_Enter(sender As Object, e As EventArgs) Handles _
        ButtonSave.MouseEnter, ButtonDelete.MouseEnter, ButtonGoUP.MouseEnter, ButtonGoDown.MouseEnter,
        ButtonTransCatalog.MouseEnter, ButtonFileInfo.MouseEnter

        If sender.Enabled = True Then
            Select Case sender.Name
                Case "ButtonSave"
                    ButtonSave.Image = LBTN_Save_on
                Case "ButtonDelete"
                    ButtonDelete.Image = LBTN_delete_on
                Case "ButtonGoUP"
                    ButtonGoUP.Image = LBTN_moveUP_on
                Case "ButtonGoDown"
                    ButtonGoDown.Image = LBTN_moveDWN_on
                Case "ButtonTransCatalog"
                    ButtonTransCatalog.Image = LBTN_transKEY_on
                Case "ButtonFileInfo"
                    ButtonFileInfo.Image = LBTN_fileInfo_on

            End Select
        End If

    End Sub

    Private Sub L_Mouse_Leave(sender As Object, e As EventArgs) Handles _
        ButtonSave.MouseLeave, ButtonDelete.MouseLeave, ButtonGoUP.MouseLeave, ButtonGoDown.MouseLeave,
        ButtonTransCatalog.MouseLeave, ButtonFileInfo.MouseLeave

        If sender.Enabled = True Then

            Select Case sender.Name
                Case "ButtonSave"
                    ButtonSave.Image = LBTN_Save_En
                Case "ButtonDelete"
                    ButtonDelete.Image = LBTN_Del_En
                Case "ButtonGoUP"
                    ButtonGoUP.Image = LBTN_MoveU_En
                Case "ButtonGoDown"
                    ButtonGoDown.Image = LBTN_MoveD_En
                Case "ButtonTransCatalog"
                    ButtonTransCatalog.Image = LBTN_TKey_En
                Case "ButtonFileInfo"
                    ButtonFileInfo.Image = LBTN_FInfo_En
                Case "ButtonGoUP"
                    B_GoUP_Holding = False
                    B_GoUP_Holding_InTimer = False
                Case "ButtonGoDown"
                    B_GoDN_Holding = False
                    B_GoDN_Holding_InTimer = False
            End Select

        End If

    End Sub

    Private Sub FileSystemWatcher1_Deleted(sender As Object, e As FileSystemEventArgs) Handles FileSystemWatcher1.Deleted
        DetectFileChange(e.Name)
    End Sub

    Private Sub FileSystemWatcher1_Created(sender As Object, e As FileSystemEventArgs) Handles FileSystemWatcher1.Created
        DetectFileChange(e.Name)
    End Sub

    Private Sub FileSystemWatcher1_Renamed(sender As Object, e As RenamedEventArgs) Handles FileSystemWatcher1.Renamed
        If e.OldName.ToUpper.EndsWith(".ACC") Then
            DetectFileChange(e.OldName)
        ElseIf e.Name.ToUpper.EndsWith(".ACC") Then
            DetectFileChange(e.Name)
        End If
    End Sub

    Private Sub FileSystemWatcher1_Changed(sender As Object, e As FileSystemEventArgs) Handles FileSystemWatcher1.Changed
        DetectFileChange(e.Name)
    End Sub

    Private Sub DetectFileChange(whatChanged As String)

        If Not whatChanged.ToUpper.EndsWith(".ACC") Then
            If whatChanged <> Notefile Then
                Exit Sub
            End If
        End If

        If MSGBOXNEW(LangStrs(LIdx, UsingTxt.Er2_Fco), MsgBoxStyle.OkCancel, LangStrs(LIdx, UsingTxt.Ti_Att), Me, PictureGray) = DialogResult.OK Then
            GetList()
        End If

    End Sub

    'ListBox Mouse Hover======================================
    'Dim hoverIndex As Integer = -1 ' 保存當前懸停的項目索引

    'Private Sub ListBoxAccounts_MouseMove(sender As Object, e As MouseEventArgs) Handles ListBoxAccounts.MouseMove
    '    Dim index As Integer = ListBoxAccounts.IndexFromPoint(e.Location)

    '    If index <> hoverIndex Then
    '        hoverIndex = index
    '        ListBoxAccounts.Invalidate() ' 重繪 ListBox，呼叫 DrawItem 事件
    '    End If
    'End Sub

    '' DrawItem 事件，負責自定義項目繪製
    'Private Sub ListBoxAccounts_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ListBoxAccounts.DrawItem
    '    Dim itemText As String = ListBoxAccounts.Items(e.Index).ToString()

    '    ' 如果項目被選取
    '    If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
    '        ' 選取項目時使用不同背景和字體顏色
    '        e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds)
    '        e.Graphics.DrawString(itemText, e.Font, SystemBrushes.HighlightText, e.Bounds)
    '    Else
    '        ' 未選取項目
    '        If e.Index = hoverIndex Then
    '            ' 當懸停時使用自定義背景和字體顏色
    '            e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds)
    '            e.Graphics.DrawString(itemText, e.Font, Brushes.Black, e.Bounds)
    '        Else
    '            ' 預設樣式（恢復原本樣式）
    '            ' 自定義 RGB 顏色 (紅色 255, 綠色 128, 藍色 0)
    '            Dim customColor As Color = Color.FromArgb(126, 237, 176)

    '            ' 使用該顏色創建 SolidBrush
    '            Dim customBrush As New SolidBrush(customColor)


    '            e.Graphics.FillRectangle(Brushes.Black, e.Bounds)
    '            e.Graphics.DrawString(itemText, e.Font, customBrush, e.Bounds)
    '        End If
    '    End If

    '    e.DrawFocusRectangle()
    'End Sub


    'Private Sub TextBoxNote_Enter(sender As Object, e As EventArgs) Handles TextBoxNote1.Enter
    '    TextBoxNote1.Multiline = True
    'End Sub

    'Private Sub TextBoxNote1_Leave(sender As Object, e As EventArgs) Handles TextBoxNote1.Leave
    '    TextBoxNote1.Multiline = False
    'End Sub
End Class

'For Textbox lighton

'Private Sub TextBox_Enter(sender As Object, e As EventArgs) Handles TextBoxTitle.Enter,
'    TextBoxNameAddr.Enter, TextBoxNote2Hid.Enter, TextBoxURL.Enter,
'    TextBoxRegMailPhone.Enter, TextBoxNote1.Enter

'End Sub

'Private Sub TextBox_Leave(sender As Object, e As EventArgs) Handles TextBoxTitle.Leave,
'        TextBoxNameAddr.Leave, TextBoxNote2Hid.Leave,
'        TextBoxRegMailPhone.Leave, TextBoxURL.Leave, TextBoxNote1.Leave

'End Sub

'For batch account file upgrade
'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

'    For IDX01 As Integer = 1 To ListBox1.Items.Count - 1
'        ListBox1.SelectedIndex = IDX01
'        GetPass()
'        NowPassStatue = 3
'        VG_Data_Done = True
'        VG_Title_Done = True
'        Thread.Sleep(200)

'        ' Dim NewPath As String = Get_New_ACC_Filename(Tmpdir)
'        WriteFile(NowProcFile, AES_KEY_Protected, DERIVED_IDT_Protected)
'    Next

'End Sub



