
'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Public Class FormCSVWorking
    Public FormL, FormT, FormW, FormH As Integer

    Private Sub CSV_Working_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.StartPosition = FormStartPosition.Manual
        Me.Left = FormL + (FormW - Me.Width) / 2
        Me.Top = FormT + (FormH - Me.Height) / 2
    End Sub
End Class