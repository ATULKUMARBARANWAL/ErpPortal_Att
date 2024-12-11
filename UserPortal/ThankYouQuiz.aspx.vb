
Partial Class UserPortal_ThankYouQuiz
    Inherits System.Web.UI.Page
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("QuizzesList.aspx?SubjectID=" & Session("SubjectID"))
    End Sub
End Class
