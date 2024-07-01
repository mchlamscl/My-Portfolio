Imports System.Data.Common
Imports System.Data.OleDb
Imports System.Globalization
Imports System.Runtime.Remoting.Messaging
Imports System.Security.Authentication.ExtendedProtection

Public Class STUDENTANNOUNCEMENT
    Dim da As New OleDbDataAdapter
    Dim dset As New DataSet
    Dim comm As OleDbCommand

    Private Sub STUDENTANNOUNCEMENT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        da = New OleDbDataAdapter("Select * from tblAnnouncement", conn)
        dset = New DataSet
        da.Fill(dset, "tblAnnouncement")
        dgv.DataSource = dset.Tables("tblAnnouncement").DefaultView
    End Sub

    Private Sub dgv_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellClick
        txtID.Text = dgv.Rows(e.RowIndex).Cells(0).Value.ToString
        txtTitle.Text = dgv.Rows(e.RowIndex).Cells(1).Value.ToString
        txtDate.Text = dgv.Rows(e.RowIndex).Cells(2).Value.ToString
        txtDefinition.Text = dgv.Rows(e.RowIndex).Cells(3).Value.ToString
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

    Function populate()
        da = New OleDbDataAdapter("Select * from tblAnnouncement", conn)
        dset = New DataSet
        da.Fill(dset, "tblAnnouncement")
        dgv.DataSource = dset.Tables("tblAnnouncement").DefaultView
        Return True
    End Function

    Private Sub btnAlreadyRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAlreadyRead.Click
        connect()

        comm = New OleDbCommand
        comm.Connection = conn
        comm.CommandText = "Update tblAnnouncement set Condition='" & txtCondition.Text & "' where ID ='" & txtID.Text & "'"
        comm.ExecuteNonQuery()
        populate()

    End Sub

End Class