'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier


Imports System.Numerics
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography

Module CommonModule

    Public Structure FileLists
        Public FileName As String
        Public ShowName As String
        Public FileIsBad As Boolean
    End Structure

    Public DirName As String
    Public CAT_setting_Str() As String
    Public ClearWorking As Boolean = False
    Public ReadingWorking As Boolean = False

    'Auto Logout Timer
    Public ACTCount As Integer
    Public ACTLimit As Integer
    Public ACTLimitSelectIDX As Integer
    Public ACTLimitSelect() As Integer = {0, 30, 60, 180, 300, 600, 1800}

    'Const
    Public Const INDTstr As String = "AccountMan"
    Public Const Notefile As String = "CATNOTE.SET"

    Public Const No_Sec_Desk_str As String = "NOSECDESK"
    Public Const Allow_CAP_SCR_str As String = "ALLOWCAP"
    Public Const ATField_Mode_str As String = "PROTECT"
    Public Const TaskSch_Name_str As String = "FADB_ATFIELD"
    Public Const OPACITY_str As String = "OPACITY"
    Public Const SALT_str As String = "SALT"
    Public Const LangFile_str As String = "LANG"
    Public Const KDFType_str As String = "KDF"

    Public Const Author_str As String = "Final Account Defencse Barrier" + vbCrLf + "Developed by overdoingism Lab."

    Public Const Quo As String = """"
    Public Const D_vbcrlf As String = vbCrLf + vbCrLf

    Public Const MainWebURL As String = "https://github.com/overdoignism/Final-Account-Defense-Barrier"

    Public Const File_Limit As Long = 134217728

    Public SCutChar() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G",
        "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "-", "=",
        "[", "]", ";", "'", "<", ">", "/"}

    Public CoinList() As String = {"-", "BTC", "TRX", "Doge", "LTC", "ETH", "BTC"}


    ' Fixed by Gemini 3 for Wine
    Public Function BigInt_to_x36(InVal As BigInteger, AsFileName As Boolean) As String

        Dim x36Str() As Char = {"0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, "A"c, "B"c, "C"c,
        "D"c, "E"c, "F"c, "G"c, "H"c, "I"c, "J"c, "K"c, "L"c, "M"c, "N"c, "O"c, "P"c, "Q"c, "R"c, "S"c, "T"c, "U"c,
        "V"c, "W"c, "X"c, "Y"c, "Z"c}

        Dim OutputStr As String = ""
        Dim Tmp1 As Integer
        Dim Tmp2 As BigInteger = InVal ' 使用 BigInteger
        Dim Remainder As BigInteger = 0

        ' 如果是 0 的情況
        If Tmp2 <= 0 Then
            Return If(AsFileName, "0".PadLeft(13, "0"c), "0")
        End If

        Do
            ' BigInteger.DivRem 同時算出商數(Tmp2)與餘數(Remainder)
            ' 這比手寫 Mod 和 / 更快且原子化操作
            Tmp2 = BigInteger.DivRem(Tmp2, 36, Remainder)

            ' 將餘數轉為 Int32 (因為餘數絕對是 0-35，所以這行絕對安全)
            Tmp1 = CInt(Remainder)

            OutputStr = x36Str(Tmp1) & OutputStr
        Loop While Tmp2 > 0

        If AsFileName Then
            Return OutputStr.PadLeft(13, "0"c)
        Else
            Return OutputStr
        End If

    End Function

    Public Sub End_Program()

        If IsProtectMode Then Exit Sub

        FullGC()
        Exe_Fill_Trash()
        FullGC()

        Try

            UnregisterHotKey(FormMain.Handle, HotKeyID1)
            UnregisterHotKey(FormMain.Handle, HotKeyID2)

        Catch ex As Exception
        End Try

        End

    End Sub

    Public Function Random_Strs(LengthNeed_Min As Integer, LengthNeed_Max As Integer, StrMode As Integer) As String

        'StrMode 0 for SecureDesktop
        'StrMode 1 for Password generator and ACC fill generator
        'StrMode 2 for Password generator without symbos
        'StrMode 3 for Password generator with number only

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

        ElseIf StrMode = 3 Then

            For IDX01 As Long = 0 To LengthNeed - 1
                SpeedSB.Append(Get_RangeRnd_ByRNG(0, 9).ToString)
            Next

        End If

        Random_Strs = SpeedSB.ToString

    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Function GetWindowText(ByVal hWnd As IntPtr, ByVal lpString As System.Text.StringBuilder, ByVal nMaxCount As Integer) As Integer
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Function IsWindow(ByVal hWnd As IntPtr) As Boolean
    End Function

    Public Sub RestartApp2(Runas As Boolean, ByRef TheForm As Form)

        TheForm.Visible = False

        If LockFile IsNot Nothing Then
            If Not Sys_Chk._ReadOnlyMode Then LockFile.Close()
            If System.IO.File.Exists(LockFilePath) Then
                IO.File.Delete(LockFilePath)
            End If
        End If

        Dim info As New ProcessStartInfo(Application.ExecutablePath)

        info.UseShellExecute = True

        If Runas Then info.Verb = "runas"

        Dim Arguments() As String = Environment.GetCommandLineArgs()

        For IDX01 As Integer = 0 To Arguments.GetUpperBound(0)
            If IDX01 > 0 Then
                'If Arguments(IDX01).ToUpper <> "SECUREDESKTOP" Then NewArgStr += Arguments(IDX01) + " "
                info.Arguments += Arguments(IDX01) + " "
            End If
        Next

        Try

            Dim process As Process = Process.Start(info)

            Dim handle As IntPtr = IntPtr.Zero
            Dim windowTitle As New System.Text.StringBuilder(256) ' 用來存放窗口標題
            Dim title As String = String.Empty
            Dim WaitCount As Integer = 0

            'Wait child process init.
            Do
                Threading.Thread.Sleep(250)
                handle = process.MainWindowHandle
                ' 如果窗口句柄有效，取得窗口標題
                If handle <> IntPtr.Zero AndAlso IsWindow(handle) Then
                    GetWindowText(handle, windowTitle, windowTitle.Capacity)
                    title = windowTitle.ToString().Trim()
                End If

                WaitCount += 1
                If WaitCount = 720 Then Exit Do
                If process.HasExited Then Exit Do

            Loop While String.IsNullOrEmpty(title) ' 當窗口標題為空時，繼續等待

            'End If

        Catch ex As Exception

            'MsgBox(ex.Message)

        End Try

        End

        Threading.Thread.Sleep(250)
        End_Program()

    End Sub

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

    '============ Read-only mode

    Public LockFile As IO.StreamWriter
    Public LockFilePath As String
    Public PwdGrayImage As Bitmap = Make_Button_Gray(My.Resources.Resource1.TOPSEC)

    Function CreateLockFile() As Integer

        Sys_Chk._ReadOnlyMode = True

        LockFilePath = DirName + "\FADB.lock"
        Try

            LockFile = IO.File.CreateText(LockFilePath)
            Sys_Chk._ReadOnlyMode = False
            Return 0

        Catch e As Exception

            Select Case e.HResult And &HFFFF
                Case 32
                    Return 1
                Case Else
                    Return e.HResult
            End Select

        End Try
    End Function

    Function IsCatLocked(ByVal dirPath As String) As Boolean
        Try
            Dim FileAndPath As String = dirPath + "\FADB.lock"
            If Not IO.File.Exists(FileAndPath) Then Return False
            Using fs As New IO.FileStream(FileAndPath, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.None)
                fs.Close()
            End Using
            Return False
        Catch ex As IO.IOException
            Return True
        End Try
    End Function

    '============ HotKey Input

    Public HotKeyID1 As Integer = 1100
    Public HotKeyID2 As Integer = 2100

    <DllImport("user32.dll")>
    Public Function RegisterHotKey(hWnd As IntPtr, id As Integer, fsModifiers As Integer, vlc As Integer) As Boolean
    End Function

    <DllImport("user32.dll")>
    Public Function UnregisterHotKey(hWnd As IntPtr, id As Integer) As Boolean
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

    <System.Diagnostics.DebuggerStepThrough()>
    Public Function PreFilterMessage(ByRef m As Message) As Boolean Implements IMessageFilter.PreFilterMessage
        Select Case m.Msg
            Case WM_LBUTTONDOWN, WM_KEYDOWN, WM_RBUTTONDOWN, WM_MBUTTONDOWN ', WM_MOUSEMOVE 'For Auto Close Countdown
                ACTCount = 0
        End Select
        Return False
    End Function

End Class
