Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Partial Class Examinationjune_UploadAssignments
    Inherits System.Web.UI.Page

    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("Academicyear") = Request.QueryString("acyr")
            ViewState("Sessionid") = Request.QueryString("s")
            ViewState("courseid") = Request.QueryString("Courseid")
            ViewState("Userid") = Request.QueryString("u")
            ViewState("ayid") = Request.QueryString("ay")
            ViewState("Subject") = Request.QueryString("Subject")
            ViewState("UAI") = Request.QueryString("Uploadassignmentid")
            lblAcademicyear.Text = Request.QueryString("acyr")
            RepeaterBinddoc()
            getcourseinfo()
            getsemoryear()
            getsection()
            getsubjectinfo()
        End If
    End Sub

    Private Sub getsection()
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim sql As String = "Select Code From CLasses where ClassesID='" & ViewState("Sessionid") & "' "
            cmd.CommandText = sql
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                Dim dt As New DataTable
                sda.Fill(dt)
                If dt.Rows.Count > 0 Then
                    '  lbl.Text = dt.Rows(0)("AdmissionNo").ToString
                    lblSection.Text = dt.Rows(0)("Code").ToString

                End If
            End Using
        End Using
    End Sub

    Private Sub getsemoryear()
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim sql As String = " Select SemYear from Assign_Total where Courseid='" & Request.QueryString("Courseid") & "' "
            cmd.CommandText = sql
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                Dim dt As New DataTable
                sda.Fill(dt)
                If dt.Rows.Count > 0 Then
                    '  lbl.Text = dt.Rows(0)("AdmissionNo").ToString
                    lblSemYr.Text = dt.Rows(0)("SemYear").ToString

                End If
            End Using
        End Using
    End Sub

    Private Sub getcourseinfo()
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim sql As String = "Select C.Course,C.Coursecode from Exam_Course C where CourseID='" & Request.QueryString("Courseid") & "'"
            cmd.CommandText = sql
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                Dim dt As New DataTable
                sda.Fill(dt)
                If dt.Rows.Count > 0 Then
                    '  lbl.Text = dt.Rows(0)("AdmissionNo").ToString
                    lblProgramName.Text = dt.Rows(0)("Coursecode").ToString

                End If
            End Using
        End Using

    End Sub

    Private Sub getsubjectinfo()
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim sql As String = "Select Subject from Exam_Subject where SubjectID='" & Request.QueryString("Subject") & "'"
            cmd.CommandText = sql
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                Dim dt As New DataTable
                sda.Fill(dt)
                If dt.Rows.Count > 0 Then
                    '  lbl.Text = dt.Rows(0)("AdmissionNo").ToString
                    LblSubjectNAme.Text = dt.Rows(0)("Subject").ToString

                End If
            End Using
        End Using

    End Sub

    Protected Sub lbtnDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnDoc.Click
        lbtnDoc.Attributes.Add("class", "nav-item nav-link active")
        lbtnDoc.BackColor = Drawing.Color.FromArgb(30, 208, 133)
        lbtnDoc.ForeColor = Drawing.Color.White

        paneldoc.Visible = True

        lblheadingpopup.Text = "Upload Documents"
        lbldocname.Text = "Documents :"
        RepeaterBinddoc()
    End Sub

    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        ViewState("Courseid") = Session("Courseid")
        Session("ExamName") = Session("Examname")
        Session("Course") = Session("Course")
        Session("Semyear") = Session("Semyear")
        Response.Redirect("AssignmentList.aspx?rid=" & Session("Courseid") & "&acyr=" & ViewState("Academicyear") & "&s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u") & "&ay=" & ViewState("ayid"))
    End Sub

    Private Sub RepeaterBinddoc()

        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand("select * from Assign_Upload where Uploadassignmentid = '" & Request.QueryString("Uploadassignmentid") & "' and  Academicyear='" & Request.QueryString("acyr") & "' and Assignment='Document'", con)
            Dim sda As New SqlDataAdapter(cmd)
            Dim ds As New DataSet
            sda.Fill(ds)
            Repeater2.DataSource = ds
            Repeater2.DataBind()
        End Using

    End Sub

    

    Protected Sub DownloadFile(ByVal sender As Object, ByVal e As EventArgs)

        Dim filePath As String = (TryCast(sender, LinkButton)).CommandArgument
        Response.Clear()
        Response.Buffer = True
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = contentType
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & Path.GetFileName(filePath))
        Response.WriteFile(filePath)
        Response.Flush()
        Response.End()
    End Sub

    Protected Sub DeleteFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim filePath As String = (TryCast(sender, LinkButton)).CommandArgument

        Using con As New SqlConnection(constr)

            con.Open()
            Dim cmd As SqlCommand = New SqlCommand("delete FROM Assign_Upload where UploadassignmentID='" & Request.QueryString("Uploadassignmentid") & "'", con)
            cmd.Connection = con
            cmd.ExecuteNonQuery()
            con.Close()
        End Using


        Dim file As FileInfo = New FileInfo(filePath)
        If file.Exists Then
            file.Delete()
        End If

        Response.Redirect(Request.Url.AbsoluteUri)
    End Sub

    Private Sub Savedoc()
        Dim filename As String = Path.GetFileName(uploadfile.PostedFile.FileName)
        uploadfile.PostedFile.SaveAs(Server.MapPath("ExamAssignmentdoc/" & filename))
        Dim contentType As String = uploadfile.PostedFile.ContentType
        Using fs As Stream = uploadfile.PostedFile.InputStream
            Using br As New BinaryReader(fs)
                Dim bytes As Byte() = br.ReadBytes(DirectCast(fs.Length, Long))
                Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
                Using con As New SqlConnection(constr)
                    Dim query As String = "insert into Assign_Upload(Dated, TotalAssignmentID, Assignment,Doc,Path, Academicyear, Remark,Userid)values ('" & Date.Now.Date & "', '" & Request.QueryString("courseid") & "','Document','" & txttopic.Text & "','ExamAssignmentdoc/" & filename & "','" & Request.QueryString("acyr") & "',null,'" & Request.QueryString("u") & "')"
                    Using cmd As New SqlCommand(query)
                        cmd.Connection = con
                        'cmd.Parameters.AddWithValue("@Dated", Date.Now)
                        'cmd.Parameters.AddWithValue("@TotalAssignmentID", Request.QueryString("taid"))
                        'cmd.Parameters.AddWithValue("@Assignment", ViewState("Assignment"))
                        'cmd.Parameters.AddWithValue("@Doc", txttopic.Text)
                        'cmd.Parameters.AddWithValue("@Userid", Request.QueryString("u"))
                        'cmd.Parameters.AddWithValue("@Remark", "")
                        'cmd.Parameters.AddWithValue("@Academicyear", Request.QueryString("acyr"))
                        'cmd.Parameters.AddWithValue("@Path", "ExamAssignmentdoc/" & filename)
                      
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
    End Sub

    
    Protected Sub btnupload_Click(sender As Object, e As System.EventArgs) Handles btnupload.Click
        Savedoc()
    End Sub
End Class
