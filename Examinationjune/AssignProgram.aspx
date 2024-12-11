<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AssignProgram.aspx.vb" Inherits="Examinationjune_AssignProgram" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%--------------------------------------------------- Designed By Shivani Pankaj ---------------------------------------%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
    <style type="text/css" >
    body
    {
        background:#f2f3f5;
    }
   .maincontainer {
    border: 2px solid #fff;
    padding: 10px;
    background-color:#fff;
    border-radius: 6px;
    text-align: left;
   }
   
    .maincontainerm
{
    background-color:#f2f3f5;
    color:#15283c; 
    font-size:16px; 
    width:42px;
    height:41px ;
    border-radius:50%;
    padding-top:8px;
  }
   .line
   {
       content:" ";
       width:100%;
       height:1px;
       background-color:#e1e2e3;
       margin-top:4px;
       margin-bottom:4px;
       
   }
    .btnAddProgram
{
    background-color:#1ed085;
    color:#f2f3f5;
    font-weight:500;
    }
 .btnAddProgram:hover
{
    background-color:#1ed085;
    color:#f2f3f5;
    font-weight:500;
    border:1px solid #1ed085;
    }
     .btnaddDecription
{
    background-color:#808080;
    color:#f2f3f5;
    font-weight:600;
    font-size:18px;
   text-align:center;
   border-radius:50%;
    }
 .btnaddDecription:hover
{
    background-color:#000;
    color:#f2f3f5;
    font-weight:500;
    }
  .DownloadExcel
   {
   color:#808080;
   font-size: 22px !important;
   cursor: pointer;
   font-weight: 500;
   border:none;
       }
   
  .DownloadExcel:hover {
   color:black;
   font-weight: 600;
    } 
 #DownloadSubject
 {
   color:#808080;
   font-size: 22px !important;
   cursor: pointer;
   font-weight: 500;
   border:none;
       }
   
  #DownloadSubject:hover {
   color:black;
   font-weight: 600;
    } 
 #DdownloadUnitName
 {
   color:#808080;
   font-size: 22px !important;
   cursor: pointer;
   font-weight: 500;
   border:none;
       }
   
  #DdownloadUnitName:hover {
   color:black;
   font-weight: 600;
    } 
    #backtoprogram
  {
    font-size:24px;
    font-weight:600;
    color:#7c858f;
}
#backtoprogram:hover
{
    color:#15283c;
}
#backtocourseSubject{
    font-size:24px;
    font-weight:600;
    color:#7c858f;
    
}
#backtocourseSubject:hover
{
    color:#15283c;
}

 .communicatesub
{
    background-color:#1ed085;
    color:#f2f3f5;
    font-weight:500;
    border:none;
    }
 .communicatesub:hover
{
       color:#fff;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
       }
  .btncanelg
  {
      margin-left:270px;
      }
    .modal-header {
  
  text-align: left;
  font-size: 22px;
  color: #f2f3f5;
  
  background-color: #152837;
  border-bottom: 0px;
  height:50px;
}
.SubjectName
   {
       color:#1ed085;
       text-decoration:none;
       font-weight:400;
    }
.SubjectName:hover
   {
       color:#20a16a;
       text-decoration:none;
       font-weight:400;
    }
    .hiddencol
    {
        display:none;
    }
    .ddlSemyear
    {
        border:1px solid gray;
        border-radius:3px;
        
    }
     .backbotton
{
    font-size:22px;
    font-weight:600;
    color:#7c858f;
    
}

.backbotton:hover
{
    color:#15283c;
}
 .addsubject
    {
   color:#15283c;
   font-size: 18px !important;
   cursor: pointer;
   font-weight: 500;
   border:1px solid #15283c; 
   background-color:#fff;
   text-decoration:none;
   text-align :center;
   border-radius:4px;
   padding:3px 10px;
    }
    .addsubject:hover
    {
   color:#fff;
   background-color:#15283c;
   text-decoration:none;
    }
     .custom-scrollbar-css 
    {
        min-height:0vh;
 max-height:65vh;
}
    .custom-scrollbar-css {
  overflow-y: scroll;
}

/* scrollbar width */
.custom-scrollbar-css::-webkit-scrollbar {
  width: 5px;
}

/* scrollbar track */
.custom-scrollbar-css::-webkit-scrollbar-track {
  background: #eee;
}

/* scrollbar handle */
.custom-scrollbar-css::-webkit-scrollbar-thumb {
  border-radius: 2rem;
  background-color: #00d2ff;
  background-image: linear-gradient(to top, #000 0%, #808080 100%);
}
 .hiddencol
    {
        display:none;
    }
     .Submit
 {
     font-size: 18px !important;
      font-weight: 500;
      text-align:center;
      cursor: pointer;
      color:#fff;
      background-color:#1ed085;
      border:none;
      padding:3px 10px;
      width:18%;
      
   }
   .Submit:hover
   {
       color:#fff;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
    }

    </style>
    <script>
        function SelectheaderCheckboxes(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {



                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {


                        row.style.backgroundColor = "#f2f3f5";

                        inputList[i].checked = true;

                    }

                    else {

                        if (row.rowIndex % 2 == 0) {
                        }

                        else {

                            row.style.backgroundColor = "white";

                        }

                        inputList[i].checked = false;

                    }

                }

            }

        }
 
</script>
</head>
<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
  </asp:ScriptManager>
   

   <asp:Panel ID="Panel1" Visible="true" runat="server">
     <div class="container-fluid maincontainer">
                     
         <asp:DropDownList ID="DropDownList1" AutoPostBack="true" Visible="false" runat="server">
         <asp:ListItem >2021</asp:ListItem>
         </asp:DropDownList>
        
         <div class="row mt-0">
             <div class="col-md-8">
                 <table width="100%">
                     <tr width="100%">
                         <td width="5%">
                             <div class="heading1 d-flex">
                                 <asp:LinkButton ID="backbotton" class="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>                                
                             </div>
                         </td>                        
                         <td width="95%">
                              <h5>Program : <asp:Label ID="lblProgramName" runat="server"></asp:Label></h5> 
                         </td>                        
                     </tr>
                 </table>
             </div>
             <div class="col-md-4 text-end">
                 <h6>
                     <span class="">Academic Year : <span class="">
                         <asp:Label ID="lblacedmic" runat="server" CssClass="fw-bold" Text="N/A"></asp:Label></span>
                     </span>
                 </h6>
             </div>
         </div> 

         <div class="row">
             <div class="col-md-12">
                 <div class="line">
                 </div>
             </div>
         </div>

         <div class="row mt-0">
            <div class="col-md-4 d-flex">
                          
                            
                          </div>
                          <div class="col-md-3"></div>
             <div class="col-md-5">
                 <table width="100%">
                     <tr width="100%">
                         <td width="23%" align="center">
                         <label class="fw-bold">Search :</label>
                         </td>
                         <td width="70%">
                             <asp:TextBox ID="TextBox1" Placeholder="Search by subject" class="form-control"
                                 runat="server"></asp:TextBox>
                  
                         </td>
                         <td width="7%" align="center">
                         <asp:LinkButton ID="LinkButton2" runat="server" CssClass="DownloadExcel"> 
                         <i class="fa fa-download fa-2"></i></asp:LinkButton>
                         </td>
                     </tr>
                 </table>
                             
             </div>
         </div>

         <div class="row mt-1">
                 <div class="col-md-12">
                     <div class="custom-scrollbar-css">
                         <asp:GridView ID="GridView1" GridLines="Both" Width="100%" AutoGenerateColumns="False"
                             runat="server" CellPadding="0" ShowHeaderWhenEmpty="true" CssClass="table table-bordered text-center">
                             <Columns>
                                 <asp:TemplateField HeaderText="S.No.">
                                     <ItemTemplate>
                                         <%#Container.DataItemIndex+1 %>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:BoundField HeaderText="CourseID" ItemStyle-Width="0px" DataField="CourseID" ItemStyle-CssClass="hiddencol"
                                     HeaderStyle-CssClass="hiddencol" ReadOnly="true">                                
                                 </asp:BoundField>
                                  <asp:BoundField HeaderText="Academicyear" ItemStyle-Width="0px" DataField="Academicyear"
                                   ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" ReadOnly="true">
                           </asp:BoundField>
                                <asp:BoundField HeaderText="Sem/Year" ItemStyle-Width="0px" DataField="Totalsem"
                                   ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol"   ReadOnly="true">
                                 </asp:BoundField>
                                      
                                  <asp:TemplateField HeaderText="Sem/Year">
                         <ItemTemplate>
                             <asp:LinkButton Text='<%# Eval("Totalsem") %>' ID="namelinkes" runat="server" CommandName="Studentlist"
                                 CssClass="CourseName" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:LinkButton>
                         </ItemTemplate>
                     </asp:TemplateField>
                                 <asp:TemplateField HeaderText="NoofAssignment">
                                     <ItemTemplate>
                                     <asp:DropDownList ID="ddlAssignment" runat="server" Width="220px">
                                       <asp:ListItem>1</asp:ListItem>
                                       <asp:ListItem>2</asp:ListItem>
                                       <asp:ListItem>3</asp:ListItem>
                                       <asp:ListItem>4</asp:ListItem>
                                       <asp:ListItem>5</asp:ListItem>
                                     </asp:DropDownList>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                             </Columns>
                         </asp:GridView>
                     </div>
                 </div>
             </div>

         <div class="row">
         <div class="col-md-12 text-center">
          <asp:Button ID="btnsavesubject" class="btn Submit" runat="server" Text="Assign Assignment"></asp:Button>
         </div>
         </div>
    
    </div>
    </asp:Panel>
  </form>
</body>
</html>