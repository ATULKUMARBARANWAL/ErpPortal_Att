Imports System.Data.SqlClient
Imports System.Data

Public Class Examinationjune_AddSection
    Inherits System.Web.UI.Page

    ' Connection string (replace with your actual connection string)
    Dim connectionString As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Set values from query string into ViewState and Session
            ViewState("Academicyear") = Request.QueryString("acyr")
            ViewState("SessionId") = Request.QueryString("s")
            Session("Sessionid") = Request.QueryString("s")
            ViewState("Courseid") = Request.QueryString("rid")
            Session("courseid") = Request.QueryString("rid")
            ViewState("Userid") = Request.QueryString("u")
            ViewState("ayid") = Request.QueryString("ay")
            fetchClass()
            BindGridView()

            ' Get the class ID from the query string
            Dim selectedClassId As String = Request.QueryString("classid")

            ' Check if class ID is not empty or null, and set the selected value of ddlProgram
            If Not String.IsNullOrEmpty(selectedClassId) Then
                ddlProgram.SelectedValue = selectedClassId
            End If
            FetchSections()
        End If
    End Sub
    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click

        Response.Redirect("Dashboard.aspx?s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u") & "&ay=" & ViewState("ayid"))
    End Sub
    Protected Sub gvClassDetails_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName = "Remove" Then
            ' Get the index of the row containing the command
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)

            ' Retrieve Classid and ClassesID from DataKeys
            Dim classId As Integer = Convert.ToInt32(gvClassDetails.DataKeys(rowIndex)("Classid"))
            Dim classesId As Integer = Convert.ToInt32(gvClassDetails.DataKeys(rowIndex)("ClassesID"))

            ' Call the delete method with both Classid and ClassesID
            DeleteSectionAssignRecord(classId, classesId)

            ' Re-bind the GridView to reflect the changes after deletion
            BindGridView()

            ' Display a custom alert to inform the user about the deletion
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "showCustomAlert('Record deleted successfully.');", True)
        End If
    End Sub




    Private Sub DeleteSectionAssignRecord(ByVal classId As Integer, ByVal classesId As Integer)
        ' SQL query to delete the record with the specified Classid and ClassesID
        Dim query As String = "DELETE FROM sectionAssign WHERE Classid = @Classid AND ClassesID = @ClassesID"

        ' Execute the delete command
        Using con As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@Classid", classId)
                cmd.Parameters.AddWithValue("@ClassesID", classesId)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using
        FetchSections()

    End Sub


    Private Sub FetchSections()
        ' Get the course ID from ViewState
        Dim courseId As Integer = ddlProgram.SelectedValue

        Using conn As New SqlConnection(connectionString)
            ' Updated query to select ClassesID and Code
            Dim query As String = "SELECT * FROM Classes WHERE ClassesID NOT IN (SELECT ClassesID FROM sectionAssign WHERE Classid = @Classid);"
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Classid", courseId)
                Dim adapter As New SqlDataAdapter(cmd)
                Dim dt As New DataTable()
                adapter.Fill(dt)

                ' Bind the fetched data to the GridView
                gvSections.DataSource = dt
                gvSections.DataBind()
            End Using
        End Using
    End Sub



    Private Sub fetchClass()
        Using con As New SqlConnection(connectionString)
            Using cmd As New SqlCommand("SELECT ClassName, Classid FROM Sch_Class", con)
                Using sda As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    sda.Fill(dt)

                    ' Bind the data to the dropdown list
                    ddlProgram.DataSource = dt
                    ddlProgram.DataTextField = "ClassName"
                    ddlProgram.DataValueField = "Classid"
                    ddlProgram.DataBind()

                    ' Retrieve CourseID from ViewState
                    Dim courseId As String = ViewState("Courseid")

                    ' Set the matching ClassName as the selected item at the 0th index if found
                    If Not String.IsNullOrEmpty(courseId) Then
                        Dim itemToSelect As ListItem = ddlProgram.Items.FindByValue(courseId)
                        If itemToSelect IsNot Nothing Then
                            ' Move the matching item to the top of the list
                            ddlProgram.Items.Remove(itemToSelect)
                            ddlProgram.Items.Insert(0, itemToSelect)
                            ddlProgram.SelectedIndex = 0
                        End If
                    End If
                End Using
            End Using
        End Using
    End Sub

    Protected Sub gvClassDetails_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Optional logic can be added here, such as custom styling for each row.
        End If
    End Sub

    Protected Sub ddlProgram_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Using con As New SqlConnection(connectionString)
            Using cmd As New SqlCommand("SELECT ClassName, Classid FROM Sch_Class WHERE Classid = @Classid", con)
                cmd.Parameters.AddWithValue("@Classid", ddlProgram.SelectedValue)
                Using sda As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    sda.Fill(dt)

                    ' Bind the GridView with the selected class data
                    If dt.Rows.Count > 0 Then

                        FetchSections()
                        BindGridView()
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub BindGridView()
        Using con As New SqlConnection(connectionString)
            Dim classId As String = ddlProgram.SelectedValue
            Using cmd As New SqlCommand("SELECT c.ClassName, c.Classid, sc.code, sc.ClassesID FROM Sch_Class c JOIN sectionAssign sc ON sc.Classid = c.Classid WHERE sc.Classid = @Classid", con)
                cmd.Parameters.AddWithValue("@Classid", classId)

                Using sda As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    sda.Fill(dt)

                    gvClassDetails.DataSource = dt
                    gvClassDetails.DataBind()

                    ' Ensure header is visible even if the data table is empty
                    If dt.Rows.Count = 0 Then
                        dt.Rows.Add(dt.NewRow()) ' Add an empty row to make the header visible
                        gvClassDetails.DataSource = dt
                        gvClassDetails.DataBind()
                        gvClassDetails.Rows(0).Visible = False ' Hide the dummy row
                    End If

                    ' Update AcademicYear label if present in ViewState
                    If ViewState("Academicyear") IsNot Nothing Then
                        For Each row As GridViewRow In gvClassDetails.Rows
                            Dim academicYearLabel As Label = CType(row.FindControl("AcademicYearLabel"), Label)
                            If academicYearLabel IsNot Nothing Then
                                academicYearLabel.Text = ViewState("Academicyear").ToString()
                            End If
                        Next
                    End If
                End Using
            End Using
        End Using
    End Sub







    Protected Sub btnAddSection_Click(ByVal sender As Object, ByVal e As EventArgs)

    End Sub



    ' Method to fetch sections from the database
    

    Private Function IsSectionAssigned(ByVal classId As String, ByVal sectionId As String) As Boolean
        Try
            Using con As New SqlConnection(connectionString)
                con.Open()

                ' Query to check if the section is already assigned to the class
                Dim query As String = "SELECT COUNT(*) FROM sectionAssign WHERE Classid = @Classid AND ClassesID = @ClassesID"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@Classid", classId)
                    cmd.Parameters.AddWithValue("@ClassesID", sectionId)

                    ' Execute the query and return true if the section is already assigned
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    Return count > 0
                End Using
            End Using
        Catch ex As Exception
            ' Handle any errors that occur during the check
            Response.Write("Error: " & ex.Message)
            Return False
        End Try
    End Function


    


    Private Function GetAssignedSections(ByVal classId As String) As List(Of String)
        Dim assignedSections As New List(Of String)()
        Try
            Using con As New SqlConnection(connectionString)
                con.Open()
                Dim query As String = "SELECT ClassesID FROM sectionAssign WHERE Classid = @Classid"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@Classid", classId)

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            assignedSections.Add(reader("ClassesID").ToString())
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try
        Return assignedSections
    End Function
    Private Sub RemoveSectionAssignment(ByVal classId As String, ByVal sectionId As String)
        Try
            Using con As New SqlConnection(connectionString)
                con.Open()
                Dim query As String = "DELETE FROM sectionAssign WHERE Classid = @Classid AND ClassesID = @ClassesID"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@Classid", classId)
                    cmd.Parameters.AddWithValue("@ClassesID", sectionId)

                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim classId As String = ddlProgram.SelectedValue
        Dim sectionSelected As Boolean = False

        ' Loop through GridView rows
        For Each row As GridViewRow In gvSections.Rows
            ' Find the checkbox control in the row
            Dim chkSelect As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)

            If chkSelect IsNot Nothing AndAlso chkSelect.Checked Then
                ' Retrieve sectionId from DataKeys using row index
                Dim sectionId As String = gvSections.DataKeys(row.RowIndex).Value.ToString()

                ' Retrieve sectionCode from the third column (index 2) in the row
                Dim sectionCode As String = row.Cells(2).Text

                ' Save the selected section
                SaveSectionAssignment(classId, sectionId, sectionCode)
                sectionSelected = True
            End If
        Next

        ' Fetch updated sections
        FetchSections()

        ' Display alert if at least one section was selected and saved
        If sectionSelected Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "showCustomAlert('Sections saved successfully.');", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "showCustomAlert('Please select at least one section to save.');", True)
        End If
    End Sub



    Private Function SaveSectionAssignment(ByVal classId As String, ByVal sectionId As String, ByVal sectionCode As String) As Boolean
        Try
            Using con As New SqlConnection(connectionString)
                con.Open()

                ' Check if the record already exists
                Dim checkQuery As String = "SELECT COUNT(*) FROM sectionAssign WHERE Classid = @Classid AND ClassesID = @ClassesID"
                Using checkCmd As New SqlCommand(checkQuery, con)
                    checkCmd.Parameters.AddWithValue("@Classid", classId)
                    checkCmd.Parameters.AddWithValue("@ClassesID", sectionId)

                    Dim exists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

                    ' Only insert if the record does not exist
                    If exists = 0 Then
                        Dim insertQuery As String = "INSERT INTO sectionAssign (Classid, ClassesID, Code) VALUES (@Classid, @ClassesID, @Code)"
                        Using cmd As New SqlCommand(insertQuery, con)
                            cmd.Parameters.AddWithValue("@Classid", classId)
                            cmd.Parameters.AddWithValue("@ClassesID", sectionId)
                            cmd.Parameters.AddWithValue("@Code", sectionCode)

                            ' Execute the insert command
                            cmd.ExecuteNonQuery()
                            BindGridView()
                            Return True ' Insertion successful
                        End Using
                    Else
                        ' Record already exists

                    End If
                End Using
            End Using
        Catch ex As Exception
            ' Handle any errors that occur during the insertion
            Response.Write("Error: " & ex.Message)
        End Try

        Return False ' Insertion not successful or record already exists
    End Function


End Class
