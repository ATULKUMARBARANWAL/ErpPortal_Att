Imports System.Data
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient
Partial Class TESTFILES_ProgramSubjectlist
    Inherits System.Web.UI.Page
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Me.labeldata()
            Me.BindGridAll()
            Me.BindGridAll1()
            'Me.BindSubject()
            Me.fetchsubjectname()
            Me.fillddlsemyear()
            Me.fetchcountsubject()
            Me.fillddlsemyearadd()

            Lblcoursename.Text = Request.QueryString("rid")
            'Lblcoursecode.Text = Session("Otherid")
            'Me.SetInitialRow()
        End If

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
                ViewState("a") = ds.Tables(0).Rows(0)("courseid").ToString()
                Lblcoursecode.Text = ds.Tables(0).Rows(0)("Coursecode").ToString()
            End If
            con.Close()

        End Using

    End Sub
    Private Sub BindGridAll()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Csub.Coursesubid, Csub.Academicyear, Csub.Coursesessionid, Csub.Courseid, Csub.Semyear, Csub.Subjectid, Subp.Subplanid,Sub.Subject, " & _
" Sub.Subjectcode, Sub.Subprefix, subp.Totalunit, subp.Totallecture  from Exam_Coursesubject Csub join Exam_Subject Sub on Csub.Subjectid =sub.Subjectid join " & _
" Exam_SubjectPlan Subp on Csub.Coursesubid =Subp.Coursesubid join Exam_Course cp on cp.Courseid=Csub.Courseid " & _
" Where Csub.Academicyear ='2021' and Csub.Coursesessionid='1' " & _
" and Csub.Courseid='5' and Csub.Semyear='1' and cp.courseid='" & ViewState("a") & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        grdSubplan.DataSource = dt
                        grdSubplan.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub BindGridSemwise()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Csub.Coursesubid, Csub.Academicyear, Csub.Coursesessionid, Csub.Courseid, Csub.Semyear, Csub.Subjectid, Subp.Subplanid,Sub.Subject, " & _
" Sub.Subjectcode, Sub.Subprefix, subp.Totalunit, subp.Totallecture  from Exam_Coursesubject Csub join Exam_Subject Sub on Csub.Subjectid =sub.Subjectid join " & _
" Exam_SubjectPlan Subp on Csub.Coursesubid =Subp.Coursesubid Where Csub.Academicyear ='2021' and Csub.Coursesessionid='1' " & _
" and Csub.Courseid='5' and Csub.Semyear='" & Ddlsemyear.SelectedValue & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        grdSubplan.DataSource = dt
                        grdSubplan.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub
    Private Sub fetchcountsubject()
        Dim ds As New DataSet
        Using con As New SqlConnection(constr)
            con.Open()
            Dim cmd1 As New SqlCommand(" Select Count(Cs.Subjectid) as 'TotalSubject' from Exam_Coursesubject Cs where Cs.Academicyear='2021' and Cs.Courseid ='5' ", con)
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
    Private Sub fetchsubjectname()
        Dim ds As New dataset
        Using con As New sqlconnection(constr)
            con.open()
            Dim cmd1 As New SqlCommand(" Select Cs.Academicyear, Cs.Courseid, C.Course, C.Coursecode, Cs.Duration, Cs.Coursetype, Cs.courselevel, " & _
" Case When Cs.Coursetype like '%Year%' then Cs.Duration*1 when Cs.Coursetype like '%Sem%' then Cs.Duration*2 end as 'Semyear' from Exam_CourseSession Cs " & _
" Join Exam_Course C on Cs.Courseid=C.Courseid Where Academicyear='2021' And Cs.Courseid='5'", con)
            Dim da As New sqldataadapter(cmd1)
            cmd1.connection = con
            da.fill(ds)
            Dim i = ds.tables(0).rows.count()
            If (i > 0) Then
                Lblcoursename.Text = ds.Tables(0).Rows(0)("Course").ToString()
                Lblcoursecode.Text = ds.Tables(0).Rows(0)("Coursecode").ToString()
                Lblcourseid.Text = ds.Tables(0).Rows(0)("Courseid").ToString()
                Lblsemyear.Text = ds.Tables(0).Rows(0)("Coursetype").ToString()
                Lblsemyearadd.Text = ds.Tables(0).Rows(0)("Coursetype").ToString()
            End If
            con.close()

        End Using

    End Sub
    Protected Sub Ddlsemyear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ddlsemyear.SelectedIndexChanged
        If Ddlsemyear.SelectedItem.Text = "All" Then
            BindGridAll()
        Else
            BindGridSemwise()
        End If
    End Sub
    Private Sub fillddlsemyear()
        fetchsubjectname()
        Try

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select Coursesessionid, Academicyear, Courseid, case when Coursetype Like '%sem%' then Duration*2 " & _
    " when Coursetype like '%year%' then Duration*1 when Coursetype like '%quart%' then Duration*4 end as 'Totalsem', " & _
     " Duration, Coursetype from Exam_CourseSession where  Academicyear ='2023' and Courseid = '" & Lblcourseid.Text & "' ")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            Ddlsemyear.DataSource = dt
                            Dim totalsem As String = dt.Rows(0)("Totalsem").ToString()
                            Dim i As Integer
                            For i = 1 To totalsem
                                Ddlsemyear.Items.Add(New ListItem(i.ToString(), i.ToString()))
                            Next
                            Ddlsemyear.Items.Insert(0, New ListItem("All", ""))
                        End Using
                    End Using
                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Private Sub fillddlsemyearadd()
        fetchsubjectname()
        Try

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select Coursesessionid, Academicyear, Courseid, case when Coursetype Like '%sem%' then Duration*2 " & _
    " when Coursetype like '%year%' then Duration*1 when Coursetype like '%quart%' then Duration*4 end as 'Totalsem', " & _
     " Duration, Coursetype from Exam_CourseSession where  Academicyear ='2023' and Courseid = '" & Lblcourseid.Text & "' ")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            ddlSemyearsub.DataSource = dt
                            Dim totalsem As String = dt.Rows(0)("Totalsem").ToString()
                            Dim i As Integer
                            For i = 1 To totalsem
                                ddlSemyearsub.Items.Add(New ListItem(i.ToString(), i.ToString()))

                            Next

                            ddlSemyearsub.Items.Insert(0, New ListItem("All", ""))
                        End Using
                    End Using
                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub
    Private Sub BindSubject()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Csub.Coursesubid, Csub.Academicyear, Csub.Coursesessionid, Csub.Courseid, Csub.Semyear, Csub.Subjectid, Subp.Subplanid,Sub.Subject, " & _
" Sub.Subjectcode, Sub.Subprefix, subp.Totalunit, subp.Totallecture  from Exam_Coursesubject Csub join Exam_Subject Sub on Csub.Subjectid =sub.Subjectid join" & _
" Exam_SubjectPlan Subp on Csub.Coursesubid =Subp.Coursesubid Where Csub.Courseid='" & ViewState("a") & "' and Csub.Semyear='" & ddlSemyearsub.SelectedValue & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        grdSubjectList.DataSource = dt
                        grdSubjectList.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub
    Protected Sub btnAddProgram_Click(sender As Object, e As System.EventArgs) Handles btnAddProgram.Click
        PanelSubjectList.Visible = True
        PanelCourseWise.Visible = False
        PanelUnitNameDescription.Visible = False
    End Sub
    Protected Sub backbotton_Click(sender As Object, e As System.EventArgs) Handles backbotton.Click
        PanelCourseWise.Visible = True
        PanelSubjectList.Visible = False
        PanelUnitNameDescription.Visible = False
        Response.Redirect("Dashboard.aspx")
    End Sub
    Protected Sub backtocourseSubject_Click(sender As Object, e As System.EventArgs) Handles backtocourseSubject.Click
        PanelCourseWise.Visible = True
        PanelUnitNameDescription.Visible = False
        PanelSubjectList.Visible = False
    End Sub
    Protected Sub backtoprogram_Click(sender As Object, e As System.EventArgs) Handles backtoprogram.Click
        PanelCourseWise.Visible = True
        PanelSubjectList.Visible = False
        PanelUnitNameDescription.Visible = False
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As System.EventArgs) Handles btnAdd.Click
        Dim Subjectid As Integer = 0
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Addsubject", con)
                cmd.CommandType = CommandType.StoredProcedure
                Using sda As New SqlDataAdapter()
                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    cmd.Parameters.AddWithValue("@Dated", "")
                    cmd.Parameters.AddWithValue("@Subject ", txtsub.Text)
                    cmd.Parameters.AddWithValue("@Subjectcode ", txtcode.Text)
                    cmd.Parameters.AddWithValue("@Subprefix ", txtprefix.Text)
                    cmd.Connection = con
                    con.Open()
                    Subjectid = cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using

            Select Case Subjectid
                Case -1
                    MsgBox(Me, "Subject Name Already Exist")
                    Exit Select

                Case -2
                    MsgBox(Me, "Subject Code Already Exist")
                    Exit Select

                Case -3
                    MsgBox(Me, "Prefix Already Exist")
                    Exit Select

                Case Else
                    MsgBox(Me, "Subject Add successful")
                    Exit Select
            End Select
            txtsub.Text = ""
            txtcode.Text = ""
            txtprefix.Text = ""
        End Using

    End Sub

    Protected Sub grdSubplan_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdSubplan.RowCommand
        'PanelUnitNameDescription.Visible = True
        'PanelCourseWise.Visible = False
        If e.CommandName = "SubjectName" Then
            grdSubplan.SelectedIndex = e.CommandArgument
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = grdSubplan.Rows(rowIndex)
            ViewState("subid") = row.Cells(1).Text
            ViewState("Subjectname") = row.Cells(2).Text
            ViewState("Subcode") = row.Cells(4).Text
            Lblsubid.Text = ViewState("subid")
            LblSubname.Text = ViewState("Subjectname")

            Lblsubcode.Text = ViewState("Subcode")
            BindUnitName()
            'ClientScript.RisterStartupScript(Me.GetType(), "Popup", "ShowPopup();", True)
            PanelUnitNameDescription.Visible = True
            PanelCourseWise.Visible = False
        End If

    End Sub

    
    Private Sub BindUnitName()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(" Select Su.Subunitid, su.Academicyear,Cs.Coursesubid, Cs.Courseid, Cs.Semyear, Cs.Subjectid, sub.Subject, Su.UnitName, " & _
" Su.Description, Su.Timeduration  from Exam_SubjectUnit Su join Exam_Coursesubject Cs on Su.Coursesubid =Cs.Coursesubid " & _
" join Exam_Subject sub on Cs.Subjectid =sub.Subjectid Where Su.Academicyear ='2021' and Cs.Courseid='5' and Cs.Semyear='1' " & _
 " and Cs.Subjectid='" & ViewState("subid") & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        GridUnitDec.DataSource = dt
                        GridUnitDec.DataBind()
                    End Using
                End Using
            End Using
        End Using
        'lblName.Text = "Name is : <b> " & name & "</b>"
    End Sub

   
    Protected Sub grdSubplan_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSubplan.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim ddlfaculties As DropDownList = CType(e.Row.FindControl("ddlfaculty"), DropDownList)

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand(" Select EmployeeID, Employee from P_Employee Where Employee is not null order by Employee ")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            ddlfaculties.DataSource = dt
                            ddlfaculties.DataTextField = "Employee"
                            ddlfaculties.DataValueField = "EmployeeID"
                            ddlfaculties.DataBind()
                            ddlfaculties.Items.Insert(0, New ListItem("Select", ""))
                        End Using
                    End Using
                End Using
            End Using

           
        End If

    End Sub
   
    Protected Sub LinkButton2_Click(sender As Object, e As System.EventArgs) Handles LinkButton2.Click
        'Me.SetInitialRow()
        AddNewRowToGrid()
    End Sub
    Private Sub SetInitialRow()
        Dim dt As New DataTable()
        Dim dr As DataRow = Nothing
        dt.Columns.Add(New DataColumn("RowNumber", GetType(String)))
        dt.Columns.Add(New DataColumn("UnitName", GetType(String)))
        dt.Columns.Add(New DataColumn("Descripption", GetType(String)))
        dt.Columns.Add(New DataColumn("Lecture duration", GetType(String)))
        dr = dt.NewRow()
        dr("RowNumber") = 1
        dr("RowNumber") = String.Empty
        dr("Descripption") = String.Empty
        dr("Lecture duration") = String.Empty
        dt.Rows.Add(dr)
        ViewState("CurrentTable") = dt
        'ViewState("CurrentTable" & 1) = dt
        GridUnitDec.DataSource = dt
        GridUnitDec.DataBind()

    End Sub




    Private Sub AddNewRowToGrid()
        Dim sc As StringCollection = New StringCollection()
        Dim rowIndex As Integer = 0
        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            Dim drCurrentRow As DataRow = Nothing
            If dtCurrentTable.Rows.Count > 0 Then
                For i As Integer = 1 To dtCurrentTable.Rows.Count
                    Dim box1 As TextBox = DirectCast(GridUnitDec.Rows(rowIndex).Cells(1).FindControl("txt4"), TextBox)
                    Dim box2 As TextBox = DirectCast(GridUnitDec.Rows(rowIndex).Cells(2).FindControl("txt5"), TextBox)
                    Dim box3 As TextBox = DirectCast(GridUnitDec.Rows(rowIndex).Cells(3).FindControl("txt6"), TextBox)

                    drCurrentRow = dtCurrentTable.NewRow()
                    drCurrentRow("RowNumber") = i + 1
                    dtCurrentTable.Rows(i - 1)("RowNumber") = box1.Text
                    dtCurrentTable.Rows(i - 1)("Descripption") = box2.Text
                    dtCurrentTable.Rows(i - 1)("Lecture duration") = box3.Text
                    sc.Add(box1.Text + "," + box2.Text + "," + box3.Text)
                    rowIndex += 1
                    InsertRecords(sc)
                Next

                dtCurrentTable.Rows.Add(drCurrentRow)
                ViewState("CurrentTable") = dtCurrentTable
                GridUnitDec.DataSource = dtCurrentTable
                GridUnitDec.DataBind()
            End If
        Else

            Response.Write("ViewState is null")
        End If

        'SetPreviousData()
    End Sub


    Private Sub SetPreviousData()
        Dim rowIndex As Integer = 0

        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim box1 As TextBox = DirectCast(GridUnitDec.Rows(rowIndex).Cells(1).FindControl("txt4"), TextBox)
                    Dim box2 As TextBox = DirectCast(GridUnitDec.Rows(rowIndex).Cells(2).FindControl("txt5"), TextBox)
                    Dim box3 As TextBox = DirectCast(GridUnitDec.Rows(rowIndex).Cells(3).FindControl("txt6"), TextBox)
                    box1.Text = dt.Rows(i)("RowNumber").ToString()
                    box2.Text = dt.Rows(i)("Descripption").ToString()
                    box3.Text = dt.Rows(i)("Lecture duration").ToString()

                    rowIndex += 1
                Next
            End If
        End If
    End Sub
    Private Function InsertRecords(ByVal sc As StringCollection) As Integer
        Dim con As SqlConnection = New SqlConnection(constr)
        Dim sb As StringBuilder = New StringBuilder(String.Empty)

        Dim splitItems As String() = {}


        For Each item In sc

            Dim sqlStatement As String = "INSERT INTO Exam_SubjectUnit (UnitName,Description,Timeduration) VALUES"

            If (item.Contains(",")) Then



                splitItems = item.Split(",".ToCharArray())

                sb.AppendFormat("{0}('{1}','{2}','{3}'); ", sqlStatement, splitItems(0), splitItems(1), splitItems(2))

            End If



        Next

        con.Open()

        Dim cmd As SqlCommand = New SqlCommand(sb.ToString(), con)

        cmd.CommandType = CommandType.Text

        cmd.ExecuteNonQuery()


    End Function


    Protected Sub ddlSemyearsub_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSemyearsub.SelectedIndexChanged
        If ddlSemyearsub.SelectedItem.Text = "All" Then
            BindGridAll1()
        Else
            BindGridSemwise1()
        End If
    End Sub

    Private Sub BindGridAll1()
         Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select Csub.Coursesubid, Csub.Academicyear, Csub.Coursesessionid, Csub.Courseid, Csub.Semyear, Csub.Subjectid, Subp.Subplanid,Sub.Subject, " & _
" Sub.Subjectcode, Sub.Subprefix, subp.Totalunit, subp.Totallecture  from Exam_Coursesubject Csub join Exam_Subject Sub on Csub.Subjectid =sub.Subjectid join" & _
" Exam_SubjectPlan Subp on Csub.Coursesubid =Subp.Coursesubid Where  Csub.Courseid='" & ViewState("a") & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        grdSubjectList.DataSource = dt
                        grdSubjectList.DataBind()
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub BindGridSemwise1()
        '        Using con As New SqlConnection(constr)
        '            Using cmd As New SqlCommand("Select Csub.Coursesubid, Csub.Academicyear, Csub.Coursesessionid, Csub.Courseid, Csub.Semyear, Csub.Subjectid, Subp.Subplanid,Sub.Subject, " & _
        '" Sub.Subjectcode, Sub.Subprefix, subp.Totalunit, subp.Totallecture  from Exam_Coursesubject Csub join Exam_Subject Sub on Csub.Subjectid =sub.Subjectid join " & _
        '" Exam_SubjectPlan Subp on Csub.Coursesubid =Subp.Coursesubid Where Csub.Academicyear ='2021' and Csub.Coursesessionid='1' " & _
        '" and Csub.Courseid='4' and Csub.Semyear='" & ddlSemyearsub.SelectedValue & "'")
        '                Using sda As New SqlDataAdapter()
        '                    cmd.Connection = con
        '                    sda.SelectCommand = cmd
        '                    Using dt As New DataTable()
        '                        sda.Fill(dt)
        '                        grdSubjectList.DataSource = dt
        '                        grdSubjectList.DataBind()
        '                    End Using
        '                End Using
        '            End Using
        '        End Using
        Me.BindSubject()
    End Sub

   
End Class
