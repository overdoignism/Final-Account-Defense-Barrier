'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Public Class FormKDF

    Public Progass10L As Integer
    Public Progass10R As Integer
    Public Progass10L2 As Integer
    Public Progass10R2 As Integer
    Public KDF_Type As Integer = 1

    Dim PWD_KDF_C As New Bitmap(My.Resources.Resource1.PWD_KDF_C)
    Dim IDXL As Integer = 0
    Dim IDXR As Integer = 0

    Private Sub FormKDF_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.Visible = True
        System.Windows.Forms.Application.DoEvents()

        Dim IDXL_Done(10) As Byte
        Dim IDXR_Done(10) As Byte
        Dim IDXL_AllDone, IDXR_AllDone As Boolean

        Dim TmpIDX As Integer

        Do

            IDXL = ((Progass10L + Progass10L2) / 10) + 1
            IDXR = ((Progass10R + Progass10R2) / 10) + 1

            Threading.Thread.Sleep(10)

            If IDXR > 10 Then IDXR = 10
            If Not IDXR_AllDone Then
                For TmpIDX = 1 To IDXR
                    If IDXR_Done(TmpIDX) = 0 Then
                        Me.Controls("PR" & TmpIDX.ToString("D2")).Visible = True
                        IDXR_Done(TmpIDX) = 1
                    End If
                Next
            End If
            If IDXR = 10 Then
                For IDX01 As Integer = 1 To 9
                    CType(Me.Controls("PR" & IDX01.ToString("D2")), PictureBox).Image = PWD_KDF_C
                Next
                PR10.Visible = True
                IDXR_AllDone = True
            End If

            If IDXL > 10 Then IDXL = 10
            If Not IDXL_AllDone Then
                For TmpIDX = 1 To IDXL
                    If IDXL_Done(TmpIDX) = 0 Then
                        Me.Controls("PL" & TmpIDX.ToString("D2")).Visible = True
                        IDXL_Done(TmpIDX) = 1
                    End If
                Next
            End If
            If IDXL = 10 Then
                For IDX01 As Integer = 1 To 9
                    CType(Me.Controls("PL" & IDX01.ToString("D2")), PictureBox).Image = PWD_KDF_C
                Next
                PL10.Visible = True
                IDXL_AllDone = True
            End If

            System.Windows.Forms.Application.DoEvents()

            If IDXR_AllDone And IDXL_AllDone Then
                PWD_PASS.Visible = True
                System.Windows.Forms.Application.DoEvents()
                Threading.Thread.Sleep(700)
                Exit Do
            End If

        Loop

        Me.DialogResult = DialogResult.OK

    End Sub

    Private Sub PicKDF_TYPE_Paint(sender As Object, e As PaintEventArgs) Handles PicKDF_TYPE.Paint
        Select Case KDF_Type
            Case 1
                e.Graphics.DrawImage(My.Resources.Resource1.KDFT_Types, 0, 0)
            Case 2
                e.Graphics.DrawImage(My.Resources.Resource1.KDFT_Types, 0, -13)
            Case 3
                e.Graphics.DrawImage(My.Resources.Resource1.KDFT_Types, 0, -26)
        End Select
    End Sub


End Class