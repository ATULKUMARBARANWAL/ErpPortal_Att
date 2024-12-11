
Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO

Partial Class Attendance_Timetable
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("Academicyear") = Request.QueryString("acyr")
            ViewState("Sessionid") = Request.QueryString("s")
            ViewState("ayid") = Request.QueryString("ay")
            ViewState("Courseid") = Request.QueryString("cid")
            lbltotalsub.Text = ViewState("Academicyear")
            fetchddlProgram()
            fillddlSection()
            'fillddlsemyear()
            BindGridSection()
        End If
    End Sub


    Private Sub fetchddlacademicyear()
        
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("select * from Exam_Session order by Academicyear  desc")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        ddlacademicyear.DataSource = dt
                        ddlacademicyear.DataTextField = "Academicyear"
                        ddlacademicyear.DataValueField = "Academicyear"
                        ddlacademicyear.DataBind()
                        Dim Year As Integer
                        Year = Convert.ToInt32(Now.ToString("yyyy"))

                        ddlacademicyear.Items.FindByValue(Year).Selected = True

                    End Using
                End Using
            End Using
        End Using

        'Catch ex As Exception


        'End Try


    End Sub

    Private Sub fetchddlProgram()
        'Try

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Classid,ClassName from Sch_Class")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Ddlprogram.DataSource = dt
                        Ddlprogram.DataTextField = "ClassName"
                        Ddlprogram.DataValueField = "Classid"
                        Ddlprogram.DataBind()
                        '  Dim Year As Integer
                        ' Year = Convert.ToInt32(Now.ToString("yyyy"))

                        Ddlprogram.Items.FindByValue(ViewState("Courseid")).Selected = True

                    End Using
                End Using
            End Using
        End Using

        'Catch ex As Exception

        'End Try


    End Sub
    Private Sub fillddlSection()
        Using con As New SqlConnection(constr)
            Dim selectedCourse = Ddlprogram.SelectedValue
            Using cmd As New SqlCommand("SELECT * FROM sectionAssign WHERE Classid = @Classid", con)

                ' Add the parameter to avoid SQL injection
                cmd.Parameters.AddWithValue("@Classid", selectedCourse)

                ' Open the connection
                con.Open()

                ' Use SqlDataAdapter to fill the DataTable
                Using sda As New SqlDataAdapter(cmd)
                    Using dt As New DataTable()
                        sda.Fill(dt)

                        '' Bind the DataTable to the dropdown list
                        Ddlsectionn.DataSource = dt
                        Ddlsectionn.DataTextField = "Code" ' Replace with the actual column name for display
                        Ddlsectionn.DataValueField = "ClassesID" ' Replace with the actual column name for the value
                        Ddlsectionn.DataBind()

                        '' Optionally add a default item, e.g., "Select Section"
                        Ddlsectionn.Items.Insert(0, New ListItem("All", ""))
                    End Using
                End Using
            End Using
        End Using
    End Sub

    
    Protected Sub Ddlsectionn_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        BindGridSection() ' Call method to bind the grid based on the selected section
    End Sub
 
    Private Sub BindGridSection()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()

                Dim selectedClass = Ddlprogram.SelectedValue

                ' Build the query to filter by CourseID (Program)
                Dim query As String = "SELECT S.ClassesID, St.CourseID, Sc.ClassName, S.Code, St.Sem, COUNT(St.StudentID) AS Totalstudent " & _
                                      "FROM sectionAssign S " & _
                                      "LEFT JOIN Sch_Class Sc ON S.Classid = Sc.Classid " & _
                                      "LEFT JOIN Student St ON S.ClassesID = St.ClassesID " & _
                                      "WHERE St.CourseID = @CourseID And S.Classid=@CourseID"

                ' Apply section filter if a specific section is selected
                If Not String.IsNullOrEmpty(Ddlsectionn.SelectedValue) Then
                    query &= " AND S.ClassesID = @ClassesID"
                End If

                query &= " GROUP BY S.ClassesID, St.CourseID, S.Code, St.Sem, Sc.ClassName"

                ' Set the command text and the connection for the SqlCommand
                cmd.CommandText = query
                cmd.Connection = con

                ' Add parameters for filtering by CourseID and ClassesID
                cmd.Parameters.AddWithValue("@CourseID", Ddlprogram.SelectedValue) ' Selected Program (CourseID)
                If Not String.IsNullOrEmpty(Ddlsectionn.SelectedValue) Then
                    cmd.Parameters.AddWithValue("@ClassesID", Ddlsectionn.SelectedValue) ' Selected Section (ClassesID)
                End If

                ' Execute the query using SqlDataAdapter
                Using sda As New SqlDataAdapter(cmd)
                    Using dt As New DataTable()
                        sda.Fill(dt)

                        ' Bind the result to the GridView
                        gridsection.DataSource = dt
                        gridsection.DataBind()

                    End Using
                End Using
            End Using
        End Using
        Checktimetable()
    End Sub





  

   
    Protected Sub Ddlprogram_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ddlprogram.SelectedIndexChanged
        'Ddlsemyear.Items.Clear()
        'fillddlsemyear()
        fillddlSection()
        BindGridSection()
    End Sub

    Protected Sub gridsection_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridsection.RowCommand
        If e.CommandName = "Timetable" Then
            gridsection.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gridsection.Rows(rowIndex)
            Dim Courseid = Ddlprogram.SelectedValue

            ViewState("Classid") = row.Cells(1).Text
            ViewState("Sem") = row.Cells(5).Text
            ViewState("sec") = row.Cells(3).Text
            ViewState("coursename") = Ddlprogram.SelectedItem.Text

            Response.Redirect("../Attendance/CreateTimetable.aspx?s=" & Request.QueryString("s") & "&cid=" & Courseid & "&acyr=" & Request.QueryString("acyr") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u") & "&Sem=" & ViewState("Sem") & "&Classid=" & ViewState("Classid") & "&rid=" & Request.QueryString("rid") & "&coursename=" & ViewState("coursename") & "&semyear=" & Lblsemyear.Text & "&Section=" & ViewState("sec") & "&ay=" & ViewState("ayid"))
            Dim a = 1
        End If
    End Sub


   

    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        Response.Redirect("../Examinationjune/Dashboard.aspx?s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u") & "&ay=" & ViewState("ayid"))
    End Sub

    'Protected Sub Ddlsemyear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ddlsemyear.SelectedIndexChanged
    '    BindGridSection()
    'End Sub

    Private Sub Checkinggrid(ByVal Query As String, ByVal btn As LinkButton)

        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim sql As String = Query
            cmd.CommandText = sql
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                Using dt As New DataTable()
                    sda.Fill(dt)
                    Dim Count As String = dt.Rows(0)("Count").ToString()

                    If Count = 0 Then
                        btn.ForeColor = Drawing.Color.FromArgb(255, 79, 79)
                    End If
                End Using
            End Using
        End Using

    End Sub

    Private Sub Checktimetable()
        For Each row As GridViewRow In gridsection.Rows
            Dim Courseid As String = row.Cells(2).Text
            Dim Section As String = row.Cells(1).Text
            Dim Sem As String = row.Cells(5).Text
            Dim query As String = "select Count(*) as Count from timetable where Courseid='" & Courseid & "' and Sessionid='" & ViewState("Sessionid") & "' and ClassesID='" & Section & "' "
            Checkinggrid(query, TryCast(row.FindControl("namelink"), LinkButton))
        Next
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub
    Protected Sub Download_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Download.Click
        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment;filename=FacultySubject.xls")
        Response.ContentType = "application/vnd.ms-excel"
        Dim sw As StringWriter = New StringWriter()
        Dim htm As HtmlTextWriter = New HtmlTextWriter(sw)
        gridsection.AllowPaging = False

        gridsection.HeaderRow.Cells(1).Visible = False
        gridsection.HeaderRow.Cells(2).Visible = False
        gridsection.HeaderRow.Cells(6).Visible = False
        gridsection.RowStyle.HorizontalAlign = HorizontalAlign.Center
        For Each row As GridViewRow In gridsection.Rows
            row.Cells(1).Visible = False
            row.Cells(2).Visible = False
            row.Cells(6).Visible = False

        Next

        gridsection.RenderControl(htm)
        Response.Write(sw.ToString())
        Response.End()
    End Sub
End Class
