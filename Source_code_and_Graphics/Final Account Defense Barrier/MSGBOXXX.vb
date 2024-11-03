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

    Private Sub PictureBackGround_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBackGround.MouseDown
        ' 紀錄滑鼠按下的位置
        isMouseDown = True
        lastLocation = e.Location
    End Sub

    Private Sub PictureBackGround_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBackGround.MouseMove
        ' 當滑鼠左鍵按下時，設定窗體的位置
        If isMouseDown Then
            Me.Location = New Point(Me.Location.X + (e.X - lastLocation.X), Me.Location.Y + (e.Y - lastLocation.Y))
        End If
    End Sub

    Private Sub PictureBackGround_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBackGround.MouseUp
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

Module MSGBOXXX_Work

    Dim MSGBX_IMG_OK As New Bitmap(Image.FromStream(New IO.MemoryStream(My.Resources.Resource1.Message_OK_PNG)))
    Dim MSGBX_IMG_VRF As New Bitmap(Image.FromStream(New IO.MemoryStream(My.Resources.Resource1.Message_VRF_PNG)))
    Dim MSGBX_IMG_CRI As New Bitmap(Image.FromStream(New IO.MemoryStream(My.Resources.Resource1.Message_CRI_PNG)))

    Dim MSGBX_IMG_OK_TXTB As New Bitmap(Image.FromStream(New IO.MemoryStream(My.Resources.Resource1.Message_OK_TXTB_PNG)))
    Dim MSGBX_IMG_VRF_TXTB As New Bitmap(Image.FromStream(New IO.MemoryStream(My.Resources.Resource1.Message_VRF_TXTB_PNG)))
    Dim MSGBX_IMG_CRI_TXTB As New Bitmap(Image.FromStream(New IO.MemoryStream(My.Resources.Resource1.Message_CRI_TXTB_PNG)))

    Public Function MSGBOXNEW(MessageStr As String, BoxType As MsgBoxStyle, BoxTitle As String,
                              ByRef FirerForm As Form, ByRef FFPicBox As PictureBox) As DialogResult

        Dim NeoMSGBOX As New MSGBOXXX
        Dim ReturnDR As DialogResult
        NeoMSGBOX.Opacity = Sys_Chk._OpacitySng

        Select Case BoxType
            Case MsgBoxStyle.OkOnly '確認
                NeoMSGBOX.ButtonOK.Visible = True
                NeoMSGBOX.PictureBackGround.Image = MSGBX_IMG_OK
                NeoMSGBOX.Label_Msg_Work.Image = ResizeBitmap(MSGBX_IMG_OK_TXTB, 2, 2)
            Case MsgBoxStyle.OkCancel, MsgBoxStyle.YesNo '是/否
                NeoMSGBOX.ButtonYes.Visible = True
                NeoMSGBOX.ButtonNo.Visible = True
                NeoMSGBOX.PictureBackGround.Image = MSGBX_IMG_VRF
                NeoMSGBOX.Label_Msg_Work.Image = ResizeBitmap(MSGBX_IMG_VRF_TXTB, 2, 2)
            Case MsgBoxStyle.Exclamation '確認
                NeoMSGBOX.ButtonOK.Visible = True
                NeoMSGBOX.PictureBackGround.Image = MSGBX_IMG_VRF
                NeoMSGBOX.Label_Msg_Work.Image = ResizeBitmap(MSGBX_IMG_VRF_TXTB, 2, 2)
            Case MsgBoxStyle.Critical '確認
                NeoMSGBOX.ButtonOK.Visible = True
                NeoMSGBOX.PictureBackGround.Image = MSGBX_IMG_CRI
                NeoMSGBOX.Label_Msg_Work.Image = ResizeBitmap(MSGBX_IMG_CRI_TXTB, 2, 2)
            Case 65535 'DELETE
                NeoMSGBOX.ButtonOK.Visible = True
                NeoMSGBOX.ButtonOK.Enabled = False
                NeoMSGBOX.ButtonOK.Image = Make_Button_Gray(My.Resources.Resource1.button_confirm)
                NeoMSGBOX.ButtonOK.Location = New Point(142, NeoMSGBOX.ButtonOK.Location.Y)
                NeoMSGBOX.ButtonCancel.Visible = True
                NeoMSGBOX.TextBoxDELETE.Visible = True
                NeoMSGBOX.PictureBackGround.Image = MSGBX_IMG_VRF
                NeoMSGBOX.Label_Msg_Work.Image = ResizeBitmap(MSGBX_IMG_VRF_TXTB, 2, 2)
        End Select

        NeoMSGBOX.Label_Msg_Work.Text = MessageStr
        Dim NeoMsgLabWork As New Bitmap(NeoMSGBOX.Label_Msg_Work.Width, NeoMSGBOX.Label_Msg_Work.Height)
        Dim NewRec As Rectangle
        NewRec.Width = NeoMSGBOX.Label_Msg_Work.Width
        NewRec.Height = NeoMSGBOX.Label_Msg_Work.Height
        NeoMSGBOX.Label_Msg_Work.DrawToBitmap(NeoMsgLabWork, NewRec)
        NeoMSGBOX.Label_Msg_Show.Image = ResizeBitmap(NeoMsgLabWork, 0.5, 0.5)

        NeoMSGBOX.Label_Title_Work.Text = BoxTitle
        Dim NeoMsgTitleWork As New Bitmap(NeoMSGBOX.Label_Title_Work.Width, NeoMSGBOX.Label_Title_Work.Height)
        Dim NewRec2 As Rectangle
        NewRec2.Width = NeoMSGBOX.Label_Title_Work.Width
        NewRec2.Height = NeoMSGBOX.Label_Title_Work.Height
        NeoMSGBOX.Label_Title_Work.DrawToBitmap(NeoMsgTitleWork, NewRec)
        NeoMSGBOX.Label_Title_Show.Image = ResizeBitmap(NeoMsgTitleWork, 0.5, 0.5)

        MakeWindowsMono(FirerForm, FFPicBox)
        ReturnDR = NeoMSGBOX.ShowDialog(FirerForm)
        UnMakeWindowsMono(FFPicBox)

        NeoMSGBOX.Close()
        NeoMSGBOX.Dispose()
        NeoMSGBOX = Nothing

        Return ReturnDR

    End Function

End Module