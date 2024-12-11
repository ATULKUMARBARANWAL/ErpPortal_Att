<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RequiredDoc.aspx.vb" Inherits="StudentAdm_RequiredDoc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"  />
    <script type="text/javascript" src="../JavaScript/jquery.min.1.7.2.js"></script>
  <style>
      
      .hiddencol
      {
          display:none;
          }
      
      .Submit
 {
     font-size: 18px !important;
      font-weight: 500;
      height: 40px;
      cursor: pointer;
      color:White;
      background-color:#1ed085;
      border:none;
      width:24%;
   }
   .Submit:hover
   {
       color:White;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
    }
      
    body
        {
            overflow-x: hidden;
            background-color: #f2f3f5;
            background-repeat: no-repeat;
            background-size: 100% 100%;
        }
     
         body::-webkit-scrollbar {
            display: none;
        }
   .maincontainer {
    border: 2px solid #fff;
    padding: 10px;
    background-color:#fff;
    border-radius: 6px;
    text-align: left;
   }
    .viewprofile
    {
   color:gray;
   font-size: 18px !important;
   cursor: pointer;
   font-weight: 500;
   border:none;
       }
   
  .viewprofile:hover {
   color:black;
   font-weight: 600;
    } 
     .communicatesub
{
    background-color:#1ed085;
    color:#f2f3f5;
    font-weight:500;
    }
 .communicatesub:hover
{
    background-color:#1ed085;
    color:#f2f3f5;
    font-weight:500;
    border:1px solid #1ed085;
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

 .DownloadExcel
   {
   color:gray;
   font-size: 22px !important;
   cursor: pointer;
   font-weight: 500;
   border:none;
       }
   
 .DownloadExcel:hover {
   color:black;
   font-weight: 600;
    } 
   .CourseName
   {
       color:#1ed085;
       text-decoration:none;
       font-weight:500;
       font-size:20px;
    }
     .CourseName:hover
   {
       color:#20a16a;
       text-decoration:none;
       font-weight:400;
    }
     .CourseName1
   {
       color:#1ed085;
       text-decoration:none;
       font-weight:500;
       
    }
     .CourseName1:hover
   {
       color:#20a16a;
       text-decoration:none;
       font-weight:400;
    }
  .line
   {
       content:" ";
       width:100%;
       height:1px;
       background-color:#e1e2e3;
       margin-top:8px;
       margin-bottom:8px;
       
   }

.Submit
 {
     font-size: 18px !important;
      font-weight: 500;
      height: 40px;
      cursor: pointer;
      color:White;
      background-color:#1ed085;
      border:none;
    width:11%;
   }
   .Submit:hover
   {
       color:White;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
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
   margin-left:200px;
    }
    .addsubject:hover
    {
   color:#fff;
   background-color:#15283c;
   text-decoration:none;
    }
.card-header
 {
     background-color:#152837;
     color:#fff;
     font-weight:500;
     font-size:22px;
     letter-spacing:1px;
     }
    .SubjectName
   {
       color:#1ed085;
       text-decoration:none;
       font-weight:400;
       font-size:16px
    }
.SubjectName:hover
   {
       color:#20a16a;
       text-decoration:none;
       font-weight:400;
    }
    .modal-header {
  
  text-align: left;
  font-size: 22px;
  color: #f2f3f5;
  
  background-color: #152837;
  border-bottom: 0px;
  height:50px;
}


  #backbotton
{
    font-size:24px;
    font-weight:600;
    color:#7c858f;
    margin-left:5px;
}
#backbotton:hover
{
    color:#15283c;
}
.heading
{
    font-size:20px;
    color:#15283c;
    font-weight:500;
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


                    row.style.backgroundColor = "white";

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
    <div class="container-fluid maincontainer">
    <asp:Panel ID="pnlRequired"  runat="server" Visible="true">
    <div class="container-fluid maincontainer">
     <div class="row">
    <div class="col-md-6">
    <div class="heading1">
      <h5>Required Document</h5>
    </div>
    </div> 

    <div class="col-md-6 text-end mt-2">
           <h6><span class="">Academic Year : <span class="">
             <asp:Label ID="lblacademicyear" runat="server" CssClass="fw-bold" Text="N/A"></asp:Label></span>
             </span></h6> 
     </div>

   
    </div>
                     
     <div class="row">
    <div class="col-md-12">
    <div class="line">
    </div>
    </div>
    </div>
 
     <div class="row">
<div class="col-md-12">
<asp:GridView ID="grdsearch" GridLines="Both" DataKeyNames="Courseid,Reqdocid" AutoGenerateColumns="False" 
runat="server" CellPadding="0" CssClass="table table-bordered" ShowHeaderWhenEmpty="true">
<Columns>

<asp:TemplateField HeaderText="S.No.">
<ItemTemplate>
<%#Container.DataItemIndex+1 %>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HeaderText="Program Id" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" DataField="Courseid"></asp:BoundField>
<asp:BoundField HeaderText="Program Code" DataField="Coursecode"></asp:BoundField>
<asp:BoundField HeaderText="Program" DataField="Course"></asp:BoundField>
  <asp:BoundField HeaderText="Docid" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" DataField="docid"></asp:BoundField>
  <asp:TemplateField HeaderText="Documents">
    <ItemTemplate >
        <asp:Label ID="lbldoc" runat="server" ></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>


    <asp:TemplateField HeaderText="Documents">
    <ItemTemplate >
    <asp:LinkButton  ID="Documents" runat="server" CommandName="Documents"
    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="SubjectName" ><i class="fa fa-file"></i></asp:LinkButton>       
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Copy To">
    <ItemTemplate >
    <asp:LinkButton  ID="Documentsco" runat="server" CommandName="Documetsco"
    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="SubjectName" ><i class="fa-solid fa-copy"></i></asp:LinkButton>       
    </ItemTemplate>
    </asp:TemplateField>                 
</Columns>
           
</asp:GridView>
</div>
</div>                                                   
     </div>                     
    </asp:Panel>

    <asp:Panel ID="paneldocuments"  runat="server" Visible="false">
    
        
        <div class="row">
        <div class="col-md-6">
        <table>
        <tr width="100%">
        <td width="6%">
        &nbsp;
         <asp:LinkButton ID="btnback" class="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
        </td>
        <td width="44%" >
          
          <asp:Label ID="LblStName" class="heading" runat="server" Text="Document(s) for"></asp:Label>  <asp:Label ID="Label1" class="h4" runat="server" Text="("></asp:Label><asp:Label ID="lblcourse" runat="server" class="heading" Text=""></asp:Label>
          <asp:Label ID="Label60" class="h4" runat="server" Text=")"></asp:Label>
        </td>
        </tr>
        </table>
        </div> 
        <div class="col-md-6 text-end">
         <asp:LinkButton ID="btnAddDoc" runat="server" CssClass="btn addsubject" ><i class="fa fa-plus"></i> Add Document</asp:LinkButton>
        </div>
       </div>
       
 <div class="row">
    <div class="col-md-12">
    <div class="line">
    </div>
    </div>
    </div>

     
         
       <div class="row">
     <div class="col-md-12">
        <asp:GridView ID="GridDocList" GridLines="Both"  DataKeyNames="" AutoGenerateColumns="False" runat="server" CellPadding="0" 
        ShowHeaderWhenEmpty="true" CssClass="table table-bordered">
        <Columns>
        <asp:TemplateField HeaderText="S.No.">
        <HeaderTemplate>
        <asp:CheckBox ID="CheckBox1" runat="server"  Onclick="javascript:SelectheaderCheckboxes(this)" />
        </HeaderTemplate>
        <ItemTemplate>
        <asp:CheckBox ID="btnselect" runat="server" />
        </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="S.No.">
        <ItemTemplate>
        <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
        </asp:TemplateField>          
        
         <asp:BoundField HeaderText="Document id" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" DataField="EssentialDocID"></asp:BoundField>
        <asp:BoundField HeaderText="Document Name" DataField="EssentialDoc"></asp:BoundField>
                     
        </Columns>
           
        </asp:GridView>
     </div>
     </div>


     <div class="row">
     <div class="col-md-12 text-center">

      <asp:Button ID="btnAddoc" class="btn Submit" runat="server" Text="Save"></asp:Button> 

      </div>
      </div>

      

        </asp:Panel>

    <asp:Panel ID="panelcopyprogram"  runat="server" Visible="false">
   
       <div class="row">
      <div class="col-md-12">
        <table width="100%">
        <tr width="100%">
        <td width="4%">
         <asp:LinkButton ID="btnbackcopy" class="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
        </td>
        <td width="96%">
            <h5 class="program">Copy to Program's</h5>
        </td>
        </tr>
        </table>
      </div>
      </div>

      <div class="row">
      <div class="col-md-12">
      <div class="line"></div>
      </div>
      </div>
         
       <div class="row">
     <div class="col-md-12">
        <asp:GridView ID="Grdcopy" GridLines="Both"  DataKeyNames="" AutoGenerateColumns="False" runat="server" CellPadding="0" 
        ShowHeaderWhenEmpty="true" CssClass="table table-bordered">
        <Columns>
        <asp:TemplateField HeaderText="S.No.">
        <HeaderTemplate>
        <asp:CheckBox ID="CheckBox1" runat="server"  Onclick="javascript:SelectheaderCheckboxes(this)" />
        </HeaderTemplate>
        <ItemTemplate>
        <asp:CheckBox ID="btnselect" runat="server" />
        </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="S.No.">
        <ItemTemplate>
        <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
        </asp:TemplateField>          
        <asp:BoundField HeaderText="Program ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" DataField="Courseid"></asp:BoundField>
        <asp:BoundField HeaderText="Program Code" DataField="Coursecode"></asp:BoundField>
        <asp:BoundField HeaderText="Program Name" DataField="Course"></asp:BoundField>
                     
        </Columns>
           
        </asp:GridView>
     </div>
     </div>  


     <div class="row">
     <div class="col-md-12 text-center">

      <asp:Button ID="btncopydoc" class="btn Submit" runat="server" Text="Copy"></asp:Button> 

      </div>
      </div>
       
    
    </asp:Panel>

    <asp:Panel ID="pnlmaster"  runat="server" Visible="false">
      <div class="container-fluid">
 
       <div class="row justify-content-center">
        <div class="col-md-12">
        <div class="card">
        <div class="card-header">
        <table width="100%">
        <tr width="100%">
        <td width="6%">
         <asp:LinkButton ID="btnbackmaster" class="backbotton1 text-white" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
        </td>
        <td width="94%">
            <h5 class="program">Document Master</h5>
        </td>
        </tr>
        </table>
            
       </div>
       
       <div class="card-body">
         

         <div class="row">
          <div class="col-md-2"></div> 
       <div class="col-md-2 text-end"> <label>Document Name : </label></div>
                            
                        <div class="col-md-6">
                         <asp:TextBox ID="txtdocname" runat="server" class="form-control" Placeholder=""></asp:TextBox>
                         </div>  
        
        <div class="col-md-2"></div>            
      </div>

      <div class="row mt-3 mb-1">
         <div class="col-md-12 ">
                    <asp:LinkButton ID="LinkButton2" class="btn btn-outline-dark mr-auto btncanelg" data-bs-dismiss="modal" runat="server"><i class="fa fa-window-close"></i> Cancel</asp:LinkButton>&nbsp;&nbsp;
                    <asp:Button ID="btnsubmit" class="btn Submit" runat="server" Text="Add" Width="14%"></asp:Button>  
                    <asp:Button ID="btnupdate" class="btn Submit" runat="server" Visible="false" Text="Update" Width="14%"></asp:Button>  
          </div>
          </div>

          <div class="row">
          <div class="col-md-12">
          <div class="line"></div>
          </div>
          </div>

            <div class="row mt-2"> 
        <div class="col-md-12">
        <asp:Panel ID="Panel3" runat="server" ScrollBars="auto" Visible="true">
            <asp:GridView ID="GrdDocument"   OnRowEditing="OnRowEditing" AutoGenerateColumns="False" runat="server" ShowHeaderWhenEmpty="true" DataKeyNames="Essentialdocid"
            Class="table table-bordered">
           <Columns>
           
        <asp:TemplateField HeaderText="Modify">
                   <ItemTemplate>
                       <asp:LinkButton ID="lnkModify"  CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CommandName="Edit"  runat="server" ><i class="fa-solid fa-pen-to-square" style="color: #27aa48;"></i></asp:LinkButton>
                      </ItemTemplate>
                       <%--<EditItemTemplate>
                   <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="Update"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel"/>
                   </EditItemTemplate>--%>
             </asp:TemplateField>
         <asp:TemplateField HeaderText="S.No.">
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
       </asp:TemplateField>
                <asp:BoundField HeaderText="DocumentID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" DataField="EssentialDocid"></asp:BoundField>
                <asp:BoundField HeaderText="Document Name" ReadOnly="true" DataField="EssentialDoc"></asp:BoundField>
            <asp:TemplateField HeaderText="Delete" Visible="false">
                   <ItemTemplate>
                    <asp:ImageButton ID="lnkDelete" Visible="false" CommandName="Delete" CausesValidation="False" ImageUrl="../ExaminationNImages/img/url.png"                           
                    CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'  Width="20px" OnClientClick="return confirm('Really!Do u want to Delete')" runat="server" />
                   </ItemTemplate>                 
             </asp:TemplateField>
            </Columns>
           <EditRowStyle Forecolor="#fff" />
          
         </asp:GridView>
         </asp:Panel>    
    </div>
               
    <div class="modal-footer">
         
           
   
    <asp:Button ID="Button3" class="btn communicatesub" Visible=false Width="15%" runat="server"  Text="Save" />   
    </div>
          
    </div>


       </div>

      </div>
      </div>
      </div>
  
    </div>
    </asp:Panel>

    

    </form>
</body>
</html>
