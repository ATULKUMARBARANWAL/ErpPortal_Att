Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Net.Mail
Imports System.Net
Imports System.Data.OleDb
Partial Class Examinationjune_SectionAlotment
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("Sessionid") = Request.QueryString("s")
            ViewState("Userid") = Request.QueryString("u")
            ViewState("Ayid") = Request.QueryString("ay")
            ViewState("Stuid") = Request.QueryString("stuid")
            ViewState("courseid") = Request.QueryString("rid")
            ViewState("Acyr") = Request.QueryString("acyr")
            Lblstucount.Text = Request.QueryString("Count")
            fetchDdlsec()
            fetchDdlclassroom()
        End If
    End Sub
    Private Sub fetchDdlsec()

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select * from Classes order by Code")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Ddlsec.DataSource = dt
                        Ddlsec.DataTextField = "Code"
                        Ddlsec.DataValueField = "ClassesID"
                        Ddlsec.DataBind()

                        Ddlsec.Items.Insert(0, New ListItem("Select", "0"))
                    End Using
                End Using
            End Using
        End Using

        'Catch ex As Exception


        'End Try


    End Sub

    Private Sub fetchDdlclassroom()

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select * from ClassRoom  order by ClassRoom")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Ddlclassroom.DataSource = dt
                        Ddlclassroom.DataTextField = "ClassRoom"
                        Ddlclassroom.DataValueField = "classid"
                        Ddlclassroom.DataBind()

                        Ddlclassroom.Items.Insert(0, New ListItem("Select", "0"))
                    End Using
                End Using
            End Using
        End Using

        'Catch ex As Exception


        'End Try


    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Ddlsec.SelectedItem.Text = "Select" Then
        ElseIf Ddlclassroom.SelectedItem.Text = "Select" Then
            Using con As New SqlConnection(constr)
                Dim cmd As New SqlCommand()
                Dim sql As String = " Update Student Set Classesid=@classid where StudentID in (" & ViewState("Stuid") & ")"
                cmd.CommandText = sql
                cmd.Connection = con
                cmd.Parameters.AddWithValue("@classid", Ddlsec.SelectedValue)
                cmd.Parameters.AddWithValue("@classroomid", Ddlclassroom.SelectedValue)

                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()

                SaralMsg.Messagebx.Alert(Me, "Saved Successfully")
            End Using
            Response.Redirect("~/StudentMis/StudentList.aspx?s=" & ViewState("Sessionid") & "&u=" & ViewState("Userid") & "&rid=" & ViewState("courseid") & "&acyr=" & ViewState("Acyr") & "&ay=" & ViewState("Ayid"))


        Else
            Using con As New SqlConnection(constr)
                Dim cmd As New SqlCommand()
                Dim sql As String = " Update Student Set Classesid=@classid , Classroomid=@classroomid where StudentID in (" & ViewState("Stuid") & ")"
                cmd.CommandText = sql
                cmd.Connection = con
                cmd.Parameters.AddWithValue("@classid", Ddlsec.SelectedValue)
                cmd.Parameters.AddWithValue("@classroomid", Ddlclassroom.SelectedValue)

                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()

                SaralMsg.Messagebx.Alert(Me, "Saved Successfully")
            End Using
            Response.Redirect("~/StudentMis/StudentList.aspx?s=" & ViewState("Sessionid") & "&u=" & ViewState("Userid") & "&rid=" & ViewState("courseid") & "&acyr=" & ViewState("Acyr") & "&ay=" & ViewState("Ayid"))

        End If
    End Sub

    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        Response.Redirect("~/StudentMis/StudentList.aspx?s=" & ViewState("Sessionid") & "&u=" & ViewState("Userid") & "&rid=" & ViewState("courseid") & "&acyr=" & ViewState("Acyr") & "&ay=" & ViewState("Ayid"))

    End Sub
End Class
