Imports System.Data.OleDb
Public Class STUDENTSETTINGS
    Dim da As New OleDbDataAdapter
    Dim dset As New DataSet
    Dim comm As OleDbCommand

    Private Sub btnEnter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnter.Click
        If txtUsername.Text <> "" And txtCurrentPassword.Text <> "" And txtConfirmPassword.Text <> "" And txtNewPassword.Text <> "" Then
            If txtConfirmPassword.Text = txtNewPassword.Text Then
                connect()

                comm = New OleDbCommand
                comm.Connection = conn
                comm.CommandText = "UPDATE tblStudentAccount SET StudentPassword='" & txtNewPassword.Text & "' WHERE Username='" & txtUsername.Text & "' AND StudentPassword = '" & txtCurrentPassword.Text & "' "
                Dim rowsAffected As Integer = comm.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    MsgBox("Successfully Changed", MsgBoxStyle.OkOnly, "CHANGE PASSWORD")

                    txtUsername.Clear()
                    txtCurrentPassword.Clear()
                    txtNewPassword.Clear()
                    txtConfirmPassword.Clear()
                Else
                    MsgBox("Username and Current Password do not match or record not found.", MsgBoxStyle.Critical, "CHANGE PASSWORD")
                    txtUsername.Clear()
                    txtCurrentPassword.Clear()
                    txtNewPassword.Clear()
                    txtConfirmPassword.Clear()
                End If
            Else
                MsgBox("New Password and Confirm Password do not match!", MsgBoxStyle.Critical, "CHANGE PASSWORD")
                txtUsername.Clear()
                txtCurrentPassword.Clear()
                txtNewPassword.Clear()
                txtConfirmPassword.Clear()
            End If
        Else
            MsgBox("Please complete the data.", MsgBoxStyle.Critical, "CHANGE PASSWORD")
        End If


    End Sub


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        txtUsername.Clear()
        txtCurrentPassword.Clear()
        txtNewPassword.Clear()
        txtConfirmPassword.Clear()
    End Sub
End Class