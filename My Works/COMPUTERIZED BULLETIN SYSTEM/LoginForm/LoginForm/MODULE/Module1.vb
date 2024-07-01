Imports System.Data.OleDb
Module Module1
    Public connStr As String = "Provider=Microsoft.Jet.Oledb.4.0; data source = " & Application.StartupPath & "\bulletdb.mdb"
    Public conn As New OleDbConnection(connStr)

    Function connect()
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Return True
    End Function
End Module
