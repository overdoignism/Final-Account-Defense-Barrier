'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Imports System.Security.Cryptography


<System.Security.SecurityCritical>
Public Class SmallDecoderAES

    Public NeedToDecryptoStr As String
    Public AES_KEY_Use() As Byte
    Public CurrentAccountPass() As Byte

    Private FillTrash() As String

    Private Sub SmallDecodeAES_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If GetCAPass() Then
            Me.DialogResult = DialogResult.OK
        Else
            Me.DialogResult = DialogResult.No
        End If

    End Sub

    Private Function GetCAPass() As Boolean

        Dim TheEncLib As New Encode_Libs
        Dim NoErrFlag As Boolean = True

        Dim WorkingStr() As String = NeedToDecryptoStr.Split(",")

        If (AES_KEY_Use Is Nothing) Or (WorkingStr.Length <> 2) Then
            NoErrFlag = False
        End If

        If NoErrFlag Then

            Try

                Dim AES_IV_PWD_Current(0) As Byte
                Dim WorkingByte() As Byte

                TheEncLib.StringIn_ByteOut(WorkingStr(1), AES_IV_PWD_Current)
                WorkingByte = TheEncLib.AES_Decrypt_Str_Return_Bytes_Cut_16_Head(WorkingStr(0), AES_KEY_Use, AES_IV_PWD_Current)

                Dim TmpBytes() As Byte = Nothing
                GetEss(WorkingByte, TmpBytes)

                CurrentAccountPass = Security.Cryptography.ProtectedData.Protect(TmpBytes, Nothing, DataProtectionScope.CurrentUser)

                WipeBytes(AES_IV_PWD_Current)
                WipeBytes(WorkingByte)
                WipeBytes(TmpBytes)

            Catch ex As Exception
                NoErrFlag = False
            End Try

        End If

        TheEncLib.Dispose()

        For idx01 As Integer = 0 To 8192
            ReDim Preserve FillTrash(idx01)
            FillTrash(idx01) = New String("x", 32)
        Next

        Return NoErrFlag

    End Function

    Private Sub GetEss(ByRef originalBytes() As Byte, ByRef theEssBytes() As Byte)

        Dim StartIDX As Integer
        Dim EndIDX As Integer

        Dim IDX02 As Integer = 0

        For IDX01 As Integer = 0 To originalBytes.Length - 1

            If originalBytes(IDX01) = 13 Then
                If IDX02 = 0 Then
                    StartIDX = IDX01 + 1
                    IDX02 = 1
                ElseIf IDX02 = 1 Then
                    EndIDX = IDX01
                    IDX02 = 2
                End If
            End If

        Next

        ReDim theEssBytes(EndIDX - StartIDX - 1)
        Buffer.BlockCopy(originalBytes, StartIDX, theEssBytes, 0, theEssBytes.Length)

    End Sub

    Private Sub SmallDecoderAES_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        GC.SuppressFinalize(Me)
    End Sub
End Class


Module SmallDecoderAES_Module

    Public AES_KEY_Protected() As Byte
    Public DERIVED_IDT_Protected() As Byte
    Public Read_File_Pass_Str As String
    Public CurrentAccountPass() As Byte

    Public NowPassStatue As Integer = 0
    '0=New Account no edit, CurrentAccountPass = "" in init
    '1=Old File Read, not decrypt
    '2=Old File Read, decrypted, CurrentAccountPass usable, Not edit
    '3=Edited

    Public Sub GetPass()

        Select Case NowPassStatue
            Case 0
            Case 1
                Dim SDAES As New SmallDecoderAES
                SDAES.AES_KEY_Use = AES_KEY_Protected
                SDAES.NeedToDecryptoStr = Read_File_Pass_Str
                If SDAES.ShowDialog() = DialogResult.OK Then
                    CurrentAccountPass = SDAES.CurrentAccountPass
                    NowPassStatue = 2
                End If
                SDAES.Dispose()
                FullGC()
            Case 2
            Case 3

        End Select

    End Sub
End Module
