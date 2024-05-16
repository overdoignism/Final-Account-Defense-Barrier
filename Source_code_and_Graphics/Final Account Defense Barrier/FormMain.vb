'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

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
    Const INDTstr As String = "AccountMan"
    Const Notefile As String = "CATNOTE.SET"
    Const SDesktopArgu As String = "SECUREDESKTOP"
    Const NONOTICEStr As String = "NONOTICE"
    Const LangFile As String = "Lang_MOD.TXT"

    Private VG_Data_Done As Boolean = False
    Private VG_Title_Done As Boolean = False
    Private VG_CCC_Done As Boolean = False
    Private FillTrash() As String

    Public SecureDesktop As Boolean = False

    Dim version As Version = Reflection.Assembly.GetEntryAssembly().GetName().Version
    Dim versionNumber As String = version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision

    Dim DbTester As New Windows.Forms.Timer()

    <DllImport("ntdll.dll", SetLastError:=True)>
    Private Shared Function NtQueryInformationProcess(processHandle As IntPtr, processInformationClass As Integer, ByRef processInformation As IntPtr, processInformationLength As Integer, ByRef returnLength As Integer) As Integer
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            If My.Computer.FileSystem.FileExists(LangFile) Then
                If My.Computer.FileSystem.GetFileInfo(LangFile).Length <= 51200 Then
                    Dim IOreader1 As IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(LangFile)

                    For IDX01 As Integer = 0 To UBound(TextStrs)
                        If Not IOreader1.EndOfStream Then
                            Try
                                TextStrs(IDX01) = Replace(IOreader1.ReadLine, "\n", vbCrLf)
                            Catch ex As Exception
                            End Try
                        End If
                    Next
                    IOreader1.Close()
                    IOreader1.Dispose()
                End If
            End If
        Catch ex As Exception
        End Try

        Dim osVersion As OperatingSystem = Environment.OSVersion
        OSver = (osVersion.Version.Major * 10) + osVersion.Version.Minor

        If OSver >= 62 Then
            SPMP_Set()
        Else
            MSGBOXNEW(TextStrs(63), MsgBoxStyle.Critical, TextStrs(9), Me, PictureGray)
        End If

        SeLock()
        ProcessPriorityUp()
        WER_Dis()

        '===================== Arguments

        Dim Arguments() As String = Environment.GetCommandLineArgs()

        For Each ArgStr As String In Arguments

            If ArgStr.Length <= 100 Then

                If ArgStr.ToUpper = SDesktopArgu Then
                    SecureDesktop = True
                    SeuDeskName = Security.Cryptography.ProtectedData.Protect(
                        System.Text.Encoding.Unicode.GetBytes(Random_Strs(7, 9, 0)), Nothing, DataProtectionScope.CurrentUser)
                End If

                If ArgStr.ToUpper = NONOTICEStr Then NoNotice = True

                If ArgStr.ToUpper.StartsWith("OPACITY,") Then
                    Dim TMPSTR1() As String = ArgStr.Split(",")
                    If TMPSTR1.Length > 1 Then
                        ALLOPACITY = Val(TMPSTR1(1)) / 100
                        Me.Opacity = ALLOPACITY
                    End If
                End If

                If ArgStr.ToUpper.StartsWith("SALT,") Then
                    Dim TMPSTR1() As String = ArgStr.Split(",")
                    If TMPSTR1.Length > 1 Then
                        Dim SHA256_Worker As New Security.Cryptography.SHA256CryptoServiceProvider
                        TheSalt = SHA256_Worker.ComputeHash(System.Text.Encoding.Unicode.GetBytes(TMPSTR1(1)))
                    End If
                End If

            End If

        Next

        '===============================================

        'NoNotice = True 'for test
        'SecureDesktop = True 'for test

        '================= Get Monitor Scale
        GetMonScale(Me)

        '========= BIP39 Load
        Load_BIP39_Word()

        'Me.SetStyle(ControlStyles.UserPaint, True)
        'Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        'Me.SetStyle(ControlStyles.DoubleBuffer, True)
        'Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)

        '================================ Login stage =====
        Me.CenterToScreen()
        Dim TmpDR As DialogResult

        If SecureDesktop Then
            TmpDR = LogInFormWork(TextStrs(59), DirName, AES_KEY_Protected, DERIVED_IDT_Protected, 0, Nothing, NoNotice, PictureGray, TheSalt)
        Else
            TmpDR = LogInFormWork(TextStrs(59), DirName, AES_KEY_Protected, DERIVED_IDT_Protected, 0, Nothing, NoNotice, PictureGray, TheSalt, Me)
        End If

        If TmpDR <> DialogResult.OK Then End_Program()

        FullGC()
        '=============================================

        '============= Set text and tooltip
        Dim ToolTip1 As System.Windows.Forms.ToolTip = New System.Windows.Forms.ToolTip()
        ToolTip1.SetToolTip(TextBoxTitle, TextStrs(31))
        ToolTip1.SetToolTip(TextBoxURL, TextStrs(32))
        ToolTip1.SetToolTip(TextBoxNameAddr, TextStrs(33))
        ToolTip1.SetToolTip(TextBoxRegMailPhone, TextStrs(36))
        ToolTip1.SetToolTip(TextBoxNote1, TextStrs(50))
        ToolTip1.SetToolTip(TextBoxNote2Hid, TextStrs(37))
        ToolTip1.SetToolTip(ButtonHotkeyMode, TextStrs(101))
        ToolTip1.InitialDelay = 1

        For IDX01 As Integer = 53 To 57
            FormConfig.CB_SIM1.Items.Add(TextStrs(IDX01))
            FormConfig.CB_SIM2.Items.Add(TextStrs(IDX01))
        Next

        For Each TmpStr As String In SCutChar
            FormConfig.TB_AC_KEY.Items.Add(TmpStr)
            FormConfig.TB_PW_KEY.Items.Add(TmpStr)
        Next

        For IDX01 As Integer = 73 To 79
            FormConfig.ComboBoxTimer.Items.Add(TextStrs(IDX01))
        Next
        '===========================

        '====================== Listbox Scrollbar
        ListBox_SB_Init()

        '===================================== AutoCountDownClose
        AutoCloseTimer.Interval = 1000
        AutoCloseTimer.Enabled = True
        FormConfig.ComboBoxTimer.SelectedIndex = 0
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

        LastAct(TextStrs(62))
        LabelBy.Text = "▎ By overdoingism Lab."
        LABVER.Text = "▎ " + TextStrs(42) + " " + versionNumber

        DbTester.Interval = 1000
        DbTester.Enabled = True
        AddHandler DbTester.Tick, AddressOf Timer2_Tick

        '========= Read-only mode

        Select Case CreateLockFile()
            Case 0
            Case 1
                MSGBOXNEW(TextStrs(104) + vbCrLf + vbCrLf + TextStrs(105), MsgBoxStyle.Exclamation, TextStrs(107), Me, PictureGray)
            Case 2
                MSGBOXNEW(TextStrs(104) + vbCrLf + vbCrLf + TextStrs(106), MsgBoxStyle.Exclamation, TextStrs(107), Me, PictureGray)
        End Select

        If Not NotLocked Then
            PictureBoxPwd.Image = PwdGrayImage
            PictureBoxPwd.Enabled = False
            PictureBoxCATMAN.Image = Make_Button_Gray(My.Resources.Resource1.button_CATMAN)
            PictureBoxCATMAN.Enabled = False
            TextBoxTitle.ReadOnly = True
            TextBoxURL.ReadOnly = True
            TextBoxNameAddr.ReadOnly = True
            TextBoxRegMailPhone.ReadOnly = True
            TextBoxNote1.ReadOnly = True
            TextBoxNote2Hid.ReadOnly = True
        End If
        '===========================================

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs)

#If Not DEBUG Then
        If IsDebugged() Then
            DbTester.Enabled = False
            MSGBOXNEW(TextStrs(99), MsgBoxStyle.Critical, TextStrs(100), Me, PictureGray)
        End If
#End If

    End Sub

    Public Function IsDebugged() As Boolean
        Dim isRemoteDebuggerPresent As IntPtr = IntPtr.Zero
        Dim returnLength As Integer
        NtQueryInformationProcess(System.Diagnostics.Process.GetCurrentProcess().Handle, 7, isRemoteDebuggerPresent, Marshal.SizeOf(isRemoteDebuggerPresent), returnLength)
        Return isRemoteDebuggerPresent <> IntPtr.Zero
    End Function

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

        Read_and_decrypt(ListBox1.SelectedIndex)
        GoCorrectPos()
        FullGC()

    End Sub

    '============== Subs and functions ================

    Private Sub Read_and_decrypt(TheIndex As Long) 'ByRef File_To_Decrypt As FileLists)

        Dim ErrFlag As Boolean = False

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
            TextBoxNote2Hid.UseSystemPasswordChar = True
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

            LastAct(TextStrs(27))
            GoCorrectPos()

            Exit Sub

        Else

            Dim AES_IV_USE(15) As Byte

            If NotLocked Then
                ButtonDelete.Enabled = True
                ButtonDelete.Image = LBTN_Del_En
            End If

            NowProcFile = FilesList1(TheIndex).FileName
            If My.Computer.FileSystem.GetFileInfo(NowProcFile).Length > 1048576 Then FilesList1(TheIndex).FileIsBad = True

            If FilesList1(TheIndex).FileIsBad Then
                ErrFlag = True
            Else

                Try
                    '==================== Stage 1 init
                    Dim TheEncLib As New Encode_Libs
                    Dim IOreader1 As IO.StreamReader

                    IOreader1 = My.Computer.FileSystem.OpenTextFileReader(NowProcFile)
                    TextBoxTitle.Text = FilesList1(TheIndex).ShowName

                    Dim AllFileStr() As String = Replace(IOreader1.ReadToEnd, vbCrLf, vbCr).Split(vbCr)

                    IOreader1.Close()
                    IOreader1.Dispose()

                    If AllFileStr.Length = 4 Then

                        Dim SHA256_Worker As New Security.Cryptography.SHA256CryptoServiceProvider
                        Dim TmpWork1 As String = AllFileStr(0) + vbCrLf + AllFileStr(1) + vbCrLf + AllFileStr(2)

                        If AllFileStr(3) <> TheEncLib.ByteIn_StringOut _
                            (SHA256_Worker.ComputeHash(System.Text.Encoding.Unicode.GetBytes(TmpWork1 +
                          TheEncLib.ByteIn_StringOut(Security.Cryptography.ProtectedData.Unprotect(
                          DERIVED_IDT_Protected, Nothing, DataProtectionScope.CurrentUser))))) Then
                            ErrFlag = True
                        End If

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
                            TextBoxTitle.Text = TextStrs(22)
                            ErrFlag = True
                        ElseIf WorkStr2(0) <> INDTstr Then
                            TextBoxTitle.Text = TextStrs(22)
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
                    ErrFlag = True
                End Try

            End If

        End If

        If ErrFlag Then
            NowPassStatue = 0
            FilesList1(TheIndex).FileIsBad = True
            LastAct(TextStrs(67))
            MSGBOXNEW(TextStrs(65), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
        Else

            If NotLocked Then
                ButtonGoUP.Enabled = True
                ButtonGoUP.Image = LBTN_MoveU_En
                ButtonGoDown.Enabled = True
                ButtonGoDown.Image = LBTN_MoveD_En
                ButtonTransCatalog.Enabled = True
                ButtonTransCatalog.Image = LBTN_TKey_En
            End If

            ButtonFileInfo.Enabled = True
            ButtonFileInfo.Image = LBTN_FInfo_En
            ButtonCopyReg.Enabled = True

            VG_Data_Done = False
            VG_Title_Done = False
            LastAct(Replace(TextStrs(68), "$$$", TextBoxTitle.Text))
            FullGC()
        End If

        ReadingWorking = False

    End Sub

    Private Function WriteFile(Filename1 As String, ByRef PKey() As Byte, ByRef DICurrent() As Byte) As Boolean

        WriteFile = False

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

                TmpStr4 += vbCrLf + TheEncLib.ByteIn_StringOut(
                SHA256_Worker.ComputeHash(System.Text.Encoding.Unicode.GetBytes(TmpStr4 +
                TheEncLib.ByteIn_StringOut(Security.Cryptography.ProtectedData.Unprotect(
                DICurrent, Nothing, DataProtectionScope.CurrentUser)))))

                If My.Computer.FileSystem.FileExists(Filename1) Then
                    Dim FileFill(My.Computer.FileSystem.GetFileInfo(Filename1).Length) As Byte
                    My.Computer.FileSystem.WriteAllBytes(Filename1, FileFill, False)
                End If

                My.Computer.FileSystem.WriteAllText(Filename1, TmpStr4, False)

            End If

        Catch ex As Exception

            WriteFile = True

        End Try

        TheEncLib.Dispose()

    End Function

    Private Sub GetList()

        ListBox1.Items.Clear()
        ListBox1.Items.Add(TextStrs(27))

        ReDim FilesList1(0)
        Dim IDX01 As Integer = 1
        Dim IOreader1 As IO.StreamReader

        For Each foundFile As String In My.Computer.FileSystem.GetFiles(DirName, FileIO.SearchOption.SearchTopLevelOnly, "*.ACC")

            Dim ErrFlag As Boolean = False

            Try

                ReDim Preserve FilesList1(IDX01)
                FilesList1(IDX01).FileName = foundFile
                If My.Computer.FileSystem.GetFileInfo(FilesList1(IDX01).FileName).Length > 1048576 Then ErrFlag = True

                If ErrFlag = False Then

                    IOreader1 = My.Computer.FileSystem.OpenTextFileReader(foundFile)

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
                FilesList1(IDX01).ShowName = TextStrs(22)
                FilesList1(IDX01).FileIsBad = True
            End If

            ListBox1.Items.Add(FilesList1(IDX01).ShowName)
            IDX01 += 1

        Next

        LB_Range_Scale = CDbl(ListBox1.Items.Count) - LB_Ration

    End Sub

    Private Function Delete_ACC_File(What_file As String, Ask_String As String, continuous_mode As Boolean) As Integer

        '0 = OK 1 = Cancel 2 = Error

        Dim OLDidx As Integer = ListBox1.SelectedIndex
        Delete_ACC_File = 0

        If continuous_mode OrElse (MSGBOXNEW(Ask_String.Replace("$$$", TextBoxTitle.Text), MsgBoxStyle.OkCancel, TextStrs(9), Me, PictureGray) = MsgBoxResult.Ok) Then
            Try
                Dim OverWriteByte(My.Computer.FileSystem.GetFileInfo(What_file).Length - 1) As Byte
                System.IO.File.WriteAllBytes(What_file, OverWriteByte)
                My.Computer.FileSystem.DeleteFile(What_file)

                If Not continuous_mode Then GetList()

                If OLDidx >= 1 Then Go_ListBoxIdx(OLDidx - 1)

            Catch ex As Exception
                Delete_ACC_File = 2
            End Try
        Else
            Delete_ACC_File = 1
        End If

    End Function

    '===================== Catalog Manager ====================================

    Private Sub TransFullCat()

        Dim ErrFlag As Boolean = False
        Dim DirNameCurrent As String = ""
        Dim AESKeyCurrent(0) As Byte
        Dim DERIVED_IDT_Current(0) As Byte
        Dim NeedRefresh As Boolean
        Dim NowSelect As Integer = ListBox1.SelectedIndex

        Dim TmpDR As DialogResult

        If SecureDesktop Then
            TmpDR = LogInFormWork(TextStrs(69), DirNameCurrent, AESKeyCurrent, DERIVED_IDT_Current, 1, Nothing, Nothing, PictureGray, TheSalt)
        Else
            TmpDR = LogInFormWork(TextStrs(69), DirNameCurrent, AESKeyCurrent, DERIVED_IDT_Current, 1, Nothing, Nothing, PictureGray, TheSalt, Me)
        End If

        If TmpDR = DialogResult.OK Then


            If DirName = DirNameCurrent Then
                If MSGBOXNEW(TextStrs(39), MsgBoxStyle.OkCancel, TextStrs(5), Me, PictureGray) = DialogResult.Cancel Then
                    FullGC()
                    Exit Sub
                Else
                    NeedRefresh = True
                End If
            End If

            Me.Enabled = False

            For IDX01 As Integer = 1 To ListBox1.Items.Count - 1

                Go_ListBoxIdx(IDX01)

                If FilesList1(IDX01).FileIsBad = False Then

                    Dim Filename2 As String = Get_New_ACC_Filename(DirNameCurrent)

                    GetPass()
                    NowPassStatue = 3
                    VG_Data_Done = True
                    VG_Title_Done = True

                    If WriteFile(Filename2, AESKeyCurrent, DERIVED_IDT_Current) Then
                        ErrFlag = True
                        Exit For
                    End If

                End If

            Next

            Me.Enabled = True

            If Not ErrFlag Then
                LastAct(TextStrs(43))

                If MSGBOXNEW(TextStrs(43) + vbCrLf + vbCrLf + TextStrs(10).Replace("$$$", LabelCatalog.Text),
                     MsgBoxStyle.OkCancel, TextStrs(9), Me, PictureGray) <> MsgBoxResult.Cancel Then
                    Full_Catalog_Delete()
                Else
                    If NeedRefresh Then
                        GetList()
                        Go_ListBoxIdx(NowSelect)
                    End If
                End If

            Else
                LastAct(TextStrs(67))
                MSGBOXNEW(TextStrs(66), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
            End If

        End If

        Exe_Fill_Trash()
        FullGC()

    End Sub

    Private Sub Full_Catalog_Delete()

        Dim ErrFlag As Boolean = False

        If MSGBOXNEW(TextStrs(15), 65535, TextStrs(9), Me, PictureGray) = MsgBoxResult.Ok Then

            Try

                Dim foundFiles As FileLists

                For IDX01 As Long = 1 To UBound(FilesList1)

                    foundFiles = FilesList1(IDX01)

                    Try
                        Dim FileLen As Long = My.Computer.FileSystem.GetFileInfo(foundFiles.FileName).Length
                        Dim OverWriteByte(FileLen - 1) As Byte
                        System.IO.File.WriteAllBytes(foundFiles.FileName, OverWriteByte)
                        My.Computer.FileSystem.DeleteFile(foundFiles.FileName)
                    Catch ex As Exception
                        ErrFlag = True
                    End Try

                Next

                My.Computer.FileSystem.DeleteDirectory(DirName, FileIO.DeleteDirectoryOption.DeleteAllContents)

            Catch ex As Exception

                ErrFlag = True

            End Try

            If ErrFlag Then
                LastAct(TextStrs(67))
                MSGBOXNEW(TextStrs(64), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)

            End If

            RestartApp2(SecureDesktop, False)

        End If

    End Sub

    Private Sub PictureBoxCATMAN_Click(sender As Object, e As EventArgs) Handles PictureBoxCATMAN.Click

        FormConfig.ComboBoxTimer.SelectedIndex = Val(CAT_setting_Str(0))
        FormConfig.TB_AC_KEY.SelectedIndex = Val(CAT_setting_Str(2))
        FormConfig.CB_SIM1.SelectedIndex = Val(CAT_setting_Str(3))
        FormConfig.TB_PW_KEY.SelectedIndex = Val(CAT_setting_Str(5))
        FormConfig.CB_SIM2.SelectedIndex = Val(CAT_setting_Str(6))

        FormConfig.FormL = Me.Left + (Me.Width - FormConfig.Width) / 2
        FormConfig.FormT = Me.Top + (Me.Height - FormConfig.Height) / 2
        FormConfig.Opacity = Me.Opacity
        FormConfig.PictureBox1.Image = CONFIG_FORM_IMG

        MakeWindowsBlur(Me, PictureGray)
        Dim DR As DialogResult = FormConfig.ShowDialog(Me)
        PictureGray.Visible = False
        PictureGray.SendToBack()
        My.Application.DoEvents()

        Select Case DR
            Case DialogResult.OK
                ACTLimitSelectIDX = FormConfig.ComboBoxTimer.SelectedIndex
                RegisterKeys(CAT_setting_Str)
                AutoCountDownClose(ACTLimitSelect(CInt(CAT_setting_Str(0))))
                Write_CatDatas()
            Case DialogResult.Ignore ' Use "Ignore" for catalog transfer
                If FormConfig.OtherWorkMode = 1 Then
                    If ListBox1.Items.Count > 1 Then
                        TransFullCat()
                    End If
                ElseIf FormConfig.OtherWorkMode = 2 Then
                    Full_Catalog_Delete()
                End If
            Case DialogResult.Abort ' ===============Use "Abort" for CSV import

                If ParseCSV_S1(FormConfig.ReturnCSV) = 0 Then
                    GetList()
                    LastAct(TextStrs(91))
                    MSGBOXNEW(TextStrs(91) + vbCrLf + vbCrLf + TextStrs(94), MsgBoxStyle.OkOnly, TextStrs(9), Me, PictureGray)
                    Go_ListBoxIdx(ListBox1.Items.Count - 1)
                Else
                    LastAct(TextStrs(92))
                    MSGBOXNEW(TextStrs(92) + vbCrLf + vbCrLf + TextStrs(93), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
                End If

                FullGC()
                Exe_Fill_Trash()
                FullGC()

            Case DialogResult.Retry ' ===============Use "Retry" for CSV export

                Dim ErrFlag As Boolean = False
                Dim DirNameCurrent As String = ""
                Dim AESKeyCurrent(0) As Byte
                Dim DERIVED_IDT_Current(0) As Byte
                Dim TmpDR As DialogResult

                If SecureDesktop Then
                    TmpDR = LogInFormWork(TextStrs(69), DirNameCurrent, AESKeyCurrent, DERIVED_IDT_Current, 3, Nothing, Nothing, PictureGray, TheSalt)
                Else
                    TmpDR = LogInFormWork(TextStrs(69), DirNameCurrent, AESKeyCurrent, DERIVED_IDT_Current, 3, Nothing, Nothing, PictureGray, TheSalt, Me)
                End If

                If TmpDR = DialogResult.OK Then

                    If DirName <> DirNameCurrent Then
                        MSGBOXNEW(TextStrs(96), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
                        Exit Sub
                    End If

                    Try
                        Dim CSV_Head As String = "TITLE,URL,USERNAME,PASSWORD,REGDATA,NOTES,NOTES2,SETS"
                        Dim CSV_Filename As String = GetOkFilename(LabelCatalog.Text + " - " + Format(Now, "yyyyMMddHHmmss") + ".CSV")
                        Dim TheWriter As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(CSV_Filename, False)
                        TheWriter.WriteLine(CSV_Head)
                        Dim Quo As String = """"
                        Dim Account_Count As String = " /" + Str(ListBox1.Items.Count - 1)

                        Dim CSVW As New FormCSVWorking
                        CSVW.ProgLab.Text = "0" + Account_Count
                        CSVW.FormW = Me.Width
                        CSVW.FormH = Me.Height
                        CSVW.FormT = Me.Top
                        CSVW.FormL = Me.Left
                        CSVW.Opacity = Me.Opacity
                        CSVW.PictureMode.Image = My.Resources.Resource1.CSV_EXP
                        CSVW.Show()

                        MakeWindowsBlur(Me, PictureGray)
                        My.Application.DoEvents()

                        For idx01 As Integer = 1 To ListBox1.Items.Count - 1
                            CSVW.ProgLab.Text = Str(idx01) + Account_Count
                            My.Application.DoEvents()

                            If Not FilesList1(idx01).FileIsBad Then
                                ListBox1.SelectedIndex = idx01
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

                        UnMakeWindowsBlur(PictureGray)

                        TheWriter.Close()
                        TheWriter.Dispose()
                        CSVW.Close()
                        CSVW.Dispose()

                        MSGBOXNEW(Replace(TextStrs(98), "$$$", CSV_Filename) + vbCrLf + vbCrLf + TextStrs(94), MsgBoxStyle.OkOnly, TextStrs(9), Me, PictureGray)

                        Process.Start("explorer.exe", "/select," + CSV_Filename)

                    Catch ex As Exception

                        MSGBOXNEW(TextStrs(66), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)

                    End Try

                End If

        End Select

    End Sub

    '============================== CSV import
    Private Function ParseCSV_S1(ByRef InputStr As String) As Integer

        Dim CSVW As New FormCSVWorking
        Dim ErrFlag As Integer = 0

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

            MakeWindowsBlur(Me, PictureGray)
            My.Application.DoEvents()

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

                    If Not WriteFile(Get_New_ACC_Filename(DirName), AES_KEY_Protected, DERIVED_IDT_Protected) Then
                        LastAct(Replace(TextStrs(20), "$$$", TextBoxTitle.Text))
                        CSVW.ProgLab.Text = Str((IDX01 + 1) / CSV_head.Length) + Acc_Counts
                        My.Application.DoEvents()
                    Else
                        LastAct(TextStrs(67))
                        MSGBOXNEW(TextStrs(66), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
                    End If
                End If

            Next

        Catch ex As Exception
            If CSVW.Visible Then
                CSVW.Visible = False
                CSVW.Close()
                CSVW.Dispose()
                UnMakeWindowsBlur(PictureGray)
            End If
            ErrFlag = 1

        End Try

        Thread.Sleep(50)
        CSVW.Visible = False
        CSVW.Close()
        CSVW.Dispose()
        UnMakeWindowsBlur(PictureGray)

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

        Try
            Dim DPNOTE As String = DirName + "\" + Notefile
            Dim IOWer1 As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(DPNOTE, False)

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
            LastAct(TextStrs(30))
        Catch ex As Exception
            MSGBOXNEW(ex.Message, MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
        End Try

    End Sub

    Private Sub Read_CatDatas()

        GetList()

        ListBox1.SelectedIndex = 0

        Dim CAT_note_File As String

        FormConfig.TB_AC_KEY.SelectedIndex = 1
        FormConfig.TB_PW_KEY.SelectedIndex = 2
        FormConfig.CB_SIM1.SelectedIndex = 0
        FormConfig.CB_SIM2.SelectedIndex = 0
        FormConfig.ComboBoxTimer.SelectedIndex = 0
        LabelCatalog.Text = TextStrs(44)
        FormConfig.TextBoxCatalog.Text = LabelCatalog.Text

        Try

            CAT_note_File = DirName + "\" + Notefile

            If My.Computer.FileSystem.FileExists(CAT_note_File) Then

                If My.Computer.FileSystem.GetFileInfo(CAT_note_File).Length < 102400 Then
                    Dim IOreader1 As IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader(CAT_note_File)
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

                    IOreader1.Close()
                    IOreader1.Dispose()

                End If

            Else
                FormConfig.TextBoxCatalog.Text = LabelCatalog.Text
            End If

        Catch ex As Exception
            MSGBOXNEW(ex.Message, MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
        End Try

    End Sub

    '============================== Auto CountDown Close ===========

    Public DIGI_NUM() As Bitmap = {My.Resources.Resource1.DIGI_Y_0, My.Resources.Resource1.DIGI_Y_1,
    My.Resources.Resource1.DIGI_Y_2, My.Resources.Resource1.DIGI_Y_3, My.Resources.Resource1.DIGI_Y_4,
    My.Resources.Resource1.DIGI_Y_5, My.Resources.Resource1.DIGI_Y_6, My.Resources.Resource1.DIGI_Y_7,
    My.Resources.Resource1.DIGI_Y_8, My.Resources.Resource1.DIGI_Y_9}

    Public DIGI_NUM_N As New Bitmap(My.Resources.Resource1.DIGI_Y__)
    Public WithEvents AutoCloseTimer As New Windows.Forms.Timer

    Dim CONFIG_FORM_IMG As New Bitmap(Image.FromStream(New IO.MemoryStream(My.Resources.Resource1.SETTINGS_PNG)))

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
            PicTimerACT.Visible = True
            PicTimerINACT.Visible = False
            ACTCount += 1
            TimeCountDown = TimeSpan.FromSeconds(ACTLimit - ACTCount + 1).ToString("mm\:ss").ToCharArray
            PicDIGI_1.Image = DIGI_NUM(Convert.ToInt32(TimeCountDown(0)) - 48)
            PicDIGI_2.Image = DIGI_NUM(Convert.ToInt32(TimeCountDown(1)) - 48)
            PicDIGI_3.Image = DIGI_NUM(Convert.ToInt32(TimeCountDown(3)) - 48)
            PicDIGI_4.Image = DIGI_NUM(Convert.ToInt32(TimeCountDown(4)) - 48)

            If ACTCount > ACTLimit Then
                RestartApp2(SecureDesktop, False)
            End If
        Else
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
                If NotLocked Then
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
                If NotLocked Then
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

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)

        MyBase.WndProc(m)

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
        My.Computer.Keyboard.SendKeys(FixSendKey(TheTextBox.Text))

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

        My.Computer.Keyboard.SendKeys("^{v}")
        My.Computer.Clipboard.Clear()

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

        My.Computer.Keyboard.SendKeys("^{v}{home}")
        My.Computer.Clipboard.Clear()

        My.Computer.Keyboard.SendKeys(SB1.ToString)

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

        Dim kc As KeysConverter = New KeysConverter()
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
            If CheckBool = False Then MSGBOXNEW(Replace(TextStrs(81), "$$$", "1"), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
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
            If CheckBool = False Then MSGBOXNEW(Replace(TextStrs(81), "$$$", "2"), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
        End If

    End Sub

    '============================ Button Works===================================

    Private Sub ButtonCopyAccount_Click(sender As Object, e As EventArgs) Handles ButtonCopyAccount.Click

        If TextBoxNameAddr.Text = "" Then Exit Sub

        Dim CPWorker As New ClipboardHelper2

        If CPWorker.SetClipboardText(TextBoxNameAddr.Text) Then
            LastAct(TextStrs(25))
        Else
            LastAct(TextStrs(102))
            MSGBOXNEW(TextStrs(102) + vbCrLf + vbCrLf + TextStrs(103), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
        End If

    End Sub

    Private Sub ButtonCopyReg_Click(sender As Object, e As EventArgs) Handles ButtonCopyReg.Click

        If TextBoxRegMailPhone.Text = "" Then Exit Sub

        Dim CPWorker As New ClipboardHelper2

        If CPWorker.SetClipboardText(TextBoxRegMailPhone.Text) Then
            LastAct(TextStrs(25))
        Else
            LastAct(TextStrs(102))
            MSGBOXNEW(TextStrs(102) + vbCrLf + vbCrLf + TextStrs(103), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
        End If

    End Sub

    Private Sub ButtonViewNote_Click(sender As Object, e As EventArgs) Handles ButtonViewNote.Click

        If TextBoxNote2Hid.UseSystemPasswordChar Then
            ButtonViewNote.Image = b_view_Son_on
        Else
            ButtonViewNote.Image = b_view_on
        End If

        TextBoxNote2Hid.UseSystemPasswordChar = Not TextBoxNote2Hid.UseSystemPasswordChar

    End Sub

    Private Sub ButtonLaunch_Click(sender As Object, e As EventArgs) Handles ButtonLaunch.Click

        If TextBoxURL.Text = "" Then Exit Sub

        Try
            Process.Start(TextBoxURL.Text)
            LastAct(TextStrs(49))
        Catch ex As Exception
            MSGBOXNEW(TextStrs(26), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
        End Try

    End Sub

    Private Sub PictureBoxPwd_Click(sender As Object, e As EventArgs) Handles PictureBoxPwd.Click

        GetPass()

        If SecureDesktop Then
            NowPassStatue = LogInFormWork(TextStrs(72), Nothing, CurrentAccountPass, Nothing, 2, NowPassStatue, Nothing, PictureGray, TheSalt)
        Else
            NowPassStatue = LogInFormWork(TextStrs(72), Nothing, CurrentAccountPass, Nothing, 2, NowPassStatue, Nothing, PictureGray, TheSalt, Me)
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

        GetPass()

        If NowPassStatue > 1 Then
            StartClipboardMonitorBlocker()
            Dim SDobj As New SmallDecoderPass
            SDobj.Workmode = 1
            SDobj.InputByte = CurrentAccountPass
            SDobj.ShowDialog()
            SDobj.Dispose()
            FullGC()
            StopClipboardMonitorBlocker()
            LastAct(TextStrs(25))
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
            PictureBoxPwd.Image = FPWDS.PictureBoxPwd.Image
            My.Application.DoEvents()
            FPWDS.Dispose()
            FullGC()
        End If
    End Sub

    Private Sub PictureBoxPwdVi_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBoxPwdVi.MouseUp
        If NotLocked Then
            PictureBoxPwd.Image = My.Resources.Resource1.TOPSEC
        Else
            PictureBoxPwd.Image = PwdGrayImage
        End If
        PictureBoxPwdVi.Image = b_view_small_on
    End Sub

    Private Sub PictureBoxPwdVi_MouseLeave(sender As Object, e As EventArgs) Handles PictureBoxPwdVi.MouseLeave
        If NotLocked Then
            PictureBoxPwd.Image = My.Resources.Resource1.TOPSEC
        Else
            PictureBoxPwd.Image = PwdGrayImage
        End If
        PictureBoxPwdVi.Image = b_view_small_on
    End Sub


    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click

        If TextBoxTitle.Text = "" Then
            MSGBOXNEW(TextStrs(23), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
            Exit Sub
        End If

        '========== Crypto Address Detect
        If (TextBoxNameAddr.Text.Length > 20) And VG_CCC_Done Then
            Dim CCC As New CryptoCurrencyChk
            CCC.DetectCurrency(TextBoxNameAddr.Text)
            If CCC.DetectState = 1 Then
                Dim WorkStr As String = (TextStrs(87) + TextStrs(88) + TextStrs(89)).Replace("$$$", CoinList(CCC.DetectType))
                If MSGBOXNEW(WorkStr, MsgBoxStyle.OkCancel, TextStrs(9), Me, PictureGray) = MsgBoxResult.Cancel Then
                    Exit Sub
                End If
            End If
            CCC.Dispose()
        End If

        If NowProcFile = "" Then
            If MSGBOXNEW(TextStrs(51).Replace("$$$", TextBoxTitle.Text), MsgBoxStyle.OkCancel,
                         TextStrs(9), Me, PictureGray) = MsgBoxResult.Ok Then

                If Not WriteFile(Get_New_ACC_Filename(DirName), AES_KEY_Protected, DERIVED_IDT_Protected) Then
                    GetList()
                    LastAct(Replace(TextStrs(20), "$$$", TextBoxTitle.Text))
                    Exe_Fill_Trash()
                    FullGC()
                    Go_ListBoxIdx(ListBox1.Items.Count - 1)
                Else
                    LastAct(TextStrs(67))
                    MSGBOXNEW(TextStrs(66), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
                End If

            End If

        Else

            Dim OLDidx As Integer = ListBox1.SelectedIndex

            If MSGBOXNEW(TextStrs(13).Replace("$$$", TextBoxTitle.Text), MsgBoxStyle.OkCancel, TextStrs(9), Me, PictureGray) = MsgBoxResult.Ok Then

                If Not WriteFile(NowProcFile, AES_KEY_Protected, DERIVED_IDT_Protected) Then
                    If OLDidx >= 1 Then
                        GetList()
                        Go_ListBoxIdx(OLDidx)
                    End If
                    LastAct(Replace(TextStrs(21), "$$$", TextBoxTitle.Text))
                    Exe_Fill_Trash()
                    FullGC()
                Else
                    LastAct(TextStrs(67))
                    MSGBOXNEW(TextStrs(66), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
                End If

            End If

        End If

    End Sub

    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click

        Dim NowDeleteAccName As String = TextBoxTitle.Text

        Select Case Delete_ACC_File(NowProcFile, TextStrs(8), False)
            Case 0
                LastAct(Replace(TextStrs(40), "$$$", NowDeleteAccName))
            Case 1
            Case 2
                LastAct(TextStrs(67))
                MSGBOXNEW(TextStrs(48), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
        End Select

    End Sub

    Private Sub ButtonGoUP_Click(sender As Object, e As EventArgs) Handles ButtonGoUP.Click

        Dim UpperFilename As String
        Dim NowSelect As Integer = ListBox1.SelectedIndex
        Dim NowWorkName As String = ListBox1.Items(NowSelect)

        If NowSelect >= 2 Then

            UpperFilename = FilesList1(NowSelect - 1).FileName
            My.Computer.FileSystem.MoveFile(NowProcFile, NowProcFile + ".TMP")
            My.Computer.FileSystem.MoveFile(UpperFilename, NowProcFile)
            My.Computer.FileSystem.MoveFile(NowProcFile + ".TMP", UpperFilename)

            GetList()
            Go_ListBoxIdx(NowSelect - 1)
            LastAct(Replace(TextStrs(28), "$$$", NowWorkName))
        Else
            Exit Sub
        End If

    End Sub

    Private Sub ButtonGoDown_Click(sender As Object, e As EventArgs) Handles ButtonGoDown.Click

        Dim LowerFilename As String
        Dim NowSelect As Integer = ListBox1.SelectedIndex
        Dim NowWorkName As String = ListBox1.Items(NowSelect)

        If NowSelect < ListBox1.Items.Count - 1 Then

            LowerFilename = FilesList1(NowSelect + 1).FileName
            My.Computer.FileSystem.MoveFile(NowProcFile, NowProcFile + ".TMP")
            My.Computer.FileSystem.MoveFile(LowerFilename, NowProcFile)
            My.Computer.FileSystem.MoveFile(NowProcFile + ".TMP", LowerFilename)

            GetList()
            Go_ListBoxIdx(NowSelect + 1)
            LastAct(Replace(TextStrs(29), "$$$", NowWorkName))

        Else
            Exit Sub
        End If

    End Sub

    Private Sub ButtonTransCatalog_Click(sender As Object, e As EventArgs) Handles ButtonTransCatalog.Click

        MSGBOXNEW(TextStrs(11).Replace("$$$", TextBoxTitle.Text) + vbCrLf + vbCrLf + TextStrs(12),
                     MsgBoxStyle.OkOnly, TextStrs(9), Me, PictureGray)

        Dim NowTransAccName As String = TextBoxTitle.Text

        Dim ErrFlag As Boolean = False
        Dim DirNameCurrent As String = ""
        Dim AESKeyCurrent(0) As Byte
        Dim DERIVED_IDT_Current(0) As Byte
        Dim NeedRefresh As Boolean
        Dim NowSelect As Integer = ListBox1.SelectedIndex

        Dim TmpDR As DialogResult

        If SecureDesktop Then
            TmpDR = LogInFormWork(TextStrs(69), DirNameCurrent, AESKeyCurrent, DERIVED_IDT_Current, 1, Nothing, Nothing, PictureGray, TheSalt)
        Else
            TmpDR = LogInFormWork(TextStrs(69), DirNameCurrent, AESKeyCurrent, DERIVED_IDT_Current, 1, Nothing, Nothing, PictureGray, TheSalt, Me)
        End If

        If TmpDR = DialogResult.OK Then

            Dim Filename2 As String = Get_New_ACC_Filename(DirNameCurrent)

            If DirName = DirNameCurrent Then
                If MSGBOXNEW(TextStrs(39), MsgBoxStyle.OkCancel, TextStrs(5), Me, PictureGray) = DialogResult.Cancel Then
                    FullGC()
                    Exit Sub
                Else
                    NeedRefresh = True
                End If
            End If

            GetPass()
            NowPassStatue = 3
            VG_Data_Done = True
            VG_Title_Done = True

            ErrFlag = WriteFile(Filename2, AESKeyCurrent, DERIVED_IDT_Current)
            If Not ErrFlag Then
                Select Case Delete_ACC_File(NowProcFile, TextStrs(7) + vbCrLf + vbCrLf + TextStrs(8), False)
                    Case 0
                    Case 1
                        If NeedRefresh Then
                            GetList()
                            Go_ListBoxIdx(NowSelect)
                        End If
                    Case 2
                        ErrFlag = True
                End Select
            End If

            If Not ErrFlag Then
                LastAct(Replace(TextStrs(41), "$$$", NowTransAccName))
            Else
                LastAct(TextStrs(67))
                MSGBOXNEW(TextStrs(66), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
            End If

        End If

        FullGC()

    End Sub

    Private Sub ButtonFileInfo_Click(sender As Object, e As EventArgs) Handles ButtonFileInfo.Click

        If NowProcFile = "" Then Exit Sub

        Try

            Dim FileNameStr As String = TextStrs(38) + vbCrLf + My.Computer.FileSystem.GetName(NowProcFile)

            Dim DateOfBuild As String = My.Computer.FileSystem.GetFileInfo(NowProcFile).CreationTime.ToShortDateString +
                " " + My.Computer.FileSystem.GetFileInfo(NowProcFile).CreationTime.ToShortTimeString

            Dim DateOfLW As String = My.Computer.FileSystem.GetFileInfo(NowProcFile).LastWriteTime.ToShortDateString +
                " " + My.Computer.FileSystem.GetFileInfo(NowProcFile).LastWriteTime.ToShortTimeString

            MSGBOXNEW(FileNameStr + vbCrLf + vbCrLf + TextStrs(17) + vbCrLf + DateOfBuild + vbCrLf + vbCrLf + TextStrs(18) + vbCrLf +
                      DateOfLW + vbCrLf + vbCrLf + TextStrs(80) + vbCrLf + DirName, MsgBoxStyle.OkOnly, TextStrs(16), Me, PictureGray)

        Catch ex As Exception

        End Try

    End Sub

    '======================================= Base Main Window Operate ======================

    Private lastLocation As Point
    Private isMouseDown As Boolean = False

    Private Sub PictureBox2_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBox2.MouseDown
        ' 紀錄滑鼠按下的位置
        isMouseDown = True
        lastLocation = e.Location
    End Sub

    Private Sub PictureBox2_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBox2.MouseMove
        ' 當滑鼠左鍵按下時，設定窗體的位置
        If isMouseDown Then
            Me.Location = New Point(Me.Location.X + (e.X - lastLocation.X), Me.Location.Y + (e.Y - lastLocation.Y))
        End If
    End Sub

    Private Sub PictureBox2_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBox2.MouseUp
        ' 當滑鼠左鍵釋放時，重設 isMouseDown 變數
        isMouseDown = False
    End Sub

    Private Sub ButtonExit_Click(sender As Object, e As EventArgs) Handles ButtonFin.Click
        End_Program()
    End Sub

    Private Sub ButtonHelp_Click(sender As Object, e As EventArgs) Handles ButtonHelp.Click
        Try
            Process.Start(MainWebURL)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ButtonRestart_Click_1(sender As Object, e As EventArgs) Handles ButtonRestart.Click
        RestartApp2(SecureDesktop, False)
    End Sub

    Private Sub PictureWinMin_Click(sender As Object, e As EventArgs) Handles PictureWinMin.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
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
        LB_Ration = ListBox1.ClientRectangle.Height / ListBox1.ItemHeight
    End Sub

    Private Sub LSCBU_MouseDown(sender As Object, e As MouseEventArgs) Handles _
        LSCBU.MouseDown, LSCBD.MouseDown, LSCBBACK.MouseDown

        If sender.Name = "LSCBU" Then
            NowUPorDW = 0
            ListBox1.TopIndex -= 1
            GoCorrectPos()
        ElseIf sender.Name = "LSCBD" Then
            NowUPorDW = 1
            ListBox1.TopIndex += 1
            GoCorrectPos()
        Else
            Dim WhereIsY As Integer = e.Y + LSCBBACK.Top
            If WhereIsY < LSCBBAR.Top Then
                NowUPorDW = 2
                If ListBox1.TopIndex - LB_Ration < 0 Then
                    ListBox1.TopIndex = 0
                Else
                    ListBox1.TopIndex -= LB_Ration
                End If
                GoCorrectPos()
            Else
                NowUPorDW = 3
                ListBox1.TopIndex += LB_Ration
                GoCorrectPos()
            End If

        End If

        LSCB_UPDW.Enabled = True
    End Sub

    Private Sub LSCBWORK(ByVal sender As Object, ByVal e As EventArgs) Handles LSCB_UPDW.Tick
        Select Case NowUPorDW
            Case 0
                ListBox1.TopIndex -= 1
            Case 1
                ListBox1.TopIndex += 1
            Case 2
                If ListBox1.TopIndex - LB_Ration < 0 Then
                    ListBox1.TopIndex = 0
                Else
                    ListBox1.TopIndex -= LB_Ration
                End If
            Case 3
                ListBox1.TopIndex += LB_Ration
        End Select
        GoCorrectPos()
    End Sub

    Private Sub LSCBU_MouseUp(sender As Object, e As MouseEventArgs) Handles LSCBU.MouseUp, LSCBD.MouseUp, LSCBBACK.MouseUp
        LSCB_UPDW.Enabled = False
    End Sub

    Private Sub LSCB_MSC_WORK(ByVal sender As Object, ByVal e As EventArgs) Handles LSCB_MSC.Tick
        GoCorrectPos()
        LSCB_MSC.Enabled = False
    End Sub

    Private Sub GoCorrectPos()

        If LB_Range_Scale > 0 Then
            Dim TmpIdx As Double = CDbl(ListBox1.TopIndex) / LB_Range_Scale
            TmpIdx = TmpIdx * CDbl(DwY - UpY)
            LSCBBAR.Top = CInt(TmpIdx + UpY)
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
                ListBox1.TopIndex = CInt((CDbl(sender.Top - UpY) / CDbl(DwY - UpY)) * LB_Range_Scale)
                sender.Top = TmpY
            ElseIf TmpY < UpY Then
                ListBox1.TopIndex = 0
                sender.Top = UpY
            End If

        End If
    End Sub

    Private Sub LSCBBAR_MouseUp(sender As Object, e As MouseEventArgs) Handles LSCBBAR.MouseUp
        BarIsHolding = False
        Cursor = Cursors.Default ' 將游標形狀恢復為預設值
    End Sub

    Private Sub ListBox1_MouseWheel(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseWheel
        LSCB_MSC.Enabled = True
    End Sub

    Private Sub LSCB_MouseWheel(sender As Object, e As MouseEventArgs) Handles _
            LSCBBAR.MouseWheel, LSCBBACK.MouseWheel, LSCBD.MouseWheel, LSCBU.MouseWheel
        If e.Delta > 0 Then
            If ListBox1.TopIndex - 3 < 0 Then
                ListBox1.TopIndex = 0
            Else
                ListBox1.TopIndex -= 3
            End If
        Else
            ListBox1.TopIndex += 3
        End If
        GoCorrectPos()
    End Sub

    Private Sub Go_ListBoxIdx(Gowhere As Integer)
        ListBox1.SelectedIndex = Gowhere
        GoCorrectPos()
    End Sub

    '====================== Last action

    Private Sub LastAct(Str As String)

        Label_Act_Last.Text = DateTime.Now.ToString("HH:mm:ss")
        Label_Act_Work.Text = Str

        Dim ActWork As New Bitmap(Label_Act_Work.Width, Label_Act_Work.Height)
        Dim NewRec As Rectangle
        NewRec.Width = Label_Act_Work.Width
        NewRec.Height = Label_Act_Work.Height
        Label_Act_Work.DrawToBitmap(ActWork, NewRec)
        Label_Act_Show.Image = ResizeBitmap(ActWork, 0.4166, 0.5)

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

    Dim b_Logout_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_LOGOUT)
    Dim b_Final_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_Final)
    Dim b_HELP_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_HELP)
    Dim b_Launch_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_Launch)
    Dim b_COPY_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_COPY)
    Dim b_copy_small_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_copy_small)

    Dim b_view_small_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_view_small)
    Dim b_view_small_Son As Bitmap = Make_Button_HueChange(My.Resources.Resource1.button_view_small, 315) 'Not actully use
    Dim b_view_small_Son_on As Bitmap = Make_Button_brighter(b_view_small_Son, 1.1)

    Dim b_view_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_view)
    Dim b_view_Son As Bitmap = Make_Button_HueChange(My.Resources.Resource1.button_view, 315)
    Dim b_view_Son_on As Bitmap = Make_Button_brighter(b_view_Son, 1.1)

    Dim b_HKO_Soff_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_HKO_small_off, 1.1)
    Dim b_HKO_Son_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_HKO_small_on, 1.1)

    Dim b_PictureBoxCATMAN_on As Bitmap = Make_Button_brighter(Make_Button_HueChange(b_PictureBoxCATMAN, 15), 1.1)


    Private Sub Mouse_Enter(sender As Object, e As EventArgs) Handles _
        ButtonRestart.MouseEnter, ButtonFin.MouseEnter, ButtonHelp.MouseEnter, ButtonLaunch.MouseEnter,
        ButtonCopyAccount.MouseEnter, PictureBoxPwdVi.MouseEnter, PictureBoxPwdCPY.MouseEnter,
        ButtonCopyReg.MouseEnter, ButtonViewNote.MouseEnter, PictureBoxCATMAN.MouseEnter, ButtonHotkeyMode.MouseEnter

        Select Case sender.Name
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
                If TextBoxNote2Hid.UseSystemPasswordChar Then
                    ButtonViewNote.Image = b_view_on
                Else
                    ButtonViewNote.Image = b_view_Son_on
                End If
            Case "PictureBoxCATMAN"
                PictureBoxCATMAN.Image = b_PictureBoxCATMAN_on
            Case "ButtonHotkeyMode"

                If TextBox_BHKMHelper.Text = "0" Then ButtonHotkeyMode.Image = b_HKO_Soff_on
                If TextBox_BHKMHelper.Text = "1" Then ButtonHotkeyMode.Image = b_HKO_Son_on

        End Select
    End Sub

    Private Sub Mouse_Leave(sender As Object, e As EventArgs) Handles _
        ButtonRestart.MouseLeave, ButtonFin.MouseLeave, ButtonHelp.MouseLeave, ButtonLaunch.MouseLeave,
        ButtonCopyAccount.MouseLeave, PictureBoxPwdVi.MouseLeave, PictureBoxPwdCPY.MouseLeave,
        ButtonCopyReg.MouseLeave, ButtonViewNote.MouseLeave, PictureBoxCATMAN.MouseLeave, ButtonHotkeyMode.MouseLeave

        Select Case sender.Name
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
                If TextBoxNote2Hid.UseSystemPasswordChar Then
                    ButtonViewNote.Image = b_view
                Else
                    ButtonViewNote.Image = b_view_Son
                End If
            Case "PictureBoxCATMAN"
                PictureBoxCATMAN.Image = b_PictureBoxCATMAN
            Case "ButtonHotkeyMode"
                If TextBox_BHKMHelper.Text = "0" Then ButtonHotkeyMode.Image = b_HKO_off
                If TextBox_BHKMHelper.Text = "1" Then ButtonHotkeyMode.Image = b_HKO_on

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
            End Select
        End If

    End Sub

    Private Sub ButtonHotkeyMode_Click(sender As Object, e As EventArgs) Handles ButtonHotkeyMode.Click

        If TextBox_BHKMHelper.Text = "0" Then
            ButtonHotkeyMode.Image = b_HKO_Son_on
            TextBox_BHKMHelper.Text = "1"
        Else
            ButtonHotkeyMode.Image = b_HKO_Soff_on
            TextBox_BHKMHelper.Text = "0"
        End If

    End Sub


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



