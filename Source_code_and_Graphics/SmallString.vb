'MIT License 
'Copyright (c) 2023 overdoingism Labs.
'https://github.com/overdoignism/Final-Account-Defense-Barrier

Public Class SmallString
    Private Sub SmallString_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClearTextBox(IamaString)
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub SmallString_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        ClearTextBox(IamaString)
    End Sub
End Class