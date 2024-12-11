Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.IO

Partial Class Attendance_Attrpt
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            ViewState("userid") = Request.QueryString("u")
            ViewState("courseid") = Request.QueryString("Courseid")

            ViewState("classid") = Request.QueryString("classesid")
            ViewState("Acyr") = Request.QueryString("acyr")

            lblprogram.Text = Request.QueryString("CourseName")

            Lblsection.Text = Request.QueryString("ClassesName")

            'getseminfo()
            Dim today As String = DateTime.Now.ToString("yyyy-MM-dd")


            Dim previoustoday As String = DateAdd("d", -7, DateTime.Now).ToString("yyyy-MM-dd")

            TxtTodate.text = today
            TxtFromDate.text = previoustoday


            BindgrdPrograms()
            'totalPresent()
            'totalAbsent()
            totalClass()

        Else
            ViewState("userid") = Request.QueryString("u")
            ViewState("courseid") = Request.QueryString("Courseid")
            ViewState("classid") = Request.QueryString("classesid")
            ViewState("Acyr") = Request.QueryString("acyr")

            lblprogram.Text = Request.QueryString("CourseName")

            Lblsection.Text = Request.QueryString("ClassesName")
            'getseminfo()
            BindgrdPrograms()

        End If
    End Sub
    Private Sub totalClass()
        Dim count As Integer = 0 ' Declare count variable outside the Using block

        Using con As New SqlConnection(constr)
            con.Open()

            ' SQL query to count distinct dates
            Dim sql As String = "SELECT COUNT(DISTINCT Dated) " & _
                                "FROM StudentAtt " & _
                                "WHERE TeacherID = @TeacherID " & _
                                "AND CourseID = @CourseID " & _
                                "AND ClassesID = @ClassesID"

            Using cmd As New SqlCommand(sql, con)
                ' Add query parameters
                cmd.Parameters.AddWithValue("@TeacherID", Request.QueryString("u"))
                cmd.Parameters.AddWithValue("@CourseID", ViewState("courseid"))
                cmd.Parameters.AddWithValue("@ClassesID", ViewState("classid"))

                ' Execute the query and store the result
                count = Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using

        ' Update the value of an ASP.NET label
        totalCountLabel.Text = "Total Classes: " & count
    End Sub



  
    Private Sub BindgrdPrograms()
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim fromDate As DateTime = DateTime.ParseExact(TxtFromDate.Text, "yyyy-MM-dd", Nothing)
            Dim toDate As DateTime = DateTime.ParseExact(TxtTodate.Text, "yyyy-MM-dd", Nothing)

            Dim sql As String = "SELECT s.student, s.studentID, sa.Dated, sa.status " &
                                "FROM StudentAtt sa " &
                                "JOIN student s ON sa.studentid = s.studentid " &
                                "WHERE sa.Dated BETWEEN @fromDate AND @toDate " &
                                "AND sa.teacherid = @Teacherid " &
                                "AND sa.CourseID = @CourseID"

            cmd.CommandText = sql
            cmd.Connection = con
            cmd.Parameters.AddWithValue("@fromDate", fromDate)
            cmd.Parameters.AddWithValue("@toDate", toDate)
            cmd.Parameters.AddWithValue("@Teacherid", ViewState("userid"))
            cmd.Parameters.AddWithValue("@CourseID", Convert.ToInt32(ViewState("courseid")))

            Using sda As New SqlDataAdapter(cmd)
                Using dt As New DataTable()
                    sda.Fill(dt)

                    ' Ensure required columns exist
                    AddColumnIfNotExists(dt, "TotalAttendance", GetType(Integer))
                    AddColumnIfNotExists(dt, "TotalAbsent", GetType(Integer))
                    AddColumnIfNotExists(dt, "AttendancePercentage", GetType(Double))

                    ' Calculate attendance statistics for each student
                    Dim studentIDs = dt.AsEnumerable().Select(Function(row) row.Field(Of Integer)("studentID")).Distinct()

                    

                    ' Bind DataTable to GridView
                    grdPrograms.DataSource = dt
                    grdPrograms.DataBind()
                End Using
            End Using
        End Using
    End Sub

    ' Utility to add a column to the DataTable if it doesn't already exist
    Private Sub AddColumnIfNotExists(ByRef dt As DataTable, ByVal columnName As String, ByVal dataType As Type)
        If Not dt.Columns.Contains(columnName) Then
            dt.Columns.Add(columnName, dataType)
        End If
    End Sub
    Protected Sub Download_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ' Set the content type and the filename
            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("content-disposition", "attachment;filename=GridViewData.xls")
            Response.Charset = ""
            Response.ContentType = "application/vnd.ms-excel"

            ' Recreate the pivoted data
            Dim pivotedData As New DataTable()
            BindPivotedData(pivotedData) ' Function to populate the pivotedData as shown earlier

            ' Use StringWriter to write the DataTable as an HTML table
            Using sw As New StringWriter()
                Using hw As New HtmlTextWriter(sw)
                    ' Create the table headers
                    hw.Write("<table border='1'>")
                    hw.Write("<tr>")
                    For Each column As DataColumn In pivotedData.Columns
                        hw.Write("<th style='background-color:#5082f5; color:#FFFFFF;'>")
                        hw.Write(column.ColumnName)
                        hw.Write("</th>")
                    Next
                    hw.Write("</tr>")

                    ' Populate rows
                    For Each row As DataRow In pivotedData.Rows
                        hw.Write("<tr>")
                        For Each column As DataColumn In pivotedData.Columns
                            hw.Write("<td>")
                            hw.Write(row(column.ColumnName).ToString())
                            hw.Write("</td>")
                        Next
                        hw.Write("</tr>")
                    Next
                    hw.Write("</table>")

                    ' Write the output to the response
                    Response.Output.Write(sw.ToString())
                    Response.Flush()
                    Response.End()
                End Using
            End Using
        Catch ex As Exception
            ' Handle any exceptions
            Response.Write("Error: " & ex.Message)
        End Try
    End Sub




    Private Sub BindPivotedData(ByRef pivotedData As DataTable)
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim fromDate As DateTime = DateTime.ParseExact(TxtFromDate.Text, "yyyy-MM-dd", Nothing)
            Dim toDate As DateTime = DateTime.ParseExact(TxtTodate.Text, "yyyy-MM-dd", Nothing)

            ' SQL query to fetch attendance data
            Dim sql As String = "SELECT s.student, s.studentID, sa.Dated, sa.status " & _
                                "FROM StudentAtt sa " & _
                                "JOIN student s ON sa.studentid = s.studentid " & _
                                "WHERE sa.Dated BETWEEN @fromDate AND @toDate " & _
                                "AND sa.teacherid = @Teacherid " & _
                                "AND sa.CourseID = @CourseID " & _
                                "ORDER BY sa.Dated ASC;"

            cmd.CommandText = sql
            cmd.Connection = con

            ' Add parameters
            cmd.Parameters.AddWithValue("@fromDate", fromDate)
            cmd.Parameters.AddWithValue("@toDate", toDate)
            cmd.Parameters.AddWithValue("@Teacherid", ViewState("userid"))
            cmd.Parameters.AddWithValue("@CourseID", Convert.ToInt32(ViewState("courseid")))

            Using sda As New SqlDataAdapter(cmd)
                Using dt As New DataTable()
                    sda.Fill(dt)

                    ' Get distinct attendance dates
                    Dim attendanceDates = dt.AsEnumerable() _
                        .Where(Function(row) Not String.IsNullOrEmpty(row.Field(Of String)("status"))) _
                        .Select(Function(row) row.Field(Of DateTime)("Dated")) _
                        .Distinct().ToList()

                    ' Add fixed columns to pivotedData
                    pivotedData.Columns.Add("studentID", GetType(Integer))
                    pivotedData.Columns.Add("student", GetType(String))

                    ' Add columns for attendance dates
                    For Each currentDate As DateTime In attendanceDates
                        pivotedData.Columns.Add(currentDate.ToString("yyyy-MM-dd"), GetType(String))
                    Next

                    ' Add attendance statistics columns
                    pivotedData.Columns.Add("TotalAttendance", GetType(Integer))
                    pivotedData.Columns.Add("TotalAbsent", GetType(Integer))
                    pivotedData.Columns.Add("AttendancePercentage", GetType(Double))

                    ' Populate the pivoted data
                    Dim students = dt.AsEnumerable() _
                        .Select(Function(row) New With {
                            Key .StudentID = row.Field(Of Integer)("studentID"),
                            Key .Student = row.Field(Of String)("student")
                        }).Distinct()

                    For Each studentRecord In students
                        Dim newRow As DataRow = pivotedData.NewRow()
                        newRow("studentID") = studentRecord.StudentID
                        newRow("student") = studentRecord.Student

                        Dim totalAttendance As Integer = 0
                        Dim totalAbsent As Integer = 0

                        ' Fill attendance data for each date
                        For Each currentDate As DateTime In attendanceDates
                            Dim statusRow = dt.AsEnumerable() _
                                .FirstOrDefault(Function(row) row.Field(Of Integer)("studentID") = studentRecord.StudentID AndAlso row.Field(Of DateTime)("Dated") = currentDate)

                            If statusRow IsNot Nothing Then
                                Dim status As String = statusRow("status").ToString().ToLower()

                                ' Increment counts based on the status
                                If status = "present" OrElse status = "p" Then
                                    totalAttendance += 1
                                    newRow(currentDate.ToString("yyyy-MM-dd")) = "P"
                                ElseIf status = "absent" OrElse status = "a" Then
                                    totalAbsent += 1
                                    newRow(currentDate.ToString("yyyy-MM-dd")) = "A"
                                Else
                                    newRow(currentDate.ToString("yyyy-MM-dd")) = "N/A" ' Optional for unknown status
                                End If
                            Else
                                newRow(currentDate.ToString("yyyy-MM-dd")) = "N/A" ' Default for no record
                            End If
                        Next

                        ' Calculate statistics
                        Dim totalDays = totalAttendance + totalAbsent
                        newRow("TotalAttendance") = totalAttendance
                        newRow("TotalAbsent") = totalAbsent
                        newRow("AttendancePercentage") = If(totalDays > 0, Math.Round((totalAttendance / totalDays) * 100, 2), 0)

                        pivotedData.Rows.Add(newRow)
                    Next
                End Using
            End Using
        End Using
    End Sub



    


    Private Function GetData() As DataTable

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("GET_ATTENDANCEREPORT")
                cmd.Parameters.AddWithValue("@STARTDATE", TxtFromDate.text)
                cmd.Parameters.AddWithValue("@ENDDATE", TxtTodate.text)
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
    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        Response.Redirect("../Attendance/FacultyAttendance.aspx?s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u"))
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub


    Protected Sub TxtFromDate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtFromDate.Load
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim courseid As Integer = Convert.ToInt32(ViewState("courseid"))
            Dim sql As String = "SELECT s.student, s.StudentID, sa.status " & _
                                "FROM StudentAtt sa " & _
                                "JOIN student s ON sa.studentid = s.studentid " & _
                                "WHERE sa.Dated = @Date " & _
                                "AND sa.CourseID = @CourseID;"

            cmd.CommandText = sql
            cmd.Connection = con
            cmd.Parameters.AddWithValue("@Date", TxtFromDate.Text)
            cmd.Parameters.AddWithValue("@CourseID", courseid)

            Using sda As New SqlDataAdapter(cmd)
                Using dt As New DataTable()
                    sda.Fill(dt)

                    ' Add the required columns to the DataTable
                    If Not dt.Columns.Contains("TotalAttendance") Then
                        dt.Columns.Add("TotalAttendance", GetType(Integer))
                    End If

                    If Not dt.Columns.Contains("TotalAbsent") Then
                        dt.Columns.Add("TotalAbsent", GetType(Integer))
                    End If

                    If Not dt.Columns.Contains("AttendancePercentage") Then
                        dt.Columns.Add("AttendancePercentage", GetType(Double))
                    End If

                    ' Calculate values for these columns
                    For Each row As DataRow In dt.Rows
                        Dim studentID As String = row("StudentID").ToString()

                        ' Count attendance and absence
                        Dim presentCountSql As String = "SELECT COUNT(*) " & _
                                                        "FROM StudentAtt " & _
                                                        "WHERE StudentID = @StudentID " & _
                                                        "AND CourseID = @CourseID " & _
                                                        "AND Status = 'P';"
                        Dim absentCountSql As String = "SELECT COUNT(*) " & _
                                                       "FROM StudentAtt " & _
                                                       "WHERE StudentID = @StudentID " & _
                                                       "AND CourseID = @CourseID " & _
                                                       "AND Status = 'A';"

                        Dim presentCount As Integer = 0
                        Dim absentCount As Integer = 0

                        ' Calculate present count
                        Using presentCmd As New SqlCommand(presentCountSql, con)
                            presentCmd.Parameters.AddWithValue("@StudentID", studentID)
                            presentCmd.Parameters.AddWithValue("@CourseID", courseid)
                            If con.State = ConnectionState.Closed Then
                                con.Open()
                            End If
                            presentCount = Convert.ToInt32(presentCmd.ExecuteScalar())
                        End Using

                        ' Calculate absent count
                        Using absentCmd As New SqlCommand(absentCountSql, con)
                            absentCmd.Parameters.AddWithValue("@StudentID", studentID)
                            absentCmd.Parameters.AddWithValue("@CourseID", courseid)
                            If con.State = ConnectionState.Closed Then
                                con.Open()
                            End If
                            absentCount = Convert.ToInt32(absentCmd.ExecuteScalar())
                        End Using

                        ' Calculate attendance percentage
                        Dim totalDays As Integer = presentCount + absentCount
                        Dim attendancePercentage As Double = If(totalDays > 0, Math.Round((presentCount / totalDays) * 100, 2), 0)

                        ' Assign calculated values to the DataTable row
                        row("TotalAttendance") = presentCount
                        row("TotalAbsent") = absentCount
                        row("AttendancePercentage") = attendancePercentage
                    Next

                    ' Bind the DataTable to the GridView
                    grdPrograms.DataSource = dt
                    grdPrograms.DataBind()
                End Using
            End Using
        End Using
    End Sub



    Protected Sub TxtTodate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtTodate.Load
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim fromDate As DateTime = DateTime.ParseExact(TxtFromDate.Text, "yyyy-MM-dd", Nothing)
            Dim toDate As DateTime = DateTime.ParseExact(TxtTodate.Text, "yyyy-MM-dd", Nothing)

            ' Clear existing columns in the GridView
            grdPrograms.Columns.Clear()

            ' Add serial number column
            Dim serialNoField As New TemplateField()
            serialNoField.HeaderText = "S.No."
            serialNoField.ItemTemplate = New DynamicSerialNumberTemplate()
            grdPrograms.Columns.Add(serialNoField)

            ' Add student ID column
            Dim studentIDField As New BoundField()
            'studentIDField.DataField = "studentID"
            'studentIDField.HeaderText = "StudentID"
            'grdPrograms.Columns.Add(studentIDField)

            ' Add student name column
            Dim studentField As New BoundField()
            studentField.DataField = "student"
            studentField.HeaderText = "Student"
            grdPrograms.Columns.Add(studentField)

            ' SQL query to fetch attendance data
            Dim sql As String = "SELECT s.student, s.studentID, sa.Dated, sa.status " & _
                                "FROM StudentAtt sa " & _
                                "JOIN student s ON sa.studentid = s.studentid " & _
                                "WHERE sa.Dated BETWEEN @fromDate AND @toDate " & _
                                "AND sa.teacherid = @Teacherid " & _
                                "AND sa.CourseID = @CourseID " & _
                                "ORDER BY sa.Dated ASC;"

            cmd.CommandText = sql
            cmd.Connection = con

            ' Add parameters
            cmd.Parameters.AddWithValue("@fromDate", fromDate)
            cmd.Parameters.AddWithValue("@toDate", toDate)
            cmd.Parameters.AddWithValue("@Teacherid", ViewState("userid"))
            cmd.Parameters.AddWithValue("@CourseID", Convert.ToInt32(ViewState("courseid")))

            Using sda As New SqlDataAdapter(cmd)
                Using dt As New DataTable()
                    sda.Fill(dt)

                    ' Get distinct dates with attendance status
                    Dim attendanceDates = dt.AsEnumerable() _
                        .Where(Function(row) Not String.IsNullOrEmpty(row.Field(Of String)("status"))) _
                        .Select(Function(row) row.Field(Of DateTime)("Dated")) _
                        .Distinct().ToList()

                    ' Create columns for each date in the GridView
                    For Each currentDate As DateTime In attendanceDates
                        Dim dateField As New TemplateField()
                        dateField.HeaderText = currentDate.ToString("yyyy-MM-dd")
                        dateField.ItemTemplate = New DynamicItemTemplate(currentDate.ToString("yyyy-MM-dd"))
                        grdPrograms.Columns.Add(dateField)
                    Next

                    ' Create a new DataTable for pivoted data
                    Dim pivotedData As New DataTable()
                    pivotedData.Columns.Add("studentID")
                    pivotedData.Columns.Add("student")

                    ' Add attendance date columns
                    For Each currentDate As DateTime In attendanceDates
                        pivotedData.Columns.Add(currentDate.ToString("yyyy-MM-dd"))
                    Next

                    ' Add columns for attendance statistics
                    pivotedData.Columns.Add("TotalAttendance", GetType(Integer))
                    pivotedData.Columns.Add("TotalAbsent", GetType(Integer))
                    pivotedData.Columns.Add("AttendancePercentage", GetType(Double))

                    ' Populate the pivoted data
                    Dim students = dt.AsEnumerable() _
                        .Select(Function(row) New With {
                            Key .StudentID = row.Field(Of Integer)("studentID"),
                            Key .Student = row.Field(Of String)("student")
                        }).Distinct()

                    For Each studentRecord In students
                        Dim newRow As DataRow = pivotedData.NewRow()
                        newRow("studentID") = studentRecord.StudentID
                        newRow("student") = studentRecord.Student

                        Dim totalAttendance As Integer = 0
                        Dim totalAbsent As Integer = 0

                        For Each currentDate As DateTime In attendanceDates
                            Dim statusRow = dt.AsEnumerable() _
                                .FirstOrDefault(Function(row) row.Field(Of Integer)("studentID") = studentRecord.StudentID AndAlso row.Field(Of DateTime)("Dated") = currentDate)

                            If statusRow IsNot Nothing Then
                                Dim status As String = statusRow("status").ToString().ToLower()

                                ' Increment counts based on the status
                                If status = "present" OrElse status = "p" Then
                                    totalAttendance += 1
                                    newRow(currentDate.ToString("yyyy-MM-dd")) = "P"
                                ElseIf status = "absent" OrElse status = "a" Then
                                    totalAbsent += 1
                                    newRow(currentDate.ToString("yyyy-MM-dd")) = "A"
                                Else
                                    newRow(currentDate.ToString("yyyy-MM-dd")) = "N/A" ' Optional for unknown status
                                End If
                            Else
                                newRow(currentDate.ToString("yyyy-MM-dd")) = "N/A" ' Default value for no record
                            End If
                        Next


                        ' Set calculated values for the new columns
                        Dim totalDays = totalAttendance + totalAbsent
                        newRow("TotalAttendance") = totalAttendance
                        newRow("TotalAbsent") = totalAbsent
                        newRow("AttendancePercentage") = If(totalDays > 0, Math.Round((totalAttendance / totalDays) * 100, 2), 0)

                        pivotedData.Rows.Add(newRow)
                    Next

                    ' Add the Present Attendance column to the GridView
                    Dim totalAttendanceField As New TemplateField()
                    totalAttendanceField.HeaderText = "Present Attendance"
                    totalAttendanceField.ItemTemplate = New DynamicTotalAttendanceTemplate()
                    grdPrograms.Columns.Add(totalAttendanceField)

                    ' Add the Total Absent column to the GridView
                    Dim totalAbsentField As New TemplateField()
                    totalAbsentField.HeaderText = "Total Absent"
                    totalAbsentField.ItemTemplate = New DynamicTotalAbsentTemplate()
                    grdPrograms.Columns.Add(totalAbsentField)

                    ' Add the Attendance Percentage column to the GridView
                    Dim attendancePercentageField As New TemplateField()
                    attendancePercentageField.HeaderText = "Attendance Percentage"
                    attendancePercentageField.ItemTemplate = New DynamicAttendancePercentageTemplate()
                    grdPrograms.Columns.Add(attendancePercentageField)

                    ' Bind the pivoted data to the GridView
                    grdPrograms.DataSource = pivotedData
                    grdPrograms.DataBind()
                End Using
            End Using
        End Using
    End Sub





    Protected Sub grdPrograms_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Find the Total Present and Total Absent labels
            Dim lblTotalPresent As Label = CType(e.Row.FindControl("lblTotalPresent"), Label)
            Dim lblTotalAbsent As Label = CType(e.Row.FindControl("lblTotalAbsent"), Label)

            ' Get the DataRowView for the current row
            Dim rowData As DataRowView = CType(e.Row.DataItem, DataRowView)

            ' Set the Total Present and Total Absent values if available
            If lblTotalPresent IsNot Nothing AndAlso rowData.DataView.Table.Columns.Contains("present") Then
                lblTotalPresent.Text = rowData("present").ToString()
            End If

            If lblTotalAbsent IsNot Nothing AndAlso rowData.DataView.Table.Columns.Contains("absent") Then
                lblTotalAbsent.Text = rowData("absent").ToString()
            End If
        End If
    End Sub






End Class
Public Class DynamicTotalAttendanceTemplate
    Implements ITemplate

    Public Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
        ' Add a Label to display Total Attendance
        Dim lbl As New Label()
        AddHandler lbl.DataBinding, AddressOf OnDataBinding
        container.Controls.Add(lbl)
    End Sub

    Private Sub OnDataBinding(ByVal sender As Object, ByVal e As EventArgs)
        Dim lbl As Label = CType(sender, Label)
        Dim container As GridViewRow = CType(lbl.NamingContainer, GridViewRow)
        Dim dataItem As DataRowView = CType(container.DataItem, DataRowView)

        If dataItem IsNot Nothing AndAlso dataItem.Row.Table.Columns.Contains("TotalAttendance") Then
            lbl.Text = dataItem("TotalAttendance").ToString()
        End If
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
Public Class DynamicAttendancePercentageTemplate
    Implements ITemplate

    Public Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
        Dim lbl As New Label()
        AddHandler lbl.DataBinding, AddressOf OnDataBinding
        container.Controls.Add(lbl)
    End Sub

    Private Sub OnDataBinding(ByVal sender As Object, ByVal e As EventArgs)
        Dim lbl As Label = CType(sender, Label)
        Dim container As GridViewRow = CType(lbl.NamingContainer, GridViewRow)
        Dim dataItem As DataRowView = CType(container.DataItem, DataRowView)

        If dataItem IsNot Nothing AndAlso dataItem.Row.Table.Columns.Contains("AttendancePercentage") Then
            lbl.Text = dataItem("AttendancePercentage").ToString() & " %"
        End If
    End Sub
End Class
Public Class DynamicTotalAbsentTemplate
    Implements ITemplate

    Public Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
        Dim lbl As New Label()
        AddHandler lbl.DataBinding, AddressOf OnDataBinding
        container.Controls.Add(lbl)
    End Sub

    Private Sub OnDataBinding(ByVal sender As Object, ByVal e As EventArgs)
        Dim lbl As Label = CType(sender, Label)
        Dim container As GridViewRow = CType(lbl.NamingContainer, GridViewRow)
        Dim dataItem As DataRowView = CType(container.DataItem, DataRowView)

        If dataItem IsNot Nothing AndAlso dataItem.Row.Table.Columns.Contains("TotalAbsent") Then
            lbl.Text = dataItem("TotalAbsent").ToString()
        End If
    End Sub
End Class

