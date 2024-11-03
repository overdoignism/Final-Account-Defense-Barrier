'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Imports System.Security.Cryptography

<System.Security.SecurityCritical>
Public Class SmallDecoderPass
    Public InputByte() As Byte
    Public Workmode As Integer '1=Copy to clipboard
    Public CopySucess As Boolean
    Private FillTrash() As String
    Public TextBoxPWDStr As New TextBox
    Public PictureBoxPwd As New Bitmap(505, 22) 'PictureBox

    Private Sub SmallDecoder_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TextBoxPWDStr.Enabled = False
        TextBoxPWDStr.Visible = False
        TextBoxPWDStr.UseSystemPasswordChar = True


        Me.Width = 1
        Me.Height = 1

        ClearTextBox(TextBoxPWDStr)
        TextBoxPWDStr.Text = System.Text.Encoding.UTF8.GetString(Security.Cryptography.ProtectedData.Unprotect(InputByte, Nothing, DataProtectionScope.CurrentUser))

        Select Case Workmode
            Case 0
            Case 1
                If TextBoxPWDStr.Text <> "" Then

                    Dim CPWorker As New ClipboardHelper2
                    CopySucess = CPWorker.SetClipboardText(TextBoxPWDStr.Text)

                End If
            Case 2
                GenPic(TextBoxPWDStr)
        End Select

        For idx01 As Integer = 0 To 8192
            ReDim Preserve FillTrash(idx01)
            FillTrash(idx01) = New String("x", 32)
        Next

        Me.DialogResult = DialogResult.OK

    End Sub

    Private Sub SmallDecoder_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        ClearTextBox(TextBoxPWDStr)
        GC.SuppressFinalize(Me)
    End Sub

    Private Sub GenPic(ByRef TheTextbox As TextBox)

        Dim Pic_H As Integer = PictureBoxPwd.Height
        Dim Pic_W As Integer = PictureBoxPwd.Width

        Dim RndFnt() As Font = {New System.Drawing.Font("Courier New", 12, FontStyle.Bold)}

        Dim BitA As New Bitmap(Pic_W, Pic_H)
        Dim GrA As Graphics = Graphics.FromImage(BitA)
        Dim Brs1 As Brush = New System.Drawing.SolidBrush(Color.Black)

        Dim LeftMove As Integer = 5
        GrA.Clear(Color.White)

        GrA.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias

        For Each TmpChar As Char In TheTextbox.Text
            Dim textSize As SizeF = GrA.MeasureString(TmpChar.ToString(), RndFnt(0), Nothing, Nothing, 1, 1)
            GrA.DrawString(TmpChar, RndFnt(0), Brs1, LeftMove, (Pic_H / 2) - 7)
            LeftMove += (textSize.Width * 0.7)
        Next

        PictureBoxPwd = DirectCast(BitA.Clone(), Image)

    End Sub


End Class
