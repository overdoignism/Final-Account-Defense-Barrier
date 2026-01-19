'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Imports System.Runtime.InteropServices

Public Class FormConfig

    Public FormL, FormT As Integer
    Public OtherWorkMode As Integer
    Public ReturnCSV As String
    Public ReadOnlyMode As Boolean

    Private Sub FormConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.StartPosition = FormStartPosition.Manual
        Me.Left = FormL
        Me.Top = FormT
        ReturnCSV = ""

        If Not Sys_Chk.Screen_Capture_Allowed Then 'Disable Screen Capture
            SetWindowDisplayAffinity(Me.Handle, WDA_EXCLUDEFROMCAPTURE)
        End If

        If ReadOnlyMode Then
            ButtonOK.Image = B_confirm_dis
            ButtonOK.Enabled = False
            ButtonTransFullCat.Image = B_CAT_TRA_dis
            ButtonTransFullCat.Enabled = False
            ButtonDelCat.Image = B_CAT_DEL_dis
            ButtonDelCat.Enabled = False
            ButtonCSVEx.Image = B_CAT_CSVEX_dis
            ButtonCSVEx.Enabled = False
            ButtonCSVIM.Image = B_CAT_CSVIM_dis
            ButtonCSVIM.Enabled = False
            PIC_READONLY.Image = New Bitmap(My.Resources.Resource1.Read_Only_Img)
            PIC_READONLY.Visible = True
        End If

        ReDrawTimer.Enabled = True

    End Sub

    '========= ComoboBox Visual

    Private Sub TB_AC_KEY_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TB_AC_KEY.SelectedIndexChanged

    End Sub


    Private Sub TB_AC_KEY_Paint(sender As Object, e As PaintEventArgs) Handles TB_AC_KEY.Paint
        MsgBox("!")
    End Sub

    Dim CB_BackColor As Color = Color.FromArgb(42, 22, 22)
    Dim CB_FontColor As Color = Color.FromArgb(126, 237, 176)
    Dim CB_Font As New Font("Arial", 11.75F, FontStyle.Bold, 3)

    Private Sub ComboBox_DrawItem(ByVal sender As Object, ByVal e As DrawItemEventArgs) Handles _
        TB_AC_KEY.DrawItem, TB_PW_KEY.DrawItem, CB_SIM1.DrawItem, CB_SIM2.DrawItem, CB_Timer.DrawItem

        ' 確保事件處理項目正確
        If e.Index < 0 Then Return

        ' 取得 ComboBox
        Dim comboBox As ComboBox = CType(sender, ComboBox)
        Dim textSize As Size = TextRenderer.MeasureText(comboBox.Items(e.Index).ToString(), CB_Font)
        Dim x As Integer = e.Bounds.X '+ (e.Bounds.Width - textSize.Width) / 2 '+ offsetX
        Dim y As Integer = e.Bounds.Y + (e.Bounds.Height - textSize.Height) / 2

        ' 是否為選取狀態
        Dim isSelected As Boolean = (e.State And DrawItemState.Selected) = DrawItemState.Selected
        e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        ' 自訂選取和非選取項目的背景色
        If isSelected Then
            e.Graphics.FillRectangle(New SolidBrush(CB_FontColor), e.Bounds) ' 選取色
            e.Graphics.DrawString(comboBox.Items(e.Index).ToString(), CB_Font, New SolidBrush(CB_BackColor), New Point(x, y)) '文字色
        Else
            e.Graphics.FillRectangle(New SolidBrush(CB_BackColor), e.Bounds) ' 背景色
            e.Graphics.DrawString(comboBox.Items(e.Index).ToString(), CB_Font, New SolidBrush(CB_FontColor), New Point(x, y)) '文字色
        End If

        ' 繪製邊界(虛線框)
        'e.DrawFocusRectangle()
        RedrawComboBox(sender)


    End Sub

    Private Sub ReDrawTimer_Tick(sender As Object, e As EventArgs) Handles ReDrawTimer.Tick
        ReDrawAllCB()
        ReDrawTimer.Enabled = False
    End Sub

    Private Sub ReDrawAllCB()
        RedrawComboBox(TB_AC_KEY)
        RedrawComboBox(CB_SIM1)
        RedrawComboBox(TB_PW_KEY)
        RedrawComboBox(CB_SIM2)
        RedrawComboBox(CB_Timer)
    End Sub

    <DllImport("user32.dll")>
    Private Shared Function GetWindowDC(ByVal hWnd As IntPtr) As IntPtr
    End Function
    <DllImport("user32.dll")>
    Private Shared Function ReleaseDC(ByVal hWnd As IntPtr, ByVal hDC As IntPtr) As Integer
    End Function

    Dim CBOX_AR As New Bitmap(My.Resources.Resource1.CBOX_AR)

    Private Sub RedrawComboBox(whatBoxToRedraw As ComboBox)

        Dim BColor As Color = Color.FromArgb(126, 237, 176)

        Dim hdc As IntPtr = GetWindowDC(whatBoxToRedraw.Handle)
        Using g As Graphics = Graphics.FromHdc(hdc)
            ' 在這裡繪製任何你想要的東西，比如藍色邊框
            Dim rect As New Rectangle(1, 1, whatBoxToRedraw.Width - 2, whatBoxToRedraw.Height - 2)
            g.DrawRectangle(New Pen(BColor, 2), rect)
            g.DrawImage(CBOX_AR, whatBoxToRedraw.Width - 18, 0)
        End Using
        ReleaseDC(whatBoxToRedraw.Handle, hdc)

    End Sub

    '========= Form controls work

    Private Sub ButtonTransCatalog_Click(sender As Object, e As EventArgs) Handles ButtonTransFullCat.Click

        If FormMain.ListBoxAccounts.Items.Count = 1 Then
            MSGBOXNEW(LangStrs(LIdx, UsingTxt.Ca_NoA), MsgBoxStyle.OkOnly, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray)
            ReDrawAllCB()
            Exit Sub
        End If

        If MSGBOXNEW(LangStrs(LIdx, UsingTxt.Ca_IwT).Replace("$$$", TextBoxCatalog.Text) + D_vbcrlf + LangStrs(LIdx, UsingTxt.Ca_NPrq),
          MsgBoxStyle.OkCancel, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray) = MsgBoxResult.Cancel Then
            ReDrawAllCB()
            Exit Sub
        Else
            OtherWorkMode = 1
            Me.DialogResult = DialogResult.Ignore
        End If

    End Sub

    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelCat.Click

        If MSGBOXNEW(LangStrs(LIdx, UsingTxt.Ca_DuD).Replace("$$$", TextBoxCatalog.Text),
                     MsgBoxStyle.OkCancel, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray) = MsgBoxResult.Cancel Then
            ReDrawAllCB()
            Exit Sub
        End If

        OtherWorkMode = 2
        Me.DialogResult = DialogResult.Ignore

    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click

        If MSGBOXNEW(LangStrs(LIdx, UsingTxt.Ca_DuUpd).Replace("$$$", TextBoxCatalog.Text), MsgBoxStyle.OkCancel,
                     LangStrs(LIdx, UsingTxt.Ti_Cnfm), FormMain, PictureGray) = MsgBoxResult.Ok Then

            CAT_setting_Str(0) = CB_Timer.SelectedIndex.ToString
            CAT_setting_Str(2) = TB_AC_KEY.SelectedIndex.ToString
            CAT_setting_Str(3) = CB_SIM1.SelectedIndex.ToString
            CAT_setting_Str(5) = TB_PW_KEY.SelectedIndex.ToString
            CAT_setting_Str(6) = CB_SIM2.SelectedIndex.ToString
            Me.DialogResult = DialogResult.OK

        End If

    End Sub

    Private Sub ButtonCSVEx_Click(sender As Object, e As EventArgs) Handles ButtonCSVEx.Click

        If FormMain.ListBoxAccounts.Items.Count = 1 Then
            MSGBOXNEW(LangStrs(LIdx, UsingTxt.CS_NcA), MsgBoxStyle.OkOnly, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray)
            ReDrawAllCB()
            Exit Sub
        End If

        If MSGBOXNEW(LangStrs(LIdx, UsingTxt.CS_Ncc), MsgBoxStyle.OkCancel, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray) = DialogResult.Cancel Then
            ReDrawAllCB()
            Exit Sub
        End If

        Me.DialogResult = DialogResult.Retry

    End Sub

    Private Sub ButtonCSVIM_Click(sender As Object, e As EventArgs) Handles ButtonCSVIM.Click

        If MSGBOXNEW(LangStrs(LIdx, UsingTxt.CS_AuIC), MsgBoxStyle.OkCancel, LangStrs(LIdx, UsingTxt.Ti_Cnfm), Me, PictureGray) = DialogResult.Cancel Then
            ReDrawAllCB()
            Exit Sub
        End If
        ReDrawAllCB()

        Dim FFE As New FormFileExplorer
        FFE.PicFileExp.Image = FormFileExpBitmap
        MakeWindowsMono(Me, PictureGray)
        FFE.Opacity = Me.Opacity

        If FFE.ShowDialog(Me) = DialogResult.OK Then
            FFE.Close()
            UnMakeWindowsMono(PictureGray)
            Dim ms As New IO.MemoryStream(FFE.BigByte) ' 假設 byte array 存在於一個名為 byteArray 的變數中
            Dim sr As New IO.StreamReader(ms, True)    ' 使用 StreamReader 讀取 byte array 內容並判斷字元編碼
            ReturnCSV = sr.ReadToEnd() ' 將 StreamReader 讀取的內容轉換成 string
            Me.DialogResult = DialogResult.Abort
        Else
        End If

        FFE.Dispose()

        UnMakeWindowsMono(PictureGray)
        ReDrawAllCB()

    End Sub

    '============================= Window base operate

    Private lastLocation As Point
    Private isMouseDown As Boolean = False

    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBoxConfig.MouseDown
        ' 紀錄滑鼠按下的位置
        isMouseDown = True
        lastLocation = e.Location
    End Sub

    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBoxConfig.MouseMove
        ' 當滑鼠左鍵按下時，設定窗體的位置
        If isMouseDown Then
            Me.Location = New Point(Me.Location.X + (e.X - lastLocation.X), Me.Location.Y + (e.Y - lastLocation.Y))
        End If
    End Sub

    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBoxConfig.MouseUp
        ' 當滑鼠左鍵釋放時，重設 isMouseDown 變數
        isMouseDown = False
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    '================================ Button visual work ================

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

    Dim B_confirm_dis As Bitmap = Make_Button_Gray(My.Resources.Resource1.button_confirm)
    Dim B_CAT_DEL_dis As Bitmap = Make_Button_Gray(My.Resources.Resource1.button_CAT_DEL)
    Dim B_CAT_TRA_dis As Bitmap = Make_Button_Gray(My.Resources.Resource1.button_CAT_TRANS)
    Dim B_CAT_CSVIM_dis As Bitmap = Make_Button_Gray(My.Resources.Resource1.button_CAT_CSVIM)
    Dim B_CAT_CSVEX_dis As Bitmap = Make_Button_Gray(My.Resources.Resource1.button_CAT_CSVEX)

    Dim B_confirm_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_confirm)
    Dim B_Cancel_on As Bitmap = Make_Button_brighter(My.Resources.Resource1.button_Cancel)

    Dim B_CAT_on As New Bitmap(My.Resources.Resource1.button_CAT_on)
    Dim B_CAT_DEL_on As Bitmap = Make_Button_brighter(TwoBmpStack(B_CAT_DEL, B_CAT_on))
    Dim B_CAT_TRA_on As Bitmap = Make_Button_brighter(TwoBmpStack(B_CAT_TRA, B_CAT_on))
    Dim B_CAT_CSVIM_on As Bitmap = Make_Button_brighter(TwoBmpStack(B_CAT_CSVIM, B_CAT_on))
    Dim B_CAT_CSVEX_on As Bitmap = Make_Button_brighter(TwoBmpStack(B_CAT_CSVEX, B_CAT_on))

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

    'fix WINE Compatible
    Private Sub Mouse_Down(sender As Object, e As EventArgs) Handles ButtonOK.MouseDown,
        ButtonCancel.MouseDown, ButtonDelCat.MouseDown, ButtonTransFullCat.MouseDown,
        ButtonCSVIM.MouseDown, ButtonCSVEx.MouseDown

        DirectCast(sender, Control).Capture = False

    End Sub

End Class

Public Class CustomComboBox
    Inherits ComboBox

    <DllImport("user32.dll")>
    Private Shared Function GetWindowDC(ByVal hWnd As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Shared Function ReleaseDC(ByVal hWnd As IntPtr, ByVal hDC As IntPtr) As Integer
    End Function

    Protected Overrides Sub WndProc(ByRef m As Message)
        MyBase.WndProc(m)

        Const WM_NCPAINT As Integer = &H85

        If m.Msg = WM_NCPAINT Then
            Dim hdc As IntPtr = GetWindowDC(Me.Handle)
            If hdc <> IntPtr.Zero Then
                Using g As Graphics = Graphics.FromHdc(hdc)
                    Dim rect As New Rectangle(0, 0, Me.Width - 1, Me.Height - 1)
                    ControlPaint.DrawBorder(g, rect, Color.Red, ButtonBorderStyle.Solid)
                End Using
                ReleaseDC(Me.Handle, hdc)
            End If
        End If
    End Sub
End Class