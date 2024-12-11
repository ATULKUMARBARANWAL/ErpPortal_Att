Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Partial Class Examinationjune_Studentelectivesubjectlist
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
    Private cmd As dbnew = New dbnew()

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

            'fillddlsemyear()

            GridsubjectAllsem()
            FetchGridallsubject()
            lbltotalsub.Text = Request.QueryString("acyr")
            'ddl.Items.FindByValue(Request.QueryString("Sem")).Selected = True
            ' Ddlsemeser.Items.FindByValue(Request.QueryString("Sem")).Selected = True
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
            Using cmd As New SqlCommand("select s.Subjectid, s.Subject,cs.semyear,cs.Coursesubid, s.Subjectcode, s.Subprefix, s.Subtype,cs.iselective from Exam_coursesubject cs join Exam_subject s on cs.subjectid = s.subjectid " & _
" where cs.courseid = '" & ViewState("Courseid") & "'" & _
"  and cs.Academicyear ='" & ViewState("Academicyear") & "' and cs.semyear = '" & Request.QueryString("Sem") & "' and cs.iselective = '1' order by cs.Semyear")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        Ddlprogram.DataSource = dt
                        Ddlprogram.DataTextField = "Subject"
                        Ddlprogram.DataValueField = "Subjectid"
                        Ddlprogram.DataBind()
                        '  Dim Year As Integer
                        ' Year = Convert.ToInt32(Now.ToString("yyyy"))
                        labeldata()
                        Ddlprogram.Items.FindByValue(Request.QueryString("Subjectid")).Selected = True

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
            Dim cmd1 As New SqlCommand("select courseid,Coursecode from Exam_Course where courseid='" & Request.QueryString("rid") & "' ", con)
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

        Ddlsection.Items.Clear()


      
    End Sub
   

   

  

    Private Sub GridsubjectAllsem()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "select s.admissionno,ss.studentid,ss.subjectid,s.Student,s.studentid,ss.sem,s.courseid,s.Fathername from studentsubject ss " & _
" join Student s on ss.studentid = s.studentid and ss.courseid = s.courseid where s.courseid = '" & Request.QueryString("rid") & "' and ss.Subjectid = '" & Ddlprogram.SelectedValue & "' " & _
"    and ss.Sem = '" & Request.QueryString("Sem") & "'  order by Sem"
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





    



    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        Response.Redirect("Dashboard.aspx?s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u") & "&ay=" & ViewState("ayid"))
    End Sub

    Private Sub fetchcountsubjectsemwise()

        Dim ds As New DataSet
        Using con As New SqlConnection(constr)
            con.Open()
            Dim cmd1 As New SqlCommand(" Select Count(Cs.Subjectid) as 'TotalSubject' from Exam_Coursesubject Cs where Cs.Academicyear='" & ViewState("Academicyear") & "' and Cs.Courseid ='" & ViewState("Courseid") & "'  ", con)
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

   



    Protected Sub gridallsubject_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridallsubject.RowCommand
        If e.CommandName = "Studentlist" Then
            gridallsubject.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gridallsubject.Rows(rowIndex)
            ViewState("Academicyear") = ddlacademicyear.SelectedItem.Text
            ViewState("Coursesessionid") = row.Cells(2).Text
            Session("Courseid") = row.Cells(2).Text
            Response.Redirect("../Studentelectivesubjectlist.aspx?rid=" & Session("Courseid") & "&acyr=" & ddlacademicyear.SelectedItem.Text & "&e=" & ViewState("evenodd") & "&u=" & ViewState("Userid") & "&s=" & Request.QueryString("s") & "&ay=" & ViewState("ayid"))
        End If
        If e.CommandName = "Delete" Then
            gridallsubject.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gridallsubject.Rows(rowIndex)
            ViewState("Coursesubid") = gridallsubject.SelectedDataKey(2)

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand()
                    Dim query As String = "Select * from Exam_Subjectplan where Coursesubid='" & ViewState("Coursesubid") & "'"
                    cmd.CommandText = query
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            If dt.Rows.Count Then
                                ViewState("facultyid") = dt.Rows(0)("facultyid").ToString
                                If ViewState("facultyid") = "0" Then
                                    DeleteDepart(ViewState("Coursesubid"))
                                    SaralMsg.Messagebx.Alert(Me, "Deleted Successfully")
                                    GridsubjectAllsem()
                                Else
                                    SaralMsg.Messagebx.Alert(Me, "Faculty Assigned in this subject")

                                End If
                                Exit Sub
                            Else

                                DeleteDepart(ViewState("Coursesubid"))
                                SaralMsg.Messagebx.Alert(Me, "Deleted Successfully")
                                GridsubjectAllsem()
                            End If
                        End Using
                    End Using
                End Using
            End Using

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


    

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub

    Protected Sub Download_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Download.Click
        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment;filename=SubjectList.xls")
        Response.ContentType = "application/vnd.ms-excel"
        Dim sw As StringWriter = New StringWriter()
        Dim htm As HtmlTextWriter = New HtmlTextWriter(sw)
        gridallsubject.AllowPaging = False

        gridallsubject.HeaderRow.Cells(1).Visible = False
        gridallsubject.HeaderRow.Cells(2).Visible = False
        gridallsubject.RowStyle.HorizontalAlign = HorizontalAlign.Center
        For Each row As GridViewRow In gridallsubject.Rows

            row.Cells(1).Visible = False
            row.Cells(2).Visible = False
        Next

        gridallsubject.RenderControl(htm)
        Response.Write(sw.ToString())
        Response.End()
    End Sub


    Private Sub bind(ByVal sid As String)
        Me.BindGrid1(sid)
    End Sub

    Private Sub BindGrid1(ByVal sid As String)

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



        ' fetchcountsubjectsemwise()

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()

                Dim query As String = "Select Csub.Coursesubid, Csub.Academicyear, Csub.Coursesessionid, Csub.Courseid, Csub.Semyear, Csub.Subjectid, Sub.Subject, " & _
" Sub.Subjectcode, Sub.Subprefix from Exam_Coursesubject Csub join Exam_Subject Sub on Csub.Subjectid =sub.Subjectid " & _
"   Where Csub.Academicyear ='" & ViewState("Academicyear") & "' and Csub.Courseid='" & Ddlprogram.SelectedValue & "'  and sub.Subjectid='" & sid & "' "
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


    End Sub

   

    Protected Sub btnAddsubject_Click(sender As Object, e As System.EventArgs) Handles btnAddsubject.Click
        PnlProgramsubjectlist.Visible = False

        backbotton.Visible = False
        PnlallSubjectlis.Visible = True

        backbotton1.Visible = True



        pnlsec.Visible = True

        FetchGridallsubject()


        stusearch.Visible = False
        txtsearchforallsubject.Visible = True
    End Sub
    Private Sub FetchGridallsubject()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                Dim query As String = "select * from student  " & _
" where courseid = '" & Request.QueryString("rid") & "'" & _
"    and Sem = '" & Request.QueryString("Sem") & "' and studentid not in (Select studentid from studentsubject where courseid = '" & Request.QueryString("rid") & "' and Sem = '" & Request.QueryString("Sem") & "' and subjectid = '" & Ddlprogram.SelectedValue & "')  and isstruckoff = '0' or isstruckoff is null  order by Sem"
               
                cmd.CommandText = query
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

    Protected Sub btnsavesubject_Click(sender As Object, e As System.EventArgs) Handles btnsavesubject.Click
        Dim Count As Integer = 0

        For Each row As GridViewRow In GridMapSubject.Rows

            If (TryCast(row.FindControl("btnselect"), CheckBox)).Checked Then
                Dim Subjectid As String = Ddlprogram.SelectedValue
                ' Dim ddlsubtype As DropDownList = CType(row.FindControl("ddlsubtype"), DropDownList)
                Dim Studentid As String = row.Cells(2).Text
                Dim Courseid As String = Request.QueryString("rid")
                Dim semyear As String = Request.QueryString("Sem")
                Dim userid As String = Request.QueryString("u")
                Dim sql As String = ""
                '  Dim cmd As New SqlCommand With {.Connection = Hari.Utility.dbutility.Con}
                sql = "Insert into     Studentsubject(Sessionid,Studentid,Courseid,sem,Subjectid,userid) " & _
    "Values( '" & Request.QueryString("s") & "','" & Studentid & "','" & Courseid & "', '" & semyear & "', '" & Subjectid & "', '" & userid & "'  )"

                cmd.execSQL(sql)
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
        GridsubjectAllsem()
        PnlProgramsubjectlist.Visible = True
        PnlallSubjectlis.Visible = False
        pnlsec.Visible = False
    End Sub
End Class

