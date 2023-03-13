'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Imports System.Runtime.InteropServices
Imports System.Security.Cryptography

Module Module1

    Public Structure FileLists
        Public FileName As String
        Public ShowName As String
        Public FileIsBad As Boolean
    End Structure

    Public AES_KEY_Protected() As Byte
    Public DERIVED_IDT_Protected() As Byte

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
    Public CurrentAccountPass() As Byte
    Public Read_File_Pass_Str As String

    Public PWDSHOWon As New Bitmap(My.Resources.Resource1.PASSWORD_SHOW)
    Public PWDSHOWoff As New Bitmap(My.Resources.Resource1.PASSWORD_HIDE)

    Public CAPon As New Bitmap(My.Resources.Resource1.caps_lock_on)
    Public CAPoff As New Bitmap(My.Resources.Resource1.caps_lock_off)

    Public ALLOPACITY As Single = 1.0F
    Public OSver As Integer

    Public Const MainWebURL As String = "https://github.com/overdoignism/Final-Account-Defense-Barrier"
    Public Const File_Limit As Long = 134217728

    Public SCutChar() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G",
        "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "-", "=",
        "[", "]", ";", "'", "<", ">", "/"}

    Public PWD_num_letter_only() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G",
        "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e",
        "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"}

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
        "Move Up.",
        "Move Down.",
        "Category settings updated.",
        "Title. This field is required.",'31
        "URL or program path.",
        "Username or cryptocurrency address.",
        "Password or cryptocurrency private key / mnemonic phrase.",
        "Password verification to prevent typos.",
        "Registed e-mail or cell phone number.",
        "Note 2. For invisible message.",
        "Filename:",'38 
        "Source and destination are the same.",
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
        "File write failed." + vbCrLf + vbCrLf + "Insufficient access rights or disk error.",
        "(Some error happend)",'67
        "Account ""$$$"" opened.",'68
        "Catalog password input", '69
        "--",
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
         "The password validation mismatch."
    }
    '"By Random Generator",
    '"By SHA512of128 (x1024)" '70

    Public HotKeyID1 As Integer = 1100
    Public HotKeyID2 As Integer = 2100

    Public Const KEY_W As Integer = &H57
    Public Const MOD_ALT As Integer = &H1
    Public Const MOD_SHIFT As Integer = &H4
    Public Const MOD_CTRL As Integer = &H2
    Public Const MOD_WIN As Integer = &H8
    Public Const WM_HOTKEY As Integer = &H312
    'Public Const WM_LBUTTONDOWN As Integer = &H201

    <DllImport("user32.dll")>
    Public Function RegisterHotKey(hWnd As IntPtr, id As Integer, fsModifiers As Integer, vlc As Integer) As Boolean
    End Function

    <DllImport("user32.dll")>
    Public Function UnregisterHotKey(hWnd As IntPtr, id As Integer) As Boolean
    End Function

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

        Try
            UnregisterHotKey(FormMain.Handle, HotKeyID1)
            UnregisterHotKey(FormMain.Handle, HotKeyID2)
        Catch ex As Exception

        End Try
        End

    End Sub

    Public Sub WipeBytes(ByRef Input_Bytes() As Byte)

        If Input_Bytes IsNot Nothing Then
            For IDX01 As Integer = 0 To UBound(Input_Bytes)
                Input_Bytes(IDX01) = 0
            Next

        End If

    End Sub

    Public Sub WipeChrs(ByRef Input_Chrs() As Char)

        For IDX01 As Integer = 0 To UBound(Input_Chrs)
            Input_Chrs(IDX01) = "x"
        Next

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
            For IDX01 As Long = 0 To LengthNeed - 1
                SpeedSB.Append(PWD_num_letter_only(Get_RangeRnd_ByRNG(0, UBound(PWD_num_letter_only))))
            Next
        End If

        Random_Strs = SpeedSB.ToString

    End Function

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

    Public Function MSGBOXNEW(MessageStr As String, BoxType As MsgBoxStyle, BoxTitle As String,
                              ByRef FirerForm As Form, ByRef FFPicBox As PictureBox) As DialogResult

        Dim NeoMSGBOX As New MSGBOXXX
        Dim ReturnDR As DialogResult

        NeoMSGBOX.MSGHEAD.Text = BoxTitle
        Select Case BoxType
            Case MsgBoxStyle.OkOnly
                NeoMSGBOX.ButtonOK.Visible = True
            Case MsgBoxStyle.OkCancel
                NeoMSGBOX.ButtonYes.Visible = True
                NeoMSGBOX.ButtonCancel.Visible = True
                NeoMSGBOX.PictureBox1.Image = New Bitmap(My.Resources.Resource1.Message_VRF)
                NeoMSGBOX.LabelMSG.Image = New Bitmap(My.Resources.Resource1.Message_VRF_TXTB)
            Case MsgBoxStyle.Exclamation
                NeoMSGBOX.ButtonOK.Visible = True
                NeoMSGBOX.PictureBox1.Image = New Bitmap(My.Resources.Resource1.Message_VRF)
                NeoMSGBOX.LabelMSG.Image = New Bitmap(My.Resources.Resource1.Message_VRF_TXTB)
            Case MsgBoxStyle.Critical
                NeoMSGBOX.ButtonCri.Visible = True
                NeoMSGBOX.PictureBox1.Image = New Bitmap(My.Resources.Resource1.Message_CRI)
                NeoMSGBOX.LabelMSG.Image = New Bitmap(My.Resources.Resource1.Message_CRI_TXTB)
            Case 65535
                NeoMSGBOX.ButtonYes.Visible = True
                NeoMSGBOX.ButtonYes.Enabled = False
                NeoMSGBOX.ButtonYes.Image = New Bitmap(My.Resources.Resource1.button_confirm_dis)
                NeoMSGBOX.ButtonCancel.Visible = True
                NeoMSGBOX.TextBoxDELETE.Visible = True
                NeoMSGBOX.PictureBox1.Image = New Bitmap(My.Resources.Resource1.Message_VRF)
                NeoMSGBOX.LabelMSG.Image = New Bitmap(My.Resources.Resource1.Message_VRF_TXTB)
        End Select

        NeoMSGBOX.LabelMSG.Text = MessageStr
        NeoMSGBOX.Opacity = ALLOPACITY

        MakeWindowsBlur(FirerForm, FFPicBox)
        ReturnDR = NeoMSGBOX.ShowDialog(FirerForm)
        UnMakeWindowsBlur(FFPicBox)

        NeoMSGBOX.Close()
        NeoMSGBOX.Dispose()
        NeoMSGBOX = Nothing

        Return ReturnDR

    End Function

    Public Sub RestartApp2(UseSecDesktop As Boolean, Runas As Boolean)

        Dim info As New ProcessStartInfo(Application.ExecutablePath)

        info.UseShellExecute = True

        If UseSecDesktop Then info.Arguments = "SECUREDESKTOP"
        If Runas Then info.Verb = "runas"

        Try
            Process.Start(info)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Threading.Thread.Sleep(500)
        End_Program()

    End Sub

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

    <DllImport("kernel32.dll")>
    Public Function GetCurrentThreadId() As Integer
    End Function

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
                                  NoNotice As Boolean, ByRef FFPicBox As PictureBox, ByRef WorkSalt() As Byte, Optional ByRef FirerForm As FormMain = Nothing) As DialogResult

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

            Dim Old_hDesktop As IntPtr = GetThreadDesktop(GetCurrentThreadId())
            Dim New_hDesktop As IntPtr = CreateDesktop(
               System.Text.Encoding.Unicode.GetString(Security.Cryptography.ProtectedData.Unprotect(
               SeuDeskName, Nothing, DataProtectionScope.CurrentUser)),
               IntPtr.Zero, IntPtr.Zero, 0, DESKTOP_ACCESS.GENERIC_ALL, IntPtr.Zero)

            If New_hDesktop = 0 Then
                MsgBox(TextStrs(6), MsgBoxStyle.Critical, TextStrs(5))
            End If

            SwitchDesktop(New_hDesktop)

            Task.Factory.StartNew(Sub()

                                      LIFW.SecureDesktopMode = True
                                      LIFW.IsUseSD = True
                                      SetThreadDesktop(New_hDesktop)
                                      LIFWRtn = LIFW.ShowDialog()
                                      SetThreadDesktop(Old_hDesktop)

                                  End Sub).Wait()

            SwitchDesktop(Old_hDesktop)
            CloseDesktop(New_hDesktop)

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
        End Select

        LIFW.Close()
        LIFW.Dispose()

    End Function

    Public Function EvaPwdStrong(ByRef InTextbox As TextBox) As Color

        Dim Score As Integer = 0
        Dim SNum As Boolean = False
        Dim SLow As Boolean = False
        Dim SUpp As Boolean = False
        Dim SSig As Boolean = False

        For Each TmpCHR As Char In InTextbox.Text
            Select Case TmpCHR
                Case "0" To "9"
                    SNum = True
                Case "a" To "z"
                    SLow = True
                Case "A" To "Z"
                    SUpp = True
                Case "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "+", "-", "=", "[", " "
                    SSig = True
                Case "]", "\", "{", "}", "|", ";", "'", ":", """", ",", ".", "/", "<", ">", "?"
                    SSig = True
            End Select
            Score += 1
        Next

        If SNum Then Score += 3
        If SLow Then Score += 3
        If SUpp Then Score += 3
        If SSig Then Score += 3

        If Score < 16 Then
            Return Color.FromArgb(172, 0, 0)
        ElseIf Score < 28 Then
            Return Color.FromArgb(124, 124, 0)
        Else
            Return Color.FromArgb(0, 172, 0)
        End If

    End Function

    Public Function Get_RangeRnd_ByRNG(minValue As Long, maxValue As Long) As Long

        ' 產生一個隨機數產生器
        Dim rng As New RNGCryptoServiceProvider()

        ' 產生一個 4 bytes 的隨機數字範圍 (0 ~ 4294967295)
        Dim randomBytes(3) As Byte
        rng.GetBytes(randomBytes)
        Dim randomNumber As Long = BitConverter.ToUInt32(randomBytes, 0)

        ' 將隨機數字轉換為指定範圍的整數
        Return CLng(Math.Floor((randomNumber / UInt32.MaxValue) * (maxValue - minValue + 1))) + minValue

    End Function

    Public Sub Exe_Fill_Trash(Optional BigNum As Integer = 1)
        Dim FillTrash() As String
        Dim Big2 As Integer = 32767 * BigNum

        For idx01 As Integer = 0 To Big2
            ReDim Preserve FillTrash(idx01)
            FillTrash(idx01) = New String("x", 8)
        Next
    End Sub

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

    Private Declare Function BitBlt Lib "gdi32" (ByVal hdcDest As IntPtr, ByVal nXDest As Integer,
    ByVal nYDest As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hdcSrc As IntPtr,
    ByVal nXSrc As Integer, ByVal nYSrc As Integer, ByVal dwRop As Integer) As Boolean

    Public Sub MakeWindowsBlur(ByRef WhatForm As Form, ByRef WhatImgToPut As PictureBox)

        Dim bmpOrg As New Bitmap(WhatForm.Width, WhatForm.Height)

        Using g1 As Graphics = Graphics.FromImage(bmpOrg)
            g1.CopyFromScreen(WhatForm.Location, Point.Empty, WhatForm.Size)
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

End Module

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