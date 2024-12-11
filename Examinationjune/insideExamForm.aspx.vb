'Design And Developed By Avaneesh Yadav
Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Partial Class TESTFILES_insideExamForm
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            ViewState("Academicyear") = Request.QueryString("acyr")
            ViewState("Sessionid") = Request.QueryString("s")
            ViewState("courseexamid") = Request.QueryString("corseexamid")
            Session("Courseid") = Request.QueryString("Courseid")
            ViewState("ayid") = Request.QueryString("ay")
            If ddlstatus.SelectedValue = "0" Then

                Me.BindPending()
            Else
                BindApprove()
            End If

        End If
    End Sub
    Private Sub BindPending()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select S.AdmissionNo,S.FirstName,S.Roll_No,s.EnrollmentNo,s.FatherName,s.Mobile ,C.Course, EFF.* from Exam_Examformstudent EFF " & _
" join Exam_CourseExam Ce on EFF.Courseexamid=Ce.CourseExamid join Studentyear Sy on EFF.Studentid=Sy.StudentID and " & _
" Ce.Courseid=Sy.Courseid and Ce.Semyear=Sy.SEm and Ce.Ayid=Sy.ayid join Student S on Sy.StudentID =S.StudentID " & _
"join Exam_Course C on Ce.Courseid=C.Courseid " & _
" where EFF.Sessionid='" & ViewState("Sessionid") & "' and EFF.Ayid='" & ViewState("ayid") & "' and Eff.Courseexamid='" & ViewState("courseexamid") & "' And EFF.Isexamformverified = '0' ")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        FormVerified.DataSource = dt
                        FormVerified.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub
    Private Sub BindApprove()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select S.AdmissionNo,S.FirstName,S.Roll_No,s.EnrollmentNo,s.FatherName,s.Mobile ,C.Course, EFF.* from Exam_Examformstudent EFF " & _
" join Exam_CourseExam Ce on EFF.Courseexamid=Ce.CourseExamid join Studentyear Sy on EFF.Studentid=Sy.StudentID and " & _
" Ce.Courseid=Sy.Courseid and Ce.Semyear=Sy.SEm and Ce.Ayid=Sy.ayid join Student S on Sy.StudentID =S.StudentID " & _
"join Exam_Course C on Ce.Courseid=C.Courseid " & _
" where EFF.Sessionid='" & ViewState("Sessionid") & "' and EFF.Ayid='" & ViewState("ayid") & "' and Eff.Courseexamid='" & ViewState("courseexamid") & "' And EFF.Isexamformverified = '" & ddlstatus.SelectedValue & "' ")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        FormVerified.DataSource = dt
                        FormVerified.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Protected Sub FormVerified_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FormVerified.RowCommand
        FormVerified.SelectedIndex = e.CommandArgument

        If e.CommandName = "ViewExamForm" Then
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = FormVerified.Rows(rowIndex)
            Dim Coursexamid As String = row.Cells(8).Text
            Dim examformid As String = row.Cells(9).Text
            Dim studentid As String = row.Cells(10).Text

            fetchstudetail(studentid, examformid, Coursexamid)
            BINDPHOTO(studentid)
            BindExamsubject(studentid, examformid, Coursexamid)
            Panel1.Visible = True
            'PanelStudentListpending.Visible = False
            Paanelstudentlistsuccess.Visible = False
        End If
    End Sub
    Sub BINDPHOTO(ByVal sid As String)
        Dim gender As String = ""

        Dim ds As New DataSet
        Using con As New SqlConnection(constr)
            con.Open()
            Dim cmd1 As New SqlCommand("Select * from Student where StudentID='" & sid & "' ", con)
            Dim da As New SqlDataAdapter(cmd1)
            cmd1.Connection = con
            da.Fill(ds)
            Dim i = ds.Tables(0).Rows.Count()
            If (i > 0) Then
                gender = ds.Tables(0).Rows(0)("Gender").ToString()
             End If
            con.Close()
        End Using


        ViewState("RegisId") = sid
        Dim files As String() = System.IO.Directory.GetFiles(Server.MapPath("~/photos/users"), "" & (Trim(ViewState("RegisId").ToString)) & ".*")
        If files.ToArray.Length > 0 Then
            Dim exten As String = files(0)
            exten = "." & exten.Substring(exten.LastIndexOf(".") + 1)
            imgstudnet.ImageUrl = "~\photos\users\" & sid.ToString & exten & ""
        Else
            imgstudnet.ImageUrl = "~\img\" & gender.ToString & ".jpg"
        End If


        Dim files1 As String() = System.IO.Directory.GetFiles(Server.MapPath("~/photos/Signature"), "" & (Trim(ViewState("RegisId").ToString)) & ".*")
        If files1.ToArray.Length > 0 Then
            Dim exten As String = files1(0)
            exten = "." & exten.Substring(exten.LastIndexOf(".") + 1)
            Image1.ImageUrl = "~\photos\Signature\" & sid.ToString & exten & ""
        Else
            Image1.ImageUrl = "~\img\sign1.jpg"
        End If

    End Sub
    Private Sub fetchstudetail(ByVal Studentid As String, ByVal Examformid As String, ByVal Coursexamid As String)

        Dim ds As New DataSet
        Using con As New SqlConnection(constr)
            con.Open()
            Dim cmd1 As New SqlCommand("Select Ce.ExamName,Ce.Examtype,EFs.Examformid, Efs.Dated as 'Formdate',I.Institue,B.Batch, C.Coursecode,C.Course, Sy.ayid,Sy.SessionID, " & _
                                     "  Ay.AcademicYearEvenodd, S.* from Student S " & _
"join StudentYear Sy  on S.StudentID=Sy.StudentID and S.CourseID=Sy.Courseid and S.Sem=Sy.SEm " & _
"join Exam_Examformstudent Efs on Sy.StudentID=Efs.Studentid and Sy.CourseID=Efs.Courseid and Sy.Sem=Efs.Sem " & _
"join Exam_CourseExam Ce on Efs.Courseexamid=Ce.CourseExamid join Exam_Course C on Ce.Courseid=C.Courseid join Institue I on C.Cid=I.InstitueID " & _
" join Batch B on S.BatchID=B.BatchID join Academic_Year Ay on Efs.Ayid=Ay.AcademicYearid " & _
"Where S.StudentID='" & Studentid & "' and Efs.CourseExamid='" & Coursexamid & "' and Efs.Examformid='" & Examformid & "' ", con)
            Dim da As New SqlDataAdapter(cmd1)
            cmd1.Connection = con
            da.Fill(ds)
            Dim i = ds.Tables(0).Rows.Count()
            If (i > 0) Then
                lblStudentName.Text = ds.Tables(0).Rows(0)("Student").ToString()
                lblProgramCourse.Text = ds.Tables(0).Rows(0)("Course").ToString()
                lblcollegename.Text = ds.Tables(0).Rows(0)("Institue").ToString()
                Label5.Text = ds.Tables(0).Rows(0)("Institue").ToString()
                Session("Sem") = ds.Tables(0).Rows(0)("Sem").ToString()
                Session("Batchid") = ds.Tables(0).Rows(0)("Batchid").ToString()
                Lblsession.Text = ds.Tables(0).Rows(0)("AcademicYearEvenodd").ToString()

                lblDate.Text = ds.Tables(0).Rows(0)("Formdate").ToString()
                lblenrollment.Text = ds.Tables(0).Rows(0)("EnrollmentNo").ToString()
                lblrollno.Text = ds.Tables(0).Rows(0)("Roll_no").ToString()
                lblsemyear.Text = ds.Tables(0).Rows(0)("Sem").ToString()
                lblexamname.Text = ds.Tables(0).Rows(0)("ExamName").ToString()
                lblgender.Text = ds.Tables(0).Rows(0)("Gender").ToString()
                lblFatherName.Text = ds.Tables(0).Rows(0)("Fathername").ToString()
                lblformno.Text = ds.Tables(0).Rows(0)("Examformid").ToString()
            End If
            con.Close()
        End Using
        
    End Sub
    Private Sub BindExamsubject(ByVal studentid As String, ByVal examformid As String, ByVal Coursexamid As String)
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Sub.Subject,Sub.Subjectcode, Esub.Subjectid,Esub.Examsubid, Efs.* from Exam_Examformstudent Efs " & _
"join Exam_CourseExam Ce on Efs.Courseexamid=Ce.CourseExamid " & _
"join Exam_ExamSubject Esub on Ce.CourseExamid=Esub.Corseexamid " & _
" join Exam_Subject Sub on Esub.Subjectid=Sub.Subjectid " & _
" where Ce.CourseExamid='" & Coursexamid & "' and Efs.Examformid='" & examformid & "' and Efs.Studentid='" & studentid & "' ")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        grdstudent.DataSource = dt
                        grdstudent.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub Fetchstudentdetail(Admissionno As String)
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select ec.Course,s.Student,s.Gender,s.Courseid,s.EnrollmentNo,ece.Examtype,s.Roll_no,i.Institue,s.Batchid, s.Sem From Student s Join Exam_course ec on s.Courseid = ec.Courseid Join institue i on s.Institueid = i.Institueid join Exam_Courseexam ece on ec.Courseid = ece.Courseid where s.Admissionno = '" & Session("Admissionno") & "' and ece.Courseexamid = '" & Session("Courseexamid") & "' ", con)
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)

                        lblStudentName.Text = dt.Rows(0)("Student").ToString()
                        lblgender.Text = dt.Rows(0)("Gender").ToString()
                        lblProgramCourse.Text = dt.Rows(0)("Course").ToString
                        lblFatherName.Text = dt.Rows(0)("FatherName").ToString()


                    End Using
                End Using
            End Using
        End Using
    End Sub

    Protected Sub btnverify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnverify.Click
        If ddlverify.SelectedValue = "0" Then
            SaralMsg.Messagebx.Alert(Me, "Select Approve/Reject")
        Else
            Using con As New SqlConnection(constr)
                Using cmd1 As New SqlCommand("update Exam_Examformstudent set Isexamformverified = '" + ddlverify.SelectedValue + "',Verifiedby = '" & Request.QueryString("u") & "', VerificationDate = '" & Date.Now & "' where Examformid ='" + lblformno.Text + "'", con)
                    con.Open()
                    cmd1.ExecuteNonQuery()
                    con.Close()
                    SaralMsg.Messagebx.Alert(Me, "Verified successfully!!!")
                End Using
            End Using
            Paanelstudentlistsuccess.Visible = True
            Panel1.Visible = False

        End If
        
    End Sub

    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
      
        Response.Redirect("CreatedExams.aspx?rid=" & Session("Courseid") & "&acyr=" & ViewState("Academicyear") & "&u=" & Request.QueryString("u") & "&s=" & Request.QueryString("s") & "&ay=" & ViewState("ayid") & "&Courseexamid" & ViewState("courseexamid"))
    End Sub

    Protected Sub backstudent_Click(sender As Object, e As System.EventArgs) Handles backstudent.Click
        Panel1.Visible = False
        'PanelStudentListpending.Visible = False
        Paanelstudentlistsuccess.Visible = True
        If ddlstatus.SelectedValue = "0" Then

            Me.BindPending()
        Else
            BindApprove()
        End If

    End Sub

    Protected Sub ddlstatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlstatus.SelectedIndexChanged
        If ddlstatus.SelectedValue = "0" Then
            BindPending()
        Else
            BindApprove()
        End If
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    Protected Sub Download_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Download.Click
        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment;filename=ExamFormVerified.xls")
        Response.ContentType = "application/vnd.ms-excel"
        Dim sw As StringWriter = New StringWriter()
        Dim htm As HtmlTextWriter = New HtmlTextWriter(sw)
        FormVerified.AllowPaging = False

        FormVerified.HeaderRow.Cells(8).Visible = False
        FormVerified.RowStyle.HorizontalAlign = HorizontalAlign.Center
        For Each row As GridViewRow In FormVerified.Rows
            row.Cells(8).Visible = False
           
        Next

        FormVerified.RenderControl(htm)
        Response.Write(sw.ToString())
        Response.End()
    End Sub
End Class
