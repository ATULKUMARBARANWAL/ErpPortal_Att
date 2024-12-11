Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Partial Class UserPortal_RevlutionDash
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("uid") = Request.QueryString("u")
            ViewState("sessionid") = Request.QueryString("s")
            Session("StudentID") = Request.QueryString("stuid")

            ' Assuming CourseID and ClassesID are part of the query string
            ViewState("CourseID") = Request.QueryString("CourseID")
            ViewState("ClassesID") = Request.QueryString("ClassesID")

            takeStudentData()
            BindExamCreate()
        End If
    End Sub

    Private Sub takeStudentData()
        Dim query As String = "SELECT CourseID, ClassesID FROM Student WHERE StudentID = @StudentID"
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@StudentID", Request.QueryString("stuid"))
                con.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim courseId As Integer = reader("CourseID")
                        Dim classesId As Integer = reader("ClassesID")

                        ' Store the data in ViewState
                        ViewState("CourseID") = courseId
                        ViewState("ClassesID") = classesId
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub BindExamCreate()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("select tt.Timetableid,pe.Employee as Teacher,tt.prd,ec.course,cl.Classes,stu.userid, " & _
" tt.ClassRoom,tt.Grp,sub.Subject,tt.Teach_Type from timetable tt " & _
" left join p_employee pe on tt.teacherid=pe.employeeID " & _
" left join Exam_Course ec on tt.Courseid=ec.Courseid " & _
" left join Classes cl on tt.ClassesID=cl.ClassesID " & _
" left join Subject sub on tt.SubjectID=sub.SubjectID " & _
" left join student stu on tt.userid=stu.userid " & _
" where ec.Courseid='15' and stu.StudentID='" & Request.QueryString("u") & "' ")
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
    End Sub
    Private Sub BindgrdPrograms()
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()

            ' Parse the from and to dates
            Dim fromDate As DateTime = DateTime.ParseExact(TxtFromDate.Text, "yyyy-MM-dd", Nothing)
            Dim toDate As DateTime = DateTime.ParseExact(TextBox1.Text, "yyyy-MM-dd", Nothing)

            ' SQL query to get the attendance data for the student within the date range
            Dim sql As String = "SELECT s.student, sa.Dated, sa.status " & _
                                "FROM StudentAtt sa " & _
                                "JOIN student s ON sa.studentid = s.studentid " & _
                                "WHERE sa.Dated BETWEEN @fromDate AND @toDate " & _
                                "AND s.studentid = @Studentid and sa.status is not null"

            cmd.CommandText = sql
            cmd.Connection = con

            ' Add parameters to prevent SQL injection
            cmd.Parameters.AddWithValue("@fromDate", fromDate)
            cmd.Parameters.AddWithValue("@toDate", toDate)
            cmd.Parameters.AddWithValue("@Studentid", Request.QueryString("stuid"))

            Using sda As New SqlDataAdapter(cmd)
                Using dt As New DataTable()
                    sda.Fill(dt)

                    ' Transform data into row format for binding
                    Dim pivotedData As New DataTable()
                    pivotedData.Columns.Add("SerialNo", GetType(Integer))
                    pivotedData.Columns.Add("Student", GetType(String))
                    pivotedData.Columns.Add("Date", GetType(String))
                    pivotedData.Columns.Add("Status", GetType(String))

                    ' Populate pivotedData with date-wise status for each student
                    Dim studentName As String = If(dt.Rows.Count > 0, dt.Rows(0)("student").ToString(), "")
                    Dim currentDate As DateTime = fromDate
                    Dim serialNumber As Integer = 1

                    While currentDate <= toDate
                        Dim statusRow = dt.AsEnumerable().FirstOrDefault(Function(row) row.Field(Of DateTime)("Dated") = currentDate)
                        Dim status As String = If(statusRow IsNot Nothing, statusRow("status").ToString(), "N/A")

                        If Not String.IsNullOrEmpty(status) AndAlso status <> "N/A" Then
                            Dim newRow As DataRow = pivotedData.NewRow()
                            newRow("SerialNo") = serialNumber
                            newRow("Student") = studentName
                            newRow("Date") = currentDate.ToString("yyyy-MM-dd")
                            newRow("Status") = status
                            pivotedData.Rows.Add(newRow)

                            serialNumber += 1
                        End If

                        currentDate = currentDate.AddDays(1)
                    End While

                    ' Bind the transformed data
                    grdPrograms.DataSource = pivotedData
                    grdPrograms.DataBind()
                End Using
            End Using
        End Using
    End Sub



    Private Function GetData() As DataTable

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("GET_ATTENDANCEREPORT")
                cmd.Parameters.AddWithValue("@STARTDATE", TxtFromDate.text)
                cmd.Parameters.AddWithValue("@ENDDATE", TextBox1.Text)
                cmd.Parameters.AddWithValue("@Teacherid", ViewState("userid"))
                cmd.Parameters.AddWithValue("@courseid", ViewState("courseid"))
                cmd.Parameters.AddWithValue("@sem", ViewState("sem"))
                cmd.Parameters.AddWithValue("@subjectid", ViewState("subjectid"))
                cmd.Parameters.AddWithValue("@Classesid", ViewState("classid"))
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    cmd.CommandType = CommandType.StoredProcedure
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Return dt
                    End Using
                End Using
            End Using
        End Using
    End Function
    

    Protected Sub totalAttendance()
        ' Query using subqueries to calculate total, present, and absent attendance
       
    Dim query As String = "" & _
        "SELECT " & _
        "(SELECT COUNT(*) " & _
        " FROM StudentAtt " & _
        " WHERE CourseID = @CourseID AND ClassesID = @ClassesID AND StudentID = @StudentID) AS TotalAttendance, " & _
        "(SELECT COUNT(*) " & _
        " FROM StudentAtt " & _
        " WHERE CourseID = @CourseID AND ClassesID = @ClassesID AND StudentID = @StudentID AND Status = 'P') AS PresentAttendance, " & _
        "(SELECT COUNT(*) " & _
        " FROM StudentAtt " & _
        " WHERE CourseID = @CourseID AND ClassesID = @ClassesID AND StudentID = @StudentID AND Status = 'A') AS AbsentAttendance"


        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query, con)

                ' Add parameters
                If ViewState("CourseID") IsNot Nothing AndAlso ViewState("ClassesID") IsNot Nothing Then
                    cmd.Parameters.AddWithValue("@CourseID", ViewState("CourseID"))
                    cmd.Parameters.AddWithValue("@ClassesID", ViewState("ClassesID"))
                Else
                    Throw New Exception("CourseID or ClassesID is missing in ViewState.")
                End If

                cmd.Parameters.AddWithValue("@StudentID", Request.QueryString("stuid"))

                con.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim totalAttendance As Integer = Convert.ToInt32(reader("TotalAttendance"))
                        Dim presentAttendance As Integer = Convert.ToInt32(reader("PresentAttendance"))
                        Dim absentAttendance As Integer = Convert.ToInt32(reader("AbsentAttendance"))

                        ' Concatenate the values and display in a single Label
                        AttendanceCountLabel.Text = "Total Attendance: " & totalAttendance.ToString() & " , Present Attendance: " & presentAttendance.ToString() & " , Absent Attendance: " & absentAttendance.ToString()
                    End If
                End Using
            End Using
        End Using
    End Sub
    Protected Sub UpdateTotalAttendentce()
        ' Parse the from and to dates
        Dim fromDate As DateTime = DateTime.ParseExact(TxtFromDate.Text, "yyyy-MM-dd", Nothing)
        Dim toDate As DateTime = DateTime.ParseExact(TextBox1.Text, "yyyy-MM-dd", Nothing)

        ' Query using subqueries to calculate total, present, and absent attendance based on date range
        Dim query As String = "SELECT " & _
            "(SELECT COUNT(*) " & _
            " FROM StudentAtt sa " & _
            " WHERE CourseID = @CourseID AND ClassesID = @ClassesID AND StudentID = @StudentID " & _
            " AND sa.Dated BETWEEN @fromDate AND @toDate) AS TotalAttendance, " & _
            "(SELECT COUNT(*) " & _
            " FROM StudentAtt sa" & _
            " WHERE CourseID = @CourseID AND ClassesID = @ClassesID AND StudentID = @StudentID " & _
            " AND Status = 'P' AND sa.Dated BETWEEN @fromDate AND @toDate) AS PresentAttendance, " & _
            "(SELECT COUNT(*) " & _
            " FROM StudentAtt sa" & _
            " WHERE CourseID = @CourseID AND ClassesID = @ClassesID AND StudentID = @StudentID " & _
            " AND Status = 'A' AND sa.Dated BETWEEN @fromDate AND @toDate) AS AbsentAttendance"

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query, con)
                ' Add parameters to prevent SQL injection
                If ViewState("CourseID") IsNot Nothing AndAlso ViewState("ClassesID") IsNot Nothing Then
                    cmd.Parameters.AddWithValue("@CourseID", ViewState("CourseID"))
                    cmd.Parameters.AddWithValue("@ClassesID", ViewState("ClassesID"))
                Else
                    Throw New Exception("CourseID or ClassesID is missing in ViewState.")
                End If

                cmd.Parameters.AddWithValue("@StudentID", Request.QueryString("stuid"))
                cmd.Parameters.AddWithValue("@fromDate", fromDate)
                cmd.Parameters.AddWithValue("@toDate", toDate)

                con.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim totalAttendance As Integer = Convert.ToInt32(reader("TotalAttendance"))
                        Dim presentAttendance As Integer = Convert.ToInt32(reader("PresentAttendance"))
                        Dim absentAttendance As Integer = Convert.ToInt32(reader("AbsentAttendance"))

                        ' Concatenate the values and display in a single Label
                        AttendanceCountLabel.Text = "Total Attendance: " & totalAttendance.ToString() & " , Present Attendance: " & presentAttendance.ToString() & " , Absent Attendance: " & absentAttendance.ToString()
                    End If
                End Using
            End Using
        End Using
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click

    End Sub




    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Response.Redirect("~/LoginPage.aspx")
    End Sub

    Protected Sub btnHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHome.Click
        Response.Redirect("~/UserPortal/DashboardStu.aspx?u=" & Request.QueryString("u") & "&s=" & ViewState("sessionid"))
    End Sub

    Protected Sub btnViewAttend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnViewAttend.Click
        PnlViewAttendance.Visible = True
        PnlTimeTable.Visible = False
        totalAttendance()
    End Sub
    'Protected Sub btnApplyForLeave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApplyForLeave.Click

    '    pnlViewApplyForLeave.Visible = True
    'End Sub

    Protected Sub btnTimeTable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTimeTable.Click
        PnlViewAttendance.Visible = False
        PnlTimeTable.Visible = True
    End Sub

    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        Response.Redirect("~/UserPortal/DashboardStu.aspx?u=" & Request.QueryString("u") & "&s=" & ViewState("sessionid"))
    End Sub

    Protected Sub LinkButton3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton3.Click
        PnlViewAttendance.Visible = False
        PnlTimeTable.Visible = True
    End Sub

    Protected Sub btnAdvanceFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdvanceFilter.Click
        BindgrdPrograms()
        UpdateTotalAttendentce()
    End Sub
End Class
Public Class DynamicItemTemplate
    Implements ITemplate

    Private _columnName As String

    Public Sub New(ByVal columnName As String)
        _columnName = columnName
    End Sub

    Public Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
        Dim lbl As New Label()
        AddHandler lbl.DataBinding, AddressOf Me.BindData
        container.Controls.Add(lbl)
    End Sub

    Private Sub BindData(ByVal sender As Object, ByVal e As EventArgs)
        Dim lbl As Label = CType(sender, Label)
        Dim dataItem As DataRowView = CType(lbl.NamingContainer, GridViewRow).DataItem

        If dataItem IsNot Nothing AndAlso dataItem.Row.Table.Columns.Contains(_columnName) Then
            lbl.Text = dataItem(_columnName).ToString()
        Else
            lbl.Text = "N/A" ' Fallback if the column is not found
        End If
    End Sub
End Class
Public Class DynamicSerialNumberTemplate
    Implements ITemplate

    Public Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
        Dim lblSerial As New Label()
        AddHandler lblSerial.DataBinding, AddressOf lblSerial_DataBinding
        container.Controls.Add(lblSerial)
    End Sub

    Private Sub lblSerial_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
        Dim lblSerial As Label = CType(sender, Label)
        Dim container As GridViewRow = CType(lblSerial.NamingContainer, GridViewRow)
        lblSerial.Text = (container.RowIndex + 1).ToString()
    End Sub
End Class
