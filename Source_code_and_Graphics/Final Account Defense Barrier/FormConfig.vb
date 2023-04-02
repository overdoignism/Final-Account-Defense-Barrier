'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Public Class FormConfig

    Public FormL, FormT As Integer
    Public OtherWorkMode As Integer
    Public ReturnCSV As String

    Private Sub FormConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.StartPosition = FormStartPosition.Manual
        Me.Left = FormL
        Me.Top = FormT
        Me.Text = TextStrs(60)
        ReturnCSV = ""

    End Sub

    '========= Form controls work

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

    Private Sub ButtonCSVEx_Click(sender As Object, e As EventArgs) Handles ButtonCSVEx.Click

        If FormMain.ListBox1.Items.Count = 1 Then
            MSGBOXNEW(TextStrs(97), MsgBoxStyle.OkOnly, TextStrs(9), Me, PictureGray)
            Exit Sub
        End If

        If MSGBOXNEW(TextStrs(95), MsgBoxStyle.OkCancel, TextStrs(9), Me, PictureGray) = DialogResult.Cancel Then Exit Sub
        Me.DialogResult = DialogResult.Retry

    End Sub

    Private Sub ButtonCSVIM_Click(sender As Object, e As EventArgs) Handles ButtonCSVIM.Click

        If MSGBOXNEW(TextStrs(90), MsgBoxStyle.OkCancel, TextStrs(9), Me, PictureGray) = DialogResult.Cancel Then Exit Sub

        Dim FFE As New FormFileExplorer

        MakeWindowsBlur(Me, PictureGray)
        FFE.Opacity = Me.Opacity

        If FFE.ShowDialog(Me) = DialogResult.OK Then
            FFE.Close()
            UnMakeWindowsBlur(PictureGray)

            Dim ms As New IO.MemoryStream(FFE.BigByte) ' 假設 byte array 存在於一個名為 byteArray 的變數中
            Dim sr As New IO.StreamReader(ms, True)    ' 使用 StreamReader 讀取 byte array 內容並判斷字元編碼
            ReturnCSV = sr.ReadToEnd() ' 將 StreamReader 讀取的內容轉換成 string
            Me.DialogResult = DialogResult.Abort

        Else

        End If
        FFE.Dispose()
        UnMakeWindowsBlur(PictureGray)

    End Sub

    '============================= Window base operate

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

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    '================================ Button visual work ================

    Dim B_confirm_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_confirm)
    Dim B_Cancel_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_Cancel)

    Dim B_CAT_DEL_on As New Bitmap(My.Resources.Resource1.button_CAT_DEL_on)
    Dim B_CAT_TRA_on As New Bitmap(My.Resources.Resource1.button_CAT_TRANS_on)
    Dim B_CAT_CSVIM_on As New Bitmap(My.Resources.Resource1.button_CAT_CSVIM_on)
    Dim B_CAT_CSVEX_on As New Bitmap(My.Resources.Resource1.button_CAT_CSVEX_on)

    Private Sub Mouse_Enter(sender As Object, e As EventArgs) Handles ButtonOK.MouseEnter,
        ButtonCancel.MouseEnter, ButtonDelCat.MouseEnter, ButtonTransFullCat.MouseEnter,
        ButtonCSVIM.MouseEnter, ButtonCSVEx.MouseEnter

        Select Case sender.Name
            Case "ButtonOK"
                ButtonOK.Image = B_confirm_on
            Case "ButtonCancel"
                ButtonCancel.Image = B_Cancel_on
            Case "ButtonDelCat"
                ButtonDelCat.Image = B_CAT_DEL_on
            Case "ButtonTransFullCat"
                ButtonTransFullCat.Image = B_CAT_TRA_on
            Case "ButtonCSVIM"
                ButtonCSVIM.Image = B_CAT_CSVIM_on
            Case "ButtonCSVEx"
                ButtonCSVEx.Image = B_CAT_CSVEX_on
        End Select

    End Sub

    Dim B_confirm As New Bitmap(My.Resources.Resource1.button_confirm)
    Dim B_Cancel As New Bitmap(My.Resources.Resource1.button_Cancel)
    Dim B_CAT_DEL As New Bitmap(My.Resources.Resource1.button_CAT_DEL)
    Dim B_CAT_TRA As New Bitmap(My.Resources.Resource1.button_CAT_TRANS)
    Dim B_CAT_CSVIM As New Bitmap(My.Resources.Resource1.button_CAT_CSVIM)
    Dim B_CAT_CSVEX As New Bitmap(My.Resources.Resource1.button_CAT_CSVEX)

    Private Sub Mouse_Leave(sender As Object, e As EventArgs) Handles ButtonOK.MouseLeave,
        ButtonCancel.MouseLeave, ButtonDelCat.MouseLeave, ButtonTransFullCat.MouseLeave,
        ButtonCSVIM.MouseLeave, ButtonCSVEx.MouseLeave

        Select Case sender.Name
            Case "ButtonOK"
                ButtonOK.Image = B_confirm
            Case "ButtonCancel"
                ButtonCancel.Image = B_Cancel
            Case "ButtonDelCat"
                ButtonDelCat.Image = B_CAT_DEL
            Case "ButtonTransFullCat"
                ButtonTransFullCat.Image = B_CAT_TRA
            Case "ButtonCSVIM"
                ButtonCSVIM.Image = B_CAT_CSVIM
            Case "ButtonCSVEx"
                ButtonCSVEx.Image = B_CAT_CSVEX
        End Select
    End Sub

End Class