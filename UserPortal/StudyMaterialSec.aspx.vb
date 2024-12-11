Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient

Partial Class UserPortal_StudyMaterialSec
    Inherits System.Web.UI.Page

    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Get the student ID and session from query string
            ViewState("uid") = Request.QueryString("Stuid")
            Session("Studentid") = Request.QueryString("stuid")
            ViewState("Sessionid") = Request.QueryString("s")

            ' Fetch student information and subjects
            GetStudentInfoAndSubjects()
        End If
    End Sub

    Private Sub GetStudentInfoAndSubjects()
        Using con As New SqlConnection(constr)
            ' First query to get student information including their course and semester
            Dim cmd As New SqlCommand("Select s.*, ec.Course from Student s " &
                                      "Join Exam_Course ec on s.Courseid = ec.Courseid " &
                                      "where s.StudentID = @StudentID", con)
            cmd.Parameters.AddWithValue("@StudentID", ViewState("uid"))

            con.Open()

            Using sdr As SqlDataReader = cmd.ExecuteReader()
                If sdr.Read() Then
                    ' Store relevant student details in ViewState
                    ViewState("Student") = sdr("Student").ToString()
                    ViewState("Rollno") = sdr("Roll_no").ToString()
                    ViewState("Courseid") = sdr("Courseid").ToString()
                    ViewState("Sem") = sdr("Sem").ToString() ' Assuming this column holds semester information

                    ' Fetch subjects for the student based on CourseID and Sem
                    FetchStudentSubjects(ViewState("Courseid").ToString(), Convert.ToInt32(ViewState("Sem")))
                End If
            End Using
        End Using
    End Sub

    Private Sub FetchStudentSubjects(ByVal courseId As String, ByVal sem As Integer)
        Using con As New SqlConnection(constr)
            ' Query to fetch subjects, subject codes, SubjectID, and AcademicYear based on the student's course and semester
            Dim cmd As New SqlCommand("SELECT es.Subject, es.SubjectCode, es.SubjectID, ecs.Academicyear " & _
                                      "FROM Exam_Subject es " & _
                                      "JOIN Exam_Coursesubject ecs ON es.Subjectid = ecs.Subjectid " & _
                                      "WHERE ecs.Courseid = @CourseID AND ecs.Semyear = @Sem", con)
            cmd.Parameters.AddWithValue("@CourseID", courseId)
            cmd.Parameters.AddWithValue("@Sem", sem)

            Using sda As New SqlDataAdapter(cmd)
                Dim dtSubjects As New DataTable()
                sda.Fill(dtSubjects)

                If dtSubjects.Rows.Count > 0 Then
                    ' Bind the subject data to the repeater in the panel
                    rptSubjects.DataSource = dtSubjects
                    rptSubjects.DataBind()
                End If
            End Using
        End Using
    End Sub



    ' Event handlers for navigation buttons
    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        Response.Redirect("~/UserPortal/DashboardStu.aspx?u=" & Session("Studentid") & "&s=" & ViewState("Sessionid"))
    End Sub

    Protected Sub btnHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHome.Click
        Response.Redirect("~/UserPortal/DashboardStu.aspx?u=" & Session("Studentid") & "&s=" & ViewState("Sessionid"))
    End Sub

    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Response.Redirect("~/LoginFinal.aspx")
    End Sub
End Class
