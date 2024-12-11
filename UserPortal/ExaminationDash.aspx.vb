Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Partial Class UserPortal_ExaminationDash
    Inherits System.Web.UI.Page

    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Session("Studentid") = Request.QueryString("stuid")
        ViewState("Sessionid") = Request.QueryString("s")
        ExamForm()

        fetchprogramdetail()
        Academic()
        BindExamCreate()
    End Sub
    Private Sub fetchprogramdetail()
        Try
            Dim ds As New DataSet
            Using con As New SqlConnection(constr)
                con.Open()
                Dim cmd1 As New SqlCommand(" Select S.AdmissionNo, s.Studentid,ec.Course,s.Courseid,i.Institue,sy.ayid,s.Batchid,s.sessionid, s.Sem From Student s left Join Exam_course ec on s.Courseid = ec.Courseid Join institue i on s.Institueid = i.Institueid  left join Studentyear sy on sy.studentid = s.studentid where s.Studentid = '" & Request.QueryString("stuid") & "' ", con)
                Dim da As New SqlDataAdapter(cmd1)
                cmd1.Connection = con
                da.Fill(ds)
                Dim i = ds.Tables(0).Rows.Count()
                If (i > 0) Then
                    Session("Course") = ds.Tables(0).Rows(0)("Course").ToString()
                    ViewState("courseid") = ds.Tables(0).Rows(0)("Courseid").ToString()
                    Session("Courseid") = ds.Tables(0).Rows(0)("Courseid").ToString()
                    Session("Institue") = ds.Tables(0).Rows(0)("Institue").ToString()
                    ViewState("Sem") = ds.Tables(0).Rows(0)("Sem").ToString()
                    Session("Sem") = ds.Tables(0).Rows(0)("Sem").ToString()
                    Session("Batchid") = ds.Tables(0).Rows(0)("Batchid").ToString()
                    ViewState("Batch") = ds.Tables(0).Rows(0)("Institue").ToString()
                    Session("Stuid") = ds.Tables(0).Rows(0)("Studentid").ToString()
                    Session("Admissionno") = ds.Tables(0).Rows(0)("AdmissionNo").ToString
                    ViewState("Sessionid") = ds.Tables(0).Rows(0)("Sessionid").ToString
                    ViewState("ayid") = ds.Tables(0).Rows(0)("ayid").ToString
                End If
                con.Close()
            End Using
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Academic()
        Dim ds As New DataSet
        Using con As New SqlConnection(constr)
            con.Open()
            Dim cmd1 As New SqlCommand(" Select Batch from Batch where Batchid = '" & Session("Batchid") & "' ", con)
            Dim da As New SqlDataAdapter(cmd1)
            cmd1.Connection = con
            da.Fill(ds)
            Dim i = ds.Tables(0).Rows.Count()
            If (i > 0) Then
                Session("Batch") = ds.Tables(0).Rows(0)("Batch").ToString()
            End If
            con.Close()
        End Using
    End Sub

    Protected Sub backbotton_Click(sender As Object, e As System.EventArgs) Handles backbotton.Click
        Response.Redirect("~/UserPortal/DashboardStu.aspx?u=" & Session("Studentid") & "&s=" & ViewState("Sessionid"))
    End Sub
    Protected Sub btnHome_Click(sender As Object, e As System.EventArgs) Handles btnHome.Click
        Response.Redirect("~/UserPortal/DashboardStu.aspx")
    End Sub
    Private Sub ExamForm()
        Dim ds As New DataSet
        Using con As New SqlConnection(constr)
            con.Open()
            Dim cmd1 As New SqlCommand("select courseid from Student where Studentid='" & Request.QueryString("stuid") & "' ", con)
            Dim da As New SqlDataAdapter(cmd1)
            cmd1.Connection = con
            da.Fill(ds)
            Dim i = ds.Tables(0).Rows.Count()
            If (i > 0) Then
                ViewState("Courseid") = ds.Tables(0).Rows(0)("courseid").ToString()
                'ViewState("Coursecode") = ds.Tables(0).Rows(0)("Coursecode").ToString()
            End If
            con.Close()

        End Using
    End Sub

    Private Sub BindExamCreate()
        Try
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("select distinct ece.Courseexamid,sy.StudentID, ece.Semyear,ece.ExamName, convert(varchar, ece.ExamFormFromDate, 107) as ExamFormFromDate, convert(varchar, ece.ExamFormToDate, 107) as ExamFormToDate from Exam_courseExam ece join StudentYear sy on ece.Courseid = sy.Courseid and ece.Semyear=Sy.SEm and ece.Sessionid=sy.SessionID " & _
" and ece.Ayid=Sy.ayid where sy.SessionID='" & ViewState("Sessionid") & "' and sy.ayid='" & ViewState("ayid") & "' and sy.Courseid = '" & ViewState("courseid") & "' and ece.Semyear = '" & ViewState("Sem") & "' and sy.Studentid = '" & Request.QueryString("stuid") & "'")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            GridExam.DataSource = dt
                            GridExam.DataBind()
                        End Using
                    End Using
                End Using
            End Using
        Catch ex As Exception
        End Try
        CheckSubjectadd()
    End Sub
    Private Sub CheckSubjectadd()
        Try


            For Each row As GridViewRow In GridExam.Rows
                Dim textbox As String = row.Cells(3).Text
                Dim enddate As String = Convert.ToDateTime(textbox)
                If enddate < Date.Now Then
                    TryCast(row.FindControl("namelink1"), LinkButton).Enabled = False
                    TryCast(row.FindControl("namelink1"), LinkButton).ForeColor = Drawing.Color.FromArgb(128, 128, 128)
                    TryCast(row.FindControl("namelink1"), LinkButton).Text = "Exam Form"
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub GridExam_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridExam.RowCommand

        If e.CommandName = "StudentExam" Then
            GridExam.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridExam.Rows(rowIndex)
            Dim corseexamid As String = row.Cells(4).Text
            'ViewState("Coursesessionid") = GridExam.SelectedDataKey(0)
            Session("Otherid") = row.Cells(2).Text
            Session("courxe") = row.Cells(4).Text
            Dim stuid As String = ViewState("stuid")
            Session("Admissionno") = Session("Admissionno")
            Bindexam(corseexamid)

        End If
        If e.CommandName = "ViewExamForm" Then
            GridExam.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridExam.Rows(rowIndex)
            'ViewState("Coursesessionid") = GridExam.SelectedDataKey(0)
            Dim examid As String = row.Cells(4).Text
            Session("Otherid") = row.Cells(2).Text
            Session("courxe") = row.Cells(4).Text
            Session("Admissionno") = Session("Admissionno")
            Dim stuid As String = ViewState("stuid")

            Bindviewexam(examid, stuid)
        End If
        If e.CommandName = "Admitcard" Then
            GridExam.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridExam.Rows(rowIndex)
            'ViewState("Coursesessionid") = GridExam.SelectedDataKey(0)
            Dim examid As String = row.Cells(4).Text
            Session("Otherid") = row.Cells(2).Text
            Session("Examname") = row.Cells(1).Text
            Session("courxe") = row.Cells(4).Text
            Session("Admissionno") = Session("Admissionno")
            Session("Courseid") = Session("Courseid")
            Session("Stuid") = Session("Stuid")
            Session("Sem") = Session("Sem")
            Admitcard(examid)
        End If
    End Sub
    Private Sub Bindexam(ByVal corseexamid As String)
        Dim ds As New DataSet
        Using con As New SqlConnection(constr)
            con.Open()
            Dim cmd1 As New SqlCommand(" Select Studentid From Exam_Examformstudent where Studentid = '" & ViewState("stuid") & "' and Courseexamid='" & corseexamid & "' ", con)
            Dim da As New SqlDataAdapter(cmd1)
            cmd1.Connection = con
            da.Fill(ds)
            Dim i = ds.Tables(0).Rows.Count()
            If (i > 0) Then
                SaralMsg.Messagebx.Alert(Me, "Exam Form Submitted Already")
            Else
                Response.Redirect("StudentExamform.aspx?Admissionno=" & Session("Admissionno") & "&acyr=" & Session("Batch") & "&Courseexamid = " & Session("courxe") & "&u=" & ViewState("stuid") & "&s=" & ViewState("Session") & "&ayid=" & ViewState("ayid") & "&corseexamid=" & corseexamid)
            End If
            con.Close()
        End Using
    End Sub
    Private Sub Admitcard(ByVal examid As String)
        Dim ds As New DataSet
        Using con As New SqlConnection(constr)
            con.Open()
            Dim cmd1 As New SqlCommand(" Select Studentid From Exam_Examformstudent where Studentid = '" & ViewState("stuid") & "' and Courseexamid='" & examid & "' and Isexamformverified = '1' ", con)
            Dim da As New SqlDataAdapter(cmd1)
            cmd1.Connection = con
            da.Fill(ds)
            Dim i = ds.Tables(0).Rows.Count()
            If (i > 0) Then
                Response.Redirect("Admitcard.aspx?Admissionno=" & Session("Admissionno") & "&acyr=" & Session("Batch") & "&Courseexamid=" & examid & "&ayid=" & ViewState("ayid") & "&s=" & ViewState("Session") & "&u=" & ViewState("stuid"))

            Else
                SaralMsg.Messagebx.Alert(Me, "Admit Card Not Available")
            End If
            con.Close()
        End Using
    End Sub
    Private Sub Bindviewexam(ByVal examid As String, ByVal stuid As String)
        Dim ds As New DataSet
        Using con As New SqlConnection(constr)
            con.Open()
            Dim cmd1 As New SqlCommand(" Select Studentid From Exam_Examformstudent where Studentid = '" & stuid & "' and Courseexamid='" & examid & "' ", con)
            Dim da As New SqlDataAdapter(cmd1)
            cmd1.Connection = con
            da.Fill(ds)
            Dim i = ds.Tables(0).Rows.Count()
            If (i > 0) Then
                Response.Redirect("Viewexamform.aspx?Admissionno=" & Session("Admissionno") & "&acyr=" & Session("Batch") & "&Courseexamid = " & Session("courxe") & "&CrsExamid=" & examid & "&stuid=" & stuid & "&s=" & ViewState("Session") & "&ayid=" & ViewState("ayid"))

            Else
                SaralMsg.Messagebx.Alert(Me, "You Have Not Apply For Exam First Apply")
            End If
            con.Close()
        End Using
    End Sub


    Protected Sub btnLogout_Click(sender As Object, e As System.EventArgs) Handles btnLogout.Click
        Response.Redirect("~/LoginFinal.aspx")
    End Sub

End Class
