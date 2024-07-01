Imports System.Data.OleDb

Public Class CREATESTUDENTACC
    Dim da As New OleDbDataAdapter
    Dim dset As New DataSet
    Dim comm As OleDbCommand

    Function populate()
        da = New OleDbDataAdapter("Select * from tblStudentAccount", conn)
        dset = New DataSet
        da.Fill(dset, "tblStudentAccount")
        dgv.DataSource = dset.Tables("tblStudentAccount").DefaultView
        Return True
    End Function

    Private Sub CREATESTUDENTACC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        da = New OleDbDataAdapter("Select * from tblStudentAccount", conn)
        dset = New DataSet
        da.Fill(dset, "tblStudentAccount")
        dgv.DataSource = dset.Tables("tblStudentAccount").DefaultView
    End Sub

    Private Sub dgv_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        txtStudentID.Text = dgv.Rows(e.RowIndex).Cells(0).Value.ToString
        txtFirstname.Text = dgv.Rows(e.RowIndex).Cells(1).Value.ToString
        txtLastname.Text = dgv.Rows(e.RowIndex).Cells(2).Value.ToString
        cboYearLevel.Text = dgv.Rows(e.RowIndex).Cells(3).Value.ToString
        cboSection1.Text = dgv.Rows(e.RowIndex).Cells(4).Value.ToString
        txtUsername.Text = dgv.Rows(e.RowIndex).Cells(5).Value.ToString
        txtPassword.Text = dgv.Rows(e.RowIndex).Cells(6).Value.ToString
        cboCourse.Text = dgv.Rows(e.RowIndex).Cells(8).Value.ToString
        btnCreate.Visible = False
        txtPassword.ReadOnly = True
        txtStudentID.ReadOnly = True
    End Sub

    Private Sub btnCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Dim comm As OleDbCommand
        Dim useraccess As String
        useraccess = "STUDENT"

        connect()
        If txtStudentID.Text <> "" And txtFirstname.Text <> "" And txtLastname.Text <> "" And cboYearLevel.Text <> "" And cboSection1.Text <> "" And txtUsername.Text <> "" And txtPassword.Text <> "" Then
            comm = New OleDbCommand
            comm.Connection = conn
            comm.CommandText = "Insert into tblStudentAccount values ('" & txtStudentID.Text & "', '" & txtFirstname.Text & "', '" & txtLastname.Text & "', '" & cboYearLevel.Text & "', '" & cboSection1.Text & "',  '" & txtUsername.Text & "', '" & txtPassword.Text & "','" & useraccess & "', '" & cboCourse.Text & "')"
            comm.ExecuteNonQuery()
            populate()


            txtStudentID.Text = ""
            txtFirstname.Text = ""
            txtLastname.Text = ""
            cboYearLevel.Text = ""
            cboSection1.Text = ""
            txtUsername.Text = ""
            txtPassword.Text = ""
            cboCourse.Text = ""
        Else
            MsgBox("Please complete the data", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        connect()

        If txtStudentID.Text <> "" And txtFirstname.Text <> "" And txtLastname.Text <> "" And cboYearLevel.Text <> "" And cboSection1.Text <> "" And txtUsername.Text <> "" And txtPassword.Text <> "" Then
            comm = New OleDbCommand
            comm.Connection = conn
            comm.CommandText = "Update tblStudentAccount set Firstname='" & txtFirstname.Text & "', Lastname='" & txtLastname.Text & "', YearLevel='" & cboYearLevel.Text & "', Sections='" & cboSection1.Text & "', Username='" & txtUsername.Text & "', Course='" & cboCourse.Text & "' where StudentID='" & txtStudentID.Text & "'"
            comm.ExecuteNonQuery()
            populate()

            btnCreate.Visible = True
            txtStudentID.Clear()
            txtFirstname.Clear()
            txtLastname.Clear()
            cboYearLevel.Text = ""
            cboSection1.Text = ""
            txtUsername.Clear()
            txtPassword.Clear()
            txtPassword.ReadOnly = False
            cboCourse.Text = ""
        Else
            MsgBox("Please complete the data", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        txtStudentID.Clear()
        txtFirstname.Clear()
        txtLastname.Clear()
        cboYearLevel.Text = ""
        cboSection1.Text = ""
        txtUsername.Clear()
        txtPassword.Clear()
        cboCourse.Text = ""
        btnCreate.Visible = True
        txtPassword.ReadOnly = False
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim Result As MsgBoxResult
        Result = MsgBox("Are you sure you want to delete?", vbYesNo, "Delete")

        If Result = vbYes Then
            connect()
            comm = New OleDbCommand
            comm.Connection = conn
            comm.CommandText = "Delete from tblStudentAccount where StudentID='" & txtStudentID.Text & "'"
            comm.ExecuteNonQuery()
            populate()
            txtStudentID.Clear()
            txtFirstname.Clear()
            txtLastname.Clear()
            txtUsername.Clear()
            txtPassword.Clear()
            cboCourse.Text = ""
            cboSection1.Text = ""
            cboYearLevel.Text = ""
        End If
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        da = New OleDbDataAdapter("Select * from tblStudentAccount where StudentID like '%" & txtSearch.Text & "' or Lastname like '" & txtSearch.Text & "' or Firstname like '" & txtSearch.Text & "' or YearLevel like '" & txtSearch.Text & "' or Sections like '" & txtSearch.Text & "' or Course like '" & txtSearch.Text & "' or Username like '" & txtSearch.Text & "' or Username like '" & txtSearch.Text & "%' ", conn)
        dset = New DataSet
        da.Fill(dset, "tblStudentAccount")
        dgv.DataSource = dset.Tables("tblStudentAccount").DefaultView
    End Sub

    Private Sub txtStudentID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStudentID.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
End Class