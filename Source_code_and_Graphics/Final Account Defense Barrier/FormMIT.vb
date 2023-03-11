'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Public Class FormMIT
    Public FormL, FormT, FormW, FormH As Integer
    Private lastLocation As Point
    Private isMouseDown As Boolean = False

    Private Sub ButtonOK_Click_1(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub FormMIT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.StartPosition = FormStartPosition.Manual
        Me.Left = FormL + (FormW - Me.Width) / 2
        Me.Top = FormT + (FormH - Me.Height) / 2

        Me.TextBox1.SelectionStart = Me.TextBox1.Text.Length

    End Sub

    Private Sub PictureMIT_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureMIT.MouseDown
        ' 紀錄滑鼠按下的位置
        isMouseDown = True
        lastLocation = e.Location
    End Sub

    Private Sub PictureMIT_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureMIT.MouseMove
        ' 當滑鼠左鍵按下時，設定窗體的位置
        If isMouseDown Then
            Me.Location = New Point(Me.Location.X + (e.X - lastLocation.X), Me.Location.Y + (e.Y - lastLocation.Y))
        End If
    End Sub

    Private Sub PictureMIT_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureMIT.MouseUp
        ' 當滑鼠左鍵釋放時，重設 isMouseDown 變數
        isMouseDown = False
    End Sub

End Class