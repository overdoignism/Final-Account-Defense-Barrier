'MIT License 
'Copyright (c) 2023 overdoingism Labs. 
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports System.Security.Principal

Public Class FormLogin

    Public FormL, FormT, FormW, FormH As Integer

    Public This_Time_Key() As Byte
    Public This_Time_DIP() As Byte
    Public This_Time_Salt() As Byte

    Public This_Time_Dir As String

    Public SecureDesktopMode As Boolean
    Private SecondSHA256() As Byte
    Dim timerA As New Timer()
    Private AES_IV_Start() As Byte

    Public LoginStart As Boolean
    Public PassByte() As Byte
    Public Note2Str As String
    Public WorkMode As Integer '0=Login 1=NonLogin 2=Password
    Public PwdState As Integer

    Dim SDOn As New Bitmap(My.Resources.Resource1.SECURE_DESKTOP_ON)
    Dim SDOff As New Bitmap(My.Resources.Resource1.SECURE_DESKTOP_OFF)
    Public IsUseSD As Boolean

    Dim RUNASOn As New Bitmap(My.Resources.Resource1.RUN_AS_ADMIN_ON)
    Dim RUNASOff As New Bitmap(My.Resources.Resource1.RUN_AS_ADMIN_OFF)
    Public IsUseRUNAS As Boolean

    Dim BT_CANCEL As New Bitmap(My.Resources.Resource1.button_Cancel)

    Dim GP_Use_Symbol As Boolean = True

    Dim TextBoxPwd2 As New MyTextBox
    Dim TextBoxPwdVerify2 As New MyTextBox

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Controls.Add(TextBoxPwd2)
        TextBoxPwd2.Location = TextBoxPwd.Location
        TextBoxPwd2.UseSystemPasswordChar = TextBoxPwd.UseSystemPasswordChar
        TextBoxPwd2.Size = TextBoxPwd.Size
        TextBoxPwd2.Font = TextBoxPwd.Font
        TextBoxPwd2.ForeColor = TextBoxPwd.ForeColor
        TextBoxPwd2.BackColor = TextBoxPwd.BackColor
        TextBoxPwd2.BorderStyle = TextBoxPwd.BorderStyle
        TextBoxPwd2.BringToFront()
        AddHandler TextBoxPwd2.KeyDown, AddressOf TextBoxPwd_KeyDown
        AddHandler TextBoxPwd2.TextChanged, AddressOf TextBoxPwd_TextChanged

        Me.Controls.Add(TextBoxPwdVerify2)
        TextBoxPwdVerify2.Location = TextBoxPwdVerify.Location
        TextBoxPwdVerify2.UseSystemPasswordChar = TextBoxPwdVerify.UseSystemPasswordChar
        TextBoxPwdVerify2.Size = TextBoxPwdVerify.Size
        TextBoxPwdVerify2.Font = TextBoxPwdVerify.Font
        TextBoxPwdVerify2.ForeColor = TextBoxPwdVerify.ForeColor
        TextBoxPwdVerify2.BackColor = TextBoxPwdVerify.BackColor
        TextBoxPwdVerify2.BorderStyle = TextBoxPwdVerify.BorderStyle
        TextBoxPwdVerify2.BringToFront()
        AddHandler TextBoxPwdVerify2.KeyDown, AddressOf TextBoxPwd_KeyDown
        AddHandler TextBoxPwdVerify2.TextChanged, AddressOf TextBoxPwd_TextChanged

        Dim ToolTip1 As System.Windows.Forms.ToolTip = New System.Windows.Forms.ToolTip()
        ToolTip1.InitialDelay = 1

        Select Case WorkMode

            Case 0 ' Login Mode

                Me.ButtonCancel.Image = My.Resources.Resource1.button_Final
                ToolTip1.SetToolTip(TextBoxPwd2, TextStrs(3))
                ToolTip1.SetToolTip(TextBoxPwdVerify2, TextStrs(4))

                Dim RAAMode As Boolean = New WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator)
                PictureBoxRUNAS.Enabled = Not RAAMode
                If RAAMode Then PictureBoxRUNAS.Image = RUNASOn
                If This_Time_Salt IsNot Nothing Then PictureSalt.Image = My.Resources.Resource1.SALT_ADD
                If OSver < 62 Then PicturePMP.Image = New Bitmap(My.Resources.Resource1.PMP_OFF)
                Me.PicturePMP.Visible = True
                Me.PictureSalt.Visible = True

            Case 1 ' Non Loging Mode

                Me.PictureBoxLogin.Image = New Bitmap(My.Resources.Resource1.Title_NORMAL)
                ToolTip1.SetToolTip(TextBoxPwd2, TextStrs(3))
                ToolTip1.SetToolTip(TextBoxPwdVerify2, TextStrs(4))

                Me.PictureBoxSD.Visible = False
                Me.PictureBoxRUNAS.Visible = False
                Me.PictureBoxMIT.Visible = False
                Me.Button_Restart.Visible = False
                Me.ButtonHelp.Visible = False
                Me.PictureWinMin.Visible = False
                Me.ButtonCancel.Image = Me.BT_CANCEL

                Me.Height = 346
                Me.PicCAP1.Top -= 201
                Me.ButtonOK.Top -= 201
                Me.ButtonFileOpen.Top -= 201
                Me.ButtonViewPass.Top -= 201
                Me.TextBoxPwd2.Top -= 201
                Me.TextBoxPwdVerify2.Top -= 201
                Me.ButtonCancel.Top -= 201
                Me.PictureSalt.Top -= 201
                If This_Time_Salt IsNot Nothing Then PictureSalt.Visible = True

            Case 2 ' Password Mode

                Me.PictureBoxSD.Visible = False
                Me.PictureBoxRUNAS.Visible = False
                Me.PictureBoxMIT.Visible = False
                Me.Button_Restart.Visible = False
                Me.ButtonHelp.Visible = False
                Me.ButtonFileOpen.Visible = False
                Me.PictureWinMin.Visible = False
                Me.ButtonCancel.Image = Me.BT_CANCEL

                Me.PictureBoxLogin.Image = New Bitmap(My.Resources.Resource1.Title_Password)
                Me.Height = 346
                Me.PicCAP1.Top -= 201
                Me.ButtonOK.Top -= 201
                Me.ButtonViewPass.Top -= 201
                Me.TextBoxPwd2.Top -= 201
                Me.TextBoxPwdVerify2.Top -= 201

                ButtonCancel.Top = 265
                ButtonCancel.Left = 148

                PictureBoxGenPwd.Visible = True
                PictureBoxGenPwd.Top = 265
                PictureBoxGenPwd.Left = 277

                PictureBoxGPUS.Left = 408
                PictureBoxGPUS.Top = 267
                PictureBoxGPUS.Visible = True

                ToolTip1.SetToolTip(TextBoxPwd2, TextStrs(34))
                ToolTip1.SetToolTip(TextBoxPwdVerify2, TextStrs(35))

                '0=New Account, CurrentAccountPass = "" in init
                '1=Old File Read, not decrypt
                '2=Old File Read, decrypted, CurrentAccountPass usable, Not edit
                '3=Edited

                Select Case PwdState
                    Case 0, 2, 3
                        TextBoxPwd2.Text = System.Text.Encoding.UTF8.GetString(
                            Security.Cryptography.ProtectedData.Unprotect(PassByte, Nothing, DataProtectionScope.CurrentUser))
                        TextBoxPwdVerify2.Text = TextBoxPwd2.Text
                    Case 1
                        MsgBox("It should be mistake.")
                        Me.DialogResult = DialogResult.OK
                End Select

        End Select

        If SecureDesktopMode = True Then
            Me.CenterToScreen()
            Me.PictureWinMin.Visible = False
            PictureBoxSD.Image = SDOn
        Else
            Me.StartPosition = FormStartPosition.Manual
            Me.Left = FormL + (FormW - Me.Width) / 2
            Me.Top = FormT + (FormH - Me.Height) / 2
        End If

        timerA.Interval = 100
        timerA.Enabled = True
        AddHandler timerA.Tick, AddressOf Timer1_Tick

    End Sub

    Private Sub TextBoxPwd_KeyDown(sender As Object, e As KeyEventArgs)

        If (e.KeyCode = Keys.Enter) Then
            GoNextPre()
            e.Handled = True
            e.SuppressKeyPress = True
        End If

    End Sub

    Private Sub TextBoxPwd_TextChanged(sender As Object, e As EventArgs)
        If Not ClearWorking Then sender.forecolor = EvaPwdStrong(sender)
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        GoNextPre()
    End Sub

    Private Sub GoNextPre()

        Select Case WorkMode

            Case 0, 1
                GoNext(System.Text.Encoding.Unicode.GetBytes(TextBoxPwd2.Text), False)
            Case 2

                If String.CompareOrdinal(TextBoxPwd2.Text, TextBoxPwdVerify2.Text) <> 0 Then
                    If TextBoxPwdVerify2.Text = "" Then

                        Select Case CheckBIP39(TextBoxPwd2.Text)
                            Case 0
                            Case 1
                                MSGBOXNEW(TextStrs(83) + TextStrs(84), MsgBoxStyle.Exclamation, TextStrs(5), Me, PictureGray)
                                Exit Sub
                            Case 2
                                MSGBOXNEW(TextStrs(83) + TextStrs(85), MsgBoxStyle.Exclamation, TextStrs(5), Me, PictureGray)
                                Exit Sub
                            Case 3
                                MSGBOXNEW(TextStrs(83) + TextStrs(86), MsgBoxStyle.Exclamation, TextStrs(5), Me, PictureGray)
                                Exit Sub
                        End Select

                    Else
                        MSGBOXNEW(TextStrs(82), MsgBoxStyle.Exclamation, TextStrs(5), Me, PictureGray)
                        Exit Sub
                    End If
                End If

                PassByte = Security.Cryptography.ProtectedData.Protect(
                    System.Text.Encoding.UTF8.GetBytes(TextBoxPwd2.Text), Nothing, DataProtectionScope.CurrentUser)

                PwdState = 3
                Me.DialogResult = DialogResult.OK

        End Select

    End Sub

    Private Sub SecondSHA256Sub(ByRef Second_SHA256() As Byte)
        Dim SHA256_Worker As New Security.Cryptography.SHA256CryptoServiceProvider
        For SHA256Times As Integer = 1 To 1000000
            Second_SHA256 = SHA256_Worker.ComputeHash(Second_SHA256)
        Next
    End Sub

    Private Sub MainSHA256Sub(ByRef First_SHA256() As Byte, ByRef Prograss As Integer)
        Dim SHA256_Worker As New Security.Cryptography.SHA256CryptoServiceProvider
        For Prograss = 1 To 100
            For IDX01 As Integer = 1 To 10000
                First_SHA256 = SHA256_Worker.ComputeHash(First_SHA256)
            Next
        Next
    End Sub

    Private Sub GoNext(ByRef BigByte() As Byte, ItsaFile As Boolean)

        '=========
        Dim FSalt As New FormHASH
        MakeWindowsBlur(Me, PictureGray)
        Me.Enabled = False
        My.Application.DoEvents()
        '=========

        Dim SHA256_Worker As New Security.Cryptography.SHA256CryptoServiceProvider
        Dim Password_SHA256_As_Key() As Byte
        Dim Password_SHA256_x2() As Byte

        Password_SHA256_As_Key = SHA256_Worker.ComputeHash(BigByte)
        WipeBytes(BigByte)

        SecondSHA256 = Password_SHA256_As_Key.Clone
        Array.Reverse(SecondSHA256)

        If This_Time_Salt IsNot Nothing Then
            Array.Resize(SecondSHA256, SecondSHA256.Length + This_Time_Salt.Length)
            Array.Copy(This_Time_Salt, 0, SecondSHA256, SecondSHA256.Length - This_Time_Salt.Length, This_Time_Salt.Length)
        End If

        'Dim PB_Value As Integer
        Dim t1 As New Threading.Thread(Sub() SecondSHA256Sub(SecondSHA256))
        Dim t2 As New Threading.Thread(Sub() MainSHA256Sub(Password_SHA256_As_Key, FSalt.Progass10))
        t1.Start()
        t2.Start()

        FSalt.Opacity = Me.Opacity
        FSalt.ShowDialog(Me)

        t1.Join()
        t2.Join()

        Password_SHA256_As_Key = SHA256_Worker.ComputeHash(Password_SHA256_As_Key.Concat(SecondSHA256).ToArray)

        Dim TheEncLib As New Encode_Libs

        UnMakeWindowsBlur(PictureGray)
        FSalt.Dispose()

        '=====================================================================================

        Dim Work_String, Work_Dir_String As String

        ReDim AES_IV_Start(15)
        Password_SHA256_x2 = SHA256_Worker.ComputeHash(Password_SHA256_As_Key)
        Buffer.BlockCopy(Password_SHA256_x2, 0, AES_IV_Start, 0, 16)

        ReDim This_Time_Key(0)
        This_Time_Key = Security.Cryptography.ProtectedData.Protect(Password_SHA256_As_Key, Nothing, DataProtectionScope.CurrentUser)

        Dim WorkDIP() As Byte = SHA256_Worker.ComputeHash(Password_SHA256_x2)
        ReDim This_Time_DIP(15)
        Buffer.BlockCopy(WorkDIP, 0, This_Time_DIP, 0, 16)
        This_Time_DIP = Security.Cryptography.ProtectedData.Protect(This_Time_DIP, Nothing, DataProtectionScope.CurrentUser)

        WipeBytes(Password_SHA256_As_Key)
        WipeBytes(SecondSHA256)

        Work_String = TheEncLib.AES_Encrypt_Byte_Return_String(
            SHA256_Worker.ComputeHash(Password_SHA256_x2), This_Time_Key, AES_IV_Start)

        Work_Dir_String = Decimal_to_x36(CX16STR_2_DEC(Work_String.Substring(0, 24)), False)
        Work_Dir_String = Work_Dir_String + "-" + Decimal_to_x36(CX16STR_2_DEC(Work_String.Substring(24, 24)), False)
        Work_Dir_String = Work_Dir_String + "-" + Decimal_to_x36(CX16STR_2_DEC(Work_String.Substring(48, 16)), False)

        WipeBytes(Password_SHA256_x2)

        Application.UseWaitCursor = False
        TheEncLib.Dispose()
        TheEncLib = Nothing
        SHA256_Worker.Dispose()
        SHA256_Worker = Nothing

        If Not My.Computer.FileSystem.DirectoryExists(Work_Dir_String) Then

            If Not ItsaFile Then
                If TextBoxPwdVerify2.Text = "" Then
                    MSGBOXNEW(TextStrs(0), MsgBoxStyle.Exclamation, TextStrs(5), Me, PictureGray)
                    Me.Enabled = True
                    Exit Sub
                End If

                If TextBoxPwd2.Text <> TextBoxPwdVerify2.Text Then
                    MSGBOXNEW(TextStrs(1), MsgBoxStyle.Exclamation, TextStrs(5), Me, PictureGray)
                    Me.Enabled = True
                    Exit Sub
                End If
            End If

            If MSGBOXNEW(TextStrs(2), MsgBoxStyle.OkCancel, TextStrs(9), Me, PictureGray) = MsgBoxResult.Ok Then
                My.Computer.FileSystem.CreateDirectory(Work_Dir_String)
            Else
                Me.Enabled = True
                Exit Sub
            End If

        End If

        This_Time_Dir = Work_Dir_String
        Me.DialogResult = DialogResult.OK

    End Sub

    'Private Sub Form2_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
    'TextBoxPwd.Focus()
    'End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs)

        If Control.IsKeyLocked(Keys.CapsLock) Then
            PicCAP1.Image = CAPon
        Else
            PicCAP1.Image = CAPoff
        End If

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Button_Restart_Click(sender As Object, e As EventArgs) Handles Button_Restart.Click
        Me.DialogResult = DialogResult.Abort
    End Sub

    Private Sub ButtonHelp_Click(sender As Object, e As EventArgs) Handles ButtonHelp.Click

        If SecureDesktopMode Then
            MSGBOXNEW(TextStrs(24), MsgBoxStyle.OkOnly, TextStrs(16), Me, PictureGray)
        Else
            Try
                Process.Start(MainWebURL)
            Catch ex As Exception
            End Try
        End If

    End Sub

    Private Sub ButtonMIT_Click(sender As Object, e As EventArgs) Handles PictureBoxMIT.Click

        FormMIT.FormW = Me.Width
        FormMIT.FormH = Me.Height
        FormMIT.FormT = Me.Top
        FormMIT.FormL = Me.Left

        MakeWindowsBlur(Me, PictureGray)
        FormMIT.ShowDialog()
        UnMakeWindowsBlur(PictureGray)

    End Sub

    Private Sub ButtonFileOpen_Click(sender As Object, e As EventArgs) Handles ButtonFileOpen.Click

        If WorkMode <= 1 Then

            Dim FFE As New FormFileExplorer

            MakeWindowsBlur(Me, PictureGray)
            FFE.Opacity = Me.Opacity

            If FFE.ShowDialog(Me) = DialogResult.OK Then
                FFE.Close()
                UnMakeWindowsBlur(PictureGray)
                GoNext(FFE.BigByte, True)
            Else
                UnMakeWindowsBlur(PictureGray)
            End If
            FFE.Dispose()

            Exe_Fill_Trash()

            FullGC()
        End If

    End Sub

    Private Sub FormLogin_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        timerA.Enabled = False
        timerA.Dispose()
        WipeBytes(This_Time_DIP)
        WipeBytes(This_Time_Key)
    End Sub

    Private Declare Sub keybd_event Lib "user32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As Integer, ByVal dwExtraInfo As Integer)

    Private Sub PicCAP1_Click(sender As Object, e As EventArgs) Handles PicCAP1.Click
        Call keybd_event(System.Windows.Forms.Keys.CapsLock, &H14, 1, 0)
        Call keybd_event(System.Windows.Forms.Keys.CapsLock, &H14, 3, 0)
    End Sub

    Private Sub ButtonViewPass_Click(sender As Object, e As EventArgs) Handles ButtonViewPass.Click
        If TextBoxPwd2.UseSystemPasswordChar Then
            TextBoxPwd2.UseSystemPasswordChar = False
            TextBoxPwdVerify2.UseSystemPasswordChar = False
            ButtonViewPass.Image = PWDSHOWon
        Else
            TextBoxPwd2.UseSystemPasswordChar = True
            TextBoxPwdVerify2.UseSystemPasswordChar = True
            ButtonViewPass.Image = PWDSHOWoff
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBoxSD.Click
        If IsUseSD Then
            PictureBoxSD.Image = SDOff
        Else
            PictureBoxSD.Image = SDOn
        End If
        IsUseSD = Not IsUseSD
    End Sub

    Private Sub PictureBoxRUNAS_Click(sender As Object, e As EventArgs) Handles PictureBoxRUNAS.Click
        If IsUseRUNAS Then
            PictureBoxRUNAS.Image = RUNASOff
        Else
            PictureBoxRUNAS.Image = RUNASOn
        End If
        IsUseRUNAS = Not IsUseRUNAS
    End Sub

    Private Sub PictureBoxGenPwd_Click(sender As Object, e As EventArgs) Handles PictureBoxGenPwd.Click
        Dim PWDGMode As Integer
        If GP_Use_Symbol Then
            PWDGMode = 1
        Else
            PWDGMode = 2
        End If
        TextBoxPwd2.Text = Random_Strs(24, 24, PWDGMode)
        TextBoxPwdVerify2.Text = TextBoxPwd2.Text
    End Sub

    Private Sub PictureBoxGPUS_Click(sender As Object, e As EventArgs) Handles PictureBoxGPUS.Click
        If Not GP_Use_Symbol Then
            PictureBoxGPUS.Image = My.Resources.Resource1.USING_SYMBOL_ON
        Else
            PictureBoxGPUS.Image = My.Resources.Resource1.USING_SYMBOL_OFF
        End If
        GP_Use_Symbol = Not GP_Use_Symbol
    End Sub

    Private Sub Button_Restart_MouseHover(sender As Object, e As EventArgs) Handles Button_Restart.MouseHover
        Button_Restart.Image = My.Resources.Resource1.button_RESTART_On
    End Sub

    Private Sub Button_Restart_MouseLeave(sender As Object, e As EventArgs) Handles Button_Restart.MouseLeave
        Button_Restart.Image = My.Resources.Resource1.button_RESTART_Off
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim bmp As New Bitmap(Me.Width, Me.Height)
        Me.DrawToBitmap(bmp, New Rectangle(0, 0, Me.Width, Me.Height))
        PictureGray.Image = bmp
    End Sub

    Private Sub PictureWinMin_Click(sender As Object, e As EventArgs) Handles PictureWinMin.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private lastLocation As Point
    Private isMouseDown As Boolean = False

    Private Sub PictureBoxLogin_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBoxLogin.MouseDown
        ' 紀錄滑鼠按下的位置
        isMouseDown = True
        lastLocation = e.Location
    End Sub

    Private Sub PictureBoxLogin_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBoxLogin.MouseMove
        ' 當滑鼠左鍵按下時，設定窗體的位置
        If isMouseDown Then
            Me.Location = New Point(Me.Location.X + (e.X - lastLocation.X), Me.Location.Y + (e.Y - lastLocation.Y))
        End If
    End Sub

    Private Sub PictureBoxLogin_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBoxLogin.MouseUp
        ' 當滑鼠左鍵釋放時，重設 isMouseDown 變數
        isMouseDown = False
    End Sub

    Private Function CheckBIP39(ByRef BIP39Str As String) As Integer

        Dim LotInt() As UInteger

        Dim PassLevel As Integer = 0
        'PassLevel 0 : Pass
        'PassLevel 1 : Length not match
        'PassLevel 2 : Word not in list
        'PassLevel 3 : CheckSum error

        Dim WorkStr() As String = BIP39Str.ToLower.Split(" ")
        If WorkStr.Length Mod 3 <> 0 Then PassLevel = 1
        If WorkStr.Length > 24 Then PassLevel = 1

        If PassLevel = 1 Then 'Why Japanese word make trouble
            PassLevel = 0
            WorkStr = BIP39Str.Split("　")
            If WorkStr.Length Mod 3 <> 0 Then PassLevel = 1
            If WorkStr.Length > 24 Then PassLevel = 1
        End If

        If PassLevel = 0 Then

            For IDX01 As Integer = 0 To WorkStr.Length - 1

                ReDim Preserve LotInt(IDX01)
                PassLevel = 2

                For IDX02 As Integer = 0 To 9
                    For IDX03 As Integer = 0 To 2047
                        If String.CompareOrdinal(WorkStr(IDX01), BIP39_Word(IDX02).BIP39Word(IDX03)) = 0 Then
                            LotInt(IDX01) = IDX03
                            PassLevel = 0
                            Exit For
                        End If
                    Next
                    If PassLevel = 0 Then Exit For
                Next
                If PassLevel = 2 Then Exit For
            Next

        End If

        If PassLevel = 0 Then

            Dim LastDigi As UInteger
            Dim CheckSumVal As Byte
            Dim TheBIP39Tmp As UInteger
            Dim TBIP39IDX As Integer = 0
            Dim TmpBytes() As Byte
            Dim TheBIP39Bytes(((WorkStr.Length / 3) * 4) - 1) As Byte

            For IDX01 As Integer = 0 To WorkStr.Length - 1 Step 3

                TheBIP39Tmp = (LastDigi << (32 - TBIP39IDX))
                TheBIP39Tmp += (LotInt(IDX01) << (21 - TBIP39IDX))
                TheBIP39Tmp += (LotInt(IDX01 + 1) << (10 - TBIP39IDX))
                TheBIP39Tmp += (LotInt(IDX01 + 2) >> (1 + TBIP39IDX))

                LastDigi = LotInt(IDX01 + 2) Mod (2 ^ (TBIP39IDX + 1))

                TmpBytes = BitConverter.GetBytes(TheBIP39Tmp)
                Array.Reverse(TmpBytes)
                Array.Copy(TmpBytes, 0, TheBIP39Bytes, TBIP39IDX * 4, 4)
                TBIP39IDX += 1

            Next

            Dim SHA256_Worker As New Security.Cryptography.SHA256CryptoServiceProvider
            CheckSumVal = SHA256_Worker.ComputeHash(TheBIP39Bytes)(0) >> (8 - TBIP39IDX)
            SHA256_Worker.Dispose()

            If LastDigi <> CheckSumVal Then PassLevel = 3

        End If

        ReDim WorkStr(0)
        WipeUINT(LotInt)

        Return PassLevel

    End Function

    Private Function EvaPwdStrong(ByRef InTextbox As TextBox) As Color

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

    Private Sub FormLogin_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        If TextBoxPwd2.DetectedPasteIn Or TextBoxPwdVerify2.DetectedPasteIn Then
            If MSGBOXNEW(TextStrs(70), MsgBoxStyle.OkCancel, TextStrs(9), Me, PictureGray) = MsgBoxResult.Ok Then
                My.Computer.Clipboard.Clear()
            End If
        End If

        ClearTextBox(TextBoxPwd2)
        ClearTextBox(TextBoxPwdVerify2)
        TextBoxPwd2.Dispose()
        TextBoxPwdVerify2.Dispose()

    End Sub
End Class

Class MyTextBox
    Inherits TextBox
    Public DetectedPasteIn As Boolean
    Protected Overrides Sub WndProc(ByRef m As Message)
        ' Trap WM_PASTE
        If m.Msg = &H302 AndAlso Clipboard.ContainsText() Then
            Me.Text = GetText()
            'Clipboard.Clear()
            DetectedPasteIn = True
            Return
        End If
        MyBase.WndProc(m)
    End Sub

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function OpenClipboard(ByVal hWndNewOwner As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function CloseClipboard() As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function GetClipboardData(ByVal uFormat As UInteger) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Shared Function GlobalLock(ByVal hMem As IntPtr) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Shared Function GlobalUnlock(ByVal hMem As IntPtr) As Boolean
    End Function

    Public Shared Function GetText() As String
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

End Class
