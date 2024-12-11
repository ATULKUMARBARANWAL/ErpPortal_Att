Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient

Partial Class StudentMis_MISdashboard
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
            FillDdlacademic()


            ddlacademicyear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = True
            'If Not IsPostBack Then
            '    Totalassigned()
            '    Me.Successcount()
            BindgrdPrograms()

        End If

    End Sub

    Private Sub BindgrdPrograms()
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim sql As String = "Select Cs.Coursesessionid, Cs.Academicyear, tbl.Courseid, C.Course, C.Coursecode  ,case when Cs.Coursetype like '%sem%' then " & _
" Cast (Cs.Duration*2 AS nvarchar(10))+' '+Cs.Coursetype when  Cs.Coursetype like '%year%' then Cast (Cs.Duration*1 AS nvarchar(10))+' '+Cs.Coursetype end " & _
" as 'Coursetype', tbl.Total from Exam_Coursesession Cs join (Select Cs.Academicyear, Cs.Courseid, Count(stn.StudentID ) as 'Total' from " & _
" ( Select * from Student where CurrentSessionid='" & Request.QueryString("s") & "' and  EnrollmentNo is not null and RegistrationApproved='1' and AdmissionApproved='1' ) stn right join Exam_CourseSession Cs on stn.CourseID =Cs.Courseid " & _
" where Cs.Academicyear ='" & ddlacademicyear.SelectedItem.Text & "' group by Cs.Courseid ,Cs.Academicyear ) " & _
" tbl on Cs.Courseid =tbl.Courseid join Exam_Course C on Cs.Courseid =C.Courseid where Cs.Academicyear ='" & ddlacademicyear.SelectedItem.Text & "'"
            cmd.CommandText = sql
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                Using dt As New DataTable()
                    sda.Fill(dt)
                    grdPrograms.DataSource = dt
                    grdPrograms.DataBind()
                End Using
            End Using
        End Using
      
    End Sub

    Protected Sub grdPrograms_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdPrograms.RowCommand
        If e.CommandName = "Studentlist" Then
            grdPrograms.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdPrograms.Rows(rowIndex)
            ViewState("Academicyear") = ddlacademicyear.SelectedItem.Text
            ViewState("Coursesessionid") = row.Cells(2).Text
            ViewState("courseid") = row.Cells(1).Text
            Response.Redirect("StudentList.aspx?rid=" & ViewState("courseid") & "&acyr=" & ddlacademicyear.SelectedItem.Text)
        End If

    End Sub

    Private Sub FillDdlacademic()
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
        Me.BindgrdPrograms()

    End Sub



    Protected Sub txtSearchSubject_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchSubject.Load
        BindgrdPrograms()
    End Sub




   
End Class
