'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Imports System.ComponentModel
Imports System.IO
Imports System.Runtime.InteropServices

Public Class FormFileExplorer

    Dim DriverList() As String
    Const UpperDir As String = "<..>"
    Public Final_File As String
    Public BigByte() As Byte

    Private lastLocation As Point
    Private isMouseDown As Boolean = False

    Private Sub FormFileExplorer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim drives() As DriveInfo = DriveInfo.GetDrives()
        ReDim DriverList(2)

        Dim IDX01 As Integer

        LabelPath.Text = My.Application.Info.DirectoryPath
        DriverList(0) = My.Application.Info.DirectoryPath
        ListBoxDrivers.Items.Add(TextStrs(47))
        DriverList(1) = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        ListBoxDrivers.Items.Add(TextStrs(45))
        DriverList(2) = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        ListBoxDrivers.Items.Add(TextStrs(46))
        IDX01 = 2

        Me.Text = TextStrs(61)

        For Each drive As DriveInfo In drives
            If drive.IsReady Then
                IDX01 += 1
                ListBoxDrivers.Items.Add(drive.Name)
                ReDim Preserve DriverList(IDX01)
                DriverList(IDX01) = drive.Name
            End If
        Next

        ListBoxDrivers.SelectedIndex = 0

    End Sub

    Private Sub ListBoxDrivers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxDrivers.SelectedIndexChanged

        Patch_Changed(DriverList(ListBoxDrivers.SelectedIndex))
        Exe_Fill_Trash()

    End Sub

    Private Function Patch_Changed(ByRef New_Patch As String) As Boolean

        ListBoxFiles.Items.Clear()
        ButtonOK.Enabled = False

        If New_Patch.Length > 3 Then
            ListBoxFiles.Items.Add(UpperDir)
        End If

        Try
            For Each foundDirectory As String In My.Computer.FileSystem.GetDirectories(New_Patch)
                ListBoxFiles.Items.Add("<" + Path.GetFileName(foundDirectory) + ">")
            Next
        Catch ex As Exception
            Return False
        End Try

        Try
            For Each foundFiles As String In My.Computer.FileSystem.GetFiles(New_Patch)
                ListBoxFiles.Items.Add(Path.GetFileName(foundFiles))
            Next
        Catch ex As Exception
            Return False
        End Try

        LabelPath.Text = New_Patch
        Return True

    End Function

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        GoWork()
    End Sub

    Private Sub ListBoxFiles_DoubleClick(sender As Object, e As EventArgs) Handles ListBoxFiles.DoubleClick
        GoWork()
    End Sub

    Private Sub ListBoxFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxFiles.SelectedIndexChanged

        If ListBoxFiles.SelectedIndex = -1 Then
            ButtonOK.Enabled = False
        Else
            ButtonOK.Enabled = True
        End If

    End Sub

    Private Sub PictureBox8_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBox8.MouseDown
        ' 紀錄滑鼠按下的位置
        isMouseDown = True
        lastLocation = e.Location
    End Sub

    Private Sub PictureBox8_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBox8.MouseMove
        ' 當滑鼠左鍵按下時，設定窗體的位置
        If isMouseDown Then
            Me.Location = New Point(Me.Location.X + (e.X - lastLocation.X), Me.Location.Y + (e.Y - lastLocation.Y))
        End If
    End Sub

    Private Sub PictureBox8_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBox8.MouseUp
        ' 當滑鼠左鍵釋放時，重設 isMouseDown 變數
        isMouseDown = False
    End Sub

    Private Sub GoWork()

        If ListBoxFiles.SelectedIndex = -1 Then Exit Sub

        Dim GetFile As String = ListBoxFiles.Items(ListBoxFiles.SelectedIndex)
        Dim Org_Path As String = LabelPath.Text

        If GetFile = UpperDir Then

            Patch_Changed(Path.GetDirectoryName(LabelPath.Text))
            Exe_Fill_Trash()

        ElseIf GetFile.First = "<" Then

            Dim New_Path As String

            If LabelPath.Text.Last = "\" Then
                New_Path = (LabelPath.Text + GetFile.Replace("<", "").Replace(">", ""))
            Else
                New_Path = (LabelPath.Text + "\" + GetFile.Replace("<", "").Replace(">", ""))
            End If

            If Not Patch_Changed(New_Path) Then
                Patch_Changed(Org_Path)
                MSGBOXNEW(TextStrs(48), MsgBoxStyle.Exclamation, TextStrs(5), Me, PictureGray)
            End If
            Exe_Fill_Trash()

        Else

            Dim Full_Filename As String

            If Org_Path.Last = "\" Then
                Full_Filename = Org_Path + GetFile
            Else
                Full_Filename = Org_Path + "\" + GetFile
            End If

            Select Case ReadFileViaAPI(Full_Filename, BigByte)
                Case 100
                    MSGBOXNEW(TextStrs(58), MsgBoxStyle.Exclamation, TextStrs(5), Me, PictureGray)
                    Exit Sub
                Case > 0
                    MSGBOXNEW(TextStrs(48), MsgBoxStyle.Exclamation, TextStrs(5), Me, PictureGray)
                    Exit Sub
                Case Else

            End Select

            Me.DialogResult = DialogResult.OK

        End If

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub FormFileExplorer_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        ListBoxDrivers.Dispose()
        ClearLabel(LabelPath)
    End Sub

    Private Const GENERIC_READ As Integer = &H80000000
    Private Const OPEN_EXISTING As Integer = 3
    Private Const FILE_ATTRIBUTE_NORMAL As Integer = &H80
    Private Const FILE_ATTRIBUTE_READONLY As Integer = &H1
    Private Const FILE_ATTRIBUTE_SYSTEM As Integer = &H4
    Private Const FILE_ATTRIBUTE_HIDDEN As Integer = &H2
    Private Const FILE_ATTRIBUTE_ALL As Integer = FILE_ATTRIBUTE_NORMAL Or FILE_ATTRIBUTE_READONLY Or FILE_ATTRIBUTE_SYSTEM Or FILE_ATTRIBUTE_HIDDEN

    Private Structure SECURITY_ATTRIBUTES
        Public nLength As Integer
        Public lpSecurityDescriptor As IntPtr
        Public bInheritHandle As Boolean
    End Structure

    Private Declare Unicode Function CreateFileW Lib "kernel32.dll" _
    (ByVal lpFileName As String, ByVal dwDesiredAccess As Integer,
    ByVal dwShareMode As Integer, ByVal lpSecurityAttributes As IntPtr,
    ByVal dwCreationDisposition As Integer, ByVal dwFlagsAndAttributes As Integer,
    ByVal hTemplateFile As IntPtr) As IntPtr

    Private Declare Function ReadFile Lib "kernel32.dll" _
        (ByVal hFile As IntPtr, ByVal lpBuffer As Byte(), ByVal nNumberOfBytesToRead As Integer,
         ByRef lpNumberOfBytesRead As Integer, ByVal lpOverlapped As IntPtr) As Boolean

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function GetFileSizeEx(
                <[In]()> ByVal hFile As IntPtr,
                <[In](), Out()> ByRef lpFileSize As Long) As Boolean
    End Function

    Private Declare Function CloseHandle Lib "kernel32.dll" (ByVal hObject As IntPtr) As Boolean

    Private Function ReadFileViaAPI(ByRef OpenTheFile As String, ByRef ReturnBytes() As Byte) As Integer

        Try

            Dim ErrInt As Integer

            Dim hFile As IntPtr = CreateFileW(OpenTheFile, GENERIC_READ, 0, Nothing,
                                         OPEN_EXISTING, FILE_ATTRIBUTE_ALL, Nothing)

            If hFile.ToInt32() = -1 Then Return 10

            Dim fileSize As ULong
            ErrInt = GetFileSizeEx(hFile, fileSize)

            If ErrInt = 0 Then
                CloseHandle(hFile)
                Return 20
            End If

            If (fileSize = 0) Or (fileSize > File_Limit) Then
                CloseHandle(hFile)
                Return 100
            End If

            ReDim ReturnBytes(fileSize - 1)
            Dim bytesRead As Integer = 0
            ErrInt = ReadFile(hFile, ReturnBytes, ReturnBytes.Length, bytesRead, Nothing)

            If ErrInt = 0 Then
                CloseHandle(hFile)
                Return 30
            End If

            CloseHandle(hFile)
            Return 0

        Catch ex As Exception

            Return 50

        End Try

    End Function

End Class

