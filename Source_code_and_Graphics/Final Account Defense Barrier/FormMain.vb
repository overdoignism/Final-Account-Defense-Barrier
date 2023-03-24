'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

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

    Private VG_Data_Done As Boolean = False
    Private VG_Title_Done As Boolean = False
    Private VG_CCC_Done As Boolean = False
    Private FillTrash() As String

    Public SecureDesktop As Boolean = False

    Dim LBTN_Save_En As New Bitmap(My.Resources.Resource1.button_L_save)
    Dim LBTN_Del_En As New Bitmap(My.Resources.Resource1.button_L_delete)
    Dim LBTN_MoveU_En As New Bitmap(My.Resources.Resource1.button_L_moveUP)
    Dim LBTN_MoveD_En As New Bitmap(My.Resources.Resource1.button_L_moveDWN)
    Dim LBTN_TKey_En As New Bitmap(My.Resources.Resource1.button_L_transKEY)
    Dim LBTN_FInfo_En As New Bitmap(My.Resources.Resource1.button_L_fileInfo)
    Dim LBTN_Save_Di As New Bitmap(My.Resources.Resource1.button_L_save_DI)
    Dim LBTN_Del_Di As New Bitmap(My.Resources.Resource1.button_L_delete_DI)
    Dim LBTN_MoveU_Di As New Bitmap(My.Resources.Resource1.button_L_moveUP_DI)
    Dim LBTN_MoveD_Di As New Bitmap(My.Resources.Resource1.button_L_moveDWN_DI)
    Dim LBTN_TKey_Di As New Bitmap(My.Resources.Resource1.button_L_transKEY_DI)
    Dim LBTN_FInfo_Di As New Bitmap(My.Resources.Resource1.button_L_fileInfo_DI)

    Public DIGI_NUM() As Bitmap = {My.Resources.Resource1.DIGI_Y_0, My.Resources.Resource1.DIGI_Y_1,
    My.Resources.Resource1.DIGI_Y_2, My.Resources.Resource1.DIGI_Y_3, My.Resources.Resource1.DIGI_Y_4,
    My.Resources.Resource1.DIGI_Y_5, My.Resources.Resource1.DIGI_Y_6, My.Resources.Resource1.DIGI_Y_7,
    My.Resources.Resource1.DIGI_Y_8, My.Resources.Resource1.DIGI_Y_9}

    Public DIGI_NUM_N As New Bitmap(My.Resources.Resource1.DIGI_Y__)

    Public WithEvents AutoCloseTimer As New Windows.Forms.Timer

    '====Listbox scroll bar
    Dim WithEvents LSCB_UPDW As New Windows.Forms.Timer
    Dim WithEvents LSCB_MSC As New Windows.Forms.Timer
    Dim NowUPorDW As Integer
    Dim LB_Ration As Double
    Dim LB_Range_Scale As Double
    Dim UpY As Integer
    Dim DwY As Integer
    Dim BarIsHolding As Boolean

    Dim version As Version = Reflection.Assembly.GetEntryAssembly().GetName().Version
    Dim versionNumber As String = version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision

    '0=New Account no edit, CurrentAccountPass = "" in init
    '1=Old File Read, not decrypt
    '2=Old File Read, decrypted, CurrentAccountPass usable, Not edit
    '3=Edited
    Dim discontinue As Boolean

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            If My.Computer.FileSystem.FileExists("Lang_MOD.TXT") Then
                If My.Computer.FileSystem.GetFileInfo("Lang_MOD.TXT").Length <= 51200 Then
                    Dim IOreader1 As IO.StreamReader = My.Computer.FileSystem.OpenTextFileReader("Lang_MOD.TXT")

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

        Me.CenterToScreen()

        Dim osVersion As OperatingSystem = Environment.OSVersion
        OSver = (osVersion.Version.Major * 10) + osVersion.Version.Minor

        If OSver >= 62 Then
            SPMP_Set()
        Else
            MSGBOXNEW(TextStrs(63), MsgBoxStyle.Critical, TextStrs(9), Me, PictureGray)
        End If

        SeLock()
        ProcessPriorityUp()

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

        Load_BIP39_Word()

        Dim ToolTip1 As System.Windows.Forms.ToolTip = New System.Windows.Forms.ToolTip()

        'Me.SetStyle(ControlStyles.UserPaint, True)
        'Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        'Me.SetStyle(ControlStyles.DoubleBuffer, True)
        'Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)

        '============= Load text
        ToolTip1.SetToolTip(TextBoxTitle, TextStrs(31))
        ToolTip1.SetToolTip(TextBoxURL, TextStrs(32))
        ToolTip1.SetToolTip(TextBoxNameAddr, TextStrs(33))
        ToolTip1.SetToolTip(TextBoxRegMailPhone, TextStrs(36))
        ToolTip1.SetToolTip(TextBoxNote1, TextStrs(50))
        ToolTip1.SetToolTip(TextBoxNote2Hid, TextStrs(37))

        Label_Act_Msg.Text = TextStrs(26)
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
        UpY = LSCBU.Top + LSCBU.Height
        DwY = LSCBD.Top - LSCBBAR.Height + 1
        LSCB_UPDW.Interval = 150
        LSCB_UPDW.Enabled = False
        LSCB_MSC.Interval = 100
        LSCB_MSC.Enabled = False
        LB_Ration = ListBox1.ClientRectangle.Height / ListBox1.ItemHeight

        '================================Login stage=====
        discontinue = False

        Dim TmpDR As DialogResult

        If SecureDesktop Then
            TmpDR = LogInFormWork(TextStrs(59), DirName, AES_KEY_Protected, DERIVED_IDT_Protected, 0, Nothing, NoNotice, PictureGray, TheSalt)
        Else
            TmpDR = LogInFormWork(TextStrs(59), DirName, AES_KEY_Protected, DERIVED_IDT_Protected, 0, Nothing, NoNotice, PictureGray, TheSalt, Me)
        End If

        If TmpDR <> DialogResult.OK Then End_Program()

        FullGC()
        '=============================================

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
        UnregisterHotKey(Me.Handle, HotKeyID1)
        UnregisterHotKey(Me.Handle, HotKeyID2)

        ReDim CAT_setting_Str(6)
        CAT_setting_Str(5) = "1"
        Read_CatDatas()
        '=================================

        Label_Act_Msg.Text = TextStrs(62)
        LabelBy.Text = "▎ By overdoingism Lab."
        LABVER.Text = "▎ " + TextStrs(42) + " " + versionNumber


    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

        Read_and_decrypt(FilesList1(ListBox1.SelectedIndex))
        GoCorrectPos()
        FullGC()

    End Sub

    Private Sub Read_and_decrypt(ByRef File_To_Decrypt As FileLists)

        ReadingWorking = True
        TextBoxTitle.Text = ""
        TextBoxURL.Text = ""
        TextBoxNameAddr.Text = ""
        TextBoxRegMailPhone.Text = ""
        TextBoxNote1.Text = ""
        TextBoxNote2Hid.Text = ""

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
        VG_CCC_Done = False

        TextBoxNote2Hid.UseSystemPasswordChar = True
        ButtonViewNote.Image = My.Resources.Resource1.button_view

        Dim AES_IV_1(15) As Byte
        Dim AES_IV_2(15) As Byte
        Dim AES_IV_USE(15) As Byte
        Dim ErrFlag As Boolean = False

        If ListBox1.SelectedIndex = 0 Then

            NowProcFile = ""
            CurrentAccountPass = Security.Cryptography.ProtectedData.
                Protect(Encoding.Unicode.GetBytes(""), Nothing, DataProtectionScope.CurrentUser)
            NowPassStatue = 0
            VG_Data_Done = True
            VG_Title_Done = True
            ReadingWorking = False
            Label_Act_Msg.Text = TextStrs(27)
            GoCorrectPos()
            Exit Sub

        Else

            ButtonDelete.Enabled = True
            ButtonDelete.Image = LBTN_Del_En

            NowProcFile = File_To_Decrypt.FileName
            If My.Computer.FileSystem.GetFileInfo(NowProcFile).Length > 1048576 Then File_To_Decrypt.FileIsBad = True

            If File_To_Decrypt.FileIsBad Then
                ErrFlag = True
            Else

                Try

                    '==================== Stage 1 init
                    Dim TheEncLib As New Encode_Libs
                    Dim IOreader1 As IO.StreamReader

                    IOreader1 = My.Computer.FileSystem.OpenTextFileReader(NowProcFile)
                    TextBoxTitle.Text = File_To_Decrypt.ShowName

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
                            TextBoxNote1.Text = WorkStr2(4)
                            TextBoxRegMailPhone.Text = WorkStr2(5)
                            TextBoxNote2Hid.Text = WorkStr2(6)
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
            File_To_Decrypt.FileIsBad = True
            Label_Act_Msg.Text = TextStrs(67)
            MSGBOXNEW(TextStrs(65), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
        Else

            ButtonGoUP.Enabled = True
            ButtonGoUP.Image = LBTN_MoveU_En
            ButtonGoDown.Enabled = True
            ButtonGoDown.Image = LBTN_MoveD_En
            ButtonTransCatalog.Enabled = True
            ButtonTransCatalog.Image = LBTN_TKey_En
            ButtonFileInfo.Enabled = True
            ButtonFileInfo.Image = LBTN_FInfo_En
            ButtonCopyReg.Enabled = True

            VG_Data_Done = False
            VG_Title_Done = False
            Label_Act_Msg.Text = Replace(TextStrs(68), "$$$", TextBoxTitle.Text)
            FullGC()
        End If

        ReadingWorking = False

    End Sub

    Private Function WriteFile(Filename1 As String, ByRef PKey() As Byte, ByRef DICurrent() As Byte) As Boolean

        WriteFile = False

        Dim confuse01 As String
        Dim confuse02 As String
        Dim AES_IV_Use(15) As Byte
        Dim TheEncLib As New Encode_Libs

        Try
            '=========================== segment 1 account title

            If (VG_Title_Done Or VG_Data_Done) Or (NowPassStatue = 3) Then

                Dim TMPstr0 As String

                If VG_Title_Done Then

                    TMPstr0 = TextBoxTitle.Text + vbCr + Random_Strs(30, 50, 1) + vbCr + INDTstr
                    TheEncLib.Get_The_IV_ByRND(AES_IV_Use)
                    TMPstr0 = TheEncLib.AES_Encrypt_String_Return_String(TMPstr0, PKey, AES_IV_Use)
                    TMPstr0 = TMPstr0 + "," + TheEncLib.ByteIn_StringOut(AES_IV_Use)

                Else
                    TMPstr0 = NowTitleStringCpt
                End If

                '=========================== segment 2 account data

                Dim TMPstr1 As String

                If VG_Data_Done Then

                    TMPstr1 = INDTstr + vbCr + TextBoxURL.Text + vbCr + TextBoxNameAddr.Text +
                    vbCr + Random_Strs(30, 50, 1) + vbCr + TextBoxNote1.Text + vbCr + TextBoxRegMailPhone.Text +
                    vbCr + TextBoxNote2Hid.Text + vbCr

                    TheEncLib.Get_The_IV_ByRND(AES_IV_Use)

                    TMPstr1 = TheEncLib.AES_Encrypt_String_Return_String(TMPstr1, PKey, AES_IV_Use)
                    TMPstr1 = TMPstr1 + "," + TheEncLib.ByteIn_StringOut(AES_IV_Use)

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
                    confuse01 = Random_Strs(D01_IDX, D01_IDX, 1)

                    Dim D02_IDX As Long = Get_RangeRnd_ByRNG(50, 100) - (PwdLength * Get_RangeRnd_ByRNG(2, 5))
                    If D02_IDX < 30 Then D02_IDX = Get_RangeRnd_ByRNG(50, 100)
                    confuse02 = Random_Strs(D02_IDX, D02_IDX, 1)

                    TheEncLib.Get_The_IV_ByRND(AES_IV_Use)

                    TMPstr2 = TheEncLib.AES_Encrypt_String_Return_String(confuse01 + vbCr + SD.TextBoxPWDStr.Text + vbCr + confuse02, PKey, AES_IV_Use)

                    TMPstr2 = TMPstr2 + "," + TheEncLib.ByteIn_StringOut(AES_IV_Use)

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

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        End_Program()
    End Sub

    Private Sub ButtonCopyAccount_Click(sender As Object, e As EventArgs) Handles ButtonCopyAccount.Click

        If TextBoxNameAddr.Text = "" Then Exit Sub
        My.Computer.Clipboard.SetText(TextBoxNameAddr.Text)
        Label_Act_Msg.Text = TextStrs(25)

    End Sub

    Private Sub ButtonCopyReg_Click(sender As Object, e As EventArgs) Handles ButtonCopyReg.Click

        If TextBoxRegMailPhone.Text = "" Then Exit Sub
        Label_Act_Msg.Text = TextStrs(25)
        My.Computer.Clipboard.SetText(TextBoxRegMailPhone.Text)

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBoxNameAddr.TextChanged

        If Not ReadingWorking Then
            VG_CCC_Done = True
            If InStr(TextBoxNameAddr.Text, "@") > 0 And InStr(TextBoxNameAddr.Text, ".") > 0 Then
                TextBoxRegMailPhone.Text = TextBoxNameAddr.Text
            End If
        End If

    End Sub

    Private Function Delete_ACC_File(What_file As String, Ask_String As String, continuous_mode As Boolean) As Integer

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

    Private Sub TransFullCat()

        Dim ErrFlag As Boolean = False
        Dim DirNameCurrent As String = ""
        Dim AESKeyCurrent(0) As Byte
        Dim DERIVED_IDT_Current(0) As Byte

        Dim TmpDR As DialogResult

        If SecureDesktop Then
            TmpDR = LogInFormWork(TextStrs(69), DirNameCurrent, AESKeyCurrent, DERIVED_IDT_Current, 1, Nothing, Nothing, PictureGray, TheSalt)
        Else
            TmpDR = LogInFormWork(TextStrs(69), DirNameCurrent, AESKeyCurrent, DERIVED_IDT_Current, 1, Nothing, Nothing, PictureGray, TheSalt, Me)
        End If

        If TmpDR = DialogResult.OK Then

            If DirName <> DirNameCurrent Then

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
                    Label_Act_Msg.Text = TextStrs(43)

                    If MSGBOXNEW(TextStrs(43) + vbCrLf + vbCrLf + TextStrs(10).Replace("$$$", LabelCatalog.Text),
                     MsgBoxStyle.OkCancel, TextStrs(9), Me, PictureGray) <> MsgBoxResult.Cancel Then
                        Full_Catalog_Delete()
                        Exe_Fill_Trash()
                    End If

                Else
                    Label_Act_Msg.Text = TextStrs(67)
                    MSGBOXNEW(TextStrs(66), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
                End If

            Else
                MSGBOXNEW(TextStrs(39), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
            End If

        End If

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

                Label_Act_Msg.Text = TextStrs(67)
                MSGBOXNEW(TextStrs(64), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)

            End If

            RestartApp2(SecureDesktop, False)

        End If

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
            TheEncLib.Get_The_IV_ByRND(AES_IV_Use)
            TmpWork = TheEncLib.AES_Encrypt_String_Return_String(TmpWork, AES_KEY_Protected, AES_IV_Use) +
                "," + TheEncLib.ByteIn_StringOut(AES_IV_Use)

            IOWer1.WriteLine(TmpWork)
            IOWer1.Close()
            LabelCatalog.Text = FormConfig.TextBoxCatalog.Text
            Label_Act_Msg.Text = TextStrs(30)
        Catch ex As Exception
            MsgBox(ex.Message, 0, TextStrs(5))
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

    Private Sub AutoCountDownClose(CountdownSec As Integer)

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

    Private Sub ButtonViewNote_Click(sender As Object, e As EventArgs) Handles ButtonViewNote.Click

        If TextBoxNote2Hid.UseSystemPasswordChar Then
            ButtonViewNote.Image = My.Resources.Resource1.button_view_Son_on
        Else
            ButtonViewNote.Image = My.Resources.Resource1.button_view_on
        End If

        TextBoxNote2Hid.UseSystemPasswordChar = Not TextBoxNote2Hid.UseSystemPasswordChar

    End Sub

    Private Sub TextBoxChanged(sender As Object, e As EventArgs) Handles TextBoxURL.TextChanged,
            TextBoxNameAddr.TextChanged, TextBoxRegMailPhone.TextChanged, TextBoxNote1.TextChanged, TextBoxNote2Hid.TextChanged

        If Not ReadingWorking Then
            VG_Data_Done = True
            If TextBoxTitle.Text <> "" Then
                ButtonSave.Enabled = True
                ButtonSave.Image = LBTN_Save_En
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
                ButtonSave.Enabled = False
                ButtonSave.Image = LBTN_Save_Di
            End If
        End If

    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)

        MyBase.WndProc(m)

        Select Case m.Msg

            Case WM_HOTKEY 'For Hotkey

                If HotKeyIsWorking Then Exit Select

                Dim id As IntPtr = m.WParam

                Select Case (id.ToString)

                    Case HotKeyID1

                        Select Case Val(CAT_setting_Str(3))
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

                            Select Case Val(CAT_setting_Str(6))

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

    Public Const WM_KEYDOWN As Integer = &H100
    Public Const WM_MOUSEMOVE As Integer = &H200
    Public Const WM_LBUTTONDOWN As Integer = &H201
    Public Const WM_RBUTTONDOWN As Integer = &H204
    Public Const WM_MBUTTONDOWN As Integer = &H207

    Public Const WM_DRAWCLIPBOARD As Integer = &H308
    Public Const WM_CHANGECBCHAIN As Integer = &H30D

    Public Declare Function ChangeClipboardChain Lib "user32" Alias "ChangeClipboardChain" (ByVal hWndRemove As IntPtr, ByVal hWndNewNext As IntPtr) As Boolean
    Public Declare Function SetClipboardViewer Lib "user32" Alias "SetClipboardViewer" (ByVal hWndNewViewer As IntPtr) As IntPtr
    Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Integer

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
                My.Computer.Clipboard.SetText(TheTextBox.Text)
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
                My.Computer.Clipboard.SetText(SB2.ToString)
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
                    My.Computer.Clipboard.SetText(TheTextBox.Text)
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

    Private Sub ButtonLaunch_Click(sender As Object, e As EventArgs) Handles ButtonLaunch.Click

        If TextBoxURL.Text = "" Then
            Exit Sub
        End If

        Try
            Process.Start(TextBoxURL.Text)
            Label_Act_Msg.Text = TextStrs(49)
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
            Label_Act_Msg.Text = TextStrs(25)
        End If

    End Sub

    Private Sub PictureBoxPwdVi_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBoxPwdVi.MouseDown
        GetPass()

        If NowPassStatue > 1 Then
            Dim FPWDS As New SmallPWDShow
            PictureBoxPwdVi.Image = My.Resources.Resource1.button_view_small_Son_on
            FPWDS.InputByte = CurrentAccountPass
            FPWDS.Width = 1
            FPWDS.Height = 1
            FPWDS.ShowDialog()
            PictureBoxPwd.Image = FPWDS.PictureBoxPwd.Image
            FPWDS.Dispose()
            FullGC()
        End If
    End Sub

    Private Sub PictureBoxPwdVi_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBoxPwdVi.MouseUp
        PictureBoxPwd.Image = My.Resources.Resource1.TOPSEC
        PictureBoxPwdVi.Image = b_view_small_on
    End Sub

    Private Sub PictureBoxPwdVi_MouseLeave(sender As Object, e As EventArgs) Handles PictureBoxPwdVi.MouseLeave
        PictureBoxPwd.Image = My.Resources.Resource1.TOPSEC
        PictureBoxPwdVi.Image = b_view_small_on
    End Sub

    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click

        If TextBoxTitle.Text = "" Then
            MSGBOXNEW(TextStrs(23), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
            Exit Sub
        End If

        Dim NowSaveAccName As String = TextBoxTitle.Text

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

                Dim Filename2 As String = Get_New_ACC_Filename(DirName)

                If Not WriteFile(Filename2, AES_KEY_Protected, DERIVED_IDT_Protected) Then
                    GetList()
                    Label_Act_Msg.Text = Replace(TextStrs(20), "$$$", NowSaveAccName)
                    Exe_Fill_Trash()
                    FullGC()
                    Go_ListBoxIdx(ListBox1.Items.Count - 1)
                Else
                    Label_Act_Msg.Text = TextStrs(67)
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
                    Label_Act_Msg.Text = Replace(TextStrs(21), "$$$", NowSaveAccName)
                    Exe_Fill_Trash()
                    FullGC()
                Else
                    Label_Act_Msg.Text = TextStrs(67)
                    MSGBOXNEW(TextStrs(66), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
                End If

            End If

        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click

        Dim NowDeleteAccName As String = TextBoxTitle.Text

        Select Case Delete_ACC_File(NowProcFile, TextStrs(8), False)
            Case 0
                Label_Act_Msg.Text = Replace(TextStrs(40), "$$$", NowDeleteAccName)
            Case 1
            Case 2
                Label_Act_Msg.Text = TextStrs(67)
                MSGBOXNEW(TextStrs(48), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
        End Select

    End Sub

    Private Sub ButtonGoUP_Click(sender As Object, e As EventArgs) Handles ButtonGoUP.Click

        Dim UpperFilename As String
        Dim NowSelect As Integer = ListBox1.SelectedIndex

        If NowSelect >= 2 Then

            UpperFilename = FilesList1(NowSelect - 1).FileName
            My.Computer.FileSystem.MoveFile(NowProcFile, NowProcFile + ".TMP")
            My.Computer.FileSystem.MoveFile(UpperFilename, NowProcFile)
            My.Computer.FileSystem.MoveFile(NowProcFile + ".TMP", UpperFilename)

            GetList()
            Go_ListBoxIdx(NowSelect - 1)
            Label_Act_Msg.Text = TextStrs(28)
        Else
            Exit Sub
        End If

    End Sub

    Private Sub ButtonGoDown_Click(sender As Object, e As EventArgs) Handles ButtonGoDown.Click

        Dim LowerFilename As String
        Dim NowSelect As Integer = ListBox1.SelectedIndex

        If NowSelect < ListBox1.Items.Count - 1 Then

            LowerFilename = FilesList1(NowSelect + 1).FileName
            My.Computer.FileSystem.MoveFile(NowProcFile, NowProcFile + ".TMP")
            My.Computer.FileSystem.MoveFile(LowerFilename, NowProcFile)
            My.Computer.FileSystem.MoveFile(NowProcFile + ".TMP", LowerFilename)

            GetList()
            Go_ListBoxIdx(NowSelect + 1)
            Label_Act_Msg.Text = TextStrs(29)

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

        Dim TmpDR As DialogResult

        If SecureDesktop Then
            TmpDR = LogInFormWork(TextStrs(69), DirNameCurrent, AESKeyCurrent, DERIVED_IDT_Current, 1, Nothing, Nothing, PictureGray, TheSalt)
        Else
            TmpDR = LogInFormWork(TextStrs(69), DirNameCurrent, AESKeyCurrent, DERIVED_IDT_Current, 1, Nothing, Nothing, PictureGray, TheSalt, Me)
        End If

        If TmpDR = DialogResult.OK Then

            Dim Filename2 As String = Get_New_ACC_Filename(DirNameCurrent)

            If DirName <> DirNameCurrent Then

                GetPass()
                NowPassStatue = 3
                VG_Data_Done = True
                VG_Title_Done = True

                ErrFlag = WriteFile(Filename2, AESKeyCurrent, DERIVED_IDT_Current)
                If Not ErrFlag Then
                    Select Case Delete_ACC_File(NowProcFile, TextStrs(7) + vbCrLf + vbCrLf + TextStrs(8), False)
                        Case 2
                            ErrFlag = True
                    End Select
                End If

                If Not ErrFlag Then
                    Label_Act_Msg.Text = Replace(TextStrs(41), "$$$", NowTransAccName)
                Else
                    Label_Act_Msg.Text = TextStrs(67)
                    MSGBOXNEW(TextStrs(66), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
                End If

            Else
                MSGBOXNEW(TextStrs(39), MsgBoxStyle.Critical, TextStrs(5), Me, PictureGray)
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

    Private Sub PictureBoxCATMAN_Click(sender As Object, e As EventArgs) Handles PictureBoxCATMAN.Click

        FormConfig.ComboBoxTimer.SelectedIndex = Val(CAT_setting_Str(0))
        FormConfig.TB_AC_KEY.SelectedIndex = Val(CAT_setting_Str(2))
        FormConfig.CB_SIM1.SelectedIndex = Val(CAT_setting_Str(3))
        FormConfig.TB_PW_KEY.SelectedIndex = Val(CAT_setting_Str(5))
        FormConfig.CB_SIM2.SelectedIndex = Val(CAT_setting_Str(6))

        FormConfig.FormL = Me.Left + (Me.Width - FormConfig.Width) / 2
        FormConfig.FormT = Me.Top + (Me.Height - FormConfig.Height) / 2
        FormConfig.Opacity = Me.Opacity

        MakeWindowsBlur(Me, PictureGray)
        Dim DR As DialogResult = FormConfig.ShowDialog(Me)
        PictureGray.Visible = False
        PictureGray.SendToBack()
        My.Application.DoEvents()

        If DR = DialogResult.OK Then

            ACTLimitSelectIDX = FormConfig.ComboBoxTimer.SelectedIndex
            RegisterKeys(CAT_setting_Str)
            AutoCountDownClose(ACTLimitSelect(CInt(CAT_setting_Str(0))))
            Write_CatDatas()

        ElseIf DR = DialogResult.Ignore Then

            If FormConfig.OtherWorkMode = 1 Then
                If ListBox1.Items.Count > 1 Then
                    TransFullCat()
                End If
            ElseIf FormConfig.OtherWorkMode = 2 Then
                Full_Catalog_Delete()
            End If

        End If

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

    Private Sub PictureWinMin_Click(sender As Object, e As EventArgs) Handles PictureWinMin.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    '==================================================================== ListBox Scrollbar work

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
                Cursor = Cursors.SizeAll ' 更改游標形狀以指示按住按鈕時可以移動它
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

    Dim b_Logout_on As New Bitmap(My.Resources.Resource1.button_LOGOUT_on)
    Dim b_Final_on As New Bitmap(My.Resources.Resource1.button_Final_on)
    Dim b_HELP_on As New Bitmap(My.Resources.Resource1.button_HELP_on)
    Dim b_Launch_on As New Bitmap(My.Resources.Resource1.button_Launch_on)
    Dim b_COPY_on As New Bitmap(My.Resources.Resource1.button_COPY_on)
    Dim b_view_small_on As New Bitmap(My.Resources.Resource1.button_view_small_on)
    Dim b_copy_small_on As New Bitmap(My.Resources.Resource1.button_copy_small_on)
    Dim b_view_on As New Bitmap(My.Resources.Resource1.button_view_on)
    Dim b_view_Son_on As New Bitmap(My.Resources.Resource1.button_view_Son_on)
    Dim b_PictureBoxCATMAN_on As New Bitmap(My.Resources.Resource1.button_CATMAN_on)

    Private Sub Mouse_Enter(sender As Object, e As EventArgs) Handles _
        ButtonRestart.MouseEnter, ButtonFin.MouseEnter, ButtonHelp.MouseEnter, ButtonLaunch.MouseEnter,
        ButtonCopyAccount.MouseEnter, PictureBoxPwdVi.MouseEnter, PictureBoxPwdCPY.MouseEnter,
        ButtonCopyReg.MouseEnter, ButtonViewNote.MouseEnter, PictureBoxCATMAN.MouseEnter

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
        End Select
    End Sub

    Dim b_Logout As New Bitmap(My.Resources.Resource1.button_LOGOUT)
    Dim b_Final As New Bitmap(My.Resources.Resource1.button_Final)
    Dim b_HELP As New Bitmap(My.Resources.Resource1.button_HELP)
    Dim b_Launch As New Bitmap(My.Resources.Resource1.button_Launch)
    Dim b_COPY As New Bitmap(My.Resources.Resource1.button_COPY)
    Dim b_view_small As New Bitmap(My.Resources.Resource1.button_view_small)
    Dim b_copy_small As New Bitmap(My.Resources.Resource1.button_copy_small)
    Dim b_view As New Bitmap(My.Resources.Resource1.button_view)
    Dim b_view_Son As New Bitmap(My.Resources.Resource1.button_view_Son)
    Dim b_PictureBoxCATMAN As New Bitmap(My.Resources.Resource1.button_CATMAN)

    Private Sub Mouse_Leave(sender As Object, e As EventArgs) Handles _
        ButtonRestart.MouseLeave, ButtonFin.MouseLeave, ButtonHelp.MouseLeave, ButtonLaunch.MouseLeave,
        ButtonCopyAccount.MouseLeave, PictureBoxPwdVi.MouseLeave, PictureBoxPwdCPY.MouseLeave,
        ButtonCopyReg.MouseLeave, ButtonViewNote.MouseLeave, PictureBoxCATMAN.MouseLeave

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
        End Select
    End Sub

    Dim B_L_Save_on As New Bitmap(My.Resources.Resource1.button_L_save_on)
    Dim B_L_delete_on As New Bitmap(My.Resources.Resource1.button_L_delete_on)
    Dim B_L_moveUP_on As New Bitmap(My.Resources.Resource1.button_L_moveUP_on)
    Dim B_L_moveDWN_on As New Bitmap(My.Resources.Resource1.button_L_moveDWN_on)
    Dim B_L_transKEY_on As New Bitmap(My.Resources.Resource1.button_L_transKEY_on)
    Dim B_L_fileInfo_on As New Bitmap(My.Resources.Resource1.button_L_fileInfo_on)

    Private Sub L_Mouse_Enter(sender As Object, e As EventArgs) Handles _
        ButtonSave.MouseEnter, ButtonDelete.MouseEnter, ButtonGoUP.MouseEnter, ButtonGoDown.MouseEnter,
        ButtonTransCatalog.MouseEnter, ButtonFileInfo.MouseEnter

        If sender.Enabled = True Then
            Select Case sender.Name
                Case "ButtonSave"
                    ButtonSave.Image = B_L_Save_on
                Case "ButtonDelete"
                    ButtonDelete.Image = B_L_delete_on
                Case "ButtonGoUP"
                    ButtonGoUP.Image = B_L_moveUP_on
                Case "ButtonGoDown"
                    ButtonGoDown.Image = B_L_moveDWN_on
                Case "ButtonTransCatalog"
                    ButtonTransCatalog.Image = B_L_transKEY_on
                Case "ButtonFileInfo"
                    ButtonFileInfo.Image = B_L_fileInfo_on
            End Select
        End If

    End Sub

    Dim B_L_Save As New Bitmap(My.Resources.Resource1.button_L_save)
    Dim B_L_delete As New Bitmap(My.Resources.Resource1.button_L_delete)
    Dim B_L_moveUP As New Bitmap(My.Resources.Resource1.button_L_moveUP)
    Dim B_L_moveDWN As New Bitmap(My.Resources.Resource1.button_L_moveDWN)
    Dim B_L_transKEY As New Bitmap(My.Resources.Resource1.button_L_transKEY)
    Dim B_L_fileInfo As New Bitmap(My.Resources.Resource1.button_L_fileInfo)

    Private Sub L_Mouse_Leave(sender As Object, e As EventArgs) Handles _
        ButtonSave.MouseLeave, ButtonDelete.MouseLeave, ButtonGoUP.MouseLeave, ButtonGoDown.MouseLeave,
        ButtonTransCatalog.MouseLeave, ButtonFileInfo.MouseLeave

        If sender.Enabled = True Then
            Select Case sender.Name
                Case "ButtonSave"
                    ButtonSave.Image = B_L_Save
                Case "ButtonDelete"
                    ButtonDelete.Image = B_L_delete
                Case "ButtonGoUP"
                    ButtonGoUP.Image = B_L_moveUP
                Case "ButtonGoDown"
                    ButtonGoDown.Image = B_L_moveDWN
                Case "ButtonTransCatalog"
                    ButtonTransCatalog.Image = B_L_transKEY
                Case "ButtonFileInfo"
                    ButtonFileInfo.Image = B_L_fileInfo
            End Select
        End If

    End Sub
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



