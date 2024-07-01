Imports System.Data.OleDb
Imports System.Diagnostics.Eventing.Reader
Public Class SIGN_UP
    Dim da As New OleDbDataAdapter
    Dim dset As New DataSet
    Dim comm As OleDbCommand

    Private Sub btnLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogin.Click
        comm = New OleDbCommand
        comm.Connection = conn

        If txtStudentID.Text = Nothing Or txtUsername.Text = Nothing Or txtPassword.Text = Nothing Then
            MsgBox("Enter Username, Password or ID", MsgBoxStyle.Exclamation)
            txtStudentID.Clear()
            txtUsername.Clear()
            txtPassword.Clear()
        Else
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim cmd As New OleDbCommand("Select count (*) from tblStudentAccount where StudentID=? and Username=? and StudentPassword=?", conn)
            cmd.Parameters.AddWithValue("@1", OleDbType.VarChar).Value = txtStudentID.Text
            cmd.Parameters.AddWithValue("@2", OleDbType.VarChar).Value = txtUsername.Text
            cmd.Parameters.AddWithValue("@3", OleDbType.VarChar).Value = txtPassword.Text
            Dim count = Convert.ToInt32(cmd.ExecuteScalar())

            If (count > 0) Then
                MsgBox("Login Successfully", MsgBoxStyle.Information)
                HOMESTUDENTACC.Show()
                Me.Hide()
            Else
                MsgBox("Account not Found", MsgBoxStyle.Critical)
                txtStudentID.Clear()
                txtUsername.Clear()
                txtPassword.Clear()
            End If

        End If
       
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.ExitThread()
    End Sub


    Private Sub btnBacktoAdmin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBacktoAdmin.Click
        LOGIN.Show()
        Me.Close()
        LOGIN.txtUsername.Clear()
        LOGIN.txtPassword.Clear()
    End Sub
End Class