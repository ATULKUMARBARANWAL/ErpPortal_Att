Imports System.IO
Imports System.Data.SqlClient
Imports System.Data

Partial Class Admin_Student
    Inherits System.Web.UI.Page
    Private cmd As dbnew = New dbnew()
    Private saralstudent As saral.student = New saral.student()
    Private saralMastercls As saral.Mastercls = New saral.Mastercls()
    Dim sql As String = ""
    Dim dt1 As DataTable
    <System.Web.Services.WebMethodAttribute(), _
System.Web.Script.Services.ScriptMethodAttribute()> _
    Public Shared Function GetCompletionList(ByVal prefixText As String, _
          ByVal count As Integer) As String()
        Dim cmd As New SqlCommand With {.Connection = db.Con}
        cmd.CommandText = "select * from udf_searchstu(1197) where s LIKE '" & _
          "%" & prefixText & "%'"
        Dim myReader As SqlDataReader
        Dim returnData As List(Of String) = New List(Of String)
        myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While myReader.Read()
            returnData.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(myReader("s").ToString(), myReader("studentid")))
        End While
        Return returnData.ToArray()
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            ViewState("acdmicyr") = Request.QueryString("acyr")

            ViewState("courseid") = Request.QueryString("programid")
            sql = "select LovUserEnglishID,FieldValue  from LovUserEnglish  where MasterListingID=18 order by FieldValue "
            cmd.FillDropdown(ddlreligion, sql)


            sql = "select LovUserEnglishID,FieldValue  from LovUserEnglish  where MasterListingID=4 order by FieldValue "
            cmd.FillDropdown(ddlcorrcity, sql)

            sql = "select LovUserEnglishID,FieldValue  from LovUserEnglish  where MasterListingID=31 order by FieldValue "
            cmd.FillDropdown(ddlcorrstate, sql)

            sql = "select LovUserEnglishID,FieldValue  from LovUserEnglish  where MasterListingID=62 order by FieldValue "
            cmd.FillDropdown(ddlcordistt, sql)

            sql = "select LovUserEnglishID,FieldValue  from LovUserEnglish  where MasterListingID=5 order by FieldValue "
            cmd.FillDropdown(ddlcorrcountry, sql)

            sql = "select LovUserEnglishID,FieldValue  from LovUserEnglish  where MasterListingID=4 order by FieldValue "
            cmd.FillDropdown(ddlprmcity, sql)

            sql = "select LovUserEnglishID,FieldValue  from LovUserEnglish  where MasterListingID=31 order by FieldValue "
            cmd.FillDropdown(ddlprmsate, sql)

            sql = "select LovUserEnglishID,FieldValue  from LovUserEnglish  where MasterListingID=62 order by FieldValue "
            cmd.FillDropdown(ddlprmdistt, sql)

            sql = "select LovUserEnglishID,FieldValue  from LovUserEnglish  where MasterListingID=5 order by FieldValue "
            cmd.FillDropdown(ddlprmcountry, sql)

            sql = "select FieldValue AS LovUserEnglishID,FieldValue  from LovUserEnglish  where MasterListingID=19 order by FieldValue "
            cmd.FillDropdown(DdlCategoryc, sql)

            sql = "select LovUserEnglishID,FieldValue  from LovUserEnglish  where MasterListingID=32 order by FieldValue "
            cmd.FillDropdown(ddlbankname, sql)

            sql = "select LovUserEnglishID,FieldValue  from LovUserEnglish  where MasterListingID=7 order by FieldValue "
            cmd.FillDropdown(ddlfOccupation, sql)
            cmd.FillDropdown(ddlMOccupation, sql)
            'sql = "select LovUserEnglishID,FieldValue  from LovUserEnglish  where MasterListingID=7 order by FieldValue "
            'cmd.FillDropdown(ddlMOccupation, sql)
            sql = "select distinct  l.LovUserEnglishID,l.FieldValue  from LovUserEnglish l inner join MASTERLISTING m on l.MasterListingID=m.MasterListingID  where m.Field='StudentType' order by l.FieldValue "
            cmd.FillDropdown(ddlstatus, sql)

            sql = "select sid,code from studentcategory order by code "
            cmd.FillDropdown(ddlstudentmode, sql)


            sql = "SELECT distinct [ConsultantId], [ConsultancyName] FROM [I_Consultant] where [ConsultancyName]<>'' order by [ConsultancyName]  "
            cmd.FillDropdown(Ddlconsul, sql)

            'sql = "select v.a as Sem, isnull(c.amount,0) as Amount from View_rowno  v left join ( select Sem,amount from  ConsultantPaymentSem where  studentid= 12 and consultantid =2 ) c on c.sem=v.a where a<9 order by a"

            educatinlistbind(0)
            doclistbind(0)
            '  photoBind(0)


            If Request.QueryString("stuid") <> "" Then
                bind(" studentid= " & Request.QueryString("stuid"))

                stusearch.Visible = False

                ' Panelpersonal.Visible = False
                Paneldocuments.Visible = False
                ' panelconcession.Visible = False
                Ddlconsul.Enabled = False
                panelConsultancy.Visible = False


                If Request.QueryString("scmd") = "Personal" Then
                    Panelpersonal.Visible = True
                ElseIf Request.QueryString("scmd") = "Documents" Then
                    Paneldocuments.Visible = True
                ElseIf Request.QueryString("scmd") = "Hostel" Then
                    Panelpersonal.Visible = True
                ElseIf Request.QueryString("scmd") = "Transport" Then
                    Panelpersonal.Visible = True
                    ' ElseIf Request.QueryString("scmd") = "Concession" Then
                    '     panelconcession.Visible = True
                ElseIf Request.QueryString("scmd") = "Consultancy" Then
                    panelConsultancy.Visible = True
                End If
            End If

        End If



        If Request.Form("__EVENTTARGET") = "load$student" Then
            bind(Request.Form("__EVENTARGUMENt"))
        End If

    End Sub
    Sub bind(ByVal sid As String)

        Dim sql As String
        'sql = " select  cls.Code as sec, b.batch,CONVERT(varchar,dated,103) as dated,* from student Inner Join Course on Course.CourseID=Student.CourseID where  " & sid
        sql = "select   case when studenttype = 'R' then 'Regular' when studenttype ='L' then 'Lateral' when studenttype = 'm' " & _
" then 'Migrate' else '' end as stutype , cls.Code as sec, b.batch, i.Institue ,  f.Tag as admissionyear,  seat.Seat as seat, " & _
" c.Course as course, CONVERT(varchar,student.dated,103) as dated, * from student Inner Join Exam_Course on Exam_Course.CourseID=Student.CourseID " & _
" left join Exam_Course c on c.courseid=student.courseid left join Batch b on b.BatchID=student.BatchID " & _
" left join seat  on seat.SeatID=student.SeatID  left join FinancialYear f on f.SessionID=student.sessionid " & _
" left join Institue i on i.InstitueID=Exam_Course.Courseid  " & _
 " left join Classes cls on cls.ClassesID=student.Classesid where " & sid



        Dim dt As DataTable = cmd.getDataTable(sql)
        If dt.Rows.Count > 0 Then
            ViewState("sid") = CInt(dt.Rows(0)("studentid").ToString)

            'ddlacademicyear.SelectedValue = dt.Rows(0)("sessionid").ToString
            ddlacademicyear.InnerText = dt.Rows(0)("admissionyear").ToString
            TxtAdmNo.InnerText = dt.Rows(0)("AdmissionNo").ToString

            ' TxtAdmNo.Text = dt.Rows(0)("AdmissionNo").ToString
            txtdated.InnerText = dt.Rows(0)("Dated").ToString

            ddlseat.InnerText = dt.Rows(0)("seatid").ToString


            ddlstutype.InnerText = dt.Rows(0)("stutype").ToString


            TxtFirstname.InnerText = dt.Rows(0)("Student").ToString


            TxtFname.InnerText = dt.Rows(0)("FatherName").ToString

            ddlcollege.InnerText = dt.Rows(0)("Institue").ToString


            ddlcourse.InnerText = dt.Rows(0)("course").ToString


            ddlsem.InnerText = dt.Rows(0)("sem").ToString

            ddlsec.InnerText = dt.Rows(0)("sec").ToString

            ddlbatch.InnerText = dt.Rows(0)("batch").ToString


            txtsturollno.Text = dt.Rows(0)("Roll_No").ToString
            txtenrolment.Text = dt.Rows(0)("EnrollmentNo").ToString

            'ddlstatus.SelectedValue = dt.Rows(0)("status").ToString
            saralMastercls.BindDropdown(ddlstatus, dt.Rows(0)("status").ToString)

            saralMastercls.BindDropdown(ddlstudentmode, dt.Rows(0)("studentcategoryid").ToString)

            txtadharno.Text = dt.Rows(0)("AdharNo").ToString

            'person

            TxtDob.text = dt.Rows(0)("dob").ToString
            TxtHeight.Text = dt.Rows(0)("Height").ToString
            txtbranchname.Text = dt.Rows(0)("Bankbranch").ToString
            txtifsecode.Text = dt.Rows(0)("banknam").ToString

            saralMastercls.BindDropdown(ddlgender, dt.Rows(0)("gender").ToString)
            saralMastercls.BindDropdown(DdlBlood, dt.Rows(0)("BloodGroup").ToString)

            TxtWeight.Text = dt.Rows(0)("Weight").ToString
            TxtPassport.Text = dt.Rows(0)("PassportNo").ToString

            saralMastercls.BindDropdown(DdlCategoryc, dt.Rows(0)("CasteCategory").ToString)
            saralMastercls.BindDropdown(ddlreligion, dt.Rows(0)("Religion").ToString)

            saralMastercls.BindDropdown(DdlVision, dt.Rows(0)("Vision").ToString)
            saralMastercls.BindDropdown(ddlmarital, dt.Rows(0)("MaritalStatus").ToString)

            TxtNationality.Text = dt.Rows(0)("Nationality").ToString
            TxtNationality.Text = dt.Rows(0)("Nationality").ToString
            TxtHobbies.Text = dt.Rows(0)("Hobbies").ToString

            TxtDomocile.Text = dt.Rows(0)("Domicile").ToString
            txtaccountno.Text = dt.Rows(0)("BankAC").ToString
            saralMastercls.BindDropdown(ddlbankname, dt.Rows(0)("BankName").ToString)

            saralMastercls.BindDropdown(ddlFOccupation, dt.Rows(0)("FatherOccupation").ToString)
            saralMastercls.BindDropdown(ddlMOccupation, dt.Rows(0)("MotherOccupation").ToString)
            Txtmothername.Text = dt.Rows(0)("MotherName").ToString
            Txtincome.Text = dt.Rows(0)("Income").ToString


            textcorraddress1.Text = dt.Rows(0)("CorrAddress1").ToString
            textcorraddress2.Text = dt.Rows(0)("CorrAddress2").ToString
            txtcorrpin.Text = dt.Rows(0)("CorrPinCode").ToString

            saralMastercls.BindDropdown(ddlcorrcountry, dt.Rows(0)("CorrCountry").ToString)
            saralMastercls.BindDropdown(ddlcorrstate, dt.Rows(0)("CorrState").ToString)
            saralMastercls.BindDropdown(ddlcorrcity, dt.Rows(0)("CorrCity").ToString)
            saralMastercls.BindDropdown(ddlcordistt, dt.Rows(0)("CorrDistt").ToString)
            txtcorrphone.Text = dt.Rows(0)("Phone").ToString
            txtcorrmobile.Text = dt.Rows(0)("Mobile").ToString
            txtcorrEmail.Text = dt.Rows(0)("Email").ToString



            txtprmadd1.Text = dt.Rows(0)("PrmAddress1").ToString
            txtprmadd2.Text = dt.Rows(0)("PrmAddress2").ToString
            txtprmpin.Text = dt.Rows(0)("PrmPinCode").ToString
            saralMastercls.BindDropdown(ddlprmcountry, dt.Rows(0)("PrmCountry").ToString)
            saralMastercls.BindDropdown(ddlprmsate, dt.Rows(0)("PrmState").ToString)
            saralMastercls.BindDropdown(ddlprmcity, dt.Rows(0)("PrmCity").ToString)
            saralMastercls.BindDropdown(ddlprmdistt, dt.Rows(0)("PrmDistt").ToString)


            txtPrmPhone.Text = dt.Rows(0)("PrmPhone").ToString
            txtPrmMobile.Text = dt.Rows(0)("PrmMobile").ToString

            txtGaurdianname.Text = dt.Rows(0)("Guardian").ToString
            txtGaurdianmobile.Text = dt.Rows(0)("GuardMobile").ToString
            txtGaurdianphone.Text = dt.Rows(0)("GuardPhone").ToString
            txtGaurdianrelation.Text = dt.Rows(0)("Relation").ToString
            txtGaurdianemail.Text = dt.Rows(0)("GEmail").ToString


            educatinlistbind(CInt(dt.Rows(0)("studentid").ToString))
            doclistbind(CInt(dt.Rows(0)("studentid").ToString))
            photoBind(CInt(dt.Rows(0)("studentid").ToString))
            txtConsultant.Text = dt.Rows(0)("ConsultantAmt").ToString
            saralMastercls.BindDropdown(Ddlconsul, dt.Rows(0)("ConsultantID").ToString)

            sql = "select v.a as Sem,  CONVERT(varchar, cast(ISNULL(amount,'') as decimal(30,2)))   as Amount from View_rowno  v left join ( select Sem,amount from  ConsultantPaymentSem where  studentid= " & ViewState("sid") & " and consultantid =" & dt.Rows(0)("ConsultantID").ToString & " ) c on c.sem=v.a where a<9 order by a"
            grdconsultant.DataSource = cmd.getDataTable(sql)
            grdconsultant.DataBind()
        End If

    End Sub




    '   Protected Sub btnsave_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
    '       Dim sql As String
    '       'If ddlacademicyear.SelectedValue = Request.QueryString("s") And ddlsem.SelectedValue = 1 Then
    '       '    If ViewState("sid") = "0" Then
    '       Try

    '           If btnsave.Text = "Save" Then
    '               Dim reqid As Integer

    '               sql = "                INSERT INTO Users " & _
    '" (			   username, userpassword, usernamefull, useremail, Tbl,  Restricted, IsLockedOut, Department, linkuser)" & _
    '       " '" & TxtAdmNo.Text & "', '1', '" & TxtFirstname.Text & " ', '','Student','0',0,'','" & ddlcourse.SelectedValue.ToString & "','../ControlImages/blank.jpeg'" & _
    '          "SELECT IDENT_CURRENT('users');"


    '               sql = "INSERT INTO Student " & _
    '                           "(sessionid,InquiryID,classesid, FatherName ,Sem, AdmissionNo, Dated, FirstName,  CourseID, cyear,  BatchID, SeatID, InstitueID,UserID,studenttype) " & _
    '                           "VALUES     ('" & ddlacademicyear.SelectedValue & "','0','" & ddlsec.SelectedValue & "','" & TxtFirstname.Text & "','" & ddlsem.SelectedValue & "','" & TxtAdmNo.Text & "','" & txtdated.text & "','" & TxtFirstname.Text & "','" & ddlcourse.SelectedValue & "'," & ddlsem.SelectedValue & ",'" & ddlbatch.SelectedValue.ToString & "','" & ddlseat.SelectedValue & "'," & ddlcollege.SelectedValue & ",'" & Request.QueryString("u") & "','" & ddlstutype.SelectedValue & "')" & _
    '                            "SELECT IDENT_CURRENT('student');"
    '               reqid = cmd.execScaler(sql)

    '               ViewState("sid") = reqid
    '               Dim sessionid As String = ddlacademicyear.SelectedValue.ToString 'Session("Csession")
    '               sql = "DELETE FROM StudentYear WHERE     StudentID ='" & reqid & "' "
    '               cmd.execSQL(sql)

    '               sql = "INSERT INTO StudentYear " & _
    '                     " (StudentID, Courseid, Classesid, SEm, SessionID, Evenodd, YearID, Batchid, Collegeid, Isstruckoff, admtype, seatid, Grp) " & _
    '                 " SELECT     StudentID, CourseID, Classesid, Sem, '" & Request.QueryString("s") & "' AS Expr1,'" & Request.QueryString("e") & "' AS Expr2, Cyear, BatchID, InstitueID, IsStruckOff, AdmType, SeatID, Group_Name " & _
    '                 " FROM         Student WHERE     (StudentID = '" & reqid & "')"
    '               cmd.execSQL(sql)

    '           Else
    '               sql = "UPDATE    Student " & _
    '                                    "SET  fathername='" & TxtFname.Text & "', Dated ='" & txtdated.text & "', FirstName = '" & TxtFname.Text & "', CourseID = '" & ddlcourse.SelectedValue.ToString & "', Cyear = '" & ddlsem.SelectedValue.ToString & "', " & _
    '                                    " BatchID = '" & ddlbatch.SelectedValue.ToString & "', SeatID = '" & ddlseat.SelectedValue.ToString & "', InstitueID = '" & ddlcollege.SelectedValue.ToString & "',Sem='" & ddlsem.SelectedValue.ToString & "',studenttype='" & ddlstutype.SelectedValue & "' " & _
    '                                    "WHERE     (StudentID = " & CInt(ViewState("sid").ToString) & ")"
    '               cmd.execSQL(sql)

    '               sql = "DELETE FROM StudentYear WHERE     StudentID ='" & CInt(ViewState("sid").ToString) & "' "
    '               cmd.execSQL(sql)

    '               sql = "INSERT INTO StudentYear " & _
    '                   " (StudentID, Courseid, Classesid, SEm, SessionID, Evenodd, YearID, Batchid, Collegeid, Isstruckoff, admtype, seatid, Grp) " & _
    '               " SELECT     StudentID, CourseID, Classesid, Sem, '" & Request.QueryString("s") & "' AS Expr1,'" & Request.QueryString("e") & "' AS Expr2, Cyear, BatchID, InstitueID, IsStruckOff, AdmType, SeatID, Group_Name " & _
    '               " FROM         Student WHERE     (StudentID = '" & CInt(ViewState("sid").ToString) & "')"
    '               cmd.execSQL(sql)
    '           End If
    '           SaralMsg.Messagebx.Alert(Me, "Successfully Save")
    '           msgsavebasic.InnerHtml = "Successfully Save"

    '       Catch ex As Exception
    '           SaralMsg.Messagebx.Alert(Me, "Plz try. Again")
    '           msgsavebasic.InnerHtml = "Plz try. Again"
    '       End Try
    '   End Sub



    Protected Sub DownloadFile(ByVal sender As Object, ByVal e As EventArgs)
        'Dim filePath As String = CType(sender, LinkButton).CommandArgument
        'Response.ContentType = ContentType
        'Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
        'Response.WriteFile(filePath)
        'Response.End()

        Try
            Dim ide As String = CType(sender, LinkButton).CommandArgument

            Dim filepath() As String = Directory.GetFiles(Server.MapPath("~/Photos/doc/"), ViewState("sid").ToString + "≈" + ide + "≈*")

            '   Dim filePath As String = CType(sender, LinkButton).CommandArgument
            ' Dim filePath As String = CType(sender, LinkButton).CommandArgument
            If Not String.IsNullOrEmpty(filepath(0)) Then
                ' Dim a As String = "E:/Data/pro/IDEAL/2010idel\Photos\Notification//photos/Notification/6917sample.xls"
                '  Dim a As String = "~/photos/Notification/6917sample.xls"
                ' Dim a As String = "6917sample.xls"

                Dim ext As String = System.IO.Path.GetExtension(Path.GetFileName(filepath(0)))
                Dim type As String = ""
                If ext IsNot Nothing Then
                    Select Case ext.ToLower()
                        Case ".htm", ".html"
                            type = "text/HTML"
                            Exit Select

                        Case ".txt"
                            type = "text/plain"
                            Exit Select

                        Case ".xls", ".xlxs"
                            type = "Application/msword"

                        Case ".doc", ".rtf"
                            type = "Application/vnd.ms-excel"

                        Case ".jpg", ".jpeg", ".png"
                            type = "Application/paint"
                            Exit Select
                    End Select
                End If
                ' Response.ContentType = type
                'Dim d As String = "E:\Data\pro\IDEAL\2010idel\photos\Notification\654314ME020 hemansu.xlsx"

                Response.ContentType = type
                Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filepath(0))))
                Response.WriteFile(filepath(0))

                Response.End()
            Else
                SaralMsg.Messagebx.Alert(Me, "Attachment Not Available")

            End If
        Catch ex As Exception

        End Try
    End Sub
    Sub DeleteImg(ByVal sender As Object, ByVal e As EventArgs)
        Dim link As String = Nothing
        Dim files As String() = System.IO.Directory.GetFiles(Server.MapPath("~/photos/users"), "" & (Trim(ViewState("sid").ToString)) & ".*")
        If Not String.IsNullOrEmpty(files(0)) Then
            Dim exten As String = files(0)
            exten = "." & exten.Substring(exten.LastIndexOf(".") + 1)
            link = "~\photos\users\" & (Trim(ViewState("sid").ToString)) & exten & ""
            If File.Exists(MapPath(link)) Then
                File.Delete(MapPath(link))
            End If
        End If
    End Sub
    Protected Sub DeleteFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim ide As String = CType(sender, LinkButton).CommandArgument
        Dim filepath() As String = Directory.GetFiles(Server.MapPath("~/Photos/doc/"), ViewState("sid").ToString + "≈" + ide + "≈*")
        'If filepath.Length > 0 Then
        '    File.Delete(filepath(0))
        '    doclistbind(ViewState("sid").ToString)
        'End If
        'doclistbind(CInt(ViewState("sid").ToString))
    End Sub
    Sub DeleteImgsig(ByVal sender As Object, ByVal e As EventArgs) Handles btnsigdelete.Click
        Dim link As String = Nothing
        Dim files As String() = System.IO.Directory.GetFiles(Server.MapPath("~/photos/Signature"), "" & (Trim(ViewState("sid").ToString)) & ".*")
        If Not String.IsNullOrEmpty(files(0)) Then
            Dim exten As String = files(0)
            exten = "." & exten.Substring(exten.LastIndexOf(".") + 1)
            link = "~\photos\Signature\" & (Trim(ViewState("sid").ToString)) & exten & ""
            If File.Exists(MapPath(link)) Then
                File.Delete(MapPath(link))
            End If
        End If
    End Sub



    'Protected Sub ddlcollege_SelectedIndexChanged(ByVal usercontrol_collegeddlctr As usercontrol_collegeddlctr, ByVal Empty As System.EventArgs) Handles ddlcollege.SelectedIndexChanged
    '    ddlcourse.getcourse(ddlcollege.SelectedValue)
    'End Sub

    'Protected Sub ddlcourse_SelectedIndexChanged(ByVal usercontrol_courseddlctr As usercontrol_courseddlctr, ByVal Empty As System.EventArgs) Handles ddlcourse.SelectedIndexChanged
    '    ddlsem.getsem(ddlcourse.SelectedValue)
    'End Sub

    Protected Sub btnsatephoto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsatephoto.Click
        Dim studentid As String = ViewState("sid").ToString
        If ViewState("sid").ToString <> "" Then
            If FileUpload1.HasFile Then
                Dim link As String = "~/photos/users/" + studentid + Right(FileUpload1.PostedFile.FileName, InStr(StrReverse(FileUpload1.PostedFile.FileName), ".", CompareMethod.Text))
                If File.Exists(MapPath(link)) Then
                    File.Delete(MapPath(link))
                    FileUpload1.SaveAs(MapPath(link))
                Else
                    FileUpload1.SaveAs(MapPath(link))
                End If
            End If



            If FileUpload2.HasFile Then
                Dim link As String = "~/photos/Signature/" + studentid + Right(FileUpload2.PostedFile.FileName, InStr(StrReverse(FileUpload2.PostedFile.FileName), ".", CompareMethod.Text))
                If File.Exists(MapPath(link)) Then
                    File.Delete(MapPath(link))
                    FileUpload2.SaveAs(MapPath(link))
                Else
                    FileUpload2.SaveAs(MapPath(link))
                End If
            End If

            If FileUpload3.HasFile Then
                Dim link As String = "~/photos/Thumb/" + studentid + Right(FileUpload3.PostedFile.FileName, InStr(StrReverse(FileUpload3.PostedFile.FileName), ".", CompareMethod.Text))
                If File.Exists(MapPath(link)) Then
                    File.Delete(MapPath(link))
                    FileUpload3.SaveAs(MapPath(link))
                Else
                    FileUpload3.SaveAs(MapPath(link))
                End If
            End If
        End If


        photoBind(studentid)
        SaralMsg.Messagebx.Alert(Me, "Successfully Save")
    End Sub
    Protected Sub btnsaveacademic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsaveacademic.Click
        Dim studentid As String = ViewState("sid").ToString
        If ViewState("sid").ToString <> "" Then

            sql = " UPDATE    Student " & _
                                "SET   studentcategoryid='" & ddlstudentmode.SelectedValue.ToString & "', status='" & ddlstatus.SelectedValue.ToString & "', AdharNo='" & txtadharno.Text & "',EnrollmentNo='" & txtenrolment.Text & "',Roll_No='" & txtsturollno.Text & "' " & _
                                    " WHERE     (StudentID = " & CInt(Trim(ViewState("sid").ToString)) & ")"
            cmd.execSQL(sql)

            'If FileUpload1.HasFile Then
            '    Dim link As String = "~/photos/users/" + studentid + Right(FileUpload1.PostedFile.FileName, InStr(StrReverse(FileUpload1.PostedFile.FileName), ".", CompareMethod.Text))
            '    If File.Exists(MapPath(link)) Then
            '        File.Delete(MapPath(link))
            '        FileUpload1.SaveAs(MapPath(link))
            '    Else
            '        FileUpload1.SaveAs(MapPath(link))
            '    End If
            'End If



            'If FileUpload2.HasFile Then
            '    Dim link As String = "~/photos/Signature/" + studentid + Right(FileUpload2.PostedFile.FileName, InStr(StrReverse(FileUpload2.PostedFile.FileName), ".", CompareMethod.Text))
            '    If File.Exists(MapPath(link)) Then
            '        File.Delete(MapPath(link))
            '        FileUpload2.SaveAs(MapPath(link))
            '    Else
            '        FileUpload2.SaveAs(MapPath(link))
            '    End If
            'End If
        End If
        ' photoBind(studentid)
        SaralMsg.Messagebx.Alert(Me, "Successfully Save")

    End Sub
    Public Sub photoBind(ByVal SID As Integer)
        Try


            ViewState("sid") = SID
            Dim files As String() = System.IO.Directory.GetFiles(Server.MapPath("~/photos/users"), "" & (Trim(ViewState("sid").ToString)) & ".*")
            If files.ToArray.Length > 0 Then
                Dim exten As String = files(0)
                exten = "." & exten.Substring(exten.LastIndexOf(".") + 1)
                stuimage.ImageUrl = "~\photos\users\" & SID.ToString & exten & ""
            Else
                stuimage.ImageUrl = "~\photos\users\blank.jpeg"
            End If


            Dim files1 As String() = System.IO.Directory.GetFiles(Server.MapPath("~/photos/Signature"), "" & (Trim(ViewState("sid").ToString)) & ".*")
            If files1.ToArray.Length > 0 Then
                Dim exten As String = files1(0)
                exten = "." & exten.Substring(exten.LastIndexOf(".") + 1)
                Stuimagesig.ImageUrl = "~\photos\Signature\" & SID.ToString & exten & ""
            Else
                Stuimagesig.ImageUrl = "~\photos\Signature\blank.jpeg"
            End If

            Dim files2 As String() = System.IO.Directory.GetFiles(Server.MapPath("~/photos/Thumb"), "" & (Trim(ViewState("sid").ToString)) & ".*")
            If files2.ToArray.Length > 0 Then
                Dim exten As String = files2(0)
                exten = "." & exten.Substring(exten.LastIndexOf(".") + 1)
                Image1.ImageUrl = "~\photos\Thumb\" & SID.ToString & exten & ""
            Else
                Image1.ImageUrl = "~\photos\Thumb\blank.jpeg"
            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnpersonalsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpersonalsave.Click

        Dim studentid As String = ViewState("sid").ToString
        If ViewState("sid").ToString <> "" Then
            sql = " UPDATE    Student " & _
                                "SET  dob='" & TxtDob.text & "' , Height='" & TxtHeight.Text & "' , Bankbranch='" & txtbranchname.Text & "', banknam='" & txtifsecode.Text & "', gender='" & ddlgender.SelectedValue.ToString & "', BloodGroup='" & DdlBlood.SelectedItem.ToString & "' , Religion='" & ddlreligion.SelectedValue.ToString & "' ,Weight='" & TxtWeight.Text & "' ,  CasteCategory='" & DdlCategoryc.SelectedValue.ToString & "', PassportNo='" & TxtPassport.Text & "', MaritalStatus='" & ddlmarital.SelectedValue.ToString & "',Vision='" & DdlVision.SelectedValue.ToString & "' , Nationality = '" & TxtNationality.Text & "', Hobbies='" & TxtHobbies.Text & "' , Domicile='" & TxtDomocile.Text & "', BankAC='" & txtaccountno.Text & "' , BankName ='" & ddlbankname.SelectedValue.ToString & "'  ,   FatherOccupation='" & ddlFOccupation.SelectedValue.ToString & "' ,MotherOccupation='" & ddlMOccupation.SelectedValue.ToString & "' , MotherName='" & Txtmothername.Text & "', Income='" & Txtincome.Text & "' " & _
                                    " WHERE     (StudentID = " & CInt(Trim(ViewState("sid").ToString)) & ")"
            cmd.execSQL(sql)
            SaralMsg.Messagebx.Alert(Me, "Successfully Save")
        End If
    End Sub
    Protected Sub btnfamilysave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnfamilysave.Click
        Dim studentid As String = ViewState("sid").ToString
        If ViewState("sid").ToString <> "" Then
            sql = " UPDATE    Student " & _
                               "SET  dob='" & TxtDob.text & "' , Height='" & TxtHeight.Text & "' , Bankbranch='" & txtbranchname.Text & "', banknam='" & txtifsecode.Text & "', gender='" & ddlgender.SelectedValue.ToString & "', BloodGroup='" & DdlBlood.SelectedItem.ToString & "' , Religion='" & ddlreligion.SelectedValue.ToString & "' ,Weight='" & TxtWeight.Text & "' ,  CasteCategory='" & DdlCategoryc.SelectedValue.ToString & "', PassportNo='" & TxtPassport.Text & "', MaritalStatus='" & ddlmarital.SelectedValue.ToString & "',Vision='" & DdlVision.SelectedValue.ToString & "' , Nationality = '" & TxtNationality.Text & "', Hobbies='" & TxtHobbies.Text & "' , Domicile='" & TxtDomocile.Text & "', BankAC='" & txtaccountno.Text & "' , BankName ='" & ddlbankname.SelectedValue.ToString & "'  ,   FatherOccupation='" & ddlFOccupation.SelectedValue.ToString & "' ,MotherOccupation='" & ddlMOccupation.SelectedValue.ToString & "' , MotherName='" & Txtmothername.Text & "', Income='" & Txtincome.Text & "' " & _
                                   " WHERE     (StudentID = " & CInt(Trim(ViewState("sid").ToString)) & ")"
            cmd.execSQL(sql)
            SaralMsg.Messagebx.Alert(Me, "Successfully Save")

        End If

    End Sub

    Protected Sub btnsavecontect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsavecontect.Click
        Dim studentid As String = ViewState("sid").ToString
        If ViewState("sid").ToString <> "" Then
            sql = " UPDATE    Student " & _
                                "SET  CorrAddress1='" & textcorraddress1.Text & "' ,CorrAddress2='" & textcorraddress2.Text & "' , CorrPinCode='" & txtcorrpin.Text & "', CorrCountry='" & ddlcorrcountry.SelectedValue.ToString & "' , CorrState='" & ddlcorrstate.SelectedValue.ToString & "' , CorrCity='" & ddlcorrcity.SelectedValue.ToString & "'  , CorrDistt='" & ddlcordistt.SelectedValue.ToString & "' ,Phone='" & txtcorrphone.Text & "', Mobile='" & txtcorrmobile.Text & "',Email='" & txtcorrEmail.Text & "'  " & _
                                " , PrmAddress1='" & txtprmadd1.Text & "' ,PrmAddress2='" & txtprmadd2.Text & "' , PrmPinCode='" & txtprmpin.Text & "', PrmCountry='" & ddlprmcountry.SelectedValue.ToString & "' , PrmState='" & ddlprmsate.SelectedValue.ToString & "' , PrmCity='" & ddlprmcity.SelectedValue.ToString & "'  , PrmDistt='" & ddlprmdistt.SelectedValue.ToString & "' , PrmPhone = '" & txtPrmPhone.Text & "' , PrmMobile='" & txtPrmMobile.Text & "', " & _
                                "  Guardian ='" & txtGaurdianname.Text & "',GuardMobile ='" & txtGaurdianmobile.Text & "',GuardPhone  ='" & txtGaurdianphone.Text & "', Relation='" & txtGaurdianrelation.Text & "' , GEmail = '" & txtGaurdianemail.Text & "' " & _
                                    " WHERE     (StudentID = " & CInt(Trim(ViewState("sid").ToString)) & ")"
            cmd.execSQL(sql)
            SaralMsg.Messagebx.Alert(Me, "Successfully Save")
        End If
    End Sub
    Protected Sub btnaddress1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnaddress1.Click
        Dim studentid As String = ViewState("sid").ToString
        If ViewState("sid").ToString <> "" Then
            sql = " UPDATE    Student " & _
                                "SET  CorrAddress1='" & textcorraddress1.Text & "' ,CorrAddress2='" & textcorraddress2.Text & "' , CorrPinCode='" & txtcorrpin.Text & "', CorrCountry='" & ddlcorrcountry.SelectedValue.ToString & "' , CorrState='" & ddlcorrstate.SelectedValue.ToString & "' , CorrCity='" & ddlcorrcity.SelectedValue.ToString & "'  , CorrDistt='" & ddlcordistt.SelectedValue.ToString & "' ,Phone='" & txtcorrphone.Text & "', Mobile='" & txtcorrmobile.Text & "',Email='" & txtcorrEmail.Text & "'  " & _
                                " , PrmAddress1='" & txtprmadd1.Text & "' ,PrmAddress2='" & txtprmadd2.Text & "' , PrmPinCode='" & txtprmpin.Text & "', PrmCountry='" & ddlprmcountry.SelectedValue.ToString & "' , PrmState='" & ddlprmsate.SelectedValue.ToString & "' , PrmCity='" & ddlprmcity.SelectedValue.ToString & "'  , PrmDistt='" & ddlprmdistt.SelectedValue.ToString & "' , PrmPhone = '" & txtPrmPhone.Text & "' , PrmMobile='" & txtPrmMobile.Text & "', " & _
                                "  Guardian ='" & txtGaurdianname.Text & "',GuardMobile ='" & txtGaurdianmobile.Text & "',GuardPhone  ='" & txtGaurdianphone.Text & "', Relation='" & txtGaurdianrelation.Text & "' , GEmail = '" & txtGaurdianemail.Text & "' " & _
                                    " WHERE     (StudentID = " & CInt(Trim(ViewState("sid").ToString)) & ")"
            cmd.execSQL(sql)
            SaralMsg.Messagebx.Alert(Me, "Successfully Save")
        End If
    End Sub
    '


    Protected Sub btneducation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btneducation.Click
        If ViewState("sid").ToString <> "" Then
            Dim rowindex As Integer = 0
            Dim sid As Integer = CInt(ViewState("sid"))

            sql = "delete from StudentEducation where studentid=" & sid & " "
            cmd.execSQL(sql)
            For Each row As GridViewRow In GrdEdu.Rows
                Dim Sno As String = CType(GrdEdu.Rows(rowindex).Cells(0).FindControl("LabelSno"), Label).Text
                Dim Qual As String = CType(GrdEdu.Rows(rowindex).Cells(1).FindControl("lblQual"), Label).Text
                '   Dim Board As String = CType(GrdEdu.Rows(rowindex).Cells(2).FindControl("TxtBoard"), TextBox).Text
                Dim Board As String = CType(GrdEdu.Rows(rowindex).Cells(2).FindControl("ddlquli"), DropDownList).SelectedItem.ToString

                Dim uni As String = CType(GrdEdu.Rows(rowindex).Cells(3).FindControl("ddluni"), DropDownList).SelectedItem.ToString

                Dim Stream As String = CType(GrdEdu.Rows(rowindex).Cells(3).FindControl("TxtStream"), TextBox).Text
                Dim Years As String = CType(GrdEdu.Rows(rowindex).Cells(4).FindControl("Txtyear"), TextBox).Text

                Dim Roll_no As String = CType(GrdEdu.Rows(rowindex).Cells(5).FindControl("txtRoll_no"), TextBox).Text
                Dim institute As String = CType(GrdEdu.Rows(rowindex).Cells(5).FindControl("txtinstitute"), TextBox).Text

                Dim MM As String = CType(GrdEdu.Rows(rowindex).Cells(5).FindControl("TxtMM"), TextBox).Text
                Dim obt As String = CType(GrdEdu.Rows(rowindex).Cells(5).FindControl("Txtobt"), TextBox).Text
                Dim persentage As String = CType(GrdEdu.Rows(rowindex).Cells(5).FindControl("Txtpercentage"), TextBox).Text


                '  Dim marckobt As String = CType(GrdEdu.Rows(rowindex).Cells(5).FindControl("txtpcm"), TextBox).Text
                Dim pcm As String = CType(GrdEdu.Rows(rowindex).Cells(5).FindControl("txtpcm"), TextBox).Text
                Dim phy As String = CType(GrdEdu.Rows(rowindex).Cells(5).FindControl("txtphy"), TextBox).Text
                Dim chem As String = CType(GrdEdu.Rows(rowindex).Cells(5).FindControl("txtchem"), TextBox).Text

                Dim maths As String = CType(GrdEdu.Rows(rowindex).Cells(5).FindControl("txtmaths"), TextBox).Text
                Dim eng As String = CType(GrdEdu.Rows(rowindex).Cells(5).FindControl("Txteng"), TextBox).Text
                Dim grade As String = CType(GrdEdu.Rows(rowindex).Cells(5).FindControl("Txtgrade"), TextBox).Text

                If Not String.IsNullOrEmpty(Board) Or Not String.IsNullOrEmpty(uni) Then
                    sql = "Insert into StudentEducation (university,Studentid,Serial,Board,Qualification,Stream,Passingyear,Percentage,Roll_No,MM,Marks,PCM,phy,chem,maths,english,Institution,grade) values('" & uni & "' , '" & sid & "','" & Sno & "','" & Board & "','" & Qual & "','" & Stream & "','" & Years & "','" & persentage & "','" & Roll_no & "','" & MM & "','" & obt & "','" & pcm & "','" & phy & "','" & chem & "','" & maths & "','" & eng & "','" & institute & "','" & grade & "')"
                    cmd.execSQL(sql)
                End If
                rowindex += 1
            Next
        End If
        SaralMsg.Messagebx.Alert(Me, "Successfully Save")
    End Sub
    Public Sub educatinlistbind(ByVal sid As Integer)
        Try
            saralstudent.stu_fill_edu(GrdEdu, sid)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnuploaddoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnuploaddoc.Click

        Try

            If ViewState("sid").ToString <> "" Then
                Dim rowindex As Integer = 0


                sql = "delete from StudentEssentialDoc where studentid= " & CInt(ViewState("sid")) & ""
                cmd.execSQL(sql)

                For Each item As GridViewRow In GrdDoclist.Rows
                    Dim a As String = CType(GrdDoclist.Rows(item.RowIndex).Cells(1).FindControl("Chkrequired"), CheckBox).Checked
                    Dim c As String = CType(GrdDoclist.Rows(item.RowIndex).Cells(2).FindControl("ChkSubmit"), CheckBox).Checked
                    Dim photocopy As String = CType(GrdDoclist.Rows(item.RowIndex).Cells(2).FindControl("ddlphotocopy"), DropDownList).SelectedItem.Text

                    If (CType(GrdDoclist.Rows(item.RowIndex).Cells(1).FindControl("Chkrequired"), CheckBox).Checked = True) Then

                        sql = "insert into StudentEssentialDoc(StudentId,EssentialDocid,isSub,photocopy,reqid)values( '" & CInt(ViewState("sid")) & "','" & GrdDoclist.DataKeys(item.RowIndex).Values(0).ToString & "','" & c & "','" & photocopy & "','" & a & "') "
                        cmd.execSQL(sql)
                        If CType(item.FindControl("FileUpload3"), FileUpload).HasFile Then
                            Dim fileName As String = Trim(ViewState("sid")).ToString + "≈" + GrdDoclist.DataKeys(item.RowIndex).Values(0).ToString + "≈" + Path.GetFileName(CType(item.FindControl("FileUpload3"), FileUpload).PostedFile.FileName).ToString
                            CType(item.FindControl("FileUpload3"), FileUpload).PostedFile.SaveAs((Server.MapPath("~/photos/doc/") + fileName))
                        End If
                    End If
                Next
            End If

            SaralMsg.Messagebx.Alert(Me, "Successfully Save")
        Catch ex As Exception
            SaralMsg.Messagebx.Alert(Me, "Plz try Again")
        End Try

    End Sub
    Public Sub doclistbind(ByVal sid As Integer)
        Try
            saralstudent.stu_fill_uploaddoc(GrdDoclist, sid)

        Catch ex As Exception
        End Try
    End Sub
  
    Protected Sub btnConsultant_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultant.Click
        Try

            sql = "UPDATE    Student " & _
     " SET ConsultantAmt='" & txtConsultant.Text & "', ConsultantID='" & Ddlconsul.SelectedValue.ToString & "'  WHERE     (StudentID = " & CInt(ViewState("sid")) & ")"
            cmd.execSQL(sql)

            sql = "delete from ConsultantPaymentSem where studentid=  " & CInt(ViewState("sid")) & " and consultantid =" & Ddlconsul.SelectedValue.ToString & ""
            cmd.execSQL(sql)

            For Each itm As GridViewRow In grdconsultant.Rows
                If CType(itm.FindControl("txtcamt"), TextBox).Text.ToString <> "" And CType(itm.FindControl("txtcamt"), TextBox).Text.ToString <> "0.00" Then
                    sql = "insert into ConsultantPaymentSem (Consultantid, sem, Studentid, Amount, userid) VALUES     (" & Ddlconsul.SelectedValue.ToString & " , " & itm.RowIndex + 1 & " , " & CInt(ViewState("sid")) & "  , " & CType(itm.FindControl("txtcamt"), TextBox).Text.ToString & ", " & Request.QueryString("u") & " ) "
                    cmd.execSQL(sql)
                End If
            Next

            SaralMsg.Messagebx.Alert(Me, "Successfully Save")
        Catch ex As Exception
            SaralMsg.Messagebx.Alert(Me, "Plz try Again")
        End Try
    End Sub

    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        Response.Redirect("StudentList.aspx?acyr=" & ViewState("acdmicyr") & "&rid=" & ViewState("courseid"))
    End Sub
End Class
