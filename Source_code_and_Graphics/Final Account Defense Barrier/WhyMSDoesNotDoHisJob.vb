'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.Security.Cryptography.X509Certificates
Imports System.IO
Imports System.Threading.Tasks
Imports System.Management
Imports System.Security.Principal
Imports System


Module Common_Use

    Public Structure SystemChecklist
        Dim HasDebugger As Boolean
        Dim HasDebugger_Warned As Boolean
        Dim HideFromDebugger As Boolean
        Dim MitigationPolicyScore As Integer
        Dim OS_ver As Integer
        Dim Running_Admin As Boolean
        Dim Screen_Capture_Allowed As Boolean
        Dim Use_Secure_Desktop As Boolean
        Dim Secure_Desktop_Name() As Byte
        Dim Use_ATField As Boolean
        Dim Use_SE As Boolean
        Dim Use_AutoClose As Boolean
        Dim WER_Disabled As Boolean
        Dim Found_Bad_MSFile As Boolean
        Dim Found_Bad_MSFile_Warned As Boolean
        Dim IsLinuxWine As Boolean
        '以上與Windows機制有關
        Dim _Self_Ver_Str As String
        Dim _KDF_Type As Integer
        Dim _LoginKeyStrength As Integer
        '以上與其他安全相關
        '以下三個順便放進來
        Dim _OpacitySng As Single
        Dim _ReadOnlyMode As Boolean
    End Structure

    Public Sys_Chk As SystemChecklist
    Public ReadOnly Windows_ST_Path As String = Environment.GetFolderPath(Environment.SpecialFolder.System) & "\schtasks.exe"
    Public ReadOnly Windows_Exp_Path As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\explorer.exe"
    Public ReadOnly Windows_CMD_Path As String = Environment.GetFolderPath(Environment.SpecialFolder.System) & "\cmd.exe"
    Public ReadOnly Windows_Notepad_Path As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows) & "\notepad.exe"

    'Common API

    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Function CloseHandle(hObject As IntPtr) As Boolean
    End Function

End Module


Module Security_Risk_Reporter

    Public Function Make_Security_Report() As String

        Dim LineStr As String = StrDup(77, "=")
        Dim RIStr0 As String = "Risk Index"
        Dim RIStr1 As String = RIStr0 + ": "
        Dim LeftSp As String = "  "
        Dim ReportTxt As String

        ReportTxt = vbCrLf + " Final Account Defense Barrier " + Sys_Chk._Self_Ver_Str + " Security Report" + D_vbcrlf
        ReportTxt += " List of issues found:" + StrDup(22, " ") + "Generated at " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + vbCrLf
        ReportTxt += LineStr + vbCrLf

        Select Case Sys_Chk._LoginKeyStrength
            Case 0
                ReportTxt += LeftSp + "<Login Key Strength> is poor.".PadRight(54) + RIStr1 + " 60 (*5)" + vbCrLf
            Case 1
                ReportTxt += LeftSp + "<Login Key Strength> is moderate.".PadRight(54) + RIStr1 + " 30 (*5)" + vbCrLf
            Case 2
                ReportTxt += LeftSp + "<Login Key Strength> is high.".PadRight(54) + RIStr1 + "  0" + vbCrLf
        End Select

        Select Case Sys_Chk._KDF_Type
            Case 2
                ReportTxt += LeftSp + "<KDF mode: Leagcy (Type 2) > is poor.".PadRight(54) + RIStr1 + " 30 (*5)" + vbCrLf
            Case 3
                ReportTxt += LeftSp + "<KDF mode: RFC2898 (Type 3) > is moderate.".PadRight(54) + RIStr1 + " 10 (*5)" + vbCrLf
            Case Else
                ReportTxt += LeftSp + "<KDF mode: MAGI-Crypt (Type 1) > is strong.".PadRight(54) + RIStr1 + "  0" + vbCrLf
        End Select

        If Not Sys_Chk.Use_SE Then
            ReportTxt += LeftSp + "<SeLockMemoryPrivilege> not in use.".PadRight(54) + RIStr1 + " 10 (*1,5)" + vbCrLf
        Else
            ReportTxt += LeftSp + "<SeLockMemoryPrivilege> is present.".PadRight(54) + RIStr1 + "  0" + vbCrLf
        End If

        If Not Sys_Chk.WER_Disabled Then
            ReportTxt += LeftSp + "<Windows Error Report> is not disabled.".PadRight(54) + RIStr1 + " 10 (*3)" + vbCrLf
        Else
            ReportTxt += LeftSp + "<Windows Error Report> is disabled.".PadRight(54) + RIStr1 + "  0" + vbCrLf
        End If

        If Not Sys_Chk.HideFromDebugger Then
            ReportTxt += LeftSp + "<Hide From Debugger> is not enabled.".PadRight(54) + RIStr1 + " 10 (*3)" + vbCrLf
        Else
            ReportTxt += LeftSp + "<Hide From Debugger> is enabled.".PadRight(54) + RIStr1 + "  0" + vbCrLf
        End If

        If Not Sys_Chk.Use_AutoClose Then
            ReportTxt += LeftSp + "<Auto log out when idle> is not enabled.".PadRight(54) + RIStr1 + " 20 (*5)" + vbCrLf
        Else
            ReportTxt += LeftSp + "<Auto log out when idle> is enabled.".PadRight(54) + RIStr1 + "  0" + vbCrLf
        End If

        If Not Sys_Chk.Use_ATField Then
            ReportTxt += LeftSp + "<A.T. Field> is undeployed.".PadRight(54) + RIStr1 + " 30 (*5)" + vbCrLf
        Else
            ReportTxt += LeftSp + "<A.T. Field> is deployed.".PadRight(54) + RIStr1 + "  0" + vbCrLf
        End If

        If Sys_Chk.Screen_Capture_Allowed Then
            ReportTxt += LeftSp + "<Screen Capture> is allowed.".PadRight(54) + RIStr1 + " 40 (*5)" + vbCrLf
        Else
            ReportTxt += LeftSp + "<Screen Capture> is not allowed.".PadRight(54) + RIStr1 + "  0" + vbCrLf
        End If

        If Not Sys_Chk.Use_Secure_Desktop Then
            ReportTxt += LeftSp + "<Secure Desktop> is not enabled.".PadRight(54) + RIStr1 + " 50 (*5)" + vbCrLf
        Else
            ReportTxt += LeftSp + "<Secure Desktop> is enabled.".PadRight(54) + RIStr1 + "  0" + vbCrLf
        End If

        If Not Sys_Chk.Running_Admin Then
            ReportTxt += LeftSp + "<Admin permissions> is not in use.".PadRight(54) + RIStr1 + "100 (*5)" + vbCrLf
        Else
            ReportTxt += LeftSp + "<Admin permissions> is present.".PadRight(54) + RIStr1 + "  0" + vbCrLf
        End If

        If Sys_Chk.OS_ver > 62 And Sys_Chk.OS_ver < 100 Then
            ReportTxt += LeftSp + "<Windows version> is older.".PadRight(54) + RIStr1 + " 30 (*6)" + vbCrLf
        ElseIf Sys_Chk.OS_ver <= 62 Then
            ReportTxt += LeftSp + "<Windows version> is too old.".PadRight(54) + RIStr1 + " 50 (*6)" + vbCrLf
        Else
            ReportTxt += LeftSp + "<Windows version> is fine.".PadRight(54) + RIStr1 + "  0" + vbCrLf
        End If

        If Sys_Chk.MitigationPolicyScore > 0 And Sys_Chk.MitigationPolicyScore < 9 Then
            ReportTxt += LeftSp + "<Mitigation Policy> are not fully functional.".PadRight(54) + RIStr1 + " 30 (*2)" + vbCrLf
        ElseIf Sys_Chk.MitigationPolicyScore = 0 Then
            ReportTxt += LeftSp + "<Mitigation Policy> are not work at all.".PadRight(54) + RIStr1 + " 60 (*2,3)" + vbCrLf
        Else
            ReportTxt += LeftSp + "<Mitigation Policy> are fully functional.".PadRight(54) + RIStr1 + "  0" + vbCrLf
        End If

        If Sys_Chk.HasDebugger Then
            ReportTxt += LeftSp + "<Debugger> has been detected.".PadRight(54) + RIStr1 + "430 (*4)" + vbCrLf
        Else
            ReportTxt += LeftSp + "<Debugger> has not been detected.".PadRight(54) + RIStr1 + "  0" + vbCrLf
        End If

        If Sys_Chk.Found_Bad_MSFile Then
            ReportTxt += LeftSp + "<Bad Windows file> has been detected.".PadRight(54) + RIStr1 + "430 (*4)" + vbCrLf
        Else
            ReportTxt += LeftSp + "<Bad Windows file> has no detected.".PadRight(54) + RIStr1 + "  0" + vbCrLf
        End If

        ReportTxt += LineStr + vbCrLf
        ReportTxt += StrDup(55, " ") + "Total " + RIStr1 + Get_Risk_Index.ToString.PadLeft(3) + D_vbcrlf

        ReportTxt += LeftSp + "(*1) Need admin permissions and windows setting." + vbCrLf
        ReportTxt += LeftSp + "(*2) Maybe windows version is older." + vbCrLf
        ReportTxt += LeftSp + "(*3) This setting has limited impact, but seems unreasonable." + vbCrLf +
                     LeftSp + "     (Hidden issues or attacks might be present!)" + vbCrLf
        ReportTxt += LeftSp + "(*4) Unless you're testing, this is likely a malicious attack!!" + vbCrLf
        ReportTxt += LeftSp + "(*5) This option is user-controlled." + vbCrLf
        ReportTxt += LeftSp + "(*6) The latest version of Windows is always recommended." + vbCrLf +
                     LeftSp + "     (At least Windows 10 22H2 or higher.)" + D_vbcrlf

        ReportTxt += LeftSp + RIStr0 + " =   0  Fullly protected. [Green]" + vbCrLf
        ReportTxt += LeftSp + RIStr0 + " <  50  Almost protected. [Lime]" + vbCrLf
        ReportTxt += LeftSp + RIStr0 + " < 100  Limited protected. [Yello]" + vbCrLf
        ReportTxt += LeftSp + RIStr0 + " > 100  There is significant risk. [Orange]" + vbCrLf
        ReportTxt += LeftSp + RIStr0 + " > 420  Maybe under malicious attack! [Red]" + vbCrLf

        ReportTxt += vbCrLf + " This report is for evaluation and reference only." + vbCrLf +
         " System security still depends on good security practices and regular updates."

        Return ReportTxt

    End Function

    Public Function Get_Risk_Index() As Integer

        Dim RiskIDX As Integer = 0

        Select Case Sys_Chk._LoginKeyStrength
            Case 0
                RiskIDX += 60
            Case 1
                RiskIDX += 30
        End Select

        Select Case Sys_Chk._KDF_Type
            Case 2
                RiskIDX += 30
            Case 3
                RiskIDX += 10
        End Select

        If Not Sys_Chk.Use_SE Then
            RiskIDX += 10
        End If

        If Not Sys_Chk.WER_Disabled Then
            RiskIDX += 10
        End If

        If Not Sys_Chk.HideFromDebugger Then
            RiskIDX += 10
        End If

        If Not Sys_Chk.Use_AutoClose Then
            RiskIDX += 20
        End If

        If Not Sys_Chk.Use_ATField Then
            RiskIDX += 30
        End If

        If Sys_Chk.Screen_Capture_Allowed Then
            RiskIDX += 40
        End If

        If Not Sys_Chk.Use_Secure_Desktop Then
            RiskIDX += 50
        End If

        If Not Sys_Chk.Running_Admin Then
            RiskIDX += 100
        End If

        If Sys_Chk.OS_ver > 62 And Sys_Chk.OS_ver < 100 Then
            RiskIDX += 30
        ElseIf Sys_Chk.OS_ver <= 62 Then
            RiskIDX += 50
        End If

        If Sys_Chk.MitigationPolicyScore > 0 And Sys_Chk.MitigationPolicyScore < 9 Then
            RiskIDX += 30
        ElseIf Sys_Chk.MitigationPolicyScore = 0 Then
            RiskIDX += 60
        End If

        If Sys_Chk.HasDebugger Then
            RiskIDX += 430
        End If

        If Sys_Chk.Found_Bad_MSFile Then
            RiskIDX += 430
        End If

        Return RiskIDX

    End Function

    Public Function Get_Risk_Color(RiskIDX As Integer) As Color

        Select Case RiskIDX
            Case 0
                Return Color.FromArgb(0, 254, 0)
            Case < 50
                Return Color.FromArgb(168, 238, 0)
            Case < 100
                Return Color.FromArgb(238, 238, 0)
            Case < 400
                Return Color.FromArgb(238, 168, 0)
            Case Else
                Return Color.FromArgb(255, 0, 0)
        End Select

    End Function

    Public Function Get_Risk_Message(RiskIDX As Integer) As String

        If Sys_Chk.IsLinuxWine Then
            Return "Wine detected.".ToUpper
        End If


        Select Case RiskIDX
            Case 0
                Return "Fullly protected".ToUpper
            Case < 50
                Return "Almost protected".ToUpper
            Case < 100
                Return "Limited protected".ToUpper
            Case < 400
                Return "Significant risk".ToUpper
            Case Else
                Return "Maybe under malicious attack!".ToUpper
        End Select

    End Function

End Module

Module AT_Field

    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Function SetProcessShutdownParameters(ByVal dwLevel As UInteger, ByVal dwFlags As UInteger) As Boolean
    End Function

    Public Function Check_Task_Exist(taskName As String) As Boolean

        If Not IO.File.Exists(Windows_ST_Path) Then Return False

        If Not Is_Legal_MS_File(Windows_ST_Path) Then
            Sys_Chk.Found_Bad_MSFile = True
            Return False
        End If

        Dim process As New Process()
        process.StartInfo.FileName = Windows_ST_Path
        process.StartInfo.Arguments = "/Query /TN """ & taskName & """"
        process.StartInfo.RedirectStandardOutput = True
        process.StartInfo.UseShellExecute = False
        process.StartInfo.CreateNoWindow = True

        Try
            ' 啟動process
            process.Start()
            Threading.Thread.Sleep(200)
            ' 讀取輸出
            Dim output As String = process.StandardOutput.ReadToEnd()
            process.WaitForExit(100)

            ' 檢查輸出中是否包含指定的工作名稱
            If output.Contains(taskName) Then Return True

        Catch ex As Exception
        End Try

        Return False

    End Function

    Public Function INSTALL_FADB_TASK(taskName As String) As Integer

        If Not Is_Legal_MS_File(Windows_ST_Path) Then
            Sys_Chk.Found_Bad_MSFile = True
            Return 2
        End If

        Dim memoryStream As New IO.MemoryStream()
        Dim xmlContent As String = ""

        Using archive As New System.IO.Compression.ZipArchive(New IO.MemoryStream(My.Resources.Resource1.TXTFile))
            For Each entry As System.IO.Compression.ZipArchiveEntry In archive.Entries
                If entry.Name = "FADB.xml" Then
                    Using entryStream As IO.Stream = entry.Open()
                        entryStream.CopyTo(memoryStream)
                        memoryStream.Position = 0
                        Dim sr As New IO.StreamReader(memoryStream, True)
                        xmlContent = sr.ReadToEnd()
                    End Using
                    Exit For
                End If
            Next
        End Using

        xmlContent = Replace(xmlContent, "$$PATH$$", System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)

        Dim tempFile As String = Path.Combine(Path.GetTempPath(), "tempTask.xml")

        File.WriteAllText(tempFile, xmlContent)

        Dim process As New Process()

        process.StartInfo.FileName = Environment.GetEnvironmentVariable("SystemRoot") & "\System32\schtasks.exe"
        process.StartInfo.Arguments = "/Create /TN """ + taskName + """ /Xml """ + tempFile + """ /HRESULT /F"
        process.StartInfo.RedirectStandardOutput = True
        process.StartInfo.UseShellExecute = False
        process.StartInfo.CreateNoWindow = True

        Try

            process.Start()
            process.WaitForExit(500)

            File.Delete(tempFile)

            Return process.ExitCode

        Catch ex As Exception

        End Try

        Return 1

    End Function

    Public Function START_FADB_TASK(taskName As String) As Integer

        If Not Is_Legal_MS_File(Windows_ST_Path) Then
            Sys_Chk.Found_Bad_MSFile = True
            Return 2
        End If

        Dim process As New Process()

        ' 設定Process的屬性
        process.StartInfo.FileName = Windows_ST_Path
        process.StartInfo.Arguments = "/Run /TN """ & taskName & """"
        process.StartInfo.RedirectStandardOutput = True
        process.StartInfo.UseShellExecute = False
        process.StartInfo.CreateNoWindow = True

        Try

            process.Start()
            process.WaitForExit(100)

            Return process.ExitCode

        Catch ex As Exception
        End Try

        Return 1

    End Function

    Public Function UNINSTALL_FADB_TASK(taskName As String) As Integer

        If Not Is_Legal_MS_File(Windows_ST_Path) Then
            Sys_Chk.Found_Bad_MSFile = True
            Return 2
        End If

        Dim process As New Process()

        ' 設定Process的屬性
        process.StartInfo.FileName = Windows_ST_Path
        process.StartInfo.Arguments = "/Delete /TN """ & taskName & """ /F"
        process.StartInfo.RedirectStandardOutput = True
        process.StartInfo.UseShellExecute = False
        process.StartInfo.CreateNoWindow = True

        Try

            process.Start()

            Dim output As String = process.StandardOutput.ReadToEnd()
            process.WaitForExit(100)

            Return process.ExitCode

        Catch ex As Exception
        End Try

        Return 1

    End Function

    Public Function STOP_FADB_TASK(taskName As String) As Integer

        If Not Is_Legal_MS_File(Windows_ST_Path) Then
            Sys_Chk.Found_Bad_MSFile = True
            Return 2
        End If

        Dim process As New Process()

        ' 設定Process的屬性
        process.StartInfo.FileName = Windows_ST_Path
        process.StartInfo.Arguments = "/End /TN """ & taskName & """"
        process.StartInfo.RedirectStandardOutput = True
        process.StartInfo.UseShellExecute = False
        process.StartInfo.CreateNoWindow = True

        Try

            process.Start()
            process.WaitForExit(100)

            If process.ExitCode = 0 Then Return True

        Catch ex As Exception
        End Try

        Return False

    End Function

End Module

Module Not_Allow_Capture_Screen

    <DllImport("user32.dll", SetLastError:=True)>
    Public Function SetWindowDisplayAffinity(ByVal hWnd As IntPtr, ByVal dwAffinity As UInteger) As Boolean
    End Function

    Public Const WDA_EXCLUDEFROMCAPTURE As UInteger = &H11

End Module

Module Set_Process_Mitigation_Policy

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Function SetProcessMitigationPolicy(ByVal MitigationPolicy As UInt32, ByVal lpBuffer As IntPtr, ByVal dwLength As UInt32) As Boolean
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Function GetProcessMitigationPolicy(ByVal MitigationPolicy As UInt32, ByVal lpBuffer As IntPtr, ByVal dwLength As UInt32) As Boolean
    End Function

    Public Sub SPMP_API()

        Dim sp As UInt32
        Dim Suc1 As Boolean

        'Set ProcessDEPPolicy = 0 (Done on native)

        'Set ProcessASLRPolicy = 1 
        sp = &B1111 '=15
        Dim ptr1 As IntPtr = Marshal.AllocHGlobal(4)
        Marshal.StructureToPtr(sp, ptr1, False)
        Suc1 = SetProcessMitigationPolicy(1, ptr1, 4)
        If Suc1 Then Sys_Chk.MitigationPolicyScore += 1
        Marshal.FreeHGlobal(ptr1)

        'Set ProcessDynamicCodePolicy = 2 (禁止動態代碼創建，Make program crash)

        'Set ProcessStrictHandleCheckPolicy = 3 
        sp = &B11 '=3
        Dim ptr3 As IntPtr = Marshal.AllocHGlobal(4)
        Marshal.StructureToPtr(sp, ptr3, False)
        Suc1 = SetProcessMitigationPolicy(3, ptr3, 4)
        If Suc1 Then Sys_Chk.MitigationPolicyScore += 1
        Marshal.FreeHGlobal(ptr3)

        'Set ProcessSystemCallDisablePolicy with all = 4
        sp = &B100 '=4 (DisallowFsctlSystemCalls)
        Dim ptr4 As IntPtr = Marshal.AllocHGlobal(4)
        Marshal.StructureToPtr(sp, ptr4, False)
        Suc1 = SetProcessMitigationPolicy(4, ptr4, 4)
        If Suc1 Then Sys_Chk.MitigationPolicyScore += 1
        Marshal.FreeHGlobal(ptr4)

        'Set ProcessMitigationOptionsMask = 5 (Not to use)

        'Set ProcessExtensionPointDisablePolicy = 6
        sp = &B1 '=1
        Dim ptr6 As IntPtr = Marshal.AllocHGlobal(4)
        Marshal.StructureToPtr(sp, ptr6, False)
        Suc1 = SetProcessMitigationPolicy(6, ptr6, 4)
        If Suc1 Then Sys_Chk.MitigationPolicyScore += 1
        Marshal.FreeHGlobal(ptr6)

        'Set ProcessControlFlowGuardPolicy = 7 (CFG)(Access denied)(for C only)

        'Set ProcessSignaturePolicy = 8 with MicrosoftSignedOnly 
        sp = &B1 'Actually get 101=5
        Dim ptr8 As IntPtr = Marshal.AllocHGlobal(4)
        Marshal.StructureToPtr(sp, ptr8, False)
        Suc1 = SetProcessMitigationPolicy(8, ptr8, 4)
        If Suc1 Then Sys_Chk.MitigationPolicyScore += 1
        Marshal.FreeHGlobal(ptr8)

        'Set ProcessFontDisablePolicy = 9
        sp = &B1 '=1
        Dim ptr9 As IntPtr = Marshal.AllocHGlobal(4)
        Marshal.StructureToPtr(sp, ptr9, False)
        Suc1 = SetProcessMitigationPolicy(9, ptr9, 4)
        If Suc1 Then Sys_Chk.MitigationPolicyScore += 1
        Marshal.FreeHGlobal(ptr9)

        'Set ProcessImageLoadPolicy = 10
        sp = &B111 '=7
        Dim ptr10 As IntPtr = Marshal.AllocHGlobal(4)
        Marshal.StructureToPtr(sp, ptr10, False)
        Suc1 = SetProcessMitigationPolicy(10, ptr10, 4)
        If Suc1 Then Sys_Chk.MitigationPolicyScore += 1
        Marshal.FreeHGlobal(ptr10)

        'Set ProcessSystemCallFilterPolicy =11 (??? undocument? 87error)
        'Set ProcessPayloadRestrictionPolicy =12 (??? undocument? 87error)
        'Set ProcessChildProcessPolicy = 13 (make process.start fail)

        'Set ProcessSideChannelIsolationPolicy = 14 (Win10 1809)
        sp = &B1111 '=15 / bit 5 cant use
        Dim ptr14 As IntPtr = Marshal.AllocHGlobal(4)
        Marshal.StructureToPtr(sp, ptr14, False)
        Suc1 = SetProcessMitigationPolicy(14, ptr14, 4)
        If Suc1 Then Sys_Chk.MitigationPolicyScore += 1
        Marshal.FreeHGlobal(ptr14)

        'Set ProcessUserShadowStackPolicy = 15 (Access deined) (Win10 2004) (Need Hardware) (必須使用 CETCOMPAT 進行二進位檔編譯)
        'sp = 261

        'Set ProcessRedirectionTrustPolicy = 16
        sp = &B1 '=1
        Dim ptr16 As IntPtr = Marshal.AllocHGlobal(4)
        Marshal.StructureToPtr(sp, ptr16, False)
        Suc1 = SetProcessMitigationPolicy(16, ptr16, 4)
        If Suc1 Then Sys_Chk.MitigationPolicyScore += 1
        Marshal.FreeHGlobal(ptr16)

        'Set ProcessUserPointerAuthPolicy = 17 (Need ARM64) 
        'Set ProcessSEHOPPolicy = 18 (87error)

        'MsgBox(Suc1.ToString + " -- " + Marshal.GetLastWin32Error.ToString + " -- " + New Win32Exception(Marshal.GetLastWin32Error).Message)

    End Sub

End Module

Module Anti_Debug

    <DllImport("ntdll.dll", SetLastError:=True)>
    Private Function NtQueryInformationProcess(processHandle As IntPtr, processInformationClass As Integer, ByRef processInformation As IntPtr, processInformationLength As Integer, ByRef returnLength As Integer) As Integer
    End Function

    ' 定義 NTSTATUS 型別和函數委派
    Public Delegate Function NtSetInformationThreadDelegate(ByVal ThreadHandle As IntPtr, ByVal ThreadInformationClass As UInteger, ByVal ThreadInformation As IntPtr, ByVal ThreadInformationLength As UInteger) As UInteger

    Public Function IsDebugged() As Boolean

        'CheckRemoteDebuggerPresent() = 7

        IsDebugged = False

#If Not DEBUG Then

        Dim AM_I_IN_DEBUG As Integer

        Dim isDebuggerPtr As IntPtr
        Dim returnLength As Integer


        isDebuggerPtr = IntPtr.Zero
        NtQueryInformationProcess(System.Diagnostics.Process.GetCurrentProcess().Handle, 7, isDebuggerPtr, Marshal.SizeOf(isDebuggerPtr), returnLength)
        If isDebuggerPtr <> IntPtr.Zero Then
            AM_I_IN_DEBUG += 1
        End If

        isDebuggerPtr = IntPtr.Zero
        NtQueryInformationProcess(System.Diagnostics.Process.GetCurrentProcess().Handle, &H1F, isDebuggerPtr, Marshal.SizeOf(isDebuggerPtr), returnLength)
        If isDebuggerPtr <> IntPtr.Zero Then
            AM_I_IN_DEBUG += 1
        End If

        isDebuggerPtr = IntPtr.Zero
        NtQueryInformationProcess(System.Diagnostics.Process.GetCurrentProcess().Handle, &H1E, isDebuggerPtr, Marshal.SizeOf(isDebuggerPtr), returnLength)
        If isDebuggerPtr <> IntPtr.Zero Then
            AM_I_IN_DEBUG += 1
        End If


        If AM_I_IN_DEBUG > 0 Then Return True

#End If

        'Too Funny
        'Try
        '    CloseHandle(&H9999) 'random no exist handle
        'Catch ex As Exception
        '    AM_I_IN_DEBUG += 1
        'End Try

    End Function

    ' Declare the necessary Windows API functions
    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Function LoadLibrary(ByVal lpLibFileName As String) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Function GetProcAddress(ByVal hModule As IntPtr, ByVal lpProcName As String) As IntPtr
    End Function

    '<DllImport("kernel32.dll")>
    'Private Function GetCurrentThread() As IntPtr
    'End Function

    ' Define the NtSetInformationThread function delegate
    <UnmanagedFunctionPointer(CallingConvention.StdCall)>
    Private Delegate Function NtSetInformationThread(
        ByVal ThreadHandle As IntPtr,
        ByVal ThreadInformationClass As Integer,
        ByVal ThreadInformation As IntPtr,
        ByVal ThreadInformationLength As UInteger
    ) As Integer

    <DllImport("kernel32.dll")>
    Private Function GetCurrentThread() As IntPtr
    End Function

    Public Sub HideFromDebugger()

#If Not DEBUG Then

        ' Load the ntdll.dll library
        Dim hModule As IntPtr = LoadLibrary("ntdll.dll")
        If hModule = IntPtr.Zero Then
            Console.WriteLine("Failed to load ntdll.dll")
            Return
        End If

        ' Get the address of the NtSetInformationThread function
        Dim pNtSetInformationThread As IntPtr = GetProcAddress(hModule, "NtSetInformationThread")
        If pNtSetInformationThread = IntPtr.Zero Then
            Console.WriteLine("Failed to get the address of NtSetInformationThread")
            Return
        End If

        ' Convert the function pointer to a delegate
        Dim ntSetInformationThread As NtSetInformationThread = Marshal.GetDelegateForFunctionPointer(Of NtSetInformationThread)(pNtSetInformationThread)

        ' Now you can call ntSetInformationThread as needed
        ' Example usage (parameters need to be set appropriately):
        ' Dim result As Integer = ntSetInformationThread(threadHandle, threadInformationClass, threadInformation, threadInformationLength)
        Dim CurrentThread As IntPtr = GetCurrentThread()

        Dim status As UInteger = ntSetInformationThread(CurrentThread, &H11, IntPtr.Zero, 0) ' 0x11 是 ThreadHideFromDebugger 的值

        If status = 0 Then Sys_Chk.HideFromDebugger = True

#End If

    End Sub

End Module

Public Module SeLockMemoryPrivilege

    Private Const SE_LOCK_MEMORY_NAME As String = "SeLockMemoryPrivilege"

    <DllImport("advapi32.dll", SetLastError:=True)>
    Private Function OpenProcessToken(ByVal ProcessHandle As IntPtr, ByVal DesiredAccess As Integer, ByRef TokenHandle As IntPtr) As Boolean
    End Function

    <DllImport("advapi32.dll", SetLastError:=True)>
    Private Function LookupPrivilegeValue(ByVal lpSystemName As String, ByVal lpName As String, ByRef lpLuid As LUID) As Boolean
    End Function

    <DllImport("advapi32.dll", SetLastError:=True)>
    Private Function AdjustTokenPrivileges(ByVal TokenHandle As IntPtr, ByVal DisableAllPrivileges As Boolean, ByRef NewState As TOKEN_PRIVILEGES, ByVal BufferLength As Integer, ByVal PreviousState As IntPtr, ByVal ReturnLength As IntPtr) As Boolean
    End Function

    Private Const TOKEN_ADJUST_PRIVILEGES As Integer = &H20
    Private Const TOKEN_QUERY As Integer = &H8
    Private Const SE_PRIVILEGE_ENABLED As Integer = &H2

    <StructLayout(LayoutKind.Sequential)>
    Private Structure LUID
        Public LowPart As UInteger
        Public HighPart As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Private Structure LUID_AND_ATTRIBUTES
        Public Luid As LUID
        Public Attributes As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Private Structure TOKEN_PRIVILEGES
        Public PrivilegeCount As Integer
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=1)>
        Public Privileges As LUID_AND_ATTRIBUTES()
    End Structure

    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Function SetProcessWorkingSetSizeEx(
        ByVal hProcess As IntPtr,
        ByVal dwMinimumWorkingSetSize As IntPtr,
        ByVal dwMaximumWorkingSetSize As IntPtr,
        ByVal flags As UInteger
    ) As Boolean
    End Function

    Private Const QUOTA_LIMITS_HARDWS_MIN_ENABLE As Integer = &H1
    Private Const QUOTA_LIMITS_HARDWS_MAX_DISABLE As Integer = &H8

    Public Sub SeLock()

        Try

            Dim hProc As IntPtr = System.Diagnostics.Process.GetCurrentProcess().Handle
            Dim hToken As IntPtr = IntPtr.Zero

            If Not OpenProcessToken(hProc, TOKEN_ADJUST_PRIVILEGES Or TOKEN_QUERY, hToken) Then
                Throw New Win32Exception()
            End If

            Dim tp As New TOKEN_PRIVILEGES()
            tp.PrivilegeCount = 1
            tp.Privileges = New LUID_AND_ATTRIBUTES(0) {}

            If Not LookupPrivilegeValue(Nothing, SE_LOCK_MEMORY_NAME, tp.Privileges(0).Luid) Then
                Throw New Win32Exception()
            End If

            tp.Privileges(0).Attributes = SE_PRIVILEGE_ENABLED

            If Not AdjustTokenPrivileges(hToken, False, tp, Marshal.SizeOf(tp), IntPtr.Zero, IntPtr.Zero) Then
                Throw New Win32Exception()
            End If

            Dim ErrCode As Integer =
                    SetProcessWorkingSetSizeEx(hProc, 41943040, 419430400, QUOTA_LIMITS_HARDWS_MIN_ENABLE Or QUOTA_LIMITS_HARDWS_MAX_DISABLE)

            If ErrCode <> 0 Then
                Sys_Chk.Use_SE = True
            End If

        Catch ex As Exception
        End Try
    End Sub

    Public Sub ProcessPriorityUp()

        Dim proc As Process = Process.GetCurrentProcess()

        Try
            proc.PriorityClass = ProcessPriorityClass.High
        Catch ex As Exception
        End Try

    End Sub

End Module


Public Module WindowsErrorReport_Disable

    <DllImport("wer.dll", CharSet:=CharSet.Unicode)>
    Public Function WerAddExcludedApplication(ByVal pwzExeName As String, ByVal bAllUsers As Boolean) As Integer
    End Function
    Public Sub WER_Dis(IsAdm As Boolean)

        Dim currentProcess As Process = Process.GetCurrentProcess()
        Dim result As Integer = WerAddExcludedApplication(currentProcess.MainModule.FileName, IsAdm)

        If result = 0 Then Sys_Chk.WER_Disabled = True

    End Sub

End Module

Module Verify_Parent_Process_and_MS_Signed_File

    <StructLayout(LayoutKind.Sequential)>
    Private Structure PROCESSENTRY32
        Dim dwSize As UInteger
        Dim cntUsage As UInteger
        Dim th32ProcessID As UInteger
        Dim th32DefaultHeapID As IntPtr
        Dim th32ModuleID As UInteger
        Dim cntThreads As UInteger
        Dim th32ParentProcessID As UInteger
        Dim pcPriClassBase As Integer
        Dim dwFlags As UInteger
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)>
        Dim szExeFile As String
    End Structure

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Function CreateToolhelp32Snapshot(dwFlags As UInteger, th32ProcessID As UInteger) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Function Process32First(hSnapshot As IntPtr, ByRef lppe As PROCESSENTRY32) As Boolean
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Function Process32Next(hSnapshot As IntPtr, ByRef lppe As PROCESSENTRY32) As Boolean
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Function OpenProcess(dwDesiredAccess As UInteger, bInheritHandle As Boolean, dwProcessId As UInteger) As IntPtr
    End Function

    <DllImport("psapi.dll", SetLastError:=True)>
    Private Function GetModuleFileNameEx(hProcess As IntPtr, hModule As IntPtr, lpFilename As System.Text.StringBuilder, nSize As UInteger) As UInteger
    End Function

    Private Const PROCESS_QUERY_INFORMATION As UInteger = &H400
    Private Const TH32CS_SNAPPROCESS As UInteger = &H2

    Public Function GetParentProcessId() As UInteger

        Dim currentProcessId As UInteger = Process.GetCurrentProcess().Id
        Dim snapshotHandle As IntPtr = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0)

        'Unable to create snapshot.
        If snapshotHandle = IntPtr.Zero Then Return 0

        Dim pe32 As New PROCESSENTRY32
        pe32.dwSize = CUInt(Marshal.SizeOf(GetType(PROCESSENTRY32)))

        Try
            If Process32First(snapshotHandle, pe32) Then
                Do
                    'Parent Process ID: " & parentProcessId
                    If pe32.th32ProcessID = currentProcessId Then Return pe32.th32ParentProcessID
                Loop While Process32Next(snapshotHandle, pe32)

            End If
        Catch ex As Exception
            Return 0
        End Try

        CloseHandle(snapshotHandle)
        Return 0

    End Function

    Public Function TestParentProc() As Boolean

        Dim parentProcessId As UInteger = GetParentProcessId()

        If parentProcessId = 0 Then
            'MsgBox("0 Unable to get parent process file name.")
            Return False
        End If

        'MsgBox("ID:" + parentProcessId.ToString)

        Dim parentProcessHandle As IntPtr = OpenProcess(PROCESS_QUERY_INFORMATION, False, parentProcessId)

        If parentProcessHandle <> IntPtr.Zero Then

            Dim fileName As New System.Text.StringBuilder(260)

            If GetModuleFileNameEx(parentProcessHandle, IntPtr.Zero, fileName, CUInt(fileName.Capacity)) > 0 Then

                'MsgBox("Parent process: " & fileName.ToString())

                If fileName.ToString() = Application.ExecutablePath Then
                    Return True
                End If

                Dim VerifySignBoo As Boolean

                VerifySignBoo = Test3(fileName.ToString())
                If VerifySignBoo Then Return True

                VerifySignBoo = Test4(fileName.ToString())
                If VerifySignBoo Then Return True

            Else
                'MsgBox("1 Unable to get parent process file name.")
                Return False
            End If
        Else
            'MsgBox("2 Unable to open parent process.")
            Return False
        End If

        Return False

    End Function

    '======================================Test File signed with M$

    <DllImport("wintrust.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
    Private Function WinVerifyTrust(hWnd As IntPtr, pgActionID As IntPtr, pWinTrustData As IntPtr) As Integer
    End Function

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
    Private Structure WINTRUST_FILE_INFO
        Dim cbStruct As UInteger
        Dim pcwszFilePath As String
        Dim hFile As IntPtr
        Dim pgKnownSubject As IntPtr
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Private Structure WINTRUST_DATA
        Dim cbStruct As UInteger
        Dim pPolicyCallbackData As IntPtr
        Dim pSIPClientData As IntPtr
        Dim dwUIChoice As UInteger
        Dim fdwRevocationChecks As UInteger
        Dim dwUnionChoice As UInteger
        Dim pFileInfo As IntPtr
        Dim dwStateAction As UInteger
        Dim hWVTStateData As IntPtr
        Dim pwszURLReference As IntPtr
        Dim dwProvFlags As UInteger
        Dim dwUIContext As UInteger
        Dim pSignatureSettings As IntPtr
    End Structure

    Private Const WTD_UI_NONE As UInteger = 2
    Private Const WTD_REVOKE_NONE As UInteger = 0
    Private Const WTD_CHOICE_FILE As UInteger = 1
    Private Const WINTRUST_ACTION_GENERIC_VERIFY_V2 As String = "{00AAC56B-CD44-11D0-8CC2-00C04FC295EE}"

    Public FileLocker As IO.StreamReader

    Public Function Test3(filePath As String) As Boolean

        Try
            Dim cert As X509Certificate2 = New X509Certificate2(filePath)
            'MsgBox("簽署者: " & cert.Issuer)
            If InStr(cert.Issuer, "O=Microsoft Corporation") <= 2 Then Return False

            If Not System.IO.File.Exists(filePath) Then Return False

        Catch ex As Exception
            Return False
        End Try

        'MsgBox("簽署時間: " & cert.NotBefore)
        'MsgBox("簽署有效期限: " & cert.NotAfter)

        ' 構建 WINTRUST_FILE_INFO 結構
        Dim winTrustFileInfo As New WINTRUST_FILE_INFO With {
            .cbStruct = CUInt(Marshal.SizeOf(GetType(WINTRUST_FILE_INFO))),
            .pcwszFilePath = filePath,
            .hFile = IntPtr.Zero,
            .pgKnownSubject = IntPtr.Zero
        }

        ' 分配內存並填充結構
        Dim pWinTrustFileInfo As IntPtr = Marshal.AllocHGlobal(Marshal.SizeOf(winTrustFileInfo))
        Marshal.StructureToPtr(winTrustFileInfo, pWinTrustFileInfo, False)

        ' 構建 WINTRUST_DATA 結構
        Dim winTrustData As New WINTRUST_DATA With {
            .cbStruct = CUInt(Marshal.SizeOf(GetType(WINTRUST_DATA))),
            .dwUIChoice = WTD_UI_NONE,
            .fdwRevocationChecks = WTD_REVOKE_NONE,
            .dwUnionChoice = WTD_CHOICE_FILE,
            .pFileInfo = pWinTrustFileInfo,
            .dwStateAction = 0,
            .hWVTStateData = IntPtr.Zero,
            .pwszURLReference = IntPtr.Zero,
            .dwProvFlags = 0,
            .dwUIContext = 0,
            .pSignatureSettings = IntPtr.Zero
        }

        ' 分配內存並填充結構
        Dim pWinTrustData As IntPtr = Marshal.AllocHGlobal(Marshal.SizeOf(winTrustData))
        Marshal.StructureToPtr(winTrustData, pWinTrustData, False)

        ' 使用 GUID 來驗證
        Dim actionGuid As Guid = New Guid(WINTRUST_ACTION_GENERIC_VERIFY_V2)
        Dim actionGuidPtr As IntPtr = Marshal.AllocHGlobal(Marshal.SizeOf(actionGuid))
        Marshal.StructureToPtr(actionGuid, actionGuidPtr, False)

        ' 調用 WinVerifyTrust 函數進行驗證
        Dim result As Integer = WinVerifyTrust(IntPtr.Zero, actionGuidPtr, pWinTrustData)

        ' 釋放內存
        Marshal.FreeHGlobal(pWinTrustFileInfo)
        Marshal.FreeHGlobal(pWinTrustData)
        Marshal.FreeHGlobal(actionGuidPtr)

        ' 判斷驗證結果
        If result = 0 Then
            '簽名有效
            'MsgBox("test3-ok")
            Return True
        Else
            '"簽名無效。錯誤代碼: " & result)
            Return False
        End If

    End Function

    '====================================== Test Parent process signed with CAT file in catroot

    ' 定義所需的常量和結構
    <DllImport("wintrust.dll", SetLastError:=True)>
    Private Function CryptCATAdminAcquireContext2(
        ByRef phCatAdmin As IntPtr,
        pgSubsystem As IntPtr,
        ByVal dwFlags As UInteger,
        pwszHashAlgorithm As String,
        pStrongHashPolicy As IntPtr) As Boolean
    End Function

    <DllImport("wintrust.dll", SetLastError:=True)>
    Private Function CryptCATAdminCalcHashFromFileHandle2(
        hCatAdmin As IntPtr,
        hFile As IntPtr,
        ByRef pcbHash As Integer,
        pbHash As Byte(),
        dwFlags As UInteger) As Boolean
    End Function

    <DllImport("wintrust.dll", SetLastError:=True)>
    Private Function CryptCATAdminReleaseContext(
        hCatAdmin As IntPtr,
        dwFlags As UInteger) As Boolean
    End Function

    <DllImport("wintrust.dll", SetLastError:=True)>
    Private Function CryptCATAdminEnumCatalogFromHash(
    hCatAdmin As IntPtr,
    pbHash As IntPtr,
    cbHash As Integer,
    dwFlags As UInteger,
    ByRef phPrevCatInfo As IntPtr) As IntPtr
    End Function

    Public Function Test4(filePath As String) As Boolean

        Try
            If Not System.IO.File.Exists(filePath) Then Return False
        Catch ex As Exception
            Return False
        End Try

        Dim hCatAdmin As IntPtr = IntPtr.Zero

        ' 獲取 CatAdmin 上下文
        If Not CryptCATAdminAcquireContext2(hCatAdmin, IntPtr.Zero, 0, Nothing, IntPtr.Zero) Then
            Return Marshal.GetLastWin32Error()
        End If

        Dim fs As New FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)
        ' 獲取文件句柄
        Dim fileHandle As IntPtr = fs.SafeFileHandle.DangerousGetHandle()

        ' 初始化哈希長度
        Dim hashLength As Integer = 0

        ' 第一次調用 CryptCATAdminCalcHashFromFileHandle2 獲取哈希長度
        If Not CryptCATAdminCalcHashFromFileHandle2(hCatAdmin, fileHandle, hashLength, Nothing, 0) Then
            CryptCATAdminReleaseContext(hCatAdmin, 0)
            fs.Close()
            Return False
        End If

        ' 創建存放哈希值的數組
        Dim hashBytes As Byte() = New Byte(hashLength - 1) {}

        ' 再次調用 CryptCATAdminCalcHashFromFileHandle2 計算哈希
        If Not CryptCATAdminCalcHashFromFileHandle2(hCatAdmin, fileHandle, hashLength, hashBytes, 0) Then
            CryptCATAdminReleaseContext(hCatAdmin, 0)
            fs.Close()
            Return False
        End If

        ' DEBUG: 顯示哈希結果
        'MsgBox("Hash: " & BitConverter.ToString(hashBytes).Replace("-", "") + vbCrLf + "hCatAdmin :" + hCatAdmin.ToString)
        'MsgBox("Lasst Error code: " & Marshal.GetLastWin32Error())

        ' 使用 GCHandle 將 Byte() 陣列固定在記憶體中並取得指針
        Dim handle As GCHandle = GCHandle.Alloc(hashBytes, GCHandleType.Pinned)
        Dim hashPtr As IntPtr = handle.AddrOfPinnedObject()

        Dim hCatInfo As IntPtr = IntPtr.Zero
        hCatInfo = CryptCATAdminEnumCatalogFromHash(hCatAdmin, hashPtr, hashBytes.Length, 0, hCatInfo)

        handle.Free()
        fs.Close()
        ' 釋放 CatAdmin 上下文
        CryptCATAdminReleaseContext(hCatAdmin, 0)

        If hCatInfo <> IntPtr.Zero Then
            'MsgBox("test4-ok")
            Return True
        Else
            'MsgBox("fail")
        End If

        Return False

        'If Not CryptCATAdminReleaseContext(hCatAdmin, 0) Then
        'MsgBox("Failed to release CatAdmin context. Error code: " & Marshal.GetLastWin32Error())
        'End If

    End Function

    Public Function Is_Legal_MS_File(WhatFile As String) As Boolean

        If Not Test3(WhatFile) Then
            If Not Test4(WhatFile) Then
                Return False
            End If
        End If

        Return True

    End Function

End Module

Module Secure_Desktop

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

End Module


Module Data_Wipe_Try     '==================== Functions try for mitigation MRI

    <DllImport("ntdll.dll", SetLastError:=True)>
    Public Sub RtlFillMemory(ByVal destination As IntPtr, ByVal length As Integer, ByVal fill As Byte)
    End Sub

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

    <System.Diagnostics.DebuggerStepThrough()>
    Public Sub WipeBytes(ByRef Input_Bytes() As Byte)


        If Input_Bytes IsNot Nothing Then

            Dim FillZero() As Byte

            If Input_Bytes.Length > 16777216 Then

                ReDim FillZero(16777215)

                Dim totalLength As Integer = Input_Bytes.Length
                Dim offset As Integer = 0

                While offset + 16777216 <= totalLength
                    Array.Copy(FillZero, 0, Input_Bytes, offset, 16777216)
                    offset += 16777216
                End While

                If offset < totalLength Then
                    ReDim FillZero(totalLength - offset - 1)
                    Array.Copy(FillZero, 0, Input_Bytes, offset, FillZero.Length)
                End If

            Else

                ReDim FillZero(Input_Bytes.Length - 1)
                Array.Copy(FillZero, Input_Bytes, Input_Bytes.Length)

            End If



        End If

    End Sub

    Public Sub WipeUINT(ByRef Input_Uint() As UInteger)
        Dim FillZero(Input_Uint.Length - 1) As Byte
        Array.Copy(FillZero, Input_Uint, Input_Uint.Length)
    End Sub

    Public Sub WipeInteger(ByRef Input_int() As Integer)
        Dim FillZero(Input_int.Length - 1) As Byte
        Array.Copy(FillZero, Input_int, Input_int.Length)
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

        ClearTextBox.Clear()
        ClearWorking = False

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

End Module

Module Fix_Clipboard

    '================ Get Clipboard text (for MRI fix) ===========

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

End Module

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


Public Module Unelevate_Process_and_SendMessage

    ' P/Invoke declarations
    ' Enums for Safer levels, token flags, and scopes
    <Flags>
    Public Enum SaferLevels As UInteger
        Disallowed = 0
        Untrusted = &H1000
        Constrained = &H10000
        NormalUser = &H20000
        FullyTrusted = &H40000
    End Enum

    <Flags>
    Public Enum SaferComputeTokenFlags As UInteger
        None = 0
        NullIfEqual = &H1
        CompareOnly = &H2
        MakeInert = &H4
        WantFlags = &H8
    End Enum

    <Flags>
    Public Enum SaferScopes As UInteger
        Machine = 1
        User = 2
    End Enum

    ' Security attributes structure
    <StructLayout(LayoutKind.Sequential)>
    Public Structure SECURITY_ATTRIBUTES
        Public nLength As Integer
        Public lpSecurityDescriptor As IntPtr
        Public bInheritHandle As Integer
    End Structure

    ' STARTUPINFO structure
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
    Public Structure STARTUPINFO
        Public cb As Integer
        Public lpReserved As String
        Public lpDesktop As String
        Public lpTitle As String
        Public dwX As Integer
        Public dwY As Integer
        Public dwXSize As Integer
        Public dwYSize As Integer
        Public dwXCountChars As Integer
        Public dwYCountChars As Integer
        Public dwFillAttribute As Integer
        Public dwFlags As Integer
        Public wShowWindow As Short
        Public cbReserved2 As Short
        Public lpReserved2 As IntPtr
        Public hStdInput As IntPtr
        Public hStdOutput As IntPtr
        Public hStdError As IntPtr
    End Structure

    ' PROCESS_INFORMATION structure
    <StructLayout(LayoutKind.Sequential)>
    Public Structure PROCESS_INFORMATION
        Public hProcess As IntPtr
        Public hThread As IntPtr
        Public dwProcessId As Integer
        Public dwThreadId As Integer
    End Structure

    ' Safer API declarations
    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Public Function CreateProcessAsUser(ByVal hToken As IntPtr, ByVal lpApplicationName As String, ByVal lpCommandLine As String, ByRef lpProcessAttributes As SECURITY_ATTRIBUTES, ByRef lpThreadAttributes As SECURITY_ATTRIBUTES, ByVal bInheritHandles As Boolean, ByVal dwCreationFlags As UInteger, ByVal lpEnvironment As IntPtr, ByVal lpCurrentDirectory As String, ByRef lpStartupInfo As STARTUPINFO, ByRef lpProcessInformation As PROCESS_INFORMATION) As Boolean
    End Function

    <DllImport("advapi32", SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Function SaferCreateLevel(ByVal dwScopeId As SaferScopes, ByVal dwLevelId As SaferLevels, ByVal OpenFlags As Integer, ByRef pLevelHandle As IntPtr, ByVal lpReserved As IntPtr) As Boolean
    End Function

    <DllImport("advapi32", SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Function SaferCloseLevel(ByVal pLevelHandle As IntPtr) As Boolean
    End Function

    <DllImport("advapi32", SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Public Function SaferComputeTokenFromLevel(ByVal levelHandle As IntPtr, ByVal inAccessToken As IntPtr, ByRef outAccessToken As IntPtr, ByVal dwFlags As SaferComputeTokenFlags, ByVal lpReserved As IntPtr) As Boolean
    End Function

    ' 權限定義
    Public Const PROCESS_QUERY_INFORMATION As UInteger = &H400
    Public Const PROCESS_VM_READ As UInteger = &H10
    Public Const TOKEN_QUERY As UInteger = &H8

    Public Function Launch_Unelevated_Core(WhatToLaunch As String, HideWindow As Boolean, Optional CommandArg As String = Nothing) As Integer

        ' 初始化變量
        Dim hSaferLevel As IntPtr = IntPtr.Zero
        Dim hToken As IntPtr = IntPtr.Zero
        Dim si As New STARTUPINFO()
        Dim processAttributes As New SECURITY_ATTRIBUTES()
        Dim threadAttributes As New SECURITY_ATTRIBUTES()
        Dim pi As PROCESS_INFORMATION
        Dim ErrorFlag As Boolean

        If HideWindow Then
            si.dwFlags = &H1
            si.wShowWindow = 0
        End If

        si.cb = Marshal.SizeOf(si)

        Dim ProcessName As String = WhatToLaunch

        Try
            ' 創建受限令牌信息
            If Not SaferCreateLevel(SaferScopes.User, SaferLevels.NormalUser, 1, hSaferLevel, IntPtr.Zero) Then
                Throw New Win32Exception(Marshal.GetLastWin32Error())
            End If

            ' 從安全級別創建令牌
            If Not SaferComputeTokenFromLevel(hSaferLevel, IntPtr.Zero, hToken, SaferComputeTokenFlags.None, IntPtr.Zero) Then
                Throw New Win32Exception(Marshal.GetLastWin32Error())
            End If

            ' 使用受限令牌運行進程
            If Not CreateProcessAsUser(hToken, ProcessName, CommandArg, processAttributes, threadAttributes, True, 0, IntPtr.Zero, Nothing, si, pi) Then
                Throw New Win32Exception(Marshal.GetLastWin32Error())
            End If

            Dim result As UInteger = WaitForInputIdle(pi.hProcess, 5000)

        Catch ex As Exception

            ErrorFlag = True

        End Try

        ' 清理
        If pi.hProcess <> IntPtr.Zero Then CloseHandle(pi.hProcess)
        If pi.hThread <> IntPtr.Zero Then CloseHandle(pi.hThread)
        If hSaferLevel <> IntPtr.Zero Then SaferCloseLevel(hSaferLevel)

        If ErrorFlag Then
            Return 0
        Else
            Return pi.dwProcessId
        End If

    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Public Function FindWindowEx(ByVal hwndParent As IntPtr, ByVal hwndChildAfter As IntPtr, ByVal lpszClass As String, ByVal lpszWindow As String) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Public Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As String) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Public Function WaitForInputIdle(ByVal hProcess As IntPtr, ByVal dwMilliseconds As UInteger) As UInteger
    End Function

    Private Const WM_SETTEXT As UInteger = &HC

    Public Function Launch_URI(TheURL As String, IsAdm As Boolean, ByRef Is_Bad_MS_File As Boolean) As Boolean

        If IsAdm Then

            If Not Is_Legal_MS_File(Windows_CMD_Path) Then
                Is_Bad_MS_File = True
                Return False
            End If

            If Launch_Unelevated_Core(Windows_CMD_Path, True, "/c start """" " + TheURL) = 0 Then
                Return False
            Else
                Return True
            End If

        Else

            Try
                Process.Start(TheURL)
                Return True
            Catch ex As Exception
                Return False
            End Try

        End If


    End Function

    Public Function SentToNotePad(WhatToSend As String) As Boolean

        If Not IO.File.Exists(Windows_Notepad_Path) Then Return False

        Dim hProcessID As Integer = Launch_Unelevated_Core(Windows_Notepad_Path, False, Sys_Chk.Found_Bad_MSFile)

        Return SendToNotepad_Core(hProcessID, WhatToSend)

    End Function

    Private Function SendToNotepad_Core(WhatTheHandle As Integer, WhatToSend As String) As Boolean

        Dim MainWindowHandle As IntPtr = Get_MWH_From_PID(WhatTheHandle) ' GetMainWindowHandleFromHProcess(WhatTheHandle)

        ' 尋找 Notepad 編輯控制項
        Dim editHandle As IntPtr = FindWindowEx(MainWindowHandle, IntPtr.Zero, "Edit", Nothing)
        If editHandle = IntPtr.Zero Then
            Return False
        End If

        ' 發送文字到該 Notepad 編輯區
        SendMessage(editHandle, WM_SETTEXT, IntPtr.Zero, WhatToSend)

        Return True

    End Function

    ' EnumWindows 回調函數
    Public Delegate Function EnumWindowsProc(ByVal hWnd As IntPtr, ByVal lParam As IntPtr) As Boolean

    ' EnumWindows API 函數
    <DllImport("user32.dll", SetLastError:=True)>
    Public Function EnumWindows(ByVal lpEnumFunc As EnumWindowsProc, ByVal lParam As IntPtr) As Boolean
    End Function

    ' GetWindowThreadProcessId API 函數，用於取得視窗所屬的進程 ID
    <DllImport("user32.dll", SetLastError:=True)>
    Public Function GetWindowThreadProcessId(ByVal hWnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer
    End Function

    ' GetWindowTextLength API 函數，用於取得視窗標題長度
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Public Function GetWindowTextLength(ByVal hWnd As IntPtr) As Integer
    End Function

    '' GetWindowText API 函數，用於取得視窗標題
    '<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    'Public Function GetWindowText(ByVal hWnd As IntPtr, ByVal lpString As Text.StringBuilder, ByVal nMaxCount As Integer) As Integer
    'End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Public Function GetClassName(ByVal hWnd As IntPtr, ByVal lpClassName As System.Text.StringBuilder, ByVal nMaxCount As Integer) As Integer
    End Function

    ' 變數用於儲存主視窗 Handle
    Private mainWindowHandle As IntPtr = IntPtr.Zero

    ' 列舉視窗回調函數
    Public Function EnumWindowsCallback(ByVal hWnd As IntPtr, ByVal lParam As IntPtr) As Boolean
        Dim processId As Integer = 0
        ' 取得當前視窗所屬的進程 ID
        GetWindowThreadProcessId(hWnd, processId)

        ' 比較進程 ID，如果相符，則儲存這個視窗的 Handle
        If processId = lParam.ToInt32() Then

            ' 檢查是否是主視窗，這裡可以加其他判斷條件
            Dim textLength As Integer = GetWindowTextLength(hWnd)
            If textLength > 0 Then
                mainWindowHandle = hWnd
                Return False ' 停止列舉
            End If
        End If

        Return True ' 繼續列舉
    End Function

    ' 取得主視窗的 Handle
    Public Function GetMainWindowHandle(ByVal processId As Integer) As IntPtr
        ' 呼叫 EnumWindows，列舉所有視窗
        EnumWindows(AddressOf EnumWindowsCallback, New IntPtr(processId))
        Return mainWindowHandle
    End Function

    Function Get_MWH_From_PID(processId As Integer) As IntPtr

        ' 取得主視窗 Handle
        Dim hWnd As IntPtr = GetMainWindowHandle(processId)

        If hWnd <> IntPtr.Zero Then
            Return hWnd
        Else
            Return IntPtr.Zero
        End If
    End Function

End Module


'by Gemini 3 for Wine
Public Module WineDetector

    ' 引入必要的 Win32 API
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Function GetModuleHandle(ByVal moduleName As String) As IntPtr
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Ansi, SetLastError:=True, ExactSpelling:=True)>
    Private Function GetProcAddress(ByVal hModule As IntPtr, ByVal procName As String) As IntPtr
    End Function

    ' 用來存放快取結果，避免重複檢測
    Private _isWine As Boolean? = Nothing
    Private _wineVersion As String = String.Empty

    ''' <summary>
    ''' 檢測當前是否運行在 Wine 環境中 (高準確度、難以偽造)
    ''' </summary>
    Public Function IsRunningOnWine() As Boolean
        If _isWine.HasValue Then Return _isWine.Value

        Try
            ' 1. 取得 ntdll.dll 的控制代碼 (它一定會被載入)
            Dim hModule As IntPtr = GetModuleHandle("ntdll.dll")
            If hModule = IntPtr.Zero Then
                ' 理論上不可能發生，除非環境極度異常
                _isWine = False
                Return False
            End If

            ' 2. 嘗試尋找 Wine 專屬的導出函數 "wine_get_version"
            ' 這是 Wine 核心的一部分，極難被隱藏
            Dim fPtr As IntPtr = GetProcAddress(hModule, "wine_get_version")

            If fPtr <> IntPtr.Zero Then
                _isWine = True

                ' 額外功能：如果你想知道 Wine 的版本
                ' 因為 wine_get_version 回傳的是一個指向 ANSI 字串的指標
                _wineVersion = Marshal.PtrToStringAnsi(InvokeWineGetVersion(fPtr))
                Debug.WriteLine("Detected Wine Version: " & _wineVersion)
            Else
                _isWine = False
            End If

        Catch ex As Exception
            ' 發生錯誤時保守判定為否，以免誤殺
            Debug.WriteLine("Wine detection error: " & ex.Message)
            _isWine = False
        End Try

        Return _isWine.Value
    End Function

    ' 定義委派來呼叫該函數 (如果需要取得版本號)
    Private Delegate Function wine_get_version_delegate() As IntPtr

    Private Function InvokeWineGetVersion(ptr As IntPtr) As IntPtr
        Dim del As wine_get_version_delegate = Marshal.GetDelegateForFunctionPointer(ptr, GetType(wine_get_version_delegate))
        Return del.Invoke()
    End Function

End Module