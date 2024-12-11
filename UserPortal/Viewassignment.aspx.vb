Imports System.IO
Imports System.Data.SqlClient
Imports System.Data

Partial Class UserPortal_Viewassignment
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("uploadassignmntid") = Request.QueryString("rid")
            RepeaterBinddoc()
        End If
    End Sub
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

    Protected Sub DeleteFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim filePath As String = (TryCast(sender, LinkButton)).CommandArgument

        Using con As New SqlConnection(constr)

            con.Open()
            Dim cmd As SqlCommand = New SqlCommand("delete FROM Assign_StuUpload where UploadassignmentID='" & ViewState("uploadassignmntid") & "' ans studentid = '" & Request.QueryString("Stuid") & "' ", con)
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
    Private Sub RepeaterBinddoc()

        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand("select * from Assign_StuUpload where Uploadassignmentid = '" & ViewState("uploadassignmntid") & "' ", con)
            Dim sda As New SqlDataAdapter(cmd)
            Dim ds As New DataSet
            sda.Fill(ds)
            Repeater2.DataSource = ds
            Repeater2.DataBind()
        End Using

    End Sub
End Class
