Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Partial Class UserPortal_Activejobs
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("Sessionid") = Request.QueryString("s")
            ViewState("userid") = Request.QueryString("u")
            ViewState("ayid") = Request.QueryString("ayid")
            ViewState("Stuid") = Request.QueryString("u")
            ViewState("admisnno") = Request.QueryString("admisnno")
            Session("admissionno") = ViewState("admisnno")
            BindGrid()
        End If
    End Sub
    Private Sub BindGrid()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select distinct pc.CompanyName,ce.PlEligibilityId,jp.* from Pl_JobPosting jp inner join PlCompany pc on pc.Companyid=jp.CompanyId inner join Pl_CourseEligibilty ce on ce.JobPostId=jp.JobPostId where jp.isactive=1 and jp.SessionId='" & ViewState("Sessionid") & "' and ce.CourseId=(Select CourseId from Student WHere StudentID='" & ViewState("Stuid") & "') and semyear=(Select Sem from Student WHere StudentID='" & ViewState("Stuid") & "')")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd

                    Dim dt As New DataTable()
                    sda.Fill(dt)
                    Grdactivejob.DataSource = dt
                    Grdactivejob.DataBind()
                End Using
            End Using
        End Using
    End Sub

    Protected Sub ddlacademicyear_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlacademicyear.SelectedIndexChanged
      If ddlacademicyear.SelectedItem.Text = "College" Then

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select distinct pc.CompanyName,ce.PlEligibilityId,jp.* from Pl_JobPosting jp inner join PlCompany pc on pc.Companyid=jp.CompanyId inner join Pl_CourseEligibilty ce on ce.JobPostId=jp.JobPostId where jp.isactive=1 and jp.SessionId='" & ViewState("Sessionid") & "' and ce.CourseId=(Select CourseId from Student WHere StudentID='" & ViewState("Stuid") & "') and semyear=(Select Sem from Student WHere StudentID='" & ViewState("Stuid") & "')")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Dim dt As New DataTable()
                        sda.Fill(dt)
                        Grdactivejob.DataSource = dt
                        Grdactivejob.DataBind()
                    End Using
                End Using
            End Using


        ElseIf ddlacademicyear.SelectedItem.Text = "Alumni" Then
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select AI.*, AI.JobID as JobPostID, AI.JobID as CompanyID, AI.JobID as PlEligibilityId, AI.Company as CompanyName, AI.Designation as Jobprofile, AI.Location as VacancyLocation from AI_Jobs AI")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Dim dt As New DataTable()
                        sda.Fill(dt)
                        Grdactivejob.DataSource = dt
                        Grdactivejob.DataBind()
                    End Using
                End Using
            End Using


        End If
    End Sub

    Protected Sub Grdactivejob_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grdactivejob.RowCommand
        If e.CommandName = "Apply" Then
            Grdactivejob.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = Grdactivejob.Rows(rowIndex)
            ViewState("link") = row.Cells(7).Text
            Dim link As String = ViewState("link")

            ' Check if the link starts with "http://" or "https://"
            If link.StartsWith("http://") OrElse link.StartsWith("https://") Then
                ' If it does, redirect directly to the link
                Response.Redirect(link)
            Else
                ' Otherwise, construct the full URL and redirect
                Dim fullLink As String = "http://" & link ' You can change "http://" to "https://" if needed
                Response.Redirect(fullLink)
            End If
        End If
    End Sub

    Protected Sub backbotton_Click(sender As Object, e As System.EventArgs) Handles backbotton.Click
        Response.Redirect("~/UserPortal/DashboardStu.aspx?u=" & Session("Studentid") & "&s=" & ViewState("Sessionid"))
    End Sub
End Class
