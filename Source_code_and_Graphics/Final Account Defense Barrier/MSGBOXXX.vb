'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier



Public Class MSGBOXXX

    Dim B_CONF_DI As Bitmap = Make_Button_Gray(My.Resources.Resource1.button_confirm)

    Private Sub TextBoxDELETE_TextChanged(sender As Object, e As EventArgs) Handles TextBoxDELETE.TextChanged

        If TextBoxDELETE.Text = "DELETE" Then
            ButtonOK.Enabled = True
            ButtonOK.Image = My.Resources.Resource1.button_confirm
        Else
            ButtonOK.Enabled = False
            ButtonOK.Image = B_CONF_DI
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

    Private Sub ButtonNo_Click(sender As Object, e As EventArgs) Handles ButtonNo.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Dim B_yes_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_yes)
    Dim B_no_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_no)
    Dim B_confirm_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_confirm)
    Dim B_Cancel_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_Cancel)

    Private Sub Mouse_Enter(sender As Object, e As EventArgs) Handles _
        ButtonOK.MouseEnter, ButtonCancel.MouseEnter, ButtonYes.MouseEnter,
        ButtonNo.MouseEnter

        Select Case sender.Name
            Case "ButtonOK"
                If ButtonOK.Enabled Then
                    ButtonOK.Image = B_confirm_on
                End If
            Case "ButtonCancel"
                ButtonCancel.Image = B_Cancel_on
            Case "ButtonYes"
                ButtonYes.Image = B_yes_on
            Case "ButtonNo"
                ButtonNo.Image = B_no_on
        End Select

    End Sub

    Dim B_confirm As New Bitmap(My.Resources.Resource1.button_confirm)
    Dim B_Cancel As New Bitmap(My.Resources.Resource1.button_Cancel)
    Dim B_yes As New Bitmap(My.Resources.Resource1.button_yes)
    Dim B_no As New Bitmap(My.Resources.Resource1.button_no)

    Private Sub Mouse_Leave(sender As Object, e As EventArgs) Handles _
        ButtonOK.MouseLeave, ButtonCancel.MouseLeave, ButtonYes.MouseLeave,
        ButtonNo.MouseLeave

        Select Case sender.Name
            Case "ButtonOK"
                If ButtonOK.Enabled Then
                    ButtonOK.Image = B_confirm
                End If
            Case "ButtonCancel"
                ButtonCancel.Image = B_Cancel
            Case "ButtonYes"
                ButtonYes.Image = B_yes
            Case "ButtonNo"
                ButtonNo.Image = B_no
        End Select
    End Sub
End Class

