Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class UserPortal_QuizzesList
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Get the student ID and session from query string
            ViewState("uid") = Request.QueryString("Stuid")
            Session("Stuid") = Request.QueryString("Stuid")
            ViewState("Sessionid") = Request.QueryString("s")
            ViewState("SubjectID") = Request.QueryString("SubjectID")
            FetchQuizzesForSubject()
        End If
    End Sub

    Private Sub FetchQuizzesForSubject()
        Using con As New SqlConnection(constr)
            ' Query to get quizzes and their question counts
            Dim cmd As New SqlCommand("SELECT q.QuizID, q.Name, q.Description, q.Duration, q.TotalPoints, (SELECT COUNT(*) FROM Question WHERE QuizID = q.QuizID) AS QuestionCount FROM Quiz q WHERE q.SubjectID = @SubjectID", con)
            cmd.Parameters.AddWithValue("@SubjectID", Request.QueryString("SubjectID"))

        Using sda As New SqlDataAdapter(cmd)
            Dim dtQuizzes As New DataTable()
            sda.Fill(dtQuizzes)

            If dtQuizzes.Rows.Count > 0 Then
                        ' Bind quizzes data to the repeater
                rptQuizzes.DataSource = dtQuizzes
                rptQuizzes.DataBind()
                    End If
                End Using
    End Using
    End Sub


    Protected Function HasPreviousAttempt(ByVal quizID As String) As Boolean
        Dim studentID As String = Session("Studentid")
        If String.IsNullOrEmpty(studentID) Then
            Throw New Exception("Student ID is not available.")
        End If

        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand("SELECT COUNT(1) FROM Response WHERE QuizID = @QuizID AND RespondentID = @StudentID", con)
            cmd.Parameters.AddWithValue("@QuizID", quizID)
            cmd.Parameters.AddWithValue("@StudentID", studentID)
            con.Open()
            Dim hasAttempt As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            Return hasAttempt > 0
        End Using
    End Function


    Protected Sub StartQuiz_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim quizID As String = btn.CommandArgument

        ' Store SubjectID and QuizID in the session
        Session("SubjectID") = ViewState("SubjectID")
        Session("QuizID") = quizID

        ' Redirect to the quiz page
        Response.Redirect("QuizPage.aspx?Studentid=" & Session("Studentid"))
    End Sub

    Protected Sub PreviewQuiz_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim quizID As String = btn.CommandArgument

        ' Store QuizID in the session for preview purposes
        Session("PreviewQuizID") = quizID
        Dim subjectID As String = Session("SubjectID")

        ' Redirect to PreviewQuiz.aspx with StudentID, SubjectID, and PreviewQuizID in the query string
        Response.Redirect("PreviewQuiz.aspx?Studentid=" & Session("Studentid") & "&SubjectID=" & subjectID & "&PreviewQuizID=" & quizID)
    End Sub

    Protected Sub btnHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHome.Click
        Response.Redirect("~/UserPortal/DashboardStu.aspx?u=" & ViewState("uid") & "&s=" & ViewState("Sessionid"))
    End Sub

    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Response.Redirect("~/LoginFinal.aspx")
    End Sub
End Class
