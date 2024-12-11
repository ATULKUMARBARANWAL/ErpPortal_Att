'Design And Developed By Avaneesh Yadav
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient


Partial Class datesheetPage
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    'Is postback function'
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'BindGrid()
            ViewState("sessionid") = Request.QueryString("s")
            ViewState("courseexamid") = Request.QueryString("courseexamid")
            ViewState("Course") = Session("Courseid")
            ViewState("Academicyear") = Request.QueryString("acyr")
            'Session("Courseid") = Request.QueryString("rid")
            ViewState("userid") = Request.QueryString("u")
            Session("CourseExamid") = Session("CourseExamid")
            ViewState("ayid") = Request.QueryString("ay")
            BindCourseExamdropdown()
            ddlExamN.Items.FindByValue(ViewState("courseexamid")).Selected = True
            BindPrograms()


            fetchddlProgram()
            'BindSem()
            'ddlSemYr.Items.FindByText(Session("Semyear")).Selected = True
            BindGridView()
            pnlGrid.Visible = True
            pnlMain.Visible = True
        End If
    End Sub

    'ddl courseExam Bind function call'
    Private Sub BindCourseExamdropdown()
        Dim query As String = ("Select * from Exam_CourseExam where Sessionid='" & ViewState("sessionid") & "' and Ayid='" & ViewState("ayid") & "'")
        BindDropDownList1(ddlExamN, query, "ExamName", "CourseExamid", "Select Exam Name")

    End Sub

    'ddl courseProgram Bind function call'
    Private Sub BindPrograms()

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select C.Course, Ce.* from Exam_CourseExam Ce join Exam_Course C on Ce.Courseid=C.Courseid where Ce.CourseExamid='" & ddlExamN.SelectedValue & "' and Ce.Ayid='" & ViewState("ayid") & "' and Ce.SessionId='" & ViewState("sessionid") & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        ddlProgram.DataSource = dt
                        ddlProgram.DataTextField = "Course"
                        ddlProgram.DataValueField = "Courseid"
                        ddlProgram.DataBind()
                      

                    End Using
                End Using
            End Using
            ddlProgram.Items.Insert(0, New ListItem("Select", "0"))
        End Using

    End Sub
    'Private Sub BindSem()
    '    Dim query As String = ("Select Semyear from Exam_Course where Courseid = '" & Session("otherid") & "' ")
    '    BindDropDownList1(ddlSemYr, query, "Semyear", "CourseExamid", "Select Semyear")
    'End Sub
    Private Sub fetchddlProgram()

        ddlSemYr.Items.Clear()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Semyear, CourseExamid from Exam_CourseExam  where CourseExamid='" & ddlExamN.SelectedValue & "' ", con)
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        ddlSemYr.DataSource = dt
                        ddlSemYr.DataTextField = "Semyear"
                        ddlSemYr.DataValueField = "Semyear"
                        ddlSemYr.DataBind()
                        If dt.Rows.Count > 0 Then
                            Dim sem As String = dt.Rows(0)("Semyear").ToString
                            ddlSemYr.Items.FindByText(sem).Selected = True

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
            Dim cmd1 As New SqlCommand("Select Semyear, CourseExamid from Exam_CourseExam where Courseid = '" & Session("otherid") & "' ", con)
            Dim da As New SqlDataAdapter(cmd1)
            cmd1.Connection = con
            da.Fill(ds)
            Dim i = ds.Tables(0).Rows.Count()
            If (i > 0) Then
                ViewState("Semyear") = Session("Semyear")
                ViewState("CourseExamid") = ds.Tables(0).Rows(0)("CourseExamid").ToString()
            End If
            con.Close()

        End Using

    End Sub
    'Function for binding Dropdown'
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

    'Function for GridView'
    Private Sub BindGridView()
        Dim query1 As String = ("Select sub.Subject,sub.Subjectcode as 'Subcode', Esub.* from Exam_ExamSubject Esub join Exam_CourseExam Ce " & _
" on Ce.CourseExamid=Esub.Corseexamid join Exam_Subject sub on Esub.Subjectid=sub.Subjectid " & _
" where Esub.Corseexamid='" & ddlExamN.SelectedValue & "' and Ce.Sessionid='" & ViewState("sessionid") & "' and Ce.Ayid='" & ViewState("ayid") & "'")
        Dim cmd As New SqlCommand(query1)
        Dim con As New SqlConnection(constr)
        Dim da As New SqlDataAdapter
        da.SelectCommand = cmd
        Dim ds As New DataSet
        cmd.Connection = con
        con.Open()
        da.Fill(ds)
        GridView2.DataSource = ds
        GridView2.DataBind()
        con.Close()
    End Sub

    'Select Index examname Change fill program level'
    'Protected Sub ddlExamN_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlExamN.SelectedIndexChanged
    '    ddlProgLevel.Items.Insert(1, New ListItem("UG", "1"))
    '    ddlProgLevel.Items.Insert(2, New ListItem("PG", "2"))
    '    ddlProgLevel.Items.Insert(3, New ListItem("Diploma", "3"))

    'End Sub

    'Select index program  Change fill sem/year function '
    Protected Sub ddlProgram_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProgram.SelectedIndexChanged
        Try

        
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("select Case when Coursetype like'%sem%' then Duration*2 when Coursetype like '%year%' then Duration*1 when Coursetype like '%quart%' then Duration*4 end as TotalSem from Exam_CourseSession where Courseid=" & ddlProgram.SelectedItem.Value & "")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        ddlSemYr.DataSource = dt
                        Dim Totalsem As String = dt.Rows(0)("TotalSem").ToString
                        Dim i As Integer
                        For i = 1 To Totalsem
                                ddlSemYr.Items.Add(New ListItem(i.ToString(), i.ToString()))
                                ddlSemYr.Items.FindByText(Session("Semyear")).Selected = True
                        Next

                    End Using
                End Using
            End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    'Select index program level Change program function in ddl '
    'Protected Sub ddlProgLevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProgLevel.SelectedIndexChanged
    '    BindPrograms()
    'End Sub

    'Select index sem/year Change call Bindgridview function'
    Protected Sub ddlSemYr_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSemYr.SelectedIndexChanged
        BindGridView()
        pnlGrid.Visible = True
    End Sub

    Protected Sub btnInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsert.Click
       
        For Each row As GridViewRow In GridView2.Rows
            Dim tExamTime As TextBox = TryCast(row.FindControl("txtExamTime"), TextBox)
            Dim tReprtingtime As TextBox = TryCast(row.FindControl("txtReportTime"), TextBox)
            Dim tDurationtime As DropDownList = TryCast(row.FindControl("ddlExamDuration"), DropDownList)
            tExamTime.Text = txtExamTime.Text
            tReprtingtime.Text = txtReportTime.Text
            tDurationtime.SelectedItem.Text = ddlExamDurinsert.SelectedItem.Text


        Next

    End Sub
    Private Sub Examdatesheet(ByVal ExamDate As String, ByVal Shift As String, ByVal ReportingTime As String, ByVal ExamTime As String, ByVal ExamDuration As String, ByVal Examsubid As Integer)

        Using con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("myconnection").ConnectionString)
            Using Command As New SqlCommand("sp_Exam_ExamDATESHEET", con)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@Examsubid", Examsubid)
                Command.Parameters.AddWithValue("@Dated", Date.Now)
                Command.Parameters.AddWithValue("@Shifttype", Shift)
                Command.Parameters.AddWithValue("@ReportingTime", ReportingTime)
                Command.Parameters.AddWithValue("@ExamTime", ExamTime)
                Command.Parameters.AddWithValue("@ExamDuration", ExamDuration)
                Command.Parameters.AddWithValue("@ExamDate", ExamDate)
                Command.Parameters.AddWithValue("@userid", Request.QueryString("u"))
                Command.Parameters.AddWithValue("@SemYear", ddlSemYr.SelectedValue)
                Command.Parameters.AddWithValue("@Courseid", ddlProgram.SelectedValue)
                Command.Parameters.AddWithValue("@sessionid", ViewState("sessionid"))
                Command.Parameters.AddWithValue("@Ayid", ViewState("ayid"))
                con.Open()
                Command.ExecuteNonQuery()
                con.Close()
            End Using
        End Using



    End Sub
   

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If ddlProgram.SelectedValue = "0" Then
            SaralMsg.Messagebx.Alert(Me, "Select Program")
        Else

            For Each row As GridViewRow In GridView2.Rows
                Dim ExamTime As String = CType(row.FindControl("txtExamTime"), TextBox).Text
                Dim ReportingTime As String = CType(row.FindControl("txtReportTime"), TextBox).Text
                Dim Examdate As String = CType(row.FindControl("txtExamDate"), TextBox).Text
                Dim shift As String = CType(row.FindControl("ddlShift"), DropDownList).SelectedItem.Text
                Dim examduration As String = CType(row.FindControl("ddlExamDuration"), DropDownList).SelectedItem.Text
                Dim Examsubid As String = row.Cells(1).Text

                If ExamTime = "" Or ReportingTime = "" Or Examdate = "" Or shift = "" Or examduration = "" Or Examsubid = "" Then
                    SaralMsg.Messagebx.Alert(Me, "Fill all Field")
                Else
                    Examdatesheet(Examdate, shift, ReportingTime, ExamTime, examduration, Examsubid)
                    SaralMsg.Messagebx.Alert(Me, "Saved successfully")
                End If

            Next
        End If
    End Sub

    Protected Sub lnkbtnView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnView.Click
        If ddlExamN.SelectedValue = "0" Or ddlProgram.SelectedValue = "0" Then
            SaralMsg.Messagebx.Alert(Me, "Select Program")
        Else
            pnlPrintGridView.Visible = True
            pnlGridheaderView.Visible = True

            pnlGrid.Visible = False
            pnlMain.Visible = False

            ViewDatesheet()

        End If
       

    End Sub


    Private Sub ViewDatesheet()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select C.Course, Esub.Corseexamid,Sub.Subject,Sub.Subjectcode,Ce.Courseid,Ce.Semyear,Ce.ExamDisplayname, Ed.* From Exam_ExamDatesheet Ed join Exam_ExamSubject Esub on Ed.Examsubid=Esub.Examsubid " & _
" join Exam_CourseExam Ce on Esub.Corseexamid=Ce.CourseExamid join Exam_Subject sub on Esub.Subjectid=sub.Subjectid " & _
" join Exam_Course C on  Ce.Courseid=C.Courseid where Esub.Corseexamid='" & ddlExamN.SelectedValue & "' and Ce.Courseid='" & ddlProgram.SelectedValue & "' and Ed.Ayid='" & ViewState("ayid") & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        grdGridView.DataSource = dt
                        grdGridView.DataBind()
                        If dt.Rows.Count > 0 Then
                            lblDurationValue.Text = (dt.Rows(0)("ExamDuration").ToString())
                            lblExamNameValue.Text = (dt.Rows(0)("ExamDisplayname").ToString())

                        End If




                    End Using
                End Using
            End Using

        End Using




    End Sub

    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        ViewState("Courseid") = Session("Courseid")
        'Session("Otherid") = row.Cells(2).Text
        Session("ExamName") = Session("Examname")
        Session("Course") = Session("Course")
        Session("Semyear") = Session("Semyear")
        Response.Redirect("CreatedExams.aspx?rid=" & Session("Courseid") & "&acyr=" & ViewState("Academicyear") & "&u=" & Request.QueryString("u") & "&s=" & Request.QueryString("s") & "&ay=" & ViewState("ayid"))
    End Sub

    Protected Sub backbuttonview_Click(sender As Object, e As System.EventArgs) Handles backbuttonview.Click
        pnlPrintGridView.Visible = False
        pnlGridheaderView.Visible = False

        pnlGrid.Visible = True
        pnlMain.Visible = True
        BindGridView()
    End Sub

    Protected Sub ddlExamN_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlExamN.SelectedIndexChanged
        BindPrograms()
        fetchddlProgram()
        BindGridView()
    End Sub
End Class
