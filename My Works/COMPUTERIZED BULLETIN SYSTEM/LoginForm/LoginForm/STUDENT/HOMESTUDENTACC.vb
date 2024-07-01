Public Class HOMESTUDENTACC
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblTime.Text = DateTime.Now.ToString("hh:mm tt")
        lblDate.Text = Date.Now.ToString("MMM dd yyyy")
    End Sub

    Private Sub pnlContent_Paint(sender As Object, e As PaintEventArgs) Handles pnlContent.Paint
        Timer1.Enabled = True
    End Sub

    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        STUDENTSETTINGS.Hide()
        STUDENTANNOUNCEMENT.Hide()
        Me.Show()
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Dim response As Integer

        response = MessageBox.Show(" Are you sure you want to Log out?", "LOG OUT", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If response = vbYes Then
            LOGIN.txtUsername.Text = ""
            LOGIN.txtPassword.Text = ""
            SIGN_UP.txtStudentID.Text = ""
            SIGN_UP.txtUsername.Text = ""
            SIGN_UP.txtPassword.Text = ""
            LOGIN.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub btnShowAnnouncement_Click(sender As Object, e As EventArgs) Handles btnShowAnnouncement.Click
        STUDENTSETTINGS.Hide()
        With STUDENTANNOUNCEMENT
            .TopLevel = False
            Panel1.Controls.Add(STUDENTANNOUNCEMENT)
            Panel2.Controls.Add(STUDENTANNOUNCEMENT)
            pnlContent.Controls.Add(STUDENTANNOUNCEMENT)
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        STUDENTANNOUNCEMENT.Hide()
        With STUDENTSETTINGS
            .TopLevel = False
            Panel1.Controls.Add(STUDENTSETTINGS)
            Panel2.Controls.Add(STUDENTSETTINGS)
            pnlContent.Controls.Add(STUDENTSETTINGS)
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Dim response As Integer

        response = MessageBox.Show("Are you sure you want to exit?", "EXIT", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If response = vbYes Then
            Application.ExitThread()
        End If
    End Sub
End Class