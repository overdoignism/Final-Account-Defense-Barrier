'MIT License 
'Copyright (c) 2023 overdoingism Labs. 
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Imports System.Security.Cryptography
Imports System.Security.Principal

Public Class FormLogin

    Public FormL, FormT, FormW, FormH As Integer
    Public This_Time_Key() As Byte
    Public This_Time_DIP() As Byte
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

    Dim RESTART_NOR As New Bitmap(My.Resources.Resource1.button_RESTART_Off)
    Dim RESTART_HOV As New Bitmap(My.Resources.Resource1.button_RESTART_On)

    Dim BT_FIN As New Bitmap(My.Resources.Resource1.button_Final)
    Dim BT_CANCEL As New Bitmap(My.Resources.Resource1.button_Cancel)

    Dim US_ON As New Bitmap(My.Resources.Resource1.USING_SYMBOL_ON)
    Dim US_OFF As New Bitmap(My.Resources.Resource1.USING_SYMBOL_OFF)
    Dim GP_Use_Symbol As Boolean = True

    Private Declare Sub keybd_event Lib "user32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As Integer, ByVal dwExtraInfo As Integer)

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click

        Select Case WorkMode
            Case 0, 1
                GoNext(System.Text.Encoding.Unicode.GetBytes(TextBoxPwd.Text), False)
            Case 2
                If TextBoxPwd.Text <> TextBoxPwdVerify.Text Then
                    MSGBOXNEW(TextStrs(82), MsgBoxStyle.Exclamation, TextStrs(5), Me, PictureGray)
                    Exit Sub
                End If

                PassByte = Security.Cryptography.ProtectedData.Protect(
                    System.Text.Encoding.UTF8.GetBytes(TextBoxPwd.Text), Nothing, DataProtectionScope.CurrentUser)

                PwdState = 3

                ClearTextBox(TextBoxPwd)
                ClearTextBox(TextBoxPwdVerify)

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
        Dim FSalt As New FormSalt
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

        'Dim PB_Value As Integer
        Dim t1 As New Threading.Thread(Sub() SecondSHA256Sub(SecondSHA256))
        Dim t2 As New Threading.Thread(Sub() MainSHA256Sub(Password_SHA256_As_Key, FSalt.Progass10))
        t1.Start()
        t2.Start()

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
                If TextBoxPwdVerify.Text = "" Then
                    MSGBOXNEW(TextStrs(0), MsgBoxStyle.Exclamation, TextStrs(5), Me, PictureGray)
                    Me.Enabled = True
                    Exit Sub
                End If

                If TextBoxPwd.Text <> TextBoxPwdVerify.Text Then
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
        ClearTextBox(TextBoxPwd)
        ClearTextBox(TextBoxPwdVerify)
        Me.DialogResult = DialogResult.OK

    End Sub

    Private Sub Form2_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        TextBoxPwd.Focus()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs)

        If Control.IsKeyLocked(Keys.CapsLock) Then
            PicCAP1.Image = CAPon
        Else
            PicCAP1.Image = CAPoff
        End If

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ClearTextBox(TextBoxPwd)
        ClearTextBox(TextBoxPwdVerify)

        Select Case WorkMode
            Case 0 ' Login Mode

                Me.ButtonCancel.Image = Me.BT_FIN

                Dim ToolTip1 As System.Windows.Forms.ToolTip = New System.Windows.Forms.ToolTip()
                ToolTip1.SetToolTip(TextBoxPwd, TextStrs(3))
                ToolTip1.SetToolTip(TextBoxPwdVerify, TextStrs(4))
                ToolTip1.InitialDelay = 1

                Dim RAAMode As Boolean = New WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator)
                PictureBoxRUNAS.Enabled = Not RAAMode
                If RAAMode Then PictureBoxRUNAS.Image = RUNASOn

            Case 1 ' Non Loging Mode
                Me.PictureBoxSD.Visible = False
                Me.PictureBoxRUNAS.Visible = False
                Me.PictureBoxMIT.Visible = False
                Me.Button_Restart.Visible = False
                Me.ButtonHelp.Visible = False
                Me.ButtonCancel.Image = Me.BT_CANCEL

                Me.PictureBoxLogin.Image = New Bitmap(My.Resources.Resource1.Title_NORMAL)
                Me.Height = 346
                Me.PicCAP1.Top -= 201
                Me.ButtonOK.Top -= 201
                Me.ButtonFileOpen.Top -= 201
                Me.ButtonViewPass.Top -= 201
                Me.TextBoxPwd.Top -= 201
                Me.TextBoxPwdVerify.Top -= 201
                Me.ButtonCancel.Top -= 201
            Case 2 ' Password Mode
                Me.PictureBoxSD.Visible = False
                Me.PictureBoxRUNAS.Visible = False
                Me.PictureBoxMIT.Visible = False
                Me.Button_Restart.Visible = False
                Me.ButtonHelp.Visible = False
                Me.ButtonFileOpen.Visible = False
                Me.ButtonCancel.Image = Me.BT_CANCEL

                Me.PictureBoxLogin.Image = New Bitmap(My.Resources.Resource1.Title_Password)
                Me.Height = 346
                Me.PicCAP1.Top -= 201
                Me.ButtonOK.Top -= 201
                Me.ButtonViewPass.Top -= 201
                Me.TextBoxPwd.Top -= 201
                Me.TextBoxPwdVerify.Top -= 201

                ButtonCancel.Top = 265
                ButtonCancel.Left = 148

                PictureBoxGenPwd.Visible = True
                PictureBoxGenPwd.Top = 265
                PictureBoxGenPwd.Left = 277

                PictureBoxGPUS.Left = 408
                PictureBoxGPUS.Top = 267
                PictureBoxGPUS.Visible = True

                Dim ToolTip1 As System.Windows.Forms.ToolTip = New System.Windows.Forms.ToolTip()
                ToolTip1.SetToolTip(TextBoxPwd, TextStrs(34))
                ToolTip1.SetToolTip(TextBoxPwdVerify, TextStrs(35))

                '0=New Account, CurrentAccountPass = "" in init
                '1=Old File Read, not decrypt
                '2=Old File Read, decrypted, CurrentAccountPass usable, Not edit
                '3=Edited

                Select Case PwdState
                    Case 0, 2, 3
                        TextBoxPwd.Text = System.Text.Encoding.UTF8.GetString(
                            Security.Cryptography.ProtectedData.Unprotect(PassByte, Nothing, DataProtectionScope.CurrentUser))
                        TextBoxPwdVerify.Text = TextBoxPwd.Text
                    Case 1
                        MsgBox("It should be mistake.")
                        Me.DialogResult = DialogResult.OK
                End Select

        End Select

        If SecureDesktopMode = True Then
            Me.CenterToScreen()
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

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub TextBoxPwd_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxPwd.KeyDown, TextBoxPwdVerify.KeyDown

        If (e.KeyCode = Keys.Enter) Then
            GoNext(System.Text.Encoding.Unicode.GetBytes(TextBoxPwd.Text), False)
            e.Handled = True
            e.SuppressKeyPress = True
        End If

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

    Private Sub TextBoxPwd_TextChanged(sender As Object, e As EventArgs) Handles TextBoxPwd.TextChanged
        If Not ClearWorking Then sender.forecolor = EvaPwdStrong(sender)
    End Sub

    Private Sub TextBoxPwdVerify_TextChanged(sender As Object, e As EventArgs) Handles TextBoxPwdVerify.TextChanged
        If Not ClearWorking Then sender.forecolor = EvaPwdStrong(sender)
    End Sub

    Private Sub ButtonFileOpen_Click(sender As Object, e As EventArgs) Handles ButtonFileOpen.Click

        If WorkMode <= 1 Then

            Dim FFE As New FormFileExplorer

            MakeWindowsBlur(Me, PictureGray)

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

    Private Sub PicCAP1_Click(sender As Object, e As EventArgs) Handles PicCAP1.Click
        Call keybd_event(System.Windows.Forms.Keys.CapsLock, &H14, 1, 0)
        Call keybd_event(System.Windows.Forms.Keys.CapsLock, &H14, 3, 0)
    End Sub

    Private Sub ButtonViewPass_Click(sender As Object, e As EventArgs) Handles ButtonViewPass.Click
        If TextBoxPwd.UseSystemPasswordChar Then
            TextBoxPwd.UseSystemPasswordChar = False
            TextBoxPwdVerify.UseSystemPasswordChar = False
            ButtonViewPass.Image = PWDSHOWon
        Else
            TextBoxPwd.UseSystemPasswordChar = True
            TextBoxPwdVerify.UseSystemPasswordChar = True
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
        TextBoxPwd.Text = Random_Strs(24, 24, PWDGMode)
        TextBoxPwdVerify.Text = TextBoxPwd.Text
    End Sub

    Private Sub PictureBoxGPUS_Click(sender As Object, e As EventArgs) Handles PictureBoxGPUS.Click
        If Not GP_Use_Symbol Then
            PictureBoxGPUS.Image = US_ON
        Else
            PictureBoxGPUS.Image = US_OFF
        End If
        GP_Use_Symbol = Not GP_Use_Symbol
    End Sub

    Private Sub Button_Restart_MouseHover(sender As Object, e As EventArgs) Handles Button_Restart.MouseHover
        Button_Restart.Image = RESTART_HOV
    End Sub

    Private Sub Button_Restart_MouseLeave(sender As Object, e As EventArgs) Handles Button_Restart.MouseLeave
        Button_Restart.Image = RESTART_NOR
    End Sub

    Private lastLocation As Point
    Private isMouseDown As Boolean = False

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim bmp As New Bitmap(Me.Width, Me.Height)
        Me.DrawToBitmap(bmp, New Rectangle(0, 0, Me.Width, Me.Height))
        PictureGray.Image = bmp
    End Sub

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

End Class