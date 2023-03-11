'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Public Class MSGBOXXX

    Dim Btn_C As Image = New Bitmap(My.Resources.Resource1.button_confirm)
    Dim Btn_C_Di As Image = New Bitmap(My.Resources.Resource1.button_confirm_dis)

    Private Sub TextBoxDELETE_TextChanged(sender As Object, e As EventArgs) Handles TextBoxDELETE.TextChanged

        If TextBoxDELETE.Text = "DELETE" Then
            ButtonYes.Enabled = True
            ButtonYes.Image = Btn_C
        Else
            ButtonYes.Enabled = False
            ButtonYes.Image = Btn_C_Di
        End If

    End Sub

    Private Sub ButtonYes_Click(sender As Object, e As EventArgs) Handles ButtonYes.Click
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub ButtonCri_Click(sender As Object, e As EventArgs) Handles ButtonCri.Click
        Me.DialogResult = DialogResult.OK
    End Sub

    Private lastLocation As Point
    Private isMouseDown As Boolean = False

    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBox1.MouseDown
        ' 紀錄滑鼠按下的位置
        isMouseDown = True
        lastLocation = e.Location
    End Sub

    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBox1.MouseMove
        ' 當滑鼠左鍵按下時，設定窗體的位置
        If isMouseDown Then
            Me.Location = New Point(Me.Location.X + (e.X - lastLocation.X), Me.Location.Y + (e.Y - lastLocation.Y))
        End If
    End Sub

    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBox1.MouseUp
        ' 當滑鼠左鍵釋放時，重設 isMouseDown 變數
        isMouseDown = False
    End Sub

End Class