Imports System.Data.SqlClient
Imports System.Data
Partial Class Examinationjune_Studentsubjectfeepract
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("Sessionid") = "13"
            ViewState("Ac_year") = "2024"
            ViewState("courseid") = "1"                                    '<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<,
            ViewState("ayid") = "25"
            ViewState("userid") = "1197"
            Session("userid") = "1197"

            fetchDdlprogram()
            Ddlsemyear.Items.Clear()
            fillddlsemyear()

            'DdlSubject.Items.Clear()
            'fillddlsubject()
            If ddlClass.SelectedValue IsNot Nothing AndAlso ddlClass.SelectedValue <> String.Empty AndAlso _
               Ddlsemyear.SelectedValue IsNot Nothing AndAlso Ddlsemyear.SelectedValue <> String.Empty AndAlso _
               Ddlsemyear.Items.Count > 0 Then
                DdlSubject.Items.Clear()
                fillddlsubject()
            End If

            BindEmptyGridView()
        End If
    End Sub

    ' Bind the GridView with empty data to show column headers initially
    Private Sub BindEmptyGridView()
        Dim dt As New DataTable()

        dt.Columns.Add("StudentId", GetType(String))
        dt.Columns.Add("Student", GetType(String))
        dt.Columns.Add("SubjectFee", GetType(Decimal))
        dt.Columns.Add("PracticalFee", GetType(Decimal))
        dt.Columns.Add("AbsentFine", GetType(Decimal))
        dt.Columns.Add("FileFine", GetType(Decimal))
        'dt.Columns.Add("TotalCollection", GetType(Decimal))

        dt.Rows.Add(dt.NewRow()) ' Add an empty row to show column headers

        gvSubjectFees.DataSource = dt
        gvSubjectFees.DataBind()
    End Sub

    ' Load the class dropdown from the database

    Private Sub fetchDdlprogram()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Cs.Academicyear, Cs.Courseid, C.Course from Exam_CourseSession Cs join Exam_Course C on Cs.Courseid =C.Courseid " & _
                                " where Cs.Academicyear ='" & ViewState("Ac_year") & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        ddlClass.DataSource = dt
                        ddlClass.DataTextField = "Course"
                        ddlClass.DataValueField = "Courseid"
                        ddlClass.DataBind()
                        'Dim Year As Integer
                        'Year = Convert.ToInt32(Now.ToString("yyyy"))  

                        ddlClass.Items.FindByValue(ViewState("courseid")).Selected = True

                    End Using
                End Using
            End Using
        End Using


    End Sub

    ' Load the semester dropdown based on the selected class

    Private Sub fillddlsemyear()

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Coursesessionid, Academicyear, Courseid,  Case when CourseType like '%sem%' then 'Semester' when CourseType like '%year%' then 'Year' end as 'CourseType', case when Coursetype Like '%sem%' then Duration*2 " & _
" when Coursetype like '%year%' then Duration*1 when Coursetype like '%quart%' then Duration*4 end as 'Totalsem', " & _
 " Duration, Coursetype from Exam_CourseSession where  Academicyear ='" & ViewState("Ac_year") & "' and Courseid = '" & ddlClass.SelectedValue & "' ")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Ddlsemyear.DataSource = dt
                        Dim totalsem As String = dt.Rows(0)("Totalsem").ToString()
                        'Lblsemyear.Text = dt.Rows(0)("CourseType").ToString()
                        Dim i As Integer
                        For i = 1 To totalsem

                            Ddlsemyear.Items.Add(New ListItem(i.ToString(), i.ToString()))
                        Next

                        Ddlsemyear.Items.Insert(0, New ListItem("Select Semester", "0"))
                    End Using
                End Using
            End Using
        End Using
    End Sub


    Private Sub fillddlsubject()
        Using con As New SqlConnection(constr)
            Dim query As String = "SELECT DISTINCT sis.subjectid, es.Subject, es.Subjectcode FROM Student s JOIN StudentindividualSubject sis ON s.StudentID = sis.StudentID JOIN Exam_Subject es ON sis.subjectid = es.Subjectid WHERE s.Courseid = @CourseID AND s.Sem = @Semester"

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@CourseID", ddlClass.SelectedValue)
                cmd.Parameters.AddWithValue("@Semester", Ddlsemyear.SelectedValue)

                Using sda As New SqlDataAdapter(cmd)
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        DdlSubject.Items.Clear()

                        If dt.Rows.Count > 0 Then
                            DdlSubject.Items.Add(New ListItem("Select Subject", "0"))

                            For Each row As DataRow In dt.Rows
                                Dim subjectId As Integer = Convert.ToInt32(row("subjectid"))
                                Dim subjectName As String = row("Subject").ToString()
                                Dim subjectCode As String = row("Subjectcode").ToString()

                                'ViewState("subjectid") = subjectId

                                Dim itemText As String = subjectName & " (" & subjectCode & ")"
                                DdlSubject.Items.Add(New ListItem(itemText, subjectId.ToString()))
                            Next
                        Else
                            DdlSubject.Items.Add(New ListItem("No subjects available", "0"))
                        End If
                    End Using
                End Using
            End Using
        End Using
    End Sub



    'Protected Sub ddlSemester_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
    'LoadGridView()
    'End Sub
    Private Sub LoadGridView()
        Dim query As String = "SELECT s.StudentID, s.Student, sf.SubjectFee, sf.PracticalFee, sf.AbsentFine, sf.FileFine " & _
                              "FROM Student s " & _
                              "JOIN StudentindividualSubject sis ON s.StudentID= sis.StudentID " & _
                              "Left JOIN StudentFees sf ON s.StudentID = sf.StudentID " & _
                              "WHERE s.CourseID = @Courseid AND s.Sem = @Sem AND sis.Subjectid = @SubjectId"

        'Dim subjectId As Integer = ViewState("subjectid").ToString()

        Dim conString As String = constr
        Using con As New SqlConnection(conString)
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@Courseid", ddlClass.SelectedValue)
                cmd.Parameters.AddWithValue("@Sem", Ddlsemyear.SelectedValue)
                'cmd.Parameters.AddWithValue("@SubjectId", subjectId) 
                cmd.Parameters.AddWithValue("@SubjectId", DdlSubject.SelectedValue)

                con.Open()
                Dim sda As New SqlDataAdapter(cmd)
                Dim dt As New DataTable()
                sda.Fill(dt)

                gvSubjectFees.DataSource = dt
                gvSubjectFees.DataBind()
                con.Close()
            End Using
        End Using
    End Sub


    ' Save button event handler (implement your save logic here)
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim query As String = ""

        Using con As New SqlConnection(constr)
            con.Open()

            For Each row As GridViewRow In gvSubjectFees.Rows

                If row.RowType = DataControlRowType.DataRow Then
                    Dim studentId As String = row.Cells(1).Text
                    'Dim studentName As String = row.Cells(2).Text

                    Dim subjectFee As Object = If(String.IsNullOrWhiteSpace(CType(row.FindControl("txtSubjectFee"), TextBox).Text), DBNull.Value, Convert.ToDecimal(CType(row.FindControl("txtSubjectFee"), TextBox).Text))
                    Dim practicalFee As Object = If(String.IsNullOrWhiteSpace(CType(row.FindControl("txtPracticalFee"), TextBox).Text), DBNull.Value, Convert.ToDecimal(CType(row.FindControl("txtPracticalFee"), TextBox).Text))
                    Dim absentFine As Object = If(String.IsNullOrWhiteSpace(CType(row.FindControl("txtAbsentFine"), TextBox).Text), DBNull.Value, Convert.ToDecimal(CType(row.FindControl("txtAbsentFine"), TextBox).Text))
                    Dim fileFine As Object = If(String.IsNullOrWhiteSpace(CType(row.FindControl("txtFileFine"), TextBox).Text), DBNull.Value, Convert.ToDecimal(CType(row.FindControl("txtFileFine"), TextBox).Text))

                    'Dim subjectId As Integer = ViewState("subjectid").ToString()
                    Dim subjectId As Integer = Convert.ToInt32(DdlSubject.SelectedValue)
                    Dim saveDate As DateTime = DateTime.Now
                    Dim Academicyear As String = ViewState("Ac_year").ToString()
                    Dim Sessionid As String = ViewState("Sessionid").ToString()

                    ' Check if the subject exists in the database
                    Dim checkQuery As String = "SELECT COUNT(1) FROM StudentFees WHERE SubjectId = @SubjectId AND Courseid = @Courseid AND Sem = @Sem AND Studentid = @Studentid"
                    Dim subjectExists As Boolean = False

                    Using checkCmd As New SqlCommand(checkQuery, con)
                        checkCmd.Parameters.AddWithValue("@Courseid", ddlClass.SelectedValue)
                        checkCmd.Parameters.AddWithValue("@Sem", Ddlsemyear.SelectedValue)
                        checkCmd.Parameters.AddWithValue("@SubjectId", subjectId)
                        checkCmd.Parameters.AddWithValue("@Studentid", studentId)
                        subjectExists = Convert.ToBoolean(checkCmd.ExecuteScalar())
                    End Using

                    If subjectExists Then
                        ' Update query
                        query = "UPDATE StudentFees SET SubjectFee = @SubjectFee, PracticalFee = @PracticalFee, AbsentFine = @AbsentFine, FileFine = @FileFine, AcademicYear = @Academicyear, Sessionid = @Sessionid, Dated= @Dated WHERE Courseid = @Courseid AND Sem = @Sem AND SubjectId = @SubjectId AND Studentid =@Studentid"
                    Else
                        ' Insert query
                        query = "INSERT INTO StudentFees (Studentid, SubjectId, Courseid, Sem, SubjectFee, PracticalFee, AbsentFine, FileFine, AcademicYear, Sessionid, Dated) VALUES (@Studentid, @SubjectId, @Courseid, @Sem, @SubjectFee, @PracticalFee, @AbsentFine, @FileFine, @Academicyear, @Sessionid , @Dated)"
                    End If

                    Using cmd As New SqlCommand(query, con)
                        cmd.Parameters.AddWithValue("@Studentid", studentId)
                        cmd.Parameters.AddWithValue("@Courseid", ddlClass.SelectedValue)
                        cmd.Parameters.AddWithValue("@Sem", Ddlsemyear.SelectedValue)
                        cmd.Parameters.AddWithValue("@SubjectId", subjectId)
                        cmd.Parameters.AddWithValue("@SubjectFee", subjectFee)
                        cmd.Parameters.AddWithValue("@PracticalFee", practicalFee)
                        cmd.Parameters.AddWithValue("@AbsentFine", absentFine)
                        cmd.Parameters.AddWithValue("@FileFine", fileFine)
                        cmd.Parameters.AddWithValue("@Dated", saveDate)
                        cmd.Parameters.AddWithValue("@Academicyear", Academicyear)
                        cmd.Parameters.AddWithValue("@Sessionid", Sessionid)

                        cmd.ExecuteNonQuery()
                    End Using
                End If
            Next

            con.Close()
        End Using


        LoadGridView()
    End Sub

    Protected Sub Ddlsemyear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ddlsemyear.SelectedIndexChanged
        BindEmptyGridView()

        fillddlsubject()
        ' LoadGridView()

    End Sub
    Protected Sub DdlSubject_SelectedIndexChanged1(ByVal sender As Object, ByVal e As EventArgs) Handles DdlSubject.SelectedIndexChanged
        LoadGridView()
    End Sub

End Class

