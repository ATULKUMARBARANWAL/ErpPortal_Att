Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Partial Class ExaminationNEW11_Dashboard
    Inherits System.Web.UI.Page
    Dim cmd As New dbnew()
    Private saralMastercls As saral.Mastercls = New saral.Mastercls()
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    <System.Web.Services.WebMethodAttribute(), _
System.Web.Script.Services.ScriptMethodAttribute()> _
    Public Shared Function GetCompletionList(ByVal prefixText As String, _
              ByVal count As Integer) As String()
        Dim cmd As New SqlCommand With {.Connection = db.Con}
        cmd.CommandText = "select * from SearchProgram(" & HttpContext.Current.Session("Sessionid") & ") where list LIKE '" & _
         "%" & prefixText & "%' ORDER BY List"
        Dim myReader As SqlDataReader
        Dim returnData As List(Of String) = New List(Of String)
        myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While myReader.Read()
            returnData.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myReader("List").ToString(), myReader("Courseid")))
        End While
        Return returnData.ToArray()
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.Form("__EVENTTARGET") = "load$student" Then
            'txtbind.bind(Request.Form("__EVENTARGUMENt"))
            '   ViewState("userid") = Request.Form("__EVENTARGUMENt")
            bind(Request.Form("__EVENTARGUMENt"))
        End If

        If Not IsPostBack Then
            Try
                ViewState("Sessionid") = Request.QueryString("s")
                Session("Sessionid") = Request.QueryString("s")
                ViewState("Userid") = Request.QueryString("u")
                ViewState("evenodd") = Request.QueryString("e")
                ViewState("ayid") = Request.QueryString("ay")
            
                BindDropDownList1()

                findyearfromsession()
                ddlacademicyear.Items.FindByText(ViewState("Academicyear")).Selected = True
                lblacademicyear.Text = ViewState("Academicyear")
                'If Not IsPostBack Then
                '    Totalassigned()
                '    Me.Successcount()
                Me.BindGrid()
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub bind(ByVal sid As String)
        Me.BindGrid1(sid)

    End Sub
    

    Protected Sub BindGrid1(ByVal sid As String)
        ViewState("sid1") = sid

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(" Select Cs.Coursesessionid, ce.Course,ce.Coursecode,ce.Courseid, Cs.Coursetype, Cs.Courselevel, Cs.NoofSeat,  tbl.Total, Cs.Duration,T.Admission, T.CourseID, " & _
 " case when Cs.Coursetype like '%year%' then Cast(Cs.Duration*1 as nvarchar(20))+' '+Cs.coursetype when Cs.Coursetype like '%sem%' then " & _
" Cast(Cs.Duration*2 as nvarchar(20))+' '+Cs.coursetype end as 'Total Sem/Year' from  Exam_CourseSession Cs join " & _
" (Select Count(St.Studentid) as 'Admission' , Cs.CourseID from Student St right join Exam_CourseSession Cs on " & _
" St.CourseID=Cs.CourseId group by Cs.CourseID ) T on Cs.Courseid =T.CourseID join Exam_COurse ce on cs.COurseid=ce.Courseid " & _
" join (Select Cs.Academicyear, Cs.Courseid, Count(stn.StudentID ) as 'Total' from " & _
" (select  c.Cid , 1 as sno,  i.Institue as Institute ,c.Courseid as Courseid,  s.StudentID , s.student,seat.Seat from student s left join Exam_Course c on s.Courseid =c.CourseID left join Institue i on i.InstitueID=c.Cid left join studentyear sy on s.StudentID= sy.StudentID left join Seat seat on seat.SeatID=sy.seatid where sy.ayid='" & Request.QueryString("ay") & "' and  sy.StudentID  is not null and sy.Isstruckoff=0 ) stn right join Exam_CourseSession Cs on stn.CourseID =Cs.Courseid " & _
" where Cs.SessionId ='" + ViewState("Sessionid") + "' and Cs.Academicyear='" & ddlacademicyear.SelectedItem.Text & "' group by Cs.Courseid ,Cs.Academicyear ) " & _
" tbl on Cs.Courseid =tbl.Courseid " & _
 " where cs.SessionId='" + ViewState("Sessionid") + "' and ce.Courseid='" & ViewState("sid1") & "' and  cs.Academicyear='" + ddlacademicyear.SelectedItem.Text + "' order by ce.Course")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd

                    Dim dt As New DataTable()
                    sda.Fill(dt)

                    grdPrograms.DataSource = dt
                    grdPrograms.DataBind()

                End Using
            End Using
        End Using
        CheckSubjectadd()
        checkSectionadd()
        checkfacultyassign()
        checkexamstructure()
        Checktimetable()
        CheckClassTeacher()
        checkexam()
        Checkplacement()
        checkstudent()

    End Sub

    Private Sub BindGrid()
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim sql As String = "SELECT sc.Classid, sc.ClassName, sc.ClassCode, " & _
                                "(SELECT COUNT(*) FROM student s WHERE s.CourseID = sc.classid) AS TotalStudents " & _
                                "FROM Sch_Class sc"

            cmd.CommandText = sql
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                Using dt As New DataTable()
                    sda.Fill(dt)
                    ' Check if data is present and bind it to the GridView
                    If dt.Rows.Count > 0 Then
                        grdPrograms.DataSource = dt
                        grdPrograms.DataBind()
                    Else
                        grdPrograms.DataSource = Nothing
                        grdPrograms.DataBind()  ' Clear the GridView if no data
                    End If
                End Using
            End Using
        End Using

        'Calling additional check methods
        Checktimetable()
        checkSectionadd()
        CheckSubjectadd()
        checkfacultyassign()
        'checkexamstructure()
        CheckClassTeacher()
        'checkexam()
        'Checkplacement()
        checkstudent()
        Checktimetable()
    End Sub
    Protected Sub GridView1_RowCommandd(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName = "AddSection" Then
            ' Optionally, you can use e.CommandArgument to get the row index if needed.
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            ' Redirect to the AddSection page.
            Response.Redirect("AddSection.aspx")
        End If
    End Sub


    Protected Sub grdPrograms_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdPrograms.RowCommand
        If e.CommandName = "CourseSubject" Then
            grdPrograms.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdPrograms.Rows(rowIndex)
            ViewState("Coursesessionid") = grdPrograms.SelectedDataKey(0)
            Session("Otherid") = row.Cells(2).Text
            Response.Redirect("ProgramSubjectlist.aspx?rid=" & grdPrograms.SelectedDataKey(0) & "&acyr=" & ddlacademicyear.SelectedItem.Text & "&e=" & ViewState("evenodd") & "&u=" & ViewState("Userid") & "&s=" & ViewState("Sessionid") & "&ay=" & ViewState("ayid"))
        End If
        If e.CommandName = "Subjectmapping" Then
            grdPrograms.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdPrograms.Rows(rowIndex)
            ViewState("Coursesessionid") = grdPrograms.SelectedDataKey(0)
            Session("Otherid") = row.Cells(2).Text
            Response.Redirect("SubjectToProgram.aspx?rid=" & grdPrograms.SelectedDataKey(0) & "&acyr=" & ddlacademicyear.SelectedItem.Text & "&s=" & ViewState("Sessionid") & "&e=" & ViewState("evenodd") & "&u=" & ViewState("Userid") & "&ay=" & ViewState("ayid"))
        End If

        If e.CommandName = "AddSection" Then
            grdPrograms.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdPrograms.Rows(rowIndex)
            ViewState("Coursesessionid") = grdPrograms.SelectedDataKey(0)
            Session("Otherid") = row.Cells(2).Text

            ' Assuming row.Cells(3).Text contains the Class ID
            Dim selectedClassId As String = row.Cells(3).Text

            ' Redirect to the AddSection.aspx page with the selected class ID as a query parameter
            Response.Redirect("AddSection.aspx?rid=" & grdPrograms.SelectedDataKey(0) & _
                              "&acyr=" & ddlacademicyear.SelectedItem.Text & _
                              "&s=" & ViewState("Sessionid") & _
                              "&e=" & ViewState("evenodd") & _
                              "&u=" & ViewState("Userid") & _
                              "&ay=" & ViewState("ayid") & _
                              "&classid=" & selectedClassId)
        End If

        


        If e.CommandName = "AssignFaculty" Then
            grdPrograms.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdPrograms.Rows(rowIndex)
            ViewState("Coursesessionid") = grdPrograms.SelectedDataKey(0)
            Session("Otherid") = row.Cells(2).Text
            Response.Redirect("AssignFaculty.aspx?rid=" & grdPrograms.SelectedDataKey(0) & "&acyr=" & ddlacademicyear.SelectedItem.Text & "&e=" & ViewState("evenodd") & "&u=" & ViewState("Userid") & "&s=" & ViewState("Sessionid") & "&ay=" & ViewState("ayid"))
        End If
        If e.CommandName = "ExamStructure" Then
            grdPrograms.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdPrograms.Rows(rowIndex)
            Session("Coursesessionid") = grdPrograms.SelectedDataKey(0)
            Session("Otherid") = row.Cells(2).Text
            Session("Course") = row.Cells(3).Text
            Response.Redirect("ExProStru.aspx?rid=" & grdPrograms.SelectedDataKey(0) & "&acyr=" & ddlacademicyear.SelectedItem.Text & "&e=" & ViewState("evenodd") & "&u=" & ViewState("Userid") & "&s=" & ViewState("Sessionid") & "&ay=" & ViewState("ayid"))
        End If
        If e.CommandName = "Exam" Then
            grdPrograms.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdPrograms.Rows(rowIndex)
            ViewState("Coursesessionid") = grdPrograms.SelectedDataKey(0)
            Session("Courseid") = row.Cells(2).Text
            Session("Program") = row.Cells(3).Text
            Response.Redirect("CreatedExams.aspx?rid=" & grdPrograms.SelectedDataKey(0) & "&acyr=" & ddlacademicyear.SelectedItem.Text & "&e=" & ViewState("evenodd") & "&u=" & ViewState("Userid") & "&s=" & ViewState("Sessionid") & "&prog=" & Session("Program") & "&ay=" & ViewState("ayid"))
        End If
        If e.CommandName = "Aassignment" Then
            grdPrograms.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdPrograms.Rows(rowIndex)
            ViewState("Coursesessionid") = grdPrograms.SelectedDataKey(0)
            Session("Courseid") = row.Cells(2).Text
            Session("Program") = row.Cells(3).Text
            Response.Redirect("AssignProgram.aspx?rid=" & grdPrograms.SelectedDataKey(0) & "&acyr=" & ddlacademicyear.SelectedItem.Text & "&e=" & ViewState("evenodd") & "&u=" & ViewState("Userid") & "&s=" & ViewState("Sessionid") & "&prog=" & Session("Program") & "&ay=" & ViewState("ayid"))
        End If
        If e.CommandName = "Timetable" Then
            grdPrograms.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdPrograms.Rows(rowIndex)
            ViewState("Coursesessionid") = grdPrograms.SelectedDataKey(0)
            Session("Courseid") = row.Cells(2).Text

            Response.Redirect("../Attendance/Timetable.aspx?rid=" & grdPrograms.SelectedDataKey(0) & "&s=" & ViewState("Sessionid") & "&cid=" & Session("Courseid") & "&acyr=" & ddlacademicyear.SelectedItem.Text & "&e=" & ViewState("evenodd") & "&u=" & ViewState("Userid") & "&ay=" & ViewState("ayid"))
        End If
        If e.CommandName = "ApproveLeave" Then
            grdPrograms.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdPrograms.Rows(rowIndex)
            ViewState("Coursesessionid") = grdPrograms.SelectedDataKey(0)
            Session("Courseid") = row.Cells(2).Text

            Response.Redirect("../Attendance/leaveApproval.aspx?rid=" & grdPrograms.SelectedDataKey(0) & "&s=" & ViewState("Sessionid") & "&cid=" & Session("Courseid") & "&acyr=" & ddlacademicyear.SelectedItem.Text & "&e=" & ViewState("evenodd") & "&u=" & ViewState("Userid") & "&ay=" & ViewState("ayid"))
        End If
        If e.CommandName = "AddClassTeacher" Then
            grdPrograms.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdPrograms.Rows(rowIndex)
            ViewState("Coursesessionid") = grdPrograms.SelectedDataKey(0)
            Session("Otherid") = row.Cells(2).Text

            Dim selectedClassId As String = row.Cells(3).Text
            ' Redirect with a success flag (e.g., success=1) after the teacher is assigned
            Response.Redirect("AssignClassTeacher.aspx?rid=" & grdPrograms.SelectedDataKey(0) & _
                              "&acyr=" & ddlacademicyear.SelectedItem.Text & _
                              "&s=" & ViewState("Sessionid") & _
                              "&e=" & ViewState("evenodd") & _
                              "&u=" & ViewState("Userid") & _
                              "&ay=" & ViewState("ayid") & _
                              "&classid=" & selectedClassId & _
                              "&success=1")
        End If

        If e.CommandName = "Feedback" Then
            grdPrograms.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdPrograms.Rows(rowIndex)
            ViewState("Coursesessionid") = grdPrograms.SelectedDataKey(0)
            Session("Courseid") = row.Cells(2).Text

            Response.Redirect("Facultyfeedback.aspx?rid=" & grdPrograms.SelectedDataKey(0) & "&s=" & ViewState("Sessionid") & "&cid=" & Session("Courseid") & "&acyr=" & ddlacademicyear.SelectedItem.Text & "&e=" & ViewState("evenodd") & "&u=" & ViewState("Userid") & "&ay=" & ViewState("ayid"))
        End If

        If e.CommandName = "Studentlist" Then
            grdPrograms.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdPrograms.Rows(rowIndex)
            ViewState("Academicyear") = ddlacademicyear.SelectedItem.Text
            ViewState("Coursesessionid") = row.Cells(2).Text
            Session("Courseid") = row.Cells(2).Text
            Response.Redirect("../StudentMis/StudentList.aspx?rid=" & Session("Courseid") & "&acyr=" & ddlacademicyear.SelectedItem.Text & "&e=" & ViewState("evenodd") & "&u=" & ViewState("Userid") & "&s=" & Request.QueryString("s") & "&ay=" & ViewState("ayid"))
        End If


        If e.CommandName = "Placement" Then
            grdPrograms.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdPrograms.Rows(rowIndex)
           
            Session("Courseid") = row.Cells(2).Text
            Response.Redirect("../Placement/PlProgramList.aspx?rid=" & Session("Courseid") & "&acyr=" & ddlacademicyear.SelectedItem.Text & "&e=" & ViewState("evenodd") & "&u=" & ViewState("Userid") & "&s=" & ViewState("Sessionid") & "&ay=" & ViewState("ayid"))
        End If

    End Sub

    Private Sub BindDropDownList1()
        Dim query As String = "select AcademicYear from Exam_Session"
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
    Private Sub checkSectionadd()
        For Each row As GridViewRow In grdPrograms.Rows
            Dim Courseid As String = row.Cells(2).Text
            Dim query As String = "select Count(*) as Count from sectionAssign where Classid='" & Courseid & "'"
            Checkinggrid(query, TryCast(row.FindControl("addSectionLink"), LinkButton))
        Next
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

    Private Sub Checktimetable()
        For Each row As GridViewRow In grdPrograms.Rows
            Dim Courseid As String = row.Cells(2).Text
            Dim query As String = "select Count(*) as Count from timetable where Courseid='" & Courseid & "' and Sessionid='" & ViewState("Sessionid") & "'"
            Checkinggrid(query, TryCast(row.FindControl("timetbllink"), LinkButton))
        Next
    End Sub
    Private Sub CheckClassTeacher()
        For Each row As GridViewRow In grdPrograms.Rows
            Dim Courseid As String = row.Cells(2).Text
            Dim query As String = "select Count(*) as Count from classTeacherAssign where Classid='" & Courseid & "'"
            Checkinggrid(query, TryCast(row.FindControl("addClassTeacher"), LinkButton))
        Next
    End Sub
  

    Private Sub Checkplacement()
        For Each row As GridViewRow In grdPrograms.Rows
            Dim Courseid As String = row.Cells(2).Text
            Dim query As String = "select Count(*) as Count from Pl_CourseEligibilty where Courseid='" & Courseid & "' and Sessionid='" & ViewState("Sessionid") & "'"
            Checkinggrid(query, TryCast(row.FindControl("assignlinplace"), LinkButton))
        Next
    End Sub

    Private Sub checkstudent()
        For Each row As GridViewRow In grdPrograms.Rows
            Dim Courseid As String = row.Cells(2).Text
            Dim query As String = "select Count(*) as Count from Student where Courseid='" & Courseid & "' and Sessionid='" & ViewState("Sessionid") & "'"
            Checkinggrid(query, TryCast(row.FindControl("namelinkes"), LinkButton))
        Next
    End Sub

    Private Sub findyearfromsession()
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim sql As String = "Select * from Exam_Session where Sessionid='" & ViewState("Sessionid") & "'"

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

    Private Sub ReadData()

        Dim query As String = " Select Cs.Coursesessionid, ce.Course,ce.Coursecode,ce.Courseid, Cs.Coursetype, Cs.Courselevel, Cs.NoofSeat,  tbl.Total, Cs.Duration,T.Admission, T.CourseID, " & _
 " case when Cs.Coursetype like '%year%' then Cast(Cs.Duration*1 as nvarchar(20))+' '+Cs.coursetype when Cs.Coursetype like '%sem%' then " & _
" Cast(Cs.Duration*2 as nvarchar(20))+' '+Cs.coursetype end as 'Total Sem/Year' from  Exam_CourseSession Cs join " & _
" (Select Count(St.Studentid) as 'Admission' , Cs.CourseID from Student St right join Exam_CourseSession Cs on " & _
" St.CourseID=Cs.CourseId group by Cs.CourseID ) T on Cs.Courseid =T.CourseID join Exam_COurse ce on cs.COurseid=ce.Courseid " & _
" join (Select Cs.Academicyear, Cs.Courseid, Count(stn.StudentID ) as 'Total' from " & _
" (select  c.Cid , 1 as sno,  i.Institue as Institute ,c.Courseid as Courseid,  s.StudentID , s.student,seat.Seat from student s left join Exam_Course c on s.Courseid =c.CourseID left join Institue i on i.InstitueID=c.Cid left join studentyear sy on s.StudentID= sy.StudentID left join Seat seat on seat.SeatID=sy.seatid where sy.ayid='" & Request.QueryString("ay") & "' and  sy.StudentID  is not null and sy.Isstruckoff=0 ) stn right join Exam_CourseSession Cs on stn.CourseID =Cs.Courseid " & _
" where Cs.SessionId ='" + ViewState("Sessionid") + "' and Cs.Academicyear='" & ddlacademicyear.SelectedItem.Text & "' group by Cs.Courseid ,Cs.Academicyear ) " & _
" tbl on Cs.Courseid =tbl.Courseid " & _
 " where cs.Academicyear='" + ddlacademicyear.SelectedItem.Text + "' and Sessionid='" & ViewState("Sessionid") & "' order by ce.Course "

        Dim cmd As SqlCommand = New SqlCommand(query)
        Using con As SqlConnection = New SqlConnection(constr)
            Using sda As SqlDataAdapter = New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                Using dt As DataTable = New DataTable()
                    sda.Fill(dt)
                    GridView1.DataSource = dt
                    GridView1.DataBind()
                End Using
            End Using
        End Using
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    Protected Sub Download_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Download.Click
        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment;filename=Programinfo.xls")
        Response.ContentType = "application/vnd.ms-excel"
        Dim sw As StringWriter = New StringWriter()
        Dim htm As HtmlTextWriter = New HtmlTextWriter(sw)
        GridView1.AllowPaging = False
        Me.ReadData()
        GridView1.RenderControl(htm)
        Response.Write(sw.ToString())
        Response.End()
    End Sub
    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        ' Check if the CommandName is "AddSection"
        If e.CommandName = "AddSection" Then
            ' Retrieve the row index stored in CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)

            ' Optionally, retrieve data from the row if needed
            Dim row As GridViewRow = GridView1.Rows(rowIndex)

            ' Redirect to AddSection.aspx
            Response.Redirect("AddSection.aspx")

        ElseIf e.CommandName = "Subjectmapping" Then
            ' Handle other commands, e.g., Subjectmapping
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            ' Redirect to another page or handle accordingly
            Response.Redirect("SubjectMapping.aspx")
        End If
    End Sub
End Class
