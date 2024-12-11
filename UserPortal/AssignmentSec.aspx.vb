Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO

Partial Class UserPortal_AssignmentSec
    Inherits System.Web.UI.Page

    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("uid") = Request.QueryString("Stuid")
            Session("Studentid") = Request.QueryString("stuid")
            ViewState("Sessionid") = Request.QueryString("s")
            getstudentinfo()
            BindAssignment()
        End If
    End Sub
    Private Sub getstudentinfo()
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim sql As String = " Select s.*,ec.Course from Student s Join Exam_Course ec on s.Courseid = ec.Courseid   where s.StudentID='" & ViewState("uid") & "'"
            cmd.CommandText = sql
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                Dim dt As New DataTable
                sda.Fill(dt)
                If dt.Rows.Count > 0 Then
                    '  lbl.Text = dt.Rows(0)("AdmissionNo").ToString
                    ViewState("Sudenet") = dt.Rows(0)("Student").ToString
                    ViewState("Rollno") = dt.Rows(0)("Roll_no").ToString
                    ViewState("Course") = dt.Rows(0)("Course").ToString
                    ViewState("DOB") = dt.Rows(0)("DOB").ToString
                    ViewState("Courseid") = dt.Rows(0)("Courseid").ToString
                    ViewState("Course") = dt.Rows(0)("course").ToString
                    ViewState("Mobile") = dt.Rows(0)("Mobile").ToString
                    ViewState("Email") = dt.Rows(0)("Email").ToString

                End If
            End Using
        End Using

    End Sub
    Private Sub BindAssignment()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(" select distinct au.uploadassignmentid,au.assignmentno,au.Doc,es.subject,au.Path,Convert(varchar,au.Submissiondate,103) as 'Submissiondate',at.courseid,ass.Assign_subjectid from  Assign_total at " & _
"join Assign_Subject ass on at.Totalassignmentid = ass.TotalAssignID " & _
"join Assign_upload au on ass.Assign_subjectid = au.Assign_subjectid " & _
"join Exam_subject es on ass.Subjectid = es.subjectid " & _
" where at.Courseid = '" & ViewState("Courseid") & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        GridAssignment.DataSource = dt
                        GridAssignment.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub
    Protected Sub DownloadFile(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim filePath As String = CType(sender, LinkButton).CommandArgument

            If Not String.IsNullOrEmpty(filePath) Then
                Dim ext As String = System.IO.Path.GetExtension(Path.GetFileName(filePath))
                Dim type As String = ""
                If ext IsNot Nothing Then
                    Select Case ext.ToLower()
                        Case ".htm", ".html"
                            type = "text/HTML"
                            Exit Select

                        Case ".txt"
                            type = "text/plain"
                            Exit Select

                        Case ".doc", ".rtf"
                            type = "Application/msword"
                        Case ".pdf"
                            type = "application/pdf"

                        Case ".xls", ".xlsx"
                            type = "application/vnd.ms-excel"


                        Case ".jpg", ".jpeg", ".png"
                            type = "Application/paint"
                            Exit Select
                    End Select
                End If

                Response.ContentType = type
                Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
                Response.WriteFile(Server.MapPath(filePath))
                ' Response.TransmitFile(Server.MapPath(filePath))
                Response.End()
            Else
                SaralMsg.Messagebx.Alert(Me, "Attachment Not Available")

            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub backbotton_Click(sender As Object, e As System.EventArgs) Handles backbotton.Click
        Response.Redirect("~/UserPortal/DashboardStu.aspx?u=" & Session("Studentid") & "&s=" & ViewState("Sessionid"))
    End Sub
    Protected Sub btnHome_Click(sender As Object, e As System.EventArgs) Handles btnHome.Click
        Response.Redirect("~/UserPortal/DashboardStu.aspx")
    End Sub
    Protected Sub btnLogout_Click(sender As Object, e As System.EventArgs) Handles btnLogout.Click
        Response.Redirect("~/LoginFinal.aspx")
    End Sub
    Private Sub Savedoc()
        Dim filename As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Examinationjune/ExamAssignmentdoc\" & filename))
        Dim contentType As String = FileUpload1.PostedFile.ContentType
        Using fs As Stream = FileUpload1.PostedFile.InputStream
            Using br As New BinaryReader(fs)
                Dim bytes As Byte() = br.ReadBytes(DirectCast(fs.Length, Long))
                Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
                Using con As New SqlConnection(constr)
                    Dim query As String = "insert into Assign_StuUpload(Uploadassignmentid,Dated, Studentid,Path)values ('" & ViewState("uploadassignmentid") & "','" & Date.Now.Date & "', '" & Request.QueryString("Stuid") & "','ExamAssignmentdoc/" & filename & "')"
                    Using cmd As New SqlCommand(query)
                        cmd.Connection = con
                        con.Open()
                        cmd.ExecuteNonQuery()
                        con.Close()

                        Dim message As String = "Successfully Upload"
                        Dim script As String = "window.onload=function(){alert('"
                        script &= message
                        script &= "');"


                        script &= "; }"
                        ClientScript.RegisterStartupScript(Me.GetType(), "SuccessMessage", script, True)

                    End Using
                End Using
            End Using
        End Using
        Response.Redirect(Request.Url.AbsoluteUri)
        'GridAssignments()
    End Sub
    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Savedoc()
    End Sub

    Protected Sub GridAssignment_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridAssignment.RowCommand
        If e.CommandName = "OpenPopup" Then
            Dim dataValue As String = e.CommandArgument.ToString()
            ViewState("uploadassignmentid") = dataValue
            ' Call JavaScript function to open the popup
            checkassignment()
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OpenModal", "OpenBootstrapModal('" & dataValue & "');", True)
        End If
        If e.CommandName = "viewassignment" Then
            Dim dataValue As String = e.CommandArgument.ToString()
            ViewState("uploadassignmentid") = dataValue
            ' Call JavaScript function to open the popup
            Response.Redirect("~/UserPortal/Viewassignment.aspx?rid=" & ViewState("uploadassignmentid"))
        End If
    End Sub

    Protected Sub GridAssignment_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridAssignment.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Attach a JavaScript function to open the modal when a grid row is clicked
            e.Row.Attributes("onclick") = "openModal('" & e.Row.Cells(2).Text & "')"
            'ViewState("uploadassignmentid") = e.Row.Cells(2).Text
            e.Row.Style("cursor") = "pointer"
        End If
    End Sub
    Private Sub checkassignment()

        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim sql As String = " Select s.* from Assign_StuUpload s   where s.Uploadassignmentid='" & ViewState("uploadassignmentid") & "' and s.Studentid = '" & Request.QueryString("Stuid") & "' "
            cmd.CommandText = sql
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                Dim dt As New DataTable
                sda.Fill(dt)
                If dt.Rows.Count > 0 Then
                    SaralMsg.Messagebx.Alert(Me, "Assignment Uploaded Already ")
                Else
                    Dim dataValue As String = ViewState("uploadassignmentid")
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OpenModal", "OpenBootstrapModal('" & dataValue & "');", True)
                End If
            End Using
        End Using
    End Sub
End Class
