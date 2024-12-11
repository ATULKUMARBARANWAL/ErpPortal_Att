Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Partial Class Examinationjune_Showstudents
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblAcademicyear.Text = Request.QueryString("acyr")

            BindGridstudent()
        End If
    End Sub

    Private Sub BindGridstudent()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "select * from Student where EnrollmentNo is not null and RegistrationApproved='1' and AdmissionApproved='1' and CourseID='" & Request.QueryString("cid") & "' and sessionid='" & Request.QueryString("s") & "'"

                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        grdstudents.DataSource = dt
                        grdstudents.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        Response.Redirect("Dashboard.aspx?s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u"))
    End Sub
End Class
