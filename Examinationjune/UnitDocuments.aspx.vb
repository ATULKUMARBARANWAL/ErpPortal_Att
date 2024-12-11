Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Partial Class Examinationjune_UnitDocuments
    Inherits System.Web.UI.Page

    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblAcademicyear.Text = Request.QueryString("acyr")
            lblunitname.Text = Request.QueryString("uname")
            lblsubject.Text = Request.QueryString("rid")
            RepeaterBinddoc()
        End If
    End Sub

    Protected Sub lbtnDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnDoc.Click
        lbtnDoc.Attributes.Add("class", "nav-item nav-link active")
        lbtnvideo.Attributes.Add("class", "nav-item nav-link")
        lbtnDoc.BackColor = Drawing.Color.FromArgb(30, 208, 133)
        lbtnDoc.ForeColor = Drawing.Color.White

        lbtnvideo.BackColor = Drawing.Color.White
        lbtnvideo.ForeColor = Drawing.Color.Black

        paneldoc.Visible = True
        panelvideo.Visible = False

        lblheadingpopup.Text = "Upload Documents"
        lbldocname.Text = "Documents :"

        RepeaterBinddoc()
    End Sub

    Protected Sub lbtnvideo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnvideo.Click
        lbtnvideo.Attributes.Add("class", "nav-item nav-link active")
        lbtnDoc.Attributes.Add("class", "nav-item nav-link")
        lbtnvideo.BackColor = Drawing.Color.FromArgb(30, 208, 133)
        lbtnvideo.ForeColor = Drawing.Color.White

        lbtnDoc.BackColor = Drawing.Color.White
        lbtnDoc.ForeColor = Drawing.Color.Black

        paneldoc.Visible = False
        panelvideo.Visible = True
        lblheadingpopup.Text = "Upload Videos"
        lbldocname.Text = "Video :"

        RepeaterBindvid()
    End Sub

 


    Protected Sub btnupload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnupload.Click
        If panelvideo.Visible = True Then
            ViewState("Mediatype") = "Video"
            SaveVideo()
        Else
            ViewState("Mediatype") = "Document"
            Savedoc()
        End If
      
    End Sub

    
    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        Response.Redirect("SubjectPlan.aspx?acyr=" & lblAcademicyear.Text & "&rid=" & lblsubject.Text & "&subjectid=" & Request.QueryString("subjectid") & "&cid=" & Request.QueryString("cid") & "&s=" & Request.QueryString("s") & "&ay=" & Request.QueryString("ay") & "&u=" & Request.QueryString("u"))
    End Sub

   

    Private Sub RepeaterBinddoc()

        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand("select * from Exam_SubjectPlanItem where Courseid='" & Request.QueryString("cid") & "' and Subunitid='" & Request.QueryString("suid") & "' and Academicyear='" & Request.QueryString("acyr") & "' and Mediatype='Document'", con)
            Dim sda As New SqlDataAdapter(cmd)
            Dim ds As New DataSet

            sda.Fill(ds)


            Repeater2.DataSource = ds
            Repeater2.DataBind()

        End Using

    End Sub

    Private Sub RepeaterBindvid()
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand("select * from Exam_SubjectPlanItem where Courseid='" & Request.QueryString("cid") & "' and Subunitid='" & Request.QueryString("suid") & "' and Academicyear='" & Request.QueryString("acyr") & "' and Mediatype='Video'", con)
            Dim sda As New SqlDataAdapter(cmd)
            Dim ds As New DataSet

            sda.Fill(ds)


            Repeater1.DataSource = ds
            Repeater1.DataBind()

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
            Dim cmd As SqlCommand = New SqlCommand("delete FROM Exam_SubjectPlanItem where Path='" & filePath & "'", con)
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
        uploadfile.PostedFile.SaveAs(Server.MapPath("Examsubdoc/" & filename))
        Dim contentType As String = uploadfile.PostedFile.ContentType
        Using fs As Stream = uploadfile.PostedFile.InputStream
            Using br As New BinaryReader(fs)
                Dim bytes As Byte() = br.ReadBytes(DirectCast(fs.Length, Long))
                Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
                Using con As New SqlConnection(constr)
                    Dim query As String = "insert into Exam_SubjectPlanItem (Dated,Courseid,Subunitid,Unitdoc,Mediatype,Topic,UserID,Academicyear,Path) values (@Dated,@Courseid,@Subunitid,@Unitdoc,@Mediatype,@Topic,@UserID,@Academicyear,@Path)"
                    Using cmd As New SqlCommand(query)
                        cmd.Connection = con
                        cmd.Parameters.Add("@Dated", SqlDbType.VarChar).Value = Date.Now
                        cmd.Parameters.Add("@Courseid", SqlDbType.VarChar).Value = Request.QueryString("cid")

                        cmd.Parameters.Add("@Subunitid", SqlDbType.VarChar).Value = Request.QueryString("suid")
                        cmd.Parameters.Add("@Unitdoc", SqlDbType.VarChar).Value = filename

                        cmd.Parameters.Add("@Mediatype", SqlDbType.VarChar).Value = ViewState("Mediatype")
                        cmd.Parameters.Add("@Topic", SqlDbType.VarChar).Value = txttopic.Text

                        cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = Request.QueryString("u")
                        'cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = ""
                        cmd.Parameters.Add("@Academicyear", SqlDbType.NVarChar).Value = Request.QueryString("acyr")
                        cmd.Parameters.Add("@Path", SqlDbType.NVarChar).Value = "Examsubdoc/" & filename

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

    Private Sub SaveVideo()
        Dim filename As String = Path.GetFileName(uploadfile.PostedFile.FileName)
        uploadfile.PostedFile.SaveAs(Server.MapPath("ExamSubvideo/" & filename))
        Dim contentType As String = uploadfile.PostedFile.ContentType
        Using fs As Stream = uploadfile.PostedFile.InputStream
            Using br As New BinaryReader(fs)
                Dim bytes As Byte() = br.ReadBytes(DirectCast(fs.Length, Long))
                Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
                Using con As New SqlConnection(constr)
                    Dim query As String = "insert into Exam_SubjectPlanItem (Dated,Courseid,Subunitid,Unitdoc,Mediatype,Topic,UserID,Academicyear,Path) values (@Dated,@Courseid,@Subunitid,@Unitdoc,@Mediatype,@Topic,@UserID,@Academicyear,@Path)"
                    Using cmd As New SqlCommand(query)
                        cmd.Connection = con
                        cmd.Parameters.Add("@Dated", SqlDbType.VarChar).Value = Date.Now
                        cmd.Parameters.Add("@Courseid", SqlDbType.VarChar).Value = Request.QueryString("cid")

                        cmd.Parameters.Add("@Subunitid", SqlDbType.VarChar).Value = Request.QueryString("suid")
                        cmd.Parameters.Add("@Unitdoc", SqlDbType.VarChar).Value = filename

                        cmd.Parameters.Add("@Mediatype", SqlDbType.VarChar).Value = ViewState("Mediatype")
                        cmd.Parameters.Add("@Topic", SqlDbType.VarChar).Value = txttopic.Text

                        cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = Request.QueryString("u")
                        'cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = ""
                        cmd.Parameters.Add("@Academicyear", SqlDbType.NVarChar).Value = Request.QueryString("acyr")
                        cmd.Parameters.Add("@Path", SqlDbType.NVarChar).Value = "ExamSubvideo/" & filename

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

End Class
