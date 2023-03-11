'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Public Class FormSalt

    Public PROG_10() As Bitmap = {My.Resources.Resource1.salting_01,
    My.Resources.Resource1.salting_02, My.Resources.Resource1.salting_03, My.Resources.Resource1.salting_04,
    My.Resources.Resource1.salting_05, My.Resources.Resource1.salting_06, My.Resources.Resource1.salting_07,
    My.Resources.Resource1.salting_08, My.Resources.Resource1.salting_09, My.Resources.Resource1.salting_10}

    Public Progass10 As Integer

    Private Sub FormSalt_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim IDX01, IDX02 As Integer
        IDX01 = 0
        IDX02 = 0

        Me.Visible = True

        Do
            IDX01 = Progass10 / 10
            If IDX01 > IDX02 Then
                PictureBox1.Image = PROG_10(IDX01 - 1)
                IDX02 = IDX01
            End If

            My.Application.DoEvents()
            Threading.Thread.Sleep(50)

        Loop Until Progass10 >= 100

        Threading.Thread.Sleep(300)
        Me.DialogResult = DialogResult.OK

    End Sub
End Class