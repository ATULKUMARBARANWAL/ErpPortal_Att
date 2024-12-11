Imports System
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Data.DataTable
Partial Class StudentMis_Studentupdate
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("stdntid") = Request.QueryString("rid")
            ViewState("Acdmcyear") = Request.QueryString("acyr")
            fetchprogramdetail()
            getstudentinfo()
            fetchDdlcountry()
            fetchDdlState()
            fetchDdlcity()
            ' fetchDdlprogram()

        End If
    End Sub
'    Private Sub fetchDdlprogram()
'        Using con As New SqlConnection(constr)
'            Using cmd As New SqlCommand("Select Cs.Academicyear, Cs.Courseid, C.Course from Exam_CourseSession Cs join Exam_Course C on Cs.Courseid =C.Courseid " & _
'" where Cs.Academicyear ='2023'")
'                Using sda As New SqlDataAdapter()
'                    cmd.Connection = con
'                    sda.SelectCommand = cmd
'                    Using dt As New DataTable()
'                        sda.Fill(dt)
'                        Ddlprogram.DataSource = dt
'                        Ddlprogram.DataTextField = "Course"
'                        Ddlprogram.DataValueField = "Courseid"
'                        Ddlprogram.DataBind()
'                        'Dim Year As Integer
'                        'Year = Convert.ToInt32(Now.ToString("yyyy"))


'                    End Using
'                End Using
'            End Using
'        End Using

'    End Sub

    Private Sub fetchDdlcountry()
        Try

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select * from CountryMaster ")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            Ddlcountry.DataSource = dt
                            Ddlcountry.DataTextField = "Name"
                            Ddlcountry.DataValueField = "ID"
                            Ddlcountry.DataBind()
                            'Dim Year As Integer
                            'Year = Convert.ToInt32(Now.ToString("yyyy"))


                        End Using
                    End Using
                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Private Sub fetchDdlState()
        Try

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select * from StateMaster  where CountryID ='" & Ddlcountry.SelectedValue & "' ")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            Ddlstate.DataSource = dt
                            Ddlstate.DataTextField = "Name"
                            Ddlstate.DataValueField = "ID"
                            Ddlstate.DataBind()
                            'Dim Year As Integer
                            'Year = Convert.ToInt32(Now.ToString("yyyy"))


                        End Using
                    End Using
                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Private Sub fetchDdlcity()
        Try

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select * from CityMaster where StateID ='" & Ddlstate.SelectedValue & "'")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            ddlcity.DataSource = dt
                            ddlcity.DataTextField = "Name"
                            ddlcity.DataValueField = "ID"
                            ddlcity.DataBind()
                            'Dim Year As Integer
                            'Year = Convert.ToInt32(Now.ToString("yyyy"))


                        End Using
                    End Using
                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Private Sub getstudentinfo()
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim sql As String = "Select CONVERT(VARCHAR, DOB, 101)as 'Datofbirth' , * from student St where St.Academicyear='" & ViewState("Acdmcyear") & "' and St.StudentID ='" & ViewState("stdntid") & "'"
            cmd.CommandText = sql
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                Dim dt As New DataTable
                sda.Fill(dt)
                If dt.Rows.Count > 0 Then
                    lbladmission.Text = dt.Rows(0)("AdmissionNo").ToString
                    txtstudent.Text = dt.Rows(0)("Student").ToString
                    txtgender.Text = dt.Rows(0)("Gender").ToString
                    txtdob.Text = dt.Rows(0)("Datofbirth").ToString
                    Ddlprogram.SelectedValue = dt.Rows(0)("CourseID").ToString
                    txtemailnew.Text = dt.Rows(0)("Email").ToString
                    txtmobilenew.Text = dt.Rows(0)("Mobile").ToString
                    txtaadhar.Text = dt.Rows(0)("AdharNo").ToString
                    txtaddress.Text = dt.Rows(0)("CorrAddress1").ToString
                    Ddlcountry.SelectedValue = dt.Rows(0)("CorrCountry").ToString
                    Ddlstate.SelectedValue = dt.Rows(0)("CorrState").ToString
                    ddlcity.SelectedValue = dt.Rows(0)("CorrCity").ToString
                    txtpincode.Text = dt.Rows(0)("CorrPinCode").ToString
                    txtguardianmobil.Text = dt.Rows(0)("GuardMobile").ToString


                End If
            End Using
        End Using

    End Sub

    Protected Sub Ddlcountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ddlcountry.SelectedIndexChanged
        fetchDdlState()
    End Sub

    Protected Sub Ddlstate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ddlstate.SelectedIndexChanged
        fetchDdlcity()
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If txtmobilenew.Text = "" Or txtmobilenew.Text = " " Then

        ElseIf txtemailnew.Text = "" Or txtemailnew.Text = " " Then
        ElseIf txtaddress.Text = "" Or txtaddress.Text = " " Then
        ElseIf txtaadhar.Text = "" Or txtaadhar.Text = " " Then
        ElseIf txtpincode.Text = "" Or txtpincode.Text = " " Then
        ElseIf txtguardianmobil.Text = "" Or txtguardianmobil.Text = " " Then

        Else

            Updatestudent()
            Page.ClientScript.RegisterClientScriptBlock(GetType(Page), "Script", "alert('Changes is done successfully');", True)
            getstudentinfo()

        End If


    End Sub

    Private Sub Updatestudent()
        Using con As SqlConnection = New SqlConnection(constr)
            Using cmd As SqlCommand = New SqlCommand("Sp_UpdateStudent")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@studentid", ViewState("stdntid"))
                cmd.Parameters.AddWithValue("@Mobile", txtmobilenew.Text)
                cmd.Parameters.AddWithValue("@Emailid", txtemailnew.Text)
                cmd.Parameters.AddWithValue("@corradd", txtaddress.Text)
                cmd.Parameters.AddWithValue("@Country", Ddlcountry.SelectedValue)
                cmd.Parameters.AddWithValue("@State", Ddlstate.SelectedValue)
                cmd.Parameters.AddWithValue("@city", ddlcity.SelectedValue)
                cmd.Parameters.AddWithValue("@pincode", txtpincode.Text)
                cmd.Parameters.AddWithValue("@guardiaanmobile", txtguardianmobil.Text)
                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()

            End Using
        End Using


    End Sub

    Private Sub fetchprogramdetail()
        Using con As New SqlConnection(constr)
            Dim cmd As New SqlCommand()
            Dim sql As String = "Select * from Exam_Course where CourseID ='" & Request.QueryString("programid") & "'"
            cmd.CommandText = sql
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                Dim dt As New DataTable
                sda.Fill(dt)
                If dt.Rows.Count > 0 Then
                    Lblprogram.Text = dt.Rows(0)("Course").ToString
                    lblprgramid.Text = dt.Rows(0)("Courseid").ToString
                    lblprogramcode.Text = dt.Rows(0)("Coursecode").ToString

                End If
            End Using
        End Using

    End Sub

    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        Response.Redirect("StudentList.aspx?rid=" & lblprgramid.Text & "&acyr=" & ViewState("Acdmcyear"))
    End Sub
End Class
