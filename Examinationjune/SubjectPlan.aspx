<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SubjectPlan.aspx.vb" Inherits="Examinationjune_SubjectPlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../Bootstrap5/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Bootstrap5/js/bootstrap.min.js" type="text/javascript"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" integrity="sha512-iecdLmaskl7CVkqkXNQ/ZH/XLlvWZOJyj7Yy7tcenmpD1ypASozpmT/E0iPtmFIB46ZmdtAc9eNBvH0H/ZpiBw==" crossorigin="anonymous" referrerpolicy="no-referrer" />

<style>

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
 .Submit
 {
     font-size: 18px !important;
      font-weight: 500;
      
      cursor: pointer;
      color:#fff;
      background-color:#1ed085;
      border:none;
      width:20%;
   }
   .Submit:hover
   {
       color:#fff;
       background-color:#1aad6f;
       border:none;
       box-shadow:0px 1px 5px 1px #dcdcdc;
    }



 #Panel1
 {
     margin-top:50px;
     }
     
     
      #Panel2
 {
     margin-top:50px;
     }

          .maincontainerm
{
    background-color:#f2f3f5;
    color:#15283c; 
    font-size:16px; 
    width:40px;
    height:40px ;
    border-radius:50%;
    padding-top:11px;
    
    margin-left:50px;
  }
#btnlefttransfer
{
    font-size:22px;
    font-weight:600;
    color:#7c858f;
   
}
#btnlefttransfer:hover
{
    color:#15283c;
}

    .maincontainerr
{
    background-color:#f2f3f5;
    color:#15283c; 
    font-size:16px; 
    width:40px;
    height:40px ;
    border-radius:50%;
    padding-top:11px;
    margin-top:70px;
    margin-left:50px;
  }
#btnRighttransfer
{
    font-size:22px;
    font-weight:600;
    color:#7c858f;
   
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
#btnRighttransfer:hover
{
    color:#15283c;
}

#backbotton
{
     font-size:22px;
    font-weight:600;
    color:#7c858f;
   
    }

    #backbotton:hover
{
     color:#15283c;
    }

.hiddencol
{
    display:none;
    }



    body 
{
    background-color:#f2f3f5;
}
.maincontainer
{
     background-color:#fff;
    border:1px solid #e1e2e3;
    border-radius:8px;
    box-shadow:0 2px 10px 2px #e1e2e3;
    padding-bottom:24px;
}
 .heading1
{
    padding-top:2px;
    }
  .heading1 h3
{
    color:#15283c;
    font-size:20px;
    font-weight:600;
   
    }
    
    .subheading
{
    padding-top:7px;
    }
  .subheading h3
{
    color:#15283c;
    font-size:24px;
    font-weight:400;
    }
    
     .line
   {
       content:" ";
       width:100%;
       height:1px;
        background-color:#e1e2e3;
        margin-top:2px;
        margin-bottom:5px;
        
       
   }
   
   #btncopy
   {
       color:#7c858f;
       }

     #btncopy:hover
   {
       color:#15283c;
       }

    .row1
    {
        margin-top:-30px;
        }
        
       .Labels
       {
           
            font-size:17px;
    font-weight:500;
    color:#000;
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
</style>


</head>
<body>
    <form id="form1" runat="server">
     <div class="container-fluid maincontainer">


             <asp:Panel ID="Panelunits" runat="server">

    <div class="row">
            <div class="col-md-5 mt-1">
            <table>
    <tr width="100%">
    <td width="30%">
      <asp:LinkButton ID="backbotton" class="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
    </td>
    <td width="70%">
       <div class="heading1">
   <h3>Subject Plan</h3>
   </div>
    </td>
    </tr>
    </table>
             
              
            </div>
            
            <div class="col-md-4 mt-1">
          
        
     
            </div>


            <div class="col-md-3 mt-1">
            <table>
            <tr width="100%">

          
           

            <td width="50%">
             <asp:Label ID="Label1" runat="server" class="Labels" Text="Academic Year :"></asp:Label>
             </td>
            <td width="50%">
             <asp:Label ID="lblAcademicyear" class="Labels" runat="server" Text=" Academic Year "></asp:Label>
            </td>
            
            </tr>
            </table>
                
            </div>
             <div class="line">
         </div>
           
             
             
            </div>

            <div class="row">
        
           
          
            <div class="col-md-4">
            <table>
            <tr width="100%">
           
              <td width="80%">
               <asp:Label ID="Label2" runat="server" class="Labels" Text="Subject :"></asp:Label>
                <asp:Label ID="lblsubject" runat="server" class="Labels" Text=" Program "></asp:Label>
              </td>
                <td width="20%"></td>
            </tr>
            </table>
            
            
            </div>
             <div class="col-md-8"></div>
            </div>
           
       

            <div class="row mt-3">
            <div class="col-md-12">
            
               <asp:GridView ID="gridsubjectplan" GridLines="Both"  Width="100%"  
                           AutoGenerateColumns="False" runat="server" CellPadding="0" 
                           ShowHeaderWhenEmpty="true" CssClass="table table-bordered"   OnRowDataBound="OnRowDataBound" OnRowDeleting="OnRowDeleting" DataKeyNames="Subunitid">
                           <Columns>
                           <asp:TemplateField HeaderText="S.No." ControlStyle-Width="10px">
                           <ItemTemplate>
                           <%#Container.DataItemIndex+1 %>
                           </ItemTemplate>
                          </asp:TemplateField>          
                          <asp:BoundField HeaderText="Unit Name"  DataField="UnitName" ReadOnly="true"></asp:BoundField>           
                          <asp:BoundField HeaderText="Description"   DataField="Description" ReadOnly="true"></asp:BoundField> 
                          <asp:BoundField HeaderText="Leacture Duration(in hours)"  DataField="Timeduration" ReadOnly="true"></asp:BoundField> 
                          <asp:TemplateField HeaderText="Documents/Videos">
                          <ItemTemplate >
                          <asp:LinkButton  ID="Documents" runat="server" CommandName="Documents"
                          CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' CssClass="SubjectName"><i class="fa fa-file"></i></asp:LinkButton>       
                           </ItemTemplate>

                           </asp:TemplateField>
                         
                           <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                         <asp:ImageButton ID="lnkDelete" CommandName="Delete" ImageUrl="~/img/url.png"
                         
                          
                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'  OnClientClick="return confirm('Really!Do u want to Delete')" Width="20px" runat="server" />

                            <%-- <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fw-bold" OnClick="lnkDelete_Click"><img src="../ExaminationNImages/img/url.png" width="18px" height="15px" /></asp:LinkButton>--%>
                        </ItemTemplate>
                    </asp:TemplateField>  
                          </Columns>
           
                         </asp:GridView>
            
            </div>
            </div>

            <div class="row">
            <div class="col-md-12 text-end">
               <asp:LinkButton ID="btnaddunit" runat="server" class="addsubject" ><i class="fa-solid fa-plus"></i> Unit</asp:LinkButton>
            </div>
            </div>



            

             </asp:Panel>

               <asp:Panel ID="Paneladdunit" Visible="false" runat="server">

        <div class="row" hidden="false">
           

             <div class="col-md-5 mt-1">
            <table>
    <tr width="100%">
    <td width="30%">
     <asp:LinkButton ID="lbtnprevious" class="backbotton" runat="server"><i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
    </td>
    <td width="70%">
       <div class="heading1">
   <h3>Add Subject Plan</h3>
   </div>
    </td>
    </tr>
    </table>
             
              
            </div>
            
            <div class="col-md-4 mt-1">
          
        
     
            </div>


            <div class="col-md-3 mt-1">
            <table>
            <tr width="100%">

            
           

            <td width="50%">
             <asp:Label ID="Label3" runat="server" class="Labels" Text="Academic Year :"></asp:Label>
             </td>
            <td width="50%">
             <asp:Label ID="lblAcademicaddunit" class="Labels" runat="server" Text=" Academic Year "></asp:Label>
            </td>
            
            </tr>
            </table>
                
            </div>
             <div class="line">
         </div>
           
             
             
            </div>

            <div class="row" hidden="false">
        
           
          
            <div class="col-md-4">
            <table>
            <tr width="100%">
            <td width="20%">
            
            </td>
              <td width="60%">
               <asp:Label ID="Label5" runat="server" class="Labels" Text="Subject :"></asp:Label>
                <asp:Label ID="lblsubjectaddunit" runat="server" class="Labels" Text=" Program "></asp:Label>
              </td>
                <td width="20%"></td>
            </tr>
            </table>
            
            
            </div>
             <div class="col-md-8"></div>
            </div>


              <div class="row mt-3">
            <div class="col-md-12">
            
              <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="Gridview1" runat="server"  AutoGenerateColumns="false"
                OnRowCreated="Gridview1_RowCreated" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="S.No" />
                    <asp:TemplateField HeaderText="Unit Name">
                        <ItemTemplate>
                            <asp:TextBox ID="UnitName" Width="300px" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:TextBox ID="Description"  Width="300px" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lecture Duration(in hours)">
                        <ItemTemplate>
                            <asp:TextBox ID="LectureDuration" Width="300px" runat="server"></asp:TextBox>
                        </ItemTemplate>
                      
                    </asp:TemplateField>
                   
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
            
            </div>
            </div>


               <div class="row">
            <div class="col-md-12 text-center">
               <asp:Button ID="btnsaveplan" class="btn Submit" runat="server" Text="Save" OnClick="btnSave_Click"></asp:Button>
            </div>
            </div>
        </asp:Panel>
      
    </div>
    







    </form>
</body>
</html>
