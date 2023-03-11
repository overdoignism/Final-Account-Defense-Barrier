'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Imports System.Drawing.Text

Public Class SmallPWDShow

    Public InputByte() As Byte
    Private Sub FormPWDShow_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim SD As New SmallDecoderPass
        SD.InputByte = InputByte
        SD.ShowDialog()

        GenPic(SD.TextBoxPWDStr)

        SD.Dispose()
        'FullGC()
        Me.DialogResult = DialogResult.OK

    End Sub

    Public Sub GenPic(ByRef TheTextbox As TextBox)

        Dim Pic_H As Integer = PictureBoxPwd.Height
        Dim Pic_W As Integer = PictureBoxPwd.Width

        Dim RNDer As New Random

        Dim RndFnt() As Font = {New System.Drawing.Font("Courier New", 12, FontStyle.Bold)}

        Dim BitA As Bitmap = New Bitmap(Pic_W, Pic_H)
        Dim GrA As Graphics = Graphics.FromImage(BitA)
        Dim Brs1 As Brush = New System.Drawing.SolidBrush(Color.Black)

        Dim LeftMove As Integer = 5
        GrA.Clear(Color.White)

        GrA.TextRenderingHint = TextRenderingHint.AntiAlias

        For Each TmpChar As Char In TheTextbox.Text
            Dim textSize As SizeF = GrA.MeasureString(TmpChar.ToString(), RndFnt(0), Nothing, Nothing, 1, 1)
            GrA.DrawString(TmpChar, RndFnt(0), Brs1, LeftMove, (Pic_H / 2) - 7)
            LeftMove += (textSize.Width * 0.7)
        Next

        Dim myImage As Image = DirectCast(BitA.Clone(), Image)
        PictureBoxPwd.Image = myImage

    End Sub

End Class