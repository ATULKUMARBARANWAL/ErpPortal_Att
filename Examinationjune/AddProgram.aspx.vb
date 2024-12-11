Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Partial Class AddProgram
    Inherits System.Web.UI.Page
    Private cmd As dbnew = New dbnew()
    Private saralMastercls As saral.Mastercls = New saral.Mastercls()
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            ViewState("Sessionid") = Request.QueryString("s")
            ViewState("Userid") = Request.QueryString("u")

            Dim sql As String = ""
            sql = "select InstitueID,code from Institue   order by Code "
            cmd.FillDropdown(ddlfaculty, sql)
            BindGrid()
        End If
    End Sub
    Protected Sub lnkClass_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Cast the sender to a LinkButton
        Dim lnkClass As LinkButton = CType(sender, LinkButton)

        ' Retrieve the CommandArgument (Courseid) from the clicked LinkButton
        Dim courseId As Integer = Convert.ToInt32(lnkClass.CommandArgument)

        ' Optional: Load details related to the clicked class
        LoadClassDetails(courseId)

        ' Show Panel3
        Panel3.Visible = True

        ' Hide other panels (if necessary)
        PanelInputFields.Visible = False
        PanelGrid.Visible = False
    End Sub
    Private Sub LoadClassDetails(ByVal courseId As Integer)
        BindInstitutes() ' Make sure the dropdown is populated first
        Dim query As String = "SELECT c.Course, i.InstitueID FROM Exam_Course c JOIN Institue i ON c.InstitueID = i.InstitueID WHERE c.CourseID = @CourseID"
        Using connection As New SqlConnection(constr)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@CourseId", courseId)

                connection.Open()
                Dim reader As SqlDataReader = command.ExecuteReader()

                If reader.Read() Then
                    TextBox1.Text = reader("Course").ToString()
                    DropDownList1.SelectedValue = reader("InstitueID").ToString() ' Make sure to use the correct column name

                    Dim className = reader("Course").ToString()
                    classI.Text = "Class:" + className
                    TextBox1.Enabled = False
                    DropDownList1.Enabled = False
                Else
                    TextBox1.Text = String.Empty
                    classI.Text = ""
                    DropDownList1.SelectedIndex = -1 ' Clear selection if no data
                    TextBox1.Enabled = False
                    DropDownList1.Enabled = False
                End If
            End Using
        End Using
    End Sub

    Private Sub BindInstitutes()
        Dim sql As String = "SELECT InstitueID, Code FROM Institue ORDER BY Code"
        cmd.FillDropdown(DropDownList1, sql)
    End Sub

    Protected Sub addButtonTwo(ByVal sender As Object, ByVal e As EventArgs)
        PanelInputFields.Visible = False
        Panel3.Visible = False
        classI.Visible = False
        PanelGrid.Visible = True
    End Sub

    Protected Sub AddButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        PanelInputFields.Visible = True
        PanelGrid.Visible = False
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        txtprefix.Enabled = True
        txtcode.Enabled = True

        Using con As New SqlConnection(constr)
            ' Updated query to include Dated column with current date
            Using cmd As New SqlCommand("INSERT INTO Sch_Class (ClassName, ClassCode, Classprefix, Cid, Dated) VALUES (@ClassName, @ClassCode, @Classprefix, @Cid, GETDATE())", con)
                cmd.CommandType = CommandType.Text

                ' Adding parameters for the query
                cmd.Parameters.AddWithValue("@ClassName", txtprogram.Text)
                cmd.Parameters.AddWithValue("@ClassCode", txtcode.Text)
                cmd.Parameters.AddWithValue("@Classprefix", txtprefix.Text)
                cmd.Parameters.AddWithValue("@Cid", ddlfaculty.SelectedValue.ToString)

                con.Open()

                ' Execute the insert command and check for success
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                con.Close()

                ' Check the result of the insert operation and provide appropriate feedback
                If rowsAffected > 0 Then
                    SaralMsg.Messagebx.Alert(Me, "Program added successfully")
                Else
                    SaralMsg.Messagebx.Alert(Me, "Error adding program")
                End If

                ' Clear input fields after successful insertion
                txtprogram.Text = ""
                txtcode.Text = ""
                txtprefix.Text = ""
            End Using
        End Using

        ' Refresh the GridView to show updated data
        BindGrid()
    End Sub

    
    Private Sub BindGrid()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(" select c.Classid,c.ClassName,c.ClassCode,c.Classprefix,i.institue from Sch_Class c join Institue i on i.InstitueID = c.Cid ORDER BY c.Classid ASC")
                Using sda As New SqlDataAdapter(cmd)
                    cmd.Connection = con
                    Dim dt As New DataTable()
                    sda.Fill(dt)
                    grdProgram.DataSource = dt
                    grdProgram.DataBind()
                End Using
            End Using
        End Using
    End Sub




    Protected Sub grdProgram_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdProgram.RowCommand
        If e.CommandName = "Delete" Then
            grdProgram.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdProgram.Rows(rowIndex)
            ViewState("id") = grdProgram.SelectedDataKey(0)
            Dim Count As Integer = 0
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select Count(*) as totalrow from Exam_CourseSession where Courseid='" & ViewState("id") & "' ")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            Count = dt.Rows(0)("totalrow").ToString()

                        End Using
                    End Using
                End Using

                If Count = 0 Then
                    Using cmd As New SqlCommand("DELETE FROM Exam_Course WHERE NOT EXISTS ( SELECT *  FROM Exam_CourseSession WHERE Courseid=Exam_Course.Courseid) and Courseid='" & ViewState("id") & "' ")
                        Using sda As New SqlDataAdapter()
                            cmd.Connection = con
                            sda.SelectCommand = cmd
                            Using dt As New DataTable()
                                sda.Fill(dt)
                                Me.BindGrid()

                            End Using
                        End Using
                    End Using

                Else
                    SaralMsg.Messagebx.Alert(Me, "This program is mapped to the session.You cannot delete this program from  now, First you have to delete it from session as well.")
                End If
            End Using
        End If

        '................Edit coding.............

        If e.CommandName = "Edit" Then



            btnAdd.Visible = False
            btnupdate.Visible = True
            grdProgram.SelectedIndex = e.CommandArgument
            Dim rowindex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdProgram.Rows(rowindex)
            ViewState("id") = grdProgram.SelectedDataKey(0)
            ViewState("academicyear") = row.Cells(4).Text
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("select * from exam_Course where Courseid='" & ViewState("id") & "' ")
                    cmd.Connection = con
                    con.Open()
                    Dim sdr As SqlDataReader = cmd.ExecuteReader
                    While (sdr.Read())
                        Dim sql As String = "select branchid,code  from branch where collegeid=" & sdr("Cid") & ""

                        txtprogram.Text = sdr("ClassName").ToString()
                        txtcode.Text = sdr("ClassCode").ToString()
                        'txtprefix.Text = sdr("Courseprefix").ToString()
                        'ddlfaculty.SelectedValue = sdr("Cid")
                        'ddldepartment.SelectedValue = sdr("Departmentid")
                    End While
                End Using
            End Using
        End If




    End Sub

    Private Sub BindDropDownList1(ByVal ddl2 As DropDownList, ByVal query As String, ByVal text As String, ByVal value As String, ByVal defaultText As String)

        Dim cmd As New SqlCommand(query)
        Using con As New SqlConnection(constr)
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

    End Sub

    Protected Sub btnupdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnupdate.Click
        Using con As New SqlConnection(constr)
            Using Command As New SqlCommand("update Exam_Course set Coursecode=@Coursecode,Course=@Course,Courseprefix=@Courseprefix Where Courseid='" & ViewState("id") & "'", con)

                Command.Parameters.AddWithValue("@Coursecode ", txtcode.Text)
                Command.Parameters.AddWithValue("@Courseprefix", txtprefix.Text)
                Command.Parameters.AddWithValue("@Course", txtprogram.Text)

                con.Open()
                If txtcode.Text = "" Or txtprogram.Text = "" Then
                    Dim msg As String = "Please fill all the fields."
                    Dim script1 As String = "window.onload= function(){alert('"
                    script1 &= msg
                    script1 &= "');"
                    script1 &= ";}"
                    ClientScript.RegisterStartupScript(Me.GetType(), "Fill all Fields", script1, True)



                    btnupdate.Visible = True
                    btnAdd.Visible = False
                    BindGrid()
                    Exit Sub
                Else

                    Command.ExecuteNonQuery()
                    con.Close()

                    Dim msg1 As String = "Successfully Updated"
                    Dim Script2 As String = "window.onload=function(){alert('"
                    Script2 &= msg1
                    Script2 &= "');"
                    Script2 &= ";}"
                    ClientScript.RegisterStartupScript(Me.GetType(), "Successfull Update", Script2, True)


                    txtcode.Text = ""
                    txtprogram.Text = ""
                    ddlfaculty.SelectedIndex = -1

                    btnAdd.Visible = True
                    btnupdate.Visible = False
                    BindGrid()
                End If

            End Using





        End Using

    End Sub

    Protected Sub OnRowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)

        Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("DELETE  Exam_Subject WHERE Subjectid = ''")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    con.Open()
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Me.BindGrid()
                        con.Close()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Protected Sub OnRowEditing(ByVal sender As Object, ByVal f As GridViewEditEventArgs)


    End Sub

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

    End Sub



    

    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        Response.Redirect("Programtosession.aspx?s=" & ViewState("Sessionid") & "&u=" & ViewState("Userid"))
    End Sub
End Class
