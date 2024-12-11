Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient


Partial Class Templates_ExamStructuretoProgram
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillddlacademicyear()
            fillddlprogram()
        End If
    End Sub
    Private Sub fillddlacademicyear()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(" Select distinct Academicyear from Exam_Session order by Academicyear desc ")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        ddlacademicyear.DataSource = dt
                        ddlacademicyear.DataTextField = "Academicyear"
                        ddlacademicyear.DataValueField = "Academicyear"
                        ddlacademicyear.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub fillddlprogram()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Cs.Academicyear, Cs.Courseid, C.Course , Cs.coursecode from Exam_CourseSession Cs join Exam_Course C on Cs.Courseid =C.Courseid " & _
         " where Cs.Academicyear='" & ddlacademicyear.SelectedItem.Text & "' ")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        ddlprogram.DataSource = dt
                        ddlprogram.DataTextField = "Course"
                        ddlprogram.DataValueField = "Courseid"
                        ddlprogram.DataBind()

                        ddlprogram.Items.Insert(0, New ListItem("Select", ""))

                        
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub fillddlexamstructure()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(" Select Distinct Structurename  from Exam_ExamStructure where AcademicYear ='" & ddlacademicyear.SelectedItem.Text & "' ")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        ddlexamname.DataSource = dt
                        ddlexamname.DataTextField = "Structurename"
                        ddlexamname.DataBind()

                        ddlexamname.Items.Insert(0, New ListItem("Select", ""))
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Protected Sub ddlacademicyear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlacademicyear.SelectedIndexChanged
        fillddlprogram()
        fillddlexamstructure()
    End Sub
    Private Sub BindsetExamMarks()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Es.AcademicYear, Es.Structureid, Es.Structurename, Et.Examname, Et.Examtype, Es.Examtypeid  from Exam_ExamStructure ES " & _
" join Exam_Examtype Et on Es.Examtypeid =Et.Examtypeid where Es.AcademicYear ='" & ddlacademicyear.SelectedItem.Text & "' and Es.Structurename = '" & ddlexamname.SelectedItem.Text & "' ")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        gridprogramexam.DataSource = dt
                        gridprogramexam.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Protected Sub ddlexamname_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlexamname.SelectedIndexChanged
        If ddlexamname.SelectedItem.Text = "Select" Then

        Else
            BindsetExamMarks()
        End If

    End Sub
    Private Sub SaveProgramstructure(ByVal Structureid As String, ByVal IntExt As String, ByVal Marks As String)
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("[Sp_ProgramStructure]")
                Using sda As New SqlDataAdapter()
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@Academicyear", ddlacademicyear.SelectedItem.Text)
                    cmd.Parameters.AddWithValue("@courseid ", ddlprogram.SelectedValue.ToString())
                    cmd.Parameters.AddWithValue("@structureid ", Structureid)
                    cmd.Parameters.AddWithValue("@IntExt", IntExt)
                    cmd.Parameters.AddWithValue("@marks ", Marks)
                    cmd.Parameters.AddWithValue("@userid ", "1197")
                    cmd.Connection = con
                    con.Open()
                    cmd.ExecuteScalar()
                    con.Close()
                End Using
            End Using

        End Using

    End Sub

    Sub SaveExammarksgrid()
        Try

            Dim Structureid As String = ""
            Dim IntExt As String = ""
            Dim Marks As String = ""

            Dim query As String = ""
            For Each Row As GridViewRow In gridprogramexam.Rows
                Structureid = Row.Cells(1).Text
                IntExt = TryCast(Row.FindControl("DdlInterexternal"), DropDownList).SelectedItem.Text
                Marks = TryCast(Row.FindControl("txtexammarks"), TextBox).Text

                If Marks = "" Or Marks = " " Then
                    SaralMsg.Messagebx.Alert(Me, "Please enter marks.")
                Else
                    Using con As New SqlConnection(constr)
                        Using cmd As New SqlCommand(" Select * from Exam_ProgramStructure where Academicyear='" & ddlacademicyear.SelectedItem.Text & "' and Courseid='" & ddlprogram.SelectedValue & "' and IntExt='" & IntExt & "' and Structureid ='" & Structureid & "' and MarkS='" & Marks & "'", con)

                            Dim da As New DataSet
                            Dim ds As New SqlDataAdapter(cmd)
                            ds.Fill(da)
                            Dim i = da.Tables(0).Rows.Count
                            If i > 0 Then
                                SaralMsg.Messagebx.Alert(Me, "Exam structure is already assigned to selected program.")
                            Else
                                Me.SaveProgramstructure(Structureid, IntExt, Marks)
                                SaralMsg.Messagebx.Alert(Me, "Saved successfully.")
                                fillddlprogram()
                                ddlexamname.Items.Clear()
                                gridprogramexam.Visible = False
                            End If
                        End Using
                    End Using

                   
                End If
            Next

        Catch ex As Exception
            SaralMsg.Messagebx.Alert(Me, "Oops! Something went wrong.")


        End Try
    End Sub
    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveExammarksgrid()
       
    End Sub

    Protected Sub ddlprogram_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlprogram.SelectedIndexChanged
        If ddlprogram.SelectedItem.Text = "Select" Then
        Else
            fillddlexamstructure()
        End If
    End Sub
End Class
