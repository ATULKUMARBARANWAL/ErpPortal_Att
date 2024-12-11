Imports System.Data.SqlClient
Imports System.Data
Imports System.Net.Mail
Imports System.Net


Partial Class UserPortal_applyForLeave
    Inherits System.Web.UI.Page

    ' Declare database connection
    Private conn As SqlConnection
    Private connString As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Initialize the connection
        conn = New SqlConnection(connString)

        If Not IsPostBack Then
            ViewState("uid") = Request.QueryString("u")
            ViewState("inqid") = Request.QueryString("uid")
            ViewState("AdmissionNo") = Request.QueryString("admissionno")
            ViewState("sessionid") = Request.QueryString("s")
            ViewState("sessionid") = ViewState("Sessionid")
            Session("Studentid") = Request.QueryString("stuid")

            ' Set default apply date to current date
            txtApplyDate.Text = DateTime.Now.ToString("yyyy-MM-dd")
            LoadLeaveApplications()

        End If
    End Sub
    Protected Function alreadyApplied() As Boolean
        Dim recordExists As Boolean = False

        Try
            conn.Open()

            Dim admissionNo As String = ViewState("AdmissionNo").ToString()
            Dim studentId As String = Session("Studentid").ToString()
            Dim applyDate As String = txtApplyDate.Text

            Dim checkQuery As String = "SELECT COUNT(*) FROM LeaveApplied WHERE admissionNo = @admissionNo AND studentID = @studentId AND applyDate = @applyDate"
            Using cmd As New SqlCommand(checkQuery, conn)
                cmd.Parameters.AddWithValue("@admissionNo", admissionNo)
                cmd.Parameters.AddWithValue("@studentId", studentId)
                cmd.Parameters.AddWithValue("@applyDate", applyDate)

                recordExists = Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using
        Catch ex As Exception
            lblError.Text = "Error checking leave application: " & ex.Message
            lblError.Visible = True
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try

        Return recordExists
    End Function


    Private Sub LoadLeaveApplications()
        ' Retrieve AdmissionNo and StudentID from ViewState and Session
        Dim admissionNo As String = ViewState("AdmissionNo").ToString()
        Dim studentID As String = Session("Studentid").ToString()

        Dim query As String = "SELECT ROW_NUMBER() OVER (ORDER BY applyDate) AS Sno, " & _
                              "applyDate, " & _
                              "fromDate, " & _
                              "toDate, " & _
                              "leaveType, " & _
                              "status " & _
                              "FROM LeaveApplied " & _
                              "WHERE studentID = @studentID AND admissionNo = @admissionNo"

        Using conn As New SqlConnection(connString)
            Using cmd As New SqlCommand(query, conn)
                ' Add parameters to avoid SQL injection
                cmd.Parameters.AddWithValue("@studentID", studentID)
                cmd.Parameters.AddWithValue("@admissionNo", admissionNo)

                ' Execute query and bind results to GridView
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                Dim dt As New DataTable()
                dt.Load(reader)

                gvLeaveApplications.DataSource = dt
                gvLeaveApplications.DataBind()
            End Using
        End Using
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        If Not Page.IsPostBack Then
            Exit Sub
        End If
        If alreadyApplied() Then
            lblError.Text = "You have already applied for leave on this date."
            lblError.Visible = True
            btnSave.Enabled = False ' Disable the button
            Exit Sub
        End If

        ' Retrieve form values
        Dim applyDate As String = txtApplyDate.Text
        Dim fromDate As String = txtFromDate.Text
        Dim toDate As String = txtToDate.Text
        Dim leaveType As String = ddlLeaveType.SelectedValue
        Dim reason As String = txtReason.Text
        Dim fileName As String = String.Empty
        Dim admissionNo As String = ViewState("AdmissionNo").ToString()
        Dim studentId As String = Session("Studentid").ToString()
        Dim studentEmail As String = ""

        ' Database variables
        Dim courseID As Integer = 0
        Dim classesID As Integer = 0
        Dim sessionID As Integer = 0

        ' Check if a file is uploaded
        If fileUploadDocument.HasFile Then
            Try
                Dim filePath As String = Server.MapPath("Uploads/")
                If Not IO.Directory.Exists(filePath) Then
                    IO.Directory.CreateDirectory(filePath)
                End If
                Dim sanitizedDate As String = applyDate.Replace("/", "-")
                fileName = admissionNo & "_" & sanitizedDate & IO.Path.GetExtension(fileUploadDocument.FileName)
                fileUploadDocument.SaveAs(IO.Path.Combine(filePath, fileName))
            Catch ex As Exception
                lblError.Text = "Error uploading file: " & ex.Message
                lblError.Visible = True
                Exit Sub
            End Try
        End If

        Try
            conn.Open()

            ' Query to get courseID, classesID, sessionID, and email
            Dim query As String = "SELECT courseID, classesID, sessionID, Email FROM Student WHERE admissionNo = @admissionNo AND studentID = @studentId"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@admissionNo", admissionNo)
                cmd.Parameters.AddWithValue("@studentId", studentId)

                Dim reader As SqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    courseID = Convert.ToInt32(reader("courseID"))
                    classesID = Convert.ToInt32(reader("classesID"))
                    sessionID = Convert.ToInt32(reader("sessionID"))
                    studentEmail = reader("Email").ToString()
                Else
                    lblError.Text = "Student record not found."
                    lblError.Visible = True
                    Exit Sub
                End If
                reader.Close()
            End Using

            ' Insert data into the LeaveApplied table
            Dim insertQuery As String = "INSERT INTO LeaveApplied (courseID, classesID, sessionID, admissionNo, studentID, applyDate, fromDate, toDate, leaveType, reason, fileName, status) " & _
                                        "VALUES (@courseID, @classesID, @sessionID, @admissionNo, @studentID, @applyDate, @fromDate, @toDate, @leaveType, @reason, @fileName, 'Pending')"
            Using insertCmd As New SqlCommand(insertQuery, conn)
                insertCmd.Parameters.AddWithValue("@courseID", courseID)
                insertCmd.Parameters.AddWithValue("@classesID", classesID)
                insertCmd.Parameters.AddWithValue("@sessionID", sessionID)
                insertCmd.Parameters.AddWithValue("@admissionNo", admissionNo)
                insertCmd.Parameters.AddWithValue("@studentID", studentId)
                insertCmd.Parameters.AddWithValue("@applyDate", applyDate)
                insertCmd.Parameters.AddWithValue("@fromDate", fromDate)
                insertCmd.Parameters.AddWithValue("@toDate", toDate)
                insertCmd.Parameters.AddWithValue("@leaveType", leaveType)
                insertCmd.Parameters.AddWithValue("@reason", reason)
                insertCmd.Parameters.AddWithValue("@fileName", fileName)
                insertCmd.ExecuteNonQuery()
            End Using

            ' Send Email to User
            SendLeaveRequestEmail(studentEmail, admissionNo, studentId, applyDate, fromDate, toDate, leaveType)

            lblSuccess.Text = "Leave applied successfully!"
            lblSuccess.Visible = True
        Catch ex As Exception
            lblError.Text = "Error processing request: " & ex.Message
            lblError.Visible = True
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try

        LoadLeaveApplications()
    End Sub

    ' Method to Send Email
    Private Sub SendLeaveRequestEmail(ByVal email As String, ByVal admissionNo As String, ByVal studentId As String, ByVal applyDate As String, ByVal fromDate As String, ByVal toDate As String, ByVal leaveType As String)
        Try
            ' Retrieve guardian's email from the Student table
            Dim guardianEmail As String = GetGuardianEmail(studentId, admissionNo) ' Call method to fetch guardian's email

            ' Initialize the SMTP client
            Dim smtpClient As New SmtpClient("smtp.gmail.com") ' Replace with your SMTP server
            smtpClient.Port = 587 ' Update port if needed
            smtpClient.Credentials = New NetworkCredential("a75342175@gmail.com", "gnfe qofp dssj rqbo") ' Replace with your email and password
            smtpClient.EnableSsl = True

            ' Create and send email to the student
            Dim studentMailMessage As New MailMessage()
            studentMailMessage.From = New MailAddress("a75342175@gmail.com", "Student ERP") ' Replace with your sender email
            studentMailMessage.To.Add(email) ' Add student's email
            studentMailMessage.Subject = "Leave Request Submitted"
            studentMailMessage.Body = "Dear Student,<br/><br/>Your leave request has been submitted successfully.<br/><br/>" & _
                                      "<b>Admission No:</b> " & admissionNo & "<br/>" & _
                                      "<b>Student ID:</b> " & studentId & "<br/>" & _
                                      "<b>Leave Type:</b> " & leaveType & "<br/>" & _
                                      "<b>Leave Dates:</b> " & fromDate & " to " & toDate & "<br/><br/>" & _
                                      "Thank you,<br/>University Team"
            studentMailMessage.IsBodyHtml = True
            smtpClient.Send(studentMailMessage)

            ' If guardian's email is available, send a separate email to the guardian
            If Not String.IsNullOrEmpty(guardianEmail) Then
                Dim guardianMailMessage As New MailMessage()
                guardianMailMessage.From = New MailAddress("a75342175@gmail.com", "Student ERP") ' Replace with your sender email
                guardianMailMessage.To.Add(guardianEmail) ' Add guardian's email
                guardianMailMessage.Subject = "Leave Request Notification"
                guardianMailMessage.Body = "Dear Guardian,<br/><br/>Please be informed that the student with Admission No: " & admissionNo &
                                           " and Student ID: " & studentId & " has submitted a leave request.<br/><br/>" & _
                                           "<b>Leave Type:</b> " & leaveType & "<br/>" & _
                                           "<b>Leave Dates:</b> " & fromDate & " to " & toDate & "<br/><br/>" & _
                                           "Thank you,<br/>University Team"
                guardianMailMessage.IsBodyHtml = True
                smtpClient.Send(guardianMailMessage)
            End If

        Catch ex As Exception
            lblError.Text = "Error sending email: " & ex.Message
            lblError.Visible = True
        End Try
    End Sub


    ' Method to retrieve the guardian's email from the Student table
    Private Function GetGuardianEmail(ByVal studentId As String, ByVal admissionNo As String) As String
        Dim guardianEmail As String = String.Empty

        ' Query to retrieve guardian's email from Student table
        Dim query As String = "SELECT GEmail FROM Student WHERE StudentID = @studentId And admissionNo=@admissionNo "
        Using conn As New SqlConnection(connString)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@studentId", studentId)
                cmd.Parameters.AddWithValue("@admissionNo", admissionNo)
                conn.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot DBNull.Value Then
                    guardianEmail = result.ToString()
                End If
            End Using
        End Using

        Return guardianEmail
    End Function



End Class