Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Partial Class Examinationjune_Facultyfeedback
    Inherits System.Web.UI.Page

    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

    <System.Web.Services.WebMethodAttribute(), _
System.Web.Script.Services.ScriptMethodAttribute()> _
    Public Shared Function GetCompletionList(ByVal prefixText As String, _
              ByVal count As Integer) As String()
        Dim cmd As New SqlCommand With {.Connection = db.Con}
        cmd.CommandText = "select * from SearchSubject(" & HttpContext.Current.Session("courseid") & "," & HttpContext.Current.Session("Sessionid") & ") where list LIKE '" & _
         "%" & prefixText & "%' ORDER BY List"
        Dim myReader As SqlDataReader
        Dim returnData As List(Of String) = New List(Of String)
        myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While myReader.Read()
            returnData.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myReader("List").ToString(), myReader("Subjectid")))
        End While
        Return returnData.ToArray()
    End Function

    <System.Web.Services.WebMethodAttribute(), _
System.Web.Script.Services.ScriptMethodAttribute()> _
    Public Shared Function GetCompletionListall(ByVal prefixText As String, _
              ByVal count As Integer) As String()
        Dim cmd As New SqlCommand With {.Connection = db.Con}
        cmd.CommandText = "select * from SearchSubjectformap(" & HttpContext.Current.Session("courseid") & "," & HttpContext.Current.Session("Sessionid") & ") where list LIKE '" & _
         "%" & prefixText & "%' ORDER BY List"
        Dim myReader As SqlDataReader
        Dim returnData As List(Of String) = New List(Of String)
        myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While myReader.Read()
            returnData.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myReader("List").ToString(), myReader("Subjectid")))
        End While
        Return returnData.ToArray()
    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.Form("__EVENTTARGET") = "load$student" Then
            'txtbind.bind(Request.Form("__EVENTARGUMENt"))
            '   ViewState("userid") = Request.Form("__EVENTARGUMENt")
            bind(Request.Form("__EVENTARGUMENt"))
        End If

        If Request.Form("__EVENTTARGET") = "load$allsub" Then
            'txtbind.bind(Request.Form("__EVENTARGUMENt"))
            '   ViewState("userid") = Request.Form("__EVENTARGUMENt")
            bindsubject(Request.Form("__EVENTARGUMENt"))
        End If

        If Not IsPostBack Then
            fetchddlacademicyear()
            ViewState("Academicyear") = Request.QueryString("acyr")
            ViewState("SessionId") = Request.QueryString("s")
            Session("Sessionid") = Request.QueryString("s")
            ViewState("Courseid") = Request.QueryString("rid")
            Session("courseid") = Request.QueryString("rid")
            ViewState("Userid") = Request.QueryString("u")
            ViewState("ayid") = Request.QueryString("ay")
            fetchddlProgram()

            fillddlsemyear()

            GridsubjectAllsem()
            lbltotalsub.Text = Request.QueryString("acyr")
            ' fetchcountsubject()
        End If
    End Sub

    Private Sub fetchddlacademicyear()
        'Dim mylist As New List(Of Integer)
        'mylist.AddRange(Enumerable.Range(2010, 30).Reverse())
        'Ddlacademicyear.DataSource = mylist
        'Ddlacademicyear.DataBind()
        'Ddlacademicyear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = True
        'Try

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("select * from Exam_Session order by Academicyear  desc")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        ddlacademicyear.DataSource = dt
                        ddlacademicyear.DataTextField = "Academicyear"
                        ddlacademicyear.DataValueField = "Academicyear"
                        ddlacademicyear.DataBind()
                        Dim Year As Integer
                        Year = Convert.ToInt32(Now.ToString("yyyy"))

                        ddlacademicyear.Items.FindByValue(Year).Selected = True

                    End Using
                End Using
            End Using
        End Using

        'Catch ex As Exception


        'End Try


    End Sub

    Private Sub fetchddlProgram()
        'Try

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Cs.Courseid, C.Course  from Exam_CourseSession Cs join Exam_Course C on Cs.Courseid =C.Courseid " & _
" Where Cs.Academicyear ='" & ViewState("Academicyear") & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Ddlprogram.DataSource = dt
                        Ddlprogram.DataTextField = "Course"
                        Ddlprogram.DataValueField = "Courseid"
                        Ddlprogram.DataBind()
                        '  Dim Year As Integer
                        ' Year = Convert.ToInt32(Now.ToString("yyyy"))
                        labeldata()
                        Ddlprogram.Items.FindByValue(ViewState("Courseid")).Selected = True

                    End Using
                End Using
            End Using
        End Using

        'Catch ex As Exception

        'End Try


    End Sub
    Private Sub labeldata()
        Dim ds As New DataSet
        Using con As New SqlConnection(constr)
            con.Open()
            Dim cmd1 As New SqlCommand("select courseid,Coursecode from Exam_Course where course='" & Request.QueryString("rid") & "' ", con)
            Dim da As New SqlDataAdapter(cmd1)
            cmd1.Connection = con
            da.Fill(ds)
            Dim i = ds.Tables(0).Rows.Count()
            If (i > 0) Then
                ViewState("Courseid") = ds.Tables(0).Rows(0)("courseid").ToString()
                ViewState("Coursecode") = ds.Tables(0).Rows(0)("Coursecode").ToString()
            End If
            con.Close()
        End Using
    End Sub
    Private Sub fetchcountsubject()
        Dim ds As New DataSet
        Using con As New SqlConnection(constr)
            con.Open()
            Dim cmd1 As New SqlCommand(" Select Count(Cs.Subjectid) as 'TotalSubject' from Exam_Coursesubject Cs where Cs.Academicyear='" & ViewState("Academicyear") & "' and Cs.Courseid ='" & ViewState("Courseid") & "'", con)
            Dim da As New SqlDataAdapter(cmd1)
            cmd1.Connection = con
            da.Fill(ds)
            Dim i = ds.Tables(0).Rows.Count()
            If (i > 0) Then
                lbltotalsub.Text = ds.Tables(0).Rows(0)("TotalSubject").ToString()
            End If
            con.Close()

        End Using

    End Sub

    Protected Sub Ddlprogram_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ddlprogram.SelectedIndexChanged
        Session("courseid") = ""

        Session("courseid") = Ddlprogram.SelectedValue

        ' fetchcountsubject()
        Ddlsemyear.Items.Clear()
        fillddlsemyear()
        Ddlsemeser.Items.Clear()

        fillddlsemyear2()
        Ddlsection.Items.Clear()
        fillDdlsection()
        FetchGridallsubject()
        If Ddlsemyear.SelectedItem.Text = "All" Then
            GridsubjectAllsem()
        Else
            Gridsubjectsemwise()
        End If
    End Sub
    Private Sub fillddlsemyear()

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Coursesessionid, Academicyear, Courseid, CourseType, case when Coursetype Like '%sem%' then Duration*2 " & _
" when Coursetype like '%year%' then Duration*1 when Coursetype like '%quart%' then Duration*4 end as 'Totalsem', " & _
 " Duration, Coursetype from Exam_CourseSession where  Academicyear ='" & ViewState("Academicyear") & "' and Courseid = '" & Ddlprogram.SelectedValue & "' ")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Ddlsemyear.DataSource = dt
                        Dim totalsem As String = dt.Rows(0)("Totalsem").ToString()
                        Lblsemyear.Text = dt.Rows(0)("CourseType").ToString()
                        Dim i As Integer
                        For i = 1 To totalsem
                            Ddlsemyear.Items.Add(New ListItem(i.ToString(), i.ToString()))

                        Next

                        Ddlsemyear.Items.Insert(0, New ListItem("All", ""))
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub fillddlsemyear2()

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Coursesessionid, Academicyear, Courseid, CourseType, case when Coursetype Like '%sem%' then Duration*2 " & _
" when Coursetype like '%year%' then Duration*1 when Coursetype like '%quart%' then Duration*4 end as 'Totalsem', " & _
 " Duration, Coursetype from Exam_CourseSession where  Academicyear ='" & ViewState("Academicyear") & "' and Courseid = '" & Ddlprogram.SelectedValue & "' ")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Ddlsemeser.DataSource = dt
                        Dim totalsem As String = dt.Rows(0)("Totalsem").ToString()
                        Lblsemyear.Text = dt.Rows(0)("CourseType").ToString()
                        Dim i As Integer
                        For i = 1 To totalsem
                            Ddlsemeser.Items.Add(New ListItem(i.ToString(), i.ToString()))

                        Next

                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub fillDdlsection()

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(" Select Distinct st.Classesid , cls.Code from Student st " & _
                                        " join Classes cls on st.Classesid=cls.ClassesID where St.sessionid='" & Request.QueryString("s") & "' and st.CourseID='" & Ddlprogram.SelectedValue & "' and st.Sem='" & Ddlsemeser.SelectedItem.Text & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Ddlsection.DataSource = dt
                        Ddlsection.DataTextField = "Code"
                        Ddlsection.DataValueField = "Classesid"
                        Ddlsection.DataBind()

                    End Using
                End Using
            End Using
        End Using

    End Sub

    Private Sub GridsubjectAllsem()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "Select Csub.Coursesubid, Csub.Academicyear,p.employee ,sp.facultyid," & _
" Csub.Coursesessionid, Csub.Courseid, Csub.Semyear, Csub.Subjectid,sp.classesid,cls.Code, Sub.Subject,  Sub.Subjectcode, Sub.Subprefix from Exam_Coursesubject Csub " & _
"join Exam_subjectplan sp on sp.subjectid COLLATE SQL_Latin1_General_CP1_CI_AS = csub.subjectid and csub.courseid = sp.courseid COLLATE SQL_Latin1_General_CP1_CI_AS " & _
" join p_employee p on p.employeeid = sp.facultyid " & _
" join Exam_Subject Sub on Csub.Subjectid =sub.Subjectid  join classes cls on cls.classesid = sp.classesid  " & _
"  Where Csub.Academicyear ='" & ViewState("Academicyear") & "' and Csub.Courseid='" & Ddlprogram.SelectedValue & "' and sp.classesid not in ('17') order by Csub.Semyear"
                If Not String.IsNullOrEmpty(txtsearch.Text.Trim()) Then
                    query += " and Sub.Subject LIKE '%' + @Subject + '%' "
                    cmd.Parameters.AddWithValue("@Subject", txtsearch.Text.Trim())
                End If
                cmd.CommandText = query
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        gridallsubject.DataSource = dt
                        gridallsubject.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub


    Private Sub Gridsubjectsemwise()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()

                Dim query As String = "Select Csub.Coursesubid, Csub.Academicyear,p.employee ,sp.facultyid," & _
" Csub.Coursesessionid, Csub.Courseid, Csub.Semyear, Csub.Subjectid,sp.classesid,cls.Code, Sub.Subject,  Sub.Subjectcode, Sub.Subprefix from Exam_Coursesubject Csub " & _
"join Exam_subjectplan sp on sp.subjectid COLLATE SQL_Latin1_General_CP1_CI_AS = csub.subjectid and csub.courseid = sp.courseid COLLATE SQL_Latin1_General_CP1_CI_AS " & _
" join p_employee p on p.employeeid = sp.facultyid " & _
" join Exam_Subject Sub on Csub.Subjectid =sub.Subjectid join classes cls on cls.classesid = sp.classesid   " & _
"  Where Csub.Academicyear ='" & ViewState("Academicyear") & "' and Csub.Courseid='" & Ddlprogram.SelectedValue & "' and sp.classesid not in ('17')  And Csub.Semyear ='" & Ddlsemyear.SelectedItem.Text & "'  order by Csub.Semyear "
                If Not String.IsNullOrEmpty(txtsearch.Text.Trim()) Then
                    Query += " and Sub.Subject LIKE '%' + @Subject + '%'"
                    cmd.Parameters.AddWithValue("@Subject", txtsearch.Text.Trim())
                End If
                cmd.CommandText = Query

                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        gridallsubject.DataSource = dt
                        gridallsubject.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub FetchGridallsubject()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim sql As String = "Select Csub.Coursesubid, Csub.Academicyear,p.employee ,sp.facultyid," & _
 " Csub.Coursesessionid, Csub.Courseid, Csub.Semyear, Csub.Subjectid, Sub.Subject,  Sub.Subjectcode, Sub.Subprefix from Exam_Coursesubject Csub " & _
 "join Exam_subjectplan sp on sp.subjectid COLLATE SQL_Latin1_General_CP1_CI_AS = csub.subjectid and csub.courseid = sp.courseid COLLATE SQL_Latin1_General_CP1_CI_AS " & _
 " join p_employee p on p.employeeid = sp.facultyid " & _
 " join Exam_Subject Sub on Csub.Subjectid =sub.Subjectid  " & _
 "  Where Csub.Academicyear ='" & ViewState("Academicyear") & "' and Csub.Courseid='" & Ddlprogram.SelectedValue & "' and sp.classesid not in ('17') order by Csub.Semyear"

                If Not String.IsNullOrEmpty(txtsearch.Text.Trim()) Then
                    sql += " where Subject LIKE '%' + @Subject + '%'"
                    cmd.Parameters.AddWithValue("@Subject", txtsearch.Text.Trim())
                End If
                cmd.CommandText = sql

                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        GridMapSubject.DataSource = dt
                        GridMapSubject.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Protected Sub Ddlsemyear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ddlsemyear.SelectedIndexChanged
        If Ddlsemyear.SelectedItem.Text = "All" Then
            ' fetchcountsubjectsemwise()
            GridsubjectAllsem()
        Else
            ' fetchcountsubjectsemwise()
            Gridsubjectsemwise()
        End If
    End Sub

    Private Sub Insertsubject()

        Dim Subjectid As Integer = 0
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Sp_InsertSubject", con)
                cmd.CommandType = CommandType.StoredProcedure
                Using sda As New SqlDataAdapter()
                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    cmd.Parameters.AddWithValue("@Dated", "")
                    cmd.Parameters.AddWithValue("@Subject ", txtsub.Text.Trim())
                    cmd.Parameters.AddWithValue("@Subjectcode ", txtcode.Text.Trim())
                    cmd.Parameters.AddWithValue("@Subprefix ", txtprefix.Text.Trim())
                    cmd.Parameters.AddWithValue("@subjecttype ", Ddlsubtype.SelectedItem.Text)
                    cmd.Connection = con
                    con.Open()
                    Subjectid = cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using
        End Using
        Page.ClientScript.RegisterClientScriptBlock(GetType(Page), "Script", "alert('Subject is saved successful.');", True)


    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        'Try
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(" SELECT * FROM dbo.Exam_Subject  WHERE Subject ='" & txtsub.Text & "' Or Subjectcode='" & txtcode.Text & "' and Subtype ='" & Ddlsubtype.SelectedItem.Text & "'", con)

                Dim da As New DataSet
                Dim ds As New SqlDataAdapter(cmd)
                ds.Fill(da)
                Dim i = da.Tables(0).Rows.Count
                If txtsub.Text = " " Or txtsub.Text = "" Then
                    SaralMsg.Messagebx.Alert(Me, "Please enter Subject")
                ElseIf txtcode.Text = " " Or txtcode.Text = "" Then
                    SaralMsg.Messagebx.Alert(Me, "Please enter Subject code")
                ElseIf txtprefix.Text = " " Or txtprefix.Text = "" Then
                    SaralMsg.Messagebx.Alert(Me, "Please enter Subject presfix")




                    'Dim message As String = " Please Enter exam name."
                    'Dim script As String = "window.onload=function(){alert('"
                    'script &= message
                    'script &= "');"
                    'script &= "window.location = '"
                    ''                    script &= Request.Url.AbsoluteUri
                    'script &= "'; }"
                    'ClientScript.RegisterStartupScript(Me.GetType(), "SuccessMessage", script, True)

                ElseIf i > 0 Then
                    SaralMsg.Messagebx.Alert(Me, "Subject name is already created with code and type.")
                Else
                    Insertsubject()
                    txtsub.Text = ""
                    txtcode.Text = ""
                    txtprefix.Text = ""
                    FetchGridallsubject()

                End If


            End Using
        End Using





        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub Insertsubject(ByVal Subjectid As String, ByVal Subjectcode As String)
        Using con As SqlConnection = New SqlConnection(constr)
            Using cmd As SqlCommand = New SqlCommand("Sp_SubjectToCourse")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@Academicyear", ViewState("Academicyear"))
                cmd.Parameters.AddWithValue("@courseid", Ddlprogram.SelectedValue)
                cmd.Parameters.AddWithValue("@Subjectid", Subjectid)

                cmd.Parameters.AddWithValue("@Subcode", Subjectcode)
                cmd.Parameters.AddWithValue("@Semyear", Ddlsemeser.SelectedItem.Text)
                cmd.Parameters.AddWithValue("@userid", ViewState("Userid"))
                cmd.Parameters.AddWithValue("@sessionid", ViewState("SessionId"))
                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()

            End Using
        End Using
    End Sub

    Protected Sub btnsavesubject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsavesubject.Click
        'Try
        Dim Count As Integer = 0

        For Each row As GridViewRow In GridMapSubject.Rows

            If (TryCast(row.FindControl("btnselect"), CheckBox)).Checked Then
                Dim Subjectid As String = row.Cells(2).Text

                Dim Subjectcode As String = row.Cells(3).Text
                Dim semyear As String = Ddlsemyear.SelectedItem.Text
                Using con As New SqlConnection(constr)
                    Using cmd As New SqlCommand("Select * from Exam_Coursesubject where Academicyear='" & ViewState("Academicyear") & "' " & _
                                                " and Courseid ='" & Ddlprogram.SelectedValue & "' and Subjectid ='" & Subjectid & "' ", con)

                        Dim da As New DataSet
                        Dim ds As New SqlDataAdapter(cmd)
                        ds.Fill(da)
                        Dim i = da.Tables(0).Rows.Count
                        If Ddlsemeser.SelectedItem.Text = "All" Then
                            SaralMsg.Messagebx.Alert(Me, "Please select any Semester")
                        ElseIf i > 0 Then
                            SaralMsg.Messagebx.Alert(Me, "The subject is alreaddy assign to program.")

                        Else
                            Me.Insertsubject(Subjectid, Subjectcode)

                        End If
                    End Using
                End Using
            End If

        Next

        For Each row As GridViewRow In GridMapSubject.Rows
            If (TryCast(row.FindControl("btnselect"), CheckBox)).Checked Then
                Count += 1
            End If

        Next

        If Count = 0 Then
            SaralMsg.Messagebx.Alert(Me, "Select any subject to assign.")
        Else
            SaralMsg.Messagebx.Alert(Me, "Subject is assigned successfully.")
        End If



        For Each row1 As GridViewRow In GridMapSubject.Rows

            TryCast(row1.FindControl("btnselect"), CheckBox).Checked = False

        Next
        FetchGridallsubject()
        'Catch ex As Exception
        '    'Meassage.Messagebx.Alert(Me, "Oops something went wrong.")

        'End Try
    End Sub

    Protected Sub btnAddsubject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddsubject.Click
        PnlProgramsubjectlist.Visible = False
        Ddlsemyear.Visible = False
        backbotton.Visible = False
        PnlallSubjectlis.Visible = True
        Ddlsemeser.Visible = True
        backbotton1.Visible = True

        Ddlsemeser.Items.Clear()
        fillddlsemyear2()
        pnlsec.Visible = True
        fillDdlsection()
        FetchGridallsubject()


        stusearch.Visible = False
        txtsearchforallsubject.Visible = True

    End Sub

    Protected Sub backbotton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton1.Click
        PnlallSubjectlis.Visible = False
        Ddlsemeser.Visible = False
        backbotton1.Visible = False
        pnlsec.Visible = False
        Ddlsection.Items.Clear()
        PnlProgramsubjectlist.Visible = True
        Ddlsemyear.Visible = True
        backbotton.Visible = True

        stusearch.Visible = True
        txtsearchforallsubject.Visible = False


        Ddlsemyear.Items.Clear()

        fillddlsemyear()

        GridsubjectAllsem()
    End Sub

    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        Response.Redirect("Dashboard.aspx?s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u") & "&ay=" & ViewState("ayid"))
    End Sub

    Private Sub fetchcountsubjectsemwise()

        Dim ds As New DataSet
        Using con As New SqlConnection(constr)
            con.Open()
            Dim cmd1 As New SqlCommand(" Select Count(Cs.Subjectid) as 'TotalSubject' from Exam_Coursesubject Cs where Cs.Academicyear='" & ViewState("Academicyear") & "' and Cs.Courseid ='" & ViewState("Courseid") & "' AND Semyear='" & Ddlsemyear.SelectedItem.Text & "' ", con)
            Dim da As New SqlDataAdapter(cmd1)
            cmd1.Connection = con
            da.Fill(ds)
            Dim i = ds.Tables(0).Rows.Count()
            If (i > 0) Then
                lbltotalsub.Text = ds.Tables(0).Rows(0)("TotalSubject").ToString()

            End If
            con.Close()

        End Using

    End Sub

    Protected Sub txtsearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsearch.TextChanged
        If PnlProgramsubjectlist.Visible = True Then
            If Ddlsemyear.SelectedItem.Text = "All" Then
                GridsubjectAllsem()
            Else
                Gridsubjectsemwise()
            End If
        Else
            FetchGridallsubject()
        End If

    End Sub



    Protected Sub gridallsubject_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridallsubject.RowCommand
        If e.CommandName = "SubjectName" Then
            gridallsubject.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gridallsubject.Rows(rowIndex)


            Response.Redirect("SubjectPlan.aspx?rid=" & gridallsubject.SelectedDataKey(1) & "&subjectid=" & gridallsubject.SelectedDataKey(0) & "&acyr=" & ViewState("Academicyear") & "&cid=" & Ddlprogram.SelectedValue & "&s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u") & "&ay=" & ViewState("ayid"))
        End If
        If e.CommandName = "viewassignment" Then

            gridallsubject.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gridallsubject.Rows(rowIndex)
            ViewState("Coursesessionid") = gridallsubject.SelectedDataKey(0)
            Session("Otherid") = row.Cells(2).Text
            Response.Redirect("../Admin/Vb.aspx?rid=" & gridallsubject.SelectedDataKey(1) & "&subjectid=" & gridallsubject.SelectedDataKey(0) & "&acyr=" & ViewState("Academicyear") & "&cid=" & Ddlprogram.SelectedValue & "&s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u") & "&ay=" & ViewState("ayid") & "&Fid=" & gridallsubject.SelectedDataKey(2) & "&Sem=" & row.Cells(6).Text & "&Classesid=" & gridallsubject.SelectedDataKey(3))
        End If


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
                        Me.GridsubjectAllsem()
                        con.Close()
                    End Using
                End Using
            End Using
        End Using
    End Sub
    Private Sub DeleteDepart(ByVal Departid As Object)
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "Delete from Exam_coursesubject where Coursesubid='" & Departid & "'"
                cmd.CommandText = query
                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using
    End Sub

    Protected Sub Ddlsemeser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ddlsemeser.SelectedIndexChanged

    End Sub

    Protected Sub Ddlsection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ddlsection.SelectedIndexChanged
        FetchGridallsubject()
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    Protected Sub Download_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Download.Click
        'Response.ClearContent()
        'Response.AddHeader("content-disposition", "attachment;filename=SubjectList.xls")
        'Response.ContentType = "application/vnd.ms-excel"
        'Dim sw As StringWriter = New StringWriter()
        'Dim htm As HtmlTextWriter = New HtmlTextWriter(sw)
        'gridallsubject.AllowPaging = False

        'gridallsubject.HeaderRow.Cells(1).Visible = False
        'gridallsubject.HeaderRow.Cells(2).Visible = False
        'gridallsubject.RowStyle.HorizontalAlign = HorizontalAlign.Center
        'For Each row As GridViewRow In gridallsubject.Rows

        '    row.Cells(1).Visible = False
        '    row.Cells(2).Visible = False
        'Next

        'gridallsubject.RenderControl(htm)
        'Response.Write(sw.ToString())
        'Response.End()
    End Sub


    Private Sub bind(ByVal sid As String)
        Me.BindGrid1(sid)
    End Sub

    Private Sub BindGrid1(ByVal sid As String)
        If Ddlsemyear.SelectedItem.Text = "All" Then
            ' fetchcountsubjectsemwise()
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand()
                    Dim query As String = "Select Csub.Coursesubid, Csub.Academicyear, Csub.Coursesessionid, Csub.Courseid, Csub.Semyear, Csub.Subjectid, Sub.Subject, " & _
    " Sub.Subjectcode, Sub.Subprefix from Exam_Coursesubject Csub join Exam_Subject Sub on Csub.Subjectid =sub.Subjectid " & _
    "  Where Csub.Academicyear ='" & ViewState("Academicyear") & "' and Csub.Courseid='" & Ddlprogram.SelectedValue & "' and sub.Subjectid='" & sid & "' order by Csub.Semyear"
                    If Not String.IsNullOrEmpty(txtsearch.Text.Trim()) Then
                        query += " and Sub.Subject LIKE '%' + @Subject + '%' "
                        cmd.Parameters.AddWithValue("@Subject", txtsearch.Text.Trim())
                    End If
                    cmd.CommandText = query
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            gridallsubject.DataSource = dt
                            gridallsubject.DataBind()
                        End Using
                    End Using
                End Using
            End Using

        Else

            ' fetchcountsubjectsemwise()

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand()

                    Dim query As String = "Select Csub.Coursesubid, Csub.Academicyear, Csub.Coursesessionid, Csub.Courseid, Csub.Semyear, Csub.Subjectid, Sub.Subject, " & _
    " Sub.Subjectcode, Sub.Subprefix from Exam_Coursesubject Csub join Exam_Subject Sub on Csub.Subjectid =sub.Subjectid " & _
    "   Where Csub.Academicyear ='" & ViewState("Academicyear") & "' and Csub.Courseid='" & Ddlprogram.SelectedValue & "' And Csub.Semyear ='" & Ddlsemyear.SelectedItem.Text & "' and sub.Subjectid='" & sid & "' "
                    If Not String.IsNullOrEmpty(txtsearch.Text.Trim()) Then
                        query += " and Sub.Subject LIKE '%' + @Subject + '%'"
                        cmd.Parameters.AddWithValue("@Subject", txtsearch.Text.Trim())
                    End If
                    cmd.CommandText = query

                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            gridallsubject.DataSource = dt
                            gridallsubject.DataBind()
                        End Using
                    End Using
                End Using
            End Using

        End If
    End Sub

    Private Sub bindsubject(ByVal sid As String)
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim sql As String = "Select Subjectid, Subject, Subjectcode, Subprefix, Subtype from Exam_Subject where Subjectid not in (Select Cs.Subjectid from Exam_Coursesubject Cs where Cs.Academicyear='" & Request.QueryString("acyr") & "' and Cs.Courseid ='" & Ddlprogram.SelectedValue & "' ) and SUbjectid = '" & sid & "' "
                If Not String.IsNullOrEmpty(txtsearch.Text.Trim()) Then
                    sql += " where Subject LIKE '%' + @Subject + '%'"
                    cmd.Parameters.AddWithValue("@Subject", txtsearch.Text.Trim())
                End If
                cmd.CommandText = sql

                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        GridMapSubject.DataSource = dt
                        GridMapSubject.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Protected Sub btnelectsub_Click(sender As Object, e As System.EventArgs) Handles btnelectsub.Click
        If Ddlsemyear.SelectedItem.Text = "All" Then
            SaralMsg.Messagebx.Alert(Me, "Select sem")
        Else
            Response.Redirect("~/Examinationjune/Electivesub.aspx?s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u") & "&ay=" & ViewState("ayid") & "&rid=" & Request.QueryString("rid") & "&acyr=" & Request.QueryString("acyr") & "&Sem=" & Ddlsemyear.SelectedValue)

        End If
    End Sub
End Class
