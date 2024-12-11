Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO

Partial Class UserPortal_StudyMaterials
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Get the Subunitid from the query string
            RepeaterBinddoc("Published")
        End If
    End Sub

    ' Method to bind documents to the repeater based on status
    'Private Sub RepeaterBindDoc(ByVal status As String, ByVal subunitid As String)
    '    Using con As New SqlConnection(constr)
    '        Dim cmd As New SqlCommand("SELECT Subplanitemid, Path, Topic FROM Exam_SubjectPlanItem WHERE Subunitid = @Subunitid AND Mediatype = 'Document' AND Status = @Status", con)
    '        cmd.Parameters.AddWithValue("@Subunitid", subunitid)
    '        cmd.Parameters.AddWithValue("@Status", status)

    '        Dim sda As New SqlDataAdapter(cmd)
    '        Dim ds As New DataSet()

    '        sda.Fill(ds)
    '        Repeater2.DataSource = ds
    '        Repeater2.DataBind()
    '    End Using
    'End Sub

    '' Method to bind videos to the repeater based on status
    'Private Sub RepeaterBindVid(ByVal status As String, ByVal subunitid As String)
    '    Using con As New SqlConnection(constr)
    '        Dim cmd As New SqlCommand("SELECT Subplanitemid, Path, Topic FROM Exam_SubjectPlanItem WHERE Subunitid = @Subunitid AND Mediatype = 'Video' AND Status = @Status", con)
    '        cmd.Parameters.AddWithValue("@Subunitid", subunitid)
    '        cmd.Parameters.AddWithValue("@Status", status)

    '        Dim sda As New SqlDataAdapter(cmd)
    '        Dim ds As New DataSet()

    '        sda.Fill(ds)
    '        Repeater1.DataSource = ds
    '        Repeater1.DataBind()
    '    End Using
    'End Sub
    ' Bind documents to Repeater based on status
    Private Sub RepeaterBinddoc(ByVal status As String)
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand("SELECT Subplanitemid, Path, Topic, Status FROM Exam_SubjectPlanItem WHERE  Subunitid = @Subunitid AND Mediatype = 'Document' AND Status = @Status", con)
            cmd.Parameters.AddWithValue("@Subunitid", Request.QueryString("suid"))
            cmd.Parameters.AddWithValue("@Status", status)

            Dim sda As New SqlDataAdapter(cmd)
            Dim ds As New DataSet()

            sda.Fill(ds)

            Dim basePath As String = "/samplepublish/Examinationjune/"
            For Each row As DataRow In ds.Tables(0).Rows
                row("Path") = basePath & row("Path").ToString().TrimStart("/"c)
            Next
            Repeater2.DataSource = ds
            Repeater2.DataBind()
        End Using
    End Sub
    Protected Sub Repeater2_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles Repeater2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            ' Get the Status from the data item
            Dim status As String = DataBinder.Eval(e.Item.DataItem, "Status").ToString()

            ' Find the LinkButtons
            Dim lbtnEditDoc As LinkButton = CType(e.Item.FindControl("lbtneditdoc"), LinkButton)
            Dim lbtnDownloadDoc As LinkButton = CType(e.Item.FindControl("lbtndownloaddoc"), LinkButton)

            Dim hfSubplanitemid As HiddenField = CType(e.Item.FindControl("hfSubplanitemid"), HiddenField)

            ' Show the pen icon if status is "Drafted", otherwise show the download icon
            'If status = "Drafted" Then
            '    lbtnEditDoc.Visible = True
            '    lbtnDownloadDoc.Visible = False
            'Else
            '    lbtnEditDoc.Visible = False
            '    lbtnDownloadDoc.Visible = True
            'End If
        End If
    End Sub
    Protected Sub Repeater1_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles Repeater1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            ' Get the Status from the data item
            Dim status As String = DataBinder.Eval(e.Item.DataItem, "Status").ToString()
            Dim path As String = DataBinder.Eval(e.Item.DataItem, "Path").ToString()
            Dim subplanitemid As String = DataBinder.Eval(e.Item.DataItem, "Subplanitemid").ToString()

            ' Find the hyperlink control for viewing the video
            Dim lnkViewDocument As HyperLink = CType(e.Item.FindControl("lnkViewDocument"), HyperLink)

            ' Set the NavigateUrl to the ViewVideo.aspx page with the encoded path
            lnkViewDocument.NavigateUrl = "~/Examinationjune/Examsubdoc/ViewVideo.aspx?videoPath=" & Server.UrlEncode(Path)
            ' Find the LinkButton for download and Button for edit
            Dim lbtnDownloadVid As LinkButton = CType(e.Item.FindControl("lbtndownloadvid"), LinkButton)
            Dim lbtnEditVid As LinkButton = CType(e.Item.FindControl("lbtnEditVid"), LinkButton)

            ' Find the hidden field for Subplanitemid
            Dim hfSubplanitemid As HiddenField = CType(e.Item.FindControl("hfSubplanitemid"), HiddenField)

            ' If Status is "Drafted", show both the edit icon and the download icon
            'If status = "Drafted" Then
            '    lbtnEditVid.Visible = True
            '    lbtnDownloadVid.Visible = True
            'Else
            '    ' If Status is not "Drafted", hide the edit icon but show the download icon
            '    lbtnDownloadVid.Visible = True
            '    lbtnEditVid.Visible = False
            'End If
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

        RepeaterBinddoc("Published")
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

        RepeaterBindvid("Published")
    End Sub
    Private Sub RepeaterBindvid(ByVal status As String)
        Using con As New SqlConnection(constr)
            ' Update SQL query to explicitly select Subplanitemid and other relevant columns
            Dim cmd As New SqlCommand("SELECT Subplanitemid, Path, Topic, Status FROM Exam_SubjectPlanItem WHERE  Subunitid = @Subunitid AND Mediatype = 'Video' AND Status = @Status", con)

            ' Add the necessary parameters to the SQL command
            cmd.Parameters.AddWithValue("@Subunitid", Request.QueryString("suid"))
            cmd.Parameters.AddWithValue("@Status", status)
            ' Use SqlDataAdapter to execute the query and fill the dataset
            Dim sda As New SqlDataAdapter(cmd)
            Dim ds As New DataSet()

            ' Fill the dataset with the result of the SQL query
            sda.Fill(ds)

            Dim basePath As String = "/samplepublish/Examinationjune/"
            For Each row As DataRow In ds.Tables(0).Rows
                row("Path") = basePath & row("Path").ToString().TrimStart("/"c)
            Next

            ' Bind the repeater with the dataset
            Repeater1.DataSource = ds
            Repeater1.DataBind()
        End Using
    End Sub
    Protected Function VideoUrl(ByVal subplanitemid As Integer) As String
        Dim vidUrl As String = ""

        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand("SELECT Unitdoc FROM Exam_SubjectPlanItem WHERE Subplanitemid = @Subplanitemid and Mediatype='video'", con)
            cmd.Parameters.AddWithValue("@Subplanitemid", subplanitemid)

            con.Open()
            Using reader As SqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    vidUrl = reader("Unitdoc").ToString()
                End If
            End Using
            con.Close()
        End Using

        Return vidUrl
    End Function
    Protected Sub DownloadFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim filePath As String = (TryCast(sender, LinkButton)).CommandArgument
        Response.Clear()
        Response.Buffer = True
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = ContentType
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & Path.GetFileName(filePath))
        Response.WriteFile(filePath)
        Response.Flush()
        Response.End()
    End Sub

    ' Handle home button click event
    Protected Sub btnHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHome.Click
        Response.Redirect("~/UserPortal/DashboardStu.aspx?u=" & Session("Studentid") & "&s=" & ViewState("Sessionid"))
    End Sub

    ' Handle logout button click event
    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Response.Redirect("~/LoginFinal.aspx")
    End Sub
End Class
