Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Partial Class ProgramMapping
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Session("Sessionidforcopy") = ""
                ViewState("Sessionid") = Request.QueryString("s")
                ViewState("Userid") = Request.QueryString("u")
                '..bindddlacademicyear..
                getacademicyear()

                Dim Academicyear As String = ""
                Dim query As String = "select AcademicYear from Exam_Session where Sessionid=" & ViewState("Sessionid") & ""
                Dim cmd As New SqlCommand(query)
                Using con As New SqlConnection(constr)
                    Using sda As New SqlDataAdapter()
                        Dim dt As New DataTable
                        cmd.Connection = con
                        con.Open()
                        sda.SelectCommand = cmd
                        sda.Fill(dt)
                        If dt.Rows.Count Then
                            Academicyear = dt.Rows(0)("AcademicYear").ToString
                        End If

                        con.Close()
                    End Using
                End Using

                '..by default current year select in ddlacademicyear..
                ddlacademicyear.Items.FindByValue(Academicyear).Selected = True

                lblacdemicyear.Text = ddlacademicyear.SelectedItem.Text
                '..bind grdCourse Grid..
                'BindGrid()
                '..bind Grdaftermap Grid..
                'BindGridaftermapping()
                '..bind GridCoursemapsession Grid..
                BindGridforupdate()
                '..gridcopyexamtype..
                bindcopygrid()

                btnRighttransfer.Visible = False
                btnlefttransfer.Visible = False
                grdcourse.Visible = False
                Grdaftermap.Visible = False
            Catch ex As Exception

            End Try


        End If


    End Sub

    '..Function for Message on popup without pageload..
    Private Sub Messagepop(ByVal p1 As String)
        Dim message As String = p1
        Dim script As String = "window.onload=function(){alert('"
        script &= message
        script &= "');"
        script &= "; }"

        ClientScript.RegisterStartupScript(Me.GetType(), "SuccessMessage", script, True)
    End Sub

    '..Function for Bind ddl for Academic Year..
    Private Sub getacademicyear()
        'Dim mylist As New List(Of Integer)
        'mylist.AddRange(Enumerable.Range(2010, 30).Reverse())
        'Ddlacademicyear.DataSource = mylist
        'Ddlacademicyear.DataBind()
        'Ddlacademicyear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = True
        'Dim Year As Integer
        'Year = Convert.ToInt32(Now.ToString("yyyy"))
        'txtacademicyear.Text = Year.ToString
        'Dim i As Integer
        'For i = Year - 10 To Year
        '    Ddlacademicyear.Items.Add(New ListItem(i.ToString(), i.ToString()))
        'Next
        'Ddlacademicyear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = True
        'Ddlsemyear.Items.Insert(0, New ListItem("All", ""))
        Dim query As String = "select AcademicYear from Exam_Session order by Academicyear"
        BindDropDownList1(ddlacademicyear, query, "AcademicYear", "AcademicYear", "")
    End Sub

    '..Function for Bind ddl..
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

    '..Function for Bind Grid Unselect Course..
    Private Sub BindGrid()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("select * from Exam_Course where Courseid not in (select Courseid from Exam_CourseSession where Academicyear='" & ddlacademicyear.SelectedItem.Text & "') order by Course")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd

                    Dim dt As New DataTable()
                    sda.Fill(dt)

                    grdcourse.DataSource = dt
                    grdcourse.DataBind()


                End Using
            End Using
        End Using
    End Sub

    '..Function for Bind Grid Unselect Course..
    Protected Sub btnRighttransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRighttransfer.Click
        UpdatePanel3.Visible = True
        For Each row As GridViewRow In grdcourse.Rows
            If (TryCast(row.FindControl("CheckBox1"), CheckBox)).Checked Then
                Dim CourseID As String = row.Cells(1).Text
                Dim Coursecode As String = row.Cells(3).Text
                Me.InsertCourse(CourseID, Coursecode)



            End If
        Next
        BindGridforupdating()
    End Sub

    Private Sub InsertCourse(ByVal CourseID As String, ByVal Coursecode As String)

        Using con As SqlConnection = New SqlConnection(constr)
            Using cmd As SqlCommand = New SqlCommand("Insert into Exam_CourseSession(Dated,Academicyear,coursecode,Courseid,Sessionid,userid) values(@Dated,@AcademicYear,@Coursecode,@Courseid,@Sessionid,@userid)", con)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.AddWithValue("@CourseID", CourseID)
                cmd.Parameters.AddWithValue("@Coursecode", Coursecode)
                cmd.Parameters.AddWithValue("@AcademicYear", ddlacademicyear.SelectedItem.Text)
                cmd.Parameters.AddWithValue("@Dated", Date.Now)

                cmd.Parameters.AddWithValue("@userid", ViewState("Userid"))

                If Session("Sessionidforcopy") = "" Then
                    cmd.Parameters.AddWithValue("@Sessionid", ViewState("Sessionid"))
                Else
                    cmd.Parameters.AddWithValue("@Sessionid", Session("Sessionidforcopy"))
                End If



                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
                BindGrid()

                BindGridaftermapping()


            End Using
        End Using
    End Sub

    Private Sub BindGridaftermapping()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("select Excs.Courseid,exc.Course from Exam_CourseSession excs join Exam_Course exc on exc.Courseid=excs.Courseid where Academicyear='" & ddlacademicyear.SelectedItem.Text & "' order by exc.Course")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd

                    Dim dt As New DataTable()
                    sda.Fill(dt)

                    Grdaftermap.DataSource = dt
                    Grdaftermap.DataBind()


                End Using
            End Using
        End Using
    End Sub

    Protected Sub btnlefttransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnlefttransfer.Click

        For Each row As GridViewRow In Grdaftermap.Rows
            If (TryCast(row.FindControl("CheckBox1"), CheckBox)).Checked Then
                Dim CourseID As String = row.Cells(1).Text

                Me.DeleteCourse(CourseID)
            End If

        Next
        BindGridforupdate()
    End Sub

    Private Sub DeleteCourse(ByVal CourseID As String)
        Dim count As String = 0
        Using con As SqlConnection = New SqlConnection(constr)
            Using cmd As SqlCommand = New SqlCommand("select Count(*) as Count from Exam_Coursesubject where Courseid='" & CourseID & "' and Academicyear='" & ddlacademicyear.SelectedItem.Text & "'", con)
                cmd.CommandType = CommandType.Text
                Using sda As New SqlDataAdapter()
                    sda.SelectCommand = cmd
                    Dim dt As New DataTable()
                    sda.Fill(dt)
                    count = dt.Rows(0)("Count").ToString

                End Using

            End Using
        End Using

        If count > 0 Then
            Messagepop("This Course is Map with " & ddlacademicyear.SelectedItem.Text & " Session")

        Else


            Using con As SqlConnection = New SqlConnection(constr)
                Using cmd As SqlCommand = New SqlCommand("Delete from Exam_CourseSession where Courseid='" & CourseID & "' and Academicyear='" + ddlacademicyear.SelectedItem.Text + "' ", con)
                    cmd.CommandType = CommandType.Text

                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                    BindGrid()

                    BindGridaftermapping()


                End Using
            End Using
        End If
    End Sub

    Private Sub BindGridforupdate()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("select t.Courseid,t.Coursecode,t.Coursetype,t.Duration,t.NoofSeat,t.SessionId,t.courselevel from " & _
"(Select c.Courseid,C.Coursecode,y.Coursetype,y.Duration,y.NoofSeat,y.SessionId,y.courselevel from Exam_Course c " & _
                "Left(Join) " & _
"(Select cs.Courseid,cs.Coursetype,cs.Duration,cs.NoofSeat,cs.SessionId,cs.courselevel from Exam_Course ce " & _
                "inner(Join) " & _
"Exam_CourseSession cs on ce.Courseid=cs.Courseid where cs.Academicyear='" & ddlacademicyear.SelectedItem.Text & "') y on c.Courseid=y.Courseid) as t " & _
"order by t.Coursecode")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd

                    Dim dt As New DataTable()
                    sda.Fill(dt)

                    GridCoursemapsession.DataSource = dt
                    GridCoursemapsession.DataBind()

                End Using
            End Using
        End Using

        fetchingridcontrol()
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click



        For Each row As GridViewRow In GridCoursemapsession.Rows


            Dim duration As String = TryCast(row.FindControl("txtduration"), TextBox).Text
            Dim noofseat As String = TryCast(row.FindControl("txtSeat"), TextBox).Text



            If duration = "" Or noofseat = "" Then

                Messagepop("Fill the all details")
                Exit Sub
            End If

        Next

        For Each row As GridViewRow In GridCoursemapsession.Rows


            Dim CourseID As String = row.Cells(1).Text
            Dim duration As String = TryCast(row.FindControl("txtduration"), TextBox).Text
            Dim noofseat As String = TryCast(row.FindControl("txtSeat"), TextBox).Text
            Dim courselevel As String = TryCast(row.FindControl("ddlCourselevel"), DropDownList).SelectedItem.Text
            Dim coursetype As String
            If TryCast(row.FindControl("ddlCoursetype"), DropDownList).SelectedItem.Text = "Semester" Then
                coursetype = "Sem"
            ElseIf TryCast(row.FindControl("ddlCoursetype"), DropDownList).SelectedItem.Text = "Year" Then
                coursetype = "Year"
            End If



            Me.Updatecoursesession(CourseID, duration, noofseat, courselevel, coursetype)


        Next

        'Response.Write("<script LANGUAGE='JavaScript' >setTimeout(function () {alert('Successfully Save').fadeTo(2000, 500).slideUp(500, function () { alert('Successfully Save').remove();}); }, 0); </script>")

        Messagepop("Saved Successfully")

    End Sub

    Private Sub Updatecoursesession(ByVal CourseID As String, ByVal duration As String, ByVal noofseat As String, ByVal courselevel As String, ByVal coursetype As String)
        Using con As New SqlConnection(constr)
            Dim query As String = "update Exam_Coursesession set Duration=@Duration,Coursetype=@Coursetype,courselevel=@courselevel,NoofSeat=@noofseat where Courseid='" & CourseID & "' and Academicyear ='" & ddlacademicyear.SelectedItem.Text & "' "
            Using cmd As New SqlCommand(query)
                cmd.Parameters.AddWithValue("@Duration", duration)
                cmd.Parameters.AddWithValue("@Coursetype", coursetype)
                cmd.Parameters.AddWithValue("@courselevel", courselevel)
                cmd.Parameters.AddWithValue("@noofseat", noofseat)


                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using
    End Sub

    Protected Sub btncopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncopy.Click
        Pnlcopyexamtype.Visible = True
        Panelmap.Visible = False
        fillDdlcopyacademicyear()

    End Sub

    Private Sub fillDdlcopyacademicyear()

        Try




            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select Distinct Academicyear from Exam_Coursesession where Academicyear <> '" & ddlacademicyear.SelectedItem.Text & "' order by Academicyear desc ")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            Ddlcopyacademicyear.DataSource = dt
                            Ddlcopyacademicyear.DataTextField = "Academicyear"
                            Ddlcopyacademicyear.DataValueField = "Academicyear"
                            Ddlcopyacademicyear.DataBind()


                        End Using
                    End Using
                End Using
            End Using
            Lblyear.Text = Ddlcopyacademicyear.SelectedItem.Text
            bindcopygrid()
        Catch ex As Exception

            Messagepop("Previous Years Not Exist")
            Panelmap.Visible = True
            Pnlcopyexamtype.Visible = False
        End Try
    End Sub

    Protected Sub Ddlcopyacademicyear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ddlcopyacademicyear.SelectedIndexChanged
        Lblyear.Text = Ddlcopyacademicyear.SelectedItem.Text
    End Sub

    Private Sub bindcopygrid()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("select Excs.*,exc.Course from Exam_CourseSession excs join Exam_Course exc on exc.Courseid=excs.Courseid where Academicyear='" & Lblyear.Text & "'  order by exc.Course")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd

                    Dim dt As New DataTable()
                    sda.Fill(dt)

                    gridcopyexamtype.DataSource = dt
                    gridcopyexamtype.DataBind()


                End Using
            End Using
        End Using
    End Sub

    Protected Sub btncopyfromprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncopyfromprevious.Click

        newacademicyearsessionid()

        For Each row As GridViewRow In gridcopyexamtype.Rows

            Dim CourseID As String = row.Cells(3).Text
            Dim Coursetype As String = row.Cells(4).Text
            Dim Duration As String = row.Cells(6).Text
            Dim courselevel As String = row.Cells(8).Text
            Dim coursecode As String = row.Cells(7).Text
            Dim noofseat As String = row.Cells(9).Text
            Me.addfrompreviosyear(CourseID, Duration, coursecode, noofseat, courselevel, Coursetype)
        Next

        Dim Count As String = 0
        For Each row As GridViewRow In gridcopyexamtype.Rows
            If (TryCast(row.FindControl("CheckBox1"), CheckBox)).Checked Then
                Count += 1
            End If

        Next

        If Count = 0 Then
            Messagepop("Please Select a Program")
        Else
            Messagepop("Copied Successfully")
        End If



        For Each row1 As GridViewRow In gridcopyexamtype.Rows

            TryCast(row1.FindControl("CheckBox1"), CheckBox).Checked = False

        Next


    End Sub

    Private Sub addfrompreviosyear(ByVal CourseID As String, ByVal Duration As String, ByVal coursecode As String, ByVal noofseat As String, ByVal courselevel As String, ByVal Coursetype As String)
        Using con As New SqlConnection(constr)
            Dim query As String = "insert into Exam_Coursesession(Dated,Userid,Academicyear,Duration,Coursetype,Courseid,Coursecode,courselevel,noofseat,Sessionid) values(@Dated,@Userid,@Academicyear,@Duration,@Coursetype,@Courseid,@Coursecode,@courselevel,@noofseat,@Sessionid) "
            Using cmd As New SqlCommand(query)
                cmd.Parameters.AddWithValue("@Dated", Date.Now)
                cmd.Parameters.AddWithValue("@Userid", Request.QueryString("u"))
                cmd.Parameters.AddWithValue("@Duration", Duration)
                cmd.Parameters.AddWithValue("@Coursetype", Coursetype)
                cmd.Parameters.AddWithValue("@Courseid", CourseID)
                cmd.Parameters.AddWithValue("@Coursecode", coursecode)
                cmd.Parameters.AddWithValue("@courselevel", courselevel)
                cmd.Parameters.AddWithValue("@noofseat", noofseat)
                cmd.Parameters.AddWithValue("@Sessionid", Session("Sessionidforcopy"))
                cmd.Parameters.AddWithValue("@Academicyear", ddlacademicyear.SelectedItem.Text)

                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using
    End Sub

    Protected Sub ddlacademicyear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlacademicyear.SelectedIndexChanged
        lblacdemicyear.Text = ddlacademicyear.SelectedItem.Text

        If ddlacademicyear.SelectedItem.Text < Convert.ToInt32(Now.ToString("2018")) Then
            BindGrid()
            BindGridaftermapping()

            BindGridforupdate()

            btnlefttransfer.Visible = False
            btnRighttransfer.Visible = False
            btnSubmit.Visible = False
            righttransfer.Visible = False
            lefttransfer.Visible = False

        Else
            newacademicyearsessionid()
            BindGrid()
            BindGridaftermapping()

            BindGridforupdate()

            btnlefttransfer.Visible = True
            btnRighttransfer.Visible = True
            btnSubmit.Visible = True
            righttransfer.Visible = True
            lefttransfer.Visible = True
        End If

    End Sub

    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        Pnlcopyexamtype.Visible = False
        Panelmap.Visible = True
        Response.Redirect(Request.RawUrl)
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        GridCoursemapsession.PageIndex = e.NewPageIndex
        Me.BindGridforupdate()
    End Sub

    Private Sub BindGridforupdating()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("select Excs.*,exc.Course from Exam_CourseSession excs join Exam_Course exc on exc.Courseid=excs.Courseid where excs.Academicyear='" & ddlacademicyear.SelectedItem.Text & "' order by excs.dated desc")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd

                    Dim dt As New DataTable()
                    sda.Fill(dt)

                    GridCoursemapsession.DataSource = dt
                    GridCoursemapsession.DataBind()

                End Using
            End Using
        End Using

        fetchingridcontrol()

    End Sub

    Private Sub fetchingridcontrol()
        Try


            For Each row As GridViewRow In GridCoursemapsession.Rows
                Dim Courseid As String = row.Cells(1).Text

                Using con As New SqlConnection(constr)
                    Using cmd As New SqlCommand("select Excs.*,exc.Course from Exam_CourseSession excs join Exam_Course exc on exc.Courseid=excs.Courseid where excs.Academicyear='" & ddlacademicyear.SelectedItem.Text & "' and excs.Courseid='" & Courseid & "'")
                        Using sda As New SqlDataAdapter()
                            cmd.Connection = con
                            sda.SelectCommand = cmd

                            Dim dt As New DataTable()
                            sda.Fill(dt)

                            TryCast(row.FindControl("txtduration"), TextBox).Text = dt.Rows(0)("Duration").ToString
                            TryCast(row.FindControl("txtSeat"), TextBox).Text = dt.Rows(0)("NoofSeat").ToString
                            Dim Coursetype As String = dt.Rows(0)("Coursetype").ToString
                            Dim Courselevel As String = dt.Rows(0)("courselevel").ToString

                            If Coursetype = "" And Courselevel = "" Then
                                TryCast(row.FindControl("ddlCoursetype"), DropDownList).SelectedItem.Value = dt.Rows(0)("Coursetype").ToString
                                TryCast(row.FindControl("ddlCourselevel"), DropDownList).SelectedItem.Value = dt.Rows(0)("courselevel").ToString

                            Else
                                TryCast(row.FindControl("ddlCoursetype"), DropDownList).Items.FindByValue(dt.Rows(0)("Coursetype").ToString()).Selected = True
                                TryCast(row.FindControl("ddlCourselevel"), DropDownList).Items.FindByValue(dt.Rows(0)("courselevel").ToString()).Selected = True
                            End If



                        End Using
                    End Using
                End Using
            Next
        Catch ex As Exception

        End Try

    End Sub

    Private Sub newacademicyearsessionid()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Sessionid from Exam_Session where Academicyear='" & ddlacademicyear.SelectedItem.Text & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd

                    Dim dt As New DataTable()
                    sda.Fill(dt)

                    Session("Sessionidforcopy") = dt.Rows(0)("Sessionid").ToString()
                End Using
            End Using
        End Using
    End Sub

    Protected Sub btnmasterprogram_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnmasterprogram.Click
        Response.Redirect("AddProgram.aspx?s=" & ViewState("Sessionid") & "&u=" & ViewState("Userid"))
    End Sub

End Class
