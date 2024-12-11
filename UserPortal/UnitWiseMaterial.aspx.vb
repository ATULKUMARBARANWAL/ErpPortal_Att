Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class UserPortal_UnitWiseMaterial
    Inherits System.Web.UI.Page

    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Retrieve query string parameters
            If Request.QueryString("cid") IsNot Nothing AndAlso _
               Request.QueryString("subjectid") IsNot Nothing AndAlso _
               Request.QueryString("acyr") IsNot Nothing Then

                BindGridsubject(Request.QueryString("cid"), Request.QueryString("subjectid"), Request.QueryString("acyr"))
            End If
        End If
    End Sub

    ' Binds units to the Repeater based on selected course, subject, and academic year
    Private Sub BindGridsubject(ByVal courseId As String, ByVal subjectId As String, ByVal academicYear As String)
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                ' SQL query to fetch the required data
                Dim query As String = "SELECT Subunitid, UnitName, Description FROM Exam_SubjectUnit " & _
                                      "WHERE Coursesubid IN (SELECT Coursesubid FROM Exam_Coursesubject " & _
                                      "WHERE Courseid = @CourseID AND Subjectid = @SubjectID AND Academicyear = @AcademicYear)"

                cmd.CommandText = query
                cmd.Parameters.AddWithValue("@CourseID", courseId)
                cmd.Parameters.AddWithValue("@SubjectID", subjectId)
                cmd.Parameters.AddWithValue("@AcademicYear", academicYear)

                cmd.Connection = con

                ' Fill the data into the DataTable
                Using sda As New SqlDataAdapter(cmd)
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        ' Bind the data to the Repeater
                        rptUnits.DataSource = dt
                        rptUnits.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    ' Handles the click event of the attachment LinkButton
    Protected Sub btnAttachment_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Retrieve the Subunitid from the CommandArgument and redirect to StudyMaterials.aspx
        Dim subunitid As String = CType(sender, LinkButton).CommandArgument
        Response.Redirect("~/UserPortal/StudyMaterials.aspx?suid=" & subunitid)
    End Sub

    ' Handle other button events such as Home and Logout
    Protected Sub btnHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHome.Click
        Response.Redirect("~/UserPortal/DashboardStu.aspx?u=" & Session("Studentid") & "&s=" & ViewState("Sessionid"))
    End Sub

    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Response.Redirect("~/LoginFinal.aspx")
    End Sub
End Class
