'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Imports System.Security.Cryptography

Public Class SmallDecoderPass
    Public InputByte() As Byte
    Public Workmode As Integer '1=Copy to clipboard

    Private FillTrash() As String

    Private Sub SmallDecoder_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Width = 1
        Me.Height = 1

        ClearTextBox(TextBoxPWDStr)
        TextBoxPWDStr.Text = System.Text.Encoding.UTF8.GetString(Security.Cryptography.ProtectedData.Unprotect(InputByte, Nothing, DataProtectionScope.CurrentUser))

        If Workmode = 1 Then
            If TextBoxPWDStr.Text <> "" Then
                My.Computer.Clipboard.SetText(TextBoxPWDStr.Text)
            End If
        End If

        For idx01 As Integer = 0 To 8192
            ReDim Preserve FillTrash(idx01)
            FillTrash(idx01) = New String("x", 32)
        Next

        Me.DialogResult = DialogResult.OK

    End Sub

    Private Sub SmallDecoder_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        ClearTextBox(TextBoxPWDStr)
    End Sub

End Class