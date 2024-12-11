Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Partial Class ExaminationNEW11_AssignFaculty
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
    Private cmd As dbnew = New dbnew()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            ViewState("SessionID") = Request.QueryString("s")
            ViewState("Courseid") = Request.QueryString("rid")
            ViewState("ayid") = Request.QueryString("ay")

            Me.labeldata()
            bindddlcourse()
            Me.BindGridSubjectFaculty()
            Me.BindGridAll1()
            'Me.BindSubject()
            Me.fetchsubjectname()
          


            Lblcoursename.Text = Request.QueryString("rid")



            fetchselectfaculty()
            lbltotalsub.Text = Request.QueryString("acyr")
            BindExcelSubject()

        End If

    End Sub
    Protected Sub ddlsection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlsection.SelectedIndexChanged
        ' Re-bind the GridView when the selected section changes.
        BindGridSubjectFaculty()

    End Sub


    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        grdSubplan.PageIndex = e.NewPageIndex
        Me.BindGridSubjectFaculty()
    End Sub

    Private Sub fillDdlsection(ByVal selectedClassId As Integer)
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("SELECT ClassesID, Code FROM sectionAssign WHERE Classid = @Classid", con)
                cmd.Parameters.AddWithValue("@Classid", selectedClassId)
                Using sda As New SqlDataAdapter(cmd)
                    Using dt As New DataTable()
                        sda.Fill(dt)

                        ' Check if DataTable has records
                        If dt.Rows.Count > 0 Then
                            ddlsection.DataSource = dt
                            ddlsection.DataTextField = "Code"
                            ddlsection.DataValueField = "ClassesID"
                            ddlsection.DataBind()
                            ddlsection.SelectedIndex = 1


                        Else
                            ddlsection.Items.Clear() ' Clear any existing items if no records found
                            ddlsection.Items.Insert(0, New ListItem("No sections available", "0")) ' Optional: message for no sections
                        End If
                    End Using
                End Using
            End Using
        End Using
    End Sub




    Private Sub labeldata()
        Dim ds As New DataSet
        Using con As New SqlConnection(constr)
            con.Open()
            Dim cmd1 As New SqlCommand("select * from Sch_Class")
            Dim da As New SqlDataAdapter(cmd1)
            cmd1.Connection = con
            da.Fill(ds)
            Dim i = ds.Tables(0).Rows.Count()
            If (i > 0) Then
                Lblcourseid.Text = ds.Tables(0).Rows(0)("Classid").ToString()
                Lblcoursecode.Text = ds.Tables(0).Rows(0)("ClassCode").ToString()
            End If
            con.Close()

        End Using

    End Sub

    Private Sub BindGridSubjectFaculty()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim selectedClassId As Integer = Convert.ToInt32(ddlsection.SelectedValue)

                Dim sql As String = " Select Csub.Coursesubid, Csub.Academicyear, Csub.Coursesessionid, Csub.Courseid, Csub.Subjectid, Sub.Subject, " & _
" Sub.Subjectcode, Sub.Subprefix, sec.Classesid, Cls.Code as 'Section' from Exam_Coursesubject Csub join Exam_Subject Sub " & _
" on Csub.Subjectid =sub.Subjectid left join (Select Distinct s.Classesid from Student s join studentyear sy on s.studentid = sy.studentid where sy.sessionid='" & ViewState("SessionID") & "' and  s.courseid='" & ddlCourse.SelectedValue & "' )sec " & _
"  on Csub.Sessionid='" & ViewState("SessionID") & "' and  Csub.Courseid='" & ddlCourse.SelectedValue & "' " & _
 " left join Classes Cls on sec.Classesid =Cls.ClassesID Where Csub.Sessionid ='" & ViewState("SessionID") & "' and Csub.Courseid='" & ddlCourse.SelectedValue & "' " & _
"  order by Section "

                cmd.CommandText = Sql
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        grdSubplan.DataSource = dt
                        grdSubplan.DataBind()
                    End Using
                End Using
            End Using
        End Using

        fetchselectfaculty()
    End Sub


    Private Sub BindGridSectionwise()


        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Csub.Coursesubid, Csub.Academicyear, Csub.Coursesessionid, Csub.Courseid, Csub.Semyear, Csub.Subjectid, Sub.Subject, " & _
" Sub.Subjectcode, Sub.Subprefix, sec.Classesid, Cls.Code as 'Section' from Exam_Coursesubject Csub join Exam_Subject Sub " & _
" on Csub.Subjectid = sub.Subjectid left join (Select Distinct Classesid from Student where sessionid='" & ViewState("SessionID") & "' and courseid='" & ddlCourse.SelectedValue & "' " & _
" ) sec on Csub.Sessionid='" & ViewState("SessionID") & "' and Csub.Courseid='" & ddlCourse.SelectedValue & "' " & _
" left join Classes Cls on sec.Classesid = Cls.ClassesID Where Csub.Sessionid ='" & ViewState("SessionID") & "' and Csub.Courseid='" & ddlCourse.SelectedValue & "' " & _
" and sec.Classesid='" & ddlsection.SelectedValue & "' order by Cls.Code, Sub.Subject"
)
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        grdSubplan.DataSource = dt
                        grdSubplan.DataBind()
                    End Using
                End Using
            End Using
        End Using
        fetchselectfaculty()
    End Sub
    Private Sub fetchcountsubject()
        Dim ds As New DataSet
        Using con As New SqlConnection(constr)
            con.Open()
            Dim cmd1 As New SqlCommand(" Select Count(Cs.Subjectid) as 'TotalSubject' from Exam_Coursesubject Cs where Cs.Academicyear='" & Request.QueryString("acyr") & "' and Cs.Courseid ='" & Lblcourseid.Text & "' ", con)
            Dim da As New SqlDataAdapter(cmd1)
            cmd1.Connection = con
            da.Fill(ds)
            Dim i = ds.Tables(0).Rows.Count()
            If (i > 0) Then
                ' lbltotalsub.Text = ds.Tables(0).Rows(0)("TotalSubject").ToString()

            End If
            con.Close()

        End Using

    End Sub
    Private Sub fetchsubjectname()
        Dim ds As New dataset
        Using con As New sqlconnection(constr)
            con.open()
            Dim cmd1 As New SqlCommand(" Select Cs.Academicyear, Cs.Courseid, C.Course, C.Coursecode, Cs.Duration,  Case When Cs.Coursetype like '%Year%' then 'Year' when Cs.Coursetype like '%Sem%' then 'Semester' end as 'Coursetype', Cs.courselevel, " & _
" Case When Cs.Coursetype like '%Year%' then Cs.Duration*1 when Cs.Coursetype like '%Sem%' then Cs.Duration*2 end as 'Semyear' from Exam_CourseSession Cs " & _
" Join Exam_Course C on Cs.Courseid=C.Courseid Where Academicyear='" & Request.QueryString("acyr") & "' And Cs.Courseid='" & Lblcourseid.Text & "'", con)
            Dim da As New sqldataadapter(cmd1)
            cmd1.connection = con
            da.fill(ds)
            Dim i = ds.tables(0).rows.count()
            If (i > 0) Then
                Lblcoursename.Text = ds.Tables(0).Rows(0)("Course").ToString()
                Lblcoursecode.Text = ds.Tables(0).Rows(0)("Coursecode").ToString()
                Lblcourseid.Text = ds.Tables(0).Rows(0)("Courseid").ToString()

                Lblsemyearadd.Text = ds.Tables(0).Rows(0)("Coursetype").ToString()
            End If
            con.close()
        End Using

    End Sub

    Private Sub BindSubject()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Csub.Coursesubid, Csub.Academicyear, Csub.Coursesessionid, Csub.Courseid, Csub.Semyear, Csub.Subjectid, Subp.Subplanid, Sub.Subject, " & _
" Sub.Subjectcode, Sub.Subprefix, subp.Totalunit, subp.Totallecture from Exam_Coursesubject Csub join Exam_Subject Sub on Csub.Subjectid = sub.Subjectid join " & _
" Exam_SubjectPlan Subp on Csub.Coursesubid = Subp.Coursesubid Where Csub.Courseid='" & Lblcourseid.Text & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        grdSubjectList.DataSource = dt
                        grdSubjectList.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub



    Protected Sub btnAddProgram_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddProgram.Click
        PanelSubjectList.Visible = True
        PanelCourseWise.Visible = False
        PanelUnitNameDescription.Visible = False
    End Sub

    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click

        Response.Redirect("Dashboard.aspx?s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u") & "&ay=" & ViewState("ayid"))
    End Sub

    Protected Sub backtocourseSubject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backtocourseSubject.Click
        PanelCourseWise.Visible = True
        PanelUnitNameDescription.Visible = False
        PanelSubjectList.Visible = False
    End Sub

    Protected Sub backtoprogram_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backtoprogram.Click
        PanelCourseWise.Visible = True
        PanelSubjectList.Visible = False
        PanelUnitNameDescription.Visible = False
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim Subjectid As Integer = 0
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Addsubject", con)
                cmd.CommandType = CommandType.StoredProcedure
                Using sda As New SqlDataAdapter()
                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    cmd.Parameters.AddWithValue("@Dated", Date.Now)
                    cmd.Parameters.AddWithValue("@Subject ", txtsub.Text)
                    cmd.Parameters.AddWithValue("@Subjectcode ", txtcode.Text)
                    cmd.Parameters.AddWithValue("@Subprefix ", txtprefix.Text)
                    cmd.Connection = con
                    con.Open()
                    Subjectid = cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using

            Select Case Subjectid
                Case -1
                    MsgBox(Me, "Subject Name Already Exist")
                    Exit Select

                Case -2
                    MsgBox(Me, "Subject Code Already Exist")
                    Exit Select

                Case -3
                    MsgBox(Me, "Prefix Already Exist")
                    Exit Select

                Case Else
                    MsgBox(Me, "Subject Add successful")
                    Exit Select
            End Select
            txtsub.Text = ""
            txtcode.Text = ""
            txtprefix.Text = ""
        End Using

    End Sub


    Private Sub BindUnitName()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(" Select Su.Subunitid, su.Academicyear,Cs.Coursesubid, Cs.Courseid, Cs.Semyear, Cs.Subjectid, sub.Subject, Su.UnitName, " & _
" Su.Description, Su.Timeduration  from Exam_SubjectUnit Su join Exam_Coursesubject Cs on Su.Coursesubid =Cs.Coursesubid " & _
" join Exam_Subject sub on Cs.Subjectid =sub.Subjectid Where Su.Academicyear ='" & Request.QueryString("acyr") & "' and Cs.Courseid='" & Lblcourseid.Text & "' and Cs.Semyear='1' " & _
 " and Cs.Subjectid='" & ViewState("subid") & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        GridUnitDec.DataSource = dt
                        GridUnitDec.DataBind()
                    End Using
                End Using
            End Using
        End Using
        'lblName.Text = "Name is : <b> " & name & "</b>"
    End Sub

    Protected Sub grdSubplan_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSubplan.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim ddlfaculties As DropDownList = CType(e.Row.FindControl("ddlfaculty"), DropDownList)
            Dim selectedClassId As String = ddlsection.SelectedValue
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand(" Select EmployeeID, Employee from P_Employee Where Employee is not null and TeachingStaff='1' order by Employee ")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            ddlfaculties.DataSource = dt
                            ddlfaculties.DataTextField = "Employee"
                            ddlfaculties.DataValueField = "EmployeeID"
                            ddlfaculties.DataBind()
                            ddlfaculties.Items.Insert(0, New ListItem("Select", "0"))
                        End Using
                    End Using
                End Using
            End Using


        End If

    End Sub


    'Protected Sub ddlSemyearsub_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSemyearsub.SelectedIndexChanged
    '    If ddlSemyearsub.SelectedItem.Text = "All" Then
    '        BindGridAll1()
    '    Else

    '    End If
    'End Sub

    Private Sub BindGridAll1()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("select * from Exam_Subject where Subjectid in ( select Subjectid from Exam_Coursesubject where Subjectid not in (select Subjectid from Exam_Coursesubject where Sessionid='" & Request.QueryString("s") & "' and Courseid='" & Lblcourseid.Text & "'))")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        grdSubjectList.DataSource = dt
                        grdSubjectList.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub



    Protected Sub btnSaveSubjects_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveSubjects.Click
        Dim facultyid As String = String.Empty
        Dim Coursesubid As String = String.Empty
        Dim Academicyear As String = String.Empty
        Dim Classid As String = String.Empty
        Dim Subjectid As String = String.Empty

        ' Loop through each row in the GridView
        For Each row As GridViewRow In grdSubplan.Rows
            facultyid = CType(row.FindControl("ddlfaculty"), DropDownList).SelectedItem.Value
            Dim selectedClassid As String = ddlsection.SelectedValue

            ' Only proceed if a valid faculty (not "0") is selected
            If facultyid <> "0" Then
                Coursesubid = row.Cells(6).Text
                Academicyear = row.Cells(2).Text
                Classid = row.Cells(3).Text
                Subjectid = row.Cells(1).Text

                ' Check if the faculty assignment exists for this course and subject
                If IsFacultyAssigned(Coursesubid, Academicyear, Subjectid) Then
                    ' If faculty assignment exists, update the faculty
                    updatefaculty(facultyid, Classid, Academicyear, Subjectid, Coursesubid, selectedClassid)
                Else
                    ' If no assignment exists, insert a new faculty assignment
                    insertvalue(facultyid, Classid, Academicyear, Subjectid, Coursesubid, selectedClassid)
                End If
            End If
        Next

        ' Removed alert message
        ' SaralMsg.Messagebx.Alert(Me, "Assigned Successfully.")

        ' Re-bind the grid to reflect any changes
        Me.BindGridSubjectFaculty()

        ' Trigger success alert using JavaScript
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "showCustomAlert('Successfully Submitted.');", True)
    End Sub

    Private Function IsFacultyAssigned(ByVal Coursesubid As String, ByVal Academicyear As String, ByVal Subjectid As String) As Boolean
        Dim sectionID As String = ddlsection.SelectedValue
        Using con As New SqlConnection(constr)
            ' Query to check if the faculty is already assigned for the given course and subject
            Dim query As String = "SELECT COUNT(1) FROM Exam_SubjectPlan WHERE Coursesubid = @Coursesubid AND AcademicYear = @Academicyear AND Subjectid = @Subjectid And @ClassesID=Classesid"

            Using cmd As New SqlCommand(query, con)
                ' Add parameters to prevent SQL injection
                cmd.Parameters.AddWithValue("@Coursesubid", Coursesubid)
                cmd.Parameters.AddWithValue("@Academicyear", Academicyear)
                cmd.Parameters.AddWithValue("@Subjectid", Subjectid)
                cmd.Parameters.AddWithValue("@ClassesID", sectionID)

                con.Open()
                ' Return True if a record exists, otherwise False
                Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using
        End Using
    End Function




    Private Sub insertvalue(ByVal facultyid As String, ByVal Classid As String, ByVal Academicyear As String, ByVal Subjectid As String, ByVal Coursesubid As String, ByVal selectedClassid As String)
        Using con As New SqlConnection(constr)
            Dim query As String = "INSERT INTO Exam_SubjectPlan (Dated, AcademicYear, Courseid, Subjectid, Coursesubid, Facultyid, Classesid) " & _
                                  "VALUES (GETDATE(), @Academicyear, @Courseid, @Subjectid, @Coursesubid, @Facultyid, @Classesid)"
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@Academicyear", Academicyear)
                cmd.Parameters.AddWithValue("@Courseid", Classid)
                cmd.Parameters.AddWithValue("@Subjectid", Subjectid)
                cmd.Parameters.AddWithValue("@Coursesubid", Coursesubid)
                cmd.Parameters.AddWithValue("@Facultyid", facultyid)
                cmd.Parameters.AddWithValue("@Classesid", selectedClassid) ' Corrected line
                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub



    Private Sub checktimetable()

        Dim sql As String
        'sql = " select  cls.Code as sec, b.batch,CONVERT(varchar,dated,103) as dated,* from student Inner Join Course on Course.CourseID=Student.CourseID where  " & sid
        sql = "Select * from timetable where subjectid = '" & ViewState("Subject") & "' "



        Dim dt As DataTable = cmd.getDataTable(sql)
        If dt.Rows.Count > 0 Then

            SaralMsg.Messagebx.Alert(Me, "Timetable for this Subject Created Already.")
        Else
            ViewState("Delete") = "Delete"
        End If

    End Sub

    Private Sub updatefaculty(ByVal facultyid As String, ByVal Classid As String, ByVal Academicyear As String, ByVal Subjectid As String, ByVal Coursesubid As String, ByVal selectedClassid As String)
        Using con As New SqlConnection(constr)
            Dim query As String = "UPDATE Exam_SubjectPlan SET Facultyid = @Facultyid, Classesid=@Classesid WHERE Coursesubid = @Coursesubid AND AcademicYear = @Academicyear AND Subjectid = @Subjectid"
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@Facultyid", facultyid)
                cmd.Parameters.AddWithValue("@Coursesubid", Coursesubid)
                cmd.Parameters.AddWithValue("@Academicyear", Academicyear)
                cmd.Parameters.AddWithValue("@Subjectid", Subjectid)
                cmd.Parameters.AddWithValue("@Classesid", selectedClassid)
                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub





    Private Sub Messagepop(ByVal p1 As String)
        Dim message As String = p1
        Dim script As String = "window.onload=function(){alert('"
        script &= message
        script &= "');"


        script &= "; }"
        ClientScript.RegisterStartupScript(Me.GetType(), "SuccessMessage", script, True)
    End Sub

    Private Sub fetchselectfaculty()
        For Each row As GridViewRow In grdSubplan.Rows
            Dim tfaculty As DropDownList = TryCast(row.FindControl("ddlfaculty"), DropDownList)
            Dim Academicyear As String = row.Cells(2).Text
            Dim Courseid As String = row.Cells(3).Text
            Dim Subjectid As String = row.Cells(1).Text
            Dim Coursesubid As String = row.Cells(6).Text
            Dim classesid As String = ddlsection.SelectedValue
            fetchfaculty(tfaculty, Courseid, Academicyear, Subjectid, Coursesubid, classesid)
        Next
    End Sub

    Private Sub fetchfaculty(ByVal tfaculty As DropDownList, ByVal Courseid As String, ByVal Academicyear As String, ByVal Subjectid As String, ByVal CourseSubid As String, ByVal Classesid As String)

        Try


            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select Facultyid from Exam_SubjectPlan  where Academicyear='" + Academicyear + "' and Courseid='" + Courseid + "' and Subjectid='" + Subjectid + "' and Coursesubid='" + CourseSubid + "' and Classesid='" + Classesid + "' ")
                    Using sda As New SqlDataAdapter()
                        Dim dt As New DataTable()

                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        sda.Fill(dt)
                        con.Open()
                        tfaculty.SelectedIndex = tfaculty.Items.IndexOf(tfaculty.Items.FindByValue(dt.Rows(0)("Facultyid")))
                        con.Close()
                    End Using

                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnmapsubject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnmapsubject.Click

    End Sub


    Private Sub bindddlcourse()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("SELECT * FROM Sch_Class", con)
                Using sda As New SqlDataAdapter(cmd)
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        ddlCourse.DataSource = dt
                        ddlCourse.DataTextField = "ClassName"
                        ddlCourse.DataValueField = "Classid"
                        ddlCourse.DataBind()

                        ' Call labeldata() only after setting DataSource
                        labeldata()

                        ' Check if ViewState("Courseid") is set from Request.QueryString
                        If ViewState("Courseid") Is Nothing Then
                            ViewState("Courseid") = Request.QueryString("rid")
                        End If

                        ' If ViewState("Courseid") exists, set it as the zeroth index item
                        If ViewState("Courseid") IsNot Nothing Then
                            Dim courseId As String = ViewState("Courseid").ToString()
                            Dim item As ListItem = ddlCourse.Items.FindByValue(courseId)
                            If item IsNot Nothing Then
                                ddlCourse.Items.Remove(item)
                                ddlCourse.Items.Insert(0, item)
                                ddlCourse.SelectedIndex = 0
                            End If
                        End If
                    End Using
                End Using
            End Using
        End Using

        ' Call fillDdlsection for the initially selected class
        If ddlCourse.SelectedValue IsNot Nothing Then
            fillDdlsection(Convert.ToInt32(ddlCourse.SelectedValue))
        End If
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

    Protected Sub ddlCourse_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCourse.SelectedIndexChanged
        ' Set the course ID label based on the selected course
        Lblcourseid.Text = ddlCourse.SelectedItem.Value
        'Ddlsemyear.Items.Clear() ' Clear the semester year dropdown

        ' Fetch the courses and subjects based on the selected course
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("SELECT Csub.Coursesubid, Csub.Academicyear, Csub.Coursesessionid, Csub.Courseid, Csub.Semyear, Csub.Subjectid, Sub.Subject, " &
                                          "Sub.Subjectcode, Sub.Subprefix, sec.Classesid, Cls.Code as 'Section' FROM Exam_Coursesubject Csub " &
                                          "JOIN Exam_Subject Sub ON Csub.Subjectid = Sub.Subjectid " &
                                          "LEFT JOIN (SELECT DISTINCT Classesid FROM Student WHERE sessionid = @SessionID AND courseid = @CourseID) sec " &
                                          "ON Csub.Sessionid = @SessionID AND Csub.Courseid = @CourseID " &
                                          "LEFT JOIN Classes Cls ON sec.Classesid = Cls.ClassesID " &
                                          "WHERE Csub.Sessionid = @SessionID AND Csub.Courseid = @CourseID " &
                                          "ORDER BY Csub.Semyear", con)

                ' Use parameters to prevent SQL injection
                cmd.Parameters.AddWithValue("@SessionID", ViewState("SessionID"))
                cmd.Parameters.AddWithValue("@CourseID", ddlCourse.SelectedValue)

                Using sda As New SqlDataAdapter(cmd)
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        grdSubplan.DataSource = dt
                        grdSubplan.DataBind()
                    End Using
                End Using
            End Using
        End Using

        fillDdlsection(Convert.ToInt32(ddlCourse.SelectedValue))

        ' Fetch other necessary data
        fetchselectfaculty()
        fetchcountsubject()
    End Sub




    Private Sub BindExcelSubject()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select isnull(emp.Employee,'-') as Employee, sp.Classesid as classsec,sp.Facultyid, tbl1.* from Exam_SubjectPlan Sp join ( " & _
" Select Csub.Coursesubid, Csub.Academicyear, Csub.Coursesessionid, Csub.Courseid, Csub.Semyear, Csub.Subjectid, Sub.Subject, " & _
" Sub.Subjectcode, Sub.Subprefix, sec.Classesid, Cls.Code as 'Section' from Exam_Coursesubject Csub join Exam_Subject Sub " & _
" on Csub.Subjectid =sub.Subjectid left join (Select Distinct s.Classesid from Student s join studentyear sy on s.studentid = sy.studentid and s.sem = sy.sem where sy.sessionid='" & ViewState("SessionID") & "' and  s.courseid='" & ddlCourse.SelectedValue & "' )sec " & _
" on Csub.Sessionid='" & ViewState("SessionID") & "' and  Csub.Courseid='" & ddlCourse.SelectedValue & "' left join Classes Cls on sec.Classesid =Cls.ClassesID " & _
" Where Csub.Sessionid ='" & ViewState("SessionID") & "' and Csub.Courseid='" & ddlCourse.SelectedValue & "') tbl1 on sp.Coursesubid=tbl1.Coursesubid " & _
"  and Sp.Classesid=tbl1.Classesid left join P_Employee Emp on Sp.Facultyid=Emp.EmployeeID where Sp.Sessionid='" & ViewState("SessionID") & "' and sp.Courseid='" & ddlCourse.SelectedValue & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Gridexcelcourse.DataSource = dt
                        Gridexcelcourse.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    Protected Sub Download_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Download.Click
        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment;filename=FacultySubject.xls")
        Response.ContentType = "application/vnd.ms-excel"
        Dim sw As StringWriter = New StringWriter()
        Dim htm As HtmlTextWriter = New HtmlTextWriter(sw)
        Gridexcelcourse.AllowPaging = False

        Gridexcelcourse.RowStyle.HorizontalAlign = HorizontalAlign.Center
        For Each row As GridViewRow In Gridexcelcourse.Rows

        Next

        Gridexcelcourse.RenderControl(htm)
        Response.Write(sw.ToString())
        Response.End()
    End Sub
End Class
