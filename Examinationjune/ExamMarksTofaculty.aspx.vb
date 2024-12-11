Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient

Partial Class Examinationjune_ExamMarksTofaculty
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("courseexamid") = Request.QueryString("courseexamid")
            ViewState("Academicyear") = Request.QueryString("acyr")
            ViewState("Sessionid") = Request.QueryString("s")
            ViewState("ayid") = Request.QueryString("ay")
            ViewState("Courseid") = Request.QueryString("rid")
            ViewState("userid") = Request.QueryString("u")
            'fetchDdlAcademicyear()
            BindCourseExamdropdown()
            If ViewState("courseexamid") <> "" Then

                ddlExamN.Items.FindByValue(ViewState("courseexamid")).Selected = True

            End If

            fetchprogramdetail()
            fetchddlProgram()
            DdlAcademicyear.Items.FindByValue(Lblcourseid.Text).Selected = True


            BindExamtypeCurrentyear()
        End If
    End Sub
    Private Sub BindCourseExamdropdown()
        Dim query As String = ("Select * from Exam_CourseExam where Sessionid='" & ViewState("Sessionid") & "' and Ayid='" & ViewState("ayid") & "'")
        BindDropDownList1(ddlExamN, query, "ExamName", "CourseExamid", "Select Exam Name")

    End Sub
    Private Sub BindDropDownList1(ByVal ddl1 As DropDownList, ByVal query As String, ByVal text As String, ByVal value As String, ByVal defaultText As String)

        Dim cmd As New SqlCommand(query)
        Using con As New SqlConnection(constr)
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                con.Open()
                ddl1.DataSource = cmd.ExecuteReader()
                ddl1.DataTextField = text
                ddl1.DataValueField = value
                ddl1.DataBind()
                con.Close()
            End Using
        End Using
        ddl1.Items.Insert(0, New ListItem(defaultText, "0"))
    End Sub

    Private Sub fetchddlProgram()
        DdlAcademicyear.Items.Clear()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select C.Course, Ce.* from Exam_CourseExam Ce join Exam_Course C on Ce.Courseid=C.Courseid where Ce.CourseExamid='" & ddlExamN.SelectedValue & "' and Ce.Ayid='" & ViewState("ayid") & "' and Ce.SessionId='" & ViewState("Sessionid") & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        DdlAcademicyear.DataSource = dt
                        DdlAcademicyear.DataTextField = "Course"
                        DdlAcademicyear.DataValueField = "Courseid"
                        DdlAcademicyear.DataBind()

                    End Using
                End Using
            End Using
        End Using
        DdlAcademicyear.Items.Insert(0, New ListItem("Select Program", "0"))
    End Sub


    Protected Sub ddlExamN_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlExamN.SelectedIndexChanged
        fetchddlProgram()
        BindExamtypeCurrentyear()
        fetchprogramdetail()
    End Sub

    Private Sub fetchDdlAcademicyear()
        Try
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select * from Exam_CourseExam")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            DdlAcademicyear.DataSource = dt
                            DdlAcademicyear.DataTextField = "Academicyear"
                            DdlAcademicyear.DataValueField = "Academicyear"
                            DdlAcademicyear.DataBind()
                            Dim Year As Integer
                            Year = Convert.ToInt32(Now.ToString("yyyy"))

                            DdlAcademicyear.Items.FindByValue(ViewState("Academicyear")).Selected = True

                        End Using
                    End Using

                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fetchprogramdetail()
        Try

            Dim ds As New DataSet
            Using con As New SqlConnection(constr)
                con.Open()
                Dim cmd1 As New SqlCommand(" Select C.Course, C.Coursecode, Ce.* from Exam_CourseExam Ce join Exam_Course C on Ce.Courseid=C.Courseid " & _
   " where Ce.CourseExamid='" & ddlExamN.SelectedValue & "' and Ce.Sessionid='" & ViewState("Sessionid") & "' and ce.Ayid='" & ViewState("ayid") & "'", con)
                Dim da As New SqlDataAdapter(cmd1)
                cmd1.Connection = con
                da.Fill(ds)
                Dim i = ds.Tables(0).Rows.Count()
                If (i > 0) Then
                    Lblcoursename.Text = ds.Tables(0).Rows(0)("Course").ToString()
                    Lblcoursecode.Text = ds.Tables(0).Rows(0)("Coursecode").ToString()
                    Lblcourseid.Text = ds.Tables(0).Rows(0)("Courseid").ToString()
                    LblcourseExamid.Text = ds.Tables(0).Rows(0)("CourseExamid").ToString()

                End If
                con.Close()

            End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindExamtypeCurrentyear()
        'Try

            Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Sub.Subject,Sub.Subjectcode as 'subcode', C.Course,C.Coursecode, Ce.Courseid,Ce.Semyear, Ce.ExamName,Ce.Examtype, Esub.* from Exam_ExamSubject Esub " & _
"join Exam_CourseExam Ce on Esub.Corseexamid=Ce.CourseExamid join Exam_Course C on Ce.Courseid=C.Courseid join Exam_Subject sub on Esub.Subjectid=sub.Subjectid " & _
"where Esub.Corseexamid='" & ddlExamN.SelectedValue & "' and  Ce.Sessionid='" & ViewState("Sessionid") & "' and ce.Ayid='" & ViewState("ayid") & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        gridexamsubjectlist.DataSource = dt
                        gridexamsubjectlist.DataBind()
                    End Using
                End Using
            End Using
        End Using
        fetchselectfaculty()
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub fetchselectfaculty()
        For Each row As GridViewRow In gridexamsubjectlist.Rows
            Dim tfaculty As DropDownList = TryCast(row.FindControl("ddlfaculty"), DropDownList)
            Dim Academicyear As String = row.Cells(1).Text
            Dim Courseid As String = row.Cells(3).Text
            Dim Subjectid As String = row.Cells(6).Text
            Dim Examsubid As String = row.Cells(5).Text
            Dim Sem As String = row.Cells(4).Text
            fetchfaculty(tfaculty, Courseid, Academicyear, Subjectid, Examsubid, Sem)
        Next
    End Sub

    Private Sub fetchfaculty(ByVal tfaculty As DropDownList, ByVal Courseid As String, ByVal Academicyear As String, ByVal Subjectid As String, ByVal Examsubid As String, ByVal Sem As String)

        Try


            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand(" Select Et.Empid from Exam_Examteacher Et where Academicyear='" & Academicyear & "' " & _
                                           " and Examsubid='" & Examsubid & "' and Courseid='" & Courseid & "' and Semyear='" & Sem & "' and Sujectid='" & Subjectid & "' ")
                    Using sda As New SqlDataAdapter()
                        Dim dt As New DataTable()

                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        sda.Fill(dt)
                        con.Open()
                        If dt.Rows.Count > 0 Then
                            tfaculty.SelectedIndex = tfaculty.Items.IndexOf(tfaculty.Items.FindByValue(dt.Rows(0)("Empid")))

                        End If
                        con.Close()
                    End Using

                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub gridexamsubjectlist_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridexamsubjectlist.RowDataBound
        Try

            If (e.Row.RowType = DataControlRowType.DataRow) Then
                Dim ddlfaculties As DropDownList = CType(e.Row.FindControl("ddlfaculty"), DropDownList)

                Using con As New SqlConnection(constr)
                    Using cmd As New SqlCommand(" Select * from P_Employee order by Employee ")
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

                'Dim country As String = CType(e.Row.FindControl("lblteacherid"), Label).Text
                'Dim ddlhod As DropDownList = CType(e.Row.FindControl("ddlhod"), DropDownList)
                'att.Att_getemprolwiselist(ddlhod)
                'Dim hod As String = CType(e.Row.FindControl("lblhod"), Label).Text
                'saralMastercls.BindDropdown(ddlhod, hod)
            End If

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnsaves_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsaves.Click
        Try


            For Each row As GridViewRow In gridexamsubjectlist.Rows
                Dim Examsubid As String = ""
                Dim Subjectid As String = ""
                Dim examtype As String = ""
                Dim Employeeid As String = ""
                Dim semyear As String = ""
                semyear = row.Cells(4).Text
                Examsubid = row.Cells(5).Text
                Subjectid = row.Cells(6).Text
                examtype = row.Cells(11).Text

                If CType(row.FindControl("ddlfaculty"), DropDownList).SelectedItem.Text = "Select" Then
                    SaralMsg.Messagebx.Alert(Me, "Select faculty")

                Else

                    Employeeid = CType(row.FindControl("ddlfaculty"), DropDownList).SelectedValue
                    InsertexamTeacher(Examsubid, semyear, Subjectid, examtype, Employeeid)
                    SaralMsg.Messagebx.Alert(Me, "Saved successful.")

          
                End If

            Next
            BindExamtypeCurrentyear()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub InsertexamTeacher(ByVal Examsubid As String, ByVal semyear As String, ByVal Subjectid As String, ByVal examtype As String, ByVal Employeeid As String)
        Try
        Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("[Sp_UpdateExamteacher]")
                    Using sda As New SqlDataAdapter()
                        Dim ID As Integer = 0
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@Academicyear", ViewState("Academicyear"))
                        cmd.Parameters.AddWithValue("@Examsubid ", Examsubid)
                        cmd.Parameters.AddWithValue("@courseid ", Lblcourseid.Text)
                        cmd.Parameters.AddWithValue("@Semyear", semyear)
                        cmd.Parameters.AddWithValue("@Subjectid ", Subjectid)
                        cmd.Parameters.AddWithValue("@examtype ", examtype)
                        cmd.Parameters.AddWithValue("@Empid ", Employeeid)
                        cmd.Parameters.AddWithValue("@userid ", Request.QueryString("u"))
                        cmd.Parameters.AddWithValue("@courseexamid ", LblcourseExamid.Text)

                        cmd.Connection = con
                        con.Open()
                        ID = Convert.ToInt32(cmd.ExecuteScalar())
                        'cmd.ExecuteNonQuery()

                        con.Close()
                    End Using
                End Using

            Select Case ID
                    Case -1

                    Case Else
                End Select
        End Using
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
       
        Response.Redirect("CreatedExams.aspx?rid=" & ViewState("Courseid") & "&acyr=" & ViewState("Academicyear") & "&u=" & Request.QueryString("u") & "&s=" & Request.QueryString("s") & "&ay=" & ViewState("ayid"))
    End Sub
End Class
