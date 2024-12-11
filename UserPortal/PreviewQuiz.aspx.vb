Imports System.Data.SqlClient
Imports System.Data
Partial Class UserPortal_PreviewQuiz
    Inherits System.Web.UI.Page
    Dim conStr As String = "Data Source=(local);Initial Catalog=sample;Integrated Security=True"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DisplayQuestions(Request.QueryString("PreviewQuizID"))
        End If
    End Sub
    Private Sub DisplayQuestions(ByVal quizID As Integer)
        Using con As New SqlConnection(conStr)
            con.Open()
            Dim cmd As New SqlCommand("SELECT * FROM Question WHERE QuizID = @QuizID", con)
            cmd.Parameters.AddWithValue("@QuizID", quizID)
            Dim questionsTable As New DataTable()
            Using reader As SqlDataReader = cmd.ExecuteReader()
                questionsTable.Load(reader)
            End Using

            rptQuestions.DataSource = questionsTable
            rptQuestions.DataBind()
        End Using
    End Sub

    Protected Sub rptQuestions_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim questionID As Integer = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "QuestionID"))
            Dim rptOptions As Repeater = CType(e.Item.FindControl("rptOptions"), Repeater)

            rptOptions.DataSource = GetAnswerOptions(questionID)
            rptOptions.DataBind()
        End If
    End Sub

    Private Function GetAnswerOptions(ByVal questionID As Integer) As DataTable
        Using con As New SqlConnection(conStr)
            con.Open()
            Dim cmd As New SqlCommand("SELECT * FROM AnswerOption WHERE QuestionID = @QuestionID", con)
            cmd.Parameters.AddWithValue("@QuestionID", questionID)
            Dim optionsTable As New DataTable()
            Using reader As SqlDataReader = cmd.ExecuteReader()
                optionsTable.Load(reader)
            End Using
            Return optionsTable
        End Using
    End Function

    Protected Function GetRomanNumber(ByVal number As Integer) As String
        Dim romans As String() = {"I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X"}
        If number > 0 AndAlso number <= romans.Length Then
            Return romans(number - 1)
        Else
            Return number.ToString()
        End If
    End Function
End Class
