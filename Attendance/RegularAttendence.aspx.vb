Imports System.Data
Imports System.Globalization
Imports System.Configuration
Imports System.Net
Imports System.Net.Mail
Imports System.Data.SqlClient

Partial Class Teacher_RegularAttendence
    Inherits System.Web.UI.Page

    ' Define connection string (adjust details as necessary)
    Dim connectionString As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Dim sql As String = Nothing
    Dim DT As DataTable = Nothing
    Private att As Attendance = New Attendance
    Dim table As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("Sessionid") = Request.QueryString("s")
            ViewState("Courseid") = Request.QueryString("Courseid")
            ViewState("classesid") = Request.QueryString("classesid")

            txtDt.Text = Format(CType(Now.Date, Date), "yyyy-MM-dd")
            bindgrd()
            CheckBoxChecked()
        End If
    End Sub

    Protected Sub txtDt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDt.TextChanged
        Retsid.Value = ""
        bindgrd()
        CheckBoxChecked()
    End Sub

    Sub bindgrd()
        ' Clear previous messages
        chkcopyatt.Items.Clear()
        msgsave.InnerHtml = ""
        Literal2.Text = ""

        ' Define SQL query to retrieve students based on CourseID and ClassesID
        Dim sql As String = "SELECT s.StudentID, s.Student " &
                            "FROM Student AS s " &
                            "WHERE s.CourseID = @CourseID AND s.ClassesID = @ClassesID"


        ' Create and open database connection
        Using conn As New SqlConnection(ConnectionString)
            Using cmd As New SqlCommand(sql, conn)
                ' Define and add parameters for SQL query
                cmd.Parameters.AddWithValue("@CourseID", ViewState("Courseid"))
                cmd.Parameters.AddWithValue("@ClassesID", ViewState("classesid"))

                ' Execute query and bind results to a DataTable
                Dim dt As New DataTable()
                Dim adapter As New SqlDataAdapter(cmd)
                conn.Open()
                adapter.Fill(dt)

                ' Add a serial number column to DataTable
                dt.Columns.Add("SerialNumber", GetType(Integer))
                For i As Integer = 0 To dt.Rows.Count - 1
                    dt.Rows(i)("SerialNumber") = i + 1
                Next

                ' Store the DataTable in ViewState for later access in btnSave_Click
                ViewState("StudentTable") = dt

                ' Bind DataTable to GridView
                If dt.Rows.Count > 0 Then
                    StudentGridView.DataSource = dt
                    StudentGridView.DataBind()
                Else
                    msgsave.InnerHtml = "Record Not Found!"
                End If
            End Using
        End Using
    End Sub



    Protected Sub chkSelectAll_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        ' Get the reference to the GridView directly
        Dim gv As GridView = StudentGridView

        ' Loop through all rows of the GridView
        For Each row As GridViewRow In gv.Rows
            ' Find the checkbox for each row
            Dim chkAttendance As CheckBox = CType(row.FindControl("AttendanceCheckbox"), CheckBox)

            ' If the "Select All" checkbox is checked, check each checkbox in the GridView
            chkAttendance.Checked = CType(sender, CheckBox).Checked
        Next
    End Sub



    Protected Sub btnHidden_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHidden.Click
        Panel1.Visible = True
        msgsave.InnerHtml = ""

        ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "fun();", True)
        Literal2.Text = att.GetStudentatt1(txtDt.Text, Retsid.Value, ViewState("Sessionid"), ViewState("Courseid"), ViewState("classesid"), ViewState("Sem"))

        chkcopyatt.Items.Clear()
        chkcopyatt.DataSource = att.GetStudentatt2("02/02/1980", Retsid.Value, ViewState("Sessionid"), ViewState("Courseid"), ViewState("classesid"), ViewState("Sem"))
        chkcopyatt.DataBind()
    End Sub
    Private Sub CheckBoxChecked()
        Using con As New SqlConnection(connectionString)
            con.Open() ' Ensure the connection is open before executing commands

            ' SQL query to count attendance with the given conditions
            Dim sql As String = "SELECT COUNT(status) " & _
                                "FROM StudentAtt " & _
                                "WHERE Dated IS NOT NULL " & _
                                "AND TeacherID = @TeacherID " & _
                                "AND CourseID = @CourseID " & _
                                "AND ClassesID = @ClassesID " & _
                                "AND StudentID = @StudentID " & _
                                "AND Dated = @Dated " & _
                                "AND Status = 'P'"

            ' Loop through each GridView row
            For Each row As GridViewRow In StudentGridView.Rows
                ' Find the checkbox in the current row
                Dim chkAttendance As CheckBox = CType(row.FindControl("AttendanceCheckbox"), CheckBox)

                ' Process only if the checkbox is checked

                ' Extract StudentID from the current row
                Dim studentId As String = row.Cells(1).Text ' Assuming StudentID is in the second column (index 1)

                Using cmd As New SqlCommand(sql, con)
                    ' Add parameters for the SQL query
                    cmd.Parameters.AddWithValue("@Dated", CDate(txtDt.Text)) ' Use the selected date
                    cmd.Parameters.AddWithValue("@TeacherID", Request.QueryString("u")) ' Employee ID (TeacherID)
                    cmd.Parameters.AddWithValue("@StudentID", studentId) ' Student ID
                    cmd.Parameters.AddWithValue("@CourseID", ViewState("Courseid")) ' Course ID
                    cmd.Parameters.AddWithValue("@ClassesID", ViewState("classesid")) ' Classes ID

                    ' Execute the query
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    ' Handle the result (count) as needed, e.g., log or update UI
                    If count > 0 Then
                        ' Set the checkbox to checked
                        chkAttendance.Checked = True
                    End If
                End Using

            Next
        End Using
    End Sub



    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            ' Retrieve the DataTable from ViewState
            Dim dt As DataTable = CType(ViewState("StudentTable"), DataTable)
            If dt Is Nothing Then
                msgsave.InnerHtml = "Error: Unable to retrieve student data. Please try again."
                Exit Sub
            End If

            ' Create DataTable with columns for StudentID and Status
            Dim table As New DataTable()
            table.Columns.Add("StudentID", GetType(String))
            table.Columns.Add("Status", GetType(String))

            ' Loop through each row in GridView to get checkbox selections
            For Each row As GridViewRow In StudentGridView.Rows
                Dim studentId As String = dt.Rows(row.RowIndex)("StudentID").ToString()
                ' Find the checkbox in the current row
                Dim chkBox As CheckBox = CType(row.FindControl("AttendanceCheckbox"), CheckBox)
                Dim status As String = If(chkBox IsNot Nothing AndAlso chkBox.Checked, "P", "A")

                ' Add student ID and their attendance status to the table
                table.Rows.Add(studentId, status)
            Next

            ' Insert attendance records if there are rows in the DataTable
            If table.Rows.Count > 0 Then
                ' Establish the connection to the database
                Using conn As New SqlConnection(connectionString)
                    conn.Open()

                    ' Flag to check if any duplicate attendance was found
                    Dim isDuplicate As Boolean = False

                    ' Loop through each row in the table to insert attendance data
                    For Each row As DataRow In table.Rows
                        Dim studentId As String = row("StudentID").ToString()
                        Dim status As String = row("Status").ToString()

                        ' Define the SQL query to check if attendance already exists for this student and date
                        Dim checkSql As String = "SELECT COUNT(*) FROM [dbo].[StudentAtt] " &
                                                 "WHERE [Dated] = @Dated AND [StudentID] = @StudentID AND [CourseID] = @CourseID AND [ClassesID] = @ClassesID"

                        Using cmdCheck As New SqlCommand(checkSql, conn)
                            ' Add parameters for the SQL query
                            cmdCheck.Parameters.AddWithValue("@Dated", CDate(txtDt.Text))
                            cmdCheck.Parameters.AddWithValue("@StudentID", studentId)
                            cmdCheck.Parameters.AddWithValue("@CourseID", ViewState("Courseid"))
                            cmdCheck.Parameters.AddWithValue("@ClassesID", ViewState("classesid"))

                            ' Execute the check query and get the result
                            Dim recordCount As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

                            ' If a record already exists, set the flag to true and skip the insert
                            If recordCount > 0 Then
                                isDuplicate = True
                            Else
                                ' Define the SQL query to insert attendance data into StudentAtt
                                Dim sql As String = "INSERT INTO [dbo].[StudentAtt] " &
                                                    "([Dated], [Period], [TeacherID], [StudentID], [Status], [CourseID], [ClassesID]) " &
                                                    "VALUES (@Dated, @Period, @TeacherID, @StudentID, @Status, @CourseID, @ClassesID)"

                                Using cmd As New SqlCommand(sql, conn)
                                    ' Add parameters for the SQL query
                                    cmd.Parameters.AddWithValue("@Dated", CDate(txtDt.Text))
                                    cmd.Parameters.AddWithValue("@Period", 0)
                                    cmd.Parameters.AddWithValue("@TeacherID", Request.QueryString("u"))
                                    cmd.Parameters.AddWithValue("@StudentID", studentId)
                                    cmd.Parameters.AddWithValue("@Status", status)
                                    cmd.Parameters.AddWithValue("@CourseID", ViewState("Courseid"))
                                    cmd.Parameters.AddWithValue("@ClassesID", ViewState("classesid"))

                                    ' Execute the insert command
                                    cmd.ExecuteNonQuery()
                                End Using
                            End If
                        End Using
                    Next

                    ' Show appropriate message based on whether a duplicate attendance was found
                    If isDuplicate Then
                        msgsave.InnerHtml = "Attendance has already been recorded. To make changes, please delete the existing entry and then re-enter the information."
                    Else
                        msgsave.InnerHtml = "Attendance saved successfully."
                    End If

                    ' Reset the form or perform any other necessary actions
                    ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "fun();", True)
                    Panel1.Visible = False
                End Using

                ' Call the updateStatus function with the attendance table
                updateStatus(table)
            End If
        Catch ex As Exception
            msgsave.InnerHtml = "An error occurred. Please try again."
        End Try
    End Sub

    Private Sub updateStatus(ByVal table As DataTable)
        ' Establish the connection to the database
        Using conn As New SqlConnection(connectionString)
            conn.Open() ' Open the connection to the database

            ' Iterate through all rows in the DataTable
            For Each row As DataRow In table.Rows
                Dim studentId As String = row("StudentID").ToString()
                Dim status As String = row("Status").ToString()

                ' Define the SQL query to check if attendance already exists for this student and date
                Dim checkSql As String = "SELECT COUNT(*) FROM [dbo].[StudentAtt] " &
                                         "WHERE [Dated] = @Dated AND [StudentID] = @StudentID AND [CourseID] = @CourseID AND [ClassesID] = @ClassesID"

                Using cmdCheck As New SqlCommand(checkSql, conn)
                    ' Add parameters for the SQL query
                    cmdCheck.Parameters.AddWithValue("@Dated", CDate(txtDt.Text)) ' Use the selected date
                    cmdCheck.Parameters.AddWithValue("@StudentID", studentId) ' Student ID
                    cmdCheck.Parameters.AddWithValue("@CourseID", ViewState("Courseid")) ' Course ID
                    cmdCheck.Parameters.AddWithValue("@ClassesID", ViewState("classesid")) ' Classes ID

                    ' Execute the check query and get the result
                    Dim recordCount As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

                    ' Proceed only if a record exists
                    If recordCount > 0 Then
                        ' Check if the student is on leave during this period
                        Dim leaveQuery As String = "SELECT 1 FROM LeaveApplied " &
                                                   "WHERE StudentID = @StudentID " &
                                                   "AND @Dated BETWEEN fromDate AND toDate " &
                                                   "AND status = 'Approve'"

                        Dim isOnLeave As Boolean = False

                        Using leaveCmd As New SqlCommand(leaveQuery, conn)
                            ' Add parameters for the SQL query
                            leaveCmd.Parameters.AddWithValue("@Dated", CDate(txtDt.Text)) ' Use the selected date
                            leaveCmd.Parameters.AddWithValue("@StudentID", studentId) ' Student ID

                            ' Execute the query and check if it returns any rows
                            Dim result As Object = leaveCmd.ExecuteScalar()
                            isOnLeave = result IsNot Nothing AndAlso Convert.ToInt32(result) = 1
                        End Using

                        If isOnLeave Then
                            ' Update the attendance record status to 'E' (Excused)
                            Dim updateSql As String = "UPDATE StudentAtt " &
                                                      "SET Status = 'E' " &
                                                      "WHERE StudentID = @StudentID AND Dated = @Dated " &
                                                      "AND CourseID = @CourseID AND ClassesID = @ClassesID"

                            Using updateCmd As New SqlCommand(updateSql, conn)
                                ' Add parameters for the SQL query
                                updateCmd.Parameters.AddWithValue("@Dated", CDate(txtDt.Text)) ' Use the selected date
                                updateCmd.Parameters.AddWithValue("@StudentID", studentId) ' Student ID
                                updateCmd.Parameters.AddWithValue("@CourseID", ViewState("Courseid")) ' Course ID
                                updateCmd.Parameters.AddWithValue("@ClassesID", ViewState("classesid")) ' Classes ID

                                ' Execute the update command
                                updateCmd.ExecuteNonQuery()
                            End Using
                        End If
                    End If
                End Using
            Next
        End Using
    End Sub




    Protected Sub btnSaveAndEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            ' Retrieve the DataTable from ViewState
            Dim dt As DataTable = CType(ViewState("StudentTable"), DataTable)
            If dt Is Nothing Then
                msgsave.InnerHtml = "Error: Unable to retrieve student data. Please try again."
                Exit Sub
            End If

            ' Create DataTable with columns for StudentID and Status
            Dim table As New DataTable()
            table.Columns.Add("StudentID", GetType(String))
            table.Columns.Add("Status", GetType(String))

            ' Loop through each row in GridView to get checkbox selections
            For Each row As GridViewRow In StudentGridView.Rows
                Dim studentId As String = dt.Rows(row.RowIndex)("StudentID").ToString()
                Dim chkBox As CheckBox = CType(row.FindControl("AttendanceCheckbox"), CheckBox)
                Dim status As String = If(chkBox IsNot Nothing AndAlso chkBox.Checked, "P", "A")

                ' Add student ID and their attendance status to the table
                table.Rows.Add(studentId, status)
            Next

            ' Insert attendance records if there are rows in the DataTable
            If table.Rows.Count > 0 Then
                Using conn As New SqlConnection(connectionString)
                    conn.Open()

                    ' Flag to check for duplicate attendance
                    Dim isDuplicate As Boolean = False

                    ' Loop through each row in the table to insert attendance data
                    For Each row As DataRow In table.Rows
                        Dim studentId As String = row("StudentID").ToString()
                        Dim status As String = row("Status").ToString()

                        ' Check for duplicate attendance
                        Dim checkSql As String = "SELECT COUNT(*) FROM [dbo].[StudentAtt] " &
                                                 "WHERE [Dated] = @Dated AND [StudentID] = @StudentID AND [CourseID] = @CourseID AND [ClassesID] = @ClassesID"

                        Using cmdCheck As New SqlCommand(checkSql, conn)
                            cmdCheck.Parameters.AddWithValue("@Dated", CDate(txtDt.Text))
                            cmdCheck.Parameters.AddWithValue("@StudentID", studentId)
                            cmdCheck.Parameters.AddWithValue("@CourseID", ViewState("Courseid"))
                            cmdCheck.Parameters.AddWithValue("@ClassesID", ViewState("classesid"))

                            Dim recordCount As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())
                            If recordCount > 0 Then
                                isDuplicate = True
                            Else
                                ' Insert new attendance record
                                Dim sql As String = "INSERT INTO [dbo].[StudentAtt] " &
                                                    "([Dated], [Period], [TeacherID], [StudentID], [Status], [CourseID], [ClassesID]) " &
                                                    "VALUES (@Dated, @Period, @TeacherID, @StudentID, @Status, @CourseID, @ClassesID)"

                                Using cmd As New SqlCommand(sql, conn)
                                    cmd.Parameters.AddWithValue("@Dated", CDate(txtDt.Text))
                                    cmd.Parameters.AddWithValue("@Period", 0)
                                    cmd.Parameters.AddWithValue("@TeacherID", Request.QueryString("u"))
                                    cmd.Parameters.AddWithValue("@StudentID", studentId)
                                    cmd.Parameters.AddWithValue("@Status", status)
                                    cmd.Parameters.AddWithValue("@CourseID", ViewState("Courseid"))
                                    cmd.Parameters.AddWithValue("@ClassesID", ViewState("classesid"))

                                    cmd.ExecuteNonQuery()
                                End Using
                            End If
                        End Using
                    Next

                    ' Send email notifications for "P" (Present) status
                    If Not isDuplicate Then
                        For Each row As DataRow In table.Rows
                            Dim studentId As String = row("StudentID").ToString()
                            Dim status As String = row("Status").ToString()

                            If status = "P" Then
                                Dim emailSql As String = "SELECT Email FROM Student WHERE StudentID = @StudentID"
                                Using cmdEmail As New SqlCommand(emailSql, conn)
                                    cmdEmail.Parameters.AddWithValue("@StudentID", studentId)

                                    Dim email As Object = cmdEmail.ExecuteScalar()
                                    If email IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(email.ToString()) Then
                                        SendEmail(email.ToString())
                                    End If
                                End Using
                            End If
                        Next
                    End If

                    ' Call the updateStatus function
                    updateStatus(table)

                    ' Display message based on result
                    If isDuplicate Then
                        msgsave.InnerHtml = "Attendance has already been recorded. To make changes, please delete the existing entry and then re-enter the information."
                    Else
                        msgsave.InnerHtml = "Attendance saved successfully, and email notifications have been sent."
                    End If

                    ' Reset the form or other UI elements
                    ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "fun();", True)
                    Panel1.Visible = False
                End Using
            End If
        Catch ex As Exception
            msgsave.InnerHtml = "An error occurred. Please try again. " & ex.Message
        End Try
    End Sub



    ' Method to send an email
    Private Sub SendEmail(ByVal recipientEmail As String)
        Try
            ' Configure the SMTP client for Gmail
            Using smtp As New SmtpClient("smtp.gmail.com")
                smtp.Port = 587 ' Use port 587 for TLS
                smtp.Credentials = New NetworkCredential("a75342175@gmail.com", "gnfe qofp dssj rqbo") ' Use your app password here
                smtp.EnableSsl = True ' Enable SSL/TLS connection

                ' Create the email message
                Dim mailMessage As New MailMessage()
                mailMessage.From = New MailAddress("a75342175@gmail.com")
                mailMessage.To.Add(recipientEmail)
                mailMessage.Subject = "Attendance Marked Successfully"
                mailMessage.Body = "प्रिय छात्र, तव उपस्थितिः यथासम्भवं चिह्निता अस्ति।"

                ' Send the email
                smtp.Send(mailMessage)
            End Using
        Catch ex As Exception
            ' Log or handle the exception if email fails
            msgsave.InnerHtml = "Error: Email could not be sent. " & ex.Message
        End Try
    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            ' Initialize the DataTable to store only StudentID
            Dim table As New DataTable()
            table.Columns.Add("StudentID", GetType(String))

            ' Make sure 'dt' is initialized properly (if it's coming from ViewState, get it like so)
            Dim dt As DataTable = CType(ViewState("StudentTable"), DataTable)
            If dt Is Nothing Then
                msgsave.InnerHtml = "Error: Student data not available."
                Exit Sub
            End If

            ' Loop through the GridView rows
            For Each row As GridViewRow In StudentGridView.Rows
                ' Fetch the student ID from the DataTable or GridView
                Dim studentId As String = dt.Rows(row.RowIndex)("StudentID").ToString()

                ' Check if the studentId is not empty
                If Not String.IsNullOrEmpty(studentId) Then
                    ' Add studentId to the table
                    table.Rows.Add(studentId)
                Else
                    ' If studentId is missing, show an error and exit
                    msgsave.InnerHtml = "!Student ID missing."
                    ScriptManager.RegisterStartupScript(Me, Page.GetType(), "Script", "fun();", True)
                    Exit Sub
                End If
            Next

            ' Counter to track the number of deleted records
            Dim deletedCount As Integer = 0

            ' Now, if there are valid rows in the table, proceed with deletion from the database
            If table.Rows.Count > 0 Then
                ' Establish the database connection
                Using conn As New SqlConnection(connectionString)
                    conn.Open()

                    ' Loop through the table and delete attendance records for each student
                    For Each row As DataRow In table.Rows
                        Dim studentId As String = row("StudentID").ToString()

                        ' Define the DELETE query
                        Dim sqlDelete As String = "DELETE FROM [dbo].[StudentAtt] " & _
                                                   "WHERE [Dated] = @Dated " & _
                                                   "AND [TeacherID] = @TeacherID " & _
                                                   "AND [CourseID] = @CourseID " & _
                                                   "AND [ClassesID] = @ClassesID " & _
                                                   "AND [StudentID] = @StudentID"

                        ' Execute the delete query for each student
                        Using cmd As New SqlCommand(sqlDelete, conn)
                            cmd.Parameters.AddWithValue("@Dated", CDate(txtDt.Text)) ' Use the selected date
                            cmd.Parameters.AddWithValue("@TeacherID", Request.QueryString("u")) ' TeacherID
                            cmd.Parameters.AddWithValue("@CourseID", ViewState("Courseid")) ' CourseID
                            cmd.Parameters.AddWithValue("@ClassesID", ViewState("classesid")) ' ClassesID
                            cmd.Parameters.AddWithValue("@StudentID", studentId) ' StudentID

                            ' Execute the DELETE command and count the affected rows
                            deletedCount += cmd.ExecuteNonQuery()
                        End Using
                    Next
                End Using

                ' Check if any records were deleted
                If deletedCount > 0 Then
                    ' Display success message if any record was deleted
                    msgsave.InnerHtml = "Attendance records deleted successfully."
                    Panel1.Visible = False
                Else
                    ' If no records were deleted, display a specific message
                    msgsave.InnerHtml = "here are no attendance entries for the selected date: " & txtDt.Text
                End If
            Else
                ' If no student IDs were found to delete
                msgsave.InnerHtml = "No records to delete."
            End If

            ScriptManager.RegisterStartupScript(Me, Page.GetType(), "Script", "fun();", True)

        Catch ex As Exception
            ' Handle any errors
            msgsave.InnerHtml = "An error occurred. Please try again."
        End Try
    End Sub








    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        Response.Redirect("../Attendance/FacultyAttendance.aspx?s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u"))
    End Sub
End Class
