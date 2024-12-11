Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Net
Partial Class UserPortal_DashboardStu
    Inherits System.Web.UI.Page

    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("uid") = Request.QueryString("u")
            ViewState("inqid") = Request.QueryString("uid")
            ViewState("uid") = Request.QueryString("stuid")
            ViewState("sessionid") = Request.QueryString("s")
            ViewState("AdmissionNo") = Request.QueryString("admissionno")
            getstudentinfo()
            ViewState("sessionid") = ViewState("Sessionid")
            Session("Studentid") = Request.QueryString("stuid")
        End If

    End Sub
    Private Sub getstudentinfo()
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim sql As String = " Select s.*,ec.Course from Student s Join Exam_Course ec on s.Courseid = ec.Courseid   where s.StudentID='" & Request.QueryString("u") & "'"
            cmd.CommandText = sql
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                Dim dt As New DataTable
                sda.Fill(dt)
                If dt.Rows.Count > 0 Then
                    '  lbl.Text = dt.Rows(0)("AdmissionNo").ToString
                    lblStuName.Text = dt.Rows(0)("Student").ToString
                    lblRollNo.Text = dt.Rows(0)("Roll_no").ToString
                    lblProgram.Text = dt.Rows(0)("Course").ToString
                    Label3.Text = dt.Rows(0)("DOB").ToString
                    lblProgram.Text = dt.Rows(0)("course").ToString
                    Label2.Text = dt.Rows(0)("Mobile").ToString
                    Label3.Text = dt.Rows(0)("Email").ToString
                    ViewState("Sessionid") = dt.Rows(0)("Sessionid").ToString
                 
                End If
            End Using
        End Using

    End Sub
    
    Protected Sub btnLogout_Click(sender As Object, e As System.EventArgs) Handles btnLogout.Click
        Response.Redirect("~/LoginFinal.aspx")
    End Sub
    Protected Sub lblbtnApplyForLeave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblbtnApplyForLEave.Click
        Response.Redirect("~/UserPortal/applyForLeave.aspx?stuid=" & Request.QueryString("u") &
                   "&s=" & ViewState("sessionid") &
                   "&admissionno=" & ViewState("AdmissionNo") &
                   "&uid=" & ViewState("uid"))
    End Sub

    Protected Sub btnRevolution_Click(sender As Object, e As System.EventArgs) Handles btnRevolution.Click
        Response.Redirect("~/UserPortal/RevlutionDash.aspx?stuid=" & Request.QueryString("u") & "&s=" & ViewState("Sessionid"))
    End Sub

    Protected Sub btnExamDashboard_Click(sender As Object, e As System.EventArgs) Handles btnExamDashboard.Click
        Response.Redirect("~/UserPortal/ExaminationDash.aspx?stuid=" & Request.QueryString("u") & "&s=" & ViewState("Sessionid"))
    End Sub

    Protected Sub btnHome_Click(sender As Object, e As System.EventArgs) Handles btnHome.Click
        Response.Redirect("~/UserPortal/DashboardStu.aspx")
    End Sub

    Protected Sub btnUpdateProfile_Click(sender As Object, e As System.EventArgs) Handles btnUpdateProfile.Click
        Response.Redirect("~/UserPortal/UpdateProfile.aspx")
    End Sub

    Protected Sub btnAssignmentFile_Click(sender As Object, e As System.EventArgs) Handles btnAssignmentFile.Click
        Response.Redirect("~/UserPortal/AssignmentSec.aspx?stuid=" & Request.QueryString("u") & "&s=" & ViewState("Sessionid"))
    End Sub

    Protected Sub btnQuizzes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuizzes.Click
        Response.Redirect("~/UserPortal/QuizSubjectList.aspx?stuid=" & Request.QueryString("u") & "&s=" & ViewState("Sessionid"))
    End Sub

    Protected Sub btnStudyMaterials_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStudyMaterials.Click
        Response.Redirect("~/UserPortal/StudyMaterialSec.aspx?stuid=" & Request.QueryString("u") & "&s=" & ViewState("Sessionid"))
    End Sub
    Protected Sub btnCertificateDoc_Click(sender As Object, e As System.EventArgs) Handles btnCertificateDoc.Click

    End Sub

    Protected Sub btnjobs_Click(sender As Object, e As System.EventArgs) Handles btnjobs.Click
        Checkregistration()


    End Sub
    Private Sub Checkregistration()
        Dim ds As New DataSet
        Using con As New SqlConnection(constr)
            con.Open()
            Dim cmd1 As New SqlCommand(" Select *  from Pl_StudentRegistration where Studentid = '" & Request.QueryString("u") & "' ", con)
            Dim da As New SqlDataAdapter(cmd1)
            cmd1.Connection = con
            da.Fill(ds)
            Dim i = ds.Tables(0).Rows.Count()
            If (i > 0) Then
                Response.Redirect("~/UserPortal/Activejobs.aspx?u=" & Request.QueryString("u") & "&s=" & ViewState("Sessionid"))
            Else
                Response.Redirect("~/Placement/PlacementRegistration.aspx?u=" & Request.QueryString("u") & "&s=" & ViewState("Sessionid"))
            End If
            con.Close()
        End Using
    End Sub

    Protected Sub lbtngrievence_Click(sender As Object, e As System.EventArgs) Handles lbtngrievence.Click
        Response.Redirect("~/Grievence/Studentgrievencedash.aspx?stuid=" & ViewState("uid") & "&s=" & ViewState("Sessionid"))
    End Sub
End Class
