'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Public Class FormKDF

    Public Progass10L As Integer
    Public Progass10R As Integer

    Private Sub FormSalt_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim PWD_KDF_R As New Bitmap(My.Resources.Resource1.PWD_KDF_R)
        Dim PWD_KDF_C As New Bitmap(My.Resources.Resource1.PWD_KDF_C)
        Dim IDXL, IDXL_old As Integer
        Dim IDXR, IDXR_old As Integer
        IDXL_old = -1
        IDXR_old = -1


        Me.Visible = True

        Do
            IDXL = (Progass10L / 10) + 1
            IDXR = (Progass10R / 10) + 1

            If IDXL > IDXL_old Then
                If IDXL <= 9 Then
                    CType(Me.Controls("PL0" & IDXL.ToString()), PictureBox).Image = PWD_KDF_R
                ElseIf IDXL = 10 Then
                    For IDX01 As Integer = 1 To 10
                        CType(Me.Controls("PL" & IDX01.ToString("D2")), PictureBox).Image = PWD_KDF_C
                    Next
                End If
                IDXL_old = IDXL
            End If

            If IDXR > IDXR_old Then
                If IDXR <= 9 Then
                    CType(Me.Controls("PR0" & IDXR.ToString()), PictureBox).Image = PWD_KDF_R
                ElseIf IDXR = 10 Then
                    For IDX01 As Integer = 1 To 10
                        CType(Me.Controls("PR" & IDX01.ToString("D2")), PictureBox).Image = PWD_KDF_C
                    Next
                End If
                IDXR_old = IDXR
            End If

            If IDXR + IDXL >= 20 Then PWD_PASS.Visible = True

            My.Application.DoEvents()
            Threading.Thread.Sleep(50)

        Loop Until (Progass10L >= 100) And (Progass10R >= 100)

        Threading.Thread.Sleep(300)
        Me.DialogResult = DialogResult.OK

    End Sub


End Class