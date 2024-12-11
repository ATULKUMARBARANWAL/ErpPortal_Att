Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Partial Class Examinationjune_raba
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Me.BindGrid()
            ViewState("StudentID") = Request.QueryString("StudentID")
            lblrollno.Text = Session("rollno")
            lblenrollment.Text = Session("enrollment")
            lblsemyear.Text = Session("semyear")
            Fetchstudentdetail()
        End If
    End Sub
    Private Sub BindGrid()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(" select  distinct excsub.Subjectid, exs.Subject,exs.Subjectcode from Student s  join Exam_Coursesubject excsub on s.CourseID=excsub.Courseid join Exam_Subject exs on exs.Subjectid = excsub.Subjectid where s.AdmissionNo='" + Session(" Admissionno").ToString + "'", con)
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
    Private Sub lblenrollment1()
        Dim ds As New DataSet
        Using con As New SqlConnection(constr)
            con.Open()
            Dim cmd = New SqlCommand("Select EnrollmentNo From Student ='" + ViewState("Studentid") + "'", con)
            ' Dim cmd = New SqlCommand("select (Drink+'/'+Smoke) As Drink,(C_City+','+C_State+'('+C_Country +')') As Familylocation,*    from Matri_Registration  where  UserId='" + Request.QueryString("uid") + "'", con)
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(ds)
            Dim i = ds.Tables(0).Rows.Count()
            If (i > 0) Then
                lblenrollment.Text = ds.Tables(0).Rows(0)("EnrollmentNo").ToString()
            End If
        End Using
    End Sub

    Private Sub Fetchstudentdetail()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("select s.Student,s.Sem,s.FatherName,ec.Course,s.EnrollmentNo,s.Roll_No from Student s Join Exam_Course ec  on ec.Courseid=s.Courseid  where AdmissionNo='" + Session(" Admissionno").ToString + "'", con)
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        lblFatherName.Text = dt.Rows(0)("FatherName").ToString()
                        lblStudentName.Text = dt.Rows(0)("Student").ToString()
                        lblProgramCourse.Text = dt.Rows(0)("Course").ToString
                        lblsemyear.Text = dt.Rows(0)("Sem").ToString()
                    End Using
                End Using
            End Using
        End Using
    End Sub
End Class