'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Imports System.IO
Imports System.Numerics
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports System.Text

<System.Security.SecurityCritical>
Public Class SmallSecurtiyWorkers : Implements IDisposable

    Private disposedValue As Boolean

    Public Function CheckBIP39(ByRef BIP39Str As String) As Integer

        Try
            Dim LotInt(0) As UInteger

            Dim PassLevel As Integer = 0
            'PassLevel 0 : Pass
            'PassLevel 1 : Length not match
            'PassLevel 2 : Word not in list
            'PassLevel 3 : CheckSum error

            Dim WorkStr() As String = BIP39Str.ToLower.Split(" ")
            If WorkStr.Length Mod 3 <> 0 Then PassLevel = 1
            If WorkStr.Length > 24 Then PassLevel = 1

            If PassLevel = 1 Then 'Why Japanese word make trouble
                PassLevel = 0
                WorkStr = BIP39Str.Split("　")
                If WorkStr.Length Mod 3 <> 0 Then PassLevel = 1
                If WorkStr.Length > 24 Then PassLevel = 1
            End If

            If PassLevel = 0 Then

                For IDX01 As Integer = 0 To WorkStr.Length - 1

                    ReDim Preserve LotInt(IDX01)
                    PassLevel = 2

                    For IDX02 As Integer = 0 To 9
                        For IDX03 As Integer = 0 To 2047
                            If String.CompareOrdinal(WorkStr(IDX01), BIP39_Word(IDX02).BIP39Word(IDX03)) = 0 Then
                                LotInt(IDX01) = IDX03
                                PassLevel = 0
                                Exit For
                            End If
                        Next
                        If PassLevel = 0 Then Exit For
                    Next
                    If PassLevel = 2 Then Exit For
                Next

            End If

            If PassLevel = 0 Then

                Dim LastDigi As UInteger
                Dim CheckSumVal As Byte
                Dim TheBIP39Tmp As UInteger
                Dim TBIP39IDX As Integer = 0
                Dim TmpBytes() As Byte
                Dim TheBIP39Bytes(((WorkStr.Length / 3) * 4) - 1) As Byte

                For IDX01 As Integer = 0 To WorkStr.Length - 1 Step 3

                    TheBIP39Tmp = (LastDigi << (32 - TBIP39IDX))
                    TheBIP39Tmp += (LotInt(IDX01) << (21 - TBIP39IDX))
                    TheBIP39Tmp += (LotInt(IDX01 + 1) << (10 - TBIP39IDX))
                    TheBIP39Tmp += (LotInt(IDX01 + 2) >> (1 + TBIP39IDX))

                    LastDigi = LotInt(IDX01 + 2) Mod (2 ^ (TBIP39IDX + 1))

                    TmpBytes = BitConverter.GetBytes(TheBIP39Tmp)
                    Array.Reverse(TmpBytes)
                    Array.Copy(TmpBytes, 0, TheBIP39Bytes, TBIP39IDX * 4, 4)
                    TBIP39IDX += 1

                Next

                Dim SHA256_Worker As New Security.Cryptography.SHA256CryptoServiceProvider
                CheckSumVal = SHA256_Worker.ComputeHash(TheBIP39Bytes)(0) >> (8 - TBIP39IDX)
                SHA256_Worker.Dispose()

                If LastDigi <> CheckSumVal Then PassLevel = 3

            End If

            ReDim WorkStr(0)
            WipeUINT(LotInt)

            Return PassLevel
        Catch ex As Exception
            Return 1
        End Try

    End Function

    Public Function EvaPwdStrong(ByRef InTextbox As TextBox) As Color

        Dim Score As Integer = 0
        Dim SNum As Boolean = False
        Dim SLow As Boolean = False
        Dim SUpp As Boolean = False
        Dim SSig As Boolean = False

        For Each TmpCHR As Char In InTextbox.Text
            Select Case TmpCHR
                Case "0" To "9"
                    SNum = True
                Case "a" To "z"
                    SLow = True
                Case "A" To "Z"
                    SUpp = True
                Case "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "+", "-", "=", "[", " "
                    SSig = True
                Case "]", "\", "{", "}", "|", ";", "'", ":", """", ",", ".", "/", "<", ">", "?"
                    SSig = True
            End Select
            Score += 1
        Next

        If SNum Then Score += 3
        If SLow Then Score += 3
        If SUpp Then Score += 3
        If SSig Then Score += 3

        If Score < 16 Then
            Sys_Chk._LoginKeyStrength = 0
            Return Color.FromArgb(224, 0, 0)
        ElseIf Score < 28 Then
            Sys_Chk._LoginKeyStrength = 1
            Return Color.FromArgb(176, 176, 0)
        Else
            Sys_Chk._LoginKeyStrength = 2
            Return Color.FromArgb(0, 224, 0)
        End If

    End Function

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

    Public Sub Dispose() Implements IDisposable.Dispose
        ' 請勿變更此程式碼。請將清除程式碼放入 'Dispose(disposing As Boolean)' 方法
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub

End Class

<System.Security.SecurityCritical>
Public Class FAST_KDF : Implements IDisposable

    '=================== KDF Function - MAGI-Crypt

    Dim AddrTableOrg() As Integer
    Public TheFullBuck() As Byte
    Public ErrCodeArray(3) As Integer

    Const TotalLength As Integer = 268435456 '256M
    Const LengthPerThread As UInteger = TotalLength / 4 '4 threads
    Const WorkTimesRound As Integer = 32768

    Public TotalLengthUpper As Integer = TotalLength - 1

    Private disposedValue As Boolean

    <DllImport("bcrypt.dll", SetLastError:=True)>
    Private Shared Function BCryptOpenAlgorithmProvider(
        ByRef phAlgorithm As IntPtr,
        ByVal pszAlgId As IntPtr, ' LPCWSTR
        ByVal pszImplementation As IntPtr,
        ByVal dwFlags As UInteger) As UInteger
    End Function

    <DllImport("bcrypt.dll")>
    Private Shared Function BCryptCreateHash(hAlgorithm As IntPtr, ByRef hHash As IntPtr, pbHashObject As IntPtr, cbHashObject As UInteger, pbSecret As IntPtr, cbSecret As UInteger, dwFlags As UInteger) As UInteger
    End Function

    <DllImport("bcrypt.dll")>
    Private Shared Function BCryptHashData(hHash As IntPtr, pbInput As Byte(), cbInput As UInteger, dwFlags As UInteger) As UInteger
    End Function

    <DllImport("bcrypt.dll")>
    Private Shared Function BCryptFinishHash(hHash As IntPtr, pbOutput As Byte(), cbOutput As UInteger, dwFlags As UInteger) As UInteger
    End Function

    <DllImport("bcrypt.dll")>
    Private Shared Function BCryptCloseAlgorithmProvider(hAlgorithm As IntPtr, dwFlags As UInteger) As UInteger
    End Function

    <DllImport("bcrypt.dll")>
    Private Shared Function BCryptDestroyHash(hHash As IntPtr) As UInteger
    End Function

    '<DllImport("kernel32.dll", SetLastError:=True)>
    'Private Function LocalFree(hMem As IntPtr) As IntPtr
    'End Function

    Public AlgorithmPtr512 As IntPtr = Marshal.StringToHGlobalUni("SHA512")
    Public AlgorithmPtr256 As IntPtr = Marshal.StringToHGlobalUni("SHA256")
    Dim ProviderPtr As IntPtr = Marshal.StringToHGlobalUni("Microsoft Primitive Provider")
    Dim hAlgorithm(3) As IntPtr
    Dim hHash(3) As IntPtr

    Const BCRYPT_PROV_DISPATCH As Integer = &H1
    Const BCRYPT_HASH_REUSABLE_FLAG As Integer = &H20

    Public Sub New()

        Dim FillDataUp As Integer = LengthPerThread / 64 - 1

        ReDim AddrTableOrg(FillDataUp)
        For IDX01 As Integer = 0 To FillDataUp
            AddrTableOrg(IDX01) = IDX01 * 64
        Next

    End Sub

    Public Function InitBCrypt(AlgorithmPtr As IntPtr, handle_IDX As Integer) As Integer

        Dim status As UInteger

        Try

            ProviderPtr = Marshal.StringToHGlobalUni("Microsoft Primitive Provider")

            ' Open an algorithm provider for SHA-256/SHA-512
            status = BCryptOpenAlgorithmProvider(hAlgorithm(handle_IDX), AlgorithmPtr, ProviderPtr, BCRYPT_HASH_REUSABLE_FLAG)
            If status <> 0 Then Throw New Exception()

            ' Create a hash object
            status = BCryptCreateHash(hAlgorithm(handle_IDX), hHash(handle_IDX), IntPtr.Zero, 0, IntPtr.Zero, 0, 0)
            If status <> 0 Then Throw New Exception()

        Catch ex As Exception

            BCryptCloseAlgorithmProvider(hAlgorithm(handle_IDX), 0)
            Return 1

        Finally

        End Try

        Return 0

    End Function

    Public Sub EndBCrypt(handle_IDX As Integer)

        ' Cleanup
        BCryptDestroyHash(hHash(handle_IDX))
        BCryptCloseAlgorithmProvider(hAlgorithm(handle_IDX), 0)

    End Sub

    Public Sub Get_MAGIC_4Piece(Input_Array() As Byte,
                                ByRef OutPut_Array1() As Byte,
                                ByRef OutPut_Array2() As Byte,
                                ByRef OutPut_Array3() As Byte,
                                ByRef OutPut_Array4() As Byte)

        Dim SHA512_Worker As New Security.Cryptography.SHA512CryptoServiceProvider

        OutPut_Array1 = SHA512_Worker.ComputeHash(Input_Array)

        OutPut_Array2 = OutPut_Array1.Clone
        Array.Reverse(OutPut_Array2)

        OutPut_Array3 = SHA512_Worker.ComputeHash(OutPut_Array2)
        Array.Reverse(OutPut_Array3)

        OutPut_Array4 = SHA512_Worker.ComputeHash(OutPut_Array3)
        Array.Reverse(OutPut_Array4)

    End Sub

    Public Sub KDF_MAGIcrypt(ByRef Input_Array() As Byte, ByRef Prograss As Integer,
                              ByRef FullBuck() As Byte, ThreadIDX As Integer,
                              ByRef ErrCode As Integer, SeLockMP As Boolean)

        Threading.Thread.Sleep(20)

        Dim AddrTableIdx As Integer

        Dim work_byte64() As Byte = Input_Array
        'Array.Copy(Input_Array, 0, work_byte64, 0, 32)

        Dim work_byte128(127) As Byte
        Dim TmpErrCode As Integer
        Dim PrograssSng As Single
        Dim pMemory As IntPtr

        Try

            TmpErrCode = InitBCrypt(AlgorithmPtr512, ThreadIDX)
            If TmpErrCode <> 0 Then Throw New Exception("1")

            Dim TheSizePtr As New UIntPtr(LengthPerThread)
            pMemory = VirtualAlloc(IntPtr.Zero, TheSizePtr, MEM_COMMIT Or MEM_RESERVE, PAGE_READWRITE)
            If pMemory = IntPtr.Zero Then Throw New Exception("2")

            Dim TheVLPtr As New UIntPtr(CUInt(8388608))
            If SeLockMP Then 'I dont know why I can only lock 8MB
                TmpErrCode = VirtualLock(pMemory, TheVLPtr)
                If TmpErrCode = 0 Then ErrCode = 4
            End If

            Dim AddrTable1(0) As Integer
            Dim AddrTable2(0) As Integer
            Dim AddrTable3(0) As Integer

            Dim TmpWork As Integer

            For LoopLeveL1 As Integer = 0 To 1023 ' 1024*1024 = TotalLength/64
                TmpWork = LoopLeveL1 * 1024
                For LoopLeveL2 As Integer = 0 To 1023
                    Marshal.Copy(work_byte64, 0, pMemory + AddrTableOrg(TmpWork + LoopLeveL2), 64)
                    HashWithBCryptSHA512(work_byte64, 64, work_byte64, ThreadIDX)
                Next
                PrograssSng += 0.0244140625 ' = (50*0.5)/1024  
                Prograss = Convert.ToInt32(PrograssSng)
            Next

            For LoopLeveL1 As Integer = 1 To 32 ' 32*WorkTimesRound = TotalLength/64

                AddrTableIdx = 0
                Wash_TableV2(BitConverter.ToInt32(work_byte64, 0), AddrTable1, AddrTable2, AddrTable3)

                For LoopLeveL2 As Integer = 0 To WorkTimesRound - 1
                    Marshal.Copy(pMemory + AddrTable1(AddrTableIdx), work_byte128, 0, 64)
                    Marshal.Copy(pMemory + AddrTable2(AddrTableIdx), work_byte128, 64, 64)
                    HashWithBCryptSHA512(work_byte128, 128, work_byte64, ThreadIDX)
                    Marshal.Copy(work_byte64, 0, pMemory + AddrTable3(AddrTableIdx), 64)
                    AddrTableIdx += 1
                Next

                PrograssSng += 0.78125 ' = (50*0.5)/32
                Prograss = Convert.ToInt32(PrograssSng)

            Next

            Marshal.Copy(pMemory, work_byte128, 0, 64)
            Array.Copy(work_byte64, 0, work_byte128, 64, 64)
            HashWithBCryptSHA512(work_byte128, 128, work_byte64, ThreadIDX)
            Marshal.Copy(work_byte64, 0, pMemory, 64)

            Marshal.Copy(pMemory, FullBuck, ThreadIDX * LengthPerThread, LengthPerThread)
            RtlFillMemory(pMemory, LengthPerThread, 0)
            'If SeLockMP Then VirtualUnlock(pMemory1, TheVLPtr)
            TmpErrCode = VirtualFree(pMemory, UIntPtr.Zero, MEM_RELEASE)
            If TmpErrCode = 0 Then ErrCode = 4

            WipeInteger(AddrTable1)
            WipeInteger(AddrTable2)
            WipeInteger(AddrTable3)
            WipeBytes(work_byte64)
            WipeBytes(work_byte128)
            WipeBytes(Input_Array)

            EndBCrypt(ThreadIDX)

        Catch ex As Exception

            Prograss = 1000

            Select Case ex.Message
                Case "1"
                    ErrCode = 1 '演算法無法初始化
                Case "2"
                    ErrCode = 2 '記憶體無法分配
                Case Else
                    ErrCode = 3 '不明的錯誤
                    VirtualFree(pMemory, UIntPtr.Zero, MEM_RELEASE)
            End Select

        End Try

        'ErrCode = 4 '不明的錯誤、不影響運作

    End Sub

    Private Sub Wash_TableV2(WhatSeed As Integer, ByRef AdderTable1() As Integer,
                             ByRef AdderTable2() As Integer, ByRef AdderTable3() As Integer)

        ReDim AdderTable1(WorkTimesRound - 1)
        ReDim AdderTable2(WorkTimesRound - 1)
        ReDim AdderTable3(WorkTimesRound - 1)

        Dim WorkableUpper As Integer = LengthPerThread - 64 + 1

        Dim GRnd As New Random(WhatSeed)

        For IDX02 As Integer = 0 To WorkTimesRound - 1
            AdderTable1(IDX02) = GRnd.Next(0, WorkableUpper)
            AdderTable2(IDX02) = GRnd.Next(0, WorkableUpper)

            If GRnd.Next(0, 2) = 0 Then
                AdderTable3(IDX02) = AdderTable1(IDX02)
            Else
                AdderTable3(IDX02) = AdderTable2(IDX02)
            End If

        Next

    End Sub

    Public Function Finish_MAGIC_KDF(ByRef LargeBytes() As Byte) As Byte()

        Dim SHA512_Worker As New Security.Cryptography.SHA512CryptoServiceProvider
        Dim SHA256_Worker As New Security.Cryptography.SHA256CryptoServiceProvider

        Dim TmpBytes64(63) As Byte

        Array.Copy(LargeBytes, 0, TmpBytes64, 0, 32)
        Array.Copy(LargeBytes, LargeBytes.Length - 32, TmpBytes64, 32, 32)

        TmpBytes64 = SHA512_Worker.ComputeHash(TmpBytes64)

        Array.Copy(TmpBytes64, 0, LargeBytes, 0, 32)
        Array.Copy(TmpBytes64, 32, LargeBytes, LargeBytes.Length - 32, 32)

        Return SHA256_Worker.ComputeHash(TmpBytes64)

    End Function

    Public Sub HashWithBCryptSHA512(ByRef InputData As Byte(), InputLength As UInteger,
                                               ByRef OutputData64 As Byte(), handle_IDX As Integer)

        ' Hash the input data
        BCryptHashData(hHash(handle_IDX), InputData, InputLength, 0) 'CUInt(inputData.Length), 0)

        ' Finalize the hash and retrieve the result
        BCryptFinishHash(hHash(handle_IDX), OutputData64, 64, 0)

        'Return hashResult_512(handle_IDX)

    End Sub

    Public Function HashWithBCryptSHA256(ByRef inputData As Byte(), handle_IDX As Integer) As Byte()

        Dim hashResult_256(31) As Byte

        ' Hash the input data
        BCryptHashData(hHash(handle_IDX), inputData, CUInt(inputData.Length), 0)

        ' Finalize the hash and retrieve the result
        BCryptFinishHash(hHash(handle_IDX), hashResult_256, 32, 0)

        Return hashResult_256

    End Function

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

    Public Sub Dispose() Implements IDisposable.Dispose
        ' 請勿變更此程式碼。請將清除程式碼放入 'Dispose(disposing As Boolean)' 方法
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub

End Class

<System.Security.SecurityCritical>
Public Class Encode_Libs : Implements IDisposable

    Private disposedValue As Boolean


    Public Function Get_Target_Dir(ByRef InByte() As Byte) As String

        Dim SHA512_Worker As New Security.Cryptography.SHA512CryptoServiceProvider
        Dim TmpBytes64(63) As Byte

        TmpBytes64 = SHA512_Worker.ComputeHash(InByte)

        For IDX As Integer = 0 To 63
            TmpBytes64 = SHA512_Worker.ComputeHash(TmpBytes64)
        Next

        Dim TmpBytes32(31) As Byte

        Using hmac As New HMACSHA256(TmpBytes64)
            TmpBytes32 = hmac.ComputeHash(Encoding.UTF8.GetBytes("Path-Derivation-Index"))
        End Using

        Dim x38Str() As Char = {"0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, "A"c, "B"c, "C"c,
        "D"c, "E"c, "F"c, "G"c, "H"c, "I"c, "J"c, "K"c, "L"c, "M"c, "N"c, "O"c, "P"c, "Q"c, "R"c, "S"c, "T"c, "U"c,
        "V"c, "W"c, "X"c, "Y"c, "Z"c, "-"c, "_"c}

        Dim FinalStr As String = ""
        Dim TmpRemainder1 As BigInteger = 0
        Dim Remainder As BigInteger = 0

        For IDX01 As Integer = 0 To 31
            TmpRemainder1 = (TmpRemainder1 * 256) + TmpBytes32(IDX01)
        Next

        Do
            TmpRemainder1 = BigInteger.DivRem(TmpRemainder1, 38, Remainder)
            If CInt(Remainder) < 0 Then Exit Do
            FinalStr = x38Str(Remainder) + FinalStr
        Loop While TmpRemainder1 > 0

        FinalStr = FinalStr.PadLeft(49, "0"c)

        Return FinalStr


    End Function


    Public Function AES_Encrypt_Str_Rtn_Str_with_16rnd(ByRef Input_String As String, ByRef AES_Key() As Byte, ByRef AES_IV() As Byte) As String

        Try

            Dim Orginial_str_Byte() As Byte = Encoding.GetEncoding("UTF-8").GetBytes(Input_String)

            'Phase 1 Get first 16 Bytes Randomization
            Dim work_buffer(Orginial_str_Byte.Length + 15) As Byte
            Using rng As RandomNumberGenerator = RandomNumberGenerator.Create()
                rng.GetBytes(work_buffer, 0, 16)
            End Using
            Array.Copy(Orginial_str_Byte, 0, work_buffer, 16, Orginial_str_Byte.Length)
            WipeBytes(Orginial_str_Byte)

            'Phase 2 Get IV
            Dim SHA512_Worker As New Security.Cryptography.SHA512CryptoServiceProvider
            Dim WorkBytes512b() As Byte = SHA512_Worker.ComputeHash(work_buffer)
            For IDX01 As Integer = 0 To 63 'no need be 254
                WorkBytes512b = SHA512_Worker.ComputeHash(WorkBytes512b)
            Next
            Buffer.BlockCopy(WorkBytes512b, 0, AES_IV, 0, 16)
            SHA512_Worker.Dispose()
            WipeBytes(WorkBytes512b)

            'Phase 3 encryption
            Dim AES_KEY_TmpWorker() As Byte = AES_Key.Clone
            Dim AES As New RijndaelManaged()

            AES.KeySize = 256

            Security.Cryptography.ProtectedMemory.Unprotect(AES_KEY_TmpWorker, MemoryProtectionScope.SameProcess)
            AES.Key = AES_KEY_TmpWorker
            'Destory The TmpWorker
            Security.Cryptography.ProtectedMemory.Protect(AES_KEY_TmpWorker, MemoryProtectionScope.SameProcess)
            WipeBytes(AES_KEY_TmpWorker)

            AES.IV = AES_IV
            AES.Mode = CipherMode.CBC
            AES.Padding = PaddingMode.PKCS7

            Dim stream1 As New MemoryStream()
            Dim stream2 As New CryptoStream(stream1, AES.CreateEncryptor(), CryptoStreamMode.Write)
            stream2.Write(work_buffer, 0, work_buffer.Length)
            stream2.FlushFinalBlock()

            Dim ReturnString As String = ByteIn_StringOut(stream1.ToArray)

            ClearMS(stream1)
            stream2.Clear()
            stream1.Close()
            stream2.Close()
            stream1.Dispose()
            stream2.Dispose()
            AES.Dispose()

            'Phase 4 wipeout
            WipeBytes(work_buffer)


            Return ReturnString

        Catch Exception_Name As Exception

            MsgBox("AES_Encrypt_Byte_Return_String() failure." + D_vbcrlf + Exception_Name.Message, 0, LangStrs(LIdx, UsingTxt.Ti_Err))
            Return ""

        End Try
    End Function

    Public Function AES_Decrypt_Str_Return_Bytes_Cut_16_Head _
        (ByRef Input_string As String, ByRef AES_Key() As Byte, ByRef AES_IV() As Byte) As Byte()

        Try

            Dim AES_KEY_TmpWorker() As Byte = AES_Key.Clone
            Dim AES As New RijndaelManaged()

            AES.KeySize = 256

            Security.Cryptography.ProtectedMemory.Unprotect(AES_KEY_TmpWorker, MemoryProtectionScope.SameProcess)
            AES.Key = AES_KEY_TmpWorker

            'Destory The TmpWorker
            Security.Cryptography.ProtectedMemory.Protect(AES_KEY_TmpWorker, MemoryProtectionScope.SameProcess)
            WipeBytes(AES_KEY_TmpWorker)

            AES.IV = AES_IV
            AES.Mode = CipherMode.CBC
            AES.Padding = PaddingMode.PKCS7

            Dim buffer(0) As Byte
            StringIn_ByteOut(Input_string, buffer)

            Dim stream1 As New MemoryStream()
            Dim stream2 As New CryptoStream(stream1, AES.CreateDecryptor(), CryptoStreamMode.Write)

            stream2.Write(buffer, 0, buffer.Length)
            stream2.FlushFinalBlock()

            AES_Decrypt_Str_Return_Bytes_Cut_16_Head = stream1.GetBuffer().Skip(16).Take(stream1.Length - 16).ToArray()

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

    <System.Diagnostics.DebuggerStepThrough()>
    Public Sub StringIn_ByteOut(ByRef InputString As String, ByRef OutputByte() As Byte)

        Dim TmpVal As Integer = (InputString.Length / 2) - 1
        ReDim OutputByte(TmpVal)
        For IDX01 As Integer = 0 To TmpVal
            OutputByte(IDX01) = CByte(Convert.ToInt32(InputString.Substring(IDX01 * 2, 2), 16))
        Next

    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Public Function ByteIn_StringOut(ByRef InByte() As Byte) As String

        Dim TheStringBuilder As New StringBuilder()
        For Each B As Byte In InByte
            TheStringBuilder.AppendFormat("{0:X2}", B)
        Next

        Return TheStringBuilder.ToString

    End Function


    'The Old way
    'Public Sub Get_The_IV_By_RND(ByRef TheIV() As Byte)
    '    Dim rng As RandomNumberGenerator = RandomNumberGenerator.Create()
    '    rng.GetBytes(TheIV)
    '    rng.Dispose()
    'End Sub

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



Module Am_I_Writing_C

    ' 宣告 VirtualAlloc 函數
    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Function VirtualAlloc(
        lpAddress As IntPtr,
        dwSize As UIntPtr,
        flAllocationType As UInteger,
        flProtect As UInteger) As IntPtr
    End Function

    ' 宣告 VirtualFree 函數
    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Function VirtualFree(
        lpAddress As IntPtr,
        dwSize As UIntPtr,
        dwFreeType As UInteger) As Boolean
    End Function

    ' 定義 VirtualAlloc 和 VirtualFree 的常數
    Public Const MEM_COMMIT As UInteger = &H1000
    Public Const MEM_RESERVE As UInteger = &H2000
    Public Const MEM_RELEASE As UInteger = &H8000
    Public Const PAGE_READWRITE As UInteger = &H4

    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Function VirtualLock(lpAddress As IntPtr, dwSize As UIntPtr) As Boolean
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Function VirtualUnlock(lpAddress As IntPtr, dwSize As UIntPtr) As Boolean
    End Function

End Module

