'Design And Developed By Avaneesh Yadav
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data

Partial Class Examinationjune_Marksheet
    Inherits System.Web.UI.Page
       Dim sql As String = ""
    Private cmd As New dbnew()
    Private saralMastercls As saral.Mastercls = New saral.Mastercls()
    Private saralstudent As saral.student = New saral.student()

    Public tb_rowCols As String
    Public tb_header_Rows As Integer
    Public tableValue As String


    Dim maindt As DataTable
    Public tb_rowCols1 As String
    Public tb_header_Rows1 As Integer
    Public tableValue1 As String
    Dim maindt1 As DataTable
    Dim totalamt As Integer
    Public tabletotal As String


    Private cmdsql As New dbnew()
    Dim dt1 As DataTable

    Dim strHTMLBuilder As StringBuilder = New StringBuilder()
    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            ViewState("courseexamid") = Request.QueryString("courseexamid")
            ViewState("courseid") = Request.QueryString("rid")
            ViewState("Academicyear") = Request.QueryString("acyr")
            ViewState("Sessionid") = Request.QueryString("s")
            ViewState("ayid") = Request.QueryString("ay")
            BindCourseExamdropdown()
            If ViewState("courseexamid") <> "" Then
                ddlExamN.Items.FindByValue(ViewState("courseexamid")).Selected = True

            End If

            fetchddlProgram()
            If ViewState("courseid") <> "" Then
                ddlAcademicyear.Items.FindByValue(ViewState("courseid")).Selected = True

            End If

            labeldata()
            Me.BindGrid()
            ViewState("StudentID") = Request.QueryString("StudentID")
        End If
    End Sub
    Private Sub BindCourseExamdropdown()
        Dim query As String = ("Select * from Exam_CourseExam where Sessionid='" & ViewState("Sessionid") & "' and Ayid='" & ViewState("ayid") & "'")
        BindDropDownList1(ddlExamN, query, "ExamName", "CourseExamid", "Select Exam Name")

    End Sub
    Private Sub BindDropDownList1(ByVal ddl1 As DropDownList, ByVal query As String, ByVal text As String, ByVal value As String, ByVal defaultText As String)

        Dim cmd As New SqlCommand(query)
        Using con As New SqlConnection(constr)
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                con.Open()
                ddl1.DataSource = cmd.ExecuteReader()
                ddl1.DataTextField = text
                ddl1.DataValueField = value
                ddl1.DataBind()
                con.Close()
            End Using
        End Using
        ddl1.Items.Insert(0, New ListItem(defaultText, "0"))
    End Sub

    Private Sub fetchddlProgram()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Select C.Course, Ce.* from Exam_CourseExam Ce join Exam_Course C on Ce.Courseid=C.Courseid where Ce.CourseExamid='" & ddlExamN.SelectedValue & "' and Ce.Ayid='" & ViewState("ayid") & "' and Ce.SessionId='" & ViewState("Sessionid") & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        ddlAcademicyear.DataSource = dt
                        ddlAcademicyear.DataTextField = "Course"
                        ddlAcademicyear.DataValueField = "Courseid"
                        ddlAcademicyear.DataBind()

                    End Using
                End Using
            End Using
        End Using
        ddlAcademicyear.Items.Insert(0, New ListItem("Select Program", "0"))
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
                ViewState("Courseid") = Session("Courseid")
                ViewState("Coursecode") = ds.Tables(0).Rows(0)("Coursecode").ToString()
            End If
            con.Close()

        End Using

    End Sub
    Public Sub Change_student(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chk As CheckBox = CType(grdstudent.HeaderRow.FindControl("chkall"), CheckBox)
        If CType(grdstudent.HeaderRow.FindControl("chkall"), CheckBox).Checked Then
            For Each rw As GridViewRow In grdstudent.Rows
                CType(rw.FindControl("chkselect"), CheckBox).Checked = True
            Next
        Else
            For Each rw As GridViewRow In grdstudent.Rows
                CType(rw.FindControl("chkselect"), CheckBox).Checked = False
            Next
        End If
    End Sub
    Private Sub BindGrid()
        If ddlExamN.SelectedValue = "0" Or ViewState("Sessionid") = "" Or ViewState("ayid") = "" Then
        Else
            Using con As New SqlConnection(constr)
                'Using cmd As New SqlCommand("select s.StudentId,s.AdmissionNo, s.Student,Convert(nvarchar, s.DOB, 103) as DOB ,ce.Course, s.Roll_no,s.EnrollmentNo,s.FatherName,s.Mobile from Student s left join Exam_Course ce on ce.CourseID=s.CourseID Left Join Exam_CourseExam ece on ce.Courseid = ece.Courseid where ece.ExamName = '" & Session("ExamName") & "' and ece.Courseid='" & Session("Courseid") & "'and ece.Semyear='" & Session("Semyear") & "'", con)
                Using cmd As New SqlCommand("Select C.Course, Sy.StudentID, St.AdmissionNo, St.Student, St.EnrollmentNo, St.Roll_No,St.FatherName, St.Gender , " & _
    " St.Email,St.Mobile ,CONVERT(varchar,St.DOB,107) as Dateofbirth,ce.* from Exam_CourseExam ce join StudentYear sy on ce.Courseid=sy.Courseid " & _
    " and ce.Sessionid=Sy.SessionID and ce.Ayid=Sy.ayid and ce.Semyear=Sy.SEm " & _
    " join Student st on sy.StudentID=st.StudentID join Exam_Course C on Ce.Courseid=C.Courseid " & _
    " where Ce.CourseExamid='" & ddlExamN.SelectedValue & "' and Ce.Sessionid='" & ViewState("Sessionid") & "' and Ce.ayid='" & ViewState("ayid") & "' and St.IsStruckOff='0'  and  " & _
   " Sy.StudentID in (Select Studentid from Exam_Examformstudent where Courseexamid='" & ddlExamN.SelectedValue & "' and Isexamformverified='1')", con)
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            grdstudent.DataSource = dt
                            grdstudent.DataBind()
                            If dt.Rows.Count > 0 Then
                                Dim courseid As String = dt.Rows(0)("Courseid").ToString
                                ddlAcademicyear.Items.FindByValue(courseid).Selected = True
                            End If
                        End Using
                    End Using
                End Using
            End Using
        End If

    End Sub

    Protected Sub btnmarksheet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnmarksheet.Click
        pnlmain.Visible = False

        pnlprint.Visible = True


        For Each itm As GridViewRow In grdstudent.Rows
            If CType(itm.FindControl("chkselect"), CheckBox).Checked = True Then

                '  printnewdmc(grdsubject.DataKeys(itm.RowIndex).Values(0))
                printnewdmc(grdstudent.DataKeys(itm.RowIndex).Values(0))
            End If
        Next
    End Sub
    Sub printnewdmc(ByVal sid As String)

        Dim aa As SqlParameter() = {New SqlParameter("@q", "Rawa"), New SqlParameter("@session", ViewState("Sessionid")), New SqlParameter("@ayid", ViewState("ayid")), New SqlParameter("@courseexamid", ddlExamN.SelectedValue.ToString), New SqlParameter("@sid", sid)}
        Dim dt As DataTable = cmd.getDataTable("[dbo].[ReportMarksheetSheet]", aa, "dt")

        If dt.Rows.Count > 0 Then
        Else
            SaralMsg.Messagebx.Alert(Me, "no records found")
            Exit Sub
        End If

        strHTMLBuilder.Append("<table  align=""center""   width=""100%"" style="" color: #000000; font-family: calibri; font-size: 19px"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th  width=""30%"" rowspan=""6"" align=""center"" >")
        strHTMLBuilder.Append("<img src=""../img/collegelogo.jpg""  style=""  height=""100"" width=""100""  >")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")

        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size: 23px"" width=""70%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("" & dt.Rows(0)("Institue").ToString & " ")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size: 19px""  width=""70%""   text-align=""center"" >")
        strHTMLBuilder.Append("Statement of Marks")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")



        'strHTMLBuilder.Append("<tr>")
        'strHTMLBuilder.Append("<th style=""font-size 16px"" width=""70%"" colspan=""2"" align=""center"" >")
        'strHTMLBuilder.Append("" & dt.Rows(0)("Course").ToString & "SE ")
        'strHTMLBuilder.Append("</th>")
        'strHTMLBuilder.Append("</tr>")


        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th style=""font-size 16px"" width=""70%"" colspan=""2"" align=""center"" >")
        strHTMLBuilder.Append("" & dt.Rows(0)("Course").ToString & " Exam " & dt.Rows(0)("ExamName").ToString & " ")
        strHTMLBuilder.Append("</th>")
        strHTMLBuilder.Append("</tr>")


        'strHTMLBuilder.Append("<tr>")
        'strHTMLBuilder.Append("<th style=""font-size 16px"" width=""70%"" colspan=""2"" align=""center"" >")
        'strHTMLBuilder.Append("Examination " & dt.Rows(0)("ExamName").ToString & " sem " & dt.Rows(0)("sem").ToString & "  ")
        'strHTMLBuilder.Append("</th>")
        'strHTMLBuilder.Append("</tr>")




        strHTMLBuilder.Append("</table>")





        strHTMLBuilder.Append("<table  width=""100%""  style="" color: #000000; font-family: Tahoma; font-size: 18px"" cellpadding=""5"" cellspacing=""0"" >")




        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<tD    width=""120px"" align=""left"" >")
        strHTMLBuilder.Append("ROLL NO :")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("<td  width=""200px"" align=""left"" >")
        strHTMLBuilder.Append("" & dt.Rows(0)("Roll_no").ToString & "")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("<td  width=""120px""  align=""left"" >")
        strHTMLBuilder.Append("CANDIDATE NAME :")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("<td  width=""200px"" align=""left"" >")
        strHTMLBuilder.Append("" & dt.Rows(0)("student").ToString & "")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</tr>")



        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<tD     align=""left"" >")
        strHTMLBuilder.Append("ENROL NO :")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("<td   align=""left"" >")
        strHTMLBuilder.Append("" & dt.Rows(0)("EnrollmentNo").ToString & "")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("<td   align=""left"" >")
        strHTMLBuilder.Append("FATHER NAME :")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("<td   align=""left"" >")
        strHTMLBuilder.Append("" & dt.Rows(0)("FatherName").ToString & "")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</tr>")





        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<tD    align=""left""  >")
        strHTMLBuilder.Append("SEMESTER/YEAR :")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("<td    align=""left"" >")
        strHTMLBuilder.Append("" & dt.Rows(0)("Semyear").ToString & "")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("<td  align=""left"" >")
        strHTMLBuilder.Append("DATE OF BIRTH :")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("<td   align=""left"" >")
        strHTMLBuilder.Append("" & dt.Rows(0)("DOB").ToString & "")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("</tr>")
        strHTMLBuilder.Append("</table>")

        'Dim aa2 As SqlParameter() = {New SqlParameter("@v", "Rawa"), New SqlParameter("@examName", Session("ExamName")), New SqlParameter("@courseid", ddlAcademicyear.SelectedValue.ToString), New SqlParameter("@sem", 1), New SqlParameter("@sid", sid)}
        'Dim dt2 As DataTable = cmd.getDataTable("[dbo].[ReportMarksheetSheet]", aa2, "dt2")

        'If dt.Rows.Count > 0 Then
        'Else
        '    SaralMsg.Messagebx.Alert(Me, "no records found")
        '    Exit Sub
        'End If



        strHTMLBuilder.Append("<table width=""100%""  border=""1"" class=""table table-bordered mt-2"" style="" color: #000000; font-family: Tahoma; font-size: 18px"" cellpadding=""5"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<thead >")
        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<th  valign=""top""  nowrap  width=""40px"" align=""center"" >")
        strHTMLBuilder.Append("Sr.No.")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th  valign=""top""  nowrap  width=""40px"" align=""center"" >")
        strHTMLBuilder.Append("Subject code")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th  valign=""top""  nowrap  width=""40px"" align=""center"" >")
        strHTMLBuilder.Append("Subject Name")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th  valign=""top""  nowrap  width=""40px"" align=""left"" >")
        strHTMLBuilder.Append("Maximum Marks")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th   valign=""top""  nowrap  width=""40px"" align=""left"" >")
        strHTMLBuilder.Append("Obtained Marks ")
        strHTMLBuilder.Append("</th>")

        strHTMLBuilder.Append("<th   valign=""top"" nowrap  width=""40px"" align=""center"" >")
        strHTMLBuilder.Append("Status")
        strHTMLBuilder.Append("</th>")


        strHTMLBuilder.Append("</tr>")







        strHTMLBuilder.Append("<tr>")

        Dim xx As Integer = 0
        For Each myRow As DataRow In dt.Rows

            strHTMLBuilder.Append("<td  valign=""top""  nowrap width=""20px"" align=""left"" >")
            strHTMLBuilder.Append("" & xx + 1 & "")
            strHTMLBuilder.Append("</td>")

            strHTMLBuilder.Append("<td  valign=""top""  nowrap width=""20px"" align=""left"" >")
            strHTMLBuilder.Append("" + myRow("Subjectcode").ToString() + "")
            strHTMLBuilder.Append("</td>")

            strHTMLBuilder.Append("<td  valign=""top""  nowrap width=""20px"" align=""left"" >")
            strHTMLBuilder.Append("" + myRow("Subject").ToString() + "")
            strHTMLBuilder.Append("</td>")

            strHTMLBuilder.Append("<td   valign=""top"" nowrap width=""20px"" align=""left"" >")
            strHTMLBuilder.Append("" + myRow("Maxmarks").ToString() + "")
            strHTMLBuilder.Append("</td>")


            strHTMLBuilder.Append("<td  valign=""top""  nowrap width=""20px""   align=""left"" >")
            strHTMLBuilder.Append("" + myRow("Obtainedmarks").ToString() + "")
            strHTMLBuilder.Append("</td>")



            strHTMLBuilder.Append("<td  valign=""top""  nowrap width=""20px"" align=""left"" >")
            strHTMLBuilder.Append("" + myRow("Reult Status").ToString() + "")
            strHTMLBuilder.Append("</td>")



            strHTMLBuilder.Append("</tr>")

            xx = xx + 1




        Next


        strHTMLBuilder.Append("</table>")

        strHTMLBuilder.Append("<br />")
        strHTMLBuilder.Append("<br />")
        '  strHTMLBuilder.Append("<p style="" color: #000000; font-family: Tahoma; font-size: 19px"">Note      : S/US GRADE NOT COUNTED FOR CALCULATION OF SGPA. <br><strong> RESULT : " & dt.Rows(0)("result").ToString & "  WITH SGPA " & dt.Rows(0)("sgpa").ToString & " ON 10 POINT SCALE.</strong><p>")
        'Dim aa2 As SqlParameter() = {New SqlParameter("@q", "Rawa2"), New SqlParameter("@examName", Session("ExamName")), New SqlParameter("@courseid", ddlAcademicyear.SelectedValue.ToString), New SqlParameter("@sem", 1), New SqlParameter("@sid", sid)}
        'Dim dt2 As DataTable = cmd.getDataTable("[dbo].[ReportMarkssum]", aa2, "dt2")

        'If dt.Rows.Count > 0 Then
        'Else
        '    SaralMsg.Messagebx.Alert(Me, "no records found")
        '    Exit Sub
        'End If
        strHTMLBuilder.Append("<table  align=""center""   width=""100%"" style="" color: #000000; font-family: calibri; font-size: 19px"" cellspacing=""0"" >")
        strHTMLBuilder.Append("<tr>")
        strHTMLBuilder.Append("<td  valign=""top""  nowrap width=""3px"" style=""font-weight:Bold"" align=""left""  >")
        'strHTMLBuilder.Append("Total : " & dt.Rows(0)("sumobtainedmarks").ToString & "/" & dt.Rows(0)("sumMaxMarks").ToString & " ")
        strHTMLBuilder.Append("</td>")

        'strHTMLBuilder.Append("<td  valign=""top""  nowrap width=""2px"" align=""left"" >")
        'strHTMLBuilder.Append("" & dt.Rows(0)("sumobtainedmarks").ToString & "/" & dt.Rows(0)("sumMaxMarks").ToString & "")
        'strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("<td  valign=""top""  nowrap width=""72px"" align=""center"" >")
        strHTMLBuilder.Append("")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("<td  valign=""top""  nowrap width=""3px"" align=""right"" >")
        strHTMLBuilder.Append("Signature:")
        strHTMLBuilder.Append("</td>")


        strHTMLBuilder.Append("<td  valign=""top""  nowrap width=""20px"" align=""left"" rowspan=""6"" >")
        strHTMLBuilder.Append("<img src=""Photos/Branchlogo/College Logo.png""  style=""  height=""100"" width=""100""  >")
        strHTMLBuilder.Append("<td  valign=""top""  nowrap width=""3px"" align=""center"" >")
        strHTMLBuilder.Append("")
        strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("</td>")

        strHTMLBuilder.Append("</tr>")
        strHTMLBuilder.Append("</table>")
        strHTMLBuilder.Append("<br />")
        strHTMLBuilder.Append("<br />")
        strHTMLBuilder.Append("<br />")
        strHTMLBuilder.Append("<br />")
        strHTMLBuilder.Append("<br />")
        strHTMLBuilder.Append("<br />")
        strHTMLBuilder.Append("<br />")
        strHTMLBuilder.Append("<br />")


        strHTMLBuilder.Append("<table  align=""center""   width=""100%"" style="" color: #000000; font-family: calibri; font-size: 19px"" cellspacing=""0"" >")

        strHTMLBuilder.Append("<td  valign=""top""  nowrap width=""5px"" align=""left"" style=""font-weight:Bold"" >")
        strHTMLBuilder.Append("** Note : Note will be write here")
        strHTMLBuilder.Append("</td>")

        'strHTMLBuilder.Append("<td  valign=""top""  nowrap width=""2px"" align=""left""  style=""font-weight:Bold"" >")
        'strHTMLBuilder.Append("Note will be write here")
        'strHTMLBuilder.Append("</td>")
        strHTMLBuilder.Append("<td  valign=""top""  nowrap width=""90px"" align=""left""  style=""font-weight:Bold"" >")
        strHTMLBuilder.Append("")
        strHTMLBuilder.Append("</td>")


        strHTMLBuilder.Append("</table>")







        strHTMLBuilder.Append("<br />")
        strHTMLBuilder.Append("<br />")
        'strHTMLBuilder.Append("<br />")
        ' strHTMLBuilder.Append(" <pre style="" color: #000000; font-family: Tahoma; font-size: 19px; font-weight: bold"" class=""tab"">   Prepared by                                          Checked by                                          Asstt. Registrar (Academics)</pre>")

        'strHTMLBuilder.Append("<p style="" color: #000000; font-family: Tahoma; font-size: 19px""><strong>Date of issue :" & dt.Rows(0)("ResultDate").ToString & " <br> Place       : Longowal</strong></p>")

        strHTMLBuilder.Append("<p style=""page-break-after:always;""></p>")
        Literal1.Text = strHTMLBuilder.ToString()
    End Sub





    Protected Sub btn_back_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_back_print.Click
        pnlmain.Visible = True
        pnlprint.Visible = False
    End Sub

    Protected Sub bntdocprint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bntdocprint.Click
        Session("ctrl") = pnl1
        ClientScript.RegisterStartupScript(Me.GetType(), "onclick", "<script language=javascript>window.open('print.aspx','PrintMe','height=800px,width=800px,scrollbars=1');</script>")
    End Sub



    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        ViewState("Courseid") = Session("Courseid")
        'Session("Otherid") = row.Cells(2).Text
        Session("ExamName") = Session("Examname")
        Session("Course") = Session("Course")
        Session("Semyear") = Session("Semyear")
        Response.Redirect("CreatedExams.aspx?rid=" & Session("Courseid") & "&acyr=" & ViewState("Academicyear") & "&u=" & Request.QueryString("u") & "&s=" & Request.QueryString("s") & "&ay=" & ViewState("ayid"))
    End Sub

    Protected Sub ddlAcademicyear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAcademicyear.SelectedIndexChanged
        BindGrid()
    End Sub

End Class
