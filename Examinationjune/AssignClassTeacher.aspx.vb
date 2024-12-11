Imports System.Data.SqlClient
Imports System.Data

Partial Class Examinationjune_AssignClassTeacher
    Inherits System.Web.UI.Page

    ' Connection string to the database (update with your connection details)
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("Academicyear") = Request.QueryString("acyr")
            ViewState("SessionId") = Request.QueryString("s")
            Session("Sessionid") = Request.QueryString("s")
            ViewState("Courseid") = Request.QueryString("rid")
            Session("courseid") = Request.QueryString("rid")
            ViewState("Userid") = Request.QueryString("u")
            ViewState("ayid") = Request.QueryString("ay")

            BindClassDropdown()

            Dim selectedClassId As String = Request.QueryString("classid")
            If Not String.IsNullOrEmpty(selectedClassId) Then
                ddlClass.SelectedValue = selectedClassId
            End If

            ' Set Academic Year from ViewState to lblAcademicYear
            If ViewState("Academicyear") IsNot Nothing Then
                'lblAcademicYear.InnerText = ViewState("Academicyear").ToString()
            End If

            ' Check if success flag is set in the query string and rebind grid data
            If Request.QueryString("success") = "1" Then
                BindRepeaterData() ' Rebind data after redirecting back
            End If
        End If
    End Sub



    Protected Sub ddlClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        BindRepeaterData() ' Re-bind data based on the selected class
    End Sub

    ' Method to bind the Repeater with class and section data
    Private Sub BindRepeaterData()
        Dim selectedClassID As String = ddlClass.SelectedValue

        If selectedClassID = "0" Then
            RepeaterSection.DataSource = Nothing
            RepeaterSection.DataBind()
            Return
        End If

        Dim query As String = "SELECT sc.Classid, sc.ClassName, c.ClassesID, c.Code, COUNT(s.StudentID) AS StudentCount " & _
                       "FROM Classes c " & _
                       "LEFT JOIN Student s ON c.ClassesID = s.ClassesID " & _
                       "LEFT JOIN Sch_Class sc ON sc.Classid = s.CourseID " & _
                       "WHERE sc.Classid = @Classid " & _
                       "AND s.ClassesID IN (SELECT ClassesID FROM sectionAssign) " & _
                       "AND s.CourseID IN (SELECT ClassID FROM sectionAssign) " & _
                       "GROUP BY sc.Classid, sc.ClassName, c.ClassesID, c.Code " & _
                       "ORDER BY c.Code"


        Dim dt As New DataTable()

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@Classid", selectedClassID)
                Using sda As New SqlDataAdapter(cmd)
                    sda.Fill(dt)
                    RepeaterSection.DataSource = dt
                    RepeaterSection.DataBind()
                End Using
            End Using
        End Using
    End Sub
    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click

        Response.Redirect("Dashboard.aspx?s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u") & "&ay=" & ViewState("ayid"))
    End Sub
    ' Method to bind data to the class dropdown
    Private Sub BindClassDropdown()
        Using con As New SqlConnection(constr)
            Dim query As String = "SELECT Classid, ClassName FROM Sch_Class ORDER BY SerialNo"
            Dim cmd As New SqlCommand(query, con)
            con.Open()
            ddlClass.DataSource = cmd.ExecuteReader()
            ddlClass.DataTextField = "ClassName"
            ddlClass.DataValueField = "Classid"
            ddlClass.DataBind()
        End Using

        ' Insert "Select Class" option first
        ddlClass.Items.Insert(0, New ListItem("Select Class", "0"))

        ' Check if ViewState("Courseid") is not null and exists in the dropdown
        If ViewState("Courseid") IsNot Nothing AndAlso ddlClass.Items.FindByValue(ViewState("Courseid").ToString()) IsNot Nothing Then
            ddlClass.SelectedValue = ViewState("Courseid").ToString()
        End If
    End Sub


    ' Event handler for the Save button click
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Loop through Repeater items to get selected values
        For Each item As RepeaterItem In RepeaterSection.Items
            Dim hfClassId As HiddenField = DirectCast(item.FindControl("hfClassId"), HiddenField)
            Dim hfClassesID As HiddenField = DirectCast(item.FindControl("hfClassesID"), HiddenField)
            Dim ddlFaculity As DropDownList = DirectCast(item.FindControl("ddlFaculity"), DropDownList)

            ' Check if faculty is selected
            If ddlFaculity.SelectedValue <> "0" Then
                ' Check if the record already exists for the combination of Classid and ClassesID
                Dim recordExists As Boolean = False
                Dim employeeIdExists As Boolean = False
                Dim queryCheck As String = "SELECT COUNT(1) FROM classTeacherAssign WHERE Classid = @Classid AND ClassesID = @ClassesID"
                Dim queryCheckEmployee As String = "SELECT COUNT(1) FROM classTeacherAssign WHERE Classid = @Classid AND ClassesID = @ClassesID AND EmployeeID = @EmployeeID"

                Using con As New SqlConnection(constr)
                    ' Check if the class and section combination exists
                    Using cmdCheck As New SqlCommand(queryCheck, con)
                        cmdCheck.Parameters.AddWithValue("@Classid", hfClassId.Value)
                        cmdCheck.Parameters.AddWithValue("@ClassesID", hfClassesID.Value)

                        con.Open()
                        recordExists = Convert.ToBoolean(cmdCheck.ExecuteScalar())
                        con.Close()
                    End Using

                    ' Check if the same employee is already assigned to this class and section
                    Using cmdCheckEmployee As New SqlCommand(queryCheckEmployee, con)
                        cmdCheckEmployee.Parameters.AddWithValue("@Classid", hfClassId.Value)
                        cmdCheckEmployee.Parameters.AddWithValue("@ClassesID", hfClassesID.Value)
                        cmdCheckEmployee.Parameters.AddWithValue("@EmployeeID", ddlFaculity.SelectedValue)

                        con.Open()
                        employeeIdExists = Convert.ToBoolean(cmdCheckEmployee.ExecuteScalar())
                        con.Close()
                    End Using
                End Using

                ' If EmployeeID already exists, show "Already Exist" popup
                If employeeIdExists Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "showCustomAlert('This faculty is already assigned to the selected class and section.');", True)
                Else
                    ' If record exists, update it, otherwise insert a new record
                    Dim query As String
                    If recordExists Then
                        ' Update the existing record
                        query = "UPDATE classTeacherAssign SET TotalStudent = @TotalStudent, EmployeeID = @EmployeeID WHERE Classid = @Classid AND ClassesID = @ClassesID"
                    Else
                        ' Insert a new record
                        query = "INSERT INTO classTeacherAssign (Classid, ClassesID, TotalStudent, EmployeeID) " & _
                                "VALUES (@Classid, @ClassesID, @TotalStudent, @EmployeeID)"
                    End If

                    ' Execute query to insert or update the record
                    Using con As New SqlConnection(constr)
                        Using cmd As New SqlCommand(query, con)
                            cmd.Parameters.AddWithValue("@Classid", hfClassId.Value)
                            cmd.Parameters.AddWithValue("@ClassesID", hfClassesID.Value)
                            cmd.Parameters.AddWithValue("@TotalStudent", DirectCast(item.FindControl("ltStudentCount"), Literal).Text)
                            cmd.Parameters.AddWithValue("@EmployeeID", ddlFaculity.SelectedValue)

                            con.Open()
                            cmd.ExecuteNonQuery()
                            con.Close()
                        End Using
                    End Using

                    ' Show success popup if data is successfully saved
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "showCustomAlert('Data has been successfully saved.');", True)
                End If
            End If
        Next
    End Sub



    Protected Sub RepeaterSection_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles RepeaterSection.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            ' Find the DropDownList within the current Repeater row
            Dim ddlFaculity As DropDownList = DirectCast(e.Item.FindControl("ddlFaculity"), DropDownList)
            Dim hfClassId As HiddenField = DirectCast(e.Item.FindControl("hfClassId"), HiddenField)
            Dim hfClassesID As HiddenField = DirectCast(e.Item.FindControl("hfClassesID"), HiddenField)

            ' Bind the DropDownList with faculty data
            Using con As New SqlConnection(constr)
                Dim query As String = "SELECT EmployeeID, Employee FROM P_Employee WHERE Employee IS NOT NULL AND TeachingStaff = '1' ORDER BY Employee"
                Using cmd As New SqlCommand(query, con)
                    con.Open()
                    ddlFaculity.DataSource = cmd.ExecuteReader()
                    ddlFaculity.DataTextField = "Employee"
                    ddlFaculity.DataValueField = "EmployeeID"
                    ddlFaculity.DataBind()
                End Using
            End Using

            ' Check if an EmployeeID is already assigned for this Classid and ClassesID
            Dim selectedEmployeeID As String = GetAssignedFaculty(hfClassId.Value, hfClassesID.Value)

            ' Add default item "Select Faculty"
            ddlFaculity.Items.Insert(0, New ListItem("Select Faculty", "0"))

            ' If an EmployeeID is assigned, select it in the dropdown
            If Not String.IsNullOrEmpty(selectedEmployeeID) Then
                Dim listItem As ListItem = ddlFaculity.Items.FindByValue(selectedEmployeeID)
                If listItem IsNot Nothing Then
                    ddlFaculity.ClearSelection()
                    listItem.Selected = True
                End If
            End If
        End If
    End Sub

    ' Method to get the assigned EmployeeID for the specific Classid and ClassesID
    Private Function GetAssignedFaculty(ByVal classId As String, ByVal classesId As String) As String
        Dim assignedEmployeeID As String = String.Empty
        Dim query As String = "SELECT EmployeeID FROM classTeacherAssign WHERE Classid = @Classid AND ClassesID = @ClassesID"

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@Classid", classId)
                cmd.Parameters.AddWithValue("@ClassesID", classesId)
                con.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot Nothing Then
                    assignedEmployeeID = result.ToString()
                End If
            End Using
        End Using

        Return assignedEmployeeID
    End Function


End Class
