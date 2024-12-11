Imports System.Data.SqlClient
Imports System.Data
Imports System.Diagnostics


Partial Class Attendance_leaveApproval
    Inherits System.Web.UI.Page
    ' Define connection string (adjust details as necessary)
    Dim connectionString As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("Sessionid") = Request.QueryString("s")
            ViewState("Courseid") = Request.QueryString("Courseid")
            ViewState("classesid") = Request.QueryString("classesid")
            ViewState("Courseid") = Request.QueryString("rid")
            Session("courseid") = Request.QueryString("rid")
            ViewState("Userid") = Request.QueryString("u")
            LeaveAppliedStudent()
            FillClassDropdown()
        End If
    End Sub
    Protected Sub LeaveAppliedStudent(Optional ByVal status As String = "Pending")
        Dim query As String = "SELECT sch.ClassName, c.code, s.student, l.Remark, l.studentID, l.applyDate, l.fromDate, l.toDate, l.leaveType, l.status, l.fileName, l.reason " &
                              "FROM LeaveApplied l " &
                              "JOIN student s ON s.classesid = l.classesid AND s.studentID = l.studentID AND s.AdmissionNo = l.admissionNo " &
                              "JOIN classes c ON c.ClassesID = l.classesID " &
                              "JOIN Sch_Class sch ON l.courseID = sch.Classid " &
                              "WHERE l.courseId = @CourseID AND l.status = @Status;"

        Dim dt As New DataTable()

        ' Using block for connection
        Using con As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, con)
                ' Parameterized query to avoid SQL injection
                cmd.Parameters.AddWithValue("@CourseID", ViewState("Courseid"))
                cmd.Parameters.AddWithValue("@Status", status)

                ' Data adapter to fill the DataTable
                Using sda As New SqlDataAdapter(cmd)
                    sda.Fill(dt)
                End Using
            End Using
        End Using

        ' If DataTable is empty, add an empty row
        If dt.Rows.Count = 0 Then
            dt.Rows.Add(dt.NewRow())
            GridView1.DataSource = dt
            GridView1.DataBind()

            ' Merge cells to show a message across the grid
            GridView1.Rows(0).Cells.Clear()
            GridView1.Rows(0).Cells.Add(New TableCell() With {
                .ColumnSpan = GridView1.Columns.Count,
                .Text = "",
                .HorizontalAlign = HorizontalAlign.Center
            })
        Else
            ' Bind the DataTable to GridView
            GridView1.DataSource = dt
            GridView1.DataBind()
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        ' Check if the row is a data row (not header or footer)
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Get the value of the "status" column
            Dim status As String = DataBinder.Eval(e.Row.DataItem, "status").ToString()

            ' Find the checkbox column and hide it if the status is "Approve" or "Reject"
            If status = "Approve" OrElse status = "Reject" Then
                ' Hide the "Select" column
                e.Row.Cells(1).Visible = False ' Assuming the "Select" column is the second column (index 1)
            End If

            ' Handle the Download column - dynamically create the link
            Dim fileName As String = DataBinder.Eval(e.Row.DataItem, "fileName").ToString()
            Dim downloadCell As TableCell = e.Row.Cells(10) ' Assuming Download is the 10th column (index 9)

            If Not String.IsNullOrEmpty(fileName) Then
                ' Construct the download link
                Dim downloadLink As String = String.Format("<a href='{0}' target='_blank' style='text-decoration: none;'>" & _
                                                           "<i class='fa-solid fa-download' style='color:rgb(75, 171, 115);font-size:larger;'></i></a>", _
                                                           ResolveUrl("~/UserPortal/Uploads/") + fileName)
                ' Set the link as the cell's inner HTML
                downloadCell.Text = downloadLink
            Else
                ' If no fileName, show a message
                downloadCell.Text = "No file available"
            End If
        ElseIf e.Row.RowType = DataControlRowType.Header Then
            ' Check if all rows have status as "Approve" or "Reject" and hide the Select column
            Dim dt As DataTable = CType(GridView1.DataSource, DataTable)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim allApprovedOrRejected = dt.AsEnumerable().All(Function(row) row.Field(Of String)("status") = "Approve" OrElse row.Field(Of String)("status") = "Reject")
                If allApprovedOrRejected Then
                    e.Row.Cells(1).Visible = False
                End If
            End If
        End If
    End Sub

  

    Protected Sub ddlStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        ' Get the reference to the DropDownList control
        Dim ddl As DropDownList = CType(sender, DropDownList)
        Dim remark As String = txtRemark.Text.Trim()
        ' Get the row index of the dropdown that triggered the event
        Dim row As GridViewRow = CType(ddl.NamingContainer, GridViewRow)

        ' Ensure the row is valid and index is within bounds
        If row IsNot Nothing AndAlso row.RowIndex >= 0 AndAlso row.RowIndex < GridView1.Rows.Count Then
            ' Get the Student ID or any other unique identifier for the row
            Dim studentID As Integer = CType(GridView1.DataKeys(row.RowIndex).Value, Integer) ' Adjust based on the primary key

            ' Get the new status from the DropDownList
            Dim newStatus As String = ddl.SelectedValue

            ' Retrieve the applyDate from the GridView row (clean the text from any HTML tags)
            Dim applyDateText As String = row.Cells(3).Text ' Assuming Apply Date is in the 4th column (index 3)
            applyDateText = applyDateText.Replace("&nbsp;", "").Trim() ' Remove non-breaking space & trim extra whitespace

            ' Parse the date string
            Dim applyDate As DateTime
            If DateTime.TryParseExact(applyDateText, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, applyDate) Then
                ' Call a method to update the status in the database
                UpdateLeaveStatus(studentID, newStatus, applyDate, remark)
            Else
                ' Log or handle invalid date format
                Trace.Warn("Invalid Apply Date format: " & applyDateText)
            End If
        Else
            ' Handle the case where the row is not valid or index is out of range
            Trace.Warn("Row index is out of range.")
        End If
    End Sub





    Private Sub UpdateLeaveStatus(ByVal studentID As Integer, ByVal newStatus As String, ByVal applyDate As DateTime, ByVal remark As String)
        ' Define your UPDATE query with applyDate in the WHERE clause
        Dim query As String = "UPDATE LeaveApplied SET status = @Status,Remark=@Remark WHERE studentID = @StudentID AND courseId = @CourseID AND applyDate = @ApplyDate "

        ' Execute the query
        Using con As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, con)
                ' Add parameters to avoid SQL injection
                cmd.Parameters.AddWithValue("@Status", newStatus)
                cmd.Parameters.AddWithValue("@StudentID", studentID)
                cmd.Parameters.AddWithValue("@CourseID", ViewState("Courseid"))
                cmd.Parameters.AddWithValue("@ApplyDate", applyDate)
                cmd.Parameters.AddWithValue("@Remark", remark)
                ' Open the connection and execute the query
                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
        ' Do not call LeaveAppliedStudent() here
    End Sub


    ' This method will be triggered when the header checkbox is clicked
    Protected Sub chkSelectAll_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        ' Get the header checkbox control
        Dim chkHeader As CheckBox = CType(sender, CheckBox)

        ' Iterate over each row in the GridView
        For Each row As GridViewRow In GridView1.Rows
            ' Check if the row type is DataRow
            If row.RowType = DataControlRowType.DataRow Then
                ' Find the checkbox control inside the row
                Dim chkRow As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)

                ' Set the checkbox checked state based on the header checkbox
                If chkRow IsNot Nothing Then
                    chkRow.Checked = chkHeader.Checked
                End If
            End If
        Next
    End Sub

    Protected Sub btnApprove_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Get the selected status and remark
        Dim selectedStatus As String = ddlStatus.SelectedValue
        Dim remark As String = txtRemark.Text.Trim()
        Dim rowsProcessed As Integer = 0 ' Counter for successful updates
        Dim errorsOccurred As Boolean = False ' Flag to track errors

        ' Loop through each row in the GridView
        For Each row As GridViewRow In GridView1.Rows
            ' Ensure the row is of type DataRow
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkSelect As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)

                ' Check if the checkbox is selected
                If chkSelect IsNot Nothing AndAlso chkSelect.Checked Then
                    Try
                        ' Validate DataKeys and row index
                        If row.RowIndex >= 0 AndAlso row.RowIndex < GridView1.DataKeys.Count Then
                            ' Retrieve StudentID from DataKeys
                            Dim studentID As Integer = CType(GridView1.DataKeys(row.RowIndex).Value, Integer)

                            ' Retrieve Apply Date and clean the text
                            Dim applyDateText As String = row.Cells(5).Text.Replace("&nbsp;", "").Trim()
                            Dim applyDate As DateTime

                            ' Parse Apply Date and update the status
                            If DateTime.TryParseExact(applyDateText, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, applyDate) Then
                                ' Update leave status
                                UpdateLeaveStatus(studentID, selectedStatus, applyDate, remark)
                                rowsProcessed += 1
                            Else
                                ' Log and handle invalid date format
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "showCustomAlert('Invalid Apply Date format for a row.');", True)
                                Trace.Warn("Invalid Apply Date format for row index: " & row.RowIndex)
                                errorsOccurred = True
                            End If
                        Else
                            ' Log and handle invalid DataKeys index
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "showCustomAlert('Invalid DataKeys index for a row.');", True)
                            Trace.Warn("Invalid DataKeys index for row index: " & row.RowIndex)
                            errorsOccurred = True
                        End If
                    Catch ex As Exception
                        ' Log and handle unexpected errors
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "showCustomAlert('An error occurred while processing a row.');", True)
                        Trace.Warn("Error processing row index: " & row.RowIndex & " - " & ex.Message)
                        errorsOccurred = True
                    End Try
                End If
            End If
        Next

        ' Show appropriate messages after processing
        If rowsProcessed > 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SuccessAlert", "showCustomAlert('" & rowsProcessed.ToString() & " leave requests updated successfully.');", True)

        End If

        If errorsOccurred And rowsProcessed = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "FailureAlert", "showCustomAlert('No rows were successfully updated due to errors.');", True)
        End If

        ' Reload the data in the GridView
        LeaveAppliedStudent()
    End Sub






    Private Sub FillClassDropdown()
        Dim query As String = "SELECT Classid, ClassName FROM sch_class"
        Dim dt As New DataTable()

        ' Using block for connection
        Using con As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, con)
                Using sda As New SqlDataAdapter(cmd)
                    sda.Fill(dt)
                End Using
            End Using
        End Using

        ' Bind data to the dropdown
        ddlClass.DataSource = dt
        ddlClass.DataTextField = "ClassName" ' Display ClassName in dropdown
        ddlClass.DataValueField = "Classid"  ' Store Classid as the value
        ddlClass.DataBind()

        ' Get the active courseId from ViewState
        Dim activeCourseId As String = ViewState("Courseid").ToString()

        ' Find the corresponding class and move it to the top
        For Each row As DataRow In dt.Rows
            If row("Classid").ToString() = activeCourseId Then
                ' Add the active class at the top
                ddlClass.Items.Insert(0, New ListItem(row("ClassName").ToString(), row("Classid").ToString()))
                ' Set the selected index to the active courseId
                ddlClass.SelectedValue = activeCourseId
                Exit For
            End If
        Next

    End Sub

    Protected Sub ddlClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        ' Update the ViewState with the selected Classid
        ViewState("Courseid") = ddlClass.SelectedValue
        HandleDropDownSelection()
    
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        HandleDropDownSelection()
    End Sub

    Private Sub HandleDropDownSelection()
        ' Core logic for handling the dropdown selection
        Dim selectedStatus As String = DropDownList1.SelectedValue
        LeaveAppliedStudent(selectedStatus)
    End Sub


End Class
