Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Partial Class StudentAdm_Viewexamform
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
    '----------------------------------developed by Er Mohit Kumar ------------------------
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Session("Admissionno") = Request.QueryString("admissionno")
            Session("Courseexamid") = Session("courxe")
            Session("Courseid") = Request.QueryString("rid")
            ViewState("Sessionid") = Request.QueryString("s")
            ViewState("Ayid") = Request.QueryString("ayid")
            ViewState("Courseexamid") = Request.QueryString("CrsExamid")
            ViewState("stuid") = Request.QueryString("stuid")
            fetchsession()
            fetchprogramdetail()
            Session("Courseid") = Session("Courseid")
            BindGrid1()
            photoBind(ViewState("stuid"))
        End If
    End Sub
    Private Sub fetchsession()
        Try
            Dim ds As New DataSet
            Using con As New SqlConnection(constr)
                con.Open()
                Dim cmd1 As New SqlCommand(" select * from Exam_Session s where Sessionid='" & ViewState("Sessionid") & "'", con)
                Dim da As New SqlDataAdapter(cmd1)
                cmd1.Connection = con
                da.Fill(ds)
                Dim i = ds.Tables(0).Rows.Count()
                If (i > 0) Then
                    Lblsession.Text = ds.Tables(0).Rows(0)("Session").ToString()
                    lblDate.Text = Date.Now

                     End If
                con.Close()
            End Using
        Catch ex As Exception
        End Try
    End Sub
    Public Sub photoBind(ByVal SID As Integer)
        Try
            Dim gender As String = ""

            Dim ds As New DataSet
            Using con As New SqlConnection(constr)
                con.Open()
                Dim cmd1 As New SqlCommand("Select * from Student where Studentid='" & SID & "' ", con)
                Dim da As New SqlDataAdapter(cmd1)
                cmd1.Connection = con
                da.Fill(ds)
                Dim i = ds.Tables(0).Rows.Count()
                If (i > 0) Then
                    gender = ds.Tables(0).Rows(0)("Gender").ToString()
                End If
                con.Close()
            End Using


            'ViewState("regisid") = SID
            Dim files As String() = System.IO.Directory.GetFiles(Server.MapPath("~/photos/users"), "" & (Trim(SID.ToString)) & ".*")
            If files.ToArray.Length > 0 Then
                Dim exten As String = files(0)
                exten = "." & exten.Substring(exten.LastIndexOf(".") + 1)
                imgstudnet.ImageUrl = "~\photos\users\" & SID.ToString & exten & ""
                
            Else
                If gender = "" Then
                    imgstudnet.ImageUrl = "~\img\Male.jpg"
                   
                Else
                    imgstudnet.ImageUrl = "~\img\" & gender.ToString & ".jpg"
                    
                End If

            End If


            Dim files1 As String() = System.IO.Directory.GetFiles(Server.MapPath("~/photos/Signature"), "" & (Trim(SID.ToString)) & ".*")
            If files1.ToArray.Length > 0 Then
                Dim exten As String = files1(0)
                exten = "." & exten.Substring(exten.LastIndexOf(".") + 1)
                Image1.ImageUrl = "~\photos\Signature\" & SID.ToString & exten & ""
                 Else
                Image1.ImageUrl = "~\img\sign1.jpg"

            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub fetchprogramdetail()
        Try
            Dim ds As New DataSet
            Using con As New SqlConnection(constr)
                con.Open()
                Dim cmd1 As New SqlCommand("Select Efst.Examformid, ec.Course,ece.ExamName, ece.Examtype,i.Institue,st.* From Exam_Courseexam ece   inner join " & _
" Studentyear sy on ece.Courseid=sy.Courseid and ece.Semyear=Sy.sem and ece.Ayid= sy.ayid " & _
" inner join Student st on Sy.StudentID=St.StudentID left join Exam_Examformstudent Efst on ece.CourseExamid=Efst.Courseexamid and st.Studentid=Efst.Studentid " & _
" left Join Exam_course ec on ece.Courseid=ec.Courseid left Join institue i on ec.Cid = i.Institueid " & _
" where Sy.StudentID = '" & ViewState("stuid") & "' and ece.Courseexamid = '" & ViewState("Courseexamid") & "' ", con)
                Dim da As New SqlDataAdapter(cmd1)
                cmd1.Connection = con
                da.Fill(ds)
                Dim i = ds.Tables(0).Rows.Count()
                If (i > 0) Then
                    lblStudentName.Text = ds.Tables(0).Rows(0)("Student").ToString()
                    lblProgramCourse.Text = ds.Tables(0).Rows(0)("Course").ToString()
                    Session("Courseid") = ds.Tables(0).Rows(0)("Courseid").ToString()
                    lblcollege.Text = ds.Tables(0).Rows(0)("Institue").ToString()
                    Label5.Text = ds.Tables(0).Rows(0)("Institue").ToString()
                    Session("Sem") = ds.Tables(0).Rows(0)("Sem").ToString()
                    Session("Batchid") = ds.Tables(0).Rows(0)("Batchid").ToString()
                    lblenrollment.Text = ds.Tables(0).Rows(0)("EnrollmentNo").ToString()
                    lblrollno.Text = ds.Tables(0).Rows(0)("Roll_no").ToString()
                    lblsemyear.Text = ds.Tables(0).Rows(0)("Sem").ToString()
                    lblexamtype.Text = ds.Tables(0).Rows(0)("Examtype").ToString()
                    lblgender.Text = ds.Tables(0).Rows(0)("Gender").ToString()
                    Lblexamname.Text = ds.Tables(0).Rows(0)("ExamName").ToString()
                    Lblexamform.Text = ds.Tables(0).Rows(0)("Examformid").ToString()
                    lblFatherName.Text = ds.Tables(0).Rows(0)("FatherName").ToString()
                End If
                con.Close()
            End Using
        Catch ex As Exception
        End Try
    End Sub
    Private Sub BindGrid1()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(" select Esub.Examsubid, Esub.Corseexamid,Sub.* from Exam_ExamSubject Esub " & _
" left join Exam_Subject sub on Esub.Subjectid=Sub.Subjectid where Esub.Corseexamid='2'")
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

    Protected Sub backstudent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backstudent.Click
        Response.Redirect("ExaminationStu.aspx?s=" & ViewState("Sessionid") & "&u=" & ViewState("stuid") & "&ayid=" & ViewState("Ayid"))
    End Sub
End Class
