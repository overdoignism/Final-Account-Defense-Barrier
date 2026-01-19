'MIT License 
'Copyright (c) 2023 overdoingism Labs. 
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
'Imports System.Security.Principal

Public Class FormLogin

    Public FormL, FormT, FormW, FormH As Integer

    Public This_Time_Key() As Byte
    Public This_Time_DIP() As Byte
    Public This_Time_Salt() As Byte
    Public This_Time_Dir As String = ""

    Friend Sys_Chk_Login As SystemChecklist

    Dim CapMonTimer As New Timer()
    Private AES_IV_Start() As Byte

    Public LoginStart As Boolean
    Public PassByte() As Byte
    Public Note2Str As String
    Public WorkMode As Integer '0=Login 1=NonLogin 2=Password 3=CSV Export

    Public PwdShow As Boolean = False
    Dim GP_Use_Symbol As Integer = 1 '1=All 2=Txt+Num 3=Num

    Public PwdState As Integer
    Dim TextBoxPwd2 As New MyTextBox
    Dim TextBoxPwdVerify2 As New MyTextBox

    Public Close_Clear_Clipper As Boolean
    Friend TempClipboardStr As String

    Dim ToolTip1 As New System.Windows.Forms.ToolTip()

    Dim Debug_Tester As New Timer()
    Dim WARN_Timer As New Timer()

    Dim KDF_Mode_ForExp As Integer = Sys_Chk_Login._KDF_Type

    Dim SSWorker As New SmallSecurtiyWorkers

    Private Sub FormLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Not Sys_Chk_Login.Screen_Capture_Allowed Then 'Disable Screen Capture
            SetWindowDisplayAffinity(Me.Handle, WDA_EXCLUDEFROMCAPTURE)
        End If

        Me.Opacity = Sys_Chk_Login._OpacitySng

        '===== BOX2 For fix Copy-past MRI
        Me.Controls.Add(TextBoxPwd2)
        TextBoxPwd2.Location = TextBoxPwd.Location
        TextBoxPwd2.Size = TextBoxPwd.Size
        TextBoxPwd2.Font = TextBoxPwd.Font
        TextBoxPwd2.ForeColor = TextBoxPwd.ForeColor
        TextBoxPwd2.BackColor = TextBoxPwd.BackColor
        TextBoxPwd2.BorderStyle = TextBoxPwd.BorderStyle
        TextBoxPwd2.PasswordChar = "●"
        TextBoxPwd2.BringToFront()
        AddHandler TextBoxPwd2.KeyDown, AddressOf TextBoxPwd2_KeyDown
        AddHandler TextBoxPwd2.TextChanged, AddressOf TextBoxPwd2_TextChanged

        Me.Controls.Add(TextBoxPwdVerify2)
        TextBoxPwdVerify2.Location = TextBoxPwdVerify.Location
        TextBoxPwdVerify2.Size = TextBoxPwdVerify.Size
        TextBoxPwdVerify2.Font = TextBoxPwdVerify.Font
        TextBoxPwdVerify2.ForeColor = TextBoxPwdVerify.ForeColor
        TextBoxPwdVerify2.BackColor = TextBoxPwdVerify.BackColor
        TextBoxPwdVerify2.BorderStyle = TextBoxPwdVerify.BorderStyle
        TextBoxPwdVerify2.PasswordChar = "●"
        TextBoxPwdVerify2.BringToFront()
        AddHandler TextBoxPwdVerify2.KeyDown, AddressOf TextBoxPwd2_KeyDown
        AddHandler TextBoxPwdVerify2.TextChanged, AddressOf TextBoxPwd2_TextChanged

        TextBoxPwd.Visible = False
        TextBoxPwdVerify.Visible = False
        TextBoxPLocked.Visible = False
        ButtonOK.Image = B_confirm_Dis

        ToolTip1.InitialDelay = 1

        Select Case WorkMode

            Case 0 ' Login Mode

                Dim t7 As New Threading.Thread(Sub() Load_Bitmap_For_Threads(7)) 'FormFileExp
                t7.Start()

                'Old Code: v1.3
                'PictureBoxLogin.Image = Image.FromStream(New IO.MemoryStream(My.Resources.Resource1.Title_NORMAL_PNG))
                Load_Bitmap_For_Threads(0)
                PictureBoxLogin.Image = FormLoginBitmap0

                ButtonCancel.Image = My.Resources.Resource1.button_Final

                If This_Time_Salt IsNot Nothing Then PictureSalt.Image = My.Resources.Resource1.SALT_ADD
                PicLang.Visible = True
                PictureSalt.Visible = True
                ButtonCancel.Visible = False
                ButtonFin.Visible = True
                ButtonPFWS.Visible = True

                'Multi Language

                If LIdx < 4 Then
                    ButtonRightArr.Image = B_LangR
                    ButtonRightArr.Visible = True
                    PicLang.Image = GetBitmapFromCode(LIdx)
                    PicLang.Visible = True
                Else
                    PicLangLoad.Top += 28
                    PicLangLoad.Visible = True
                End If

                ToolTip1.SetToolTip(TextBoxPwd2, LangStrs(LIdx, UsingTxt.Lo_Login_tt1))
                ToolTip1.SetToolTip(TextBoxPwdVerify2, LangStrs(LIdx, UsingTxt.Lo_Login_tt2))
                ToolTip1.SetToolTip(ButtonMIT, LangStrs(LIdx, UsingTxt.MIT_Tol))

                t7.Join()

            Case 1, 3 ' 1= Transform 3= CSV export

                PictureBoxLogin.Image = FormLoginBitmap13
                ToolTip1.SetToolTip(TextBoxPwd2, LangStrs(LIdx, UsingTxt.Lo_Login_tt1))
                ToolTip1.SetToolTip(TextBoxPwdVerify2, LangStrs(LIdx, UsingTxt.Lo_Login_tt2))

                ButtonMIT.Visible = False
                ButtonHAGP.Visible = False
                ButtonWinMin.Visible = False
                ButtonCancel.Visible = True
                ButtonFin.Visible = False

                If WorkMode = 3 Then
                    TextBoxPLocked.Text = "-- NOT USED IN CSV EXPORT MODE --"
                    TextBoxPLocked.TextAlign = HorizontalAlignment.Center
                    TextBoxPLocked.ForeColor = Color.FromArgb(126, 237, 176)
                    TextBoxPLocked.ReadOnly = True
                    TextBoxPLocked.Visible = True
                    TextBoxPLocked.SelectionStart = 0
                    TextBoxPwdVerify2.Visible = False
                    TextBoxPLocked.TabIndex = 2
                    TextBoxPwd2.TabIndex = 1
                    PicSelect.Visible = False
                    ButtonRightArr2.Visible = False
                    Me.Height = 334
                ElseIf WorkMode = 1 Then

                    Me.Height = 374
                    PicSelect.Top = 343
                    ButtonRightArr2.Top = 342
                    KDF_Mode_ForExp = Sys_Chk_Login._KDF_Type
                    PicSelect.Image = My.Resources.Resource1.KDFS_Types

                End If

                ButtonCaps.Top -= 201
                TextBoxPLocked.Top -= 201
                ButtonViewPass.Top -= 201
                TextBoxPwd2.Top -= 201
                TextBoxPwdVerify2.Top -= 201
                PictureSalt.Top -= 201

                ButtonOK.Top -= 201
                ButtonFileOpen.Top -= 201
                ButtonCancel.Top -= 201

                If This_Time_Salt IsNot Nothing Then PictureSalt.Visible = True

            Case 2 ' Password Mode

                ButtonMIT.Visible = False
                ButtonFileOpen.Visible = False
                ButtonWinMin.Visible = False
                ButtonCancel.Visible = True
                ButtonFin.Visible = False

                PictureBoxLogin.Image = FormLoginBitmap2
                Height = 374
                ButtonCaps.Top -= 201
                ButtonViewPass.Top -= 201
                TextBoxPwd2.Top -= 201
                TextBoxPwdVerify2.Top -= 201

                ButtonCancel.Top = 260
                ButtonCancel.Left = 147
                ButtonOK.Top -= 201

                ButtonHAGP.Visible = True
                ButtonHAGP.Image = B_genpwd
                ButtonHAGP.Top = 260
                ButtonHAGP.Left = 276

                PicSelect.Top = 343
                ButtonRightArr2.Top = 342

                ToolTip1.SetToolTip(TextBoxPwd2, LangStrs(LIdx, UsingTxt.TT_Pw))
                ToolTip1.SetToolTip(TextBoxPwdVerify2, LangStrs(LIdx, UsingTxt.TT_Pv))

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

        PicSelect.Image = My.Resources.Resource1.KDFS_Types

        If Sys_Chk_Login.Running_Admin Then
            ButtonPFWS.Enabled = True
            If Sys_Chk.Use_ATField = False Then
                ButtonPFWS.Image = B_PFWSoff
            Else
                ButtonPFWS.Image = B_PFWSon
            End If
        Else
            ButtonPFWS.Image = The_Shine_Visual_Filter2(My.Resources.Resource1.PFWS_RA)
        End If

        If Sys_Chk_Login.Use_Secure_Desktop = True Then
            Me.CenterToScreen()
            Me.ButtonWinMin.Visible = False
            TextBoxPwd2.SDMode = True
            TextBoxPwdVerify2.SDMode = True
            TextBoxPwd2.TempClipboardStr = TempClipboardStr
            TextBoxPwdVerify2.TempClipboardStr = TempClipboardStr
            TempClipboardStr = ""
        Else
            Me.StartPosition = FormStartPosition.Manual
            Me.Left = FormL + (FormW - Me.Width) / 2
            Me.Top = FormT + (FormH - Me.Height) / 2
        End If

        Me.Icon = FormMain.Icon

        CapMonTimer.Interval = 100
        CapMonTimer.Enabled = True
        AddHandler CapMonTimer.Tick, AddressOf CapMonTimer_Tick

        Debug_Tester.Interval = 1000
        Debug_Tester.Enabled = True
        AddHandler Debug_Tester.Tick, AddressOf Debug_Tester_Tick

        WARN_Timer.Interval = 500
        WARN_Timer.Enabled = False
        AddHandler WARN_Timer.Tick, AddressOf WARN_Timer_Tick

        SetForegroundWindow(Me.Handle)

        'Dim g As Graphics = Me.CreateGraphics()
        'MessageBox.Show(g.PageUnit.ToString())
        'Application.VisualStyleState = VisualStyles.VisualStyleState.NoneEnabled

    End Sub

    <DllImport("user32.dll")>
    Private Shared Function SetForegroundWindow(hWnd As IntPtr) As Boolean
    End Function

    Private Sub WARN_Timer_Tick(sender As Object, e As EventArgs)

        PicWarn.Visible = Not PicWarn.Visible

    End Sub

    Private Sub Debug_Tester_Tick(sender As Object, e As EventArgs)

        If Not Sys_Chk_Login.HasDebugger Then
            Sys_Chk_Login.HasDebugger = IsDebugged()
        End If

        If Sys_Chk_Login.HasDebugger And Not Sys_Chk_Login.HasDebugger_Warned Then
            Sys_Chk_Login.HasDebugger_Warned = True

            If Not Sys_Chk_Login.IsLinuxWine Then
                MSGBOXNEW(LangStrs(LIdx, UsingTxt.OT_dbg), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Cri), Me, PictureGray)
                PicWarn.Image = The_Shine_Visual_Filter2(My.Resources.Resource1.WARN_1)
                WARN_Timer.Enabled = True
            End If

        End If

        If Sys_Chk_Login.Found_Bad_MSFile And Not Sys_Chk_Login.Found_Bad_MSFile_Warned Then
            Sys_Chk_Login.Found_Bad_MSFile_Warned = True

            If Not Sys_Chk_Login.IsLinuxWine Then
                MSGBOXNEW(LangStrs(LIdx, UsingTxt.OT_bwf), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Cri), Me, PictureGray)
                PicWarn.Image = The_Shine_Visual_Filter2(My.Resources.Resource1.WARN_1)
                WARN_Timer.Enabled = True
            End If
        End If

        If Sys_Chk_Login.IsLinuxWine Then
            PicWarn.Image = The_Shine_Visual_Filter2(My.Resources.Resource1.WARN_2)
            If WorkMode = 0 Then WARN_Timer.Enabled = True
            Debug_Tester.Enabled = False
        End If


    End Sub

    '=============== Functions and subs 
    Private Sub GoNextPre()

        If TextBoxPwd2.Text.Length = 0 Then Exit Sub

        Select Case WorkMode

            Case 0, 3
                GoNext(System.Text.Encoding.Unicode.GetBytes(TextBoxPwd2.Text), False, Sys_Chk._KDF_Type)
            Case 1
                GoNext(System.Text.Encoding.Unicode.GetBytes(TextBoxPwd2.Text), False, KDF_Mode_ForExp)
            Case 2

                If String.CompareOrdinal(TextBoxPwd2.Text, TextBoxPwdVerify2.Text) <> 0 Then
                    If TextBoxPwdVerify2.Text = "" Then

                        Select Case SSWorker.CheckBIP39(TextBoxPwd2.Text)
                            Case 0
                            Case 1
                                MSGBOXNEW(LangStrs(LIdx, UsingTxt.PK_Pv1) + LangStrs(LIdx, UsingTxt.PK_Pv2), MsgBoxStyle.Exclamation, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
                                Exit Sub
                            Case 2
                                MSGBOXNEW(LangStrs(LIdx, UsingTxt.PK_Pv1) + LangStrs(LIdx, UsingTxt.PK_Pv3), MsgBoxStyle.Exclamation, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
                                Exit Sub
                            Case 3
                                MSGBOXNEW(LangStrs(LIdx, UsingTxt.PK_Pv1) + LangStrs(LIdx, UsingTxt.PK_Pv4), MsgBoxStyle.Exclamation, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
                                Exit Sub
                        End Select

                    Else
                        MSGBOXNEW(LangStrs(LIdx, UsingTxt.PK_Pvf), MsgBoxStyle.Exclamation, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
                        Exit Sub
                    End If
                End If

                PassByte = Security.Cryptography.ProtectedData.Protect(
                    System.Text.Encoding.UTF8.GetBytes(TextBoxPwd2.Text), Nothing, DataProtectionScope.CurrentUser)

                PwdState = 3
                Me.DialogResult = DialogResult.OK

        End Select

    End Sub

    Private Sub GoNext(ByRef BigByte() As Byte, ItsaFile As Boolean, KDF_Type As Integer)

        '=========Form KDF
        Dim FKDF As New FormKDF
        FKDF.PictureBoxKDF.Image = FormKDFBitmap 'Image.FromStream(New IO.MemoryStream(My.Resources.Resource1.PWD_KDF_PNG))
        FKDF.Opacity = Me.Opacity
        MakeWindowsMono(Me, PictureGray)
        Me.Enabled = False
        System.Windows.Forms.Application.DoEvents()
        '=========

        Dim FixSalt() As Byte = {0, 255, 1, 254, 2, 253, 3, 252}
        Dim SHA256_Worker As New Security.Cryptography.SHA256CryptoServiceProvider
        This_Time_Key = SHA256_Worker.ComputeHash(BigByte)
        WipeBytes(BigByte)

        Select Case KDF_Type

            Case 2 'Legacy

                Dim WorkArray02() As Byte

                WorkArray02 = This_Time_Key.Clone
                Array.Reverse(WorkArray02)

                If This_Time_Salt IsNot Nothing Then
                    Array.Resize(WorkArray02, WorkArray02.Length + This_Time_Salt.Length)
                    Array.Copy(This_Time_Salt, 0, WorkArray02, WorkArray02.Length - This_Time_Salt.Length, This_Time_Salt.Length)
                End If

                Dim t1 As New Threading.Thread(Sub() KDF_legacy_S256_1M(WorkArray02, FKDF.Progass10R, 0))
                Dim t2 As New Threading.Thread(Sub() KDF_legacy_S256_1M(This_Time_Key, FKDF.Progass10L, 1))
                t1.Start()
                t2.Start()
                FKDF.KDF_Type = KDF_Type
                FKDF.PicKDF_TYPE.Image = My.Resources.Resource1.KDFT_Types
                FKDF.ShowDialog(Me)
                t1.Join()
                t2.Join()

                This_Time_Key = SHA256_Worker.ComputeHash(This_Time_Key.Concat(WorkArray02).ToArray)
                WipeBytes(WorkArray02)

            Case 3 'RFC2898

                Dim WorkArray02() As Byte = This_Time_Key.Clone

                Dim Salt1() As Byte
                Dim Salt2() As Byte

                If This_Time_Salt Is Nothing Then
                    Salt1 = SHA256_Worker.ComputeHash(WorkArray02).Concat(FixSalt).ToArray
                Else
                    Salt1 = SHA256_Worker.ComputeHash(WorkArray02).Concat(This_Time_Salt).ToArray
                End If

                Salt2 = Salt1.Clone
                Array.Reverse(Salt2)

                Dim t1 As New Threading.Thread(Sub() KDF_UseRFC2898(WorkArray02, Salt2, FKDF.Progass10R))
                Dim t2 As New Threading.Thread(Sub() KDF_UseRFC2898(This_Time_Key, Salt1, FKDF.Progass10L))
                t1.Start()
                t2.Start()
                FKDF.KDF_Type = KDF_Type
                FKDF.PicKDF_TYPE.Image = My.Resources.Resource1.KDFT_Types
                FKDF.ShowDialog(Me)
                t1.Join()
                t2.Join()

                This_Time_Key = SHA256_Worker.ComputeHash(This_Time_Key.Concat(WorkArray02).ToArray)
                WipeBytes(WorkArray02)

            Case Else 'MAGI-Crypt

                Dim KDF_MAGIC As New FAST_KDF

                Dim WorkArray01(0), WorkArray02(0), WorkArray03(0), WorkArray04(0) As Byte

                If This_Time_Salt Is Nothing Then
                    This_Time_Key = This_Time_Key.Concat(FixSalt).ToArray
                Else
                    This_Time_Key = This_Time_Key.Concat(This_Time_Salt).ToArray
                End If

                KDF_MAGIC.Get_MAGIC_4Piece(This_Time_Key, WorkArray01, WorkArray02, WorkArray03, WorkArray04)

                ReDim KDF_MAGIC.TheFullBuck(KDF_MAGIC.TotalLengthUpper)

                Dim t1 As New Threading.Thread _
                    (Sub() KDF_MAGIC.KDF_MAGIcrypt(WorkArray01, FKDF.Progass10L, KDF_MAGIC.TheFullBuck, 0,
                                                  KDF_MAGIC.ErrCodeArray(0), Sys_Chk.Use_SE))
                Dim t2 As New Threading.Thread _
                    (Sub() KDF_MAGIC.KDF_MAGIcrypt(WorkArray02, FKDF.Progass10R, KDF_MAGIC.TheFullBuck, 1,
                                                  KDF_MAGIC.ErrCodeArray(1), Sys_Chk.Use_SE))
                Dim t3 As New Threading.Thread _
                    (Sub() KDF_MAGIC.KDF_MAGIcrypt(WorkArray03, FKDF.Progass10L2, KDF_MAGIC.TheFullBuck, 2,
                                                  KDF_MAGIC.ErrCodeArray(2), Sys_Chk.Use_SE))
                Dim t4 As New Threading.Thread _
                    (Sub() KDF_MAGIC.KDF_MAGIcrypt(WorkArray04, FKDF.Progass10R2, KDF_MAGIC.TheFullBuck, 3,
                                                  KDF_MAGIC.ErrCodeArray(3), Sys_Chk.Use_SE))
                t1.Start()
                t2.Start()
                t3.Start()
                t4.Start()
                FKDF.ShowDialog(Me)
                t1.Join()
                t2.Join()
                t3.Join()
                t4.Join()

                For IDX01 As Integer = 0 To 3
                    Select Case KDF_MAGIC.ErrCodeArray(IDX01)
                        Case 1
                            MSGBOXNEW(LangStrs(LIdx, UsingTxt.KDF_Me1), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
                            Me.Enabled = True
                            Exit Sub
                        Case 2
                            MSGBOXNEW(LangStrs(LIdx, UsingTxt.KDF_Me2), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
                            Me.Enabled = True
                            Exit Sub
                        Case 3
                            MSGBOXNEW(LangStrs(LIdx, UsingTxt.KDF_Me3), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
                            Me.Enabled = True
                            Exit Sub
                    End Select
                Next

                This_Time_Key = KDF_MAGIC.Finish_MAGIC_KDF(KDF_MAGIC.TheFullBuck)
                KDF_MAGIC.Dispose()

                FullGC()

        End Select

        UnMakeWindowsMono(PictureGray)
        FKDF.Dispose()

        '=====================================================================================

        Dim TheEncLib As New Encode_Libs
        Dim Work_Dir_String As String
        Dim Password_SHA256_x2() As Byte
        ReDim AES_IV_Start(15)

        Password_SHA256_x2 = SHA256_Worker.ComputeHash(This_Time_Key)
        Buffer.BlockCopy(Password_SHA256_x2, 0, AES_IV_Start, 0, 16)

        Security.Cryptography.ProtectedMemory.Protect(This_Time_Key, MemoryProtectionScope.SameProcess)

        Dim WorkDIP() As Byte = SHA256_Worker.ComputeHash(Password_SHA256_x2)
        ReDim This_Time_DIP(15)
        Buffer.BlockCopy(WorkDIP, 0, This_Time_DIP, 0, 16)

        Security.Cryptography.ProtectedMemory.Protect(This_Time_DIP, MemoryProtectionScope.SameProcess)

        'Dim DirWorkByte() As Byte = TheEncLib.AES_Encrypt_Byte_Return_Byte_For_Enter(SHA256_Worker.ComputeHash(Password_SHA256_x2), This_Time_Key, AES_IV_Start)
        'Work_Dir_String = Byte_to_x36(DirWorkByte)

        Work_Dir_String = TheEncLib.Get_Target_Dir(Password_SHA256_x2)

        WipeBytes(Password_SHA256_x2)

        Application.UseWaitCursor = False
        TheEncLib.Dispose()
        TheEncLib = Nothing
        SHA256_Worker.Dispose()
        SHA256_Worker = Nothing

        If Not System.IO.Directory.Exists(Work_Dir_String) Then

            If WorkMode = 3 Then
                Me.DialogResult = DialogResult.OK
                Exit Sub
            End If

            If Not ItsaFile Then
                If TextBoxPwdVerify2.Text = "" Then
                    MSGBOXNEW(LangStrs(LIdx, UsingTxt.Lo_NoMatch) + LangStrs(LIdx, UsingTxt.Lo_IfNew), MsgBoxStyle.Exclamation, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
                    Me.Enabled = True
                    Exit Sub
                End If

                If TextBoxPwd2.Text <> TextBoxPwdVerify2.Text Then
                    MSGBOXNEW(LangStrs(LIdx, UsingTxt.Lo_NoMatch) + LangStrs(LIdx, UsingTxt.Lo_AllNo), MsgBoxStyle.Exclamation, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
                    Me.Enabled = True
                    Exit Sub
                End If
            End If

            If MSGBOXNEW(LangStrs(LIdx, UsingTxt.Lo_NoMatch) + LangStrs(LIdx, UsingTxt.Lo_DoNew), MsgBoxStyle.OkCancel, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray) = MsgBoxResult.Ok Then

                Try
                    System.IO.Directory.CreateDirectory(Work_Dir_String)
                Catch ex As Exception
                    MSGBOXNEW(GetSimpleErrorMessage(ex.HResult, WorkType.FileC), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, Me.PictureGray)
                    Me.DialogResult = DialogResult.Cancel
                    Exit Sub
                End Try

            Else
                Me.Enabled = True
                Exit Sub
            End If

        End If

        This_Time_Dir = Work_Dir_String
        Me.DialogResult = DialogResult.OK

    End Sub


    '=============================KDF Function
    Private Sub KDF_UseRFC2898(ByRef Input_Datas() As Byte, Thesalt() As Byte, ByRef Prograss As Integer)

        Dim deriveBytes As Rfc2898DeriveBytes = Nothing

        'Dim A As Integer = Environment.TickCount

        For PrograssIDX As Integer = 1 To 10
            deriveBytes = New Rfc2898DeriveBytes(Input_Datas, Thesalt, 20000)
            Input_Datas = deriveBytes.GetBytes(32)
            Prograss = PrograssIDX * 10
        Next

        'MsgBox((Environment.TickCount - A).ToString)

        deriveBytes.Dispose()

    End Sub

    Private Sub KDF_legacy_S256_1M(ByRef Input_Array() As Byte, ByRef Prograss As Integer, ThreadIDX As Integer)

        Dim FK256 As New FAST_KDF

        FK256.InitBCrypt(FK256.AlgorithmPtr256, ThreadIDX)

        For Prograss = 1 To 100
            For IDX01 As Integer = 1 To 10000
                Input_Array = FK256.HashWithBCryptSHA256(Input_Array, ThreadIDX)
            Next
        Next

        FK256.EndBCrypt(ThreadIDX)

    End Sub


    '============== Form control works =================

    Private Sub ButtonRightArr2_Click(sender As Object, e As EventArgs) Handles ButtonRightArr2.Click

        If WorkMode = 2 Then

            GP_Use_Symbol += 1
            If GP_Use_Symbol >= 4 Then GP_Use_Symbol = 1
            PicSelect.Image = My.Resources.Resource1.PWD_Gen

        ElseIf WorkMode = 1 Then

            KDF_Mode_ForExp += 1
            If KDF_Mode_ForExp >= 4 Then KDF_Mode_ForExp = 1
            PicSelect.Image = My.Resources.Resource1.KDFS_Types

        ElseIf WorkMode = 0 Then

            Sys_Chk._KDF_Type += 1
            If Sys_Chk._KDF_Type >= 4 Then Sys_Chk._KDF_Type = 1
            PicSelect.Image = My.Resources.Resource1.KDFS_Types

        End If

    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        GoNextPre()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub ButtonHAGP_Click(sender As Object, e As EventArgs) Handles ButtonHAGP.Click

        If WorkMode = 0 Then
            If Sys_Chk_Login.Use_Secure_Desktop Then
                MSGBOXNEW(LangStrs(LIdx, UsingTxt.OT_SDnw), MsgBoxStyle.OkOnly, LangStrs(LIdx, UsingTxt.Ti_Info), Me, PictureGray)
            Else
                If Not Launch_URI(MainWebURL, Sys_Chk.Running_Admin, Sys_Chk.Found_Bad_MSFile) Then
                    MSGBOXNEW(LangStrs(LIdx, UsingTxt.Err_Unk), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
                End If
            End If
        Else

            Select Case GP_Use_Symbol
                Case 1
                    TextBoxPwd2.Text = Random_Strs(20, 22, GP_Use_Symbol)
                    TextBoxPwdVerify2.Text = TextBoxPwd2.Text
                Case 2
                    TextBoxPwd2.Text = Random_Strs(22, 24, GP_Use_Symbol)
                    TextBoxPwdVerify2.Text = TextBoxPwd2.Text
                Case 3
                    TextBoxPwd2.Text = Random_Strs(12, 12, GP_Use_Symbol)
                    TextBoxPwdVerify2.Text = TextBoxPwd2.Text
            End Select
        End If

    End Sub

    Private Sub ButtonMIT_Click(sender As Object, e As EventArgs) Handles ButtonMIT.Click

        Dim t1 As New Threading.Thread(Sub() Load_Bitmap_For_Threads(8)) 'FormMIT
        t1.Start()

        FormMIT.FormW = Me.Width
        FormMIT.FormH = Me.Height
        FormMIT.FormT = Me.Top
        FormMIT.FormL = Me.Left
        FormMIT.DocRight.Visible = True
        FormMIT.DocLeft.Visible = False
        FormMIT.Opacity = Sys_Chk_Login._OpacitySng
        FormMIT.NowPage = 0
        MakeWindowsMono(Me, PictureGray)
        t1.Join()
        FormMIT.PictureMIT.Image = FormMITBitmap
        FormMIT.ShowDialog()
        UnMakeWindowsMono(PictureGray)
        FullGC()

    End Sub

    Private Sub ButtonFileOpen_Click(sender As Object, e As EventArgs) Handles ButtonFileOpen.Click

        Dim FFE As New FormFileExplorer
        FFE.PicFileExp.Image = FormFileExpBitmap

        MakeWindowsMono(Me, PictureGray)
        FFE.Opacity = Me.Opacity

        If FFE.ShowDialog(Me) = DialogResult.OK Then
            FFE.Close()
            UnMakeWindowsMono(PictureGray)
            GoNext(FFE.BigByte, True, Sys_Chk._KDF_Type)
        Else
            UnMakeWindowsMono(PictureGray)
        End If
        FFE.Dispose()

        Exe_Fill_Trash()
        FullGC()

    End Sub

    Dim B_PWDSHOWon As New Bitmap(My.Resources.Resource1.PASSWORD_SHOW)
    Dim B_PWDSHOWoff As New Bitmap(My.Resources.Resource1.PASSWORD_HIDE)
    Dim B_PWDSHOWon_on As Bitmap = Make_Button_brighter(B_PWDSHOWon)
    Dim B_PWDSHOWoff_on As Bitmap = Make_Button_brighter(B_PWDSHOWoff)

    Dim B_LangR As Bitmap = My.Resources.Resource1.Lang_arr
    Dim B_LangR_on As New Bitmap(MakeRedArrow(B_LangR))

    Private Sub ButtonViewPass_Click(sender As Object, e As EventArgs) Handles ButtonViewPass.Click

        If Not PwdShow Then
            TextBoxPwdVerify2.PasswordChar = ControlChars.NullChar
            TextBoxPwd2.PasswordChar = ControlChars.NullChar
            ButtonViewPass.Image = B_PWDSHOWon_on
        Else
            TextBoxPwdVerify2.PasswordChar = "●"
            TextBoxPwd2.PasswordChar = "●"
            ButtonViewPass.Image = B_PWDSHOWoff_on
        End If

        PwdShow = Not PwdShow

        Exit Sub

    End Sub

    Private Sub ButtonArr_Click(sender As Object, e As EventArgs) Handles ButtonRightArr.Click

        LIdx += 1
        If LIdx = 4 Then LIdx = 0
        PicLang.Image = GetBitmapFromCode(LIdx)
        ToolTip1.SetToolTip(TextBoxPwd2, LangStrs(LIdx, UsingTxt.Lo_Login_tt1))
        ToolTip1.SetToolTip(TextBoxPwdVerify2, LangStrs(LIdx, UsingTxt.Lo_Login_tt2))
        ToolTip1.SetToolTip(ButtonMIT, LangStrs(LIdx, UsingTxt.MIT_Tol))

        If WorkMode = 0 Then
            Select Case sender.Name
                Case "ButtonLeftArr"
                    LIdx -= 1
                    If LIdx = -1 Then LIdx = 3
                Case "ButtonRightArr"

            End Select
        End If

    End Sub

    Private Sub FormLogin_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        If TextBoxPwd2.DetectedPasteIn Or TextBoxPwdVerify2.DetectedPasteIn Then
            Close_Clear_Clipper = False
            If MSGBOXNEW(LangStrs(LIdx, UsingTxt.OT_CPC), MsgBoxStyle.OkCancel, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray) = MsgBoxResult.Ok Then
                'My.Computer.Clipboard.Clear()
                Close_Clear_Clipper = True
            End If
        End If

        TextBoxPwd2.TempClipboardStr = ""
        TextBoxPwdVerify2.TempClipboardStr = ""

        Debug_Tester.Enabled = False
        Debug_Tester.Dispose()
        ClearTextBox(TextBoxPwd2)
        ClearTextBox(TextBoxPwdVerify2)
        TextBoxPwd2.Dispose()
        TextBoxPwdVerify2.Dispose()

    End Sub

    Private Sub FormLogin_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed

        SSWorker.Dispose()
        CapMonTimer.Enabled = False
        CapMonTimer.Dispose()
        WipeBytes(This_Time_DIP)
        WipeBytes(This_Time_Key)
        GC.SuppressFinalize(Me)
    End Sub

    '================= AT Filed (Protect From Windows Startup (PFWS))

    Dim B_PFWSon As Bitmap = The_Shine_Visual_Filter2(My.Resources.Resource1.PFWS_ON)
    Dim B_PFWSoff As Bitmap = The_Shine_Visual_Filter2(My.Resources.Resource1.PFWS_OFF)
    Dim B_PFWSon_on As Bitmap = Make_Button_brighter(B_PFWSon)
    Dim B_PFWSoff_on As Bitmap = Make_Button_brighter(B_PFWSoff)

    Private Sub ButtonPFWS_Click(sender As Object, e As EventArgs) Handles ButtonPFWS.Click

        Dim ChkErrCode1 As Integer
        Dim ChkErrCode2 As Integer

        Try

            If Not Sys_Chk.Use_ATField Then

                If MSGBOXNEW(LangStrs(LIdx, UsingTxt.AT_D), MsgBoxStyle.OkCancel, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray) = DialogResult.OK Then

                    ChkErrCode1 = INSTALL_FADB_TASK(TaskSch_Name_str)
                    If ChkErrCode1 = 0 Or ChkErrCode1 = 1 Then
                        ChkErrCode2 = START_FADB_TASK(TaskSch_Name_str)
                        MSGBOXNEW(LangStrs(LIdx, UsingTxt.AT_dn), MsgBoxStyle.OkOnly, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray)
                    Else
                        'Error
                        MSGBOXNEW(LangStrs(LIdx, UsingTxt.Ti_Err) + ":" + ChkErrCode2.ToString("x") + D_vbcrlf + LangStrs(LIdx, UsingTxt.AT_Df),
                                    MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
                    End If

                End If

            Else

                If MSGBOXNEW(LangStrs(LIdx, UsingTxt.AT_uD), MsgBoxStyle.OkCancel, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray) = DialogResult.OK Then
                    ChkErrCode1 = STOP_FADB_TASK(TaskSch_Name_str)
                    ChkErrCode2 = UNINSTALL_FADB_TASK(TaskSch_Name_str)
                    If ChkErrCode2 = 0 Or ChkErrCode2 = 1 Then
                        MSGBOXNEW(LangStrs(LIdx, UsingTxt.AT_dn), MsgBoxStyle.OkOnly, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray)
                    Else
                        MSGBOXNEW(LangStrs(LIdx, UsingTxt.Ti_Err) + ":" + ChkErrCode2.ToString("x") + D_vbcrlf + LangStrs(LIdx, UsingTxt.AT_Df),
                                  MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
                    End If
                End If

            End If
        Catch ex As Exception
            MSGBOXNEW(LangStrs(LIdx, UsingTxt.AT_Df), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err), Me, PictureGray)
        End Try


        Sys_Chk.Use_ATField = Check_Task_Exist(TaskSch_Name_str)

        If Sys_Chk.Use_ATField Then
            ButtonPFWS.Image = B_PFWSon
        Else
            ButtonPFWS.Image = B_PFWSoff
        End If

    End Sub

    '================= TextBox2 (For fix Copy-past MRI) ======= Works

    Private Sub TextBoxPwd2_KeyDown(sender As Object, e As KeyEventArgs)

        If (e.KeyCode = Keys.Enter) Then
            GoNextPre()
            e.Handled = True
            e.SuppressKeyPress = True
        End If

    End Sub

    Private Sub TextBoxPwd2_TextChanged(sender As Object, e As EventArgs)

        If Not ClearWorking Then
            sender.forecolor = SSWorker.EvaPwdStrong(sender)
            If TextBoxPwd2.Text.Length > 0 Then
                EnDisConfirmBtn(True)
            Else
                EnDisConfirmBtn(False)
            End If
        End If

    End Sub
    Private Sub EnDisConfirmBtn(En As Boolean)

        If En Then
            ButtonOK.Enabled = True
            ButtonOK.Image = B_confirm
        Else
            ButtonOK.Enabled = False
            ButtonOK.Image = B_confirm_Dis
        End If

    End Sub


    '=============== About CAPS Lock function

    Private MouseOnIt As Boolean

    Private B_CAPon As New Bitmap(My.Resources.Resource1.caps_lock_on)
    Private B_CAPoff As New Bitmap(My.Resources.Resource1.caps_lock_off)
    Private B_CAPon_on As Bitmap = Make_Button_brighter(B_CAPon)
    Private B_CAPoff_on As Bitmap = Make_Button_brighter(B_CAPoff)

    <DllImport("user32.dll")>
    Private Shared Sub keybd_event(ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As UInteger, ByVal dwExtraInfo As UInteger)
    End Sub

    Private Sub PicCAP1_Click(sender As Object, e As EventArgs) Handles ButtonCaps.Click
        Call keybd_event(System.Windows.Forms.Keys.CapsLock, &H14, 1, 0)
        Call keybd_event(System.Windows.Forms.Keys.CapsLock, &H14, 3, 0)
    End Sub

    Private Sub CapMonTimer_Tick(sender As Object, e As EventArgs)

        If MouseOnIt Then
            If Control.IsKeyLocked(Keys.CapsLock) Then
                ButtonCaps.Image = B_CAPon_on
            Else
                ButtonCaps.Image = B_CAPoff_on
            End If
        Else
            If Control.IsKeyLocked(Keys.CapsLock) Then
                ButtonCaps.Image = B_CAPon
            Else
                ButtonCaps.Image = B_CAPoff
            End If
        End If

    End Sub

    ' ====================== Base window operate

    Private lastLocation As Point
    Private isMouseDown As Boolean = False

    Private Sub PictureWinMin_Click(sender As Object, e As EventArgs) Handles ButtonWinMin.Click
        Me.WindowState = FormWindowState.Minimized
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

    Private Sub ButtonFin_Click(sender As Object, e As EventArgs) Handles ButtonFin.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    '================= Button visual work

    Dim B_confirm_Dis As Bitmap = Make_Button_Gray(My.Resources.Resource1.button_confirm)

    Dim B_Logout_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_LOGOUT)
    Dim B_Final_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_Final)
    Dim B_HELP_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_HELP)
    Dim B_confirm_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_confirm)
    Dim B_OpenF_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_OpenF)
    Dim B_Cancel_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_Cancel)
    Dim B_genpwd_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_genpwd)

    Dim B_NextClick As New Bitmap(My.Resources.Resource1.NextClick)
    Dim B_NextClick_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.NextClick)

    Dim B_Win_Min As New Bitmap(My.Resources.Resource1.WinMin)
    Dim B_Win_Min_on As Bitmap = Make_Button_brighter(B_Win_Min)

    Dim B_MIT As New Bitmap(My.Resources.Resource1.mit_Btn)
    Dim B_MIT_on As Bitmap = Make_Button_brighter(B_MIT)


    Private Sub Mouse_Enter(sender As Object, e As EventArgs) Handles _
        ButtonOK.MouseEnter, ButtonFileOpen.MouseEnter, ButtonFin.MouseEnter, ButtonCancel.MouseEnter,
        ButtonHAGP.MouseEnter, ButtonCaps.MouseEnter, ButtonViewPass.MouseEnter, ButtonPFWS.MouseEnter,
        ButtonPFWS.MouseEnter, ButtonRightArr2.MouseEnter, ButtonRightArr.MouseEnter, ButtonWinMin.MouseEnter,
        ButtonMIT.MouseEnter

        Select Case sender.Name
            Case "ButtonOK"
                ButtonOK.Image = B_confirm_on
            Case "ButtonFileOpen"
                ButtonFileOpen.Image = B_OpenF_on
            Case "ButtonFin"
                ButtonFin.Image = B_Final_on
            Case "ButtonCancel"
                ButtonCancel.Image = B_Cancel_on
            Case "ButtonHAGP"
                If WorkMode = 0 Then
                    ButtonHAGP.Image = B_HELP_on
                Else
                    ButtonHAGP.Image = B_genpwd_on
                End If
            Case "ButtonCaps"
                MouseOnIt = True
            Case "ButtonViewPass"
                If PwdShow Then
                    ButtonViewPass.Image = B_PWDSHOWon_on
                Else
                    ButtonViewPass.Image = B_PWDSHOWoff_on
                End If
            Case "ButtonPFWS"
                If Sys_Chk.Use_ATField Then
                    ButtonPFWS.Image = B_PFWSon_on
                Else
                    ButtonPFWS.Image = B_PFWSoff_on
                End If
            Case "ButtonRightArr"
                ButtonRightArr.Image = B_LangR_on
            Case "ButtonRightArr2"
                ButtonRightArr2.Image = B_NextClick_on
            Case "ButtonWinMin"
                ButtonWinMin.Image = B_Win_Min_on
            Case "ButtonMIT"
                ButtonMIT.Image = B_MIT_on
        End Select

    End Sub

    Dim B_confirm As New Bitmap(My.Resources.Resource1.button_confirm)
    Dim B_OpenF As New Bitmap(My.Resources.Resource1.button_OpenF)
    Dim B_Final As New Bitmap(My.Resources.Resource1.button_Final)
    Dim B_Cancel As New Bitmap(My.Resources.Resource1.button_Cancel)
    Dim B_HELP As New Bitmap(My.Resources.Resource1.button_HELP)
    Dim B_genpwd As New Bitmap(My.Resources.Resource1.button_genpwd)

    Private Sub Mouse_Leave(sender As Object, e As EventArgs) Handles _
        ButtonOK.MouseLeave, ButtonFileOpen.MouseLeave, ButtonFin.MouseLeave, ButtonCancel.MouseLeave,
        ButtonHAGP.MouseLeave, ButtonCaps.MouseLeave, ButtonViewPass.MouseLeave, ButtonPFWS.MouseLeave,
        ButtonPFWS.MouseLeave, ButtonRightArr2.MouseLeave, ButtonRightArr.MouseLeave, ButtonWinMin.MouseLeave,
        ButtonMIT.MouseLeave

        Select Case sender.Name
            Case "ButtonOK"
                ButtonOK.Image = B_confirm
            Case "ButtonFileOpen"
                ButtonFileOpen.Image = B_OpenF
            Case "ButtonFin"
                ButtonFin.Image = B_Final
            Case "ButtonCancel"
                ButtonCancel.Image = B_Cancel
            Case "ButtonHAGP"
                If WorkMode = 0 Then
                    ButtonHAGP.Image = B_HELP
                Else
                    ButtonHAGP.Image = B_genpwd
                End If
            Case "ButtonCaps"
                MouseOnIt = False
            Case "ButtonViewPass"
                If PwdShow Then
                    ButtonViewPass.Image = B_PWDSHOWon
                Else
                    ButtonViewPass.Image = B_PWDSHOWoff
                End If
            Case "ButtonPFWS"
                If Sys_Chk.Use_ATField Then
                    ButtonPFWS.Image = B_PFWSon
                Else
                    ButtonPFWS.Image = B_PFWSoff
                End If
            Case "ButtonRightArr"
                ButtonRightArr.Image = B_LangR
            Case "ButtonRightArr2"
                ButtonRightArr2.Image = B_NextClick
            Case "ButtonWinMin"
                ButtonWinMin.Image = B_Win_Min
            Case "ButtonMIT"
                ButtonMIT.Image = B_MIT
        End Select
    End Sub

    'fix WINE Compatible
    Private Sub Mouse_Down(sender As Object, e As EventArgs) Handles _
        ButtonOK.MouseDown, ButtonFileOpen.MouseDown, ButtonFin.MouseDown, ButtonCancel.MouseDown,
        ButtonHAGP.MouseDown, ButtonCaps.MouseDown, ButtonViewPass.MouseDown, ButtonPFWS.MouseDown,
        ButtonPFWS.MouseDown, ButtonRightArr2.MouseDown, ButtonRightArr.MouseDown, ButtonWinMin.MouseDown,
        ButtonMIT.MouseDown

        DirectCast(sender, Control).Capture = False

    End Sub

    Private Sub PicSelect_Paint(sender As Object, e As PaintEventArgs) Handles PicSelect.Paint

        If WorkMode = 2 Then
            Select Case GP_Use_Symbol
                Case 1
                    e.Graphics.DrawImage(My.Resources.Resource1.PWD_Gen, 0, 0)
                Case 2
                    e.Graphics.DrawImage(My.Resources.Resource1.PWD_Gen, 0, -22)
                Case 3
                    e.Graphics.DrawImage(My.Resources.Resource1.PWD_Gen, 0, -44)
            End Select
        ElseIf WorkMode = 1 Then
            Select Case KDF_Mode_ForExp
                Case 1
                    e.Graphics.DrawImage(My.Resources.Resource1.KDFS_Types, 0, 0)
                Case 2
                    e.Graphics.DrawImage(My.Resources.Resource1.KDFS_Types, 0, -22)
                Case 3
                    e.Graphics.DrawImage(My.Resources.Resource1.KDFS_Types, 0, -44)
            End Select
        ElseIf WorkMode = 0 Then
            Select Case Sys_Chk._KDF_Type
                Case 1
                    e.Graphics.DrawImage(My.Resources.Resource1.KDFS_Types, 0, 0)
                Case 2
                    e.Graphics.DrawImage(My.Resources.Resource1.KDFS_Types, 0, -22)
                Case 3
                    e.Graphics.DrawImage(My.Resources.Resource1.KDFS_Types, 0, -44)
            End Select
        End If

    End Sub


End Class

'===== BOX2 For fix Copy-past memory MRI

Class MyTextBox
    Inherits TextBox
    Public DetectedPasteIn As Boolean
    Public SDMode As Boolean
    Public TempClipboardStr As String = ""

    Protected Overrides Sub WndProc(ByRef m As Message)
        ' Trap WM_PASTE

        If m.Msg = &H302 Then
            If Not SDMode Then
                If Clipboard.ContainsText() Then
                    Me.Text = GetTextFromClipboard()
                    DetectedPasteIn = True
                End If
            Else
                If TempClipboardStr <> "" Then
                    Me.Text = TempClipboardStr
                    DetectedPasteIn = True
                End If
            End If
            Return
        End If

        MyBase.WndProc(m)

    End Sub

End Class


Module Login_Work

    Public Function LogInFormWork(TitleStr As String, ByRef ThisTimeDir As String, ByRef ThisTimeKey() As Byte,
                                  ByRef ThisTimeDIP() As Byte, WorkMode As Integer, PwdState As Integer,
                                  ByRef Sys_Chk_ As SystemChecklist, ByRef FFPicBox As PictureBox, ByRef WorkSalt() As Byte,
                                  Optional ByRef FirerForm As FormMain = Nothing) As DialogResult

        LogInFormWork = DialogResult.Cancel
        Dim LIFWRtn As DialogResult = DialogResult.Cancel
        Dim Close_Clear_Clipper As Boolean

        Dim LIFW = New FormLogin
        LIFW.Text = TitleStr
        LIFW.WorkMode = WorkMode
        LIFW.This_Time_Salt = WorkSalt
        LIFW.Sys_Chk_Login = Sys_Chk_

        Select Case WorkMode
            Case 0 '0=Start mode 
            Case 1 '1=Non Start mode
            Case 2 '2=Password mode
                LIFW.PassByte = CurrentAccountPass
                LIFW.PwdState = PwdState
        End Select

        If FirerForm Is Nothing Then

            'GetCurrentThreadId()
            'Dim ErrHappend As Boolean = False

            Dim Old_hDesktop As IntPtr = GetThreadDesktop(Process.GetCurrentProcess().Threads(0).Id)
            Dim New_hDesktop As IntPtr = CreateDesktop(
               System.Text.Encoding.Unicode.GetString(Security.Cryptography.ProtectedData.Unprotect(
               Sys_Chk_.Secure_Desktop_Name, Nothing, DataProtectionScope.CurrentUser)),
               IntPtr.Zero, IntPtr.Zero, 0, DESKTOP_ACCESS.GENERIC_ALL, IntPtr.Zero)

            If New_hDesktop = 0 Then
                MsgBox(LangStrs(LIdx, UsingTxt.OT_SDce) + " 0x" + Marshal.GetLastWin32Error.ToString("x") +
                       D_vbcrlf + LangStrs(LIdx, UsingTxt.Err_SDf), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err))
                Exit Function
            End If

            SwitchDesktop(New_hDesktop)

            Task.Factory.StartNew(Sub()

                                      LIFW.TempClipboardStr = GetTextFromClipboard()
                                      SetThreadDesktop(New_hDesktop)
                                      Try
                                          LIFWRtn = LIFW.ShowDialog()
                                      Catch ex As Exception
                                          'ErrHappend = True
                                      End Try
                                      SetThreadDesktop(Old_hDesktop)

                                  End Sub).Wait()

            SwitchDesktop(Old_hDesktop)
            CloseDesktop(New_hDesktop)

            'If ErrHappend Then
            '    MsgBox(LangStrs(LIdx, UsingTxt.OT_SDce) + " 0x" + Marshal.GetLastWin32Error.ToString("x") +
            '           D_vbcrlf + LangStrs(LIdx, UsingTxt.Err_SDf), MsgBoxStyle.Critical, LangStrs(LIdx, UsingTxt.Ti_Err))
            '    Exit Function
            'End If

        Else

            LIFW.FormW = FirerForm.Width
            LIFW.FormH = FirerForm.Height
            LIFW.FormT = FirerForm.Top
            LIFW.FormL = FirerForm.Left

            MakeWindowsMono(FirerForm, FFPicBox)
            LIFW.StartPosition = FormStartPosition.CenterParent
            LIFWRtn = LIFW.ShowDialog()
            UnMakeWindowsMono(FFPicBox)

        End If

        Select Case WorkMode

            Case 0, 1

                If LIFWRtn = DialogResult.OK Then
                    ThisTimeKey = LIFW.This_Time_Key.Clone
                    ThisTimeDir = LIFW.This_Time_Dir.Clone
                    ThisTimeDIP = LIFW.This_Time_DIP.Clone
                    If WorkMode = 0 Then
                        Sys_Chk_._LoginKeyStrength = LIFW.Sys_Chk_Login._LoginKeyStrength
                        Sys_Chk_.HasDebugger = LIFW.Sys_Chk_Login.HasDebugger
                        Sys_Chk_.Found_Bad_MSFile = LIFW.Sys_Chk_Login.Found_Bad_MSFile
                    End If
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

        Close_Clear_Clipper = LIFW.Close_Clear_Clipper
        LIFW.Close()
        LIFW.Dispose()
        GC.SuppressFinalize(LIFW)
        If Close_Clear_Clipper Then System.Windows.Forms.Clipboard.Clear()

    End Function

End Module
