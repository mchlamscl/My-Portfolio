Imports System.Data.OleDb
Imports System.Diagnostics.Eventing.Reader

Public Class LOGIN
    Dim da As New OleDbDataAdapter
    Dim dset As New DataSet
    Dim comm As OleDbCommand

    Private Sub btnLogin_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        comm = New OleDbCommand
        comm.Connection = conn

        If txtUsername.Text = Nothing Or txtPassword.Text = Nothing Then
            MsgBox("Enter Username or Password", MsgBoxStyle.Exclamation)
            txtUsername.Clear()
            txtPassword.Clear()
        Else
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            Dim cmd As New OleDbCommand("Select count (*) from tblAccount where Uname=? and Pword=? ", conn)
            cmd.Parameters.AddWithValue("@1", OleDbType.VarChar).Value = txtUsername.Text
            cmd.Parameters.AddWithValue("@2", OleDbType.VarChar).Value = txtPassword.Text
            Dim count = Convert.ToInt32(cmd.ExecuteScalar())

            If (count > 0) Then
                MsgBox("Login Successfully", MsgBoxStyle.Information)
                HOME.Show()
                Me.Hide()
            Else
                MsgBox("Account not Found", MsgBoxStyle.Critical)
                txtUsername.Clear()
                txtPassword.Clear()
                txtUsername.Select()
            End If

        End If
    End Sub

    Private Sub btnStudent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStudent.Click
        SIGN_UP.Show()
        Me.Hide()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Application.ExitThread()
    End Sub
End Class
