'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Public Class FormMIT
    Public FormL, FormT, FormW, FormH As Integer
    Private lastLocation As Point
    Private isMouseDown As Boolean = False
    Public NowPage As Integer = 0
    Dim DocPage(2) As Bitmap

    Private Sub FormMIT_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.StartPosition = FormStartPosition.Manual
        Me.Left = FormL + (FormW - Me.Width) / 2
        Me.Top = FormT + (FormH - Me.Height) / 2

        'PictureLICENSEBack.Image = Image.FromStream(New IO.MemoryStream(My.Resources.Resource1.MIT_DOC_PNG))
        'PictureBoxTXT.Image = My.Resources.Resource1.Doc01P

        Dim BackPaper As Bitmap = CreateGradientBitmap(423, 470)

        DocPage(0) = TwoBmpStack(BackPaper, Image.FromStream(New IO.MemoryStream(My.Resources.Resource1.Doc01P_PNG)))
        DocPage(1) = TwoBmpStack(BackPaper, Image.FromStream(New IO.MemoryStream(My.Resources.Resource1.Doc02P_PNG)))
        DocPage(2) = TwoBmpStack(BackPaper, Image.FromStream(New IO.MemoryStream(My.Resources.Resource1.Doc03P_PNG)))

        PictureLICENSEBack.Image = DocPage(0)

        DocRight.Image = MirrorBitmap(DocLeft.Image, 0)

    End Sub

    Private Sub DocRight_Click(sender As Object, e As EventArgs) Handles DocRight.Click

        DocRight.Visible = True
        DocLeft.Visible = True

        If NowPage < 2 Then
            NowPage += 1
            PictureLICENSEBack.Image = DocPage(NowPage)
        End If

        If NowPage = 2 Then DocRight.Visible = False

    End Sub

    Private Sub DocLeft_Click(sender As Object, e As EventArgs) Handles DocLeft.Click

        DocRight.Visible = True
        DocLeft.Visible = True

        If NowPage > 0 Then
            NowPage -= 1
            PictureLICENSEBack.Image = DocPage(NowPage)
        End If

        If NowPage = 0 Then DocLeft.Visible = False

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

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub PictureMIT_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureMIT.MouseUp
        ' 當滑鼠左鍵釋放時，重設 isMouseDown 變數
        isMouseDown = False
    End Sub

End Class