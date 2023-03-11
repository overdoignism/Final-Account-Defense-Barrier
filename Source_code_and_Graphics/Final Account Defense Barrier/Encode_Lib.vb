'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Imports System.Security.Cryptography
Imports System.Text
Imports System.IO

Public Class Encode_Libs : Implements IDisposable

    Private disposedValue As Boolean

    Public Overloads Function AES_Encrypt_String_Return_String(ByRef Input_String As String, ByRef AES_Key() As Byte, ByRef AES_IV() As Byte) As String

        AES_Encrypt_String_Return_String = AES_Encrypt_Byte_Return_String(Encoding.GetEncoding("UTF-8").GetBytes(Input_String), AES_Key, AES_IV)

    End Function


    Public Function AES_Encrypt_Byte_Return_String(ByRef Input_byte() As Byte, ByRef AES_Key() As Byte, ByRef AES_IV() As Byte) As String

        Try
            Dim AES As New RijndaelManaged()
            AES.KeySize = 256
            AES.Key = Security.Cryptography.ProtectedData.Unprotect(AES_Key, Nothing, DataProtectionScope.CurrentUser)
            AES.IV = AES_IV
            AES.Mode = CipherMode.CBC
            AES.Padding = PaddingMode.PKCS7

            Dim stream1 As New MemoryStream()
            Dim stream2 As New CryptoStream(stream1, AES.CreateEncryptor(), CryptoStreamMode.Write)
            stream2.Write(Input_byte, 0, Input_byte.Length)
            stream2.FlushFinalBlock()

            Dim ReturnString As String = ByteIn_StringOut(stream1.ToArray)

            ClearMS(stream1)
            stream2.Clear()
            stream1.Close()
            stream2.Close()
            stream1.Dispose()
            stream2.Dispose()
            AES.Dispose()

            Return ReturnString

        Catch Exception_Name As Exception
            MsgBox(Exception_Name.Message, 0, TextStrs(5))
            Return ""
        End Try
    End Function

    Public Function AES_Decrypt_Str_Return_Bytes(ByRef Input_string As String, ByRef AES_Key() As Byte, ByRef AES_IV() As Byte) As Byte()

        Try
            Dim AES As New RijndaelManaged()

            AES.KeySize = 256
            AES.Key = Security.Cryptography.ProtectedData.Unprotect(AES_Key, Nothing, DataProtectionScope.CurrentUser)
            AES.IV = AES_IV
            AES.Mode = CipherMode.CBC
            AES.Padding = PaddingMode.PKCS7

            Dim buffer(0) As Byte
            StringIn_ByteOut(Input_string, buffer)

            Dim stream1 As New MemoryStream()
            Dim stream2 As New CryptoStream(stream1, AES.CreateDecryptor(), CryptoStreamMode.Write)

            stream2.Write(buffer, 0, buffer.Length)
            stream2.FlushFinalBlock()

            AES_Decrypt_Str_Return_Bytes = stream1.ToArray()

            ClearMS(stream1)
            stream2.Clear()
            stream1.Close()
            stream2.Close()
            stream1.Dispose()
            stream2.Dispose()
            AES.Dispose()

        Catch Exception_Name As Exception
            Return Nothing
        End Try

    End Function

    Public Sub StringIn_ByteOut(ByRef InputString As String, ByRef OutputByte() As Byte)

        Dim TmpVal As Integer = (InputString.Length / 2) - 1
        ReDim OutputByte(TmpVal)
        For IDX01 As Integer = 0 To TmpVal
            OutputByte(IDX01) = CByte(Convert.ToInt32(InputString.Substring(IDX01 * 2, 2), 16))
        Next

    End Sub

    Public Function ByteIn_StringOut(ByRef InByte() As Byte) As String

        Dim TheStringBuilder As New StringBuilder()
        For Each B As Byte In InByte
            TheStringBuilder.AppendFormat("{0:X2}", B)
        Next

        Return TheStringBuilder.ToString

    End Function

    'Not use yet
    Public Function Get_The_IV_BySHA512(ByRef inputStr As String) As Byte()

        Dim SHA512_Worker As New Security.Cryptography.SHA512CryptoServiceProvider

        Dim WorkBytes512b(0) As Byte
        Dim WorkBytes16(0) As Byte

        StringIn_ByteOut(inputStr, WorkBytes512b)

        For IDX01 As Integer = 0 To 1023
            WorkBytes512b = SHA512_Worker.ComputeHash(WorkBytes512b)
        Next

        Buffer.BlockCopy(WorkBytes512b, 0, WorkBytes16, 0, 16)
        SHA512_Worker.Dispose()

        Return WorkBytes16

    End Function

    Public Sub Get_The_IV_ByRND(ByRef TheIV() As Byte)
        Dim rng As RandomNumberGenerator = RandomNumberGenerator.Create()
        rng.GetBytes(TheIV)
        rng.Dispose()
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

