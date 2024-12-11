Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Partial Class Attendance_TimetableIncharge
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
            BindDropDownList1()


            ddlacademicyear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = True
            'If Not IsPostBack Then
            '    Totalassigned()
            '    Me.Successcount()
            Me.BindGrid()

        End If
    End Sub

    Private Sub BindGrid()
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim sql As String = "Select Cs.Coursesessionid, ce.Course,ce.Coursecode,ce.Courseid, Cs.Coursetype, Cs.Courselevel, Cs.NoofSeat, Cs.Duration,T.Admission, T.CourseID, " & _
" case when Cs.Coursetype like '%year%' then Cast(Cs.Duration*1 as nvarchar(20))+' '+Cs.coursetype when Cs.Coursetype like '%sem%' then " & _
" Cast(Cs.Duration*2 as nvarchar(20))+' '+Cs.coursetype end as 'Total Sem/Year' from  Exam_CourseSession Cs join " & _
" (Select Count(St.Studentid) as 'Admission' , Cs.CourseID from Student St right join Exam_CourseSession Cs on " & _
" St.CourseID=Cs.CourseId group by Cs.CourseID ) T on Cs.Courseid =T.CourseID join Exam_COurse ce on cs.COurseid=ce.Courseid " & _
"  where cs.Academicyear='" + ddlacademicyear.SelectedItem.Text + "' "

            If Not String.IsNullOrEmpty(txtSearchSubject.Text.Trim()) Then
                sql += " and ce.Course LIKE '%' + @Course + '%' or ce.Coursecode LIKE '%' + @Course + '%'"
                cmd.Parameters.AddWithValue("@Course", txtSearchSubject.Text.Trim())
            End If
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
        CheckSubjectadd()
        checkfacultyassign()
        checkexamstructure()
        checkexam()
    End Sub

    Protected Sub grdPrograms_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdPrograms.RowCommand
        If e.CommandName = "CourseSubject" Then
            grdPrograms.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdPrograms.Rows(rowIndex)
            ViewState("Coursesessionid") = grdPrograms.SelectedDataKey(0)
            Session("Otherid") = row.Cells(2).Text
            Response.Redirect("ProgramSubjectlist.aspx?rid=" & grdPrograms.SelectedDataKey(0) & "&acyr=" & ddlacademicyear.SelectedItem.Text)
        End If
        If e.CommandName = "Subjectmapping" Then
            grdPrograms.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdPrograms.Rows(rowIndex)
            ViewState("Coursesessionid") = grdPrograms.SelectedDataKey(0)
            Session("Otherid") = row.Cells(2).Text
            Response.Redirect("SubjectToProgram.aspx?rid=" & grdPrograms.SelectedDataKey(0) & "&acyr=" & ddlacademicyear.SelectedItem.Text)
        End If

        If e.CommandName = "AssignFaculty" Then
            grdPrograms.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdPrograms.Rows(rowIndex)
            ViewState("Coursesessionid") = grdPrograms.SelectedDataKey(0)
            Session("Otherid") = row.Cells(2).Text
            Response.Redirect("AssignFaculty.aspx?rid=" & grdPrograms.SelectedDataKey(0) & "&acyr=" & ddlacademicyear.SelectedItem.Text)
        End If
        If e.CommandName = "ExamStructure" Then
            grdPrograms.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdPrograms.Rows(rowIndex)
            Session("Coursesessionid") = grdPrograms.SelectedDataKey(0)
            Session("Otherid") = row.Cells(2).Text
            Session("Course") = row.Cells(3).Text
            Response.Redirect("ExProStru.aspx?rid=" & grdPrograms.SelectedDataKey(0) & "&acyr=" & ddlacademicyear.SelectedItem.Text)
        End If
        If e.CommandName = "Exam" Then
            grdPrograms.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdPrograms.Rows(rowIndex)
            ViewState("Coursesessionid") = grdPrograms.SelectedDataKey(0)
            Session("Courseid") = row.Cells(2).Text
            Response.Redirect("CreatedExams.aspx?rid=" & grdPrograms.SelectedDataKey(0) & "&acyr=" & ddlacademicyear.SelectedItem.Text)
        End If

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
        Me.BindGrid()

    End Sub



    Protected Sub txtSearchSubject_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchSubject.Load
        BindGrid()
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

    Private Sub CheckSubjectadd()
        For Each row As GridViewRow In grdPrograms.Rows
            Dim Courseid As String = row.Cells(2).Text
            Dim query As String = "select Count(*) as Count from Exam_Coursesubject where Courseid='" & Courseid & "' and Academicyear='" & ddlacademicyear.SelectedItem.Text & "'"
            Checkinggrid(query, TryCast(row.FindControl("namelink1"), LinkButton))
        Next

    End Sub

    Private Sub checkfacultyassign()
        For Each row As GridViewRow In grdPrograms.Rows
            Dim Courseid As String = row.Cells(2).Text
            Dim query As String = "select Count(*) as Count from Exam_SubjectPlan where Courseid='" & Courseid & "' and Academicyear='" & ddlacademicyear.SelectedItem.Text & "'"
            Checkinggrid(query, TryCast(row.FindControl("assignlink"), LinkButton))
        Next
    End Sub

    Private Sub checkexamstructure()
        For Each row As GridViewRow In grdPrograms.Rows
            Dim Courseid As String = row.Cells(2).Text
            Dim query As String = "select Count(*) as Count from Exam_ProgramStructure where Courseid='" & Courseid & "' and Academicyear='" & ddlacademicyear.SelectedItem.Text & "'"
            Checkinggrid(query, TryCast(row.FindControl("assignlink1"), LinkButton))
        Next
    End Sub

    Private Sub checkexam()
        For Each row As GridViewRow In grdPrograms.Rows
            Dim Courseid As String = row.Cells(2).Text
            Dim query As String = "select Count(*) as Count from Exam_CourseExam where Courseid='" & Courseid & "' and Academicyear='" & ddlacademicyear.SelectedItem.Text & "'"
            Checkinggrid(query, TryCast(row.FindControl("assignlink2"), LinkButton))
        Next
    End Sub

End Class
