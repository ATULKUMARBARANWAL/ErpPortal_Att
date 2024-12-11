Imports System.Data
Imports System.Data.SqlClient

Partial Class UserPortal_QuizPage
    Inherits System.Web.UI.Page
    Private Const pageSize As Integer = 1 ' Number of questions per page
    Private constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("Studentid") Is Nothing Then
                Response.Redirect("~/LoginFinal.aspx")
            End If
            Dim quizID As Integer = Convert.ToInt32(Session("QuizID"))
            ViewState("CurrentPage") = 1
            LoadQuizDetails(quizID)
            LoadQuestions(quizID, ViewState("CurrentPage"))

            BindQuizNavigation(quizID)
        End If
    End Sub

    Private Sub LoadQuizDetails(ByVal quizID As Integer)
        Dim query As String = "SELECT Name, Duration, TotalPoints FROM Quiz WHERE QuizID = @QuizID"
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@QuizID", quizID)
                con.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        litQuizTitle.Text = reader("Name").ToString()
                        litQuizDuration.Text = reader("Duration").ToString()
                        litQuizTotalPoints.Text = reader("TotalPoints").ToString()
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub LoadQuestions(ByVal quizID As Integer, ByVal page As Integer)
        Dim offset As Integer = (page - 1) * pageSize
        Dim query As String = "WITH QuestionPaging AS (" &
                              "SELECT QuestionID, QuestionText, QuestionType, ImageQuestion, " &
                              "ROW_NUMBER() OVER (ORDER BY QuestionID) AS RowNum " &
                              "FROM Question WHERE QuizID = @QuizID) " &
                              "SELECT QuestionID, QuestionText, QuestionType, ImageQuestion, RowNum " &
                              "FROM QuestionPaging WHERE RowNum BETWEEN @StartRow AND @EndRow"

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@QuizID", quizID)
                cmd.Parameters.AddWithValue("@StartRow", offset + 1)
                cmd.Parameters.AddWithValue("@EndRow", offset + pageSize)
                con.Open()

                Dim questionsTable As New DataTable()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    questionsTable.Load(reader)
                End Using

                ' Bind the question data to the repeater
                rptQuestions.DataSource = questionsTable
                rptQuestions.DataBind()

                ' Set the current question ID to hfCurrentQuestionID
                If questionsTable.Rows.Count > 0 Then
                    hfCurrentQuestionID.Value = questionsTable.Rows(0)("QuestionID").ToString()
                End If
            End Using
        End Using

        UpdateNavigationButtons(page)
    End Sub


    Protected Sub rptQuestions_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim questionID As Integer = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "QuestionID"))
            Dim quizID As Integer = Convert.ToInt32(Session("QuizID")) ' Assuming QuizID is stored in Session
            Dim studentID As Integer = Convert.ToInt32(Session("StudentID")) ' Assuming StudentID is stored in Session

            ' Retrieve saved answer response
            Dim savedResponse As String = GetSavedAnswerResponse(questionID, quizID, studentID)

            ' Store saved response in hidden field
            Dim hfSavedResponse As HiddenField = CType(e.Item.FindControl("hfSavedResponse"), HiddenField)
            hfSavedResponse.Value = savedResponse

            ' Bind answer options
            Dim rptOptions As Repeater = CType(e.Item.FindControl("rptOptions"), Repeater)
            rptOptions.DataSource = GetAnswerOptions(questionID)
            rptOptions.DataBind()
        End If
    End Sub


    Protected Sub rptOptions_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim optionID As String = CType(e.Item.FindControl("hfAnswerID"), HiddenField).Value
            Dim hfSavedResponse As HiddenField = CType(e.Item.Parent.Parent.FindControl("hfSavedResponse"), HiddenField)
            Dim savedResponse As String = hfSavedResponse.Value

            Dim selectedOptions As List(Of String) = savedResponse.Split(","c).ToList()

            Dim rbOption As RadioButton = CType(e.Item.FindControl("rbOption"), RadioButton)
            Dim cbOption As CheckBox = CType(e.Item.FindControl("cbOption"), CheckBox)

            If rbOption IsNot Nothing AndAlso (rbOption.Visible) Then
                rbOption.Checked = selectedOptions.Contains(optionID)
            End If

            If cbOption IsNot Nothing AndAlso (cbOption.Visible) Then
                cbOption.Checked = selectedOptions.Contains(optionID)
            End If
        End If
    End Sub


    Private Function GetSavedAnswerResponse(ByVal questionID As Integer, ByVal quizID As Integer, ByVal studentID As Integer) As String
        Dim savedResponse As String = ""
        Dim query As String = "SELECT AnswerResponse FROM ResponseAnswerOption " &
                              "WHERE QuestionID = @QuestionID AND QuizID = @QuizID AND StudentID = @StudentID"
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@QuestionID", questionID)
                cmd.Parameters.AddWithValue("@QuizID", quizID)
                cmd.Parameters.AddWithValue("@StudentID", studentID)
                con.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot DBNull.Value Then
                    savedResponse = Convert.ToString(result)
                End If
            End Using
        End Using
        Return savedResponse
    End Function

    ' For RadioButton selection
    Protected Sub rbOption_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        ' Get the clicked RadioButton control
        Dim rbOption As RadioButton = CType(sender, RadioButton)

        ' Access the parent RepeaterItem using NamingContainer to find the HiddenField
        Dim repeaterItem As RepeaterItem = CType(rbOption.NamingContainer, RepeaterItem)
        Dim hfAnswerID As HiddenField = CType(repeaterItem.FindControl("hfAnswerID"), HiddenField)

        ' Check if the RadioButton is selected
        If rbOption.Checked Then
            ' Get the AnswerID from the HiddenField
            Dim answerID As String = hfAnswerID.Value

            ' Store the selected AnswerID for the RadioButton in Session
            Session("SelectedRadioAnswerID") = answerID
        End If
    End Sub

    ' For CheckBox selection (multi-select)
    Protected Sub cbOption_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        ' Get the clicked CheckBox control
        Dim cbOption As CheckBox = CType(sender, CheckBox)

        ' Access the parent RepeaterItem using NamingContainer to find the HiddenField
        Dim repeaterItem As RepeaterItem = CType(cbOption.NamingContainer, RepeaterItem)
        Dim hfAnswerID As HiddenField = CType(repeaterItem.FindControl("hfAnswerID"), HiddenField)

        If hfAnswerID IsNot Nothing Then
            Dim answerID As String = hfAnswerID.Value

            ' Retrieve the current stored values from Session
            Dim currentSelected As String = If(Session("SelectedCheckBoxAnswerIDs") IsNot Nothing, Session("SelectedCheckBoxAnswerIDs").ToString(), "")

            If cbOption.Checked Then
                ' Add the AnswerID to the stored list if not already present
                If Not currentSelected.Contains(answerID) Then
                    If currentSelected = "" Then
                        currentSelected = answerID
                    Else
                        currentSelected &= "," & answerID
                    End If
                End If
            Else
                ' Remove the AnswerID from the stored list if unchecked
                If currentSelected.Contains(answerID) Then
                    Dim answerIDs As List(Of String) = currentSelected.Split(","c).ToList()
                    answerIDs.Remove(answerID) ' Remove the unchecked AnswerID
                    currentSelected = String.Join(",", answerIDs)
                End If
            End If

            ' Store the updated list of selected CheckBox IDs in Session
            Session("SelectedCheckBoxAnswerIDs") = currentSelected
        End If
    End Sub




    Private Sub BindQuizNavigation(ByVal quizID As Integer)
        Dim totalQuestions As Integer = GetTotalQuestions(quizID)
        Dim questionNumbers As New List(Of Integer)()
        For i As Integer = 1 To totalQuestions
            questionNumbers.Add(i)
        Next
        quizNavRepeater.DataSource = questionNumbers
        quizNavRepeater.DataBind()
    End Sub

    Private Function GetTotalQuestions(ByVal quizID As Integer) As Integer
        Dim totalQuestions As Integer
        Dim query As String = "SELECT COUNT(*) FROM Question WHERE QuizID = @QuizID"
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@QuizID", quizID)
                con.Open()
                totalQuestions = Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
        Return totalQuestions
    End Function
    Private Function GetAnswerOptions(ByVal questionID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim query As String = "SELECT AnswerOption.AnswerID, AnswerOption.AnswerText, AnswerOption.ImageOption, AnswerOption.QuestionID, Question.QuestionType " &
                              "FROM AnswerOption " &
                              "INNER JOIN Question ON AnswerOption.QuestionID = Question.QuestionID " &
                              "WHERE AnswerOption.QuestionID = @QuestionID"
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@QuestionID", questionID)
                Using sda As New SqlDataAdapter(cmd)
                    sda.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function


    Private Sub UpdateNavigationButtons(ByVal page As Integer)
        btnPrevious.Enabled = (page > 1)
        btnNext.Visible = HasMoreQuestions(page)
        btnSubmit.Visible = Not btnNext.Visible
    End Sub

    Protected Sub btnPrevious_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim quizID As Integer = Convert.ToInt32(Session("QuizID"))
        ViewState("CurrentPage") = CInt(ViewState("CurrentPage")) - 1
        LoadQuestions(quizID, ViewState("CurrentPage"))
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim quizID As Integer = Convert.ToInt32(Session("QuizID"))
        Dim questionID As Integer = Convert.ToInt32(hfCurrentQuestionID.Value)
        Dim studentID As Integer = Convert.ToInt32(Request.QueryString("StudentID"))

        ' Check if either SelectedRadioAnswerID or SelectedCheckBoxAnswerIDs has a value
        If Session("SelectedRadioAnswerID") IsNot Nothing OrElse Session("SelectedCheckBoxAnswerIDs") IsNot Nothing Then
            ' Save the current response
            Dim selectedOptions As String = If(Session("SelectedRadioAnswerID"), Session("SelectedCheckBoxAnswerIDs"))
            SaveOrUpdateResponse(questionID, selectedOptions, studentID, quizID)

            ' Clear session values after saving the response
            Session.Remove("SelectedCheckBoxAnswerIDs")
            Session.Remove("SelectedRadioAnswerID")
        End If

        ' Increment page and load the next question
        ViewState("CurrentPage") = CInt(ViewState("CurrentPage")) + 1
        LoadQuestions(quizID, ViewState("CurrentPage"))
    End Sub


    Private Sub SaveOrUpdateResponse(ByVal questionID As Integer, ByVal selectedOptions As String, ByVal studentID As Integer, ByVal quizID As Integer)
        Dim query As String = "IF EXISTS (SELECT 1 FROM ResponseAnswerOption WHERE QuestionID = @QuestionID AND StudentID = @StudentID) " &
                              "UPDATE ResponseAnswerOption SET AnswerResponse = @AnswerResponse WHERE QuestionID = @QuestionID AND StudentID = @StudentID " &
                              "ELSE INSERT INTO ResponseAnswerOption (QuestionID, AnswerResponse, StudentID, QuizID) VALUES (@QuestionID, @AnswerResponse, @StudentID, @QuizID)"

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@QuestionID", questionID)
                cmd.Parameters.AddWithValue("@AnswerResponse", selectedOptions)
                cmd.Parameters.AddWithValue("@StudentID", studentID)
                cmd.Parameters.AddWithValue("@QuizID", quizID)
                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    'Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim quizID As Integer = Convert.ToInt32(Session("QuizID"))
    '    Dim questionID As Integer = Convert.ToInt32(hfCurrentQuestionID.Value)
    '    Dim studentID As Integer = Convert.ToInt32(Request.QueryString("StudentID"))

    '    ' Check if either SelectedRadioAnswerID or SelectedCheckBoxAnswerIDs has a value
    '    If Session("SelectedRadioAnswerID") IsNot Nothing OrElse Session("SelectedCheckBoxAnswerIDs") IsNot Nothing Then
    '        ' Save the current response
    '        Dim selectedOptions As String = If(Session("SelectedRadioAnswerID"), Session("SelectedCheckBoxAnswerIDs"))
    '        SaveOrUpdateResponse(questionID, selectedOptions, studentID, quizID)

    '        ' Clear session values after saving the response
    '        Session.Remove("SelectedCheckBoxAnswerIDs")
    '        Session.Remove("SelectedRadioAnswerID")
    '    End If
    'End Sub
    Protected Sub quizNavRepeater_ItemCommand(ByVal source As Object, ByVal e As RepeaterCommandEventArgs)
        If e.CommandName = "NavigateToQuestion" Then
            Dim selectedPage As Integer = Convert.ToInt32(e.CommandArgument)
            ViewState("CurrentPage") = selectedPage

            Dim quizID As Integer = Convert.ToInt32(Session("QuizID"))
            LoadQuestions(quizID, selectedPage)
        End If
    End Sub


    Private Function HasMoreQuestions(ByVal page As Integer) As Boolean
        Dim quizID As Integer = Convert.ToInt32(Session("QuizID"))
        Dim query As String = "SELECT COUNT(*) FROM Question WHERE QuizID = @QuizID"
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@QuizID", quizID)
                con.Open()
                Dim totalQuestions As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return (page * pageSize) < totalQuestions
            End Using
        End Using
    End Function

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim quizID As Integer = Convert.ToInt32(Session("QuizID"))
        Dim studentID As Integer = Convert.ToInt32(Request.QueryString("StudentID"))

        Dim questionID As Integer = Convert.ToInt32(hfCurrentQuestionID.Value)

        ' Check if either SelectedRadioAnswerID or SelectedCheckBoxAnswerIDs has a value for the current question
        If Session("SelectedRadioAnswerID") IsNot Nothing OrElse Session("SelectedCheckBoxAnswerIDs") IsNot Nothing Then
            ' Save the current response
            Dim selectedOptions As String = If(Session("SelectedRadioAnswerID"), Session("SelectedCheckBoxAnswerIDs"))
            SaveOrUpdateResponse(questionID, selectedOptions, studentID, quizID)

            ' Clear session values after saving the response
            Session.Remove("SelectedCheckBoxAnswerIDs")
            Session.Remove("SelectedRadioAnswerID")
        End If
        ' Step 1: Retrieve total points and calculate question weightage
        Dim totalPoints As Integer = Convert.ToInt32(litQuizTotalPoints.Text)
        Dim totalQuestions As Integer = GetTotalQuestions(quizID)
        Dim questionWeight As Double = totalPoints / totalQuestions
        Dim score As Double = 0

        ' Step 2: Retrieve all questions and calculate score based on correct answers
        Dim query As String = "SELECT QuestionID, AnswerResponse FROM ResponseAnswerOption WHERE QuizID = @QuizID AND StudentID = @StudentID"
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@QuizID", quizID)
                cmd.Parameters.AddWithValue("@StudentID", studentID)
                con.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim innerQuestionID As Integer = Convert.ToInt32(reader("QuestionID"))
                        Dim userAnswers As String() = reader("AnswerResponse").ToString().Split(","c)

                        ' Step 3: Get correct answers for the question
                        Dim correctAnswers As List(Of String) = GetCorrectAnswers(innerQuestionID)

                        ' Step 4: Compare user's answer with correct answers
                        If correctAnswers.All(Function(answer) userAnswers.Contains(answer)) AndAlso correctAnswers.Count = userAnswers.Length Then
                            ' Add question weight to score if answer is correct
                            score += questionWeight
                        End If
                    End While
                End Using
            End Using
        End Using

        ' Step 5: Update or insert the final score in the Response table
        UpdateQuizScore(quizID, studentID, score)

        ' Clear session values after submission
        Session.Remove("SelectedCheckBoxAnswerIDs")
        Session.Remove("SelectedRadioAnswerID")
        'quizContainer.Visible = False
        Response.Redirect("ThankYouQuiz.aspx")
        
    End Sub

    ' Method to retrieve correct answers for a question
    Private Function GetCorrectAnswers(ByVal questionID As Integer) As List(Of String)
        Dim correctAnswers As New List(Of String)()
        Dim query As String = "SELECT AnswerID FROM AnswerOption WHERE QuestionID = @QuestionID AND IsCorrect = 1"
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@QuestionID", questionID)
                con.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        correctAnswers.Add(reader("AnswerID").ToString())
                    End While
                End Using
            End Using
        End Using
        Return correctAnswers
    End Function

    ' Method to insert or update score in the Response table
    Private Sub UpdateQuizScore(ByVal quizID As Integer, ByVal studentID As Integer, ByVal score As Double)
        Dim query As String = "IF EXISTS (SELECT 1 FROM Response WHERE QuizID = @QuizID AND RespondentID = @StudentID) " &
                              "UPDATE Response SET Score = @Score, ResponseDate = GETDATE() WHERE QuizID = @QuizID AND RespondentID = @StudentID " &
                              "ELSE INSERT INTO Response (QuizID, RespondentID, Score, ResponseDate) VALUES (@QuizID, @StudentID, @Score, GETDATE())"
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@QuizID", quizID)
                cmd.Parameters.AddWithValue("@StudentID", studentID)
                cmd.Parameters.AddWithValue("@Score", score)
                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class