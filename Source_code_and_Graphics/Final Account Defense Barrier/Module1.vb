﻿'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography

Module Module1

    Public Structure FileLists
        Public FileName As String
        Public ShowName As String
        Public FileIsBad As Boolean
    End Structure

    Public Structure BIP39Str
        Public BIP39Word() As String
    End Structure

    Public BIP39_Word(9) As BIP39Str

    Public AES_KEY_Protected() As Byte
    Public DERIVED_IDT_Protected() As Byte
    Public UIMode As Integer = 0

    Public DirName As String
    Public CAT_setting_Str() As String
    Public NoNotice As Boolean = False
    Public ClearWorking As Boolean = False
    Public ReadingWorking As Boolean = False

    Public SeuDeskName() As Byte

    Public ACTCount As Integer
    Public ACTLimit As Integer
    Public ACTLimitSelectIDX As Integer
    Public ACTLimitSelect() As Integer = {0, 30, 60, 180, 300, 600, 1800}

    Public NowPassStatue As Integer = 0
    '0=New Account no edit, CurrentAccountPass = "" in init
    '1=Old File Read, not decrypt
    '2=Old File Read, decrypted, CurrentAccountPass usable, Not edit
    '3=Edited

    Public CurrentAccountPass() As Byte
    Public Read_File_Pass_Str As String

    Public ALLOPACITY As Single = 1.0F
    Public OSver As Integer
    Public Close_Clear_Clipper As Boolean


    Public Const MainWebURL As String = "https://github.com/overdoignism/Final-Account-Defense-Barrier"
    Public Const File_Limit As Long = 134217728

    Public SCutChar() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G",
        "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "-", "=",
        "[", "]", ";", "'", "<", ">", "/"}

    Public TextStrs() As String = {
        "No matching catalog found," + vbCrLf + vbCrLf + "and the validation is required when creating a new one.", '0
        "No matching catalog found, and the validation is mismatch.",
        "No matching catalog found, do you want to create a new one?",
        "Please input the password. Each key corresponds to a catalog.",
        "For new password validation. If using an existing password, no input required.",
        "Error",'5
        "Error: Secure desktop creation error." + vbCrLf + vbCrLf + "Please try again, if the problem persists, there may be some interference.",
        "Catalog transform done.",
        "Do you want to delete ""$$$"" from this catalog ?",
        "Confirm",
        "Do you want to delete this catalog ""$$$"" ?",'10
        "It will transform ""$$$"" to other catalog.",
        "You need to input the target catalog password in the next step.",
        "Do you want update this account ""$$$"" ?", '13
        "It will transform catalog ""$$$"" to other one.",
        "Please confirm by entering ""DELETE"" in the text box.",
        "Information",
        "Build time (by OS):",
        "Last write time (by OS):", '18
        "Full catalog transform",
        "Account ""$$$"" added.",
        "Account ""$$$"" updated.",
        "(Decryption failed)", '22
        "Title is empty. This is a required field.",
        "The web browser can't be opened in secure desktop mode." + vbCrLf + vbCrLf + "You can open it in the main window later.", '24
        "Copied.",
        "Unable to launch:" + vbCrLf + vbCrLf + "The path (file/URL) could not be resolved or does not exist.", '26
        "(New account)",
        """$$$"" Move Up.",
        """$$$"" Move Down.",
        "Category settings updated.",
        "Title. This field is required.",'31
        "URL or program path.",
        "Username or cryptocurrency address.",
        "Input password, private key or passphrase.",
        "The validation to prevent typos. (Omit when using BIP39)",
        "Registed e-mail or cell phone number.",
        "Note 2. For invisible message.",
        "Filename:",'38 
        "Source and destination are the same." + vbCrLf + vbCrLf + "It will make duplicate account data in the catalog, are you sure?",
        "Account ""$$$"" deleted.", '40
        "Account ""$$$"" transformed.", '41
        "Version",
        "Catalog transformed.", '43
        "(Untitled)",
        "(Desktop)",
        "(My Documents)",
        "(App Home)",
        "Access deined.", '48
        "Launched.",
        "Note 1. Any notes can be placed here.",
        "Do you want to add this account ""$$$"" ?", '51
        "Do you want to update the settings of catalog ""$$$"" ?",
        "(Disabled)",
        "Copy only", '54
        "Copy + paste + clear",
        "Send key",
        "Hybrid",
        "File size must be greater than 0 and less than or equal to 128MB.", '58
        "Login",
        "Settings",
        "File Explorer",
        "Catalog loaded.",
        "The Windows version is too low to enable" + vbCrLf + vbCrLf + """Process Mitigation Policy"" security measures." + vbCrLf + vbCrLf + "At least Windows 8 or Windows Server 2012 or later versions are required." + vbCrLf + vbCrLf + "(It is recommended to use Windows 10 version 20H2 or later)",
        "Some files or directory could not be deleted." + vbCrLf + vbCrLf + "Please check if you have appropriate permissions.",
        "The file is inaccessible." + vbCrLf + vbCrLf + "Maybe permissions insufficient, or corruption / misplacement.",
        "File write failed." + vbCrLf + vbCrLf + "Insufficient permissions or disk error.",
        "(Some error happend)",'67
        "Account ""$$$"" opened.",'68
        "Catalog password input", '69
        "Detected that you used copy-paste to input." + vbCrLf + vbCrLf + "Do you want to clear the clipboard?",
        "Generator don't use symbols", '71
        "Account Password Input",
        "(Disabled)",'73
         "30 sec",
         "1 min",
         "3 min",
         "5 min",
         "10 min",
         "30 min", '79
         "Current folder:",
         "Hotkey ($$$) registration failed: possible conflict with existing hotkeys." + vbCrLf + vbCrLf + "Please avoid using the same hotkey and reconfigure.",
         "The validation string is mismatched.",
         "The validation string is empty and the input doesn't pass the BIP39 rule." + vbCrLf + vbCrLf + "If you're using BIP39, the problem could be:" + vbCrLf + vbCrLf, '83
         "Incorrect format or word count.",
         "An unknown word appeared. Maybe a typo.",
         "Bad checksum, probably in the wrong order.",
         "Detected an invaild $$$ (or its compatible coin) address, this may caused by:" + vbCrLf + vbCrLf,'87
         "Typo, case error, order error." + vbCrLf + vbCrLf + "Length too logn or too short, or copy-paste miss." + vbCrLf + vbCrLf + "Misjudgment. It's not a $$$ address." + vbCrLf + vbCrLf,
         "Do you want to continue to save?", '89
         "Are you sure you want to import CSV file?" + vbCrLf + vbCrLf + "If you have any questions," + vbCrLf + vbCrLf + "please refer to the online help, ""CSV Import"" section.",
         "CSV import done.", '91
         "CSV import failure.",
         "This may not be a correct CSV file.",
         "Please handle the CSV files properly to avoid leakage of confidentiality.", '94
         "CSV file is not encrypted and has security risks." + vbCrLf + vbCrLf + "Are you sure you want to export?" + vbCrLf + vbCrLf + "(Yow need to enter the password of the current catalog.)",
         "The password does not match the current catalog. Export abort.",
         "No account in the catalog. Export abort.", '97
         "The saved file name is " + vbCrLf + vbCrLf + "$$$" + vbCrLf + vbCrLf + "in the application folder." + vbCrLf + vbCrLf + "(Next we will guide you to the file location.)",
         "Attached debugger detected." + vbCrLf + vbCrLf + "If you're not programming, it probably means a malicious attack has occurred." + vbCrLf + vbCrLf + "Strongly recommended to stop using and perform anti-virus.",
         "Critical",
         "When hotkey enabled, override to ""Send key"" mode.",  '101
         "Copying failed.",
         "May be caused by a clipboard utility, malware, or antivirus.",
         "Switch to read-only mode.", '104
         "Multiple instances detected. Writing locked to prevent conflicts.",
         "Insufficient permissions to write to file.",
         "Attention"
    }

    Public CoinList() As String = {"-", "BTC", "TRX", "Doge", "LTC", "ETH", "BTC"}

    Public HotKeyID1 As Integer = 1100
    Public HotKeyID2 As Integer = 2100

    <DllImport("user32.dll")>
    Public Function RegisterHotKey(hWnd As IntPtr, id As Integer, fsModifiers As Integer, vlc As Integer) As Boolean
    End Function

    <DllImport("user32.dll")>
    Public Function UnregisterHotKey(hWnd As IntPtr, id As Integer) As Boolean
    End Function

    Public Function Get_New_ACC_Filename(ByRef The_path As String) As String

        Dim elapsedTicks As Decimal = Now.Ticks - New DateTime(2001, 1, 1).Ticks
        Return The_path + "\" + Decimal_to_x36(elapsedTicks, True).ToString + ".ACC"

    End Function

    Public Function CX16STR_2_DEC(ByRef InputStr As String) As Decimal

        Dim TmpDecimal As Decimal = 0
        For IDX01 As Integer = 0 To InputStr.Length - 1
            TmpDecimal = (TmpDecimal * 16) + Convert.ToDecimal(Convert.ToInt32(InputStr.Substring(IDX01, 1), 16))
        Next
        Return TmpDecimal

    End Function

    Public Function Decimal_to_x36(InVal As Decimal, AsFileName As Boolean) As String

        Dim x36Str() As Char = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C",
        "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U",
        "V", "W", "X", "Y", "Z"}

        Dim OutputStr As String = ""
        Dim Tmp1, Tmp2 As Decimal

        Tmp2 = InVal

        Do
            Tmp1 = Tmp2 Mod 36
            Tmp2 = (Tmp2 - Tmp1) / 36
            OutputStr = x36Str(Tmp1) + OutputStr
        Loop While Tmp2 > 0

        If AsFileName Then
            Return OutputStr.PadLeft(13, "0")
        Else
            Return OutputStr
        End If

    End Function

    Public Sub End_Program()

        FullGC()
        Exe_Fill_Trash()
        FullGC()

        Try
            UnregisterHotKey(FormMain.Handle, HotKeyID1)
            UnregisterHotKey(FormMain.Handle, HotKeyID2)

            If NotLockEd Then LockFile.Close()
            IO.File.Delete(LockFilePath)

        Catch ex As Exception
        End Try

        End

    End Sub

    Public Function Random_Strs(LengthNeed_Min As Integer, LengthNeed_Max As Integer, StrMode As Integer) As String

        'StrMode 0 for SecureDesktop
        'StrMode 1 for Password generator and confuse generator
        'StrMode 2 for Password generator without symbos

        Dim SpeedSB As New System.Text.StringBuilder
        Threading.Thread.Sleep(5)

        Dim LengthNeed As Integer = Get_RangeRnd_ByRNG(LengthNeed_Min, LengthNeed_Max)

        If StrMode = 0 Then
            For IDX01 As Long = 0 To LengthNeed - 1
                SpeedSB.Append(Chr(Get_RangeRnd_ByRNG(33, 90)))
            Next
        ElseIf StrMode = 1 Then
            For IDX01 As Long = 0 To LengthNeed - 1
                SpeedSB.Append(Chr(Get_RangeRnd_ByRNG(33, 126)))
            Next
        ElseIf StrMode = 2 Then

            Dim PWD_num_letter_only() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C",
                "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W",
                "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q",
                "r", "s", "t", "u", "v", "w", "x", "y", "z"}

            For IDX01 As Long = 0 To LengthNeed - 1
                SpeedSB.Append(PWD_num_letter_only(Get_RangeRnd_ByRNG(0, UBound(PWD_num_letter_only))))
            Next
        End If

        Random_Strs = SpeedSB.ToString

    End Function

    Dim MSGBX_IMG_OK As New Bitmap(Image.FromStream(New IO.MemoryStream(My.Resources.Resource1.Message_OK_PNG)))
    Dim MSGBX_IMG_VRF As New Bitmap(Image.FromStream(New IO.MemoryStream(My.Resources.Resource1.Message_VRF_PNG)))
    Dim MSGBX_IMG_CRI As New Bitmap(Image.FromStream(New IO.MemoryStream(My.Resources.Resource1.Message_CRI_PNG)))

    Dim MSGBX_IMG_OK_TXTB As New Bitmap(Image.FromStream(New IO.MemoryStream(My.Resources.Resource1.Message_OK_TXTB_PNG)))
    Dim MSGBX_IMG_VRF_TXTB As New Bitmap(Image.FromStream(New IO.MemoryStream(My.Resources.Resource1.Message_VRF_TXTB_PNG)))
    Dim MSGBX_IMG_CRI_TXTB As New Bitmap(Image.FromStream(New IO.MemoryStream(My.Resources.Resource1.Message_CRI_TXTB_PNG)))

    Public Function MSGBOXNEW(MessageStr As String, BoxType As MsgBoxStyle, BoxTitle As String,
                              ByRef FirerForm As Form, ByRef FFPicBox As PictureBox) As DialogResult

        Dim NeoMSGBOX As New MSGBOXXX
        Dim ReturnDR As DialogResult
        NeoMSGBOX.Opacity = ALLOPACITY

        Select Case BoxType
            Case MsgBoxStyle.OkOnly
                NeoMSGBOX.ButtonOK.Visible = True
                NeoMSGBOX.PictureBox1.Image = MSGBX_IMG_OK
                NeoMSGBOX.Label_Msg_Work.Image = ResizeBitmap(MSGBX_IMG_OK_TXTB, 2, 2)
            Case MsgBoxStyle.OkCancel, MsgBoxStyle.YesNo
                NeoMSGBOX.ButtonYes.Visible = True
                NeoMSGBOX.ButtonNo.Visible = True
                NeoMSGBOX.PictureBox1.Image = MSGBX_IMG_VRF
                NeoMSGBOX.Label_Msg_Work.Image = ResizeBitmap(MSGBX_IMG_VRF_TXTB, 2, 2)
            Case MsgBoxStyle.Exclamation
                NeoMSGBOX.ButtonOK.Visible = True
                NeoMSGBOX.PictureBox1.Image = MSGBX_IMG_VRF
                NeoMSGBOX.Label_Msg_Work.Image = ResizeBitmap(MSGBX_IMG_VRF_TXTB, 2, 2)
            Case MsgBoxStyle.Critical
                NeoMSGBOX.ButtonOK.Visible = True
                NeoMSGBOX.PictureBox1.Image = MSGBX_IMG_CRI
                NeoMSGBOX.Label_Msg_Work.Image = ResizeBitmap(MSGBX_IMG_CRI_TXTB, 2, 2)
            Case 65535
                NeoMSGBOX.ButtonOK.Visible = True
                NeoMSGBOX.ButtonOK.Enabled = False
                NeoMSGBOX.ButtonOK.Image = Make_Button_Gray(My.Resources.Resource1.button_confirm)
                NeoMSGBOX.ButtonOK.Location = New Point(142, NeoMSGBOX.ButtonOK.Location.Y)
                NeoMSGBOX.ButtonCancel.Visible = True
                NeoMSGBOX.TextBoxDELETE.Visible = True
                NeoMSGBOX.PictureBox1.Image = MSGBX_IMG_VRF
                NeoMSGBOX.Label_Msg_Work.Image = ResizeBitmap(MSGBX_IMG_VRF_TXTB, 2, 2)
        End Select

        NeoMSGBOX.Label_Msg_Work.Text = MessageStr
        Dim NeoMsgLabWork As New Bitmap(NeoMSGBOX.Label_Msg_Work.Width, NeoMSGBOX.Label_Msg_Work.Height)
        Dim NewRec As Rectangle
        NewRec.Width = NeoMSGBOX.Label_Msg_Work.Width
        NewRec.Height = NeoMSGBOX.Label_Msg_Work.Height
        NeoMSGBOX.Label_Msg_Work.DrawToBitmap(NeoMsgLabWork, NewRec)
        NeoMSGBOX.Label_Msg_Show.Image = ResizeBitmap(NeoMsgLabWork, 0.5, 0.5)

        NeoMSGBOX.Label_Title_Work.Text = BoxTitle
        Dim NeoMsgTitleWork As New Bitmap(NeoMSGBOX.Label_Title_Work.Width, NeoMSGBOX.Label_Title_Work.Height)
        Dim NewRec2 As Rectangle
        NewRec2.Width = NeoMSGBOX.Label_Title_Work.Width
        NewRec2.Height = NeoMSGBOX.Label_Title_Work.Height
        NeoMSGBOX.Label_Title_Work.DrawToBitmap(NeoMsgTitleWork, NewRec)
        NeoMSGBOX.Label_Title_Show.Image = ResizeBitmap(NeoMsgTitleWork, 0.5, 0.5)

        MakeWindowsBlur(FirerForm, FFPicBox)
        ReturnDR = NeoMSGBOX.ShowDialog(FirerForm)
        UnMakeWindowsBlur(FFPicBox)

        NeoMSGBOX.Close()
        NeoMSGBOX.Dispose()
        NeoMSGBOX = Nothing

        Return ReturnDR

    End Function

    Public Function ResizeBitmap(originalBitmap As Bitmap, ScaleX As Single, ScaleY As Single) As Bitmap
        Dim newWidth As Integer = originalBitmap.Width * ScaleX
        Dim newHeight As Integer = originalBitmap.Height * ScaleY

        Dim resizedBitmap As New Bitmap(newWidth, newHeight)

        Using g As Graphics = Graphics.FromImage(resizedBitmap)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.DrawImage(originalBitmap, New Rectangle(0, 0, newWidth, newHeight))
        End Using

        Return resizedBitmap
    End Function

    Public Sub RestartApp2(UseSecDesktop As Boolean, Runas As Boolean)

        Dim info As New ProcessStartInfo(Application.ExecutablePath)

        info.UseShellExecute = True

        If Runas Then info.Verb = "runas"

        Dim Arguments() As String = Environment.GetCommandLineArgs()

        Dim NewArgStr As String = ""
        For Each ArgStr As String In Arguments
            If ArgStr.ToUpper <> "SECUREDESKTOP" Then NewArgStr += ArgStr + " "
        Next
        If UseSecDesktop Then
            info.Arguments = "SECUREDESKTOP " + NewArgStr
        Else
            info.Arguments = NewArgStr
        End If

        Try
            Process.Start(info)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Threading.Thread.Sleep(500)
        End_Program()

    End Sub

    Public Sub Load_BIP39_Word()

        Dim memoryStream As New IO.MemoryStream()
        Dim BIP39Text As String = ""

        Using archive As New System.IO.Compression.ZipArchive(New IO.MemoryStream(My.Resources.Resource1.BIP39WORD))
            For Each entry As System.IO.Compression.ZipArchiveEntry In archive.Entries
                If entry.Name = "BIP39WORD.txt" Then
                    Using entryStream As IO.Stream = entry.Open()
                        entryStream.CopyTo(memoryStream)
                        memoryStream.Position = 0
                        Dim sr As New IO.StreamReader(memoryStream, True)
                        BIP39Text = sr.ReadToEnd()
                    End Using
                    Exit For
                End If
            Next
        End Using

        BIP39Text = Replace(BIP39Text, vbCrLf, vbCr)
        Dim BIP39Text_Arr() As String = BIP39Text.Split(vbCr)

        For IDX01 As Integer = 0 To 9
            BIP39_Word(IDX01).BIP39Word = BIP39Text_Arr(IDX01).Split(",")
        Next

    End Sub

    Public Function GetOkFilename(TheFilename As String) As String

        Dim invalidChars As String = New String(IO.Path.GetInvalidFileNameChars())
        For Each c As Char In invalidChars
            TheFilename = TheFilename.Replace(c, "_"c)
        Next

        Return TheFilename

    End Function

    Public Function Get_RangeRnd_ByRNG(minValue As Long, maxValue As Long) As Long

        ' 產生一個隨機數產生器
        Dim rng As RandomNumberGenerator = RandomNumberGenerator.Create()

        ' 產生一個 4 bytes 的隨機數字範圍 (0 ~ 4294967295)
        Dim randomBytes(3) As Byte
        rng.GetBytes(randomBytes)
        Dim randomNumber As Long = BitConverter.ToUInt32(randomBytes, 0)

        ' 將隨機數字轉換為指定範圍的整數
        Return CLng(Math.Floor((randomNumber / UInt32.MaxValue) * (maxValue - minValue + 1))) + minValue

    End Function

    Public Sub GetPass()

        Select Case NowPassStatue
            Case 0
            Case 1
                Dim SDAES As New SmallDecoderAES
                SDAES.AES_KEY_Use = AES_KEY_Protected
                SDAES.NeedToDecryptoStr = Read_File_Pass_Str
                If SDAES.ShowDialog() = DialogResult.OK Then
                    CurrentAccountPass = SDAES.CurrentAccountPass
                    NowPassStatue = 2
                End If
                SDAES.Dispose()
                FullGC()
            Case 2
            Case 3

        End Select

    End Sub

    '==================== Functions try for mitigation memory leak

    Public Sub Exe_Fill_Trash(Optional BigNum As Integer = 1)
        Dim FillTrash() As String
        Dim Big2 As Integer = 8 * BigNum

        For idx01 As Integer = 0 To 32767
            ReDim Preserve FillTrash(idx01)
            FillTrash(idx01) = New String("x", Big2)
        Next
    End Sub

    Public Sub FullGC()

        GC.Collect(2, GCCollectionMode.Forced, True, True)
        GC.WaitForPendingFinalizers()
        GC.WaitForFullGCApproach()
        GC.WaitForFullGCComplete()
        GC.Collect(2, GCCollectionMode.Forced, True, True)
        GC.WaitForPendingFinalizers()
        GC.WaitForFullGCApproach()
        GC.WaitForFullGCComplete()

        System.Diagnostics.Process.GetCurrentProcess().MinWorkingSet = CType(3000, IntPtr)

    End Sub

    Public Sub WipeBytes(ByRef Input_Bytes() As Byte)

        If Input_Bytes IsNot Nothing Then
            For IDX01 As Integer = 0 To UBound(Input_Bytes)
                Input_Bytes(IDX01) = 0
            Next
        End If

    End Sub

    Public Sub WipeUINT(ByRef Input_Uint() As UInteger)

        If Input_Uint IsNot Nothing Then
            For IDX01 As Integer = 0 To UBound(Input_Uint)
                Input_Uint(IDX01) = 0
            Next
        End If

    End Sub

    Public Sub WipeChrs(ByRef Input_Chrs() As Char)

        For IDX01 As Integer = 0 To UBound(Input_Chrs)
            Input_Chrs(IDX01) = "x"
        Next

    End Sub

    Public Sub ClearTextBox(ByRef ClearTextBox As TextBox)

        Dim Short1KBYTEstr() As String = {New String("x", 1024), New String("y", 1024)}

        ClearWorking = True

        For idx01 As Integer = 0 To 511
            ClearTextBox.Text = Short1KBYTEstr(idx01 Mod 2)
            ClearTextBox.ClearUndo()
        Next

        ClearWorking = False
        ClearTextBox.Clear()

    End Sub

    Public Sub ClearLabel(ByRef ClearLabel As Label)

        Dim Short1KBYTEstr() As String = {New String("x", 1024), New String("y", 1024)}

        ClearWorking = True

        For idx01 As Integer = 0 To 511
            ClearLabel.Text = Short1KBYTEstr(idx01 Mod 2)
        Next

        ClearWorking = False
        ClearLabel.Dispose()

    End Sub

    Public Sub ClearMS(ByRef source As System.IO.MemoryStream)

        Dim buffer As Byte() = source.GetBuffer()
        Array.Clear(buffer, 0, buffer.Length)
        source.Position = 0
        source.SetLength(0)

    End Sub

    ' ====================== Secure desktop mode and Password input window (FormLogin)

    <DllImport("user32.dll")>
    Public Function CreateDesktop(lpszDesktop As String, lpszDevice As IntPtr, pDevmode As IntPtr,
                                  dwFlags As Integer, dwDesiredAccess As UInteger, lpsa As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Public Function SwitchDesktop(hDesktop As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")>
    Public Function CloseDesktop(handle As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")>
    Public Function SetThreadDesktop(hDesktop As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")>
    Public Function GetThreadDesktop(dwThreadId As Integer) As IntPtr
    End Function

    '<DllImport("kernel32.dll")> Public Function GetCurrentThreadId() As Integer    End Function

    Enum DESKTOP_ACCESS As UInteger
        DESKTOP_NONE = 0
        DESKTOP_READOBJECTS = &H1
        DESKTOP_CREATEWINDOW = &H2
        DESKTOP_CREATEMENU = &H4
        DESKTOP_HOOKCONTROL = &H8
        DESKTOP_JOURNALRECORD = &H10
        DESKTOP_JOURNALPLAYBACK = &H20
        DESKTOP_ENUMERATE = &H40
        DESKTOP_WRITEOBJECTS = &H80
        DESKTOP_SWITCHDESKTOP = &H100

        GENERIC_ALL = (DESKTOP_READOBJECTS Or DESKTOP_CREATEWINDOW Or DESKTOP_CREATEMENU Or DESKTOP_HOOKCONTROL Or DESKTOP_JOURNALRECORD Or DESKTOP_JOURNALPLAYBACK Or DESKTOP_ENUMERATE Or DESKTOP_WRITEOBJECTS Or DESKTOP_SWITCHDESKTOP)
    End Enum

    Public Function LogInFormWork(TitleStr As String, ByRef ThisTimeDir As String, ByRef ThisTimeKey() As Byte,
                                  ByRef ThisTimeDIP() As Byte, WorkMode As Integer, PwdState As Integer,
                                  NoNotice As Boolean, ByRef FFPicBox As PictureBox, ByRef WorkSalt() As Byte,
                                  Optional ByRef FirerForm As FormMain = Nothing) As DialogResult

        LogInFormWork = DialogResult.Cancel
        Dim LIFWRtn As DialogResult = DialogResult.Cancel

        Dim LIFW = New FormLogin
        LIFW.Text = TitleStr
        LIFW.Opacity = ALLOPACITY

        Dim RunAAMode As Boolean
        Dim RunSDMode As Boolean

        LIFW.WorkMode = WorkMode
        LIFW.This_Time_Salt = WorkSalt

        Select Case WorkMode
            Case 0 '0=Start mode 
                If NoNotice Then
                    LIFW.Height = 661
                End If
            Case 1 '1=Non Start mode
            Case 2 '2=Password mode
                LIFW.PassByte = CurrentAccountPass
                LIFW.PwdState = PwdState
        End Select

        If FirerForm Is Nothing Then
            'GetCurrentThreadId()
            Dim ErrHappend As Boolean = False
            Dim Old_hDesktop As IntPtr = GetThreadDesktop(Process.GetCurrentProcess().Threads(0).Id)
            Dim New_hDesktop As IntPtr = CreateDesktop(
               System.Text.Encoding.Unicode.GetString(Security.Cryptography.ProtectedData.Unprotect(
               SeuDeskName, Nothing, DataProtectionScope.CurrentUser)),
               IntPtr.Zero, IntPtr.Zero, 0, DESKTOP_ACCESS.GENERIC_ALL, IntPtr.Zero)

            If New_hDesktop = 0 Then MsgBox(TextStrs(6), MsgBoxStyle.Critical, TextStrs(5))

            SwitchDesktop(New_hDesktop)

            Task.Factory.StartNew(Sub()

                                      LIFW.TempClipboardStr = GetTextFromClipboard()
                                      LIFW.SecureDesktopMode = True
                                      LIFW.IsUseSD = True
                                      SetThreadDesktop(New_hDesktop)
                                      Try
                                          LIFWRtn = LIFW.ShowDialog()
                                      Catch ex As Exception
                                          ErrHappend = True
                                      End Try
                                      SetThreadDesktop(Old_hDesktop)

                                  End Sub).Wait()

            SwitchDesktop(Old_hDesktop)
            CloseDesktop(New_hDesktop)

            If ErrHappend Then MsgBox(TextStrs(6), MsgBoxStyle.Critical, TextStrs(5))

        Else

            LIFW.FormW = FirerForm.Width
            LIFW.FormH = FirerForm.Height
            LIFW.FormT = FirerForm.Top
            LIFW.FormL = FirerForm.Left

            MakeWindowsBlur(FirerForm, FFPicBox)
            LIFW.StartPosition = FormStartPosition.CenterParent
            LIFW.SecureDesktopMode = False
            LIFWRtn = LIFW.ShowDialog()
            UnMakeWindowsBlur(FFPicBox)

        End If

        Select Case WorkMode
            Case 0, 1
                If LIFWRtn = DialogResult.OK Then
                    ThisTimeKey = LIFW.This_Time_Key.Clone
                    ThisTimeDir = LIFW.This_Time_Dir.Clone
                    ThisTimeDIP = LIFW.This_Time_DIP.Clone
                End If

                RunAAMode = LIFW.IsUseRUNAS
                RunSDMode = LIFW.IsUseSD

                If LIFWRtn = DialogResult.Abort Then
                    RestartApp2(RunSDMode, RunAAMode)
                End If
                LogInFormWork = LIFWRtn

            Case 2
                If LIFWRtn = DialogResult.OK Then
                    CurrentAccountPass = LIFW.PassByte.Clone
                    LogInFormWork = LIFW.PwdState
                Else
                    LogInFormWork = PwdState
                End If
            Case 3
                ThisTimeDir = LIFW.This_Time_Dir.Clone
                LogInFormWork = LIFWRtn
        End Select

        LIFW.Close()
        LIFW.Dispose()

        If Close_Clear_Clipper Then
            My.Computer.Clipboard.Clear()
        End If

    End Function

    '================ Get Clipboard text (for memory leak fix) ===========

    <DllImport("user32.dll", SetLastError:=True)>
    Private Function OpenClipboard(ByVal hWndNewOwner As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Function CloseClipboard() As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Function GetClipboardData(ByVal uFormat As UInteger) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Function GlobalLock(ByVal hMem As IntPtr) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Function GlobalUnlock(ByVal hMem As IntPtr) As Boolean
    End Function

    Public Function GetTextFromClipboard() As String

        If Not OpenClipboard(IntPtr.Zero) Then Return Nothing

        Dim handle = GetClipboardData(13)
        If handle = IntPtr.Zero Then Return Nothing

        Dim pointer = GlobalLock(handle)
        If pointer = IntPtr.Zero Then Return Nothing

        Dim data = Marshal.PtrToStringUni(pointer)
        GlobalUnlock(handle)
        CloseClipboard()

        Return data
    End Function

    '================ For window grayed out visual ==============

    <DllImport("Shcore.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
    Public Function GetScaleFactorForMonitor(hMon As IntPtr, ByRef pScale As DEVICE_SCALE_FACTOR) As Integer 'HRESULT
    End Function

    <DllImport("User32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
    Public Function MonitorFromWindow(ByVal hwnd As IntPtr, ByVal dwFlags As Integer) As IntPtr
    End Function

    Public Enum DEVICE_SCALE_FACTOR
        SCALE_100_PERCENT = 100
        SCALE_120_PERCENT = 120
        SCALE_125_PERCENT = 125
        SCALE_140_PERCENT = 140
        SCALE_150_PERCENT = 150
        SCALE_160_PERCENT = 160
        SCALE_175_PERCENT = 175
        SCALE_180_PERCENT = 180
        SCALE_200_PERCENT = 200
        SCALE_225_PERCENT = 225
        SCALE_250_PERCENT = 250
        SCALE_300_PERCENT = 300
        SCALE_350_PERCENT = 350
        SCALE_400_PERCENT = 400
        SCALE_450_PERCENT = 450
        SCALE_500_PERCENT = 500
    End Enum

    Public Const MONITOR_DEFAULTTONULL As Integer = &H0
    Public Const MONITOR_DEFAULTTOPRIMARY As Integer = &H1
    Public Const MONITOR_DEFAULTTONEAREST As Integer = &H2

    Public ScaleFolat As Single = 1.0F

    Public Sub GetMonScale(ByRef WhatForm As Form)

        Dim hMon As IntPtr = MonitorFromWindow(WhatForm.Handle, MONITOR_DEFAULTTOPRIMARY)
        Dim scaleFactor As DEVICE_SCALE_FACTOR
        GetScaleFactorForMonitor(hMon, scaleFactor)
        ScaleFolat = scaleFactor / 100

    End Sub

    Public Sub MakeWindowsBlur(ByRef WhatForm As Form, ByRef WhatImgToPut As PictureBox)

        Dim NewPos As Point
        NewPos.X = WhatForm.Location.X * ScaleFolat
        NewPos.Y = WhatForm.Location.Y * ScaleFolat
        Dim NewSize As Size
        NewSize.Width = WhatForm.Size.Width * ScaleFolat
        NewSize.Height = WhatForm.Size.Height * ScaleFolat

        Dim bmpOrg As New Bitmap(NewSize.Width, NewSize.Height)

        Using g1 As Graphics = Graphics.FromImage(bmpOrg)
            g1.CopyFromScreen(NewPos, Point.Empty, NewSize)
        End Using

        Dim newBitmap As Bitmap = New Bitmap(bmpOrg.Width, bmpOrg.Height)
        Dim g As Graphics = Graphics.FromImage(newBitmap)

        '創建ColorMatrix
        Dim colorMatrix As New Imaging.ColorMatrix(New Single()() _
        {
            New Single() {0.3F, 0.3F, 0.3F, 0, 0},
            New Single() {0.59F, 0.59F, 0.59F, 0, 0},
            New Single() {0.11F, 0.11F, 0.11F, 0, 0},
            New Single() {0, 0, 0, 1, 0},
            New Single() {-0.4F, -0.4F, -0.4F, 0, 1}
        })
        '創建ImageAttributes
        Dim imageAttributes As New Imaging.ImageAttributes()
        imageAttributes.SetColorMatrix(colorMatrix)

        bmpOrg = ResizeBitmap(bmpOrg, 1 / ScaleFolat, 1 / ScaleFolat)
        '使用Graphics將原始圖片繪製到Bitmap中，同時應用ImageAttributes
        g.DrawImage(bmpOrg, New Rectangle(0, 0, bmpOrg.Width, bmpOrg.Height), 0, 0, bmpOrg.Width, bmpOrg.Height, GraphicsUnit.Pixel, imageAttributes)

        WhatImgToPut.Image = newBitmap
        WhatImgToPut.BringToFront()
        WhatImgToPut.Visible = True

    End Sub

    Public Sub UnMakeWindowsBlur(ByRef WhatImgToPut As PictureBox)
        WhatImgToPut.Visible = False
        WhatImgToPut.SendToBack()
        My.Application.DoEvents()
    End Sub

    Public Function Make_Button_brighter(ByRef Orig_Bitmap As Bitmap, Optional BrightPN As Single = 1.3) As Bitmap

        ' 建立目標圖像的副本
        Dim targetBitmap As New Bitmap(Orig_Bitmap.Width, Orig_Bitmap.Height)

        ' 創建ColorMatrix，用於調整圖像亮度
        Dim brightenMatrix As New Imaging.ColorMatrix(New Single()() _
            {New Single() {BrightPN, 0, 0, 0, 0},
             New Single() {0, BrightPN, 0, 0, 0},
             New Single() {0, 0, BrightPN, 0, 0},
             New Single() {0, 0, 0, 1, 0},
             New Single() {0.12, 0.12, 0.12, 0, 1}})

        ' 創建ImageAttributes對象，並設定ColorMatrix
        Dim imageAttributes As New Imaging.ImageAttributes()
        imageAttributes.SetColorMatrix(brightenMatrix)

        ' 創建Graphics對象，並使用ImageAttributes繪製目標圖像
        Dim graphics As Graphics = Graphics.FromImage(targetBitmap)
        graphics.DrawImage(Orig_Bitmap, New Rectangle(0, 0, Orig_Bitmap.Width, Orig_Bitmap.Height), 0, 0, Orig_Bitmap.Width, Orig_Bitmap.Height, GraphicsUnit.Pixel, imageAttributes)

        ' 釋放資源
        graphics.Dispose()

        ' 顯示調整後的圖像
        Return targetBitmap

    End Function
    Public Function Make_Button_Gray(ByRef Orig_Bitmap As Bitmap, Optional LowDimm As Single = 0) As Bitmap

        ' 建立目標圖像的副本
        Dim targetBitmap As New Bitmap(Orig_Bitmap.Width, Orig_Bitmap.Height)

        ' 創建ColorMatrix，用於調整圖像亮度
        Dim brightenMatrix As New Imaging.ColorMatrix(New Single()() _
            {New Single() {0.3F, 0.3F, 0.3F, 0, 0},
             New Single() {0.59F, 0.59F, 0.59F, 0, 0},
             New Single() {0.11F, 0.11F, 0.11F, 0, 0},
             New Single() {0, 0, 0, 1, 0},
             New Single() {LowDimm, LowDimm, LowDimm, 0, 1}})

        ' 創建ImageAttributes對象，並設定ColorMatrix
        Dim imageAttributes As New Imaging.ImageAttributes()
        imageAttributes.SetColorMatrix(brightenMatrix)

        ' 創建Graphics對象，並使用ImageAttributes繪製目標圖像
        Dim graphics As Graphics = Graphics.FromImage(targetBitmap)
        graphics.DrawImage(Orig_Bitmap, New Rectangle(0, 0, Orig_Bitmap.Width, Orig_Bitmap.Height), 0, 0, Orig_Bitmap.Width, Orig_Bitmap.Height, GraphicsUnit.Pixel, imageAttributes)

        ' 釋放資源
        graphics.Dispose()

        ' 顯示調整後的圖像
        Return targetBitmap

    End Function

    Public Function Make_Button_HueChange(ByVal Orig_Bitmap As Bitmap, HueAngle As Single) As Bitmap

        ' 建立目標圖像的副本
        Dim r As Double = HueAngle * Math.PI / 180 ' degrees to radians

        ' 創建ColorMatrix，用於調整圖像亮度
        Dim theta As Single = HueAngle / 360 * 2 * Math.PI ' Degrees --> Radians
        Dim c As Single = Math.Cos(theta)
        Dim s As Single = Math.Sin(theta)

        Dim A00 As Single = 0.213 + 0.787 * c - 0.213 * s
        Dim A01 As Single = 0.213 - 0.213 * c + 0.413 * s
        Dim A02 As Single = 0.213 - 0.213 * c - 0.787 * s

        Dim A10 As Single = 0.715 - 0.715 * c - 0.715 * s
        Dim A11 As Single = 0.715 + 0.285 * c + 0.14 * s
        Dim A12 As Single = 0.715 - 0.715 * c + 0.715 * s

        Dim A20 As Single = 0.072 - 0.072 * c + 0.928 * s
        Dim A21 As Single = 0.072 - 0.072 * c - 0.283 * s
        Dim A22 As Single = 0.072 + 0.928 * c + 0.072 * s

        ' 建立目標圖像的副本
        Dim targetBitmap As New Bitmap(Orig_Bitmap.Width, Orig_Bitmap.Height)

        ' 創建ColorMatrix，用於調整圖像亮度
        Dim hueMatrix As New Imaging.ColorMatrix(New Single()() _
            {New Single() {A00, A01, A02, 0, 0},
             New Single() {A10, A11, A12, 0, 0},
             New Single() {A20, A21, A22, 0, 0},
             New Single() {0, 0, 0, 1, 0},
             New Single() {0, 0, 0, 0, 1}})

        ' 創建ImageAttributes對象，並設定ColorMatrix
        Dim imageAttributes As New Imaging.ImageAttributes()
        imageAttributes.SetColorMatrix(hueMatrix)

        ' 創建Graphics對象，並使用ImageAttributes繪製目標圖像
        Dim graphics As Graphics = Graphics.FromImage(targetBitmap)
        graphics.DrawImage(Orig_Bitmap, New Rectangle(0, 0, Orig_Bitmap.Width, Orig_Bitmap.Height), 0, 0, Orig_Bitmap.Width, Orig_Bitmap.Height, GraphicsUnit.Pixel, imageAttributes)

        ' 釋放資源
        graphics.Dispose()

        ' 顯示調整後的圖像
        Return targetBitmap


    End Function

    '============ Read-only mode

    Public LockFile As IO.StreamWriter
    Public NotLocked As Boolean
    Public LockFilePath As String
    Public PwdGrayImage As Bitmap = Make_Button_Gray(My.Resources.Resource1.TOPSEC)

    Function CreateLockFile() As Integer
        LockFilePath = DirName + "\FADB.lock"
        Try

            LockFile = IO.File.CreateText(LockFilePath)
            NotLocked = True
            Return 0

        Catch e As Exception

            Select Case e.HResult And &HFFFF
                Case 32
                    Return 1
                Case Else
                    Return 2
            End Select

        End Try
    End Function

End Module

'============= Mouse/KB detect for Auto Count down 

Public Class DetectActivityMessageFilter

    Implements IMessageFilter

    Public Const WM_KEYDOWN As Integer = &H100
    Public Const WM_MOUSEMOVE As Integer = &H200
    Public Const WM_LBUTTONDOWN As Integer = &H201
    Public Const WM_RBUTTONDOWN As Integer = &H204
    Public Const WM_MBUTTONDOWN As Integer = &H207

    Public Function PreFilterMessage(ByRef m As Message) As Boolean Implements IMessageFilter.PreFilterMessage
        Select Case m.Msg
            Case WM_LBUTTONDOWN, WM_KEYDOWN, WM_RBUTTONDOWN, WM_MBUTTONDOWN ', WM_MOUSEMOVE 'For Auto Close Countdown
                ACTCount = 0
        End Select
        Return False
    End Function

End Class

'=============== Clipboard using Win32 API

Public Class ClipboardHelper2
    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function OpenClipboard(hWndNewOwner As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function EmptyClipboard() As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function CloseClipboard() As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function SetClipboardData(uFormat As Integer, hMem As IntPtr) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Shared Function GlobalAlloc(uFlags As Integer, dwBytes As Integer) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Shared Function GlobalLock(hMem As IntPtr) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Shared Function GlobalUnlock(hMem As IntPtr) As Boolean
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Shared Function GlobalFree(hMem As IntPtr) As IntPtr
    End Function

    Private Const CF_UNICODETEXT As Integer = 13
    Private Const GMEM_MOVEABLE As Integer = &H2
    Private Const GMEM_ZEROINIT As Integer = &H40

    Public Function SetClipboardText(text As String) As Boolean
        If Not OpenClipboard(IntPtr.Zero) Then
            Return False
        End If

        EmptyClipboard()

        Dim hGlobal As IntPtr = GlobalAlloc(GMEM_MOVEABLE Or GMEM_ZEROINIT, (text.Length + 1) * 2)
        If hGlobal = IntPtr.Zero Then
            CloseClipboard()
            Return False
        End If

        Dim pText As IntPtr = GlobalLock(hGlobal)
        If pText = IntPtr.Zero Then
            GlobalFree(hGlobal)
            CloseClipboard()
            Return False
        End If

        Marshal.Copy(text.ToCharArray(), 0, pText, text.Length)
        GlobalUnlock(hGlobal)

        If SetClipboardData(CF_UNICODETEXT, hGlobal) = IntPtr.Zero Then
            GlobalFree(hGlobal)
            CloseClipboard()
            Return False
        End If

        CloseClipboard()
        Return True
    End Function
End Class
