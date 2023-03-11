'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Public Class FormConfig

    Public FormL, FormT As Integer
    Public OtherWorkMode As Integer

    Private Sub ButtonTransCatalog_Click(sender As Object, e As EventArgs) Handles ButtonTransFullCat.Click
        If MSGBOXNEW(TextStrs(14).Replace("$$$", TextBoxCatalog.Text) + vbCrLf + vbCrLf + TextStrs(12),
          MsgBoxStyle.OkCancel, TextStrs(9), Me, PictureGray) = MsgBoxResult.Cancel Then
            Exit Sub
        Else
            OtherWorkMode = 1
            Me.DialogResult = DialogResult.Ignore
        End If
    End Sub

    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelCat.Click
        If MSGBOXNEW(TextStrs(10).Replace("$$$", TextBoxCatalog.Text),
                     MsgBoxStyle.OkCancel, TextStrs(9), Me, PictureGray) = MsgBoxResult.Cancel Then Exit Sub

        OtherWorkMode = 2
        Me.DialogResult = DialogResult.Ignore

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click

        If MSGBOXNEW(TextStrs(52).Replace("$$$", TextBoxCatalog.Text), MsgBoxStyle.OkCancel,
                     TextStrs(9), FormMain, PictureGray) = MsgBoxResult.Ok Then

            CAT_setting_Str(0) = ComboBoxTimer.SelectedIndex.ToString
            CAT_setting_Str(2) = TB_AC_KEY.SelectedIndex.ToString
            CAT_setting_Str(3) = CB_SIM1.SelectedIndex.ToString
            CAT_setting_Str(5) = TB_PW_KEY.SelectedIndex.ToString
            CAT_setting_Str(6) = CB_SIM2.SelectedIndex.ToString
            Me.DialogResult = DialogResult.OK

        End If

    End Sub

    Private Sub FormConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.StartPosition = FormStartPosition.Manual
        Me.Left = FormL
        Me.Top = FormT
        Me.Text = TextStrs(60)
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