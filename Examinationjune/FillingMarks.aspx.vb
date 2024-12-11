Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient

Partial Class Examinationjune_FillingMarks
    Inherits System.Web.UI.Page
    
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("Sessionid") = Request.QueryString("s")
            ViewState("ayid") = Request.QueryString("ay")
            ViewState("userid") = Request.QueryString("u")

            fetchDdlAcademicyear()
            BindGridlistExams()
        End If
    End Sub

    Private Sub fetchDdlpnl2academicyear()
        Try
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select * from Exam_CourseExam")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            Ddlpnl2academicyear.DataSource = dt
                            Ddlpnl2academicyear.DataTextField = "Academicyear"
                            Ddlpnl2academicyear.DataValueField = "Academicyear"
                            Ddlpnl2academicyear.DataBind()
                           

                            Ddlpnl2academicyear.Items.FindByValue(ViewState("Acyear")).Selected = True

                        End Using
                    End Using

                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fetchDdlAcademicyear()
        Try
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select * from Exam_Session  where sessionid='" & ViewState("Sessionid") & "'")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            If dt.Rows.Count > 0 Then
                                lblacyr.Text = dt.Rows(0)("Academicyear").ToString
                            End If
                        End Using
                    End Using

                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindGridlistExams()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(" Select C.Course, C.Coursecode, sub.Subject, sub.Subjectcode, Ce.ExamName, Et.* from Exam_Examteacher Et " & _
"  left join Exam_CourseExam Ce on Et.Courseexamid=Ce.CourseExamid " & _
"  Left join Exam_Course C on Et.Courseid =C.Courseid " & _
 " Left join Exam_Subject sub on Et.Sujectid = sub.Subjectid " & _
 " Where Ce.Sessionid='" & ViewState("Sessionid") & "' and Ce.Ayid='" & ViewState("ayid") & "' and Et.Empid='" & ViewState("userid") & "' and Cast(GETDATE() as date) between Ce.MarksEntryFromDate and Ce.MarksEntryToDate")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        GridlistExams.DataSource = dt
                        GridlistExams.DataBind()
                    End Using
                End Using
            End Using
        End Using
        CheckMarksfilling()
    End Sub

    Protected Sub GridlistExams_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridlistExams.RowCommand
        If e.CommandName = "MarksEnter" Then
          
            GridlistExams.SelectedIndex = e.CommandArgument
            backbotton2.Visible = False
            backbotton.Visible = True

            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridlistExams.Rows(rowIndex)
            ViewState("Acyear") = row.Cells(1).Text
            ViewState("Courseid") = row.Cells(3).Text
            ViewState("Program") = row.Cells(5).Text
            ViewState("Subject") = row.Cells(8).Text
            ViewState("Examsubid") = row.Cells(2).Text
            ViewState("semyear") = row.Cells(4).Text
            ViewState("corseExamid") = row.Cells(10).Text
            PnlExamlist.Visible = False
            Pnlstudentmarks.Visible = True
            Lblprogram.Text = ViewState("Program")
            Lblsubject.Text = ViewState("Subject")
            'lblacyr.Text = ViewState("Acyear")
            fetchDdlpnl2academicyear()
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select * from Exam_ExamMarks where Academicyear ='" & ViewState("Acyear") & "' and Examsubid ='" & ViewState("Examsubid") & "' ", con)

                    Dim da As New DataSet
                    Dim ds As New SqlDataAdapter(cmd)
                    ds.Fill(da)
                    Dim i = da.Tables(0).Rows.Count

                    If i > 0 Then
                        btnsaves.Visible = False
                        Pnlstudentmarksinsert.Visible = False
                        Pnlstudentmarksupdate.Visible = True
                        BindGridMarksforupdate()
                    Else
                        Pnlstudentmarksupdate.Visible = False

                        Pnlstudentmarksinsert.Visible = True
                        Pnlstudentmarksupdate.Visible = False
                        btnsaves.Visible = True
                        BindGridstudentmarks()
                    End If

                End Using
            End Using


        End If
    End Sub

    Private Sub BindGridstudentmarks()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Ce.CourseExamid, Ce.Semyear,Ce.ExamName, Ce.Examtype,Et.Examsubid,Et.Sujectid, " & _
 " sub.Subject, sub.Subjectcode,St.* from  Student st inner join StudentYear sy on st.StudentID=sy.StudentID and sy.SEm=st.Sem " & _
  "inner join Exam_CourseExam ce on Ce.Courseid=Sy.CourseID and Ce.Ayid=Sy.ayid and Ce.Semyear=Sy.SEm " & _
  " inner join Exam_Examteacher Et on Et.Courseexamid=Ce.CourseExamid " & _
  " left join Exam_Subject sub on Et.Sujectid=sub.Subjectid " & _
  "where Et.Courseexamid='" & ViewState("corseExamid") & "' and  Et.Examsubid='" & ViewState("Examsubid") & "'  and Et.Empid='" & ViewState("userid") & "' and " & _
  " St.StudentID in (Select Studentid from Exam_Examformstudent where sessionid='" & ViewState("Sessionid") & "' " & _
 " and Ayid='" & ViewState("ayid") & "' and Courseexamid='" & ViewState("corseExamid") & "' and Isexamformverified='1' )")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Gridstudentmarks.DataSource = dt
                        Gridstudentmarks.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub BindGridstudentupdatemarks()
        For Each row As GridViewRow In Gridstudentupdatemarks.Rows
            Dim ExamfillmarksheetId As String = row.Cells(1).Text

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand(" Select Em.ExamfillmarksheetId,  Em.Obtainedmarks, Em.Attendance, Em.paperid, Em.AnswersheetNo,  Ce.CourseExamid, Ce.Semyear,Ce.ExamName, Ce.Examtype,Et.Examsubid,Et.Sujectid, " & _
 " sub.Subject, sub.Subjectcode,St.* from  Student st " & _
 " inner join StudentYear sy on st.StudentID=sy.StudentID and sy.SEm=st.Sem " & _
 " inner join Exam_CourseExam ce on Ce.Courseid=Sy.CourseID and Ce.Ayid=Sy.ayid and Ce.Semyear=Sy.SEm " & _
 " inner join Exam_Examteacher Et on Et.Courseexamid=Ce.CourseExamid " & _
 "  join Exam_ExamMarks Em on Et.Examsubid=Em.Examsubid " & _
 " left join Exam_Subject sub on Et.Sujectid=sub.Subjectid " & _
 " where Et.Courseexamid='" & ViewState("corseExamid") & "' and   Et.Examsubid='" & ViewState("Examsubid") & "' and  Et.Empid='" & ViewState("userid") & "' and " & _
 " St.StudentID in (Select Studentid from Exam_Examformstudent where sessionid='" & ViewState("Sessionid") & "' and Ayid='" & ViewState("ayid") & "' and Courseexamid='" & ViewState("corseExamid") & "' and Isexamformverified='1' ) ")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd

                        Dim dt As New DataTable()
                        sda.Fill(dt)


                        Dim Attendance As String = dt.Rows(0)("Attendance").ToString
                        ' Dim Coursetype As String = dt.Rows(0)("Coursetype").ToString
                        '  Dim Courselevel As String = dt.Rows(0)("courselevel").ToString

                        If Attendance = "" Or Attendance = " " Then

                            TryCast(row.FindControl("ddlattendance"), DropDownList).SelectedItem.Text = dt.Rows(0)("Attendance").ToStringng

                        Else
                            TryCast(row.FindControl("ddlattendance"), DropDownList).Items.FindByText(dt.Rows(0)("Attendance").ToString()).Selected = True
                        End If

                    End Using
                End Using
            End Using
        Next


    End Sub

    Private Sub BindGridMarksforupdate()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Em.ExamfillmarksheetId,  Em.Obtainedmarks, Em.Attendance, Em.paperid, Em.AnswersheetNo,  Ce.CourseExamid, Ce.Semyear,Ce.ExamName, Ce.Examtype,Et.Examsubid,Et.Sujectid, " & _
 " sub.Subject, sub.Subjectcode,St.* from  Student st " & _
 " inner join StudentYear sy on st.StudentID=sy.StudentID and sy.SEm=st.Sem " & _
 " inner join Exam_CourseExam ce on Ce.Courseid=Sy.CourseID and Ce.Ayid=Sy.ayid and Ce.Semyear=Sy.SEm " & _
 " inner join Exam_Examteacher Et on Et.Courseexamid=Ce.CourseExamid " & _
 "  join Exam_ExamMarks Em on Et.Examsubid=Em.Examsubid " & _
 " left join Exam_Subject sub on Et.Sujectid=sub.Subjectid " & _
 " where Et.Courseexamid='" & ViewState("corseExamid") & "' and   Et.Examsubid='" & ViewState("Examsubid") & "' and Et.Empid='" & ViewState("userid") & "' and " & _
 " St.StudentID in (Select Studentid from Exam_Examformstudent where sessionid='" & ViewState("Sessionid") & "' and Ayid='" & ViewState("ayid") & "' and Courseexamid='" & ViewState("corseExamid") & "' and Isexamformverified='1' ) ")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd

                    Dim dt As New DataTable()
                    sda.Fill(dt)

                    Gridstudentupdatemarks.DataSource = dt
                    Gridstudentupdatemarks.DataBind()

                End Using
            End Using
        End Using

        BindGridstudentupdatemarks()
    End Sub
    '    Private Sub gdsauifh()
    '        Using con As New SqlConnection(constr)
    '            Using cmd As New SqlCommand(" Select Em.ExamfillmarksheetId, Em.Academicyear, Em.Examsubid, Em.StudentId, St.AdmissionNo, Strl.Rollno, " & _
    '                                       " Em.Obtainedmarks,Em.Attendance, Em.AnswersheetNo, " & _
    '" Em.paperid, Em.userid from Exam_ExamMarks Em Join Student St on Em.StudentId =St.StudentID join Exam_Studentrollno Strl On St.AdmissionNo =Strl.Admissionno  " & _
    '"  where Em.Academicyear ='" & ViewState("Acyear") & "' and Em.Examsubid ='" & ViewState("Examsubid") & "' and Em.userid ='9221'")
    '                Using sda As New SqlDataAdapter()
    '                    cmd.Connection = con
    '                    sda.SelectCommand = cmd
    '                    Using dt As New DataTable()
    '                        sda.Fill(dt)
    '                        Gridstudentupdatemarks.DataSource = dt
    '                        Gridstudentupdatemarks.DataBind()

    '                        Dim Attendance As String = dt.Rows(0)("Attendance").ToString()
    '                        Dim Paperid As String = dt.Rows(0)("paperid").ToString()
    '                        Dim Answersht As String = dt.Rows(0)("AnswersheetNo").ToString()
    '                        Dim Obtmarks As String = dt.Rows(0)("Obtainedmarks").ToString()
    '                        gridfetchvaluefield(Attendance, Paperid, Answersht, Obtmarks)
    '                    End Using
    '                End Using
    '            End Using
    '        End Using


    '    End Sub

    Protected Sub OnRowEditing(ByVal sender As Object, ByVal f As GridViewEditEventArgs)
        Gridstudentupdatemarks.EditIndex = f.NewEditIndex

        Me.BindGridMarksforupdate()
    End Sub
    Protected Sub OnRowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim row As GridViewRow = Gridstudentupdatemarks.Rows(e.RowIndex)
        Dim ddl As DropDownList = CType(row.FindControl("ddlattendance"), DropDownList)

        TryCast(row.Cells(6).Controls(0), TextBox).ReadOnly = True
        TryCast(row.Cells(7).Controls(0), TextBox).ReadOnly = True
       
        Dim ExamfillmarksheetId As String = row.Cells(1).Text
        Dim Obtmarks As String = TryCast(row.Cells(10).Controls(0), TextBox).Text
        Dim Attendance As String = CType(row.FindControl("ddlattendance"), DropDownList).SelectedItem.Text
        Dim paperid As String = TryCast(row.Cells(8).Controls(0), TextBox).Text
        Dim Answersheetno As String = TryCast(row.Cells(9).Controls(0), TextBox).Text
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("[Sp_UpdateExamMarks]")
                Using sda As New SqlDataAdapter()
                    Dim ID As Integer = 0
                    cmd.CommandType = CommandType.StoredProcedure


                    If Attendance = "Absent" Then
                        TryCast(row.Cells(10).Controls(0), TextBox).ReadOnly = True
                        TryCast(row.Cells(8).Controls(0), TextBox).ReadOnly = True
                        TryCast(row.Cells(9).Controls(0), TextBox).ReadOnly = True

                        cmd.Parameters.AddWithValue("@examfillmarks", ExamfillmarksheetId)

                        cmd.Parameters.AddWithValue("@obmarks", Nothing)
                        cmd.Parameters.AddWithValue("@Attendance", Attendance)
                        cmd.Parameters.AddWithValue("@paperid", Nothing)
                        cmd.Parameters.AddWithValue("@Answersheetno", Nothing)
                        cmd.Parameters.AddWithValue("@userid", ViewState("userid"))
                        cmd.Connection = con
                        con.Open()
                        ID = Convert.ToInt32(cmd.ExecuteScalar())
                        'cmd.ExecuteNonQuery()

                        con.Close()
                    Else
                        cmd.Parameters.AddWithValue("@examfillmarks", ExamfillmarksheetId)

                        cmd.Parameters.AddWithValue("@obmarks", Obtmarks)
                        cmd.Parameters.AddWithValue("@Attendance", Attendance)
                        cmd.Parameters.AddWithValue("@paperid", paperid)
                        cmd.Parameters.AddWithValue("@Answersheetno", Answersheetno)
                        cmd.Parameters.AddWithValue("@userid", ViewState("userid"))
                        cmd.Connection = con
                        con.Open()
                        ID = Convert.ToInt32(cmd.ExecuteScalar())
                        'cmd.ExecuteNonQuery()

                        con.Close()


                    End If

                End Using
            End Using

        End Using


        'Dim Courseid As String = TryCast(row.Cells(2).Controls(0), TextBox).Text
        'Dim Course As String = TryCast(row.Cells(3).Controls(0), TextBox).Text
        'Dim Coursecode As String = TryCast(row.Cells(4).Controls(0), TextBox).Text
        'Dim Courseprefix As String = TryCast(row.Cells(5).Controls(0), TextBox).Text

        'Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
        'Using con As New SqlConnection(constr)
        '    Using cmd As New SqlCommand("UPDATE Exam_Course SET Course=@Course, Coursecode = @Coursecode, Courseprefix = @Courseprefix WHERE Courseid = '" + Courseid + "'")
        '        cmd.Parameters.AddWithValue("@Courseid", Courseid)
        '        cmd.Parameters.AddWithValue("@Course", Course)
        '        cmd.Parameters.AddWithValue("@Coursecode", Coursecode)
        '        cmd.Parameters.AddWithValue("@Courseprefix", Courseprefix)

        '        cmd.Connection = con
        '        con.Open()
        '        cmd.ExecuteNonQuery()
        '        Me.BindGridMarksforupdate()
        '        con.Close()
        '    End Using
        'End Using
        Gridstudentupdatemarks.EditIndex = -1
        Me.BindGridMarksforupdate()
    End Sub
    Protected Sub OnRowCancelingEdit(ByVal sender As Object, ByVal e As EventArgs)
        Gridstudentupdatemarks.EditIndex = -1
        Me.BindGridMarksforupdate()
    End Sub
    Protected Sub OnRowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)

        Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    con.Open()

                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Me.BindGridMarksforupdate()
                        con.Close()
                    End Using
                End Using
            End Using
        End Using

    End Sub
    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow AndAlso e.Row.RowIndex <> Gridstudentupdatemarks.EditIndex Then
            '    TryCast(e.Row.Cells(0).Controls(2), LinkButton).Attributes("onclick") = "return confirm('Do you want to delete this row?');"
        End If
    End Sub

    
    Protected Sub btnsaves_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsaves.Click
        Try
            For Each row As GridViewRow In Gridstudentmarks.Rows
                Dim Studentid As String = row.Cells(3).Text
                Dim Obtmarks As String = TryCast(row.FindControl("txtobtainmarks"), TextBox).Text
                Dim Attendance As String = CType(row.FindControl("ddlattendance"), DropDownList).SelectedItem.Text

                Dim paperid As String = TryCast(row.FindControl("txtpaperid"), TextBox).Text
                Dim Answersheetno As String = TryCast(row.FindControl("txtanswershet"), TextBox).Text

                If CType(row.FindControl("ddlattendance"), DropDownList).SelectedItem.Text = "Absent" Then
                    CType(row.FindControl("txtanswershet"), TextBox).ReadOnly = True
                    CType(row.FindControl("txtpaperid"), TextBox).ReadOnly = True
                    CType(row.FindControl("txtanswershet"), TextBox).ReadOnly = True

                    Obtmarks = ""
                    paperid = ""
                    Answersheetno = ""
                    InsertUpdatemarks(Studentid, Obtmarks, Attendance, paperid, Answersheetno)





                    'ElseIf CType(row.FindControl("txtanswershet"), TextBox).Text = "" Or CType(row.FindControl("txtanswershet"), TextBox).Text = " " Then
                    '    CType(row.FindControl("txtanswershet"), TextBox).ForeColor = Drawing.Color.Red

                    '    CType(row.FindControl("txtanswershet"), TextBox).Text = "*"

                    'ElseIf CType(row.FindControl("txtobtainmarks"), TextBox).Text = "" Or CType(row.FindControl("txtobtainmarks"), TextBox).Text = " " Then
                    '    CType(row.FindControl("txtobtainmarks"), TextBox).ForeColor = Drawing.Color.Red

                    '    CType(row.FindControl("txtobtainmarks"), TextBox).Text = "*"

                Else

                    InsertUpdatemarks(Studentid, Obtmarks, Attendance, paperid, Answersheetno)
                    SaralMsg.Messagebx.Alert(Me, "Changes is done successful.")

                End If

            Next


        Catch ex As Exception

        End Try
    End Sub

    Private Sub InsertUpdatemarks(ByVal Studentid As Object, ByVal Obtmarks As Object, ByVal Attendance As String, ByVal paperid As Object, ByVal Answersheetno As Object)
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("[Sp_SaveExamMarks]")
                Using sda As New SqlDataAdapter()
                    Dim ID As Integer = 0
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@Academicyear", ViewState("Acyear"))
                    cmd.Parameters.AddWithValue("@Examsubid", ViewState("Examsubid"))
                    cmd.Parameters.AddWithValue("@studentid", Studentid)
                    cmd.Parameters.AddWithValue("@obmarks", Obtmarks)
                    cmd.Parameters.AddWithValue("@Attendance", Attendance)
                    cmd.Parameters.AddWithValue("@paperid", paperid)
                    cmd.Parameters.AddWithValue("@Answersheetno", Answersheetno)
                    cmd.Parameters.AddWithValue("@userid ", ViewState("userid"))
                    cmd.Parameters.AddWithValue("@sessionid", ViewState("Sessionid"))
                    cmd.Parameters.AddWithValue("@ayid", ViewState("ayid"))
                    cmd.Parameters.AddWithValue("@examteacherid", ViewState("userid"))
                    cmd.Connection = con
                    con.Open()
                    ID = Convert.ToInt32(cmd.ExecuteScalar())
                    'cmd.ExecuteNonQuery()

                    con.Close()
                End Using
            End Using

        End Using

    End Sub

    Private Sub gridfetchvaluefield(ByVal Atendance As String, ByVal Paperid As String, ByVal Answersheet As String, ByVal Obtmarks As String)
        For Each row As GridViewRow In Gridstudentupdatemarks.Rows
            Dim Pprid As TextBox = TryCast(row.FindControl("txtpaperid"), TextBox)
            Dim Answerst As TextBox = TryCast(row.FindControl("txtanswershet"), TextBox)
            Dim Attendance As DropDownList = TryCast(row.FindControl("ddlattendance"), DropDownList)
            Dim obtnmark As TextBox = TryCast(row.FindControl("txtobtainmarks"), TextBox)
            Pprid.Text = Paperid.ToString()
            Answerst.Text = Answersheet.ToString()
            Attendance.SelectedItem.Text = Atendance.ToString()
            obtnmark.Text = Obtmarks.ToString()

        Next
    End Sub

    

    

    'Protected Sub Gridstudentmarks_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Gridstudentmarks.RowCommand
    '    If e.CommandName = "Modify" Then
    '        Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
    '        Dim row As GridViewRow = Gridstudentmarks.Rows(rowIndex)
    '        ViewState("ExamfillmarksheetId") = Gridstudentmarks.SelectedDataKey(0)
    '        Dim sql As String = ""
    '    End If

    'End Sub
    
    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        backbotton.Visible = False
        Pnlstudentmarks.Visible = False
        PnlExamlist.Visible = True
        backbotton2.Visible = True
        BindGridlistExams()

    End Sub

    Private Sub CheckExamlist()
        For Each row As GridViewRow In GridlistExams.Rows
            Using con As New SqlConnection(constr)
                Dim cmd As New SqlCommand("Select Stud.StudentID from  (Select TS.* from (Select St.Academicyear, St.CourseID, St.Sem, Strl.Rollgenerateid , Strl.Rollno, St.StudentID from Student st " & _
 " join Exam_Studentrollno Strl on St.AdmissionNo =Strl.Admissionno where St.Academicyear ='2023' and St.CourseID ='4' ) TS " & _
" join Exam_CourseExam Ce on Ts.CourseID = Ce.Courseid and Ts.Sem =Ce.Semyear join Exam_ExamSubject Es on Ce.CourseExamid=Es.Corseexamid " & _
" join Exam_ExamMarks Em on Es.Examsubid =Em.Examsubid and Ts.StudentID =Em.StudentId join Exam_Subject sub " & _
" on Es.Subjectid = sub.Subjectid Where Es.Academicyear ='2023' and Ce.CourseExamid ='1') Stud " & _
" Group by Stud.StudentID " & _
" Having Count(*)= (Select Count(*) from " & _
" ( Select TS.*, Ce.CourseExamid, Es.Examsubid, Es.Subjectid, sub.Subjectcode, sub.Subject, Em.Obtainedmarks, Em.paperid, Em.Attendance, " & _
" Em.AnswersheetNo  from (Select St.Academicyear, St.CourseID, St.Sem, Strl.Rollgenerateid , Strl.Rollno, St.StudentID from Student st " & _
" join Exam_Studentrollno Strl on St.AdmissionNo =Strl.Admissionno where St.Academicyear ='2023' and St.CourseID ='4' ) TS " & _
" join Exam_CourseExam Ce on Ts.CourseID = Ce.Courseid and Ts.Sem =Ce.Semyear join Exam_ExamSubject Es on Ce.CourseExamid=Es.Corseexamid " & _
" left join Exam_ExamMarks Em on Es.Examsubid =Em.Examsubid and Ts.StudentID =Em.StudentId join Exam_Subject sub " & _
" on Es.Subjectid = sub.Subjectid Where Es.Academicyear ='2023' and Ce.CourseExamid ='1') stud2)", con)

            End Using

        Next


        'Dim cmd As New SqlCommand("select * from ImageUpload where UserId='" & Request.QueryString("uid") & "'", con)
        'Dim da As New SqlDataAdapter(cmd)
        'Dim dt As New DataTable()

        'da.Fill(dt)
        'If dt.Rows.Count > 0 Then
        '    Label11.Visible = "True"
        'Else
        '    Label10.Visible = "True"
        'End If
        'D()
        'B()
    End Sub
    Private Sub Checkinggrid(ByVal Query As String, ByVal btnx As LinkButton, ByVal btny As LinkButton)

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
                        btnx.Visible = False
                        btny.Visible = True
                    Else
                        btnx.Visible = True
                        btny.Visible = False
                    End If
                End Using
            End Using
        End Using

    End Sub
    Private Sub CheckMarksfilling()
        For Each row As GridViewRow In GridlistExams.Rows
            Dim Examsubid As String = row.Cells(2).Text
            ' Dim Examteacherid As String = row.Cells(12).Text
            Dim query As String = "Select Count(*) as Count from Exam_ExamMarks where  Examsubid='" & Examsubid & " '"
            Checkinggrid(query, TryCast(row.FindControl("btncheck"), LinkButton), TryCast(row.FindControl("namelink"), LinkButton))
        Next

    End Sub
End Class
