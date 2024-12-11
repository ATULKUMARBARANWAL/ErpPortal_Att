

Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Partial Class Attendance_FacultyAttendance
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            'Dim query As String = "select distinct  Academicyear from Exam_CourseSession"
            'BindDropDownList1(ddlacedmicyear, query, "Academicyear", "Academicyear", "Select")
            'If Request.Form("__EVENTTARGET") = "load$student" Then
            '    'txtbind.bind(Request.Form("__EVENTARGUMENt"))
            '    '   ViewState("userid") = Request.Form("__EVENTARGUMENt")
            '    bind(Request.Form("__EVENTARGUMENt"))
            'End If
            ViewState("Acyr") = Date.Today.Year
            ViewState("userid") = Request.QueryString("u")
            ViewState("sessionid") = Request.QueryString("s")
            ViewState("ayid") = Request.QueryString("ayid")
            BindDropDownList1()
            getacademicyear()
            '..by default current year select in ddlacademicyear..
            findyearfromsession()
            ddlacademicyear.Items.FindByText(ViewState("Academicyear")).Selected = True
            lblacademicyear.Text = ViewState("Academicyear")


            ' ddlacademicyear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = True
            'If Not IsPostBack Then
            '    Totalassigned()
            '    Me.Successcount()
            Me.BindgrdSectionAtnd()

        End If
    End Sub

    Private Sub findyearfromsession()
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim sql As String = "Select * from Exam_Session where Sessionid='" & ViewState("sessionid") & "'"

            cmd.CommandText = sql
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                Using dt As New DataTable()
                    sda.Fill(dt)
                    If dt.Rows.Count Then
                        ViewState("Academicyear") = dt.Rows(0)("Academicyear").ToString()
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub getacademicyear()
        Dim query As String = "select AcademicYear from Exam_Session order by Academicyear"
        BindDropDownList1(ddlacademicyear, query, "AcademicYear", "AcademicYear", "")
    End Sub
    Private Sub BindgrdSectionAtnd()
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim sql As String = "SELECT C.Classid, C.ClassName,Cls.ClassesID, Cls.Code, cta.TotalStudent FROM classTeacherAssign cta JOIN Sch_Class C ON cta.Classid = C.Classid JOIN Classes Cls ON cta.ClassesID = Cls.ClassesID WHERE cta.EmployeeID = @EmployeeID"





        cmd.CommandText = sql
        cmd.Parameters.AddWithValue("@EmployeeID", ViewState("userid"))

        Using sda As New SqlDataAdapter()
            cmd.Connection = con
            sda.SelectCommand = cmd
            Using dt As New DataTable()
                sda.Fill(dt)
                grdSectionAtnd.DataSource = dt
                grdSectionAtnd.DataBind()
                    End Using
                End Using
    End Using
    End Sub


    Private Sub BindDropDownList1()
        Dim query As String = "select AcademicYear from Exam_Session where SessionCreate=1"
        BindDropDownList1(ddlacademicyear, query, "AcademicYear", "AcademicYear", "")
    End Sub

    Private Sub BindDropDownList1(ByVal ddl2 As DropDownList, ByVal query As String, ByVal text As String, ByVal value As String, ByVal defaultText As String)

        Dim cmd As New SqlCommand(query)
        Using con As New SqlConnection(constr)
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                con.Open()
                ddl2.DataSource = cmd.ExecuteReader()
                ddl2.DataTextField = text
                ddl2.DataValueField = value
                ddl2.DataBind()
                con.Close()
            End Using
        End Using

    End Sub

    Protected Sub ddlacademicyear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlacademicyear.SelectedIndexChanged
        Me.BindgrdSectionAtnd()

    End Sub

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


    Protected Sub grdSectionAtnd_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdSectionAtnd.RowCommand

        If e.CommandName = "Attandence" Then
            grdSectionAtnd.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdSectionAtnd.Rows(rowIndex)
            Dim classesid As String = row.Cells(3).Text
            Dim courseid As String = row.Cells(1).Text

            Response.Redirect("../Attendance/RegularAttendence.aspx?s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u") & "&classesid=" & classesid & "&Courseid=" & courseid)
        End If

        If e.CommandName = "Viewstudentattendence" Then
            grdSectionAtnd.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdSectionAtnd.Rows(rowIndex)
            Dim courseID As String = row.Cells(1).Text
            Dim courseName As String = row.Cells(2).Text
            Dim classesID As String = row.Cells(3).Text
            Dim classesName As String = row.Cells(4).Text
            Dim totalStudent As String = row.Cells(5).Text

            Response.Redirect("../Attendance/Attrpt.aspx?s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u") & "&classesid=" & classesID & "&Courseid=" & courseID & "&acyr=" & ddlacademicyear.SelectedValue.ToString & "&TotalStudent=" & totalStudent & "&CourseName=" & courseName & "&ClassesName=" & classesName)
        End If



    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub
    Protected Sub Download_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Download.Click
        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment;filename=AttendanceList.xls")
        Response.ContentType = "application/vnd.ms-excel"
        Dim sw As StringWriter = New StringWriter()
        Dim htm As HtmlTextWriter = New HtmlTextWriter(sw)
        grdSectionAtnd.AllowPaging = False

        grdSectionAtnd.HeaderRow.Cells(1).Visible = False
        grdSectionAtnd.HeaderRow.Cells(2).Visible = False
        grdSectionAtnd.HeaderRow.Cells(3).Visible = False

        grdSectionAtnd.HeaderRow.Cells(4).Visible = False
        grdSectionAtnd.HeaderRow.Cells(5).Visible = False
        grdSectionAtnd.HeaderRow.Cells(8).Visible = False
        grdSectionAtnd.HeaderRow.Cells(12).Visible = False
        grdSectionAtnd.HeaderRow.Cells(13).Visible = False
        grdSectionAtnd.RowStyle.HorizontalAlign = HorizontalAlign.Center
        For Each row As GridViewRow In grdSectionAtnd.Rows
            row.Cells(1).Visible = False
            row.Cells(2).Visible = False
            row.Cells(3).Visible = False
            row.Cells(4).Visible = False
            row.Cells(5).Visible = False
            row.Cells(8).Visible = False
            row.Cells(12).Visible = False
            row.Cells(13).Visible = False

        Next

        grdSectionAtnd.RenderControl(htm)
        Response.Write(sw.ToString())
        Response.End()
    End Sub

End Class
