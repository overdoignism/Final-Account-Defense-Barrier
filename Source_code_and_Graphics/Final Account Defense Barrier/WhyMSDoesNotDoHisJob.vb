'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Imports System.Runtime.InteropServices
Imports System.ComponentModel

Module SPMP

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Function SetProcessMitigationPolicy(ByVal MitigationPolicy As UInt32, ByVal lpBuffer As IntPtr, ByVal dwLength As UInt32) As Boolean
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Function GetProcessMitigationPolicy(ByVal MitigationPolicy As UInt32, ByVal lpBuffer As IntPtr, ByVal dwLength As UInt32) As Boolean
    End Function

    Public Sub SPMP_Set()

        Dim sp As UInt32
        Dim Suc1 As Boolean

        'Set ProcessDEPPolicy = 0 (Done on native)

        'Set ProcessASLRPolicy = 1 
        sp = &B1111
        Dim ptr0 As IntPtr = Marshal.AllocHGlobal(4)
        Marshal.StructureToPtr(sp, ptr0, False)
        Suc1 = SetProcessMitigationPolicy(1, ptr0, 4)
        Marshal.FreeHGlobal(ptr0)

        'Set ProcessDynamicCodePolicy = 2 (Make program crash)

        'Set ProcessStrictHandleCheckPolicy = 3 
        sp = &B11
        Dim ptr1 As IntPtr = Marshal.AllocHGlobal(4)
        Marshal.StructureToPtr(sp, ptr1, False)
        Suc1 = SetProcessMitigationPolicy(3, ptr1, 4)
        Marshal.FreeHGlobal(ptr1)

        'Set ProcessSystemCallDisablePolicy with all = 4 (Media Read only protect)
        'Set ProcessMitigationOptionsMask = 5 (Not to use)

        'Set ProcessExtensionPointDisablePolicy = 6
        sp = &B1
        Dim ptr2 As IntPtr = Marshal.AllocHGlobal(4)
        Marshal.StructureToPtr(sp, ptr2, False)
        Suc1 = SetProcessMitigationPolicy(6, ptr2, 4)
        Marshal.FreeHGlobal(ptr2)

        'Set ProcessControlFlowGuardPolicy = 7 (CFG)(Access denied)(for C only)

        'Set ProcessSignaturePolicy = 8 with MicrosoftSignedOnly 
        sp = &B1
        Dim ptr3 As IntPtr = Marshal.AllocHGlobal(4)
        Marshal.StructureToPtr(sp, ptr3, False)
        Suc1 = SetProcessMitigationPolicy(8, ptr3, 4)
        Marshal.FreeHGlobal(ptr3)

        'Set ProcessFontDisablePolicy = 9
        sp = &B1
        Dim ptr5 As IntPtr = Marshal.AllocHGlobal(4)
        Marshal.StructureToPtr(sp, ptr5, False)
        Suc1 = SetProcessMitigationPolicy(9, ptr5, 4)
        Marshal.FreeHGlobal(ptr5)

        'Set ProcessImageLoadPolicy = 10
        sp = &B111
        Dim ptr6 As IntPtr = Marshal.AllocHGlobal(4)
        Marshal.StructureToPtr(sp, ptr6, False)
        Suc1 = SetProcessMitigationPolicy(10, ptr6, 4)
        Marshal.FreeHGlobal(ptr6)

        'Set ProcessSystemCallFilterPolicy =11 (??? undocument? 87error)
        'Set ProcessPayloadRestrictionPolicy =12 (??? undocument? 87error)
        'Set ProcessChildProcessPolicy = 13 (make restart fail)

        'Set ProcessSideChannelIsolationPolicy = 14 
        sp = &B1111 'bit 5 cant use
        Dim ptr8 As IntPtr = Marshal.AllocHGlobal(4)
        Marshal.StructureToPtr(sp, ptr8, False)
        Suc1 = SetProcessMitigationPolicy(14, ptr8, 4)
        Marshal.FreeHGlobal(ptr8)

        'Set ProcessUserShadowStackPolicy = 15 (Access deined) (Win10 2004) (Need Hardware)
        'Set ProcessRedirectionTrustPolicy = 16 (Access deined) 
        'Set ProcessUserPointerAuthPolicy = 17 (Access deined) 
        'Set ProcessSEHOPPolicy = 18 (87error)

        'MsgBox(New Win32Exception(Marshal.GetLastWin32Error).Message)

    End Sub
End Module

Public Module SE01
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

    Private Declare Function SetProcessWorkingSetSizeEx Lib "kernel32.dll" _
    (ByVal hProcess As Integer, ByVal dwMinimumWorkingSetSize As Long, ByVal dwMaximumWorkingSetSize As Long, ByVal Flags As Integer) As Integer
    Private Const QUOTA_LIMITS_HARDWS_MIN_ENABLE As Integer = &H1
    Private Const QUOTA_LIMITS_HARDWS_MAX_DISABLE As Integer = &H8

    Public Sub SeLock()

        Try
            Dim proc As Process = Process.GetCurrentProcess()
            Dim hProc As IntPtr = proc.Handle
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
                    SetProcessWorkingSetSizeEx(hProc, 20971520, 209715200, QUOTA_LIMITS_HARDWS_MIN_ENABLE Or QUOTA_LIMITS_HARDWS_MAX_DISABLE)

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
