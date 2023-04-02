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

    Private Sub FormFileExplorer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '====================== Listbox Scrollbar work
        UpY = LSCBU.Top + LSCBU.Height
        DwY = LSCBD.Top - LSCBBAR.Height + 1
        LSCB_UPDW.Interval = 200
        LSCB_UPDW.Enabled = False
        LSCB_MSC.Interval = 100
        LSCB_MSC.Enabled = False
        LB_Ration = ListBoxFiles.ClientRectangle.Height / ListBoxFiles.ItemHeight

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

    '=============== Functions and subs 

    Private Function Patch_Changed(ByRef New_Patch As String) As Boolean

        ListBoxFiles.Items.Clear()
        ButtonFileOpen.Enabled = False
        ButtonFileOpen.Image = B_OpenF_DI

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

    Private Sub GoWork()

        If ListBoxFiles.SelectedIndex = -1 Then Exit Sub

        Dim GetFile As String = ListBoxFiles.Items(ListBoxFiles.SelectedIndex)
        Dim Org_Path As String = LabelPath.Text

        If GetFile = UpperDir Then

            Patch_Changed(Path.GetDirectoryName(LabelPath.Text))
            Exe_Fill_Trash()
            LB_Range_Scale = CDbl(ListBoxFiles.Items.Count) - LB_Ration
            GoCorrectPos()

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
            LB_Range_Scale = CDbl(ListBoxFiles.Items.Count) - LB_Ration
            GoCorrectPos()
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

    '================= Form contorls work

    Private Sub ListBoxDrivers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxDrivers.SelectedIndexChanged

        Patch_Changed(DriverList(ListBoxDrivers.SelectedIndex))
        Exe_Fill_Trash()
        LB_Range_Scale = CDbl(ListBoxFiles.Items.Count) - LB_Ration
        GoCorrectPos()

    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonFileOpen.Click
        GoWork()
    End Sub

    Private Sub ListBoxFiles_DoubleClick(sender As Object, e As EventArgs) Handles ListBoxFiles.DoubleClick
        GoWork()
    End Sub

    Private Sub ListBoxFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxFiles.SelectedIndexChanged

        If ListBoxFiles.SelectedIndex = -1 Then
            ButtonFileOpen.Enabled = False
            ButtonFileOpen.Image = B_OpenF_DI
        Else
            ButtonFileOpen.Enabled = True
            ButtonFileOpen.Image = My.Resources.Resource1.button_OpenF
        End If

        GoCorrectPos()

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub FormFileExplorer_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        ListBoxDrivers.Dispose()
        ClearLabel(LabelPath)
    End Sub

    '============================= Read file via API avoid memory leak=================================

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

    '====================================== Window base operate =====================

    Private lastLocation As Point
    Private isMouseDown As Boolean = False

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

    '========================================== ListBox Scrollbar work

    Dim WithEvents LSCB_UPDW As New Windows.Forms.Timer
    Dim WithEvents LSCB_MSC As New Windows.Forms.Timer
    Dim NowUPorDW As Integer
    Dim LB_Ration As Double
    Dim LB_Range_Scale As Double
    Dim UpY As Integer
    Dim DwY As Integer
    Dim BarIsHolding As Boolean

    Private Sub LSCBU_MouseDown(sender As Object, e As MouseEventArgs) Handles _
        LSCBU.MouseDown, LSCBD.MouseDown, LSCBBACK.MouseDown

        If sender.Name = "LSCBU" Then
            NowUPorDW = 0
            ListBoxFiles.TopIndex -= 1
            GoCorrectPos()
        ElseIf sender.Name = "LSCBD" Then
            NowUPorDW = 1
            ListBoxFiles.TopIndex += 1
            GoCorrectPos()
        Else
            Dim WhereIsY As Integer = e.Y + LSCBBACK.Top
            If WhereIsY < LSCBBAR.Top Then
                NowUPorDW = 2
                If ListBoxFiles.TopIndex - LB_Ration < 0 Then
                    ListBoxFiles.TopIndex = 0
                Else
                    ListBoxFiles.TopIndex -= LB_Ration
                End If
                GoCorrectPos()
            Else
                NowUPorDW = 3
                ListBoxFiles.TopIndex += LB_Ration
                GoCorrectPos()
            End If

        End If

        LSCB_UPDW.Enabled = True
    End Sub

    Private Sub LSCBWORK(ByVal sender As Object, ByVal e As EventArgs) Handles LSCB_UPDW.Tick
        Select Case NowUPorDW
            Case 0
                ListBoxFiles.TopIndex -= 1
            Case 1
                ListBoxFiles.TopIndex += 1
            Case 2
                If ListBoxFiles.TopIndex - LB_Ration < 0 Then
                    ListBoxFiles.TopIndex = 0
                Else
                    ListBoxFiles.TopIndex -= LB_Ration
                End If
            Case 3
                ListBoxFiles.TopIndex += LB_Ration
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
            Dim TmpIdx As Double = CDbl(ListBoxFiles.TopIndex) / LB_Range_Scale
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
                Cursor = Cursors.SizeNS ' 更改游標形狀以指示按住按鈕時可以移動它
            End If
        End If
    End Sub

    Private Sub LSCBBAR_MouseMove(sender As Object, e As MouseEventArgs) Handles LSCBBAR.MouseMove

        If BarIsHolding Then

            Dim TmpY As Integer = sender.Top + e.Y - sender.Height / 2
            'sender.Left += e.X - sender.Width / 2 ' 移動按鈕的位置

            If (TmpY >= UpY) And (TmpY <= DwY) Then
                ListBoxFiles.TopIndex = CInt((CDbl(sender.Top - UpY) / CDbl(DwY - UpY)) * LB_Range_Scale)
                sender.Top = TmpY
            ElseIf TmpY < UpY Then
                ListBoxFiles.TopIndex = 0
                sender.Top = UpY
            End If

        End If
    End Sub

    Private Sub LSCBBAR_MouseUp(sender As Object, e As MouseEventArgs) Handles LSCBBAR.MouseUp
        BarIsHolding = False
        Cursor = Cursors.Default ' 將游標形狀恢復為預設值
    End Sub

    Private Sub Go_ListBoxIdx(Gowhere As Integer)
        ListBoxFiles.SelectedIndex = Gowhere
        GoCorrectPos()
    End Sub

    Private Sub ListBoxFiles_MouseWheel(sender As Object, e As MouseEventArgs) Handles ListBoxFiles.MouseWheel
        LSCB_MSC.Enabled = True
    End Sub

    Private Sub LSCB_MouseWheel(sender As Object, e As MouseEventArgs) Handles _
            LSCBBAR.MouseWheel, LSCBBACK.MouseWheel, LSCBD.MouseWheel, LSCBU.MouseWheel
        If e.Delta > 0 Then
            If ListBoxFiles.TopIndex - 3 < 0 Then
                ListBoxFiles.TopIndex = 0
            Else
                ListBoxFiles.TopIndex -= 3
            End If
        Else
            ListBoxFiles.TopIndex += 3
        End If
        GoCorrectPos()
    End Sub

    '=============== Button Visual Work =========================
    Dim B_OpenF As New Bitmap(My.Resources.Resource1.button_OpenF)
    Dim B_OpenF_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_OpenF)
    Dim B_Cancel_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_Cancel)

    Private Sub Mouse_Enter(sender As Object, e As EventArgs) Handles _
        ButtonFileOpen.MouseEnter, ButtonCancel.MouseEnter

        Select Case sender.Name
            Case "ButtonFileOpen"
                ButtonFileOpen.Image = B_OpenF_on
            Case "ButtonCancel"
                ButtonCancel.Image = B_Cancel_on
        End Select

    End Sub

    Dim B_OpenF_DI As Bitmap = Make_Button_Gray(My.Resources.Resource1.button_OpenF)
    Dim B_Cancel As New Bitmap(My.Resources.Resource1.button_Cancel)

    Private Sub Mouse_Leave(sender As Object, e As EventArgs) Handles _
        ButtonFileOpen.MouseLeave, ButtonCancel.MouseLeave

        Select Case sender.Name
            Case "ButtonFileOpen"
                If ButtonFileOpen.Enabled = True Then
                    ButtonFileOpen.Image = B_OpenF
                Else
                    ButtonFileOpen.Image = B_OpenF_DI
                End If
            Case "ButtonCancel"
                ButtonCancel.Image = B_Cancel
        End Select
    End Sub

End Class

