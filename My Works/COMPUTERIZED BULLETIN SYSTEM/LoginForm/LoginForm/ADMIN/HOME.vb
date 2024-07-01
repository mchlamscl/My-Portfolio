Imports System.Data.OleDb
Imports System.Runtime.Remoting.Messaging

Public Class HOME
    Dim da As New OleDbDataAdapter
    Dim dset As New DataSet
    Dim comm As OleDbCommand

    Private Sub btnShowAnnouncement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With ADMINSETTINGS
            .TopLevel = False
            Panel1.Controls.Add(ADMINSETTINGS)
            Panel2.Controls.Add(ADMINSETTINGS)
            pnlContent.Controls.Add(ADMINSETTINGS)
            .BringToFront()
            .Show()
        End With
    End Sub
    Private Sub PictureBoxExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim response As Integer

        response = MessageBox.Show(" Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If response = vbYes Then

            Application.ExitThread()

        End If
    End Sub

    Private Sub btnAnnounce_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnnounce.Click
        With ANNOUNCE
            .TopLevel = False
            Panel1.Controls.Add(ANNOUNCE)
            Panel2.Controls.Add(ANNOUNCE)
            pnlContent.Controls.Add(ANNOUNCE)
            .BringToFront()
            .Show()
        End With
        populate()
    End Sub

    Function populate()
        da = New OleDbDataAdapter("Select * from tblAnnouncement", conn)
        dset = New DataSet
        da.Fill(dset, "tblAnnouncement")
        ANNOUNCE.dgv.DataSource = dset.Tables("tblAnnouncement").DefaultView
        Return True
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        lblTime.Text = DateTime.Now.ToString("hh:mm tt")
        lblDate.Text = Date.Now.ToString("MMM dd yyyy")
    End Sub

    Private Sub pnlContent_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlContent.Paint
        Timer1.Enabled = True
    End Sub

    Private Sub btnLogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Dim response As Integer

        response = MessageBox.Show(" Are you sure you want to Log out?", "LOG OUT", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If response = vbYes Then
            LOGIN.txtUsername.Text = ""
            LOGIN.txtPassword.Text = ""
            LOGIN.Show()
            Me.Hide()
        End If

    End Sub

    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreate.Click
        With CREATESTUDENTACC
            .TopLevel = False
            Panel1.Controls.Add(CREATESTUDENTACC)
            Panel2.Controls.Add(CREATESTUDENTACC)
            pnlContent.Controls.Add(CREATESTUDENTACC)
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PictureBox2.Click
        Dim response As Integer

        response = MessageBox.Show("Are you sure you want to exit?", "EXIT", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If response = vbYes Then
            Application.ExitThread()
        End If
    End Sub

    Private Sub btnHome_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnHome.Click
        ANNOUNCE.Hide()
        ADMINSETTINGS.Hide()
        CREATESTUDENTACC.Hide()
        Me.Show()
    End Sub

    Private Sub btnSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettings.Click
        With ADMINSETTINGS
            .TopLevel = False
            Panel1.Controls.Add(ADMINSETTINGS)
            Panel2.Controls.Add(ADMINSETTINGS)
            pnlContent.Controls.Add(ADMINSETTINGS)
            .BringToFront()
            .Show()
        End With
    End Sub
    Private Sub HOME_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        populate1()
        populate2()
    End Sub

    Function populate1()
        comm = New OleDbCommand
        comm.Connection = conn
        comm.CommandText = "Select count (*) from tblStudentAccount"
        comm.ExecuteNonQuery()

        Dim count As Int16 = Convert.ToInt16(comm.ExecuteScalar())
        lblStudents.Text = count.ToString
        Return True
    End Function

    Function populate2()
        comm = New OleDbCommand
        comm.Connection = conn
        comm.CommandText = "Select count (*) from tblAnnouncement"
        comm.ExecuteNonQuery()

        Dim count1 As Int16 = Convert.ToInt16(comm.ExecuteScalar())
        lblAnnouncement.Text = count1.ToString
        Return True
    End Function
End Class