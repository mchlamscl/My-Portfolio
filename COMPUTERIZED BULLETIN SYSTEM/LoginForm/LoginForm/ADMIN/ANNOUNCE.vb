Imports System.Data.Common
Imports System.Data.OleDb
Imports System.Globalization
Imports System.Runtime.Remoting.Messaging
Imports System.Security.Authentication.ExtendedProtection

Public Class ANNOUNCE
    Dim da As New OleDbDataAdapter
    Dim dset As New DataSet
    Dim comm As OleDbCommand
    Dim datetoday As String = Date.Now.ToString("MMM dd yyyy")

    Private Sub ANNOUNCE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        da = New OleDbDataAdapter("Select * from tblAnnouncement", conn)
        dset = New DataSet
        da.Fill(dset, "tblAnnouncement")
        dgv.DataSource = dset.Tables("tblAnnouncement").DefaultView
        populate()
    End Sub

    Function populate()
        da = New OleDbDataAdapter("Select * from tblAnnouncement", conn)
        dset = New DataSet
        da.Fill(dset, "tblAnnouncement")
        dgv.DataSource = dset.Tables("tblAnnouncement").DefaultView
        Return True
    End Function

    Private Sub dgv_CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgv.CellClick
        txtID.Text = dgv.Rows(e.RowIndex).Cells(0).Value.ToString
        txtTitle.Text = dgv.Rows(e.RowIndex).Cells(1).Value.ToString
        txtDefinition.Text = dgv.Rows(e.RowIndex).Cells(3).Value.ToString
        btnSend.Visible = False
        txtID.ReadOnly = True
        btnGenerate.Visible = False
        btnUpdate.Visible = True

        populate()
    End Sub

    Private Sub btnSend_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSend.Click
        Dim comm As OleDbCommand
        connect()

        If txtTitle.Text <> "" And txtID.Text <> "" And txtDefinition.Text <> "" Then
            comm = New OleDbCommand
            comm.Connection = conn

            comm.CommandText = "INSERT INTO tblAnnouncement VALUES (@ID, @Title, @DateToday, @Definition, @Condition)"
            comm.Parameters.AddWithValue("@ID", txtID.Text)
            comm.Parameters.AddWithValue("@Title", txtTitle.Text)
            comm.Parameters.AddWithValue("@DateToday", datetoday)
            comm.Parameters.AddWithValue("@Definition", txtDefinition.Text)
            comm.Parameters.AddWithValue("@Condition", txtCondition.Text)

            comm.ExecuteNonQuery()
            populate()

            txtID.Text = ""
            txtTitle.Text = ""
            txtDefinition.Text = ""
            btnGenerate.Visible = True
        Else
            MsgBox("Please complete the data", MsgBoxStyle.Critical, "ERROR")
        End If
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click

        connect()

        comm = New OleDbCommand
        comm.Connection = conn
        comm.CommandText = "UPDATE tblAnnouncement SET Title=@Title, DateAnn=@DateAnn, Definition=@Definition, Condition=@Condition WHERE ID=@ID"
        comm.Parameters.AddWithValue("@Title", txtTitle.Text)
        comm.Parameters.AddWithValue("@DateAnn", datetoday)
        comm.Parameters.AddWithValue("@Definition", txtDefinition.Text)
        comm.Parameters.AddWithValue("@Condition", txtCondition.Text)
        comm.Parameters.AddWithValue("@ID", txtID.Text)

        Try
            comm.ExecuteNonQuery()
            btnSend.Visible = True
            txtID.ReadOnly = True
            txtID.Text = ""
            txtTitle.Text = ""
            txtDefinition.Text = ""
            btnGenerate.Visible = True
            btnUpdate.Visible = False
            populate()
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        End Try

    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        txtID.ReadOnly = False
        txtID.Clear()
        txtTitle.Clear()
        txtDefinition.Clear()
        btnSend.Visible = True
        btnUpdate.Visible = False
        btnGenerate.Visible = True

    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click

        If txtID.Text <> "" Then
            Dim Result As MsgBoxResult
            Result = MsgBox("Are you sure you want to delete?", vbYesNo + vbQuestion, "DELETE")

            If Result = vbYes Then

                connect()
                comm = New OleDbCommand
                comm.Connection = conn
                comm.CommandText = "Delete from tblAnnouncement where ID='" & txtID.Text & "'"
                comm.ExecuteNonQuery()
                populate()
                txtID.Clear()
                txtTitle.Clear()
                txtDefinition.Clear()
                btnGenerate.Visible = True
                btnUpdate.Visible = False
                btnSend.Visible = True

            Else
                txtID.ReadOnly = False
                txtID.Clear()
                txtTitle.Clear()
                txtDefinition.Clear()
                btnSend.Visible = True
                btnUpdate.Visible = False
                btnGenerate.Visible = True
            End If

           
        Else
            Dim Result1 As MsgBoxResult
            Result1 = MsgBox("Please click any announcement you want to delete", vbInformation, "DELETE")
          
        End If


    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        da = New OleDbDataAdapter("Select * from tblAnnouncement where ID like '%" & txtSearch.Text & "' or DateAnn like '" & txtSearch.Text & "' or Title like '" & txtSearch.Text & "%'", conn)
        dset = New DataSet
        da.Fill(dset, "tblStudentAccount")
        dgv.DataSource = dset.Tables("tblStudentAccount").DefaultView
    End Sub

    Private Sub txtID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtID.KeyPress
       If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub dgv_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgv.CellFormatting

        For Each row As DataGridViewRow In dgv.Rows
            Dim cellValue As String = Convert.ToString(row.Cells(4).Value)

            If String.Compare(cellValue, "DELIVERED", StringComparison.OrdinalIgnoreCase) > 0 Then
                row.DefaultCellStyle.BackColor = Color.Red
            Else
                row.DefaultCellStyle.BackColor = Color.Green
            End If
        Next
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        txtID.Text = BasicFunction.GenerateRandomNumber.ToString
        btnGenerate.Visible = False
    End Sub
End Class