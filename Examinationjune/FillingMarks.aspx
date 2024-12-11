<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FillingMarks.aspx.vb" Inherits="Examinationjune_FillingMarks" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    
 <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
 
    <link href="../ExaminationNCSS/FillingMarks.css" rel="stylesheet" type="text/css" />
    <style type="text/css" >
    .hiddencol
    {
       
    }
    .lblsemyear
    {
        font-size:18px;
        font-weight:400;
    }
     .clsx
   {
       color:#ffd166;
       text-decoration:none;
       font-size:22px;
       font-weight:400;
    }
.clsx:hover
   {
       color:#d4ac4e;
       text-decoration:none;
       font-weight:400;
    }
     .clsy
   {
       color:#1ed085;
       text-decoration:none;
       font-size:22px;
       font-weight:400;
    }
.clsy:hover
   {
       color:#1aad6f;
       text-decoration:none;
       font-weight:400;
    }
    .hiddencol
    {
        display:none;
    }
    .error
    {
        color: Red;
        display: none;
    }
    .subheading1
    {
            padding-top:14px;
    }
    .subheading1 h3
    {
       color:#15283c;
    font-size:20px;
    font-weight:400;
     
    } 
    </style>
    <script type="text/javascript">
        function Validate() {
            //Reference the GridView.
            var grid = document.getElementById("<%=Gridstudentmarks.ClientID %>");

            //Reference all INPUT elements.
            var inputs = grid.getElementsByTagName("INPUT");

            //Set the Validation Flag to True.
            var isValid = true;
            for (var i = 0; i < inputs.length; i++) {
                //If TextBox.
                if (inputs[i].type == "text") {
                    //Reference the Error Label.
                    var label = inputs[i].parentNode.getElementsByTagName("SPAN")[0];

                    //If Blank, display Error Label.
                    if (inputs[i].value == "") {
                        label.style.display = "block";
                        isValid = false;
                    } else {
                        label.style.display = "none";
                    }
                }
            }

            return isValid;
        }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid maincontainer">
    <div class="row">
    <div class="col-md-6">
      <div class="heading1 d-flex">
       <asp:LinkButton ID="backbotton2" class="backbotton" Visible="false"  runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
         
                       <asp:LinkButton ID="LinkButton1" class="backbotton" Visible="false"  runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
                <asp:LinkButton ID="backbotton" class="backbotton" Visible="false" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
                &nbsp &nbsp &nbsp
                                    <h3>
                                        <asp:Label ID="Lbleheading1" Visible="true"  runat="server" Text="Marks Enter"></asp:Label>

                                        </h3>
                                 </div>
      
    
    </div>
    <div class="col-md-6 d-flex justify-content-end">
    <div class="subheading1">
    <h3>
    <asp:Label ID="lblname" class="lblacdmic" runat="server" Text="Academic Year : "></asp:Label>
               &nbsp &nbsp<asp:Label ID="lblacyr" class="lblacdmic" runat="server" Text=""></asp:Label>&nbsp
              
                                        </h3>
                                        </div>
                               
    </div>
    <div class="col-md-12">
    <div class="line">
         </div>
    </div>
        <asp:Panel ID="PnlExamlist" Visible="True" runat="server">
        
    <div class="row">
                          <div class="col-md-7">
                             <div class="subheading d-flex">
                                
                <h5>Exam Subject list </h5>
                                 </div> 
                             
                          </div>
                          
                          
                          <div class="col-md-5 text-end">
                              <span class=""><span class="">
                             
                                  <asp:DropDownList ID="DdlAcademicyear" visible="false"  runat="server">
                                  </asp:DropDownList> </span> </span> 
                          </div>
                       </div>
    
                       <div class="row mt-2">
          <div class="col-md-12  custom-scrollbar-css">
          
              <asp:GridView ID="GridlistExams" class="table table-bordered " AutoGenerateColumns="false"  runat="server">
              <Columns >
              

                             <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="10%">
                           <ItemTemplate>
                           <%#Container.DataItemIndex+1 %>
                           </ItemTemplate>
                          </asp:TemplateField>
                           <asp:BoundField HeaderText="academic year" ItemStyle-Width="0px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true" DataField="Academicyear"></asp:BoundField>
                          <asp:BoundField HeaderText="Examsub id" ItemStyle-Width="0px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"  DataField="Examsubid"></asp:BoundField>
                          <asp:BoundField HeaderText="Courseid" ItemStyle-Width="0px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true" DataField="Courseid"></asp:BoundField>
                          <asp:BoundField HeaderText="Semyear" ItemStyle-Width="0px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true" DataField="Semyear"></asp:BoundField>
                          <asp:BoundField HeaderText="Program" DataField= "Course"></asp:BoundField>
                         
                          <asp:BoundField HeaderText="Subjectid" ItemStyle-Width="0px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true" DataField= "Sujectid"></asp:BoundField>
                          
                          <asp:BoundField HeaderText="Subject code" DataField= "Subjectcode"></asp:BoundField>
                         
                           <asp:BoundField HeaderText="Subject" DataField= "Subject"></asp:BoundField>
                         
                           <asp:TemplateField HeaderText="Marks">
                          <ItemTemplate >
                          <asp:LinkButton ID="namelink" runat="server" CommandName="MarksEnter"
                          CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="clsx"><i class="fa-solid fa-circle-plus "></i></asp:LinkButton>       
                          <asp:LinkButton ID="btncheck" runat="server" Visible="false" CommandName="MarksEnter"
                          CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="clsy"><i class="fa-solid fa-circle-check"></i></asp:LinkButton>       
                         
                           </ItemTemplate>
                           </asp:TemplateField>
                           
                           <asp:BoundField ItemStyle-Width="0px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true" HeaderText="Courseexamid" DataField= "Courseexamid"></asp:BoundField>
                         
                            <asp:BoundField HeaderText="Program code" ItemStyle-Width="0px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true" DataField= "Coursecode"></asp:BoundField>
                          
              </Columns>
              </asp:GridView>
           </div>
     
      </div>

      </asp:Panel>


      <asp:Panel ID="Pnlstudentmarks" Visible="false" runat="server">
        
    <div class="row mt-2">
                          <div class="col-md-7">
                              <div class="subheading d-flex">
                               
                <h5>
                    <asp:Label ID="Lblprogram" runat="server" Text="N/A"></asp:Label> (&nbsp<asp:Label ID="Lblsubject" runat="server" Text="N/A"></asp:Label>&nbsp)</h5>
                                 </div>
                             
                          </div>
                          
                          
                          <div class="col-md-5 d-flex justify-content-end">
                             <div class="subheading d-flex">
                <h5>
                    </h5> &nbsp &nbsp
                                 </div>
                              <div class="hiencol"><span class="">
                             
                                  <asp:DropDownList ID="Ddlpnl2academicyear" Visible="false" runat="server">
                                  </asp:DropDownList> </span>
                                   </div> 
                          </div>
                       </div>

                       <div class="row mt-2">
                          <div class="col-md-7">
                              <asp:Label ID="Lblexamsubid" Visible="false"  runat="server" Text="Label"></asp:Label>
                              <asp:Label ID="Lblacademicyear" Visible="false" runat="server" Text="Label"></asp:Label>
                             
                          </div>
                          
                          
                          <div class="col-md-5 ">
                             
                             </div>
                       </div>
                       
    
                       <div class="row mt-2">
          <div class="col-md-12  custom-scrollbar-css">
              <asp:Panel ID="Pnlstudentmarksinsert" runat="server">
              
              <asp:GridView ID="Gridstudentmarks" class="table table-bordered " AutoGenerateColumns="false"  runat="server">
              <Columns >
              

                             <asp:TemplateField HeaderText="Sr.No.">
                           <ItemTemplate>
                           <%#Container.DataItemIndex+1 %>
                           </ItemTemplate>
                          </asp:TemplateField>
                           <asp:BoundField HeaderText="academic year" ItemStyle-Width="0px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"  DataField="Academicyear"></asp:BoundField>
                          <asp:BoundField HeaderText="Examsub id"  DataField="Examsubid" ItemStyle-Width="0px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>
                          <asp:BoundField HeaderText="Student id"  DataField="StudentID" ItemStyle-Width="0px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>
                          <asp:BoundField HeaderText="RollNo"  DataField="Roll_No" ></asp:BoundField>
                          <asp:BoundField HeaderText="Admission No"  DataField="Admissionno" ></asp:BoundField>
                          <asp:BoundField HeaderText="Student"  DataField="Firstname" ></asp:BoundField>
                          <asp:TemplateField HeaderText="Attendance">
                          <ItemTemplate >
                              <asp:DropDownList ID="ddlattendance" runat="server">
                              <asp:ListItem Text="Present">Present</asp:ListItem>
                              <asp:ListItem Text="Absent">Absent</asp:ListItem>
                              </asp:DropDownList>
                               </ItemTemplate>
                           
                           </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Paper Id" >
                          <ItemTemplate >
                                <%--<asp:TextBox ID="txtmaxmarks" runat="server" CssClass="txtbox">
                                      </asp:TextBox>--%>
                                      <asp:TextBox ID="txtpaperid" runat="server" CssClass="txtbox">
                                      </asp:TextBox>
                           </ItemTemplate>
                           
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Answer Sheet No">
                          <ItemTemplate >
                                <asp:TextBox ID="txtanswershet" runat="server" CssClass="txtbox">
                                      </asp:TextBox>
                           </ItemTemplate>
                           
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ob. Marks">
                          <ItemTemplate >
                                <asp:TextBox ID="txtobtainmarks" runat="server" CssClass="txtbox">
                                      </asp:TextBox>
                           </ItemTemplate>
                           
                           </asp:TemplateField>
                                
                            
                         
                          
              </Columns>
              </asp:GridView>
              </asp:Panel>

            <asp:Panel ID="Pnlstudentmarksupdate" runat="server">
              <asp:GridView ID="Gridstudentupdatemarks" class="table table-bordered " 
               OnRowDataBound="OnRowDataBound" OnRowEditing="OnRowEditing" OnRowDeleting="OnRowDeleting"
           OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating"  DataKeyNames="ExamfillmarksheetId" AutoGenerateColumns="false"  runat="server">
              <Columns >
              
              <asp:TemplateField HeaderText="Modify" ItemStyle-Width="0px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                          <ItemTemplate>
                          <asp:LinkButton ID="lnkModify" CommandName="Edit"  runat="server" ><i class="fa-solid fa-pen-to-square" style="color: #27aa48;"></i></asp:LinkButton>     
                           </ItemTemplate>
                            <EditItemTemplate>
                   <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="Update"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel"/>
                   </EditItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField HeaderText="Examfillingid" DataField="ExamfillmarksheetId" ItemStyle-Width="0px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>
                          
                             <asp:TemplateField HeaderText="Sr.No.">
                           <ItemTemplate>
                           <%#Container.DataItemIndex+1 %>
                           </ItemTemplate>
                          </asp:TemplateField>
                          
                           <asp:BoundField HeaderText="academic year" DataField="Academicyear" ItemStyle-Width="0px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>
                          <asp:BoundField HeaderText="Examsub id"  DataField="Examsubid" ItemStyle-Width="0px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>
                          <asp:BoundField HeaderText="Student id" DataField="StudentId" ItemStyle-Width="0px" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true"></asp:BoundField>
                         <asp:BoundField HeaderText="Admission No" DataField="Admissionno" ></asp:BoundField>
                          <asp:BoundField HeaderText="RollNo" DataField="Roll_No" ></asp:BoundField>
                          <asp:TemplateField HeaderText="Attendance">
                          <ItemTemplate >
                              <asp:DropDownList ID="ddlattendance" runat="server">
                              <asp:ListItem Text="Present">Present</asp:ListItem>
                              <asp:ListItem Text="Absent">Absent</asp:ListItem>
                              </asp:DropDownList>
                               </ItemTemplate>
                           
                           </asp:TemplateField>
                          <asp:BoundField HeaderText="Paperid"  DataField="paperid"></asp:BoundField>
                          <asp:BoundField HeaderText="Answersheet No"  DataField="AnswersheetNo"></asp:BoundField>
                          <asp:BoundField HeaderText="Obtained Marks"  DataField="Obtainedmarks"></asp:BoundField>
                               <%--       <asp:TemplateField HeaderText="Paper Id" >
                          <ItemTemplate >
                                      <asp:TextBox ID="txtpaperid" runat="server" CssClass="txtbox">
                                      </asp:TextBox>  <span class="error">*</span>
                           </ItemTemplate>
                           
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Answer Sheet No">
                          <ItemTemplate >
                                <asp:TextBox ID="txtanswershet" runat="server" CssClass="txtbox">
                                      </asp:TextBox>  <span class="error">*</span>
                           </ItemTemplate>
                           
                           </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ob. Marks">
                          <ItemTemplate >
                                <asp:TextBox ID="txtobtainmarks" runat="server" CssClass="txtbox">
                                      </asp:TextBox>  <span class="error">*</span>
                           </ItemTemplate>
                           
                           </asp:TemplateField>
                               --%> 
                            
                         
                          
              </Columns>
              </asp:GridView>
              </asp:Panel> 
           </div>
     
      </div>

          <div class="row">
                       <div class="col-md-12 d-flex justify-content-center">
                        <asp:Button ID="btnsaves"  OnClientClick="return Validate()" class="btn Submit" runat="server" Text="Save"></asp:Button>
          
                       </div>
                       </div>
   

      </asp:Panel>

                       
    </div>
    
    </div>
    </form>
</body>
</html>
