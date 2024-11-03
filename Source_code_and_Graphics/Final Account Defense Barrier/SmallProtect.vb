'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Imports System.Runtime.InteropServices
Imports System.Security.Principal

<System.Security.SecurityCritical>
Public Class SmallProtect

    Dim TheReader As IO.StreamReader

    Private Sub SmallProtect_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Sys_Chk.Use_ATField = Check_Task_Exist(TaskSch_Name_str)

        HideFromDebugger()
        ProcessPriorityUp()
        Sys_Chk.HasDebugger = Not TestParentProc()

        Dim osVersion As OperatingSystem = Environment.OSVersion
        Sys_Chk.OS_ver = (Environment.OSVersion.Version.Major * 10) + osVersion.Version.Minor

        '============== Admin level verify
        Sys_Chk.Screen_Capture_Allowed = False
        Sys_Chk.Running_Admin = New WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator)

        If Sys_Chk.OS_ver >= 62 Then
            SPMP_API()
        End If

        SeLock()
        WER_Dis(Sys_Chk.Running_Admin)
        IsDebugged()

        Dim Arguments() As String = Environment.GetCommandLineArgs()
        For Each ArgStr As String In Arguments
            If ArgStr.Length <= 100 Then
                If ArgStr.ToUpper = ATField_Mode_str Then
                    IsProtectMode = True
                    Exit For
                End If
            End If
        Next

        If IsProtectMode Then

            SetProcessShutdownParameters(&H100, 1)

            TheReader = New IO.StreamReader(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
            FullGC()
            Me.Width = 0
            Me.Height = 0

        Else

            Sys_Chk._OpacitySng = 1.0F
            FormMain.Show()
            Me.Close()

        End If

    End Sub

End Class

Module SmallProtect_Module

    Public IsProtectMode As Boolean = False

End Module