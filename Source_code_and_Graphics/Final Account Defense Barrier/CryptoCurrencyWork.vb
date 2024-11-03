'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Imports System.Numerics

'======= For check CryptoCurrency address

Public Class CryptoCurrencyWork : Implements IDisposable

    Private disposedValue As Boolean

    Public DetectType As Integer
    Public DetectState As Integer

    Public Sub DetectCurrency(TheInputStr As String)

        DetectType = 0
        '0 = Not Detect
        '1 = BTC legacy or compatible
        '2 = TRON TRX (BTC compatible)
        '3 = Doge (BTC compatible)
        '4 = LTC (BTC compatible)
        '5 = ETH or compatible 
        '6 = BTC new 

        DetectState = 0
        '0 = Not detect
        '1 = Detected but bad
        '2 = Detected and OK

        '============================== BTC legacy compatible
        Select Case Test_BTC_Addr_Mode1(TheInputStr)
            Case 0
                DetectState = 2
            Case 1
                DetectState = 0
            Case 2 To 4
                DetectState = 1
        End Select
        If DetectState > 0 Then
            Select Case Char.ToUpper(TheInputStr.First)
                Case "1", "3"
                    DetectType = 1
                Case "T"
                    DetectType = 2
                Case "D"
                    DetectType = 3
                Case "L", "M"
                    DetectType = 4
            End Select
        End If

        '============================= ETH compatible
        If DetectState = 0 Then
            Select Case Test_ETH_Addr(TheInputStr)
                Case 2
                    DetectType = 5
                    DetectState = 2
                Case 1
                    DetectType = 5
                    DetectState = 1
                Case 0
                    DetectState = 0
            End Select
        End If

        '============================== BTC new
        If DetectState = 0 Then
            Select Case TheInputStr.Length
                Case 39 To 44, 59 To 64
                    If TheInputStr.Substring(0, 4).ToUpper = "BC1Q" Then
                        If TheInputStr.Length <> 42 And TheInputStr.Length <> 62 Then DetectState = 1
                        If TheInputStr.Substring(0, 4) <> "bc1q" Then DetectState = 1
                        Select Case TestB32NotMatch(TheInputStr.Substring(4, TheInputStr.Length - 4))
                            Case 0
                                DetectType = 6
                                If DetectState = 0 Then DetectState = 2
                            Case <= 3
                                DetectType = 6
                                DetectState = 1
                            Case Else
                                DetectState = 0
                        End Select
                    End If
            End Select
        End If

    End Sub

    Private Function Test_ETH_Addr(InputStr As String) As Integer

        Dim Addr_State As Integer
        '0 = Not ETH Addr
        '1 = Sus ETH Addr
        '2 = OK ETH Addr

        Select Case InputStr.Length
            Case 42
                Addr_State = 2
            Case 39 To 44
                Addr_State = 1
            Case Else
                Addr_State = 0
        End Select

        If Addr_State > 0 Then
            If InputStr.Substring(0, 2).ToUpper = "0X" Then
                If InputStr.Substring(0, 2) = "0X" Then Addr_State = 1
                Select Case TestHexNotMatch(InputStr.Substring(2, InputStr.Length - 2))
                    Case 0
                    Case <= 3
                        Addr_State = 1
                    Case Else
                        Addr_State = 0
                End Select
            Else
                Addr_State = 0
            End If
        End If

        Return Addr_State

    End Function

    Private Function TestHexNotMatch(ByRef InputStr As String) As Integer

        Dim InputStrChr() As Char = InputStr.ToLower.ToCharArray
        Dim B16() As Char = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f"}
        Dim NotMatch As Boolean
        Dim ErrCount As Integer = 0

        For Each IChar As Char In InputStrChr
            For IDX01 As Integer = 0 To 15
                NotMatch = False
                If IChar = B16(IDX01) Then Exit For
                NotMatch = True
            Next
            If NotMatch Then ErrCount += 1
        Next

        Return ErrCount

    End Function

    Private Function TestB32NotMatch(ByRef InputStr As String) As Integer

        Dim InputStrChr() As Char = InputStr.ToCharArray
        Dim B32() As Char = {"q", "p", "z", "r", "y", "9", "x", "8", "g", "f", "2", "t", "v", "d", "w", "0",
            "s", "3", "j", "n", "5", "4", "k", "h", "c", "e", "6", "m", "u", "a", "7", "l"}
        Dim NotMatch As Boolean
        Dim ErrCount As Integer = 0

        For Each IChar As Char In InputStrChr
            For IDX01 As Integer = 0 To B32.Length - 1
                NotMatch = False
                If IChar = B32(IDX01) Then Exit For
                NotMatch = True
            Next
            If NotMatch Then ErrCount += 1
        Next

        Return ErrCount

    End Function

    Private Function Test_BTC_Addr_Mode1(AddrStr As String) As Integer

        ' 0 = Pass
        ' 1 = Detect as not a Address
        ' 2 = Base58 pass but length error
        ' 3 = Length pass but there has non Base58 word
        ' 4 = Base58 and Length passed but Checksum bad (or prefix lower case)

        If AddrStr = "" Then Return 1

        Dim LengthBad As Integer

        Select Case Char.ToUpper(AddrStr.First)
            Case "1"
                Select Case AddrStr.Length
                    Case 26 To 34
                    Case 23 To 38
                        LengthBad = 1
                    Case Else
                        Return 1
                End Select
            Case "L", "M"
                Select Case AddrStr.Length
                    Case 30 To 34
                    Case 27 To 36
                        LengthBad = 1
                    Case Else
                        Return 1
                End Select
            Case "3", "D", "T"
                Select Case AddrStr.Length
                    Case 34
                    Case 31 To 36
                        LengthBad = 1
                    Case Else
                        Return 1
                End Select
            Case Else
                Return 1
        End Select

        Dim BNum As BigInteger = DecodeBase58(AddrStr)

        If BNum = -1 Then
            If LengthBad = 1 Then
                Return 1
            Else
                Return 3
            End If
        Else
            If LengthBad = 1 Then Return 2
        End If

        Dim ByteArray() As Byte = Nothing
        Dim WorkStr As String = New String("0", 50 - BNum.ToString("X").Length) + BNum.ToString("X")

        StringIn_ByteOut(WorkStr, ByteArray)

        Dim CheckSum(3) As Byte
        Dim MainBody(ByteArray.Length - 5) As Byte

        Array.Copy(ByteArray, 0, MainBody, 0, ByteArray.Length - 4)
        Array.Copy(ByteArray, ByteArray.Length - 4, CheckSum, 0, 4)

        Dim Check256Result() As Byte

        Dim SHA256_Worker As New Security.Cryptography.SHA256CryptoServiceProvider
        Check256Result = SHA256_Worker.ComputeHash(MainBody)
        Check256Result = SHA256_Worker.ComputeHash(Check256Result)
        SHA256_Worker.Dispose()

        For IDX01 As Integer = 0 To 3
            If CheckSum(IDX01) <> Check256Result(IDX01) Then
                Return 3
            End If
        Next

        Return 0

    End Function

    Private Function DecodeBase58(InputStr As String) As BigInteger

        Dim Base58Map As Char() = {"1", "2", "3", "4", "5", "6", "7", "8", "9",
           "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R",
           "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h",
           "i", "j", "k", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"}

        Dim B58IdxV As Integer
        Dim BigNum As BigInteger = 0
        Dim AddrStrChr() As Char = InputStr.ToCharArray

        For IDX01 As Integer = 0 To InputStr.Length - 1

            B58IdxV = 0

            Do
                If AddrStrChr(IDX01) = Base58Map(B58IdxV) Then Exit Do
                B58IdxV += 1
            Loop Until B58IdxV = Base58Map.Length

            If B58IdxV = Base58Map.Length Then
                Return -1
            End If

            BigNum = (BigNum * 58) + B58IdxV

        Next

        Return BigNum

    End Function

    'Private Function ByteIn_StringOut(ByRef InByte() As Byte) As String

    '    Dim TheStringBuilder As New System.Text.StringBuilder()
    '    For Each B As Byte In InByte
    '        TheStringBuilder.AppendFormat("{0:X2}", B)
    '    Next

    '    Return TheStringBuilder.ToString

    'End Function

    Private Sub StringIn_ByteOut(ByRef InputString As String, ByRef OutputByte() As Byte)

        Dim TmpVal As Integer = (InputString.Length / 2) - 1
        ReDim OutputByte(TmpVal)
        For IDX01 As Integer = 0 To TmpVal
            OutputByte(IDX01) = CByte(Convert.ToInt32(InputString.Substring(IDX01 * 2, 2), 16))
        Next

    End Sub

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: 處置受控狀態 (受控物件)
            End If

            ' TODO: 釋出非受控資源 (非受控物件) 並覆寫完成項
            ' TODO: 將大型欄位設為 Null
            disposedValue = True
        End If
    End Sub

    ' ' TODO: 僅有當 'Dispose(disposing As Boolean)' 具有會釋出非受控資源的程式碼時，才覆寫完成項
    ' Protected Overrides Sub Finalize()
    '     ' 請勿變更此程式碼。請將清除程式碼放入 'Dispose(disposing As Boolean)' 方法
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' 請勿變更此程式碼。請將清除程式碼放入 'Dispose(disposing As Boolean)' 方法
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub

End Class

Module BIP39Work

    Public Structure BIP39Str
        Public BIP39Word() As String
    End Structure

    Public BIP39_Word(9) As BIP39Str

    Public Sub Load_BIP39_Word()

        Dim memoryStream As New IO.MemoryStream()
        Dim BIP39Text As String = ""

        Using archive As New System.IO.Compression.ZipArchive(New IO.MemoryStream(My.Resources.Resource1.TXTFile))
            For Each entry As System.IO.Compression.ZipArchiveEntry In archive.Entries
                If entry.Name = "BIP39WORD.txt" Then
                    Using entryStream As IO.Stream = entry.Open()
                        entryStream.CopyTo(memoryStream)
                        memoryStream.Position = 0
                        Dim sr As New IO.StreamReader(memoryStream, True)
                        BIP39Text = sr.ReadToEnd()
                    End Using
                    Exit For
                End If
            Next
        End Using

        BIP39Text = Replace(BIP39Text, vbCrLf, vbCr)
        Dim BIP39Text_Arr() As String = BIP39Text.Split(vbCr)

        For IDX01 As Integer = 0 To 9
            BIP39_Word(IDX01).BIP39Word = BIP39Text_Arr(IDX01).Split(",")
        Next

    End Sub

End Module
