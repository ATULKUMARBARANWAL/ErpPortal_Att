Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Partial Class Examinationjune_Programtosession
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
    Private cmd1 As dbnew = New dbnew()

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
             
                '..bind GridCoursemapsession Grid..
                BindGridforupdate()
                '..gridcopyexamtype..
                bindcopygrid()

                checkforbutton()
            Catch ex As Exception

            End Try


        End If


    End Sub

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

    Private Sub BindGridforupdate()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("select t.Coursesessionid ,t.Courseid,t.Coursecode,t.Coursetype,t.Duration,t.NoofSeat,t.SessionId,t.courselevel from " & _
"(Select c.Courseid,C.Coursecode,y.Coursesessionid ,y.Coursetype,y.Duration,y.NoofSeat,y.SessionId,y.courselevel from Exam_Course c " & _
                "Left Join  " & _
"(Select cs.Coursesessionid ,cs.Courseid,cs.Coursetype,cs.Duration,cs.NoofSeat,cs.SessionId,cs.courselevel from Exam_Course ce " & _
                "inner Join  " & _
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

                            If Coursetype = "" Then
                                TryCast(row.FindControl("ddlCoursetype"), DropDownList).SelectedItem.Value = dt.Rows(0)("Coursetype").ToString
                                ' TryCast(row.FindControl("ddlCourselevel"), DropDownList).SelectedItem.Value = dt.Rows(0)("courselevel").ToString

                            Else
                                TryCast(row.FindControl("ddlCoursetype"), DropDownList).Items.FindByValue(dt.Rows(0)("Coursetype").ToString()).Selected = True
                                '  TryCast(row.FindControl("ddlCourselevel"), DropDownList).Items.FindByValue(dt.Rows(0)("courselevel").ToString()).Selected = True
                            End If

                            If Courselevel = "" Then
                                'TryCast(row.FindControl("ddlCoursetype"), DropDownList).SelectedItem.Value = dt.Rows(0)("Coursetype").ToString
                                TryCast(row.FindControl("ddlCourselevel"), DropDownList).SelectedItem.Value = dt.Rows(0)("courselevel").ToString

                            Else
                                ' TryCast(row.FindControl("ddlCoursetype"), DropDownList).Items.FindByValue(dt.Rows(0)("Coursetype").ToString()).Selected = True
                                TryCast(row.FindControl("ddlCourselevel"), DropDownList).Items.FindByValue(dt.Rows(0)("courselevel").ToString()).Selected = True
                            End If

                        End Using
                    End Using
                End Using
            Next
        Catch ex As Exception

        End Try

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
            Dim Course As String = row.Cells(2).Text
            Dim duration As String = TryCast(row.FindControl("txtduration"), TextBox).Text
            Dim noofseat As String = TryCast(row.FindControl("txtSeat"), TextBox).Text
            Dim courselevel As String = TryCast(row.FindControl("ddlCourselevel"), DropDownList).SelectedItem.Text
            Dim coursetype As String = ""
            If TryCast(row.FindControl("ddlCoursetype"), DropDownList).SelectedItem.Text = "Semester" Then
                coursetype = "Sem"
            ElseIf TryCast(row.FindControl("ddlCoursetype"), DropDownList).SelectedItem.Text = "Year" Then
                coursetype = "Year"
            End If

            Dim Sql As String = "Select * from Exam_coursesession where Sessionid='" & ViewState("Sessionid") & "' and Courseid='" & CourseID & "'"
            Dim COursesessionid As String = ""
            Dim dt As DataTable = cmd1.getDataTable(Sql)
            If dt.Rows.Count > 0 Then
                COursesessionid = dt.Rows(0)("Coursesessionid").ToString

                updateexamCourseSession(COursesessionid, duration, noofseat, courselevel, coursetype)
            Else '
                Me.Updatecoursesession(CourseID, Course, duration, noofseat, courselevel, coursetype)
            End If




        Next

        'Response.Write("<script LANGUAGE='JavaScript' >setTimeout(function () {alert('Successfully Save').fadeTo(2000, 500).slideUp(500, function () { alert('Successfully Save').remove();}); }, 0); </script>")

        Messagepop("Saved Successfully")
    End Sub

    Private Sub Updatecoursesession(ByVal CourseID As String, ByVal Course As String, ByVal duration As String, ByVal noofseat As String, ByVal courselevel As String, ByVal coursetype As String)
        Using con As New SqlConnection(constr)
            Dim query As String = " insert into Exam_CourseSession(Dated,Academicyear,Duration,Coursetype,Courseid,coursecode,courselevel,userid,NoofSeat,SessionId ) " & _
"Values(@Dated,@Academicyear,@Duration,@Coursetype,@Courseid,@coursecode,@courselevel,@userid,@NoofSeat,@SessionId)"
            Using cmd As New SqlCommand(query)
                cmd.Parameters.AddWithValue("@Courseid", CourseID)
                cmd.Parameters.AddWithValue("@dated", Date.Now())
                cmd.Parameters.AddWithValue("@Academicyear", ddlacademicyear.SelectedItem.Text)
                cmd.Parameters.AddWithValue("@coursecode", Course)
                cmd.Parameters.AddWithValue("@Duration", duration)
                cmd.Parameters.AddWithValue("@Coursetype", coursetype)
                cmd.Parameters.AddWithValue("@courselevel", courselevel)
                cmd.Parameters.AddWithValue("@noofseat", noofseat)
                cmd.Parameters.AddWithValue("@userid", ViewState("Userid"))
                If Session("Sessionidforcopy") = "" Then
                    cmd.Parameters.AddWithValue("@Sessionid", ViewState("Sessionid"))
                Else
                    cmd.Parameters.AddWithValue("@Sessionid", Session("Sessionidforcopy"))
                End If

                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using
    End Sub

    Protected Sub ddlacademicyear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlacademicyear.SelectedIndexChanged
        lblacdemicyear.Text = ddlacademicyear.SelectedItem.Text

            newacademicyearsessionid()
           
            BindGridforupdate()

          
        checkforbutton()
           

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

    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        Pnlcopyexamtype.Visible = False
        Panelmap.Visible = True
        Response.Redirect(Request.RawUrl)
    End Sub

    Private Sub checkforbutton()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select * from Exam_CourseSession where Academicyear='" & ddlacademicyear.SelectedItem.Text & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd

                    Dim dt As New DataTable()
                    sda.Fill(dt)
                    If dt.Rows.Count Then

                        btnSubmit.Visible = True

                        GridCoursemapsession.HeaderRow.Cells(7).Visible = True
                        For Each row As GridViewRow In GridCoursemapsession.Rows
                            row.Cells(7).Visible = True

                        Next

                    Else
                        btnSubmit.Visible = True

                        GridCoursemapsession.HeaderRow.Cells(7).Visible = True
                        For Each row As GridViewRow In GridCoursemapsession.Rows


                            row.Cells(7).Visible = True
                        Next
                    End If

                End Using
            End Using
        End Using
    End Sub

    Protected Sub GridCoursemapsession_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridCoursemapsession.RowCommand
        If e.CommandName = "select" Then
            GridCoursemapsession.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridCoursemapsession.Rows(rowIndex)

            Dim Courseid As String = row.Cells(1).Text
            Dim Course As String = row.Cells(2).Text


            Dim duration As String = TryCast(row.FindControl("txtduration"), TextBox).Text
            Dim noofseat As String = TryCast(row.FindControl("txtSeat"), TextBox).Text



            If duration = "" Or noofseat = "" Then

                Messagepop("Fill the all details")
                Exit Sub
            Else



                Dim courselevel As String = TryCast(row.FindControl("ddlCourselevel"), DropDownList).SelectedItem.Text
                Dim coursetype As String = ""
                If TryCast(row.FindControl("ddlCoursetype"), DropDownList).SelectedItem.Text = "Semester" Then
                    coursetype = "Sem"
                ElseIf TryCast(row.FindControl("ddlCoursetype"), DropDownList).SelectedItem.Text = "Year" Then
                    coursetype = "Year"
                End If


                Using con As New SqlConnection(constr)
                    Using cmd As New SqlCommand("Select * from Exam_CourseSession where Academicyear='" & ddlacademicyear.SelectedItem.Text & "' and Courseid='" & Courseid & "'")
                        Using sda As New SqlDataAdapter()
                            cmd.Connection = con
                            sda.SelectCommand = cmd

                            Dim dt As New DataTable()
                            sda.Fill(dt)

                            If dt.Rows.Count Then
                                Dim COursesessionid As String = GridCoursemapsession.SelectedDataKey(0)
                                updateexamCourseSession(COursesessionid, duration, noofseat, courselevel, coursetype)

                                SaralMsg.Messagebx.Alert(Me, "Sucessfully Updated")

                            Else
                                insertexamCourseSession(Courseid, Course, duration, noofseat, courselevel, coursetype)

                                SaralMsg.Messagebx.Alert(Me, "Sucessfully Updated")


                            End If

                        End Using
                    End Using
                End Using


               
            End If



        End If
    End Sub

    Private Sub updateexamCourseSession(ByVal COursesessionid As String, ByVal duration As String, ByVal noofseat As String, ByVal courselevel As String, ByVal coursetype As String)

        Using con As New SqlConnection(constr)
            Dim query As String = "update Exam_Coursesession set Duration=@Duration,Coursetype=@Coursetype,courselevel=@courselevel,NoofSeat=@noofseat where Coursesessionid ='" & COursesessionid & "' "
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

    Protected Sub OnRowEditing(ByVal sender As Object, ByVal f As GridViewEditEventArgs)


    End Sub

    Private Sub insertexamCourseSession(ByVal Courseid As String, ByVal Course As String, ByVal duration As String, ByVal noofseat As String, ByVal courselevel As String, ByVal coursetype As String)
        Using con As New SqlConnection(constr)
            Dim query As String = " insert into Exam_CourseSession(Dated,Academicyear,Duration,Coursetype,Courseid,coursecode,courselevel,userid,NoofSeat,SessionId ) " & _
"Values(@Dated,@Academicyear,@Duration,@Coursetype,@Courseid,@coursecode,@courselevel,@userid,@NoofSeat,@SessionId)"
            Using cmd As New SqlCommand(query)
                cmd.Parameters.AddWithValue("@Courseid", Courseid)
                cmd.Parameters.AddWithValue("@dated", Date.Now())
                cmd.Parameters.AddWithValue("@Academicyear", ddlacademicyear.SelectedItem.Text)
                cmd.Parameters.AddWithValue("@coursecode", Course)
                cmd.Parameters.AddWithValue("@Duration", duration)
                cmd.Parameters.AddWithValue("@Coursetype", coursetype)
                cmd.Parameters.AddWithValue("@courselevel", courselevel)
                cmd.Parameters.AddWithValue("@noofseat", noofseat)
                cmd.Parameters.AddWithValue("@userid", ViewState("Userid"))
                If Session("Sessionidforcopy") = "" Then
                    cmd.Parameters.AddWithValue("@Sessionid", ViewState("Sessionid"))
                Else
                    cmd.Parameters.AddWithValue("@Sessionid", Session("Sessionidforcopy"))
                End If

                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using
    End Sub

End Class
