Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net.Mail
Partial Class TESTFILES_RollNoGenration
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    'Working on Page Load
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Me.IsPostBack Then
            ViewState("Sessionid") = Request.QueryString("s")
            bindddlAcayear()
        End If
    End Sub

    'Function for binding Dropdown
    Private Sub BindDropDownList1(ByVal ddl2 As DropDownList, ByVal query As String, ByVal text As String, ByVal value As String, ByVal defaultText As String)
        Dim conString As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
        Dim cmd As New SqlCommand(query)
        Using con As New SqlConnection(conString)
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
        ddl2.Items.Insert(0, New ListItem(defaultText, "0"))
    End Sub

    'Function for binding Grid
    Private Sub BindGrid(ByVal grd As GridView, ByVal query As String)
        Dim conString As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
        Dim cmd As New SqlCommand(query)
        Using con As New SqlConnection(conString)
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                con.Open()
                Using dt As New DataTable()
                    sda.Fill(dt)
                    grd.DataSource = dt
                    grd.DataBind()
                End Using
                con.Close()
            End Using
        End Using
    End Sub

    'bind Grid OF Course
    'Private Sub BindGridCourse()
    '    Dim query As String = ("select c.Courseid, c.ExamName, cs.courselevel from Exam_CourseExam c Join Exam_coursesession cs on cs.courseid=c.courseid " & _
    '                           " where c.Courseid='" & ddlcourse.SelectedItem.Value & "' and c.ExamName='" & ddlexamname.SelectedItem.Text & "' and cs.courselevel='" & ddllevel.SelectedItem.Text & "'")
    '    Dim cmd As New SqlCommand(query)
    '    Using con As New SqlConnection(constr)
    '        Using sda As New SqlDataAdapter()
    '            cmd.Connection = con
    '            sda.SelectCommand = cmd
    '            con.Open()
    '            Using dt As New DataTable()
    '                sda.Fill(dt)
    '                grdstudent.DataSource = dt
    '                grdstudent.DataBind()
    '            End Using
    '            con.Close()
    '        End Using
    '    End Using
    'End Sub

    'to Show Panel to generate Roll Number
    'Protected Sub grdstudent_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdstudent.RowCommand
    '    If e.CommandName = "View" Then
    '        Panel1.Visible = False
    '        Panel2.Visible = True
    '        btnbackstudent.Visible = True
    '        'Bind Grid Of Students
    '        BindGrid1()
    '    End If
    'End Sub

    'back button to going previous

    Protected Sub btnbackstudent_Click(sender As Object, e As System.EventArgs) Handles btnbackstudent.Click
        Panel1.Visible = True
        Panel2.Visible = False
    End Sub

    'bind grid of students to generate roll number
    Private Sub BindGrid1()
        Dim query As String = ("Select CONVERT(varchar,Sr.DOB,103) as Dateofbirth,CONVERT(varchar,Sr.DOB,103) as 'AdmissionNo', C.Course, Sr.* from StuRegistration Sr " & _
 " left join Exam_Course C on Sr.Courseid=C.Courseid " & _
" where Sr.Sessionid='" & ViewState("Sessionid") & "' and Sr.RegistrationApproved='1' and Sr.Courseid='" & ddlcourse.SelectedValue & "' and Sr.Registrationid not in " & _
" (Select st.Registrationid From Student st where st.Registrationid is not null) order by Sr.FirstName ")
        BindGrid(GridView1, query)  
    End Sub

    'button generate roll number and check also Admission number
    Protected Sub btngeneraterollno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btngeneraterollno.Click
        Dim condition1 As String = String.Empty
        Dim condition As String = String.Empty
        Dim Admissionno1 As String = String.Empty
        For Each row As GridViewRow In GridView1.Rows
            If (TryCast(row.FindControl("btnselect"), CheckBox)).Checked Then
                Dim Admissionno As String = row.Cells(2).Text
                Dim sem As String = row.Cells(4).Text
                condition1 += If(String.Format("'{0}',", row.Cells(2).Text), String.Empty)

                
                            Me.Generaterollno(Admissionno, sem)
                        End If
                        
        Next
        Dim message As String = "Successfully."
        Dim script As String = "window.onload=function(){alert('"
        script &= message
        script &= "');"
        script &= "window.location = '"
        script &= Request.Url.AbsoluteUri
        script &= "'; }"
        ClientScript.RegisterStartupScript(Me.GetType(), "SuccessMessage", script, True)
       


    End Sub

    'bind ddl Academic Year
    Private Sub bindddlAcayear()
        Dim query As String = ("select distinct Academicyear from Exam_Session  order by Academicyear")
        BindDropDownList1(ddlacademicyear, query, "Academicyear", "Academicyear", "Select Academic Year")
    End Sub

    'bind ddl Course
    Protected Sub ddllevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddllevel.SelectedIndexChanged
      
        If ddllevel.SelectedItem.Text = "UG" Then
            Dim query As String = ("select c.* from Exam_CourseSession cs join Exam_Course c on c.Courseid=cs.Courseid where cs.courselevel='UG'")
            BindDropDownList1(ddlcourse, query, "Course", "Courseid", "Select Program")

        ElseIf ddllevel.SelectedItem.Text = "PG" Then
            Dim query As String = ("select c.* from Exam_CourseSession cs join Exam_Course c on c.Courseid=cs.Courseid where cs.courselevel='PG'")
            BindDropDownList1(ddlcourse, query, "Course", "Courseid", "Select Program")

        ElseIf ddllevel.SelectedItem.Text = "Diploma" Then
            Dim query As String = ("select c.* from Exam_CourseSession cs join Exam_Course c on c.Courseid=cs.Courseid where cs.courselevel='Diploma'")
            BindDropDownList1(ddlcourse, query, "Course", "Courseid", "Select Program")

        End If
    End Sub


    

    'Function to generate Roll number through Store Procedure
    Private Sub Generaterollno(ByVal Admissionno As String, ByVal sem As String)
        Using con As SqlConnection = New SqlConnection(constr)
            Using cmd As SqlCommand = New SqlCommand("Getrollno", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@Academicyear", ViewState("Academicyear"))
                cmd.Parameters.AddWithValue("@Admissionno", Admissionno)
                cmd.Parameters.AddWithValue("@courseid", ViewState("Courseid"))
                cmd.Parameters.AddWithValue("@CourseExamid", "")
                cmd.Parameters.AddWithValue("@userid", "1197")
                cmd.Parameters.AddWithValue("@sem", sem)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using
       
    End Sub

    'Bind Grid Of Course To view Students 
    'Protected Sub ddlexamname_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlexamname.SelectedIndexChanged
    '    ViewState("ExamNameid") = ddlexamname.SelectedItem.Value

    '    Me.BindGridCourse()
    'End Sub

 
    Protected Sub btnload_Click(sender As Object, e As System.EventArgs) Handles btnload.Click
        If ddlacademicyear.SelectedItem.Text = "Select Academic Year" Or ddllevel.SelectedItem.Value = "" Or ddlcourse.SelectedItem.Value = "" Then
            Dim message As String = "Please Select All Fields."
            Dim script As String = "window.onload=function(){alert('"
            script &= message
            script &= "');"
            script &= "window.location = '"
            script &= Request.Url.AbsoluteUri
            script &= "'; }"
            ClientScript.RegisterStartupScript(Me.GetType(), "SuccessMessage", script, True)
        Else
            ViewState("Courseid") = ddlcourse.SelectedItem.Value
            ViewState("Academicyear") = ddlacademicyear.SelectedItem.Text
            BindGrid1()
            Panel1.Visible = False
            Panel2.Visible = True
            btnbackstudent.Visible = True
        End If
       
    End Sub
End Class
