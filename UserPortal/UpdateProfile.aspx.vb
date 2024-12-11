Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System.Drawing
Partial Class UserPortal_UpdateProfile
    Inherits System.Web.UI.Page

    Dim constr As String = ConfigurationManager.ConnectionStrings("myconnection").ConnectionString

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
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("Acyr") = Request.QueryString("acyr")
            ViewState("uid") = Request.QueryString("stuid")
            ViewState("sid") = Request.QueryString("stuid")
            ViewState("crsid") = Request.QueryString("programid")
            fetchddlreligion()

            sql = "select FieldValue AS LovUserEnglishID,FieldValue  from LovUserEnglish  where MasterListingID=19 order by FieldValue "
            cmd.FillDropdown(DdlCategoryc, sql)

            sql = "Select OccupationID as LovUserEnglishID, Name as FieldValue from OccupationMaster  order by FieldValue "
            cmd.FillDropdown(ddlFOccupation, sql)
            cmd.FillDropdown(ddlMOccupation, sql)

            sql = "Select ID, Name from CountryMaster Order by Name "
            cmd.FillDropdown(ddlcorrcountry, sql)
            fetchddlcorrstate()
            fetchddlcordistt()
            fetchddlcorrcity()

            sql = "Select ID, Name from CountryMaster Order by Name "
            cmd.FillDropdown(ddlprmcountry, sql)
            fetchddlprmsate()
            fetchddlprmdistt()
            fetchddlprmcity()

            sql = "select LovUserEnglishID,FieldValue  from LovUserEnglish  where MasterListingID=32 order by FieldValue "
            cmd.FillDropdown(ddlbankname, sql)

            sql = "select sid,code from studentcategory order by code "
            cmd.FillDropdown(ddlstudentmode, sql)

            personal.Attributes.Add("class", "active")
            personal.Style.Add("color", "lightgray")
            account.Style.Add("color", "#15283c")
            Family.Style.Add("color", "#15283c")
            educational.Style.Add("color", "#15283c")
            Address.Style.Add("color", "#15283c")
            UploadFile.Style.Add("color", "#15283c")



            If Request.QueryString("stuid") <> "" Then
                bind(" studentid= " & Request.QueryString("stuid"))

                stusearch.Visible = False

                ' Panelpersonal.Visible = False
                '  Paneldocuments.Visible = False
                ' panelconcession.Visible = False
                '  Ddlconsul.Enabled = False
                ' panelConsultancy.Visible = False


                'If Request.QueryString("scmd") = "Personal" Then
                '    Panelpersonal.Visible = True
                'ElseIf Request.QueryString("scmd") = "Documents" Then
                '    Paneldocuments.Visible = True
                'ElseIf Request.QueryString("scmd") = "Hostel" Then
                '    Panelpersonal.Visible = True
                'ElseIf Request.QueryString("scmd") = "Transport" Then
                '    Panelpersonal.Visible = True
                '    ' ElseIf Request.QueryString("scmd") = "Concession" Then
                '    '     panelconcession.Visible = True
                'ElseIf Request.QueryString("scmd") = "Consultancy" Then
                '    panelConsultancy.Visible = True
                'End If
            End If

            educatinlistbind(ViewState("sid"))
            BINDPHOTO()
        End If
        If Request.Form("__EVENTTARGET") = "load$student" Then
            bind(Request.Form("__EVENTARGUMENt"))
        End If
    End Sub

    Sub BINDPHOTO()
        '   Image1.ImageUrl = "~/Photos/Passbook/" & ViewState("inqid") & ".*"
        Dim files1 As String() = System.IO.Directory.GetFiles(Server.MapPath("~/Photos/users"), "" & ViewState("uid") & ".*")
        If files1.ToArray.Length > 0 Then
            Dim exten As String = files1(0)
            exten = "." & exten.Substring(exten.LastIndexOf(".") + 1)
            Imgprofile.ImageUrl = "~\Photos\users\" & ViewState("uid").ToString & exten & "" + "?" + DateTime.Now.Ticks.ToString()
        Else

        End If

    End Sub

    Sub bind(ByVal sid As String)

        Dim sql As String
        'sql = " select  cls.Code as sec, b.batch,CONVERT(varchar,dated,103) as dated,* from student Inner Join Course on Course.CourseID=Student.CourseID where  " & sid
        sql = "select   case when studenttype = 'R' then 'Regular' when studenttype ='L' then 'Lateral' when studenttype = 'm' " & _
" then 'Migrate' else '' end as stutype , cls.Code as sec, b.batch, i.Institue ,  f.Tag as admissionyear,  seat.Seat as seat, " & _
" c.Course as course, c.Coursecode, CONVERT(varchar,student.dated,103) as dated, * from student Inner Join Exam_Course on Exam_Course.CourseID=Student.CourseID " & _
" left join Exam_Course c on c.courseid=student.courseid left join Batch b on b.BatchID=student.BatchID " & _
" left join seat  on seat.SeatID=student.SeatID  left join FinancialYear f on f.SessionID=student.sessionid " & _
" left join Institue i on i.InstitueID=Exam_Course.Courseid  " & _
 " left join Classes cls on cls.ClassesID=student.Classesid where " & sid



        Dim dt As DataTable = cmd.getDataTable(sql)
        If dt.Rows.Count > 0 Then
            ViewState("sid") = CInt(dt.Rows(0)("studentid").ToString)

            'ddlacademicyear.SelectedValue = dt.Rows(0)("sessionid").ToString
            ddlacademicyear.Text = dt.Rows(0)("admissionyear").ToString
            TxtAdmNo.Text = dt.Rows(0)("AdmissionNo").ToString
            Lblstuname.Text = dt.Rows(0)("Student").ToString
            ' TxtAdmNo.Text = dt.Rows(0)("AdmissionNo").ToString
            txtdated.Text = dt.Rows(0)("Dated").ToString
            Lblcoursecode.Text = dt.Rows(0)("Coursecode").ToString
            '  ddlseat.InnerText = dt.Rows(0)("seatid").ToString
            LbladmissionNo.Text = dt.Rows(0)("AdmissionNo").ToString

            ' ddlstutype.InnerText = dt.Rows(0)("stutype").ToString


            TxtFirstname.Text = dt.Rows(0)("FirstName").ToString
            Txtlastname.Text = dt.Rows(0)("LastName").ToString



            TxtFname.Text = dt.Rows(0)("FatherName").ToString

            '  ddlcollege.Text = dt.Rows(0)("Institue").ToString


            ddlcourse.Text = dt.Rows(0)("course").ToString


            ' ddlsem.InnerText = dt.Rows(0)("sem").ToString

            ' ddlsec.InnerText = dt.Rows(0)("sec").ToString

            ' ddlbatch.InnerText = dt.Rows(0)("batch").ToString


            txtsturollno.Text = dt.Rows(0)("Roll_No").ToString
            txtenrolment.Text = dt.Rows(0)("EnrollmentNo").ToString

            'ddlstatus.SelectedValue = dt.Rows(0)("status").ToString
            ' saralMastercls.BindDropdown(ddlstatus, dt.Rows(0)("status").ToString)

            saralMastercls.BindDropdown(ddlstudentmode, dt.Rows(0)("studentcategoryid").ToString)

            txtadharno.Text = dt.Rows(0)("AdharNo").ToString

            'person

            TxtDob.text = dt.Rows(0)("dob").ToString
            TxtHeight.Text = dt.Rows(0)("Height").ToString
            txtbranchname.Text = dt.Rows(0)("Bankbranch").ToString
            txtifsecode.Text = dt.Rows(0)("banknam").ToString
            Lblstugender.Text = dt.Rows(0)("gender").ToString
            saralMastercls.BindDropdown(ddlgender, dt.Rows(0)("gender").ToString)
            saralMastercls.BindDropdown(DdlBlood, dt.Rows(0)("BloodGroup").ToString)

            TxtWeight.Text = dt.Rows(0)("Weight").ToString
            TxtPassport.Text = dt.Rows(0)("PassportNo").ToString

            saralMastercls.BindDropdown(DdlCategoryc, dt.Rows(0)("CasteCategory").ToString)
            ddlreligion.Items.Clear()
            fetchddlreligion()
            saralMastercls.BindDropdown(ddlreligion, dt.Rows(0)("Religion").ToString)

            saralMastercls.BindDropdown(DdlVision, dt.Rows(0)("Vision").ToString)
            saralMastercls.BindDropdown(ddlmarital, dt.Rows(0)("MaritalStatus").ToString)

            TxtNationality.Text = dt.Rows(0)("Nationality").ToString
            ' TxtNationality.Text = dt.Rows(0)("Nationality").ToString
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

            ddlcorrstate.Items.Clear()
            fetchddlcorrstate()

            saralMastercls.BindDropdown(ddlcorrstate, dt.Rows(0)("CorrState").ToString)

            ddlcorrcity.Items.Clear()
            ddlcordistt.Items.Clear()
            fetchddlcordistt()
            fetchddlcorrcity()

            saralMastercls.BindDropdown(ddlcorrcity, dt.Rows(0)("CorrCity").ToString)
            saralMastercls.BindDropdown(ddlcordistt, dt.Rows(0)("CorrDistt").ToString)
            txtcorrphone.Text = dt.Rows(0)("Phone").ToString
            txtcorrmobile.Text = dt.Rows(0)("Mobile").ToString
            txtcorrEmail.Text = dt.Rows(0)("Email").ToString
            TxtfamIncome.Text = dt.Rows(0)("FamilyIncome").ToString
            txtcaste.Text = dt.Rows(0)("Caste").ToString


            txtprmadd1.Text = dt.Rows(0)("PrmAddress1").ToString
            txtprmadd2.Text = dt.Rows(0)("PrmAddress2").ToString
            txtprmpin.Text = dt.Rows(0)("PrmPinCode").ToString
            saralMastercls.BindDropdown(ddlprmcountry, dt.Rows(0)("PrmCountry").ToString)

            ddlprmsate.Items.Clear()
            fetchddlprmsate()

            saralMastercls.BindDropdown(ddlprmsate, dt.Rows(0)("PrmState").ToString)
            ddlprmcity.Items.Clear()
            ddlprmdistt.Items.Clear()
            fetchddlprmcity()
            fetchddlprmdistt()
            saralMastercls.BindDropdown(ddlprmcity, dt.Rows(0)("PrmCity").ToString)
            saralMastercls.BindDropdown(ddlprmdistt, dt.Rows(0)("PrmDistt").ToString)


            '  txtPrmPhone.Text = dt.Rows(0)("PrmPhone").ToString
            txtPrmMobile.Text = dt.Rows(0)("PrmMobile").ToString

            txtGaurdianname.Text = dt.Rows(0)("Guardian").ToString
            txtGaurdianmobile.Text = dt.Rows(0)("GuardMobile").ToString
            txtGaurdianphone.Text = dt.Rows(0)("GuardPhone").ToString
            txtGaurdianrelation.Text = dt.Rows(0)("Relation").ToString
            txtGaurdianemail.Text = dt.Rows(0)("GEmail").ToString


            educatinlistbind(CInt(dt.Rows(0)("studentid").ToString))
            '   doclistbind(CInt(dt.Rows(0)("studentid").ToString))
            photoBind(CInt(dt.Rows(0)("studentid").ToString))
            'txtConsultant.Text = dt.Rows(0)("ConsultantAmt").ToString
            '  saralMastercls.BindDropdown(Ddlconsul, dt.Rows(0)("ConsultantID").ToString)

            sql = "select v.a as Sem,  CONVERT(varchar, cast(ISNULL(amount,'') as decimal(30,2)))   as Amount from View_rowno  v left join ( select Sem,amount from  ConsultantPaymentSem where  studentid= " & ViewState("sid") & " and consultantid =" & dt.Rows(0)("ConsultantID").ToString & " ) c on c.sem=v.a where a<9 order by a"
            '   grdconsultant.DataSource = cmd.getDataTable(sql)
            '  grdconsultant.DataBind()
        End If

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

    Public Sub doclistbind(ByVal sid As Integer)
        Try
            ' saralstudent.stu_fill_uploaddoc(GrdDoclist, sid)

        Catch ex As Exception
        End Try
    End Sub
    Public Sub educatinlistbind(ByVal sid As Integer)

        Try


            saralstudent.stu_fill_edu(GrdEdu, sid)


            Fetchboarduni(sid)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Fetchboarduni(ByVal sid As Integer)
        For Each row As GridViewRow In GrdEdu.Rows
            Dim board As DropDownList = TryCast(row.FindControl("ddlquli"), DropDownList)
            Dim Uni As DropDownList = TryCast(row.FindControl("ddluni"), DropDownList)
            Dim Rid As String = row.Cells(1).Text
            fetchboauni(sid, board, Uni, Rid)

        Next
    End Sub

    Private Sub fetchboauni(ByVal sid As Integer, ByVal board As DropDownList, ByVal Uni As DropDownList, ByVal Rid As String)
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("SELECT     StudentID, Serial, Board, Qualification, Institution, Stream, PassingYear, Percentage, Roll_No, MM, Marks, PCM, phy, chem, maths, english, Grade, university " & _
"FROM         StudentEducation where StudentID='" & sid & "' and Rid='" & Rid & "'")
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        board.SelectedIndex = board.Items.IndexOf(board.Items.FindByText(dt.Rows(0)("Board")))
                        Uni.SelectedIndex = Uni.Items.IndexOf(Uni.Items.FindByText(dt.Rows(0)("university")))
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub fetchddlreligion()
        'Dim mylist As New List(Of Integer)
        'mylist.AddRange(Enumerable.Range(2010, 30).Reverse())
        'Ddlacademicyear.DataSource = mylist
        'Ddlacademicyear.DataBind()
        'Ddlacademicyear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = True
        Try

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select ID as LovUserEnglishID, Name AS FieldValue  from Religion  order by FieldValue ")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            ddlreligion.DataSource = dt
                            ddlreligion.DataTextField = "FieldValue"
                            ddlreligion.DataValueField = "LovUserEnglishID"
                            ddlreligion.DataBind()

                            ddlreligion.Items.Insert(0, New ListItem("Select", ""))



                        End Using
                    End Using
                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fetchddlcorrstate()
        'Dim mylist As New List(Of Integer)
        'mylist.AddRange(Enumerable.Range(2010, 30).Reverse())
        'Ddlacademicyear.DataSource = mylist
        'Ddlacademicyear.DataBind()
        'Ddlacademicyear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = True
        Try

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select ID, Name from StateMaster where CountryID ='" & ddlcorrcountry.SelectedValue & "' Order by Name ")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            ddlcorrstate.DataSource = dt
                            ddlcorrstate.DataTextField = "Name"
                            ddlcorrstate.DataValueField = "ID"
                            ddlcorrstate.DataBind()

                            ddlcorrstate.Items.Insert(0, New ListItem("Select", ""))



                        End Using
                    End Using
                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fetchddlcordistt()
        'Dim mylist As New List(Of Integer)
        'mylist.AddRange(Enumerable.Range(2010, 30).Reverse())
        'Ddlacademicyear.DataSource = mylist
        'Ddlacademicyear.DataBind()
        'Ddlacademicyear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = True
        Try

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select ID, Name from CityMaster  where StateID  ='" & ddlcorrstate.SelectedValue & "' Order by Name ")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            ddlcordistt.DataSource = dt
                            ddlcordistt.DataTextField = "Name"
                            ddlcordistt.DataValueField = "ID"
                            ddlcordistt.DataBind()

                            ddlcordistt.Items.Insert(0, New ListItem("Select", ""))



                        End Using
                    End Using
                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fetchddlcorrcity()
        'Dim mylist As New List(Of Integer)
        'mylist.AddRange(Enumerable.Range(2010, 30).Reverse())
        'Ddlacademicyear.DataSource = mylist
        'Ddlacademicyear.DataBind()
        'Ddlacademicyear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = True
        Try

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select ID, Name from CityMaster  where StateID  ='" & ddlcorrstate.SelectedValue & "' Order by Name ")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            ddlcorrcity.DataSource = dt
                            ddlcorrcity.DataTextField = "Name"
                            ddlcorrcity.DataValueField = "ID"
                            ddlcorrcity.DataBind()

                            ddlcorrcity.Items.Insert(0, New ListItem("Select", ""))



                        End Using
                    End Using
                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fetchddlprmsate()
        'Dim mylist As New List(Of Integer)
        'mylist.AddRange(Enumerable.Range(2010, 30).Reverse())
        'Ddlacademicyear.DataSource = mylist
        'Ddlacademicyear.DataBind()
        'Ddlacademicyear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = True
        Try

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select ID, Name from StateMaster where CountryID ='" & ddlprmcountry.SelectedValue & "' Order by Name ")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            ddlprmsate.DataSource = dt
                            ddlprmsate.DataTextField = "Name"
                            ddlprmsate.DataValueField = "ID"
                            ddlprmsate.DataBind()

                            ddlprmsate.Items.Insert(0, New ListItem("Select", ""))



                        End Using
                    End Using
                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fetchddlprmdistt()
        'Dim mylist As New List(Of Integer)
        'mylist.AddRange(Enumerable.Range(2010, 30).Reverse())
        'Ddlacademicyear.DataSource = mylist
        'Ddlacademicyear.DataBind()
        'Ddlacademicyear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = True
        Try

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select ID, Name from CityMaster  where StateID  ='" & ddlprmsate.SelectedValue & "' Order by Name ")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            ddlprmdistt.DataSource = dt
                            ddlprmdistt.DataTextField = "Name"
                            ddlprmdistt.DataValueField = "ID"
                            ddlprmdistt.DataBind()

                            ddlprmdistt.Items.Insert(0, New ListItem("Select", ""))



                        End Using
                    End Using
                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub fetchddlprmcity()
        'Dim mylist As New List(Of Integer)
        'mylist.AddRange(Enumerable.Range(2010, 30).Reverse())
        'Ddlacademicyear.DataSource = mylist
        'Ddlacademicyear.DataBind()
        'Ddlacademicyear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = True
        Try

            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Select ID, Name from CityMaster  where StateID  ='" & ddlprmsate.SelectedValue & "' Order by Name ")
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            ddlprmcity.DataSource = dt
                            ddlprmcity.DataTextField = "Name"
                            ddlprmcity.DataValueField = "ID"
                            ddlprmcity.DataBind()

                            ddlprmcity.Items.Insert(0, New ListItem("Select", ""))



                        End Using
                    End Using
                End Using
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlcorrstate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlcorrstate.SelectedIndexChanged
        ddlcordistt.Items.Clear()
        ddlcorrcity.Items.Clear()

        fetchddlcordistt()
        fetchddlcorrcity()

    End Sub

    Protected Sub ddlcorrcountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlcorrcountry.SelectedIndexChanged
        ddlcorrstate.Items.Clear()
        fetchddlcorrstate()

    End Sub

    Protected Sub ddlprmcountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlprmcountry.SelectedIndexChanged
        ddlprmsate.Items.Clear()
        fetchddlprmsate()
    End Sub

    Protected Sub ddlprmsate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlprmsate.SelectedIndexChanged
        ddlprmdistt.Items.Clear()
        fetchddlprmdistt()

        ddlprmcity.Items.Clear()
        fetchddlprmcity()
    End Sub

    Protected Sub LnkPersonal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkPersonal.Click
        personal.Attributes.Add("class", "active")
        personal.Style.Add("color", "lightgray")
        educational.Attributes.Add("class", "Inactive")
        educational.Style.Add("color", "#15283c")
        Family.Attributes.Add("class", "Inactive")
        Family.Style.Add("color", "#15283c")
        Address.Attributes.Add("class", "Inactive")
        Address.Style.Add("color", "#15283c")
        account.Attributes.Add("class", "Inactive")
        account.Style.Add("color", "#15283c")
        UploadFile.Attributes.Add("class", "Inactive")
        UploadFile.Style.Add("color", "#15283c")
        Pnlpersonal.Visible = True
        Pnleducation.Visible = False
        PnlFamily.Visible = False
        Pnlcontact.Visible = False
        PnlAccount.Visible = False
        Pnlphoto.Visible = False

    End Sub

    Protected Sub lnkFamily_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkFamily.Click
        personal.Attributes.Add("class", "Inactive")
        personal.Style.Add("color", "#15283c")
        educational.Attributes.Add("class", "Inactive")
        educational.Style.Add("color", "#15283c")
        Family.Attributes.Add("class", "active")
        Family.Style.Add("color", "lightgray")
        Address.Attributes.Add("class", "Inactive")
        Address.Style.Add("color", "#15283c")
        account.Attributes.Add("class", "Inactive")
        account.Style.Add("color", "#15283c")
        UploadFile.Attributes.Add("class", "Inactive")
        UploadFile.Style.Add("color", "#15283c")
        Pnlpersonal.Visible = False
        Pnleducation.Visible = False
        PnlFamily.Visible = True
        Pnlcontact.Visible = False
        PnlAccount.Visible = False
        Pnlphoto.Visible = False

    End Sub

    Protected Sub Lnkeducation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lnkeducation.Click
        personal.Attributes.Add("class", "Inactive")
        personal.Style.Add("color", "#15283c")
        educational.Attributes.Add("class", "active")
        educational.Style.Add("color", "lightgray")
        Family.Attributes.Add("class", "Inactive")
        Family.Style.Add("color", "#15283c")
        Address.Attributes.Add("class", "Inactive")
        Address.Style.Add("color", "#15283c")
        account.Attributes.Add("class", "Inactive")
        account.Style.Add("color", "#15283c")
        UploadFile.Attributes.Add("class", "Inactive")
        UploadFile.Style.Add("color", "#15283c")
        Pnlpersonal.Visible = False
        Pnleducation.Visible = True
        educatinlistbind(ViewState("sid"))
        PnlFamily.Visible = False
        Pnlcontact.Visible = False
        PnlAccount.Visible = False
        Pnlphoto.Visible = False
    End Sub

    Protected Sub LnkContact_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkContact.Click
        personal.Attributes.Add("class", "Inactive")
        personal.Style.Add("color", "#15283c")
        educational.Attributes.Add("class", "Inactive")
        educational.Style.Add("color", "#15283c")
        Family.Attributes.Add("class", "Inactive")
        Family.Style.Add("color", "#15283c")
        Address.Attributes.Add("class", "active")
        Address.Style.Add("color", "lightgray")
        account.Attributes.Add("class", "Inactive")
        account.Style.Add("color", "#15283c")
        UploadFile.Attributes.Add("class", "Inactive")
        UploadFile.Style.Add("color", "#15283c")
        Pnlpersonal.Visible = False
        Pnleducation.Visible = False
        PnlFamily.Visible = False
        Pnlcontact.Visible = True

        fetchddlcorrstate()
        fetchddlcordistt()
        fetchddlcorrcity()
        If Request.QueryString("stuid") <> "" Then
            bind(" studentid= " & Request.QueryString("stuid"))

            stusearch.Visible = False

            ' Panelpersonal.Visible = False
            '  Paneldocuments.Visible = False
            ' panelconcession.Visible = False
            '  Ddlconsul.Enabled = False
            ' panelConsultancy.Visible = False


            'If Request.QueryString("scmd") = "Personal" Then
            '    Panelpersonal.Visible = True
            'ElseIf Request.QueryString("scmd") = "Documents" Then
            '    Paneldocuments.Visible = True
            'ElseIf Request.QueryString("scmd") = "Hostel" Then
            '    Panelpersonal.Visible = True
            'ElseIf Request.QueryString("scmd") = "Transport" Then
            '    Panelpersonal.Visible = True
            '    ' ElseIf Request.QueryString("scmd") = "Concession" Then
            '    '     panelconcession.Visible = True
            'ElseIf Request.QueryString("scmd") = "Consultancy" Then
            '    panelConsultancy.Visible = True
            'End If
        End If
        PnlAccount.Visible = False
        Pnlphoto.Visible = False
    End Sub

    Protected Sub LnkAccount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkAccount.Click
        personal.Attributes.Add("class", "Inactive")
        personal.Style.Add("color", "#15283c")
        educational.Attributes.Add("class", "Inactive")
        educational.Style.Add("color", "#15283c")
        Family.Attributes.Add("class", "Inactive")
        Family.Style.Add("color", "#15283c")
        Address.Attributes.Add("class", "Inactive")
        Address.Style.Add("color", "#15283c")
        account.Attributes.Add("class", "active")
        account.Style.Add("color", "lightgray")
        UploadFile.Attributes.Add("class", "Inactive")
        UploadFile.Style.Add("color", "#15283c")
        Pnlpersonal.Visible = False
        Pnleducation.Visible = False
        PnlFamily.Visible = False
        Pnlcontact.Visible = False
        PnlAccount.Visible = True
        Pnlphoto.Visible = False
    End Sub

    Protected Sub btnsavepersonal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsavepersonal.Click
        Dim studentid As String = ViewState("sid").ToString
        If ViewState("sid").ToString <> "" Then
            sql = " UPDATE    Student " & _
                                "SET FirstName='" & TxtFirstname.Text & "', gender='" & ddlgender.SelectedValue.ToString & "', " & _
" dob='" & TxtDob.text & "' , Height='" & TxtHeight.Text & "' , LastName='" & Txtlastname.Text & "', " & _
"  BloodGroup='" & DdlBlood.SelectedValue.ToString & "' , Religion='" & ddlreligion.SelectedValue.ToString & "' ,Weight='" & TxtWeight.Text & "' , " & _
" CasteCategory='" & DdlCategoryc.SelectedValue.ToString & "', PassportNo='" & TxtPassport.Text & "', MaritalStatus='" & ddlmarital.SelectedValue.ToString & "'," & _
" Vision='" & DdlVision.SelectedValue.ToString & "' , Nationality = '" & TxtNationality.Text & "', Hobbies='" & TxtHobbies.Text & "' , " & _
"Caste='" & txtcaste.Text & "', AdharNo='" & txtadharno.Text & "', studentcategoryid='" & ddlstudentmode.SelectedValue.ToString & "', " & _
" Email='" & txtcorrEmail.Text & "', Mobile='" & txtcorrmobile.Text & "' WHERE     (StudentID = " & CInt(Trim(ViewState("sid").ToString)) & ")"
            cmd.execSQL(sql)
            SaralMsg.Messagebx.Alert(Me, "Successfully Save")

            If Request.QueryString("stuid") <> "" Then
                bind(" studentid= " & Request.QueryString("stuid"))

                stusearch.Visible = False

                ' Panelpersonal.Visible = False
                '  Paneldocuments.Visible = False
                ' panelconcession.Visible = False
                '  Ddlconsul.Enabled = False
                ' panelConsultancy.Visible = False


                'If Request.QueryString("scmd") = "Personal" Then
                '    Panelpersonal.Visible = True
                'ElseIf Request.QueryString("scmd") = "Documents" Then
                '    Paneldocuments.Visible = True
                'ElseIf Request.QueryString("scmd") = "Hostel" Then
                '    Panelpersonal.Visible = True
                'ElseIf Request.QueryString("scmd") = "Transport" Then
                '    Panelpersonal.Visible = True
                '    ' ElseIf Request.QueryString("scmd") = "Concession" Then
                '    '     panelconcession.Visible = True
                'ElseIf Request.QueryString("scmd") = "Consultancy" Then
                '    panelConsultancy.Visible = True
                'End If
            End If

        End If
    End Sub

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

    Protected Sub btnfamilysave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnfamilysave.Click
        Dim studentid As String = ViewState("sid").ToString
        If ViewState("sid").ToString <> "" Then
            sql = " UPDATE    Student " & _
                                "SET FatherName='" & TxtFname.Text & "',  FatherOccupation='" & ddlFOccupation.SelectedValue.ToString & "' ," & _
" EMobile='" & txtFmobile.Text & "', MotherOccupation='" & ddlMOccupation.SelectedValue.ToString & "' , MotherName='" & Txtmothername.Text & "', " & _
" Income='" & Txtincome.Text & "', MotherIncome='" & TxtMincome.Text & "', Guardian ='" & txtGaurdianname.Text & "',GuardMobile ='" & txtGaurdianmobile.Text & "'," & _
 " GuardPhone  ='" & txtGaurdianphone.Text & "', Relation='" & txtGaurdianrelation.Text & "' , GEmail = '" & txtGaurdianemail.Text & "', Domicile='" & TxtDomocile.Text & "', " & _
" FamilyIncome='" & TxtfamIncome.Text & "' WHERE     (StudentID = " & CInt(Trim(ViewState("sid").ToString)) & ")"
            cmd.execSQL(sql)
            SaralMsg.Messagebx.Alert(Me, "Successfully Save")
        End If
    End Sub

    Protected Sub btncontact_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncontact.Click
        Dim studentid As String = ViewState("sid").ToString
        If ViewState("sid").ToString <> "" Then
            sql = " UPDATE    Student " & _
                                "SET  CorrAddress1='" & textcorraddress1.Text & "' ,CorrAddress2='" & textcorraddress2.Text & "' , " & _
" CorrPinCode='" & txtcorrpin.Text & "', CorrCountry='" & ddlcorrcountry.SelectedValue.ToString & "' , CorrState='" & ddlcorrstate.SelectedValue.ToString & "' ," & _
" CorrCity='" & ddlcorrcity.SelectedValue.ToString & "'  , CorrDistt='" & ddlcordistt.SelectedValue.ToString & "' ,Phone='" & txtcorrphone.Text & "', Mobile='" & txtcorrmobile.Text & "', " & _
" PrmAddress1='" & txtprmadd1.Text & "' ,PrmAddress2='" & txtprmadd2.Text & "' , PrmPinCode='" & txtprmpin.Text & "', PrmCountry='" & ddlprmcountry.SelectedValue.ToString & "' ," & _
" PrmState='" & ddlprmsate.SelectedValue.ToString & "' , PrmCity='" & ddlprmcity.SelectedValue.ToString & "'  , PrmDistt='" & ddlprmdistt.SelectedValue.ToString & "' ," & _
"  PrmMobile='" & txtPrmMobile.Text & "' " & _
" WHERE     (StudentID = " & CInt(Trim(ViewState("sid").ToString)) & ")"
            cmd.execSQL(sql)
            SaralMsg.Messagebx.Alert(Me, "Successfully Save")
        End If
    End Sub

    Protected Sub btnaccount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnaccount.Click

        Dim studentid As String = ViewState("sid").ToString
        If ViewState("sid").ToString <> "" Then
            sql = " UPDATE    Student " & _
                                "SET Bankbranch='" & txtbranchname.Text & "', banknam='" & txtifsecode.Text & "',  BankAC='" & txtaccountno.Text & "' , BankName ='" & ddlbankname.SelectedValue.ToString & "' " & _
                                    " WHERE     (StudentID = " & CInt(Trim(ViewState("sid").ToString)) & ")"
            cmd.execSQL(sql)
            SaralMsg.Messagebx.Alert(Me, "Successfully Save")
        End If
    End Sub

    Protected Sub Lnkphotos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lnkphotos.Click
        personal.Attributes.Add("class", "Inactive")
        personal.Style.Add("color", "#15283c")
        educational.Attributes.Add("class", "Inactive")
        educational.Style.Add("color", "#15283c")
        Family.Attributes.Add("class", "Inactive")
        Family.Style.Add("color", "#15283c")
        Address.Attributes.Add("class", "Inactive")
        Address.Style.Add("color", "#15283c")
        account.Attributes.Add("class", "Inactive")
        account.Style.Add("color", "#15283c")
        UploadFile.Attributes.Add("class", "active")
        UploadFile.Style.Add("color", "lightgray")
        Pnlpersonal.Visible = False
        Pnleducation.Visible = False
        PnlFamily.Visible = False
        Pnlcontact.Visible = False
        PnlAccount.Visible = False
        Pnlphoto.Visible = True
        BINDPHOTO()

    End Sub

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

    Protected Sub backbotton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles backbotton.Click
        Response.Redirect("Studentmodify.aspx?stuid=" & ViewState("sid") & "&acyr=" & ViewState("Acyr") & "&programid=" & ViewState("crsid") & "&s=" & Request.QueryString("s") & "&e=" & Request.QueryString("e") & "&u=" & Request.QueryString("u"))

    End Sub

    Protected Sub backbutton_Click(sender As Object, e As System.EventArgs) Handles backbutton.Click
        Response.Redirect("~/UserPortal/DashboardStu.aspx")
    End Sub

    Protected Sub btnLogout_Click(sender As Object, e As System.EventArgs) Handles btnLogout.Click
        Response.Redirect("~/LoginFinal.aspx")
    End Sub

    Protected Sub btnHome_Click(sender As Object, e As System.EventArgs) Handles btnHome.Click
        Response.Redirect("~/UserPortal/DashboardStu.aspx")
    End Sub
End Class
